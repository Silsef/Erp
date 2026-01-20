using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Offre
{
    public class OffreDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal? SalaireMin { get; set; }
        public decimal? SalaireMax { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime? DateCloture { get; set; }
        public bool EstActive { get; set; }
        public int NombrePostes { get; set; }
        public int? EntrepriseId { get; set; }
        public string? NomEntreprise { get; set; }
        public int TypeContratId { get; set; }
        public string? TypeContratLibelle { get; set; }
    }
}
