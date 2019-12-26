using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddSeasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeasonId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeasonProgresses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeasonId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Position = table.Column<int>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonProgresses_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    SalvageValue = table.Column<int>(nullable: false),
                    Bonus = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonSummaries_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonSummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4,
                column: "Options",
                value: "{\"IntervalCron\":\"0 * * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}");

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 5,
                column: "Options",
                value: "{\"IntervalCron\":\"0 */2 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"c34c17d6-550f-4bb1-bfc2-65d443deeb53\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"a9bfa4b5-df2d-4ca3-93b1-f6c721c4e8ff\",\"StaleCondition\":\"cc049ce5-13c5-4f1b-b679-f216eb7c27d9\"}}");

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "Id", "EndDate", "SeasonId", "StartDate" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new Guid("1e2c8876-ff2e-408e-9034-14667cbba862"), new DateTimeOffset(new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonProgresses_SeasonId",
                table: "SeasonProgresses",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonProgresses_UserId",
                table: "SeasonProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonSummaries_SeasonId",
                table: "SeasonSummaries",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonSummaries_UserId",
                table: "SeasonSummaries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeasonProgresses");

            migrationBuilder.DropTable(
                name: "SeasonSummaries");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4,
                column: "Options",
                value: "{\"IntervalCron\":\"0 */2 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}");

            migrationBuilder.UpdateData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 5,
                column: "Options",
                value: "{\"IntervalCron\":\"0 */5 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"c34c17d6-550f-4bb1-bfc2-65d443deeb53\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"a9bfa4b5-df2d-4ca3-93b1-f6c721c4e8ff\",\"StaleCondition\":\"cc049ce5-13c5-4f1b-b679-f216eb7c27d9\"}}");
        }
    }
}
