using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddAttunements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FighterConfigurations",
                columns: table => new
                {
                    FighterId = table.Column<Guid>(nullable: false),
                    AttunementId = table.Column<Guid>(nullable: true),
                    HealThreshold = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterConfigurations", x => x.FighterId);
                    table.ForeignKey(
                        name: "FK_FighterConfigurations_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterConfigurations");
        }
    }
}
