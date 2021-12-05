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
    public static class MapStraat
    {
        public static Straat MapToDomain(StraatEF db)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapStraat - MapToDomain", ex);
            }
        }
        public static StraatEF MapToDB(Straat s,AdresbeheerContext ctx)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new MapException("MapStraat - MapToDomain", ex);
            }
        }
    }
}
