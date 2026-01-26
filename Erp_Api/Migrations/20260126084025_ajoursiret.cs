using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class ajoursiret : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ent_siret",
                schema: "erp",
                table: "t_e_entite_ent",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ent_siret",
                schema: "erp",
                table: "t_e_entite_ent");
        }
    }
}
