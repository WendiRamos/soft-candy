using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class softdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AtivoVendedor",
                table: "Vendedor",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtivoProduto",
                table: "Produto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtivoPedido",
                table: "Pedido",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtivoItemPedido",
                table: "Item_Pedido",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtivoCliente",
                table: "Cliente",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AtivoCategoria",
                table: "Categoria",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtivoVendedor",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "AtivoProduto",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "AtivoPedido",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "AtivoItemPedido",
                table: "Item_Pedido");

            migrationBuilder.DropColumn(
                name: "AtivoCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "AtivoCategoria",
                table: "Categoria");
        }
    }
}
