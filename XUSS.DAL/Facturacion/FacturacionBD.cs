using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Facturacion
{
    public class FacturacionBD
    {
        public static DataTable GetFacturaHD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TOP 100 FACTURAHD.*,TRNOMBRE,TRNOMBR2,TRAPELLI,TRCORREO,TRDIRECC,TRNROTEL,TRFECNAC,TRCIUDAD,(HD_TIPREM+'-'+CAST(HD_NROREM AS VARCHAR)) rms_lnk,");
                sSql.AppendLine("(SELECT LH_PEDIDO FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_CODEMP = HDCODEMP AND TB_EMPAQUEHD.LH_LSTPAQ = FACTURAHD.LH_LSTPAQ) PEDIDO,TFNOMBRE,TRCDPAIS,TRTERPAG,TRMONEDA,SC_NOMBRE ");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)	");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = HDCODEMP AND TFTIPFAC = HDTIPFAC)");
                sSql.AppendLine("INNER JOIN TERCEROS ON (TRCODEMP = HDCODEMP AND TRCODTER = HDCODCLI) ");
                sSql.AppendLine("LEFT OUTER JOIN TB_SUCURSAL WITH(NOLOCK) ON (TRCODEMP = SC_CODEMP AND TB_SUCURSAL.TRCODTER = TERCEROS.TRCODTER AND SC_CONSECUTIVO = HDCODSUC) ");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("WHERE " + filter);
                }
                sSql.AppendLine(" ORDER BY HDNROFAC DESC");
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
        public static DataTable GetFacturaDT(SessionManager oSessionManager, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT FACTURADT.*,ARNOMBRE,'AAAAAAA' LOTE,'N' DEV, ARDTTEC5,");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO =DTTIPPRO AND ASCLAVEO = DTCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END C2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE ARCLAVE3 END C3,TTVALORF ");
                sSql.AppendLine("  FROM FACTURADT WITH(NOLOCK)");
                sSql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (DTCODEMP = ARCODEMP AND DTTIPPRO = ARTIPPRO AND DTCLAVE1 = ARCLAVE1 AND DTCLAVE2 = ARCLAVE2 AND DTCLAVE3 = ARCLAVE3 AND DTCLAVE4 = ARCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (ARCODEMP = TACODEMP AND ARTIPPRO = TATIPPRO)");
                sSql.AppendLine(" LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP AND TTCODTAB = 'IMPF' AND TTCODCLA = ARCDIMPF)");
                sSql.AppendLine(" WHERE DTCODEMP = @p0");
                sSql.AppendLine("   AND DTTIPFAC = @p1");
                sSql.AppendLine("   AND DTNROFAC = @p2");

                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text, DTCODEMP, DTTIPFAC, DTNROFAC);
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
        public static DataTable GetFacturaDTIMP(SessionManager oSessionManager, string DTCODEMP, string DTTIPFAC, int DTNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT FACTURADT.*,ARNOMBRE,'AAAAAAA' LOTE,'N' DEV, ARDTTEC5,");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO =DTTIPPRO AND ASCLAVEO = DTCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END C2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE ARCLAVE3 END C3,TTVALORN TARIFA,TTCODCLA COD_TARIFA ");
                sSql.AppendLine("  FROM FACTURADT WITH(NOLOCK)");
                sSql.AppendLine(" INNER JOIN ARTICULO WITH(NOLOCK) ON (DTCODEMP = ARCODEMP AND DTTIPPRO = ARTIPPRO AND DTCLAVE1 = ARCLAVE1 AND DTCLAVE2 = ARCLAVE2 AND DTCLAVE3 = ARCLAVE3 AND DTCLAVE4 = ARCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (ARCODEMP = TACODEMP AND ARTIPPRO = TATIPPRO)");
                sSql.AppendLine(" INNER JOIN TB_IMPUESTOSXFACTURA WITH(NOLOCK) ON (IF_CODEMP = DTCODEMP AND IF_NROFAC = DTNROFAC AND IF_TIPFAC = DTTIPFAC AND IF_ITEM = DTNROITM)");
                sSql.AppendLine(" INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB ='IMPF' AND TTCODEMP = DTCODEMP AND TTCODCLA = IF_CODIMP)");
                sSql.AppendLine(" WHERE DTCODEMP = @p0");
                sSql.AppendLine("   AND DTTIPFAC = @p1");
                sSql.AppendLine("   AND DTNROFAC = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, DTCODEMP, DTTIPFAC, DTNROFAC);
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
        public static DataTable GetPagos(SessionManager oSessionManager, string PGCODEMP, string PGTIPFAC, int PGNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT PGFACTUR.*, B.TTDESCRI DETALLE,A.TTDESCRI PAGO ");
                sSql.AppendLine(" FROM PGFACTUR WITH(NOLOCK)");
                sSql.AppendLine(" INNER JOIN TBTABLAS A WITH(NOLOCK) ON (A.TTCODEMP = PGCODEMP AND TTCODTAB = 'PAGO' AND TTCODCLA = PGTIPPAG )    ");
                sSql.AppendLine(" LEFT OUTER JOIN TBTABLAS B WITH(NOLOCK) ON (B.TTCODEMP = PGCODEMP AND B.TTCODTAB = A.TTVALORC AND B.TTCODCLA = PGDETTPG) ");
                sSql.AppendLine(" WHERE PGCODEMP =@p0");
                sSql.AppendLine("   AND PGTIPFAC =@p1");
                sSql.AppendLine("   AND PGNROFAC =@p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PGCODEMP, PGTIPFAC, PGNROFAC);
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
        public static int InsertFacturaHD(SessionManager oSessionManager,string HDCODEMP, string HDTIPFAC, int HDNROFAC, DateTime HDFECFAC, int HDCODCLI, int HDCODSUC, DateTime? HDFECVEN,
                                          double HDSUBTOT, double HDTOTDES, double HDTOTIVA, double HDTOTFAC, string HDMONEDA, double HDSUBTTL, double HDTOTDSL,
                                          double HDTOTIVL, double HDTOTFCL, double HDSUBTTD, double HDTOTDSD, double HDTOTIVD, double HDTOTFCD, string HDCODNIT,
                                          string HDCDPAIS, string HDCIUDAD, string HDMODDES, string HDTERDES, string HDTERPAG, int HDAGENTE, string HDRSDIAN,
                                          string HDCATEGO, string HDCAJCOM, string HDNROALJ, string HDTIPALJ, string HDDIVISI, double HDTOTOTR, double HDTOTSEG,
                                          double HDTOTFLE, string HDCNTFIS, string HDOBSERV, double HDTOTICA, double HDTOTFTE, double HDTOTFIV, string HDESTADO,
                                          string HDCAUSAE, string HDNMUSER, string HDTRASMI, DateTime? HDFECCIE, string HDTIPDEV, int? HDNRODEV, string HDTFCDEV,
                                          int? HDFACDEV, string HDNROCAJA, int? LH_LSTPAQ,string HD_TIPREM, int? HD_NROREM)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO FACTURAHD (HDCODEMP, HDTIPFAC, HDNROFAC, HDFECFAC, HDCODCLI, HDCODSUC, HDFECVEN, HDSUBTOT, HDTOTDES, HDTOTIVA, HDTOTFAC, HDMONEDA, HDSUBTTL, HDTOTDSL,");
                sSql.AppendLine("                       HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD, HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, HDRSDIAN,");
                sSql.AppendLine("                       HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG, HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,");
                sSql.AppendLine("                       HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV, HDFACDEV, HDNROCAJA, LH_LSTPAQ, HD_TIPREM, HD_NROREM, HDFECMOD, HDFECING) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,");
                sSql.AppendLine(" @p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,");
                sSql.AppendLine(" @p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,");
                sSql.AppendLine(" @p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,@p50,@p51,@p52,@p53,GETDATE(),GETDATE())");


                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text,
                        HDCODEMP, HDTIPFAC, HDNROFAC, HDFECFAC, HDCODCLI, HDCODSUC, HDFECVEN, HDSUBTOT, HDTOTDES, HDTOTIVA, HDTOTFAC, HDMONEDA, HDSUBTTL, HDTOTDSL,
                        HDTOTIVL, HDTOTFCL, HDSUBTTD, HDTOTDSD, HDTOTIVD, HDTOTFCD, HDCODNIT, HDCDPAIS, HDCIUDAD, HDMODDES, HDTERDES, HDTERPAG, HDAGENTE, HDRSDIAN,
                        HDCATEGO, HDCAJCOM, HDNROALJ, HDTIPALJ, HDDIVISI, HDTOTOTR, HDTOTSEG, HDTOTFLE, HDCNTFIS, HDOBSERV, HDTOTICA, HDTOTFTE, HDTOTFIV, HDESTADO,
                        HDCAUSAE, HDNMUSER, HDTRASMI, HDFECCIE, HDTIPDEV, HDNRODEV, HDTFCDEV, HDFACDEV, HDNROCAJA, LH_LSTPAQ, HD_TIPREM, HD_NROREM);
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
        public static int InsertFacturaDT(SessionManager oSessionManager, string DTCODEMP, string DTTIPFAC, int DTNROFAC, int DTNROITM, int? DTPEDIDO, int? DTLINNUM, string DTTIPPRO,
                                          string DTCLAVE1, string DTCLAVE2, string DTCLAVE3, string DTCLAVE4, string DTCODCAL, string DTUNDPED, double DTCANPED, double DTCANTID, double DTCANKLG,
                                          string DTLISPRE, DateTime? DTFECPRE, double DTPRELIS, double DTPRECIO, double DTDESCUE, double DTSUBTOT, double DTTOTDES, double DTTOTIVA, double DTTOTFAC,
                                          double DTSUBTTL, double DTTOTDSL, double DTTOTIVL, double DTTOTFCL, double DTSUBTTD, double DTTOTDSD, double DTTOTIVD, double DTTOTFCD, int? DTNROMOV,
                                          int? DTITMMOV, string DTESTADO, string DTCAUSAE, string DTNMUSER, int? DTNROELE, string DTTIPPED, string DTTIPLIN, int DTCODDES, string DTNROCAJA)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO FACTURADT (DTCODEMP, DTTIPFAC, DTNROFAC, DTNROITM, DTPEDIDO, DTLINNUM, DTTIPPRO, DTCLAVE1, DTCLAVE2, DTCLAVE3, DTCLAVE4, DTCODCAL, DTUNDPED, DTCANPED, DTCANTID, DTCANKLG,");
                sSql.AppendLine("                       DTLISPRE, DTFECPRE, DTPRELIS, DTPRECIO, DTDESCUE, DTSUBTOT, DTTOTDES, DTTOTIVA, DTTOTFAC, DTSUBTTL, DTTOTDSL, DTTOTIVL, DTTOTFCL, DTSUBTTD, DTTOTDSD, DTTOTIVD,"); 
                sSql.AppendLine("                       DTTOTFCD, DTNROMOV, DTITMMOV, DTESTADO, DTCAUSAE, DTNMUSER, DTNROELE, DTTIPPED, DTTIPLIN, DTCODDES, DTNROCAJA,DTFECING, DTFECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,");
                sSql.AppendLine(" @p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,");
                sSql.AppendLine(" @p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,
                    DTCODEMP, DTTIPFAC, DTNROFAC, DTNROITM, DTPEDIDO, DTLINNUM, DTTIPPRO, DTCLAVE1, DTCLAVE2, DTCLAVE3, DTCLAVE4, DTCODCAL, DTUNDPED, DTCANPED, DTCANTID, DTCANKLG,
                    DTLISPRE, DTFECPRE, DTPRELIS, DTPRECIO, DTDESCUE, DTSUBTOT, DTTOTDES, DTTOTIVA, DTTOTFAC, DTSUBTTL, DTTOTDSL, DTTOTIVL, DTTOTFCL, DTSUBTTD, DTTOTDSD, DTTOTIVD,
                    DTTOTFCD, DTNROMOV, DTITMMOV, DTESTADO, DTCAUSAE, DTNMUSER, DTNROELE, DTTIPPED, DTTIPLIN, DTCODDES, DTNROCAJA);
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
        public static int InsertPagos(SessionManager oSessionManager, string PGCODEMP, string PGTIPFAC, int PGNROFAC, int PGNROITM, string PGTIPPAG, string PGDETTPG, double PGVLRPAG,
                                      string PGSOPORT, DateTime? PGSOPFEC, string PGPAGIMP, string PGESTADO, string PGCAUSAE, string PGNMUSER, string PGNROCAJA)
        {
            StringBuilder sSql = new StringBuilder();
            try { 
                sSql.AppendLine("INSERT INTO PGFACTUR (PGCODEMP, PGTIPFAC, PGNROFAC, PGNROITM, PGTIPPAG, PGDETTPG, PGVLRPAG,");
                sSql.AppendLine("PGSOPORT, PGSOPFEC, PGPAGIMP, PGESTADO, PGCAUSAE, PGNMUSER, PGNROCAJA, PGFECING, PGFECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PGCODEMP, PGTIPFAC, PGNROFAC, PGNROITM, PGTIPPAG, PGDETTPG, PGVLRPAG,
                                                PGSOPORT, PGSOPFEC, PGPAGIMP, PGESTADO, PGCAUSAE, PGNMUSER, PGNROCAJA);
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
        public static int AnularFacturaHD(SessionManager oSessionManager,string HDCODEMP, string HDTIPFAC, int HDNROFAC,string HDCAUSAE, string HDNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE FACTURAHD SET HDESTADO = 'AN', HDFECMOD = GETDATE(), HDCAUSAE =@p3,HDNMUSER=@p4");
                sSql.AppendLine(" WHERE HDCODEMP =@p0");
                sSql.AppendLine("   AND HDTIPFAC =@p1");
                sSql.AppendLine("   AND HDNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC, HDCAUSAE, HDNMUSER);
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
        public static int AnularFacturaDT(SessionManager oSessionManager, string DTCODEMP, string DTTIPFAC, int DTNROFAC, string DTNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE FACTURADT SET DTESTADO = 'AN', DTFECMOD = GETDATE(), DTNMUSER=@p3");
                sSql.AppendLine(" WHERE DTCODEMP =@p0");
                sSql.AppendLine("   AND DTTIPFAC =@p1");
                sSql.AppendLine("   AND DTNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DTCODEMP, DTTIPFAC, DTNROFAC, DTNMUSER);
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
        public static int AnularPagos(SessionManager oSessionManager, string PGCODEMP, string PGTIPFAC, int PGNROFAC, string PGNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PGFACTUR SET PGESTADO = 'AN', PGFECMOD = GETDATE(), PGNMUSER=@p3");
                sSql.AppendLine(" WHERE PGCODEMP =@p0");
                sSql.AppendLine("   AND PGTIPFAC =@p1");
                sSql.AppendLine("   AND PGNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PGCODEMP, PGTIPFAC, PGNROFAC, PGNMUSER);
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
        public static int UpdateRemisionHD(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string HD_ESTADO, string HD_CAUSAE,string HD_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE FACTURAHD SET HDESTADO =@p3, HDCAUSAE=@p4, HDNMUSER=@p5, HDFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE HDCODEMP =@p0");
                sSql.AppendLine("   AND HDTIPFAC =@p1");
                sSql.AppendLine("   AND HDNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC, HD_ESTADO, HD_CAUSAE, HD_NMUSER);
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
        public static int UpdateFacturaHD(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int? HDNROFAC,string HDTFCDEV, int? HDFACDEV)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE FACTURAHD SET HDTFCDEV =@p3, HDFACDEV=@p4");
                sSql.AppendLine(" WHERE HDCODEMP =@p0");
                sSql.AppendLine("   AND HDTIPFAC =@p1");
                sSql.AppendLine("   AND HDNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC, HDTFCDEV, HDFACDEV);
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
        public static int UpdateFacturaHD(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int? HDNROFAC, string HDTFCDEV, int? HDFACDEV,string HDESTADO,string HDUSUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE FACTURAHD SET HDTFCDEV =@p3, HDFACDEV=@p4,HDESTADO=@p5,HDNMUSER=@p6");
                sSql.AppendLine(" WHERE HDCODEMP =@p0");
                sSql.AppendLine("   AND HDTIPFAC =@p1");
                sSql.AppendLine("   AND HDNROFAC =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC, HDTFCDEV, HDFACDEV, HDESTADO, HDUSUARIO);
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
        public static int InsertImpuestosxFactura(SessionManager oSessionManager, string IF_CODEMP, string IF_TIPFAC, int IF_NROFAC, int IF_ITEM, string IF_CODIMP, double IF_VALOR)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_IMPUESTOSXFACTURA (IF_CODEMP,IF_TIPFAC,IF_NROFAC,IF_ITEM,IF_CODIMP,IF_VALOR,IF_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IF_CODEMP, IF_TIPFAC, IF_NROFAC, IF_ITEM, IF_CODIMP, IF_VALOR);
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
        public static DataTable GetDifInvFac(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, DateTime? inFecini, DateTime? inFecFin)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                /*
                sSql.AppendLine("SELECT TANOMBRE,DTTIPPRO,DTCLAVE1,DTCLAVE2,DTCLAVE3,DTCLAVE4,BBCANTID,DTCANTID,HDNROFAC,HDFECFAC, ");
                sSql.AppendLine(" CASE ");
                sSql.AppendLine(" WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE2                   ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE DTCLAVE2 END CLAVE2,                       ");
                sSql.AppendLine(" CASE ");
                sSql.AppendLine(" WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = DTCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = DTTIPPRO AND ASCLAVEO = DTCLAVE3                    ");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE DTCLAVE3 END CLAVE3                         ");
                sSql.AppendLine("  FROM FACTURAHD WITH(NOLOCK)                                                                    ");
                sSql.AppendLine(" INNER JOIN FACTURADT WITH(NOLOCK) ON(HDCODEMP = DTCODEMP AND HDTIPFAC = DTTIPFAC AND HDNROFAC = DTNROFAC)    ");
                sSql.AppendLine(" INNER JOIN BALANBOD WITH(NOLOCK) ON( DTCODEMP = BBCODEMP                                                     ");
                sSql.AppendLine("                     AND DTTIPPRO = BBTIPPRO                                                     ");
                sSql.AppendLine("                     AND DTCLAVE1 = BBCLAVE1                                                     ");
                sSql.AppendLine("                     AND DTCLAVE2 = BBCLAVE2                                                     ");
                sSql.AppendLine("                     AND DTCLAVE3 = BBCLAVE3                                                     ");
                sSql.AppendLine("                     AND DTCLAVE4 = BBCLAVE4)                                                    ");
                sSql.AppendLine("  INNER JOIN TBTIPPRO WITH(NOLOCK) ON(DTCODEMP = TACODEMP AND DTTIPPRO = TATIPPRO)               ");                
                sSql.AppendLine(" WHERE HDCODEMP = @p0");
                sSql.AppendLine("   AND BBBODEGA IN (SELECT TFBODEGA FROM TBTIPFAC WITH(NOLOCK) WHERE TFCODEMP = HDCODEMP AND TFTIPFAC=@p1)");
                sSql.AppendLine("   AND BBCANTID < 0 ");
                sSql.AppendLine("   AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,@p2-80,101) AND CONVERT(DATE,@p3,101) ");
                sSql.AppendLine("   ORDER BY DTTIPPRO,DTCLAVE1,DTCLAVE2,DTCLAVE3,DTCLAVE4,HDFECFAC DESC");
                */

                sSql.AppendLine("SELECT ARNOMBRE,BBBODEGA,TANOMBRE,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4,BBCANTID,BBCANTID,0 HDNROFAC,GETDATE() HDFECFAC, ");
                sSql.AppendLine("CASE ");
                sSql.AppendLine("WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = BBCODEMP ");
                sSql.AppendLine("              AND ASTIPPRO = BBTIPPRO AND ASCLAVEO = BBCLAVE2                   ");
                sSql.AppendLine("              AND ASNIVELC = 2) ELSE BBCLAVE2 END CLAVE2,                       ");
                sSql.AppendLine("CASE ");
                sSql.AppendLine("WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = BBCODEMP ");
                sSql.AppendLine("              AND ASTIPPRO = BBTIPPRO AND ASCLAVEO = BBCLAVE3                    ");
                sSql.AppendLine("              AND ASNIVELC = 3) ELSE BBCLAVE3 END CLAVE3                         ");
                sSql.AppendLine(" FROM BALANBOD WITH(NOLOCK)                                                                    ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = BBCODEMP AND ARCLAVE1 = BBCLAVE1 AND ARCLAVE2 = BBCLAVE2 AND ARCLAVE3 = BBCLAVE3 AND ARCLAVE4 = BBCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON(BBCODEMP = TACODEMP AND BBTIPPRO = TATIPPRO)               ");
                sSql.AppendLine("WHERE BBCODEMP = @p0");
                sSql.AppendLine("AND BBBODEGA IN (SELECT TFBODEGA FROM TBTIPFAC WITH(NOLOCK) WHERE TFCODEMP = BBCODEMP AND TFTIPFAC=@p1)   ");
                sSql.AppendLine("AND BBCANTID < 0    ");


                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC);
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
        public static int GetFacturasCierre(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, DateTime? inFecini)
        {
            StringBuilder sSql = new StringBuilder();
            try
            { 
                sSql.AppendLine(" SELECT COUNT(*) tot ");
                sSql.AppendLine("  FROM FACTURAHD WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE HDCODEMP = @p1 ");
                sSql.AppendLine("   AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,@p0-30,101) AND CONVERT(DATE,@p0-1,101)");
                sSql.AppendLine("   AND HDFECCIE IS NULL AND HDTIPFAC=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inFecini, HDCODEMP, HDTIPFAC));
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
        public static int CerrarFacturas(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, DateTime? inFecini)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine(" UPDATE FACTURAHD SET HDFECCIE = GETDATE(), HDFECMOD = GETDATE() ");
                sSql.AppendLine(" WHERE HDCODEMP = @p1 ");
                sSql.AppendLine("   AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,@p0,101) AND CONVERT(DATE,@p0,101)");
                sSql.AppendLine("   AND HDFECCIE IS NULL AND HDTIPFAC=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, inFecini, HDCODEMP, HDTIPFAC);
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
        public static string GetEstadoFatcuraHD(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int HDNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT HDESTADO");  
                sSql.AppendLine("  FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine(" WHERE HDCODEMP = @p0");
                sSql.AppendLine("   AND HDTIPFAC = @p1");
                sSql.AppendLine("   AND HDNROFAC = @p2");

                return Convert.ToString(DBAccess.GetScalar(oSessionManager,sSql.ToString(),CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC));
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
        public static int InsertTasas(SessionManager oSessionManager, string DTCODEMP, string DTTIPFAC, int DTNROFAC, int DTNROITM, int DTLINNUM, string PMMONEDA, double PMTASA, double PMPRECIO, double PMPRELIS, double PMSUBTOT, string PMUSUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_FACTURADT_MONEDA (DTCODEMP,DTTIPFAC,DTNROFAC,DTNROITM,DTLINNUM,PMMONEDA,PMTASA,PMPRECIO,PMPRELIS,PMSUBTOT,PMUSUARIO) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DTCODEMP, DTTIPFAC, DTNROFAC, DTNROITM, DTLINNUM, PMMONEDA, PMTASA, PMPRECIO, PMPRELIS, PMSUBTOT, PMUSUARIO);
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
        //Bonos
        #region
        public static int InsertBono(SessionManager oSessionManager, string DTCODEMP, int T_NROBONO, string DTTIPFAC, int DTNROFAC, int DTNROITM, string T_CODIGO,
                                     double T_VALOR, string T_ESTADO, string T_CEDULA, double T_SALDO, string T_CDUSER, string T_DESCRIPCION)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_BONOCANJE (DTCODEMP,T_NROBONO,DTTIPFAC,DTNROFAC,DTNROITM,T_CODIGO,T_VALOR,T_ESTADO,T_CEDULA,T_SALDO,T_CDUSER,T_DESCRIPCION,T_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, DTCODEMP, T_NROBONO, DTTIPFAC, DTNROFAC, DTNROITM, T_CODIGO, T_VALOR, T_ESTADO, T_CEDULA, T_SALDO, T_CDUSER, T_DESCRIPCION);
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
        public static int ExisteBono(SessionManager oSessionManager, string DTCODEMP, string T_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_BONOCANJE WHERE T_CODIGO = @p0");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, T_CODIGO));
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
        public static double GetSaldoBono(SessionManager oSessionManager, string DTCODEMP, string T_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT T_SALDO FROM TB_BONOCANJE WHERE T_CODIGO = @p0");
                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, T_CODIGO));
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
        public static DataTable GetBonos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT T_NROBONO,T_CODIGO,T_VALOR,T_SALDO ");
                sSql.AppendLine("FROM TB_BONOCANJE WITH(NOLOCK)");
                sSql.AppendLine("WHERE T_SALDO > 0 "+filter);
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
        #region
        public static DataTable GetDetalleMovimientos(SessionManager oSessionManager, string CODEMP, string DTTIPFAC,int DTNROFAC, int DTNROITM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {

                sSql.AppendLine("SELECT MBBODEGA,MBCANTID,MLCDLOTE,MLCANTID");
                sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN MOVIMLOT WITH(NOLOCK) ON (MBCODEMP = MLCODEMP AND MBIDMOVI = MLIDMOVI AND MBIDITEM = MLIDITEM )");
                sSql.AppendLine("WHERE MBCODEMP =@p0 ");
                sSql.AppendLine(" AND MBIDMOVI = (SELECT DTNROMOV FROM FACTURADT WITH(NOLOCK) WHERE DTCODEMP = MBCODEMP AND DTTIPFAC = @p1 AND DTNROFAC =@p2 AND DTNROITM = MBIDITEM ) ");
                sSql.AppendLine("AND MBIDITEM = @p3");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, DTTIPFAC, DTNROFAC, DTNROITM);
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
        //Exportacion
        #region
        public static DataTable GetAdicionales(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int HDNROFAC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTCODCLA AFCONCEPTO,AFVALOR,TTDESCRI CONCEPTO");
                sSql.AppendLine("FROM  TBTABLAS  WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TB_ADDFACTURA WITH(NOLOCK) ON (TTCODEMP = HDCODEMP  AND TTCODCLA = AFCONCEPTO AND HDCODEMP = @p0 AND HDTIPFAC = @p1 AND HDNROFAC = @p2)");
                sSql.AppendLine(" WHERE TTCODTAB = 'ADDFAC'");                               

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
        public static int InsertAdicionales(SessionManager oSessionManager, string HDCODEMP, string HDTIPFAC, int HDNROFAC, string AFCONCEPTO, double AFVALOR, string AFESTADO,string AFUSUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_ADDFACTURA (HDCODEMP,HDTIPFAC,HDNROFAC,AFCONCEPTO,AFVALOR,AFESTADO,AFUSUARIO,AFFECING,AFFECMOD) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, HDCODEMP, HDTIPFAC, HDNROFAC, AFCONCEPTO, AFVALOR, AFESTADO, AFUSUARIO);
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
