using Shared_Erp.Attributes;
using Shared_Erp.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared_Erp.Projet
{
    public class ProjetCreateDTO
    {
        [Required(ErrorMessage = "Le nom du projet est obligatoire")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Le nom doit contenir entre 3 et 200 caractères")]
        public string Nom { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire")]
        public DateTime DateDebut { get; set; }

        [DateSuperieure(nameof(DateDebut), ErrorMessage = "La fin doit être après le début")]
        public DateTime? DateFin { get; set; }

        public int? EntiteRealisatriceId { get; set; }

        [Required(ErrorMessage = "L'entité cliente est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "L'entité cliente doit être sélectionnée")]
        public int? EntiteClienteId { get; set; }
        public NiveauPriorite Priorite { get; set; } = NiveauPriorite.Normale;


        [Required(ErrorMessage = "Le type de projet est obligatoire")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un type de projet")]
        public int? TypeProjetId { get; set; }
    }
}