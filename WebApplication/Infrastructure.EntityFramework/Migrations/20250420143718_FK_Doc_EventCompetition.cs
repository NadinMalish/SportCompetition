using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FK_Doc_EventCompetition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_event_competition",
                table: "docs",
                newName: "id_competition");

            migrationBuilder.CreateIndex(
                name: "IX_docs_id_competition",
                table: "docs",
                column: "id_competition");

            migrationBuilder.CreateIndex(
                name: "IX_docs_id_event",
                table: "docs",
                column: "id_event");

            migrationBuilder.AddForeignKey(
                name: "fk_docs_competitions",
                table: "docs",
                column: "id_competition",
                principalTable: "Competitions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_docs_events",
                table: "docs",
                column: "id_event",
                principalTable: "Events",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_docs_competitions",
                table: "docs");

            migrationBuilder.DropForeignKey(
                name: "fk_docs_events",
                table: "docs");

            migrationBuilder.DropIndex(
                name: "IX_docs_id_competition",
                table: "docs");

            migrationBuilder.DropIndex(
                name: "IX_docs_id_event",
                table: "docs");

            migrationBuilder.RenameColumn(
                name: "id_competition",
                table: "docs",
                newName: "id_event_competition");
        }
    }
}
