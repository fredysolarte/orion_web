using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using System.Data.SqlClient;

namespace XUSS.DAL.Comun
{
    public class ComunBD
    {
        public static DataTable GetProveedores(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TRCODTER,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) TRNOMBRE FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP = '001' AND TRINDPRO = 'S' ");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

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
        public static string GetNombreProveedor(SessionManager oSessionManager, string codigo)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TRNOMBRE FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP = '001' AND TRINDPRO = 'S' AND TRCODNIT =@p0");                
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, codigo));
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
        public static int GeneraConsecutivo(SessionManager oSessionManager, string TTCODCLA)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TBTABLAS         ");
            sql.AppendLine("SET TTVALORN = TTVALORN + 1 ");
            sql.AppendLine("WHERE TTCODEMP = '001'  ");
            sql.AppendLine("AND TTCODTAB = 'CONT'   ");
            sql.AppendLine("AND TTCODCLA = @p0 ");//NROTCK

            try
            {
                DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, TTCODCLA);

                sql.Clear();
                sql.AppendLine("SELECT TTVALORN ");
                sql.AppendLine(" FROM TBTABLAS  ");
                sql.AppendLine("WHERE TTCODEMP = '001' ");
                sql.AppendLine("  AND TTCODTAB = 'CONT' ");
                sql.AppendLine("  AND TTCODCLA = @p0 ");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text, TTCODCLA));
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
        public static int GeneraConsecutivo(SessionManager oSessionManager, string TTCODCLA, string TTCODEMP)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TBTABLAS         ");
            sql.AppendLine("SET TTVALORN = TTVALORN + 1 ");
            sql.AppendLine("WHERE TTCODEMP = @p0  ");
            sql.AppendLine("AND TTCODTAB = 'CONT'   ");
            sql.AppendLine("AND TTCODCLA = @p1 ");//NROTCK

            try
            {
                DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, TTCODEMP, TTCODCLA);

                sql.Clear();
                sql.AppendLine("SELECT TTVALORN ");
                sql.AppendLine(" FROM TBTABLAS  ");
                sql.AppendLine("WHERE TTCODEMP = @p0 ");
                sql.AppendLine("  AND TTCODTAB = 'CONT' ");
                sql.AppendLine("  AND TTCODCLA = @p1 ");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sql.ToString(), CommandType.Text, TTCODEMP, TTCODCLA));
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
        public static DataTable GetTbTablaLista(SessionManager oSessionManager, string TTCODTAB)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTCODEMP,TTCODTAB,TTCODCLA,TTVALORC,TTVALORN,TTVALORF,TTVALORD,TTDESCRI,TTCDUSER,TTFECING,TTFECMOD,TTVLRFL2,TTDESLAR,TTPREGUN,TTESTADO FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = '001' AND TTCODTAB = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TTCODTAB);
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
        public static DataTable GetClasesParametros(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM CLASES_PARAMETROS WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

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
        public static DataTable GetTbTablaLista(SessionManager oSessionManager, string TTCODEMP,string TTCODTAB)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTCODCLA,TTVALORC,TTDESCRI FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = @p0 AND TTCODTAB = @p1 AND TTESTADO ='AC'");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TTCODEMP,TTCODTAB);
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
        public IDataReader GetTbTablaListaR(SessionManager oSessionManager, string TTCODEMP, string TTCODTAB)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTCODCLA,TTVALORC,TTDESCRI FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = @p0 AND TTCODTAB = @p1 AND TTESTADO ='AC'");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, TTCODEMP, TTCODTAB);
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
        public static string GetValorc(SessionManager oSessionManager, string TTCDOEMP,string TTCODTAB,string TTCODCLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTVALORC FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = @p0 AND TTCODTAB = @p1 AND TTCODCLA=@p2");
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TTCDOEMP, TTCODTAB, TTCODCLA));
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
        public static double GetValorN(SessionManager oSessionManager, string TTCDOEMP, string TTCODTAB, string TTCODCLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TTVALORN FROM TBTABLAS WITH(NOLOCK) WHERE TTCODEMP = @p0 AND TTCODTAB = @p1 AND TTCODCLA=@p2");
                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TTCDOEMP, TTCODTAB, TTCODCLA));
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
        public static DataTable GetLstBodegas(SessionManager oSessionManager, string ALAMCEN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBODEGA.BDBODEGA,TBBODEGA.BDNOMBRE FROM TBBODEGA WITH(NOLOCK) WHERE TBBODEGA.BDALMACE = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ALAMCEN);
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
        public static DataTable GetTiposProducto(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TATIPPRO,TANOMBRE FROM TBTIPPRO WITH(NOLOCK) WHERE TAESTADO = 'AC'");
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
        public static IDataReader GetCaracteristicaTP(SessionManager oSessionManager, string CODEMP,string TP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBTIPPRO WITH(NOLOCK) WHERE TAESTADO = 'AC' AND TATIPPRO =@p0 AND TACODEMP=@p1 ");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text,TP,CODEMP);
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
        public static int InsertTercero(SessionManager oSessionManager, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI, string TRCODNIT,
                                                        string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA,
                                                        string TRBODEGA, string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE,
                                                        string TRLISPRA, double? TRDESCUE, double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP,
                                                        string TRINDSOC, string TRINDVEN, string TRCDCLA1, string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5,
                                                        string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3, string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6,
                                                        int? TRPROGDT, double? TRSALCAR,
                                                        string TRCONVEN, string TROBSERV, string TRREQCLI, string TRRETAUT, string TRRETIVA, string TRRETICA, string TRRETFUE,
                                                        string TRCODALT, double? TRSALVEN, string TRCENCOS, string TRTIPCART, DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP,
                                                        string TRAPELLI, string TRNOMBR3)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("INSERT INTO TERCEROS (");
            //sql.AppendLine("TRCODEMP,TRCODTER,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,TRNROFAX,");
            //sql.AppendLine("TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,");
            //sql.AppendLine("TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN,TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,");
            //sql.AppendLine("TRCDCLA6,TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TRFECING,TRFECMOD,TRSALCAR,");
            //sql.AppendLine("TRCONVEN,TROBSERV,TRREQCLI,TRRETAUT,TRRETIVA,TRRETICA,TRRETFUE,TRCODALT,TRSALVEN,TRCENCOS,TRTIPCART,TRFECNAC,TRRESPAL,TRRESCUP,");
            //sql.AppendLine("TRAPELLI,TRNOMBR3) VALUES(");
            //sql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,");
            //sql.AppendLine("@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,");
            //sql.AppendLine("@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,'AC','.','ADMIN',GETDATE(),GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,");
            //sql.AppendLine("NULL,NULL,NULL,NULL,NULL,NULL,@p50,NULL,NULL,NULL,NULL)");

            sql.AppendLine("INSERT INTO TERCEROS (");
            sql.AppendLine("TRCODEMP,TRCODTER,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,TRNROFAX,");
            sql.AppendLine("TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,");
            sql.AppendLine("TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN,TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,");
            sql.AppendLine("TRCDCLA6,TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TRFECING,TRFECMOD,TRSALCAR,");
            sql.AppendLine("TRCONVEN,TROBSERV,TRREQCLI,TRRETAUT,TRRETIVA,TRRETICA,TRRETFUE,TRCODALT,TRSALVEN,TRCENCOS,TRTIPCART,TRFECNAC,TRRESPAL,TRRESCUP,");
            sql.AppendLine("TRAPELLI,TRNOMBR3) VALUES(");
            sql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,");
            sql.AppendLine("@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,");
            sql.AppendLine("@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,'AC','.','ADMIN',GETDATE(),GETDATE(),@p50,@p51,@p52,@p53,@p54,@p55,");
            sql.AppendLine("@p56,@p57,@p58,@p59,@p60,@p61,@p62,@p63,@p64,@p65,@p66)");

            try
            {
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text,
                    TRCODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL, TRNROFAX,
                    TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                    TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
                    TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, 
                    TRSALCAR, TRCONVEN, TROBSERV, TRREQCLI, TRRETAUT, TRRETIVA, TRRETICA, TRRETFUE, TRCODALT, TRSALVEN, TRCENCOS, TRTIPCART, TRFECNAC, TRRESPAL,
                    TRRESCUP, TRAPELLI, TRNOMBR3);

                //return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text,
                //    TRCODEMP, TRCODTER, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL, TRNROFAX,
                //    TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                //    TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5,
                //    TRCDCLA6, TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRFECNAC);//TRFECNAC.ToString("yyyyMMdd"));
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

        public static Boolean ExisteTercero(SessionManager oSessionManager, string TRCODNIT)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT COUNT(*) FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT =@p0 ");
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
        public static int UpdateTerceros(SessionManager oSessionManager, string TRCODEMP, string TRCODNIT, DateTime? TRFECNAC, string TRAPELLI, string TRNOMBRE, string TRCORREO, string TRDTTEC4, string TRNROTEL)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TERCEROS SET");
            sql.AppendLine("       TRFECNAC=@p0,TRFECMOD=GETDATE(),TRAPELLI=@p3,TRNOMBRE=@p4, TRCORREO=@p5, TRDTTEC4=@p6, TRNROTEL=@p7");
            sql.AppendLine(" WHERE TRCODEMP=@p1 AND TRCODNIT =@p2");
            
            try
            {
                //return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text,TRFECNAC.ToString("yyyyMMdd"),TRCODEMP,TRCODNIT );
                return DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text, TRFECNAC, TRCODEMP, TRCODNIT, TRAPELLI, TRNOMBRE, TRCORREO, TRDTTEC4, TRNROTEL);
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
        public static IDataReader GetInformacionTercero(SessionManager oSessionManager, string TBCODNIT)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT TOP 1 * FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT=@p0 ");
            try
            {
                return DBAccess.GetDataReader(oSessionManager, sql.ToString(),CommandType.Text ,TBCODNIT);
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
        public static Boolean ExisteDescuentoHappy(SessionManager oSessionManager, string TRCODNIT)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT 1 FROM TB_DETDESCUENTO WHERE ID_DESCUENTO = 37 AND CONDICION_1=@p0 ");
                sql.AppendLine("AND DATEPART(YEAR,FECHAINI) = DATEPART(YEAR,GETDATE()) ");
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
        public static IDataReader GetCiudades(SessionManager oSessionManager, string InPais, string inCodemp)
        {
            try
            {
                StringBuilder sSql = new StringBuilder();
                sSql.AppendLine("SELECT CDCIUDAD,(CDNOMBRE+'-'+CDREGION) CIUDAD FROM CIUDADES WHERE CDCODEMP=@p0 AND CDCDPAIS=@p1 AND CDESTADO='AC'");

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, inCodemp, InPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public static DataTable GetTerminosPago(SessionManager oSessionManager, string InCodemp)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBTERPAG WITH(NOLOCK)");                

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
        public static string GetMoneda(SessionManager oSessionManager, string InCodemp)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT CNMONEDA FROM COMPANIA WITH(NOLOCK) WHERE CNCODEMP =@p0");
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, InCodemp));
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
        public static int ExisteTasaCambio(SessionManager oSessionManager,string TC_CODEMP,string TC_MONEDA,DateTime TC_FECHA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_TASACAMBIO WHERE TC_CODEMP =@p0 AND TC_MONEDA =@p1 AND CONVERT(DATE,TC_FECHA,101) = CONVERT(DATE,@p2,101) AND TC_ESTADO ='AC'");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager,sSql.ToString(),CommandType.Text,TC_CODEMP,TC_MONEDA,TC_FECHA));
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
        public static int InsertTBTABLAS(SessionManager oSessionManager, string TTCODEMP, string TTCODTAB, string TTCODCLA, string TTVALORC, int? TTVALORN, double? TTVALORF, DateTime? TTVALORD, string TTDESCRI, string TTCDUSER, string TTESTADO,double? TTVLRFL2, string TTDESLAR, string TTPREGUN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBTABLAS (TTCODEMP,TTCODTAB,TTCODCLA,TTVALORC,TTVALORN,TTVALORF,TTVALORD,TTDESCRI,TTCDUSER,TTVLRFL2,TTDESLAR,TTPREGUN,TTESTADO,TTFECING,TTFECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TTCODEMP, TTCODTAB, TTCODCLA, TTVALORC, TTVALORN, TTVALORF, TTVALORD, TTDESCRI, TTCDUSER, TTVLRFL2, TTDESLAR, TTPREGUN, TTESTADO);
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
        public static int UpdateTBTABLAS(SessionManager oSessionManager, string TTCODEMP, string TTCODTAB, string TTCODCLA, string TTVALORC, int? TTVALORN, double? TTVALORF, DateTime? TTVALORD, string TTDESCRI, string TTCDUSER, string TTESTADO, double? TTVLRFL2, string TTDESLAR, string TTPREGUN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBTABLAS SET TTVALORC=@p3,TTVALORN=@p4,TTVALORF=@p5,TTVALORD=@p6,TTDESCRI=@p7,TTCDUSER=@p8,TTVLRFL2=@p9,TTDESLAR=@p10,TTPREGUN=@p11,TTESTADO=@p12,TTFECMOD=GETDATE() WHERE TTCODEMP =@p0 AND TTCODTAB =@p1 AND TTCODCLA=@p2");               
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TTCODEMP, TTCODTAB, TTCODCLA, TTVALORC, TTVALORN, TTVALORF, TTVALORD, TTDESCRI, TTCDUSER, TTVLRFL2, TTDESLAR, TTPREGUN, TTESTADO);
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
