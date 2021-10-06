using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class updateTablesUsuarioInst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "institucionId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_institucionId",
                table: "Usuarios",
                column: "institucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Instituciones_institucionId",
                table: "Usuarios",
                column: "institucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Instituciones_institucionId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_institucionId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "institucionId",
                table: "Usuarios");
        }
    }
}
