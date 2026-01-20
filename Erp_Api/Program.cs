using Erp_Api.Models.Entity;
using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Erp_Api.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
            context.Token = context.Request.Cookies["access_token"];
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