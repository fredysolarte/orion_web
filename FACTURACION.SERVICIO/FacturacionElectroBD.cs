using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACTURACION.SERVICIO
{
    public class FacturacionElectroBD
    {
        public DataTable GetFacturaHD(DBAccess ObjDB, string HDTIPFAC,int HDNROFAC,string HDCODEMP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM V_ventashd WITH(NOLOCK)");                
                sSql.AppendLine("WHERE TFTIPFAC=@p0 AND HDNROFAC=@p1 AND HDCODEMP=@p2");

                return ObjDB.ExecuteDataTable(sSql.ToString(), HDTIPFAC, HDNROFAC,HDCODEMP);
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

        public SqlDataReader GetFacturaDT(DBAccess ObjDB, string DTCODEMP,string DTTIPFAC, int DTNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM V_facturadt WITH(NOLOCK)");
                sSql.AppendLine("WHERE HDCODEMP =@p0 AND TFTIPFAC=@p1 AND HDNROFAC=@p2");
                sSql.AppendLine("ORDER BY DTNROITM");

                return ObjDB.ExecuteReader(sSql.ToString(), DTCODEMP, DTTIPFAC, DTNROFAC);
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

        public DataTable GetPagos(DBAccess ObjDB, string HDCODEMP, string HDTIPFAC, int HDNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,CASE WHEN Cod_TipPag = '06' THEN '2' ELSE '1' END FORMA_PAGO,");
                sSql.AppendLine("CASE WHEN Cod_TipPag = '06' THEN '31'");
                sSql.AppendLine("WHEN Cod_TipPag = '01' THEN '10'");
                sSql.AppendLine("WHEN Cod_TipPag = '02' THEN '20'");
                sSql.AppendLine("WHEN Cod_TipPag = '03' THEN '49'");
                sSql.AppendLine("WHEN Cod_TipPag = '04' THEN '48'");
                sSql.AppendLine("END  MEDIO_PAGO");
                sSql.AppendLine("FROM V_PGFACTUR WITH(NOLOCK)");
                sSql.AppendLine("WHERE Cod_TipFac=@p0 AND Nro_Documento=@p1 AND HDCODEMP=@p2");

                return ObjDB.ExecuteDataTable(sSql.ToString(), HDTIPFAC, HDNROFAC, HDCODEMP);
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

        public DataTable GetFacPendientesEnvio(DBAccess ObjDB)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT HDTIPFAC,HDNROFAC,TFPREFIJ ");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC)");
                sSql.AppendLine("INNER JOIN TBBODEGA WITH(NOLOCK) ON(HDCODEMP = BDCODEMP AND TFBODEGA = BDBODEGA)");
                sSql.AppendLine("WHERE(HDTIPFAC + CAST(HDNROFAC AS VARCHAR)) NOT IN(SELECT HDTIPFAC + CAST(HDNROFAC AS VARCHAR) FROM TB_ENVIO_FACTURAS WITH(NOLOCK))");
                sSql.AppendLine("AND CONVERT(DATE, HDFECFAC, 101) > CONVERT(DATE, '31/08/2020')");
                sSql.AppendLine("AND BDBODEGA = 'BO' AND TFCLAFAC = '1'");

                return ObjDB.ExecuteDataTable(sSql.ToString());
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

        public int InsertFacturaEnvio(DBAccess ObjDB, string EF_CODEMP, string HDTIPFAC, int HDNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_ENVIO_FACTURAS (EF_CODEMP,HDTIPFAC,HDNROFAC,EF_FECING) VALUES (@p0,@p1,@p2,GETDATE())");                

                return ObjDB.ExecuteNonQuery(sSql.ToString(),EF_CODEMP,HDTIPFAC,HDNROFAC);
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
