using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_Id Cliente",
                table: "Pedido");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.RenameColumn(
                name: "Id Cliente",
                table: "Pedido",
                newName: "ClienteId_Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_Id Cliente",
                table: "Pedido",
                newName: "IX_Pedido_ClienteId_Cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_ClienteId_Cliente",
                table: "Pedido",
                column: "ClienteId_Cliente",
                principalTable: "Cliente",
                principalColumn: "Id_Cliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_ClienteId_Cliente",
                table: "Pedido");

            migrationBuilder.RenameColumn(
                name: "ClienteId_Cliente",
                table: "Pedido",
                newName: "Id Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_ClienteId_Cliente",
                table: "Pedido",
                newName: "IX_Pedido_Id Cliente");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id_Categoria = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome_Categoria = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id_Categoria);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_Id Cliente",
                table: "Pedido",
                column: "Id Cliente",
                principalTable: "Cliente",
                principalColumn: "Id_Cliente",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
