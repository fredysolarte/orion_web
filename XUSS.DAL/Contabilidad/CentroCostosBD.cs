using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Contabilidad
{
    public class CentroCostosBD
    {
        public DataTable GetCentroCostos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM CONT_CTCOSTOS WITH(NOLOCK) WHERE 1=1 ");

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
        public int InsertCentroCostos(SessionManager oSessionManager, string CTC_CODEMP, string CTC_IDENTI, string CTC_NOMBRE, string CTC_USUARIO,
                                     DateTime CTC_FECMOD, string CTC_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_CTCOSTOS (CTC_CODEMP, CTC_IDENTI, CTC_NOMBRE, CTC_USUARIO, CTC_FECMOD, CTC_ESTADO) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CTC_CODEMP, CTC_IDENTI, CTC_NOMBRE, CTC_USUARIO, CTC_FECMOD, CTC_ESTADO);
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
        public int UpdateCentroCostos(SessionManager oSessionManager, string CTC_CODEMP, string CTC_IDENTI, string CTC_NOMBRE, string CTC_USUARIO,
                                     DateTime CTC_FECMOD, string CTC_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CONT_CTCOSTOS SET CTC_NOMBRE=@p0, CTC_USUARIO=@p1, CTC_FECMOD=@p2, CTC_ESTADO=@p3");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CTC_CODEMP, CTC_IDENTI, CTC_NOMBRE, CTC_USUARIO, CTC_FECMOD, CTC_ESTADO);
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
