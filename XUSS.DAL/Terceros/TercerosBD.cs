using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Terceros
{
    public class TercerosBD
    {
        //Terceros
        #region
        public static DataTable GetTerceros(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT * ,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'') +' '+ISNULL(TRNOMBR3,'')) NOM_COMPLETO FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP = '001' ");
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
        public static IDataReader GetTercerosR(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("SELECT * FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP = '001' AND TRINDEMP = 'S' ");
                sSql.AppendLine("SELECT * FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP = '001' ");
                if (!string.IsNullOrWhiteSpace(filter))
                    sSql.AppendLine("AND " + filter);

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text);
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
        public static DataTable GetEstudios(SessionManager oSessionManager, int CODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_ESTUDIOS.*, A.TTVALORC Institucion, B.TTVALORC Tipo FROM TB_ESTUDIOS WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN TBTABLAS A WITH(NOLOCK) ON (ET_CODEMP = A.TTCODEMP AND A.TTCODTAB = 'INSTI' )");
                sSql.AppendLine(" INNER JOIN TBTABLAS B WITH(NOLOCK) ON (ET_CODEMP = B.TTCODEMP AND B.TTCODTAB = 'TESTU' )");
                sSql.AppendLine(" WHERE ET_CODEMP = '001' ");
                sSql.AppendLine("   AND ET_CODTER = @p0");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,CODTER);
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
        public static DataTable GetExperiencia(SessionManager oSessionManager, int CODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_EXPERIENCIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE EP_CODEMP = '001' ");
                sSql.AppendLine("   AND EP_CODTER = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODTER);
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
        public static DataTable GetInfomormacionLaboral(SessionManager oSessionManager, int CODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_INFLABORAL WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE LB_CODEMP = '001' ");
                sSql.AppendLine("   AND LB_CODNIT = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODTER);
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
        public static DataTable GetTerminosPago(SessionManager oSessionManager, string InCodEmp)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                //sSql.AppendLine("SELECT TPTERPAG, TPNOMBRE FROM TBTERPAG WITH(NOLOCK) WHERE TPCODEMP =@p0");
                sSql.AppendLine("SELECT (A.TTCODCLA+'-'+ISNULL(B.TTCODCLA,'')) AS TPTERPAG,(A.TTDESCRI+'-'+ISNULL(B.TTDESCRI,'')) AS TPNOMBRE");
                sSql.AppendLine("FROM TBTABLAS A WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS B  WITH(NOLOCK) ON (A.TTCODEMP = B.TTCODEMP AND A.TTVALORC = B.TTCODTAB AND B.TTESTADO ='AC')");
                sSql.AppendLine("WHERE A.TTCODTAB ='PAGO' AND A.TTESTADO ='AC'");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, InCodEmp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        public static int ExisteTercero(SessionManager oSessionManager, string TRCODNIT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TERCEROS WITH(NOLOCK) WHERE TRCODNIT =@p0");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TRCODNIT));
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
        public static int InsertTercero(SessionManager oSessionManager, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI,
                                        string TRCODNIT, string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA, string TRBODEGA,
                                        string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE, string TRLISPRA, double? TRDESCUE,
                                        double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP, string TRINDSOC, string TRINDVEN,string TRINDFOR, string TRCDCLA1,
                                        string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5, string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3,
                                        string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6, int? TRPROGDT, string TRESTADO, string TRCAUSAE, string TRNMUSER, string TROBSERV,
                                        DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP, string TRAPELLI, string TRNOMBR3, string TRTIPDOC, string TRDIGCHK,
                                        string TRCODZONA, string TRTIPREG, string TRGRANCT, string TRAUTORE,string TRNOMCOMERCIAL)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TERCEROS (TRCODEMP,TRCODTER,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,");
                sSql.AppendLine("TRNROFAX,TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,");
                sSql.AppendLine("TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN,TRINDFOR,TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,TRCDCLA6,");
                sSql.AppendLine("TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TROBSERV,TRFECNAC,TRRESPAL,TRRESCUP,TRAPELLI,"); 
                sSql.AppendLine("TRNOMBR3,TRTIPDOC,TRDIGCHK,TRCODZONA,TRTIPREG,TRGRANCT,TRAUTORE,TRNOMCOMERCIAL,TRFECING,TRFECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine("@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,");
                sSql.AppendLine("@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,");
                sSql.AppendLine("@p43,@p44,@p45,@p46,@p47,@p48,@p49,@p50,@p51,@p52,@p53,@p54,@p55,@p56,@p57,");
                sSql.AppendLine("@p58,@p59,@p60,@p61,@p62,@p63,@p64,@p65,@p66,GETDATE(),GETDATE())");


                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,TRCODEMP,TRCODTER,TRNOMBRE,TRNOMBR2,TRCONTAC,TRCODEDI,TRCODNIT,TRDIGVER,TRDIRECC,TRDIREC2,TRDELEGA,TRCOLONI,TRNROTEL,
                TRNROFAX,TRPOSTAL,TRCORREO,TRCIUDAD,TRCIUDA2,TRCDPAIS,TRMONEDA,TRIDIOMA,TRBODEGA,TRTERPAG,TRMODDES,TRTERDES,TRCATEGO,TRAGENTE,TRLISPRE,
                TRLISPRA,TRDESCUE,TRCUPOCR,TRINDCLI,TRINDPRO,TRINDSOP,TRINDEMP,TRINDSOC,TRINDVEN, TRINDFOR, TRCDCLA1,TRCDCLA2,TRCDCLA3,TRCDCLA4,TRCDCLA5,TRCDCLA6,
                TRDTTEC1,TRDTTEC2,TRDTTEC3,TRDTTEC4,TRDTTEC5,TRDTTEC6,TRPROGDT,TRESTADO,TRCAUSAE,TRNMUSER,TROBSERV,TRFECNAC,TRRESPAL,TRRESCUP,TRAPELLI,
                TRNOMBR3,TRTIPDOC,TRDIGCHK,TRCODZONA,TRTIPREG,TRGRANCT,TRAUTORE, TRNOMCOMERCIAL);
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

        public static int UpdateTercero(SessionManager oSessionManager, string TRCODEMP, int TRCODTER, string TRNOMBRE, string TRNOMBR2, string TRCONTAC, int? TRCODEDI,
                                        string TRCODNIT, string TRDIGVER, string TRDIRECC, string TRDIREC2, string TRDELEGA, string TRCOLONI, string TRNROTEL, string TRNROFAX,
                                        string TRPOSTAL, string TRCORREO, string TRCIUDAD, string TRCIUDA2, string TRCDPAIS, string TRMONEDA, string TRIDIOMA, string TRBODEGA,
                                        string TRTERPAG, string TRMODDES, string TRTERDES, string TRCATEGO, int? TRAGENTE, string TRLISPRE, string TRLISPRA, double? TRDESCUE,
                                        double? TRCUPOCR, string TRINDCLI, string TRINDPRO, string TRINDSOP, string TRINDEMP, string TRINDSOC, string TRINDVEN,string TRINDFOR, string TRCDCLA1,
                                        string TRCDCLA2, string TRCDCLA3, string TRCDCLA4, string TRCDCLA5, string TRCDCLA6, string TRDTTEC1, string TRDTTEC2, string TRDTTEC3,
                                        string TRDTTEC4, double? TRDTTEC5, double? TRDTTEC6, int? TRPROGDT, string TRESTADO, string TRCAUSAE, string TRNMUSER, string TROBSERV,
                                        DateTime? TRFECNAC, string TRRESPAL, double? TRRESCUP, string TRAPELLI, string TRNOMBR3, string TRTIPDOC, string TRDIGCHK,
                                        string TRCODZONA, string TRTIPREG, string TRGRANCT, string TRAUTORE,string TRNOMCOMERCIAL)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TERCEROS SET TRCODEMP=@p0,TRNOMBRE=@p1,TRNOMBR2=@p2,TRCONTAC=@p3,TRCODEDI=@p4,TRCODNIT=@p5,TRDIGVER=@p6,TRDIRECC=@p7,TRDIREC2=@p8,TRDELEGA=@p9,TRCOLONI=@p10,TRNROTEL=@p11,");
                sSql.AppendLine("TRNROFAX=@p12,TRPOSTAL=@p13,TRCORREO=@p14,TRCIUDAD=@p15,TRCIUDA2=@p16,TRCDPAIS=@p17,TRMONEDA=@p18,TRIDIOMA=@p19,TRBODEGA=@p20,TRTERPAG=@p21,TRMODDES=@p22,TRTERDES=@p23,TRCATEGO=@p24,TRAGENTE=@p25,TRLISPRE=@p26,");
                sSql.AppendLine("TRLISPRA=@p27,TRDESCUE=@p28,TRCUPOCR=@p29,TRINDCLI=@p30,TRINDPRO=@p31,TRINDSOP=@p32,TRINDEMP=@p33,TRINDSOC=@p34,TRINDVEN=@p35,TRCDCLA1=@p36,TRCDCLA2=@p37,TRCDCLA3=@p38,TRCDCLA4=@p39,TRCDCLA5=@p40,TRCDCLA6=@p41,");
                sSql.AppendLine("TRDTTEC1=@p42,TRDTTEC2=@p43,TRDTTEC3=@p44,TRDTTEC4=@p45,TRDTTEC5=@p46,TRDTTEC6=@p47,TRPROGDT=@p48,TRESTADO=@p49,TRCAUSAE=@p50,TRNMUSER=@p51,TROBSERV=@p52,TRFECNAC=@p53,TRRESPAL=@p54,TRRESCUP=@p55,TRAPELLI=@p56,");
                sSql.AppendLine("TRNOMBR3=@p57,TRTIPDOC=@p58,TRDIGCHK=@p59,TRCODZONA=@p60,TRTIPREG=@p61,TRGRANCT=@p62,TRAUTORE=@p63,TRNOMCOMERCIAL=@p64,TRINDFOR=@p65,TRFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE TRCODTER=@p66");
                


                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRNOMBRE, TRNOMBR2, TRCONTAC, TRCODEDI, TRCODNIT, TRDIGVER, TRDIRECC, TRDIREC2, TRDELEGA, TRCOLONI, TRNROTEL,
                TRNROFAX, TRPOSTAL, TRCORREO, TRCIUDAD, TRCIUDA2, TRCDPAIS, TRMONEDA, TRIDIOMA, TRBODEGA, TRTERPAG, TRMODDES, TRTERDES, TRCATEGO, TRAGENTE, TRLISPRE,
                TRLISPRA, TRDESCUE, TRCUPOCR, TRINDCLI, TRINDPRO, TRINDSOP, TRINDEMP, TRINDSOC, TRINDVEN, TRCDCLA1, TRCDCLA2, TRCDCLA3, TRCDCLA4, TRCDCLA5, TRCDCLA6,
                TRDTTEC1, TRDTTEC2, TRDTTEC3, TRDTTEC4, TRDTTEC5, TRDTTEC6, TRPROGDT, TRESTADO, TRCAUSAE, TRNMUSER, TROBSERV, TRFECNAC, TRRESPAL, TRRESCUP, TRAPELLI,
                TRNOMBR3, TRTIPDOC, TRDIGCHK, TRCODZONA, TRTIPREG, TRGRANCT, TRAUTORE, TRNOMCOMERCIAL, TRINDFOR, TRCODTER);
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
        public static int ExisteTerceroIden(SessionManager oSessionManager, string TRCODEMP, string TRCODNIT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TERCEROS WITH(NOLOCK) WHERE TRCODEMP =@p0 AND TRCODNIT= @p1 ");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODNIT));
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
        //Sucursales
        #region
        public static DataTable GetSucursales(SessionManager oSessionManager, string SC_CODEMP,int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT * FROM TB_SUCURSAL WITH(NOLOCK) WHERE SC_CODEMP = @p0 AND TRCODTER=@p1 ");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,SC_CODEMP,TRCODTER);
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
        public static int InsertSucursales(SessionManager oSessionManager, string SC_CODEMP, int TRCODTER,string SC_NOMBRE,string SC_TELEFONO,string SC_DIRECCION,
                                                string SC_DIRECCION2,string SC_PAIS,string SC_CIUDAD,string SC_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SUCURSAL (SC_CODEMP,TRCODTER,SC_NOMBRE,SC_TELEFONO,SC_DIRECCION,SC_DIRECCION2,SC_PAIS,CDCIUDAD,SC_ESTADO,SC_FECMOD,SC_FECING) ");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SC_CODEMP, TRCODTER, SC_NOMBRE, SC_TELEFONO, SC_DIRECCION, SC_DIRECCION2,
                                            SC_PAIS, SC_CIUDAD, SC_ESTADO);
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
        public static IDataReader GetSucursalesID(SessionManager oSessionManager, string SC_CODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_SUCURSAL WITH(NOLOCK) WHERE SC_CODEMP = @p0 AND TRCODTER=@p1 ");

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, SC_CODEMP, TRCODTER);
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
        //Impuestos
        #region        
        public static int ExisteCuentaXTercero(SessionManager oSessionManager, int CTT_ID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) ");
                sSql.AppendLine("FROM CONT_CTA_TERCERO WITH(NOLOCK)");                
                sSql.AppendLine("WHERE CTT_ID = @p0 ");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, CTT_ID));
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
        public static DataTable GetCuentasxTercero(SessionManager oSessionManager, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT CONT_CTA_TERCERO.*, CASE WHEN CTT_TIPPLA = 1 THEN 'P. Ventas'  WHEN CTT_TIPPLA = 1 THEN 'P. Compras' END T_PLA,PC_CODIGO,PC_NOMBRE,TTDESCRI ");
                sSql.AppendLine("FROM CONT_CTA_TERCERO WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PUC WITH(NOLOCK) ON (TB_PUC.PC_ID = CONT_CTA_TERCERO.PC_ID)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODTAB = 'IMPF' AND TTCODCLA = CTT_IMPUESTO)");
                sSql.AppendLine("WHERE TRCODTER = @p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER);
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
        public static int InsertCuentasxTercero(SessionManager oSessionManager, int PC_ID, int TRCODTER,string CTT_NATURALEZA,string CTT_BASE,string CTT_IMPUESTO,int CTT_TIPPLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO CONT_CTA_TERCERO (PC_ID,TRCODTER,CTT_NATURALEZA,CTT_BASE,CTT_IMPUESTO,CTT_TIPPLA) VALUES (@p0,@p1,@p2,@p3,@p4,@p5) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PC_ID, TRCODTER, CTT_NATURALEZA, CTT_BASE, CTT_IMPUESTO, CTT_TIPPLA);
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
        public static int UpdateCuentasxTercero(SessionManager oSessionManager, int CTT_ID, int PC_ID,string CTT_NATURALEZA, string CTT_BASE, string CTT_IMPUESTO, int CTT_TIPPLA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE CONT_CTA_TERCERO SET PC_ID=@p1,CTT_NATURALEZA=@p2,CTT_BASE=@p3,CTT_IMPUESTO=@p4,CTT_TIPPLA=@p5 WHERE CTT_ID=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CTT_ID, PC_ID, CTT_NATURALEZA, CTT_BASE, CTT_IMPUESTO, CTT_TIPPLA);
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
        public static DataTable GetImpuestosxTercero(SessionManager oSessionManager, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PLANILLA_IMPHD.*, CASE WHEN PH_TIPPLA = 1 THEN 'P. Ventas'  WHEN PH_TIPPLA = 1 THEN 'P. Compras' END T_PLA ");
                sSql.AppendLine("FROM TB_PLANILLA_IMP_TERCEROS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TB_PLANILLA_IMPHD ON(TB_PLANILLA_IMP_TERCEROS.PH_CODIGO = TB_PLANILLA_IMPHD.PH_CODIGO)");
                sSql.AppendLine("WHERE TRCODTER = @p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODTER);
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
        public static int InsertImpuestosxTercero(SessionManager oSessionManager, int PH_CODIGO, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PLANILLA_IMP_TERCEROS (PH_CODIGO,TRCODTER) VALUES (@p0,@p1) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO,TRCODTER);
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
        public static int DeleteImpuestosxTercero(SessionManager oSessionManager, int PH_CODIGO, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_PLANILLA_IMP_TERCEROS WHERE PH_CODIGO=@p0 AND TRCODTER=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, TRCODTER);
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
        //Familia & Titulos
        #region
        public static DataTable GetOtrosAnexos(SessionManager oSessionManager, string TRCODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT OA_CONSECUTIVO,OA_DESCRIPCION,OA_ALERTA,OA_FECVENCIMINTO FROM TB_OTROSANEXOS WITH(NOLOCK) ");                
                sSql.AppendLine(" WHERE TRCODEMP = @p0 ");
                sSql.AppendLine("   AND TRCODTER = @p1");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODTER);
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
        public static DataTable GetFamilia(SessionManager oSessionManager, string TRCODEMP ,int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_FAMILIA.*,TTDESCRI,CASE WHEN FM_TIPO = '1' THEN 'Familiar' WHEN FM_TIPO = '2' THEN 'Personal' END NOM_TIPO  FROM TB_FAMILIA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = FM_CODEMP AND TTCODCLA = FM_PARENTESCO AND TTCODTAB = 'PARE')");
                sSql.AppendLine(" WHERE FM_CODEMP = @p0 ");
                sSql.AppendLine("   AND TRCODTER = @p1");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODTER);
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
        public static int InsertFamilia(SessionManager oSessionManager, string FM_CODEMP, int TRCODTER, string FM_TIPDOC, string FM_IDENTIFICACION, string FM_PNOMBRE, string FM_SNOMBRE, string FM_PAPELLIDO, string FM_SAPELLIDO, DateTime? FM_FNACIMIENTO, 
                                string FM_PARENTESCO, string FM_DIRECCION, string FM_EMAIL, string FM_TELEFONO, string FM_TIPO, string FM_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_FAMILIA (FM_CODEMP,TRCODTER,FM_TIPDOC,FM_IDENTIFICACION,FM_PNOMBRE,FM_SNOMBRE,FM_PAPELLIDO,FM_SAPELLIDO,FM_FNACIMIENTO,FM_PARENTESCO,FM_DIRECCION,FM_EMAIL,FM_TELEFONO,FM_TIPO,FM_USUARIO,FM_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FM_CODEMP, TRCODTER, FM_TIPDOC, FM_IDENTIFICACION, FM_PNOMBRE, FM_SNOMBRE, FM_PAPELLIDO, FM_SAPELLIDO, FM_FNACIMIENTO, FM_PARENTESCO, FM_DIRECCION, FM_EMAIL, FM_TELEFONO, FM_TIPO, FM_USUARIO);
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
        public static DataTable GetTitulos(SessionManager oSessionManager, string TRCODEMP, int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TT_CODIGO,TT_CODEMP,TRCODTER,TT_TIPO,TT_PROFESION,TT_DESCRIPCION,TT_FECHA,TT_FECHAVEN,TT_ALERTA,TT_USUARIO,TT_FECING,A.TTDESCRI TIPO_ESTUDIO,B.TTDESCRI PROFESION  ");
                sSql.AppendLine(" FROM TB_TITULOS WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTABLAS A WITH(NOLOCK) ON (A.TTCODEMP = TT_CODEMP AND A.TTCODCLA = TT_TIPO AND A.TTCODTAB = 'ESTU')");
                sSql.AppendLine("INNER JOIN TBTABLAS B WITH(NOLOCK) ON (B.TTCODEMP = TT_CODEMP AND B.TTCODCLA = TT_PROFESION AND B.TTCODTAB = 'PROF')");
                sSql.AppendLine(" WHERE TT_CODEMP = @p0 ");
                sSql.AppendLine("   AND TRCODTER = @p1");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODTER);
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
        public static int InserTitulos(SessionManager oSessionManager, string TT_CODEMP, int TRCODTER, string TT_TIPO, string TT_PROFESION, string TT_DESCRIPCION, DateTime? TT_FECHA, DateTime? TT_FECHAVEN, string TT_ALERTA, object TT_ADJUNTO, string TT_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_TITULOS (TT_CODEMP,TRCODTER,TT_TIPO,TT_PROFESION,TT_DESCRIPCION,TT_FECHA,TT_FECHAVEN,TT_ALERTA,TT_ADJUNTO,TT_USUARIO,TT_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TT_CODEMP, TRCODTER, TT_TIPO, TT_PROFESION, TT_DESCRIPCION, TT_FECHA, TT_FECHAVEN, TT_ALERTA, TT_ADJUNTO ,TT_USUARIO);
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
        public static int InsertOtrosAnexos(SessionManager oSessionManager, string TRCODEMP, int TRCODTER, string OA_DESCRIPCION, string OA_ALERTA, DateTime? OA_FECVENCIMINTO, object OA_IMAGEN, string OA_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_OTROSANEXOS (TRCODEMP,TRCODTER,OA_DESCRIPCION,OA_ALERTA,OA_FECVENCIMINTO,OA_IMAGEN,OA_USUARIO,OA_FECING)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODTER, OA_DESCRIPCION, OA_ALERTA, OA_FECVENCIMINTO, OA_IMAGEN, OA_USUARIO);
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

        public static int DeleteFamilia(SessionManager oSessionManager, int FM_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_FAMILIA WHERE FM_CODIGO = @p0");                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FM_CODIGO);
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
        public static int DeleteTitulos(SessionManager oSessionManager, int TT_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_TITULOS WHERE TT_CODIGO = @p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TT_CODIGO);
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
        public static int DeleteOtrosAnexos(SessionManager oSessionManager, int OA_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_TITULOS WHERE  OA_CONSECUTIVO = @p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, OA_CONSECUTIVO);
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
        public static object GetImagenOtros(SessionManager oSessionManager, int OA_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT OA_IMAGEN FROM TB_OTROSANEXOS WITH(NOLOCK) WHERE OA_CONSECUTIVO=@p0");
                return DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, OA_CONSECUTIVO);

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
        public static object GetImagenAcademico(SessionManager oSessionManager, int TT_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TT_ADJUNTO FROM TB_TITULOS WITH(NOLOCK) WHERE TT_CODIGO=@p0");
                return DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, TT_CODIGO);

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
        //Nomina
        #region
        public static DataTable GetContratos(SessionManager oSessionManager, string TRCODEMP,int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_NMCONTRATOS.*,A.TTDESCRI T_CONTRATO,B.TTDESCRI T_NOVEDAD, C.TTDESCRI T_CARGO");
                sSql.AppendLine("FROM TB_NMCONTRATOS WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTABLAS A WITH(NOLOCK) ON(A.TTCODEMP = TRCODEMP AND A.TTCODTAB = 'TICO' AND CT_TCONTRATO = A.TTCODCLA)");
                sSql.AppendLine("INNER JOIN TBTABLAS B WITH(NOLOCK) ON(B.TTCODEMP = TRCODEMP AND B.TTCODTAB = 'TNOVE' AND CT_TNOVEDAD = B.TTCODCLA)");
                sSql.AppendLine("INNER JOIN TBTABLAS C WITH(NOLOCK) ON(C.TTCODEMP = TRCODEMP AND C.TTCODTAB = 'CARGO' AND CT_CARGO = C.TTCODCLA)");
                sSql.AppendLine("WHERE TRCODEMP=@p0 AND TRCODTER =@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP,TRCODTER);
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
        public static int InsertContratos(SessionManager oSessionManager,string TRCODEMP,int TRCODTER,string CT_TNOVEDAD,string CT_TCONTRATO, string CT_CARGO, DateTime CT_FINGRESO, double CT_SALARIO,string CT_USUARIO,string CT_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_NMCONTRATOS (TRCODEMP,TRCODTER,CT_TNOVEDAD,CT_TCONTRATO, CT_CARGO, CT_FINGRESO, CT_SALARIO,CT_USUARIO,CT_ESTADO,CT_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TRCODEMP, TRCODTER, CT_TNOVEDAD, CT_TCONTRATO, CT_CARGO, CT_FINGRESO, CT_SALARIO, CT_USUARIO, CT_ESTADO);
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
        public static int UpdateContratos(SessionManager oSessionManager, int CT_ID, string CT_TNOVEDAD, string CT_TCONTRATO, string CT_CARGO, DateTime CT_FINGRESO,DateTime? CT_FTERMINACION, double CT_SALARIO, string CT_USUARIO, string CT_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_NMCONTRATOS SET CT_TNOVEDAD=@p0,CT_TCONTRATO=@p1, CT_CARGO=@p2, CT_FINGRESO=@p3,CT_FTERMINACION=@p4, CT_SALARIO=@p5,CT_USUARIO=@p6,CT_ESTADO=@p7,CT_FECING=GETDATE() WHERE CT_ID =@p8");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CT_TNOVEDAD, CT_TCONTRATO, CT_CARGO, CT_FINGRESO, CT_FTERMINACION, CT_SALARIO, CT_USUARIO, CT_ESTADO, CT_ID);
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
        public static int DeleteContratos(SessionManager oSessionManager, int CT_ID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_NMCONTRATOS WHERE CT_ID =@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, CT_ID);
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
        public static DataTable GetTercerosPlanillaNomina(SessionManager oSessionManager,string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TERCEROS.TRCODTER,TRNOMBRE,TRNOMBR2,TRAPELLI,TRNOMBR3,TRCODNIT,TRTIPDOC,A.TTDESCRI T_CONTRATO,C.TTDESCRI T_CARGO,(TRNOMBRE+' '+ISNULL(TRNOMBR2,'') + ' ' + ISNULL(TRAPELLI,'')) EMPLEADO,CT_FINGRESO,CT_FTERMINACION");
                sSql.AppendLine("FROM TERCEROS WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TB_NMCONTRATOS WITH(NOLOCK) ON(TERCEROS.TRCODTER = TB_NMCONTRATOS.TRCODTER AND TERCEROS.TRCODEMP = TB_NMCONTRATOS.TRCODEMP)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS A WITH(NOLOCK) ON(A.TTCODEMP = TERCEROS.TRCODEMP AND A.TTCODTAB = 'TICO' AND CT_TCONTRATO = A.TTCODCLA)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS C WITH(NOLOCK) ON(C.TTCODEMP = TERCEROS.TRCODEMP AND C.TTCODTAB = 'CARGO' AND CT_CARGO = C.TTCODCLA)");
                sSql.AppendLine("WHERE TRINDEMP = 'S' AND TRESTADO = 'AC' "+ filter);

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
        //Propiedad Horizontal
        #region
        public static DataTable GetPropiedadHorizontal(SessionManager oSessionManager, string PH_CODEMP,int TRCODTER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_PROPIEDAHORIZONTAL.*,IT_USUARIO,IT_ESTADO,IT_FECHA, MECDELEM,ARNOMBRE,inst.usua_nombres tecnico_inst,ISNULL(TTDESCRI,'Pendiente Instalar') TTDESCRI,CO_FECHA,DI_FECHA,DI_USUARIO,des.usua_nombres tec_desistala,");
                //sSql.AppendLine("CASE WHEN CO_ESTADO = 'CO' THEN 'Comerzializado' ELSE 'Por Comercializar' END ESTADO_CO, com.usua_nombres comercial,(TR.TRNOMBRE + ' ' + ISNULL(TR.TRNOMBR2, '') + ' ' + ISNULL(TR.TRAPELLI, '') + ' ' + ISNULL(TR.TRNOMBR3, '')) CLIENTE,TR.TRNROTEL, TR.TRCODNIT,");
                sSql.AppendLine("CASE WHEN CO_ESTADO = 'CO' THEN 'Pendiente Radicar Oficina' ELSE 'Por Comercializar' END ESTADO_CO, com.usua_nombres comercial,(TR.TRNOMBRE + ' ' + ISNULL(TR.TRNOMBR2, '') + ' ' + ISNULL(TR.TRAPELLI, '') + ' ' + ISNULL(TR.TRNOMBR3, '')) CLIENTE,TR.TRNROTEL, TR.TRCODNIT,");                
                sSql.AppendLine("TR.TRNOMBRE,TR.TRNOMBR2,TR.TRAPELLI,TR.TRNOMBR3,CO_USUARIO,CIH_FECHA,CID_ESTADO,CO_FECCOMODATO,CO_FECPAGARE,COH_FECHA,COD_ESTADO,COD_CAUSAE,CID_CAUSAE,TB_INSTALACION.CP_ID, CP_NROCAMPANA, CP_VALOR,");
                sSql.AppendLine("CASE");
                sSql.AppendLine("WHEN DI_CODIGO IS NOT NULL THEN 'Desmontado'");
                sSql.AppendLine("WHEN(SELECT TOP 1 TK_TIPO FROM TB_TICKETHD WHERE TB_TICKETHD.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO AND TK_ESTADO = 'AC' AND TK_TIPO = '0') = '0' THEN 'Proceso Desmonte'");
                sSql.AppendLine("WHEN TB_CORRESPONDENCIADTOUT.COD_ITEM IS NOT NULL THEN 'Radicado Vanti'");
                sSql.AppendLine("WHEN TB_CORRESPONDENCIADTIN.CID_ITEM IS NOT NULL THEN 'Radicado Oficina'");
                sSql.AppendLine("WHEN TB_COMERCIAL.CO_CODIGO IS NOT NULL THEN 'Pendiente Radicar Oficina'");
                sSql.AppendLine("WHEN TB_COMERCIAL.CO_CODIGO IS NULL THEN 'Pendiente Comercializar'");
                sSql.AppendLine("WHEN TB_INSTALACION.PH_CODIGO IS NOT NULL THEN 'Instalado'");
                sSql.AppendLine("WHEN TB_INSTALACION.PH_CODIGO IS NULL THEN 'Pendiente Instalar'");
                sSql.AppendLine("END ESTADO_T, TR.TRCODTER CODCLI");
                sSql.AppendLine("FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TB_INSTALACION WITH(NOLOCK) ON(TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_INSTALACION.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN TB_CAMPANA WITH(NOLOCK) ON (TB_INSTALACION.CP_ID = TB_CAMPANA.CP_ID)");
                sSql.AppendLine("LEFT OUTER JOIN MOVIMELE WITH(NOLOCK) ON(TB_INSTALACION.IT_CODEMP = MECODEMP AND TB_INSTALACION.MEIDMOVI = MOVIMELE.MEIDMOVI AND TB_INSTALACION.MEIDITEM = MOVIMELE.MEIDITEM");                
                sSql.AppendLine("AND TB_INSTALACION.MEIDLOTE = MOVIMELE.MEIDLOTE AND TB_INSTALACION.MEIDELEM = MOVIMELE.MEIDELEM)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = MECODEMP AND ARTIPPRO = METIPPRO AND ARCLAVE1 = MECLAVE1 AND ARCLAVE2 = MECLAVE2 AND ARCLAVE3 = MECLAVE3 AND ARCLAVE4 = MECLAVE4)");
                sSql.AppendLine("LEFT OUTER JOIN admi_tusuario inst WITH(NOLOCK) ON (TB_INSTALACION.IT_USUARIO = inst.usua_usuario)");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON(TTCODEMP = MECODEMP AND TTCODTAB = 'ESTINT' AND TTCODCLA = IT_ESTADO)");
                sSql.AppendLine("LEFT OUTER JOIN TB_COMERCIAL WITH(NOLOCK) ON(TB_COMERCIAL.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN admi_tusuario com WITH(NOLOCK) ON(TB_COMERCIAL.CO_USUARIO = com.usua_usuario)");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS TR WITH(NOLOCK) ON(TB_COMERCIAL.TRCODTER = TR.TRCODTER)");
                sSql.AppendLine("LEFT OUTER JOIN TB_DESISTALACION WITH(NOLOCK) ON(TB_DESISTALACION.PH_CODIGO = TB_PROPIEDAHORIZONTAL.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN admi_tusuario des WITH(NOLOCK) ON(TB_DESISTALACION.DI_USUARIO = des.usua_usuario)");
                sSql.AppendLine("LEFT OUTER JOIN TB_CORRESPONDENCIADTIN WITH(NOLOCK) ON (TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_CORRESPONDENCIADTIN.PH_CODIGO AND TB_CORRESPONDENCIADTIN.CID_ESTADO NOT IN ('AN') )");
                sSql.AppendLine("LEFT OUTER JOIN TB_CORRESPONDENCIAHDIN WITH(NOLOCK) ON(TB_CORRESPONDENCIADTIN.CIH_CODIGO = TB_CORRESPONDENCIAHDIN.CIH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN TB_CORRESPONDENCIADTOUT WITH(NOLOCK) ON (TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_CORRESPONDENCIADTOUT.PH_CODIGO)");
                sSql.AppendLine("LEFT OUTER JOIN TB_CORRESPONDENCIAHDOUT WITH(NOLOCK) ON(TB_CORRESPONDENCIADTOUT.COH_CODIGO = TB_CORRESPONDENCIAHDOUT.COH_CODIGO)");
                sSql.AppendLine("WHERE TB_PROPIEDAHORIZONTAL.PH_CODEMP=@p0 AND TB_PROPIEDAHORIZONTAL.TRCODTER=@p1");
                sSql.AppendLine("ORDER BY TB_PROPIEDAHORIZONTAL.PH_EDIFICIO,TB_PROPIEDAHORIZONTAL.PH_ESCALERA");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,PH_CODEMP,TRCODTER);
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
        public static DataTable GetImagenesInstalacion(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM (");
                sSql.AppendLine("SELECT IT_CODIGO,IT_FOTO1 IT_FOTO FROM TB_INSTALACION WITH(NOLOCK) WHERE PH_CODIGO=@p0");
                sSql.AppendLine("UNION ALL ");
                sSql.AppendLine("SELECT IT_CODIGO,IT_FOTO2 FROM TB_INSTALACION WITH(NOLOCK) WHERE PH_CODIGO=@p0) XX");                                

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static DataTable GetImagenesComercial(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                sSql.AppendLine("SELECT EC_CODIGO,EC_IMAGEN FROM TB_EVIDENCIA_COMERCIAL WITH(NOLOCK) WHERE PH_CODIGO=@p0");                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static object GetImageneComercial(SessionManager oSessionManager, int EC_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EC_IMAGEN FROM TB_EVIDENCIA_COMERCIAL WITH(NOLOCK) WHERE EC_CODIGO=@p0");
                return DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, EC_CODIGO);
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
        public static DataTable GetImageneDesmonte(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ED_CODIGO,ED_IMAGEN FROM TB_EVIDENCIA_DESMONTE WITH(NOLOCK) WHERE PH_CODIGO=@p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static int InsertPropiedadHorizontal(SessionManager oSessionManager, string PH_CTACONTRATO, string PH_POLIZA, string PH_CODEMP, int TRCODTER, string PH_EDIFICIO, string PH_PORTAL, string PH_PISO, string PH_ESCALERA, string PH_OBJCONEXION, string PH_PTOSUMINISTRO, 
            string PH_INSTALACION, string PH_UBCAPARATO, string PH_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_PROPIEDAHORIZONTAL (PH_CTACONTRATO,PH_POLIZA,PH_CODEMP,TRCODTER,PH_EDIFICIO,PH_PORTAL,PH_PISO,PH_ESCALERA,PH_OBJCONEXION,PH_PTOSUMINISTRO,PH_INSTALACION,PH_UBCAPARATO,PH_USUARIO,PH_FECING) VALUES ");                
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CTACONTRATO, PH_POLIZA, PH_CODEMP, TRCODTER, PH_EDIFICIO, PH_PORTAL, PH_PISO, PH_ESCALERA, PH_OBJCONEXION, PH_PTOSUMINISTRO, PH_INSTALACION, PH_UBCAPARATO, PH_USUARIO);
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
        public static int UpdatePropiedadHorizontal(SessionManager oSessionManager, string PH_CTACONTRATO, string PH_POLIZA, string PH_OBJCONEXION, string PH_PTOSUMINISTRO,
            string PH_INSTALACION, string PH_UBCAPARATO, string PH_USUARIO, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_PROPIEDAHORIZONTAL SET PH_CTACONTRATO=@p0,PH_POLIZA=@p1,PH_OBJCONEXION=@p2,PH_PTOSUMINISTRO=@p3,PH_INSTALACION=@p4,PH_UBCAPARATO=@p5,PH_USUARIO=@p6");
                sSql.AppendLine(" WHERE PH_CODIGO=@p7");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CTACONTRATO, PH_POLIZA, PH_OBJCONEXION, PH_PTOSUMINISTRO, PH_INSTALACION, PH_UBCAPARATO, PH_USUARIO, PH_CODIGO);
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
        public static int UpdateCampana(SessionManager oSessionManager, int PH_CODIGO,int? CP_ID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_INSTALACION SET CP_ID=@p1");
                sSql.AppendLine(" WHERE PH_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO,CP_ID);
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
        public static int ExistePropiedaHorizontal(SessionManager oSessionManager, string PH_CODEMP, int TRCODTER, string PH_EDIFICIO, string PH_ESCALERA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE PH_CODEMP =@p0 AND TRCODTER =@p1 AND PH_EDIFICIO =@p2 AND PH_ESCALERA =@p3");                

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODEMP, TRCODTER, PH_EDIFICIO, PH_ESCALERA));
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
        public static int InsertTranDesmonte(SessionManager oSessionManager, int? PH_CODIGO, int TK_NUMERO, string TD_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_TRANDESMONTES (PH_CODIGO,TK_NUMERO,TD_ESTADO) VALUES (@p0,@p1,@p2) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO,TK_NUMERO,TD_ESTADO);
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
        public static int InsertTranServicio(SessionManager oSessionManager, string SV_CODEMP, int? PH_CODIGO, int TK_NUMERO, string SV_USUARIO, string SV_ESTADO,string SV_CAUSAE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_SERVICIOS (SV_CODEMP, PH_CODIGO, TK_NUMERO, SV_USUARIO,SV_ESTADO,SV_CAUSAE,SV_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE()) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, SV_CODEMP,PH_CODIGO, TK_NUMERO, SV_USUARIO, SV_ESTADO, SV_CAUSAE);
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
        public static DataTable GetgestionP(SessionManager oSessionManager, string SV_CODEMP, int? PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_GESTIONP.*,usua_nombres FROM TB_GESTIONP WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN admi_tusuario WITH(NOLOCK) ON (usua_usuario = GP_USUARIO)");
                sSql.AppendLine(" WHERE PH_CODIGO=@p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static DataTable GetServicios(SessionManager oSessionManager, string SV_CODEMP, int? PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TB_SERVICIOS.*,usua_nombres FROM TB_SERVICIOS WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN admi_tusuario WITH(NOLOCK) ON (usua_usuario = SV_USUARIO)");
                sSql.AppendLine(" WHERE PH_CODIGO=@p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static DataTable GetCampanas(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_CAMPANA WITH(NOLOCK) ");                                
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
        public static int ExisteDesmontaje(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(PH_CODIGO) FROM TB_DESISTALACION WITH(NOLOCK) WHERE PH_CODIGO=@p0");                

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO));
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
        public static int InsertDesmontaje(SessionManager oSessionManager, int PH_CODIGO, string DI_CODEMP, DateTime DI_FECHA, string DI_USUARIO,string DI_NRODOC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_DESISTALACION (PH_CODIGO,DI_CODEMP,DI_FECHA,DI_USUARIO,DI_NRODOC,DI_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO, DI_CODEMP, DI_FECHA, DI_USUARIO, DI_NRODOC);
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
        public static int DeleteDesmontaje(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_DESISTALACION WHERE PH_CODIGO=@p0");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static int DeleteEvidenciaComercial(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_EVIDENCIA_COMERCIAL WHERE PH_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
        public static int DeleteComercial(SessionManager oSessionManager, int PH_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_COMERCIAL WHERE PH_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PH_CODIGO);
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
