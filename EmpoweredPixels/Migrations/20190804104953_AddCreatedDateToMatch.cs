using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
  public partial class AddCreatedDateToMatch : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "Created",
          table: "Matches",
          nullable: true);

      migrationBuilder.Sql("delete from Matches where Options is null or Started is null");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Created",
          table: "Matches");
    }
  }
}
