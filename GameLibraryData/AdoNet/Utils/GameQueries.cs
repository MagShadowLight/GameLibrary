using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class GameQueries
    {
        public string SelectAllGamesQuery = """
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games;
                    """;
        public string SelectGamesByIdQuery = """
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games
                    WHERE GameId = @Id;
                    """;
        public string SelectGamesByNameQuery = """
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games
                    WHERE Title = @Title;
                    """;
        public string UpdateGamesQuery = """
                    Update dbo.Games
                    SET Title = @Title,
                        Developer = @Developer,
                        Publisher = @Publisher,
                        ReleaseDate = @ReleaseDate,
                        Genre = @Genre,
                        Prices = @Price
                    WHERE GameId = @Id;
                    """;
        public string DeleteGamesQuery = """
                    DELETE FROM dbo.Games
                    WHERE GameId = @Id;
                    """;
        public string UpdateTitleAndGenreQuery = """
                    UPDATE dbo.Games
                    SET Title = @Title,
                        Genre = @Genre
                    WHERE GameId = @Id
                    """;
        public string InsertGamesQuery = """
                    INSERT INTO dbo.Games
                    VALUES (
                        @Title,
                        @Developer,
                        @Publisher,
                        @ReleaseDate,
                        @Genre,
                        @Price
                    )
                    """;
    }
}
