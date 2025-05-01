using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Potent_Gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "potents",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "gender",
                table: "potents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1);
        }
    }
}
