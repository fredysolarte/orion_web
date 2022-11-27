using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Pedidos
{
    public class PedidosBD
    {
        public static DataTable GetPedidos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT PHCODEMP, PHPEDIDO, PHFECPED, PHCODCLI, PHCODSUC, PHTIPPED, PHBODEGA, PHLISPRE, PHFECPRE, PHIDIOMA, ");
                sql.AppendLine("       PHMONEDA, PHTRMLOC, PHTRMUSD, PHTRMPED, PHAGENTE, PHTERPAG, PHMODDES, PHTERDES, PHPTOENT, PHTIPDES, ");
                sql.AppendLine("       PHDESCU1, PHDESPAG, PHDESVAL, PHTOTDES, PHTOTPED, PHFECINI, PHFECFIN, PHCIUDES, PHDIRDES, PHOBSERV, ");
                sql.AppendLine("       PHCDCLA1, PHCDCLA2, PHCDCLA3, PHCDCLA4, PHCDCLA5, PHCDCLA6, PHDTTEC1, PHDTTEC2, PHDTTEC3, PHDTTEC4, ");
                sql.AppendLine("       PHDTTEC5, PHDTTEC6, PHPROGDT, PHESTADO, PHCAUSAE, PHNMUSER, PHFECING, PHFECMOD, PHCANTID, PHCONVEN, ");
                sql.AppendLine("       PHDIVISI, PHPTODES, PHTIPLIN, PHSOLCOM, TRNOMBRE, PHPEDMES, PHPEDANO, (TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOM_TER,TRCODNIT,PHFECLIQ ");
                sql.AppendLine("  FROM PEDIDOHD WITH(NOLOCK) ");                
                sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (PHCODEMP = TRCODEMP AND PHCODCLI = TRCODTER)");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                sql.AppendLine("ORDER BY PHPEDIDO");
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
        public static DataTable GetPedidosEmpaques(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT PHCODEMP, PHPEDIDO, PHFECPED, PHCODCLI, PHCODSUC, PHTIPPED, PHBODEGA, PHLISPRE, PHFECPRE, PHIDIOMA, ");
                sql.AppendLine("       PHMONEDA, PHTRMLOC, PHTRMUSD, PHTRMPED, PHAGENTE, PHTERPAG, PHMODDES, PHTERDES, PHPTOENT, PHTIPDES, ");
                sql.AppendLine("       PHDESCU1, PHDESPAG, PHDESVAL, PHTOTDES, PHTOTPED, PHFECINI, PHFECFIN, PHCIUDES, PHDIRDES, PHOBSERV, ");
                sql.AppendLine("       PHCDCLA1, PHCDCLA2, PHCDCLA3, PHCDCLA4, PHCDCLA5, PHCDCLA6, PHDTTEC1, PHDTTEC2, PHDTTEC3, PHDTTEC4, ");
                sql.AppendLine("       PHDTTEC5, PHDTTEC6, PHPROGDT, PHESTADO, PHCAUSAE, PHNMUSER, PHFECING, PHFECMOD, PHCANTID, PHCONVEN, ");
                sql.AppendLine("       PHDIVISI, PHPTODES, PHTIPLIN, PHSOLCOM, TRNOMBRE, PHPEDMES, PHPEDANO, (TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOM_TER,TRCODNIT,LH_LSTPAQ ");
                sql.AppendLine("  FROM PEDIDOHD WITH(NOLOCK) ");
                sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (PHCODEMP = TRCODEMP AND PHCODCLI = TRCODTER)");
                sql.AppendLine("INNER JOIN TB_EMPAQUEHD WITH(NOLOCK) ON (LH_CODEMP = PHCODEMP AND PHPEDIDO = LH_PEDIDO)");
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                sql.AppendLine("ORDER BY PHPEDIDO");
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
        public static DataTable GetTipPro(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  SELECT TATIPPRO, TANOMBRE FROM  TBTIPPRO WITH(NOLOCK) WHERE 1=1 ");
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
        public static DataTable GetPedidoDT(SessionManager oSessionManager, string PDCODEMP ,int PDPEDIDO)
        {

            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT PDBODEGA, PDTIPPRO, PDCLAVE1, PDCLAVE2, PDCLAVE3, PDCLAVE4, PDDESCRI, PDCODCAL, PDUNDPED, PDCANPED, ");
                sql.AppendLine("       PDCANTID, PDPRECIO, PDLISPRE, PDFECPRE, PDPRELIS, PDDESCUE, PDCDIMPF, PDFECINI, PDFECFIN, PDDESMIN, ");
                sql.AppendLine("       PDDESMAX, PDTAMMIN, PDTAMMAX, PDNRLMIN, PDNRLMAX, PDCANDES, PDFECDES, PDCANCAN, PDFECCAN, PDASGBOD, ");
                sql.AppendLine("       PDASGCOM, PDASGPRO, PDCDCLA1, PDCDCLA2, PDCDCLA3, PDCDCLA4, PDCDCLA5, PDCDCLA6, PDDTTEC1, PDDTTEC2, ");
                sql.AppendLine("       PDDTTEC3, PDDTTEC4, PDDTTEC5, PDDTTEC6, PDPROGDT, PDESTADO, PDCAUSAE, PDNMUSER, PDFECING, PDFECMOD, ");
                sql.AppendLine("       PDCUNDDE, PDCAUNAL, PDUNDDES, PDUNDALT, PDPORCOM, PDTIPLIN, PDCUMFEC, PDCUMCAN, PDINCFEC, PDINCCAN, ");
                sql.AppendLine("       ARNOMBRE, PDLINNUM, PDCODDES, PDSUBTOT, NULL IM_IMAGEN,TANOMBRE ");
                sql.AppendLine("  FROM PEDIDODT WITH(NOLOCK) ");
                sql.AppendLine("  INNER JOIN ARTICULO WITH(NOLOCK) ON(PDCODEMP = ARCODEMP AND PDCLAVE1 = ARCLAVE1 AND PDCLAVE2 = ARCLAVE2");
                sql.AppendLine("                                   AND PDCLAVE3 = ARCLAVE3 AND PDCLAVE4 = ARCLAVE4 AND PDTIPPRO = ARTIPPRO)");
                sql.AppendLine("  INNER JOIN TBTIPPRO WITH(NOLOCK) ON(TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                //sql.AppendLine("  LEFT OUTER JOIN IMAGENES WITH(NOLOCK) ON( IM_CODEMP = ARCODEMP AND IM_TIPPRO = ARTIPPRO AND IM_CLAVE1 = ARCLAVE1 ");
                //sql.AppendLine("                                   AND IM_CLAVE2 ='.' AND IM_CLAVE3 ='.' AND IM_CLAVE4 ='.' AND IM_TIPIMA = 1)");
                sql.AppendLine("   WHERE PDCODEMP=@p0 AND PDPEDIDO = @p1");
                
                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, PDCODEMP, PDPEDIDO);
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
        public static int InsertPedidoDT(SessionManager oSessionManager, string PDCODEMP, int PDPEDIDO, int PDLINNUM, string PDTIPPED, int PDCODCLI, string PDBODEGA,
                                        string PDTIPPRO, string PDCLAVE1, string PDCLAVE2, string PDCLAVE3, string PDCLAVE4, string PDDESCRI, string PDCODCAL, string PDUNDPED,
                                        double PDCANPED, double PDCANTID, double? PDPRECIO, string PDLISPRE, DateTime? PDFECPRE, double? PDPRELIS, double? PDDESCUE, string PDCDIMPF,
                                        DateTime? PDFECINI, DateTime? PDFECFIN, double? PDDESMIN, double? PDDESMAX, double? PDTAMMIN, double? PDTAMMAX, int? PDNRLMIN, int? PDNRLMAX,
                                        double PDCANDES, DateTime? PDFECDES, double PDCANCAN, DateTime? PDFECCAN, double PDASGBOD, double PDASGCOM, double PDASGPRO, string PDCDCLA1,
                                        string PDCDCLA2, string PDCDCLA3, string PDCDCLA4, string PDCDCLA5, string PDCDCLA6, string PDDTTEC1, string PDDTTEC2, string PDDTTEC3, string PDDTTEC4,
                                        string PDDTTEC5, double? PDDTTEC6, int? PDPROGDT, string PDESTADO, string PDCAUSAE, string PDNMUSER, int? PDCUNDDE,
                                        string PDCAUNAL, string PDUNDDES, string PDUNDALT, double? PDPORCOM, string PDTIPLIN, int? PDCUMFEC, int? PDCUMCAN, int? PDINCFEC, int? PDINCCAN, 
                                        int? PDCODDES,double? PDSUBTOT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO PEDIDODT");
                sSql.AppendLine("(PDCODEMP,PDPEDIDO,PDLINNUM,PDTIPPED,PDFECPED,PDCODCLI,PDBODEGA,PDTIPPRO,PDCLAVE1,PDCLAVE2,PDCLAVE3,PDCLAVE4,PDDESCRI,PDCODCAL");
                sSql.AppendLine(",PDUNDPED,PDCANPED,PDCANTID,PDPRECIO,PDLISPRE,PDFECPRE,PDPRELIS,PDDESCUE,PDCDIMPF,PDFECINI,PDFECFIN,PDDESMIN,PDDESMAX,PDTAMMIN");
                sSql.AppendLine(",PDTAMMAX,PDNRLMIN,PDNRLMAX,PDCANDES,PDFECDES,PDCANCAN,PDFECCAN,PDASGBOD,PDASGCOM,PDASGPRO,PDCDCLA1,PDCDCLA2,PDCDCLA3,PDCDCLA4");
                sSql.AppendLine(",PDCDCLA5,PDCDCLA6,PDDTTEC1,PDDTTEC2,PDDTTEC3,PDDTTEC4,PDDTTEC5,PDDTTEC6,PDPROGDT,PDESTADO,PDCAUSAE,PDNMUSER,PDFECING,PDFECMOD");
                sSql.AppendLine(",PDCUNDDE,PDCAUNAL,PDUNDDES,PDUNDALT,PDPORCOM,PDTIPLIN,PDCUMFEC,PDCUMCAN,PDINCFEC,PDINCCAN,PDCODDES,PDSUBTOT)");
                sSql.AppendLine("VALUES(");
                sSql.AppendLine("@p0,@p1,@p2,@p3,GETDATE(),@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine("@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,");
                sSql.AppendLine("@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,");
                sSql.AppendLine("@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,@p50,@p51,@p52,GETDATE(),GETDATE(),");
                sSql.AppendLine("@p53,@p54,@p55,@p56,@p57,@p58,@p59,@p60,@p61,@p62,@p63,@p64)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PDCODEMP, PDPEDIDO, PDLINNUM, PDTIPPED, PDCODCLI, PDBODEGA,
                                                                                                    PDTIPPRO, PDCLAVE1, PDCLAVE2, PDCLAVE3, PDCLAVE4, PDDESCRI, PDCODCAL, PDUNDPED,
                                                                                                    PDCANPED, PDCANTID, PDPRECIO, PDLISPRE, PDFECPRE, PDPRELIS, PDDESCUE, PDCDIMPF,
                                                                                                    PDFECINI, PDFECFIN, PDDESMIN, PDDESMAX, PDTAMMIN, PDTAMMAX, PDNRLMIN, PDNRLMAX,
                                                                                                    PDCANDES, PDFECDES, PDCANCAN, PDFECCAN, PDASGBOD, PDASGCOM, PDASGPRO, PDCDCLA1,
                                                                                                    PDCDCLA2, PDCDCLA3, PDCDCLA4, PDCDCLA5, PDCDCLA6, PDDTTEC1, PDDTTEC2, PDDTTEC3, PDDTTEC4,
                                                                                                    PDDTTEC5, PDDTTEC6, PDPROGDT, PDESTADO, PDCAUSAE, PDNMUSER, PDCUNDDE,
                                                                                                    PDCAUNAL, PDUNDDES, PDUNDALT, PDPORCOM, PDTIPLIN, PDCUMFEC, PDCUMCAN, PDINCFEC, PDINCCAN, PDCODDES, PDSUBTOT);
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
        public static int InsertPedidoHD(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO, int PHCODCLI, int PHCODSUC, int? PHAGENTE, string PHTIPPED, string PHBODEGA,
                                          string PHIDIOMA, string PHMONEDA, double PHTRMLOC, double PHTRMUSD, double PHTRMPED, string PHTIPDES, double PHDESCU1, double PHDESPAG,
                                          double PHDESVAL, double PHTOTDES, double PHTOTPED, string PHESTADO, string PHCAUSAE, string PHNMUSER, string PHLISPRE,
                                          int PHPEDMES, int PHPEDANO, DateTime PHFECPED, string PHOBSERV,DateTime PHFECLIQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO PEDIDOHD ");
                sSql.AppendLine("(PHCODEMP,PHPEDIDO,PHFECPED,PHCODCLI,PHCODSUC,PHAGENTE,PHTIPPED,PHBODEGA,PHIDIOMA,PHMONEDA,PHTRMLOC,PHTRMUSD,PHTRMPED,PHTIPDES,PHDESCU1");
                sSql.AppendLine(",PHDESPAG,PHDESVAL,PHTOTDES,PHTOTPED,PHESTADO,PHCAUSAE,PHNMUSER,PHPEDMES,PHPEDANO,PHLISPRE,PHOBSERV,PHFECLIQ,PHDIVISI,PHTIPLIN,PHFECING,PHFECMOD) VALUES(");
                sSql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,");
                sSql.AppendLine("@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,'.','.',GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text,
                                                  PHCODEMP, PHPEDIDO, PHFECPED, PHCODCLI, PHCODSUC, PHAGENTE, PHTIPPED, PHBODEGA, PHIDIOMA, PHMONEDA, PHTRMLOC, PHTRMUSD,
                                                  PHTRMPED, PHTIPDES, PHDESCU1, PHDESPAG, PHDESVAL, PHTOTDES, PHTOTPED, PHESTADO, PHCAUSAE, PHNMUSER, PHPEDMES, PHPEDANO, 
                                                  PHLISPRE, PHOBSERV, PHFECLIQ);
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
        public static int UpdatePedidoHD(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO, int PHCODCLI, int PHCODSUC, int? PHAGENTE, string PHTIPPED,
                                          string PHIDIOMA, string PHMONEDA, double PHTRMLOC, double PHTRMUSD, double PHTRMPED, string PHTIPDES, double PHDESCU1, double PHDESPAG,
                                          double PHDESVAL, double PHTOTDES, double PHTOTPED, string PHESTADO, string PHCAUSAE, string PHNMUSER, string PHLISPRE,string PHOBSERV,DateTime PHFECLIQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PEDIDOHD SET PHESTADO=@p0,PHNMUSER=@p1,PHFECMOD=GETDATE(),PHLISPRE=@p4,PHOBSERV=@p5,PHCODCLI=@p6,PHAGENTE=@p7,PHTRMLOC=@p8,PHFECLIQ=@p9,PHCODSUC=@p10 WHERE PHCODEMP=@p2 AND PHPEDIDO=@p3 ");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text,PHESTADO,PHNMUSER,PHCODEMP,PHPEDIDO,PHLISPRE,PHOBSERV,PHCODCLI,PHAGENTE,PHTRMLOC,PHFECLIQ,PHCODSUC);                                                  
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

        public static int UpdatePedidoHD(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO, int PHCODCLI, int PHCODSUC, int? PHAGENTE,
                                          double PHTRMLOC, string PHESTADO, string PHUSERLQ, string PHLISPRE, string PHOBSERV, DateTime PHFECLIQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PEDIDOHD SET PHESTADO=@p0,PHUSERLQ=@p1,PHFECMOD=GETDATE(),PHLISPRE=@p4,PHOBSERV=@p5,PHCODCLI=@p6,PHAGENTE=@p7,PHTRMLOC=@p8,PHFECLIQ=@p9,PHCODSUC=@p10 WHERE PHCODEMP=@p2 AND PHPEDIDO=@p3 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PHESTADO, PHUSERLQ, PHCODEMP, PHPEDIDO, PHLISPRE, PHOBSERV, PHCODCLI, PHAGENTE, PHTRMLOC, PHFECLIQ, PHCODSUC);
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

        public static int UpdatePedidoHD(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO, int PHCODCLI, int PHCODSUC, string PHTIPPED,
                                          string PHIDIOMA, string PHMONEDA, double PHTRMLOC, double PHTRMUSD, double PHTRMPED,
                                          string PHTIPDES, double PHDESCU1, double PHDESPAG,
                                          double PHDESVAL, double PHTOTDES, double PHTOTPED,
                                          string PHESTADO, string PHCAUSAE, string PHNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PEDIDOHD SET PHESTADO=@p0,PHNMUSER=@p1,PHFECMOD=GETDATE() WHERE PHCODEMP=@p2 AND PHPEDIDO=@p3 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PHESTADO, PHNMUSER, PHCODEMP, PHPEDIDO);
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
        public static int BorrarItemsPedido(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("DELETE FROM PEDIDODT WHERE PDCODEMP =@p0 AND PDPEDIDO=@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, PHCODEMP, PHPEDIDO);
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
        public static int BorrarItemsPedidoDTMoneda(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("DELETE FROM TB_PEDIDODT_MONEDA WHERE PDCODEMP =@p0 AND PDPEDIDO=@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, PHCODEMP, PHPEDIDO);
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
        public static int TieneListaEmpaque(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_EMPAQUEHD WHERE LH_CODEMP =@p0 AND LH_PEDIDO =@p1 AND LH_ESTADO <> 'AN'");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager,sSql.ToString(),CommandType.Text,PHCODEMP,PHPEDIDO));
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
        public static int AnularPedidoHD(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO, string PHNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE PEDIDOHD SET PHESTADO='AN',PHNMUSER=@p0,PHFECMOD=GETDATE() WHERE PHCODEMP =@p1 AND PHPEDIDO = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PHNMUSER, PHCODEMP, PHPEDIDO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static int AnularPedidoDT(SessionManager oSessionManager, string PDCODEMP, string PDNMUSER, int PDPEDIDO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE PEDIDODT SET PDESTADO='AN',PDNMUSER=@p0,PDFECMOD=GETDATE() WHERE PDCODEMP =@p1 AND PDPEDIDO = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PDNMUSER, PDCODEMP, PDPEDIDO);
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
        public static IDataReader GetPedidoPlano(SessionManager oSessionManager, string PHCODEMP, int PHPEDIDO)
        {
            StringBuilder sSql = new StringBuilder();
            try { 
                sSql.AppendLine("SELECT PDPEDIDO,PHFECPED,TRCODNIT,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,''))TERCERO,ARTIPPRO,   ");
                sSql.AppendLine("TANOMBRE,ARCLAVE1,ARNOMBRE,PDCANTID                                                                             ");
                sSql.AppendLine("  FROM PEDIDODT WITH(NOLOCK)                                                                                    ");
                sSql.AppendLine("   INNER JOIN ARTICULO WITH(NOLOCK) ON (PDCODEMP = ARCODEMP AND PDTIPPRO = ARTIPPRO                             ");
                sSql.AppendLine("                    AND PDCLAVE1 = ARCLAVE1 AND PDCLAVE2 = ARCLAVE2                                             ");
                sSql.AppendLine("                    AND PDCLAVE3 = ARCLAVE3 AND PDCLAVE4 = ARCLAVE4)                                            ");
                sSql.AppendLine("   INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP  = ARCODEMP AND TATIPPRO = ARTIPPRO)                           ");
                sSql.AppendLine("   INNER JOIN PEDIDOHD WITH(NOLOCK) ON (PHCODEMP = ARCODEMP AND PHPEDIDO = PDPEDIDO)                            ");
                sSql.AppendLine("   INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = ARCODEMP AND TRCODTER = PHCODCLI)                            ");
                sSql.AppendLine("   WHERE PDCODEMP = @p0 ");
                sSql.AppendLine("     AND PDPEDIDO = @p1 ");

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, PHCODEMP, PHPEDIDO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            }
        }

        
        //Articulos
        #region
        public static DataTable GetArticulos(SessionManager oSessionManager, string filter, string inBodega,string LT)
        {
            StringBuilder sSql = new StringBuilder();
            string lc_sql = "";
            try
            {
                if (inBodega != null)                
                    lc_sql = "@p1";
                else
                    lc_sql = "@p0";

                sSql.AppendLine("SELECT TANOMBRE,ARCLAVE1,ARNOMBRE,ARUNDINV,DD.ASNOMBRE ARDTTEC4,AA.ASNOMBRE ARDTTEC1,BB.ASNOMBRE ARDTTEC2,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARTIPPRO, CC.ASNOMBRE ARDTTEC3,EE.ASNOMBRE ARDTTEC5,FF.ASNOMBRE ARDTTEC7, BDNOMBRE,BDBODEGA,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA,"+lc_sql+ "),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA),0) DESCUENTO,ISNULL(BBCANTID-BBCANTRN,0) BBCANTID,ISNULL(BBCANTRN,0) BBCANTRN");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (ARCODEMP = BBCODEMP AND ARTIPPRO = BBTIPPRO AND ARCLAVE1 = BBCLAVE1 AND ARCLAVE2 = BBCLAVE2 AND ARCLAVE3 = BBCLAVE3 AND ARCLAVE4 = BBCLAVE4) ");
                sSql.AppendLine("LEFT OUTER JOIN TBBODEGA WITH(NOLOCK) ON (ARCODEMP = BDCODEMP AND BDBODEGA = BBBODEGA) ");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARTICULO.ARCODEMP AND AA.ASTIPPRO = ARTICULO.ARTIPPRO AND AA.ASCLAVEO = ARTICULO.ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARTICULO.ARCODEMP AND BB.ASTIPPRO = ARTICULO.ARTIPPRO AND BB.ASCLAVEO = ARTICULO.ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARTICULO.ARCODEMP AND CC.ASTIPPRO = ARTICULO.ARTIPPRO AND CC.ASCLAVEO = ARTICULO.ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARTICULO.ARCODEMP AND DD.ASTIPPRO = ARTICULO.ARTIPPRO AND DD.ASCLAVEO = ARTICULO.ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARTICULO.ARCODEMP AND EE.ASTIPPRO = ARTICULO.ARTIPPRO AND EE.ASCLAVEO = ARTICULO.ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARTICULO.ARCODEMP AND FF.ASTIPPRO = ARTICULO.ARTIPPRO AND FF.ASCLAVEO = ARTICULO.ARDTTEC7 AND FF.ASNIVELC = 10)");

                if (inBodega != null)
                {
                    sSql.AppendLine("WHERE BBBODEGA = @p0 " + filter);
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inBodega,LT);
                }
                else
                {
                    sSql.AppendLine("WHERE 1=1 " + filter);
                    return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, LT);
                }

                
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
        public static DataTable GetPedidoDT_Moneda(SessionManager oSessionManager, string PDCODEMP, int PDPEDIDO)
        {

            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT * ");
                sql.AppendLine("  FROM TB_PEDIDODT_MONEDA WITH(NOLOCK) ");                
                sql.AppendLine("   WHERE PDCODEMP=@p0 AND PDPEDIDO = @p1");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, PDCODEMP, PDPEDIDO);
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
        public static int InsertPedidoDTMoneda(SessionManager oSessionManager, string  PDCODEMP, int PDPEDIDO, int PDLINNUM, string PMMONEDA, double PMTASA, double PMPRECIO, double PMPRELIS, double PMSUBTOT, string PMUSUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PEDIDODT_MONEDA (PDCODEMP,PDPEDIDO,PDLINNUM,PMMONEDA,PMTASA,PMPRECIO,PMPRELIS,PMSUBTOT,PMUSUARIO)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PDCODEMP, PDPEDIDO, PDLINNUM, PMMONEDA, PMTASA, PMPRECIO, PMPRELIS, PMSUBTOT, PMUSUARIO);
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
        public static DataTable getEvidenciasPedidos(SessionManager oSessionManager, string EP_CODEMP, int PHPEDIDO)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT EP_CODIGO,EP_FECING ");
                sql.AppendLine("  FROM TB_EVIDENCIA_PEDIDO WITH(NOLOCK) ");
                sql.AppendLine("   WHERE EP_CODEMP=@p0 AND PHPEDIDO = @p1");

                return DBAccess.GetDataTable(oSessionManager, sql.ToString(), CommandType.Text, EP_CODEMP, PHPEDIDO);
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
        public static DataTable getImagenPedido(SessionManager oSessionManager, int EP_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EP_IMAGEN ");
                sSql.AppendLine("  FROM TB_EVIDENCIA_PEDIDO WITH(NOLOCK) WHERE EP_CODIGO =@p0 ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, EP_CODIGO);
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
