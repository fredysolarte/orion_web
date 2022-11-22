using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class ReposicionDiariaBD
    {
        public IDataReader GetLista(SessionManager oSessionManager, string filter, DateTime infechaIni, DateTime infechaFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT BDBODEGA,DTTIPPRO,BDNOMBRE,TANOMBRE,DTCLAVE1,DTCLAVE3,ASNOMBRE,SUM(A) A,SUM(B) B,SUM(C) C,SUM(D) D,SUM(E) E,SUM(F) F,SUM(G) G,SUM(H) H,SUM(I) I, SUM(X) X");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT BDBODEGA,BDNOMBRE,DTTIPPRO,TANOMBRE,DTCLAVE1,CASE WHEN DTTIPPRO = 'A' THEN DTCLAVE3 ELSE DTCLAVE2 END AS DTCLAVE2 ,");
                sSql.AppendLine("CASE WHEN DTTIPPRO = 'A' THEN DTCLAVE2 ELSE DTCLAVE3 END AS DTCLAVE3,ASNOMBRE,DTCLAVE4,");

                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE2 NOT IN ('3XL','18','XXXL','XXS','35','4','XS','36','6','S','37','8','M','38','10','L','39','12','XL','40','14','2XL','16','XXL','3XL','18','XXXL') THEN SUM(DTCANTID) END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 NOT IN ('3XL','18','XXXL','XXS','35','4','XS','36','6','S','37','8','M','38','10','L','39','12','XL','40','14','2XL','16','XXL','3XL','18','XXXL') THEN SUM(DTCANTID) END,0) END,0) AS A,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('XXS','35','4') THEN SUM(DTCANTID) END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('XXS','35','4') THEN SUM(DTCANTID) END,0) END,0) AS B,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('XS','36','6') THEN SUM(DTCANTID) END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('XS','36','6') THEN SUM(DTCANTID) END,0)END,0) AS C,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('S','37','8') THEN SUM(DTCANTID)  END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('S','37','8') THEN SUM(DTCANTID)  END,0) END,0) AS D,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('M','38','10') THEN SUM(DTCANTID) END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('M','38','10') THEN SUM(DTCANTID) END,0) END,0) AS E,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('L','39','12') THEN SUM(DTCANTID) END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('L','39','12') THEN SUM(DTCANTID) END,0) END,0) AS F,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE3 IN ('XL','40','14') THEN SUM(DTCANTID)END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('XL','40','14') THEN SUM(DTCANTID)END,0) END,0) AS G,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE2 IN ('2XL','16','XXL') THEN SUM(DTCANTID)END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('2XL','16','XXL') THEN SUM(DTCANTID)END,0) END,0) AS H,");
                sSql.AppendLine("ISNULL(CASE WHEN DTTIPPRO = 'A' THEN ISNULL(CASE WHEN DTCLAVE2 IN ('3XL','18','XXXL') THEN SUM(DTCANTID)END,0) ");
                sSql.AppendLine("ELSE ISNULL(CASE WHEN DTCLAVE2 IN ('3XL','18','XXXL') THEN SUM(DTCANTID)END,0) END,0) AS I");
                sSql.AppendLine(",ISNULL(SUM(DTCANTID),0) X");
                sSql.AppendLine("    FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("    INNER JOIN FACTURADT WITH(NOLOCK) ON (HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND DTNROFAC = HDNROFAC)");
                sSql.AppendLine("    INNER JOIN TBTIPFAC WITH(NOLOCK) ON (HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC <>2)");
                sSql.AppendLine("    INNER JOIN TBBODEGA WITH(NOLOCK) ON(HDCODEMP = BDCODEMP AND BDBODEGA = TFBODEGA)");
                sSql.AppendLine("    INNER JOIN TBTIPPRO WITH(NOLOCK) ON (HDCODEMP = TACODEMP AND DTTIPPRO = TATIPPRO)");
                sSql.AppendLine("    LEFT OUTER JOIN ARTICSEC WITH(NOLOCK) ON (DTCODEMP = ASCODEMP AND DTCLAVE3 = ASCLAVEO AND ASNIVELC = 3 AND DTTIPPRO= ASTIPPRO)");
                sSql.AppendLine("    WHERE CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,@p0,101) AND CONVERT(DATE,@p1,101)");
                sSql.AppendLine("    GROUP BY BDNOMBRE,DTTIPPRO,TANOMBRE,DTCLAVE1,DTCLAVE2,DTCLAVE3,ASNOMBRE,DTCLAVE4,BDBODEGA");
                sSql.AppendLine(") TMP_HD");
                sSql.AppendLine(" WHERE 1=1 " + filter);
                sSql.AppendLine("GROUP BY BDBODEGA,DTTIPPRO,BDNOMBRE,TANOMBRE,DTCLAVE1,ASNOMBRE, DTCLAVE3  ");
                sSql.AppendLine("ORDER BY DTCLAVE1");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, infechaIni, infechaFin);             
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
        public IDataReader GetListaRecorrido(SessionManager oSessionManager, string filter, DateTime infechaIni, DateTime infechaFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DTTIPPRO,DTCLAVE1,DTCLAVE3,SUM(A) A,SUM(B) B,SUM(C) C,SUM(D) D,SUM(E) E,SUM(F) F,SUM(G) G,SUM(H) H,SUM(I) I, SUM(X) X");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT BDBODEGA,BDNOMBRE,DTTIPPRO,TANOMBRE,DTCLAVE1,DTCLAVE2,DTCLAVE3,ASNOMBRE,DTCLAVE4,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 NOT IN ('3XL','18','XXXL','XXS','35','4','XS','36','6','S','37','8','M','38','10','L','39','12','XL','40',");
                sSql.AppendLine("		                    '14','2XL','16','XXL','3XL','18','XXXL') THEN SUM(DTCANTID) END,0) AS A,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('XXS','35','4') THEN SUM(DTCANTID) END,0) AS B,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('XS','36','6') THEN SUM(DTCANTID) END,0) AS C,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('S','37','8') THEN SUM(DTCANTID)  END,0) AS D,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('M','38','10') THEN SUM(DTCANTID) END,0) AS E,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('L','39','12') THEN SUM(DTCANTID) END,0) AS F,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('XL','40','14') THEN SUM(DTCANTID)END,0) AS G,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('2XL','16','XXL') THEN SUM(DTCANTID)END,0) AS H,");
                sSql.AppendLine("ISNULL(CASE WHEN DTCLAVE2 IN ('3XL','18','XXXL') THEN SUM(DTCANTID)END,0) AS I");
                sSql.AppendLine(",ISNULL(SUM(DTCANTID),0) X");
                sSql.AppendLine("    FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("    INNER JOIN FACTURADT WITH(NOLOCK) ON (HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND DTNROFAC = HDNROFAC)");
                sSql.AppendLine("    INNER JOIN TBTIPFAC WITH(NOLOCK) ON (HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC <>2)");
                sSql.AppendLine("    INNER JOIN TBBODEGA WITH(NOLOCK) ON(HDCODEMP = BDCODEMP AND BDBODEGA = TFBODEGA)");
                sSql.AppendLine("    INNER JOIN TBTIPPRO WITH(NOLOCK) ON (HDCODEMP = TACODEMP AND DTTIPPRO = TATIPPRO)");
                sSql.AppendLine("    LEFT OUTER JOIN ARTICSEC WITH(NOLOCK) ON (DTCODEMP = ASCODEMP AND DTCLAVE3 = ASCLAVEO AND ASNIVELC = 3 AND DTTIPPRO= ASTIPPRO)");
                sSql.AppendLine("    WHERE CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,@p0,101) AND CONVERT(DATE,@p1,101) ");
                sSql.AppendLine("    GROUP BY BDNOMBRE,DTTIPPRO,TANOMBRE,DTCLAVE1,DTCLAVE2,DTCLAVE3,ASNOMBRE,DTCLAVE4,BDBODEGA");
                sSql.AppendLine(") TMP_HD");
                sSql.AppendLine(" WHERE 1=1 " + filter);
                sSql.AppendLine("GROUP BY DTTIPPRO,DTCLAVE1,DTCLAVE3  ");
                sSql.AppendLine("ORDER BY DTCLAVE1");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, infechaIni, infechaFin);
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
        public int GetValorBodega(SessionManager oSessionManager, string inCodemp,string inTippro, string inClave1, string inClave2,string inClave3)
        { 
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ISNULL(BBCANTID,0) FROM BALANBOD WITH(NOLOCK) WHERE BBBODEGA='BO' AND BBCODEMP=@p0 AND BBTIPPRO=@p1  AND BBCLAVE1=@p2 AND BBCLAVE3=@p3 AND BBCLAVE2 " + inClave2);
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inCodemp, inTippro, inClave1, inClave3));
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
