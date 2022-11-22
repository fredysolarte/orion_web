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
    public class DesmPuntoVentaBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetDesAlmacen(string connection, String mes, String ano, string bodega)
        {
            DesmPuntoVentaBD ObjDB = new DesmPuntoVentaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDesAlmacen(oSessionManager, Convert.ToInt32(ano),Convert.ToInt32(mes),bodega);

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
        public DataTable GetDesAlmacen(string connection, String mes, String ano, string bodega, string dia)
        {
            DesmPuntoVentaBD ObjDB = new DesmPuntoVentaBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetDesAlmacen(oSessionManager, Convert.ToInt32(ano), Convert.ToInt32(mes), bodega,Convert.ToInt32(dia));

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
