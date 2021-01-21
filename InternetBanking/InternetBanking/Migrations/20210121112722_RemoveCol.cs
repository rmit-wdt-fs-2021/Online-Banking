using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Migrations
{
    public partial class RemoveCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
