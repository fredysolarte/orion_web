using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Consultas
{
    public class CtaVentasBD
    {
        public DataTable GetVentas(SessionManager oSessionManager, string filter, DateTime FecIni,DateTime FecFin)
        {
            StringBuilder sSql = new StringBuilder();        
            try
            {
                sSql.AppendLine(" SELECT (SELECT BDNOMBRE FROM TBTIPFAC, TBBODEGA WHERE TFCODEMP = BDCODEMP AND TFBODEGA = BDBODEGA AND TFTIPFAC = FHTIPFAC) BODEGA, ");
                sSql.AppendLine("FHAGENTE,TRNOMBRE,TOT,NROPRENDAS,NROFACTURAS,(TOT/NROPRENDAS) VLRPRPROM,0.00 UNDPROMFAC,");
                sSql.AppendLine("(TOT/NROFACTURAS) VLRFACPROM");
                sSql.AppendLine("FROM (SELECT FHTIPFAC,FHAGENTE,TRNOMBRE,SUM(PFVLRPAG) TOT,(SELECT COUNT(*) FROM FACVENDT, FACVENHD FA");                                  
                sSql.AppendLine("                                               WHERE FDCODEMP = FA.FHCODEMP");
                sSql.AppendLine("                                                 AND FDTIPFAC = FA.FHTIPFAC");
                sSql.AppendLine("                                                 AND FDNROFAC = FA.FHNROFAC");
                sSql.AppendLine("                                                 AND FA.FHFECFAC  BETWEEN @p0 AND @p1");
                sSql.AppendLine("                                                 AND FA.FHAGENTE = z.FHAGENTE");
                sSql.AppendLine("                                                 AND FDESTADO <> 'AN') NROPRENDAS,");
                sSql.AppendLine("         (SELECT COUNT(*)");
                sSql.AppendLine("            FROM FACVENHD F ");
                sSql.AppendLine("           WHERE F.FHCODEMP = z.FHCODEMP");
                sSql.AppendLine("             AND F.FHAGENTE = z.FHAGENTE");
                sSql.AppendLine("             AND F.FHFECFAC  BETWEEN @p0 AND @p1 ) NROFACTURAS");
                sSql.AppendLine("  FROM FACVENHD z");
                sSql.AppendLine("  INNER JOIN PAGOSFAC ON (FHTIPFAC = PFTIPFAC AND FHNROFAC = PFNROFAC)");
                sSql.AppendLine("  INNER JOIN TERCEROS ON (FHAGENTE = TRCODTER)");
                sSql.AppendLine("  WHERE FHFECFAC BETWEEN @p0 AND @p1");
                sSql.AppendLine("  AND FHESTADO <> 'AN'");
                sSql.AppendLine("  AND PFTIPPAG <> '05'");
                sSql.AppendLine("  AND PFVLRPAG > 0");
                sSql.AppendLine("  GROUP BY FHTIPFAC,FHAGENTE, TRNOMBRE,FHCODEMP ) XX");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, DateTime.Parse(FecIni.ToString()), DateTime.Parse(FecFin.ToString()));
                //return doObject.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, DateTime.TryParse(FecIni.ToString(), out x), DateTime.TryParse(FecFin.ToString(), out x));
            }            
            catch (Exception Ex)
            {
              throw Ex;
            }
            finally
            {
                sSql = null;
            }
        }
    }
}
