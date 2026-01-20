using Shared_Erp.Adresse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Employe
{
    public class EntrepriseDTO
    {
        public int id { get; set; }
        public string Nom { get; set; } = null!;
        public AdresseDTO? Adresse { get; set; }
        public string? Telephone { get; set; }
    }
}
