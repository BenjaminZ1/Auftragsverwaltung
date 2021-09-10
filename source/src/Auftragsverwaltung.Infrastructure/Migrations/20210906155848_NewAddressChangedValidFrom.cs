using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class NewAddressChangedValidFrom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 4,
                column: "ValidFrom",
                value: new DateTime(2021, 1, 2, 23, 59, 59, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 4,
                column: "ValidFrom",
                value: new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
