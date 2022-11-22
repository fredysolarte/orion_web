using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Contabilidad;

namespace XUSS.BLL.Contabilidad
{
   public class PlanillaImpuestosBL
    {
        public DataTable GetPlanillaImpuestosHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaImpuestosBD.GetPlanillaImpuestosHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetPlanillaImpuestosDT(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaImpuestosBD.GetPlanillaImpuestosDT(oSessionManager, PH_CODIGO);
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
        public int InsertPlanillaImpuestosHD(string connection, string PH_NOMBRE, string PH_ESTADO, string PH_TIPPLA, object inDT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            PlanillaBD Obj = new PlanillaBD();
            int i = 0,pc_id=0;
            try
            {
                oSessionManager.BeginTransaction();
                int PH_CODIGO = ComunBL.GeneraConsecutivo(null, "PLCONT");                
                PlanillaImpuestosBD.InsertPlanillaImpuestosHD(oSessionManager, PH_CODIGO, PH_NOMBRE, PH_ESTADO, PH_TIPPLA);
                foreach(DataRow rw in (inDT as DataTable).Rows)
                {
                    i++;
                    foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + Convert.ToString(rw["PC_CODIGO"]) + "'").Rows)
                        pc_id = Convert.ToInt32(dt["PC_ID"]);

                    PlanillaImpuestosBD.InsertPlanillaImpuestosDT(oSessionManager, PH_CODIGO, Convert.ToString(rw["IM_IMPUESTO"]), Convert.ToDouble(rw["PI_PORCENTAJE"]), Convert.ToString(rw["PI_INDBASE"]), 
                        Convert.ToDouble(rw["PI_BASE"]), Convert.ToString(rw["PI_ESTADO"]), pc_id, Convert.ToString(rw["PI_NATURALEZA"]));
                }
                oSessionManager.CommitTranstaction();
                return PH_CODIGO;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }
        public int UpdatePlanillaImpuestosHD(string connection, int original_PH_CODIGO, string PH_NOMBRE, string PH_ESTADO, string PH_TIPPLA, object inDT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            PlanillaBD Obj = new PlanillaBD();
            int i = 0, pc_id = 0; 
            try
            {
                oSessionManager.BeginTransaction();
                int PH_CODIGO = original_PH_CODIGO;
                PlanillaImpuestosBD.UpdatePlanillaImpuestosHD(oSessionManager, PH_CODIGO, PH_NOMBRE, PH_ESTADO, PH_TIPPLA);
                foreach (DataRow rw in (inDT as DataTable).Rows)
                {
                    foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + Convert.ToString(rw["PC_CODIGO"]) + "'").Rows)
                        pc_id = Convert.ToInt32(dt["PC_ID"]);

                    if (PlanillaImpuestosBD.ExistePlanillaiMpuestosDT(oSessionManager,PH_CODIGO, Convert.ToString(rw["IM_IMPUESTO"]))==0)
                        PlanillaImpuestosBD.InsertPlanillaImpuestosDT(oSessionManager, PH_CODIGO, Convert.ToString(rw["IM_IMPUESTO"]), Convert.ToDouble(rw["PI_PORCENTAJE"]), Convert.ToString(rw["PI_INDBASE"]), Convert.ToDouble(rw["PI_BASE"]), Convert.ToString(rw["PI_ESTADO"]), pc_id, Convert.ToString(rw["PI_NATURALEZA"]));
                    else
                        PlanillaImpuestosBD.UpdatePlanillaImpuestosDT(oSessionManager, PH_CODIGO, Convert.ToInt32(rw["PI_ITEM"]), Convert.ToString(rw["IM_IMPUESTO"]), Convert.ToDouble(rw["PI_PORCENTAJE"]), Convert.ToString(rw["PI_INDBASE"]), Convert.ToDouble(rw["PI_BASE"]), Convert.ToString(rw["PI_ESTADO"]), Convert.ToString(rw["PI_NATURALEZA"]));
                }
                oSessionManager.CommitTranstaction();
                return PH_CODIGO;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
                Obj = null;
            }
        }
        public int DeletePlanillaImpuestosDT(string connection, int PI_ITEM)
        {
            SessionManager oSessionManager = new SessionManager(connection);            
            try
            {
                PlanillaImpuestosBD.DeletePlanillaImpuestosDT(oSessionManager, PI_ITEM);                                
                return 0;
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
        public DataTable GetImpuestosxTercero(string connection, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaImpuestosBD.GetImpuestosxTercero(oSessionManager, TRCODTER);
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
