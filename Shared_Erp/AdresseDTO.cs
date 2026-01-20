using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp
{
    public class AdresseDTO
    {
        public int Id { get; set; }
        public string Rue { get; set; } = null!;
        public string Ville { get; set; } = null!;
        public string CodePostal { get; set; } = null!;
        public string Pays { get; set; } = null!;
    }
}
