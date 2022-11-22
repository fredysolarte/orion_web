using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Contabilidad;

namespace XUSS.BLL.Contabilidad
{
    public class MaestroTercerosBL
    {
        public DataTable GetTercerosContabilidad(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            MaestroTecerosBD Obj = new MaestroTecerosBD();
            try
            {
                return Obj.GetTercerosContabilidad(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }
    }
}
