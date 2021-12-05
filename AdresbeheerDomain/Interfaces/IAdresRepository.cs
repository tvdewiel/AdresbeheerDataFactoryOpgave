using AdresbeheerDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Interfaces
{
    public interface IAdresRepository
    {
        public Adres GeefAdres(int id);
        bool HeeftAdres(int adresId);
        bool HeeftAdres(int straatId, string huisnummer, string busnummer, string appartementnummer);
        void VerwijderAdres(int iD);
        Adres VoegAdresToe(Adres adres);
        void UpdateAdres(Adres adres);
        IEnumerable<Adres> GeefAdressenStraat(int id);
    }
}
