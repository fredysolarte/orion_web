using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using DataAccess;
using XUSS.DAL.Tareas;

namespace XUSS.BLL.Tareas
{
    public class PollaBL
    {
        public DataTable GetGrupo(string connection, string Usuario, string inCadena)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PollaBD.GetGrupo(oSessionManager, HttpContext.Current.Session["UserLogon"].ToString(), inCadena);
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
