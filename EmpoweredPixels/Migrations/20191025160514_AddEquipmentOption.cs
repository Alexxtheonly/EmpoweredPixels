using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddEquipmentOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentOptions",
                columns: table => new
                {
                    EquipmentId = table.Column<Guid>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentOptions", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_EquipmentOptions_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentOptions");
        }
    }
}
