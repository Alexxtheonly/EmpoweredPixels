using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddLeagues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDeactivated = table.Column<bool>(nullable: false),
                    Options = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchFighterResults",
                columns: table => new
                {
                    FighterId = table.Column<Guid>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "LeagueMatches",
                columns: table => new
                {
                    LeagueId = table.Column<int>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueMatches", x => new { x.LeagueId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_LeagueMatches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueMatches_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueSubscriptions",
                columns: table => new
                {
                    LeagueId = table.Column<int>(nullable: false),
                    FighterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueSubscriptions", x => new { x.LeagueId, x.FighterId });
                    table.ForeignKey(
                        name: "FK_LeagueSubscriptions_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueSubscriptions_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 1, false, "League 300", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":300,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 2, false, "League 500", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":500,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueMatches_MatchId",
                table: "LeagueMatches",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueSubscriptions_FighterId",
                table: "LeagueSubscriptions",
                column: "FighterId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchFighterResults_FighterId",
                table: "MatchFighterResults",
                column: "FighterId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchFighterResults_Result",
                table: "MatchFighterResults",
                column: "Result");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeagueMatches");

            migrationBuilder.DropTable(
                name: "LeagueSubscriptions");

            migrationBuilder.DropTable(
                name: "MatchFighterResults");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
