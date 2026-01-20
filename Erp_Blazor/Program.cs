using Blazored.LocalStorage;
using Erp_Blazor.Service.Security;
using Erp_Blazor.Service.WebServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Erp_Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configuration de l'URL de base de l'API
            var apiBaseUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7223";

            // HttpClient simple - les cookies sont gérés automatiquement par le navigateur
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(apiBaseUrl)
            });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();

            // Enregistrer les services
            builder.Services.AddScoped<AuthService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}