using GameLibraryData.AdoNet.Models;
using GameLibraryData.AdoNet.Utils;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class UserRepository : IDataAccess<Users>
    {
        private UserQueries _queries = new UserQueries();
        private UserReader _reader = new UserReader();
        private UserParameters _parameters = new UserParameters();
        private readonly string _connectionString;
        public UserRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public List<Users> GetAll()
        {
            List<Users> users = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.SelectAllUserQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = _reader.ReadUser(reader);
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
        }
        public Users GetById(int id)
        {
            Users user = new Users();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.SelectUserByIdQuery, connection))
                    {
                        _parameters.InitializeUserIdParameter(command, id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = _reader.ReadUser(reader);
                            }
                        }
                    }
                }
                return user;
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public Users GetByName(string name)
        {
            Users user = new Users();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.SelectUserByUserNameQuery, connection))
                    {
                        _parameters.InitializeUserNameParameter(command, name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = _reader.ReadUser(reader);
                            }
                        }
                    }
                }
                return user;
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while loading users.
                    Message: {{exception.Message}}
                    """);
            }
        }

        public int Update(Users entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.UpdateUserQuery, connection))
                    {
                        _parameters.InitializeUserParameter(command, entity);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while updating users.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while updating users.
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
                    using (SqlCommand command = new SqlCommand(_queries.DeleteUserQuery, connection))
                    {
                        _parameters.InitializeUserIdParameter(command, id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception($$"""
                    Database error while deleting users.
                    Message: {{exception.Message}}
                    """);
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Unexpected error while deleting users.
                    Message: {{exception.Message}}
                    """);
            }
        }
        public bool Create(Users entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(_queries.InsertUserQuery, connection))
                    {
                        _parameters.InitializeInsertUserParameters(command, entity);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception($$"""
                    Database error while inserting users.
                    Message: {{exception.Message}}
                    """);
            }
        }
    }
}
