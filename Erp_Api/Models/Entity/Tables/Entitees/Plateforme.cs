using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_plateforme_pla")]
    public class Plateforme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tco_id")]
        public int Id { get; set; }
        
        [Column("pla_libelle")]
        public string Libelle { get; set; } = null!;
        public ICollection<Candidature> Candidature { get; set; } = new List<Candidature>();

    }
}
