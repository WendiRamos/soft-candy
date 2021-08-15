using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_Categoria",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Id_Categoria",
                table: "Produto",
                column: "Id_Categoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_Id_Categoria",
                table: "Produto",
                column: "Id_Categoria",
                principalTable: "Categoria",
                principalColumn: "Id_Categoria",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_Id_Categoria",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Id_Categoria",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Id_Categoria",
                table: "Produto");
        }
    }
}
