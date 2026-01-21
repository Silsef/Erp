using Erp_Api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_tache_tac")]
    public class Tache
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tac_id")]
        public int Id { get; set; }

        [Column("tac_nom")]
        public int Nom { get; set; }

        [Column("tac_Description")]
        public int Description { get; set; }

        [Column("tac_datedebut")]
        public int DateDebut { get; set; }

        [Column("tac_datefin")]
        public int DateFin { get; set; }

        [Column("tac_projet_id")]
        public int? ProjetId { get; set; }

        // La tâche "Maman"
        [ForeignKey(nameof(ProjetId))]
        public Projet? Projet { get; set; }



        [Column("tac_priorite")]
        public NiveauPriorite Priorite { get; set; } = NiveauPriorite.Normale;

        [Column("tac_tache_parente_id")]
        public int? TacheParenteId { get; set; }

        // La tâche "Maman"
        [ForeignKey(nameof(TacheParenteId))]
        public Tache? TacheParente { get; set; }

        // Les tâches "Enfants" (Sous-tâches)
        public ICollection<Tache> SousTaches { get; set; } = new List<Tache>();
    }
}
