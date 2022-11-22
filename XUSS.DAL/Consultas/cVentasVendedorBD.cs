using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class cVentasVendedorBD
    {
        public DataTable GetVentas(SessionManager oSessionManager, string filter, DateTime FecIni, DateTime FecFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT V_ventashd.*,(TERCEROS.TRNOMBRE + ' ' + ISNULL(TERCEROS.TRAPELLI, '')) TRNOMBREC ,");
                sSql.AppendLine("(SELECT COUNT(*) FROM FACTURADT WITH(NOLOCK) WHERE DTCODEMP = HDCODEMP AND DTTIPFAC = TFTIPFAC AND DTNROFAC = HDNROFAC ) ITEMS");
                sSql.AppendLine("FROM V_ventashd ");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (HDCODNIT = TRCODNIT)   ");
                sSql.AppendLine("WHERE HDFECFAC BETWEEN @p0 AND @p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, FecIni, FecFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
    }
}
