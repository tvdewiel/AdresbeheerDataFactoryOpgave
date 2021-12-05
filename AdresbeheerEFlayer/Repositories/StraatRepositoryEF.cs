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
    public class StraatRepositoryEF : IStraatRepository
    {
        private AdresbeheerContext ctx;

        public StraatRepositoryEF(string connectionString)
        {
            this.ctx = new AdresbeheerContext(connectionString);
        }

        public Straat GeefStraat(int id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("GeefStraat", ex);
            }
        }
        public List<Straat> GeefStratenGemeente(int gemeenteId)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public bool HeeftAdressen(int id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public bool HeeftStraat(int id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public bool HeeftStraat(string straatnaam, int gemeenteid)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public void UpdateStraat(Straat straat)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public void VerwijderStraat(int id)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
        public Straat VoegStraatToe(Straat straat)
        {
            try
            {
               //
                return straat;
            }
            catch (Exception ex)
            {
                throw new StraatRepositoryException("Straat", ex);
            }
        }
    }
}
