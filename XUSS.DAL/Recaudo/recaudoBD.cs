using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Recaudo
{
    public class recaudoBD
    {
        public static DataTable GetRecaudo(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT DISTINCT TB_RECAUDOHD.*,(TRNOMBRE +' '+ISNULL(TRNOMBR2,' ')+' '+ISNULL(TRAPELLI,' ')) CLIENTE  ");
                sql.AppendLine("FROM TB_RECAUDOHD WITH(NOLOCK) ");                
                sql.AppendLine("INNER JOIN TB_RECAUDO WITH(NOLOCK) ON (RC_CODEMP = RH_CODEMP AND RC_NRORECIBO = RH_NRORECIBO)");
                sql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC)");
                sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (RC_CODEMP = TRCODEMP AND HDCODCLI = TRCODTER)");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                sql.AppendLine("ORDER BY RH_NRORECIBO");
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
        public static DataTable GetRecaudoDT(SessionManager oSessionManager,string RC_CODEMP,int RC_NRORECIBO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_RECAUDO.*,HDFECFAC,TRNOMBRE +' '+ISNULL(TRNOMBR2,' ')+' '+ISNULL(TRAPELLI,' ') CLIENTE,A.TTDESCRI CONCEPTO,HDNROFAC,HDTIPFAC,(HDTIPFAC+'-'+CAST(HDNROFAC AS VARCHAR)) DOCUMENTO,0.00 SALDO, '                             ' NH_NRONOTA,0.00 RECAUDO,0.00 HDTOTFAC");
                sql.AppendLine("FROM TB_RECAUDO WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN FACTURAHD WITH(NOLOCK) ON (RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC)");
                sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (RC_CODEMP = TRCODEMP AND HDCODCLI = TRCODTER)");
                sql.AppendLine("INNER JOIN TBTABLAS A WITH(NOLOCK) ON (TTCODEMP = RC_CODEMP AND TTCODCLA = RC_CONCEPTO AND TTCODTAB = 'CONREC')");
                sql.AppendLine("WHERE RC_CODEMP =@p0 AND RC_NRORECIBO =@p1");                
                sql.AppendLine("ORDER BY RC_NRORECIBO");
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text,RC_CODEMP,RC_NRORECIBO);
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
        public static DataTable GetItemsRecaudo(SessionManager oSessionManager, int InRecuado)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT RC_CODEMP,RC_NRORECIBO,RC_TIPFAC,RC_NROFAC,RC_CONCEPTO,RC_VALOR,RC_USUARIO,RC_ESTADO,RC_FECREC,RC_TIPFACSF,RC_NROFACSF");
                sSql.AppendLine("  FROM TB_RECAUDO ");
                sSql.AppendLine(" WHERE RC_NRORECIBO=@p0 ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, InRecuado);
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
        public static IDataReader GetDatosFactura(SessionManager oSessionManager, string inTfactura, int inFactura)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TRNOMBRE +' '+ISNULL(TRNOMBR2,' ')+' '+ISNULL(TRAPELLI,' ') CLIENTE,TRCODNIT,((FACTURAHD.HDSUBTOT*(SELECT CASE WHEN CONVERT(DATE,HDFECFAC,101) <= CONVERT(DATE,'01/01/2017',101) THEN 16 ELSE 19 END ) )/100)+HDSUBTOT HDTOTFAC,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WITH(NOLOCK) WHERE RC_TIPFAC=HDTIPFAC AND RC_NROFAC=HDNROFAC AND RC_ESTADO='AC'),0) RC,");
                sSql.AppendLine("ISNULL((SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO <> 'AN'),0) TDEV,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF =HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO ='AC'),0) RECA_SF");
                sSql.AppendLine(" FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (HDCODEMP = TRCODEMP AND HDCODCLI = TRCODTER)");
                sSql.AppendLine(" WHERE HDTIPFAC =@p0 AND HDNROFAC=@p1");

                return DBAccess.GetDataReader(oSessionManager,sSql.ToString(),CommandType.Text,inTfactura,inFactura);
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
        public static int ExisteFactura(SessionManager oSessionManager, string inTfactura, int inFactura)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*)");
                sSql.AppendLine(" FROM FACTURAHD WITH(NOLOCK)");                
                sSql.AppendLine(" WHERE HDTIPFAC =@p0 AND HDNROFAC=@p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inTfactura, inFactura));
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
        public static int InsertRecaudo(SessionManager oSessionManager, string RC_CODEMP, int RC_NRORECIBO, string RC_TIPFAC, int RC_NROFAC, string RC_CONCEPTO, double RC_VALOR,
                                        string RC_USUARIO, string RC_ESTADO, DateTime RC_FECREC, string RC_TIPFACSF, int RC_NROFACSF, string RC_NRORECFISICO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_RECAUDO (RC_CODEMP,RC_NRORECIBO,RC_TIPFAC,RC_NROFAC,RC_CONCEPTO,RC_VALOR,RC_USUARIO,RC_ESTADO,RC_FECING,RC_FECMOD,RC_FECREC,RC_TIPFACSF,RC_NROFACSF,RC_NRORECFISICO)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE(),GETDATE(),@p8,@p9,@p10,@p11)");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,RC_CODEMP,RC_NRORECIBO,RC_TIPFAC,RC_NROFAC,RC_CONCEPTO,RC_VALOR,RC_USUARIO,
                                                RC_ESTADO, RC_FECREC, RC_TIPFACSF, RC_NROFACSF, RC_NRORECFISICO);
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
        public static int InsertRecaudoHD(SessionManager oSessionManager, string RH_CODEMP, int RH_NRORECIBO, DateTime RH_FECHA, string RH_NRORECFISICO, string RH_OBSERVACIONES, double RH_VALOR,
                                          int RH_BANCO, string RH_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_RECAUDOHD (RH_CODEMP,RH_NRORECIBO,RH_FECHA,RH_NRORECFISICO,RH_OBSERVACIONES,RH_VALOR,RH_BANCO,RH_ESTADO,RH_FECING)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RH_CODEMP, RH_NRORECIBO, RH_FECHA, RH_NRORECFISICO, RH_OBSERVACIONES, RH_VALOR,
                                                RH_BANCO,RH_ESTADO);
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
        public static DataTable GetSaldoFavor(SessionManager oSessionManager,int inTercero)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT *,TFACT-((ISNULL(RECAUDO,0)+ISNULL(TDEV,0)) - ISNULL(DESCONTADO,0)) SF");
                sSql.AppendLine("FROM (");
                sSql.AppendLine("SELECT A.TRCODNIT,A.TRCODTER,HDTOTFAC,HDSUBTOT,HDNROFAC,HDTIPFAC,((HDSUBTOT*CASE WHEN HDFECFAC < '01/01/2017' THEN 16 ELSE 19 END/100)+HDSUBTOT) TFACT,");
                sSql.AppendLine("CASE WHEN DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),GETDATE()) < 0 THEN 0 ELSE DATEDIFF(DAY,(HDFECFAC+D.TTVALORN),GETDATE()) END DIAS,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC') RECAUDO,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND X.HDESTADO NOT IN ('AN')) TDEV,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF =HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO ='AC') DESCONTADO");
                sSql.AppendLine("FROM FACTURAHD");
                sSql.AppendLine("INNER JOIN TERCEROS A WITH(NOLOCK) ON (A.TRCODEMP = HDCODEMP  AND A.TRCODTER = HDCODCLI)");
                sSql.AppendLine("INNER JOIN TERCEROS B WITH(NOLOCK) ON (B.TRCODEMP = HDCODEMP  AND B.TRCODTER = HDAGENTE)");
                sSql.AppendLine("INNER JOIN PGFACTUR WITH(NOLOCK) ON (PGCODEMP = HDCODEMP AND PGTIPFAC = HDTIPFAC AND PGNROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTABLAS C WITH(NOLOCK) ON (C.TTCODEMP = HDCODEMP  AND C.TTCODCLA = PGTIPPAG AND C.TTCODTAB = 'PAGO')");
                sSql.AppendLine("INNER JOIN TBTABLAS D WITH(NOLOCK) ON (D.TTCODEMP = HDCODEMP  AND D.TTCODCLA = PGDETTPG AND D.TTCODTAB = C.TTVALORC)");
                sSql.AppendLine("WHERE HDESTADO <> 'AN'");
                sSql.AppendLine(") TTTTT");
                //sSql.AppendLine("WHERE TRCODTER = (SELECT HDCODCLI FROM FACTURAHD WHERE HDNROFAC = @p0)");
                sSql.AppendLine("WHERE TRCODTER = @p0");
                sSql.AppendLine(") YYYY");
                sSql.AppendLine("WHERE SF < 0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inTercero);
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
        public static int AnularRecaudo(SessionManager oSessionManager, string RC_CODEMP,int RC_NRORECIBO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_RECAUDO SET RC_ESTADO ='AN', RC_FECMOD = GETDATE() WHERE RC_CODEMP=@p0 AND RC_NRORECIBO=@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,RC_CODEMP,RC_NRORECIBO);
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
        public static int AnularRecaudoHD(SessionManager oSessionManager, string RH_CODEMP, int RH_NRORECIBO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_RECAUDOHD SET RH_ESTADO ='AN' WHERE RH_CODEMP=@p0 AND RH_NRORECIBO=@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RH_CODEMP, RH_NRORECIBO);
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
        public static DataTable GetDetalle(SessionManager oSessionManager, string inIdentificacion)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT * FROM (");
                sSql.AppendLine("SELECT XX_TMP_1.*,TTCODCLA,TTDESCRI,NULL NH_NRONOTA, 0 VLR_NOTA FROM (");
                sSql.AppendLine("SELECT *,");
                sSql.AppendLine("CASE WHEN (((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)-(ISNULL(TDEV,0)-ISNULL(RECAUDO,0)) < 0 THEN ((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)-(ISNULL(TDEV,0)-ISNULL(RECAUDO,0))) -(ISNULL(RECA_SF,0)*-1) ELSE 0 END SAL_FAVOR_APL,");
                sSql.AppendLine("CASE WHEN ((((HDSUBTOT*POR_IVA)/100+HDSUBTOT)+ISNULL(RECA_SF,0)+ISNULL(ND,0))-(ISNULL(RECAUDO,0)+ISNULL(TDEV,0)+ISNULL(NC,0)))< 0 THEN (((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+ISNULL(ND,0))-(ISNULL(TDEV,0)+ISNULL(RECAUDO,0)+ISNULL(TDEV,0)+ISNULL(NC,0))) -(ISNULL(RECA_SF,0)*-1) ELSE ((((HDSUBTOT*POR_IVA)/100)+HDSUBTOT)+ISNULL(RECA_SF,0)+ISNULL(ND,0))-(ISNULL(RECAUDO,0)+ISNULL(TDEV,0)+ISNULL(NC,0)) END SALDO,");
                sSql.AppendLine("(((HDSUBTOT*POR_IVA)/100)+HDSUBTOT) VLR_CAR");
                sSql.AppendLine("FROM");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT HDCODEMP,HDSUBTOT,(HDTIPFAC+'-'+CAST(HDNROFAC AS VARCHAR) ) NRO_FAC,HDTIPFAC, HDNROFAC,HDFECFAC,HDTOTFAC,HDTOTIVA,");
                sSql.AppendLine("(SELECT top 1 (SELECT  TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB ='IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC) POR_IVA,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC =HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO ='AC'),0) RECAUDO,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN') TDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN') IDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC ='59' AND HDESTADO<> 'AN') SDEV,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WITH(NOLOCK) WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF =HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO ='AC') RECA_SF, ");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON (TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA) WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO ='AC'  AND HDCODCLI = TRCODTER) NC,");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON(TB_NOTADEBHD.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA) WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO = 'AC') ND");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (HDCODEMP =TFCODEMP AND HDTIPFAC = TFTIPFAC )");
                sSql.AppendLine("WHERE HDCODCLI IN (SELECT TRCODTER FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT = @p0)");
                sSql.AppendLine("AND TFCLAFAC in (1) AND HDESTADO NOT IN ('AN') ");
                sSql.AppendLine(") XX_TMP)XX_TMP_1");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS A WITH(NOLOCK) ON (A.TTCODTAB ='CONREC')");
                sSql.AppendLine("WHERE SALDO <> 0");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT XX_TMP_1.*, -3 TTCODCLA, 'Nota Credito' TTDESCRI, NH_TIPFAC + '-' +CAST(NH_NRONOTA AS VARCHAR) NH_NRONOTA, SUM(ND_VALOR) * -1 VLR_NOTA FROM(");
                sSql.AppendLine("SELECT *, 0 SAL_FAVOR_APL,");
                sSql.AppendLine("CASE WHEN((((HDSUBTOT * POR_IVA) / 100 + HDSUBTOT) + ISNULL(RECA_SF, 0) + ISNULL(ND, 0)) - (ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0))) < 0 THEN(((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(ND, 0)) - (ISNULL(TDEV, 0) + ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0))) - (ISNULL(RECA_SF, 0) * -1) ELSE((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(RECA_SF, 0) + ISNULL(ND, 0)) - (ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0)) END SALDO,");
                sSql.AppendLine("(((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) VLR_CAR");
                sSql.AppendLine("FROM");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT HDCODEMP, HDSUBTOT, (HDTIPFAC + '-' + CAST(HDNROFAC AS VARCHAR)) NRO_FAC, HDTIPFAC, HDNROFAC, HDFECFAC, HDTOTFAC, HDTOTIVA,");
                sSql.AppendLine("(SELECT top 1(SELECT  TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB = 'IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC) POR_IVA,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO = 'AC'),0) RECAUDO,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO <> 'AN') TDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN') IDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN') SDEV,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WITH(NOLOCK) WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF = HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO = 'AC') RECA_SF, ");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON(TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA AND TB_NOTAHD.NH_TIPFAC = TB_NOTADT.NH_TIPFAC) WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO = 'AC') NC,");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON(TB_NOTADEBDT.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA AND TB_NOTADEBDT.NH_TIPFAC = TB_NOTADEBDT.NH_TIPFAC) WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND TB_NOTADEBDT.DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO = 'AC') ND");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC)");
                sSql.AppendLine("WHERE HDCODCLI IN(SELECT TRCODTER FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT = @p0)");
                sSql.AppendLine("AND TFCLAFAC in (1) AND HDESTADO NOT IN('AN') ");
                sSql.AppendLine(") XX_TMP)XX_TMP_1");
                sSql.AppendLine("INNER JOIN TB_NOTADT WITH(NOLOCK) ON(XX_TMP_1.HDCODEMP = TB_NOTADT.ND_CODEMP AND XX_TMP_1.HDTIPFAC = TB_NOTADT.DTTIPFAC AND XX_TMP_1.HDNROFAC = TB_NOTADT.DTNROFAC AND TB_NOTADT.ND_ESTADO = 'AC')");
                sSql.AppendLine("WHERE SALDO<> 0");
                sSql.AppendLine("GROUP BY HDCODEMP,HDSUBTOT,NRO_FAC,HDTIPFAC,HDNROFAC,HDFECFAC,HDTOTFAC,HDTOTIVA,POR_IVA,RECAUDO,TDEV,IDEV,SDEV,RECA_SF,SAL_FAVOR_APL,SALDO,VLR_CAR,NH_NRONOTA,NC,ND,NH_TIPFAC");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT XX_TMP_1.*,-4 TTCODCLA,'Nota Debito' TTDESCRI,NH_TIPFAC + '-' +CAST(NH_NRONOTA AS VARCHAR) NH_NRONOTA,SUM(ND_VALOR) VLR_NOTA FROM(");
                sSql.AppendLine("SELECT *,0 SAL_FAVOR_APL,");
                sSql.AppendLine("CASE WHEN((((HDSUBTOT* POR_IVA)/ 100 + HDSUBTOT)+ISNULL(RECA_SF, 0) + ISNULL(ND, 0))-(ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0)))< 0 THEN(((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(ND, 0)) - (ISNULL(TDEV, 0) + ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0))) - (ISNULL(RECA_SF, 0) * -1) ELSE((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(RECA_SF, 0) + ISNULL(ND, 0)) - (ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(NC, 0)) END SALDO,");
                sSql.AppendLine("                                          (((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) VLR_CAR");
                sSql.AppendLine("FROM");
                sSql.AppendLine("(");
                sSql.AppendLine("SELECT HDCODEMP, HDSUBTOT, (HDTIPFAC + '-' + CAST(HDNROFAC AS VARCHAR)) NRO_FAC, HDTIPFAC, HDNROFAC, HDFECFAC, HDTOTFAC, HDTOTIVA,");
                sSql.AppendLine("(SELECT top 1(SELECT  TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB = 'IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC) POR_IVA,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO = 'AC'),0) RECAUDO,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN') TDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN') IDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WITH(NOLOCK) WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN') SDEV,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WITH(NOLOCK) WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF = HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO = 'AC') RECA_SF, ");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON(TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA AND TB_NOTAHD.NH_TIPFAC = TB_NOTADT.NH_TIPFAC) WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO = 'AC') NC,");
                sSql.AppendLine("(SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON(TB_NOTADEBDT.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA AND TB_NOTADEBHD.NH_TIPFAC = TB_NOTADEBDT.NH_TIPFAC) WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND TB_NOTADEBDT.DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO = 'AC') ND");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC)");
                sSql.AppendLine("WHERE HDCODCLI IN(SELECT TRCODTER FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT = @p0)");
                sSql.AppendLine("AND TFCLAFAC in (1) AND HDESTADO NOT IN('AN') ");
                sSql.AppendLine(") XX_TMP)XX_TMP_1");
                sSql.AppendLine("INNER JOIN TB_NOTADEBDT WITH(NOLOCK) ON(XX_TMP_1.HDCODEMP = TB_NOTADEBDT.ND_CODEMP AND XX_TMP_1.HDTIPFAC = TB_NOTADEBDT.DTTIPFAC AND XX_TMP_1.HDNROFAC = TB_NOTADEBDT.DTNROFAC AND TB_NOTADEBDT.ND_ESTADO = 'AC')");
                sSql.AppendLine("WHERE SALDO <> 0");
                sSql.AppendLine("GROUP BY HDCODEMP,HDSUBTOT,NRO_FAC,HDTIPFAC,HDNROFAC,HDFECFAC,HDTOTFAC,HDTOTIVA,POR_IVA,RECAUDO,TDEV,IDEV,SDEV,RECA_SF,SAL_FAVOR_APL,SALDO,VLR_CAR,NH_NRONOTA,NC,ND,NH_TIPFAC");

                sSql.AppendLine(") xtnom WHERE(CAST(HDTIPFAC AS VARCHAR)+'-'+CAST(HDNROFAC  AS VARCHAR))  IN (");
                sSql.AppendLine("SELECT NRO_FAC FROM(");
                sSql.AppendLine("SELECT *, CASE WHEN(((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT + ISNULL(TND, 0)) - (ISNULL(TDEV, 0) - ISNULL(RECAUDO, 0)) < 0 THEN(((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(TND, 0)) - (ISNULL(TDEV, 0) - ISNULL(RECAUDO, 0) + ISNULL(TNC, 0))) - (ISNULL(RECA_SF, 0) * -1) ELSE 0 END SAL_FAVOR_APL,");
                sSql.AppendLine("CASE WHEN((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT + RECA_SF + ISNULL(TND, 0)) - (ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0))) < 0 THEN(((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(TND, 0)) - (ISNULL(TDEV, 0) + ISNULL(RECAUDO, 0) + ISNULL(TNC, 0))) - (ISNULL(RECA_SF, 0) * -1) ELSE((((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) + ISNULL(RECA_SF, 0) + ISNULL(TND, 0)) - (ISNULL(RECAUDO, 0) + ISNULL(TDEV, 0) + ISNULL(TNC, 0)) END SALDO,");
                sSql.AppendLine("(((HDSUBTOT * POR_IVA) / 100) + HDSUBTOT) VLR_CAR");
                sSql.AppendLine("FROM(");
                sSql.AppendLine("SELECT(HDTIPFAC + '-' + CAST(HDNROFAC AS VARCHAR)) NRO_FAC, HDNROFAC, HDTIPFAC, HDFECFAC, HDSUBTOT, HDTOTFAC, HDTOTIVA, A.TRCODNIT, A.TRNOMBRE + ' ' + ISNULL(A.TRNOMBR2, '') + ' ' + ISNULL(A.TRAPELLI, '') CLIENTE, A.TRCODTER,");
                sSql.AppendLine("B.TRNOMBRE + ' ' + ISNULL(B.TRNOMBR2, '') + ' ' + ISNULL(B.TRAPELLI, '') VENDEDOR,");
                sSql.AppendLine("PGVLRPAG, C.TTDESCRI, D.TTDESCRI TTDESCRI1, D.TTVALORN, (HDFECFAC + D.TTVALORN) F_VENCIMIENTO,");
                sSql.AppendLine("ISNULL((SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFAC = HDTIPFAC AND RC_NROFAC = HDNROFAC AND RC_ESTADO = 'AC' AND CONVERT(DATE, RC_FECREC, 101) <= CONVERT(DATE, GETDATE(), 101)),0) RECAUDO,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTFAC) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO <> 'AN' AND CONVERT(DATE, X.HDFECFAC, 101) <= CONVERT(DATE, GETDATE(), 101)) TDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDTOTIVA) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO <> 'AN' AND CONVERT(DATE, X.HDFECFAC, 101) <= CONVERT(DATE, GETDATE(), 101)) IDEV,");
                sSql.AppendLine("(SELECT SUM(X.HDSUBTOT) FROM FACTURAHD X WHERE X.HDNRODEV = FACTURAHD.HDNROFAC AND FACTURAHD.HDTIPFAC = X.HDTIPDEV AND X.HDTIPFAC = '59' AND HDESTADO<> 'AN' AND CONVERT(DATE, X.HDFECFAC,101) <= CONVERT(DATE, GETDATE(), 101)) SDEV,");
                sSql.AppendLine("(SELECT top 1(SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE IF_CODIMP = TTCODCLA AND TTCODTAB = 'IMPF') FROM TB_IMPUESTOSXFACTURA WITH(NOLOCK) WHERE IF_CODEMP = HDCODEMP AND IF_TIPFAC = HDTIPFAC AND IF_NROFAC = HDNROFAC) POR_IVA,");
                sSql.AppendLine("(SELECT SUM(RC_VALOR) FROM TB_RECAUDO WHERE RC_CODEMP = HDCODEMP AND RC_TIPFACSF = HDTIPFAC AND RC_NROFACSF = HDNROFAC AND RC_ESTADO = 'AC' AND CONVERT(DATE, RC_FECREC,101) <= CONVERT(DATE, GETDATE(), 101)) RECA_SF,");
                sSql.AppendLine("ISNULL((SELECT SUM(ND_VALOR) FROM TB_NOTADT WITH(NOLOCK) INNER JOIN TB_NOTAHD WITH(NOLOCK) ON(TB_NOTAHD.NH_NRONOTA = TB_NOTADT.NH_NRONOTA AND TB_NOTAHD.NH_TIPFAC = TB_NOTADT.NH_TIPFAC)");
                sSql.AppendLine("WHERE TB_NOTADT.DTNROFAC = HDNROFAC AND TB_NOTADT.DTTIPFAC = HDTIPFAC AND TB_NOTADT.ND_ESTADO = 'AC'),0) TNC,");
                sSql.AppendLine("ISNULL((SELECT SUM(ND_VALOR) FROM TB_NOTADEBDT WITH(NOLOCK) INNER JOIN TB_NOTADEBHD WITH(NOLOCK) ON(TB_NOTADEBHD.NH_NRONOTA = TB_NOTADEBDT.NH_NRONOTA AND TB_NOTADEBHD.NH_TIPFAC = TB_NOTADEBDT.NH_TIPFAC)");
                sSql.AppendLine("WHERE TB_NOTADEBDT.DTNROFAC = HDNROFAC AND TB_NOTADEBDT.DTTIPFAC = HDTIPFAC AND TB_NOTADEBDT.ND_ESTADO = 'AC'),0) TND");
                sSql.AppendLine("FROM FACTURAHD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON(HDCODEMP = TFCODEMP AND HDTIPFAC = TFTIPFAC)");
                sSql.AppendLine("INNER JOIN TERCEROS A WITH(NOLOCK) ON(A.TRCODEMP = HDCODEMP  AND A.TRCODTER = HDCODCLI)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS B WITH(NOLOCK) ON(B.TRCODEMP = HDCODEMP  AND B.TRCODTER = A.TRAGENTE)");
                sSql.AppendLine("INNER JOIN PGFACTUR WITH(NOLOCK) ON(PGCODEMP = HDCODEMP AND PGTIPFAC = HDTIPFAC AND PGNROFAC = HDNROFAC)");
                sSql.AppendLine("INNER JOIN TBTABLAS C WITH(NOLOCK) ON(C.TTCODEMP = HDCODEMP  AND C.TTCODCLA = PGTIPPAG AND C.TTCODTAB = 'PAGO')");
                sSql.AppendLine("INNER JOIN TBTABLAS D WITH(NOLOCK) ON(D.TTCODEMP = HDCODEMP  AND D.TTCODCLA = PGDETTPG AND D.TTCODTAB = C.TTVALORC)");
                sSql.AppendLine("WHERE CONVERT(DATE, HDFECFAC,101) <= CONVERT(DATE, GETDATE(), 101)");
                sSql.AppendLine("AND HDESTADO<> 'AN'");
                sSql.AppendLine("AND TFCLAFAC IN(1, 2)");
                sSql.AppendLine(") XXX_TMP) XX_TMP");
                sSql.AppendLine("WHERE 1 = 1 AND TRCODNIT = @p0 AND SALDO<> 0)");
                //sSql.AppendLine("and hdnrofac = 57032");
                
                sSql.AppendLine("ORDER BY HDFECFAC, HDNROFAC DESC,TTCODCLA ASC");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inIdentificacion);
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
