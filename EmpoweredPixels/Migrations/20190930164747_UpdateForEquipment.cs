using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
  public partial class UpdateForEquipment : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("delete from Fighters");
      migrationBuilder.Sql("delete from Matches");

      migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 1);

      migrationBuilder.DeleteData(
          table: "Leagues",
          keyColumn: "Id",
          keyValue: 2);

      migrationBuilder.DeleteData(
          table: "Leagues",
          keyColumn: "Id",
          keyValue: 3);

      migrationBuilder.DropColumn(
          name: "Expertise",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Regeneration",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Stamina",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Toughness",
          table: "Fighters");

      migrationBuilder.AddColumn<int>(
          name: "Level",
          table: "Rewards",
          nullable: true);

      migrationBuilder.AlterColumn<int>(
          name: "Vitality",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AlterColumn<int>(
          name: "Vision",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AlterColumn<int>(
          name: "Speed",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AlterColumn<int>(
          name: "Power",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AlterColumn<int>(
          name: "Agility",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AlterColumn<int>(
          name: "Accuracy",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(float));

      migrationBuilder.AddColumn<int>(
          name: "Armor",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "ConditionPower",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Ferocity",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "HealingPower",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Level",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Precision",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Equipment",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            Type = table.Column<Guid>(nullable: false),
            UserId = table.Column<long>(nullable: false),
            FighterId = table.Column<Guid>(nullable: true),
            Power = table.Column<int>(nullable: false),
            ConditionPower = table.Column<int>(nullable: false),
            Precision = table.Column<int>(nullable: false),
            Ferocity = table.Column<int>(nullable: false),
            Accuracy = table.Column<int>(nullable: false),
            Agility = table.Column<int>(nullable: false),
            Armor = table.Column<int>(nullable: false),
            Vitality = table.Column<int>(nullable: false),
            HealingPower = table.Column<int>(nullable: false),
            Speed = table.Column<int>(nullable: false),
            Vision = table.Column<int>(nullable: false),
            Level = table.Column<int>(nullable: false),
            Rarity = table.Column<int>(nullable: false),
            Enhancement = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Equipment", x => x.Id);
            table.ForeignKey(
                      name: "FK_Equipment_Fighters_FighterId",
                      column: x => x.FighterId,
                      principalTable: "Fighters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Equipment_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "FighterExperiences",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            FighterId = table.Column<Guid>(nullable: false),
            Points = table.Column<long>(nullable: false),
            LastUpdate = table.Column<DateTimeOffset>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_FighterExperiences", x => x.Id);
            table.ForeignKey(
                      name: "FK_FighterExperiences_Fighters_FighterId",
                      column: x => x.FighterId,
                      principalTable: "Fighters",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RewardTracks",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            IsActive = table.Column<bool>(nullable: false),
            TotalPoints = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RewardTracks", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "SocketStones",
          columns: table => new
          {
            Id = table.Column<Guid>(nullable: false),
            EquipmentId = table.Column<Guid>(nullable: true),
            UserId = table.Column<long>(nullable: false),
            Level = table.Column<int>(nullable: false),
            Rarity = table.Column<int>(nullable: false),
            Enhancement = table.Column<int>(nullable: false),
            Stat = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_SocketStones", x => x.Id);
            table.ForeignKey(
                      name: "FK_SocketStones_Equipment_EquipmentId",
                      column: x => x.EquipmentId,
                      principalTable: "Equipment",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_SocketStones_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RewardTiers",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            RewardTrackId = table.Column<int>(nullable: false),
            Points = table.Column<int>(nullable: false),
            RewardPoolId = table.Column<Guid>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RewardTiers", x => x.Id);
            table.ForeignKey(
                      name: "FK_RewardTiers_RewardTracks_RewardTrackId",
                      column: x => x.RewardTrackId,
                      principalTable: "RewardTracks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "RewardTrackProgresses",
          columns: table => new
          {
            RewardTrackId = table.Column<int>(nullable: false),
            UserId = table.Column<long>(nullable: false),
            Activated = table.Column<DateTimeOffset>(nullable: false),
            Completed = table.Column<DateTimeOffset>(nullable: true),
            Progress = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_RewardTrackProgresses", x => new { x.UserId, x.RewardTrackId });
            table.ForeignKey(
                      name: "FK_RewardTrackProgresses_RewardTracks_RewardTrackId",
                      column: x => x.RewardTrackId,
                      principalTable: "RewardTracks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_RewardTrackProgresses_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.InsertData(
          table: "Leagues",
          columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
          values: new object[] { 4, false, "league.lastmanstanding", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });

      migrationBuilder.InsertData(
          table: "RewardTracks",
          columns: new[] { "Id", "IsActive", "TotalPoints" },
          values: new object[] { 1, true, 10000 });

      migrationBuilder.InsertData(
          table: "RewardTiers",
          columns: new[] { "Id", "Points", "RewardPoolId", "RewardTrackId" },
#pragma warning disable SA1118 // Parameter should not span multiple lines
          values: new object[,]
          {
                    { 1, 400, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 23, 9200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 22, 8800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 21, 8400, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 20, 8000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 19, 7600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 18, 7200, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 17, 6800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 16, 6400, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 15, 6000, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 14, 5600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 24, 9600, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 13, 5200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 11, 4400, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 10, 4000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 9, 3600, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 8, 3200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 7, 2800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 6, 2400, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 5, 2000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 4, 1600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 3, 1200, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 2, 800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 12, 4800, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 25, 10000, new Guid("d00258c4-cb35-4ab3-bd00-bdb356bb6c2c"), 1 }
          });
#pragma warning restore SA1118 // Parameter should not span multiple lines

      migrationBuilder.CreateIndex(
          name: "IX_Equipment_FighterId",
          table: "Equipment",
          column: "FighterId");

      migrationBuilder.CreateIndex(
          name: "IX_Equipment_UserId",
          table: "Equipment",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_FighterExperiences_FighterId",
          table: "FighterExperiences",
          column: "FighterId",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_RewardTiers_RewardTrackId",
          table: "RewardTiers",
          column: "RewardTrackId");

      migrationBuilder.CreateIndex(
          name: "IX_RewardTrackProgresses_RewardTrackId",
          table: "RewardTrackProgresses",
          column: "RewardTrackId");

      migrationBuilder.CreateIndex(
          name: "IX_SocketStones_EquipmentId",
          table: "SocketStones",
          column: "EquipmentId");

      migrationBuilder.CreateIndex(
          name: "IX_SocketStones_UserId",
          table: "SocketStones",
          column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "FighterExperiences");

      migrationBuilder.DropTable(
          name: "RewardTiers");

      migrationBuilder.DropTable(
          name: "RewardTrackProgresses");

      migrationBuilder.DropTable(
          name: "SocketStones");

      migrationBuilder.DropTable(
          name: "RewardTracks");

      migrationBuilder.DropTable(
          name: "Equipment");

      migrationBuilder.DeleteData(
          table: "Leagues",
          keyColumn: "Id",
          keyValue: 4);

      migrationBuilder.DropColumn(
          name: "Level",
          table: "Rewards");

      migrationBuilder.DropColumn(
          name: "Armor",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "ConditionPower",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Ferocity",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "HealingPower",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Level",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "Precision",
          table: "Fighters");

      migrationBuilder.AlterColumn<float>(
          name: "Vitality",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AlterColumn<float>(
          name: "Vision",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AlterColumn<float>(
          name: "Speed",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AlterColumn<float>(
          name: "Power",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AlterColumn<float>(
          name: "Agility",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AlterColumn<float>(
          name: "Accuracy",
          table: "Fighters",
          nullable: false,
          oldClrType: typeof(int));

      migrationBuilder.AddColumn<float>(
          name: "Expertise",
          table: "Fighters",
          nullable: false,
          defaultValue: 0f);

      migrationBuilder.AddColumn<float>(
          name: "Regeneration",
          table: "Fighters",
          nullable: false,
          defaultValue: 0f);

      migrationBuilder.AddColumn<float>(
          name: "Stamina",
          table: "Fighters",
          nullable: false,
          defaultValue: 0f);

      migrationBuilder.AddColumn<float>(
          name: "Toughness",
          table: "Fighters",
          nullable: false,
          defaultValue: 0f);

      migrationBuilder.InsertData(
          table: "Leagues",
          columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
          values: new object[] { 1, false, "League 300", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":300,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"86b56a4f-77f4-4624-b67e-1887e77039a0\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });

      migrationBuilder.InsertData(
          table: "Leagues",
          columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
          values: new object[] { 2, false, "League 500", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":500,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });

      migrationBuilder.InsertData(
          table: "Leagues",
          columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
          values: new object[] { 3, false, "League 750", "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":750,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}" });
    }
  }
}
