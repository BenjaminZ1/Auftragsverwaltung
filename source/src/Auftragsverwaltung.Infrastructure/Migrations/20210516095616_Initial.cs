using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleGroups",
                columns: table => new
                {
                    ArticleGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    ParentArticleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleGroups", x => x.ArticleGroupId);
                    table.ForeignKey(
                        name: "FK_ArticleGroups_ArticleGroups_ParentArticleGroupId",
                        column: x => x.ParentArticleGroupId,
                        principalTable: "ArticleGroups",
                        principalColumn: "ArticleGroupId");
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    TownId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    Townname = table.Column<string>(type: "varchar(85)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.TownId);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(255)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ArticleGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleGroups_ArticleGroupId",
                        column: x => x.ArticleGroupId,
                        principalTable: "ArticleGroups",
                        principalColumn: "ArticleGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AdressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "varchar(255)", nullable: false),
                    BuildingNr = table.Column<string>(type: "varchar(50)", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AdressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "TownId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "varchar(255)", nullable: false),
                    Lastname = table.Column<string>(type: "varchar(255)", nullable: false),
                    AdressId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Website = table.Column<string>(type: "varchar(255)", nullable: true),
                    Password = table.Column<byte[]>(type: "binary(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Addresses",
                        principalColumn: "AdressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => new { x.OrderId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_Positions_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TownId",
                table: "Addresses",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleGroups_ParentArticleGroupId",
                table: "ArticleGroups",
                column: "ParentArticleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleGroupId",
                table: "Articles",
                column: "ArticleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AdressId",
                table: "Customers",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ArticleId",
                table: "Positions",
                column: "ArticleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ArticleGroups");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Towns");
        }
    }
}
