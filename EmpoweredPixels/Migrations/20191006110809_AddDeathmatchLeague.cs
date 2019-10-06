using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddDeathmatchLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 5, false, "league.deathmatch", "{\"IntervalCron\":\"0 */2 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"c34c17d6-550f-4bb1-bfc2-65d443deeb53\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"86b56a4f-77f4-4624-b67e-1887e77039a0\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"a9bfa4b5-df2d-4ca3-93b1-f6c721c4e8ff\",\"StaleCondition\":\"cc049ce5-13c5-4f1b-b679-f216eb7c27d9\"}}" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
