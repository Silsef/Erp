using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionentrepriseversentite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_adresse_adr_t_e_entreprise_ent_EntrepriseId",
                schema: "erp",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_entretien_ent_t_e_type_entretien_ten_ent_type_entretien~",
                schema: "erp",
                table: "t_e_entretien_ent");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_offre_off_t_e_entreprise_ent_off_entreprise_id",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_offre_off_t_e_type_contrat_tco_off_type_contrat_id",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropTable(
                name: "t_e_type_contrat_tco",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_type_entretien_ten",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_j_employe_entreprise_eem",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_entreprise_ent",
                schema: "erp");

            migrationBuilder.DropIndex(
                name: "IX_t_e_offre_off_off_entreprise_id",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropIndex(
                name: "IX_t_e_offre_off_off_type_contrat_id",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropIndex(
                name: "IX_t_e_entretien_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entretien_ent");

            migrationBuilder.RenameColumn(
                name: "off_entreprise_id",
                schema: "erp",
                table: "t_e_offre_off",
                newName: "off_entite_id");

            migrationBuilder.AddColumn<int>(
                name: "EntiteId",
                schema: "erp",
                table: "t_e_offre_off",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeContrat",
                schema: "erp",
                table: "t_e_offre_off",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeEntretien",
                schema: "erp",
                table: "t_e_entretien_ent",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "t_e_entite_ent",
                schema: "erp",
                columns: table => new
                {
                    ent_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ent_nom = table.Column<string>(type: "text", nullable: false),
                    ent_telephone = table.Column<string>(type: "text", nullable: true),
                    ent_est_entreprise = table.Column<bool>(type: "boolean", nullable: false),
                    ent_est_silsefnapa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entite_ent", x => x.ent_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_employe_entite_eem",
                schema: "erp",
                columns: table => new
                {
                    eem_employe_id = table.Column<int>(type: "integer", nullable: false),
                    eem_Entite_id = table.Column<int>(type: "integer", nullable: false),
                    eem_date_debut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    eem_date_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    eem_poste = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_employe_entite_eem", x => new { x.eem_employe_id, x.eem_Entite_id });
                    table.ForeignKey(
                        name: "FK_t_j_employe_entite_eem_t_e_employe_emp_eem_employe_id",
                        column: x => x.eem_employe_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_employe_entite_eem_t_e_entite_ent_eem_Entite_id",
                        column: x => x.eem_Entite_id,
                        principalSchema: "erp",
                        principalTable: "t_e_entite_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_offre_off_EntiteId",
                schema: "erp",
                table: "t_e_offre_off",
                column: "EntiteId");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_employe_entite_eem_eem_Entite_id",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                column: "eem_Entite_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_adresse_adr_t_e_entite_ent_EntrepriseId",
                schema: "erp",
                table: "t_e_adresse_adr",
                column: "EntrepriseId",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_offre_off_t_e_entite_ent_EntiteId",
                schema: "erp",
                table: "t_e_offre_off",
                column: "EntiteId",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_adresse_adr_t_e_entite_ent_EntrepriseId",
                schema: "erp",
                table: "t_e_adresse_adr");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_offre_off_t_e_entite_ent_EntiteId",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropTable(
                name: "t_j_employe_entite_eem",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_entite_ent",
                schema: "erp");

            migrationBuilder.DropIndex(
                name: "IX_t_e_offre_off_EntiteId",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropColumn(
                name: "EntiteId",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropColumn(
                name: "TypeContrat",
                schema: "erp",
                table: "t_e_offre_off");

            migrationBuilder.DropColumn(
                name: "TypeEntretien",
                schema: "erp",
                table: "t_e_entretien_ent");

            migrationBuilder.RenameColumn(
                name: "off_entite_id",
                schema: "erp",
                table: "t_e_offre_off",
                newName: "off_entreprise_id");

            migrationBuilder.CreateTable(
                name: "t_e_entreprise_ent",
                schema: "erp",
                columns: table => new
                {
                    ent_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ent_est_entreprise = table.Column<bool>(type: "boolean", nullable: false),
                    ent_est_silsefnapa = table.Column<bool>(type: "boolean", nullable: false),
                    ent_nom = table.Column<string>(type: "text", nullable: false),
                    ent_telephone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entreprise_ent", x => x.ent_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_type_contrat_tco",
                schema: "erp",
                columns: table => new
                {
                    tco_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tco_libelle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_type_contrat_tco", x => x.tco_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_type_entretien_ten",
                schema: "erp",
                columns: table => new
                {
                    ten_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ten_libelle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_type_entretien_ten", x => x.ten_id);
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

            migrationBuilder.CreateIndex(
                name: "IX_t_e_offre_off_off_entreprise_id",
                schema: "erp",
                table: "t_e_offre_off",
                column: "off_entreprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_offre_off_off_type_contrat_id",
                schema: "erp",
                table: "t_e_offre_off",
                column: "off_type_contrat_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entretien_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entretien_ent",
                column: "ent_type_entretien_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_employe_entreprise_eem_eem_entreprise_id",
                schema: "erp",
                table: "t_j_employe_entreprise_eem",
                column: "eem_entreprise_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_adresse_adr_t_e_entreprise_ent_EntrepriseId",
                schema: "erp",
                table: "t_e_adresse_adr",
                column: "EntrepriseId",
                principalSchema: "erp",
                principalTable: "t_e_entreprise_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_entretien_ent_t_e_type_entretien_ten_ent_type_entretien~",
                schema: "erp",
                table: "t_e_entretien_ent",
                column: "ent_type_entretien_id",
                principalSchema: "erp",
                principalTable: "t_e_type_entretien_ten",
                principalColumn: "ten_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_offre_off_t_e_entreprise_ent_off_entreprise_id",
                schema: "erp",
                table: "t_e_offre_off",
                column: "off_entreprise_id",
                principalSchema: "erp",
                principalTable: "t_e_entreprise_ent",
                principalColumn: "ent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_offre_off_t_e_type_contrat_tco_off_type_contrat_id",
                schema: "erp",
                table: "t_e_offre_off",
                column: "off_type_contrat_id",
                principalSchema: "erp",
                principalTable: "t_e_type_contrat_tco",
                principalColumn: "tco_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entreprise_id",
                principalSchema: "erp",
                principalTable: "t_e_entreprise_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entrepriseclient_id",
                principalSchema: "erp",
                principalTable: "t_e_entreprise_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
