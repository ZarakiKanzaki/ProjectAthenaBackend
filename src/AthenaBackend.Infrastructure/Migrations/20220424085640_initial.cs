using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AthenaBackend.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Themebooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ThemebookType = table.Column<short>(type: "INTEGER", nullable: true),
                    Type_Name = table.Column<string>(type: "TEXT", nullable: true),
                    ExamplesOfApplication = table.Column<string>(type: "TEXT", nullable: true),
                    MisteryOptions = table.Column<string>(type: "TEXT", nullable: true),
                    TitleExamples = table.Column<string>(type: "TEXT", nullable: true),
                    CrewRelationships = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Version = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    UserCreationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreationDatetime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserUpdateId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UpdateDatetime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserDeletionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DeletionDatetime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themebooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: true),
                    ThemebookId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Answers = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagQuestion_Themebooks_ThemebookId",
                        column: x => x.ThemebookId,
                        principalTable: "Themebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThemebookConcept",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: true),
                    ThemebookId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemebookConcept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemebookConcept_Themebooks_ThemebookId",
                        column: x => x.ThemebookId,
                        principalTable: "Themebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThemebookImprovement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Decription = table.Column<string>(type: "TEXT", nullable: true),
                    ThemebookId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemebookImprovement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemebookImprovement_Themebooks_ThemebookId",
                        column: x => x.ThemebookId,
                        principalTable: "Themebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagQuestion_ThemebookId",
                table: "TagQuestion",
                column: "ThemebookId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemebookConcept_ThemebookId",
                table: "ThemebookConcept",
                column: "ThemebookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThemebookImprovement_ThemebookId",
                table: "ThemebookImprovement",
                column: "ThemebookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagQuestion");

            migrationBuilder.DropTable(
                name: "ThemebookConcept");

            migrationBuilder.DropTable(
                name: "ThemebookImprovement");

            migrationBuilder.DropTable(
                name: "Themebooks");
        }
    }
}
