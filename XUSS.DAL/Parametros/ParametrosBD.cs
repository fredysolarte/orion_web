using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class ParametrosBD
    {
        public static DataTable GetTiposFactura(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBTIPFAC");

                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
    }
}
