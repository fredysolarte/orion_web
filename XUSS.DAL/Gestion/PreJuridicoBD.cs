using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Gestion
{
    public class PreJuridicoBD
    {
        public static DataTable CargarObligaciones(SessionManager oSessionManager, string DD_CODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,(ISNULL(DD_FCAPITAL,0)+ISNULL(DD_FCAPITAL,0)+ISNULL(DD_FMORA,0)) SALDO FROM TB_DETALLE_DEUDA WITH(NOLOCK) WHERE DD_CODEMP=@p0 AND TRCODTER=@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, DD_CODEMP, TRCODTER);
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
        public static int InsertObligacion(SessionManager oSessionManager, string DD_CODEMP, int TRCODTER, string DD_NROOBLIGACION, string DD_DESCRIPCIN, string DD_TCARTERA, int DD_DIASMORA,double? DD_FCAPITAL,double? DD_FCORRIENTE,double? DD_FMORA, string DD_USUARIO, string DD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_DETALLE_DEUDA (DD_CODEMP,TRCODTER,DD_NROOBLIGACION,DD_DESCRIPCIN,DD_TCARTERA,DD_DIASMORA,DD_FCAPITAL,DD_FCORRIENTE,DD_FMORA,DD_USUARIO,DD_ESTADO,DD_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DD_CODEMP, TRCODTER, DD_NROOBLIGACION, DD_DESCRIPCIN, DD_TCARTERA, DD_DIASMORA, DD_FCAPITAL, DD_FCORRIENTE, DD_FMORA, DD_USUARIO, DD_ESTADO);
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
        public static int InsertPrejuridico(SessionManager oSessionManager, string PD_CODEMP, int TRCODTER, string PD_TIPIFICACION, string PD_TELEFONO, string PD_EMAIL, string PD_OBSERVACION, string PD_USUARIO, string PD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_DETALLE_PREJURIDICO (PD_CODEMP,TRCODTER,PD_TIPIFICACION,PD_TELEFONO,PD_EMAIL,PD_OBSERVACION,PD_USUARIO,PD_ESTADO,PD_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PD_CODEMP, TRCODTER, PD_TIPIFICACION, PD_TELEFONO, PD_EMAIL,PD_OBSERVACION, PD_USUARIO, PD_ESTADO);
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

        public static DataTable GetPrejuridico(SessionManager oSessionManager, string PD_CODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_DETALLE_PREJURIDICO.*,TTDESCRI,usua_nombres");
                sSql.AppendLine("FROM TB_DETALLE_PREJURIDICO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON(TTCODEMP = PD_CODEMP AND TTCODTAB = 'TIPIF' AND TTCODCLA = PD_TIPIFICACION)");
                sSql.AppendLine("INNER JOIN admi_tusuario WITH(NOLOCK) ON(usua_usuario = PD_USUARIO)");
                sSql.AppendLine("WHERE  PD_CODEMP =@p0 AND TRCODTER=@p1 ");
                sSql.AppendLine("ORDER BY PD_FECING DESC");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PD_CODEMP, TRCODTER);
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
