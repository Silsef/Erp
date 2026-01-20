using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correction_offre_entretien_candidature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_candidature_can_t_e_plateforme_pla_can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can");

            migrationBuilder.AddColumn<int>(
                name: "ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "can_status_id",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent",
                column: "ent_type_entretien_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_candidature_can_t_e_plateforme_pla_can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_plateforme_id",
                principalSchema: "erp",
                principalTable: "t_e_plateforme_pla",
                principalColumn: "tco_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_type_entretien_ten_ent_type_entretie~",
                schema: "erp",
                table: "t_e_entreprise_ent",
                column: "ent_type_entretien_id",
                principalSchema: "erp",
                principalTable: "t_e_type_entretien_ten",
                principalColumn: "ten_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_candidature_can_t_e_plateforme_pla_can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_type_entretien_ten_ent_type_entretie~",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropIndex(
                name: "IX_t_e_entreprise_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropColumn(
                name: "ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.AlterColumn<int>(
                name: "can_status_id",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_candidature_can_t_e_plateforme_pla_can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_plateforme_id",
                principalSchema: "erp",
                principalTable: "t_e_plateforme_pla",
                principalColumn: "tco_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
