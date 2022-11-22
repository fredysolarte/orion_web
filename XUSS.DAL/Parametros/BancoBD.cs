using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Parametros
{
    public class BancoBD
    {
        public static DataTable GetBancos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_BANCOS WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
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
