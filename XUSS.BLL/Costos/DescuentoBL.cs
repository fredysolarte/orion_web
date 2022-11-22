using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Costos;
using System.Web;

namespace XUSS.BLL.Costos
{
    public class DescuentoBL
    {
        public DataTable GetDescuentosArticulos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return DescuentoBD.GetDescuentosArticulos(oSessionManager, filter);
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
        public DataTable GetTipoDescuento(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return DescuentoBD.GetTipoDescuento(oSessionManager);
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
        public int InsertDecuento(string connection, int ID_DESCUENTO, string BODEGA, string TP, string CLAVE1, string CLAVE2, string CLAVE3, string CLAVE4,
                                  double VALOR, DateTime FECHAINI, DateTime FECHAFIN)
        {
            int ln_conse = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                ln_conse = DescuentoBD.GetMaximoDescuento(oSessionManager)+1;
                return DescuentoBD.InsertDecuento(oSessionManager, ln_conse, ID_DESCUENTO, BODEGA, TP, CLAVE1, CLAVE2, CLAVE3, CLAVE4, VALOR, FECHAINI, FECHAFIN, HttpContext.Current.Session["UserLogon"].ToString());
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
