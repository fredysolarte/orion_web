using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Correspondencia
{
    public class PlanillaDesmontesBD
    {
        public static DataTable GetPlanillaDesmonteHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_PLANILLA_DESMONTE_HD WITH(NOLOCK) WHERE 1=1");
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

        public static DataTable GetPlanillaDesmonteDT(SessionManager oSessionManager, int PDH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PLANILLA_DESMONTE_DT.* ,TRNOMBRE,PH_EDIFICIO,PH_ESCALERA,PH_PISO,PH_PORTAL,MECDELEM,ARNOMBRE");
                sSql.AppendLine("FROM TB_PLANILLA_DESMONTE_DT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PROPIEDAHORIZONTAL WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_PLANILLA_DESMONTE_DT.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_INSTALACION.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.TRCODTER = TERCEROS.TRCODTER)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON (MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                                AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(MECODEMP = ARCODEMP AND METIPPRO = ARTIPPRO AND MECLAVE1 = ARCLAVE1 AND MECLAVE2 = ARCLAVE2 AND MECLAVE3 = ARCLAVE3 AND MECLAVE4 = ARCLAVE4) ");
                sSql.AppendLine(" WHERE PDH_CODIGO=@p0");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PDH_CODIGO);
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
        public static DataTable GetCuentasRestantes(SessionManager oSessionManager, string inFiltro)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PROPIEDAHORIZONTAL.PH_CODIGO,TRNOMBRE,PH_EDIFICIO,PH_ESCALERA,PH_PISO,PH_PORTAL,MECDELEM,ARNOMBRE,'N' CHK");
                sSql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");                
                sSql.AppendLine("INNER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_INSTALACION.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.TRCODTER = TERCEROS.TRCODTER)");
                sSql.AppendLine("INNER JOIN MOVIMELE WITH(NOLOCK) ON (MOVIMELE.MECODEMP = TB_INSTALACION.IT_CODEMP AND MOVIMELE.MEIDMOVI = TB_INSTALACION.MEIDMOVI AND MOVIMELE.MEIDITEM = TB_INSTALACION.MEIDITEM");
                sSql.AppendLine("                                AND MOVIMELE.MEIDLOTE = TB_INSTALACION.MEIDLOTE AND MOVIMELE.MEIDELEM = TB_INSTALACION.MEIDELEM)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(MECODEMP = ARCODEMP AND METIPPRO = ARTIPPRO AND MECLAVE1 = ARCLAVE1 AND MECLAVE2 = ARCLAVE2 AND MECLAVE3 = ARCLAVE3 AND MECLAVE4 = ARCLAVE4) ");
                sSql.AppendLine("WHERE 1=1 "+inFiltro);


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
        public static int InsertPlanillaHD(SessionManager oSessionManager, int PDH_CODIGO,DateTime PDH_FECHA,string PDH_DESCRIPCION,string PDH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_DESMONTE_HD (PDH_CODIGO,PDH_FECHA,PDH_DESCRIPCION,PDH_USUARIO,PDH_FECING) VALUES (@p0,@p1,@p2,@p3,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PDH_CODIGO, PDH_FECHA, PDH_DESCRIPCION, PDH_USUARIO);
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
        public static int InsertPlanillaDT(SessionManager oSessionManager, int PDH_CODIGO, int PH_CODIGO, int? PDD_TUERCAPLANA, int? PDD_COPASCONICAS, int? PDD_RACORFLARES, int? PDD_VALVULAALIVIO, int? PDD_VALVULAAGUA,
                    int? PDD_CHEQUE, int? PDD_CODOGALVANIZADO, int? PDD_CODOCALLE, int? PDD_MGFLEXOMETALICA, int? PDD_COBREMT, string PDD_TECNICO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_DESMONTE_DT (PDH_CODIGO,PH_CODIGO,PDD_TUERCAPLANA,PDD_COPASCONICAS,PDD_RACORFLARES,PDD_VALVULAALIVIO,PDD_VALVULAAGUA,");
                sSql.AppendLine("PDD_CHEQUE,PDD_CODOGALVANIZADO,PDD_CODOCALLE,PDD_MGFLEXOMETALICA,PDD_COBREMT,PDD_TECNICO) ");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PDH_CODIGO, PH_CODIGO, PDD_TUERCAPLANA, PDD_COPASCONICAS, PDD_RACORFLARES, PDD_VALVULAALIVIO, PDD_VALVULAAGUA,
                    PDD_CHEQUE, PDD_CODOGALVANIZADO, PDD_CODOCALLE, PDD_MGFLEXOMETALICA, PDD_COBREMT, PDD_TECNICO);
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
