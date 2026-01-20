using Erp_Api.Models.Entity.Tables.Entitees;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Jointures
{
    [Table("t_j_employe_entreprise_eem")]
    public class EmployeEntreprise
    {
        [Column("eem_employe_id")]
        [ForeignKey(nameof(Employe))]
        public int EmployeId { get; set; }

        [Column("eem_entreprise_id")]
        [ForeignKey(nameof(Entreprise))]
        public int EntrepriseId { get; set; }

        [Column("eem_date_debut")]
        public DateTime DateDebut { get; set; }

        [Column("eem_date_fin")]
        public DateTime? DateFin { get; set; }

        [Column("eem_poste")]
        public string? Poste { get; set; }

        // Navigation properties
        public Employe Employe { get; set; } = null!;
        public Entreprise Entreprise { get; set; } = null!;
    }
}