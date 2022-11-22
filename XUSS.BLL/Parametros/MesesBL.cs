using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Parametros;

namespace XUSS.BLL.Parametros
{
    public class MesesBL
    {
        public DataTable GetAnos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return MesesBD.GetAnos(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetMeses(string connection, string MA_CODEMP,int MA_ANO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return MesesBD.GetMeses(oSessionManager, MA_ANO);
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
        public int InsertMeses(string connection, string MA_CODEMP, int MA_ANO, string MA_USUARIO,object tbMes)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try {
                oSessionManager.BeginTransaction();
                foreach (DataRow rw in (tbMes as DataTable).Rows)
                {
                    MesesBD.InsertMeses(oSessionManager, MA_CODEMP, MA_ANO, Convert.ToInt32(rw["MA_MES"]), MA_USUARIO);
                }
                oSessionManager.CommitTranstaction();
                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }            
        }
    }
}
