using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Utils;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class CollectionRepository : IDataAccess<Collections>
    {
        private readonly string _connectionString;
        private CollectionQueries _queries = new CollectionQueries();
        private CollectionReader _reader = new CollectionReader();
        private CollectionParameters _parameters = new CollectionParameters();
        private UserRepository _userrepo;
        private GamesRepository _gamesrepo;
        public int CommandCount { get; set; }

        public CollectionRepository(string connectionString)
        {
            _connectionString = connectionString;
            _userrepo = new UserRepository(connectionString);
            _gamesrepo = new GamesRepository(connectionString);
        }
        private SqlCommand CreateCommand(string sqlQuery, SqlConnection connection)
        {
            CommandCount++;
            return new SqlCommand(sqlQuery, connection);
        }       
        public List<Collections> GetAll()
        {
            CommandCount = 0;
            string Query = _queries.SelectAllCollectionQueryWithJoin;
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
                                collection = _reader.ReadCollection(reader, collectionId);
                                collections.Add(collectionId, collection);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                collection.users.Add(_reader.ReadUser(reader, collectionId));
                            }
                            if (!reader.IsDBNull(12))
                            {
                                collection.games.Add(_reader.ReadGames(reader, collectionId));
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
            CommandCount = 0;
            string Query = _queries.SelectCollectionByIdQuery;
            Dictionary<int, Collections> collections = new Dictionary<int, Collections>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = CreateCommand(Query, connection))
                    {
                        _parameters.InitializeCollectionId(command, id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int collectionId = reader.GetInt32(0);

                                if (!collections.TryGetValue(collectionId, out Collections? collection))
                                {
                                    collection = _reader.ReadCollection(reader, collectionId);
                                    collections.Add(collectionId, collection);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    collection.users.Add(_reader.ReadUser(reader, collectionId));
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    collection.games.Add(_reader.ReadGames(reader, collectionId));
                                }
                            }
                        }
                    }
                }
                return collections.Values.FirstOrDefault()!;
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

        public Collections GetByName(string name)
        {
            CommandCount = 0;
            string Query = _queries.SelectCollectionByUserNameFromUserTableQuery;
            Dictionary<int, Collections> collections = new Dictionary<int, Collections>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = CreateCommand(Query, connection))
                    {
                        _parameters.InitializeUsernameFromUserTable(command, name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int collectionId = reader.GetInt32(0);

                                if (!collections.TryGetValue(collectionId, out Collections? collection))
                                {
                                    collection = _reader.ReadCollection(reader, collectionId);
                                    collections.Add(collectionId, collection);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    collection.users.Add(_reader.ReadUser(reader, collectionId));
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    collection.games.Add(_reader.ReadGames(reader, collectionId));
                                }
                            }
                        }
                    }
                }
                Random rng = new Random();
                int userNum = rng.Next(0, collections.Values.Count);
                return collections.Values.FirstOrDefault(c => c.users[userNum].UserId == c.UserId)!;
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

        public int Update(Collections entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.UpdateCollectionQuery, connection))
                    {
                        _parameters.InitializeCollectionParameters(command, entity);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while updating collection.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while updating collection.
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
                    using (SqlCommand command = new SqlCommand(_queries.DeleteCollectionQuery, connection))
                    {
                        _parameters.InitializeCollectionId(command, id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while deleting collection.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while deleting collection.
                    Message: {{exception.Message}}
                    """);
            }
        }
        

        public bool Create(Collections entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.InsertCollectionQuery, connection))
                    {
                        _parameters.InitalizeInsertCollectionParameters(command, entity);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Database error while inserting Collection.
                    Message: {{exception.Message}}
                    """);
            }
        }
    }
}
