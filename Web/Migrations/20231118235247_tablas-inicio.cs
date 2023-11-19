using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class tablasinicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Edad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edad", x => x.ID);
                });

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
                name: "Genero",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaloAdoptante",
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
                    table.PrimaryKey("PK_MaloAdoptante", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vacuna",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacuna", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FutAdoptado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImagemGato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneroRefId = table.Column<int>(type: "int", nullable: true),
                    EdadRefId = table.Column<int>(type: "int", nullable: true),
                    VacunaRefId = table.Column<int>(type: "int", nullable: true),
                    EnfermedadRefId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_FutAdoptado_Enfermedad_EnfermedadRefId",
                        column: x => x.EnfermedadRefId,
                        principalTable: "Enfermedad",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FutAdoptado_Genero_GeneroRefId",
                        column: x => x.GeneroRefId,
                        principalTable: "Genero",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FutAdoptado_Vacuna_VacunaRefId",
                        column: x => x.VacunaRefId,
                        principalTable: "Vacuna",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_EdadRefId",
                table: "FutAdoptado",
                column: "EdadRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_EnfermedadRefId",
                table: "FutAdoptado",
                column: "EnfermedadRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_GeneroRefId",
                table: "FutAdoptado",
                column: "GeneroRefId");

            migrationBuilder.CreateIndex(
                name: "IX_FutAdoptado_VacunaRefId",
                table: "FutAdoptado",
                column: "VacunaRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FutAdoptado");

            migrationBuilder.DropTable(
                name: "FutAdoptante");

            migrationBuilder.DropTable(
                name: "MaloAdoptante");

            migrationBuilder.DropTable(
                name: "Edad");

            migrationBuilder.DropTable(
                name: "Enfermedad");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Vacuna");
        }
    }
}
