using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class ConsultaBonosBD
    {
        public static DataTable GetConsultaBonos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_BONOCANJE.*,HDFECFAC,TFNOMBRE,(TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI ,'')) TERCERO,TRCODNIT");
                sSql.AppendLine("FROM TB_BONOCANJE WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT WITH(NOLOCK) ON (TB_BONOCANJE.DTTIPFAC = FACTURADT.DTTIPFAC AND TB_BONOCANJE.DTNROFAC = FACTURADT.DTNROFAC AND TB_BONOCANJE.DTNROITM = FACTURADT.DTNROITM)");
                sSql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (FACTURADT.DTCODEMP = HDCODEMP AND FACTURADT.DTTIPFAC = HDTIPFAC AND FACTURADT.DTNROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (FACTURADT.DTCODEMP = TFCODEMP AND FACTURADT.DTTIPFAC = TFTIPFAC)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = HDCODEMP AND HDCODCLI = TRCODTER)");
                sSql.AppendLine("WHERE 1=1 "+filter);
                
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
