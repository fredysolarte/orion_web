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
    public class CtaVentasBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
         public DataTable GetVentas(string connection, string filter, DateTime FecIni,DateTime FecFin)
        {
            CtaVentasBD ObjDB = new CtaVentasBD();
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
