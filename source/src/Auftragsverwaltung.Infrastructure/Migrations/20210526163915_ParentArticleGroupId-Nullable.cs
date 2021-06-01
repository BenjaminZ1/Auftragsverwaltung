using Microsoft.EntityFrameworkCore.Migrations;

namespace Auftragsverwaltung.Infrastructure.Migrations
{
    public partial class ParentArticleGroupIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentArticleGroupId",
                table: "ArticleGroup",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentArticleGroupId",
                table: "ArticleGroup",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
