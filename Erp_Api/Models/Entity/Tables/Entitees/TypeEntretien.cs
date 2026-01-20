using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_type_entretien_ten")]
    public class TypeEntretien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ten_id")]
        public int Id { get; set; }

        [Column("ten_libelle")]
        public string Libelle { get; set; } = null!;
    }
}
