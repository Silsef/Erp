using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ent_est_silsefnapa",
                schema: "erp",
                table: "t_e_entreprise_ent",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ent_est_silsefnapa",
                schema: "erp",
                table: "t_e_entreprise_ent");
        }
    }
}
