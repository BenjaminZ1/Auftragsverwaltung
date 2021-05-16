using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class TableNamesSingular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleGroups_ArticleGroups_ParentArticleGroupId",
                table: "ArticleGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleGroups_ArticleGroupId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AdressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Articles_ArticleId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Orders_OrderId",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Towns",
                table: "Towns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleGroups",
                table: "ArticleGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Towns",
                newName: "Town");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Position");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleGroups",
                newName: "ArticleGroup");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Positions_ArticleId",
                table: "Position",
                newName: "IX_Position_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AdressId",
                table: "Customer",
                newName: "IX_Customer_AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ArticleGroupId",
                table: "Article",
                newName: "IX_Article_ArticleGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleGroups_ParentArticleGroupId",
                table: "ArticleGroup",
                newName: "IX_ArticleGroup_ParentArticleGroupId");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Address",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_TownId",
                table: "Address",
                newName: "IX_Address_TownId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Town",
                table: "Town",
                column: "TownId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                columns: new[] { "OrderId", "ArticleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleGroup",
                table: "ArticleGroup",
                column: "ArticleGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Town_TownId",
                table: "Address",
                column: "TownId",
                principalTable: "Town",
                principalColumn: "TownId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleGroup_ArticleGroupId",
                table: "Article",
                column: "ArticleGroupId",
                principalTable: "ArticleGroup",
                principalColumn: "ArticleGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleGroup_ArticleGroup_ParentArticleGroupId",
                table: "ArticleGroup",
                column: "ParentArticleGroupId",
                principalTable: "ArticleGroup",
                principalColumn: "ArticleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AdressId",
                table: "Customer",
                column: "AdressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Article_ArticleId",
                table: "Position",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Order_OrderId",
                table: "Position",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Town_TownId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleGroup_ArticleGroupId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleGroup_ArticleGroup_ParentArticleGroupId",
                table: "ArticleGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AdressId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_Article_ArticleId",
                table: "Position");

            migrationBuilder.DropForeignKey(
                name: "FK_Position_Order_OrderId",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Town",
                table: "Town");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleGroup",
                table: "ArticleGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Town",
                newName: "Towns");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "ArticleGroup",
                newName: "ArticleGroups");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Position_ArticleId",
                table: "Positions",
                newName: "IX_Positions_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AdressId",
                table: "Customers",
                newName: "IX_Customers_AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleGroup_ParentArticleGroupId",
                table: "ArticleGroups",
                newName: "IX_ArticleGroups_ParentArticleGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_ArticleGroupId",
                table: "Articles",
                newName: "IX_Articles_ArticleGroupId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_TownId",
                table: "Addresses",
                newName: "IX_Addresses_TownId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Towns",
                table: "Towns",
                column: "TownId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                columns: new[] { "OrderId", "ArticleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleGroups",
                table: "ArticleGroups",
                column: "ArticleGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Towns_TownId",
                table: "Addresses",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "TownId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleGroups_ArticleGroups_ParentArticleGroupId",
                table: "ArticleGroups",
                column: "ParentArticleGroupId",
                principalTable: "ArticleGroups",
                principalColumn: "ArticleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleGroups_ArticleGroupId",
                table: "Articles",
                column: "ArticleGroupId",
                principalTable: "ArticleGroups",
                principalColumn: "ArticleGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AdressId",
                table: "Customers",
                column: "AdressId",
                principalTable: "Addresses",
                principalColumn: "AdressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Articles_ArticleId",
                table: "Positions",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Orders_OrderId",
                table: "Positions",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
