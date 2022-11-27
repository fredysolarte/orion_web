using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class tb_comercialBD
    {
        public static SqlDataReader getComercial(DBAccess ObjDB, string inFiltro)
        {
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.AppendLine("SELECT *");
                sql.AppendLine("FROM TB_COMERCIAL WITH(NOLOCK)");
                sql.AppendLine("WHERE 1=1 " + inFiltro);

                return ObjDB.ExecuteReader(sql.ToString());
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
        public int ExisteComercial(DBAccess Obj, int? PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_COMERCIAL WITH(NOLOCK) WHERE PH_CODIGO=@p0 ");

                return Convert.ToInt32(Obj.ExecuteScalar(sSql.ToString(), PH_CODIGO));
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

        public int InsertComercial(DBAccess Obj, string CO_CODEMP, int? PH_CODIGO, int? CO_CUOTAS, double? CO_PRECIO, int? TRCODTER, DateTime CO_FECHA, string CO_ESTADO, string CO_USUARIO, DateTime? CO_FECCOMODATO, DateTime? CO_FECPAGARE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_COMERCIAL (CO_CODEMP,PH_CODIGO,CO_CUOTAS,CO_PRECIO,TRCODTER,CO_FECHA, CO_FECCOMODATO,CO_FECPAGARE,CO_ESTADO,CO_USUARIO,CO_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE())");

                return Obj.ExecuteNonQuery(sSql.ToString(), CO_CODEMP, PH_CODIGO, CO_CUOTAS, CO_PRECIO, TRCODTER, CO_FECHA, CO_FECCOMODATO, CO_FECPAGARE, CO_ESTADO, CO_USUARIO);
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
        public int ExisteComercial(DBAccess Obj, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_COMERCIAL WITH(NOLOCK) WHERE PH_CODIGO=@p0 ");

                return Convert.ToInt32(Obj.ExecuteScalar(sSql.ToString(), PH_CODIGO));
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

        public static int InsertEvidenciaComercial(DBAccess ObjDB, string EC_CODEMP,int PH_CODIGO, int EC_TIPO, string EC_USUARIO,object EC_FOTO)
        {
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.AppendLine("INSERT INTO TB_EVIDENCIA_COMERCIAL (EC_CODEMP, PH_CODIGO, EC_TIPO, EC_USUARIO, EC_IMAGEN, EC_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE())");
                return ObjDB.ExecuteNonQuery(sql.ToString(), EC_CODEMP, PH_CODIGO, EC_TIPO, EC_USUARIO, EC_FOTO);
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
