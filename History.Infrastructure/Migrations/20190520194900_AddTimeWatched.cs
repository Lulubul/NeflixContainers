using Microsoft.EntityFrameworkCore.Migrations;

namespace History.Infrastructure.Migrations
{
    public partial class AddTimeWatched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TimeWatched",
                table: "HistoryEntity",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWatched",
                table: "HistoryEntity");
        }
    }
}
