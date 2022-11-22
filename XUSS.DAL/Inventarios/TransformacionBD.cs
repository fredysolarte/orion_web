using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Inventarios
{
    public class TransformacionBD
    {
        public DataTable GetTransformacion(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_TRANSFORMACION.*, A.BDNOMBRE");
                sSql.AppendLine("FROM TB_TRANSFORMACION WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (A.BDCODEMP = TR_CODEMP AND A.BDBODEGA = TR_BODEGA)");                                

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
        public int InsertTransformacion(SessionManager oSessionManager, string TR_CODEMP, int TR_NROTRA, DateTime TR_FECTRA, string TR_BODEGA, string TR_CDTRAN, string TR_OTTRAN,
                                  int TR_MOVENT, int TR_MOVSAL, string TR_COMENT, string P_CLISPRE, string TR_ESTADO, string TR_CAUSAE, string TR_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.AppendLine("INSERT INTO TB_TRANSFORMACION (TR_CODEMP,TR_NROTRA,TR_FECTRA,TR_BODEGA,TR_CDTRAN,TR_OTTRAN,TR_MOVENT,TR_MOVSAL,TR_COMENT,TR_ESTADO,TR_CAUSAE,TR_NMUSER,P_CLISPRE,TR_FECING,TR_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TR_CODEMP,TR_NROTRA,TR_FECTRA,TR_BODEGA,TR_CDTRAN,TR_OTTRAN,TR_MOVENT,TR_MOVSAL,TR_COMENT,TR_ESTADO,TR_CAUSAE,
                                                TR_NMUSER,P_CLISPRE);
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
