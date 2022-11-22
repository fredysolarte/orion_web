using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Contabilidad
{
    public class MaestroDocumentosBD
    {
        public DataTable GetDocumentos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM CONT_DOCUMENTOS WITH(NOLOCK) WHERE 1=1 ");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
        public int InsertDocumento(SessionManager oSessionManager, string DOC_CODEMP, string DOC_IDENTI, string DOC_NOMBRE, int DOC_CONSE, string DOC_CLASE,
                                   string DOC_USUARIO, DateTime DOC_FECMOD, string DOC_ESTADO, string DOC_AINC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_DOCUMENTOS (DOC_CODEMP,DOC_IDENTI,DOC_NOMBRE,DOC_CONSE,DOC_CLASE,DOC_USUARIO,DOC_FECMOD,DOC_ESTADO,DOC_AINC)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DOC_CODEMP, DOC_IDENTI, DOC_NOMBRE, DOC_CONSE, DOC_CLASE, DOC_USUARIO, DOC_FECMOD, DOC_ESTADO, DOC_AINC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
        public int UpdateDocumento(SessionManager oSessionManager, string DOC_CODEMP, string DOC_IDENTI, string DOC_NOMBRE, int DOC_CONSE, string DOC_CLASE,
                                   string DOC_USUARIO, DateTime DOC_FECMOD, string DOC_AINC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CONT_DOCUMENTOS ");
                sSql.AppendLine("   SET DOC_CODEMP=@p0,DOC_NOMBRE=@p1,DOC_CONSE=@p2,DOC_CLASE=@p3,DOC_USUARIO=@p4,DOC_FECMOD=@p5,DOC_AINC=@p6");
                sSql.AppendLine(" WHERE DOC_IDENTI =@p7");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DOC_CODEMP, DOC_NOMBRE, DOC_CONSE, DOC_CLASE, DOC_USUARIO, DOC_FECMOD, DOC_AINC, DOC_IDENTI);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
        
    }
}
