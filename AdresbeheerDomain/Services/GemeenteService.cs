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
    public class GemeenteService
    {
        private IGemeenteRepository repo;

        public GemeenteService(IGemeenteRepository repo)
        {
            this.repo = repo;
        }
        public Gemeente GeefGemeente(int id)
        {
            try
            {
                return repo.GeefGemeente(id);
            }
            catch (Exception ex)
            {
                throw new GemeenteServiceException("GeefGemeente", ex);
            }
        }
        public void VerwijderGemeente(int id)
        {
            try
            {
                if (!repo.HeeftGemeente(id)) throw new GemeenteServiceException("VerwijderGemeente - gemeente bestaat niet");
                if (repo.HeeftStraten(id)) throw new GemeenteServiceException("VerwijderGemeente - straten niet leeg");
                repo.VerwijderGemeente(id);
            }
            catch(Exception ex)
            {
                throw new GemeenteServiceException("VerwijderGemeente", ex);
            }
        }
        public Gemeente VoegGemeenteToe(Gemeente gemeente)
        {
            try
            {
                if (gemeente==null) throw new GemeenteServiceException("VoegGemeenteToe - gemeente is null");
                if (repo.HeeftGemeente(gemeente.NIScode)) throw new GemeenteServiceException("VoegGemeenteToe - gemeente bestaat reeds");
                repo.VoegGemeenteToe(gemeente);
                return gemeente;
            }
            catch (Exception ex)
            {
                throw new GemeenteServiceException("VoegGemeenteToe", ex);
            }
        }
        public List<Gemeente> GeefGemeenten()
        {
            try
            {
                return repo.GeefGemeenten();
            }
            catch (Exception ex)
            {
                throw new GemeenteServiceException("GeefGemeente", ex);
            }
        }
        public bool BestaatGemeente(int id)
        {
            try
            {
                return repo.HeeftGemeente(id);
            }
            catch(Exception ex)
            {
                throw new GemeenteServiceException("BestaatGemeente", ex);
            }
        }
        public Gemeente UpdateGemeente(Gemeente gemeente)
        {
            try
            {
                if (gemeente == null) throw new GemeenteServiceException("UpdateGemeente - gemeente is null");
                if (!repo.HeeftGemeente(gemeente.NIScode)) throw new GemeenteServiceException("UpdateGemeente - gemeente bestaat niet");
                Gemeente gemeenteDB = repo.GeefGemeente(gemeente.NIScode);
                if (gemeente == gemeenteDB) throw new GemeenteServiceException("UpdateGemeente - geen verschillen");
                repo.UpdateGemeente(gemeente);
                return gemeente;
            }
            catch(Exception ex)
            {
                throw new GemeenteServiceException("UpdateGemeente", ex);
            }
        }        
    }
}
