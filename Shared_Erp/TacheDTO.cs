using Shared_Erp.Employe;
using Shared_Erp.Projet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp
{
    public class TacheDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string Statut { get; set; } = null!;
        public int Priorite { get; set; }
        public ProjetDTO? ProjetId { get; set; }
        public EmployeDTO? EmployeAssigneId { get; set; }
    }
}
