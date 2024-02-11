using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerService.DataAccess.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDiscounts_Customers_CustomerId",
                table: "UserDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDiscounts",
                table: "UserDiscounts");

            migrationBuilder.RenameTable(
                name: "UserDiscounts",
                newName: "CustomerDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_UserDiscounts_CustomerId",
                table: "CustomerDiscounts",
                newName: "IX_CustomerDiscounts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerDiscounts",
                table: "CustomerDiscounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDiscounts_Customers_CustomerId",
                table: "CustomerDiscounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDiscounts_Customers_CustomerId",
                table: "CustomerDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerDiscounts",
                table: "CustomerDiscounts");

            migrationBuilder.RenameTable(
                name: "CustomerDiscounts",
                newName: "UserDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerDiscounts_CustomerId",
                table: "UserDiscounts",
                newName: "IX_UserDiscounts_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDiscounts",
                table: "UserDiscounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDiscounts_Customers_CustomerId",
                table: "UserDiscounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
