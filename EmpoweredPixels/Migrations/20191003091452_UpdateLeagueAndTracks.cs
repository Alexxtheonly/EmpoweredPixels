using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class UpdateLeagueAndTracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4,
                column: "Options",
                value: "{\"IntervalCron\":\"0 */2 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"86b56a4f-77f4-4624-b67e-1887e77039a0\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}");

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Points",
                value: 200);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Points",
                value: 300);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Points",
                value: 750);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Points",
                value: 1000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4,
                column: "Options",
                value: "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}");

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Points",
                value: 250);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Points",
                value: 500);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Points",
                value: 1250);

            migrationBuilder.UpdateData(
                table: "RewardTiers",
                keyColumn: "Id",
                keyValue: 4,
                column: "Points",
                value: 5000);
        }
    }
}
