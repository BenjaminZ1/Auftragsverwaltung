using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class UpdatedSeededData_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "BuildingNr", "CustomerId", "Street", "TownId", "ValidFrom", "ValidUntil" },
                values: new object[] { 3, "69", 3, "Jumbostrasse", 1, new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 3);
        }
    }
}
