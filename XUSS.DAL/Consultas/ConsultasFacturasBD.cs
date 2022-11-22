using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
    public class ConsultasFacturasBD
    {
        public DataTable GetFacturasExistentes(SessionManager oSessionManager, string filter, string filter_, string filter__)
        {
            StringBuilder sSql = new StringBuilder();
            try 
            {
                sSql.AppendLine("SELECT DISTINCT HDNROFAC,(TRNOMBRE + TRNOMBR2) NOMBRE, HDFECFAC, BDNOMBRE, TFNOMBRE, HDTOTFAC, HDTIPFAC");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON(HDCODNIT = TRCODNIT AND HDCODEMP = TRCODEMP");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                sSql.AppendLine(") INNER JOIN FACTURADT WITH(NOLOCK) ON(DTCODEMP = HDCODEMP AND HDNROFAC = DTNROFAC AND HDTIPFAC = DTTIPFAC     ");
                if (!string.IsNullOrWhiteSpace(filter__))
                {
                    sSql.AppendLine("AND " + filter__);
                }
                sSql.AppendLine(") ,");
                sSql.AppendLine("TBTIPFAC WITH(NOLOCK), TBBODEGA WITH(NOLOCK)");
                sSql.AppendLine("WHERE HDTIPFAC = TFTIPFAC ");
                sSql.AppendLine("AND HDCODEMP = TFCODEMP");
                sSql.AppendLine("AND TFBODEGA = BDBODEGA");
                sSql.AppendLine("AND TFCODEMP = BDCODEMP");

                if (!string.IsNullOrWhiteSpace(filter_))
                {
                    sSql.AppendLine("AND " + filter_);
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
        public DataTable GetDetalleFactura(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TANOMBRE,ARNOMBRE,DTCLAVE1,DTCLAVE2,DTCLAVE3,DTCANTID,DTPRELIS DTPRECIO,(DTTOTFAC/DTCANTID) PVENTA, DTDESCUE,DTCANTID,IM_IMAGEN,NOMDES,CLAVE2,CLAVE3");                
                //sSql.AppendLine(" CASE ");
                //sSql.AppendLine(" WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                //sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE2");
                //sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                //sSql.AppendLine(" CASE ");
                //sSql.AppendLine(" WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP ");
                //sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE3");
                //sSql.AppendLine("                              AND ASNIVELC = 3) ELSE DTCLAVE3 END CLAVE3");
                //sSql.AppendLine("  FROM FACTURADT WITH(NOLOCK)");
                sSql.AppendLine("  FROM V_facturadt ");

                sSql.AppendLine("  LEFT OUTER JOIN IMAGENES WITH(NOLOCK) ON( IM_CODEMP = HDCODEMP AND IM_TIPPRO = DTTIPPRO AND IM_CLAVE1 = DTCLAVE1 ");
                sSql.AppendLine("                                   AND IM_CLAVE2 ='.' AND IM_CLAVE3 ='.' AND IM_CLAVE4 ='.' AND IM_TIPIMA = 1)");

                //sSql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (DTTIPPRO = ARTIPPRO  AND DTCLAVE1 = ARCLAVE1 AND DTCLAVE2 = ARCLAVE2 ");
                //sSql.AppendLine("                                  AND DTCLAVE3 = ARCLAVE3 AND HDCODEMP = ARCODEMP)");
                //sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON( DTTIPPRO = TATIPPRO AND HDCODEMP = TACODEMP)");

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
        public DataTable GetDetalleFacturacion(SessionManager oSessionManager, string filter, int inMes, int inAno)
        { 
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT V_facturadt.* FROM V_facturadt");
                sSql.AppendLine(" WHERE HDESTADO<>'AN' AND MONTH(HDFECFAC)=@p0 AND YEAR(HDFECFAC)=@p1 "+filter);

                //if (!string.IsNullOrWhiteSpace(filter))
                //{
                //    sSql.AppendLine("AND " + filter);
                //}
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,inMes,inAno);
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
        public DataTable GetDetalleFacturacion(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT V_facturadt.*,(TFTIPFAC+'-'+CAST(HDNROFAC AS VARCHAR)) NRO_FAC FROM V_facturadt");
                sSql.AppendLine(" WHERE 1=1 ");

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
    }
}
