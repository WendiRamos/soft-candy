using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Vendedor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnderecoVendedor",
                table: "Vendedor",
                newName: "NumeroVendedor");

            migrationBuilder.AddColumn<string>(
                name: "BairroVendedor",
                table: "Vendedor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CidadeVendedor",
                table: "Vendedor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoVendedor",
                table: "Vendedor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogradouroVendedor",
                table: "Vendedor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BairroVendedor",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "CidadeVendedor",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "EstadoVendedor",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "LogradouroVendedor",
                table: "Vendedor");

            migrationBuilder.RenameColumn(
                name: "NumeroVendedor",
                table: "Vendedor",
                newName: "EnderecoVendedor");
        }
    }
}
