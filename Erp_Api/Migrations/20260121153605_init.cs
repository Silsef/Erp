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
                    emp_login = table.Column<string>(type: "text", nullable: false),
                    emp_password_hash = table.Column<string>(type: "text", nullable: false),
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
                    ent_telephone = table.Column<string>(type: "text", nullable: true),
                    ent_est_entreprise = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_entreprise_ent", x => x.ent_id);
                });

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
                name: "t_e_projet_pro",
                schema: "erp",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pro_nom = table.Column<string>(type: "text", nullable: false),
                    pro_description = table.Column<string>(type: "text", nullable: true),
                    pro_date_debut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pro_date_fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    pro_entreprise_id = table.Column<int>(type: "integer", nullable: true),
                    pro_entrepriseclient_id = table.Column<int>(type: "integer", nullable: true),
                    pro_priorite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_projet_pro", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entreprise_id",
                        column: x => x.pro_entreprise_id,
                        principalSchema: "erp",
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_t_e_projet_pro_t_e_entreprise_ent_pro_entrepriseclient_id",
                        column: x => x.pro_entrepriseclient_id,
                        principalSchema: "erp",
                        principalTable: "t_e_entreprise_ent",
                        principalColumn: "ent_id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "t_e_feuilletemps_ft",
                schema: "erp",
                columns: table => new
                {
                    ft_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ft_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ft_est_matin = table.Column<bool>(type: "boolean", nullable: false),
                    ft_est_present = table.Column<bool>(type: "boolean", nullable: false),
                    ft_employe_id = table.Column<int>(type: "integer", nullable: false),
                    ft_projet_id = table.Column<int>(type: "integer", nullable: true),
                    ft_commentaire = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_feuilletemps_ft", x => x.ft_id);
                    table.ForeignKey(
                        name: "FK_t_e_feuilletemps_ft_t_e_employe_emp_ft_employe_id",
                        column: x => x.ft_employe_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_feuilletemps_ft_t_e_projet_pro_ft_projet_id",
                        column: x => x.ft_projet_id,
                        principalSchema: "erp",
                        principalTable: "t_e_projet_pro",
                        principalColumn: "pro_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_materiel_mat",
                schema: "erp",
                columns: table => new
                {
                    mat_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mat_nom = table.Column<string>(type: "text", nullable: false),
                    mat_type = table.Column<int>(type: "integer", nullable: false),
                    mat_quantite = table.Column<int>(type: "integer", nullable: false),
                    mat_projet_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_materiel_mat", x => x.mat_id);
                    table.ForeignKey(
                        name: "FK_t_e_materiel_mat_t_e_projet_pro_mat_projet_id",
                        column: x => x.mat_projet_id,
                        principalSchema: "erp",
                        principalTable: "t_e_projet_pro",
                        principalColumn: "pro_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_tache_tac",
                schema: "erp",
                columns: table => new
                {
                    tac_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tac_nom = table.Column<string>(type: "text", nullable: false),
                    tac_Description = table.Column<string>(type: "text", nullable: false),
                    tac_datedebut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tac_datefin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tac_statut = table.Column<int>(type: "integer", nullable: false),
                    tac_projet_id = table.Column<int>(type: "integer", nullable: true),
                    tacemployeassigne_id = table.Column<int>(type: "integer", nullable: true),
                    tac_priorite = table.Column<int>(type: "integer", nullable: false),
                    tac_tache_parente_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_tache_tac", x => x.tac_id);
                    table.ForeignKey(
                        name: "FK_t_e_tache_tac_t_e_employe_emp_tacemployeassigne_id",
                        column: x => x.tacemployeassigne_id,
                        principalSchema: "erp",
                        principalTable: "t_e_employe_emp",
                        principalColumn: "emp_id");
                    table.ForeignKey(
                        name: "FK_t_e_tache_tac_t_e_projet_pro_tac_projet_id",
                        column: x => x.tac_projet_id,
                        principalSchema: "erp",
                        principalTable: "t_e_projet_pro",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_tache_tac_t_e_tache_tac_tac_tache_parente_id",
                        column: x => x.tac_tache_parente_id,
                        principalSchema: "erp",
                        principalTable: "t_e_tache_tac",
                        principalColumn: "tac_id",
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
                    can_status_id = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    can_plateforme_id = table.Column<int>(type: "integer", nullable: true)
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
                        principalColumn: "tco_id");
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
                name: "IX_t_e_feuilletemps_ft_ft_employe_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                column: "ft_employe_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_feuilletemps_ft_ft_projet_id",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                column: "ft_projet_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_materiel_mat_mat_projet_id",
                schema: "erp",
                table: "t_e_materiel_mat",
                column: "mat_projet_id");

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
                name: "IX_t_e_projet_pro_pro_entreprise_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entreprise_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_projet_pro_pro_entrepriseclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entrepriseclient_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_tache_tac_tac_projet_id",
                schema: "erp",
                table: "t_e_tache_tac",
                column: "tac_projet_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_tache_tac_tac_tache_parente_id",
                schema: "erp",
                table: "t_e_tache_tac",
                column: "tac_tache_parente_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_tache_tac_tacemployeassigne_id",
                schema: "erp",
                table: "t_e_tache_tac",
                column: "tacemployeassigne_id");

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
                name: "t_e_entretien_ent",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_feuilletemps_ft",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_materiel_mat",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_tache_tac",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_j_a_pour_role_apr",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_j_employe_entreprise_eem",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_candidature_can",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_type_entretien_ten",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_projet_pro",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_role_rol",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_employe_emp",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_offre_off",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_plateforme_pla",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_entreprise_ent",
                schema: "erp");

            migrationBuilder.DropTable(
                name: "t_e_type_contrat_tco",
                schema: "erp");
        }
    }
}
