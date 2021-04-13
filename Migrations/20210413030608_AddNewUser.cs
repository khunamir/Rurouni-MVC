using Microsoft.EntityFrameworkCore.Migrations;

namespace Rurouni_v2.Migrations
{
    public partial class AddNewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Journals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_UserId",
                table: "Journals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_AspNetUsers_UserId",
                table: "Journals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_AspNetUsers_UserId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_UserId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Journals");
        }
    }
}
