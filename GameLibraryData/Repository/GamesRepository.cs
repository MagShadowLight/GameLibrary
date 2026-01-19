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
                            UnitPrice
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
                            UnitPrice
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

        public Games GetByTitle(string title)
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
                            UnitPrice
                    FROM dbo.Games
                    WHERE Title = @Title;
                    """, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);

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
    }
}
