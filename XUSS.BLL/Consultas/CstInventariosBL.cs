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
    public class CstInventariosBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable Get_Inventario(string connection, string filter) 
        {
            CstInventariosBD ObjDB = new CstInventariosBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.Get_Inventario(oSessionManager, filter);
                
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
        public static DataTable GetLinea(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CstInventariosBD.GetLinea(oSessionManager);
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
        public static DataTable GetBodega(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CstInventariosBD.GetBodega(oSessionManager);
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
