using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class updated_docs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "docum",
                table: "Docs");

            migrationBuilder.DropColumn(
                name: "name_doc",
                table: "Docs");

            migrationBuilder.AddColumn<string>(
                name: "DocId",
                table: "Docs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocId",
                table: "Docs");

            migrationBuilder.AddColumn<byte[]>(
                name: "docum",
                table: "Docs",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_doc",
                table: "Docs",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
