using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Comun
{
    public class VisorBD
    {
        public static DataTable GetReporte(SessionManager oSessionManager, int RR_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT RR_REPORTE FROM TB_RREPORTE WHERE RR_CODEMP ='001' AND RR_CODIGO =@p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(),CommandType.Text, RR_CODIGO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
    }
}
