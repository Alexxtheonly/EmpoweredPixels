using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class UpdateDistanceTraveledType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalDistanceTraveled",
                table: "MatchScoreTeams",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "TotalDistanceTraveled",
                table: "MatchScoreFighters",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalDistanceTraveled",
                table: "MatchScoreTeams",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "TotalDistanceTraveled",
                table: "MatchScoreFighters",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
