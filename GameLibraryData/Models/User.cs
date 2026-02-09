using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryData.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int CollectionId { get; set; }
        public string UserName { get; set; }
        public DateTime DateofBirth { get; set; }
        // for placeholder, never store those in plain text
        public string Password { get; set; }
        public string Region { get; set; }
        public string Bios { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Email { get; set; }
    }
}
