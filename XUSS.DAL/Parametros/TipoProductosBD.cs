using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class TipoProductosBD
    {
        public static DataTable GetTipoProducto(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBTIPPRO WHERE 1=1");
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
        public static IDataReader GetTipoProductoxBodegaR(SessionManager oSessionManager, string CODEMP, string BODEGA, string TP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ABMNLOTE, ABMNELEM, TACLAPRO ");
                sSql.AppendLine("  FROM  TBARTBOD WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ABCODEMP AND TATIPPRO = ABTIPPRO)");
                sSql.AppendLine(" WHERE ABCODEMP = @p0 AND ABBODEGA = @p1 AND ABTIPPRO =@p2 ");                
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text,CODEMP,BODEGA,TP);
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
        public static IDataReader GetTipoProductoxBodegaTFR(SessionManager oSessionManager, string CODEMP, string TFTIPFAC, string TP)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ABMNLOTE, ABMNELEM, TACLAPRO ");
                sSql.AppendLine("  FROM  TBARTBOD WITH(NOLOCK) ");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ABCODEMP AND TATIPPRO = ABTIPPRO)");
                sSql.AppendLine(" INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = ABCODEMP AND TFBODEGA = ABBODEGA)");
                sSql.AppendLine(" WHERE ABCODEMP = @p0 AND TFTIPFAC = @p1 AND ABTIPPRO =@p2 ");
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, TFTIPFAC, TP);
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
        public static int InsertTipoProducto(SessionManager oSessionManager,string TACODEMP,string TATIPPRO,string TANOMBRE,int TACLAVES,string TACLAPRO,string TADSCLA1,string TADSCLA2,string TACTLSE2,string TADSCLA3,string TACTLSE3,string TADSCLA4,string TACTLSE4,
                                                                            string TACDCLA1, string TACDCLA2, string TACDCLA3, string TACDCLA4, string TACDCLA5, string TACDCLA6, string TACDCLA7, string TACDCLA8, string TACDCLA9, string TACDCLA10, string TADTTEC1, string TADTTEC2, string TADTTEC3, string TADTTEC4, string TADTTEC5, string TADTTEC6, string TADTTEC7, string TADTTEC8, string TADTTEC9, string TADTTEC10,
                                                                            string TADTLOT1,string TADTLOT2,string TADTLOT3,string TADTLOT4,string TADTLOT5,string TADTLOT6,string TAUMPESO,string TAUMANCH,string TAUMLARG,string TADTELE1,string TADTELE2,string TADTELE3,
                                                                            string TADTELE4,string TADTELE5,string TADTELE6,string TAESTADO,string TACAUSAE,string TANMUSER,int? TACLAFLO,string TAINDALT,int? TACLATEC,int? TANROBAR,string TAFRMBAR,string TACONSEC,
                                                                            string TACONCAT, string TACALIFI, string TASUFCON, int? TACNTCLA, string TAREFINI, string TAVENTA, string TAAUTINC, double TATOLERA, string TAREPORTE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBTIPPRO (TACODEMP, TATIPPRO, TANOMBRE, TACLAVES, TACLAPRO, TADSCLA1, TADSCLA2, TACTLSE2, TADSCLA3, TACTLSE3, TADSCLA4, TACTLSE4,");
                sSql.AppendLine("                      TACDCLA1, TACDCLA2, TACDCLA3, TACDCLA4, TACDCLA5, TACDCLA6, TACDCLA7, TACDCLA8, TACDCLA9, TACDCLA10, TADTTEC1, TADTTEC2, TADTTEC3, TADTTEC4, TADTTEC5, TADTTEC6, TADTTEC7, TADTTEC8, TADTTEC9, TADTTEC10,");
                sSql.AppendLine("                      TADTLOT1, TADTLOT2, TADTLOT3, TADTLOT4, TADTLOT5, TADTLOT6, TAUMPESO, TAUMANCH, TAUMLARG, TADTELE1, TADTELE2, TADTELE3,");
                sSql.AppendLine("                      TADTELE4, TADTELE5, TADTELE6, TAESTADO, TACAUSAE, TANMUSER, TACLAFLO, TAINDALT, TACLATEC, TANROBAR, TAFRMBAR, TACONSEC,");
                sSql.AppendLine("                      TACONCAT, TACALIFI, TASUFCON, TACNTCLA, TAREFINI, TAVENTA,  TAAUTINC, TATOLERA, TAREPORTE,TAFECING, TAFECMOD) VALUES ");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,");
                sSql.AppendLine(" @p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,@p42,@p43,@p44,@p45,@p46,@p47,@p48,@p49,@p50,@p51,@p52,@p53,@p54,@p55,@p56,@p57,@p58,@p59,@p60,@p61,@p62,@p63,@p64,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TACODEMP, TATIPPRO, TANOMBRE, TACLAVES, TACLAPRO, TADSCLA1, TADSCLA2, TACTLSE2, TADSCLA3, TACTLSE3, TADSCLA4, TACTLSE4,
                                      TACDCLA1, TACDCLA2, TACDCLA3, TACDCLA4, TACDCLA5, TACDCLA6, TACDCLA7, TACDCLA8, TACDCLA9, TACDCLA10, TADTTEC1, TADTTEC2, TADTTEC3, TADTTEC4, TADTTEC5, TADTTEC6, TADTTEC7, TADTTEC8, TADTTEC9, TADTTEC10,
                                      TADTLOT1, TADTLOT2, TADTLOT3, TADTLOT4, TADTLOT5, TADTLOT6, TAUMPESO, TAUMANCH, TAUMLARG, TADTELE1, TADTELE2, TADTELE3,
                                      TADTELE4, TADTELE5, TADTELE6, TAESTADO, TACAUSAE, TANMUSER, TACLAFLO, TAINDALT, TACLATEC, TANROBAR, TAFRMBAR, TACONSEC,
                                      TACONCAT, TACALIFI, TASUFCON, TACNTCLA, TAREFINI, TAVENTA, TAAUTINC, TATOLERA, TAREPORTE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }

        public static int UpdateTipoProducto(SessionManager oSessionManager, string TACODEMP, string TATIPPRO, string TANOMBRE, int TACLAVES, string TACLAPRO, string TADSCLA1, string TADSCLA2, string TACTLSE2, string TADSCLA3, string TACTLSE3, string TADSCLA4, string TACTLSE4,
                                                                            string TACDCLA1, string TACDCLA2, string TACDCLA3, string TACDCLA4, string TACDCLA5, string TACDCLA6, string TADTTEC1, string TADTTEC2, string TADTTEC3, string TADTTEC4, string TADTTEC5, string TADTTEC6,
                                                                            string TADTLOT1, string TADTLOT2, string TADTLOT3, string TADTLOT4, string TADTLOT5, string TADTLOT6, string TAUMPESO, string TAUMANCH, string TAUMLARG, string TADTELE1, string TADTELE2, string TADTELE3,
                                                                            string TADTELE4, string TADTELE5, string TADTELE6, string TAESTADO, string TACAUSAE, string TANMUSER, int? TACLAFLO, string TAINDALT, int? TACLATEC, int? TANROBAR, string TAFRMBAR, string TACONSEC,
                                                                            string TACONCAT, string TACALIFI, string TASUFCON, int? TACNTCLA, string TAREFINI, string TAVENTA, string TAAUTINC, double TATOLERA, string TAREPORTE,
                                                                            string TACDCLA7, string TACDCLA8, string TACDCLA9, string TACDCLA10, string TADTTEC7, string TADTTEC8, string TADTTEC9, string TADTTEC10)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBTIPPRO SET   TANOMBRE=@p2, TACLAVES=@p3, TACLAPRO=@p4, TADSCLA1=@p5, TADSCLA2=@p6, TACTLSE2=@p7, TADSCLA3=@p8, TACTLSE3=@p9, TADSCLA4=@p10, TACTLSE4=@p11,");
                sSql.AppendLine("                      TACDCLA1=@p12, TACDCLA2=@p13, TACDCLA3=@p14, TACDCLA4=@p15, TACDCLA5=@p16, TACDCLA6=@p17, TADTTEC1=@p18, TADTTEC2=@p19, TADTTEC3=@p20, TADTTEC4=@p21, TADTTEC5=@p22, TADTTEC6=@p23,");
                sSql.AppendLine("                      TADTLOT1=@p24, TADTLOT2=@p25, TADTLOT3=@p26, TADTLOT4=@p27, TADTLOT5=@p28, TADTLOT6=@p29, TAUMPESO=@p30, TAUMANCH=@p31, TAUMLARG=@p32, TADTELE1=@p33, TADTELE2=@p34, TADTELE3=@p35,");
                sSql.AppendLine("                      TADTELE4=@p36, TADTELE5=@p37, TADTELE6=@p38, TAESTADO=@p39, TACAUSAE=@p40, TANMUSER=@p41, TACLAFLO=@p42, TAINDALT=@p43, TACLATEC=@p44, TANROBAR=@p45, TAFRMBAR=@p46, TACONSEC=@p47,");
                sSql.AppendLine("                      TACONCAT=@p48, TACALIFI=@p49, TASUFCON=@p50, TACNTCLA=@p51, TAREFINI=@p52, TAVENTA=@p53,  TAAUTINC=@p54, TATOLERA=@p55, TAREPORTE=@p56,TAFECMOD=GETDATE(),");
                sSql.AppendLine("                      TACDCLA7=@p57, TACDCLA8=@p58, TACDCLA9=@p59, TACDCLA10=@p60, TADTTEC7=@p61, TADTTEC8=@p62, TADTTEC9=@p63, TADTTEC10=@p64");
                sSql.AppendLine("WHERE TACODEMP = @p0 AND TATIPPRO = @p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TACODEMP, TATIPPRO, TANOMBRE, TACLAVES, TACLAPRO, TADSCLA1, TADSCLA2, TACTLSE2, TADSCLA3, TACTLSE3, TADSCLA4, TACTLSE4,
                                      TACDCLA1, TACDCLA2, TACDCLA3, TACDCLA4, TACDCLA5, TACDCLA6, TADTTEC1, TADTTEC2, TADTTEC3, TADTTEC4, TADTTEC5, TADTTEC6,
                                      TADTLOT1, TADTLOT2, TADTLOT3, TADTLOT4, TADTLOT5, TADTLOT6, TAUMPESO, TAUMANCH, TAUMLARG, TADTELE1, TADTELE2, TADTELE3,
                                      TADTELE4, TADTELE5, TADTELE6, TAESTADO, TACAUSAE, TANMUSER, TACLAFLO, TAINDALT, TACLATEC, TANROBAR, TAFRMBAR, TACONSEC,
                                      TACONCAT, TACALIFI, TASUFCON, TACNTCLA, TAREFINI, TAVENTA, TAAUTINC, TATOLERA, TAREPORTE,TACDCLA7, TACDCLA8, TACDCLA9, TACDCLA10, TADTTEC7, TADTTEC8, TADTTEC9, TADTTEC10);
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

        public static DataTable GetNivelesTP(SessionManager oSessionManager, string TATIPPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT 2 NIVEL,TADSCLA2,TATIPPRO ");
                sSql.AppendLine("FROM TBTIPPRO WITH(NOLOCK)");
                sSql.AppendLine("WHERE TACTLSE2 ='S'");
                sSql.AppendLine("  AND TATIPPRO=@p0");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT 3 NIVEL,TADSCLA3,TATIPPRO");
                sSql.AppendLine("FROM TBTIPPRO WITH(NOLOCK)");
                sSql.AppendLine("WHERE TACTLSE3 ='S'");
                sSql.AppendLine("  AND TATIPPRO=@p0");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT 4 NIVEL,TADSCLA4,TATIPPRO");
                sSql.AppendLine("FROM TBTIPPRO WITH(NOLOCK)");
                sSql.AppendLine("WHERE TACTLSE4 ='S'");
                sSql.AppendLine("  AND TATIPPRO=@p0");
                sSql.AppendLine("UNION ALL");
                sSql.AppendLine("SELECT 5 NIVEL, CASE WHEN(TACDCLA1= '' OR TACDCLA1 IS NULL) THEN 'DT1' ELSE TACDCLA1 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 UNION ALL");
                sSql.AppendLine("SELECT 6 NIVEL, CASE WHEN(TACDCLA2= '' OR TACDCLA2 IS NULL) THEN 'DT2' ELSE TACDCLA2 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 UNION ALL ");
                sSql.AppendLine("SELECT 7 NIVEL, CASE WHEN(TACDCLA3= '' OR TACDCLA3 IS NULL) THEN 'DT3' ELSE TACDCLA3 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 UNION ALL ");
                sSql.AppendLine("SELECT 8 NIVEL, CASE WHEN(TACDCLA4= '' OR TACDCLA4 IS NULL) THEN 'DT4' ELSE TACDCLA4 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 UNION ALL ");
                sSql.AppendLine("SELECT 9 NIVEL, CASE WHEN(TACDCLA5= '' OR TACDCLA5 IS NULL) THEN 'DT5' ELSE TACDCLA5 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 UNION ALL ");
                sSql.AppendLine("SELECT 10 NIVEL, CASE WHEN(TACDCLA7= '' OR TACDCLA7 IS NULL) THEN 'DT7' ELSE TACDCLA7 END NOM_NIVEL, TATIPPRO  FROM TBTIPPRO WITH(NOLOCK) WHERE TATIPPRO = @p0 ");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,TATIPPRO);
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
