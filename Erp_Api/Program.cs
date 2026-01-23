using Erp_Api.Mapper;
using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Erp API", Version = "v1" });

    // 1. Définir le schéma de sécurité (Bearer)
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Entrez un jeton JWT valide",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // 2. Ajouter l'exigence de sécurité globale
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Configuration de la base de données
builder.Services.AddDbContext<ErpBdContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocaleConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Managers
builder.Services.AddScoped<EmployeManager>();
builder.Services.AddScoped<OffreManager>();
builder.Services.AddScoped<CandidatureManager>();
builder.Services.AddScoped<EntretienManager>();
builder.Services.AddScoped<ProjetManager>();
builder.Services.AddScoped<EntiteManager>();
builder.Services.AddScoped<TacheManager>();
builder.Services.AddScoped<FeuilleTempsManager>();
builder.Services.AddScoped<TacheManager>();

// Configuration JWT Authentication
var jwtSettings = builder.Configuration.GetSection("App:Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

    // Support des cookies pour JWT
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var cookie = context.Request.Cookies["access_token"];

            if (!string.IsNullOrEmpty(cookie))
            {
                context.Token = cookie;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// *** CONFIGURATION CORS - IMPORTANT ***
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7129",  // URL HTTPS Blazor
                "http://localhost:5001"    // URL HTTP Blazor
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();  // Important pour les cookies
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// *** UTILISER CORS - DOIT ÊTRE AVANT Authentication/Authorization ***
app.UseCors("AllowBlazorClient");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();