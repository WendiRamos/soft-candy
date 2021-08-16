using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Pedido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Pedido",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
