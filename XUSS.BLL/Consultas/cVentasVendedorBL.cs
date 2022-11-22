using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using DataAccess;
using XUSS.DAL.Consultas;

namespace XUSS.BLL.Consultas
{
    [DataObject(true)]
    public class cVentasVendedorBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetVentas(string connection, string filter, DateTime FecIni, DateTime FecFin)
        {
            cVentasVendedorBD ObjDB = new cVentasVendedorBD();
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ObjDB.GetVentas(oSessionManager, filter, FecIni, FecFin);

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
