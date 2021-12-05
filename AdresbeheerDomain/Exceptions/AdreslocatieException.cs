using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Exceptions
{
    public class AdreslocatieException : Exception
    {
        public AdreslocatieException(string message) : base(message)
        {
        }

        public AdreslocatieException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
