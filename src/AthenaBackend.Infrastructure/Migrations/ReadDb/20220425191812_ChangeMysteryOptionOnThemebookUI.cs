using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AthenaBackend.Infrastructure.Migrations.ReadDb
{
    public partial class ChangeMysteryOptionOnThemebookUI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view ThemebookUI;");
            migrationBuilder.Sql($@"
                CREATE VIEW ThemebookUI
                AS
                SELECT [Id]
                      ,[Name]
                      ,[Description]
                      ,[ThemebookType]
                      ,[Type_Name]
                      ,[ExamplesOfApplication]
                      ,[IdentityMisteryOptions]
                      ,[TitleExamples]
                      ,[CrewRelationships]
                      ,[IsDeleted]
                      ,[Version]
                      ,[UserCreationId]
                      ,[CreationDatetime]
                      ,[UserUpdateId]
                      ,[UpdateDatetime]
                      ,[UserDeletionId]
                      ,[DeletionDatetime]
                  FROM [Themebooks]
                  WHERE [IsDeleted] = 0;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view ThemebookUI;");
        }
    }
}
