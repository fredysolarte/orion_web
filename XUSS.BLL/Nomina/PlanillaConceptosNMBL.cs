using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Contabilidad;
using XUSS.DAL.Nomina;

namespace XUSS.BLL.Nomina
{
    public class PlanillaConceptosNMBL
    {
        public DataTable GetPlanillaConceptosHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaConceptosNMBD.GetPlanillaConceptosHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetPlanillaConceptosDT(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaConceptosNMBD.GetPlanillaConceptosDT(oSessionManager, PH_CODIGO);
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
        public int InsertPlanillaConceptosHD(string connection, string PH_CODEMP, string PH_NOMBRE, string PH_ESTADO, string PH_USUARIO, object inDT)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            PlanillaBD Obj = new PlanillaBD();
            int i = 0, pc_id = 0;
            try
            {
                oSessionManager.BeginTransaction();
                int PH_CODIGO = ComunBL.GeneraConsecutivo(null, "PLCONT");
                PlanillaConceptosNMBD.InsertPlanillaConceptosHD(oSessionManager, PH_CODEMP, PH_CODIGO, PH_NOMBRE, PH_ESTADO, PH_USUARIO);
                foreach (DataRow rw in (inDT as DataTable).Rows)
                {
                    i++;
                    foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + Convert.ToString(rw["PC_CODIGO"]) + "'").Rows)
                        pc_id = Convert.ToInt32(dt["PC_ID"]);

                    PlanillaConceptosNMBD.InsertPlanillaConceptosDT(oSessionManager, PH_CODIGO, Convert.ToString(rw["PD_CODEMP"]), Convert.ToString(rw["PD_TIPO"]), Convert.ToString(rw["PD_TIPOPV"]), Convert.ToString(rw["PD_CONCEPTO"]),
                        Convert.ToDouble(rw["PD_VALOR"]), Convert.ToString(rw["PD_BASE"]), Convert.ToString(rw["PD_OCONCEPTO"]), pc_id, Convert.ToString(rw["PD_USUARIO"]),"AC");
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
        public int UpdatePlanillaConceptosHD(string connection, string PH_CODEMP ,string PH_NOMBRE, string PH_ESTADO, string PH_USUARIO, object inDT, int original_PH_CODPLAN)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            PlanillaBD Obj = new PlanillaBD();
            int i = 0, pc_id = 0;
            try
            {
                oSessionManager.BeginTransaction();
                int PH_CODIGO = original_PH_CODPLAN;
                //PlanillaConceptosNMBD.UpdatePlanillaImpuestosHD(oSessionManager, PH_CODIGO, PH_NOMBRE, PH_ESTADO, PH_TIPPLA);
                foreach (DataRow rw in (inDT as DataTable).Rows)
                {
                    foreach (DataRow dt in Obj.GetPuc(oSessionManager, " AND PC_CODIGO='" + Convert.ToString(rw["PC_CODIGO"]) + "'").Rows)
                        pc_id = Convert.ToInt32(dt["PC_ID"]);

                    if (PlanillaConceptosNMBD.ExistePlanillaConceptosDT(oSessionManager, PH_CODIGO, Convert.ToString(rw["PD_CONCEPTO"])) == 0)
                        PlanillaConceptosNMBD.InsertPlanillaConceptosDT(oSessionManager, PH_CODIGO, Convert.ToString(rw["PD_CODEMP"]), Convert.ToString(rw["PD_TIPO"]), Convert.ToString(rw["PD_TIPOPV"]), Convert.ToString(rw["PD_CONCEPTO"]),
                        Convert.ToDouble(rw["PD_VALOR"]), Convert.ToString(rw["PD_BASE"]), Convert.ToString(rw["PD_OCONCEPTO"]), pc_id, Convert.ToString(rw["PD_USUARIO"]), "AC");
                    else
                        PlanillaConceptosNMBD.UpdatePlanillaConceptosDT(oSessionManager, Convert.ToInt32(rw["PD_CODIGO"]), Convert.ToString(rw["PD_CODEMP"]), Convert.ToString(rw["PD_TIPO"]), Convert.ToString(rw["PD_TIPOPV"]), Convert.ToString(rw["PD_CONCEPTO"]),
                            Convert.ToDouble(rw["PD_VALOR"]), Convert.ToString(rw["PD_BASE"]), Convert.ToString(rw["PD_OCONCEPTO"]), pc_id, PH_USUARIO,"AC");
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
                PlanillaConceptosNMBD.DeletePlanillaImpuestosDT(oSessionManager, PI_ITEM);
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
        public DataTable GetPlanillasxTercero(string connection, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PlanillaConceptosNMBD.GetPlanillasxTercero(oSessionManager, TRCODTER);
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
        public int InsertPlanillaNMTercero(string connection, int PH_CODIGO, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                PlanillaConceptosNMBD.InsertPlanillaNMTercero(oSessionManager, PH_CODIGO, TRCODTER);
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
        public int DeletePlanillaNMTercero(string connection, int PH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                PlanillaConceptosNMBD.DeletePlanillaNMTercero(oSessionManager, PH_CODIGO);
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
    }

}
