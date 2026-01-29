using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ent_telephone",
                schema: "erp",
                table: "t_e_entite_ent",
                newName: "ent_contact");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ent_contact",
                schema: "erp",
                table: "t_e_entite_ent",
                newName: "ent_telephone");
        }
    }
}
