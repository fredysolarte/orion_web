using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Parametros
{
    public class ClavesAlternasBD
    {
        public static DataTable GetClavesAlternas(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ARTICSEC.*,TANOMBRE, ");
                sSql.AppendLine("CASE WHEN ASNIVELC = '5' THEN TACDCLA1 WHEN ASNIVELC='6' THEN TACDCLA2 WHEN ASNIVELC='7' THEN TACDCLA3 WHEN ASNIVELC='8' THEN TACDCLA4 WHEN ASNIVELC='9' THEN TACDCLA5 WHEN ASNIVELC='10' THEN TACDCLA7 END NOM_NIVEL");
                sSql.AppendLine("FROM ARTICSEC WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (ASCODEMP = TACODEMP AND ASTIPPRO = TATIPPRO)");
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
        public static int InsertClaveAlterna(SessionManager oSessionManager,string ASCODEMP,string ASTIPPRO,string ASNIVELC,string ASCLAVEO,string ASNOMBRE,string ASDESCRI,string ASESTADO,string ASCAUSAE,string ASNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO ARTICSEC (ASCODEMP,ASTIPPRO,ASNIVELC,ASCLAVEO,ASNOMBRE,ASDESCRI,ASESTADO,ASCAUSAE,ASNMUSER,ASFECING,ASFECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ASCODEMP, ASTIPPRO, ASNIVELC, ASCLAVEO, ASNOMBRE, ASDESCRI, ASESTADO, ASCAUSAE, ASNMUSER);
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
        public static int UpdateClaveAlterna(SessionManager oSessionManager, string ASCODEMP, string ASTIPPRO, string ASNIVELC, string ASCLAVEO, string ASNOMBRE, string ASDESCRI, string ASESTADO, string ASCAUSAE, string ASNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE ARTICSEC SET ASNOMBRE=@p4,ASDESCRI=@p5,ASESTADO=@p6,ASCAUSAE=@p7,ASNMUSER=@p8,ASFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE ASCODEMP = @p0 AND ASTIPPRO = @p1 AND ASNIVELC = @p2 AND ASCLAVEO = @p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ASCODEMP, ASTIPPRO, ASNIVELC, ASCLAVEO, ASNOMBRE, ASDESCRI, ASESTADO, ASCAUSAE, ASNMUSER);
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
        public static DataTable GetClavesAlternas(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, int ASNIVELC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ASCLAVEO,ASNOMBRE              ");
                sSql.AppendLine("  FROM ARTICSEC WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE ASCODEMP = @p0");
                sSql.AppendLine("   AND ASTIPPRO = @p1");                
                sSql.AppendLine("   AND ASNIVELC = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ASNIVELC);
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
