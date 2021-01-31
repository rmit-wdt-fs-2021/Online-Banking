using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Migrations
{
    public partial class Renamedfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Customers_CustomerID1",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CustomerID1",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "CustomerID1",
                table: "Logins");

            migrationBuilder.RenameColumn(
                name: "LoginID",
                table: "Customers",
                newName: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Customers_CustomerID",
                table: "Logins",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Customers_CustomerID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_CustomerID",
                table: "Logins");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Customers",
                newName: "LoginID");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID1",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_CustomerID1",
                table: "Logins",
                column: "CustomerID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Customers_CustomerID1",
                table: "Logins",
                column: "CustomerID1",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
