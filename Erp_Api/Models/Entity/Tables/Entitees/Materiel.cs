using Shared_Erp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_materiel_mat")]
    public class Materiel
    {
        [Key]
        [Column("mat_id")]
        public int Id { get; set; }

        [Column("mat_nom")]
        public string Nom { get; set; } = null!;

        [Column("mat_type")]
        public TypeMateriel Type { get; set; }

        [Column("mat_quantite")]
        public int Quantite { get; set; }

        [Column("mat_projet_id")]
        public int? ProjetId { get; set; }
        public Projet? Projet { get; set; }
    }
}