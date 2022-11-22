using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Genericas
{
    public class GenericaBD
    {
        public static int GetSecuencia(SessionManager oSessionManager,int Entidad, int Discriminante)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.AppendLine("UPDATE admi_tsecuencia SET scnc_valorsecuencia= (scnc_valorsecuencia + 1) WHERE scnc_discriminante =@p0 AND scnc_entidad = @p1");
            try 
            {
                Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, Discriminante, Entidad));
                sSql.Clear();
                sSql.AppendLine("SELECT scnc_valorsecuencia FROM admi_tsecuencia WHERE scnc_discriminante =@p0 AND scnc_entidad = @p1");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, Discriminante, Entidad));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                sSql = null;
            }
        }
    }
}
