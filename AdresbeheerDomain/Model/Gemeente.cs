using AdresbeheerDomain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Model
{
    public class Gemeente
    {
        public int NIScode { get; private set; }
        public string Gemeentenaam { get; private set; }
        public Gemeente(int NIScode, string gemeentenaam)
        {
            ZetNIScode(NIScode);
            ZetGemeentenaam(gemeentenaam);
        }
     
        public void ZetGemeentenaam(string naam)
        {
            if ((string.IsNullOrWhiteSpace(naam)) || (!char.IsUpper(naam[0])))
            {
                GemeenteException ex = new GemeenteException("naam niet correct");
                ex.Data.Add("Gemeentenaam", naam);
                throw ex;
            }
            Gemeentenaam = naam;
        }
        public void ZetNIScode(int code)
        {
            if ((code < 10000) || (code > 99999))
            {
                GemeenteException ex = new GemeenteException("NIScode niet correct");
                ex.Data.Add("NIScode", code);
                throw ex;
            }
            NIScode = code;
        }
        override public string ToString()
        {
            return Gemeentenaam + "," + NIScode.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Gemeente gemeente &&
                   NIScode == gemeente.NIScode &&
                   Gemeentenaam == gemeente.Gemeentenaam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NIScode, Gemeentenaam);
        }

        public static bool operator ==(Gemeente g1, Gemeente g2)
        {
            if ((object)g1 == null)
                return (object)g2 == null;

            return g1.Equals(g2);
        }
        public static bool operator !=(Gemeente g1, Gemeente g2)
        {
            return !(g1 == g2);
        }

    }
}
