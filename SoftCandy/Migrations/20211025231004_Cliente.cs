using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Cliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoCliente",
                table: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCliente",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "CelularCliente",
                table: "Cliente",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11);

            migrationBuilder.AddColumn<string>(
                name: "BairroCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CidadeCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogradouroCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroCliente",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "Categoria",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BairroCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "CidadeCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "EmailCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "EstadoCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "LogradouroCliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "NumeroCliente",
                table: "Cliente");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCliente",
                table: "Cliente",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CelularCliente",
                table: "Cliente",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoCliente",
                table: "Cliente",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "Categoria",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
