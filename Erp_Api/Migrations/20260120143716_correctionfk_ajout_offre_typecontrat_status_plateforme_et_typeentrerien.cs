using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionfk_ajout_offre_typecontrat_status_plateforme_et_typeentrerien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_plateforme_pla",
                schema: "erp",
                columns: table => new
                {
                    tco_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pla_libelle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_plateforme_pla", x => x.tco_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_status_sta",
                schema: "erp",
                columns: table => new
                {
                    sta_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sta_libelle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_status_sta", x => x.sta_id);
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
                name: "t_e_offre_off",
                schema: "erp",
                columns: table => new
                {
                    off_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    off_titre = table.Column<string>(type: "text", nullable: false),
                    off_description = table.Column<string>(type: "text", nullable: false),
                    off_salaire_min = table.Column<decimal>(type: "numeric", nullable: true),
                    off_salaire_max = table.Column<decimal>(type: "numeric", nullable: true),
                    off_date_publication = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    off_date_cloture = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    off_est_active = table.Column<bool>(type: "boolean", nullable: false),
                    off_nombre_postes = table.Column<int>(type: "integer", nullable: false),
                    off_entreprise_id = table.Column<int>(type: "integer", nullable: true),
                    off_type_contrat_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_offre_off", x => x.off_id);
                    table.ForeignKey(
                        name: "FK_t_e_offre_off_t_e_entreprise_ent_off_entreprise_id",
                        column: x => x.off_entreprise_id,
                        principalSchema: "erp",
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id");
                    table.ForeignKey(
                        name: "FK_t_e_offre_off_t_e_type_contrat_tco_off_type_contrat_id",
                        column: x => x.off_type_contrat_id,
                        principalSchema: "erp",
                        principalTable: "t_e_type_contrat_tco",
                        principalColumn: "tco_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_candidature_can",
                schema: "erp",
                columns: table => new
                {
                    can_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    can_offre_emploi_id = table.Column<int>(type: "integer", nullable: false),
                    can_nom = table.Column<string>(type: "text", nullable: false),
                    can_prenom = table.Column<string>(type: "text", nullable: false),
                    can_email = table.Column<string>(type: "text", nullable: false),
                    can_telephone = table.Column<string>(type: "text", nullable: true),
                    can_date_naissance = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    can_date_candidature = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    can_notes = table.Column<string>(type: "text", nullable: true),
                    can_pretentions_salariales = table.Column<decimal>(type: "numeric", nullable: true),
                    can_employe_id = table.Column<int>(type: "integer", nullable: true),
                    can_status_id = table.Column<int>(type: "integer", nullable: false),
                    can_plateforme_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_candidature_can", x => x.can_id);
                    table.ForeignKey(
                        name: "FK_t_e_candidature_can_t_e_employe_emp_can_employe_id",
                        column: x => x.can_employe_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id");
                    table.ForeignKey(
                        name: "FK_t_e_candidature_can_t_e_offre_off_can_offre_emploi_id",
                        column: x => x.can_offre_emploi_id,
                        principalSchema: "erp",
                        principalTable: "t_e_offre_off",
                        principalColumn: "off_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_candidature_can_t_e_plateforme_pla_can_plateforme_id",
                        column: x => x.can_plateforme_id,
                        principalSchema: "erp",
                        principalTable: "t_e_plateforme_pla",
                        principalColumn: "tco_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_candidature_can_t_e_status_sta_can_status_id",
                        column: x => x.can_status_id,
                        principalSchema: "erp",
                        principalTable: "t_e_status_sta",
                        principalColumn: "sta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_e_entretien_ent",
                schema: "erp",
                columns: table => new
                {
                    ent_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ent_candidature_id = table.Column<int>(type: "integer", nullable: false),
                    ent_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ent_interviewer_id = table.Column<int>(type: "integer", nullable: true),
                    ent_notes = table.Column<string>(type: "text", nullable: true),
                    ent_type_entretien_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entretien_ent", x => x.ent_id);
                    table.ForeignKey(
                        name: "FK_t_e_entretien_ent_t_e_candidature_can_ent_candidature_id",
                        column: x => x.ent_candidature_id,
                        principalSchema: "erp",
                        principalTable: "t_e_candidature_can",
                        principalColumn: "can_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_entretien_ent_t_e_employe_emp_ent_interviewer_id",
                        column: x => x.ent_interviewer_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id");
                    table.ForeignKey(
                        name: "FK_t_e_entretien_ent_t_e_type_entretien_ten_ent_type_entretien~",
                        column: x => x.ent_type_entretien_id,
                        principalSchema: "erp",
                        principalTable: "t_e_type_entretien_ten",
                        principalColumn: "ten_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_candidature_can_can_employe_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_candidature_can_can_offre_emploi_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_offre_emploi_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_candidature_can_can_plateforme_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_plateforme_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_candidature_can_can_status_id",
                schema: "erp",
                table: "t_e_candidature_can",
                column: "can_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entretien_ent_ent_candidature_id",
                schema: "erp",
                table: "t_e_entretien_ent",
                column: "ent_candidature_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entretien_ent_ent_interviewer_id",
                schema: "erp",
                table: "t_e_entretien_ent",
                column: "ent_interviewer_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entretien_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entretien_ent",
                column: "ent_type_entretien_id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_entretien_ent",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_candidature_can",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_type_entretien_ten",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_offre_off",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_plateforme_pla",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_status_sta",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_type_contrat_tco",
                schema: "erp");
        }
    }
}
