using Shared_Erp.Enums;
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
        public string Nom { get; set; }

        [Column("tac_Description")]
        public string Description { get; set; }

        [Column("tac_datedebut")]
        public DateTime DateDebut { get; set; }

        [Column("tac_datefin")]
        public DateTime DateFin { get; set; }

        [Column("tac_statut")]
        public StatutTache Statut { get; set; } = StatutTache.AFaire; 

        [Column("tac_projet_id")]
        public int? ProjetId { get; set; }

        // La tâche "Maman"
        [ForeignKey(nameof(ProjetId))]
        public Projet? Projet { get; set; }


        [Column("tacemployeassigne_id")]
        public int? EmployeAssigneId { get; set; }

        [ForeignKey(nameof(EmployeAssigneId))]
        public Employe? EmployeAssigne { get; set; }


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
