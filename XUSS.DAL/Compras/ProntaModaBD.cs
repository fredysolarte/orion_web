using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Compras
{
    public class ProntaModaBD
    {
        public DataTable GetProntaModa(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_INGCOSTO.*, (TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOM_TER,TRCODNIT ");
                sSql.AppendLine("FROM TB_INGCOSTO WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (ICCODEMP = TRCODEMP AND ICPROVEE = TRCODTER)");
                sSql.AppendLine("WHERE 1=1 ");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
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
        public int InsertProntaModa(SessionManager oSessionManager,int ICCONSE,string ICCODEMP,string ICTIPPRO,string ICCLAVE1,int ICPROVEE,string ICREFERENCIA,int ICCANTIDAD,string ICMONEDA,double ICCOSTO,DateTime ICFECHA,string ICOBSERVACION,string ICCDUSER,string ICESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_INGCOSTO (ICCONSE,ICCODEMP,ICTIPPRO,ICCLAVE1,ICPROVEE,ICREFERENCIA,ICCANTIDAD,ICMONEDA,ICCOSTO,ICFECHA,ICOBSERVACION,ICCDUSER,ICESTADO,ICFECING,ICFECMOD) ");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ICCONSE, ICCODEMP, ICTIPPRO, ICCLAVE1, ICPROVEE, ICREFERENCIA, ICCANTIDAD, ICMONEDA, ICCOSTO, ICFECHA, ICOBSERVACION, ICCDUSER, ICESTADO);
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
        public DataTable GetProntaModaDT(SessionManager oSessionManager, int ICCONSE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_INGCOSTODT.*,TANOMBRE,ARNOMBRE,ARPRECIO");
                sSql.AppendLine("  FROM TB_INGCOSTODT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = CC_CODEMP AND TB_INGCOSTODT.ARTIPPRO = TATIPPRO)");
                sSql.AppendLine("INNER JOIN ARTICULO ON (ARCODEMP = CC_CODEMP AND TB_INGCOSTODT.ARTIPPRO = ARTICULO.ARTIPPRO AND TB_INGCOSTODT.ARCLAVE1 = ARTICULO.ARCLAVE1 AND TB_INGCOSTODT.ARCLAVE2 = ARTICULO.ARCLAVE2 AND TB_INGCOSTODT.ARCLAVE3 = ARTICULO.ARCLAVE3 AND TB_INGCOSTODT.ARCLAVE4 = ARTICULO.ARCLAVE4)");
                sSql.AppendLine(" WHERE ICCONSE =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,ICCONSE);
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
        public DataTable GetProntaModaInv(SessionManager oSessionManager, int ICCONSE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_INGCOSTODT.*,TANOMBRE,");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = IC_CODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = IC_CODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("  FROM TB_INGCOSTODT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = IC_CODEMP AND TB_INGCOSTODT.ARTIPPRO = TATIPPRO)");                
                sSql.AppendLine(" WHERE ICCONSE =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ICCONSE);
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
