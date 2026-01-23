using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class aaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "t_e_typeprojet_tpo",
                schema: "erp",
                columns: table => new
                {
                    tpo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tpo_nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_typeprojet_tpo", x => x.tpo_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_projet_pro_pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_typeprojet_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_typeprojet_tpo_pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_typeprojet_id",
                principalSchema: "erp",
                principalTable: "t_e_typeprojet_tpo",
                principalColumn: "tpo_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_typeprojet_tpo_pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropTable(
                name: "t_e_typeprojet_tpo",
                schema: "erp");

            migrationBuilder.DropIndex(
                name: "IX_t_e_projet_pro_pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropColumn(
                name: "pro_typeprojet_id",
                schema: "erp",
                table: "t_e_projet_pro");
        }
    }
}
