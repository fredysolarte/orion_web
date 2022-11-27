using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Compras
{
    public class OrdenesComprasBD
    {
        //compras
        #region
        public static DataTable GetComprasHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASHD.*, BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO='AP' THEN 'Aprobado' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO"); 
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                sql.AppendLine("ORDER BY CH_NROCMP");
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetComprasDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASDT.*, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(CD_NROCMP AS VARCHAR)+CD_TIPPRO+CD_CLAVE1+CD_CLAVE2+CD_CLAVE3+CD_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = CD_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = CD_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = CD_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = CD_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = CD_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = CD_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = CD_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (CD_CANTIDAD*CD_PRECIO) VLRTOT, (CD_CANTIDAD*CD_PRECIO) NEW_TOT ,TANOMBRE,ARNOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8, ");
                sql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3= ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM CMP_COMPRASDT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = CD_CODEMP AND TATIPPRO = CD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CD_CODEMP AND ARTIPPRO = CD_TIPPRO AND ARCLAVE1 = CD_CLAVE1 AND ARCLAVE2 = CD_CLAVE2 AND ARCLAVE3 = CD_CLAVE3 AND ARCLAVE4 = CD_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE CD_CODEMP =@p0");
                sql.AppendLine("   AND CD_NROCMP =@p1");

                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CD_CODEMP,CD_NROCMP);
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
        public static DataTable GetProforma(SessionManager oSessionManager, string PR_CODEMP, int PR_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_PROFACTURADT.*, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(PR_NROCMP AS VARCHAR)+PR_TIPPRO+PR_CLAVE1+PR_CLAVE2+PR_CLAVE3+PR_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = PR_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = PR_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = PR_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = PR_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = PR_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = PR_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = PR_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (PR_CANTIDAD*PR_PRECIO) VLRTOT,TANOMBRE,ARNOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8, ");
                sql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3= ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM CMP_PROFACTURADT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = PR_CODEMP AND TATIPPRO = PR_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = PR_CODEMP AND ARTIPPRO = PR_TIPPRO AND ARCLAVE1 = PR_CLAVE1 AND ARCLAVE2 = PR_CLAVE2 AND ARCLAVE3 = PR_CLAVE3 AND ARCLAVE4 = PR_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE PR_CODEMP =@p0");
                sql.AppendLine("   AND PR_NROCMP =@p1");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, PR_CODEMP, PR_NROCMP);
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
        public static DataTable GetProformas(SessionManager oSessionManager, string PR_CODEMP, string PR_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_PROFACTURADT.*, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = PR_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = PR_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = PR_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE PR_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(PR_NROCMP AS VARCHAR)+PR_TIPPRO+PR_CLAVE1+PR_CLAVE2+PR_CLAVE3+PR_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = PR_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = PR_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = PR_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = PR_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = PR_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = PR_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = PR_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (PR_CANTIDAD*PR_PRECIO) VLRTOT,TANOMBRE,ARNOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8, ");
                sql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3= ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM CMP_PROFACTURADT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = PR_CODEMP AND TATIPPRO = PR_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = PR_CODEMP AND ARTIPPRO = PR_TIPPRO AND ARCLAVE1 = PR_CLAVE1 AND ARCLAVE2 = PR_CLAVE2 AND ARCLAVE3 = PR_CLAVE3 AND ARCLAVE4 = PR_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE PR_CODEMP =@p0");
                sql.AppendLine("   AND PR_NROFACPROFORMA IN (" + PR_NROCMP + ")");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, PR_CODEMP);
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
        public static DataTable GetProformas(SessionManager oSessionManager, string inFilter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_PROFACTURADT.*, '               ' LOT1,'                 ' LOT2,      ");                
                sql.AppendLine(" WHERE PR_CODEMP =@p0 "+inFilter);


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetFactura(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_FACTURADT.*, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(FD_NROCMP AS VARCHAR)+FD_TIPPRO+FD_CLAVE1+FD_CLAVE2+FD_CLAVE3+FD_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = FD_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = FD_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = FD_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = FD_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = FD_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = FD_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = FD_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (FD_CANTIDAD*FD_PRECIO) VLRTOT,TANOMBRE,ARNOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8, ");
                sql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3= ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM CMP_FACTURADT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = FD_CODEMP AND TATIPPRO = FD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = FD_CODEMP AND ARTIPPRO = FD_TIPPRO AND ARCLAVE1 = FD_CLAVE1 AND ARCLAVE2 = FD_CLAVE2 AND ARCLAVE3 = FD_CLAVE3 AND ARCLAVE4 = FD_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE FD_CODEMP =@p0");
                sql.AppendLine("   AND FD_NROCMP =@p1");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP);
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
        public static DataTable GetFacturas(SessionManager oSessionManager, string FD_CODEMP, string FD_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT FD_CODEMP,FD_NROCMP PR_NROCMP,FD_NROITEM PR_NROITEM,FD_BODEGA,FD_TIPPRO PR_TIPPRO,FD_CLAVE1 PR_CLAVE1,FD_CLAVE2 PR_CLAVE2,FD_CLAVE3 PR_CLAVE3,FD_CLAVE4 PR_CLAVE4,FD_PROVEE,FD_REFPRO PR_REFPRO,FD_COLPRO,");
                sql.AppendLine("FD_CANTIDAD PR_CANTIDAD, FD_UNIDAD, FD_PRECIO PR_PRECIO, FD_OBSERVACIONES, FD_USUARIO, FD_ESTADO, FD_FECING, FD_FECMOD, FD_NROFACTURA PR_NROFACPROFORMA, FD_FECFAC PR_FECPROFORMA, FD_REFERENCIA,");
                sql.AppendLine("FD_DIAS, FD_ORIGEN PR_ORIGEN, FD_POSARA, FD_PAGO PR_PAGO, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(FD_NROCMP AS VARCHAR)+FD_TIPPRO+FD_CLAVE1+FD_CLAVE2+FD_CLAVE3+FD_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = FD_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = FD_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = FD_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = FD_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = FD_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = FD_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = FD_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (FD_CANTIDAD*FD_PRECIO) VLRTOT,TANOMBRE,ARNOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8, ");
                sql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3= ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM CMP_FACTURADT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = FD_CODEMP AND TATIPPRO = FD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = FD_CODEMP AND ARTIPPRO = FD_TIPPRO AND ARCLAVE1 = FD_CLAVE1 AND ARCLAVE2 = FD_CLAVE2 AND ARCLAVE3 = FD_CLAVE3 AND ARCLAVE4 = FD_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE FD_CODEMP =@p0");
                sql.AppendLine("   AND FD_NROFACTURA IN (" + FD_NROCMP + ") ");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, FD_CODEMP);
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
        public static DataTable GetCostos(SessionManager oSessionManager, string CT_CODEMP, int CH_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COSTOS.*,TRCODNIT,TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'') NOMBRE, ARNOMBRE");
                sql.AppendLine("FROM CMP_COSTOS WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CT_CODEMP AND CT_TIPPRO = ARTIPPRO AND CT_CLAVE1 = ARCLAVE1 AND CT_CLAVE2 = ARCLAVE2 AND CT_CLAVE3 = ARCLAVE3 AND CT_CLAVE4 = ARCLAVE4)");
                sql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (CT_CODEMP = TRCODEMP AND CMP_COSTOS.TRCODTER = TERCEROS.TRCODTER) ");
                sql.AppendLine("WHERE CT_CODEMP =@p0");
                sql.AppendLine("AND CH_NROCMP =@p1");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CT_CODEMP,CH_NROCMP);
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
        public static int InsertCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA,string CH_CNROCMPALT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_COMPRASHD (CH_CODEMP,CH_NROCMP,CH_BODEGA,CH_PROVEEDOR,CH_TIPORD,CH_FECORD,CH_TIPCMP,CH_TIPDPH,CH_TERPAG,CH_NROMUESTRA,CH_SERVICIO,");
                sSql.AppendLine("CH_VLRTOT,CH_OBSERVACIONES,CH_USUARIO,CH_ESTADO,CH_ORDENOR,CH_FENTREGA,CH_GENINV,CH_CMPINT,CH_MONEDA,CH_CNROCMPALT,CH_FECING,CH_FECMOD) VALUES ");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP, CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO,
                                                CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO, CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA, CH_CNROCMPALT);
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
        public static int InsertCompraDT(SessionManager oSessionManager,string CD_CODEMP,int CD_NROCMP,int CD_NROITEM,string CD_BODEGA,string CD_TIPPRO,string CD_CLAVE1,string CD_CLAVE2,
                                     string CD_CLAVE3,string CD_CLAVE4,int CD_PROVEE,string CD_REFPRO,string CD_COLPRO,double CD_CANTIDAD,double CD_CANSOL,string CD_UNIDAD,double CD_PRECIO,string CD_OBSERVACIONES,
                                     string CD_USUARIO,string CD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_COMPRASDT (CD_CODEMP,CD_NROCMP,CD_NROITEM,CD_BODEGA,CD_TIPPRO,CD_CLAVE1,CD_CLAVE2,CD_CLAVE3,CD_CLAVE4,CD_PROVEE,CD_REFPRO,CD_COLPRO,CD_CANTIDAD,CD_CANSOL,CD_UNIDAD,CD_PRECIO,");
                sSql.AppendLine("CD_OBSERVACIONES,CD_USUARIO,CD_ESTADO,CD_FECING,CD_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,CD_CODEMP,CD_NROCMP,CD_NROITEM,CD_BODEGA,CD_TIPPRO,CD_CLAVE1,CD_CLAVE2,CD_CLAVE3,CD_CLAVE4,CD_PROVEE,CD_REFPRO,CD_COLPRO,CD_CANTIDAD,CD_CANSOL,CD_UNIDAD,CD_PRECIO,
                                                CD_OBSERVACIONES,CD_USUARIO,CD_ESTADO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
            sSql =null;
            }
        }
        public static int UpdateCompraDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP, int CD_NROITEM, string CD_BODEGA, string CD_TIPPRO, string CD_CLAVE1, string CD_CLAVE2,
                                     string CD_CLAVE3, string CD_CLAVE4, int CD_PROVEE, string CD_REFPRO, string CD_COLPRO, double CD_CANTIDAD, double CD_CANSOL, string CD_UNIDAD, double CD_PRECIO, string CD_OBSERVACIONES,
                                     string CD_USUARIO, string CD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_COMPRASDT SET CD_BODEGA=@p3,CD_TIPPRO=@p4,CD_CLAVE1=@p5,CD_CLAVE2=@p6,CD_CLAVE3=@p7,CD_CLAVE4=@p8,CD_PROVEE=@p9,CD_REFPRO=@p10,CD_COLPRO=@p11,CD_CANTIDAD=@p12,CD_CANSOL=@p13,CD_UNIDAD=@p14,CD_PRECIO=@p15,");
                sSql.AppendLine("CD_OBSERVACIONES=@p16,CD_USUARIO=@p17,CD_ESTADO=@p18,CD_FECMOD=GETDATE()");
                sSql.AppendLine("WHERE CD_CODEMP=@p0 AND CD_NROCMP=@p1 AND CD_NROITEM=@p2");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CD_CODEMP, CD_NROCMP, CD_NROITEM, CD_BODEGA, CD_TIPPRO, CD_CLAVE1, CD_CLAVE2, CD_CLAVE3, CD_CLAVE4, CD_PROVEE, CD_REFPRO, CD_COLPRO, CD_CANTIDAD, CD_CANSOL, CD_UNIDAD, CD_PRECIO,
                                                CD_OBSERVACIONES, CD_USUARIO, CD_ESTADO);
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
        public static int ExisteItemCMPComprasDT(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP, int FD_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP=@p0 AND CD_NROCMP=@p1 AND CD_NROITEM=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROITEM));
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
        public static int InsertCMPFacturaDT(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP, int FD_NROITEM, string FD_BODEGA, string FD_TIPPRO, string FD_CLAVE1, string FD_CLAVE2,
                                     string FD_CLAVE3, string FD_CLAVE4, int FD_PROVEE, string FD_REFPRO, string FD_COLPRO, double FD_CANTIDAD, string FD_UNIDAD, double FD_PRECIO, string FD_OBSERVACIONES,
                                     string FD_USUARIO, string FD_ESTADO, string FD_NROFACTURA, DateTime? FD_FECFAC, string FD_REFERENCIA, int FD_DIAS, string FD_ORIGEN, string FD_POSARA,string FD_PAGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_FACTURADT (FD_CODEMP,FD_NROCMP,FD_NROITEM,FD_BODEGA,FD_TIPPRO,FD_CLAVE1,FD_CLAVE2,FD_CLAVE3,FD_CLAVE4,FD_PROVEE,FD_REFPRO,FD_COLPRO,FD_CANTIDAD,FD_UNIDAD,FD_PRECIO,");
                sSql.AppendLine("FD_OBSERVACIONES,FD_USUARIO,FD_ESTADO,FD_NROFACTURA,FD_FECFAC,FD_REFERENCIA, FD_DIAS,FD_ORIGEN,FD_POSARA,FD_PAGO,FD_FECING,FD_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROITEM, FD_BODEGA, FD_TIPPRO, FD_CLAVE1, FD_CLAVE2, FD_CLAVE3, FD_CLAVE4, FD_PROVEE, FD_REFPRO, FD_COLPRO, FD_CANTIDAD, FD_UNIDAD, FD_PRECIO,
                                                FD_OBSERVACIONES, FD_USUARIO, FD_ESTADO, FD_NROFACTURA, FD_FECFAC, FD_REFERENCIA, FD_DIAS, FD_ORIGEN, FD_POSARA, FD_PAGO);
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
        public static int UpdateCMPFacturaDT(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP, int FD_NROITEM, string FD_BODEGA, string FD_TIPPRO, string FD_CLAVE1, string FD_CLAVE2,
                                     string FD_CLAVE3, string FD_CLAVE4, int FD_PROVEE, string FD_REFPRO, string FD_COLPRO, double FD_CANTIDAD, string FD_UNIDAD, double FD_PRECIO, string FD_OBSERVACIONES,
                                     string FD_USUARIO, string FD_ESTADO, string FD_NROFACTURA, DateTime? FD_FECFAC, string FD_REFERENCIA, int FD_DIAS, string FD_ORIGEN, string FD_POSARA,string FD_PAGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_FACTURADT SET FD_BODEGA=@p3,FD_TIPPRO=@p4,FD_CLAVE1=@p5,FD_CLAVE2=@p6,FD_CLAVE3=@p7,FD_CLAVE4=@p8,FD_PROVEE=@p9,FD_REFPRO=@p10,FD_COLPRO=@p11,FD_CANTIDAD=@p12,FD_UNIDAD=@p13,FD_PRECIO=@p14,");
                sSql.AppendLine("FD_OBSERVACIONES=@p15,FD_USUARIO=@p16,FD_ESTADO=@p17,FD_NROFACTURA=@p18,FD_FECFAC=@p19,FD_REFERENCIA=@p20, FD_DIAS=@p21,FD_ORIGEN=@p22,FD_POSARA=@p23,FD_PAGO=@p24,FD_FECMOD=GETDATE() WHERE FD_CODEMP=@p0 AND FD_NROCMP=@p1 AND FD_NROITEM=@p2");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROITEM, FD_BODEGA, FD_TIPPRO, FD_CLAVE1, FD_CLAVE2, FD_CLAVE3, FD_CLAVE4, FD_PROVEE, FD_REFPRO, FD_COLPRO, FD_CANTIDAD, FD_UNIDAD, FD_PRECIO,
                                                FD_OBSERVACIONES, FD_USUARIO, FD_ESTADO, FD_NROFACTURA, FD_FECFAC, FD_REFERENCIA, FD_DIAS, FD_ORIGEN, FD_POSARA, FD_PAGO);
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
        public static int ExisteItemCMPFacturaDT(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP, int FD_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_CODEMP=@p0 AND FD_NROCMP=@p1 AND FD_NROITEM=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROITEM));
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
        public static int InsertCMPFacturaPRO(SessionManager oSessionManager, string PR_CODEMP, int PR_NROCMP, int PR_NROITEM, string PR_BODEGA, string PR_TIPPRO, string PR_CLAVE1, string PR_CLAVE2,
                                     string PR_CLAVE3, string PR_CLAVE4, int PR_PROVEE, string PR_REFPRO, string PR_COLPRO, double PR_CANTIDAD, string PR_UNIDAD, double PR_PRECIO, string PR_OBSERVACIONES,
                                     string PR_USUARIO, string PR_ESTADO, string PR_NROFACPROFORMA, DateTime? PR_FECPROFORMA, string PR_REFERENCIA, int PR_DIAS,string PR_PAGO,string PR_ORIGEN, string PR_POSARA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_PROFACTURADT (PR_CODEMP,PR_NROCMP,PR_NROITEM,PR_BODEGA,PR_TIPPRO,PR_CLAVE1,PR_CLAVE2,PR_CLAVE3,PR_CLAVE4,PR_PROVEE,PR_REFPRO,PR_COLPRO,PR_CANTIDAD,PR_UNIDAD,PR_PRECIO,");
                sSql.AppendLine("PR_OBSERVACIONES,PR_USUARIO,PR_ESTADO, PR_NROFACPROFORMA, PR_FECPROFORMA, PR_REFERENCIA, PR_DIAS,PR_PAGO,PR_ORIGEN, PR_POSARA,PR_FECING,PR_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PR_CODEMP, PR_NROCMP, PR_NROITEM, PR_BODEGA, PR_TIPPRO, PR_CLAVE1, PR_CLAVE2, PR_CLAVE3, PR_CLAVE4, PR_PROVEE, PR_REFPRO, PR_COLPRO, PR_CANTIDAD, PR_UNIDAD, PR_PRECIO,
                                                PR_OBSERVACIONES, PR_USUARIO, PR_ESTADO, PR_NROFACPROFORMA, PR_FECPROFORMA, PR_REFERENCIA, PR_DIAS, PR_PAGO, PR_ORIGEN, PR_POSARA);
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
        public static int UpdateCMPFacturaPRO(SessionManager oSessionManager, string PR_CODEMP, int PR_NROCMP, int PR_NROITEM, string PR_BODEGA, string PR_TIPPRO, string PR_CLAVE1, string PR_CLAVE2,
                                     string PR_CLAVE3, string PR_CLAVE4, int PR_PROVEE, string PR_REFPRO, string PR_COLPRO, double PR_CANTIDAD, string PR_UNIDAD, double PR_PRECIO, string PR_OBSERVACIONES,
                                     string PR_USUARIO, string PR_ESTADO, string PR_NROFACPROFORMA, DateTime? PR_FECPROFORMA, string PR_REFERENCIA, int PR_DIAS,string PR_PAGO,string PR_ORIGEN,string PR_POSARA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_PROFACTURADT SET PR_BODEGA=@p3,PR_TIPPRO=@p4,PR_CLAVE1=@p5,PR_CLAVE2=@p6,PR_CLAVE3=@p7,PR_CLAVE4=@p8,PR_PROVEE=@p9,PR_REFPRO=@p10,PR_COLPRO=@p11,PR_CANTIDAD=@p12,PR_UNIDAD=@p13,PR_PRECIO=@p14,");
                sSql.AppendLine("PR_OBSERVACIONES=@p15,PR_USUARIO=@p16,PR_ESTADO=@p17,PR_NROFACPROFORMA=@p18, PR_FECPROFORMA=@p19, PR_REFERENCIA=@p20,PR_DIAS=@p21,PR_PAGO=@p22,PR_ORIGEN=@p23,PR_POSARA=@p24,PR_FECMOD=GETDATE() WHERE PR_CODEMP=@p0 AND PR_NROCMP=@p1 AND PR_NROITEM=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PR_CODEMP, PR_NROCMP, PR_NROITEM, PR_BODEGA, PR_TIPPRO, PR_CLAVE1, PR_CLAVE2, PR_CLAVE3, PR_CLAVE4, PR_PROVEE, PR_REFPRO, PR_COLPRO, PR_CANTIDAD, PR_UNIDAD, PR_PRECIO,
                                                PR_OBSERVACIONES, PR_USUARIO, PR_ESTADO, PR_NROFACPROFORMA, PR_FECPROFORMA, PR_REFERENCIA, PR_DIAS, PR_PAGO,PR_ORIGEN,PR_POSARA);
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

        public static int UpdateCompraDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP,string CD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_COMPRASDT SET CD_ESTADO=@p2");
                sSql.AppendLine("WHERE CD_CODEMP=@p0 AND CD_NROCMP=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CD_CODEMP, CD_NROCMP, CD_ESTADO);
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
        public static int UpdateCMPFacturaPRO(SessionManager oSessionManager, string PR_CODEMP, int PR_NROCMP, string PR_NROFACPROFORMA, string PR_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("UPDATE CMP_PROFACTURADT SET PR_ESTADO=@p3");
                sSql.AppendLine("WHERE PR_CODEMP=@p0 AND PR_NROCMP=@p1 AND PR_NROFACPROFORMA=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PR_CODEMP, PR_NROCMP, PR_NROFACPROFORMA, PR_ESTADO);
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
        public static int UpdateCMPFacturaDT(SessionManager oSessionManager, string FD_CODEMP, int FD_NROCMP, string FD_NROFACTURA, string FD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_FACTURADT SET FD_ESTADO=@p3");
                sSql.AppendLine("WHERE FD_CODEMP=@p0 AND FD_NROCMP=@p1 AND FD_NROFACTURA=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROFACTURA, FD_ESTADO);
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
        public static int ExisteItemCMPFacturaPRO(SessionManager oSessionManager, string PR_CODEMP, int PR_NROCMP, int PR_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM CMP_PROFACTURADT WITH(NOLOCK) WHERE PR_CODEMP=@p0 AND PR_NROCMP=@p1 AND PR_NROITEM=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PR_CODEMP, PR_NROCMP, PR_NROITEM));
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
        public static int InsertCMCostos(SessionManager oSessionManager,string CT_CODEMP, int CH_NROCMP, string CT_TIPPRO, string CT_CLAVE1,
                                         string CT_CLAVE2,string CT_CLAVE3,string CT_CLAVE4,double CT_PRECIO,int? TRCODTER,
                                         string CT_TIPDOC,string CT_NUMDOC,DateTime CT_FECDOC,string CT_MONEDA,string CT_OBSERVACIONES,string CT_USUARIO,string CT_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_COSTOS (CT_CODEMP,CH_NROCMP,CT_TIPPRO,CT_CLAVE1,CT_CLAVE2,CT_CLAVE3,CT_CLAVE4,CT_PRECIO,");
                sSql.AppendLine("TRCODTER,CT_TIPDOC,CT_NUMDOC,CT_FECDOC,CT_MONEDA,CT_OBSERVACIONES,CT_USUARIO,CT_ESTADO,CT_FECING,CT_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CT_CODEMP, CH_NROCMP, CT_TIPPRO, CT_CLAVE1, CT_CLAVE2, CT_CLAVE3, CT_CLAVE4, CT_PRECIO, TRCODTER, CT_TIPDOC, CT_NUMDOC, CT_FECDOC, CT_MONEDA, CT_OBSERVACIONES, CT_USUARIO, CT_ESTADO);
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
        public static int DeleteCompraDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP, int CD_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM CMP_COMPRASDT WHERE CD_CODEMP = @p0 AND CD_NROCMP = @p1 AND CD_NROITEM = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text, CD_CODEMP, CD_NROCMP, CD_NROITEM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public static int DeleteCMPFacturaDT(SessionManager oSessionManager, string CD_CODEMP, int FD_NROCMP, int FD_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM CMP_FACTURADT WHERE FD_CODEMP = @p0 AND FD_NROCMP = @p1 AND FD_NROITEM = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CD_CODEMP, FD_NROCMP, FD_NROITEM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static int DeleteCMPFacturaPRO(SessionManager oSessionManager, string CD_CODEMP, int PR_NROCMP, int PR_NROITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM CMP_PROFACTURADT WHERE PR_CODEMP = @p0 AND PR_NROCMP = @p1 AND PR_NROITEM = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CD_CODEMP, PR_NROCMP, PR_NROITEM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static DataTable GetReciboHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO, CMP_RECIBOHD.*,CMP_COMPRASHD.*,RH_NRORECIBO ");
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine(" INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON(RH_CODEMP = CH_CODEMP AND RH_NROCMP = CH_NROCMP) ");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetReciboDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP, int RD_NRORECIBO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASDT.*,RD_CANTIDAD,TANOMBRE,ARNOMBRE, '               ' LOT1,'                 ' LOT2,");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE4                                  ");
                sql.AppendLine("                  END CLAVE4                                 ");
                sql.AppendLine("FROM CMP_COMPRASDT WITH(NOLOCK)");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = CD_CODEMP AND TATIPPRO = CD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CD_CODEMP AND ARTIPPRO = CD_TIPPRO AND ARCLAVE1 = CD_CLAVE1 AND ARCLAVE2 = CD_CLAVE2 AND ARCLAVE3 = CD_CLAVE3 AND ARCLAVE4 = CD_CLAVE4)");
                sql.AppendLine("   LEFT OUTER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (CMP_COMPRASDT.CD_NROCMP = CMP_RECIBOHD.RH_NROCMP AND CD_CODEMP = RH_CODEMP)");
                sql.AppendLine("   LEFT OUTER JOIN CMP_RECIBODT WITH(NOLOCK) ON (CMP_RECIBOHD.RH_NRORECIBO = CMP_RECIBODT.RD_NRORECIBO AND RD_ITEM = CMP_COMPRASDT.CD_NROITEM)");
                sql.AppendLine("WHERE CD_CODEMP=@p0");
                sql.AppendLine("  AND CD_NROCMP=@p1");
                sql.AppendLine("  AND RH_NRORECIBO=@p2");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CD_CODEMP,CD_NROCMP,RD_NRORECIBO);
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
        public static int InsertReciboHD(SessionManager oSessionManager, string RH_CODEMP, int RH_NRORECIBO, int RH_NROCMP, string RH_TIPDOC, string RH_NRODOC, DateTime? RH_FECDOC, string RH_OBSERVACIONES,
                                         string RH_ESTADO, string RH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_RECIBOHD (RH_CODEMP,RH_NRORECIBO,RH_NROCMP,RH_TIPDOC,RH_NRODOC,RH_FECDOC,RH_OBSERVACIONES,RH_ESTADO,RH_USUARIO,RH_FECING,RH_FECMOD)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RH_CODEMP, RH_NRORECIBO, RH_NROCMP, RH_TIPDOC, RH_NRODOC, RH_FECDOC, RH_OBSERVACIONES, RH_ESTADO, RH_USUARIO);
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
        public static int InsertReciboDT(SessionManager oSessionManager, string RD_CODEMP, int RD_NRORECIBO, int RD_ITEM, string RD_TIPPRO, string RD_CLAVE1, string RD_CLAVE2, string RD_CLAVE3, string RD_CLAVE4, string RD_UNIDAD,
                                         double RD_CANTIDAD, int RD_IDMOVI, string RD_USUARIO, string RD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_RECIBODT (RD_CODEMP,RD_NRORECIBO,RD_ITEM,RD_TIPPRO,RD_CLAVE1,RD_CLAVE2,RD_CLAVE3,RD_CLAVE4,RD_UNIDAD,RD_CANTIDAD,RD_IDMOVI,RD_USUARIO,RD_ESTADO,RD_FECING,RD_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RD_CODEMP, RD_NRORECIBO, RD_ITEM, RD_TIPPRO, RD_CLAVE1, RD_CLAVE2, RD_CLAVE3, RD_CLAVE4, RD_UNIDAD, RD_CANTIDAD, RD_IDMOVI, RD_USUARIO, RD_ESTADO);
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
        public static int UpdateEstCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP,string CH_ESTADO,string CH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {               
                sSql.AppendLine("UPDATE CMP_COMPRASHD SET CH_FECMOD = GETDATE(), CH_ESTADO =@p0,CH_USUARIO =@p1");
                sSql.AppendLine(" WHERE CH_CODEMP =@p2 AND CH_NROCMP =@p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_ESTADO, CH_USUARIO, CH_CODEMP, CH_NROCMP);
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
        public static int UpdateCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA, string CH_CNROCMPALT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CMP_COMPRASHD SET CH_BODEGA=@p2, CH_PROVEEDOR=@p3, CH_TIPORD=@p4, CH_FECORD=@p5, CH_TIPCMP=@p6, CH_TIPDPH=@p7, CH_TERPAG=@p8, CH_NROMUESTRA=@p9, CH_SERVICIO=@p10, ");
                sSql.AppendLine("                         CH_VLRTOT=@p11, CH_OBSERVACIONES=@p12, CH_USUARIO=@p13, CH_ESTADO=@p14, CH_ORDENOR=@p15, CH_FENTREGA=@p16, CH_GENINV=@p17, CH_CMPINT=@p18, ");
                sSql.AppendLine("                         CH_MONEDA=@p19, CH_CNROCMPALT=@p20");
                sSql.AppendLine(" WHERE CH_CODEMP =@p0 AND CH_NROCMP =@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP,
                                                CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO, CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO,
                                                CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA, CH_CNROCMPALT);
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
        public static IDataReader GetComprasHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASHD.*, BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO ='AP' THEN 'Aprobado' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO");
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine("WHERE CH_CODEMP =@p0 AND CH_NROCMP =@p1");
                
                return DBAccess.GetDataReader(oSessionManager, sql.ToString(), CommandType.Text,CH_CODEMP,CH_NROCMP);
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

        public static double GetUltimoPrecioOC(SessionManager oSessionManager, string CD_CODEMP, string CD_TIPPRO, string CD_CLAVE1)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CD_PRECIO");
                sql.AppendLine("FROM CMP_COMPRASDT WITH(NOLOCK)");
                sql.AppendLine("WHERE CD_CODEMP=@p0 AND CD_CLAVE1 = @p2 AND CD_TIPPRO=@p1");
                sql.AppendLine("AND CD_NROCMP = (");
                sql.AppendLine("SELECT MAX(CD_NROCMP) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CLAVE1 = @p2 AND CD_TIPPRO=@p1");
                sql.AppendLine(")");

                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text, CD_CODEMP, CD_TIPPRO, CD_CLAVE1));
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

        public static DataTable GetSeguimiento(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_SEGUIMIENTO.*,usua_nombres ");                
                sql.AppendLine("  FROM TB_SEGUIMIENTO WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN admi_tusuario WITH(NOLOCK) ON (usua_usuario = SG_USUARIO)");
                sql.AppendLine("WHERE CH_CODEMP =@p0 AND CH_NROCMP =@p1");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP);
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
        public static int InsertSeguimiento(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP,string SG_DESCRIPCION, string SG_USUARIO, string SG_ESTADO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT TB_SEGUIMIENTO (CH_CODEMP,CH_NROCMP,SG_DESCRIPCION,SG_USUARIO,SG_ESTADO,SG_FECING,SG_FECMOD) VALUES ");
                sql.AppendLine("(@p0,@p1,@p2,@p3,@p4,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, SG_DESCRIPCION, SG_USUARIO, SG_ESTADO);
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
        public static int AnularOrdenCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP,string CH_ESTADO,string CH_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("UPDATE CMP_COMPRASHD SET CH_ESTADO=@p2,CH_USUARIO=@p3,CH_FECMOD=GETDATE()");                
                sql.AppendLine("WHERE CH_CODEMP =@p0 AND CH_NROCMP =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
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
        public static int AnularOrdenCompraDT(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_ESTADO, string CH_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("UPDATE CMP_COMPRASDT SET CD_ESTADO=@p2,CD_USUARIO=@p3,CD_FECMOD=GETDATE()");
                sql.AppendLine("WHERE CD_CODEMP =@p0 AND CD_NROCMP =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
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
        public static int AnularProforma(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_ESTADO, string CH_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("UPDATE CMP_PROFACTURADT SET PR_ESTADO=@p2,PR_USUARIO=@p3,PR_FECMOD=GETDATE()");
                sql.AppendLine("WHERE PR_CODEMP =@p0 AND PR_NROCMP =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
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
        public static int AnularFactura(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_ESTADO, string CH_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("UPDATE CMP_FACTURADT SET FD_ESTADO=@p2,FD_USUARIO=@p3,FD_FECMOD=GETDATE()");
                sql.AppendLine("WHERE FD_CODEMP =@p0 AND FD_NROCMP =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_ESTADO, CH_USUARIO);
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
        public static DataTable GetSummari(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine(" SELECT DISTINCT 1 ITM,'Purchar Order' tipo,CAST(CD_NROCMP AS VARCHAR) CD_NROCMP ,CD_ESTADO");
                sql.AppendLine("FROM CMP_COMPRASDT WITH(NOLOCK)");
                sql.AppendLine("WHERE CD_NROCMP = @p0 AND CD_CODEMP=@p1");
                sql.AppendLine("UNION ALL");
                sql.AppendLine("SELECT DISTINCT 2 ITM, 'Proforma Invoice' tipo, PR_NROFACPROFORMA, PR_ESTADO");
                sql.AppendLine("FROM CMP_PROFACTURADT WITH(NOLOCK)");
                sql.AppendLine("WHERE PR_NROCMP = @p0 AND PR_CODEMP=@p1");
                sql.AppendLine("UNION ALL");
                sql.AppendLine("SELECT DISTINCT 3 ITM, 'Invoice' tipo, FD_NROFACTURA, FD_ESTADO");
                sql.AppendLine("FROM CMP_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("WHERE FD_NROCMP = @p0 AND FD_CODEMP=@p1");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CH_NROCMP,CH_CODEMP);
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
        #endregion
        //wr in
        #region
        public static DataTable GetWRIN(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_WRINHD.* ");
                sql.AppendLine("  FROM TB_WRINHD WITH(NOLOCK) ");                
                sql.AppendLine(" WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetWRINDT(SessionManager oSessionManager, int WIH_CONSECUTIVO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_WRINDT.*,ARNOMBRE,TANOMBRE,AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7, ");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARTICULO.ARCODEMP AND BTIPPRO = ARTICULO.ARTIPPRO AND BCLAVE1 = ARTICULO.ARCLAVE1 AND BCLAVE2 = ARTICULO.ARCLAVE2 AND BCLAVE3= ARTICULO.ARCLAVE3 AND BCLAVE4 = ARTICULO.ARCLAVE4) BARRAS");
                sql.AppendLine("  FROM TB_WRINDT WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (TB_WRINDT.ARCODEMP = ARTICULO.ARCODEMP AND TB_WRINDT.ARTIPPRO = ARTICULO.ARTIPPRO AND TB_WRINDT.ARCLAVE1 = ARTICULO.ARCLAVE1 ");
                sql.AppendLine("                                  AND TB_WRINDT.ARCLAVE2 = ARTICULO.ARCLAVE2 AND TB_WRINDT.ARCLAVE3 = ARTICULO.ARCLAVE3 AND TB_WRINDT.ARCLAVE4 = ARTICULO.ARCLAVE4 )");
                sql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TB_WRINDT.ARCODEMP = TACODEMP AND TB_WRINDT.ARTIPPRO = TATIPPRO)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARTICULO.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARTICULO.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARTICULO.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARTICULO.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARTICULO.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARTICULO.ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE WIH_CONSECUTIVO=@p0");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, WIH_CONSECUTIVO);
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
        public static DataTable GetCompraDTWRIN(SessionManager oSessionManager, string FD_CODEMP, string inFilter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT *,CAN_RESTANTE DIF,NOMTTEC1,NOMTTEC2,NOMTTEC3,NOMTTEC4,NOMTTEC5,NOMTTEC7, BARRAS");                
                sql.AppendLine("FROM (");
                sql.AppendLine("SELECT CMP_FACTURADT.*,TANOMBRE,ARNOMBRE,0 CAN_SOL,'N' CHK, ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = FD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = FD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = FD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE FD_CLAVE4                                  ");
                sql.AppendLine("                  END CLAVE4 ,                                ");
                sql.AppendLine(" (FD_CANTIDAD - ISNULL((SELECT SUM(WID_CANTIDAD) FROM TB_WRINDT WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TB_WRINHD WITH(NOLOCK) ON (TB_WRINHD.WIH_CONSECUTIVO = TB_WRINDT.WIH_CONSECUTIVO)");
                sql.AppendLine("   WHERE TB_WRINDT.ARCODEMP = FD_CODEMP AND TB_WRINDT.ARTIPPRO = FD_TIPPRO AND TB_WRINDT.ARCLAVE1 = FD_CLAVE1 ");
                sql.AppendLine("     AND TB_WRINDT.ARCLAVE2 = FD_CLAVE2 AND TB_WRINDT.ARCLAVE3 = FD_CLAVE3 AND TB_WRINDT.ARCLAVE4 = FD_CLAVE4 ");
                sql.AppendLine("     AND WIH_ESTADO ='AC' AND TB_WRINDT.WID_NROFACTURA=FD_NROFACTURA),0)) CAN_RESTANTE,AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7, ");
                sql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARTICULO.ARCODEMP AND BTIPPRO = ARTICULO.ARTIPPRO AND BCLAVE1 = ARTICULO.ARCLAVE1 AND BCLAVE2 = ARTICULO.ARCLAVE2 AND BCLAVE3= ARTICULO.ARCLAVE3 AND BCLAVE4 = ARTICULO.ARCLAVE4) BARRAS");
                sql.AppendLine("FROM CMP_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = FD_CODEMP AND TATIPPRO = FD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = FD_CODEMP AND ARTIPPRO = FD_TIPPRO AND ARCLAVE1 = FD_CLAVE1 AND ARCLAVE2 = FD_CLAVE2 AND ARCLAVE3 = FD_CLAVE3 AND ARCLAVE4 = FD_CLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARTICULO.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARTICULO.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARTICULO.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARTICULO.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARTICULO.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARTICULO.ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine("WHERE FD_CODEMP=@p0 " + inFilter);
                sql.AppendLine(" ) XXX_TMP WHERE CAN_RESTANTE <> 0");
                //sql.AppendLine("  AND FD_NROCMP=@p1");                

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, FD_CODEMP);
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
        public static int InsertWRIN(SessionManager oSessionManager, int WIH_CONSECUTIVO, string WIH_CODEMP, DateTime WIH_FECHA, double WIH_KILOS, double WIH_BULTOS, string BDBODEGA, 
            string WIH_OBSERVACIONES, string WIH_USUARIO, string WIH_ESTADO,string WIH_NROALT,int TSNROTRA,string WIH_DESPACHO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_WRINHD (WIH_CONSECUTIVO,WIH_CODEMP,WIH_FECHA,WIH_KILOS,WIH_BULTOS,BDBODEGA,WIH_OBSERVACIONES,WIH_USUARIO,WIH_ESTADO,WIH_NROALT,WIH_DESPACHO,TSNROTRA,WIH_FECING,WIH_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, WIH_CODEMP, WIH_FECHA, WIH_KILOS, WIH_BULTOS, BDBODEGA, WIH_OBSERVACIONES, WIH_USUARIO, WIH_ESTADO, WIH_NROALT, WIH_DESPACHO, TSNROTRA);            
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
        public static int UpdateWRIN(SessionManager oSessionManager, int WIH_CONSECUTIVO, string WIH_OBSERVACIONES, string WIH_USUARIO, string WIH_ESTADO, string WIH_NROALT, string WIH_DESPACHO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_WRINHD SET WIH_OBSERVACIONES=@p1,WIH_USUARIO=@p2,WIH_NROALT=@p3,WIH_DESPACHO=@p4,WIH_FECMOD=GETDATE()");
                sSql.AppendLine("WHERE WIH_CONSECUTIVO = @p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, WIH_OBSERVACIONES, WIH_USUARIO, WIH_NROALT, WIH_DESPACHO);
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
        public static int InsertWRINDT(SessionManager oSessionManager, int WIH_CONSECUTIVO, int WID_ITEM, string ARCODEMP, int CD_NROCMP, int CD_NROITEM, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string WID_NROFACTURA, int WID_CANTIDAD, 
                                       double WID_PRECIO, double WID_PRECIOVTA, string WID_USUARIO,int MBIDMOVI)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_WRINDT (WIH_CONSECUTIVO,WID_ITEM,ARCODEMP,CD_NROCMP,CD_NROITEM,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,WID_NROFACTURA,WID_CANTIDAD,WID_PRECIO, WID_PRECIOVTA,WID_USUARIO,MBIDMOVI,WID_FECING,WID_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, WID_ITEM, ARCODEMP, CD_NROCMP, CD_NROITEM, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, WID_NROFACTURA, WID_CANTIDAD, WID_PRECIO, WID_PRECIOVTA, WID_USUARIO, MBIDMOVI);
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
        public static int AnularWRIN(SessionManager oSessionManager, string WIH_CODEMP,int WIH_CONSECUTIVO,string WIH_USUARIO, string WIH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_WRINHD SET WIH_USUARIO=@p2,WIH_ESTADO=@p3,WIH_FECMOD=GETDATE() WHERE WIH_CONSECUTIVO = @p0 AND WIH_CODEMP =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, WIH_CODEMP, WIH_USUARIO, WIH_ESTADO);
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
        public static int InsertTrasladoWrIn(SessionManager oSessionManager, int WIH_CONSECUTIVO, int WID_ITEM, string MBCODEMP, int MBIDMOVI, int MBIDITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_TRASLADO_WRINDT (WIH_CONSECUTIVO, WID_ITEM, MBCODEMP, MBIDMOVI, MBIDITEM) VALUES (@p0,@p1,@p2,@p3,@p4)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WIH_CONSECUTIVO, WID_ITEM, MBCODEMP, MBIDMOVI, MBIDITEM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        #endregion
        //wr out
        #region
        public static DataTable GetWROUT(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_WROUTHD.*,(HDTIPFAC+'-'+CAST(HDNROFAC AS VARCHAR)) lkn,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOM_TER");
                sql.AppendLine("  FROM TB_WROUTHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = WOH_CODEMP AND TERCEROS.TRCODTER = TB_WROUTHD.TRCODTER)");
                sql.AppendLine(" WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetWROUTDT(SessionManager oSessionManager, int WOH_CONSECUTIVO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_WROUTDT.*,WID_NROFACTURA,ARNOMBRE,TANOMBRE,0 CAN_DIF ");
                sql.AppendLine(",AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7");
                sql.AppendLine("  FROM TB_WROUTDT WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (TB_WROUTDT.ARCODEMP = ARTICULO.ARCODEMP AND TB_WROUTDT.ARTIPPRO = ARTICULO.ARTIPPRO AND TB_WROUTDT.ARCLAVE1 = ARTICULO.ARCLAVE1 ");
                sql.AppendLine("                                  AND TB_WROUTDT.ARCLAVE2 = ARTICULO.ARCLAVE2 AND TB_WROUTDT.ARCLAVE3 = ARTICULO.ARCLAVE3 AND TB_WROUTDT.ARCLAVE4 = ARTICULO.ARCLAVE4 )");
                sql.AppendLine(" INNER JOIN TB_WRINDT WITH(NOLOCK) ON (TB_WROUTDT.WIH_CONSECUTIVO = TB_WRINDT.WIH_CONSECUTIVO AND TB_WROUTDT.WID_ID = TB_WRINDT.WID_ID)");
                sql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TB_WROUTDT.ARCODEMP = TACODEMP AND TB_WROUTDT.ARTIPPRO = TATIPPRO)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARTICULO.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARTICULO.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARTICULO.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARTICULO.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARTICULO.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARTICULO.ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine(" WHERE WOH_CONSECUTIVO=@p0");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, WOH_CONSECUTIVO);
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
        public static DataTable GetCompraDTWROUT(SessionManager oSessionManager, string FD_CODEMP, string inFilter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT *,CAN_RESTANTE DIF FROM (");
                sql.AppendLine("SELECT TB_WRINDT.*,TANOMBRE,ARNOMBRE,0 CAN_SOL,'N' CHK, ");
                sql.AppendLine("CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE");
                sql.AppendLine("                    FROM ARTICSEC WITH(NOLOCK)");      
                sql.AppendLine("                   WHERE ASCODEMP = TB_WRINDT.ARCODEMP");
                sql.AppendLine("                     AND ASTIPPRO = TB_WRINDT.ARTIPPRO");       
                sql.AppendLine("                     AND ASCLAVEO = TB_WRINDT.ARCLAVE2");       
                sql.AppendLine("                     AND ASNIVELC = 2 )");
                sql.AppendLine("    ELSE TB_WRINDT.ARCLAVE2");
                sql.AppendLine("  END CLAVE2,");                
                sql.AppendLine("CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE");
                sql.AppendLine("                    FROM ARTICSEC WITH(NOLOCK)");
                sql.AppendLine("                   WHERE ASCODEMP = TB_WRINDT.ARCODEMP");
                sql.AppendLine("                     AND ASTIPPRO = TB_WRINDT.ARTIPPRO");
                sql.AppendLine("                     AND ASCLAVEO = TB_WRINDT.ARCLAVE3");
                sql.AppendLine("                     AND ASNIVELC = 3 )");
                sql.AppendLine("    ELSE TB_WRINDT.ARCLAVE3");
                sql.AppendLine("  END CLAVE3,");
                sql.AppendLine("CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE");
                sql.AppendLine("                    FROM ARTICSEC WITH(NOLOCK)");
                sql.AppendLine("                   WHERE ASCODEMP = TB_WRINDT.ARCODEMP");
                sql.AppendLine("                     AND ASTIPPRO = TB_WRINDT.ARTIPPRO");
                sql.AppendLine("                     AND ASCLAVEO = TB_WRINDT.ARCLAVE4");
                sql.AppendLine("                     AND ASNIVELC = 4 )");
                sql.AppendLine("    ELSE TB_WRINDT.ARCLAVE4");
                sql.AppendLine("  END CLAVE4 ,");
                sql.AppendLine("(WID_CANTIDAD - ISNULL((SELECT SUM(WOD_CANTIDAD) FROM TB_WROUTDT WITH(NOLOCK)");  
                sql.AppendLine("INNER JOIN TB_WROUTHD WITH(NOLOCK) ON (TB_WROUTHD.WOH_CONSECUTIVO = TB_WROUTDT.WOH_CONSECUTIVO)");
                sql.AppendLine("WHERE TB_WROUTDT.WIH_CONSECUTIVO = TB_WRINDT.WIH_CONSECUTIVO");
                sql.AppendLine("AND TB_WRINDT.ARCODEMP = TB_WROUTDT.ARCODEMP AND TB_WRINDT.ARTIPPRO = TB_WROUTDT.ARTIPPRO AND TB_WRINDT.ARCLAVE1 = TB_WROUTDT.ARCLAVE1 ");
                sql.AppendLine("AND TB_WRINDT.ARCLAVE2 = TB_WROUTDT.ARCLAVE2 AND TB_WRINDT.ARCLAVE3 = TB_WROUTDT.ARCLAVE3 AND TB_WRINDT.ARCLAVE4 = TB_WROUTDT.ARCLAVE4 ");
                sql.AppendLine("AND WOH_ESTADO ='AC'),0)) CAN_RESTANTE");
                sql.AppendLine(",AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7");
                sql.AppendLine("FROM TB_WRINDT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO) ");
                sql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARTICULO.ARCODEMP = TB_WRINDT.ARCODEMP AND ARTICULO.ARTIPPRO = TB_WRINDT.ARTIPPRO AND ARTICULO.ARCLAVE1 = TB_WRINDT.ARCLAVE1 AND ARTICULO.ARCLAVE2 = TB_WRINDT.ARCLAVE2 AND ARTICULO.ARCLAVE3 = TB_WRINDT.ARCLAVE3 AND ARTICULO.ARCLAVE4 = TB_WRINDT.ARCLAVE4)");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARTICULO.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARTICULO.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARTICULO.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARTICULO.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARTICULO.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARTICULO.ARDTTEC7 AND FF.ASNIVELC = 10)");

                sql.AppendLine("WHERE ARTICULO.ARCODEMP=@p0 " + inFilter);
                sql.AppendLine(") XXX_TMP WHERE CAN_RESTANTE <> 0");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, FD_CODEMP);
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
        public static int InsertWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO, string WOH_CODEMP, DateTime WOH_FECHASAL, DateTime WOH_FECHAENT, string BDBODEGA, string BDBODEGA1, string BDBODEGA2,
            int TRCODTER, string WOH_OBSERVACIONES, string WOH_USUARIO, string WOH_ESTADO, int TSNROTRA, string WOH_NROALT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_WROUTHD (WOH_CONSECUTIVO,WOH_CODEMP,WOH_FECHASAL,WOH_FECHAENT,BDBODEGA,BDBODEGA1,BDBODEGA2,TRCODTER,WOH_OBSERVACIONES,WOH_USUARIO,WOH_ESTADO,TSNROTRA,WOH_NROALT,WOH_FECING,WOH_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WOH_CONSECUTIVO, WOH_CODEMP, WOH_FECHASAL, WOH_FECHAENT, BDBODEGA, BDBODEGA1, BDBODEGA2, TRCODTER, WOH_OBSERVACIONES, WOH_USUARIO, WOH_ESTADO,TSNROTRA, WOH_NROALT);
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
        public static int InsertWROUTDT(SessionManager oSessionManager, int WOH_CONSECUTIVO,string ARCODEMP,int WID_ID,int WIH_CONSECUTIVO,string ARTIPPRO,string ARCLAVE1,string ARCLAVE2,string ARCLAVE3,string ARCLAVE4,double WOD_CANTIDAD,double WOD_PRECIO,
            double WOD_PRECIOVTA, int TSNROTRA, int MIIDMOVI, int MIOTMOVI, int MBIDITEM, string WOD_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_WROUTDT (WOH_CONSECUTIVO,ARCODEMP,WID_ID,WIH_CONSECUTIVO,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,WOD_CANTIDAD,WOD_PRECIO,WOD_PRECIOVTA,TSNROTRA,MIIDMOVI,MIOTMOVI,MBIDITEM, WOD_USUARIO,WOD_FECING,WOD_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, WOH_CONSECUTIVO, ARCODEMP, WID_ID, WIH_CONSECUTIVO, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, WOD_CANTIDAD, WOD_PRECIO, WOD_PRECIOVTA, TSNROTRA, MIIDMOVI, MIOTMOVI, MBIDITEM, WOD_USUARIO);
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
        public static DataTable GetSoportesWROUT(SessionManager oSessionManager, string SP_TIPO, string SP_TIPPRO, string SP_CLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SPW_CONSECUTIVO ,SPW_TIPO,SPW_REFERENCIA,SPW_DESCRIPCION,SPW_EXTENCION,SPW_USUARIO,SPW_FECING,");
                sSql.AppendLine("'                                                                                                                                                                         ' RUTA");
                sSql.AppendLine("  FROM TB_SOPORTES_WROUT WITH(NOLOCK) WHERE SPW_TIPO = @p0 AND SPW_TIPPRO = @p1 AND SP_CLAVE1 =@p2");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SP_TIPO, SP_TIPPRO, SP_CLAVE1);
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
        public static int InsertSoporteWROUT(SessionManager oSessionManager, string SP_TIPO, int SP_REFERENCIA, string SP_DESCRIPCION, string SP_EXTENCION, object SP_IMAGEN, string SP_USUARIO, string SP_TIPPRO, string SP_CLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SOPORTES_WROUT  (SP_TIPO,SP_REFERENCIA,SP_DESCRIPCION,SP_EXTENCION,SP_IMAGEN,SP_USUARIO,SP_TIPPRO,SP_CLAVE1,SP_FECING)");
                sSql.AppendLine("  VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SP_TIPO, SP_REFERENCIA, SP_DESCRIPCION, SP_EXTENCION, SP_IMAGEN, SP_USUARIO, SP_TIPPRO, SP_CLAVE1);
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
        public static int UpdateWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO, string HDTIPFAC, int HDNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_WROUTHD SET HDTIPFAC=@p0, HDNROFAC=@p1 WHERE WOH_CONSECUTIVO=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDTIPFAC, HDNROFAC, WOH_CONSECUTIVO);
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
        public static DataTable GetTrasladosWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_WROUT_TRASLADO.*,A.BDNOMBRE BDBODEGA,B.BDNOMBRE OTBODEGA");
                sql.AppendLine("  FROM TB_WROUT_TRASLADO WITH(NOLOCK) ");
                sql.AppendLine("  INNER JOIN TRASLADO WITH(NOLOCK) ON (TB_WROUT_TRASLADO.TSNROTRA = TRASLADO.TSNROTRA) ");
                sql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (TRASLADO.TSBODEGA = A.BDBODEGA AND A.BDCODEMP = TSCODEMP) ");
                sql.AppendLine("INNER JOIN TBBODEGA B WITH(NOLOCK) ON (TRASLADO.TSOTBODE = B.BDBODEGA AND B.BDCODEMP = TSCODEMP) ");
                sql.AppendLine(" WHERE WOH_CONSECUTIVO=@p0");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, WOH_CONSECUTIVO);
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
        public static int InsertTrasladosWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO, int TSNROTRA)
        {
            StringBuilder sql = new StringBuilder();
            try
            {                
                sql.AppendLine("  INSERT INTO TB_WROUT_TRASLADO (WOH_CONSECUTIVO, TSNROTRA) VALUES (@p0,@p1)");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, WOH_CONSECUTIVO,TSNROTRA);
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
        #endregion
        //Pagos
        #region
        public static DataTable GetConsultaPagos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT FD_NROCMP,FD_NROFACTURA,FD_FECFAC,SUM(FD_CANTIDAD) CANTIDAD,SUM(FD_PRECIO*FD_CANTIDAD) TOT_FAC,FD_DIAS,DATEADD(DAY,FD_DIAS,FD_FECFAC) AS F_VENCIMIENTO,");
                sql.AppendLine("(SELECT(TRNOMBRE + ' ' + ISNULL(TRNOMBR2, '') + ' ' + ISNULL(TRAPELLI, '')) TERCERO FROM TERCEROS WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN CMP_COMPRASHD WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND TRCODTER = CH_PROVEEDOR)");
                sql.AppendLine("WHERE CH_NROCMP = FD_NROCMP");
                sql.AppendLine("AND CH_CODEMP = FD_CODEMP) TERCERO,");
                sql.AppendLine("(SELECT TRCODNIT FROM TERCEROS WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN CMP_COMPRASHD WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND TRCODTER = CH_PROVEEDOR)");
                sql.AppendLine("WHERE CH_NROCMP = FD_NROCMP");
                sql.AppendLine("AND CH_CODEMP = FD_CODEMP) NIT_TER");
                sql.AppendLine("FROM CMP_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("WHERE FD_ESTADO = 'AC' "+filter);
                sql.AppendLine("GROUP BY FD_NROCMP,FD_NROFACTURA,FD_FECFAC,FD_NROCMP,FD_CODEMP,FD_DIAS");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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
        public static DataTable GetDetalleFacturas(SessionManager oSessionManager, string filter, string FD_CODEMP, int FD_NROCMP, string FD_NROFACTURA)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT FD_TIPPRO,TANOMBRE,FD_CLAVE1,ARNOMBRE,FD_CANTIDAD,FD_PRECIO,(FD_CANTIDAD*FD_PRECIO) TOT");
                sql.AppendLine("FROM CMP_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(TACODEMP = FD_CODEMP AND TATIPPRO = FD_TIPPRO)");
                sql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = FD_CODEMP AND ARTIPPRO = FD_TIPPRO AND ARCLAVE1 = FD_CLAVE1 AND ARCLAVE2 = FD_CLAVE2 AND ARCLAVE3 = FD_CLAVE3 AND ARCLAVE4 = FD_CLAVE4)");
                sql.AppendLine("WHERE FD_CODEMP=@p0 AND FD_NROCMP=@p1 AND FD_NROFACTURA=@p2 ");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, FD_CODEMP, FD_NROCMP, FD_NROFACTURA);
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
        public static DataTable GetPagosHD(SessionManager oSessionManager, string filter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM TB_PAGOSHD WITH(NOLOCK)");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);                
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

        public static DataTable GetPagosDT(SessionManager oSessionManager, string HP_CODEMP, int HP_NROPAGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_PAGOSDT.*,TTDESCRI CONCEPTO,DP_FECING FD_FECFAC,'xxxxxxxxxxxxxxxxxxxx' DOCUMENTO,0 SALDO  ");
                sql.AppendLine("  FROM TB_PAGOSDT WITH(NOLOCK)");
                sql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = HP_CODEMP AND TTCODTAB ='CONREC' AND TTCODCLA = DP_CONCEPTO)");
                sql.AppendLine("WHERE HP_CODEMP=@p0 AND HP_NROPAGO=@p1");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, HP_CODEMP,HP_NROPAGO);
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

        public static DataTable GetFacturasxPago(SessionManager oSessionManager, string CH_CODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT FD_NROCMP,FD_NROFACTURA,SUM(FD_CANTIDAD) CANTIDAD,SUM(FD_PRECIO*FD_CANTIDAD) TOT_FAC,TTDESCRI,TTCODCLA,FD_FECFAC");
                sSql.AppendLine("  FROM CMP_FACTURADT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN CMP_COMPRASHD WITH(NOLOCK) ON (CH_CODEMP = FD_CODEMP AND CH_NROCMP = FD_NROCMP)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON(TTCODEMP = CH_CODEMP AND TTCODTAB = 'CONREC' AND TTESTADO='AC')");
                sSql.AppendLine("WHERE FD_ESTADO = 'AC'  AND CH_CODEMP =@p0 AND CH_PROVEEDOR=@p1 ");
                sSql.AppendLine("GROUP BY FD_NROCMP, FD_NROFACTURA, FD_NROCMP, FD_CODEMP, TTDESCRI,TTCODCLA,FD_FECFAC");

                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,CH_CODEMP,TRCODTER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }

        public static int InsertPagosHD(SessionManager oSessionManager, string HP_CODEMP, int HP_NROPAGO,DateTime HP_FECHA, int TRCODTER, int HP_NRORECFISICO, string HP_OBSERVACIONES, string HP_ESTADO,double HP_VALOR)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_PAGOSHD (HP_CODEMP,HP_NROPAGO,HP_FECHA,TRCODTER,HP_NRORECFISICO,HP_OBSERVACIONES,HP_ESTADO,HP_FECING,RH_VALOR) ");
                sql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE(),@p7) ");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, HP_CODEMP, HP_NROPAGO, HP_FECHA, TRCODTER, HP_NRORECFISICO, HP_OBSERVACIONES, HP_ESTADO, HP_VALOR);
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
        public static int InsertPagosDT(SessionManager oSessionManager, string HP_CODEMP, int HP_NROPAGO, int CH_NROCMP, string FD_NROFACTURA, string DP_CONCEPTO, double DP_VALOR, string DP_USUARIO, string DP_ESTADO, DateTime DP_FECREC)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_PAGOSDT (HP_CODEMP,HP_NROPAGO,CH_NROCMP,FD_NROFACTURA,DP_CONCEPTO,DP_VALOR,DP_USUARIO,DP_ESTADO,DP_FECREC,DP_FECING,DP_FECMOD) ");
                sql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, HP_CODEMP, HP_NROPAGO, CH_NROCMP, FD_NROFACTURA, DP_CONCEPTO, DP_VALOR, DP_USUARIO, DP_ESTADO, DP_FECREC);
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

        #endregion
    }
}
