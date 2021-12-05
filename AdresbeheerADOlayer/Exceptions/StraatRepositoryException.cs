using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerADOlayer.Exceptions
{
    public class StraatRepositoryException : Exception
    {
        public StraatRepositoryException(string message) : base(message)
        {
        }

        public StraatRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
