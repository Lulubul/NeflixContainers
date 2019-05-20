using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanEntity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CancelAnytime = table.Column<bool>(nullable: false),
                    HD = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NoScreens = table.Column<int>(nullable: false),
                    UltraHD = table.Column<bool>(nullable: false),
                    MonthlyPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PlanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
