using AdresbeheerDomain.Model;
using AdresbeheerEFlayer.Exceptions;
using AdresbeheerEFlayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresbeheerEFlayer.Mappers
{
    public static class MapAdres
    {
        public static Adres MapToDomain(AdresEF db)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapAdres - MapToDomain", ex);
            }
        }
        public static AdresEF MapToDB(Adres a, AdresbeheerContext ctx)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapAdres - MapToDomain", ex);
            }
        }
    }
}
