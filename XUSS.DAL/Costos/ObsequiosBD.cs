using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using System.Data;

namespace XUSS.DAL.Costos
{
    public class ObsequiosBD
    {
        public DataTable GetObsequiosCedula(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_DETDESCUENTO.*,TRNOMBRE FROM TB_DETDESCUENTO LEFT OUTER JOIN TERCEROS  ON( CONDICION_1 = TRCODNIT) WHERE ID_DESCUENTO = 5");
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

        public string GetNombreTerceros(SessionManager oSessionManager, string filter)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT (TRNOMBRE + ISNULL(TRNOMBR2,'')) FROM TERCEROS " + filter);
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text));
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

        public int InsertObsequioCedula(SessionManager oSessionManager, string BODEGA, double VALOR, string CONDICION_1, DateTime FECHAINI, string USUARIO)
        {
            int ln_id = 0;
            StringBuilder sql = new StringBuilder();
            try
            {
                //Obteniendo Numero Maximo de Descuento para Crear
                ln_id = GetMaximoDescuento(oSessionManager) + 1;

                sql.AppendLine("INSERT INTO TB_DETDESCUENTO (ID,ID_DESCUENTO,BODEGA,TP,CLAVE1,CLAVE2,CLAVE3,CLAVE4,VALOR,CONDICION_1,CONDICION_2,FECHAINI,FECHAFIN,USUARIO,");
                sql.AppendLine("                             ESTADO,FECMOD,FECING,CONDICION_3,CONDICION_4,CONDICION_5) ");
                sql.AppendLine("VALUES (@p0,5,@p1,'.','.','.','.','.',@p2,@p3,NULL,@p4,@p4,@p5,'AC',GETDATE(),GETDATE(),NULL,NULL,NULL)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, ln_id, BODEGA, VALOR, CONDICION_1, FECHAINI.ToString("yyyyMMdd"), USUARIO);
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
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text));
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

        public int InsertTercero(SessionManager oSessionManager, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI, string TRCODNIT,
                                                        string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA,
                                                        string TRBODEGA, string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE,
                                                        string TRLISPRA, double? TRDESCUE, double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP,
                                                        string TRINDSOC, string TRINDVEN, string TRCDCLA1, string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5,
                                                        string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3, string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6,
                                                        int? TRPROGDT, double? TRSALCAR,
                                                        string TRCONVEN, string TROBSERV, string TRREQCLI, string TRRETAUT, string TRRETIVA, string TRRETICA, string TRRETFUE,
                                                        string TRCODALT, double? TRSALVEN, string TRCENCOS, string TRTIPCART, DateTime TRFECNAC, string TRRESPAL, double? TRRESCUP,
                                                        string TRAPELLI, string TRNOMBR3)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO TERCEROS (");
            sql.AppendLine("TRCODEMP,TRCODTER,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,TRNROFAX,");
            sql.AppendLine("TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,");
            sql.AppendLine("TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN,TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,");
            sql.AppendLine("TRCDCLA6,TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TRFECING,TRFECMOD,TRSALCAR,");
            sql.AppendLine("TRCONVEN,TROBSERV,TRREQCLI,TRRETAUT,TRRETIVA,TRRETICA,TRRETFUE,TRCODALT,TRSALVEN,TRCENCOS,TRTIPCART,TRFECNAC,TRRESPAL,TRRESCUP,");
            sql.AppendLine("TRAPELLI,TRNOMBR3) VALUES(");
            sql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,");
            sql.AppendLine("@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,");
            sql.AppendLine("@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,'AC','.','ADMIN',GETDATE(),GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,");
            sql.AppendLine("NULL,NULL,NULL,NULL,NULL,NULL,@p50,NULL,NULL,NULL,NULL)");
            try
            {
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text,
                    TRCODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL, TRNROFAX,
                    TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                    TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
                    TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRFECNAC.ToString("yyyyMMdd"));
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

        public int GetContador(SessionManager oSessionManager, string CODEMP, string CODCLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine(" UPDATE TBTABLAS");
                sSql.AppendLine("SET TTVALORN = TTVALORN +1");
                sSql.AppendLine("WHERE TTCODEMP = @p0");
                sSql.AppendLine("  AND TTCODTAB = 'CONT'");
                sSql.AppendLine("  AND TTCODCLA = @p1");

                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, CODCLA);

                sSql.AppendLine("SELECT TTVALORN");
                sSql.AppendLine("  FROM TBTABLAS ");
                sSql.AppendLine("WHERE TTCODEMP = @p0");
                sSql.AppendLine("   AND TTCODTAB = 'CONT'");
                sSql.AppendLine("   AND TTCODCLA = @p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, CODCLA));
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
        public Boolean ExisteTercero(SessionManager oSessionManager, string TRCODNIT)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT COUNT(*) FROM TERCEROS WHERE TRCODNIT =@p0 ");
                if (Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text, TRCODNIT)) > 0)
                    return true;
                else
                    return false;
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
