using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Terceros
{
    public class InventarioEquiposBD
    {
        public static DataTable GetEquipos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_EQUIPOINV WHERE CODEMP = '001' ");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

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
        public static DataTable GetEquipos(SessionManager oSessionManager, int CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_EQUIPOINV WHERE CODEMP = '001' AND CODIGO =@p0");
                

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,CODIGO);
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
        public static DataTable GetTbTablas(SessionManager oSessionManager, string TTCODTAB)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = '001' AND TTCODTAB = @p0");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,TTCODTAB);
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
        public static DataTable GetHadware(SessionManager oSessionManager, int Codigo)
        {
            StringBuilder sSql = new StringBuilder();
            try
            { 
                sSql.AppendLine("SELECT a.*, b.TTVALORC MARCA, c.TTVALORC Tipo, TRNOMBRE");
                sSql.AppendLine("FROM TB_HADWARE a WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTABLAS b WITH(NOLOCK) ON(a.CODEMP = b.TTCODEMP AND a.MARCA = b.TTCODCLA AND b.TTCODTAB = 'MARCAH')");
                sSql.AppendLine("INNER JOIN TBTABLAS c WITH(NOLOCK) ON(a.CODEMP = c.TTCODEMP AND a.TIPO = c.TTCODCLA AND c.TTCODTAB = 'TIPOH')");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON(TRCODEMP = a.CODEMP AND a.PROVEEDOR = TRCODTER)");
                sSql.AppendLine("WHERE CODEMP = '001' AND CODIGO =@p0");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,Codigo);
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
        public static DataTable GetSoftware(SessionManager oSessionManager, int Codigo)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT a.*");
                sSql.AppendLine("FROM TB_SOFTWARE a WITH(NOLOCK) ");                
                sSql.AppendLine("WHERE CODEMP = '001' AND CODIGO =@p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, Codigo);
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
        public static string GetNombreTbTablas(SessionManager oSessionManager, string TTCODTAB, string TTCODCLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTVALORC FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = '001' AND TTCODTAB = @p0 AND TTCODCLA =@p1");
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TTCODTAB, TTCODCLA));
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
        public static int InsertEquipo(SessionManager oSessionManager, int CODIGO, string T_EQUIPO, string UBICACION, string IP1, string IP2,
                                       string IP3, string IP4, string IP5, string IP6, string USUARIO, string REFERENCIA, string NOMBRE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_EQUIPOINV (CODEMP, CODIGO, T_EQUIPO, UBICACION, IP1, IP2, IP3, IP4, IP5, IP6, USUARIO, FECING, REFERENCIA,NOMBRE)");
                sSql.AppendLine(" VALUES('001',@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE(),@p10,@p11) ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODIGO, T_EQUIPO, UBICACION, IP1, IP2, IP3, IP4, IP5, IP6, 
                                                USUARIO, REFERENCIA,NOMBRE);
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
        public static int UpdateEquipo(SessionManager oSessionManager, int CODIGO, string T_EQUIPO, string UBICACION, string IP1, string IP2,
                                       string IP3, string IP4, string IP5, string IP6, string USUARIO, string REFERENCIA, string NOMBRE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_EQUIPOINV SET T_EQUIPO=@p0, UBICACION=@p1, IP1=@p2, IP2=@p3, IP3=@p4, IP4=@p5, IP5=@p6, IP6=@p7, USUARIO=@p8, ");
                sSql.AppendLine(" FECING=GETDATE(), REFERENCIA=@p9,NOMBRE=@p10 WHERE CODIGO =@p11");
                
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, T_EQUIPO, UBICACION, IP1, IP2, IP3, IP4, IP5, IP6,
                                                USUARIO, REFERENCIA, NOMBRE, CODIGO);
                 return CODIGO;
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
        public static int InsertHadware(SessionManager oSessionManager, int CODIGO, int CODINT, string MARCA, string TIPO, string DESCRIPCION,
                                                                       string ESTADO, string CAUSAE, DateTime? FECCOMPRA, string PROVEEDOR)
        {
            StringBuilder sSql = new StringBuilder();
            try 
            {
                sSql.AppendLine("INSERT INTO TB_HADWARE (CODEMP, CODIGO, CODINT, MARCA, TIPO, DESCRIPCION, ESTADO, CAUSAE, FECING, FECMOD, FECCOMPRA, PROVEEDOR)");
                sSql.AppendLine("VALUES('001',@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE(),GETDATE(),@p7,@p8)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODIGO, CODINT, MARCA, TIPO, DESCRIPCION, ESTADO, 
                                                                                                    CAUSAE, FECCOMPRA, PROVEEDOR);
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
        public static int InsertSoftware(SessionManager oSessionManager,int CODIGO,int CODINT,string NOMBRE,string LICENCIA,DateTime? FECVEN, string DESCRIPCION)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SOFTWARE (CODEMP,CODIGO,CODINT,NOMBRE,LICENCIA,FECVEN,DESCRIPCION,ESTADO,CAUSAE,FECING,FECMOD)");
                sSql.AppendLine("VALUES('001',@p0,@p1,@p2,@p3,@p4,@p5,'AC','.',GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODIGO, CODINT, NOMBRE, LICENCIA, FECVEN, DESCRIPCION);
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
        public static int DeleteHadware(SessionManager oSessionManager, int CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_HADWARE WHERE CODIGO = @p0");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODIGO);
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
        public static int DeleteSoftware(SessionManager oSessionManager, int CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_SOFTWARE WHERE CODIGO = @p0");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODIGO);
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
