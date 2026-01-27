using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibraryCLI.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException()
        {
        }

        public InvalidPriceException(string? message) : base("Invalid unit price. Please type in the number with two decimal point")
        {
        }
    }
}
