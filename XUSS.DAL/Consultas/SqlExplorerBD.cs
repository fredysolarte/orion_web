using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Consultas
{
    public class SqlExplorerBD
    {
        public static DataTable ExecuteSQL(SessionManager oSessionManager, string sSql)
        {            
            try
            {                
                return DBAccess.GetDataTable(oSessionManager, sSql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public static int ExecuteNonSQL(SessionManager oSessionManager, string sSql)
        {
            try
            {
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
