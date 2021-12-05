using AdresbeheerDomain.Exceptions;
using AdresbeheerDomain.Interfaces;
using AdresbeheerDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerDomain.Services
{
    public class StraatService
    {
        private IStraatRepository repo;

        public StraatService(IStraatRepository repo)
        {
            this.repo = repo;
        }

        public Straat GeefStraat(int id)
        {
            try
            {
                return repo.GeefStraat(id);
            }
            catch (Exception ex)
            {
                throw new StraatServiceException("GeefStraat", ex);
            }
        }
        public List<Straat> GeefstratenGemeente(int gemeenteId)
        {
            try
            {
                return repo.GeefStratenGemeente(gemeenteId);
            }
            catch (Exception ex)
            {
                throw new StraatServiceException("GeefStratenGemeente", ex);
            }
        }
        public void VerwijderStraat(int id)
        {
            try
            {
                if (!repo.HeeftStraat(id)) throw new StraatServiceException("VerwijderStraat - bestaat niet");
                if (repo.HeeftAdressen(id)) throw new StraatServiceException("VerwijderStraat - heeft nog adressen");
                repo.VerwijderStraat(id);
            }            
            catch (Exception ex)
            {
                throw new StraatServiceException("VerwijderStraat", ex);
            }
        }
        public Straat VoegStraatToe(Straat straat)
        {
            try
            {
                if (straat == null) throw new StraatServiceException("VoegStraatToe - null");
                //TODO Heeftstraat moet checken op dubbels dus straatnaam+gemeente
                if (!repo.HeeftStraat(straat.Straatnaam,straat.Gemeente.NIScode))
                {
                    return repo.VoegStraatToe(straat);
                }
                else
                {
                    throw new StraatServiceException("VoegStraatToe - bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new StraatServiceException("VoegStraatToe", ex);
            }
        }
        public bool BestaatStraat(int id)
        {
            try
            {
                return repo.HeeftStraat(id);
            }
            catch (Exception ex)
            {
                throw new StraatServiceException("BestaatStraat", ex);
            }
        }
        public Straat UpdateStraat(Straat straat)
        {
            try
            {
                if (straat==null) throw new StraatServiceException("UpdateStraat - null");
                if (!repo.HeeftStraat(straat.ID)) throw new StraatServiceException("UpdateStraat - bestaat niet");
                if (repo.HeeftStraat(straat.Straatnaam, straat.Gemeente.NIScode)) throw new StraatServiceException("UpdateStraat - dubbel");
                Straat straatDB = repo.GeefStraat(straat.ID);
                if (straat==straatDB) throw new StraatServiceException("UpdateStraat - geen verschillen");
                repo.UpdateStraat(straat);
                return straat;
            }
            catch (Exception ex)
            {
                throw new StraatServiceException("UpdateStraat", ex);
            }
        }
    }
}
