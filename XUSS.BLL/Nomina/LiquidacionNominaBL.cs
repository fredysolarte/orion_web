using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.DAL.Nomina;

namespace XUSS.BLL.Nomina
{
    public class LiquidacionNominaBL
    {
        public DataTable GetPlanillaNominaHD(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return LiquidacionNominaBD.GetPlanillaNominaHD(oSessionManager, filter, startRowIndex, maximumRows);
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
        public DataTable GetNovedades(string connection, int NMP_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);

            try
            {
                return LiquidacionNominaBD.GetNovedades(oSessionManager, NMP_CODIGO);
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
        public DataTable GetPlanillaNominaDT(string connection, int NMH_CODIGO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LiquidacionNominaBD.GetPlanillaNominaDT(oSessionManager, NMH_CODIGO);
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
        public int InsertPlanillaNomina(string connection, int NMP_CODIGO, string NMH_DESCRIPCION, string NMH_USUARIO, string NMH_ESTADO,object inDT,object inNovedades)
        {
            int NMH_CODIGO = 0;
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                NMH_CODIGO = ComunBL.GeneraConsecutivo(null, "NMPLAN");
                LiquidacionNominaBD.InsertPlanillaNominaHD(oSessionManager, NMH_CODIGO, NMP_CODIGO, NMH_DESCRIPCION, NMH_USUARIO, NMH_ESTADO);
                foreach (DataRow rw in ((DataTable)inDT).Rows)
                {
                    LiquidacionNominaBD.InsertPlanillaNominaDT(oSessionManager, NMH_CODIGO, Convert.ToInt32(rw["TRCODTER"]), Convert.ToInt32(rw["NMD_ORIGEN"]), Convert.ToInt32(rw["PD_CODIGO"]), Convert.ToDouble(rw["NMD_VALOR"]), NMH_ESTADO);
                }

                foreach (DataRow rw in ((DataTable)inNovedades).Rows)
                {
                    LiquidacionNominaBD.InsertNovedades(oSessionManager, NMP_CODIGO, Convert.ToInt32(rw["TRCODTER"]), Convert.ToString(rw["NV_CONCEPTO"]), Convert.ToDouble(rw["NV_VALOR"]), Convert.ToString(rw["NV_TIPOPV"]), Convert.ToString(rw["NV_TIPOSR"]), Convert.ToString(rw["NV_BASE"]), NMH_USUARIO, "AC");
                }

                oSessionManager.CommitTranstaction();
                return NMH_CODIGO;
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

        public int UpdatePlanillaNomina(string connection, int NMP_CODIGO, string NMH_DESCRIPCION, string NMH_USUARIO, string NMH_ESTADO, int original_NMH_CODIGO, object inDT, object inNovedades)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                //NMH_CODIGO = ComunBL.GeneraConsecutivo(null, "NMPLAN");
                LiquidacionNominaBD.UpdatePlanillaNominaHD(oSessionManager, original_NMH_CODIGO, NMP_CODIGO, NMH_DESCRIPCION, NMH_USUARIO, NMH_ESTADO);
                LiquidacionNominaBD.DeletePlanillaDT(oSessionManager, original_NMH_CODIGO);
                foreach (DataRow rw in ((DataTable)inDT).Rows)                
                    LiquidacionNominaBD.InsertPlanillaNominaDT(oSessionManager, original_NMH_CODIGO, Convert.ToInt32(rw["TRCODTER"]), Convert.ToInt32(rw["NMD_ORIGEN"]), Convert.ToInt32(rw["PD_CODIGO"]), Convert.ToDouble(rw["NMD_VALOR"]), NMH_ESTADO);

                LiquidacionNominaBD.DeleteNovedades(oSessionManager, NMP_CODIGO);
                foreach (DataRow rw in ((DataTable)inNovedades).Rows)                
                    LiquidacionNominaBD.InsertNovedades(oSessionManager, NMP_CODIGO, Convert.ToInt32(rw["TRCODTER"]), Convert.ToString(rw["NV_CONCEPTO"]), Convert.ToDouble(rw["NV_VALOR"]), Convert.ToString(rw["NV_TIPOPV"]), Convert.ToString(rw["NV_TIPOSR"]), Convert.ToString(rw["NV_BASE"]), NMH_USUARIO, "AC");                

                oSessionManager.CommitTranstaction();
                return original_NMH_CODIGO;
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
