using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Costos
{
    public class DescuentoBD
    {
        public static DataTable GetDescuentosArticulos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR,FECHAINI,FECHAFIN,USUARIO,ESTADO FROM TB_DETDESCUENTO WHERE ID_DESCUENTO = 1 ");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine(" AND " + filter);

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
        public static DataTable GetTipoDescuento(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT ID,NOMBRE FROM TB_DESCUENTO WHERE ESTADO = 'AC' AND ID IN (1,2)");
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
        public static int InsertDecuento(SessionManager oSessionManager, int ID, int ID_DESCUENTO,string BODEGA, string TP, string CLAVE1, string CLAVE2, string CLAVE3, string CLAVE4, double VALOR, DateTime FECHAINI, DateTime FECHAFIN, string USUARIO)
        {            
            StringBuilder sql = new StringBuilder();
            try
            {

                sql.AppendLine("INSERT INTO TB_DETDESCUENTO (ID,ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR,CONDICION_1,CONDICION_2,FECHAINI,FECHAFIN,USUARIO,");
                sql.AppendLine("                             ESTADO,FECMOD,FECING,CONDICION_3,CONDICION_4,CONDICION_5) ");
                sql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,NULL,NULL,@p9,@p10,@p11,'AC',GETDATE(),GETDATE(),NULL,NULL,NULL)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, ID,ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR, 
                                                FECHAINI.ToString("yyyyMMdd"), FECHAFIN.ToString("yyyyMMdd"), USUARIO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }
        public static int GetMaximoDescuento(SessionManager oSessionManager)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT MAX(ID) FROM TB_DETDESCUENTO");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }
        }       
    }
}
