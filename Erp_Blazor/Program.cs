using Blazored.LocalStorage;
using Erp_Blazor;
using Erp_Blazor.Service.Interfaces;
using Erp_Blazor.Service.Security;
using Erp_Blazor.Service.WebServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuration de l'URL de l'API
var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7223";

// Ajouter le localStorage
builder.Services.AddBlazoredLocalStorage();

// Enregistrer le AuthTokenHandler comme scoped
builder.Services.AddScoped<AuthTokenHandler>();

// Configurer HttpClient avec le handler qui ajoute automatiquement le token
builder.Services.AddScoped(sp =>
{
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var handler = new AuthTokenHandler(localStorage)
    {
        InnerHandler = new HttpClientHandler()
    };

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(apiUrl)
    };
});

// Services d'authentification
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

// Services métier
builder.Services.AddScoped<IProjetService, ProjetWebService>();
builder.Services.AddScoped<IEntiteService, EntiteWebService>();
builder.Services.AddScoped<IEmployeService, EmployeWebService>();
builder.Services.AddScoped<IOffreService, OffreWebService>();
builder.Services.AddScoped<ICandidatureService, CandidatureWebService>();
builder.Services.AddScoped<IEntretienService, EntretienWebService>();
builder.Services.AddScoped<IFeuilleTempsService, FeuilleTempsWebService>();
builder.Services.AddScoped<ITacheService, TacheWebService>();

await builder.Build().RunAsync();