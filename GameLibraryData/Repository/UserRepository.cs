using GameLibraryData.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.Repository
{
    public class UserRepository : IDataAccess<User>
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
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
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
                            User user = ReadUser(reader);
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
        public User GetById(int id)
        {
            User user = new User();
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

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(User entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        private User ReadUser(SqlDataReader reader)
        {
            return new User
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
