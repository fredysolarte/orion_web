using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Contabilidad
{
    public class CausacionProveedoresBD
    {
        public DataTable GetTiposDocumentos(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DOC_IDENTI,DOC_NOMBRE FROM CONT_DOCUMENTOS WITH(NOLOCK) ");              
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
        public DataTable GetMovimientosHD(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM CONT_MOVIMIENTOSHD WITH(NOLOCK) WHERE 1=1 ");

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
        public DataTable GetMovimientosDT(SessionManager oSessionManager, string MVTH_CODEMP, int MVTH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT CONT_MOVIMIENTOSDT.*,PC_CODIGO,PC_NOMBRE,'            ' IM_IMPUESTO,");
                sSql.AppendLine("CASE WHEN MVTD_TIPDOC = '01' THEN 'Factura' WHEN MVTD_TIPDOC = '02' THEN 'Cta Cobro' WHEN MVTD_TIPDOC = '03' THEN 'Remision' END  MVTD_NOMTIPDOC ");
                sSql.AppendLine("  FROM CONT_MOVIMIENTOSDT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TB_PUC WITH(NOLOCK) ON (TB_PUC.PC_ID = CONT_MOVIMIENTOSDT.PC_ID)");
                sSql.AppendLine("WHERE MVTH_CODEMP=@p0 AND MVTH_CODIGO=@p1");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MVTH_CODEMP, MVTH_CODIGO);
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
        public int InsertMovimientoHD(SessionManager oSessionManager, int MVTH_CODIGO, string MVTH_CODEMP, string TFTIPFAC, int MVTH_NUMERO, string MVTH_DOCCON, int MVTH_DIA, int MVTH_MES,
                                      int MVTH_ANO, DateTime MVTH_FECMOV, string MVTH_CDUSER, string MVTH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_MOVIMIENTOSHD (MVTH_CODIGO,MVTH_CODEMP,TFTIPFAC,MVTH_NUMERO,MVTH_DOCCON,MVTH_DIA,MVTH_MES,MVTH_ANO,MVTH_FECMOV,MVTH_CDUSER,MVTH_ESTADO,");
                sSql.AppendLine("MVTH_FECMOD,MVTH_FECING)VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MVTH_CODIGO, MVTH_CODEMP, TFTIPFAC, MVTH_DOCCON, MVTH_NUMERO,
                                                MVTH_DIA, MVTH_MES, MVTH_ANO, MVTH_FECMOV, MVTH_CDUSER,MVTH_ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;                  
            }
            finally
            { 
            
            }
        }
        public int InsertMovimientoDT(SessionManager oSessionManager, string MVTD_CODEMP, int MVTH_CODIGO, int PC_ID, int TRCODTER, string MVTD_TIPDOC, string MVTD_NRODOC, DateTime MVTD_FECDOC, string MVTD_DESCRIPCION, double MVTD_CREDITO, double MVTD_DEBITO, string MVTD_CDUSER, string MVTD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try {

                sSql.AppendLine("INSERT INTO CONT_MOVIMIENTOSDT (MVTD_CODEMP,MVTH_CODIGO,PC_ID,TRCODTER,MVTD_TIPDOC,MVTD_TIPDOC,MVTD_FECDOC,MVTD_DESCRIPCION,MVTD_CREDITO,MVTD_DEBITO,MVTD_CDUSER,MVTD_ESTADO,MVTD_FECING,MVTD_FECMOD)");                
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,GETDATE(),GETDATE())");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MVTD_CODEMP, MVTH_CODIGO, PC_ID, TRCODTER, MVTD_TIPDOC, MVTD_NRODOC, MVTD_FECDOC, MVTD_DESCRIPCION, MVTD_CREDITO, MVTD_DEBITO, MVTD_CDUSER, MVTD_ESTADO); ;
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

        public int ExisteDocumento(SessionManager oSessionManager, int TRCODTER, string MVTD_TIPDOC, string MVTD_NRODOC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.AppendLine("SELECT COUNT(*)  FROM CONT_MOVIMIENTOSDT WITH(NOLOCK)");
                sSql.AppendLine("WHERE MVTD_TIPDOC=@p0 AND TRCODTER =@p1 AND MVTD_NRODOC =@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MVTD_TIPDOC, TRCODTER, MVTD_NRODOC));
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

        #region
        public static DataTable GetEvidencias(SessionManager oSessionManager, string MVEV_CODEMP, int MVTH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT MVEV_ID,MVEV_CODEMP, MVTH_CODIGO, MVEV_DESCRIPCION, MVEV_ANEXO, MVEV_USUARIO,");
                sSql.AppendLine("'                                                                                                                                                                                                                                                        ' ruta");
                sSql.AppendLine("FROM CONT_EVIDENCIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE MVEV_CODEMP=@p0 AND MVTH_CODIGO=@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MVEV_CODEMP, MVTH_CODIGO);
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
        public static int InsertEvidencia(SessionManager oSessionManager, string MVEV_CODEMP, int MVTH_CODIGO, string MVEV_DESCRIPCION, object MVEV_ANEXO, string MVEV_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_EVIDENCIA (MVEV_CODEMP, MVTH_CODIGO, MVEV_DESCRIPCION, MVEV_ANEXO, MVEV_USUARIO,MVEV_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MVEV_CODEMP, MVTH_CODIGO, MVEV_DESCRIPCION, MVEV_ANEXO, MVEV_USUARIO);
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
        public static DataTable GetEvidenciasFoto(SessionManager oSessionManager, int MVEV_ID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT MVEV_ANEXO");
                sSql.AppendLine("FROM CONT_EVIDENCIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE MVEV_ID=@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MVEV_ID);
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
        #endregion

    }
}
