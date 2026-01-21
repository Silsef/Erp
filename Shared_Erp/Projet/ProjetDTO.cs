using Shared_Erp.Enums;
using Shared_Erp.Employe;
using Shared_Erp.Entreprise;
using Shared_Erp.Enums;
using Shared_Erp.Tache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Projet
{
    public class ProjetDTO
    {
        public int Id { get; set; }
        
        public string Nom { get; set; } 
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public EntrepriseDTO? EntrepriseRealisatrice { get; set; }
        public EntrepriseDTO? EntrepriseCliente { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public List<TacheDTO> Taches { get; set; } = new List<TacheDTO>();

    }
}
