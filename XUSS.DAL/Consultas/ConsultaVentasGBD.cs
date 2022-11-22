using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class ConsultaVentasGBD
    {
        public DataTable GetVentas(SessionManager oSessionManager, DateTime filter)
        {
            StringBuilder sql = new StringBuilder();
            try { 
                sql.AppendLine("SELECT ");
                sql.AppendLine("    CASE");
                sql.AppendLine("    WHEN");
                sql.AppendLine("    (SELECT COUNT(*) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDESTADO <> 'AN' ");
                sql.AppendLine("        AND X.HDCODEMP = HDCODEMP AND X.HDTIPFAC = HDTIPFAC AND  HDFECCIE IS NOT NULL");
                sql.AppendLine("        AND CONVERT(VARCHAR,X.HDFECFAC,112) = CONVERT(VARCHAR,@p0,112)) = 0 THEN 'N'");
                sql.AppendLine("        ELSE 'S'");
                sql.AppendLine("    END CHK,");
                sql.AppendLine(" BDNOMBRE,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '01' THEN PGVLRPAG END) EFECTIVO,");
                //sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '02' THEN PGVLRPAG END) CHEQUED,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '03' THEN PGVLRPAG END) TDB,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '04' THEN PGVLRPAG END) TCD,");
                //sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '07' THEN PGVLRPAG END) CHEQUEP,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG = '11' THEN PGVLRPAG END) GIFTCARD,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG IN ('02','06','08','10','09','07','12','13','14','15','16','17') THEN PGVLRPAG END) OTROS,");
                sql.AppendLine("SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END)  TOT,  ");
                sql.AppendLine("(SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END)- (SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END) - (SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END)/1.16)) ) SUB,");
                sql.AppendLine("(SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END) - (SUM(CASE WHEN PGTIPPAG <> '05' THEN PGVLRPAG END)/1.16)) IVA ");
                sql.AppendLine("FROM FACTURAHD WITH(NOLOCK), PGFACTUR WITH(NOLOCK), TBTIPFAC WITH(NOLOCK), TBBODEGA WITH(NOLOCK)");
                sql.AppendLine("WHERE HDCODEMP = PGCODEMP");
                sql.AppendLine("AND HDTIPFAC = PGTIPFAC");
                sql.AppendLine("AND HDNROFAC = PGNROFAC");
                sql.AppendLine("AND TFCODEMP = HDCODEMP");
                sql.AppendLine("AND TFTIPFAC = HDTIPFAC");
                sql.AppendLine("AND TFCODEMP = BDCODEMP");
                sql.AppendLine("AND TFBODEGA = BDBODEGA");
                sql.AppendLine("AND HDESTADO <> 'AN'");
                sql.AppendLine("AND CONVERT(VARCHAR,HDFECFAC,112) = CONVERT(VARCHAR,@p0,112)");
                sql.AppendLine("GROUP BY BDNOMBRE ORDER BY TOT DESC");
                return DBAccess.GetDataTable(oSessionManager,sql.ToString(),CommandType.Text,filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
    }
}
