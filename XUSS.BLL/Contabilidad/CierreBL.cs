using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XUSS.DAL.Contabilidad;

namespace XUSS.BLL.Contabilidad
{
    public class CierreBL
    {
        public void GenerarCierre(string connection, int inMes,int inAno,string inUsuario)
        {
            SessionManager oSessionManager = new SessionManager(null);
            CierreBD Obj = new CierreBD();
            try
            {
                Obj.GenerarCierre(oSessionManager, inMes, inAno, inUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }
    }
}
