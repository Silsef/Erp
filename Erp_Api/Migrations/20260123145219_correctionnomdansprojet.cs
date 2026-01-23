using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp_Api.Migrations
{
    /// <inheritdoc />
    public partial class correctionnomdansprojet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "Priorite",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_priorite");

            migrationBuilder.RenameColumn(
                name: "EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_entiteclient_id");

            migrationBuilder.RenameColumn(
                name: "EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "pro_employeresponsable_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_entiteclient_id");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_pro_employeresponsable_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "eem_date_fin",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "eem_date_debut",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "tac_datefin",
                schema: "erp",
                table: "t_e_tache_tac",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "tac_datedebut",
                schema: "erp",
                table: "t_e_tache_tac",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pro_date_fin",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "pro_date_debut",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "off_date_publication",
                schema: "erp",
                table: "t_e_offre_off",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "off_date_cloture",
                schema: "erp",
                table: "t_e_offre_off",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ft_date",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ent_date",
                schema: "erp",
                table: "t_e_entretien_ent",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "emp_datenaissance",
                schema: "erp",
                table: "t_e_employe_emp",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "can_date_naissance",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "can_date_candidature",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_employeresponsable_id",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "pro_entiteclient_id",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro");

            migrationBuilder.RenameColumn(
                name: "pro_priorite",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "Priorite");

            migrationBuilder.RenameColumn(
                name: "pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "EntiteClienteId");

            migrationBuilder.RenameColumn(
                name: "pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "EmployeResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_entiteclient_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_EntiteClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_t_e_projet_pro_pro_employeresponsable_id",
                schema: "erp",
                table: "t_e_projet_pro",
                newName: "IX_t_e_projet_pro_EmployeResponsableId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "eem_date_fin",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "eem_date_debut",
                schema: "erp",
                table: "t_j_employe_entite_eem",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "tac_datefin",
                schema: "erp",
                table: "t_e_tache_tac",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "tac_datedebut",
                schema: "erp",
                table: "t_e_tache_tac",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "pro_date_fin",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "pro_date_debut",
                schema: "erp",
                table: "t_e_projet_pro",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "off_date_publication",
                schema: "erp",
                table: "t_e_offre_off",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "off_date_cloture",
                schema: "erp",
                table: "t_e_offre_off",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ft_date",
                schema: "erp",
                table: "t_e_feuilletemps_ft",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ent_date",
                schema: "erp",
                table: "t_e_entretien_ent",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "emp_datenaissance",
                schema: "erp",
                table: "t_e_employe_emp",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "can_date_naissance",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "can_date_candidature",
                schema: "erp",
                table: "t_e_candidature_can",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_employe_emp_EmployeResponsableId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EmployeResponsableId",
                principalSchema: "erp",
                principalTable: "t_e_employe_emp",
                principalColumn: "emp_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_projet_pro_t_e_entite_ent_EntiteClienteId",
                schema: "erp",
                table: "t_e_projet_pro",
                column: "EntiteClienteId",
                principalSchema: "erp",
                principalTable: "t_e_entite_ent",
                principalColumn: "ent_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
