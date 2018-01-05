using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackWebAPI.Exceptions
{
    public class BackpackRequestException : Exception
    {
        public BackpackRequestException()
        {
        }

        public BackpackRequestException(string message)
            : base(message)
        {

        }

        public BackpackRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
