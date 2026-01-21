using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class créationtacheprojetfeuilletemps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_type_entretien_ten_ent_type_entretie~",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropIndex(
                name: "IX_t_e_entreprise_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.DropColumn(
                name: "ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.AddColumn<bool>(
                name: "ent_est_entreprise",
                schema: "erp",
                table: "t_e_entreprise_ent",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
                    tac_nom = table.Column<int>(type: "integer", nullable: false),
                    tac_Description = table.Column<int>(type: "integer", nullable: false),
                    tac_datedebut = table.Column<int>(type: "integer", nullable: false),
                    tac_datefin = table.Column<int>(type: "integer", nullable: false),
                    tac_projet_id = table.Column<int>(type: "integer", nullable: true),
                    tac_priorite = table.Column<int>(type: "integer", nullable: false),
                    tac_tache_parente_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_tache_tac", x => x.tac_id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "t_e_projet_pro",
                schema: "erp");

            migrationBuilder.DropColumn(
                name: "ent_est_entreprise",
                schema: "erp",
                table: "t_e_entreprise_ent");

            migrationBuilder.AddColumn<int>(
                name: "ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_entreprise_ent_ent_type_entretien_id",
                schema: "erp",
                table: "t_e_entreprise_ent",
                column: "ent_type_entretien_id");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_entreprise_ent_t_e_type_entretien_ten_ent_type_entretie~",
                schema: "erp",
                table: "t_e_entreprise_ent",
                column: "ent_type_entretien_id",
                principalSchema: "erp",
                principalTable: "t_e_type_entretien_ten",
                principalColumn: "ten_id");
        }
    }
}
