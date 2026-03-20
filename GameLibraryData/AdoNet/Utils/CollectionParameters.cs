using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Utils
{
    public class CollectionParameters
    {
        public void InitializeCollectionId(SqlCommand command, int id)
        {
            command.Parameters.AddWithValue("@Id", id);
        }
        public void InitializeUsernameFromUserTable(SqlCommand command, string name)
        {
            command.Parameters.AddWithValue("@Username", name);
        }
        public void InitializeCollectionParameters(SqlCommand command, Collections entity)
        {
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@GameId", entity.GameId);
            command.Parameters.AddWithValue("@DateLastPlayed", entity.DateLastPlayed);
            command.Parameters.AddWithValue("@TimesPlayed", entity.TimesPlayed);
            command.Parameters.AddWithValue("@Id", entity.CollectionId);
        }
        public void InitalizeInsertCollectionParameters(SqlCommand command, Collections entity)
        {
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@GameId", entity.GameId);
            command.Parameters.AddWithValue("@DateLastPlayed", entity.DateLastPlayed);
            command.Parameters.AddWithValue("@TimesPlayed", entity.TimesPlayed);
        }
    }
}
