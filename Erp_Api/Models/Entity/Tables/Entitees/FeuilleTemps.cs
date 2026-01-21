using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_feuilletemps_ft")]
    public class FeuilleTemps
    {
        [Key]
        [Column("ft_id")]
        public int Id { get; set; }

        [Column("ft_date")]
        public DateTime Date { get; set; }

        [Column("ft_est_matin")]
        public bool EstMatin { get; set; } // Vrai = Matin, Faux = Après-midi

        [Column("ft_est_present")]
        public bool EstPresent { get; set; } // A validé sa présence

        // Qui ?
        [Column("ft_employe_id")]
        public int EmployeId { get; set; }
        public Employe Employe { get; set; } = null!;

        // Sur quel projet ? (Null si absent ou tâche interne générale)
        [Column("ft_projet_id")]
        public int? ProjetId { get; set; }
        public Projet? Projet { get; set; }

        [Column("ft_commentaire")]
        public string? Commentaire { get; set; }
    }
}