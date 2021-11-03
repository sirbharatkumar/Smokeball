using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.Common
{
    public class ClientException : Exception
    {
        public ClientException(string message) : base(message)
        {
            //Log the exception message
        }
    }

    public class NoResponseException : Exception
    {
        public NoResponseException(string message) : base(message)
        {
            //Log the exception message
        }
    }
}
