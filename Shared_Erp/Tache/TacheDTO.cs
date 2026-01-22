using Shared_Erp.Employe;
using Shared_Erp.Enums;
using Shared_Erp.Projet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Tache
{
    public class TacheDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public StatutTache Statut { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public int? ProjetId { get; set; }
        public int? EmployeAssigneId { get; set; } 
        public int? TacheParenteId { get; set; }
        public string? ProjetNom { get; set; }
        public string? EmployeAssigneNom { get; set; }
    }

    public class TacheCreateDTO
    {
        public string Nom { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public int? ProjetId { get; set; }
        public int? EmployeAssigneId { get; set; }
        public int? TacheParenteId { get; set; }
    }

    public class TacheUpdateDTO
    {
        public string Nom { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public StatutTache Statut { get; set; }
        public NiveauPriorite Priorite { get; set; } // Changé de int à NiveauPriorite
        public int? ProjetId { get; set; }
        public int? EmployeAssigneId { get; set; }
        public int? TacheParenteId { get; set; }
    }
}
