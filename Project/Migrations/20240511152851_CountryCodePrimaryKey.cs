using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class CountryCodePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orchestras_Countries_CountryId",
                table: "Orchestras");

            migrationBuilder.DropIndex(
                name: "IX_Orchestras_CountryId",
                table: "Orchestras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Orchestras");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Orchestras",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Countries",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Orchestras_CountryCode",
                table: "Orchestras",
                column: "CountryCode");

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

            migrationBuilder.DropIndex(
                name: "IX_Orchestras_CountryCode",
                table: "Orchestras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Orchestras");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Orchestras",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Countries",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orchestras_CountryId",
                table: "Orchestras",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orchestras_Countries_CountryId",
                table: "Orchestras",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
