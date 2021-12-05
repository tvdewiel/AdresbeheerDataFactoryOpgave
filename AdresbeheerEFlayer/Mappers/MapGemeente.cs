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
    public static class MapGemeente
    {
        public static Gemeente MapToDomain(GemeenteEF db)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapGemeente - MapToDomain", ex);
            }
        }
        public static GemeenteEF MapToDB(Gemeente g)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapGemeente - MapToDB", ex);
            }
        }
    }
}
