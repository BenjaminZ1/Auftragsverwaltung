using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class SetCustomerPasswortToBinary70 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Customer",
                type: "binary(70)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(64)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Customer",
                type: "binary(64)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(70)");
        }
    }
}
