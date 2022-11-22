using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XUSS.DAL.Contabilidad;
using System.Data;
using DataAccess;
using XUSS.BLL.Comun;

namespace XUSS.BLL.Contabilidad
{
    public class TercerosContabilidadBL
    {
        public DataTable GetTerceros(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            TercerosContabilidadBD Obj = new TercerosContabilidadBD();
            try
            {
                return Obj.GetTerceros(oSessionManager, filter);
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
        public int InsertTerceros(string connection, string TR_CODEMP, string TR_TIPDOC, string TR_NUMDOC, string TR_PRIMERNOMBRE,
                                  string TR_PRIMERAPELLI, string TR_SEGUNDNOMBRE, string TR_SEGUNDAPELLI, string TR_DIRECCION, string TR_TELEFONO,
                                  string TR_USUARIO, string TR_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            int ln_contador = 0;
            TercerosContabilidadBD Obj = new TercerosContabilidadBD();

            try
            {
                oSessionManager.BeginTransaction();
                ln_contador = ComunBL.GeneraConsecutivo(null, "TERCON", TR_CODEMP);
                Obj.InsertTerceros(oSessionManager, ln_contador, TR_CODEMP, TR_TIPDOC, TR_NUMDOC, TR_PRIMERNOMBRE,
                                          TR_PRIMERAPELLI, TR_SEGUNDNOMBRE, TR_SEGUNDAPELLI, TR_DIRECCION, TR_TELEFONO, TR_USUARIO, TR_ESTADO);
                oSessionManager.CommitTranstaction();
                return ln_contador;
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
        public int UpdateTerceros(string connection, int TR_CONSE, string TR_TIPDOC, string TR_NUMDOC, string TR_PRIMERNOMBRE,
                                  string TR_PRIMERAPELLI, string TR_SEGUNDNOMBRE, string TR_SEGUNDAPELLI, string TR_DIRECCION, string TR_TELEFONO, string TR_USUARIO,
                                  string TR_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(null);
            TercerosContabilidadBD Obj = new TercerosContabilidadBD();

            try
            {
                return Obj.UpdateTerceros(oSessionManager, TR_CONSE, TR_TIPDOC, TR_NUMDOC, TR_PRIMERNOMBRE,
                                          TR_PRIMERAPELLI, TR_SEGUNDNOMBRE, TR_SEGUNDAPELLI, TR_DIRECCION, TR_TELEFONO, TR_USUARIO, TR_ESTADO);
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
