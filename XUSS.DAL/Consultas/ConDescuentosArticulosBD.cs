using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class ConDescuentosArticulosBD
    {
        public static DataTable GetClave1(SessionManager oSessionManager, string TP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT ARCLAVE1 FROM ARTICULO WITH(NOLOCK) WHERE ARTIPPRO = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TP);
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
        public static DataTable GetClave2(SessionManager oSessionManager, string TP, string C1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT ARCLAVE2 FROM ARTICULO WITH(NOLOCK) WHERE ARTIPPRO = @p0 AND ARCLAVE1 =@p1");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TP, C1);
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
        public static DataTable GetClave3(SessionManager oSessionManager, string TP, string C1, string C2)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT ARCLAVE3 FROM ARTICULO WITH(NOLOCK) WHERE ARTIPPRO = @p0 AND ARCLAVE1 =@p1 AND ARCLAVE2 =@p2");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TP, C1, C2);
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
        public static double GetDescuento(SessionManager oSessionManager, string TP, string C1, string C2, string C3, string C4, string CB)
        {
            string CODEMP = "001";
            
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ISNULL(dbo.FGET_DESCUENTOART(@p0,@p1,@p2,@p3,@p4,@p5,@p6),0) FROM DUAL");
                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, TP, C1, C2, C3, C4, CB));
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
        public static IDataReader GetArticulo(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARNOMBRE,ARTIPPRO,ARUNDINV,ARCDIMPF");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBARRA ON (");
                sSql.AppendLine("ARCODEMP = BCODEMP");
                sSql.AppendLine("AND ARTIPPRO = BTIPPRO");
                sSql.AppendLine("AND ARCLAVE1 = BCLAVE1");
                sSql.AppendLine("AND ARCLAVE2 = BCLAVE2");
                sSql.AppendLine("AND ARCLAVE3 = BCLAVE3");
                sSql.AppendLine("AND ARCLAVE4 = BCLAVE4)");
                sSql.AppendLine("WHERE " + filter);
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text);
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
