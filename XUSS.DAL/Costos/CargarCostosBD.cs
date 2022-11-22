using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Costos
{
    public class CargarCostosBD
    {
        public static DataTable GetCostos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TRA_COSTOS.*,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOMBRE,ARNOMBRE FROM TRA_COSTOS WITH(NOLOCK)");                
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = CT_CODEMP AND CT_TIPPRO = ARTIPPRO AND CT_CLAVE1 = ARCLAVE1 AND CT_CLAVE2 = ARCLAVE2 AND CT_CLAVE3 = ARCLAVE3 AND CT_CLAVE4 = ARCLAVE4)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (CT_CODEMP = TRCODEMP AND TRA_COSTOS.TRCODTER = TERCEROS.TRCODTER) ");
                sSql.AppendLine(" WHERE 1=1");
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
        public static int InsertCosto(SessionManager oSessionManager,string CT_CODEMP,int TSNROTRA,string CT_TIPPRO,string CT_CLAVE1,string CT_CLAVE2,string CT_CLAVE3,string CT_CLAVE4,
                               int TRCODTER, string CT_TIPDOC, string CT_NUMDOC, DateTime CT_FECDOC, string CT_MONEDA, double CT_PRECIO, string CT_OBSERVACIONES, string CT_USUARIO, string CT_ESTADO, string CT_TDOCORIGEN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TRA_COSTOS (CT_CODEMP,TSNROTRA,CT_TIPPRO,CT_CLAVE1,CT_CLAVE2,CT_CLAVE3,CT_CLAVE4,TRCODTER,CT_TIPDOC,CT_NUMDOC,CT_FECDOC,CT_MONEDA,CT_PRECIO,CT_OBSERVACIONES,CT_USUARIO,CT_ESTADO,CT_TDOCORIGEN,CT_FECING,CT_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CT_CODEMP, TSNROTRA, CT_TIPPRO, CT_CLAVE1, CT_CLAVE2, CT_CLAVE3, CT_CLAVE4, TRCODTER, CT_TIPDOC, CT_NUMDOC, CT_FECDOC,
                                                CT_MONEDA, CT_PRECIO, CT_OBSERVACIONES, CT_USUARIO, CT_ESTADO, CT_TDOCORIGEN);
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
        public static int DeleteCosto(SessionManager oSessionManager, int TSNROTRA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TRA_COSTOS WHERE TSNROTRA=@p0");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TSNROTRA);
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
