using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class TEst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Conductor",
                table: "Orchestras",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                    { 1, 4 },
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "AU");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "CO");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "HU");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "LB");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "SG");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "TW");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "ZA");

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 9, 8 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 20, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 21, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 23, 1 });

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Musicians",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orchestras",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "BE");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "DE");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "NL");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Code",
                keyValue: "UK");

            migrationBuilder.UpdateData(
                table: "Orchestras",
                keyColumn: "Conductor",
                keyValue: null,
                column: "Conductor",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Conductor",
                table: "Orchestras",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
