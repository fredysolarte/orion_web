using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Correspondencia;

namespace XUSS.BLL.Correspondencia
{
    public class CorrespondenciaBL
    {
        //Corresondencia IN
        #region
        public DataTable GetCorrespondenciaHDIN(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCorrespondenciaHDIN(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetCorrespondenciDTIN(string connection, int CIH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCorrespondenciDTIN(oSessionManager, CIH_CODIGO);
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
        public DataTable GetCtasRestantes(string connection, int TRCODTER,string inFiltro)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCtasRestantes(oSessionManager, TRCODTER,inFiltro);
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
        public int InsertCorrespondenciaIN(string connection, string CIH_CODEMP, string CIH_DESCRIPCION, DateTime CIH_FECHA, string CIH_USUARIO, string CIH_ESTADO, object tbItems)
        {
            int ln_codigo = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                ln_codigo = ComunBL.GeneraConsecutivo(connection, "CORRIN", CIH_CODEMP);
                CorrespondenciaBD.InsertCorrespondenciaHDIN(oSessionManager, ln_codigo, CIH_CODEMP,CIH_DESCRIPCION,CIH_FECHA,CIH_USUARIO,CIH_ESTADO);
                foreach (DataRow rw in ((DataTable)tbItems).Rows)
                {
                    if (Convert.ToString(rw["CID_ESTADO"]) == "AN")
                        CorrespondenciaBD.UpdateCorrespondenciaDTIN(oSessionManager, Convert.ToInt32(rw["PH_CODIGO"]), Convert.ToString(rw["CID_ESTADO"]));

                    CorrespondenciaBD.InsertCorrespondenciaDTIN(oSessionManager,ln_codigo,Convert.ToInt32(rw["CID_ITEM"]),Convert.ToInt32(rw["PH_CODIGO"]),Convert.ToString(rw["CID_ASESOR"]),CIH_USUARIO,"AC",".");
                }
                oSessionManager.CommitTranstaction();
                return ln_codigo;
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
        public int UpdateCorrespondenciaIN(string connection, string CIH_CODEMP, int original_CIH_CODIGO, string CIH_DESCRIPCION, DateTime CIH_FECHA, string CIH_USUARIO, string CIH_ESTADO, object tbItems)
        {
            int ln_codigo = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                                
                foreach (DataRow rw in ((DataTable)tbItems).Rows)
                {
                    CorrespondenciaBD.UpdateCorrespondenciaDTIN(oSessionManager, original_CIH_CODIGO, Convert.ToInt32(rw["CID_ITEM"]), Convert.ToString(rw["CID_ESTADO"]), Convert.ToString(rw["CID_CAUSAE"]));
                }
                oSessionManager.CommitTranstaction();
                return ln_codigo;
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
        #endregion
        //Correspondencia OUT
        #region
        public DataTable GetCorrespondenciaHDOUT(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCorrespondenciaHDOUT(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetCorrespondenciaDTOUT(string connection, int COH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCorrespondenciDTOUT(oSessionManager, COH_CODIGO);
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
        public DataTable GetCtasRestantesIN(string connection, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return CorrespondenciaBD.GetCtasRestantesIN(oSessionManager, TRCODTER);
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
        public int InsertCorrespondenciaOUT(string connection, string COH_CODEMP, string COH_DESCRIPCION, DateTime COH_FECHA, string COH_USUARIO, string COH_ESTADO, object tbItems)
        {
            int ln_codigo = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                ln_codigo = ComunBL.GeneraConsecutivo(connection, "CORROUT", COH_CODEMP);
                CorrespondenciaBD.InsertCorrespondenciaHDOUT(oSessionManager, ln_codigo, COH_CODEMP, COH_DESCRIPCION, COH_FECHA, COH_USUARIO, COH_ESTADO);
                foreach (DataRow rw in ((DataTable)tbItems).Rows)
                {
                    CorrespondenciaBD.InsertCorrespondenciaDTOUT(oSessionManager, ln_codigo, Convert.ToInt32(rw["COD_ITEM"]), Convert.ToInt32(rw["PH_CODIGO"]), COH_USUARIO, "AC", ".");
                }
                oSessionManager.CommitTranstaction();
                return ln_codigo;
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
        #endregion
    }
}
