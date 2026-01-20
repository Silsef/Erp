using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_entretien_ent")]
    public class Entretien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ent_id")]
        public int Id { get; set; }

        // FK vers Candidature
        [Column("ent_candidature_id")]
        public int CandidatureId { get; set; }
        public Candidature Candidature { get; set; } = null!;

        [Column("ent_date")]
        public DateTime DateEntretien { get; set; }

        [Column("ent_interviewer_id")]
        public int? InterviewerId { get; set; }
        public Employe? Interviewer { get; set; }

        [Column("ent_notes")]
        public string? Notes { get; set; }

        [Column("ent_type_entretien_id")]
        public int TypeEntretienId { get; set; }

        [ForeignKey("TypeEntretienId")]
        public TypeEntretien TypeEntretien { get; set; } = null!;
    }
}
