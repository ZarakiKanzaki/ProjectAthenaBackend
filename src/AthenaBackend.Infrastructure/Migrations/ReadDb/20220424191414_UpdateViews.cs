using Microsoft.EntityFrameworkCore.Migrations;

namespace AthenaBackend.Infrastructure.Migrations.ReadDb
{
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AthenaBackend.Infrastructure.Migrations.ReadDb
{
    public partial class UpdateViews : Migration
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
                      ,[MisteryOptions]
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

            migrationBuilder.Sql(@"drop view ThemebookImprovementUI;");
            migrationBuilder.Sql($@"
                CREATE VIEW ThemebookImprovementUI
                AS
                SELECT [Id]
                      ,[Title]
                      ,[Decription]
                      ,[ThemebookId]
                      ,[IsDeleted]
                FROM [ThemebookImprovement]
                WHERE [IsDeleted] = 0;

            ");

            migrationBuilder.Sql(@"drop view ThemebookConceptUI;");
            migrationBuilder.Sql($@"
                CREATE VIEW ThemebookConceptUI
                AS
                SELECT [Id]
                      ,[Question]
                      ,[ThemebookId]
                      ,[Answers]
                      ,[IsDeleted]
                FROM [ThemebookConcept]
                WHERE [IsDeleted] = 0;
            ");

            migrationBuilder.Sql(@"drop view TagQuestionUI;");
            migrationBuilder.Sql($@"
                CREATE VIEW TagQuestionUI
                AS
                SELECT [Id]
                      ,[Type]
                      ,[Question]
                      ,[ThemebookId]
                      ,[Answers]
                      ,[IsDeleted]
                FROM [TagQuestion]
                WHERE [IsDeleted] = 0;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view ThemebookUI;");
            migrationBuilder.Sql(@"drop view ThemebookImprovementUI;");
            migrationBuilder.Sql(@"drop view ThemebookConceptUI;");
            migrationBuilder.Sql(@"drop view TagQuestionUI;");
        }
    }
}

}
