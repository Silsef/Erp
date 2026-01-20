using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Shared_Erp/Auth/LoginRequestDTO.cs
namespace Shared_Erp.Auth
{
    public class LoginRequestDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

// Shared_Erp/Auth/LoginResponseDTO.cs
namespace Shared_Erp.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; } = null!;
        public int EmployeId { get; set; }
        public string Email { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
    }
}

// Shared_Erp/Auth/RegisterRequestDTO.cs
namespace Shared_Erp.Auth
{
    public class RegisterRequestDTO
    {
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Telephone { get; set; }
        public DateTime DateNaissance { get; set; }
        public string NumSecuriteSociale { get; set; } = null!;
    }
}