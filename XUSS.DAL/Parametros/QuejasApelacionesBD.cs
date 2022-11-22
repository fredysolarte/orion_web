using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.BLL.Parametros
{
    public class QuejasApelacionesBD
    {
        public static DataTable GetQuejaSolicitudeApelacion(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_QUEJAS_SOLICITUD_APELACION.*,TRNOMBRE,TRNOMBR2,TRAPELLI,TRNOMBR3,TRCODNIT,TRNROTEL,TRCORREO,TRDIRECC,TRTIPDOC,TRDIGCHK,TB_RESPUESTAS.* ");
                sSql.AppendLine("  FROM TB_QUEJAS_SOLICITUD_APELACION WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TERCEROS.TRCODTER = TB_QUEJAS_SOLICITUD_APELACION.TRCODTER)");
                sSql.AppendLine("LEFT OUTER JOIN TB_RESPUESTAS WITH(NOLOCK) ON (TB_RESPUESTAS.TR_CODEMP = TB_QUEJAS_SOLICITUD_APELACION.TQ_CODEMP AND TB_RESPUESTAS.TQ_NUMERO = TB_QUEJAS_SOLICITUD_APELACION.TQ_NUMERO)");
                sSql.AppendLine("WHERE 1=1");
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
        public static int InsertQuejaSolicitudApelacion(SessionManager oSessionManager, string TQ_CODEMP, int TQ_NUMERO, string TQ_TRATAMIENTO, int TRCODTER, string TQ_TIPO, DateTime TQ_FECHA, string TQ_DESCRIPCION,
                                                        string TQ_ESTADO, string TQ_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_QUEJAS_SOLICITUD_APELACION (TQ_CODEMP, TQ_NUMERO, TQ_TRATAMIENTO, TRCODTER, TQ_TIPO, TQ_FECHA, TQ_DESCRIPCION, TQ_ESTADO, TQ_USUARIO, TQ_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE())");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TQ_CODEMP, TQ_NUMERO, TQ_TRATAMIENTO, TRCODTER, TQ_TIPO, TQ_FECHA, TQ_DESCRIPCION, TQ_ESTADO, TQ_USUARIO);
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
        public static int UpdateQuejaSolicitudApelacion(SessionManager oSessionManager, string TQ_CODEMP, int TQ_NUMERO, string TQ_TIPO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_QUEJAS_SOLICITUD_APELACION SET  TQ_TIPO=@p2");
                sSql.AppendLine(" WHERE TQ_CODEMP=@p0 AND TQ_NUMERO=@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TQ_CODEMP, TQ_NUMERO, TQ_TIPO);
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
        public static int InsertDetalleQuejaSolicitudApelacion(SessionManager oSessionManager, string TR_CODEMP,int TQ_NUMERO,string TR_RESPONSABLE,string TR_CARGO,string TR_VALIDA,string TR_PRIORIDAD,string TR_PROCESO,
                                                                string TR_DESICION,string TR_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_RESPUESTAS (TR_CODEMP, TQ_NUMERO, TR_RESPONSABLE, TR_CARGO, TR_VALIDA, TR_PRIORIDAD, TR_PROCESO, TR_DESICION, TR_USUARIO, TR_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TR_CODEMP, TQ_NUMERO, TR_RESPONSABLE, TR_CARGO, TR_VALIDA, TR_PRIORIDAD, TR_PROCESO,
                                                                TR_DESICION, TR_USUARIO);
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
        public static int DeleteDetalleRespuestaQUejaApelacion(SessionManager oSessionManager, string TR_CODEMP, int TQ_NUMERO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_RESPUESTAS WHERE TR_CODEMP=@p0 AND TQ_NUMERO=@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text, TR_CODEMP, TQ_NUMERO);
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
        public static DataTable GetImagenes(SessionManager oSessionManager, string IE_CODEMP, int TQ_NUMERO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *              ");
                sSql.AppendLine("  FROM TB_EVIDENCIAS_SOLICITUDES WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE IE_CODEMP = @p0");
                sSql.AppendLine("   AND TQ_NUMERO = @p1");                

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, IE_CODEMP, TQ_NUMERO);
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
        public static DataTable GetImagenesAntencionCliente(SessionManager oSessionManager, int AT_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EI_CODIGO,IP_CODIGO,");
                sSql.AppendLine("CASE");
                sSql.AppendLine("WHEN EI_TIPO = 1 THEN 'Lecturas'");
                sSql.AppendLine("WHEN EI_TIPO = 2 THEN 'Trazado'");
                sSql.AppendLine("WHEN EI_TIPO = 3 THEN 'Linea Matriz'");
                sSql.AppendLine("WHEN EI_TIPO = 4 THEN 'Artefactos'");
                sSql.AppendLine("WHEN EI_TIPO = 5 THEN 'Ventilacion'");
                sSql.AppendLine("WHEN EI_TIPO = 6 THEN 'Panoramica'");
                sSql.AppendLine("WHEN EI_TIPO = 7 THEN 'Vacio Interno'");
                sSql.AppendLine("WHEN EI_TIPO = 8 THEN 'Evacuacion'");
                sSql.AppendLine("WHEN EI_TIPO = 9 THEN 'co sin Artefactos'");
                sSql.AppendLine("WHEN EI_TIPO = 10 THEN 'co con Artefactos'");
                sSql.AppendLine("WHEN EI_TIPO = 11 THEN 'Recibo'");
                sSql.AppendLine("WHEN EI_TIPO = 12 THEN 'Certificado'");
                sSql.AppendLine("END Tipo_Nombre, EI_TIPO,             ");
                sSql.AppendLine("'                                                                                                                                                                                                ' ruta              ");
                sSql.AppendLine("  FROM TB_EVIDENCIAS_INSPECCION WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE IP_CODIGO IN (SELECT IP_CODIGO FROM TB_INSPECCION WITH(NOLOCK) WHERE AT_CODIGO = @p0)");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, AT_CODIGO);
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
        public static DataTable GetImagenAtencionCliente(SessionManager oSessionManager, int EI_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EI_FOTO");                
                sSql.AppendLine("  FROM TB_EVIDENCIAS_INSPECCION WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE EI_CODIGO = @p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, EI_CODIGO);
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
        public static int deleteImagenAntencionCliente(SessionManager oSessionManager, int EI_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("  DELETE FROM TB_EVIDENCIAS_INSPECCION ");
                sSql.AppendLine(" WHERE EI_CODIGO = @p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, EI_CODIGO);
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
        public static int insertImagenAntencionCliente(SessionManager oSessionManager, int IP_CODIGO, int EI_TIPO, object EI_FOTO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  INSERT INTO  TB_EVIDENCIAS_INSPECCION (IP_CODIGO, EI_TIPO, EI_FOTO) VALUES ((SELECT TOP 1 IP_CODIGO FROM TB_INSPECCION WITH(NOLOCK) WHERE AT_CODIGO = @p0),@p1,@p2)");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IP_CODIGO, EI_TIPO, EI_FOTO);
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
        //Imagenes/Fotografias/Dibujos
        public static int InsertImagen(SessionManager oSessionManager, string IE_CODEMP, int TQ_NUMERO, string IE_TIPIMA, object IE_IMAGEN, string IE_DESCRIPCION, string IE_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_EVIDENCIAS_SOLICITUDES (IE_CODEMP,TQ_NUMERO,IE_TIPIMA,IE_IMAGEN,IE_DESCRIPCION,IE_USUARIO,IE_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IE_CODEMP, TQ_NUMERO, IE_TIPIMA, IE_IMAGEN, IE_DESCRIPCION, IE_USUARIO);
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
        public static DataTable getAtencionCLiente(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_ATENCIONCLIENTE.* ,TRCODEMP, TRNOMBRE, TRNOMBR2, TRAPELLI, TRNOMBR3, TRNOMCOMERCIAL, TRFECNAC, TRCORREO, TRNROTEL, TRDIRECC, TRTIPDOC, TRCODNIT, TRDIGCHK");
                sSql.AppendLine("FROM TB_ATENCIONCLIENTE WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_ATENCIONCLIENTE.AT_CODEMP = TERCEROS.TRCODEMP AND TB_ATENCIONCLIENTE.TRCODTER = TERCEROS.TRCODTER)");
                sSql.AppendLine("WHERE 1=1");
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

        public static int insertAtencionCliente(SessionManager oSessionManager, int AT_CODIGO, string AT_CODEMP, int TRCODTER, string AT_TIPOINSPECCION, string AT_TIPOPREDIO, string AT_TIPO, string AT_ESTADO, string AT_USUARIO, 
                                                DateTime AT_FECPROGRAMACION, string AT_RESPONSABLE, string AT_CTACONTRATO, string AT_DISISOMETRICO, string AT_PLANOAPROBADO, string AT_CERTLABORAL, string AT_COMPETENCIAS,
                                                string AT_ADMINISTRADOR, string AT_CONTACTO, string AT_IDCONSTRUCTOR, string AT_NOMCONSTRUCTOR, string AT_NEWUSD,
                                                string AT_ALMATRIZ, string AT_CLMATRIZ, string AT_CUINSPECCION, DateTime? AT_FECUINSPECCION,string AT_ZONA,string AT_TIPOGAS)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_ATENCIONCLIENTE (AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_FECPROGRAMACION, AT_RESPONSABLE, AT_CTACONTRATO, ");
                sSql.AppendLine("AT_DISISOMETRICO,AT_PLANOAPROBADO,AT_CERTLABORAL,AT_COMPETENCIAS, AT_ADMINISTRADOR,AT_CONTACTO,AT_IDCONSTRUCTOR,AT_NOMCONSTRUCTOR,AT_NEWUSD, ");
                sSql.AppendLine("AT_ALMATRIZ,AT_CLMATRIZ,AT_CUINSPECCION, AT_FECUINSPECCION,AT_ZONA,AT_ESTADO, AT_USUARIO, AT_TIPOGAS,AT_FECING, AT_FECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, AT_CODIGO, AT_CODEMP, TRCODTER, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_FECPROGRAMACION, AT_RESPONSABLE, 
                    AT_CTACONTRATO, AT_DISISOMETRICO, AT_PLANOAPROBADO, AT_CERTLABORAL, AT_COMPETENCIAS, AT_ADMINISTRADOR, AT_CONTACTO, AT_IDCONSTRUCTOR, AT_NOMCONSTRUCTOR, AT_NEWUSD, 
                    AT_ALMATRIZ, AT_CLMATRIZ, AT_CUINSPECCION, AT_FECUINSPECCION, AT_ZONA, AT_ESTADO, AT_USUARIO, AT_TIPOGAS);
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

        public static int updateAtencionCliente(SessionManager oSessionManager, int AT_CODIGO, string AT_CODEMP, int TRCODTER, string AT_TIPOINSPECCION, string AT_TIPOPREDIO, string AT_TIPO, string AT_ESTADO, string AT_USUARIO,
                                                DateTime AT_FECPROGRAMACION, string AT_RESPONSABLE, string AT_CTACONTRATO, string AT_DISISOMETRICO, string AT_PLANOAPROBADO, string AT_CERTLABORAL, string AT_COMPETENCIAS,
                                                string AT_ADMINISTRADOR, string AT_CONTACTO, string AT_IDCONSTRUCTOR, string AT_NOMCONSTRUCTOR, string AT_NEWUSD,
                                                string AT_ALMATRIZ, string AT_CLMATRIZ, string AT_CUINSPECCION, DateTime? AT_FECUINSPECCION, string AT_ZONA, string AT_TIPOGAS)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE  TB_ATENCIONCLIENTE SET AT_CODEMP=@p1, AT_TIPOINSPECCION=@p2, AT_TIPOPREDIO=@p3, AT_TIPO=@p4, AT_FECPROGRAMACION=@p5, AT_RESPONSABLE=@p6, AT_CTACONTRATO=@p7, ");
                sSql.AppendLine("AT_DISISOMETRICO=@p8,AT_PLANOAPROBADO=@p9,AT_CERTLABORAL=@p10,AT_COMPETENCIAS=@p11, AT_ADMINISTRADOR=@p12,AT_CONTACTO=@p13,AT_IDCONSTRUCTOR=@p14,AT_NOMCONSTRUCTOR=@p15,AT_NEWUSD=@p16, ");
                sSql.AppendLine("AT_ALMATRIZ=@p17,AT_CLMATRIZ=@p18,AT_CUINSPECCION=@p19, AT_FECUINSPECCION=@p20,AT_ZONA=@p21,AT_ESTADO=@p22, AT_USUARIO=@p23, AT_TIPOGAS=@p24, AT_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE AT_CODIGO = @p0");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, AT_CODIGO, AT_CODEMP, AT_TIPOINSPECCION, AT_TIPOPREDIO, AT_TIPO, AT_FECPROGRAMACION, AT_RESPONSABLE,
                    AT_CTACONTRATO, AT_DISISOMETRICO, AT_PLANOAPROBADO, AT_CERTLABORAL, AT_COMPETENCIAS, AT_ADMINISTRADOR, AT_CONTACTO, AT_IDCONSTRUCTOR, AT_NOMCONSTRUCTOR, AT_NEWUSD,
                    AT_ALMATRIZ, AT_CLMATRIZ, AT_CUINSPECCION, AT_FECUINSPECCION, AT_ZONA, AT_ESTADO, AT_USUARIO, AT_TIPOGAS);
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
