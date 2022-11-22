using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Nomina
{
    public class PeriodosNominaBD
    {
        public static DataTable GetPeriodoNomina(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,'Periodo ' + cast(CONVERT(DATE,NMP_FECINI,101) AS VARCHAR) + ' a ' + cast(CONVERT(DATE,NMP_FECFIN,101) AS VARCHAR) Periodo FROM NM_PERIODO WITH(NOLOCK) WHERE 1=1");
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

        public static int InsertPeriodoNomina(SessionManager oSessionManager, int NMP_CODIGO,string NMP_CODEMP,DateTime NMP_FECINI,DateTime NMP_FECFIN,string NMP_USUARIO,string NMP_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO NM_PERIODO (NMP_CODIGO, NMP_CODEMP, NMP_FECINI, NMP_FECFIN, NMP_USUARIO, NMP_ESTADO,NMP_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMP_CODIGO, NMP_CODEMP, NMP_FECINI, NMP_FECFIN, NMP_USUARIO, NMP_ESTADO);
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

        public static int UpdatePeriodoNomina(SessionManager oSessionManager, int NMP_CODIGO, string NMP_CODEMP, DateTime NMP_FECINI, DateTime NMP_FECFIN, string NMP_USUARIO, string NMP_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE NM_PERIODO SET  NMP_CODEMP=@p1, NMP_FECINI=@p2, NMP_FECFIN=@p3, NMP_USUARIO=@p4, NMP_ESTADO=@p5 WHERE NMP_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, NMP_CODIGO, NMP_CODEMP, NMP_FECINI, NMP_FECFIN, NMP_USUARIO, NMP_ESTADO);
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
