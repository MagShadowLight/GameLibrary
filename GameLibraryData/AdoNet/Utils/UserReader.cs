using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class UserReader
    {
        public Users ReadUser(SqlDataReader reader)
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
