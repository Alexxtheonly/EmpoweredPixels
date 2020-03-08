using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddTotalAssistsToScoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAssists",
                table: "MatchScoreTeams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalAssists",
                table: "MatchScoreFighters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAssists",
                table: "MatchScoreTeams");

            migrationBuilder.DropColumn(
                name: "TotalAssists",
                table: "MatchScoreFighters");
        }
    }
}
