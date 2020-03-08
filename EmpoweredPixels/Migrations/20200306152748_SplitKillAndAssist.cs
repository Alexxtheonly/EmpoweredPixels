using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class SplitKillAndAssist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KillsAndAssists",
                table: "MatchContributions");

            migrationBuilder.AddColumn<string>(
                name: "Assists",
                table: "MatchContributions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kills",
                table: "MatchContributions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assists",
                table: "MatchContributions");

            migrationBuilder.DropColumn(
                name: "Kills",
                table: "MatchContributions");

            migrationBuilder.AddColumn<int>(
                name: "KillsAndAssists",
                table: "MatchContributions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
