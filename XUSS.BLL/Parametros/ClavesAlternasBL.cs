using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class ClavesAlternasBL
    {
        public DataTable GetClavesAlternas(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ClavesAlternasBD.GetClavesAlternas(oSessionManager, filter, startRowIndex, maximumRows);
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
        public int InsertClaveAlterna(string connection, string ASCODEMP, string ASTIPPRO, string ASNIVELC, string ASCLAVEO, string ASNOMBRE, string ASDESCRI, string ASESTADO, string ASCAUSAE, string ASNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ClavesAlternasBD.InsertClaveAlterna(oSessionManager, ASCODEMP, ASTIPPRO, ASNIVELC, ASCLAVEO, ASNOMBRE, ASDESCRI, ASESTADO, ASCAUSAE, ASNMUSER);
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
        public int UpdateClaveAlterna(string connection, string ASCODEMP, string ASTIPPRO, string ASNIVELC, string ASCLAVEO, string ASNOMBRE, string ASDESCRI, string ASESTADO, string ASCAUSAE, string ASNMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ClavesAlternasBD.UpdateClaveAlterna(oSessionManager, ASCODEMP, ASTIPPRO, ASNIVELC, ASCLAVEO, ASNOMBRE, ASDESCRI, ASESTADO, ASCAUSAE, ASNMUSER);
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
        public DataTable GetClavesAlternas(string connection, string ARCODEMP, string ARTIPPRO, int ASNIVELC)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ClavesAlternasBD.GetClavesAlternas(oSessionManager, ARCODEMP, ARTIPPRO, ASNIVELC);
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
        public DataTable GetClavesAlternas(string connection, string ARCODEMP, string ARTIPPRO, int ASNIVELC, string ASESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ClavesAlternasBD.GetClavesAlternas(oSessionManager, ARCODEMP, ARTIPPRO, ASNIVELC, ASESTADO);
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
