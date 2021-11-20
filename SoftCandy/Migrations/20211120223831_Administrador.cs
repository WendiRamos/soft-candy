using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCandy.Migrations
{
    public partial class Administrador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    IdAdministrador = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeAdministrador = table.Column<string>(nullable: true),
                    CelularAdministrador = table.Column<string>(nullable: true),
                    LogradouroAdministrador = table.Column<string>(nullable: true),
                    NumeroAdministrador = table.Column<string>(nullable: true),
                    BairroAdministrador = table.Column<string>(nullable: true),
                    CidadeAdministrador = table.Column<string>(nullable: true),
                    EstadoAdministrador = table.Column<string>(nullable: true),
                    EmailAdministrador = table.Column<string>(nullable: true),
                    SenhaAdministrador = table.Column<string>(nullable: true),
                    AtivoAdministrador = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.IdAdministrador);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");
        }
    }
}
