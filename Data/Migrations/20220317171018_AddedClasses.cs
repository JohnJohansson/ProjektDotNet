using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektDotNet.Data.Migrations
{
    public partial class AddedClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Condtition = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameConsoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsoleName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameConsoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublisherName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    ImgName = table.Column<string>(type: "TEXT", nullable: true),
                    PublisherFK = table.Column<int>(type: "INTEGER", nullable: false),
                    GameConsoleFK = table.Column<int>(type: "INTEGER", nullable: false),
                    ConditionFK = table.Column<int>(type: "INTEGER", nullable: false),
                    GameContentFK = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Conditions_ConditionFK",
                        column: x => x.ConditionFK,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_GameConsoles_GameConsoleFK",
                        column: x => x.GameConsoleFK,
                        principalTable: "GameConsoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_GameContents_GameContentFK",
                        column: x => x.GameContentFK,
                        principalTable: "GameContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherFK",
                        column: x => x.PublisherFK,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ConditionFK",
                table: "Games",
                column: "ConditionFK");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameConsoleFK",
                table: "Games",
                column: "GameConsoleFK");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameContentFK",
                table: "Games",
                column: "GameContentFK");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherFK",
                table: "Games",
                column: "PublisherFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "GameConsoles");

            migrationBuilder.DropTable(
                name: "GameContents");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
