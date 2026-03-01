using GameLibraryData.AdoNet.Models;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class CollectionRepository : IDataAccess<Collections>
    {
        private readonly string _connectionString;
        private const string SelectAllCollectionQueryWithJoin = """
                SELECT
                c.CollectionId, c.UserId, c.GameId, c.DateLastPlayed, c.TimesPlayed,
                u.UserName, u.DateofBirth, u.Password, u.Region, u.Bios, u.DateCreated, u.Email,
                g.Title, g.Developer, g.Publisher, g.ReleaseDate, g.Genre, g.Prices
                FROM dbo.Collections c
                JOIN dbo.Users u ON c.UserId = u.UserId
                JOIN dbo.Games g ON c.GameId = g.GameId
                ORDER BY c.CollectionId, u.UserId, g.GameId
                """;
        public int CommandCount { get; set; }

        public CollectionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlCommand CreateCommand(string sqlQuery, SqlConnection connection)
        {
            CommandCount++;
            return new SqlCommand(sqlQuery, connection);
        }
        // Before Refactor:
        /*
        public List<Collection> GetAll()
        {
            CommandCount = 0;
            List<Collection> collections = new List<Collection>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string Query = """
                    SELECT CollectionId,
                    UserId,
                    GameId,
                    DateLastPlayed,
                    TimesPlayed
                    FROM dbo.Collection
                    ORDER BY CollectionId;
                    """;

                using (SqlCommand command = CreateCommand(Query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            collections.Add(new Collection
                            {
                                CollectionId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                GameId = reader.GetInt32(2),
                                DateLastPlayed = reader.GetDateTime(3),
                                TimesPlayed = reader.GetInt32(4)
                            });
                        }
                    }
                }

                Query = """
                    SELECT UserId,
                    UserName,
                    DateofBirth,
                    Password,
                    Region,
                    Bios,
                    DateCreated,
                    Email
                    FROM dbo.Users
                    WHERE UserId = @Id
                    ORDER BY UserId;
                    """;
                string Query2 = """
                    SELECT GameId,
                    Title,
                    Developer,
                    Publisher,
                    ReleaseDate,
                    Genre,
                    Prices
                    FROM dbo.Games
                    WHERE GameId = @Id
                    ORDER BY GameId;
                    """;
                foreach (var collection in collections)
                {
                    using (SqlCommand command = CreateCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", collection.UserId);

                        using (SqlDataReader reader1 = command.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                collection.users.Add(new User
                                {
                                    UserId = reader1.GetInt32(0),
                                    UserName = reader1.GetString(1),
                                    DateofBirth = reader1.GetDateTime(2),
                                    Password = reader1.GetString(3),
                                    Region = reader1.GetString(4),
                                    Bios = reader1.GetString(5),
                                    DateCreated = reader1.GetDateTime(6),
                                    Email = reader1.GetString(7)
                                });
                            }
                        }
                    }
                    using (SqlCommand command1 = CreateCommand(Query2, connection))
                    {
                        command1.Parameters.AddWithValue("@Id", collection.GameId);

                        using (SqlDataReader reader2 = command1.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                collection.games.Add(new Games
                                {
                                    GameId = reader2.GetInt32(0),
                                    Title = reader2.GetString(1),
                                    Developer = reader2.GetString(2),
                                    Publisher = reader2.GetString(3),
                                    ReleaseDate = reader2.GetDateTime(4),
                                    Genre = reader2.GetString(5),
                                    UnitPrice = reader2.GetDecimal(6)
                                });
                            }
                        }
                    }
                     
                }
                return collections;
            }
        }
        */
        // After Refactor
        
        public List<Collections> GetAll()
        {
            CommandCount = 0;
            string Query = SelectAllCollectionQueryWithJoin;
            Dictionary<int, Collections> collections = new Dictionary<int, Collections>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = CreateCommand(Query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int collectionId = reader.GetInt32(0);

                            if (!collections.TryGetValue(collectionId, out Collections? collection))
                            {
                                collection = ReadCollection(reader, collectionId);
                                collections.Add(collectionId, collection);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                collection.users.Add(ReadUser(reader, collectionId));
                            }
                            if (!reader.IsDBNull(12))
                            {
                                collection.games.Add(ReadGames(reader, collectionId));
                            }
                        }
                    }
                }
                return new List<Collections>(collections.Values);
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading collections.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading collections.
                    Message: {{exception.Message}}
                    """);
            }
        }
        

        public Collections GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Collections GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(Collections entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        private Collections ReadCollection(SqlDataReader reader, int collectionId)
        {
            return new Collections
            {
                CollectionId = collectionId,
                UserId = reader.GetInt32(1),
                GameId = reader.GetInt32(2),
                DateLastPlayed = reader.GetDateTime(3),
                TimesPlayed = reader.GetInt32(4)
            };
        }
        private Users ReadUser(SqlDataReader reader, int collectionId)
        {
            return new Users
            {
                UserId = reader.GetInt32(1),
                UserName = reader.GetString(5),
                DateofBirth = reader.GetDateTime(6),
                Password = reader.GetString(7),
                Region = reader.GetString(8),
                Bios = reader.GetString(9),
                DateCreated = reader.GetDateTime(10),
                Email = reader.GetString(11),
            };
        }
        private Games ReadGames(SqlDataReader reader, int collectionId)
        {
            return new Games
            {
                GameId = reader.GetInt32(2),
                Title = reader.GetString(12),
                Developer = reader.GetString(13),
                Publisher = reader.GetString(14),
                ReleaseDate = reader.GetDateTime(15),
                Genre = reader.GetString(16),
                UnitPrice = reader.GetDecimal(17)
            };
        }

        public bool Create(Collections entity)
        {
            throw new NotImplementedException();
        }
    }
}
