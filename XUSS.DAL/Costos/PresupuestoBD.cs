using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Costos
{
    public class PresupuestoBD
    {
        //Presupuesto Almacen
        #region
        public static int ExistePresupuesto(SessionManager oSessionManager, int inmes, int inano, string inbodega)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM PR_PRESUPUESTOXDIAXMESXANO WITH(NOLOCK) ");
                sSql.AppendLine("WHERE PR_MES = @p0 ");
                sSql.AppendLine("  AND PR_ANO = @p1 ");
                sSql.AppendLine("  AND PR_BODEGA = @p2 ");
                
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inmes,inano,inbodega));
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
        public static int ExistePresupuesto(SessionManager oSessionManager, int inmes, int inano, int india, string inbodega)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM PR_PRESUPUESTOXDIAXMESXANO WITH(NOLOCK) ");
                sSql.AppendLine("WHERE PR_MES = @p0 ");
                sSql.AppendLine("  AND PR_ANO = @p1 ");
                sSql.AppendLine("  AND PR_DIA = @p2 ");
                sSql.AppendLine("  AND PR_BODEGA = @p3 ");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano, india, inbodega));
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
        public static DataTable GetPresupuesto(SessionManager oSessionManager, int inmes, int inano, string inbodega)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT PR_ANO ano,PR_MES mes,PR_DIA dia,PR_BODEGA bodega,ISNULL(CAST(PR_VALOR AS INT),0) valor FROM PR_PRESUPUESTOXDIAXMESXANO WITH(NOLOCK) ");
                sSql.AppendLine("SELECT PR_ANO ano,PR_MES mes,PR_DIA dia,PR_BODEGA bodega,ISNULL(PR_VALOR,0) valor FROM PR_PRESUPUESTOXDIAXMESXANO WITH(NOLOCK) ");
                sSql.AppendLine("WHERE PR_MES = @p0 ");
                sSql.AppendLine("  AND PR_ANO = @p1 ");
                sSql.AppendLine("  AND PR_BODEGA = @p2 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano, inbodega);
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
        public static int InsertPresupuesto(SessionManager oSessionManager, string PR_CODEMP, int PR_ANO, int PR_MES, int PR_DIA, string PR_BODEGA, double PR_VALOR, string PR_USUARIO,
                                    string PR_ESTADO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO PR_PRESUPUESTOXDIAXMESXANO VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sql.ToString(),CommandType.Text, PR_CODEMP,PR_ANO,PR_MES,PR_DIA,PR_BODEGA,PR_VALOR,PR_USUARIO,PR_ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
 
            }
        }
        public static int UpdatePresupuesto(SessionManager oSessionManager, int inmes, int inano, int india, string inbodega, double valor,string usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PR_PRESUPUESTOXDIAXMESXANO  ");
                sSql.AppendLine(" SET PR_VALOR=@p4,PR_FECMOD=GETDATE(),PR_USUARIO=@p5");
                sSql.AppendLine("WHERE PR_MES = @p0 ");
                sSql.AppendLine("  AND PR_ANO = @p1 ");
                sSql.AppendLine("  AND PR_DIA = @p2 ");
                sSql.AppendLine("  AND PR_BODEGA = @p3 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano, india, inbodega,valor,usuario);
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
        //Presuouesto Vendedor
        #region 
        public static int ExistePresupuestoVendedor(SessionManager oSessionManager, int inmes, int inano, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_PPTOVENDEDOR WITH(NOLOCK) ");
                sSql.AppendLine("WHERE PP_MES = @p0 ");
                sSql.AppendLine("  AND PP_ANO = @p1 ");
                sSql.AppendLine("  AND TRCODTER = @p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano, TRCODTER));
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
        public static int ExistePresupuestoVendedor(SessionManager oSessionManager, int inmes, int inano)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_PPTOVENDEDOR WITH(NOLOCK) ");
                sSql.AppendLine("WHERE PP_MES = @p0 ");
                sSql.AppendLine("  AND PP_ANO = @p1 ");                

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano));
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
        public static DataTable GetPresupuestoVendedor(SessionManager oSessionManager, int inmes, int inano)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT PR_ANO ano,PR_MES mes,PR_DIA dia,PR_BODEGA bodega,ISNULL(CAST(PR_VALOR AS INT),0) valor FROM PR_PRESUPUESTOXDIAXMESXANO WITH(NOLOCK) ");
                sSql.AppendLine("SELECT PP_ANO ano,PP_MES mes,ISNULL(PP_VENTAS,0) Ventas,ISNULL(PP_CARTERA,0) Cartera,TB_PPTOVENDEDOR.TRCODTER cod_vendedor,(TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) VENDEDOR ");
                sSql.AppendLine("FROM TB_PPTOVENDEDOR WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = PP_CODEMP AND TERCEROS.TRCODTER = TB_PPTOVENDEDOR.TRCODTER)");
                sSql.AppendLine("WHERE PP_MES = @p0 ");
                sSql.AppendLine("  AND PP_ANO = @p1 ");                

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inmes, inano);
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
        public static int InsertPresupuestoVendedor(SessionManager oSessionManager, string PP_CODEMP, int PP_ANO, int PP_MES, int TRCODTER, double PP_VENTAS, double PP_CARTERA, string PR_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_PPTOVENDEDOR (PP_CODEMP, PP_ANO, PP_MES, TRCODTER, PP_VENTAS, PP_CARTERA, PP_USUARIO, PP_FECMOD,PP_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, PP_CODEMP, PP_ANO, PP_MES, TRCODTER, PP_VENTAS, PP_CARTERA, PR_USUARIO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public static int UpdatePresupuestoVendedor(SessionManager oSessionManager, int TRCODTER, int PP_MES, int PP_ANO, double PP_VENTAS, double PP_CARTERA, string usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_PPTOVENDEDOR  ");
                sSql.AppendLine(" SET PP_VENTAS=@p3,PP_CARTERA=@p4,PP_FECMOD=GETDATE(),PP_USUARIO=@p5");
                sSql.AppendLine("WHERE TRCODTER = @p0 ");
                sSql.AppendLine("  AND PP_ANO = @p1 ");
                sSql.AppendLine("  AND PP_MES = @p2 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER, PP_ANO, PP_MES, PP_VENTAS, PP_CARTERA, usuario);
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
