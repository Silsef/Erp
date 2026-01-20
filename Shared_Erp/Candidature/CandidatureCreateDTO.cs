using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Candidature
{
    public class CandidatureCreateDTO
    {
        public int OffreEmploiId { get; set; }
        public string Nom { get; set; } = null!;
        public string Prenom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telephone { get; set; }
        public DateTime? DateNaissance { get; set; }
        public DateTime DateCandidature { get; set; }
        public string? Notes { get; set; }
        public decimal? PretentionsSalariales { get; set; }
        public int? EmployeId { get; set; }
    }
}
