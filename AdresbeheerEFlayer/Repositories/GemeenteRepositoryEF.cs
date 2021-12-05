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
    public class GemeenteRepositoryEF : IGemeenteRepository
    {
        private AdresbeheerContext ctx;

        public GemeenteRepositoryEF(string connectionString)
        {
            this.ctx = new AdresbeheerContext(connectionString);
        }

        public Gemeente GeefGemeente(int id)
        {
            try
            {
                return null;
            }
            catch(Exception ex)
            {
                throw new GemeenteRepositoryException("GeefGemeente", ex);
            }
        }
        public List<Gemeente> GeefGemeenten()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("HeeftGemeente", ex);
            }
        }
        public bool HeeftGemeente(int id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("HeeftGemeente", ex);
            }
        }
        public bool HeeftStraten(int id)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("HeeftGemeente", ex);
            }
        }
        public void UpdateGemeente(Gemeente gemeente)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("HeeftGemeente", ex);
            }
        }
        public void VerwijderGemeente(int id)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("VerwijderGemeente", ex);
            }
        }
        public void VoegGemeenteToe(Gemeente gemeente)
        {
            try { 
               
            }
            catch (Exception ex)
            {
                throw new GemeenteRepositoryException("VoegGemeenteToe", ex);
            }
        }
    }
}
