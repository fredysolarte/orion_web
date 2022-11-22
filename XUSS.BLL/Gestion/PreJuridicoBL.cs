using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Gestion;

namespace XUSS.BLL.Gestion
{
    public class PreJuridicoBL
    {
        public DataTable CargarObligaciones(string connection, string DD_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PreJuridicoBD.CargarObligaciones(oSessionManager, DD_CODEMP, TRCODTER);
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

        public DataTable GetPrejuridico(string connection, string PD_CODEMP, int TRCODTER)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PreJuridicoBD.GetPrejuridico(oSessionManager, PD_CODEMP, TRCODTER);
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

        public int InsertPrejuridico(string connection, string PD_CODEMP, int TRCODTER, string PD_TIPIFICACION, string PD_TELEFONO, string PD_EMAIL, string PD_OBSERVACION, string PD_USUARIO, string PD_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PreJuridicoBD.InsertPrejuridico(oSessionManager, PD_CODEMP, TRCODTER, PD_TIPIFICACION, PD_TELEFONO, PD_EMAIL, PD_OBSERVACION, PD_USUARIO, PD_ESTADO);
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
        public int InsertObligacion(string connection, string DD_CODEMP, int TRCODTER, string DD_NROOBLIGACION, string DD_DESCRIPCIN, string DD_TCARTERA, int DD_DIASMORA, double? DD_FCAPITAL, double? DD_FCORRIENTE, double? DD_FMORA, string DD_USUARIO, string DD_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return PreJuridicoBD.InsertObligacion(oSessionManager, DD_CODEMP, TRCODTER, DD_NROOBLIGACION, DD_DESCRIPCIN, DD_TCARTERA, DD_DIASMORA, DD_FCAPITAL, DD_FCORRIENTE, DD_FMORA, DD_USUARIO, DD_ESTADO);
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
