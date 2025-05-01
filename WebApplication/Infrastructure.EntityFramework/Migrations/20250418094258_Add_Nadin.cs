using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Add_Nadin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "sq_doc_types");

            migrationBuilder.CreateSequence(
                name: "sq_docs");

            migrationBuilder.CreateSequence(
                name: "sq_potents");

            migrationBuilder.CreateTable(
                name: "doc_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_doc_types'::regclass)"),
                    name_doc_type = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    comment_doc = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("doc_types_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "potents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_potents'::regclass)"),
                    lastname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    firstname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    date_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    pass = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    dat_reg = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("potents_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "docs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('sq_docs'::regclass)"),
                    name_doc = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    file_name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    docum = table.Column<byte[]>(type: "bytea", nullable: true),
                    comment_doc = table.Column<string>(type: "text", nullable: true),
                    id_doc_type = table.Column<int>(type: "integer", nullable: true),
                    id_event = table.Column<int>(type: "integer", nullable: true),
                    id_event_competition = table.Column<int>(type: "integer", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("docs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_docs_doctypes",
                        column: x => x.id_doc_type,
                        principalTable: "doc_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ukdoctypes_namedoctype",
                table: "doc_types",
                column: "name_doc_type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_docs_id_doc_type",
                table: "docs",
                column: "id_doc_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "docs");

            migrationBuilder.DropTable(
                name: "potents");

            migrationBuilder.DropTable(
                name: "doc_types");

            migrationBuilder.DropSequence(
                name: "sq_doc_types");

            migrationBuilder.DropSequence(
                name: "sq_docs");

            migrationBuilder.DropSequence(
                name: "sq_potents");
        }
    }
}
