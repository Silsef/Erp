using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "erp");

            migrationBuilder.CreateTable(
                name: "t_e_employe_emp",
                schema: "erp",
                columns: table => new
                {
                    emp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    emp_nom = table.Column<string>(type: "text", nullable: false),
                    emp_prenom = table.Column<string>(type: "text", nullable: false),
                    emp_email = table.Column<string>(type: "text", nullable: false),
                    emp_telephone = table.Column<string>(type: "text", nullable: true),
                    emp_datenaissance = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    emp_numsecuritesociale = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_employe_emp", x => x.emp_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entreprise_ent",
                schema: "erp",
                columns: table => new
                {
                    ent_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ent_nom = table.Column<string>(type: "text", nullable: false),
                    ent_telephone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entreprise_ent", x => x.ent_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_role_rol",
                schema: "erp",
                columns: table => new
                {
                    rol_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol_nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_role_rol", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_adresse_adr",
                schema: "erp",
                columns: table => new
                {
                    adr_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    adr_rue = table.Column<string>(type: "text", nullable: false),
                    adr_ville = table.Column<string>(type: "text", nullable: false),
                    adr_code_postal = table.Column<string>(type: "text", nullable: false),
                    adr_pays = table.Column<string>(type: "text", nullable: false),
                    EmployeId = table.Column<int>(type: "integer", nullable: true),
                    EntrepriseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_adresse_adr", x => x.adr_id);
                    table.ForeignKey(
                        name: "FK_t_e_adresse_adr_t_e_employe_emp_EmployeId",
                        column: x => x.EmployeId,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_adresse_adr_t_e_entreprise_ent_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalSchema: "erp",
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_employe_entreprise_eem",
                schema: "erp",
                columns: table => new
                {
                    eem_employe_id = table.Column<int>(type: "integer", nullable: false),
                    eem_entreprise_id = table.Column<int>(type: "integer", nullable: false),
                    eem_date_debut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    eem_date_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    eem_poste = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_employe_entreprise_eem", x => new { x.eem_employe_id, x.eem_entreprise_id });
                    table.ForeignKey(
                        name: "FK_t_j_employe_entreprise_eem_t_e_employe_emp_eem_employe_id",
                        column: x => x.eem_employe_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_employe_entreprise_eem_t_e_entreprise_ent_eem_entrepris~",
                        column: x => x.eem_entreprise_id,
                        principalSchema: "erp",
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_a_pour_role_apr",
                schema: "erp",
                columns: table => new
                {
                    apr_employe_id = table.Column<int>(type: "integer", nullable: false),
                    apr_role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_a_pour_role_apr", x => new { x.apr_employe_id, x.apr_role_id });
                    table.ForeignKey(
                        name: "FK_t_j_a_pour_role_apr_t_e_employe_emp_apr_employe_id",
                        column: x => x.apr_employe_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_a_pour_role_apr_t_e_role_rol_apr_role_id",
                        column: x => x.apr_role_id,
                        principalSchema: "erp",
                        principalTable: "t_e_role_rol",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_EmployeId",
                schema: "erp",
                table: "t_e_adresse_adr",
                column: "EmployeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_adresse_adr_EntrepriseId",
                schema: "erp",
                table: "t_e_adresse_adr",
                column: "EntrepriseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_j_a_pour_role_apr_apr_role_id",
                schema: "erp",
                table: "t_j_a_pour_role_apr",
                column: "apr_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_employe_entreprise_eem_eem_entreprise_id",
                schema: "erp",
                table: "t_j_employe_entreprise_eem",
                column: "eem_entreprise_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_adresse_adr",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_j_a_pour_role_apr",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_j_employe_entreprise_eem",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_role_rol",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_employe_emp",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_entreprise_ent",
                schema: "erp");
        }
    }
}
