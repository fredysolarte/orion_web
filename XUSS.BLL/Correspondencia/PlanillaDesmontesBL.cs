using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Correspondencia;
using XUSS.DAL.Terceros;

namespace XUSS.BLL.Correspondencia
{
    public class PlanillaDesmontesBL
    {
        public DataTable GetPlanillaDesmonteHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return PlanillaDesmontesBD.GetPlanillaDesmonteHD(oSessionManager, filter, startRowIndex, maximumRows);                
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
        public DataTable GetPlanillaDesmonteDT(string connection, int PDH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return PlanillaDesmontesBD.GetPlanillaDesmonteDT(oSessionManager, PDH_CODIGO);
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
        public DataTable GetCuentasRestantes(string connection, string inFlitro)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaDesmontesBD.GetCuentasRestantes(oSessionManager, inFlitro);
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
        public int InsertPlanilla(string connection, DateTime PDH_FECHA, string PDH_DESCRIPCION, string PDH_USUARIO, object tbDetalle)
        {
            int PDH_CODIGO = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                PDH_CODIGO = ComunBL.GeneraConsecutivo(connection, "PLADES", "001");
                PlanillaDesmontesBD.InsertPlanillaHD(oSessionManager, PDH_CODIGO, PDH_FECHA, PDH_DESCRIPCION, PDH_USUARIO);
                foreach (DataRow rw in ((DataTable)tbDetalle).Rows)
                {
                    //Planilla
                    PlanillaDesmontesBD.InsertPlanillaDT(oSessionManager, PDH_CODIGO, Convert.ToInt32(rw["PH_CODIGO"]), Convert.ToInt32(rw["PDD_TUERCAPLANA"]), Convert.ToInt32(rw["PDD_COPASCONICAS"]), Convert.ToInt32(rw["PDD_RACORFLARES"]),
                        Convert.ToInt32(rw["PDD_VALVULAALIVIO"]), Convert.ToInt32(rw["PDD_VALVULAAGUA"]), Convert.ToInt32(rw["PDD_CHEQUE"]), Convert.ToInt32(rw["PDD_CODOGALVANIZADO"]), Convert.ToInt32(rw["PDD_CODOCALLE"]), Convert.ToInt32(rw["PDD_MGFLEXOMETALICA"]),
                        Convert.ToInt32(rw["PDD_COBREMT"]), Convert.ToString(rw["PDD_TECNICO"]));
                    //Ejecucion Desmonte
                    if (TercerosBD.ExisteDesmontaje(oSessionManager, Convert.ToInt32(rw["PH_CODIGO"]))==0)
                        TercerosBD.InsertDesmontaje(oSessionManager, Convert.ToInt32(rw["PH_CODIGO"]), "001", PDH_FECHA, Convert.ToString(rw["PDD_TECNICO"]), "");
                }
                oSessionManager.CommitTranstaction();
                return PDH_CODIGO;
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
