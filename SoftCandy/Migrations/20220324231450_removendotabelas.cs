using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class removendotabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Vendedor_IdVendedor",
                table: "Pedido");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Estoquista");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.RenameColumn(
                name: "IdVendedor",
                table: "Pedido",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_IdVendedor",
                table: "Pedido",
                newName: "IX_Pedido_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Funcionario_Id",
                table: "Pedido",
                column: "Id",
                principalTable: "Funcionario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Funcionario_Id",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pedido",
                newName: "IdVendedor");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_Id",
                table: "Pedido",
                newName: "IX_Pedido_IdVendedor");

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    IdAdministrador = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AtivoAdministrador = table.Column<bool>(nullable: false),
                    BairroAdministrador = table.Column<string>(nullable: true),
                    CelularAdministrador = table.Column<string>(nullable: true),
                    CidadeAdministrador = table.Column<string>(nullable: true),
                    EmailAdministrador = table.Column<string>(nullable: true),
                    EstadoAdministrador = table.Column<string>(nullable: true),
                    LogradouroAdministrador = table.Column<string>(nullable: true),
                    NomeAdministrador = table.Column<string>(nullable: true),
                    NumeroAdministrador = table.Column<string>(nullable: true),
                    SenhaAdministrador = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.IdAdministrador);
                });

            migrationBuilder.CreateTable(
                name: "Estoquista",
                columns: table => new
                {
                    IdEstoquista = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AtivoEstoquista = table.Column<bool>(nullable: false),
                    BairroEstoquista = table.Column<string>(nullable: true),
                    CelularEstoquista = table.Column<string>(nullable: true),
                    CidadeEstoquista = table.Column<string>(nullable: true),
                    EmailEstoquista = table.Column<string>(nullable: true),
                    EstadoEstoquista = table.Column<string>(nullable: true),
                    LogradouroEstoquista = table.Column<string>(nullable: true),
                    NomeEstoquista = table.Column<string>(nullable: true),
                    NumeroEstoquista = table.Column<string>(nullable: true),
                    SenhaEstoquista = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoquista", x => x.IdEstoquista);
                });

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    IdVendedor = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AtivoVendedor = table.Column<bool>(nullable: false),
                    BairroVendedor = table.Column<string>(nullable: true),
                    CelularVendedor = table.Column<string>(nullable: true),
                    CidadeVendedor = table.Column<string>(nullable: true),
                    EmailVendedor = table.Column<string>(nullable: true),
                    EstadoVendedor = table.Column<string>(nullable: true),
                    LogradouroVendedor = table.Column<string>(nullable: true),
                    NomeVendedor = table.Column<string>(nullable: true),
                    NumeroVendedor = table.Column<string>(nullable: true),
                    SenhaVendedor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.IdVendedor);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Vendedor_IdVendedor",
                table: "Pedido",
                column: "IdVendedor",
                principalTable: "Vendedor",
                principalColumn: "IdVendedor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
