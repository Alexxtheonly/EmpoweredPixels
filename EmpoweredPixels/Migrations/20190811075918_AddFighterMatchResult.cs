using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddFighterMatchResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_MatchFighterResults", x => new { x.FighterId, x.MatchId });
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
                name: "IX_MatchFighterResults_MatchId",
                table: "MatchFighterResults",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchFighterResults_Result",
                table: "MatchFighterResults",
                column: "Result");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchFighterResults");
        }
    }
}
