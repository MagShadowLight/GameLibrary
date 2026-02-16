using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.AdoNet.Models
{
    public class Collections
    {
        public int CollectionId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public DateTime DateLastPlayed { get; set; }
        public int TimesPlayed { get; set; }

        public List<Users> users { get; set; } = new List<Users>();

        public List<Games> games { get; set; } = new List<Games>();
    }
}
