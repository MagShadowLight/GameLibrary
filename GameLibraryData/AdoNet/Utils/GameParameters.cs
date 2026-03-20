using GameLibraryData.AdoNet.Models;
using Microsoft.Data.SqlClient;

namespace GameLibraryData.AdoNet.Utils
{
    public class GameParameters
    {
        public void InitializeGameParameters(SqlCommand command, Games entity)
        {
            command.Parameters.AddWithValue("@Title", entity.Title);
            command.Parameters.AddWithValue("@Developer", entity.Developer);
            command.Parameters.AddWithValue("@Publisher", entity.Publisher);
            command.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate);
            command.Parameters.AddWithValue("@Genre", entity.Genre);
            command.Parameters.AddWithValue("@Price", entity.UnitPrice);
            command.Parameters.AddWithValue("@Id", entity.GameId);
        }
        public void InitializeGameTitleAndGenreParameters(SqlCommand command, Games entity)
        {
            command.Parameters.AddWithValue("@Title", entity.Title);
            command.Parameters.AddWithValue("@Genre", entity.Genre);
            command.Parameters.AddWithValue("@Id", entity.GameId);
        }
        public void InitializeGameIdParameter(SqlCommand command, int id)
        {
            command.Parameters.AddWithValue("@Id", id);
        }
        public void InitializeGameTitleParameter(SqlCommand command, string name)
        {
            command.Parameters.AddWithValue("@Title", name);
        }
        public void InitializeInsertGameParameters(SqlCommand command, Games entity)
        {
            command.Parameters.AddWithValue("@Title", entity.Title);
            command.Parameters.AddWithValue("@Developer", entity.Developer);
            command.Parameters.AddWithValue("@Publisher", entity.Publisher);
            command.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate);
            command.Parameters.AddWithValue("@Genre", entity.Genre);
            command.Parameters.AddWithValue("@Price", entity.UnitPrice);
        }
    }
}
