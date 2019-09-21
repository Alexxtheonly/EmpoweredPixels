using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpoweredPixels.Migrations
{
    public partial class AddEloRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FighterEloRatings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FighterId = table.Column<Guid>(nullable: false),
                    CurrentElo = table.Column<int>(nullable: false),
                    PreviousElo = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FighterEloRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FighterEloRatings_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FighterEloRatings_FighterId",
                table: "FighterEloRatings",
                column: "FighterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FighterEloRatings");
        }
    }
}
