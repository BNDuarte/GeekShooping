using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShooping.CartAPI.Migrations
{
    public partial class AdicionarCampoCuponConde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cupon_code",
                table: "cart_header",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cart_header",
                keyColumn: "cupon_code",
                keyValue: null,
                column: "cupon_code",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "cupon_code",
                table: "cart_header",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
