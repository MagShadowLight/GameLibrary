using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.Models
{
    public class Games
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
