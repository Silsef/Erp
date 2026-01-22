using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class ajoutresponsableprojet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_projet_pro_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_employeresponsable_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_employeresponsable_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropIndex(
                name: "IX_t_e_projet_pro_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropColumn(
                name: "pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro");
        }
    }
}
