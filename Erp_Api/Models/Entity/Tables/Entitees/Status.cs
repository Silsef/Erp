using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_status_sta")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("sta_id")]
        public int Id { get; set; }

        [Column("sta_libelle")]
        public string Libelle { get; set; } = null!;
        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();

    }
}
