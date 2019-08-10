using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddMatchTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Powerlevel",
                table: "MatchScoreFighters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoundsAlive",
                table: "MatchScoreFighters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "MatchRegistrations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MatchTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchRegistrations_TeamId",
                table: "MatchRegistrations",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeams_MatchId",
                table: "MatchTeams",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRegistrations_MatchTeams_TeamId",
                table: "MatchRegistrations",
                column: "TeamId",
                principalTable: "MatchTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchRegistrations_MatchTeams_TeamId",
                table: "MatchRegistrations");

            migrationBuilder.DropTable(
                name: "MatchTeams");

            migrationBuilder.DropIndex(
                name: "IX_MatchRegistrations_TeamId",
                table: "MatchRegistrations");

            migrationBuilder.DropColumn(
                name: "Powerlevel",
                table: "MatchScoreFighters");

            migrationBuilder.DropColumn(
                name: "RoundsAlive",
                table: "MatchScoreFighters");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "MatchRegistrations");
        }
    }
}
