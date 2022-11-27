using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Pedidos
{
    public class LtaEmpaqueBD
    {
        public static DataTable GetLtaEmpaque(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.AppendLine("SELECT TB_EMPAQUEHD.*,PEDIDOHD.* ,TRCODNIT,TRNOMBRE,TRNOMBR2,TRAPELLI,(TRNOMBRE+ISNULL(TRNOMBR2,'')+ ' ' + ISNULL(TRAPELLI,'')) NOM_TER, ");
                sql.AppendLine("TRDIRECC,TRNROTEL,TRCORREO,TRCIUDAD,TRCDPAIS,TRMONEDA,TRTERPAG");
                sql.AppendLine("FROM TB_EMPAQUEHD WITH(NOLOCK)");
                sql.AppendLine("INNER JOIN PEDIDOHD WITH(NOLOCK) ON (PHCODEMP = LH_CODEMP AND PHPEDIDO = LH_PEDIDO)");
                sql.AppendLine("INNER JOIN TERCEROS WITH(NOLOCK) ON (TRCODEMP = PHCODEMP AND TRCODTER = PHCODCLI) ");                               
                sql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql.AppendLine(" AND " + filter);
                }
                //sql.AppendLine("ORDER BY PHPEDIDO");
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
        public static DataTable GetLtaEmpaqueDT(SessionManager oSessionManager, string LD_CODEMP, int LH_LSTPAQ)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT TB_EMPAQUEDT.*,ARNOMBRE,PDPRECIO,PDPRELIS,PDDESCUE,TTVALORF,PDCODDES,TANOMBRE,0 LD_CANCAN ");
                sSql.AppendLine("FROM TB_EMPAQUEDT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TB_EMPAQUEHD WITH(NOLOCK) ON (LH_CODEMP = LD_CODEMP AND TB_EMPAQUEHD.LH_LSTPAQ = TB_EMPAQUEDT.LH_LSTPAQ)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (LD_CODEMP = ARCODEMP AND LD_TIPPRO = ARTIPPRO AND LD_CLAVE1 = ARCLAVE1 AND LD_CLAVE2 = ARCLAVE2 AND LD_CLAVE3 = ARCLAVE3 AND LD_CLAVE4 = ARCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");                
                sSql.AppendLine("INNER JOIN PEDIDODT WITH(NOLOCK) ON (PDCODEMP = LD_CODEMP AND PDTIPPRO = LD_TIPPRO AND PDCLAVE1 = LD_CLAVE1 AND PDCLAVE2 = LD_CLAVE2 ");
                sSql.AppendLine("                                 AND PDCLAVE3 = LD_CLAVE3 AND PDCLAVE4 = LD_CLAVE4 AND PDLINNUM = LD_ITMPAQ AND PDPEDIDO = LH_PEDIDO) ");
                sSql.AppendLine("LEFT OUTER JOIN TBTABLAS WITH(NOLOCK) ON (TTCODEMP = PDCODEMP AND TTCODTAB = 'IMPF' AND TTCODCLA = ARCDIMPF)");
                sSql.AppendLine(" WHERE LD_CODEMP=@p0 AND TB_EMPAQUEDT.LH_LSTPAQ=@p1");


                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, LD_CODEMP, LH_LSTPAQ);
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
        public static double getCantidadesPedidos(SessionManager oSessionManager, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT ISNULL((SUM(PDCANTID)-SUM(LD_CANTID)),0) DIFF ");
                sSql.AppendLine("FROM PEDIDODT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN PEDIDOHD WITH(NOLOCK) ON (PHCODEMP = PDCODEMP AND PDPEDIDO = PHPEDIDO)     ");               
                sSql.AppendLine(" WHERE PDCODEMP=@p0 AND PDTIPPRO=@p1 AND PDCLAVE1=@p2 AND PDCLAVE2=@p3 AND PDCLAVE3=@p4 AND PDCLAVE4=@p5 AND PDBODEGA=@p6 AND PHESTADO IN ('LQ')");


                return Convert.ToDouble(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, TP, C1, C2, C3, C4, BD));
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
        public static DataTable GetDetalleMovimientos(SessionManager oSessionManager, string CODEMP, int LH_LSTPAQ, int ITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try {

                sSql.AppendLine("SELECT MBBODEGA,MBCANTID,MLCDLOTE,MLCANTID,@p1 LH_LSTPAQ,@p2 LD_ITMPAQ");
                sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN MOVIMLOT WITH(NOLOCK) ON (MBCODEMP = MLCODEMP AND MBIDMOVI = MLIDMOVI AND MBIDITEM = MLIDITEM )");
                sSql.AppendLine("WHERE MBCODEMP =@p0 ");
                sSql.AppendLine(" AND MBIDMOVI = (SELECT LD_NRMOV FROM TB_EMPAQUEDT WITH(NOLOCK) WHERE LD_CODEMP = MBCODEMP AND LH_LSTPAQ = @p1 AND LD_ITMPAQ = MBIDITEM ) ");
                sSql.AppendLine("AND MBIDITEM = @p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP,LH_LSTPAQ,ITEM);
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
        public static DataTable GetDisposicion(SessionManager oSessionManager, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD, int IT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT BBBODEGA,BBCANTID,BBBODPED,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4,BLCDLOTE,BLCANTID,BLDTTEC1,BLDTTEC2,BDNOMBRE,@p7 IT");
                sSql.AppendLine("FROM BALANBOD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA ON (BBCODEMP = BDCODEMP AND BBBODEGA = BDBODEGA)");
                sSql.AppendLine("LEFT OUTER JOIN BALANLOT ON (BBCODEMP = BLCODEMP AND BBCLAVE1 = BLCLAVE1 AND BBCLAVE2 = BLCLAVE2 AND BBCLAVE3 = BLCLAVE3 AND BBCLAVE4 = BLCLAVE4 AND BBBODEGA = BLBODEGA AND BLCANTID> 0 )");                
                sSql.AppendLine("WHERE BBCANTID > 0 AND BBCODEMP=@p0 AND BBBODEGA=@p1 AND BBTIPPRO=@p2 AND BBCLAVE1 =@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP, BD, TP, C1, C2, C3, C4, IT);
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
        public static DataTable GetDisposicion(SessionManager oSessionManager, string CODEMP, string TP, string C1, string C2, string C3, string C4, string BD,string LOTE,string ELEMENTO,int IT )
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT BBBODEGA,BBCANTID,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4,BLCDLOTE,BLCANTID,BLDTTEC1,BLDTTEC2,BDNOMBRE,@p7 IT,BECANTID,BECDELEM");
                sSql.AppendLine("FROM BALANBOD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA ON (BBCODEMP = BDCODEMP AND BBBODEGA = BDBODEGA)");
                sSql.AppendLine("INNER JOIN BALANLOT ON (BBCODEMP = BLCODEMP AND BBCLAVE1 = BLCLAVE1 AND BBCLAVE2 = BLCLAVE2 AND BBCLAVE3 = BLCLAVE3 AND BBCLAVE4 = BLCLAVE4 AND BBBODEGA = BLBODEGA AND BLCANTID> 0 )");
                sSql.AppendLine("INNER JOIN BALANELE ON (BECODEMP = BLCODEMP AND BECLAVE1 = BLCLAVE1 AND BECLAVE2 = BLCLAVE2 AND BECLAVE3 = BLCLAVE3 AND BBCLAVE4 = BLCLAVE4 AND BEBODEGA = BLBODEGA AND BECANTID> 0 AND BLCDLOTE = BECDLOTE)");
                sSql.AppendLine("WHERE BBCANTID > 0 AND BBCODEMP=@p0 AND BBBODEGA=@p1 AND BBTIPPRO=@p2 AND BBCLAVE1 =@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6 AND BLCDLOTE=@p8 AND BECDELEM=@p9");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(),CommandType.Text, CODEMP, BD, TP, C1, C2, C3, C4, IT,LOTE,ELEMENTO);
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
        public int InsertListaHD(SessionManager oSessionManager, string LH_CODEMP,int LH_LSTPAQ,int LH_PEDIDO,string LH_BODEGA,string LH_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_EMPAQUEHD (LH_CODEMP,LH_LSTPAQ,LH_FECLST,LH_PEDIDO,LH_BODEGA,LH_ESTADO,LH_CAUSAE,LH_NMUSER,LH_FECING,LH_FECMOD)");
                sSql.AppendLine("VALUES(@p0,@p1,GETDATE(),@p2,@p3,'CE','.',@p4,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_CODEMP, LH_LSTPAQ, LH_PEDIDO, LH_BODEGA, LH_NMUSER);
            
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
        public int InsertListaDT(SessionManager oSessionManager, string LD_CODEMP,int LH_LSTPAQ,int LD_ITMPAQ,string LD_TIPPRO,string LD_CLAVE1,string LD_CLAVE2,string LD_CLAVE3,
                                 string LD_CLAVE4,double LD_CANTID,double LD_PESONT,string LD_BODEGA,string LD_CDLOTE,int LD_CDELEM,int LD_NRMOV,string LD_ESTADO,string LD_CAUSAE,
                                 string LD_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TB_EMPAQUEDT (LD_CODEMP,LH_LSTPAQ,LD_ITMPAQ,LD_TIPPRO,LD_CLAVE1,LD_CLAVE2,LD_CLAVE3,LD_CLAVE4,LD_CANTID,LD_PESONT,LD_BODEGA,");
                sSql.AppendLine("LD_CDLOTE,LD_CDELEM,LD_NRMOV,LD_ESTADO,LD_CAUSAE,LD_NMUSER,LD_FECING,LD_FECMOD) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,");
                sSql.AppendLine("@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LD_CODEMP, LH_LSTPAQ, LD_ITMPAQ, LD_TIPPRO, LD_CLAVE1, LD_CLAVE2, LD_CLAVE3, LD_CLAVE4, LD_CANTID, LD_PESONT, LD_BODEGA,
                                                LD_CDLOTE, LD_CDELEM, LD_NRMOV, LD_ESTADO, LD_CAUSAE, LD_NMUSER);
            
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
        public static DataTable GetPedidoDT(SessionManager oSessionManager, string PDCODEMP, int PDPEDIDO)
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
                sql.AppendLine("       ARNOMBRE, PDLINNUM, PDCODDES, PDSUBTOT, IM_IMAGEN, ");
                sql.AppendLine("ISNULL((SELECT SUM(LD_CANTID) ");
                sql.AppendLine("    FROM TB_EMPAQUEHD WITH(NOLOCK)");
                sql.AppendLine("  INNER JOIN TB_EMPAQUEDT WITH(NOLOCK) ON (TB_EMPAQUEHD.LH_LSTPAQ = TB_EMPAQUEDT.LH_LSTPAQ)");
                sql.AppendLine("  WHERE PDPEDIDO = TB_EMPAQUEHD.LH_PEDIDO");
                sql.AppendLine("    AND PDTIPPRO = TB_EMPAQUEDT.LD_TIPPRO");
                sql.AppendLine("    AND PDCLAVE1 = TB_EMPAQUEDT.LD_CLAVE1");
                sql.AppendLine("    AND PDCLAVE2 = TB_EMPAQUEDT.LD_CLAVE2");
                sql.AppendLine("    AND PDCLAVE3 = TB_EMPAQUEDT.LD_CLAVE3");
                sql.AppendLine("    AND PDCLAVE4 = TB_EMPAQUEDT.LD_CLAVE4");
                sql.AppendLine("    AND LH_ESTADO <> 'AN'");
                sql.AppendLine("    ),0)	CAN_LST,TANOMBRE ");
                sql.AppendLine("  FROM PEDIDODT WITH(NOLOCK) ");
                sql.AppendLine("  INNER JOIN ARTICULO WITH(NOLOCK) ON( PDCODEMP = ARCODEMP AND PDCLAVE1 = ARCLAVE1 AND PDCLAVE2 = ARCLAVE2");
                sql.AppendLine("                                   AND PDCLAVE3 = ARCLAVE3 AND PDCLAVE4 = ARCLAVE4)");
                sql.AppendLine("  INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");
                sql.AppendLine("  LEFT OUTER JOIN IMAGENES WITH(NOLOCK) ON( IM_CODEMP = ARCODEMP AND IM_TIPPRO = ARTIPPRO AND IM_CLAVE1 = ARCLAVE1 ");
                sql.AppendLine("                                   AND IM_CLAVE2 ='.' AND IM_CLAVE3 ='.' AND IM_CLAVE4 ='.' AND IM_TIPIMA = 1)");
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
        public static int GetTieneFactura(SessionManager oSessionManager, string LD_CODEMP, int LH_LSTPAQ)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT ISNULL(COUNT(*),0) FROM FACTURAHD WHERE HDCODEMP=@p0 AND LH_LSTPAQ=@p0");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, LD_CODEMP, LH_LSTPAQ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                sSql = null;
            }
        }
        public static int ActivarEmpaqueHD(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ, string LH_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_EMPAQUEHD SET LH_ESTADO='CE',LH_NMUSER=@p0,LH_FECMOD=GETDATE() WHERE LH_CODEMP =@p1 AND LH_LSTPAQ = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_NMUSER, LH_CODEMP, LH_LSTPAQ);
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
        public static int AnularEmpaqueHD(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ, string LH_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_EMPAQUEHD SET LH_ESTADO='AN',LH_NMUSER=@p0,LH_FECMOD=GETDATE() WHERE LH_CODEMP =@p1 AND LH_LSTPAQ = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_NMUSER, LH_CODEMP, LH_LSTPAQ);
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
        public static int AnularEmpaqueDT(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ, string LH_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TB_EMPAQUEDT SET LD_ESTADO='AN',LD_NMUSER=@p0,LD_FECMOD=GETDATE() WHERE LD_CODEMP =@p1 AND LH_LSTPAQ = @p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_NMUSER, LH_CODEMP, LH_LSTPAQ);
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
        public static int GetLtaNroMov(SessionManager oSessionManager, string LD_CODEMP, int LH_LSTPAQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TOP 1 LD_NRMOV ");
                sSql.AppendLine("FROM TB_EMPAQUEDT ");               
                sSql.AppendLine(" WHERE LD_CODEMP=@p0 AND LH_LSTPAQ=@p1");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, LD_CODEMP, LH_LSTPAQ));
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
        public static int CerrarEmpaqueHD(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ, string LH_NMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                //sSql.AppendLine("UPDATE TB_EMPAQUEHD SET LH_ESTADO='FA',LH_NMUSER=@p0,LH_FECMOD=GETDATE() WHERE LH_CODEMP =@p1 AND LH_LSTPAQ = @p2");
                sSql.AppendLine("UPDATE TB_EMPAQUEHD SET LH_ESTADO='FA',LH_FECMOD=GETDATE() WHERE LH_CODEMP =@p0 AND LH_LSTPAQ = @p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_CODEMP, LH_LSTPAQ);
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
        public static DataTable GetCajas(SessionManager oSessionManager, string LD_CODEMP, int LH_LSTPAQ, int LD_ITMPAQ)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT * FROM TB_CAJASXLISTA WITH(NOLOCK) WHERE LD_CODEMP=@p0 AND LH_LSTPAQ=@p1 AND LD_ITMPAQ=@p2");
                return DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,LD_CODEMP, LH_LSTPAQ, LD_ITMPAQ);
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
        public int InsertCajas(SessionManager oSessionManager, string LD_CODEMP, int LH_LSTPAQ, int LD_ITMPAQ, int CL_CAJA, int CL_CANTIDAD)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_CAJASXLISTA (LD_CODEMP, LH_LSTPAQ, LD_ITMPAQ, CL_CAJA, CL_CANTIDAD) VALUES (@p0,@p1,@p2,@p3,@p4) ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LD_CODEMP, LH_LSTPAQ, LD_ITMPAQ, CL_CAJA, CL_CANTIDAD);
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
        //Evidencias
        public static DataTable GetEvidencias(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EV_CODIGO,LH_CODEMP,LH_LSTPAQ,EV_DESCRIPCION,EV_FECING,EV_USUARIO,");
                sSql.AppendLine("'                                                                                                                                                                                                                                                        ' ruta");
                sSql.AppendLine("FROM TB_EVIDENCIA WITH(NOLOCK) ");                
                sSql.AppendLine(" WHERE LH_CODEMP=@p0 AND LH_LSTPAQ=@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, LH_CODEMP, LH_LSTPAQ);
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
        public static int InsertEvidencia(SessionManager oSessionManager, string LH_CODEMP, int LH_LSTPAQ, string EV_DESCRIPCION, object EV_FOTO, string EV_USUARIO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TB_EVIDENCIA (LH_CODEMP, LH_LSTPAQ, EV_DESCRIPCION, EV_FOTO, EV_USUARIO, EV_FECING) VALUES (@p0,@p1,@p2,@p3,@p4,GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, LH_CODEMP, LH_LSTPAQ, EV_DESCRIPCION, EV_FOTO, EV_USUARIO);
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
        public static DataTable GetEvidenciasFoto(SessionManager oSessionManager, int EV_CODIGO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT EV_FOTO");
                sSql.AppendLine("FROM TB_EVIDENCIA WITH(NOLOCK) ");
                sSql.AppendLine(" WHERE EV_CODIGO=@p0");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, EV_CODIGO);
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
