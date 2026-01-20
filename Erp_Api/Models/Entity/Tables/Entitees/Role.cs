using Erp_Api.Models.Entity.Tables.Jointures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Entitees
{
    [Table("t_e_role_rol")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rol_id")]
        public int Id { get; set; }
        [Column("rol_nom")]
        public string Nom { get; set; } = null!;

        public ICollection<A_Pour_Role> EmployeRoles { get; set; } = new List<A_Pour_Role>();

    }
}
