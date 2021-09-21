using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id_Categoria = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome_Categoria = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id_Categoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 60, nullable: false),
                    Celular = table.Column<string>(maxLength: 11, nullable: false),
                    Endereco = table.Column<string>(maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Id_Vendedor = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome_Vendedor = table.Column<string>(nullable: true),
                    Celular_Vendedor = table.Column<string>(nullable: true),
                    Endereco_Vendedor = table.Column<string>(nullable: true),
                    Email_Vendedor = table.Column<string>(nullable: true),
                    Senha_Vendedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id_Vendedor);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Cod_Produto = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome_Produto = table.Column<string>(maxLength: 60, nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Preco_Venda = table.Column<decimal>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 60, nullable: true),
                    Id_Categoria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Cod_Produto);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_Id_Categoria",
                        column: x => x.Id_Categoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Num_Pedido = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor_Total = table.Column<decimal>(nullable: false),
                    Data_Pedido = table.Column<DateTime>(nullable: false),
                    ID_CLIENTE = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Num_Pedido);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item_Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrecoPago = table.Column<decimal>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Cod_Produto = table.Column<int>(nullable: false),
                    Num_Pedido = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Pedido_Produto_Cod_Produto",
                        column: x => x.Cod_Produto,
                        principalTable: "Produto",
                        principalColumn: "Cod_Produto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Pedido_Pedido_Num_Pedido",
                        column: x => x.Num_Pedido,
                        principalTable: "Pedido",
                        principalColumn: "Num_Pedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Pedido_Cod_Produto",
                table: "Item_Pedido",
                column: "Cod_Produto");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Pedido_Num_Pedido",
                table: "Item_Pedido",
                column: "Num_Pedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ID_CLIENTE",
                table: "Pedido",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Id_Categoria",
                table: "Produto",
                column: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item_Pedido");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
