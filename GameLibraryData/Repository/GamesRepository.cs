using GameLibraryData.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.Repository
{
    public class GamesRepository : IDataAccess<Games>
    {
        private readonly string _connectionString;
        public GamesRepository(string collectionString)
        {
            _connectionString = collectionString;
        }

        public List<Games> GetAll()
        {
            List<Games> games = new List<Games>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("""
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games;
                    """, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Games game = new Games
                            {
                                GameId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Developer = reader.GetString(2),
                                Publisher = reader.GetString(3),
                                ReleaseDate = reader.GetDateTime(4),
                                Genre = reader.GetString(5),
                                UnitPrice = reader.GetDecimal(6),
                            };
                            games.Add(game);
                        }
                    }
                }
                connection.Dispose();
            }
            return games;
        }

        public Games GetById(int id)
        {
            Games game = new Games();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("""
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games
                    WHERE GameId = @Id;
                    """, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new Games
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
                connection.Dispose();
            }
            return game;
        }

        public Games GetByName(string name)
        {
            Games game = new Games();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("""
                    SELECT GameId,
                            Title,
                            Developer,
                            Publisher,
                            ReleaseDate,
                            Genre,
                            Prices
                    FROM dbo.Games
                    WHERE Title = @Title;
                    """, connection))
                {
                    command.Parameters.AddWithValue("@Title", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            game = new Games
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
                connection.Dispose();
            }
            return game;
        }

        public int Update(Games entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("""
                    Update dbo.Games
                    SET Title = @Title,
                        Developer = @Developer,
                        Publisher = @Publisher,
                        ReleaseDate = @ReleaseDate,
                        Genre = @Genre,
                        Prices = @Price
                    WHERE GameId = @Id;
                    """, connection))
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

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("""
                    DELETE FROM dbo.Games
                    WHERE GameId = @Id;
                    """, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool UpdateAndDeleteGames(Games games, int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand command = new SqlCommand("""
                        UPDATE dbo.Games
                        SET Title = @Title,
                            Genre = @Genre
                        WHERE GameId = @Id
                        """, connection, transaction);
                    command.Parameters.AddWithValue("@Title", games.Title);
                    command.Parameters.AddWithValue("@Genre", games.Genre);
                    command.Parameters.AddWithValue("@Id", games.GameId);


                    int result1 = command.ExecuteNonQuery();

                    command = new SqlCommand("""
                        DELETE FROM dbo.Games
                        WHERE GameId = @Id
                        """, connection, transaction);

                    command.Parameters.AddWithValue("@Id", id);
                    
                    int result2 = command.ExecuteNonQuery();

                    if (result1 <= 0 || result2 <= 0)
                        throw new Exception();

                    transaction.Commit();
                    return true;
                } catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
