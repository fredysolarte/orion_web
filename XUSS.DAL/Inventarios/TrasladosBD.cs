using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Inventarios
{
    public class TrasladosBD
    {
        public DataTable GetTraslados(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TRASLADO.*, A.BDNOMBRE, A.BDMNCAJA, B.BDNOMBRE OTNOMBRE, ETNOMBRE,");
                sSql.AppendLine("(SELECT TOP 1 WIH_CONSECUTIVO FROM TB_WRINHD WITH(NOLOCK) WHERE TB_WRINHD.TSNROTRA = TRASLADO.TSNROTRA AND WIH_ESTADO IN ('AC','CE')) WIH_CONSECUTIVO,");
                sSql.AppendLine("(SELECT TOP 1 WOH_CONSECUTIVO FROM TB_WROUT_TRASLADO WITH(NOLOCK) WHERE TB_WROUT_TRASLADO.TSNROTRA = TRASLADO.TSNROTRA) WOH_CONSECUTIVO, ");
                sSql.AppendLine("(TRNOMBRE + ' ' + ISNULL(TRNOMBR2,'') + '-' + TRNOMCOMERCIAL) CLIENTE");
                sSql.AppendLine("FROM TRASLADO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (A.BDCODEMP = TSCODEMP AND A.BDBODEGA = TSBODEGA)");
                sSql.AppendLine("INNER JOIN TBBODEGA B WITH(NOLOCK) ON (B.BDCODEMP = TSCODEMP AND B.BDBODEGA = TSOTBODE)");
                sSql.AppendLine("LEFT OUTER JOIN TBESTADO WITH(NOLOCK) ON (ETCODEMP = TSCODEMP AND ETGRPTAB = 'TS' AND ETESTADO = TSESTADO  AND ETCAUSAE = TSCAUSAE)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (TERCEROS.TRCODEMP = TSCODEMP AND TERCEROS.TRCODTER = TRASLADO.TRCODTER)");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("WHERE " + filter);
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
        public int InsertTraslado(SessionManager oSessionManager, string TSCODEMP, int TSNROTRA, DateTime TSFECTRA, string TSBODEGA, string TSOTBODE, string TSCDTRAN, string TSOTTRAN,
                                  int TSMOVENT, int TSMOVSAL, string TSCOMENT, string P_CLISPRE, string TSESTADO, string TSCAUSAE, string TSNMUSER, DateTime? TSFECCNF, string TSMLIBRE, string TSCFUSER, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try {

                sSql.AppendLine("INSERT INTO TRASLADO (TSCODEMP,TSNROTRA,TSFECTRA,TSBODEGA,TSOTBODE,TSCDTRAN,TSOTTRAN,TSMOVENT,TSMOVSAL,TSCOMENT,TSESTADO,TSCAUSAE,TSNMUSER,TSFECCNF,TSMLIBRE,P_CLISPRE,TSCFUSER,TRCODTER,TSFECING,TSFECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,TSCODEMP, TSNROTRA, TSFECTRA, TSBODEGA, TSOTBODE, TSCDTRAN, TSOTTRAN,
                                  TSMOVENT, TSMOVSAL, TSCOMENT, TSESTADO, TSCAUSAE, TSNMUSER, TSFECCNF, TSMLIBRE, P_CLISPRE, TSCFUSER, TRCODTER);
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
        public int UpdateTraslado(SessionManager oSessionManager, string TSCODEMP, int TSNROTRA, DateTime TSFECTRA, string TSBODEGA, string TSOTBODE, string TSCDTRAN, string TSOTTRAN,
                                  int TSMOVENT, int TSMOVSAL, string TSCOMENT, string TSCAUSAE,string TSESTADO, string TSMLIBRE, string TSCFUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.AppendLine("UPDATE TRASLADO SET TSFECTRA=@p2,TSBODEGA=@p3,TSOTBODE=@p4,TSCDTRAN=@p5,TSOTTRAN=@p6,TSMOVENT=@p7,TSMOVSAL=@p8,TSCOMENT=@p9,TSESTADO=@p10,TSCAUSAE=@p11,TSFECCNF=GETDATE(),TSMLIBRE=@p12,TSCFUSER=@p13");
                sSql.AppendLine(" WHERE TSCODEMP=@p0 AND TSNROTRA=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TSCODEMP, TSNROTRA, TSFECTRA, TSBODEGA, TSOTBODE, TSCDTRAN, TSOTTRAN,
                                  TSMOVENT, TSMOVSAL, TSCOMENT, TSESTADO, TSCAUSAE, TSMLIBRE, TSCFUSER);
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
        public int AnulaTraslado(SessionManager oSessionManager, string TSCODEMP, int TSNROTRA, string TSESTADO, string TSCFUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.AppendLine("UPDATE TRASLADO SET TSESTADO=@p2,TSCFUSER=@p3");
                sSql.AppendLine(" WHERE TSCODEMP=@p0 AND TSNROTRA=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TSCODEMP, TSNROTRA, TSESTADO, TSCFUSER);
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
