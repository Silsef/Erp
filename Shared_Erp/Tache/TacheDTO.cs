using Shared_Erp.Employe;
using Shared_Erp.Enums;
using Shared_Erp.Projet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Statut Statut { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public int? ProjetId { get; set; }
        public int? EmployeAssigneId { get; set; } 
        public int? TacheParenteId { get; set; }
        public string? ProjetNom { get; set; }
        public string? EmployeAssigneNom { get; set; }
    }

    public class TacheCreateDTO
    {
        [Required(ErrorMessage = "Le nom de la tache est obligatoire")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre 3 et 200 caractères")]
        public string Nom { get; set; } = null!;
        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string? Description { get; set; } 
        [Required(ErrorMessage = "La date de début est obligatoire")]
        public DateTime DateDebut { get; set; }
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
        public Statut Statut { get; set; }
        public NiveauPriorite Priorite { get; set; } 
        public int? ProjetId { get; set; }
        public int? EmployeAssigneId { get; set; }
        public int? TacheParenteId { get; set; }
    }
}
