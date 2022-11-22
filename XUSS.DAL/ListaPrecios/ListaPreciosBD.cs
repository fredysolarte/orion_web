using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.ListaPrecios
{
    public class ListaPreciosBD
    {
        public static DataTable GetListaPrecioHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT *");
                sql.AppendLine("FROM TB_LSTPRECIO WITH(NOLOCK)");                
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
        public static DataTable GetListaPrecioDT(SessionManager oSessionManager, string P_RCODEMP,string P_RLISPRE)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_LSTPRECIODT.*,ARNOMBRE,TANOMBRE, ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = P_RTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = P_RCLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE2                                  ");
                sql.AppendLine("                  END C2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = P_RTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = P_RCLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE3                                  ");
                sql.AppendLine("                  END C3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = P_RTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = P_RCLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE3                                  ");
                sql.AppendLine("                  END C4                                 ");
                sql.AppendLine("FROM TB_LSTPRECIODT WITH(NOLOCK)");
                sql.AppendLine("LEFT OUTER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = P_RCODEMP AND ARTIPPRO = P_RTIPPRO AND ARCLAVE1 = P_RCLAVE1)");

                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (P_RCODEMP = TACODEMP AND P_RTIPPRO = TATIPPRO)");
                sql.AppendLine("WHERE P_RCODEMP = @p0 AND P_RLISPRE = @p1");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,P_RCODEMP,P_RLISPRE);
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
        public static DataTable GetListaPrecioDTF(SessionManager oSessionManager, string P_RCODEMP, string P_RLISPRE,string filter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_LSTPRECIODT.*,ARNOMBRE,TANOMBRE, ");
                sql.AppendLine(" CASE WHEN TACTLSE2 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = ARTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = ARCLAVE2       ");
                sql.AppendLine("                                     AND ASNIVELC = 2 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE2                                  ");
                sql.AppendLine("                  END C2,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE3 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = ARTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = ARCLAVE3       ");
                sql.AppendLine("                                     AND ASNIVELC = 3 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE3                                  ");
                sql.AppendLine("                  END C3,                                           ");
                sql.AppendLine(" CASE WHEN TACTLSE4 = 'S' THEN (SELECT ASNOMBRE                   ");
                sql.AppendLine("                                    FROM ARTICSEC WITH(NOLOCK)      ");
                sql.AppendLine("                                   WHERE ASCODEMP = P_RCODEMP       ");
                sql.AppendLine("                                     AND ASTIPPRO = ARTIPPRO       ");
                sql.AppendLine("                                     AND ASCLAVEO = ARCLAVE4       ");
                sql.AppendLine("                                     AND ASNIVELC = 4 )             ");
                sql.AppendLine("                    ELSE P_RCLAVE3                                  ");
                sql.AppendLine("                  END C4                                 ");
                sql.AppendLine("FROM TB_LSTPRECIODT WITH(NOLOCK)");
                sql.AppendLine("LEFT OUTER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = P_RCODEMP AND ARTIPPRO = P_RTIPPRO AND ARCLAVE1 = P_RCLAVE1)");

                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (P_RCODEMP = TACODEMP AND P_RTIPPRO = TATIPPRO)");
                sql.AppendLine("WHERE P_RCODEMP = @p0 AND P_RLISPRE = @p1 "+filter);

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, P_RCODEMP, P_RLISPRE);
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
        public static int InsertListaPrecioDT(SessionManager oSessionManager, string P_RCODEMP, string P_RLISPRE, int? P_RCODTER, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2,
                                              string P_RCLAVE3, string P_RCLAVE4, string P_RCODCLA, string P_RCODCAL, string P_RUNDPRE, double P_RPRECIO, double P_RCANMIN, string P_RESTADO,
                                              string P_RCAUSAE, string P_RNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try { 
                sSql.AppendLine("INSERT INTO TB_LSTPRECIODT (P_RCODEMP,P_RLISPRE,P_RCODTER,P_RTIPPRO,P_RCLAVE1,P_RCLAVE2,P_RCLAVE3,P_RCLAVE4,P_RCODCLA,");
                sSql.AppendLine("P_RCODCAL,P_RUNDPRE,P_RPRECIO,P_RCANMIN,P_RESTADO,P_RCAUSAE,P_RNMUSER,P_RFECING,P_RFECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,P_RCODEMP, P_RLISPRE, P_RCODTER, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2,
                                              P_RCLAVE3, P_RCLAVE4, P_RCODCLA, P_RCODCAL, P_RUNDPRE, P_RPRECIO, P_RCANMIN, P_RESTADO,
                                              P_RCAUSAE, P_RNMUSER);
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
        public static int ExisteListaPrecioDT(SessionManager oSessionManager, string P_RCODEMP, string P_RLISPRE, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2, string P_RCLAVE3, string P_RCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_LSTPRECIODT WITH(NOLOCK) ");               
                sSql.AppendLine("WHERE P_RCODEMP=@p0 AND P_RLISPRE=@p1 AND P_RTIPPRO = @p2 AND P_RCLAVE1 = @p3 AND P_RCLAVE2 = @p4 AND P_RCLAVE3 = @p5 AND P_RCLAVE4 = @p6");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, P_RCODEMP, P_RLISPRE, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2, P_RCLAVE3, P_RCLAVE4));
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
        public static int UpdateListaPrecioDT(SessionManager oSessionManager, string P_RCODEMP, string P_RLISPRE, int? P_RCODTER, string P_RTIPPRO, string P_RCLAVE1, string P_RCLAVE2,
                                              string P_RCLAVE3, string P_RCLAVE4, string P_RCODCLA, string P_RCODCAL, string P_RUNDPRE, double P_RPRECIO, double P_RCANMIN, string P_RESTADO,
                                              string P_RCAUSAE, string P_RNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_LSTPRECIODT SET P_RCODCLA=@p7,");
                sSql.AppendLine("P_RCODCAL=@p8,P_RUNDPRE=@p9,P_RPRECIO=@p10,P_RCANMIN=@p11,P_RESTADO=@p12,P_RCAUSAE=@p13,P_RNMUSER=@p14,P_RFECING=GETDATE(),P_RFECMOD=GETDATE()");
                sSql.AppendLine("WHERE P_RCODEMP=@p0 AND P_RLISPRE=@p1 AND P_RTIPPRO = @p2 AND P_RCLAVE1 = @p3 AND P_RCLAVE2 = @p4 AND P_RCLAVE3 = @p5 AND P_RCLAVE4 = @p6");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, P_RCODEMP, P_RLISPRE, P_RTIPPRO, P_RCLAVE1, P_RCLAVE2,
                                              P_RCLAVE3, P_RCLAVE4, P_RCODCLA, P_RCODCAL, P_RUNDPRE, P_RPRECIO, P_RCANMIN, P_RESTADO,
                                              P_RCAUSAE, P_RNMUSER);
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
        public static int UpdateListaPrecioHD(SessionManager oSessionManager, string P_CCODEMP, string P_CLISPRE, DateTime P_CFECPRE, string P_CNOMBRE, string P_CDESCRI, string P_CMONEDA, string P_CCLIPRO, string P_CTIPLIS,
                                       double P_CREDOND, string P_CESTADO, string P_CCAUSAE, string P_CNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_LSTPRECIO SET P_CFECPRE=@p2,P_CNOMBRE=@p3,P_CDESCRI=@p4,P_CMONEDA=@p5,P_CCLIPRO=@p6,P_CTIPLIS=@p7,");
                sSql.AppendLine("                        P_CREDOND=@p8,P_CESTADO=@p9,P_CCAUSAE=@p10,P_CNMUSER=@p11,P_CFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE P_CCODEMP=@p0 AND P_CLISPRE=@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, P_CCODEMP, P_CLISPRE, P_CFECPRE, P_CNOMBRE, P_CDESCRI, P_CMONEDA, P_CCLIPRO, P_CTIPLIS,
                                       P_CREDOND, P_CESTADO, P_CCAUSAE, P_CNMUSER);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                
            }
        }
        public static int InsertListaPrecioHD(SessionManager oSessionManager, string P_CCODEMP, string P_CLISPRE, DateTime P_CFECPRE, string P_CNOMBRE, string P_CDESCRI, string P_CMONEDA, string P_CCLIPRO, string P_CTIPLIS,
                                       double P_CREDOND, string P_CESTADO, string P_CCAUSAE, string P_CNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_LSTPRECIO (P_CCODEMP,P_CLISPRE,P_CFECPRE,P_CNOMBRE,P_CDESCRI,P_CMONEDA,P_CCLIPRO,P_CTIPLIS,P_CREDOND,P_CESTADO,P_CCAUSAE,P_CNMUSER,P_CFECMOD,P_CFECING)");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,GETDATE(),GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, P_CCODEMP, P_CLISPRE, P_CFECPRE, P_CNOMBRE, P_CDESCRI, P_CMONEDA, P_CCLIPRO, P_CTIPLIS,
                                       P_CREDOND, P_CESTADO, P_CCAUSAE, P_CNMUSER);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public static DataTable GetListaPrecioDT(SessionManager oSessionManager, string P_RCODEMP, string P_RTIPPRO, string P_RCLAVE1)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_LSTPRECIODT.*,P_CNOMBRE, ");
                sql.AppendLine("ISNULL(CASE");
                sql.AppendLine("WHEN TACTLSE2 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = P_RCODEMP");
                sql.AppendLine("             AND ASTIPPRO = P_RTIPPRO AND ASCLAVEO = P_RCLAVE2");
                sql.AppendLine("             AND ASNIVELC = 2) ELSE P_RCLAVE2 END, 'Todos') CLAVE2,");
                sql.AppendLine("ISNULL(CASE");
                sql.AppendLine("WHEN TACTLSE3 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = P_RCODEMP");
                sql.AppendLine("             AND ASTIPPRO = P_RTIPPRO AND ASCLAVEO = P_RCLAVE3");
                sql.AppendLine("             AND ASNIVELC = 3) ELSE P_RCLAVE3 END, 'Todos') CLAVE3");
                sql.AppendLine("FROM TB_LSTPRECIODT WITH(NOLOCK)");
                sql.AppendLine(" INNER JOIN TB_LSTPRECIO WITH(NOLOCK) ON(P_CLISPRE = P_RLISPRE)");
                sql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (P_RCODEMP = TACODEMP AND P_RTIPPRO = TATIPPRO)");
                sql.AppendLine("WHERE P_RCODEMP = @p0 AND P_RTIPPRO =@p1 AND P_RCLAVE1 =@p2");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, P_RCODEMP,P_RTIPPRO,P_RCLAVE1);
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

        public static double GetPrecio(SessionManager oSessionManager, string P_RCODEMP, string P_RTIPPRO, string P_RCLAVE1,string P_RCLAVE2,string P_RCLAVE3,string P_RCLAVE4,string P_RLISPRE)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT ISNULL(dbo.FGET_PRECIO(@p0,@p1,@p2,@p3,@p4,@p5,'.',@p6),0) PRECIO");
                                              
                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text, P_RCODEMP, P_RTIPPRO, P_RCLAVE1,P_RCLAVE2,P_RCLAVE3,P_RCLAVE4,P_RLISPRE));
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
