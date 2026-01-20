using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_status_sta")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tco_id")]
        public int Id { get; set; }

        [Column("tco_libelle")]
        public string Libelle { get; set; } = null!;
        public ICollection<Candidature> Candidature { get; set; } = new List<Candidature>();

    }
}
