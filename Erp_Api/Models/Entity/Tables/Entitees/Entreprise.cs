using Erp_Api.Models.Entity.Tables.Jointures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_entreprise_ent")]
    public class Entreprise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ent_id")]
        public int Id { get; set; }
        [Column("ent_nom")]
        public string Nom { get; set; } = null!;

        public Adresse? Adresse { get; set; }

        [Column("ent_telephone")]
        public string? Telephone { get; set; }

        [Column("ent_type_entretien_id")]
        public int? TypeEntretienId { get; set; }
        public TypeEntretien? TypeEntretien { get; set; }

        public ICollection<EmployeEntreprise> EmployeEntreprises { get; set; } = new List<EmployeEntreprise>();

    }
}
