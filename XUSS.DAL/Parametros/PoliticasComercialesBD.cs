using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Parametros
{
    public class PoliticasComercialesBD
    {
        public static DataTable GetPoliticaHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_DESCUENTO WITH(NOLOCK) WHERE 1=1");
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
        public static DataTable GetPoliticaDT(SessionManager oSessionManager, string filter, int ID_DESCUENTO,string CODEMP)
        { 
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine(" SELECT CASE WHEN TP = '.' THEN 'Todos' ELSE TANOMBRE END TP_,");
                sSql.AppendLine("CASE WHEN BODEGA = '.' THEN 'Todos' ELSE BDNOMBRE END BODEGA_,");
                sSql.AppendLine("CASE WHEN CLAVE1 = '.' THEN 'Todos' ELSE CLAVE1 END CLAVE1_,");
                sSql.AppendLine("CASE WHEN CLAVE2 = '.' THEN 'Todos' ELSE CLAVE2 END CLAVE2_,");
                sSql.AppendLine("CASE WHEN CLAVE3 = '.' THEN 'Todos' ELSE CLAVE3 END CLAVE3_,");
                sSql.AppendLine("CASE WHEN CLAVE4 = '.' THEN 'Todos' ELSE CLAVE4 END CLAVE4_,");
                sSql.AppendLine("ID,ID_DESCUENTO,VALOR,CONDICION_1,CONDICION_2,FECHAINI,FECHAFIN,USUARIO,");
                sSql.AppendLine("ESTADO,FECMOD,FECING,CONDICION_3,CONDICION_4,CONDICION_5,TP,BODEGA,CLAVE1,CLAVE2,CLAVE3,CLAVE4");
                sSql.AppendLine("FROM TB_DETDESCUENTO WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TBTIPPRO WITH(NOLOCK) ON (TATIPPRO = TP AND TACODEMP =@p0)");
                sSql.AppendLine("LEFT OUTER JOIN TBBODEGA WITH(NOLOCK) ON (BDBODEGA = BODEGA AND BDCODEMP =@p0)");
                sSql.AppendLine("WHERE ID_DESCUENTO =@p1 ");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, ID_DESCUENTO);
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
        public static int InsertPoliticaDT(SessionManager oSessionManager, int ID_DESCUENTO, string BODEGA, string TP, string CLAVE1, string CLAVE2, string CLAVE3, string CLAVE4, double? VALOR, string CONDICION_1,
                                           string CONDICION_2, DateTime? FECHAINI, DateTime? FECHAFIN, string USUARIO, string ESTADO, double CONDICION_3, double CONDICION_4, int CONDICION_5)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_DETDESCUENTO (ID_DESCUENTO, BODEGA, TP, CLAVE1, CLAVE2, CLAVE3, CLAVE4, VALOR, CONDICION_1, CONDICION_2, FECHAINI, FECHAFIN, USUARIO, ESTADO, CONDICION_3, CONDICION_4, CONDICION_5,FECMOD, FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,ID_DESCUENTO, BODEGA, TP, CLAVE1, CLAVE2, CLAVE3, CLAVE4, VALOR, CONDICION_1, CONDICION_2, FECHAINI, FECHAFIN, USUARIO, ESTADO, CONDICION_3, CONDICION_4, CONDICION_5);
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
        public static int AnularPolitaDT(SessionManager oSessionManager, int ID, string USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE TB_DETDESCUENTO SET ESTADO='AN',FECMOD=GETDATE(),USUARIO=@p0 WHERE ID=@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, USUARIO, ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                sSql =null;
            }
        }
    }
}
