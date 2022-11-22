using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class EstadosBD
    {
        public static DataTable GetEstados(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ETCAUSAE, ETNOMBRE ");
                sSql.AppendLine("  FROM TBESTADO ");
                //sSql.AppendLine(" WHERE ETCODEMP = ");
                //sSql.AppendLine("   AND ETGRPTAB = ''FH'' ' +
                //sSql.AppendLine("   AND ETESTADO = ''AN'' '+               

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("WHERE " + filter);
                }
                sSql.AppendLine(" ORDER BY ETCAUSAE ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
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
