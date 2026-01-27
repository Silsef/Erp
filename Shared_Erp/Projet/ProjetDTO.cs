using Shared_Erp.Enums;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
using Shared_Erp.Tache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Erp.TypeProjet;

namespace Shared_Erp.Projet
{
    public class ProjetDTO
    {
        public int Id { get; set; }
        
        public string Nom { get; set; } 
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? EntiteRealisatriceNom { get; set; }
        public string? EntiteClienteNom { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public Statut Statut { get; set; }
        public string? TypeProjetNom { get; set; }
        public int ? TypeProjetId { get; set; }
        public List<TacheDTO> Taches { get; set; } = new List<TacheDTO>();
        public string ? EmployeResponsablePrenom { get; set; }

    }
}
