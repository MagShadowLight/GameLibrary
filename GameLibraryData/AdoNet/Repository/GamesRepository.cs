using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Utils;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class GamesRepository : IDataAccess<Games>
    {
        private GameReader _reader = new GameReader();
        private GameQueries _queries = new GameQueries();
        private GameParameters _parameters = new GameParameters();
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
                    using (SqlCommand command = new SqlCommand(_queries.SelectAllGamesQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Games game = _reader.ReadGames(reader);
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
                    using (SqlCommand command = new SqlCommand(_queries.SelectGamesByIdQuery, connection))
                    {
                        _parameters.InitializeGameIdParameter(command, id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = _reader.ReadGames(reader);
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
                    using (SqlCommand command = new SqlCommand(_queries.SelectGamesByNameQuery, connection))
                    {
                        _parameters.InitializeGameTitleParameter(command, name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                game = _reader.ReadGames(reader);
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
                    using (SqlCommand command = new SqlCommand(_queries.UpdateGamesQuery, connection))
                    {
                        _parameters.InitializeGameParameters(command, entity);
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
                    using (SqlCommand command = new SqlCommand(_queries.DeleteGamesQuery, connection))
                    {
                        _parameters.InitializeGameIdParameter(command, id);
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
                        SqlCommand command = new SqlCommand(_queries.UpdateTitleAndGenreQuery, connection, transaction);
                        _parameters.InitializeGameTitleAndGenreParameters(command, games);
                        int result1 = command.ExecuteNonQuery();
                        command = new SqlCommand(_queries.DeleteGamesQuery, connection, transaction);
                        _parameters.InitializeGameIdParameter(command, id);
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
        public bool Create(Games entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.InsertGamesQuery, connection))
                    {
                        _parameters.InitializeInsertGameParameters(command, entity);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            } catch (Exception exception)
            {
                throw new Exception($$"""
                    Database error while inserting games.
                    Message: {{exception.Message}}
                    """);
            }
        }
    }
}
