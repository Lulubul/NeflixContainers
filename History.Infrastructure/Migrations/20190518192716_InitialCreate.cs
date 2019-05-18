using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace History.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryEntity",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ProfileId = table.Column<string>(nullable: false),
                    WatchingItemId = table.Column<string>(nullable: true),
                    WatchingItemType = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntity", x => new { x.UserId, x.ProfileId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEntity");
        }
    }
}
