using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryData.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyToCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Games",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users",
                table: "Collections");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Games",
                table: "Collections",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users",
                table: "Collections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Games",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users",
                table: "Collections");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Games",
                table: "Collections",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users",
                table: "Collections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
