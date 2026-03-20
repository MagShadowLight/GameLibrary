using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class CollectionReader
    {
        public Collections ReadCollection(SqlDataReader reader, int collectionId)
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
        public Users ReadUser(SqlDataReader reader, int collectionId)
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
        public Games ReadGames(SqlDataReader reader, int collectionId)
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
    }
}
