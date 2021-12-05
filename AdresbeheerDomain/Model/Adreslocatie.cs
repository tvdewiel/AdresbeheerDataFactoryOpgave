using AdresbeheerDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Model
{
    public class Adreslocatie
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Adreslocatie(double x, double y)
        {
            ZetX(x);
            ZetY(y);
        }

        public void ZetX(double x)
        {
            if ((x < 22000) || (x > 258000))
            {
                AdreslocatieException ex = new AdreslocatieException("x coördinaat niet correct");
                ex.Data.Add("x", x);
                throw ex;
            }
            X = x;
        }
        public void ZetY(double y)
        {
            if ((y < 152000) || (y > 244000))
            {
                AdreslocatieException ex = new AdreslocatieException("y coördinaat niet correct");
                ex.Data.Add("y", y);
                throw ex;
            }
            Y = y;
        }
        public override bool Equals(object obj)
        {
            return obj is Adreslocatie adreslocatie &&
                   Math.Abs(X - adreslocatie.X)<1 &&
                   Math.Abs(Y- adreslocatie.Y)<1;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
