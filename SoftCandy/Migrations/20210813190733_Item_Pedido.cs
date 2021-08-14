using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Item_Pedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.CreateTable(
                name: "Item_Pedido",
                columns: table => new
                {
                    Preco_Pago = table.Column<decimal>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Cod_Produto = table.Column<int>(nullable: false),
                    Num_Pedido = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PedidoNum_Pedido = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Pedido", x => x.Num_Pedido);
                    table.ForeignKey(
                        name: "FK_Item_Pedido_Produto_Cod_Produto",
                        column: x => x.Cod_Produto,
                        principalTable: "Produto",
                        principalColumn: "Cod_Produto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Pedido_Pedido_PedidoNum_Pedido",
                        column: x => x.PedidoNum_Pedido,
                        principalTable: "Pedido",
                        principalColumn: "Num_Pedido",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Pedido_Cod_Produto",
                table: "Item_Pedido",
                column: "Cod_Produto");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Pedido_PedidoNum_Pedido",
                table: "Item_Pedido",
                column: "PedidoNum_Pedido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item_Pedido");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaNome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });
        }
    }
}
