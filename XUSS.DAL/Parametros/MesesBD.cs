using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Parametros
{
    public class MesesBD
    {
        public static DataTable GetAnos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT MA_ANO FROM CONT_MES_ANO WITH(NOLOCK) WHERE 1=1");
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
        public static DataTable GetMeses(SessionManager oSessionManager, int MA_ANO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,CASE WHEN MA_MES = 1 THEN 'Enero' WHEN MA_MES = 2 THEN 'Febrero' WHEN MA_MES = 3 THEN 'Marzo' WHEN MA_MES = 4 THEN 'Abril' ");
                sSql.AppendLine("WHEN MA_MES = 5 THEN 'Mayo' WHEN MA_MES = 6 THEN 'Junio' WHEN MA_MES = 7 THEN 'Julio' WHEN MA_MES = 8 THEN 'Agosto' WHEN MA_MES = 9 THEN 'Septiembre'");
                sSql.AppendLine("WHEN MA_MES = 10 THEN 'Octubre' WHEN MA_MES = 11 THEN 'Noviembre' WHEN MA_MES = 12 THEN 'Diciembre' END NOM_MES FROM CONT_MES_ANO WITH(NOLOCK) WHERE MA_ANO=@p0");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,MA_ANO);
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

        public static int InsertMeses(SessionManager oSessionManager, string MA_CODEMP,int MA_ANO, int MA_MES, string MA_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO CONT_MES_ANO (MA_CODEMP,MA_MES,MA_ANO,MA_USUARIO,MA_FECING) VALUES (@p0,@p1,@p2,@p3,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MA_CODEMP, MA_MES, MA_ANO, MA_USUARIO);
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
