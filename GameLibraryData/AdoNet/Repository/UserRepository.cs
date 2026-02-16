using GameLibraryData.AdoNet.Models;
using GameLibraryData.Interface;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Repository
{
    public class UserRepository : IDataAccess<Users>
    {
        private const string SelectAllUserQuery = """
                    SELECT UserId,
                           UserName,
                           DoteofBirth,
                           Password,
                           Region,
                           Bios,
                           DateCreated,
                           Email
                    FROM dbo.Users;
                    """;
        private const string SelectUserByIdQuery = """
                    SELECT UserId,
                           Username,
                           DateofBirth,
                           Password,
                           Region,
                           Bios,
                           DateCreated,
                           Email
                    FROM dbo.Users
                    WHERE UserId = @Id;
                    """;
        private readonly string _connectionString;
        public List<Users> GetAll()
        {
            List<Users> users = new List<Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(SelectAllUserQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Users user = ReadUser(reader);
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
                    using (SqlCommand command = new SqlCommand(SelectUserByIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = ReadUser(reader);
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
            throw new NotImplementedException();
        }

        public int Update(Users entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        private Users ReadUser(SqlDataReader reader)
        {
            return new Users
            {
                UserId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                DateofBirth = reader.GetDateTime(2),
                Password = reader.GetString(3),
                Region = reader.GetString(4),
                Bios = reader.GetString(5),
                DateCreated = reader.GetDateTime(6),
                Email = reader.GetString(7),
            };
        }
    }
}
