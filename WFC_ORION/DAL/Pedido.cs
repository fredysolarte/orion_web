using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFC_ORION.DAL
{
    public class Pedido
    {
		public static SqlDataReader GetPedidos(DBAccess ObjDB, string EMPRESA, DateTime P_FECHA)
		{
			StringBuilder sql = new StringBuilder();
			sql.AppendLine("SELECT * FROM PEDIDOHD WITH(NOLOCK) WHERE PHCODEMP = @p0 AND convert(date,PHFECPED,101) =@p1 ");			
			try
			{
				return ObjDB.ExecuteReader(sql.ToString(), EMPRESA, P_FECHA.ToString("yyyy-MM-dd"));
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
		public static SqlDataReader GetPedidos(DBAccess ObjDB, string EMPRESA, string inFiltro)
		{
			StringBuilder sql = new StringBuilder();
			
			try
			{
				sql.AppendLine("SELECT TOP 100 PEDIDOHD.*,TERCEROS.* FROM PEDIDOHD WITH(NOLOCK) ");
				sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON TRCODEMP = PHCODEMP AND TRCODTER = PHCODCLI");               
				sql.AppendLine("WHERE PHCODEMP = @p0 " + inFiltro);
                sql.AppendLine("ORDER BY PHFECPED DESC");


                return ObjDB.ExecuteReader(sql.ToString(), EMPRESA);
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

        public static int InsertPedidoDT(DBAccess ObjDB, string PDCODEMP, int PDPEDIDO, int PDLINNUM, string PDTIPPED, int PDCODCLI, string PDBODEGA,
                                        string PDTIPPRO, string PDCLAVE1, string PDCLAVE2, string PDCLAVE3, string PDCLAVE4, string PDDESCRI, string PDCODCAL, string PDUNDPED,
                                        double PDCANPED, double PDCANTID, double? PDPRECIO, string PDLISPRE, DateTime? PDFECPRE, double? PDPRELIS, double? PDDESCUE, string PDCDIMPF,
                                        DateTime? PDFECINI, DateTime? PDFECFIN, double? PDDESMIN, double? PDDESMAX, double? PDTAMMIN, double? PDTAMMAX, int? PDNRLMIN, int? PDNRLMAX,
                                        double PDCANDES, DateTime? PDFECDES, double PDCANCAN, DateTime? PDFECCAN, double PDASGBOD, double PDASGCOM, double PDASGPRO, string PDCDCLA1,
                                        string PDCDCLA2, string PDCDCLA3, string PDCDCLA4, string PDCDCLA5, string PDCDCLA6, string PDDTTEC1, string PDDTTEC2, string PDDTTEC3, string PDDTTEC4,
                                        string PDDTTEC5, double? PDDTTEC6, int? PDPROGDT, string PDESTADO, string PDCAUSAE, string PDNMUSER, int? PDCUNDDE,
                                        string PDCAUNAL, string PDUNDDES, string PDUNDALT, double? PDPORCOM, string PDTIPLIN, int? PDCUMFEC, int? PDCUMCAN, int? PDINCFEC, int? PDINCCAN,
                                        int? PDCODDES, double? PDSUBTOT)
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

                return ObjDB.ExecuteNonQuery(sSql.ToString(), PDCODEMP, PDPEDIDO, PDLINNUM, PDTIPPED, PDCODCLI, PDBODEGA,
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
        public static int InsertPedidoHD(DBAccess ObjDB, string PHCODEMP, int PHPEDIDO, int PHCODCLI, int PHCODSUC, int? PHAGENTE, string PHTIPPED, string PHBODEGA,
                                          string PHIDIOMA, string PHMONEDA, double? PHTRMLOC, double PHTRMUSD, double PHTRMPED, string PHTIPDES, double PHDESCU1, double PHDESPAG,
                                          double PHDESVAL, double PHTOTDES, double PHTOTPED, string PHESTADO, string PHCAUSAE, string PHNMUSER, string PHLISPRE,
                                          int PHPEDMES, int PHPEDANO, DateTime PHFECPED, string PHOBSERV, DateTime? PHFECLIQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO PEDIDOHD ");
                sSql.AppendLine("(PHCODEMP,PHPEDIDO,PHFECPED,PHCODCLI,PHCODSUC,PHAGENTE,PHTIPPED,PHBODEGA,PHIDIOMA,PHMONEDA,PHTRMLOC,PHTRMUSD,PHTRMPED,PHTIPDES,PHDESCU1");
                sSql.AppendLine(",PHDESPAG,PHDESVAL,PHTOTDES,PHTOTPED,PHESTADO,PHCAUSAE,PHNMUSER,PHPEDMES,PHPEDANO,PHLISPRE,PHOBSERV,PHFECLIQ,PHDIVISI,PHTIPLIN,PHFECING,PHFECMOD) VALUES(");
                sSql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,");
                sSql.AppendLine("@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,'.','.',GETDATE(),GETDATE())");

                return ObjDB.ExecuteNonQuery(sSql.ToString(), PHCODEMP, PHPEDIDO, PHFECPED, PHCODCLI, PHCODSUC, PHAGENTE, PHTIPPED, PHBODEGA, PHIDIOMA, PHMONEDA, PHTRMLOC, PHTRMUSD,
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

        public static int InsertEvidenciaPedido(DBAccess ObjDB, string EP_CODEMP, int PHPEDIDO, object EP_IMAGEN, string EP_USUARIO)
        {
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.AppendLine("INSERT INTO TB_EVIDENCIA_PEDIDO (EP_CODEMP, PHPEDIDO, EP_IMAGEN, EP_USUARIO, EP_FECING) VALUES (@p0,@p1,@p2,@p3,GETDATE())");
                return ObjDB.ExecuteNonQuery(sql.ToString(), EP_CODEMP, PHPEDIDO, EP_IMAGEN, EP_USUARIO);
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
