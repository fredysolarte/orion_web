using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class TPedidoBD
    {
        public static DataTable GetTipPed(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("  SELECT * FROM  TBTIPPED WITH(NOLOCK) WHERE 1=1 ");
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
        public static DataTable GetBodegasxPedido(SessionManager oSessionManager, string BDCODEMP,string PTTIPPED)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT CASE WHEN TB_BODEGA_PEDIDO.BPCODIGO IS NULL THEN 'N' ELSE 'S' END CHK, BDNOMBRE,TBBODEGA.BDBODEGA,BPORDEN");
                sSql.AppendLine("  FROM TBBODEGA WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TB_BODEGA_PEDIDO WITH(NOLOCK) ON (TB_BODEGA_PEDIDO.BPCODEMP = TBBODEGA.BDCODEMP AND TB_BODEGA_PEDIDO.BDBODEGA = TBBODEGA.BDBODEGA AND PTTIPPED=@p1) ");
                sSql.AppendLine(" WHERE BDCODEMP = @p0 ");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text,BDCODEMP,PTTIPPED);
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
        public static int InsertBodegaxTPedido(SessionManager oSessionManager, string BPCODEMP, string BDBODEGA, string PTTIPPED, int BPORDEN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_BODEGA_PEDIDO (BPCODEMP, BDBODEGA, PTTIPPED, BPORDEN) VALUES (@p0,@p1,@p2,@p3)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BPCODEMP, BDBODEGA, PTTIPPED, BPORDEN);
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
        public static int DeleteBodegaxTPedido(SessionManager oSessionManager, string BPCODEMP, string PTTIPPED)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE TB_BODEGA_PEDIDO WHERE BPCODEMP=@p0 AND PTTIPPED=@p1");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BPCODEMP, PTTIPPED);
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
        public static int InsertTipoPedido(SessionManager oSessionManager, string PTCODEMP, string PTTIPPED, string PTNOMBRE, string PTDESCRI, string PTBODEGA, string PTTERPAG, string PTMODDES, string PTTERDES, string PTMONEDA, string PTIDIOMA, string PTLISPRE,
                                          string PTCDIMPF, string PTCDTRAN, string PTTIPFAC, int? PTLSTSEP, string PTCDCLA1, string PTCDCLA2, string PTCDCLA3, string PTCDCLA4, string PTCDCLA5, string PTCDCLA6, string PTDTTEC1, string PTDTTEC2, string PTDTTEC3,
                                          string PTDTTEC4, string PTDTTEC5, string PTDTTEC6, string PTCDCLD1, string PTCDCLD2, string PTCDCLD3, string PTCDCLD4, string PTCDCLD5, string PTCDCLD6, string PTDTTCD1, string PTDTTCD2, string PTDTTCD3, string PTDTTCD4,
                                          string PTDTTCD5, string PTDTTCD6, string PTESTADO, string PTCAUSAE, string PTNMUSER, string PTCOTIZA)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TBTIPPED (PTCODEMP,PTTIPPED,PTNOMBRE,PTDESCRI,PTBODEGA,PTTERPAG,PTMODDES,PTTERDES,PTMONEDA,PTIDIOMA,PTLISPRE,PTCDIMPF,PTCDTRAN,PTTIPFAC,PTLSTSEP,PTCDCLA1,");
                sSql.AppendLine("                      PTCDCLA2,PTCDCLA3,PTCDCLA4,PTCDCLA5,PTCDCLA6,PTDTTEC1,PTDTTEC2,PTDTTEC3,PTDTTEC4,PTDTTEC5,PTDTTEC6,PTCDCLD1,PTCDCLD2,PTCDCLD3,PTCDCLD4,PTCDCLD5,");
                sSql.AppendLine("                      PTCDCLD6,PTDTTCD1,PTDTTCD2,PTDTTCD3,PTDTTCD4,PTDTTCD5,PTDTTCD6,PTESTADO,PTCAUSAE,PTNMUSER,PTFECING,PTFECMOD,PTCOTIZA) VALUES (");
                sSql.AppendLine("@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,@p35,@p36,@p37,@p38,@p39,@p40,@p41,GETDATE(),GETDATE(),@p42)");

                return DBAccess.ExecuteNonQuery(oSessionManager,sSql.ToString(),CommandType.Text,PTCODEMP,PTTIPPED,PTNOMBRE,PTDESCRI,PTBODEGA,PTTERPAG,PTMODDES,PTTERDES,PTMONEDA,PTIDIOMA,PTLISPRE,PTCDIMPF,PTCDTRAN,PTTIPFAC,PTLSTSEP,PTCDCLA1,
                                                PTCDCLA2,PTCDCLA3,PTCDCLA4,PTCDCLA5,PTCDCLA6,PTDTTEC1,PTDTTEC2,PTDTTEC3,PTDTTEC4,PTDTTEC5,PTDTTEC6,PTCDCLD1,PTCDCLD2,PTCDCLD3,PTCDCLD4,PTCDCLD5,
                                                PTCDCLD6,PTDTTCD1,PTDTTCD2,PTDTTCD3,PTDTTCD4,PTDTTCD5,PTDTTCD6,PTESTADO,PTCAUSAE,PTNMUSER,PTCOTIZA);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static int UpdateTipoPedido(SessionManager oSessionManager, string PTCODEMP, string PTTIPPED, string PTNOMBRE, string PTDESCRI, string PTBODEGA, string PTTERPAG, string PTMODDES, string PTTERDES, string PTMONEDA, string PTIDIOMA, string PTLISPRE,
                                          string PTCDIMPF, string PTCDTRAN, string PTTIPFAC, int? PTLSTSEP, string PTCDCLA1, string PTCDCLA2, string PTCDCLA3, string PTCDCLA4, string PTCDCLA5, string PTCDCLA6, string PTDTTEC1, string PTDTTEC2, string PTDTTEC3,
                                          string PTDTTEC4, string PTDTTEC5, string PTDTTEC6, string PTCDCLD1, string PTCDCLD2, string PTCDCLD3, string PTCDCLD4, string PTCDCLD5, string PTCDCLD6, string PTDTTCD1, string PTDTTCD2, string PTDTTCD3, string PTDTTCD4,
                                          string PTDTTCD5, string PTDTTCD6, string PTESTADO, string PTCAUSAE, string PTNMUSER, string PTCOTIZA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBTIPPED SET PTNOMBRE=@p2,PTDESCRI=@p3,PTBODEGA=@p4,PTTERPAG=@p5,PTMODDES=@p6,PTTERDES=@p7,PTMONEDA=@p8,PTIDIOMA=@p9,PTLISPRE=@p10,PTCDIMPF=@p11,PTCDTRAN=@p12,PTTIPFAC=@p13,PTLSTSEP=@p14,PTCDCLA1=@p15,");
                sSql.AppendLine("                      PTCDCLA2=@p16,PTCDCLA3=@p17,PTCDCLA4=@p18,PTCDCLA5=@p19,PTCDCLA6=@p20,PTDTTEC1=@p21,PTDTTEC2=@p22,PTDTTEC3=@p23,PTDTTEC4=@p24,PTDTTEC5=@p25,PTDTTEC6=@p26,PTCDCLD1=@p27,PTCDCLD2=@p28,PTCDCLD3=@p29,PTCDCLD4=@p30,PTCDCLD5=@p31,");
                sSql.AppendLine("                      PTCDCLD6=@p32,PTDTTCD1=@p33,PTDTTCD2=@p34,PTDTTCD3=@p35,PTDTTCD4=@p36,PTDTTCD5=@p37,PTDTTCD6=@p38,PTESTADO=@p39,PTCAUSAE=@p40,PTNMUSER=@p41,PTFECMOD=GETDATE(),PTCOTIZA=@p42");
                sSql.AppendLine("WHERE PTCODEMP=@p0 AND PTTIPPED=@p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, PTCODEMP, PTTIPPED, PTNOMBRE, PTDESCRI, PTBODEGA, PTTERPAG, PTMODDES, PTTERDES, PTMONEDA, PTIDIOMA, PTLISPRE, PTCDIMPF, PTCDTRAN, PTTIPFAC, PTLSTSEP, PTCDCLA1,
                                                PTCDCLA2, PTCDCLA3, PTCDCLA4, PTCDCLA5, PTCDCLA6, PTDTTEC1, PTDTTEC2, PTDTTEC3, PTDTTEC4, PTDTTEC5, PTDTTEC6, PTCDCLD1, PTCDCLD2, PTCDCLD3, PTCDCLD4, PTCDCLD5,
                                                PTCDCLD6, PTDTTCD1, PTDTTCD2, PTDTTCD3, PTDTTCD4, PTDTTCD5, PTDTTCD6, PTESTADO, PTCAUSAE, PTNMUSER, PTCOTIZA);
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
