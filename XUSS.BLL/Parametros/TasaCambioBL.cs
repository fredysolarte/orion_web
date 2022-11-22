using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class TasaCambioBL
    {
        public DataTable GetTasas(string connection, string filter,DateTime inFecha, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TasaCambioBD.GetTasas(oSessionManager, filter, inFecha, startRowIndex, maximumRows);
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
        public int InsertTasaCambio(string connection, string TC_CODEMP, string TC_MONEDA, DateTime? TC_FECHA, double? TC_VALOR, string TC_USUARIO, string TC_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TasaCambioBD.InsertTasaCambio(oSessionManager, TC_CODEMP, TC_MONEDA, TC_FECHA, TC_VALOR, TC_USUARIO, TC_ESTADO);
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
        public int UpdateTasaCambio(string connection, string TC_CODEMP, string TC_MONEDA, DateTime? TC_FECHA, double? TC_VALOR, string TC_USUARIO, string TC_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TasaCambioBD.UpdateTasaCambio(oSessionManager, TC_CODEMP, TC_MONEDA, TC_FECHA, TC_VALOR, TC_USUARIO, TC_ESTADO);
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
        public Boolean ExisteTasa(string connection, string TC_MONEDA, DateTime? TC_FECHA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                if (TasaCambioBD.ExisteTasa(oSessionManager, TC_MONEDA, TC_FECHA) >= 1)
                    return true;
                else
                    return false;
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

        public DataTable GetTasas(string connection, DateTime? TC_FECHA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return TasaCambioBD.GetTasas(oSessionManager, TC_FECHA);
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
