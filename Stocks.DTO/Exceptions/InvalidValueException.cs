using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.DTO.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(String message) : base(message)
        {
        }
    }
}
