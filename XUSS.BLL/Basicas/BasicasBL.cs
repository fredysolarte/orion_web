using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using XUSS.DAL.Basicas;
using DataAccess;

namespace XUSS.BLL.Basicas
{
    [DataObject(true)]
    public class BasicasBL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable GetTipPro(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return BasicasBD.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
                //ObjDB.GetTipPro(oSessionManager, filter, startRowIndex, maximumRows);
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
