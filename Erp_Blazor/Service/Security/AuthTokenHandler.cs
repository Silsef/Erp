using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Erp_Blazor.Service.Security
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthTokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Récupérer le token depuis le localStorage
            string? token = await _localStorage.GetItemAsStringAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Nettoyer les guillemets éventuels
                token = token.Replace("\"", "");

                // Ajouter le token dans l'en-tête Authorization
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}