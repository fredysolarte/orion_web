using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XUSS.DAL.Costos;

namespace XUSS.BLL.Costos
{
    public class CargarCostosBL
    {
        public DataTable GetCostos(string connection, string filter, int startRowIndex, int maximumRows)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return CargarCostosBD.GetCostos(oSessionManager, filter, startRowIndex, maximumRows);
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
        public int InsertCosto(string connection, string CT_CODEMP, int TSNROTRA, string CT_TIPPRO, string CT_CLAVE1, string CT_CLAVE2, string CT_CLAVE3, string CT_CLAVE4,
                               int TRCODTER, string CT_TIPDOC, string CT_NUMDOC, DateTime CT_FECDOC, string CT_MONEDA, double CT_PRECIO, string CT_OBSERVACIONES, string CT_USUARIO, string CT_ESTADO, string CT_TDOCORIGEN)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            { 
                return CargarCostosBD.InsertCosto(oSessionManager,CT_CODEMP, TSNROTRA, CT_TIPPRO, CT_CLAVE1, CT_CLAVE2, CT_CLAVE3, CT_CLAVE4,
                               TRCODTER, CT_TIPDOC, CT_NUMDOC, CT_FECDOC, CT_MONEDA, CT_PRECIO, CT_OBSERVACIONES, CT_USUARIO, CT_ESTADO, CT_TDOCORIGEN);
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
