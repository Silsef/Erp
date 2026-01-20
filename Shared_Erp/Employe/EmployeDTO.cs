using Shared_Erp.Adresse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Employe
{
    public class EmployeDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telephone { get; set; }
        public DateTime DateNaissance { get; set; }
        public string NumSecuriteSociale { get; set; } = null!;
        public AdresseDTO? Adresse { get; set; }

        public List<EmployeEntrepriseDTO> EmployeEntreprises { get; set; } = new List<EmployeEntrepriseDTO>();
    }
}
