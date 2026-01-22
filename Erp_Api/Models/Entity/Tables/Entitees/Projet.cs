using Shared_Erp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_projet_pro")]
    public class Projet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pro_id")]
        public int Id { get; set; }

        [Column("pro_nom")]
        public string Nom { get; set; } = null!;

        [Column("pro_description")]
        public string? Description { get; set; }

        [Column("pro_date_debut")]
        public DateTime DateDebut { get; set; }

        [Column("pro_date_fin")]
        public DateTime? DateFin { get; set; }

        [Column("pro_entite_id")]
        public int? EntiteRealisatriceId { get; set; }
        public Entite? EntiteRealisatrice { get; set; }

        [Column("pro_entiteclient_id")]
        public int? EntiteClienteId { get; set; }
        public Entite? EntiteCliente { get; set; }

        [Column("pro_priorite")]
        public NiveauPriorite Priorite { get; set; } = NiveauPriorite.Normale;
        public ICollection<Tache> Taches { get; set; } = new List<Tache>();


    }
}
