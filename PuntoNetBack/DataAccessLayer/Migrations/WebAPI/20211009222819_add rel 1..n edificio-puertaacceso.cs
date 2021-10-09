using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations.WebAPI
{
    public partial class addrel1nedificiopuertaacceso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "edificioId",
                table: "PuertaAccesos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PuertaAccesos_edificioId",
                table: "PuertaAccesos",
                column: "edificioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PuertaAccesos_Edificios_edificioId",
                table: "PuertaAccesos",
                column: "edificioId",
                principalTable: "Edificios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PuertaAccesos_Edificios_edificioId",
                table: "PuertaAccesos");

            migrationBuilder.DropIndex(
                name: "IX_PuertaAccesos_edificioId",
                table: "PuertaAccesos");

            migrationBuilder.DropColumn(
                name: "edificioId",
                table: "PuertaAccesos");
        }
    }
}
