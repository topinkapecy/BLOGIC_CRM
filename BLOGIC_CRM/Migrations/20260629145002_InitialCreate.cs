using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BLOGIC_CRM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RodneCislo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Vek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Poradci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RodneCislo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Vek = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poradci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smlouvy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvidencniCislo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KlientId = table.Column<int>(type: "int", nullable: false),
                    SpravceSmlouvyId = table.Column<int>(type: "int", nullable: false),
                    DatumUzavreni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumPlatnosti = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumUkonceni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smlouvy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smlouvy_Klienti_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Klienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Smlouvy_Poradci_SpravceSmlouvyId",
                        column: x => x.SpravceSmlouvyId,
                        principalTable: "Poradci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmlouvaPoradci",
                columns: table => new
                {
                    SmlouvaId = table.Column<int>(type: "int", nullable: false),
                    PoradceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmlouvaPoradci", x => new { x.SmlouvaId, x.PoradceId });
                    table.ForeignKey(
                        name: "FK_SmlouvaPoradci_Poradci_PoradceId",
                        column: x => x.PoradceId,
                        principalTable: "Poradci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmlouvaPoradci_Smlouvy_SmlouvaId",
                        column: x => x.SmlouvaId,
                        principalTable: "Smlouvy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmlouvaPoradci_PoradceId",
                table: "SmlouvaPoradci",
                column: "PoradceId");

            migrationBuilder.CreateIndex(
                name: "IX_Smlouvy_KlientId",
                table: "Smlouvy",
                column: "KlientId");

            migrationBuilder.CreateIndex(
                name: "IX_Smlouvy_SpravceSmlouvyId",
                table: "Smlouvy",
                column: "SpravceSmlouvyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmlouvaPoradci");

            migrationBuilder.DropTable(
                name: "Smlouvy");

            migrationBuilder.DropTable(
                name: "Klienti");

            migrationBuilder.DropTable(
                name: "Poradci");
        }
    }
}
