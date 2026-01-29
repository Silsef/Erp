using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Erp_Blazor.Service.Security
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // 1. Récupérer le token (sans guillemets)
            string? token = await _localStorage.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    token = token.Replace("\"", "");
                    var claims = ParseClaimsFromJwt(token);

                    var expirationClaim = claims.FirstOrDefault(c => c.Type == "exp");
                    if (expirationClaim != null)
                    {
                        var exp = long.Parse(expirationClaim.Value);
                        var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

                        if (expDate <= DateTime.UtcNow)
                        {
                            // Le token est expiré ! On nettoie et on retourne "non connecté"
                            await _localStorage.RemoveItemAsync("authToken");
                            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                        }
                    }

                    identity = new ClaimsIdentity(claims, "jwt");
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors du parsing du token : {ex.Message}");
                    await _localStorage.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        // Méthode utilitaire pour extraire les claims du JWT
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                foreach (var kvp in keyValuePairs)
                {
                    if (kvp.Value != null)
                    {
                        if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var item in element.EnumerateArray())
                            {
                                AddClaim(claims, kvp.Key, item.ToString());
                            }
                        }
                        else
                        {
                            AddClaim(claims, kvp.Key, kvp.Value.ToString());
                        }
                    }
                }
            }

            return claims;
        }

        private static void AddClaim(List<Claim> claims, string key, string value)
        {
            switch (key.ToLower())
            {
                case "email":
                case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress":
                    claims.Add(new Claim(ClaimTypes.Email, value));
                    claims.Add(new Claim(ClaimTypes.Name, value));
                    break;
                case "nom":
                    claims.Add(new Claim("nom", value));
                    break;
                case "prenom":
                    claims.Add(new Claim("prenom", value));
                    break;
                case "id":
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, value));
                    break;
                case "role":
                case "http://schemas.microsoft.com/ws/2008/06/identity/claims/role":
                    claims.Add(new Claim(ClaimTypes.Role, value));
                    break;
                default:
                    claims.Add(new Claim(key, value));
                    break;
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        // Méthode pour déconnecter l'utilisateur
        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}