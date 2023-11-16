using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class FutAdoptado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enfermedad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enfermedad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FutAdoptado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagemPelicula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clasificacion = table.Column<int>(type: "int", nullable: true),
                    GeneroRefId = table.Column<int>(type: "int", nullable: true),
                    EdadRefId = table.Column<int>(type: "int", nullable: true),
                    VacunaRefId = table.Column<int>(type: "int", nullable: true),
                    CiudadRefId = table.Column<int>(type: "int", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutAdoptado", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FutAdoptado_Edad_EdadRefId",
                        column: x => x.EdadRefId,
                        principalTable: "Edad",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FutAdoptado_Genero_GeneroRefId",
                        column: x => x.GeneroRefId,
                        principalTable: "Genero",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FutAdoptado_Vacuna_CiudadRefId",
                        column: x => x.CiudadRefId,
                        principalTable: "Vacuna",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FutAdoptante",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreyApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Interes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FutAdoptante", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaloAdoptantes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreyApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaloAdoptantes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_CiudadRefId",
                table: "FutAdoptado",
                column: "CiudadRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_EdadRefId",
                table: "FutAdoptado",
                column: "EdadRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_GeneroRefId",
                table: "FutAdoptado",
                column: "GeneroRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enfermedad");

            migrationBuilder.DropTable(
                name: "FutAdoptado");

            migrationBuilder.DropTable(
                name: "FutAdoptante");

            migrationBuilder.DropTable(
                name: "MaloAdoptantes");
        }
    }
}
