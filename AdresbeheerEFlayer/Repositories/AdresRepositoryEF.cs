using AdresbeheerDomain.Interfaces;
using AdresbeheerDomain.Model;
using AdresbeheerEFlayer.Exceptions;
using AdresbeheerEFlayer.Mappers;
using AdresbeheerEFlayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerEFlayer.Repositories
{
    public class AdresRepositoryEF : IAdresRepository
    {
        private AdresbeheerContext ctx;

        public AdresRepositoryEF(string connectionString)
        {
            this.ctx = new AdresbeheerContext(connectionString);
        }

        public Adres GeefAdres(int id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("GeefAdres", ex);
            }
        }
        public IEnumerable<Adres> GeefAdressenStraat(int id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("GeefAdressenStraat", ex);
            }
        }
        public bool HeeftAdres(int adresId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("HeeftAdres", ex);
            }
        }
        public bool HeeftAdres(int straatId, string huisnummer, string busnummer, string appartementnummer)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("HeeftAdres", ex);
            }
        }
        public void UpdateAdres(Adres adres)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("UpdateAdres", ex);
            }
        }
        public void VerwijderAdres(int id)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("VerwijderAdres", ex);
            }
        }
        public Adres VoegAdresToe(Adres adres)
        {
            try
            {
                //
                return adres;
            }
            catch (Exception ex)
            {
                throw new AdresRepositoryException("VoegAdresToe", ex);
            }
        }
    }
}
