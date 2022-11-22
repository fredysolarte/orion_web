using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Comun;
using XUSS.DAL.Inventarios;

namespace XUSS.BLL.Inventarios
{
    public class FotoInventarioBL
    {
        public DataTable GetFotoInv(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetFotoInv(oSessionManager, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public DataTable GetFotoBod(string connection, string FBCODEMP, int FBNROFOT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            try
            {
                return Obj.GetFotoBod(oSessionManager, FBCODEMP, FBNROFOT);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        public int InsertFotoBod(string connection, string FBCODEMP, int FBNROFOT,string FIBODEGA,string FIINVINI,string FINMUSER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            MovimientosBD Obj = new MovimientosBD();
            int ln_nrofoto = 0;
            try
            {
                oSessionManager.BeginTransaction();
                ln_nrofoto = ComunBD.GeneraConsecutivo(oSessionManager, "FOTOIN", FBCODEMP);
                Obj.InsertFotoBod(oSessionManager, FBCODEMP, ln_nrofoto, FIBODEGA, FIINVINI, FINMUSER);
                oSessionManager.CommitTranstaction();
                return ln_nrofoto;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
    }
}
