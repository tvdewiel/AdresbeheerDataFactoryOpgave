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
    public class AdresService
    {
        private IAdresRepository repo;

        public AdresService(IAdresRepository repo)
        {
            this.repo = repo;
        }

        public Adres GeefAdres(int id)
        {
            try
            {
                return repo.GeefAdres(id);
            }
            catch(Exception ex){
                throw new AdresServiceException("GeefAdres", ex);
            }
        }
        //TODO straat + nr + ...
        public bool BestaatAdres(int adresId)
        {
            try
            {
                return repo.HeeftAdres(adresId);
            }
            catch(Exception ex)
            {
                throw new AdresServiceException("BestaatAdres", ex);
            }
        }
        public void VerwijderAdres(Adres adres)
        {
            try
            {
                if (repo.HeeftAdres(adres.ID))
                {
                    repo.VerwijderAdres(adres.ID);
                }
                else
                {
                    throw new AdresServiceException("VerwijderAdres - bestaat niet");
                }
            }
            catch(Exception ex)
            {
                throw new AdresServiceException("VerwijderAdres", ex);
            }
        }
        public IEnumerable<Adres> GeefAdressenStraat(int id)
        {
            try
            {
                return repo.GeefAdressenStraat(id);
            }
            catch (Exception ex)
            {
                throw new AdresServiceException("GeefAdressenStraat", ex);
            }
        }
        public Adres VoegAdresToe(Adres adres)
        {
            try
            {
                //if (!repo.HeeftAdres(adres.ID))
                if (!repo.HeeftAdres(adres.Straat.ID,adres.Huisnummer,adres.Busnummer,adres.Appartementnummer))
                {
                    return repo.VoegAdresToe(adres);
                }
                else
                {
                    throw new AdresServiceException("VoegAdresToe - adres bestaat al");
                }
            }
            catch (Exception ex)
            {
                throw new AdresServiceException("VoegAdresToe", ex);
            }
        }
        public Adres UpdateAdres(Adres adres)
        {
            try
            {
                if (adres == null) throw new AdresServiceException("UpdateAdres - null");
                if (!repo.HeeftAdres(adres.ID)) throw new AdresServiceException("UpdateAdres - bestaat niet");
                Adres adresDB = repo.GeefAdres(adres.ID);
                if (adres == adresDB) throw new AdresServiceException("UpdateAdres - geen verschillen");
                repo.UpdateAdres(adres);
                return adres;
            }
            catch (Exception ex)
            {
                throw new AdresServiceException("UpdateAdres", ex);
            }
        }
    }
}
