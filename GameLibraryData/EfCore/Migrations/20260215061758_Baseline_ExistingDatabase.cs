using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameLibraryData.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Baseline_ExistingDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Games",
            //    columns: table => new
            //    {
            //        GameId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Prices = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Games", x => x.GameId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Bios = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.UserId);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Collections",
            //    columns: table => new
            //    {
            //        CollectionId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        GameId = table.Column<int>(type: "int", nullable: false),
            //        DateLastPlayed = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TimesPlayed = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Collections", x => x.CollectionId);
            //        table.ForeignKey(
            //            name: "FK_Collections_Games",
            //            column: x => x.GameId,
            //            principalTable: "Games",
            //            principalColumn: "GameId");
            //        table.ForeignKey(
            //            name: "FK_Collections_Users",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "UserId");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Collections_GameId",
            //    table: "Collections",
            //    column: "GameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Collections_UserId",
            //    table: "Collections",
            //    column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Collections");

            //migrationBuilder.DropTable(
            //    name: "Games");

            //migrationBuilder.DropTable(
            //    name: "Users");
        }
    }
}
