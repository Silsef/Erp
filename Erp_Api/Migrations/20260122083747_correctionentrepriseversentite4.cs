using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionentrepriseversentite4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entiteclient_id");

            migrationBuilder.RenameColumn(
                name: "pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entite_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entiteclient_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entite_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entite_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entrepriseclient_id");

            migrationBuilder.RenameColumn(
                name: "pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entreprise_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entrepriseclient_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entreprise_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entreprise_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entrepriseclient_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
