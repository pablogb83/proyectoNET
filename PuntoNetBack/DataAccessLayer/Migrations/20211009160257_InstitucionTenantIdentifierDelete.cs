using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class InstitucionTenantIdentifierDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Institucion_Identifier",
                table: "Institucion");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "Institucion",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Institucion_Identifier",
                table: "Institucion",
                column: "Identifier",
                unique: true,
                filter: "[Identifier] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Institucion_Identifier",
                table: "Institucion");

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "Institucion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institucion_Identifier",
                table: "Institucion",
                column: "Identifier",
                unique: true);
        }
    }
}
