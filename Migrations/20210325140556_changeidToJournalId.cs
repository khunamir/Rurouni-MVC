using Microsoft.EntityFrameworkCore.Migrations;

namespace Rurouni_v2.Migrations
{
    public partial class changeidToJournalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Journals",
                newName: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalId",
                table: "Journals",
                newName: "id");
        }
    }
}
