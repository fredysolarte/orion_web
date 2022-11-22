using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Consultas
{
	public class ConsultasBD
    {
        //General
        #region
        public DataSet GetInvVsVtas(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows, int servicio)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TATIPPRO,TANOMBRE,BBCLAVE1,SUM(BBCANTID),MIN(BBFECENT) FEC_MIN, DATEDIFF(day,MIN(BBFECENT), GETDATE()) NRO_DIAS, ");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'CM' THEN X END),0) VTA_CM,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'CM' THEN BBCANTID END),0) SAL_CM,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'E1' THEN X END),0) VTA_E1,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'E1' THEN BBCANTID END),0) SAL_E1,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'E2' THEN X END),0) VTA_E2,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'E2' THEN BBCANTID END),0) SAL_E2,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'GL' THEN X END),0) VTA_GL,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'GL' THEN BBCANTID END),0) SAL_GL,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'GR' THEN X END),0) VTA_GR,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'GR' THEN BBCANTID END),0) SAL_GR,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'SF' THEN X END),0) VTA_SF,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'SF' THEN BBCANTID END),0) SAL_SF,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'SP' THEN X END),0) VTA_SP,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'SP' THEN BBCANTID END),0) SAL_SP,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'U1' THEN X END),0) VTA_U1,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'U1' THEN BBCANTID END),0) SAL_U1,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'U2' THEN X END),0) VTA_U2,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = 'U2' THEN BBCANTID END),0) SAL_U2,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = '13' THEN X END),0) VTA_13,");
				sSql.AppendLine("   ISNULL(SUM(CASE WHEN BBBODEGA = '13' THEN BBCANTID END),0) SAL_13,");
				sSql.AppendLine("   SUM(X) VTA_TOT ,SUM(BBCANTID) SAL_TOT,PRPRECIO, DSVALPOR ");
				sSql.AppendLine("  FROM BALANBOD , TBTIPPRO, TBTIPFAC, ");
				sSql.AppendLine("(SELECT FDCODEMP,FDTIPPRO,FDCLAVE1,FDCLAVE2,FDCLAVE3,FDCLAVE4,SUM(FDCANTID) X,FDTIPFAC");
                sSql.AppendLine("               FROM FACVENHD, FACVENDT");
                sSql.AppendLine("              WHERE FHNROFAC = FDNROFAC");                          
                sSql.AppendLine("                AND FHTIPFAC = FDTIPFAC");   
                sSql.AppendLine("                AND FHCODEMP = FDCODEMP");
				sSql.AppendLine("              GROUP BY FDCLAVE1,FDCLAVE2,FDCLAVE3,FDCLAVE4,FDCODEMP,FDTIPPRO,FDTIPFAC ) FACTURACION");
				sSql.AppendLine("INNER JOIN PRECIODT ON( FDCODEMP = PRCODEMP");
                sSql.AppendLine("           AND FDTIPPRO = PRTIPPRO");
                sSql.AppendLine("           AND FDCLAVE1 = PRCLAVE1");
				sSql.AppendLine("           AND PRLISPRE =	'LBASE07' ");
                sSql.AppendLine("           AND PRESTADO = 'AC' )");
				sSql.AppendLine("LEFT OUTER JOIN DESCUEDT ON( FDCODEMP = DSCODEMP");
                sSql.AppendLine("            AND FDTIPPRO = DSTIPPRO");
				sSql.AppendLine("            AND FDCLAVE1 = DSCLAVE1");
				sSql.AppendLine("            AND DSLISDES = '06') ");
				sSql.AppendLine(" WHERE BBCODEMP = TACODEMP");
				sSql.AppendLine("   AND BBTIPPRO = TATIPPRO");
				sSql.AppendLine("   AND BBTIPPRO = FDTIPPRO");
				sSql.AppendLine("   AND BBCLAVE1 = FDCLAVE1");
				sSql.AppendLine("   AND BBCLAVE2 = FDCLAVE2");
				sSql.AppendLine("   AND BBCLAVE3 = FDCLAVE3");
				sSql.AppendLine("   AND BBCLAVE4 = FDCLAVE4");
				sSql.AppendLine("	AND TFTIPFAC = FDTIPFAC");
				sSql.AppendLine("	AND TFBODEGA = BBBODEGA");
				sSql.AppendLine("   AND BBCANTID <> 0");
				sSql.AppendLine("   AND BBCODEMP = '001'   ");
				//sSql.AppendLine("   AND BBBODEGA IN ('13')");
				//sSql.AppendLine("   AND 1=0");


				if (!string.IsNullOrWhiteSpace(filter))
				{
					sSql.AppendLine("AND " + filter);
				}

				sSql.AppendLine("  GROUP BY TATIPPRO,TANOMBRE,BBCLAVE1,PRPRECIO, DSVALPOR  ");
                return DBAccess.GetDataSet(oSessionManager, sSql.ToString(), CommandType.Text);
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
		public static DataTable GetTipPro(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("  SELECT TATIPPRO, TANOMBRE FROM  TBTIPPRO ");
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
		public static DataTable GetBodegas(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT BDBODEGA, BDNOMBRE FROM TBBODEGA WHERE BDALMACE = 'S' ");
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
		public static DataTable GetArticulos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARNOMBRE FROM ARTICULO ");
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
		public static DataTable GetFoto(SessionManager oSessionManager, string filter)
		{
			StringBuilder sSql = new StringBuilder();			
			try
			{
				sSql.AppendLine("SELECT TOP 1 ARTIPPRO ,ARCLAVE1 ,IM_IMAGEN");
				sSql.AppendLine("  FROM ARTICULO ");
				sSql.AppendLine(" LEFT OUTER JOIN IMAGENES ON(ARCODEMP = IM_CODEMP AND ARTIPPRO = IM_TIPPRO AND ARCLAVE1 = IM_CLAVE1 AND IM_TIPIMA = 4)");
				//sSql.AppendLine(" WHERE ARCODEMP ='001' ");
				//sSql.AppendLine("   AND ARTIPPRO =@p0 AND ARCLAVE1=@p1 ");
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
		public static DataTable GetVentas(SessionManager oSessionManager,string filter)
		{
		
			StringBuilder sql = new StringBuilder();		
			try
			{
				sql.AppendLine("SELECT FHESTADO, (FHTIPFAC +' -' + CAST(FHNROFAC AS VARCHAR)) FACTURA, FHCODCLI, FHSUBTOT, FHTOTIVA,FHTOTFAC,BDNOMBRE  ");
                sql.AppendLine("  FROM FACVENHD, TBBODEGA, TBTIPFAC"); 
                sql.AppendLine(" WHERE FHCODEMP = BDCODEMP");
                sql.AppendLine("   AND FHCODEMP = TFCODEMP");
                sql.AppendLine("   AND TFTIPFAC = FHTIPFAC");
                sql.AppendLine("   AND TFBODEGA = BDBODEGA");
                sql.AppendLine("   AND FHCODEMP ='001'" + filter);

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text);                
                
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
		public static DataTable GetPagos(SessionManager oSessionManager, string filter)
		{
			StringBuilder sql = new StringBuilder();			
			try
			{
				sql.AppendLine("SELECT ISNULL(A.TTDESCRI,'.') PAGO,B.TTDESCRI TARJETA,SUM(PFVLRPAG) VALOR");
                sql.AppendLine("   FROM PAGOSFAC");
                sql.AppendLine("     LEFT OUTER JOIN TBTABLAS A ON(A.TTCODEMP = PFCODEMP AND A.TTCODCLA = PFTIPPAG AND A.TTCODTAB = 'PAGO' )");
                sql.AppendLine("     LEFT OUTER JOIN TBTABLAS B ON(B.TTCODEMP = PFCODEMP AND B.TTCODCLA = PFDETTPG AND B.TTCODTAB = A.TTVALORC  ),");
                sql.AppendLine("     TBBODEGA, TBTIPFAC");
                sql.AppendLine("  WHERE PFCODEMP = BDCODEMP");
                sql.AppendLine("    AND PFCODEMP = TFCODEMP");
                sql.AppendLine("    AND TFTIPFAC = PFTIPFAC");
                sql.AppendLine("    AND TFBODEGA = BDBODEGA");
                //sql.AppendLine("    AND PFCODEMP = '+ QuotedStr(CODEMP) +
                //sql.AppendLine("    AND BDBODEGA = '+ QuotedStr(lc_bodega) +
                sql.AppendLine("    AND PFNROFAC IN(SELECT FHNROFAC");
                sql.AppendLine("            FROM FACVENHD");
                sql.AppendLine("             WHERE FHCODEMP = PFCODEMP");
                sql.AppendLine("               AND FHTIPFAC = PFTIPFAC");
                //sql.AppendLine("               AND FHFECFAC BETWEEN '+ QuotedStr(FecIni) +' AND '+ QuotedStr(FecFin) +
                sql.AppendLine("               AND FHESTADO <> 'AN'");
                sql.AppendLine("               )");
                sql.AppendLine("  GROUP BY A.TTDESCRI,B.TTDESCRI");
                sql.AppendLine("  UNION ALL");
                sql.AppendLine("  SELECT 'DEVOLUCION','', SUM(FHTOTFAC)");
                sql.AppendLine("    FROM FACVENHD, TBBODEGA, TBTIPFAC");
                //sql.AppendLine("   WHERE FHFECFAC BETWEEN '+ QuotedStr(FecIni) +' AND '+ QuotedStr(FecFin) +
                sql.AppendLine("     AND TFCODEMP = FHCODEMP");
                sql.AppendLine("     AND TFCODEMP = BDCODEMP");
                sql.AppendLine("     AND TFTIPFAC = FHTIPFAC");
                sql.AppendLine("     AND TFBODEGA = BDBODEGA");
                //sql.AppendLine("     AND BDBODEGA = '+ QuotedStr(lc_bodega) +
                sql.AppendLine("     AND FHTOTFAC < 0");
                sql.AppendLine("     AND FHESTADO <> 'AN'");
                sql.AppendLine("   UNION ALL");
                sql.AppendLine("   SELECT ISNULL(A.TTDESCRI,'.') PAGO,B.TTDESCRI TARJETA,SUM(PCVLRPAG)");
                sql.AppendLine("     FROM PAGOSCON");
                sql.AppendLine("     LEFT OUTER JOIN TBTABLAS A ON(A.TTCODEMP = PCCODEMP AND A.TTCODCLA = PCTIPPAG AND A.TTCODTAB = 'PAGO' )"); 
                sql.AppendLine("     LEFT OUTER JOIN TBTABLAS B ON(B.TTCODEMP = PCCODEMP AND B.TTCODCLA = PCDETTPG AND B.TTCODTAB = A.TTVALORC  ),");
                sql.AppendLine("      PTOVTAUSU");
                sql.AppendLine("    WHERE PCTIPFAC = PUTIPREC");
                sql.AppendLine("      AND PCCODEMP = PUCODEMP");
                //sql.AppendLine("      AND PUCDPTVT = '+ QuotedStr(lc_bodega) +
                //sql.AppendLine("      AND PUCODUSU = '+ QuotedStr(CDUSER) +
                //sql.AppendLine("      AND Cast(CONVERT(VARCHAR(19),PCFECING,112) AS DATETIME) BETWEEN '+ QuotedStr(FecIni) +' AND '+ QuotedStr(FecFin) +
				sql.AppendLine("      GROUP BY A.TTDESCRI,B.TTDESCRI"); 
				sql.AppendLine("   AND FHCODEMP ='001'" + filter);

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
        #endregion
        //Consulta de Inventarios
        #region
        public static DataTable GetConsulatInventario(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT BBCODEMP,BBBODEGA,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4,BBCODCAL,ISNULL(BBCANTID-BBCANTRN,0) BBCANTID,BBBODBOD,BBBODPED,BBBODPRO,BBCANCOM,BBCOMPED,BBCOMPRO,BBCANPRO,BBPROPED,BBCANPED,");
                sSql.AppendLine("       BBPEDBOD,BBCANTRN,BBLOCALI,BBFECENT,BBFECSAL,BBNMUSER,BBFECING,BBFECMOD,BBCANCTL,BBCANREC, ");
                sSql.AppendLine("AR.ARUNDINV, AR.ARNOMBRE, BDNOMBRE,AR.ARCLAVE1,AR.ARCLAVE2,AR.ARCLAVE3,AR.ARCLAVE4,AR.ARTIPPRO,AA.ASNOMBRE ARDTTEC1,BB.ASNOMBRE ARDTTEC2,CC.ASNOMBRE ARDTTEC3,DD.ASNOMBRE ARDTTEC4,EE.ASNOMBRE ARDTTEC5,FF.ASNOMBRE ARDTTEC8, ");
                sSql.AppendLine("       A.TTVALORC SUBCATEGORIA,B.TTVALORC FONDO, C.TTVALORC TEJIDO, D.TTVALORC PROCEDENCIA, E.TTVALORC UEN, F.TTVALORC TPRODUC,");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = AR.ARCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = AR.ARTIPPRO AND ASCLAVEO = AR.ARCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE AR.ARCLAVE2 END CLAVE2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = AR.ARCODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = AR.ARTIPPRO AND ASCLAVEO = AR.ARCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE AR.ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BTIPPRO = BBTIPPRO AND BCLAVE1 = BBCLAVE1 AND BCLAVE2 = BBCLAVE2  ");
                sSql.AppendLine("     AND BCLAVE3 = BBCLAVE3 AND BCLAVE4 = BBCLAVE4) BARRAS,");
                sSql.AppendLine(" dbo.FGET_PRECIO(AR.ARCODEMP,AR.ARTIPPRO,AR.ARCLAVE1,AR.ARCLAVE2,AR.ARCLAVE3,AR.ARCLAVE4,BBBODEGA,CASE WHEN BDCENCOS='-1' THEN NULL ELSE BDCENCOS END) PRECIO, ");
                sSql.AppendLine("  dbo.FGET_DESCUENTOART(AR.ARCODEMP,AR.ARTIPPRO,AR.ARCLAVE1,AR.ARCLAVE2,AR.ARCLAVE3,AR.ARCLAVE4,BBBODEGA) DESCUENTO, TANOMBRE ");
                sSql.AppendLine(",DBO.GET_ORIGEN(AR.ARCODEMP,AR.ARTIPPRO,AR.ARCLAVE1,AR.ARCLAVE2,AR.ARCLAVE3,AR.ARCLAVE4) ORIGEN, TT_CLAVE1 REF_TESTER,TE.ARNOMBRE NOM_TESTER,TE.ARDTTEC1 TAM_TESTER,");
                sSql.AppendLine("(SELECT Z.BBCANTID FROM BALANBOD Z WITH(NOLOCK) WHERE Z.BBCODEMP = TE.ARCODEMP AND Z.BBTIPPRO = TE.ARTIPPRO AND Z.BBCLAVE1 = TE.ARCLAVE1 AND Z.BBCLAVE2 = TE.ARCLAVE2 AND Z.BBCLAVE3 = TE.ARCLAVE3 AND Z.BBCLAVE4 = TE.ARCLAVE4 AND Z.BBBODEGA = BALANBOD.BBBODEGA) CAN_INV_TESTER");
                sSql.AppendLine("FROM BALANBOD WITH(NOLOCK)");
                sSql.AppendLine(" INNER JOIN ARTICULO AR WITH(NOLOCK) ON (AR.ARCODEMP = BBCODEMP AND AR.ARTIPPRO = BBTIPPRO AND AR.ARCLAVE1 = BBCLAVE1 AND AR.ARCLAVE2 = BBCLAVE2 AND AR.ARCLAVE3 = BBCLAVE3 AND AR.ARCLAVE4 = BBCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBBODEGA WITH(NOLOCK) ON (BDCODEMP = BBCODEMP AND BDBODEGA = BBBODEGA)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = AR.ARCODEMP AND TATIPPRO = AR.ARTIPPRO)");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS A WITH(NOLOCK) ON (A.TTCODEMP = AR.ARCODEMP AND A.TTCODCLA = AR.TR_SCT AND A.TTCODTAB ='SUBCATG')");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS B WITH(NOLOCK) ON (B.TTCODEMP = AR.ARCODEMP AND B.TTCODCLA = AR.TR_FONDO AND B.TTCODTAB ='FONDO')");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS C WITH(NOLOCK) ON (C.TTCODEMP = AR.ARCODEMP AND C.TTCODCLA = AR.TR_TEJIDO AND C.TTCODTAB ='TEJIDO')");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS D WITH(NOLOCK) ON (D.TTCODEMP = AR.ARCODEMP AND D.TTCODCLA = AR.TR_PROCEDENCIA AND D.TTCODTAB ='PROCED')");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS E WITH(NOLOCK) ON (E.TTCODEMP = AR.ARCODEMP AND E.TTCODCLA = AR.TR_PROCEDENCIA AND E.TTCODTAB ='UEN')");
                sSql.AppendLine("   LEFT OUTER JOIN TBTABLAS F WITH(NOLOCK) ON (F.TTCODEMP = AR.ARCODEMP AND F.TTCODCLA = AR.TR_PROCEDENCIA AND F.TTCODTAB ='TPRODUC')");
                sSql.AppendLine("   LEFT OUTER JOIN TB_TESTER WITH(NOLOCK) ON(TB_TESTER.ARTIPPRO = AR.ARTIPPRO AND TB_TESTER.ARCLAVE1 = AR.ARCLAVE1)");
                sSql.AppendLine("   LEFT OUTER JOIN ARTICULO TE WITH(NOLOCK) ON(TB_TESTER.TT_TIPPRO = TE.ARTIPPRO AND TB_TESTER.TT_CLAVE1 = TE.ARCLAVE1)");

                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = AR.ARCODEMP AND AA.ASTIPPRO = AR.ARTIPPRO AND AA.ASCLAVEO = AR.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = AR.ARCODEMP AND BB.ASTIPPRO = AR.ARTIPPRO AND BB.ASCLAVEO = AR.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = AR.ARCODEMP AND CC.ASTIPPRO = AR.ARTIPPRO AND CC.ASCLAVEO = AR.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = AR.ARCODEMP AND DD.ASTIPPRO = AR.ARTIPPRO AND DD.ASCLAVEO = AR.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = AR.ARCODEMP AND EE.ASTIPPRO = AR.ARTIPPRO AND EE.ASCLAVEO = AR.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = AR.ARCODEMP AND FF.ASTIPPRO = AR.ARTIPPRO AND FF.ASCLAVEO = AR.ARDTTEC7 AND FF.ASNIVELC = 10)");

                //sSql.AppendLine("   LEFT OUTER JOIN TBBARRA ON (BCODEMP = BBCODEMP AND BTIPPRO = BBTIPPRO AND BCLAVE1 = BBCLAVE1 AND BCLAVE2 = BBCLAVE2 AND BCLAVE3 = BBCLAVE3 AND BCLAVE4 = BBCLAVE4) ");
                sSql.AppendLine(" WHERE 1=1 "+filter);

                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static DataTable GetConsulatInventarioLote(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT BLCODEMP,BLTIPPRO,BLCLAVE1,BLCLAVE2,BLCLAVE3,BLCLAVE4,BLCDLOTE,BLCANTID,BLDTTEC1,BLDTTEC2,(BLCODEMP + BLTIPPRO + BLCLAVE1 + BLCLAVE2 + BLCLAVE3 + BLCLAVE4 + BLCDLOTE) LLAVE ");
                sSql.AppendLine("FROM BALANLOT WITH(NOLOCK) ");
                sSql.AppendLine("WHERE BLCANTID > 0 AND BLCODEMP=@p0 AND BLBODEGA=@p1 AND BLTIPPRO=@p2 AND BLCLAVE1 =@p3 AND BLCLAVE2=@p4 AND BLCLAVE3=@p5 AND BLCLAVE4=@p6");

                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,BLCODEMP,BLBODEGA,BLTIPPRO,BLCLAVE1,BLCLAVE2,BLCLAVE3,BLCLAVE4);
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
        public static DataTable GetConsulatInventarioElemento(SessionManager oSessionManager, string inLlave)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT BECDELEM,BECANTID ");
                sSql.AppendLine("FROM BALANELE WITH(NOLOCK)");
                sSql.AppendLine("WHERE BECANTID > 0 AND (BECODEMP + BETIPPRO + BECLAVE1 + BECLAVE2 + BECLAVE3 + BECLAVE4 + BECDLOTE) = @p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inLlave);
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
        public static DataTable GetConsultaMovimientos(SessionManager oSessionManager, string lc_sql, DateTime inFecIni, DateTime inFecFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT A.BDNOMBRE, MBUNDMOV,MBFECMOV,TMNOMBRE,MBTIPPRO,MBCLAVE1,MBCLAVE2,MBCLAVE3,MBESTADO, B.BDNOMBRE AS DESTINO,TMNOMBRE DESCRIPCION,");
                sSql.AppendLine("CASE ");
                sSql.AppendLine("WHEN MBCDTRAN IN ('14','15','31') THEN (SELECT TOP 1 CAST(DTTIPFAC AS VARCHAR)+'-'+CAST(DTNROFAC AS VARCHAR) FROM FACTURADT WHERE DTNROMOV = MIIDMOVI)");
		        sSql.AppendLine("WHEN MBCDTRAN ='16' THEN CAST ((SELECT TOP 1 RH_NROCMP FROM CMP_RECIBODT ");
				sSql.AppendLine("					INNER JOIN CMP_RECIBOHD ON (RH_CODEMP = RD_CODEMP AND RH_NRORECIBO = RD_NRORECIBO)");
				sSql.AppendLine("					WHERE RD_IDMOVI = MIIDMOVI) AS VARCHAR)");
		        sSql.AppendLine("WHEN MBCDTRAN ='30' THEN CAST ((SELECT TOP 1 LH_LSTPAQ FROM TB_EMPAQUEDT WHERE TB_EMPAQUEDT.LD_NRMOV = MIIDMOVI) AS VARCHAR)");
		        sSql.AppendLine("WHEN MBCDTRAN ='98' THEN CAST ((SELECT TSNROTRA FROM TRASLADO WHERE TSCODEMP = MICODEMP AND TSMOVENT = MIIDMOVI) AS VARCHAR)");
		        sSql.AppendLine("WHEN MBCDTRAN ='99' THEN CAST ((SELECT TSNROTRA FROM TRASLADO WHERE TSCODEMP = MICODEMP AND TSMOVSAL = MIIDMOVI) AS VARCHAR)");
		        sSql.AppendLine("ELSE CAST (MIIDMOVI AS VARCHAR)");
                sSql.AppendLine("END AS DOCUMENTO , MBNMUSER,  ");
                sSql.AppendLine(" CASE WHEN TMENTSAL = 'E' THEN MBCANMOV ");
                sSql.AppendLine("			WHEN TMENTSAL = 'S' THEN MBCANMOV*-1 END MBCANMOV,ARNOMBRE, ");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = MBCODEMP AND BTIPPRO = MBTIPPRO AND BCLAVE1=MBCLAVE1 AND BCLAVE2 = MBCLAVE2 AND BCLAVE3 = MBCLAVE3 AND BCLAVE4 = MBCLAVE4) BARRAS");
                sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK)");
                sSql.AppendLine(" INNER JOIN TBBODEGA A WITH(NOLOCK) ON (MBCODEMP = A.BDCODEMP AND MBBODEGA = A.BDBODEGA)");
                sSql.AppendLine(" INNER JOIN MOVIMINV WITH(NOLOCK) ON (MBIDMOVI = MIIDMOVI AND MBCODEMP = MICODEMP)");
                sSql.AppendLine("LEFT OUTER JOIN TBBODEGA B WITH(NOLOCK)  ON(B.BDBODEGA = MIOTBODE) ");
                sSql.AppendLine("LEFT OUTER JOIN TBTIPMOV ON (MICODEMP = TMCODEMP AND MICDTRAN = TMCDTRAN)");
                sSql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = MBCODEMP AND ARTIPPRO = MBTIPPRO AND ARCLAVE1 = MBCLAVE1 AND ARCLAVE2 = MBCLAVE2 AND ARCLAVE3 = MBCLAVE3 AND ARCLAVE4 = MBCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("WHERE MBCDTRAN = TMCDTRAN "+lc_sql);
                sSql.AppendLine("AND CONVERT(DATE,MBFECMOV,101) BETWEEN CONVERT(DATE,@p0,101) AND CONVERT(DATE,@p1,101) ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inFecIni, inFecFin);
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
        public static DataTable GetTrazaInventarios(SessionManager oSessionManager, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD)
        {

            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT FBNROFOT,FBBODEGA,FBCLAVE1,FBCLAVE2,FBCLAVE3,FBCLAVE4,MBCDTRAN,DESCRIPCION,TIPO,");
                sql.AppendLine("CASE WHEN TIPO ='S' THEN FBCANTID*-1 ELSE FBCANTID END FBCANTID,");
                sql.AppendLine("ESTADO,FBFECING,DOCUEMNTO_CON");
                sql.AppendLine("FROM (");
                sql.AppendLine("SELECT FBNROFOT,FBBODEGA,FBTIPPRO,FBCLAVE1,FBCLAVE2,FBCLAVE3,FBCLAVE4,0 MBCDTRAN,'INV INICIAL' DESCRIPCION,'E' TIPO,FBCANTID,'CE' ESTADO,FBFECING,CAST(FBNROFOT AS VARCHAR) DOCUMENTO,CAST(FBNROFOT AS VARCHAR) DOCUEMNTO_CON");
                sql.AppendLine("FROM FOTOIBOD WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = FBCODEMP AND ARTIPPRO = FBTIPPRO AND ARCLAVE1 = FBCLAVE1 AND ARCLAVE2 = FBCLAVE2 AND ARCLAVE3 = FBCLAVE3 AND ARCLAVE4 = FBCLAVE4)");
                sql.AppendLine("WHERE FBNROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK) INNER JOIN FOTOIBOD WITH(NOLOCK) ON (FICODEMP = FBCODEMP AND FINROFOT = FBNROFOT) WHERE FIINVINI = 'S'   AND FIBODEGA =@p6 AND FBTIPPRO=ARTIPPRO )");
                sql.AppendLine("AND FBCANTID > 0");
                sql.AppendLine("AND FBCODEMP = @p0");
                sql.AppendLine("AND FBTIPPRO = @p1");
                sql.AppendLine("AND FBCLAVE1 = @p2");
                sql.AppendLine("AND FBCLAVE2 = @p3");
                sql.AppendLine("AND FBCLAVE3 = @p4");
                sql.AppendLine("AND FBCLAVE4 = @p5");
                sql.AppendLine("AND FBBODEGA = @p6");
                sql.AppendLine("UNION ALL");
                sql.AppendLine("SELECT MIIDMOVI,MBBODEGA,MBTIPPRO,MBCLAVE1,MBCLAVE2,MBCLAVE3,MBCLAVE4,MBCDTRAN,TMNOMBRE,TMENTSAL,MBCANTID,MBESTADO,MBFECING,MICDDOCU,");
                sql.AppendLine("CASE");
                sql.AppendLine("WHEN MBCDTRAN IN ('14','15','31') THEN (SELECT TOP 1 CAST(DTTIPFAC AS VARCHAR)+'-'+CAST(DTNROFAC AS VARCHAR) FROM FACTURADT WHERE DTNROMOV = MIIDMOVI)");
                sql.AppendLine("WHEN MBCDTRAN ='16' THEN CAST ((SELECT TOP 1 CMP_RECIBOHD.RH_NRORECIBO FROM CMP_RECIBODT ");
                sql.AppendLine("					INNER JOIN CMP_RECIBOHD ON (RH_CODEMP = RD_CODEMP AND RH_NRORECIBO = RD_NRORECIBO)");
                sql.AppendLine("					WHERE RD_IDMOVI = MIIDMOVI) AS VARCHAR)");
                sql.AppendLine("WHEN MBCDTRAN ='25' THEN CAST ((SELECT TOP 1 WIH_CONSECUTIVO FROM TB_WRINDT WITH(NOLOCK) WHERE TB_WRINDT.MBIDMOVI = MIIDMOVI) AS VARCHAR)");
                sql.AppendLine("WHEN MBCDTRAN ='30' THEN CAST ((SELECT TOP 1 LH_LSTPAQ FROM TB_EMPAQUEDT WITH(NOLOCK) WHERE TB_EMPAQUEDT.LD_NRMOV = MIIDMOVI) AS VARCHAR)");
                sql.AppendLine("WHEN MBCDTRAN ='98' THEN CAST ((SELECT TSNROTRA FROM TRASLADO WITH(NOLOCK) WHERE TSCODEMP = MICODEMP AND TSMOVENT = MIIDMOVI) AS VARCHAR)");
                sql.AppendLine("WHEN MBCDTRAN ='99' THEN CAST ((SELECT TSNROTRA FROM TRASLADO WITH(NOLOCK) WHERE TSCODEMP = MICODEMP AND TSMOVSAL = MIIDMOVI) AS VARCHAR)");
                sql.AppendLine("ELSE CAST (MIIDMOVI AS VARCHAR)");
                sql.AppendLine("END NUMERO");
                sql.AppendLine("FROM MOVIMBOD");
                sql.AppendLine("INNER JOIN MOVIMINV ON (MBCODEMP = MICODEMP AND MIIDMOVI = MBIDMOVI)");
                sql.AppendLine("LEFT OUTER JOIN TBTIPMOV ON (MBCODEMP = TMCODEMP AND MBCDTRAN = TMCDTRAN)");
                sql.AppendLine("WHERE MBCODEMP = @p0");
                sql.AppendLine("AND MBTIPPRO = @p1");
                sql.AppendLine("AND MBCLAVE1 = @p2");
                sql.AppendLine("AND MBCLAVE2 = @p3");
                sql.AppendLine("AND MBCLAVE3 = @p4");
                sql.AppendLine("AND MBCLAVE4 = @p5");
                sql.AppendLine("AND MBBODEGA = @p6");
                sql.AppendLine("AND MBFECMOV > ISNULL((SELECT FIFECING FROM FOTOINVF WITH(NOLOCK) WHERE FINROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK) INNER JOIN FOTOIBOD WITH(NOLOCK) ON (FICODEMP = FBCODEMP AND FINROFOT = FBNROFOT) WHERE FIINVINI = 'S'  AND FBTIPPRO=MBTIPPRO AND FIBODEGA =@p6)),'01/01/2000')");
                sql.AppendLine(") TTT");
                sql.AppendLine("ORDER BY FBFECING");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, CODEMP, TP, C1, C2, C3, C4, BD);

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
        #endregion
        //Consulta de Ventas
        #region
        public static DataTable GetVentasDetalle(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT V_facturadt.* FROM V_facturadt  ");                
                sSql.AppendLine(" WHERE 1=1 "+filter);             
                
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
        public static DataTable GetCartera(SessionManager oSessionManager, string filter, string inMes, string inAno)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT * FROM (");
                sSql.AppendLine("SELECT *,(((HDSUBTOT*POR_IVA)/100)+HDSUBTOT) VLR_CARTERA,");
                sSql.AppendLine("CASE WHEN (((HDSUBTOT*POR_IVA)/100)+HDSUBTOT) < 0 THEN ((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT) - ((isnull(TDEV,0)+isnull(RECAUDO,0)+isnull(RECA_SF,0))*-1) ) ELSE 0 END SFXAPL,");
                sSql.AppendLine("CASE WHEN (((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+isnull(RECA_SF,0)) - (isnull(RECAUDO,0)+isnull(TDEV,0))) < 0 THEN 0 ELSE (((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+isnull(RECA_SF,0)) - (isnull(RECAUDO,0)+isnull(TDEV,0))) END SALDO");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT FACTURAHD.*,A.TRCODNIT,A.TRNOMBRE +' '+ISNULL(A.TRNOMBR2,'')+' '+ISNULL(A.TRAPELLI,'') CLIENTE,");
		        sSql.AppendLine("B.TRNOMBRE +' '+ISNULL(B.TRNOMBR2,'')+' '+ISNULL(B.TRAPELLI,'') VENDEDOR,");
                sSql.AppendLine("PGVLRPAG,C.TTDESCRI tipo_pago,D.TTDESCRI tipo_pago_detalle,D.TTVALORN,(HDFECFAC+D.TTVALORN) F_VENCIMIENTO,");
		        sSql.AppendLine("CASE WHEN DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),GETDATE()) < 0 THEN 0 ELSE DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),GETDATE()) END DIAS,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC' AND CONVERT(DATE,RC_FECREC,101) <= CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)) RECAUDO,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)) TDEV,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)) IDEV,");
		        sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)) SDEV,");
                sSql.AppendLine("(SELECT CASE WHEN CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,'01/01/2017',101) THEN 16 ELSE 19 END ) POR_IVA,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF =HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO ='AC' AND CONVERT(DATE,RC_FECREC,101) <= CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)) RECA_SF");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS A WITH(NOLOCK) ON (A.TRCODEMP = HDCODEMP  AND A.TRCODTER = HDCODCLI)");
                sSql.AppendLine("INNER JOIN TERCEROS B WITH(NOLOCK) ON (B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDAGENTE)");
                sSql.AppendLine("INNER JOIN PGFACTUR WITH(NOLOCK) ON (PGCODEMP = HDCODEMP AND PGTIPFAC = HDTIPFAC AND PGNROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTABLAS C WITH(NOLOCK) ON (C.TTCODEMP = HDCODEMP  AND C.TTCODCLA = PGTIPPAG AND C.TTCODTAB = 'PAGO')");
                sSql.AppendLine("INNER JOIN TBTABLAS D WITH(NOLOCK) ON (D.TTCODEMP = HDCODEMP  AND D.TTCODCLA = PGDETTPG AND D.TTCODTAB = C.TTVALORC)");
                sSql.AppendLine("WHERE CONVERT(DATE,HDFECFAC,101) <=  CONVERT(DATE,EOMONTH('01/'+cast(@p0 as varchar)+'/'+cast(@p1 as varchar)),101)");
                sSql.AppendLine("AND HDESTADO <> 'AN' " + filter);
                sSql.AppendLine(") XX ) XY WHERE ISNULL(SALDO,0) <> 0");

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
        public static DataTable GetRecaudo(SessionManager oSessionManager, string filter, string inMes, string inAno)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SUM(RC_VALOR) RECAUDO,RC_NRORECIBO,RC_FECREC,HDNROFAC,HDTIPFAC,TRCODTER,(TRNOMBRE +' '+ ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOMBRE");
                sSql.AppendLine("FROM TB_RECAUDO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (HDCODEMP = RC_CODEMP AND HDTIPFAC = RC_TIPFAC AND HDNROFAC = RC_NROFAC)");
                sSql.AppendLine("INNER JOIN TERCEROS A WITH(NOLOCK) ON (TRCODEMP = HDCODEMP AND HDAGENTE = TRCODTER)");
                sSql.AppendLine("WHERE MONTH(RC_FECREC) = @p0");
                sSql.AppendLine("AND YEAR(RC_FECREC) = @p1 "+ filter);
                sSql.AppendLine("GROUP BY RC_NRORECIBO,RC_FECREC,HDNROFAC,HDTIPFAC,TRNOMBRE,TRNOMBR2,TRAPELLI,TRCODTER ");

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
        public static DataTable GetVentasGrafica(SessionManager oSessionManager, string filter, string inMes, string inAno)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SUM(DTTOTFAC) TOT,SUM(DTSUBTOT) STOT,ISNULL(ARDTTEC4,'OTRAS') ARDTTEC4,SUM(DTCANTID) CANTIDAD");
                sSql.AppendLine("FROM FACTURAHD");
                sSql.AppendLine("INNER JOIN FACTURADT ON (HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC) ");
                sSql.AppendLine("INNER JOIN ARTICULO ON (ARCODEMP = DTCODEMP AND ARTIPPRO = DTTIPPRO AND ARCLAVE1 = DTCLAVE1 AND ARCLAVE2 = DTCLAVE2 AND ARCLAVE3 = DTCLAVE3");
				sSql.AppendLine("	  AND ARCLAVE4 = DTCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPFAC ON (HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC)");
                sSql.AppendLine("WHERE MONTH(HDFECFAC) = @p0");
                sSql.AppendLine("AND YEAR(HDFECFAC) = @p1");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("AND TFCLAFAC = 1 " + filter);
                sSql.AppendLine("GROUP BY ARDTTEC4" );

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
        public static DataTable GetClientesxVendedor(SessionManager oSessionManager, string filter,int inMes, int inAno)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT A.TRCODTER,A.TRCODNIT,(A.TRNOMBRE+' '+ISNULL(A.TRNOMBR2,'')+' '+ISNULL(A.TRAPELLI,'')) CLIENTE,A.TRNROTEL,A.TRCORREO,A.TRDIRECC,CDNOMBRE,CDREGION,");
	            sSql.AppendLine("(B.TRNOMBRE+' '+isnull(B.TRNOMBR2,'')+' '+isnull(B.TRAPELLI,'')) VENDEDOR,A.TRFECING,");
                sSql.AppendLine("(SELECT SUM(HDTOTFAC) FROM FACTURAHD WHERE HDCODCLI = A.TRCODTER AND HDTIPFAC ='FT' AND HDESTADO <> 'AN') TT,");
                sSql.AppendLine("(SELECT MAX(HDFECFAC) FROM FACTURAHD WHERE HDCODCLI = A.TRCODTER AND HDTIPFAC ='FT' AND HDESTADO <> 'AN') MXFEC,");
                sSql.AppendLine("(SELECT SUM(HDTOTFAC) FROM FACTURAHD WHERE HDCODCLI = A.TRCODTER AND HDTIPFAC ='FT' AND HDESTADO <> 'AN' AND MONTH(HDFECFAC) =@p0 AND YEAR(HDFECFAC)=@p1 ) TTMES");
                sSql.AppendLine("FROM TERCEROS A");
                sSql.AppendLine("LEFT OUTER JOIN CIUDADES ON (TRCODEMP = CDCODEMP AND TRCIUDAD = CDCIUDAD)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS B ON (B.TRCODEMP = A.TRCODEMP AND A.TRAGENTE = B.TRCODTER)");
                sSql.AppendLine("WHERE A.TRINDCLI='S' "+filter);

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,inMes, inAno);
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
        public static DataTable GetConsultaPagos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT V_PGFACTUR.*,(HDTIPFAC + '-' + CAST(HDNROFAC AS VARCHAR)) NRO_FAC FROM V_PGFACTUR ");
                sSql.AppendLine(" WHERE 1=1 " + filter);

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sSql = null;
            }
        }
        #endregion
        //DashBoard
        #region
        public static DataTable GetVentasHDAgrupadasxMes(SessionManager oSessionManager, int inYear)            
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *,(((X/Z)-1)*100) Porcentaje FROM (");
                sSql.AppendLine("SELECT TTCODCLA,TTVALORN,TTDESCRI MES,(SELECT SUM(A.HDSUBTOT) FROM V_ventashd A WHERE A.TFCLAFAC IN (1,2) AND A.HDESTADO NOT IN ('AN') AND YEAR(A.HDFECFAC) IN (@p0) AND MONTH(A.HDFECFAC) = TTVALORN ) X,");
                sSql.AppendLine("                             (SELECT SUM(A.HDSUBTOT) FROM V_ventashd A WHERE A.TFCLAFAC IN (1,2) AND A.HDESTADO NOT IN ('AN') AND YEAR(A.HDFECFAC) IN (@p1) AND MONTH(A.HDFECFAC) = TTVALORN ) Z");
                sSql.AppendLine("FROM TBTABLAS WITH(NOLOCK)");                
                sSql.AppendLine("WHERE TTCODTAB ='MESES'");                
                sSql.AppendLine(") XX ORDER BY TTVALORN");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inYear, inYear-1);
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
        public static DataTable GetVentasTipoxMes(SessionManager oSessionManager, int inMonth, int inYear)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT A.TANOMBRE,SUM(X.DTCANTID) XC,SUM(X.DTSUBTOT) XT,");
	            sSql.AppendLine("(SELECT SUM(Y.DTCANTID) FROM V_facturadt Y WHERE YEAR(Y.HDFECFAC) = @p2 AND MONTH(Y.HDFECFAC) = @p3 AND Y.DTTIPPRO = A.TATIPPRO AND Y.TFCLAFAC IN (1,2)) YC,");
	            sSql.AppendLine("(SELECT SUM(Y.DTSUBTOT) FROM V_facturadt Y WHERE YEAR(Y.HDFECFAC) = @p2 AND MONTH(Y.HDFECFAC) = @p3 AND Y.DTTIPPRO = A.TATIPPRO AND Y.TFCLAFAC IN (1,2)) YT,");
	            sSql.AppendLine("(SELECT SUM(Y.DTCANTID) FROM V_facturadt Y WHERE YEAR(Y.HDFECFAC) = @p4 AND MONTH(Y.HDFECFAC) = @p5 AND Y.DTTIPPRO = A.TATIPPRO AND Y.TFCLAFAC IN (1,2)) ZC,");
                sSql.AppendLine("(SELECT SUM(Y.DTSUBTOT) FROM V_facturadt Y WHERE YEAR(Y.HDFECFAC) = @p4 AND MONTH(Y.HDFECFAC) = @p5 AND Y.DTTIPPRO = A.TATIPPRO AND Y.TFCLAFAC IN (1,2)) ZT");
                sSql.AppendLine("FROM TBTIPPRO A");
                sSql.AppendLine("LEFT OUTER JOIN V_facturadt X ON (YEAR(X.HDFECFAC) = @p0 AND MONTH(X.HDFECFAC) = @p1 AND X.DTTIPPRO = A.TATIPPRO AND X.TFCLAFAC IN (1,2) AND HDESTADO NOT IN ('AN'))");
                sSql.AppendLine("GROUP BY A.TANOMBRE,A.TATIPPRO");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inYear, inMonth, inYear, inMonth - 1, inYear - 1, inMonth);
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
        public static DataTable GetVentasVendedorxMes(SessionManager oSessionManager, int inMonth, int inYear)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT x.HDAGENTE,x.NOMBRE, SUM(x.DTCANTID) CVTA,SUM(x.DTSUBTOT) TVTA,CASE WHEN TFCLAFAC = '1' THEN 'Facturacion' Else 'Devoluciones' End Tipo");                
                sSql.AppendLine("FROM V_facturadt x");
                sSql.AppendLine("WHERE YEAR(x.HDFECFAC) = @p0 AND MONTH(x.HDFECFAC) = @p1");
                sSql.AppendLine("AND TFCLAFAC IN (1,2) ");
                sSql.AppendLine("AND HDESTADO NOT IN ('AN')");
                sSql.AppendLine("GROUP BY HDAGENTE,NOMBRE,TFCLAFAC");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inYear, inMonth);
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
        #endregion
        //Hoja Kardex
        #region
        public static DataTable GetAnosMov(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT DISTINCT YEAR(MIFECMOV) ANO FROM MOVIMINV WITH(NOLOCK)");

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
        public static DataTable GetHojaKardex(SessionManager oSessionManager, string inFiltro,string inBodega,DateTime inFechaIni,DateTime inFechaFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT SUM(CASE WHEN TMENTSAL ='S' THEN MBCANTID*-1 ELSE MBCANTID END) FBCANTID,");
                //sSql.AppendLine("TANOMBRE, ARNOMBRE, MBBODEGA, MBTIPPRO, MBCLAVE1, MBCLAVE2, MBCLAVE3, MBCLAVE4, MBCDTRAN, TMNOMBRE, CLAVE2, CLAVE3");
                //sSql.AppendLine("FROM(");
                //sSql.AppendLine("SELECT TANOMBRE, ARNOMBRE, MBBODEGA, MBTIPPRO, MBCLAVE1, MBCLAVE2, MBCLAVE3, MBCLAVE4, MBCDTRAN, TMNOMBRE, TMENTSAL, MBCANTID,");
                //sSql.AppendLine("CASE WHEN TACTLSE2 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                //sSql.AppendLine("AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2");
                //sSql.AppendLine("AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                //sSql.AppendLine("CASE WHEN TACTLSE3 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                //sSql.AppendLine("AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                //sSql.AppendLine("AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                //sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK)");
                //sSql.AppendLine("INNER JOIN MOVIMINV ON(MBCODEMP = MICODEMP AND MIIDMOVI = MBIDMOVI)");
                //sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MICODEMP AND ARTIPPRO = MBTIPPRO AND ARCLAVE1 = MBCLAVE1 AND ARCLAVE2 = MBCLAVE2 AND ARCLAVE3 = MBCLAVE3 AND ARCLAVE4 = MBCLAVE4)");
                //sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                //sSql.AppendLine("INNER JOIN TBTIPMOV ON(MBCODEMP = TMCODEMP AND MBCDTRAN = TMCDTRAN)");
                //sSql.AppendLine("WHERE MBBODEGA = @p0");
                //sSql.AppendLine("AND CONVERT(DATE, MBFECMOV, 101) BETWEEN CONVERT(DATE, @p1, 101) AND CONVERT(DATE, @p2, 101)");
                //sSql.AppendLine("AND MBESTADO NOT IN('AC')");
                //sSql.AppendLine("UNION ALL");
                //sSql.AppendLine("SELECT TANOMBRE, ARNOMBRE, FBBODEGA, FBTIPPRO, FBCLAVE1, FBCLAVE2, FBCLAVE3, FBCLAVE4,99 MBCDTRAN,'A. INV INICIAL','E' TIPO,FBCANTID");
                //sSql.AppendLine("+ (SELECT SUM(CASE WHEN TMENTSAL ='S' THEN MBCANTID*-1 ELSE MBCANTID END) FROM MOVIMBOD WITH(NOLOCK)");
                //sSql.AppendLine("INNER JOIN TBTIPMOV ON(MBCODEMP = TMCODEMP AND MBCDTRAN = TMCDTRAN)");
                //sSql.AppendLine("WHERE MBFECMOV BETWEEN ISNULL((SELECT FIFECING FROM FOTOINVF WITH(NOLOCK) WHERE FINROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK)");
                //sSql.AppendLine("                                                                      INNER JOIN FOTOIBOD WITH(NOLOCK) ON (FICODEMP = FBCODEMP AND FINROFOT = FBNROFOT)");
                //sSql.AppendLine("                                                                      WHERE FIINVINI = 'S'  AND FBBODEGA = MBBODEGA AND FBTIPPRO = MBTIPPRO AND CONVERT(DATE, FIFECING,101)<= CONVERT(DATE, @p1, 101) )),'01/01/2000') ");
                //sSql.AppendLine("                                           AND DATEADD(DAY,-1,CONVERT(DATE, @p1, 101)) AND FBCODEMP = MBCODEMP AND MBBODEGA = FBBODEGA AND MBTIPPRO = FBTIPPRO AND MBCLAVE1 = FBCLAVE1 AND MBCLAVE2 = FBCLAVE2 AND MBCLAVE3 = FBCLAVE3 AND MBCLAVE4 = FBCLAVE4),");
                //sSql.AppendLine("CASE WHEN TACTLSE2 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                //sSql.AppendLine("AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2");
                //sSql.AppendLine("AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                //sSql.AppendLine("CASE WHEN TACTLSE3 = 'S' THEN(SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                //sSql.AppendLine("AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                //sSql.AppendLine("AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                //sSql.AppendLine("FROM FOTOIBOD WITH(NOLOCK)");
                //sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = FBCODEMP AND ARTIPPRO = FBTIPPRO AND ARCLAVE1 = FBCLAVE1 AND ARCLAVE2 = FBCLAVE2 AND ARCLAVE3 = FBCLAVE3 AND ARCLAVE4 = FBCLAVE4)");
                //sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                //sSql.AppendLine("WHERE FBNROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK)");
                //sSql.AppendLine("   INNER JOIN FOTOIBOD WITH(NOLOCK) ON(FICODEMP = FBCODEMP AND FINROFOT = FBNROFOT)");
                //sSql.AppendLine("   WHERE FIINVINI = 'S' AND FBBODEGA = @p0 AND FBTIPPRO = ARTIPPRO AND CONVERT(DATE, FIFECING,101)<= DATEADD(DAY,-1,CONVERT(DATE, @p1, 101)) ) ");
                //sSql.AppendLine("AND FBCANTID > 0");
                //sSql.AppendLine(") XX WHERE 1=1 "+ inFiltro);
                //sSql.AppendLine("GROUP BY TANOMBRE,ARNOMBRE,MBBODEGA,MBTIPPRO,MBCLAVE1,MBCLAVE2,MBCLAVE3,MBCLAVE4,MBCDTRAN,TMNOMBRE,CLAVE2,CLAVE3");

                sSql.AppendLine("SELECT *, '1- Inv Inicial' Concepto,GETDATE() Fecha_Mov,ISNULL((SELECT  SUM(CASE WHEN TIPO ='S' THEN FBCANTID*-1 ELSE FBCANTID END) FBCANTID    ");
                sSql.AppendLine("FROM(");
                sSql.AppendLine("SELECT FBNROFOT, FBBODEGA, FBTIPPRO, FBCLAVE1, FBCLAVE2, FBCLAVE3, FBCLAVE4, 0 MBCDTRAN, 'INV INICIAL' DESCRIPCION, 'E' TIPO, isnull(FBCANTID, 0) FBCANTID, 'CE' ESTADO, FBFECING, CAST(FBNROFOT AS VARCHAR) DOCUMENTO, CAST(FBNROFOT AS VARCHAR) DOCUEMNTO_CON");
                sSql.AppendLine("FROM FOTOIBOD WITH(NOLOCK)");
                sSql.AppendLine("WHERE FBNROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK) WHERE FIINVINI = 'S'   AND FIBODEGA = @p0)");
                sSql.AppendLine("AND FBCANTID > 0");
                sSql.AppendLine("AND FBCODEMP = X.ARCODEMP");
                sSql.AppendLine("AND FBTIPPRO = X.ARTIPPRO");
                sSql.AppendLine("AND FBCLAVE1 = X.Referencia");
                sSql.AppendLine("AND FBCLAVE2 = X.ARCLAVE2");
                sSql.AppendLine("AND FBCLAVE3 = X.ARCLAVE3");
                sSql.AppendLine("AND FBCLAVE4 = X.ARCLAVE4");
                sSql.AppendLine("AND FBBODEGA = @p0");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT MIIDMOVI, MBBODEGA, MBTIPPRO, MBCLAVE1, MBCLAVE2, MBCLAVE3, MBCLAVE4, MBCDTRAN, TMNOMBRE, TMENTSAL, isnull(MBCANTID, 0) MBCANTID, MBESTADO, MBFECING, MICDDOCU,");
                sSql.AppendLine("CASE");
                sSql.AppendLine("WHEN MBCDTRAN IN('14', '15', '31') THEN(SELECT TOP 1 CAST(DTTIPFAC AS VARCHAR) + '-' + CAST(DTNROFAC AS VARCHAR) FROM FACTURADT WHERE DTNROMOV = MIIDMOVI)");
                sSql.AppendLine("WHEN MBCDTRAN = '16' THEN CAST((SELECT TOP 1 CMP_RECIBOHD.RH_NRORECIBO FROM CMP_RECIBODT");
                sSql.AppendLine("INNER JOIN CMP_RECIBOHD ON(RH_CODEMP = RD_CODEMP AND RH_NRORECIBO = RD_NRORECIBO)");
                sSql.AppendLine("    WHERE RD_IDMOVI = MIIDMOVI) AS VARCHAR)");
                sSql.AppendLine("WHEN MBCDTRAN = '30' THEN CAST((SELECT TOP 1 LH_LSTPAQ FROM TB_EMPAQUEDT WHERE TB_EMPAQUEDT.LD_NRMOV = MIIDMOVI) AS VARCHAR)");
                sSql.AppendLine("WHEN MBCDTRAN = '98' THEN CAST((SELECT TSNROTRA FROM TRASLADO WHERE TSCODEMP = MICODEMP AND TSMOVENT = MIIDMOVI) AS VARCHAR)");
                sSql.AppendLine("WHEN MBCDTRAN = '99' THEN CAST((SELECT TSNROTRA FROM TRASLADO WHERE TSCODEMP = MICODEMP AND TSMOVSAL = MIIDMOVI) AS VARCHAR)");
                sSql.AppendLine("ELSE CAST(MIIDMOVI AS VARCHAR)");
                sSql.AppendLine("END NUMERO");
                sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN MOVIMINV WITH(NOLOCK) ON(MBCODEMP = MICODEMP AND MIIDMOVI = MBIDMOVI)");
                sSql.AppendLine("INNER JOIN TBTIPMOV ON(MBCODEMP = TMCODEMP AND MBCDTRAN = TMCDTRAN)");
                sSql.AppendLine("WHERE MBCODEMP = X.ARCODEMP");
                sSql.AppendLine("AND MBTIPPRO = X.ARTIPPRO");
                sSql.AppendLine("AND MBCLAVE1 = X.Referencia");
                sSql.AppendLine("AND MBCLAVE2 = X.ARCLAVE2");
                sSql.AppendLine("AND MBCLAVE3 = X.ARCLAVE3");
                sSql.AppendLine("AND MBCLAVE4 = X.ARCLAVE4");
                sSql.AppendLine("AND MBBODEGA = @p0");
                sSql.AppendLine("AND MBFECMOV < ISNULL((SELECT FIFECING FROM FOTOINVF WITH(NOLOCK) WHERE FINROFOT = (SELECT MAX(FINROFOT) FROM FOTOINVF WITH(NOLOCK) WHERE FIINVINI = 'S'  AND FIBODEGA = @p0)),'01/01/2000')");
                sSql.AppendLine("AND MBESTADO NOT IN('AC')");
                sSql.AppendLine(") TTT),0) CAN_CERRADA");
                sSql.AppendLine("FROM VW_RTICULOS X");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT MBTIPPRO,MBCODEMP,MBCLAVE2,MBCLAVE3,MBCLAVE4,COD_BARRAS,Fragancia,Marca,Referencia,Descripcion,Tamano,Linea,PRECIO,PRECIO_USD,PRECIO_USD_RETAIL,COSTO_FOB,COSTO_CIF,");
                sSql.AppendLine("Tipo,Genero,DT6,DT8,DT9,DT10,FEC_INICIAL,FEC_FINAL,REGISTRO,ESTADO,dias,REF_TESTER,NOM_TESTER,Ref_Alterna,Moneda,Proveedor,CAST(TMCDTRAN AS VARCHAR) + '-' + TMNOMBRE Concepto,MBFECMOV Fecha_Mov,");
                sSql.AppendLine("MBCANTID CAN_CERRADA");
                sSql.AppendLine("FROM VW_RTICULOS X");
                sSql.AppendLine("INNER JOIN MOVIMBOD WITH(NOLOCK) ON(MBCODEMP = ARCODEMP AND MBTIPPRO = ARTIPPRO AND MBCLAVE1 = Referencia AND MBCLAVE2 = ARCLAVE2 AND MBCLAVE3 = ARCLAVE3)");
                sSql.AppendLine("INNER JOIN TBTIPMOV WITH(NOLOCK) ON(MBCODEMP = TMCODEMP AND MBCDTRAN = TMCDTRAN)");
                sSql.AppendLine("WHERE MBESTADO NOT IN('AN')");
                sSql.AppendLine("AND MBBODEGA = @p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inBodega,inFechaIni,inFechaFin);
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
        #endregion
        //Cartera
        #region
        public static DataTable GetCartera(SessionManager oSessionManager, DateTime inFecha ,string inFiltro)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT * FROM (");
                sSql.AppendLine("SELECT *,CASE WHEN (((HDSUBTOT*POR_IVA)/100)+HDSUBTOT+ISNULL(TND,0))-(ISNULL(TDEV,0)-ISNULL(RECAUDO,0)) < 0 THEN (((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+ISNULL(TND,0))-(ISNULL(TDEV,0)-ISNULL(RECAUDO,0) + ISNULL(TNC,0))) - (ISNULL(RECA_SF,0)*-1) ELSE 0 END SAL_FAVOR_APL,");
                sSql.AppendLine("         CASE WHEN ((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT+RECA_SF+ISNULL(TND,0))-(ISNULL(RECAUDO,0)+ISNULL(TDEV,0)))< 0 THEN (((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+ISNULL(TND,0))-(ISNULL(TDEV,0)+ISNULL(RECAUDO,0) + ISNULL(TNC,0))) - (ISNULL(RECA_SF,0)*-1) ELSE ((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+ISNULL(RECA_SF,0)+ISNULL(TND,0))-(ISNULL(RECAUDO,0)+ISNULL(TDEV,0)+ ISNULL(TNC,0)) END SALDO,");
                sSql.AppendLine("(((HDSUBTOT*POR_IVA)/100)+HDSUBTOT) VLR_CAR");
                sSql.AppendLine("FROM ( ");
                sSql.AppendLine("SELECT (HDTIPFAC+'-'+CAST(HDNROFAC AS VARCHAR) ) NRO_FAC, HDNROFAC,HDTIPFAC,HDFECFAC,HDSUBTOT,HDTOTFAC,HDTOTIVA,A.TRCODNIT,A.TRNOMBRE +' '+ISNULL(A.TRNOMBR2,'')+' '+ISNULL(A.TRAPELLI,'') CLIENTE,A.TRCODTER,");
		        sSql.AppendLine("B.TRNOMBRE +' '+ISNULL(B.TRNOMBR2,'')+' '+ISNULL(B.TRAPELLI,'') VENDEDOR,");
                sSql.AppendLine("PGVLRPAG,C.TTDESCRI,D.TTDESCRI TTDESCRI1,D.TTVALORN,(HDFECFAC+D.TTVALORN) F_VENCIMIENTO,");
		        sSql.AppendLine("CASE WHEN DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),@p0) < 0 THEN 0 ELSE DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),@p0) END DIAS,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC' AND CONVERT(DATE,RC_FECREC,101) <= CONVERT(DATE,@p0,101)) RECAUDO,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,@p0,101)) TDEV,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,@p0,101)) IDEV,");
		        sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN' AND CONVERT(DATE,X.HDFECFAC,101) <= CONVERT(DATE,@p0,101)) SDEV,");                
                sSql.AppendLine("(SELECT top 1 (SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC) POR_IVA,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF =HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO ='AC' AND CONVERT(DATE,RC_FECREC,101) <= CONVERT(DATE,@p0,101)) RECA_SF,");

                sSql.AppendLine("ISNULL((SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON (TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA AND TB_NOTAHD.NH_TIPFAC = TB_NOTADT.NH_TIPFAC) ");
                sSql.AppendLine("WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND TB_NOTADT.DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO ='AC'),0) TNC,");

                sSql.AppendLine("ISNULL((SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON (TB_NOTADEBHD.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA AND TB_NOTADEBHD.NH_TIPFAC = TB_NOTADEBDT.NH_TIPFAC) ");
                sSql.AppendLine("WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND TB_NOTADEBDT.DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO ='AC'),0) TND");

                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (HDCODEMP =TFCODEMP AND HDTIPFAC = TFTIPFAC )");
                sSql.AppendLine("INNER JOIN TERCEROS A WITH(NOLOCK) ON (A.TRCODEMP = HDCODEMP  AND A.TRCODTER = HDCODCLI)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS B WITH(NOLOCK) ON (B.TRCODEMP = HDCODEMP  AND B.TRCODTER = A.TRAGENTE)");
                sSql.AppendLine("INNER JOIN PGFACTUR WITH(NOLOCK) ON (PGCODEMP = HDCODEMP AND PGTIPFAC = HDTIPFAC AND PGNROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTABLAS C WITH(NOLOCK) ON (C.TTCODEMP = HDCODEMP  AND C.TTCODCLA = PGTIPPAG AND C.TTCODTAB = 'PAGO')");
                sSql.AppendLine("INNER JOIN TBTABLAS D WITH(NOLOCK) ON (D.TTCODEMP = HDCODEMP  AND D.TTCODCLA = PGDETTPG AND D.TTCODTAB = C.TTVALORC)");
                sSql.AppendLine("WHERE CONVERT(DATE,HDFECFAC,101) <=  CONVERT(DATE,@p0,101)");
                sSql.AppendLine("AND HDESTADO <> 'AN'");
                sSql.AppendLine("AND TFCLAFAC IN (1,2)");
                sSql.AppendLine(") XXX_TMP) XX_TMP");
                sSql.AppendLine("WHERE 1=1 "+inFiltro);
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,inFecha);
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
        public static DataTable GetCarteraxCliente(SessionManager oSessionManager, DateTime inFecha, string inFiltro)
        {
            StringBuilder sSql = new StringBuilder();
            try { 
                sSql.AppendLine("SELECT * FROM (");
                sSql.AppendLine("SELECT *,((ISNULL(FACTURACION_30,0)-ISNULL(DEVOLUCION_30,0))-ISNULL(RECAUDO_30,0)) CAR_30,((ISNULL(FACTURACION_60,0)-ISNULL(DEVOLUCION_60,0))-ISNULL(RECAUDO_60,0)) CAR_60,");
                sSql.AppendLine("((ISNULL(FACTURACION_90,0)-ISNULL(DEVOLUCION_90,0))-ISNULL(RECAUDO_90,0)) CAR_90, ((ISNULL(FACTURACION,0)+ISNULL(ND,0))-(ISNULL(DEVOLUCION,0)+ISNULL(RECAUDO,0)+ISNULL(NC,0))) SALDO");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT B.TRCODTER,B.TRCODNIT,B.TRNOMBRE +' '+ISNULL(B.TRNOMBR2,'')+' '+ISNULL(B.TRAPELLI,'') CLIENTE,B.TRAGENTE,");
		        sSql.AppendLine("(SELECT SUM(TOT) FROM ");		        
                sSql.AppendLine("(SELECT (((HDSUBTOT*(SELECT top 1 (SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC))/100)+HDSUBTOT) TOT");
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = HDCODEMP AND TFTIPFAC = HDTIPFAC AND TFCLAFAC NOT IN (2,5))");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,@p0,101)"); 
		        sSql.AppendLine("AND HDESTADO<>'AN')  TMP ) FACTURACION,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR)");
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
			    sSql.AppendLine("LEFT OUTER JOIN TB_RECAUDO WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC')");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,@p0,101)");
		        sSql.AppendLine("AND HDESTADO<>'AN') RECAUDO,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC)");
		        sSql.AppendLine("FROM FACTURAHD T WITH(NOLOCK)");
			    sSql.AppendLine("INNER JOIN FACTURAHD X WITH(NOLOCK) ON (X.HDTIPDEV = T.HDTIPFAC AND X.HDNRODEV = T.HDNROFAC AND X.HDESTADO <> 'AN')");
		        sSql.AppendLine("WHERE B.TRCODEMP = T.HDCODEMP");  
		        sSql.AppendLine("AND B.TRCODTER = T.HDCODCLI"); 
			    sSql.AppendLine("AND CONVERT(DATE,T.HDFECFAC,101) <=  CONVERT(DATE,@p0,101)");
		        sSql.AppendLine("AND T.HDESTADO<>'AN') DEVOLUCION,");
		        sSql.AppendLine("(SELECT SUM(TOT) FROM ");
		        //sSql.AppendLine("(SELECT (((HDSUBTOT*(SELECT CASE WHEN CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,'01/01/2017',101) THEN 16 ELSE 19 END ))/100)+HDSUBTOT) TOT");
                sSql.AppendLine("(SELECT ((HDSUBTOT*(SELECT top 1 (SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC)/100)+HDSUBTOT) TOT");                
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = HDCODEMP AND TFTIPFAC = HDTIPFAC AND TFCLAFAC NOT IN (2,5))");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-30,@p0),101) AND CONVERT(DATE,@p0,101) ");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(")  TMP ) FACTURACION_30,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR)");
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
			    sSql.AppendLine("LEFT OUTER JOIN TB_RECAUDO WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC')");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-30,@p0),101) AND CONVERT(DATE,@p0,101) ");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(") RECAUDO_30,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC)");
		        sSql.AppendLine("FROM FACTURAHD T WITH(NOLOCK)");
			    sSql.AppendLine("INNER JOIN FACTURAHD X WITH(NOLOCK) ON (X.HDTIPDEV = T.HDTIPFAC AND X.HDNRODEV = T.HDNROFAC AND X.HDESTADO <> 'AN')");
		        sSql.AppendLine("WHERE B.TRCODEMP = T.HDCODEMP  ");
		        sSql.AppendLine("AND B.TRCODTER = T.HDCODCLI ");
			    sSql.AppendLine("AND CONVERT(DATE,T.HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-30,@p0),101) AND CONVERT(DATE,@p0,101) ");
		        sSql.AppendLine("AND T.HDESTADO<>'AN') DEVOLUCION_30,");
		        sSql.AppendLine("(SELECT SUM(TOT) FROM ");
		        //sSql.AppendLine("(SELECT (((HDSUBTOT*(SELECT CASE WHEN CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,'01/01/2017',101) THEN 16 ELSE 19 END ))/100)+HDSUBTOT) TOT");
                sSql.AppendLine("(SELECT ((HDSUBTOT*(SELECT top 1 (SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC)/100)+HDSUBTOT) TOT");                
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = HDCODEMP AND TFTIPFAC = HDTIPFAC AND TFCLAFAC NOT IN (2,5))");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-60,@p0),101) AND CONVERT(DATE,DATEADD(DAY,-30,@p0),101) ");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(")  TMP ) FACTURACION_60,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR)");
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("LEFT OUTER JOIN TB_RECAUDO WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC')");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-60,@p0),101) AND CONVERT(DATE,DATEADD(DAY,-30,@p0),101) ");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(") RECAUDO_60,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC)");
		        sSql.AppendLine("FROM FACTURAHD T WITH(NOLOCK)");
			    sSql.AppendLine("INNER JOIN FACTURAHD X WITH(NOLOCK) ON (X.HDTIPDEV = T.HDTIPFAC AND X.HDNRODEV = T.HDNROFAC AND X.HDESTADO <> 'AN')");
		        sSql.AppendLine("WHERE B.TRCODEMP = T.HDCODEMP  ");
		        sSql.AppendLine("AND B.TRCODTER = T.HDCODCLI ");
			    sSql.AppendLine("AND CONVERT(DATE,T.HDFECFAC,101) BETWEEN CONVERT(DATE,DATEADD(DAY,-60,@p0),101) AND CONVERT(DATE,DATEADD(DAY,-30,@p0),101) ");
		        sSql.AppendLine("AND T.HDESTADO<>'AN') DEVOLUCION_60,");
		        sSql.AppendLine("(SELECT SUM(TOT) FROM ");
		        //sSql.AppendLine("(SELECT (((HDSUBTOT*(SELECT CASE WHEN CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,'01/01/2017',101) THEN 16 ELSE 19 END ))/100)+HDSUBTOT) TOT");
                sSql.AppendLine("(SELECT ((HDSUBTOT*(SELECT top 1 (SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC)/100)+HDSUBTOT) TOT");                
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = HDCODEMP AND TFTIPFAC = HDTIPFAC AND TFCLAFAC NOT IN (2,5))");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,DATEADD(DAY,-61,@p0),101)");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(")  TMP ) FACTURACION_90,");
		        sSql.AppendLine("(SELECT SUM(RC_VALOR)");
		        sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
		        sSql.AppendLine("LEFT OUTER JOIN TB_RECAUDO WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC')");
		        sSql.AppendLine("WHERE B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDCODCLI AND CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,DATEADD(DAY,-61,@p0),101)");
		        sSql.AppendLine("AND HDESTADO<>'AN'");
		        sSql.AppendLine(") RECAUDO_90,");
		        sSql.AppendLine("(SELECT SUM(X.HDTOTFAC)");
		        sSql.AppendLine("FROM FACTURAHD T WITH(NOLOCK)");
			    sSql.AppendLine("INNER JOIN FACTURAHD X WITH(NOLOCK) ON (X.HDTIPDEV = T.HDTIPFAC AND X.HDNRODEV = T.HDNROFAC AND X.HDESTADO <> 'AN')");
		        sSql.AppendLine("WHERE B.TRCODEMP = T.HDCODEMP  ");
		        sSql.AppendLine("AND B.TRCODTER = T.HDCODCLI ");
			    sSql.AppendLine("AND CONVERT(DATE,T.HDFECFAC,101) <= CONVERT(DATE,DATEADD(DAY,-61,@p0),101)");
		        sSql.AppendLine("AND T.HDESTADO<>'AN') DEVOLUCION_90,");
		        sSql.AppendLine("(SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE TTCODTAB = 'IMPF' AND TTCODCLA='16') V_IVA,");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON (TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA AND TB_NOTAHD.NH_TIPFAC = TB_NOTADT.NH_TIPFAC)");
                sSql.AppendLine(" INNER JOIN FACTURAHD WITH(NOLOCK) ON (HDNROFAC = DTNROFAC AND HDTIPFAC = DTTIPFAC) WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO ='AC' AND HDCODCLI = B.TRCODTER) NC,");                
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON (TB_NOTADEBHD.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA AND TB_NOTADEBHD.NH_TIPFAC = TB_NOTADEBDT.NH_TIPFAC)");
                sSql.AppendLine(" INNER JOIN FACTURAHD WITH(NOLOCK) ON (HDNROFAC = DTNROFAC AND HDTIPFAC = DTTIPFAC) WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO ='AC' AND HDCODCLI = B.TRCODTER) ND");
                sSql.AppendLine("FROM TERCEROS B WITH(NOLOCK)");
                sSql.AppendLine(") XX_TMP ) XX_TMP1 WHERE 1=1 "+inFiltro);

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inFecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static DataTable GetRecaudoxFactura(SessionManager oSessionManager, int HDNROFAC,string HDTIPFAC,string HDCODEMP  )
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SUM(RC_VALOR) TOT ,RC_NRORECIBO,RC_FECREC");
                sSql.AppendLine("FROM TB_RECAUDO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = RC_CODEMP AND TTCODCLA = RC_CONCEPTO AND TTCODTAB = 'CONREC')");
                sSql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (RC_TIPFAC = HDTIPFAC AND HDNROFAC = RC_NROFAC)");
                sSql.AppendLine("WHERE HDCODEMP =@p0 AND HDTIPFAC =@p1 AND HDNROFAC = @p2");
                sSql.AppendLine("AND RC_ESTADO ='AC'");
                sSql.AppendLine("GROUP BY RC_NRORECIBO,RC_FECREC");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC);
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
        #endregion
        //Recaudo
        #region
        public static DataTable GetConsultaRecaudos(SessionManager oSessionManager, string lc_sql,params object[] paramtervalues)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT RC_NRORECIBO,RC_FECREC, SUM(RC_VALOR) VALOR, TRCODNIT,(TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) TERCERO,TRDIRECC,TRNROTEL,BN_NOMBRE");
                sSql.AppendLine("FROM TB_RECAUDO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_RECAUDOHD WITH(NOLOCK) ON (RH_CODEMP = RC_CODEMP AND RC_NRORECIBO = RH_NRORECIBO)");
                sSql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC)");                
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = RC_CODEMP AND TRCODTER = HDCODCLI)");
                sSql.AppendLine("LEFT OUTER JOIN TB_BANCOS WITH(NOLOCK) ON (BN_ID = RH_BANCO)");
                sSql.AppendLine("WHERE RC_ESTADO ='AC'");
                sSql.AppendLine("AND RC_VALOR <> 0 " + lc_sql);               
                sSql.AppendLine("GROUP BY RC_NRORECIBO,RC_FECREC,TRCODNIT,TRNOMBRE,TRNOMBR2,TRAPELLI,TRDIRECC,TRNROTEL,BN_NOMBRE");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,paramtervalues);
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
        public static DataTable GetConsultaDetalleRecaudos(SessionManager oSessionManager, int RC_NRORECIBO)

        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT RC_NRORECIBO, RC_CONCEPTO,TTDESCRI,RC_VALOR,(RC_TIPFAC+'-'+CAST(RC_NROFAC AS VARCHAR)) NROFAC,TRCODNIT,");
                sSql.AppendLine("(TRNOMBRE + ' ' + ISNULL(TRNOMBR2, '') + ' ' + ISNULL(TRAPELLI, '')) TERCERO, TRDIRECC, TRNROTEL");
                sSql.AppendLine("FROM TB_RECAUDO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON(RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON(RC_CODEMP = TTCODEMP AND TTCODTAB = 'CONREC' AND TTCODCLA = RC_CONCEPTO)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON(TRCODEMP = RC_CODEMP AND TRCODTER = HDCODCLI)");
                sSql.AppendLine("WHERE RC_ESTADO = 'AC'");
                sSql.AppendLine("AND RC_VALOR <> 0 AND RC_NRORECIBO =@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, RC_NRORECIBO);
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
        #endregion
        //Pedidos
        #region
        public static DataTable GetTrazePedidos(SessionManager oSessionManager, string inFilter)

        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT Id,ParentId,[start],ISNULL([end],GETDATE()) [end],ESTADO,");
                sSql.AppendLine("CASE WHEN  ESTADO = 1 then 0.2");
                sSql.AppendLine("WHEN  ESTADO = 2 then 0.4");
                sSql.AppendLine("WHEN  ESTADO = 3 then 0.6");
                sSql.AppendLine("WHEN  ESTADO = 4 then 0.8");
                sSql.AppendLine("WHEN  ESTADO = 5 then 1 ELSE 0 END PercentComplete, Title, OrderID, Summary,");
                sSql.AppendLine("CASE WHEN  ESTADO = 1 then 'Pedido'");
                sSql.AppendLine("WHEN  ESTADO = 2 then 'Liquidado'");
                sSql.AppendLine("WHEN  ESTADO = 3 then 'Packing'");
                sSql.AppendLine("WHEN  ESTADO = 4 then 'Facturado/Remision'");
                sSql.AppendLine("WHEN  ESTADO = 5 then 'Entregado' ELSE 'Liquidado' END N_Estado");
                sSql.AppendLine("FROM(");
                sSql.AppendLine("SELECT PHPEDIDO Id, 1 ParentId, PHFECING[start],");
                //sSql.AppendLine("(SELECT TOP 1 HDFECING  FROM FACTURAHD WITH(NOLOCK) WHERE LH_LSTPAQ IN(SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_ESTADO NOT IN('AN') AND LH_PEDIDO = PHPEDIDO) AND HDESTADO NOT IN('AN') ORDER BY HDFECFAC DESC)[end],
                sSql.AppendLine("(SELECT TOP 1 ET_FECING FROM TB_ENTREGA WITH(NOLOCK) WHERE LH_LSTPAQ IN(SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_ESTADO NOT IN('AN') AND LH_PEDIDO = PHPEDIDO) ORDER BY ET_FECING DESC)[end],");
                sSql.AppendLine("CASE");
                sSql.AppendLine("WHEN(SELECT COUNT(ET_ID) FROM TB_ENTREGA WITH(NOLOCK) WHERE LH_LSTPAQ IN(SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_ESTADO NOT IN('AN') AND LH_PEDIDO = PHPEDIDO)) > 0 THEN 5");
                sSql.AppendLine("WHEN(SELECT COUNT(HDNROFAC) FROM FACTURAHD WITH(NOLOCK) WHERE LH_LSTPAQ IN(SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_ESTADO NOT IN('AN') AND LH_PEDIDO = PHPEDIDO) AND HDESTADO NOT IN('AN')) > 0 THEN 4");
                sSql.AppendLine("WHEN(SELECT COUNT(LH_LSTPAQ) FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_PEDIDO = PHPEDIDO AND LH_ESTADO NOT IN('AN')) > 0 THEN 3");
                sSql.AppendLine("WHEN PHESTADO = 'LQ' THEN 2");
                sSql.AppendLine("WHEN PHESTADO = 'AC' THEN 1");
                sSql.AppendLine("END ESTADO,");
                sSql.AppendLine("0.03 PercentComplete,");
                sSql.AppendLine("(TRNOMBRE + ' ' + ISNULL(TRNOMBR2, '') + ' ' + ISNULL(TRAPELLI, '')) Title, 0 OrderID,CAST(0 AS BIT) Summary");
                sSql.AppendLine("FROM PEDIDOHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON(TRCODEMP = PHCODEMP AND TRCODTER = PHCODCLI)");
                sSql.AppendLine("WHERE PHESTADO NOT IN('AN')");
                sSql.AppendLine(") TMP_XT WHERE 1=1 "+ inFilter);
                sSql.AppendLine("order by Id");
                 
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
        #endregion
        //Contabilidad
        #region
        public static DataTable GetMovimientosMes(SessionManager oSessionManager, string inFilter)

        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT CONT_MOVIMIENTOSHD.*,TFNOMBRE,CASE WHEN (MVTH_CREDITO - MVTH_DEBITO) != 0 THEN 'N' ELSE 'S' END CRZ ");
                sSql.AppendLine("  FROM CONT_MOVIMIENTOSHD WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(MVTH_CODEMP = TFCODEMP AND CONT_MOVIMIENTOSHD.TFTIPFAC = TBTIPFAC.TFTIPFAC) ");
                sSql.AppendLine(" WHERE 1=1 "+ inFilter);
                sSql.AppendLine("ORDER BY MVTH_FECMOV");

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
        #endregion
    }
}
