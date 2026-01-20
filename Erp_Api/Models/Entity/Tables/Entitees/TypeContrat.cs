using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_type_contrat_tco")]
    public class TypeContrat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tco_id")]
        public int Id { get; set; }

        [Column("tco_libelle")]
        public string Libelle { get; set; } = null!;

        public ICollection<Offre> Offres { get; set; } = new List<Offre>();
    }
}
