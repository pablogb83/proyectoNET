using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations.WebAPI
{
    public partial class MigrateEventosPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "Eventos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "Eventos");
        }
    }
}
