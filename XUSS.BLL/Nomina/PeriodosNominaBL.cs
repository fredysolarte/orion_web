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
    public class PeriodosNominaBL
    {
        public DataTable GetPeriodoNomina(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PeriodosNominaBD.GetPeriodoNomina(oSessionManager, filter, startRowIndex, maximumRows);
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
        public int InsertPeriodoNomina(string connection, string NMP_CODEMP, DateTime NMP_FECINI, DateTime NMP_FECFIN, string NMP_USUARIO, string NMP_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);            
            try
            {
                int NMP_CODIGO = ComunBL.GeneraConsecutivo(null, "PERNM");
                return PeriodosNominaBD.InsertPeriodoNomina(oSessionManager, NMP_CODIGO, NMP_CODEMP, NMP_FECINI, NMP_FECFIN, NMP_USUARIO, NMP_ESTADO);
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
        public int UpdatePeriodoNomina(string connection, int NMP_CODIGO, string NMP_CODEMP, DateTime NMP_FECINI, DateTime NMP_FECFIN, string NMP_USUARIO, string NMP_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PeriodosNominaBD.UpdatePeriodoNomina(oSessionManager, NMP_CODIGO, NMP_CODEMP, NMP_FECINI, NMP_FECFIN, NMP_USUARIO, NMP_ESTADO);
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
