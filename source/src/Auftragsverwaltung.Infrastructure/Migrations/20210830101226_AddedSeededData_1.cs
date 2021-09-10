using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class AddedSeededData_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ArticleGroup",
                columns: new[] { "ArticleGroupId", "Name", "ParentArticleGroupId" },
                values: new object[,]
                {
                    { 1, "Pflegeprodukte", null },
                    { 2, "Haushaltsprodukte", null }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CustomerNumber", "Email", "Firstname", "Lastname", "Password", "Website" },
                values: new object[,]
                {
                    { 1, "CU00001", "hans@test.com", "Hans", "Müller", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "www.hans.ch" },
                    { 2, "CU00002", "ida@gmail.com", "Ida", "Muster", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "www.ida.com" },
                    { 3, "CU00003", "vreni@test.com", "Vreni", "Müller", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, "www.vreni.ch" }
                });

            migrationBuilder.InsertData(
                table: "Town",
                columns: new[] { "TownId", "Townname", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Heerbrugg", "9435" },
                    { 2, "Widnau", "9443" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "BuildingNr", "CustomerId", "Street", "TownId", "ValidFrom", "ValidUntil" },
                values: new object[,]
                {
                    { 1, "69", 1, "Jumbostrasse", 1, new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "420", 2, "Wumbostrasse", 2, new DateTime(2021, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2042, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "ArticleId", "ArticleGroupId", "Description", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Zahnbürste", 2m },
                    { 2, 2, "Flaschenöffner", 25m }
                });

            migrationBuilder.InsertData(
                table: "ArticleGroup",
                columns: new[] { "ArticleGroupId", "Name", "ParentArticleGroupId" },
                values: new object[] { 3, "Körperpflege", 1 });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerId", "Date" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "ArticleId", "OrderId", "Amount" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "ArticleId", "OrderId", "Amount" },
                values: new object[] { 2, 1, 4 });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "ArticleId", "OrderId", "Amount" },
                values: new object[] { 1, 2, 12 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleGroup",
                keyColumn: "ArticleGroupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumns: new[] { "ArticleId", "OrderId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumns: new[] { "ArticleId", "OrderId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Position",
                keyColumns: new[] { "ArticleId", "OrderId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Town",
                keyColumn: "TownId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Town",
                keyColumn: "TownId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ArticleGroup",
                keyColumn: "ArticleGroupId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ArticleGroup",
                keyColumn: "ArticleGroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2);
        }
    }
}
