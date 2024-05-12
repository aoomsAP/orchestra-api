using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CountryReferenceOnOrchestraAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orchestras_Countries_CountryCode",
                table: "Orchestras");

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Orchestras",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Conductor",
                table: "Orchestras",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Orchestras_Countries_CountryCode",
                table: "Orchestras",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orchestras_Countries_CountryCode",
                table: "Orchestras");

            migrationBuilder.UpdateData(
                table: "Orchestras",
                keyColumn: "CountryCode",
                keyValue: null,
                column: "CountryCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CountryCode",
                table: "Orchestras",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orchestras_Countries_CountryCode",
                table: "Orchestras",
                column: "CountryCode",
                principalTable: "Countries",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
