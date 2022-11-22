using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BE.Administracion;
using DAL.Administracion;
using DataAccess;


namespace BLL.Administracion
{
    public class AdmiColumnaBL
    {
        public List<AdmiColumna> GetList(string connection, string filter, int startRowIndex, int maximumRows)
        {
            AdmiColumnaDB objDB = new AdmiColumnaDB();
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                //oSessionManager.CreateConnection();
                return objDB.GetAllList(oSessionManager,filter,startRowIndex,maximumRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //oSessionManager.CloseConnection();
                objDB = null;
                oSessionManager = null;
            }
        }
    }
}
