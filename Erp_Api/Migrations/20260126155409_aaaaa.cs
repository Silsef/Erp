using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class aaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_feuilletemps_ft_ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                column: "ft_tache_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_feuilletemps_ft_t_e_tache_tac_ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                column: "ft_tache_id",
                principalSchema: "erp",
                principalTable: "t_e_tache_tac",
                principalColumn: "tac_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_feuilletemps_ft_t_e_tache_tac_ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft");

            migrationBuilder.DropIndex(
                name: "IX_t_e_feuilletemps_ft_ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft");

            migrationBuilder.DropColumn(
                name: "ft_tache_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft");
        }
    }
}
