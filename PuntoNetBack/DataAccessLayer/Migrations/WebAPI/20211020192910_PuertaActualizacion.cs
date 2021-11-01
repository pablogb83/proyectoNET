using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations.WebAPI
{
    public partial class PuertaActualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PuertaAccesos");

            migrationBuilder.CreateTable(
                name: "Puertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denominacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    edificioId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Puertas_Edificios_edificioId",
                        column: x => x.edificioId,
                        principalTable: "Edificios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Puertas_edificioId",
                table: "Puertas",
                column: "edificioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Puertas");

            migrationBuilder.CreateTable(
                name: "PuertaAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denominacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    edificioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuertaAccesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PuertaAccesos_Edificios_edificioId",
                        column: x => x.edificioId,
                        principalTable: "Edificios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PuertaAccesos_edificioId",
                table: "PuertaAccesos",
                column: "edificioId");
        }
    }
}
