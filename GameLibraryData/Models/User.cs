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
        public string UserName { get; set; } = string.Empty;
        public DateTime DateofBirth { get; set; }
        // for placeholder, never store those in plain text
        public string Password { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Bios { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
    }
}
