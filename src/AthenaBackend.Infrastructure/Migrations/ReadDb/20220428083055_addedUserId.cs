using Microsoft.EntityFrameworkCore.Migrations;

namespace AthenaBackend.Infrastructure.Migrations.ReadDb
{
    public partial class addedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                CREATE VIEW UserUI
                AS
                SELECT [Id]
                      ,[GuildMemberId]
                FROM [Users];
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view UserUI;");
        }
    }
}
