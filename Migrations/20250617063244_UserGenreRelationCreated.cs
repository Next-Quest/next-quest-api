using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextQuest.Migrations
{
    /// <inheritdoc />
    public partial class UserGenreRelationCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenreUser",
                columns: table => new
                {
                    PreferredGenresId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreUser", x => new { x.PreferredGenresId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GenreUser_Genres_PreferredGenresId",
                        column: x => x.PreferredGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreUser_UsersId",
                table: "GenreUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreUser");
        }
    }
}
