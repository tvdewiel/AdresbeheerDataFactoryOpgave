using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerADOlayer.Exceptions
{
    public class AdresRepositoryException : Exception
    {
        public AdresRepositoryException(string message) : base(message)
        {
        }

        public AdresRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
