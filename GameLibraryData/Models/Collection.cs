using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.Models
{
    public class Collection
    {
        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime DateLastPlayed { get; set; }
        public int TimesPlayed { get; set; }

        public List<User> users { get; set; } = new List<User>();

        public List<Games> games { get; set; } = new List<Games>();
    }
}
