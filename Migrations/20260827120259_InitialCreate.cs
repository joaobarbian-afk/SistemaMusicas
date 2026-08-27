using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMusicas.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeMusica = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LinkYoutube = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musicas");
        }
    }
}
