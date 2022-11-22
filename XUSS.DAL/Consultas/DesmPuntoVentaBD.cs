using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Consultas
{
    public class DesmPuntoVentaBD
    {
        public DataTable GetDesAlmacen(SessionManager oSessionManager, int ano, int mes, string bodega)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT dbo.FGET_NOMDIASEMANA(DATEPART(DW,(CONVERT(DATE,CAST(TEMP_1.MES AS VARCHAR)+'/'+CAST(TEMP_1.DIA AS VARCHAR)+'/'+CAST(TEMP_1.ANO AS VARCHAR),101)))) DSEMA,");
                sSql.AppendLine("(dbo.FGET_NOMDIASEMANA(DATEPART(DW,(CONVERT(DATE,CAST(TEMP_1.MES AS VARCHAR)+'/'+CAST(TEMP_1.DIA AS VARCHAR)+'/'+CAST(TEMP_1.ANO AS VARCHAR),101)))) +'-'+ CAST( TEMP_1.DIA AS VARCHAR)) DIAC,");
                sSql.AppendLine("TEMP_1.DIA,TEMP_1.UND_DIA,SUM(UND_ACU) UND_ACU,TEMP_1.FAC_DIA, SUM(FAC_ACU) FAC_ACU, dbo.SP_GET_VALORNETO(TOTFAC_DIA) AS TOTFAC_DIA,");
                sSql.AppendLine("dbo.SP_GET_VALORNETO(SUM(TOTFAC_ACU)) TOTFAC_ACU, dbo.SP_GET_VALORNETO(TOTFAC_ACU)/TEMP_1.DIA PRO_VTAFAC, dbo.SP_GET_VALORNETO(TOTFAC_ACU)/SUM(UND_ACU) VLR_PREN,");
                sSql.AppendLine("dbo.SP_GET_VALORNETO(TOTFAC_ACU)/SUM(FAC_ACU) VLR_FAC,XX.BDNOMBRE, CAST(SUM(UND_ACU)AS DECIMAL(16,2))/CAST(SUM(FAC_ACU)AS DECIMAL(16,2)) UND_FAC");
                sSql.AppendLine("FROM");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT MONTH(HDFECFAC) MES,YEAR(HDFECFAC) ANO,DAY(HDFECFAC) DIA,SUM(DTCANTID) UND_DIA,COUNT(DISTINCT HDNROFAC) FAC_DIA, ");
                sSql.AppendLine("");
                sSql.AppendLine("(SELECT SUM(C.PGVLRPAG)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN PGFACTUR C WITH(NOLOCK)  ON(A.HDCODEMP = C.PGCODEMP AND A.HDTIPFAC = C.PGTIPFAC AND A.HDNROFAC = C.PGNROFAC AND C.PGTIPPAG NOT IN('05') )");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) = DAY(X.HDFECFAC)");
                sSql.AppendLine("AND A.HDESTADO <> 'AN'");
                sSql.AppendLine(")");
                sSql.AppendLine(" TOTFAC_DIA, J.BDBODEGA");
                sSql.AppendLine("FROM FACTURAHD X WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT WITH(NOLOCK) ON(HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC = 1)");
                sSql.AppendLine("INNER JOIN TBBODEGA J WITH(NOLOCK) ON(HDCODEMP = J.BDCODEMP AND TFBODEGA = J.BDBODEGA )");
                sSql.AppendLine("WHERE MONTH(HDFECFAC) = @p0");
                sSql.AppendLine("AND YEAR(HDFECFAC) = @p1");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("GROUP BY DAY(HDFECFAC), MONTH(HDFECFAC) ,YEAR(HDFECFAC), J.BDBODEGA");
                sSql.AppendLine(") TEMP_1");
                sSql.AppendLine("INNER JOIN (");
                sSql.AppendLine("SELECT DAY(HDFECFAC) DIA,MONTH(HDFECFAC) MES,YEAR(HDFECFAC) ANO, ");
                sSql.AppendLine("(SELECT COUNT(*)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT B WITH(NOLOCK) ON(A.HDCODEMP = B.DTCODEMP AND A.HDTIPFAC = B.DTTIPFAC AND A.HDNROFAC = B.DTNROFAC)  ");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5))");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN') UND_ACU,");
                sSql.AppendLine("");
                sSql.AppendLine("(SELECT COUNT(*)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN') FAC_ACU,");
                sSql.AppendLine("(SELECT SUM(C.PGVLRPAG)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN PGFACTUR C WITH(NOLOCK)  ON(A.HDCODEMP = C.PGCODEMP AND A.HDTIPFAC = C.PGTIPFAC AND A.HDNROFAC = C.PGNROFAC AND C.PGTIPPAG NOT IN('05') )");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN')");
                sSql.AppendLine(" TOTFAC_ACU, J.BDBODEGA");
                sSql.AppendLine("FROM FACTURAHD X WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT WITH(NOLOCK) ON(HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA J WITH(NOLOCK)  ON(HDCODEMP = J.BDCODEMP AND TFBODEGA = J.BDBODEGA )");
                sSql.AppendLine("WHERE MONTH(HDFECFAC) = @p0");
                sSql.AppendLine("AND YEAR(HDFECFAC)  = @p1");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("GROUP BY DAY(HDFECFAC), MONTH(HDFECFAC) ,YEAR(HDFECFAC),J.BDBODEGA ");
                sSql.AppendLine(")TEMP_2 ON(TEMP_2.DIA = TEMP_1.DIA AND TEMP_2.MES = TEMP_1.MES AND TEMP_2.ANO = TEMP_1.ANO AND TEMP_1.BDBODEGA = TEMP_2.BDBODEGA");
                if (bodega != "-1")
                    sSql.AppendLine("AND TEMP_1.BDBODEGA =@p2");
                sSql.AppendLine("    )");
                sSql.AppendLine("INNER JOIN TBBODEGA XX WITH(NOLOCK) ON(TEMP_2.BDBODEGA = XX.BDBODEGA)");
                sSql.AppendLine("GROUP BY TEMP_1.ANO,TEMP_1.MES,TEMP_1.DIA,TEMP_1.UND_DIA,TEMP_1.FAC_DIA, TOTFAC_DIA, TOTFAC_ACU,XX.BDNOMBRE");
                sSql.AppendLine("ORDER BY TEMP_1.DIA DESC");

                if (bodega != "-1")
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, mes, ano, bodega);
                else
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, mes, ano);
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
        public DataTable GetDesAlmacen(SessionManager oSessionManager, int ano, int mes, string bodega, int dia)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT dbo.FGET_NOMDIASEMANA(DATEPART(DW,(CAST(TEMP_1.DIA AS VARCHAR)+'/'+CAST(TEMP_1.MES AS VARCHAR)+'/'+CAST(TEMP_1.ANO AS VARCHAR)))) DSEMA,");
                sSql.AppendLine("(dbo.FGET_NOMDIASEMANA(DATEPART(DW,(CAST(TEMP_1.DIA AS VARCHAR)+'/'+CAST(TEMP_1.MES AS VARCHAR)+'/'+CAST(TEMP_1.ANO AS VARCHAR)))) +'-'+ CAST( TEMP_1.DIA AS VARCHAR)) DIAC,");
                sSql.AppendLine("TEMP_1.DIA,TEMP_1.UND_DIA,SUM(UND_ACU) UND_ACU,TEMP_1.FAC_DIA, SUM(FAC_ACU) FAC_ACU, dbo.SP_GET_VALORNETO(TOTFAC_DIA) AS TOTFAC_DIA,");
                sSql.AppendLine("dbo.SP_GET_VALORNETO(SUM(TOTFAC_ACU)) TOTFAC_ACU, dbo.SP_GET_VALORNETO(TOTFAC_ACU)/TEMP_1.DIA PRO_VTAFAC, dbo.SP_GET_VALORNETO(TOTFAC_ACU)/SUM(UND_ACU) VLR_PREN,");
                sSql.AppendLine("dbo.SP_GET_VALORNETO(TOTFAC_ACU)/SUM(FAC_ACU) VLR_FAC,XX.BDNOMBRE, CAST(SUM(UND_ACU)AS DECIMAL(16,2))/CAST(SUM(FAC_ACU)AS DECIMAL(16,2)) UND_FAC");
                sSql.AppendLine("FROM");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT MONTH(HDFECFAC) MES,YEAR(HDFECFAC) ANO,DAY(HDFECFAC) DIA,SUM(DTCANTID) UND_DIA,COUNT(DISTINCT HDNROFAC) FAC_DIA, ");
                sSql.AppendLine("");
                sSql.AppendLine("(SELECT SUM(C.PGVLRPAG)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN PGFACTUR C WITH(NOLOCK)  ON(A.HDCODEMP = C.PGCODEMP AND A.HDTIPFAC = C.PGTIPFAC AND A.HDNROFAC = C.PGNROFAC AND C.PGTIPPAG NOT IN('05'))");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) = DAY(X.HDFECFAC)");
                sSql.AppendLine("AND A.HDESTADO <> 'AN'");
                sSql.AppendLine(")");
                sSql.AppendLine(" TOTFAC_DIA, J.BDBODEGA");
                sSql.AppendLine("FROM FACTURAHD X WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT WITH(NOLOCK) ON(HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC IN (1,5) )");
                sSql.AppendLine("INNER JOIN TBBODEGA J WITH(NOLOCK) ON(HDCODEMP = J.BDCODEMP AND TFBODEGA = J.BDBODEGA )");
                sSql.AppendLine("WHERE MONTH(HDFECFAC) = @p0");
                sSql.AppendLine("AND YEAR(HDFECFAC) = @p1");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("GROUP BY DAY(HDFECFAC), MONTH(HDFECFAC) ,YEAR(HDFECFAC), J.BDBODEGA");
                sSql.AppendLine(") TEMP_1");
                sSql.AppendLine("INNER JOIN (");
                sSql.AppendLine("SELECT DAY(HDFECFAC) DIA,MONTH(HDFECFAC) MES,YEAR(HDFECFAC) ANO, ");
                sSql.AppendLine("(SELECT COUNT(*)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT B WITH(NOLOCK) ON(A.HDCODEMP = B.DTCODEMP AND A.HDTIPFAC = B.DTTIPFAC AND A.HDNROFAC = B.DTNROFAC)  ");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC = 1)");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN') UND_ACU,");
                sSql.AppendLine("");
                sSql.AppendLine("(SELECT COUNT(*)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5))");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN') FAC_ACU,");
                sSql.AppendLine("(SELECT SUM(C.PGVLRPAG)");
                sSql.AppendLine("  FROM FACTURAHD A WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN PGFACTUR C WITH(NOLOCK)  ON(A.HDCODEMP = C.PGCODEMP AND A.HDTIPFAC = C.PGTIPFAC AND A.HDNROFAC = C.PGNROFAC AND C.PGTIPPAG NOT IN('05') )");
                sSql.AppendLine("INNER JOIN TBTIPFAC D WITH(NOLOCK)  ON(A.HDCODEMP = D.TFCODEMP AND A.HDTIPFAC = D.TFTIPFAC AND D.TFCLAFAC IN (1,5))");
                sSql.AppendLine("INNER JOIN TBBODEGA E WITH(NOLOCK)  ON(A.HDCODEMP = E.BDCODEMP AND D.TFBODEGA = E.BDBODEGA AND E.BDBODEGA = J.BDBODEGA)");
                sSql.AppendLine("WHERE MONTH(A.HDFECFAC) = MONTH(X.HDFECFAC)");
                sSql.AppendLine("AND YEAR(A.HDFECFAC)  = YEAR(X.HDFECFAC) ");
                sSql.AppendLine("AND DAY(A.HDFECFAC) <= DAY(X.HDFECFAC) ");
                sSql.AppendLine("AND A.HDESTADO <> 'AN')");
                sSql.AppendLine(" TOTFAC_ACU, J.BDBODEGA");
                sSql.AppendLine("FROM FACTURAHD X WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURADT WITH(NOLOCK) ON(HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC AND TFCLAFAC IN (1,5))");
                sSql.AppendLine("INNER JOIN TBBODEGA J WITH(NOLOCK)  ON(HDCODEMP = J.BDCODEMP AND TFBODEGA = J.BDBODEGA )");
                sSql.AppendLine("WHERE MONTH(HDFECFAC) = @p0");
                sSql.AppendLine("AND YEAR(HDFECFAC)  = @p1");
                sSql.AppendLine("AND DAY(HDFECFAC)  = @p2");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("GROUP BY DAY(HDFECFAC), MONTH(HDFECFAC) ,YEAR(HDFECFAC),J.BDBODEGA ");
                sSql.AppendLine(")TEMP_2 ON(TEMP_2.DIA = TEMP_1.DIA AND TEMP_2.MES = TEMP_1.MES AND TEMP_2.ANO = TEMP_1.ANO AND TEMP_1.BDBODEGA = TEMP_2.BDBODEGA");
                if (bodega != "-1")
                    sSql.AppendLine("AND TEMP_1.BDBODEGA =@p3");
                sSql.AppendLine("    )");
                sSql.AppendLine("INNER JOIN TBBODEGA XX WITH(NOLOCK) ON(TEMP_2.BDBODEGA = XX.BDBODEGA)");
                sSql.AppendLine("GROUP BY TEMP_1.ANO,TEMP_1.MES,TEMP_1.DIA,TEMP_1.UND_DIA,TEMP_1.FAC_DIA, TOTFAC_DIA, TOTFAC_ACU,XX.BDNOMBRE");
                sSql.AppendLine("ORDER BY TEMP_1.DIA DESC");

                if (bodega != "-1")
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, mes, ano, dia, bodega);
                else
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, mes, ano, dia);
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
