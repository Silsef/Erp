using Shared_Erp.Enums;
using Shared_Erp.Tache;
using Shared_Erp.TypeProjet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Projet
{
    public class ProjetUpdateDTO
    {
        public int Id { get; set; }

        public string Nom { get; set; }
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? EntiteRealisatriceNom { get; set; }
        public string? EntiteClienteNom { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public int? TypeProjetId { get; set; }
        public Statut Statut { get; set; }

    }
}
