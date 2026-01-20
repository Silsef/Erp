using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Entretien
{
    public class EntretienDTO
    {
        public int Id { get; set; }
        public int CandidatureId { get; set; }
        public string NomCandidat { get; set; } = null!;
        public string PrenomCandidat { get; set; } = null!;
        public int OffreId { get; set; }
        public string OffreTitre { get; set; } = null!;
        public DateTime DateEntretien { get; set; }
        public int? InterviewerId { get; set; }
        public string? Nominterviewer { get; set; }
        public string? Notes { get; set; }
    }
}
