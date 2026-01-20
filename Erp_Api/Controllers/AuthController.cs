using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shared_Erp.Auth;
using BCrypt.Net;

namespace Erp_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeManager _employeManager;
        private readonly IConfiguration _configuration;

        public AuthController(EmployeManager employeManager, IConfiguration configuration)
        {
            _employeManager = employeManager;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            // Récupérer l'employé par email
            var employe = await _employeManager.GetByEmailLoginAsync(request.Identifiant);

            if (employe == null)
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });

            // Vérifier le mot de passe avec BCrypt
            if (!BCrypt.Net.BCrypt.Verify(request.Password, employe.PasswordHash))
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });

            // Générer le token JWT
            var token = GenerateJwtToken(employe.Id, employe.Email, employe.Nom, employe.Prenom);

            // Créer le cookie HTTP-only
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // HTTPS uniquement en production
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(24)
            });

            return Ok(new LoginResponseDTO
            {
                Token = token,
                EmployeId = employe.Id,
                Email = employe.Email,
                Nom = employe.Nom,
                Prenom = employe.Prenom
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDTO>> Register([FromBody] RegisterRequestDTO request)
        {
            // Vérifier si l'email existe déjà
            var existingUser = await _employeManager.GetByEmailLoginAsync(request.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Un compte avec cet email existe déjà" });

            // Hasher le mot de passe
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Créer le nouvel employé
            var employe = new Erp_Api.Models.Entity.Tables.Entitees.Employe
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Email = request.Email,
                PasswordHash = passwordHash,
                Login = $"{request.Nom[0]}{request.Prenom}".ToLower(),
                Telephone = request.Telephone,
                DateNaissance = request.DateNaissance,
                NumSecuriteSociale = request.NumSecuriteSociale
            };

            await _employeManager.AddAsync(employe);

            // Générer le token JWT
            var token = GenerateJwtToken(employe.Id, employe.Email, employe.Nom, employe.Prenom);

            // Créer le cookie HTTP-only
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(24)
            });

            return CreatedAtAction(nameof(Register), new LoginResponseDTO
            {
                Token = token,
                EmployeId = employe.Id,
                Email = employe.Email,
                Nom = employe.Nom,
                Prenom = employe.Prenom
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return Ok(new { message = "Déconnexion réussie" });
        }

        [HttpGet]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDTO>> ValidateToken()
        {
            var token = Request.Cookies["access_token"];

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["App:Jwt:SecretKey"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["App:Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["App:Jwt:Audience"],
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var employeId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var nom = jwtToken.Claims.First(x => x.Type == "nom").Value;
                var prenom = jwtToken.Claims.First(x => x.Type == "prenom").Value;

                return Ok(new LoginResponseDTO
                {
                    Token = token,
                    EmployeId = employeId,
                    Email = email,
                    Nom = nom,
                    Prenom = prenom
                });
            }
            catch
            {
                return Unauthorized();
            }
        }

        private string GenerateJwtToken(int employeId, string email, string nom, string prenom)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["App:Jwt:SecretKey"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", employeId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("nom", nom),
                new Claim("prenom", prenom),
                new Claim(ClaimTypes.Role, "Authorized")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["App:Jwt:Issuer"],
                audience: _configuration["App:Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}