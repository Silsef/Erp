
// ============================================
// TACHE.CS - VERSION CORRIGÉE
// ============================================
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
        public string Nom { get; set; } = null!;

        [Column("tac_Description")]
        public string Description { get; set; } = null!;

        [Column("tac_datedebut")]
        public DateTime DateDebut { get; set; }

        [Column("tac_datefin")]
        public DateTime? DateFin { get; set; }

        [Column("tac_statut")]
        public StatutTache Statut { get; set; } = StatutTache.A_Faire;

        [Column("tac_priorite")]
        public NiveauPriorite Priorite { get; set; } = NiveauPriorite.Normale;

        // ========================================
        // RELATION AVEC PROJET
        // ========================================
        [Column("tac_projet_id")]
        public int? ProjetId { get; set; }

        [ForeignKey(nameof(ProjetId))]
        public Projet? Projet { get; set; }

        // ========================================
        // RELATION AVEC EMPLOYE ASSIGNÉ
        // ========================================
        [Column("tac_employe_assigne_id")]
        public int? EmployeAssigneId { get; set; }

        [ForeignKey(nameof(EmployeAssigneId))]
        public Employe? EmployeAssigne { get; set; }

        // ========================================
        // AUTO-RÉFÉRENCE (Tâche parente/enfants)
        // ========================================
        [Column("tac_tache_parente_id")]
        public int? TacheParenteId { get; set; }

        [ForeignKey(nameof(TacheParenteId))]
        public Tache? TacheParente { get; set; }

        // Les sous-tâches (relation inverse)
        public ICollection<Tache> SousTaches { get; set; } = new List<Tache>();
    }
}
