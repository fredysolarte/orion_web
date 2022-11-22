using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.CtDescuentos
{
    public class CtaUsuariosRegistradosBD
    {
        public static DataTable GetTB_UserDescuento(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBCODIGO, TBCODNIT, TBCODALT, TBFACTURA, TBFECMOD, TBESTADO, TBCAUSAE, TRNOMBRE, TRNOMBR2 ");
                sSql.AppendLine(" FROM TB_USERDESCUENTOS, TERCEROS WHERE TRCODNIT = TBCODNIT AND TRCODTER =(SELECT MAX(A.TRCODTER) FROM TERCEROS A WHERE A.TRCODNIT = TBCODNIT)");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
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

        public static int UpdateTb_UserDescuento(SessionManager oSessionManager, string TBCODNIT, int TBFACTURA)
        {
            StringBuilder sql = new StringBuilder();
            try 
            {
                sql.AppendLine("UPDATE TB_USERDESCUENTOS SET TBFECMOD= GETDATE(), TBESTADO = 'CE', TBCAUSAE = 'Redimido', TBFACTURA = @p0  WHERE TBCODNIT =@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, TBFACTURA, TBCODNIT);
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
