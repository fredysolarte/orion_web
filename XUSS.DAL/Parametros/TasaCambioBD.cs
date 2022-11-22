using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class TasaCambioBD
    {
        public static DataTable GetTasas(SessionManager oSessionManager, string filter, DateTime inFecha,int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_TASACAMBIO WITH(NOLOCK) WHERE 1=1 AND CONVERT(DATE,TC_FECHA,101) = CASE WHEN CONVERT(DATE,@p0,101) = CONVERT(DATE,'01/01/1987',101) THEN CONVERT(DATE,TC_FECHA,101) ELSE CONVERT(DATE,@p0,101) END ");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inFecha);
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

        public static int InsertTasaCambio(SessionManager oSessionManager,string TC_CODEMP,string TC_MONEDA,DateTime? TC_FECHA,double? TC_VALOR,string TC_USUARIO,string TC_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_TASACAMBIO(TC_CODEMP,TC_MONEDA,TC_FECHA,TC_VALOR,TC_USUARIO,TC_ESTADO,TC_FECING,TC_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TC_CODEMP, TC_MONEDA, TC_FECHA, TC_VALOR, TC_USUARIO, TC_ESTADO);
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

        public static int UpdateTasaCambio(SessionManager oSessionManager, string TC_CODEMP, string TC_MONEDA, DateTime? TC_FECHA, double? TC_VALOR, string TC_USUARIO, string TC_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_TASACAMBIO SET TC_VALOR=@p3,TC_USUARIO=@p4,TC_ESTADO=@p5,TC_FECMOD=GETDATE()");
                sSql.AppendLine("WHERE TC_CODEMP=@p0 AND TC_MONEDA=@p1 AND TC_FECHA = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TC_CODEMP, TC_MONEDA, TC_FECHA, TC_VALOR, TC_USUARIO, TC_ESTADO);
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

        public static int ExisteTasa(SessionManager oSessionManager, string TC_MONEDA,DateTime? TC_FECHA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_TASACAMBIO WITH(NOLOCK) WHERE TC_MONEDA = @p0 AND TC_FECHA = @p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TC_MONEDA, TC_FECHA));
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

        public static DataTable GetTasas(SessionManager oSessionManager, DateTime? TC_FECHA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TC_MONEDA,TC_VALOR FROM TB_TASACAMBIO WITH(NOLOCK) WHERE CONVERT(DATE,TC_FECHA,101) = CONVERT(DATE,@p0,101)");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TC_FECHA);
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
