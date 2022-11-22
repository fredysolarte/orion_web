using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class SoportesBL
    {
        //1 Invoces / Facturas
        //5 WR OUT
        //6 WR IN
        
        public DataTable GetSoportes(string connection, string SP_TIPO, int SP_REFERENCIA)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SoportesBD.GetSoportes(oSessionManager, SP_REFERENCIA, SP_TIPO);
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
        public DataTable GetSoporte(string connection, int SP_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SoportesBD.GetSoporte(oSessionManager, SP_CONSECUTIVO);
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
        public DataTable GetSoportesArticulos(string connection, string SP_TIPO, string SP_TIPPRO,string SP_CLAVE1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SoportesBD.GetSoportes(oSessionManager, SP_TIPO,SP_TIPPRO,SP_CLAVE1);
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
        public int DeleteSoporte(string connection, int SP_CONSECUTIVO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SoportesBD.DeleteSoporte(oSessionManager, SP_CONSECUTIVO);
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
        public DataTable GetCodSoporte(string connection, string SP_DESCRIPCION,string inTipo)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return SoportesBD.GetCodSoporte(oSessionManager, SP_DESCRIPCION, inTipo);
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
