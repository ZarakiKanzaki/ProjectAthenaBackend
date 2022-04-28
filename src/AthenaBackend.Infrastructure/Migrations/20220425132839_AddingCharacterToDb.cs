using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AthenaBackend.Infrastructure.Migrations
{
    public partial class AddingCharacterToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MisteryOptions",
                table: "Themebooks",
                newName: "IdentityMisteryOptions");

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Mythos = table.Column<string>(type: "TEXT", nullable: false),
                    Logos = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterThemebook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ThemebookId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ThemebookType = table.Column<short>(type: "INTEGER", nullable: true),
                    Type_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Concept = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityMistery = table.Column<string>(type: "TEXT", nullable: false),
                    Flipside = table.Column<string>(type: "TEXT", nullable: true),
                    AttentionLevel = table.Column<short>(type: "INTEGER", nullable: false),
                    FadeCrackLevel = table.Column<short>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterThemebook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterThemebook_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<short>(type: "INTEGER", nullable: false),
                    IsSubtractive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterThemebookTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterThemebookId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagName = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterThemebookTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterThemebookTag_CharacterThemebook_CharacterThemebookId",
                        column: x => x.CharacterThemebookId,
                        principalTable: "CharacterThemebook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterThemebook_CharacterId",
                table: "CharacterThemebook",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterThemebookTag_CharacterThemebookId",
                table: "CharacterThemebookTag",
                column: "CharacterThemebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CharacterId",
                table: "Tag",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterThemebookTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "CharacterThemebook");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.RenameColumn(
                name: "IdentityMisteryOptions",
                table: "Themebooks",
                newName: "MisteryOptions");
        }
    }
}
