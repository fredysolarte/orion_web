using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace XUSS.DAL.Compras
{
    public class SegregacionBD
    {
        public static DataTable GetSegregacionHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine(" SELECT TB_SEGREACION_HD.*,(CMP.TRNOMBRE + ' ' + ISNULL(CMP.TRNOMBR2 ,'') + ' ' + ISNULL(CMP.TRAPELLI,''))  COMPRADOR, (VEN.TRNOMBRE + ' ' + ISNULL(VEN.TRNOMBR2 ,'') + ' ' + ISNULL(VEN.TRAPELLI,''))  VENDEDOR");
                sql.AppendLine("  FROM TB_SEGREACION_HD WITH(NOLOCK)");
                sql.AppendLine("LEFT OUTER JOIN TERCEROS CMP WITH(NOLOCK) ON(CMP.TRCODEMP = HDCODEMP AND SG_COMPRADORPRO = CMP.TRCODTER)");
                sql.AppendLine("LEFT OUTER JOIN TERCEROS VEN WITH(NOLOCK) ON(VEN.TRCODEMP = HDCODEMP AND SG_VENDEDORPRO = VEN.TRCODTER) ");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
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
        //public static DataTable GetSegregacionHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    try
        //    {
        //        sql.AppendLine("SELECT DISTINCT SG_CODIGO FROM TB_SEGREGACION WITH(NOLOCK) ");                
        //        sql.AppendLine("WHERE 1=1");

        //        if (!string.IsNullOrWhiteSpace(filter))
        //        {
        //            sql.AppendLine(" AND " + filter);
        //        }
        //        return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        sql = null;
        //    }
        //}
        public static DataTable GetSegregacionProforma(SessionManager oSessionManager, int SGH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT SGD_ID,SGH_CODIGO,TB_SEGREACION_PROFORMADT.PR_CODEMP,TB_SEGREACION_PROFORMADT.PR_NROCMP,TB_SEGREACION_PROFORMADT.PR_NROITEM,SGD_CANTIDAD,SGD_PRECIO,SGD_BODEGA,SGD_GRUPO,SGD_USUARIO,SGD_ESTADO,SGD_CANTIDADAPRO-SGD_CANTIDAD as SGD_CANTIDADAPRO");
                sSql.AppendLine("SELECT SGD_ID,SGH_CODIGO,TB_SEGREACION_PROFORMADT.PR_CODEMP,TB_SEGREACION_PROFORMADT.PR_NROCMP,TB_SEGREACION_PROFORMADT.PR_NROITEM,SGD_CANTIDAD,SGD_PRECIO,SGD_BODEGA,SGD_GRUPO,SGD_USUARIO,SGD_ESTADO,SGD_CANTIDADAPRO as SGD_CANTIDADAPRO");
                sSql.AppendLine("    , PR_CLAVE1,ARNOMBRE,TANOMBRE,PR_NROFACPROFORMA,AA.ASNOMBRE ARDTTEC1,BB.ASNOMBRE ARDTTEC2,CC.ASNOMBRE ARDTTEC3,DD.ASNOMBRE ARDTTEC4,EE.ASNOMBRE ARDTTEC5,FF.ASNOMBRE ARDTTEC8,");
                sSql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2  AND BCLAVE3 = ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS, PR_ORIGEN, PR_POSARA, PR_PAGO, PR_PRECIO,");
                sSql.AppendLine("PR_CANTIDAD, PR_REFPRO, PR_FECPROFORMA, PR_NROFACPROFORMA, SGD_CANTIDADAPRO,SGD_CANTIDADCMP");
                sSql.AppendLine("FROM TB_SEGREACION_PROFORMADT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN CMP_PROFACTURADT WITH(NOLOCK) ON(TB_SEGREACION_PROFORMADT.PR_CODEMP = CMP_PROFACTURADT.PR_CODEMP AND TB_SEGREACION_PROFORMADT.PR_NROCMP = CMP_PROFACTURADT.PR_NROCMP");
                sSql.AppendLine("                         AND TB_SEGREACION_PROFORMADT.PR_NROITEM = CMP_PROFACTURADT.PR_NROITEM)");
                sSql.AppendLine("INNER JOIN ARTICULO AR WITH(NOLOCK) ON(AR.ARCODEMP = CMP_PROFACTURADT.PR_CODEMP AND AR.ARTIPPRO = PR_TIPPRO AND AR.ARCLAVE1 = PR_CLAVE1)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(AR.ARCODEMP = TACODEMP AND AR.ARTIPPRO = TATIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = AR.ARCODEMP AND AA.ASTIPPRO = AR.ARTIPPRO AND AA.ASCLAVEO = AR.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = AR.ARCODEMP AND BB.ASTIPPRO = AR.ARTIPPRO AND BB.ASCLAVEO = AR.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = AR.ARCODEMP AND CC.ASTIPPRO = AR.ARTIPPRO AND CC.ASCLAVEO = AR.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = AR.ARCODEMP AND DD.ASTIPPRO = AR.ARTIPPRO AND DD.ASCLAVEO = AR.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = AR.ARCODEMP AND EE.ASTIPPRO = AR.ARTIPPRO AND EE.ASCLAVEO = AR.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = AR.ARCODEMP AND FF.ASTIPPRO = AR.ARTIPPRO AND FF.ASCLAVEO = AR.ARDTTEC7 AND FF.ASNIVELC = 10)");
                sSql.AppendLine("WHERE SGH_CODIGO = @p0");
                sSql.AppendLine("ORDER BY PR_NROCMP,PR_NROITEM");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,SGH_CODIGO);
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
        public static DataTable GetSegregacionFactura(SessionManager oSessionManager, int SGH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT SGD_ID,SGH_CODIGO,TB_SEGREACION_FACTURADT.FD_CODEMP PR_CODEMP,TB_SEGREACION_FACTURADT.FD_NROCMP PR_NROCMP,TB_SEGREACION_FACTURADT.FD_NROITEM PR_NROITEM,SGD_CANTIDAD,SGD_PRECIO,SGD_BODEGA,SGD_GRUPO,SGD_USUARIO,SGD_ESTADO,SGD_CANTIDADAPRO as SGD_CANTIDADAPRO");
                sSql.AppendLine("    , ARCLAVE1 PR_CLAVE1,ARNOMBRE,TANOMBRE,FD_NROFACTURA PR_NROFACPROFORMA,AA.ASNOMBRE ARDTTEC1,BB.ASNOMBRE ARDTTEC2,CC.ASNOMBRE ARDTTEC3,DD.ASNOMBRE ARDTTEC4,EE.ASNOMBRE ARDTTEC5,FF.ASNOMBRE ARDTTEC8,");
                sSql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2  AND BCLAVE3 = ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS, FD_ORIGEN PR_ORIGEN, FD_POSARA PR_POSARA, FD_PAGO PR_PAGO, FD_PRECIO PR_PRECIO,");
                sSql.AppendLine("FD_CANTIDAD PR_CANTIDAD, FD_REFPRO PR_REFPRO, FD_FECFAC PR_FECPROFORMA, FD_NROFACTURA PR_NROFACPROFORMA, SGD_CANTIDADAPRO,FD_TIPPRO,FD_CLAVE1,FD_CLAVE2,FD_CLAVE3,FD_CLAVE4,SGD_CANTIDADCMP,(SGD_CANTIDADCMP-(SGD_CANTIDAD + SGD_CANTIDADAPRO)) as DIFERENCIA  ");
                sSql.AppendLine("FROM TB_SEGREACION_FACTURADT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN CMP_FACTURADT WITH(NOLOCK) ON(TB_SEGREACION_FACTURADT.FD_CODEMP = CMP_FACTURADT.FD_CODEMP AND TB_SEGREACION_FACTURADT.FD_NROCMP = CMP_FACTURADT.FD_NROCMP");
                sSql.AppendLine("                         AND TB_SEGREACION_FACTURADT.FD_NROITEM = CMP_FACTURADT.FD_NROITEM)");
                sSql.AppendLine("INNER JOIN ARTICULO AR WITH(NOLOCK) ON(AR.ARCODEMP = CMP_FACTURADT.FD_CODEMP AND AR.ARTIPPRO = FD_TIPPRO AND AR.ARCLAVE1 = FD_CLAVE1)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(AR.ARCODEMP = TACODEMP AND AR.ARTIPPRO = TATIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = AR.ARCODEMP AND AA.ASTIPPRO = AR.ARTIPPRO AND AA.ASCLAVEO = AR.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = AR.ARCODEMP AND BB.ASTIPPRO = AR.ARTIPPRO AND BB.ASCLAVEO = AR.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = AR.ARCODEMP AND CC.ASTIPPRO = AR.ARTIPPRO AND CC.ASCLAVEO = AR.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = AR.ARCODEMP AND DD.ASTIPPRO = AR.ARTIPPRO AND DD.ASCLAVEO = AR.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = AR.ARCODEMP AND EE.ASTIPPRO = AR.ARTIPPRO AND EE.ASCLAVEO = AR.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = AR.ARCODEMP AND FF.ASTIPPRO = AR.ARTIPPRO AND FF.ASCLAVEO = AR.ARDTTEC7 AND FF.ASNIVELC = 10)");
                sSql.AppendLine("WHERE SGH_CODIGO = @p0");
                sSql.AppendLine("ORDER BY CMP_FACTURADT.FD_NROCMP,CMP_FACTURADT.FD_NROITEM");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SGH_CODIGO);
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
        public static DataTable GetSegregacionFacturaTraslados(SessionManager oSessionManager, int SGH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT SGD_ID,SGH_CODIGO,TB_SEGREACION_PROFORMADT.PR_CODEMP,TB_SEGREACION_PROFORMADT.PR_NROCMP,TB_SEGREACION_PROFORMADT.PR_NROITEM,SGD_CANTIDAD,SGD_PRECIO,SGD_BODEGA,SGD_GRUPO,SGD_USUARIO,SGD_ESTADO,SGD_CANTIDADAPRO-SGD_CANTIDAD as SGD_CANTIDADAPRO");
                sSql.AppendLine("SELECT FD_TIPPRO,FD_CLAVE1,FD_CLAVE2,FD_CLAVE3,FD_CLAVE4,SGD_CANTIDAD,ARNOMBRE,TANOMBRE");
                sSql.AppendLine("FROM TB_SEGREACION_FACTURADT WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN CMP_FACTURADT WITH(NOLOCK) ON(TB_SEGREACION_FACTURADT.FD_CODEMP = CMP_FACTURADT.FD_CODEMP AND TB_SEGREACION_FACTURADT.FD_NROCMP = CMP_FACTURADT.FD_NROCMP");
                sSql.AppendLine("                         AND TB_SEGREACION_FACTURADT.FD_NROITEM = CMP_FACTURADT.FD_NROITEM)");
                sSql.AppendLine("INNER JOIN ARTICULO AR WITH(NOLOCK) ON(AR.ARCODEMP = CMP_FACTURADT.FD_CODEMP AND AR.ARTIPPRO = FD_TIPPRO AND AR.ARCLAVE1 = FD_CLAVE1)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(AR.ARCODEMP = TACODEMP AND AR.ARTIPPRO = TATIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = AR.ARCODEMP AND AA.ASTIPPRO = AR.ARTIPPRO AND AA.ASCLAVEO = AR.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = AR.ARCODEMP AND BB.ASTIPPRO = AR.ARTIPPRO AND BB.ASCLAVEO = AR.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = AR.ARCODEMP AND CC.ASTIPPRO = AR.ARTIPPRO AND CC.ASCLAVEO = AR.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = AR.ARCODEMP AND DD.ASTIPPRO = AR.ARTIPPRO AND DD.ASCLAVEO = AR.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = AR.ARCODEMP AND EE.ASTIPPRO = AR.ARTIPPRO AND EE.ASCLAVEO = AR.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = AR.ARCODEMP AND FF.ASTIPPRO = AR.ARTIPPRO AND FF.ASCLAVEO = AR.ARDTTEC7 AND FF.ASNIVELC = 10)");
                sSql.AppendLine("WHERE SGH_CODIGO = @p0");
                sSql.AppendLine("ORDER BY CMP_FACTURADT.FD_NROCMP,CMP_FACTURADT.FD_NROITEM");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, SGH_CODIGO);
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
        public static DataTable GetSegregacionDT(SessionManager oSessionManager,int SG_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_SEGREGACION.*,ARNOMBRE,AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7,TANOMBRE,'    ' SGD_GRUPO");
                sql.AppendLine("  FROM TB_SEGREGACION WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = FD_CODEMP AND ARTICULO.ARTIPPRO = TB_SEGREGACION.ARTIPPRO AND ARTICULO.ARCLAVE1 = TB_SEGREGACION.ARCLAVE1 ");
                sql.AppendLine("                                 AND ARTICULO.ARCLAVE2 = TB_SEGREGACION.ARCLAVE2 AND ARTICULO.ARCLAVE3 = TB_SEGREGACION.ARCLAVE3 AND ARTICULO.ARCLAVE4 = TB_SEGREGACION.ARCLAVE4 )");
                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (ARCODEMP = TACODEMP AND ARTICULO.ARTIPPRO = TATIPPRO) ");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");
                sql.AppendLine("WHERE SG_CODIGO=@p0");
                sql.AppendLine("ORDER BY FD_NROCMP,FD_NROITEM,FD_NROFACTURA,BDBODEGA");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, SG_CODIGO);
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
        public static DataTable GetSegregacionxWR(SessionManager oSessionManager, int WOH_CONSECUTIVO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_SEGREGACION_WROUT.*,SGH_BODDIF TSOTBOD, A.BDNOMBRE BDBODEGA,B.BDNOMBRE OTBODEGA,0 WIH_CONSECUTIVO,'AAA' TSOTBODE, 'AAA' TSBODEGA");
                sql.AppendLine("FROM TB_SEGREGACION_WROUT WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN TB_SEGREACION_HD WITH(NOLOCK) ON (TB_SEGREACION_HD.SGH_CODIGO = TB_SEGREGACION_WROUT.SGH_CODIGO)");
                sql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (SGH_BODCAN = A.BDBODEGA AND A.BDCODEMP = HDCODEMP) ");
                sql.AppendLine("INNER JOIN TBBODEGA B WITH(NOLOCK) ON (SGH_BODDIF = B.BDBODEGA AND B.BDCODEMP = HDCODEMP) ");
                sql.AppendLine("WHERE WOH_CONSECUTIVO = @p0");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, WOH_CONSECUTIVO);
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
        public static DataTable GetSegregacionxTraslado(SessionManager oSessionManager, int TSNROTRA,string TSCODEMP)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_SEGREGACION_TRASLADO.*,SGH_BODDIF TSOTBOD, A.BDNOMBRE BDBODEGA,B.BDNOMBRE OTBODEGA,0 WIH_CONSECUTIVO,'AAA' TSOTBODE, 'AAA' TSBODEGA");
                sql.AppendLine("FROM TB_SEGREGACION_TRASLADO WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN TB_SEGREACION_HD WITH(NOLOCK) ON (TB_SEGREACION_HD.SGH_CODIGO = TB_SEGREGACION_TRASLADO.SGH_CODIGO)");
                sql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (SGH_BODCAN = A.BDBODEGA AND A.BDCODEMP = HDCODEMP) ");
                sql.AppendLine("INNER JOIN TBBODEGA B WITH(NOLOCK) ON (SGH_BODDIF = B.BDBODEGA AND B.BDCODEMP = HDCODEMP) ");
                sql.AppendLine("WHERE TSNROTRA = @p0 AND TSCODEMP =@p1");


                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, TSNROTRA, TSCODEMP);
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
        public static DataTable GetSegregacionProformasBodegasDT(SessionManager oSessionManager, string FD_CODEMP, int SGH_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT DISTINCT TB_SEGREACION_PROFORMADT.SGD_BODEGA,BDNOMBRE,SGD_GRUPO FROM TB_SEGREACION_PROFORMADT WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN TBBODEGA WITH(NOLOCK) ON (TB_SEGREACION_PROFORMADT.SGD_BODEGA = TBBODEGA.BDBODEGA)");
                sql.AppendLine("WHERE SGH_CODIGO=@p0");
                sql.AppendLine("ORDER BY SGD_GRUPO");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, SGH_CODIGO);
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
        public static DataTable GetSegregacionBodegasDT(SessionManager oSessionManager, string FD_CODEMP, int SG_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT DISTINCT TB_SEGREGACION.BDBODEGA,BDNOMBRE FROM TB_SEGREGACION WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN TBBODEGA WITH(NOLOCK) ON (TB_SEGREGACION.BDBODEGA = TBBODEGA.BDBODEGA)");
                sql.AppendLine("WHERE SG_CODIGO=@p0");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, SG_CODIGO);
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
        public static DataTable GetProformasProveedor(SessionManager oSessionManager, int CH_PROVEEDOR,string inFilter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT * FROM (");
                sql.AppendLine("SELECT DISTINCT PR_NROFACPROFORMA, PR_FECPROFORMA, ");
                sql.AppendLine("ISNULL((SELECT TOP 1 'S' FROM TB_SEGREACION_PROFORMADT WITH(NOLOCK) WHERE TB_SEGREACION_PROFORMADT.PR_NROCMP = CMP_PROFACTURADT.PR_NROCMP AND TB_SEGREACION_PROFORMADT.PR_NROITEM = CMP_PROFACTURADT.PR_NROITEM), 'N') CHK,");
                sql.AppendLine("ISNULL((SELECT TOP 1 ISNULL(SGH_PARCIAL, 'N') FROM TB_SEGREACION_PROFORMADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN TB_SEGREACION_HD WITH(NOLOCK) ON(TB_SEGREACION_HD.SGH_CODIGO = TB_SEGREACION_PROFORMADT.SGH_CODIGO)");
                sql.AppendLine("WHERE TB_SEGREACION_PROFORMADT.PR_NROCMP = CMP_PROFACTURADT.PR_NROCMP AND TB_SEGREACION_PROFORMADT.PR_NROITEM = CMP_PROFACTURADT.PR_NROITEM), 'N') CHK_SEG");
                sql.AppendLine("FROM CMP_PROFACTURADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN CMP_COMPRASHD WITH(NOLOCK) ON(CH_CODEMP = PR_CODEMP AND CH_NROCMP = PR_NROCMP)");
                sql.AppendLine("WHERE CH_PROVEEDOR = @p0) XX WHERE 1=1 AND "+ inFilter);
                sql.AppendLine("ORDER BY PR_FECPROFORMA DESC");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, CH_PROVEEDOR);
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
        public static DataTable GetFacturasProveedor(SessionManager oSessionManager, int CH_PROVEEDOR, string inFilter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT * FROM (");
                sql.AppendLine("SELECT DISTINCT FD_NROFACTURA PR_NROFACPROFORMA, FD_FECFAC PR_FECPROFORMA, ");
                sql.AppendLine("ISNULL((SELECT TOP 1 'S' FROM TB_SEGREACION_FACTURADT WITH(NOLOCK) WHERE TB_SEGREACION_FACTURADT.FD_NROCMP = CMP_FACTURADT.FD_NROCMP AND TB_SEGREACION_FACTURADT.FD_NROITEM = CMP_FACTURADT.FD_NROITEM), 'N') CHK,");
                sql.AppendLine("ISNULL((SELECT TOP 1 ISNULL(SGH_PARCIAL, 'N') FROM TB_SEGREACION_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN TB_SEGREACION_HD WITH(NOLOCK) ON(TB_SEGREACION_HD.SGH_CODIGO = TB_SEGREACION_FACTURADT.SGH_CODIGO)");
                sql.AppendLine("WHERE TB_SEGREACION_FACTURADT.FD_NROCMP = CMP_FACTURADT.FD_NROCMP AND TB_SEGREACION_FACTURADT.FD_NROITEM = CMP_FACTURADT.FD_NROITEM), 'N') CHK_SEG");
                sql.AppendLine("FROM CMP_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN CMP_COMPRASHD WITH(NOLOCK) ON(CH_CODEMP = FD_CODEMP AND CH_NROCMP = FD_NROCMP)");
                sql.AppendLine("WHERE CH_PROVEEDOR = @p0) XX WHERE 1=1 AND " + inFilter);
                sql.AppendLine("ORDER BY PR_FECPROFORMA DESC");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, CH_PROVEEDOR);
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
        public static DataTable GetDetalleSegregacionFacturaDT(SessionManager oSessionManager, int SGH_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_SEGREACION_FACTURADT.* ,TB_WRINDT.*, ARNOMBRE,TANOMBRE, SGH_BODDIF, A.BDNOMBRE BDBODEGA,B.BDNOMBRE OTBODEGA, TB_SEGREACION_HD.SGH_BODDIF TSOTBODE,TB_WRINHD.BDBODEGA TSBODEGA,");
                sql.AppendLine("ISNULL((SELECT TOP 1 YT.ARNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4) WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),AR.ARNOMBRE) ARNOMBRE,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC1 AND AX.ASNIVELC = 5)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),AA.ASNOMBRE) NOMTTEC1,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT with(nolock) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC2 AND AX.ASNIVELC = 6)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),BB.ASNOMBRE) NOMTTEC2,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC3 AND AX.ASNIVELC = 7)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),CC.ASNOMBRE) NOMTTEC3,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC4 AND AX.ASNIVELC = 8)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),DD.ASNOMBRE) NOMTTEC4,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC5 AND AX.ASNIVELC = 9)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),EE.ASNOMBRE) NOMTTEC5,");

                sql.AppendLine("ISNULL((SELECT TOP 1 AX.ASNOMBRE");
                sql.AppendLine("FROM TB_ORIGEN WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO YT WITH(NOLOCK) ON(TB_ORIGEN.ARCODEMP = yt.ARCODEMP AND TB_ORIGEN.ARTIPPRO = yt.ARTIPPRO AND TB_ORIGEN.ARCLAVE1 = yt.ARCLAVE1 AND TB_ORIGEN.ARCLAVE2 = yt.ARCLAVE2 AND TB_ORIGEN.ARCLAVE3 = yt.ARCLAVE3 AND TB_ORIGEN.ARCLAVE4 = yt.ARCLAVE4)");
                sql.AppendLine("INNER JOIN ARTICSEC AX WITH(NOLOCK) ON(AX.ASCODEMP = YT.ARCODEMP AND AX.ASTIPPRO = YT.ARTIPPRO AND AX.ASCLAVEO = YT.ARDTTEC7 AND AX.ASNIVELC = 10)");
                sql.AppendLine("WHERE TB_ORIGEN.OR_REFERENCIA = AR.ARCLAVE1),FF.ASNOMBRE) NOMTTEC7");

                sql.AppendLine("FROM TB_SEGREACION_FACTURADT WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN TB_SEGREACION_HD WITH(NOLOCK) ON (TB_SEGREACION_FACTURADT.SGH_CODIGO = TB_SEGREACION_HD.SGH_CODIGO)");
                sql.AppendLine("INNER JOIN CMP_FACTURADT WITH(NOLOCK) ON(TB_SEGREACION_FACTURADT.FD_NROCMP = CMP_FACTURADT.FD_NROCMP AND TB_SEGREACION_FACTURADT.FD_NROITEM = CMP_FACTURADT.FD_NROITEM)");
                sql.AppendLine("INNER JOIN TB_WRINDT WITH(NOLOCK) ON (CMP_FACTURADT.FD_NROCMP = TB_WRINDT.CD_NROCMP AND CMP_FACTURADT.FD_NROITEM = TB_WRINDT.CD_NROITEM)");
                sql.AppendLine("INNER JOIN TB_WRINHD WITH(NOLOCK) ON (TB_WRINHD.WIH_CONSECUTIVO = TB_WRINDT.WIH_CONSECUTIVO) ");
                sql.AppendLine("INNER JOIN ARTICULO AR WITH(NOLOCK) ON(AR.ARCODEMP = TB_WRINDT.ARCODEMP AND AR.ARTIPPRO = TB_WRINDT.ARTIPPRO AND AR.ARCLAVE1 = TB_WRINDT.ARCLAVE1 AND AR.ARCLAVE2 = TB_WRINDT.ARCLAVE2 AND AR.ARCLAVE3 = TB_WRINDT.ARCLAVE3 AND AR.ARCLAVE4 = TB_WRINDT.ARCLAVE4)");
                sql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(AR.ARCODEMP = TACODEMP AND AR.ARTIPPRO = TATIPPRO)");
                
                sql.AppendLine("INNER JOIN TBBODEGA A WITH(NOLOCK) ON (SGH_BODCAN = A.BDBODEGA AND A.BDCODEMP = HDCODEMP) ");
                sql.AppendLine("INNER JOIN TBBODEGA B WITH(NOLOCK) ON (SGH_BODDIF = B.BDBODEGA AND B.BDCODEMP = HDCODEMP) ");

                sql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = AR.ARCODEMP AND AA.ASTIPPRO = AR.ARTIPPRO AND AA.ASCLAVEO = AR.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = AR.ARCODEMP AND BB.ASTIPPRO = AR.ARTIPPRO AND BB.ASCLAVEO = AR.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = AR.ARCODEMP AND CC.ASTIPPRO = AR.ARTIPPRO AND CC.ASCLAVEO = AR.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = AR.ARCODEMP AND DD.ASTIPPRO = AR.ARTIPPRO AND DD.ASCLAVEO = AR.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = AR.ARCODEMP AND EE.ASTIPPRO = AR.ARTIPPRO AND EE.ASCLAVEO = AR.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = AR.ARCODEMP AND FF.ASTIPPRO = AR.ARTIPPRO AND FF.ASCLAVEO = AR.ARDTTEC7 AND FF.ASNIVELC = 10)");
                sql.AppendLine("WHERE TB_SEGREACION_HD.SGH_CODIGO =@p0 AND SGD_GRUPO ='gp_detalle'");
                sql.AppendLine(" ORDER BY TB_SEGREACION_FACTURADT.SGH_CODIGO");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, SGH_CODIGO);
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
        public static int InsertSegregacion(SessionManager oSessionManager,int SG_CODIGO, string FD_CODEMP, int FD_NROCMP, int FD_NROITEM, string FD_NROFACTURA,
                                            string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string BDBODEGA, double? SG_CANTIDAD, string SG_ESTADO, string SG_USUARIO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_SEGREGACION (SG_CODIGO,FD_CODEMP,FD_NROCMP,FD_NROITEM,FD_NROFACTURA,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BDBODEGA,SG_CANTIDAD,SG_ESTADO,SG_USUARIO,SG_FECING,SG_FECMOD) VALUES ");
                sql.AppendLine(" (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, SG_CODIGO, FD_CODEMP, FD_NROCMP, FD_NROITEM, FD_NROFACTURA, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, BDBODEGA, SG_CANTIDAD, SG_ESTADO, SG_USUARIO);
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

        public static int InsertSegregacionHD(SessionManager oSessionManager, int SGH_CODIGO, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string SGH_NROFAC, DateTime? SG_FECPROFORMA, string SG_NROPROFORMA, int? SG_VENDEDORPRO, int? SG_COMPRADORPRO, string SGH_TIPO, int SGH_PROVEEDOR, string SGH_OBSERVACIONES, string SGH_BODCAN, string SGH_BODDIF, string SGH_PARCIAL, string SGH_ESTADO, string SGH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SEGREACION_HD (SGH_CODIGO,HDCODEMP, HDTIPFAC, HDNROFAC, SGH_NROFAC, SG_FECPROFORMA, SG_NROPROFORMA, SG_VENDEDORPRO, SG_COMPRADORPRO,SGH_TIPO,SGH_PROVEEDOR, SGH_OBSERVACIONES, SGH_BODCAN, SGH_BODDIF, SGH_PARCIAL, SGH_ESTADO, SGH_USUARIO, SGH_FECING,SGH_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(),CommandType.Text, SGH_CODIGO, HDCODEMP, HDTIPFAC, HDNROFAC, SGH_NROFAC, SG_FECPROFORMA, SG_NROPROFORMA, SG_VENDEDORPRO, SG_COMPRADORPRO, SGH_TIPO, SGH_PROVEEDOR, SGH_OBSERVACIONES, SGH_BODCAN, SGH_BODDIF, SGH_PARCIAL, SGH_ESTADO, SGH_USUARIO);
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
        public static int UpdateSegregacionHD(SessionManager oSessionManager, int SGH_CODIGO, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string SGH_NROFAC, DateTime? SG_FECPROFORMA, 
            string SG_NROPROFORMA, int? SG_VENDEDORPRO, int? SG_COMPRADORPRO, string SGH_ESTADO, string SGH_OBSERVACIONES, string SGH_PARCIAL, string SGH_USUARIO, string SGH_BODCAN, string SGH_BODDIF)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_SEGREACION_HD SET HDCODEMP=@p1, HDTIPFAC=@p2, HDNROFAC=@p3, SGH_NROFAC=@p4, SG_FECPROFORMA=@p5, SG_NROPROFORMA=@p6, SG_VENDEDORPRO=@p7, SG_COMPRADORPRO=@p8,SGH_ESTADO=@p9, SGH_USUARIO=@p10,SGH_OBSERVACIONES=@p11, SGH_PARCIAL=@p12,SGH_BODCAN=@p13, SGH_BODDIF=@p14 ,SGH_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE SGH_CODIGO=@p0");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SGH_CODIGO, HDCODEMP, HDTIPFAC, HDNROFAC, SGH_NROFAC, SG_FECPROFORMA, SG_NROPROFORMA, SG_VENDEDORPRO, SG_COMPRADORPRO, SGH_ESTADO, SGH_USUARIO, SGH_OBSERVACIONES,SGH_PARCIAL, SGH_BODCAN, SGH_BODDIF);
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
        public static int InsertSegregacionProforma(SessionManager oSessionManager, int SGH_CODIGO, string PR_CODEMP, int PR_NROCMP, int PR_NROITEM, double SGD_CANTIDAD, double SGD_CANTIDADAPRO, double SGD_CANTIDADCMP, double SGD_PRECIO, string SGD_BODEGA,string SGD_GRUPO, string SGD_USUARIO, string SGD_ESTADO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_SEGREACION_PROFORMADT (SGH_CODIGO,PR_CODEMP, PR_NROCMP, PR_NROITEM, SGD_CANTIDAD,SGD_CANTIDADAPRO, SGD_CANTIDADCMP,SGD_PRECIO,SGD_BODEGA, SGD_GRUPO, SGD_USUARIO, SGD_ESTADO) VALUES ");
                sql.AppendLine(" (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, SGH_CODIGO, PR_CODEMP, PR_NROCMP, PR_NROITEM, SGD_CANTIDAD, SGD_CANTIDADAPRO, SGD_CANTIDADCMP, SGD_PRECIO, SGD_BODEGA, SGD_GRUPO, SGD_USUARIO, SGD_ESTADO);
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
        public static int DeleteSegregacionFactura(SessionManager oSessionManager, int SGH_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("DELETE FROM TB_SEGREACION_FACTURADT WHERE SGH_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, SGH_CODIGO);
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
        public static int InsertSegregacionFactura(SessionManager oSessionManager, int SGH_CODIGO, string PR_CODEMP, int PR_NROCMP, int PR_NROITEM, double SGD_CANTIDAD, double SGD_CANTIDADAPRO, double SGD_CANTIDADCMP, double SGD_PRECIO, string SGD_BODEGA, string SGD_GRUPO, string SGD_USUARIO, string SGD_ESTADO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_SEGREACION_FACTURADT (SGH_CODIGO,FD_CODEMP, FD_NROCMP, FD_NROITEM, SGD_CANTIDAD,SGD_CANTIDADAPRO, SGD_CANTIDADCMP,SGD_PRECIO,SGD_BODEGA, SGD_GRUPO, SGD_USUARIO, SGD_ESTADO) VALUES ");
                sql.AppendLine(" (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, SGH_CODIGO, PR_CODEMP, PR_NROCMP, PR_NROITEM, SGD_CANTIDAD, SGD_CANTIDADAPRO, SGD_CANTIDADCMP, SGD_PRECIO, SGD_BODEGA, SGD_GRUPO, SGD_USUARIO, SGD_ESTADO);
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
        public static int UpdateSegregacionProforma(SessionManager oSessionManager,double SGD_CANTIDAD, double SGD_CANTIDADAPRO, int SGD_ID)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("UPDATE TB_SEGREACION_PROFORMADT SET SGD_CANTIDAD=@p0,SGD_CANTIDADAPRO=@p1");
                sql.AppendLine(" WHERE SGD_ID=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, SGD_CANTIDAD, SGD_CANTIDADAPRO, SGD_ID);
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
        public static int InsertSegregacionxWROUT(SessionManager oSessionManager, int WOH_CONSECUTIVO, int SGH_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_SEGREGACION_WROUT (WOH_CONSECUTIVO, SGH_CODIGO) VALUES ");
                sql.AppendLine(" (@p0,@p1)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, WOH_CONSECUTIVO, SGH_CODIGO);
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

        public static int InsertSegregacionxTraslado(SessionManager oSessionManager, string TSCODEMP,int TSNROTRA, int SGH_CODIGO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("INSERT INTO TB_SEGREGACION_TRASLADO (TSCODEMP,TSNROTRA, SGH_CODIGO) VALUES ");
                sql.AppendLine(" (@p0,@p1,@p2)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, TSCODEMP,TSNROTRA,SGH_CODIGO);
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
