using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Tareas
{
    public class PollaBD
    {
        public static DataTable GetGrupo(SessionManager oSessionManager, string inUsuario,string inCadena)
        {
            StringBuilder sSql = new StringBuilder();
            
            sSql.AppendLine("SELECT PL_PARTIDOS.*,A.EQ_NOMBRE E_LOCAL,B.EQ_NOMBRE E_VISITANTE");
            sSql.AppendLine("FROM PL_PARTIDOS WITH(NOLOCK)");
            sSql.AppendLine("INNER JOIN PL_EQUIPOS A WITH(NOLOCK) ON (A.EQ_CODIGO = PR_LOCAL)");
            sSql.AppendLine("INNER JOIN PL_EQUIPOS B WITH(NOLOCK) ON (B.EQ_CODIGO = PR_VISITANTE)");
            sSql.AppendLine("WHERE PL_PARTIDOS.PR_CODIGO IN ("+inCadena+")");
            sSql.AppendLine("  AND PL_PARTIDOS.PR_USUARIO =@p0"); 
            sSql.AppendLine("ORDER BY PL_PARTIDOS.PR_CODIGO");
            

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inUsuario);
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
