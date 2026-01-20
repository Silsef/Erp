using Shared_Erp.Adresse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Employe
{
    public class EmployeEntrepriseDTO
    {
        public int EmployeId { get; set; }
        public int EntrepriseId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? Poste { get; set; }

        //
        public string NomEntreprise { get; set; }
        public AdresseDTO? Adresse { get; set; }
        public string? Telephone { get; set; }


    }
}
