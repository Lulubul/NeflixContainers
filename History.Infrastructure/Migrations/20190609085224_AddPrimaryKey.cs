using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace History.Infrastructure.Migrations
{
    public partial class AddPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryEntity",
                table: "HistoryEntity");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "HistoryEntity",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HistoryEntity",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HistoryEntity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryEntity",
                table: "HistoryEntity",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoryEntity",
                table: "HistoryEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HistoryEntity");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HistoryEntity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "HistoryEntity",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoryEntity",
                table: "HistoryEntity",
                columns: new[] { "UserId", "ProfileId" });
        }
    }
}
