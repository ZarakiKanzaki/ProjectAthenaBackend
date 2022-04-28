using Microsoft.EntityFrameworkCore.Migrations;

namespace AthenaBackend.Infrastructure.Migrations
{
    public partial class UpdateOnConcept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answers",
                table: "ThemebookConcept",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answers",
                table: "ThemebookConcept");
        }
    }
}
