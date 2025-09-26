using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addedDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_competition_potent",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_ApplicationStatuses_StatusId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Roles_RoleId",
                table: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_EditorId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "potents");

            migrationBuilder.DropColumn(
                name: "pass",
                table: "potents");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FinishActualControlDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FinishRegistrationDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StartActualControlDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "IsCaptainConfirmed",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "SetStatusId",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "docs");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "MaxComandSize",
                table: "Competitions");

            migrationBuilder.RenameTable(
                name: "potents",
                newName: "Potents");

            migrationBuilder.RenameTable(
                name: "docs",
                newName: "Docs");

            migrationBuilder.RenameTable(
                name: "doc_types",
                newName: "DocTypes");

            migrationBuilder.RenameColumn(
                name: "StartRegistrationDate",
                table: "Events",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "EventParticipants",
                newName: "ParticipantCompetitionId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "EventParticipants",
                newName: "ApplicationStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_StatusId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_ParticipantCompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_RoleId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_ApplicationStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_docs_id_event",
                table: "Docs",
                newName: "IX_Docs_id_event");

            migrationBuilder.RenameIndex(
                name: "IX_docs_id_doc_type",
                table: "Docs",
                newName: "IX_Docs_id_doc_type");

            migrationBuilder.RenameIndex(
                name: "IX_docs_id_competition",
                table: "Docs",
                newName: "IX_Docs_id_competition");

            migrationBuilder.RenameColumn(
                name: "MinComandSize",
                table: "Competitions",
                newName: "PotentId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Competitions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_PotentId",
                table: "Competitions",
                column: "PotentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitions_Potents_PotentId",
                table: "Competitions",
                column: "PotentId",
                principalTable: "Potents",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_ApplicationStatuses_ApplicationStatusId",
                table: "EventParticipants",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Competitions_ParticipantCompetitionId",
                table: "EventParticipants",
                column: "ParticipantCompetitionId",
                principalTable: "Competitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitions_Potents_PotentId",
                table: "Competitions");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_ApplicationStatuses_ApplicationStatusId",
                table: "EventParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_Competitions_ParticipantCompetitionId",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Competitions_PotentId",
                table: "Competitions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Competitions");

            migrationBuilder.RenameTable(
                name: "Potents",
                newName: "potents");

            migrationBuilder.RenameTable(
                name: "Docs",
                newName: "docs");

            migrationBuilder.RenameTable(
                name: "DocTypes",
                newName: "doc_types");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Events",
                newName: "StartRegistrationDate");

            migrationBuilder.RenameColumn(
                name: "ParticipantCompetitionId",
                table: "EventParticipants",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "ApplicationStatusId",
                table: "EventParticipants",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_ParticipantCompetitionId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EventParticipants_ApplicationStatusId",
                table: "EventParticipants",
                newName: "IX_EventParticipants_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Docs_id_event",
                table: "docs",
                newName: "IX_docs_id_event");

            migrationBuilder.RenameIndex(
                name: "IX_Docs_id_doc_type",
                table: "docs",
                newName: "IX_docs_id_doc_type");

            migrationBuilder.RenameIndex(
                name: "IX_Docs_id_competition",
                table: "docs",
                newName: "IX_docs_id_competition");

            migrationBuilder.RenameColumn(
                name: "PotentId",
                table: "Competitions",
                newName: "MinComandSize");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "potents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "pass",
                table: "potents",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishActualControlDate",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishRegistrationDate",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartActualControlDate",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "EventParticipants",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "EventParticipants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCaptainConfirmed",
                table: "EventParticipants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EventParticipants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SetStatusId",
                table: "EventParticipants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "docs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EditorId",
                table: "Competitions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Competitions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxComandSize",
                table: "Competitions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_teams'::regclass)"),
                    CaptainId = table.Column<int>(type: "integer", nullable: false),
                    CompetitionOfEventId = table.Column<int>(type: "integer", nullable: false),
                    ConsidererId = table.Column<int>(type: "integer", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RejectNote = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("team_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_Teams_Competitions_CompetitionOfEventId",
                        column: x => x.CompetitionOfEventId,
                        principalTable: "Competitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_competition_potent_by_captain",
                        column: x => x.CaptainId,
                        principalTable: "potents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_competition_potent_by_considerer",
                        column: x => x.ConsidererId,
                        principalTable: "potents",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_EditorId",
                table: "Competitions",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CaptainId",
                table: "Teams",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompetitionOfEventId",
                table: "Teams",
                column: "CompetitionOfEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ConsidererId",
                table: "Teams",
                column: "ConsidererId");

            migrationBuilder.AddForeignKey(
                name: "fk_competition_potent",
                table: "Competitions",
                column: "EditorId",
                principalTable: "potents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_ApplicationStatuses_StatusId",
                table: "EventParticipants",
                column: "StatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_Roles_RoleId",
                table: "EventParticipants",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
