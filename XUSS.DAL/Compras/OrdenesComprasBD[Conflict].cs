using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Compras
{
    public class OrdenesComprasBD
    {
        public static DataTable GetComprasHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASHD.*, BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO"); 
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
        public static DataTable GetComprasDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASDT.*, '               ' LOT1,'                 ' LOT2,      ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE4,                                 ");
                sql.AppendLine(" (CAST(CD_NROCMP AS VARCHAR)+CD_TIPPRO+CD_CLAVE1+CD_CLAVE2+CD_CLAVE3+CD_CLAVE4) ENLACE, ");
                sql.AppendLine(" ISNULL((SELECT SUM(RD_CANTIDAD)                                                           ");
                sql.AppendLine("    FROM CMP_RECIBODT WITH(NOLOCK)                                                                 ");
                sql.AppendLine("   INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (RD_CODEMP = RH_CODEMP AND RD_NRORECIBO = RH_NRORECIBO) ");
                sql.AppendLine("  WHERE RH_CODEMP = CD_CODEMP       ");
                sql.AppendLine("    AND RH_NROCMP = CD_NROCMP       ");
                sql.AppendLine("    AND RD_TIPPRO = CD_TIPPRO       ");
                sql.AppendLine("    AND RD_CLAVE1 = CD_CLAVE1       ");
                sql.AppendLine("    AND RD_CLAVE2 = CD_CLAVE2       ");
                sql.AppendLine("    AND RD_CLAVE3 = CD_CLAVE3       ");
                sql.AppendLine("    AND RD_CLAVE4 = CD_CLAVE4       ");
                sql.AppendLine(" ),0.0) AS CANRECIBE,0.0 CANRESTANTE, (CD_CANTIDAD*CD_PRECIO) VLRTOT,TANOMBRE,ARNOMBRE                     ");
                sql.AppendLine("  FROM CMP_COMPRASDT WITH(NOLOCK)   ");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = CD_CODEMP AND TATIPPRO = CD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CD_CODEMP AND ARTIPPRO = CD_TIPPRO AND ARCLAVE1 = CD_CLAVE1 AND ARCLAVE2 = CD_CLAVE2 AND ARCLAVE3 = CD_CLAVE3 AND ARCLAVE4 = CD_CLAVE4)");
                sql.AppendLine(" WHERE CD_CODEMP =@p0");
                sql.AppendLine("   AND CD_NROCMP =@p1");

                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CD_CODEMP,CD_NROCMP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
        public static int InsertCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP, string CH_BODEGA, int CH_PROVEEDOR, int CH_TIPORD, DateTime CH_FECORD, string CH_TIPCMP,
                                         string CH_TIPDPH, string CH_TERPAG, string CH_NROMUESTRA, string CH_SERVICIO, double CH_VLRTOT, string CH_OBSERVACIONES, string CH_USUARIO, string CH_ESTADO,
                                        int CH_ORDENOR, DateTime CH_FENTREGA, string CH_GENINV, string CH_CMPINT, string CH_MONEDA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_COMPRASHD (CH_CODEMP,CH_NROCMP,CH_BODEGA,CH_PROVEEDOR,CH_TIPORD,CH_FECORD,CH_TIPCMP,CH_TIPDPH,CH_TERPAG,CH_NROMUESTRA,CH_SERVICIO,");
                sSql.AppendLine("CH_VLRTOT,CH_OBSERVACIONES,CH_USUARIO,CH_ESTADO,CH_ORDENOR,CH_FENTREGA,CH_GENINV,CH_CMPINT,CH_MONEDA,CH_FECING,CH_FECMOD) VALUES ");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_CODEMP, CH_NROCMP, CH_BODEGA, CH_PROVEEDOR, CH_TIPORD, CH_FECORD, CH_TIPCMP, CH_TIPDPH, CH_TERPAG, CH_NROMUESTRA, CH_SERVICIO,
                                                CH_VLRTOT, CH_OBSERVACIONES, CH_USUARIO, CH_ESTADO, CH_ORDENOR, CH_FENTREGA, CH_GENINV, CH_CMPINT, CH_MONEDA);
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
        public static int InsertCompraDT(SessionManager oSessionManager,string CD_CODEMP,int CD_NROCMP,int CD_NROITEM,string CD_BODEGA,string CD_TIPPRO,string CD_CLAVE1,string CD_CLAVE2,
                                     string CD_CLAVE3,string CD_CLAVE4,int CD_PROVEE,string CD_REFPRO,string CD_COLPRO,double CD_CANTIDAD,string CD_UNIDAD,double CD_PRECIO,string CD_OBSERVACIONES,
                                     string CD_USUARIO,string CD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_COMPRASDT (CD_CODEMP,CD_NROCMP,CD_NROITEM,CD_BODEGA,CD_TIPPRO,CD_CLAVE1,CD_CLAVE2,CD_CLAVE3,CD_CLAVE4,CD_PROVEE,CD_REFPRO,CD_COLPRO,CD_CANTIDAD,CD_UNIDAD,CD_PRECIO,");
                sSql.AppendLine("CD_OBSERVACIONES,CD_USUARIO,CD_ESTADO,CD_FECING,CD_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,CD_CODEMP,CD_NROCMP,CD_NROITEM,CD_BODEGA,CD_TIPPRO,CD_CLAVE1,CD_CLAVE2,CD_CLAVE3,CD_CLAVE4,CD_PROVEE,CD_REFPRO,CD_COLPRO,CD_CANTIDAD,CD_UNIDAD,CD_PRECIO,
                                                CD_OBSERVACIONES,CD_USUARIO,CD_ESTADO);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
            sSql =null;
            }
        }

        public static int DeleteCompraDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM CMP_COMPRASDT WHERE CD_CODEMP = @p0 AND CD_NROCMP = @p1");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text, CD_CODEMP, CD_NROCMP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public static DataTable GetReciboHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO, CMP_RECIBOHD.*,CMP_COMPRASHD.*,RH_NRORECIBO ");
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine(" INNER JOIN CMP_RECIBOHD WITH(NOLOCK) ON(RH_CODEMP = CH_CODEMP AND RH_NROCMP = CH_NROCMP) ");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
        public static DataTable GetReciboDT(SessionManager oSessionManager, string CD_CODEMP, int CD_NROCMP, int RD_NRORECIBO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASDT.*,RD_CANTIDAD,TANOMBRE,ARNOMBRE, '               ' LOT1,'                 ' LOT2,");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE2                                  ");
                sql.AppendLine("                  END CLAVE2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE3                                  ");
                sql.AppendLine("                  END CLAVE3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = CD_CODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = CD_TIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = CD_CLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE CD_CLAVE4                                  ");
                sql.AppendLine("                  END CLAVE4                                 ");
                sql.AppendLine("FROM CMP_COMPRASDT WITH(NOLOCK)");
                sql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = CD_CODEMP AND TATIPPRO = CD_TIPPRO) ");
                sql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CD_CODEMP AND ARTIPPRO = CD_TIPPRO AND ARCLAVE1 = CD_CLAVE1 AND ARCLAVE2 = CD_CLAVE2 AND ARCLAVE3 = CD_CLAVE3 AND ARCLAVE4 = CD_CLAVE4)");
                sql.AppendLine("   LEFT OUTER JOIN CMP_RECIBOHD WITH(NOLOCK) ON (CMP_COMPRASDT.CD_NROCMP = CMP_RECIBOHD.RH_NROCMP AND CD_CODEMP = RH_CODEMP)");
                sql.AppendLine("   LEFT OUTER JOIN CMP_RECIBODT WITH(NOLOCK) ON (CMP_RECIBOHD.RH_NRORECIBO = CMP_RECIBODT.RD_NRORECIBO AND RD_ITEM = CMP_COMPRASDT.CD_NROITEM)");
                sql.AppendLine("WHERE CD_CODEMP=@p0");
                sql.AppendLine("  AND CD_NROCMP=@p1");
                sql.AppendLine("  AND RH_NRORECIBO=@p2");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,CD_CODEMP,CD_NROCMP,RD_NRORECIBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
        public static int InsertReciboHD(SessionManager oSessionManager, string RH_CODEMP, int RH_NRORECIBO, int RH_NROCMP, string RH_TIPDOC, string RH_NRODOC, DateTime? RH_FECDOC, string RH_OBSERVACIONES,
                                         string RH_ESTADO, string RH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_RECIBOHD (RH_CODEMP,RH_NRORECIBO,RH_NROCMP,RH_TIPDOC,RH_NRODOC,RH_FECDOC,RH_OBSERVACIONES,RH_ESTADO,RH_USUARIO,RH_FECING,RH_FECMOD)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RH_CODEMP, RH_NRORECIBO, RH_NROCMP, RH_TIPDOC, RH_NRODOC, RH_FECDOC, RH_OBSERVACIONES, RH_ESTADO, RH_USUARIO);
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
        public static int InsertReciboDT(SessionManager oSessionManager, string RD_CODEMP, int RD_NRORECIBO, int RD_ITEM, string RD_TIPPRO, string RD_CLAVE1, string RD_CLAVE2, string RD_CLAVE3, string RD_CLAVE4, string RD_UNIDAD,
                                         double RD_CANTIDAD, int RD_IDMOVI, string RD_USUARIO, string RD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CMP_RECIBODT (RD_CODEMP,RD_NRORECIBO,RD_ITEM,RD_TIPPRO,RD_CLAVE1,RD_CLAVE2,RD_CLAVE3,RD_CLAVE4,RD_UNIDAD,RD_CANTIDAD,RD_IDMOVI,RD_USUARIO,RD_ESTADO,RD_FECING,RD_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RD_CODEMP, RD_NRORECIBO, RD_ITEM, RD_TIPPRO, RD_CLAVE1, RD_CLAVE2, RD_CLAVE3, RD_CLAVE4, RD_UNIDAD, RD_CANTIDAD, RD_IDMOVI, RD_USUARIO, RD_ESTADO);
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
        public static int UpdateEstCompraHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP,string CH_ESTADO,string CH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {               
                sSql.AppendLine("UPDATE CMP_COMPRASHD SET CH_FECMOD = GETDATE(), CH_ESTADO =@p0,CH_USUARIO =@p1");
                sSql.AppendLine(" WHERE CH_CODEMP =@p2 AND CH_NROCMP =@p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CH_ESTADO, CH_USUARIO, CH_CODEMP, CH_NROCMP);
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
        public static IDataReader GetComprasHD(SessionManager oSessionManager, string CH_CODEMP, int CH_NROCMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT CMP_COMPRASHD.*, BDNOMBRE, TRNOMBRE, 0 RECIBO, (SELECT SUM(CD_PRECIO*CD_CANTIDAD) FROM CMP_COMPRASDT WITH(NOLOCK) WHERE CD_CODEMP = CH_CODEMP AND CD_NROCMP = CH_NROCMP) PRECIO,  ");
                sql.AppendLine("       CASE WHEN CH_ESTADO = 'AC' THEN 'Activo' WHEN CH_ESTADO = 'CE' THEN 'Cerrado' WHEN CH_ESTADO = 'AN' THEN 'Anulado' END ESTADO");
                sql.AppendLine("  FROM CMP_COMPRASHD WITH(NOLOCK) ");
                sql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON(CH_CODEMP = BDCODEMP AND CH_BODEGA = BDBODEGA)    ");
                sql.AppendLine(" INNER JOIN TERCEROS WITH(NOLOCK) ON(CH_CODEMP = TRCODEMP AND CH_PROVEEDOR = TRCODTER) ");
                sql.AppendLine("WHERE CH_CODEMP =@p0 AND CH_NROCMP =@p1");
                
                return DBAccess.GetDataReader(oSessionManager, sql.ToString(), CommandType.Text,CH_CODEMP,CH_NROCMP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
    }
}
