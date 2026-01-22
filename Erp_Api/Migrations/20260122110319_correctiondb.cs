using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctiondb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropColumn(
                name: "TypeContrat",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.RenameColumn(
                name: "pro_priorite",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "Priorite");

            migrationBuilder.RenameColumn(
                name: "pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "EntiteClienteId");

            migrationBuilder.RenameColumn(
                name: "pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "EmployeResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_EntiteClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_EmployeResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EmployeResponsableId",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EntiteClienteId",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "Priorite",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_priorite");

            migrationBuilder.RenameColumn(
                name: "EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entiteclient_id");

            migrationBuilder.RenameColumn(
                name: "EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_employeresponsable_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entiteclient_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_employeresponsable_id");

            migrationBuilder.AddColumn<int>(
                name: "TypeContrat",
                schema: "erp",
                table: "t_e_offre_off",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_employeresponsable_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entiteclient_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
