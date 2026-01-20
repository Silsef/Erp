using Erp_Blazor;
using Erp_Blazor.Service.Security;
using Erp_Blazor.Service.WebServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration de l'URL de l'API
var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7223";

// HttpClient configuré avec l'URL de base
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiUrl)
});

// LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Services d'authentification
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>());

// Ajouter les services d'autorisation
builder.Services.AddAuthorizationCore();

// Ajouter vos services métier
builder.Services.AddScoped<EmployeWebService>();

await builder.Build().RunAsync();