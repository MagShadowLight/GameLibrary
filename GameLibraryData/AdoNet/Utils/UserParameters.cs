using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class UserParameters
    {
        public void InitializeUserIdParameter(SqlCommand command, int id)
        {
            command.Parameters.AddWithValue("@Id", id);
        }
        public void InitializeUserNameParameter(SqlCommand command, string name)
        {
            command.Parameters.AddWithValue("@Username", name);
        }
        public void InitializeUserParameter(SqlCommand command, Users entity)
        {
            command.Parameters.AddWithValue("@Username", entity.UserName);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@Region", entity.Region);
            command.Parameters.AddWithValue("@Bios", entity.Bios);
            command.Parameters.AddWithValue("@Id", entity.UserId);
        }
        public void InitializeInsertUserParameters(SqlCommand command, Users entity)
        {
            command.Parameters.AddWithValue("@Username", entity.UserName);
            command.Parameters.AddWithValue("@DateofBirth", entity.DateofBirth);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@Region", entity.Region);
            command.Parameters.AddWithValue("@Bios", entity.Bios);
            command.Parameters.AddWithValue("@DateCreated", entity.DateCreated);
            command.Parameters.AddWithValue("@Email", entity.Email);
        }
    }
}
