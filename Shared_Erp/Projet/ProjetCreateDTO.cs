using Shared_Erp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Projet
{
    public   class ProjetCreateDTO
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public int ? EntiteRealisatriceId { get; set; }
        public int ? EntiteClienteId { get; set; }
        public NiveauPriorite Priorite { get; set; }
    }
}
