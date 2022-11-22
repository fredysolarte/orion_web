using DataAccess;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Contabilidad
{
    public class CierreBD
    {
        public void GenerarCierre(SessionManager oSessionManager, int inMes, int inAno , string inUsuario )
        {
            Database db = oSessionManager.GetDatabase();
            try
            {                

                using (DbCommand cmd = db.GetStoredProcCommand("PL_CIERRE_CONTABLE"))
                {
                    db.AddInParameter(cmd, "@p0", DbType.Int32, inMes);
                    db.AddInParameter(cmd, "@p1", DbType.Int32, inAno);
                    db.AddInParameter(cmd, "@p2", DbType.Int32, 5);
                    db.AddInParameter(cmd, "@USUARIO", DbType.String, inUsuario);
                    
                    db.ExecuteScalar(cmd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
            }
        }
    }
}
