using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Consultas;

namespace XUSS.BLL.Consultas
{
    public class ConDescuentosArticulosBL
    {
        public DataTable GetClave1(string connection, string TP)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConDescuentosArticulosBD.GetClave1(oSessionManager, TP);
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

        public DataTable GetClave2(string connection, string TP, string C1)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConDescuentosArticulosBD.GetClave2(oSessionManager, TP, C1);
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

        public DataTable GetClave3(string connection, string TP, string C1, string C2)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConDescuentosArticulosBD.GetClave3(oSessionManager, TP, C1, C2);
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

        public double GetDescuento(string connection, string TP, string C1, string C2, string C3, string C4, string CB)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConDescuentosArticulosBD.GetDescuento(oSessionManager, TP, C1, C2, C3, C4, CB);
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

        public static IDataReader GetArticulo(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return ConDescuentosArticulosBD.GetArticulo(oSessionManager, filter);
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
