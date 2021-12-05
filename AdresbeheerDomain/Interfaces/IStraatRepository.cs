using AdresbeheerDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Interfaces
{
    public interface IStraatRepository
    {
        Straat GeefStraat(int id);
        bool HeeftStraat(int id);
        bool HeeftStraat(string straatnaam,int gemeenteid);
        void VerwijderStraat(int id);
        Straat VoegStraatToe(Straat straat);
        void UpdateStraat(Straat straat);
        List<Straat> GeefStratenGemeente(int gemeenteId);
        bool HeeftAdressen(int id);
    }
}
