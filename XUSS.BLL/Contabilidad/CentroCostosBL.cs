using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Contabilidad;

namespace XUSS.BLL.Contabilidad
{
    public class CentroCostosBL
    {
        public DataTable GetCentroCostos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            CentroCostosBD Obj = new CentroCostosBD();
            try
            {
                return Obj.GetCentroCostos(oSessionManager, filter);
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
        public int InsertCentroCostos(string connection, string CTC_CODEMP, string CTC_IDENTI, string CTC_NOMBRE, string CTC_USUARIO, string CTC_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            CentroCostosBD Obj = new CentroCostosBD();
            try
            {
                return Obj.InsertCentroCostos(oSessionManager, CTC_CODEMP, CTC_IDENTI, CTC_NOMBRE, CTC_USUARIO, System.DateTime.Now, CTC_ESTADO);
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
