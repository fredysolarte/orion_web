using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Compras;

namespace XUSS.BLL.Compras
{
    public class BillofLadingBL
    {
        public DataTable GetBLDT(string connection, int BLH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.GetBLDT(oSessionManager, BLH_CODIGO);
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
        public DataTable GetBLWROUT(string connection, int WOH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.GetBLWROUT(oSessionManager, WOH_CONSECUTIVO);
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
        public DataTable GetBLWRIN(string connection, int WIH_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.GetBLWRIN(oSessionManager, WIH_CONSECUTIVO);
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
        public DataTable GetBLCompra(string connection, int CH_NROCMP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.GetBLCompra(oSessionManager, CH_NROCMP);
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
        public DataTable GetBLTraslado(string connection, int TSNROTRA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.GetBLTraslado(oSessionManager, TSNROTRA);
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
        public int DeteleBLCompra(string connection, int BLC_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return BillofLadingBD.DeteleBLCompra(oSessionManager, BLC_CONSECUTIVO);
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
