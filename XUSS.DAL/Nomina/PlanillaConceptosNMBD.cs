using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Nomina
{
    public class PlanillaConceptosNMBD
    {
        public static DataTable GetPlanillaConceptosHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM NM_PLANTILLAHD WITH(NOLOCK) WHERE 1=1");
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
        public static DataTable GetPlanillaConceptosDT(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT NM_PLANTILLADT.*,TTDESCRI,PC_CODIGO,PC_NOMBRE,TTVALORC,PC_NATURALEZA,CASE WHEN PD_TIPO = 'S' THEN 'Suma' WHEN PD_TIPO = 'R' THEN 'Resta' END TIPOSR, CASE WHEN PD_TIPOPV = 'P' THEN 'Porcentaje' WHEN PD_TIPOPV = 'V' THEN 'Valor' END TIPOPV ");
                sSql.AppendLine("FROM NM_PLANTILLADT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB ='NMCONC' AND TTCODCLA = PD_CONCEPTO AND TTCODEMP = PD_CODEMP)");
                sSql.AppendLine("INNER JOIN TB_PUC WITH(NOLOCK) ON (TB_PUC.PC_ID = NM_PLANTILLADT.PC_ID)");
                sSql.AppendLine("WHERE PH_CODIGO=@p0 ORDER BY PD_CODIGO");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static int InsertPlanillaConceptosDT(SessionManager oSessionManager, int PH_CODIGO, string PD_CODEMP, string PD_TIPO, string PD_TIPOPV, string PD_CONCEPTO, double PD_VALOR, string PD_BASE, string PD_OCONCEPTO, int PC_ID, string PD_USUARIO, string PD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO NM_PLANTILLADT (PH_CODIGO,PD_CODEMP,PD_TIPO,PD_TIPOPV,PD_CONCEPTO,PD_VALOR,PD_BASE,PD_OCONCEPTO,PC_ID,PD_USUARIO,PD_ESTADO,PD_FECING,PD_FECMOD) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, PD_CODEMP, PD_TIPO, PD_TIPOPV, PD_CONCEPTO, PD_VALOR, PD_BASE, PD_OCONCEPTO, PC_ID, PD_USUARIO, PD_ESTADO);
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
        public static int InsertPlanillaConceptosHD(SessionManager oSessionManager, string PH_CODEMP, int PH_CODPLAN, string PH_NOMBRE, string PH_ESTADO,string PH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO NM_PLANTILLAHD (PH_CODEMP,PH_CODPLAN,PH_NOMBRE,PH_ESTADO,PH_USUARIO,PH_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODEMP, PH_CODPLAN, PH_NOMBRE, PH_ESTADO, PH_USUARIO);
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
        public static int UpdatePlanillaImpuestosHD(SessionManager oSessionManager, int PH_CODIGO, string PH_NOMBRE, string PH_ESTADO, string PH_TIPPLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_PLANILLA_IMPHD SET PH_NOMBRE=@p1,PH_ESTADO=@p2,PH_TIPPLA=@p3 WHERE PH_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, PH_NOMBRE, PH_ESTADO, PH_TIPPLA);
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
        public static int UpdatePlanillaConceptosDT(SessionManager oSessionManager, int PD_CODIGO, string PD_CODEMP, string PD_TIPO, string PD_TIPOPV, string PD_CONCEPTO, double PD_VALOR, string PD_BASE, string PD_OCONCEPTO, int PC_ID, string PD_USUARIO, string PD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE NM_PLANTILLADT SET PD_TIPO=@p1, PD_TIPOPV=@p2, PD_CONCEPTO=@p3, PD_VALOR=@p4, PD_BASE=@p5, PD_OCONCEPTO=@p6, PC_ID=@p7, PD_USUARIO=@p8,PD_FECMOD=GETDATE() WHERE PD_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PD_CODIGO, PD_TIPO, PD_TIPOPV, PD_CONCEPTO, PD_VALOR, PD_BASE, PD_OCONCEPTO, PC_ID, PD_USUARIO);
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
        public static int ExistePlanillaConceptosDT(SessionManager oSessionManager, int PH_CODIGO, string PD_CONCEPTO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM NM_PLANTILLADT WITH(NOLOCK) WHERE PH_CODIGO=@p0 AND PD_CONCEPTO=@p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, PD_CONCEPTO));
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
        public static int DeletePlanillaImpuestosDT(SessionManager oSessionManager, int PI_ITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_PLANILLA_IMPDT WHERE PI_ITEM=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PI_ITEM);
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
        public static DataTable GetPlanillasxTercero(SessionManager oSessionManager, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT NM_PLANTILLAHD.*");
                sSql.AppendLine("FROM TB_PLANILLA_NOMINA_TERCEROS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN NM_PLANTILLAHD WITH(NOLOCK) ON(TB_PLANILLA_NOMINA_TERCEROS.PH_CODIGO = NM_PLANTILLAHD.PH_CODPLAN)");
                sSql.AppendLine("WHERE TRCODTER = @p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER);
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
        public static int InsertPlanillaNMTercero(SessionManager oSessionManager, int PH_CODIGO, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_NOMINA_TERCEROS (PH_CODIGO, TRCODTER) VALUES (@p0,@p1) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, TRCODTER);
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

        public static int DeletePlanillaNMTercero(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_PLANILLA_NOMINA_TERCEROS WHERE PH_CODIGO=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
