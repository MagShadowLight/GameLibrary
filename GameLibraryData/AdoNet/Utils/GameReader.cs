using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class GameReader
    {
        public Games ReadGames(SqlDataReader reader)
        {
            return new Games
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
