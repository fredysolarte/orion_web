using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.CtDescuentos;

namespace XUSS.BLL.CtDescuentos
{
    public class CtaUsuariosRegistradosBL
    {
        public DataTable GetTB_UserDescuento(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CtaUsuariosRegistradosBD.GetTB_UserDescuento(oSessionManager, filter, startRowIndex, maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int UpdateTB_UserDescuento(string connection, string TBCODNIT, string TRNOMBRE, string TRNOMBR2, int TBFACTURA, string TBCODALT, string TBESTADO, DateTime TBFECMOD)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CtaUsuariosRegistradosBD.UpdateTb_UserDescuento(oSessionManager, TBCODNIT, TBFACTURA);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
    }
}
