using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
  public partial class AddContributionPoints : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "MatchFighterResults");

      migrationBuilder.DropColumn(
          name: "MaxEnergy",
          table: "MatchScoreFighters");

      migrationBuilder.DropColumn(
          name: "MaxHealth",
          table: "MatchScoreFighters");

      migrationBuilder.DropColumn(
          name: "Powerlevel",
          table: "MatchScoreFighters");

      migrationBuilder.DropColumn(
          name: "ResultJson",
          table: "MatchResults");

      migrationBuilder.AddColumn<Guid>(
          name: "TeamId",
          table: "MatchScoreFighters",
          nullable: true);

      migrationBuilder.AddColumn<byte[]>(
          name: "RoundTicks",
          table: "MatchResults",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "MatchContributions",
          columns: table => new
          {
            FighterId = table.Column<Guid>(nullable: false),
            MatchId = table.Column<Guid>(nullable: false),
            HasWon = table.Column<bool>(nullable: false),
            KillsAndAssists = table.Column<int>(nullable: false),
            PercentageOfRoundsAlive = table.Column<double>(nullable: false),
            MatchParticipation = table.Column<double>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_MatchContributions", x => new { x.FighterId, x.MatchId });
            table.ForeignKey(
                      name: "FK_MatchContributions_Fighters_FighterId",
                      column: x => x.FighterId,
                      principalTable: "Fighters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_MatchContributions_Matches_MatchId",
                      column: x => x.MatchId,
                      principalTable: "Matches",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "MatchScoreTeams",
          columns: table => new
          {
            MatchId = table.Column<Guid>(nullable: false),
            TeamId = table.Column<Guid>(nullable: false),
            RoundsAlive = table.Column<int>(nullable: false),
            TotalDamageDone = table.Column<int>(nullable: false),
            TotalDamageTaken = table.Column<int>(nullable: false),
            TotalEnergyUsed = table.Column<int>(nullable: false),
            TotalKills = table.Column<int>(nullable: false),
            TotalDeaths = table.Column<int>(nullable: false),
            TotalDistanceTraveled = table.Column<float>(nullable: false),
            TotalRegeneratedHealth = table.Column<int>(nullable: false),
            TotalRegeneratedEnergy = table.Column<int>(nullable: false),
            Created = table.Column<DateTimeOffset>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_MatchScoreTeams", x => new { x.MatchId, x.TeamId });
            table.ForeignKey(
                      name: "FK_MatchScoreTeams_Matches_MatchId",
                      column: x => x.MatchId,
                      principalTable: "Matches",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_MatchScoreTeams_MatchTeams_TeamId",
                      column: x => x.TeamId,
                      principalTable: "MatchTeams",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_MatchContributions_MatchId",
          table: "MatchContributions",
          column: "MatchId");

      migrationBuilder.CreateIndex(
          name: "IX_MatchScoreTeams_TeamId",
          table: "MatchScoreTeams",
          column: "TeamId");

      migrationBuilder.Sql("delete from Matches");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "MatchContributions");

      migrationBuilder.DropTable(
          name: "MatchScoreTeams");

      migrationBuilder.DropColumn(
          name: "TeamId",
          table: "MatchScoreFighters");

      migrationBuilder.DropColumn(
          name: "RoundTicks",
          table: "MatchResults");

      migrationBuilder.AddColumn<int>(
          name: "MaxEnergy",
          table: "MatchScoreFighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "MaxHealth",
          table: "MatchScoreFighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Powerlevel",
          table: "MatchScoreFighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<string>(
          name: "ResultJson",
          table: "MatchResults",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "MatchFighterResults",
          columns: table => new
          {
            MatchId = table.Column<Guid>(nullable: false),
            FighterId = table.Column<Guid>(nullable: false),
            Position = table.Column<int>(nullable: false),
            Result = table.Column<short>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_MatchFighterResults", x => new { x.MatchId, x.FighterId });
            table.ForeignKey(
                      name: "FK_MatchFighterResults_Fighters_FighterId",
                      column: x => x.FighterId,
                      principalTable: "Fighters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_MatchFighterResults_Matches_MatchId",
                      column: x => x.MatchId,
                      principalTable: "Matches",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_MatchFighterResults_FighterId",
          table: "MatchFighterResults",
          column: "FighterId");

      migrationBuilder.CreateIndex(
          name: "IX_MatchFighterResults_Result",
          table: "MatchFighterResults",
          column: "Result");
    }
  }
}
