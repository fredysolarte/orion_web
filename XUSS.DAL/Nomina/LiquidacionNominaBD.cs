using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Nomina
{
    public class LiquidacionNominaBD
    {
        public static DataTable GetPlanillaNominaHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM NM_LIQUIDACIONHD WITH(NOLOCK) WHERE 1=1 ");
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
        public static DataTable GetNovedades(SessionManager oSessionManager,int NMP_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT NM_NOVEDADES.*,TTDESCRI,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'') + ' ' + ISNULL(TRAPELLI,'')) EMPLEADO,TRCODNIT ,CASE WHEN NV_TIPOSR = 'S' THEN 'Suma' WHEN NV_TIPOSR = 'R' THEN 'Resta' END D_TIPOSR, CASE WHEN NV_TIPOPV = 'P' THEN 'Porcentaje' WHEN NV_TIPOPV = 'V' THEN 'Valor' END D_TIPOPV ");
                sSql.AppendLine("FROM NM_NOVEDADES WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TERCEROS.TRCODTER = NM_NOVEDADES.TRCODTER)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB ='NMCONC' AND TTCODCLA = NV_CONCEPTO AND TTCODEMP = '001')");
                sSql.AppendLine("WHERE NMP_CODIGO=@p0 ");                

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, NMP_CODIGO);
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
        public static DataTable GetPlanillaNominaDT(SessionManager oSessionManager, int NMH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT NM_LIQUIDACIONDT.*,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'') + ' ' + ISNULL(TRAPELLI,'')) EMPLEADO,TRCODNIT,TTDESCRI CONCEPTO,CASE WHEN NMD_ORIGEN=1 THEN 'Salario' WHEN NMD_ORIGEN=2 THEN 'Planilla' WHEN NMD_ORIGEN=3 THEN 'Novedad' END ORIGEN  ");
                sSql.AppendLine("FROM NM_LIQUIDACIONDT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TERCEROS.TRCODTER = NM_LIQUIDACIONDT.TRCODTER)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB ='NMCONC' AND TTCODCLA = PD_CODIGO AND TTCODEMP = '001')");
                sSql.AppendLine("WHERE NMH_CODIGO=@p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, NMH_CODIGO);
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
        public static int InsertPlanillaNominaHD(SessionManager oSessionManager, int NMH_CODIGO,int NMP_CODIGO, string NMH_DESCRIPCION,string NMH_USUARIO,string NMH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO NM_LIQUIDACIONHD (NMH_CODIGO,NMP_CODIGO,NMH_DESCRIPCION,NMH_USUARIO,NMH_ESTADO,NMH_FECING,NMH_FECMOD) VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE(),GETDATE()) ");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMH_CODIGO, NMP_CODIGO, NMH_DESCRIPCION, NMH_USUARIO, NMH_ESTADO);
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
        public static int UpdatePlanillaNominaHD(SessionManager oSessionManager, int NMH_CODIGO, int NMP_CODIGO, string NMH_DESCRIPCION, string NMH_USUARIO, string NMH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE NM_LIQUIDACIONHD SET NMP_CODIGO=@p1,NMH_DESCRIPCION=@p2,NMH_USUARIO=@p3,NMH_ESTADO=@p4,NMH_FECMOD=GETDATE() WHERE NMH_CODIGO=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMH_CODIGO, NMP_CODIGO, NMH_DESCRIPCION, NMH_USUARIO, NMH_ESTADO);
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
        public static int InsertPlanillaNominaDT(SessionManager oSessionManager, int NMH_CODIGO, int TRCODTER, int NMD_ORIGEN, int PD_CODIGO, double NMD_VALOR, string NMD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO NM_LIQUIDACIONDT (NMH_CODIGO,TRCODTER,NMD_ORIGEN,PD_CODIGO,NMD_VALOR,NMD_ESTADO,NMD_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMH_CODIGO, TRCODTER, NMD_ORIGEN, PD_CODIGO, NMD_VALOR, NMD_ESTADO);
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
        public static int InsertNovedades(SessionManager oSessionManager, int NMP_CODIGO, int TRCODTER,  string NV_CONCEPTO, double NV_VALOR, string NV_TIPOPV, string NV_TIPOSR, string NV_BASE, string NV_USUARIO, string NV_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO NM_NOVEDADES (NMP_CODIGO, TRCODTER, NV_CONCEPTO, NV_VALOR, NV_TIPOPV, NV_TIPOSR, NV_BASE, NV_USUARIO, NV_ESTADO, NV_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text, NMP_CODIGO, TRCODTER, NV_CONCEPTO, NV_VALOR, NV_TIPOPV, NV_TIPOSR, NV_BASE, NV_USUARIO, NV_ESTADO);
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
        public static int DeletePlanillaDT(SessionManager oSessionManager, int NMH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("DELETE FROM NM_LIQUIDACIONDT WHERE NMH_CODIGO = @p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMH_CODIGO);
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
        public static int DeleteNovedades(SessionManager oSessionManager, int NMP_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM NM_NOVEDADES WHERE NMP_CODIGO=@p0");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMP_CODIGO);
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
