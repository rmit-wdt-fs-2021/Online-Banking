using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Migrations
{
    public partial class AddAccNumbCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "BillPay",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BillPay_AccountNumber",
                table: "BillPay",
                column: "AccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPay_Accounts_AccountNumber",
                table: "BillPay",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPay_Accounts_AccountNumber",
                table: "BillPay");

            migrationBuilder.DropIndex(
                name: "IX_BillPay_AccountNumber",
                table: "BillPay");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "BillPay");
        }
    }
}
