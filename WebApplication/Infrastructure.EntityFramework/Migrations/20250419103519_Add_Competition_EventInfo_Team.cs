using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Add_Competition_EventInfo_Team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "sq_competitions");

            migrationBuilder.CreateSequence(
                name: "sq_events");

            migrationBuilder.CreateSequence(
                name: "sq_teams");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_events'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartRegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishRegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartActualControlDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FinishActualControlDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrganizerId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("event_info_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_entity_info_potent",
                        column: x => x.OrganizerId,
                        principalTable: "potents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_competitions'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompetitionType = table.Column<int>(type: "integer", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MinComandSize = table.Column<int>(type: "integer", nullable: true),
                    MaxComandSize = table.Column<int>(type: "integer", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    EditorId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("competition_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_competition_event_info",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_competition_potent",
                        column: x => x.EditorId,
                        principalTable: "potents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_teams'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RegistryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CaptainId = table.Column<int>(type: "integer", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: true),
                    RejectNote = table.Column<string>(type: "text", nullable: true),
                    ConsidererId = table.Column<int>(type: "integer", nullable: true),
                    CompetitionOfEventId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "IX_Competitions_EventId",
                table: "Competitions",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropSequence(
                name: "sq_competitions");

            migrationBuilder.DropSequence(
                name: "sq_events");

            migrationBuilder.DropSequence(
                name: "sq_teams");
        }
    }
}
