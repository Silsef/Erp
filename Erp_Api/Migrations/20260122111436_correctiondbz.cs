using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctiondbz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_tache_tac_t_e_employe_emp_tacemployeassigne_id",
                schema: "erp",
                table: "t_e_tache_tac");

            migrationBuilder.RenameColumn(
                name: "tacemployeassigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                newName: "tac_employe_assigne_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_tache_tac_tacemployeassigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                newName: "IX_t_e_tache_tac_tac_employe_assigne_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EmployeResponsableId",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_tache_tac_t_e_employe_emp_tac_employe_assigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                column: "tac_employe_assigne_id",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_tache_tac_t_e_employe_emp_tac_employe_assigne_id",
                schema: "erp",
                table: "t_e_tache_tac");

            migrationBuilder.RenameColumn(
                name: "tac_employe_assigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                newName: "tacemployeassigne_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_tache_tac_tac_employe_assigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                newName: "IX_t_e_tache_tac_tacemployeassigne_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EmployeResponsableId",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_tache_tac_t_e_employe_emp_tacemployeassigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                column: "tacemployeassigne_id",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id");
        }
    }
}
