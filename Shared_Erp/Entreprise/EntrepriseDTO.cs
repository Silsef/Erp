using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Entreprise
{
    public class EntrepriseDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;

    }

    public class EntrepriseCreateDTO
    {
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;
    }

    public class EntrepriseUpdateDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;
    }
}
