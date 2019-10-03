using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddOldLeaguesForRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 1, true, null, null });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 2, true, null, null });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "IsDeactivated", "Name", "Options" },
                values: new object[] { 3, true, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
