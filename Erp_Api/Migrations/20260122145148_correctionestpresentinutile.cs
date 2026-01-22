using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionestpresentinutile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ft_est_present",
                schema: "erp",
                table: "t_e_feuilletemps_ft");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ft_est_present",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
