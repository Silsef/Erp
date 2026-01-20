using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Entretien
{
    public class EntretienCreateDTO
    {
        public int CandidatureId { get; set; }
        public DateTime DateEntretien { get; set; }
        public int? InterviewerId { get; set; }
        public string? Notes { get; set; }
    }
}
