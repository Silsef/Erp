using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_candidature_can")]
    public class Candidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("can_id")]
        public int Id { get; set; }

        // FK vers l'offre d'emploi
        [Column("can_offre_emploi_id")]
        public int OffreEmploiId { get; set; }
        public Offre Offre{ get; set; } = null!;

        // Informations du candidat
        [Column("can_nom")]
        public string Nom { get; set; } = null!;

        [Column("can_prenom")]
        public string Prenom { get; set; } = null!;

        [Column("can_email")]
        public string Email { get; set; } = null!;

        [Column("can_telephone")]
        public string? Telephone { get; set; }

        [Column("can_date_naissance")]
        public DateTime? DateNaissance { get; set; }

        [Column("can_date_candidature")]
        public DateTime DateCandidature { get; set; }

        [Column("can_notes")]
        public string? Notes { get; set; }

        [Column("can_pretentions_salariales")]
        public decimal? PretentionsSalariales { get; set; }

        [Column("can_employe_id")]
        public int? EmployeId { get; set; }
        public Employe? Employe { get; set; }
    }
}
