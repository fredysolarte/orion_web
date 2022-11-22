using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Articulos
{
    public class ArticulosBD
    {
        public static DataTable GetArticulos(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARNOMBRE,ARUNDINV,ARUMALT1,ARUMALT2,ARFCA1IN,ARFCA2IN,ARCDALTR,");
                sql.AppendLine("       ARMONEDA,ARCOSTOA,ARCSTMPR,ARCSTMOB,ARCSTCIF,ARCOSTOB,ARPRECIO,ARCDIMPF,ARORIGEN,ARPOSARA,ARPESOUN,ARPESOUM,ARCDCLA1,");
                sql.AppendLine("       ARCDCLA2,ARCDCLA3,ARCDCLA4,ARCDCLA6,ARCDCLA7,ARCDCLA8,ARCDCLA9,ARCDCLA10,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8,ARDTTEC9,ARDTTEC10,ARCODPRO,ARFECCOM,ARPRECOM,");
                sql.AppendLine("       ARMONCOM,ARPROCOM,ARPROGDT,ARESTADO,ARCAUSAE,ARNMUSER,ARFECING,ARFECMOD,ARFCINA1,ARCOMPOS,ARCONVEN,ARMERCON,ARCODCOM,");
                sql.AppendLine("       ARCDCLA5,ARCAOPDS,ARUNDODS,ARTIPTAR,ARANO,ARCOLECCION,ARPRIORIDAD,TR_PROCEDENCIA,TR_UEN,TR_TP,TR_SCT,TR_FONDO,TR_TEJIDO");
                sql.AppendLine("  FROM ARTICULO WITH(NOLOCK) ");                
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
        public static DataTable GetArticulosD(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT DISTINCT ARCODEMP,ARTIPPRO,ARCLAVE1,'.' ARCLAVE2,'.' ARCLAVE3,'.' ARCLAVE4,ARNOMBRE,ARUNDINV,ARUMALT1,ARUMALT2,ARFCA1IN,ARFCA2IN,ARCDALTR,");
                sql.AppendLine("       ARMONEDA,ARCOSTOA,ARCSTMPR,ARCSTMOB,ARCSTCIF,ARCOSTOB,ARPRECIO,ARCDIMPF,ARORIGEN,ARPOSARA,ARPESOUN,ARPESOUM,ARCDCLA1,");
                sql.AppendLine("       ARCDCLA2,ARCDCLA3,ARCDCLA4,ARCDCLA6,ARCDCLA7,ARCDCLA8,ARCDCLA9,ARCDCLA10,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8,ARDTTEC9,ARDTTEC10,ARCODPRO,ARFECCOM,ARPRECOM,");
                sql.AppendLine("       ARMONCOM,ARPROCOM,ARPROGDT,ARESTADO,ARCAUSAE,ARNMUSER,ARFECING,ARFECMOD,ARFCINA1,ARCOMPOS,ARCONVEN,ARMERCON,ARCODCOM,");
                sql.AppendLine("       ARCDCLA5,ARCAOPDS,ARUNDODS,ARTIPTAR,ARANO,ARCOLECCION,ARPRIORIDAD,TR_PROCEDENCIA,TR_UEN,TR_TP,TR_SCT,TR_FONDO,TR_TEJIDO");
                sql.AppendLine("  FROM ARTICULO WITH(NOLOCK) ");
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
        public static DataTable GetArticulosDINV(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT * FROM (SELECT DISTINCT ARCODEMP,ARTIPPRO,ARCLAVE1,'.' ARCLAVE2,'.' ARCLAVE3,'.' ARCLAVE4,ARNOMBRE,ARUNDINV,ARUMALT1,ARUMALT2,ARFCA1IN,ARFCA2IN,ARCDALTR,");
                sql.AppendLine("       ARMONEDA,0 ARCOSTOA,ARCSTMPR,ARCSTMOB,ARCSTCIF,0 ARCOSTOB,ARPRECIO,ARCDIMPF,ARORIGEN,ARPOSARA,ARPESOUN,ARPESOUM,ARCDCLA1,");
                sql.AppendLine("       ARCDCLA2,ARCDCLA3,ARCDCLA4,ARCDCLA6,ARCDCLA7,ARCDCLA8,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8,ARCODPRO,ARFECCOM,ARPRECOM,");
                sql.AppendLine("       ARMONCOM,ARPROCOM,ARPROGDT,ARESTADO,ARCAUSAE,ARNMUSER,'01/01/1900' ARFECING,'01/01/1900' ARFECMOD,ARFCINA1,ARCOMPOS,ARCONVEN,ARMERCON,ARCODCOM,");
                sql.AppendLine("       ARCDCLA5,ARCAOPDS,ARUNDODS,ARTIPTAR,ARANO,ARCOLECCION,ARPRIORIDAD,TR_PROCEDENCIA,TR_UEN,TR_TP,TR_SCT,TR_FONDO,TR_TEJIDO,");
                sql.AppendLine("(SELECT SUM(BBCANTID) FROM BALANBOD WITH(NOLOCK) WHERE BBCODEMP= ARCODEMP AND BBTIPPRO = ARTIPPRO AND BBCLAVE1 = ARCLAVE1) CANTIDAD");
                sql.AppendLine("  FROM ARTICULO WITH(NOLOCK) ) XX ");
                sql.AppendLine("WHERE CANTIDAD > 0 ");

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
        public static IDataReader GetClavesAlternas(SessionManager oSessionManager, string ASCODEMP, string ASTIPPRO,int ASNIVELC)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ASNOMBRE,ASCLAVEO  ");
                sSql.AppendLine("  FROM ARTICSEC WITH(NOLOCK)");
                sSql.AppendLine(" WHERE ASCODEMP = @p0");
                sSql.AppendLine("   AND ASTIPPRO = @p1");
                sSql.AppendLine("   AND ASNIVELC = @p2");

                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, ASCODEMP, ASTIPPRO, ASNIVELC);
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
        public static DataTable GetClave2(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT CASE WHEN ASNOMBRE IS NULL THEN ARCLAVE2 ELSE ASNOMBRE END ASNOMBRE,ARCLAVE2              ");
                sSql.AppendLine("  FROM ARTICULO WITH(NOLOCK) ");
                sSql.AppendLine("  LEFT OUTER JOIN ARTICSEC WITH(NOLOCK)ON(ARCODEMP = ASCODEMP AND ARTIPPRO = ASTIPPRO AND ARCLAVE2 = ASCLAVEO AND ASNIVELC = 2) ");
                sSql.AppendLine(" WHERE ARCODEMP = @p0");
                sSql.AppendLine("   AND ARTIPPRO = @p1");
                sSql.AppendLine("   AND ARCLAVE1 = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public static DataTable GetClave3(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT DISTINCT CASE WHEN ASNOMBRE IS NULL THEN ARCLAVE3 ELSE ASNOMBRE END ASNOMBRE,ARCLAVE3              ");
                sSql.AppendLine("  FROM ARTICULO WITH(NOLOCK) ");
                sSql.AppendLine("  LEFT OUTER JOIN ARTICSEC WITH(NOLOCK)ON(ARCODEMP = ASCODEMP AND ARTIPPRO = ASTIPPRO AND ARCLAVE3 = ASCLAVEO AND ASNIVELC = 3) ");
                sSql.AppendLine(" WHERE ARCODEMP = @p0");
                sSql.AppendLine("   AND ARTIPPRO = @p1");
                sSql.AppendLine("   AND ARCLAVE1 = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public static DataTable GetImagenes(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT *              ");
                sSql.AppendLine("  FROM IMAGENES WITH(NOLOCK) ");                
                sSql.AppendLine(" WHERE IM_CODEMP = @p0");
                sSql.AppendLine("   AND IM_TIPPRO = @p1");
                sSql.AppendLine("   AND IM_CLAVE1 = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1);
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
        public static int InsertArticulo(SessionManager oSessionManager,string ARCODEMP,string ARTIPPRO,string ARCLAVE1,string ARCLAVE2,string ARCLAVE3,string ARCLAVE4,string ARNOMBRE,string ARUNDINV,string ARUMALT1,string ARUMALT2,double? ARFCA1IN,double? ARFCA2IN,string ARCDALTR,
                            string ARMONEDA,double? ARCOSTOA,double? ARCSTMPR,double? ARCSTMOB,double? ARCSTCIF,double? ARCOSTOB,double ARPRECIO,string ARCDIMPF,string ARORIGEN,string ARPOSARA,double? ARPESOUN,string ARPESOUM,string ARCDCLA1,string ARCDCLA2,string ARCDCLA3,string ARCDCLA4,
                            string ARCDCLA6, string ARCDCLA7, string ARCDCLA8, string ARCDCLA9, string ARCDCLA10, string ARDTTEC1, string ARDTTEC2, string ARDTTEC3, string ARDTTEC4, string ARDTTEC5, double? ARDTTEC6, string ARDTTEC7, string ARDTTEC8, string ARDTTEC9, string ARDTTEC10, int? ARCODPRO, DateTime? ARFECCOM, double? ARPRECOM, string ARMONCOM, int ARPROCOM, int ARPROGDT,
                            double? ARFCINA1,string ARCOMPOS,string ARCONVEN,string ARMERCON,string ARCODCOM,string ARCDCLA5,int? ARCAOPDS,string ARUNDODS,string ARTIPTAR,string ARANO,string ARCOLECCION,string ARPRIORIDAD,string TR_PROCEDENCIA,
                            string TR_UEN,string TR_TP,string TR_SCT,string TR_FONDO,string TR_TEJIDO,string ARESTADO,string ARCAUSAE,string ARNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO ARTICULO (ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARNOMBRE,ARUNDINV,ARUMALT1,ARUMALT2,ARFCA1IN,ARFCA2IN,ARCDALTR,");
                sSql.AppendLine("ARMONEDA,ARCOSTOA,ARCSTMPR,ARCSTMOB,ARCSTCIF,ARCOSTOB,ARPRECIO,ARCDIMPF,ARORIGEN,ARPOSARA,ARPESOUN,ARPESOUM,ARCDCLA1,ARCDCLA2,ARCDCLA3,ARCDCLA4,");
                sSql.AppendLine("ARCDCLA6,ARCDCLA7,ARCDCLA8,ARCDCLA9,ARCDCLA10,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8,ARDTTEC9,ARDTTEC10,ARCODPRO,ARFECCOM,ARPRECOM,ARMONCOM,ARPROCOM,ARPROGDT,");
                sSql.AppendLine("ARFCINA1,ARCOMPOS,ARCONVEN,ARMERCON,ARCODCOM,ARCDCLA5,ARCAOPDS,ARUNDODS,ARTIPTAR,ARANO,ARCOLECCION,ARPRIORIDAD,TR_PROCEDENCIA,");
                sSql.AppendLine("TR_UEN,TR_TP,TR_SCT,TR_FONDO,TR_TEJIDO,ARESTADO,ARCAUSAE,ARNMUSER,ARFECING,ARFECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,");
                sSql.AppendLine(" @p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,@p50,@p51,@p52,@p53,@p54,@p55,@p56,@p57,@p58,@p59,@p60,@p61,@p62, @p63,@p64,@p65,@p66,@p67,@p68,@p69,@p70,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARNOMBRE,ARUNDINV,ARUMALT1,ARUMALT2,ARFCA1IN,ARFCA2IN,ARCDALTR,
                            ARMONEDA,ARCOSTOA,ARCSTMPR,ARCSTMOB,ARCSTCIF,ARCOSTOB,ARPRECIO,ARCDIMPF,ARORIGEN,ARPOSARA,ARPESOUN,ARPESOUM,ARCDCLA1,ARCDCLA2,ARCDCLA3,ARCDCLA4,
                            ARCDCLA6, ARCDCLA7, ARCDCLA8, ARCDCLA9, ARCDCLA10, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARDTTEC7, ARDTTEC8, ARDTTEC9, ARDTTEC10, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                            ARFCINA1,ARCOMPOS,ARCONVEN,ARMERCON,ARCODCOM,ARCDCLA5,ARCAOPDS,ARUNDODS,ARTIPTAR,ARANO,ARCOLECCION,ARPRIORIDAD,TR_PROCEDENCIA,
                            TR_UEN,TR_TP,TR_SCT,TR_FONDO,TR_TEJIDO,ARESTADO,ARCAUSAE,ARNMUSER);

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
        public static int UpdateArticulo(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string ARNOMBRE, string ARUNDINV, string ARUMALT1, string ARUMALT2, double? ARFCA1IN, double? ARFCA2IN, string ARCDALTR,
                            string ARMONEDA, double? ARCOSTOA, double? ARCSTMPR, double? ARCSTMOB, double? ARCSTCIF, double? ARCOSTOB, double ARPRECIO, string ARCDIMPF, string ARORIGEN, string ARPOSARA, double? ARPESOUN, string ARPESOUM, string ARCDCLA1, string ARCDCLA2, string ARCDCLA3, string ARCDCLA4,
                            string ARCDCLA6, string ARCDCLA7, string ARCDCLA8, string ARCDCLA9, string ARCDCLA10, string ARDTTEC1, string ARDTTEC2, string ARDTTEC3, string ARDTTEC4, string ARDTTEC5, double? ARDTTEC6, string ARDTTEC7, string ARDTTEC8, string ARDTTEC9, string ARDTTEC10, int? ARCODPRO, DateTime? ARFECCOM, double? ARPRECOM, string ARMONCOM, int ARPROCOM, int ARPROGDT,
                            double? ARFCINA1, string ARCOMPOS, string ARCONVEN, string ARMERCON, string ARCODCOM, string ARCDCLA5, int? ARCAOPDS, string ARUNDODS, string ARTIPTAR, string ARANO, string ARCOLECCION, string ARPRIORIDAD, string TR_PROCEDENCIA,
                            string TR_UEN, string TR_TP, string TR_SCT, string TR_FONDO, string TR_TEJIDO, string ARESTADO, string ARCAUSAE, string ARNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE ARTICULO SET ARNOMBRE=@p6,ARUNDINV=@p7,ARUMALT1=@p8,ARUMALT2=@p9,ARFCA1IN=@p10,ARFCA2IN=@p11,ARCDALTR=@p12,");
                sSql.AppendLine("ARMONEDA=@p13,ARCOSTOA=@p14,ARCSTMPR=@p15,ARCSTMOB=@p16,ARCSTCIF=@p17,ARCOSTOB=@p18,ARPRECIO=@p19,ARCDIMPF=@p20,ARORIGEN=@p21,ARPOSARA=@p22,ARPESOUN=@p23,ARPESOUM=@p24,ARCDCLA1=@p25,ARCDCLA2=@p26,ARCDCLA3=@p27,ARCDCLA4=@p28,");
                sSql.AppendLine("ARCDCLA6=@p29,ARDTTEC1=@p30,ARDTTEC2=@p31,ARDTTEC3=@p32,ARDTTEC4=@p33,ARDTTEC5=@p34,ARDTTEC6=@p35,ARCODPRO=@p36,ARFECCOM=@p37,ARPRECOM=@p38,ARMONCOM=@p39,ARPROCOM=@p40,ARPROGDT=@p41,");
                sSql.AppendLine("ARFCINA1=@p42,ARCOMPOS=@p43,ARCONVEN=@p44,ARMERCON=@p45,ARCODCOM=@p46,ARCDCLA5=@p47,ARCAOPDS=@p48,ARUNDODS=@p49,ARTIPTAR=@p50,ARANO=@p51,ARCOLECCION=@p52,ARPRIORIDAD=@p53,TR_PROCEDENCIA=@p54,");
                sSql.AppendLine("TR_UEN=@p55,TR_TP=@p56,TR_SCT=@p57,TR_FONDO=@p58,TR_TEJIDO=@p59,ARESTADO=@p60,ARCAUSAE=@p61,ARNMUSER=@p62,ARCDCLA7=@p63,ARCDCLA8=@p64,ARDTTEC7=@p65,ARDTTEC8=@p66,ARCDCLA9=@p67,ARCDCLA10=@p68,ARDTTEC9=@p69,ARDTTEC10=@p70,ARFECMOD=GETDATE()");
                sSql.AppendLine("WHERE ARCODEMP=@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, ARNOMBRE, ARUNDINV, ARUMALT1, ARUMALT2, ARFCA1IN, ARFCA2IN, ARCDALTR,
                            ARMONEDA, ARCOSTOA, ARCSTMPR, ARCSTMOB, ARCSTCIF, ARCOSTOB, ARPRECIO, ARCDIMPF, ARORIGEN, ARPOSARA, ARPESOUN, ARPESOUM, ARCDCLA1, ARCDCLA2, ARCDCLA3, ARCDCLA4,
                            ARCDCLA6, ARDTTEC1, ARDTTEC2, ARDTTEC3, ARDTTEC4, ARDTTEC5, ARDTTEC6, ARCODPRO, ARFECCOM, ARPRECOM, ARMONCOM, ARPROCOM, ARPROGDT,
                            ARFCINA1, ARCOMPOS, ARCONVEN, ARMERCON, ARCODCOM, ARCDCLA5, ARCAOPDS, ARUNDODS, ARTIPTAR, ARANO, ARCOLECCION, ARPRIORIDAD, TR_PROCEDENCIA,
                            TR_UEN, TR_TP, TR_SCT, TR_FONDO, TR_TEJIDO, ARESTADO, ARCAUSAE, ARNMUSER, ARCDCLA7, ARCDCLA8, ARDTTEC7, ARDTTEC8, ARCDCLA9, ARCDCLA10, ARDTTEC9, ARDTTEC10);

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
        public static int ExisteArticulo(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM ARTICULO WITH(NOLOCK)");
                sSql.AppendLine("WHERE ARCODEMP=@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4));
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
        public static DataTable GetArticulos(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            string lc_sql = "";
            try
            {                
                sSql.AppendLine("SELECT ARCLAVE1,ARNOMBRE,ARUNDINV,ARDTTEC4,ARDTTEC1,ARDTTEC2,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARTIPPRO,TANOMBRE,ARDTTEC1,ARDTTEC2,ARDTTEC3,ARDTTEC4,ARDTTEC5,ARDTTEC6,ARDTTEC7,ARDTTEC8,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("(SELECT TOP 1 BCODIGO FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP = ARCODEMP AND BTIPPRO = ARTIPPRO AND BCLAVE1 = ARCLAVE1 AND BCLAVE2 = ARCLAVE2 AND BCLAVE3 = ARCLAVE3 AND BCLAVE4 = ARCLAVE4) BARRAS,");
                sSql.AppendLine(" AA.ASNOMBRE NOMTTEC1,BB.ASNOMBRE NOMTTEC2,CC.ASNOMBRE NOMTTEC3,DD.ASNOMBRE NOMTTEC4,EE.ASNOMBRE NOMTTEC5,FF.ASNOMBRE NOMTTEC7");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK)");                
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");

                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC AA WITH(NOLOCK) ON(AA.ASCODEMP = ARCODEMP AND AA.ASTIPPRO = ARTIPPRO AND AA.ASCLAVEO = ARDTTEC1 AND AA.ASNIVELC = 5)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC BB WITH(NOLOCK) ON(BB.ASCODEMP = ARCODEMP AND BB.ASTIPPRO = ARTIPPRO AND BB.ASCLAVEO = ARDTTEC2 AND BB.ASNIVELC = 6)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC CC WITH(NOLOCK) ON(CC.ASCODEMP = ARCODEMP AND CC.ASTIPPRO = ARTIPPRO AND CC.ASCLAVEO = ARDTTEC3 AND CC.ASNIVELC = 7)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC DD WITH(NOLOCK) ON(DD.ASCODEMP = ARCODEMP AND DD.ASTIPPRO = ARTIPPRO AND DD.ASCLAVEO = ARDTTEC4 AND DD.ASNIVELC = 8)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC EE WITH(NOLOCK) ON(EE.ASCODEMP = ARCODEMP AND EE.ASTIPPRO = ARTIPPRO AND EE.ASCLAVEO = ARDTTEC5 AND EE.ASNIVELC = 9)");
                sSql.AppendLine("LEFT OUTER JOIN ARTICSEC FF WITH(NOLOCK) ON(FF.ASCODEMP = ARCODEMP AND FF.ASTIPPRO = ARTIPPRO AND FF.ASCLAVEO = ARDTTEC7 AND FF.ASNIVELC = 10)");

                sSql.AppendLine("WHERE 1=1 " + filter);
                
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
        //Barras
        #region
        public static DataTable GetTbBarras(SessionManager oSessionManager, string BCODEMP, string BTIPPRO, string BCLAVE1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBARRA.*, ");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = BCODEMP AND ASTIPPRO = BTIPPRO AND ASCLAVEO = BCLAVE2 AND ASNIVELC = 2) ELSE BCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = BCODEMP AND ASTIPPRO = BTIPPRO AND ASCLAVEO = BCLAVE3 AND ASNIVELC = 3) ELSE BCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM TBBARRA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = BCODEMP AND TATIPPRO = BTIPPRO)");
                sSql.AppendLine("WHERE BCODEMP=@p0 AND BTIPPRO=@p1 AND BCLAVE1=@p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BCODEMP, BTIPPRO, BCLAVE1);
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
        public static DataTable GetTbBarras(SessionManager oSessionManager, string BCODIGO,string LT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBARRA.*,ARNOMBRE,ISNULL(BBCANTID,0) BBCANTID,TTVALORN ARCDIMPF,TANOMBRE, ");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA,@p1),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA),0) DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM TBBARRA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = BCODEMP AND ARTIPPRO = BTIPPRO AND ARCLAVE1 = BCLAVE1 AND ARCLAVE2 = BCLAVE2 AND ARCLAVE3 = BCLAVE3 AND ARCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = BCODEMP AND BBTIPPRO = BTIPPRO AND BBCLAVE1 = BCLAVE1 AND BBCLAVE2 = BCLAVE2 AND BBCLAVE3 = BCLAVE3 AND BBCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");
                
                //if ((BCODIGO.Length == 13) && (BCODIGO.Substring(0, 2) == "77"))
                if (BCODIGO.Length == 13)
                    sSql.AppendLine("WHERE SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER(@p0),1,12) ");
                else
                    sSql.AppendLine("WHERE BCODIGO=@p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BCODIGO, LT);
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
        public static DataTable GetTbBarrasNoInv(SessionManager oSessionManager, string BCODIGO, string LT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBARRA.*,ARNOMBRE,TTVALORN ARCDIMPF,TANOMBRE, ");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,'.',@p1),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,'.'),0) DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM TBBARRA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = BCODEMP AND ARTIPPRO = BTIPPRO AND ARCLAVE1 = BCLAVE1 AND ARCLAVE2 = BCLAVE2 AND ARCLAVE3 = BCLAVE3 AND ARCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");
                
                if (BCODIGO.Length == 13)
                    sSql.AppendLine("WHERE SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER(@p0),1,12) ");
                else
                    sSql.AppendLine("WHERE BCODIGO=@p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BCODIGO, LT);
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
        public static DataTable GetTbBarrasInv(SessionManager oSessionManager, string BCODIGO, string LT,string BODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBARRA.*,TANOMBRE,ARNOMBRE,ISNULL(BBCANTID-BBCANTRN,0) BBCANTID,TTVALORN ARCDIMPF, ");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA,@p1),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA),0) DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARTIPPRO");
                sSql.AppendLine("FROM TBBARRA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = BCODEMP AND ARTIPPRO = BTIPPRO AND ARCLAVE1 = BCLAVE1 AND ARCLAVE2 = BCLAVE2 AND ARCLAVE3 = BCLAVE3 AND ARCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = BCODEMP AND BBTIPPRO = BTIPPRO AND BBCLAVE1 = BCLAVE1 AND BBCLAVE2 = BCLAVE2 AND BBCLAVE3 = BCLAVE3 AND BBCLAVE4 = BCLAVE4 AND BBBODEGA =@p2)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");
                
                //if ((BCODIGO.Length == 13) && (BCODIGO.Substring(0,2)=="77"))
                if (BCODIGO.Length == 13)
                    sSql.AppendLine("WHERE SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER(@p0),1,12) ");
                else
                    sSql.AppendLine("WHERE BCODIGO=@p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BCODIGO, LT,BODEGA);
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
        public static DataTable GetTbBarrasInv(SessionManager oSessionManager, string BCODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBBARRA.*,TANOMBRE,ARNOMBRE,ISNULL(BBCANTID-BBCANTRN,0) BBCANTID,TTVALORN ARCDIMPF, ");
                sSql.AppendLine("0 PRECIO,0 DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3,");
                sSql.AppendLine("ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,ARTIPPRO,BBBODEGA");
                sSql.AppendLine("FROM TBBARRA WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = BCODEMP AND ARTIPPRO = BTIPPRO AND ARCLAVE1 = BCLAVE1 AND ARCLAVE2 = BCLAVE2 AND ARCLAVE3 = BCLAVE3 AND ARCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = BCODEMP AND BBTIPPRO = BTIPPRO AND BBCLAVE1 = BCLAVE1 AND BBCLAVE2 = BCLAVE2 AND BBCLAVE3 = BCLAVE3 AND BBCLAVE4 = BCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");

                //if ((BCODIGO.Length == 13) && (BCODIGO.Substring(0,2)=="77"))
                if (BCODIGO.Length == 13)
                    sSql.AppendLine("WHERE SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER(@p0),1,12) ");
                else
                    sSql.AppendLine("WHERE BCODIGO=@p0 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BCODIGO);
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
        public static DataTable GetArticuloInv(SessionManager oSessionManager, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string LT, string BODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ARTIPPRO,ARCLAVE1,ARCLAVE2, ARCLAVE3,ARCLAVE4,TANOMBRE,ARNOMBRE,ISNULL(BBCANTID,0) BBCANTID,TTVALORN ARCDIMPF, ");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA,@p4),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA),0) DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK) ");                
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = ARCODEMP AND BBTIPPRO = ARTIPPRO AND BBCLAVE1 = ARCLAVE1 AND BBCLAVE2 = ARCLAVE2 AND BBCLAVE3 = ARCLAVE3 AND BBCLAVE4 = ARCLAVE4 AND BBBODEGA =@p5)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");             
                sSql.AppendLine("WHERE ARCLAVE1=@p0 AND ARCLAVE2=@p1 AND ARCLAVE3=@p2 AND ARCLAVE4=@p3 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, LT, BODEGA);
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
        public static DataTable GetArticuloInv(SessionManager oSessionManager, string ARTIPPRO,string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string LT, string BODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ARTIPPRO,ARCLAVE1,ARCLAVE2, ARCLAVE3,ARCLAVE4,TANOMBRE,ARNOMBRE,ISNULL(BBCANTID,0) BBCANTID,TTVALORN ARCDIMPF, ");
                sSql.AppendLine("ISNULL(dbo.FGET_PRECIO(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA,@p4),0) PRECIO,ISNULL(dbo.FGET_DESCUENTOART(ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,BBBODEGA),0) DESCUENTO,");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = ARCODEMP AND BBTIPPRO = ARTIPPRO AND BBCLAVE1 = ARCLAVE1 AND BBCLAVE2 = ARCLAVE2 AND BBCLAVE3 = ARCLAVE3 AND BBCLAVE4 = ARCLAVE4 AND BBBODEGA =@p5)");
                sSql.AppendLine("INNER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = ARCODEMP  AND TTCODCLA = ARCDIMPF AND TTCODTAB='IMPF')");
                sSql.AppendLine("WHERE ARTIPPRO=@p0 AND ARCLAVE1=@p1 AND ARCLAVE2=@p2 AND ARCLAVE3=@p3 AND ARCLAVE4=@p4 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, LT, BODEGA);
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
        public static DataTable GetArticuloInv(SessionManager oSessionManager, string inFilter, string inBodega)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ARTIPPRO,ARCLAVE1,ARCLAVE2, ARCLAVE3,ARCLAVE4,TANOMBRE,ARNOMBRE,ISNULL(BBCANTID,0) BBCANTID, ");                
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM ARTICULO WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sSql.AppendLine("INNER JOIN BALANBOD WITH(NOLOCK) ON (BBCODEMP = ARCODEMP AND BBTIPPRO = ARTIPPRO AND BBCLAVE1 = ARCLAVE1 AND BBCLAVE2 = ARCLAVE2 AND BBCLAVE3 = ARCLAVE3 AND BBCLAVE4 = ARCLAVE4 )");                
                sSql.AppendLine("WHERE BBBODEGA =@p0 " + inFilter);

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inBodega);
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
        public static int InserTbBarras(SessionManager oSessionManager, string BCODEMP, string BCODIGO, string BTIPPRO, string BCLAVE1, string BCLAVE2, string BCLAVE3, string BCLAVE4, string BCODCAL, string BCOPAIS, string BEMPRES, string BMNUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TBBARRA (BCODEMP,BCODIGO,BTIPPRO,BCLAVE1,BCLAVE2,BCLAVE3,BCLAVE4,BCODCAL,BCOPAIS,BEMPRES,BMNUSER,BTBARRA,BFECCRE,BFECMOD)");
                sSql.AppendLine("VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,1,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BCODEMP, BCODIGO, BTIPPRO, BCLAVE1, BCLAVE2, BCLAVE3, BCLAVE4, BCODCAL, BCOPAIS, BEMPRES, BMNUSER);
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
        public static int ExisteBarras(SessionManager oSessionManager, string BCODEMP, string BCODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP=@p0 AND SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER(@p1),1,12)");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BCODEMP, BCODIGO));
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
        public static int TieneBarras(SessionManager oSessionManager, string BCODEMP, string BTIPPRO, string BCLAVE1, string BCLAVE2, string BCLAVE3, string BCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TBBARRA WITH(NOLOCK) WHERE BCODEMP=@p0 AND BTIPPRO=@p1 AND BCLAVE1=@p2 AND BCLAVE2=@p3 AND BCLAVE3=@p4 AND BCLAVE4=@p5 AND BTBARRA= 1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BCODEMP, BTIPPRO, BCLAVE1, BCLAVE2, BCLAVE3, BCLAVE4));
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
        public static int InsertTMPImpresion(SessionManager oSessionManager, string BTIPPRO, string BCLAVE1, string BCLAVE2, string BCLAVE3, string BCLAVE4, string BMNUSER,int BCANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TMP_IMPRESIONES (TMP_USUARIO,TMP_TP,TMP_C1,TMP_C2,TMP_C3,TMP_C4,TMP_CANT) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BMNUSER, BTIPPRO, BCLAVE1, BCLAVE2, BCLAVE3, BCLAVE4, BCANTID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static int DeleteTMPImpresion(SessionManager oSessionManager, string BMNUSER)
        {
        StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TMP_IMPRESIONES  WHERE TMP_USUARIO = @p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BMNUSER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static int DeleteTbBarras(SessionManager oSessionManager, string BCODEMP, string BCODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBBARRA WHERE BCODEMP=@p0 AND BCODIGO=@p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BCODEMP, BCODIGO));
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
        //Imagenes/Fotografias/Dibujos
        public static int InsertImagen(SessionManager oSessionManager,string IM_CODEMP, string IM_TIPPRO, string IM_CLAVE1, string IM_CLAVE2, string IM_CLAVE3, string IM_CLAVE4, int IM_TIPIMA, object IM_IMAGEN, string IM_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO IMAGENES (IM_CODEMP,IM_TIPPRO,IM_CLAVE1,IM_CLAVE2,IM_CLAVE3,IM_CLAVE4,IM_TIPIMA,IM_IMAGEN,IM_NMUSER,IM_FECING,IM_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IM_CODEMP, IM_TIPPRO, IM_CLAVE1, IM_CLAVE2, IM_CLAVE3, IM_CLAVE4, IM_TIPIMA, IM_IMAGEN, IM_NMUSER);
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
        public static int DeteleImagen(SessionManager oSessionManager, int IM_CONSECUTIVO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM IMAGENES WHERE IM_CONSECUTIVO=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IM_CONSECUTIVO);
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
        //Tester - Origen
        #region
        public static DataTable GetTester(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT X.*,Y.ARNOMBRE,Y.ARDTTEC1,Y.ARDTTEC2,Y.ARDTTEC3,Y.ARDTTEC4,Y.ARDTTEC5,Y.ARDTTEC7,Y.ARDTTEC8,Z.TANOMBRE,T.BCODIGO ");                
                sSql.AppendLine("  FROM TB_TESTER X WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN ARTICULO Y WITH(NOLOCK) ON (X.TT_CODEMP = Y.ARCODEMP AND X.TT_TIPPRO = Y.ARTIPPRO AND X.TT_CLAVE1 = Y.ARCLAVE1 AND X.TT_CLAVE2 = Y.ARCLAVE2 AND X.TT_CLAVE3 = Y.ARCLAVE3 AND X.TT_CLAVE4 = Y.ARCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO Z WITH(NOLOCK) ON (Y.ARCODEMP = Z.TACODEMP AND Y.ARTIPPRO = Z.TATIPPRO)");
                sSql.AppendLine(" LEFT OUTER JOIN TBBARRA T WITH(NOLOCK) ON (X.ARCODEMP = T.BCODEMP AND X.TT_TIPPRO = T.BTIPPRO AND X.TT_CLAVE1 = T.BCLAVE1 AND X.TT_CLAVE2 = T.BCLAVE2 AND X.TT_CLAVE3 = T.BCLAVE3 AND X.TT_CLAVE4 = T.BCLAVE4)");
                sSql.AppendLine(" WHERE X.ARCODEMP =@p0 AND X.ARTIPPRO=@p1 AND X.ARCLAVE1=@p2 AND X.ARCLAVE2=@p3 AND X.ARCLAVE3=@p4 AND X.ARCLAVE4=@p5");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4);
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
        public static DataTable GetTesterInv(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4,string BBBODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT X.*,Y.ARNOMBRE,Y.ARDTTEC1,Y.ARDTTEC2,Y.ARDTTEC3,Y.ARDTTEC4,Y.ARDTTEC5,Y.ARDTTEC7,Y.ARDTTEC8,Z.TANOMBRE,T.BCODIGO,BBCANTID ");
                sSql.AppendLine("  FROM TB_TESTER X WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN ARTICULO Y WITH(NOLOCK) ON (X.TT_CODEMP = Y.ARCODEMP AND X.TT_TIPPRO = Y.ARTIPPRO AND X.TT_CLAVE1 = Y.ARCLAVE1 AND X.TT_CLAVE2 = Y.ARCLAVE2 AND X.TT_CLAVE3 = Y.ARCLAVE3 AND X.TT_CLAVE4 = Y.ARCLAVE4)");
                sSql.AppendLine(" INNER JOIN TBTIPPRO Z WITH(NOLOCK) ON (Y.ARCODEMP = Z.TACODEMP AND Y.ARTIPPRO = Z.TATIPPRO)");
                sSql.AppendLine(" LEFT OUTER JOIN TBBARRA T WITH(NOLOCK) ON (X.ARCODEMP = T.BCODEMP AND X.TT_TIPPRO = T.BTIPPRO AND X.TT_CLAVE1 = T.BCLAVE1 AND X.TT_CLAVE2 = T.BCLAVE2 AND X.TT_CLAVE3 = T.BCLAVE3 AND X.TT_CLAVE4 = T.BCLAVE4)");
                sSql.AppendLine(" LEFT OUTER JOIN BALANBOD WITH(NOLOCK) ON (Y.ARCODEMP = BBCODEMP AND Y.ARTIPPRO = BBTIPPRO AND Y.ARCLAVE1 = BBCLAVE1 AND Y.ARCLAVE2 = BBCLAVE2 AND Y.ARCLAVE3 = BBCLAVE3 AND Y.ARCLAVE4 = BBCLAVE4 AND BBBODEGA =@p6)");
                sSql.AppendLine(" WHERE X.ARCODEMP =@p0 AND X.ARTIPPRO=@p1 AND X.ARCLAVE1=@p2 AND X.ARCLAVE2=@p3 AND X.ARCLAVE3=@p4 AND X.ARCLAVE4=@p5");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, BBBODEGA);
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
        public static DataTable GetOrigen(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TB_ORIGEN WITH(NOLOCK) WHERE ARCODEMP =@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4);
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
        public static string GetReffromOrigen(SessionManager oSessionManager, string ARCODEMP, string OR_REFERENCIA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TOP 1 TB_ORIGEN.ARCLAVE1 FROM TB_ORIGEN WITH(NOLOCK) WHERE ARCODEMP = @p0 AND OR_REFERENCIA=@p1");
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, OR_REFERENCIA));
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
        public static int ExisteTester(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string TT_CODEMP,
                                       string TT_TIPPRO, string TT_CLAVE1, string TT_CLAVE2, string TT_CLAVE3, string TT_CLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_TESTER WITH(NOLOCK) WHERE ARCODEMP=@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5 AND TT_CODEMP =@p6 AND TT_TIPPRO=@p7 AND TT_CLAVE1=@p8 AND TT_CLAVE2=@p9 AND TT_CLAVE3 =@p10 AND TT_CLAVE4=@p11 ");
                
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, TT_CODEMP, TT_TIPPRO, TT_CLAVE1, TT_CLAVE2, TT_CLAVE3, TT_CLAVE4));
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
        public static int InsertTester(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string TT_CODEMP,
                                       string TT_TIPPRO, string TT_CLAVE1, string TT_CLAVE2, string TT_CLAVE3, string TT_CLAVE4, string TT_USUARIO, string TT_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_TESTER (ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,TT_CODEMP,TT_TIPPRO,TT_CLAVE1,TT_CLAVE2,TT_CLAVE3,TT_CLAVE4,TT_USUARIO,TT_ESTADO,TT_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, TT_CODEMP, TT_TIPPRO, TT_CLAVE1, TT_CLAVE2, TT_CLAVE3, TT_CLAVE4, TT_USUARIO, TT_ESTADO);
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
        public static int DeleteTester(SessionManager oSessionManager, int TT_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_TESTER WHERE TT_CODIGO=@p0 ");

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
        public static int ExisteOrigen(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string OR_REFERENCIA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_ORIGEN WITH(NOLOCK) WHERE ARCODEMP=@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5 AND OR_REFERENCIA=@p6");
                
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, OR_REFERENCIA));
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
        public static int InsertOrigen(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string OR_REFERENCIA,string OR_USUARIO, string OR_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_ORIGEN (ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4,OR_REFERENCIA,OR_USUARIO,OR_ESTADO,OR_FECING)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, OR_REFERENCIA, OR_USUARIO, OR_ESTADO);
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
        public static int DeleteOrigen(SessionManager oSessionManager, int OR_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_ORIGEN WHERE OR_CODIGO=@p0 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, OR_CODIGO);
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
        //Cod Aranceles
        #region
        public static DataTable GetAranceles(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TOP 200 * ");
                sSql.AppendLine("FROM TB_UNARANCELARIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE 1=1 " + filter);

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
        //Registros Sanitarios
        #region
        public static DataTable GetRegistrosSanitarios(SessionManager oSessionManager, string ARCODEMP,string ARTIPPRO, string ARCLAVE1,string ARCLAVE2,string ARCLAVE3,string ARCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * ");
                sSql.AppendLine("FROM TB_RRSANITARIO WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE ARCODEMP=@p0 AND ARTIPPRO=@p1 AND ARCLAVE1=@p2 AND ARCLAVE2=@p3 AND ARCLAVE3=@p4 AND ARCLAVE4=@p5");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,ARCODEMP,ARTIPPRO,ARCLAVE1,ARCLAVE2,ARCLAVE3,ARCLAVE4);
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
        public static int InsertRegistroSanitario(SessionManager oSessionManager, string ARCODEMP, string ARTIPPRO, string ARCLAVE1, string ARCLAVE2, string ARCLAVE3, string ARCLAVE4, string RS_REGISTRO, DateTime RS_FEINICIO, DateTime RS_FECFINAL, string RS_USUARIO, string RS_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_RRSANITARIO (ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, RS_REGISTRO, RS_FEINICIO, RS_FECFINAL, RS_USUARIO, RS_ESTADO, RS_FECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ARCODEMP, ARTIPPRO, ARCLAVE1, ARCLAVE2, ARCLAVE3, ARCLAVE4, RS_REGISTRO, RS_FEINICIO, RS_FECFINAL, RS_USUARIO, RS_ESTADO);
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
        public static int UpdateRegistroSanitario(SessionManager oSessionManager,  string RS_REGISTRO, DateTime RS_FEINICIO, DateTime RS_FECFINAL, string RS_USUARIO, string RS_ESTADO,int RS_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_RRSANITARIO SET RS_REGISTRO=@p0, RS_FEINICIO=@p1, RS_FECFINAL=@p2, RS_USUARIO=@p3, RS_ESTADO=@p4, RS_FECMOD=GETDATE()");
                sSql.AppendLine(" WHERE RS_CODIGO=@p5");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RS_REGISTRO, RS_FEINICIO, RS_FECFINAL, RS_USUARIO, RS_ESTADO, RS_CODIGO);
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
        public static int ExisteRegistroSanitario(SessionManager oSessionManager, int RS_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM TB_RRSANITARIO WITH(NOLOCK) WHERE RS_CODIGO=@p0");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, RS_CODIGO));
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
        public static int DeleteRegistroSanitario(SessionManager oSessionManager, int RS_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TB_RRSANITARIO WHERE RS_CODIGO=@p0");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, RS_CODIGO);
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
