using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Basicas
{
    public class BasicasBD
    {
        public static DataTable GetTipPro(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                sSql.AppendLine("  SELECT TATIPPRO, TANOMBRE FROM  TBTIPPRO ");
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
