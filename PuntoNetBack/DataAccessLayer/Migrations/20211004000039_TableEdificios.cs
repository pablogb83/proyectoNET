using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class TableEdificios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Usuarios",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nro_doc",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "tipo_doc",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usuarioId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Edificios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edificios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuertaAccesos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denominacion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuertaAccesos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_usuarioId",
                table: "Roles",
                column: "usuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Usuarios_usuarioId",
                table: "Roles",
                column: "usuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Usuarios_usuarioId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Edificios");

            migrationBuilder.DropTable(
                name: "PuertaAccesos");

            migrationBuilder.DropIndex(
                name: "IX_Roles_usuarioId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "nro_doc",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "tipo_doc",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "usuarioId",
                table: "Roles");
        }
    }
}
