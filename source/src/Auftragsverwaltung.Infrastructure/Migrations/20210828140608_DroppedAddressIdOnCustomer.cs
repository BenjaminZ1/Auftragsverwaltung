using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class DroppedAddressIdOnCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
