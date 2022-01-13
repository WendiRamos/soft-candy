using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Quantidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtivoItemPedido",
                table: "Item_Pedido");

            migrationBuilder.RenameColumn(
                name: "QuantidadeProduto",
                table: "Item_Pedido",
                newName: "Quantidade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Item_Pedido",
                newName: "QuantidadeProduto");

            migrationBuilder.AddColumn<bool>(
                name: "AtivoItemPedido",
                table: "Item_Pedido",
                nullable: false,
                defaultValue: false);
        }
    }
}
