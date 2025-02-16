using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metafar.Migrations
{
    /// <inheritdoc />
    public partial class MigracionOperaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoInicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoFinal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarjeta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operaciones", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operaciones");
        }
    }
}
