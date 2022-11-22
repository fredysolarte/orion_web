using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Costos
{
    public class DecuentoCedulaBD
    {
        public DataTable GetDescuentosCedula(SessionManager oSessionManager, string filter)
        { 
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_DETDESCUENTO.*,TRNOMBRE,TRAPELLI,TRCONTAC ");
                sSql.AppendLine("FROM TB_DETDESCUENTO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON( CONDICION_1 = TRCODNIT) WHERE ID_DESCUENTO = 5");
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

        public DataTable GetAlmacenes(SessionManager oSessionManager)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT BDBODEGA,BDNOMBRE FROM TBBODEGA WHERE BDALMACE ='S'");
                sql.AppendLine("UNION");
                sql.AppendLine("SELECT '.' BDBODEGA, 'TODOS' BDNOMBRE FROM DUAL");
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
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

        public IDataReader GetNombreTerceros(SessionManager oSessionManager, string filter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TOP 1 TRNOMBRE,TRAPELLI,TRCONTAC,TRCORREO,TRFECNAC FROM TERCEROS "+ filter);
                //return Convert.ToString(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text));
                return DBAccess.GetDataReader(oSessionManager, sql.ToString(), CommandType.Text);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql = null;
            }

        }

        public int InsertDecuento(SessionManager oSessionManager, string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, DateTime FECHAFIN, string USUARIO, string CONDICION_2)
        {
            int ln_id = 0;
            int ln_dias = 0;
            StringBuilder sql = new StringBuilder();
            try
            {
                if (CONDICION_2 == "S")
                    ln_dias = 1;
                //Obteniendo Numero Maximo de Descuento para Crear
                ln_id = GetMaximoDescuento(oSessionManager) +  1;

                sql.AppendLine("INSERT INTO TB_DETDESCUENTO (ID,ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR,CONDICION_1,CONDICION_2,FECHAINI,FECHAFIN,USUARIO,");
                sql.AppendLine("                             ESTADO,FECMOD,FECING,CONDICION_3,CONDICION_4,CONDICION_5) ");
                sql.AppendLine("VALUES (@p0,5,@p1,'.','.','.','.','.',@p2,@p3,@p4,@p5,DATEADD(YEAR,@p7,@p6),@p8,'AC',GETDATE(),GETDATE(),NULL,NULL,NULL)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, ln_id, BODEGA, VALOR, CONDICION_1, CONDICION_2, FECHAINI.ToString("yyyyMMdd"), FECHAFIN.ToString("yyyyMMdd"), ln_dias, USUARIO);
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

        public int UpdateDecuento(SessionManager oSessionManager, int ID,string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, 
                                  DateTime FECHAFIN, string USUARIO, string CONDICION_2, string ESTADO)
        {
            //int ln_id = 0;
            //int ln_dias = 0;
            StringBuilder sql = new StringBuilder();
            try
            {
                //if (CONDICION_2 == "S")
                //    ln_dias = 1;
                //Obteniendo Numero Maximo de Descuento para Crear
                //ln_id = GetMaximoDescuento(oSessionManager) + 1;

                //sql.AppendLine("INSERT INTO TB_DETDESCUENTO (ID,ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR,CONDICION_1,CONDICION_2,FECHAINI,FECHAFIN,USUARIO,");
                //sql.AppendLine("                             ESTADO,FECMOD,FECING,CONDICION_3,CONDICION_4,CONDICION_5) ");
                //sql.AppendLine("VALUES (@p0,5,@p1,'.','.','.','.','.',@p2,@p3,@p4,@p5,DATEADD(YEAR,@p7,@p6),@p8,'AC',GETDATE(),GETDATE(),NULL,NULL,NULL)");
                sql.AppendLine("UPDATE TB_DETDESCUENTO  SET FECHAINI =@p0,FECHAFIN=@p1,VALOR=@p2,FECMOD=GETDATE(),USUARIO=@p3, ESTADO=@p4 WHERE ID=@p5");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, FECHAINI.ToString("yyyyMMdd"), FECHAFIN.ToString("yyyyMMdd"), VALOR,USUARIO,ESTADO,ID);
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

        public int GetMaximoDescuento(SessionManager oSessionManager)
        {
            StringBuilder sql = new StringBuilder();
            try 
            {
                sql.AppendLine("SELECT MAX(ID) FROM TB_DETDESCUENTO");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager,sql.ToString(),CommandType.Text));
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
        //public int GetContador(SessionManager oSessionManager, string CODEMP, string CODCLA)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    try
        //    {
        //        sSql.AppendLine(" UPDATE TBTABLAS");
        //        sSql.AppendLine("SET TTVALORN = TTVALORN +1");
        //        sSql.AppendLine("WHERE TTCODEMP = @p0");
        //        sSql.AppendLine("  AND TTCODTAB = 'CONT'");
        //        sSql.AppendLine("  AND TTCODCLA = @p1");

        //        DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, CODCLA);

        //        sSql.AppendLine("SELECT TTVALORN");
        //        sSql.AppendLine("  FROM TBTABLAS ");
        //        sSql.AppendLine("WHERE TTCODEMP = @p0");
        //        sSql.AppendLine("   AND TTCODTAB = 'CONT'");
        //        sSql.AppendLine("   AND TTCODCLA = @p1");

        //        return Convert.ToInt32(DBAccess.GetScalar(oSessionManager,sSql.ToString(), CommandType.Text, CODEMP, CODCLA));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sSql = null;
        //    }
        //}
        
    } 
}
