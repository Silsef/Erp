using Shared_Erp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_offre_off")]
    public class Offre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("off_id")]
        public int Id { get; set; }

        [Column("off_titre")]
        public string Titre { get; set; } = null!;

        [Column("off_description")]
        public string Description { get; set; } = null!;

        [Column("off_salaire_min")]
        public decimal? SalaireMin { get; set; }

        [Column("off_salaire_max")]
        public decimal? SalaireMax { get; set; }

        [Column("off_date_publication")]
        public DateTime DatePublication { get; set; }

        [Column("off_date_cloture")]
        public DateTime? DateCloture { get; set; }

        [Column("off_est_active")]
        public bool EstActive { get; set; } = true;

        [Column("off_nombre_postes")]
        public int NombrePostes { get; set; } = 1;

        [Column("off_entite_id")]
        public int? EntrepriseId { get; set; }
        public Entite? Entite { get; set; }

        // 🔽 FK TypeContrat
        [Column("off_type_contrat_id")]
        public int TypeContratId { get; set; }

        public TypeContrat TypeContrat { get; set; }

        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();

    }
}
