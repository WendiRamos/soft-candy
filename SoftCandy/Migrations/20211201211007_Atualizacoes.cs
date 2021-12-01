using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Atualizacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdVendedor",
                table: "Pedido",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_IdVendedor",
                table: "Pedido",
                column: "IdVendedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Vendedor_IdVendedor",
                table: "Pedido",
                column: "IdVendedor",
                principalTable: "Vendedor",
                principalColumn: "IdVendedor",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Vendedor_IdVendedor",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_IdVendedor",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "IdVendedor",
                table: "Pedido");
        }
    }
}
