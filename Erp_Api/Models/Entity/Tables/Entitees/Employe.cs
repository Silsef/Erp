using Erp_Api.Models.Entity.Tables.Jointures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_employe_emp")]
    public class Employe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("emp_id")]
        public int Id { get; set; }
        [Column("emp_nom")]
        public string Nom { get; set; } = null!;
        [Column("emp_prenom")]
        public string Prenom { get; set; } = null!;

        [NotMapped]
        public string Login => $"{Nom[0]}{Prenom}".ToLower();

        [Column("emp_email")]
        public string Email { get; set; } = null!;
        
        [Column("emp_telephone")]
        public string? Telephone { get; set; }

        public Adresse? Adresse { get; set; }

        [Column("emp_datenaissance")]
        public DateTime DateNaissance { get; set; }

        [Column("emp_numsecuritesociale")]
        [RegularExpression("^[0-9]{15}$", ErrorMessage = "Le numéro de sécurité sociale doit contenir exactement 15 chiffres.")]
        public string NumSecuriteSociale { get; set; } = null!;
        public ICollection<EmployeEntreprise> EmployeEntreprises { get; set; } = new List<EmployeEntreprise>();
        public ICollection<A_Pour_Role> Employeroles { get; set; } = new List<A_Pour_Role>();
    }
}
