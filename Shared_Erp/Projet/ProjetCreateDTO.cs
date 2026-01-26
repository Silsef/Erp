using System.ComponentModel.DataAnnotations;
using Shared_Erp.Enums;

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

        [Range(1, int.MaxValue, ErrorMessage = "L'entité réalisatrice doit être sélectionnée")]
        public int? EntiteRealisatriceId { get; set; }
        public int? EntiteClienteId { get; set; }
        public NiveauPriorite Priorite { get; set; } = NiveauPriorite.Normale;
        public int? TypeProjetId { get; set; }
    }
}