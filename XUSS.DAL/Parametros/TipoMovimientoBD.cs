using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class TipoMovimientoBD
    {
        public static DataTable GetTipoMovimiento(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  SELECT * FROM TBTIPMOV WITH(NOLOCK) WHERE 1=1");
                
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
        public static IDataReader GetTipoMovimientoR(SessionManager oSessionManager, string CODEMP, string CDTRAN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  SELECT TMENTSAL, TMMOVPAR, TMCONPRO,TMTIPMOV FROM TBTIPMOV WITH(NOLOCK) WHERE TMCODEMP = @p0 AND TMCDTRAN = @p1 ");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, CDTRAN);
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
        public static int InsertTipoMovimiento(SessionManager oSessionManager, string TMCODEMP, string TMCDTRAN, string TMNOMBRE, string TMDESCRI, string TMENTSAL, string TMREQDOC, string TMTIPMOV, string TMMOVMAN,
            string TMOTTRAN, string TMMOVPAR, string TMCONMAQ, string TMORICST, string TMPRIORI, string TMBODCON, string TMACTFEC, string TMESTADO, string TMCAUSAE, string TMNMUSER, string TMCONPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBTIPMOV (TMCODEMP,TMCDTRAN,TMNOMBRE,TMDESCRI,TMENTSAL,TMREQDOC,TMTIPMOV,TMMOVMAN,TMOTTRAN,TMMOVPAR,TMCONMAQ,TMORICST,TMPRIORI,TMBODCON,TMACTFEC,TMESTADO,");
                sSql.AppendLine("TMCAUSAE,TMNMUSER,TMCONPRO,TMFECING,TMFECMOD) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TMCODEMP, TMCDTRAN, TMNOMBRE, TMDESCRI, TMENTSAL, TMREQDOC, TMTIPMOV, TMMOVMAN, TMOTTRAN, TMMOVPAR, TMCONMAQ, TMORICST, TMPRIORI, TMBODCON, TMACTFEC, TMESTADO, TMCAUSAE, TMNMUSER, TMCONPRO);
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

        public static int UpdateTipoMovimiento(SessionManager oSessionManager, string TMCODEMP, string TMCDTRAN, string TMNOMBRE, string TMDESCRI, string TMENTSAL, string TMREQDOC, string TMTIPMOV, string TMMOVMAN,
            string TMOTTRAN, string TMMOVPAR, string TMCONMAQ, string TMORICST, string TMPRIORI, string TMBODCON, string TMACTFEC, string TMESTADO, string TMCAUSAE, string TMNMUSER, string TMCONPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBTIPMOV SET TMNOMBRE =@p2,TMDESCRI =@p3,TMENTSAL =@p4,TMREQDOC =@p5,TMTIPMOV =@p6,TMMOVMAN =@p7,TMOTTRAN=@p8,TMMOVPAR=@p9,TMCONMAQ=@p10,TMORICST=@p11,TMPRIORI=@p12,");
                sSql.AppendLine("                    TMBODCON =@p13,TMACTFEC =@p14,TMESTADO = @p15,TMCAUSAE =@p16,TMNMUSER=@p17,TMCONPRO=@p18,TMFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE TMCODEMP=@p0 AND TMCDTRAN = @p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TMCODEMP, TMCDTRAN, TMNOMBRE, TMDESCRI, TMENTSAL, TMREQDOC, TMTIPMOV, TMMOVMAN, TMOTTRAN, TMMOVPAR, TMCONMAQ, TMORICST, TMPRIORI, TMBODCON, TMACTFEC, TMESTADO, TMCAUSAE, TMNMUSER, TMCONPRO);
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
