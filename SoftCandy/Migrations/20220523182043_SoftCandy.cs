using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class SoftCandy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCategoria = table.Column<string>(nullable: true),
                    AtivoCategoria = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    IdFornecedor = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cnpj = table.Column<string>(nullable: true),
                    RazaoSocial = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    CelularFornecedor = table.Column<string>(nullable: true),
                    EmailFornecedor = table.Column<string>(nullable: true),
                    LogradouroFornecedor = table.Column<string>(nullable: true),
                    NumeroFornecedor = table.Column<string>(nullable: true),
                    BairroFornecedor = table.Column<string>(nullable: true),
                    CidadeFornecedor = table.Column<string>(nullable: true),
                    EstadoFornecedor = table.Column<string>(nullable: true),
                    AtivoFornecedor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.IdFornecedor);
                });

            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Cargo = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoboy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoboy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    QuantidadeMinima = table.Column<int>(nullable: false),
                    QuantidadeDescartada = table.Column<int>(nullable: false),
                    QuantidadeDecremento = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Medida = table.Column<int>(nullable: false),
                    IdCategoria = table.Column<int>(nullable: false),
                    IdFornecedor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "IdFornecedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    IdCaixa = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHoraAbertura = table.Column<DateTime>(nullable: false),
                    DataHoraFechamento = table.Column<DateTime>(nullable: false),
                    EstaAberto = table.Column<bool>(nullable: false),
                    FuncionarioAberturaId = table.Column<int>(nullable: false),
                    FuncionarioFechamentoId = table.Column<int>(nullable: false),
                    ValorDinheiroAbertura = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorTotalFechamentoDinheiro = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorVendasDinheiro = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorVendasCartaoDebito = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorVendasCartaoCredito = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorVendasPix = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorTotalVendas = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorOperacoesEntrada = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorOperacoesSaida = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorTotalOperacoes = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.IdCaixa);
                    table.ForeignKey(
                        name: "FK_Caixa_Funcionario_FuncionarioAberturaId",
                        column: x => x.FuncionarioAberturaId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caixa_Funcionario_FuncionarioFechamentoId",
                        column: x => x.FuncionarioFechamentoId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuantidadeEstoque = table.Column<int>(nullable: false),
                    DataHoraFabricacao = table.Column<DateTime>(nullable: false),
                    DataHoraValidade = table.Column<DateTime>(nullable: false),
                    PrecoCompra = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lote_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValorTotal = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(nullable: false),
                    DataHoraRecebimento = table.Column<DateTime>(nullable: false),
                    Recebido = table.Column<bool>(nullable: false),
                    IdCaixa = table.Column<int>(nullable: false),
                    FormaPagamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comanda_Caixa_IdCaixa",
                        column: x => x.IdCaixa,
                        principalTable: "Caixa",
                        principalColumn: "IdCaixa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValorTotal = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(nullable: false),
                    DataHoraRecebimento = table.Column<DateTime>(nullable: false),
                    EnderecoEntrega = table.Column<DateTime>(nullable: false),
                    Recebido = table.Column<bool>(nullable: false),
                    IdCaixa = table.Column<int>(nullable: false),
                    IdMotoboy = table.Column<int>(nullable: false),
                    FormaPagamento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Caixa_IdCaixa",
                        column: x => x.IdCaixa,
                        principalTable: "Caixa",
                        principalColumn: "IdCaixa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Motoboy_IdMotoboy",
                        column: x => x.IdMotoboy,
                        principalTable: "Motoboy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperacaoCaixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataHora = table.Column<DateTime>(nullable: false),
                    IdFuncionario = table.Column<int>(nullable: false),
                    IdCaxa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperacaoCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperacaoCaixa_Caixa_IdCaxa",
                        column: x => x.IdCaxa,
                        principalTable: "Caixa",
                        principalColumn: "IdCaixa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperacaoCaixa_Funcionario_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemComanda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(nullable: false),
                    IdLote = table.Column<int>(nullable: false),
                    IdComanda = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemComanda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemComanda_Comanda_IdComanda",
                        column: x => x.IdComanda,
                        principalTable: "Comanda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemComanda_Lote_IdLote",
                        column: x => x.IdLote,
                        principalTable: "Lote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemDelivery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<int>(nullable: false),
                    IdLote = table.Column<int>(nullable: false),
                    IdDelivery = table.Column<int>(nullable: false),
                    DeliveryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDelivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDelivery_Delivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Delivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemDelivery_Lote_IdLote",
                        column: x => x.IdLote,
                        principalTable: "Lote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_FuncionarioAberturaId",
                table: "Caixa",
                column: "FuncionarioAberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_FuncionarioFechamentoId",
                table: "Caixa",
                column: "FuncionarioFechamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_IdCaixa",
                table: "Comanda",
                column: "IdCaixa");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_IdCaixa",
                table: "Delivery",
                column: "IdCaixa");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_IdMotoboy",
                table: "Delivery",
                column: "IdMotoboy");

            migrationBuilder.CreateIndex(
                name: "IX_ItemComanda_IdComanda",
                table: "ItemComanda",
                column: "IdComanda");

            migrationBuilder.CreateIndex(
                name: "IX_ItemComanda_IdLote",
                table: "ItemComanda",
                column: "IdLote");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDelivery_DeliveryId",
                table: "ItemDelivery",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDelivery_IdLote",
                table: "ItemDelivery",
                column: "IdLote");

            migrationBuilder.CreateIndex(
                name: "IX_Lote_IdProduto",
                table: "Lote",
                column: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_OperacaoCaixa_IdCaxa",
                table: "OperacaoCaixa",
                column: "IdCaxa");

            migrationBuilder.CreateIndex(
                name: "IX_OperacaoCaixa_IdFuncionario",
                table: "OperacaoCaixa",
                column: "IdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdCategoria",
                table: "Produto",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdFornecedor",
                table: "Produto",
                column: "IdFornecedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemComanda");

            migrationBuilder.DropTable(
                name: "ItemDelivery");

            migrationBuilder.DropTable(
                name: "OperacaoCaixa");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Lote");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "Motoboy");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
