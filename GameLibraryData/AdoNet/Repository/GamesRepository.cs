using GameLibraryData.AdoNet.Models;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class GamesRepository : IDataAccess<Games>
    {
        private const string SelectAllGamesQuery = """
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games;
                    """;
        private const string SelectGamesByIdQuery = """
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
        private const string SelectGamesByNameQuery = """
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
        private const string UpdateGamesQuery = """
                    Update dbo.Games
                    SET Title = @Title,
                        Developer = @Developer,
                        Publisher = @Publisher,
                        ReleaseDate = @ReleaseDate,
                        Genre = @Genre,
                        Prices = @Price
                    WHERE GameId = @Id;
                    """;
        private const string DeleteGamesQuery = """
                    DELETE FROM dbo.Games
                    WHERE GameId = @Id;
                    """;
        private const string UpdateTitleAndGenreQuery = """
                        UPDATE dbo.Games
                        SET Title = @Title,
                            Genre = @Genre
                        WHERE GameId = @Id
                        """;

        private readonly string _connectionString;
        public GamesRepository(string collectionString)
        {
            _connectionString = collectionString;
        }

        public List<Games> GetAll()
        {
            List<Games> games = new List<Games>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SelectAllGamesQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Games game = ReadGames(reader);
                            games.Add(game);
                        }
                    }
                }
                return games;
            } catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading games.
                    Message: {{exception.Message}}
                    """); 
            } catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public Games GetById(int id)
        {
            Games game = new Games();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SelectGamesByIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = ReadGames(reader);
                            }
                        }
                    }
                }
                return game;
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading games.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public Games GetByName(string name)
        {
            Games game = new Games();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SelectGamesByNameQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Title", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = ReadGames(reader);
                            }
                        }
                    }
                }
                return game;
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading games.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public int Update(Games entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(UpdateGamesQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Title", entity.Title);
                        command.Parameters.AddWithValue("@Developer", entity.Developer);
                        command.Parameters.AddWithValue("@Publisher", entity.Publisher);
                        command.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate);
                        command.Parameters.AddWithValue("@Genre", entity.Genre);
                        command.Parameters.AddWithValue("@Price", entity.UnitPrice);
                        command.Parameters.AddWithValue("@Id", entity.GameId);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while updating games.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while updating games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public int Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(DeleteGamesQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while deleting games.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while deleting games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public bool UpdateAndDeleteGames(Games games, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        SqlCommand command = new SqlCommand(UpdateTitleAndGenreQuery, connection, transaction);
                        command.Parameters.AddWithValue("@Title", games.Title);
                        command.Parameters.AddWithValue("@Genre", games.Genre);
                        command.Parameters.AddWithValue("@Id", games.GameId);
                        int result1 = command.ExecuteNonQuery();
                        command = new SqlCommand(DeleteGamesQuery, connection, transaction);
                        command.Parameters.AddWithValue("@Id", id);
                        int result2 = command.ExecuteNonQuery();
                        if (result1 <= 0 || result2 <= 0)
                            throw new Exception();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while updating and deleting games.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while updating and deleting games.
                    Message: {{exception.Message}}
                    """);
            }
        }

        private Games ReadGames(SqlDataReader reader)
        {
            return new Games
            {
                GameId = reader.GetInt32(0),
                Title = reader.GetString(1),
                Developer = reader.GetString(2),
                Publisher = reader.GetString(3),
                ReleaseDate = reader.GetDateTime(4),
                Genre = reader.GetString(5),
                UnitPrice = reader.GetDecimal(6),
            };
        }
    }
}
