using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using XUSS.DAL.Contabilidad;

namespace XUSS.BLL.Contabilidad
{
    public class MaestroDocumentosBL
    {
        public DataTable GetDocumentos(string connection, string filter)
        {
            SessionManager oSessionManager = new SessionManager(null);
            MaestroDocumentosBD Obj = new MaestroDocumentosBD();
            try
            {
                return Obj.GetDocumentos(oSessionManager, filter);
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
        public int InsertDocumento(string connection, string DOC_CODEMP, string DOC_IDENTI, string DOC_NOMBRE, int DOC_CONSE, string DOC_CLASE,
                                   string DOC_USUARIO, string DOC_ESTADO,string DOC_AINC)
        {
            MaestroDocumentosBD Obj = new MaestroDocumentosBD();
            SessionManager oSessionManager = new SessionManager(null);

            try
            {
                return Obj.InsertDocumento(oSessionManager, DOC_CODEMP, DOC_IDENTI, DOC_NOMBRE, DOC_CONSE, DOC_CLASE, DOC_USUARIO, System.DateTime.Today, DOC_ESTADO, DOC_AINC);
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

        public int UpdateDocumento(string connection, string DOC_CODEMP, string DOC_IDENTI, string DOC_NOMBRE, int DOC_CONSE, string DOC_CLASE,
                                   string DOC_USUARIO, string DOC_AINC)
        {
            SessionManager oSessionManager = new SessionManager(null);
            MaestroDocumentosBD Obj = new MaestroDocumentosBD();
            try
            {
                return Obj.UpdateDocumento(oSessionManager, DOC_CODEMP, DOC_IDENTI, DOC_NOMBRE, DOC_CONSE, DOC_CLASE, DOC_USUARIO, System.DateTime.Today, DOC_AINC);
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
