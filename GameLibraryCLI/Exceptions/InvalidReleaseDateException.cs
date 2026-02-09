using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryCLI.Exceptions
{
    public class InvalidReleaseDateException : Exception
    {
        public InvalidReleaseDateException()
        {
        }

        public InvalidReleaseDateException(string? message) : base("Invalid Release Date, Please type in the correct date format")
        {
        }
    }
}
