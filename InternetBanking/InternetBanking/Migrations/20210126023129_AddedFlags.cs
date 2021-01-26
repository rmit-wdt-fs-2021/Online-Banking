using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Migrations
{
    public partial class AddedFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Logins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "BillPay",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "BillPay");
        }
    }
}
