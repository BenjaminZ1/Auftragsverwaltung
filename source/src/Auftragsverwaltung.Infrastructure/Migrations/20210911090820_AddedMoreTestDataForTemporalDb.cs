using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class AddedMoreTestDataForTemporalDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerId", "Date" },
                values: new object[] { 3, 2, new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "ArticleId", "OrderId", "Amount" },
                values: new object[] { 2, 3, 24 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Position",
                keyColumns: new[] { "ArticleId", "OrderId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 3);
        }
    }
}
