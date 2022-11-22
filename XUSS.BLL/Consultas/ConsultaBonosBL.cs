using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using XUSS.DAL.Consultas;

namespace XUSS.BLL.Consultas
{
    public class ConsultaBonosBL
    {
        public DataTable GetConsultaBonos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return ConsultaBonosBD.GetConsultaBonos(oSessionManager, filter, startRowIndex, maximumRows);
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
