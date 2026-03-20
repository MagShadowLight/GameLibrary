using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class CollectionQueries
    {
        public string SelectAllCollectionQueryWithJoin = """
                SELECT
                c.CollectionId, c.UserId, c.GameId, c.DateLastPlayed, c.TimesPlayed,
                u.UserName, u.DateofBirth, u.Password, u.Region, u.Bios, u.DateCreated, u.Email,
                g.Title, g.Developer, g.Publisher, g.ReleaseDate, g.Genre, g.Prices
                FROM dbo.Collections c
                JOIN dbo.Users u ON c.UserId = u.UserId
                JOIN dbo.Games g ON c.GameId = g.GameId
                ORDER BY c.CollectionId, u.UserId, g.GameId
                """;
        public string SelectCollectionByIdQuery = """
                SELECT
                c.CollectionId, c.UserId, c.GameId, c.DateLastPlayed, c.TimesPlayed,
                u.UserName, u.DateofBirth, u.Password, u.Region, u.Bios, u.DateCreated, u.Email,
                g.Title, g.Developer, g.Publisher, g.ReleaseDate, g.Genre, g.Prices
                FROM dbo.Collections c
                JOIN dbo.Users u ON c.UserId = u.UserId
                JOIN dbo.Games g ON c.GameId = g.GameId
                WHERE c.CollectionId = @Id;
                """;
        public string SelectCollectionByUserNameFromUserTableQuery = """
                SELECT
                c.CollectionId, c.UserId, c.GameId, c.DateLastPlayed, c.TimesPlayed,
                u.UserName, u.DateofBirth, u.Password, u.Region, u.Bios, u.DateCreated, u.Email,
                g.Title, g.Developer, g.Publisher, g.ReleaseDate, g.Genre, g.Prices
                FROM dbo.Collections c
                JOIN dbo.Users u ON c.UserId = u.UserId
                JOIN dbo.Games g ON c.GameId = g.GameId
                WHERE u.UserName = @Username
                """;
        public string UpdateCollectionQuery = """
                Update dbo.Collections
                SET UserId = @UserId,
                    GameId = @GameId,
                    DateLastPlayed = @DateLastPlayed,
                    TimesPlayed = @TimesPlayed
                WHERE CollectionId = @Id;
                """;
        public string DeleteCollectionQuery = """
                DELETE FROM dbo.Collections
                WHERE CollectionId = @Id;
                """;
        public string InsertCollectionQuery = """
                INSERT INTO dbo.Collections
                VALUES (
                    @UserId,
                    @GameId,
                    @DateLastPlayed,
                    @TimesPlayed
                )
                """;
    }
}
