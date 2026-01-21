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
        public int ? EntrepriseRealisatriceId { get; set; }
        public int ? EntrepriseClienteId { get; set; }
        public int Priorite { get; set; }
    }
}
