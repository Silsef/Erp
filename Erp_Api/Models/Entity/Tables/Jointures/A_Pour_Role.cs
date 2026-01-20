using Erp_Api.Models.Entity.Tables.Entitees;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erp_Api.Models.Entity.Tables.Jointures
{
    [Table("t_j_a_pour_role_apr")]
    public class A_Pour_Role
    {
        [Column("apr_employe_id")]
        [ForeignKey(nameof(Employe))]
        public int EmployeId { get; set; }

        [Column("apr_role_id")]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public Employe Employe { get; set; } = null!;

        public Role Role { get; set; } = null!;

    }
}
