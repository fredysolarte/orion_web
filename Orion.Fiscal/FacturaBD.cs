using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Orion.Fiscal
{
    public class FacturaBD
    {
        public DataTable GetFacturaHD(DBAccess ObjDB, string HDTIPFAC, int HDNROFAC, string HDCODEMP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM V_ventashd WITH(NOLOCK)");
                sSql.AppendLine("WHERE TFTIPFAC=@p0 AND HDNROFAC=@p1 AND HDCODEMP=@p2");

                return ObjDB.ExecuteDataTable(sSql.ToString(), HDTIPFAC, HDNROFAC, HDCODEMP);
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

        public DataTable GetFacturaDT(DBAccess ObjDB, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT FACTURADT.*,ARNOMBRE, TTCODCLA,(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE ARCODEMP = BCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1");
                sSql.AppendLine("AND BCLAVE2 = ARCLAVE2 AND BCLAVE3 = ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BCODIGO");
                sSql.AppendLine("FROM FACTURADT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = DTCODEMP AND ARTIPPRO = DTTIPPRO");
                sSql.AppendLine("                   AND ARCLAVE1 = DTCLAVE1 AND ARCLAVE2 = DTCLAVE2");
                sSql.AppendLine("                   AND ARCLAVE3 = DTCLAVE3 AND ARCLAVE4 = DTCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON(TTCODEMP = ARCODEMP AND TTCODCLA = ARCDIMPF AND TTCODTAB = 'IMPF')");
                sSql.AppendLine("WHERE DTCODEMP = @p0");
                sSql.AppendLine("AND DTTIPFAC = @p1");
                sSql.AppendLine("AND DTNROFAC = @p2");

                return ObjDB.ExecuteDataTable(sSql.ToString(), DTCODEMP, DTTIPFAC, DTNROFAC);
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
