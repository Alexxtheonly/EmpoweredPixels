using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
  public partial class UpdateRewardTracks : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 5);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 6);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 7);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 8);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 9);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 10);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 11);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 12);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 13);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 14);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 15);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 16);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 17);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 18);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 19);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 20);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 21);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 22);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 23);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 24);

      migrationBuilder.DeleteData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 25);

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
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 500, new Guid("e620ff6f-e081-4588-b1e1-652f06808359") });

      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 3,
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 1250, new Guid("d00258c4-cb35-4ab3-bd00-bdb356bb6c2c") });

      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 4,
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 5000, new Guid("b051e5c9-a679-489f-95c6-4e32aed2d15b") });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 1,
          column: "Points",
          value: 400);

      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 2,
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751") });

      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 3,
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 1200, new Guid("e620ff6f-e081-4588-b1e1-652f06808359") });

      migrationBuilder.UpdateData(
          table: "RewardTiers",
          keyColumn: "Id",
          keyValue: 4,
          columns: new[] { "Points", "RewardPoolId" },
          values: new object[] { 1600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751") });

      migrationBuilder.InsertData(
          table: "RewardTiers",
          columns: new[] { "Id", "Points", "RewardPoolId", "RewardTrackId" },
#pragma warning disable SA1118 // Parameter should not span multiple lines
          values: new object[,]
          {
                    { 23, 9200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 22, 8800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 21, 8400, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 20, 8000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 19, 7600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 18, 7200, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 17, 6800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 16, 6400, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 15, 6000, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 13, 5200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 24, 9600, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 12, 4800, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 11, 4400, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 10, 4000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 9, 3600, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 8, 3200, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 7, 2800, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 6, 2400, new Guid("e620ff6f-e081-4588-b1e1-652f06808359"), 1 },
                    { 5, 2000, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 14, 5600, new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"), 1 },
                    { 25, 10000, new Guid("d00258c4-cb35-4ab3-bd00-bdb356bb6c2c"), 1 }
          });
#pragma warning restore SA1118 // Parameter should not span multiple lines
    }
  }
}
