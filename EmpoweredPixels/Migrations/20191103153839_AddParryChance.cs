using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
  public partial class AddParryChance : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<int>(
          name: "ParryChance",
          table: "Fighters",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "ParryChance",
          table: "Equipment",
          nullable: false,
          defaultValue: 0);

      // set greatsword parry chance
      migrationBuilder.Sql("Update Equipment set ParryChance = 15 where [Type] = '96753286-FD45-489E-A3FB-25DC758F94F7'");

      // set dagger parry chance
      migrationBuilder.Sql("Update Equipment set ParryChance = 5 where [Type] = '4DD298A2-2405-47CF-B554-6E3DB9D57920'");

      // set glaive parry chance
      migrationBuilder.Sql("Update Equipment set ParryChance = 15 where [Type] = 'C615E5EC-88C2-4EBC-B338-C71BAA24D5C6'");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "ParryChance",
          table: "Fighters");

      migrationBuilder.DropColumn(
          name: "ParryChance",
          table: "Equipment");
    }
  }
}
