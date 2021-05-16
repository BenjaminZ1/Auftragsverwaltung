using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class CorrectedAddresstypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AdressId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Customer",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AdressId",
                table: "Customer",
                newName: "IX_Customer_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Customer",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                newName: "IX_Customer_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AdressId",
                table: "Customer",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
