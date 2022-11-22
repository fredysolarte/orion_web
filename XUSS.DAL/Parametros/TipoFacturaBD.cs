using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class TipoFacturaBD
    {
        public DataTable GetTiposFactura(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  SELECT BDNOMBRE, BDBODEGA, TBTIPFAC.*");
                sSql.AppendLine("  FROM TBTIPFAC WITH(NOLOCK)  ");
                sSql.AppendLine("  INNER JOIN TBBODEGA WITH(NOLOCK) ON (BDCODEMP = TFCODEMP AND BDBODEGA = TFBODEGA )");
                sSql.AppendLine(" WHERE 1=1");
                
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
        public int UpdateTipoFactura(SessionManager oSessionManager, string TFCODEMP, string TFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE TBTIPFAC SET TFNROFAC = TFNROFAC + 1  , TFFECFAC = GETDATE()");
                sSql.AppendLine("WHERE TFCODEMP =@p0 AND TFTIPFAC =@p1  AND TFESTADO = 'AC'");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TFCODEMP, TFTIPFAC);
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
        public int GetUltimoNroFac(SessionManager oSessionManager, string TFCODEMP, string TFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT TFNROFAC FROM TBTIPFAC ");
                sSql.AppendLine("WHERE TFCODEMP =@p0 AND TFTIPFAC = @p1 AND TFESTADO='AC'");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TFCODEMP, TFTIPFAC));
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
        public int getMaxItemFac(SessionManager oSessionManager, string TFCODEMP, string TFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ISNULL(TFMAXITM,0) FROM TBTIPFAC ");
                sSql.AppendLine("WHERE TFCODEMP =@p0 AND TFTIPFAC = @p1 AND TFESTADO='AC'");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TFCODEMP, TFTIPFAC));
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
        public string GetNumeroResolucion(SessionManager oSessionManager, string RFCODEMP, string RFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT RFNRORES FROM TBRESFAC WITH(NOLOCK) ");
                sSql.AppendLine("WHERE RFCODEMP =@p0 AND RFTIPFAC = @p1 AND RFESTADO='AC'");

                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, RFCODEMP, RFTIPFAC));
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
        public static DataTable GetResolucion(SessionManager oSessionManager, string RFCODEMP, string RFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBRESFAC WITH(NOLOCK) ");
                sSql.AppendLine("WHERE RFCODEMP =@p0 AND RFTIPFAC = @p1 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, RFCODEMP, RFTIPFAC);
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

        public static int InsertTipoFactura(SessionManager oSessionManager, string TFCODEMP, string TFTIPFAC, string TFCLAFAC, string TFNOMBRE, int TFNROFAC, DateTime? TFFECFAC, string TFBODEGA,
                                     string TFCDTRAN, string TFEXPORT, string TFESTADO, string TFCAUSAE, string TFNMUSER, string TFLSTPRE, string TFPREFIJ, string TFREPORT,
                                     string TFFORFAC,int? TFMAXITM)
        {
            StringBuilder sSql = new StringBuilder(); 
            try
            { 
                sSql.AppendLine("INSERT INTO TBTIPFAC (TFCODEMP, TFTIPFAC, TFCLAFAC, TFNOMBRE, TFNROFAC, TFFECFAC, TFBODEGA, TFCDTRAN, TFEXPORT, TFESTADO, TFCAUSAE, ");
                sSql.AppendLine("TFNMUSER, TFLSTPRE, TFPREFIJ, TFREPORT, TFFORFAC, TFMAXITM, TFFECING, TFFECMOD ) ");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,TFCODEMP, TFTIPFAC, TFCLAFAC, TFNOMBRE, TFNROFAC, TFFECFAC, TFBODEGA, TFCDTRAN, TFEXPORT, TFESTADO, TFCAUSAE, 
                                                TFNMUSER, TFLSTPRE, TFPREFIJ, TFREPORT, TFFORFAC, TFMAXITM);
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
        public static int UpdateTipoFactura(SessionManager oSessionManager, string TFCODEMP, string TFTIPFAC, string TFCLAFAC, string TFNOMBRE, int TFNROFAC, DateTime? TFFECFAC, string TFBODEGA,
                                     string TFCDTRAN, string TFEXPORT, string TFESTADO, string TFCAUSAE, string TFNMUSER, string TFLSTPRE, string TFPREFIJ, string TFFORFAC,int? TFMAXITM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBTIPFAC SET TFCLAFAC=@p2, TFNOMBRE=@p3, TFNROFAC=@p4, TFFECFAC=@p5, TFBODEGA=@p6, TFCDTRAN=@p7, TFEXPORT=@p8, TFESTADO=@p9, TFCAUSAE=@p10, ");
                sSql.AppendLine("TFNMUSER=@p11, TFLSTPRE=@p12, TFPREFIJ=@p13, TFFORFAC=@p14, TFMAXITM=@p15,TFFECING=GETDATE(), TFFECMOD =GETDATE() ");
                sSql.AppendLine(" WHERE TFCODEMP=@p0 AND TFTIPFAC=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TFCODEMP, TFTIPFAC, TFCLAFAC, TFNOMBRE, TFNROFAC, TFFECFAC, TFBODEGA, TFCDTRAN, TFEXPORT, TFESTADO, TFCAUSAE,
                                                TFNMUSER, TFLSTPRE, TFPREFIJ,  TFFORFAC, TFMAXITM);
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
        public static int ExisteResolucion(SessionManager oSessionManager, string RFCODEMP, string RFTIPFAC, string RFNRORES)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM TBRESFAC WITH(NOLOCK) WHERE RFCODEMP=@p0 AND RFTIPFAC =@p1 AND RFNRORES=@p2 ");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, RFCODEMP, RFTIPFAC, RFNRORES));
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
        public static int DeleteResolucion(SessionManager oSessionManager, string RFCODEMP, string RFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBRESFAC WHERE RFCODEMP=@p0 AND RFTIPFAC =@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RFCODEMP, RFTIPFAC);
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
        public static int InsertResolucion(SessionManager oSessionManager,string RFCODEMP,string RFTIPFAC,string RFNRORES,DateTime RFFECRES,string RFTIPRES,int RFFACINI,int RFFACFIN,
                                           string RFESTADO,string RFCAUSAE,string RFNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBRESFAC (RFCODEMP,RFTIPFAC,RFNRORES,RFFECRES,RFTIPRES,RFFACINI,RFFACFIN,RFESTADO,RFCAUSAE,RFNMUSER,RFFECING,RFFECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RFCODEMP, RFTIPFAC, RFNRORES, RFFECRES, RFTIPRES, RFFACINI, RFFACFIN, RFESTADO, RFCAUSAE, RFNMUSER);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
            
            }
        }
        #endregion
        //Usuarios x TF
        #region
        public static DataTable GetUsuarioxTF(SessionManager oSessionManager, string TFTIPFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBTFACUSR.*,usua_usuario");
                sSql.AppendLine(" FROM admi_tusuario WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TBTFACUSR WITH(NOLOCK) ON (FUUSUARIO = usua_usuario AND FUTIPFAC=@p0)");
                sSql.AppendLine("");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TFTIPFAC);
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
        public static int InsertUsuarioxTF(SessionManager oSessionManager,string FUCODEMP, string FUUSUARIO, string FUTIPFAC, string FUUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBTFACUSR (FUCODEMP,FUUSUARIO,FUTIPFAC,FUUSER,FUESTADO,FUFECING,FUFECMOD) VALUES(@p0,@p1,@p2,@p3,'AC',GETDATE(),GETDATE())");
                
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,FUCODEMP, FUUSUARIO, FUTIPFAC, FUUSER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public static int ExisteUsuarioxTF(SessionManager oSessionManager, string FUCODEMP, string FUUSUARIO, string FUTIPFAC, string FUUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TBTFACUSR WITH(NOLOCK) WHERE FUCODEMP=@p0 AND FUUSUARIO=@p1 AND FUTIPFAC=@p2");

                return  Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, FUCODEMP, FUUSUARIO, FUTIPFAC));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static int DeleteUsuarioxTF(SessionManager oSessionManager, string FUCODEMP, string FUUSUARIO, string FUTIPFAC, string FUUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBTFACUSR WHERE FUCODEMP=@p0 AND FUUSUARIO=@p1 AND FUTIPFAC=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FUCODEMP, FUUSUARIO, FUTIPFAC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        public static DataTable GetTFxUsuario(SessionManager oSessionManager, string filter, string usua_usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TFNOMBRE,TFTIPFAC,TFBODEGA ");
                sSql.AppendLine("FROM TBTIPFAC WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTFACUSR WITH(NOLOCK) ON (TFCODEMP = FUCODEMP AND TFTIPFAC = FUTIPFAC)");
                sSql.AppendLine("WHERE FUUSUARIO=@p0");
                sSql.AppendLine("");
                
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usua_usuario);
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
