using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShooping.ProductAPI.Migrations
{
    public partial class ajusteNome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cateogry_name",
                table: "product",
                newName: "category_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "product",
                newName: "cateogry_name");
        }
    }
}