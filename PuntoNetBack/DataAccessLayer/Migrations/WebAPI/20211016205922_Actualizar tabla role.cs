using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations.WebAPI
{
    public partial class Actualizartablarole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "Usuarios",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                newName: "IX_Usuarios_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Usuarios",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_RoleId",
                table: "Usuarios",
                newName: "IX_Usuarios_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
