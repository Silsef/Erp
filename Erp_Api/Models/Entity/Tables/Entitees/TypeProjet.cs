using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_typeprojet_tpo")]
    public class TypeProjet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tpo_id")]
        public int Id { get; set; }
        [Column("tpo_nom")]
        public string Nom { get; set; } = null!;

        public ICollection<Projet> Projets { get; set; } = new List<Projet>();

    }
}
