using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class OrchestraMusicianRelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MusicianOrchestra",
                columns: new[] { "MusiciansId", "OrchestrasId" },
                values: new object[] { 1, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MusicianOrchestra",
                keyColumns: new[] { "MusiciansId", "OrchestrasId" },
                keyValues: new object[] { 1, 4 });
        }
    }
}
