using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class AddedCustomerNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Position_ArticleId",
                table: "Position");

            migrationBuilder.AddColumn<string>(
                name: "CustomerNumber",
                table: "Customer",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ArticleId",
                table: "Position",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Position_ArticleId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Position_ArticleId",
                table: "Position",
                column: "ArticleId",
                unique: true);
        }
    }
}
