using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionnomdansprojet1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "pro_entitecliente_id");

            migrationBuilder.RenameColumn(
                name: "pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entiterealisatrice_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entitecliente_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entite_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entiterealisatrice_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entitecliente_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entitecliente_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiterealisatrice_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entiterealisatrice_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entitecliente_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiterealisatrice_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "pro_entiterealisatrice_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entite_id");

            migrationBuilder.RenameColumn(
                name: "pro_entitecliente_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entiteclient_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entiterealisatrice_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entite_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entitecliente_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entiteclient_id");

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
    }
}
