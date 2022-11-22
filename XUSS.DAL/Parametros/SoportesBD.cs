using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class SoportesBD
    {
        public static DataTable GetSoportes(SessionManager oSessionManager, int SP_REFERENCIA,string SP_TIPO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SP_CONSECUTIVO ,SP_TIPO,SP_REFERENCIA,SP_DESCRIPCION,SP_EXTENCION,SP_USUARIO,SP_FECING,");
                sSql.AppendLine("'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          ' RUTA");
                sSql.AppendLine("  FROM TB_SOPORTES WITH(NOLOCK) WHERE SP_REFERENCIA =@p0 AND SP_TIPO = @p1");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SP_REFERENCIA,SP_TIPO);
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
        public static DataTable GetSoportes(SessionManager oSessionManager, string SP_TIPO,string SP_TIPPRO,string SP_CLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SP_CONSECUTIVO ,SP_TIPO,SP_REFERENCIA,SP_DESCRIPCION,SP_EXTENCION,SP_USUARIO,SP_FECING,");
                sSql.AppendLine("'                                                                                                                                                                                 ' RUTA");
                sSql.AppendLine("  FROM TB_SOPORTES WITH(NOLOCK) WHERE SP_TIPO = @p0 AND SP_TIPPRO = @p1 AND SP_CLAVE1 =@p2");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SP_TIPO,SP_TIPPRO,SP_CLAVE1);
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
        public static int InsertSoporte(SessionManager oSessionManager, string SP_TIPO, int SP_REFERENCIA, string SP_DESCRIPCION, string SP_EXTENCION, object SP_IMAGEN, string SP_USUARIO,string SP_TIPPRO,string SP_CLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SOPORTES (SP_TIPO,SP_REFERENCIA,SP_DESCRIPCION,SP_EXTENCION,SP_IMAGEN,SP_USUARIO,SP_TIPPRO,SP_CLAVE1,SP_FECING)");
                sSql.AppendLine("  VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SP_TIPO, SP_REFERENCIA, SP_DESCRIPCION, SP_EXTENCION, SP_IMAGEN, SP_USUARIO,SP_TIPPRO,SP_CLAVE1);
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

        public static int ExisteImagen(SessionManager oSessionManager, int SP_CONSECUTIVO, int SP_REFERENCIA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(SP_CONSECUTIVO) FROM TB_SOPORTES WITH(NOLOCK) WHERE SP_CONSECUTIVO=@p0 AND SP_REFERENCIA =@p1");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, SP_CONSECUTIVO, SP_REFERENCIA));

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

        public static int ExisteSoporteArticulo(SessionManager oSessionManager, int SP_CONSECUTIVO, string SP_TIPPRO,string SP_CLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(SP_CONSECUTIVO) FROM TB_SOPORTES WITH(NOLOCK) WHERE SP_CONSECUTIVO=@p0 AND SP_TIPPRO=@p1 AND SP_CLAVE1=@p2");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, SP_CONSECUTIVO, SP_TIPPRO,SP_CLAVE1));

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
        public static DataTable GetSoporte(SessionManager oSessionManager, int SP_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SP_IMAGEN,SP_EXTENCION,SP_DESCRIPCION ");
                sSql.AppendLine("  FROM TB_SOPORTES WITH(NOLOCK) WHERE SP_CONSECUTIVO =@p0 ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SP_CONSECUTIVO);
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
        public static DataTable GetCodSoporte(SessionManager oSessionManager, string SP_DESCRIPCION,string inTipo)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SP_CONSECUTIVO FROM TB_SOPORTES WHERE SP_DESCRIPCION LIKE '%"+inTipo+"%"+SP_DESCRIPCION+"%' ");
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
        public static int DeleteSoporte(SessionManager oSessionManager, int SP_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_SOPORTES WHERE SP_CONSECUTIVO =@p0 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SP_CONSECUTIVO);
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
