using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionentrepriseversentite1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_j_employe_entite_eem_t_e_entite_ent_eem_Entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem");

            migrationBuilder.RenameColumn(
                name: "eem_Entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                newName: "eem_entite_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_employe_entite_eem_eem_Entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                newName: "IX_t_j_employe_entite_eem_eem_entite_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_employe_entite_eem_t_e_entite_ent_eem_entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                column: "eem_entite_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_j_employe_entite_eem_t_e_entite_ent_eem_entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem");

            migrationBuilder.RenameColumn(
                name: "eem_entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                newName: "eem_Entite_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_j_employe_entite_eem_eem_entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                newName: "IX_t_j_employe_entite_eem_eem_Entite_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_j_employe_entite_eem_t_e_entite_ent_eem_Entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                column: "eem_Entite_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
