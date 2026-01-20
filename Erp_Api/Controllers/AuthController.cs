using Erp_Api.Models.Repository.Managers.Models_Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Shared_Erp.Auth;

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
            var employe = await _employeManager.GetByEmailAsync(request.Email);

            if (employe == null)
                return Unauthorized(new { message = "Email ou mot de passe incorrect" });

            // TODO: Vérifier le mot de passe haché
            // Pour l'instant, on simule la vérification
            // if (!VerifyPassword(request.Password, employe.PasswordHash))
            //     return Unauthorized(new { message = "Email ou mot de passe incorrect" });

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