using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Consultas;
using DataAccess;

namespace XUSS.BLL.Consultas
{
    [DataObject(true)]
    public class ConsultasFacturasBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetFacturasExistentes(string connection, string filter, string filter_, string filter__)
        {
            ConsultasFacturasBD ObjDB = new ConsultasFacturasBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetFacturasExistentes(oSessionManager, filter, filter_, filter__);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetDetalleFactura(string connection, string filter)
        {
            ConsultasFacturasBD ObjDB = new ConsultasFacturasBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDetalleFactura(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        public static DataTable GetDetalleFacturacion(string connection, string filter, int inMes, int inAno)
        {
            ConsultasFacturasBD ObjDB = new ConsultasFacturasBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDetalleFacturacion(oSessionManager, filter,inMes, inAno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }

        public static DataTable GetDetalleFacturacion(string connection, string filter)
        {
            ConsultasFacturasBD ObjDB = new ConsultasFacturasBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDetalleFacturacion(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjDB = null;
                oSessionManager = null;
            }
        }
    }
}
