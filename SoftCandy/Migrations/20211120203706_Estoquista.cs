using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Estoquista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoquista",
                columns: table => new
                {
                    IdEstoquista = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeEstoquista = table.Column<string>(nullable: true),
                    CelularEstoquista = table.Column<string>(nullable: true),
                    LogradouroEstoquista = table.Column<string>(nullable: true),
                    NumeroEstoquista = table.Column<string>(nullable: true),
                    BairroEstoquista = table.Column<string>(nullable: true),
                    CidadeEstoquista = table.Column<string>(nullable: true),
                    EstadoEstoquista = table.Column<string>(nullable: true),
                    EmailEstoquista = table.Column<string>(nullable: true),
                    SenhaEstoquista = table.Column<string>(nullable: true),
                    AtivoEstoquista = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoquista", x => x.IdEstoquista);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoquista");
        }
    }
}
