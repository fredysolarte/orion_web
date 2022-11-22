using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Consultas;

namespace XUSS.BLL.Consultas
{
    public class SqlExplorerBL
    {
        public DataTable ExecuteSQL(string connection, string sSql)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return SqlExplorerBD.ExecuteSQL(oSessionManager, sSql);
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

        public int ExecuteNonSQL(string connection, string sSql)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return SqlExplorerBD.ExecuteNonSQL(oSessionManager, sSql);
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
