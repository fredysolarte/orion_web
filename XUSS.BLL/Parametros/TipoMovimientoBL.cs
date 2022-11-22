using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class TipoMovimientoBL
    {
        public static DataTable GetTipoMovimiento(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoMovimientoBD.GetTipoMovimiento(oSessionManager, filter, startRowIndex, maximumRows);
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
        public int InsertTipoMovimiento(string connection, string TMCODEMP, string TMCDTRAN, string TMNOMBRE, string TMDESCRI, string TMENTSAL, string TMREQDOC, string TMTIPMOV, string TMMOVMAN,
            string TMOTTRAN, string TMMOVPAR, string TMCONMAQ, string TMORICST, string TMPRIORI, string TMBODCON, string TMACTFEC, string TMESTADO, string TMCAUSAE, string TMNMUSER, string TMCONPRO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoMovimientoBD.InsertTipoMovimiento(oSessionManager, TMCODEMP, TMCDTRAN, TMNOMBRE, TMDESCRI, TMENTSAL, TMREQDOC, TMCDTRAN, TMMOVMAN,
                                                                              TMOTTRAN, TMMOVPAR, TMCONMAQ, TMORICST, TMPRIORI, TMBODCON, TMACTFEC, TMESTADO, 
                                                                              TMCAUSAE, TMNMUSER, TMCONPRO);
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
        public int UpdateTipoMovimiento(string connection, string TMCODEMP, string TMCDTRAN, string TMNOMBRE, string TMDESCRI, string TMENTSAL, string TMREQDOC, string TMTIPMOV, string TMMOVMAN,
            string TMOTTRAN, string TMMOVPAR, string TMCONMAQ, string TMORICST, string TMPRIORI, string TMBODCON, string TMACTFEC, string TMESTADO, string TMCAUSAE, string TMNMUSER, string TMCONPRO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TipoMovimientoBD.UpdateTipoMovimiento(oSessionManager, TMCODEMP, TMCDTRAN, TMNOMBRE, TMDESCRI, TMENTSAL, TMREQDOC, TMCDTRAN, TMMOVMAN,
                                                                              TMOTTRAN, TMMOVPAR, TMCONMAQ, TMORICST, TMPRIORI, TMBODCON, TMACTFEC, TMESTADO,
                                                                              TMCAUSAE, TMNMUSER, TMCONPRO);
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
    }
}
