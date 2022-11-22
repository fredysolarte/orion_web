using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Comun;

namespace XUSS.BLL.Comun
{
    public class VisorBL
    {
        public static DataTable GetReporte(string connection, int RR_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return VisorBD.GetReporte(oSessionManager, RR_CODIGO);
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
