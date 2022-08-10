using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public class InvalidTaxException : Exception
    {
        public InvalidTaxException() : base("Tax not valid!") { }
        public InvalidTaxException(string message) : base(message) { }
        public InvalidTaxException(string message, Exception innerException) : base(message, innerException) { }
    }
}
