using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Facturacion
{
    public class GarantiasBD
    {
        public static DataTable GetGarantia(SessionManager oSessionManager, string GT_CODEMP, string GT_TIPFAC, int GT_NROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * ");
                sSql.AppendLine("  FROM TB_GARANTIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE GT_CODEMP = @p0");
                sSql.AppendLine("   AND GT_TIPFAC = @p1");
                sSql.AppendLine("   AND GT_NROFAC = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, GT_CODEMP, GT_TIPFAC, GT_NROFAC);
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
        public static int Insertgarantia(SessionManager oSessionManager, string GT_CODEMP, string GT_TIPFAC, int GT_NROFAC,string GT_USUARIO,string GT_OBSERVACIONES,string GT_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_GARANTIA (GT_CODEMP,GT_TIPFAC,GT_NROFAC,GT_USUARIO,GT_OBSERVACIONES,GT_ESTADO,GT_FECING) ");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, GT_CODEMP, GT_TIPFAC, GT_NROFAC, GT_USUARIO, GT_OBSERVACIONES, GT_ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
    }
}
