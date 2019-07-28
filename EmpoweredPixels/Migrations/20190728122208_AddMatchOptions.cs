using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddMatchOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_CreatorUserId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_CreatorUserId",
                table: "Matches",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_CreatorUserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "Matches");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_CreatorUserId",
                table: "Matches",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
