using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Contabilidad
{
    public class PlanillaImpuestosBD
    {
        public static DataTable GetPlanillaImpuestosHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,CASE WHEN PH_TIPPLA = 1 THEN 'P. Ventas'  WHEN PH_TIPPLA = 1 THEN 'P. Compras' END T_PLA FROM TB_PLANILLA_IMPHD WITH(NOLOCK) WHERE 1=1");
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
        public static DataTable GetPlanillaImpuestosDT(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PLANILLA_IMPDT.*,TTDESCRI,PC_CODIGO,PC_NOMBRE,TTVALORC,PC_NATURALEZA FROM TB_PLANILLA_IMPDT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB ='IMPF' AND TTCODCLA = IM_IMPUESTO)");
                sSql.AppendLine("INNER JOIN TB_PUC WITH(NOLOCK) ON (TB_PUC.PC_ID = TB_PLANILLA_IMPDT.PC_ID)");
                sSql.AppendLine("WHERE PH_CODIGO=@p0 ORDER BY PI_ITEM");
                
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
        public static int InsertPlanillaImpuestosDT(SessionManager oSessionManager, int PH_CODIGO, string IM_IMPUESTO, double PI_PORCENTAJE, string PI_INDBASE, double PI_BASE, string PI_ESTADO, int PC_ID, string PI_NATURALEZA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_IMPDT (PH_CODIGO,IM_IMPUESTO,PI_PORCENTAJE,PI_INDBASE,PI_BASE,PC_ID,PI_NATURALEZA,PI_ESTADO) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7)");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, IM_IMPUESTO, PI_PORCENTAJE, PI_INDBASE, PI_BASE, PC_ID, PI_NATURALEZA, PI_ESTADO);
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
        public static int InsertPlanillaImpuestosHD(SessionManager oSessionManager, int PH_CODIGO, string PH_NOMBRE, string PH_ESTADO, string PH_TIPPLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_IMPHD (PH_CODIGO,PH_NOMBRE,PH_ESTADO,PH_TIPPLA) VALUES (@p0,@p1,@p2,@p3)");

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
        public static int UpdatePlanillaImpuestosDT(SessionManager oSessionManager, int PH_CODIGO, int PI_ITEM, string IM_IMPUESTO, double PI_PORCENTAJE, string PI_INDBASE, double PI_BASE, string PI_ESTADO,string PI_NATURALEZA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_PLANILLA_IMPDT SET IM_IMPUESTO=@p2,PI_PORCENTAJE=@p3,PI_INDBASE=@p4,PI_BASE=@p5,PI_ESTADO=@p6,PI_NATURALEZA=@p7 WHERE PH_CODIGO=@p0 AND PI_ITEM=@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, PI_ITEM, IM_IMPUESTO, PI_PORCENTAJE, PI_INDBASE, PI_BASE, PI_ESTADO, PI_NATURALEZA);
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
        public static int ExistePlanillaiMpuestosDT(SessionManager oSessionManager, int PH_CODIGO, string IM_IMPUESTO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_PLANILLA_IMPDT WITH(NOLOCK) WHERE PH_CODIGO=@p0 AND IM_IMPUESTO=@p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO,IM_IMPUESTO));
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
        public static DataTable GetImpuestosxTercero(SessionManager oSessionManager, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT TB_PLANILLA_IMPHD.*, CASE WHEN PH_TIPPLA = 1 THEN 'P. Ventas'  WHEN PH_TIPPLA = 1 THEN 'P. Compras' END T_PLA , TTDESCRI,PI_PORCENTAJE,TTCODCLA");
                sSql.AppendLine("FROM TB_PLANILLA_IMP_TERCEROS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PLANILLA_IMPHD WITH(NOLOCK) ON(TB_PLANILLA_IMP_TERCEROS.PH_CODIGO = TB_PLANILLA_IMPHD.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TB_PLANILLA_IMPDT WITH(NOLOCK) ON(TB_PLANILLA_IMPHD.PH_CODIGO = TB_PLANILLA_IMPDT.PH_CODIGO)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON(TTCODTAB = 'IMPF' AND TTCODCLA = IM_IMPUESTO)");
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
    }
}
