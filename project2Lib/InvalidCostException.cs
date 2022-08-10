using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public class InvalidCostException : Exception
    {
        public InvalidCostException() : base("Cost not Valid!") { }
        public InvalidCostException(string message) : base(message) { }

        public InvalidCostException(string message, Exception innerException) : base(message, innerException) { }
    }
}
