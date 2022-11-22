using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class CstInventariosBD
    {
        public DataTable Get_Inventario(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();            
            try
            {
                sSql.AppendLine(" SELECT TANOMBRE,ARNOMBRE, ARTIPPRO, ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,(BBCANTID-(BBBODBOD+BBBODPRO+BBBODPED)) BBCANTID,BBBODEGA,IM_IMAGEN, BDNOMBRE ");
                sSql.AppendLine("   FROM ARTICULO WITH(NOLOCK), TBTIPPRO WITH(NOLOCK), TBBODEGA WITH(NOLOCK),BALANBOD WITH(NOLOCK) ");
                sSql.AppendLine("LEFT OUTER JOIN IMAGENES ON( ");
                sSql.AppendLine("        BBCODEMP = IM_CODEMP		");
                sSql.AppendLine("    AND BBTIPPRO = IM_TIPPRO		");
                sSql.AppendLine("    AND BBCLAVE1 = IM_CLAVE1		");
                sSql.AppendLine("    AND IM_CLAVE2 = '.'			");
                sSql.AppendLine("    AND IM_CLAVE3 = '.'			");
                sSql.AppendLine("    AND IM_CLAVE4 = '.' 			");
                sSql.AppendLine("    AND IM_TIPIMA = 1)             ");
                sSql.AppendLine("  WHERE ARTICULO.ARCODEMP = BALANBOD.BBCODEMP");
                sSql.AppendLine("    AND ARTICULO.ARTIPPRO = BALANBOD.BBTIPPRO");
                sSql.AppendLine("    AND ARTICULO.ARCLAVE1 = BALANBOD.BBCLAVE1");
                sSql.AppendLine("    AND ARTICULO.ARCLAVE2 = BALANBOD.BBCLAVE2");
                sSql.AppendLine("    AND ARTICULO.ARCLAVE3 = BALANBOD.BBCLAVE3");
                sSql.AppendLine("    AND ARTICULO.ARCLAVE4 = BALANBOD.BBCLAVE4");
                sSql.AppendLine("    AND ARTICULO.ARCODEMP = TBTIPPRO.TACODEMP");
                sSql.AppendLine("    AND ARTICULO.ARTIPPRO = TBTIPPRO.TATIPPRO");
                sSql.AppendLine("    AND BDCODEMP = BBCODEMP ");
                sSql.AppendLine("    AND BDBODEGA = BBBODEGA ");
                sSql.AppendLine("    AND BBCANTID > 0 ");
                sSql.AppendLine("    AND BBBODEGA IN('ID','SF','U2','U1','GR','13','SB','GL','SP','E1','E2','CM','BO','UN','CT','AD','MV','OA')");
                
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
        public static DataTable GetLinea(SessionManager oSessionManager)
        {
            StringBuilder sql = new StringBuilder();            
            sql.AppendLine("SELECT TATIPPRO,TANOMBRE FROM TBTIPPRO ");

            try
            {
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
        public static DataTable GetBodega(SessionManager oSessionManager)
        {
            StringBuilder sql = new StringBuilder();            
            sql.AppendLine("SELECT BDBODEGA,BDNOMBRE  FROM  TBBODEGA WHERE BDALMACE ='S' ");

            try
            {
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
    }
}
