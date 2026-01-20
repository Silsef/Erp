using Shared_Erp.Auth;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Erp_Blazor.Service.WebServices
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la connexion : {ex.Message}");
                return null;
            }
        }

        public async Task<LoginResponseDTO?> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Register", registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'inscription : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("api/Auth/Logout", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO?> ValidateTokenAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Auth/ValidateToken");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}