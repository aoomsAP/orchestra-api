using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Musicians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Instrument = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orchestras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conductor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryCode = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orchestras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orchestras_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusicianOrchestra",
                columns: table => new
                {
                    MusiciansId = table.Column<int>(type: "int", nullable: false),
                    OrchestrasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianOrchestra", x => new { x.MusiciansId, x.OrchestrasId });
                    table.ForeignKey(
                        name: "FK_MusicianOrchestra_Musicians_MusiciansId",
                        column: x => x.MusiciansId,
                        principalTable: "Musicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianOrchestra_Orchestras_OrchestrasId",
                        column: x => x.OrchestrasId,
                        principalTable: "Orchestras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "AU", "Australia" },
                    { "BE", "Belgium" },
                    { "CO", "Colombia" },
                    { "DE", "Germany" },
                    { "HU", "Hungary" },
                    { "LB", "Lebanon" },
                    { "NL", "Netherlands" },
                    { "SG", "Singapore" },
                    { "TW", "Taiwan" },
                    { "UK", "United Kingdom" },
                    { "ZA", "South Africa" }
                });

            migrationBuilder.InsertData(
                table: "Musicians",
                columns: new[] { "Id", "Instrument", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Sylvia Huang" },
                    { 2, 0, "Maria Kouznetsova" },
                    { 3, 0, "Christophe Mourguiart" },
                    { 4, 0, "Mona Verhas" },
                    { 5, 0, "Orsolya Horváth" },
                    { 6, 0, "Miki Tsunoda" },
                    { 7, 1, "Sander Geerts" },
                    { 8, 1, "Elaine Ng" },
                    { 9, 2, "Raphael Bell" },
                    { 10, 2, "Marc Vossen" },
                    { 11, 3, "Ioan Baranga" },
                    { 12, 3, "Vlad Rațiu" },
                    { 13, 4, "Aldo Baerten" },
                    { 14, 4, "Peter Verhoyen" },
                    { 15, 5, "Louis Baumann" },
                    { 16, 6, "Nele Delafonteyne" },
                    { 17, 7, "Oliver Engels" },
                    { 18, 9, "Michaela Bužková" },
                    { 19, 10, "Alain De Rudder" },
                    { 20, 11, "Daniel Quiles Cascant" },
                    { 21, 12, "Bernd van Echelpoel" },
                    { 22, 13, "Pieterjan Vrankx" },
                    { 23, 13, "Cristiano Menegazzo" }
                });

            migrationBuilder.InsertData(
                table: "Orchestras",
                columns: new[] { "Id", "Conductor", "CountryCode", "Name" },
                values: new object[,]
                {
                    { 1, "Elim Chan", "BE", "Antwerp Symphony Orchestra" },
                    { 2, "Lorenzo Viotti", "NL", "Nederlands Philharmonisch Orkest" },
                    { 3, "Kazushi Ono", "BE", "Brussels Philharmonic" },
                    { 4, "Alain Altinoglu", "BE", "Symfonieorkest van de Munt" },
                    { 5, "Erik Sluys", "BE", "Brussels Sinfonietta" },
                    { 6, "Candida Thompson", "NL", "Amsterdam Sinfonietta" },
                    { 7, "Edward Gardner", "UK", "London Philharmonic Orchestra" },
                    { 8, "Lahav Shani", "DE", "Münchner Philharmoniker" }
                });

            migrationBuilder.InsertData(
                table: "MusicianOrchestra",
                columns: new[] { "MusiciansId", "OrchestrasId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 4, 1 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 5, 1 },
                    { 6, 1 },
                    { 6, 6 },
                    { 7, 1 },
                    { 7, 7 },
                    { 8, 1 },
                    { 9, 1 },
                    { 9, 8 },
                    { 10, 1 },
                    { 10, 2 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianOrchestra_OrchestrasId",
                table: "MusicianOrchestra",
                column: "OrchestrasId");

            migrationBuilder.CreateIndex(
                name: "IX_Orchestras_CountryCode",
                table: "Orchestras",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianOrchestra");

            migrationBuilder.DropTable(
                name: "Musicians");

            migrationBuilder.DropTable(
                name: "Orchestras");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
