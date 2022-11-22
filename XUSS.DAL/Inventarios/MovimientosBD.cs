using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Inventarios
{
    public class MovimientosBD
    {
        //MOVININV
        #region
        public int InsertMovimiento(SessionManager oSessionManager, string MICODEMP, int MIIDMOVI, int MIOTMOVI, string MIBODEGA, string MIOTBODE,string MICDTRAN, 
                                    int? MIPEDIDO,int? MICOMPRA,int? MICODTER,string MICDDOCU,DateTime? MIFECDOC,string MICOMENT,string MIESTADO,string MICAUSAE,string MINMUSER,
                                    int? MIORDPRO,int? MILINPRO,int? MINROTRA,string MICODMAQ,int? MIRECIBO, int? MISUCURSAL, string MIUSERSOL, string MIUSERAPR)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO MOVIMINV (MICODEMP,MIIDMOVI,MIOTMOVI,MIFECMOV,MIBODEGA,MIOTBODE,MICDTRAN,MIPEDIDO,MICOMPRA,MICODTER,MICDDOCU,MIFECDOC,MICOMENT,MIESTADO,");
                sSql.AppendLine("MICAUSAE,MINMUSER,MIFECING,MIFECMOD,MIORDPRO,MILINPRO,MINROTRA,MICODMAQ,MIRECIBO,MISUCURSAL,MIUSERSOL,MIUSERAPR) VALUES ");
                sSql.AppendLine("(@p0,@p1,@p2,GETDATE(),@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,GETDATE(),GETDATE(),@p5,@p16,@p17,@p18,@p19,@p20,@p21,@p22)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, 
                                MICODEMP, MIIDMOVI, MIOTMOVI, MIBODEGA, MIOTBODE, MICDTRAN, MIPEDIDO, MICOMPRA, MICODTER, MICDDOCU, MIFECDOC, MICOMENT, MIESTADO,
                                MICAUSAE, MINMUSER, MIORDPRO, MILINPRO, MINROTRA, MICODMAQ, MIRECIBO, MISUCURSAL, MIUSERSOL, MIUSERAPR);

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
        public int UpdateMovimiento(SessionManager oSessionManager, string MICODEMP, int MIIDMOVI, string MIESTADO, string MINMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMINV SET MIESTADO=@p2,MINMUSER=@p3,MIFECMOD=GETDATE() WHERE MICODEMP=@p0 AND MIIDMOVI = @p1 ");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MICODEMP, MIIDMOVI, MIESTADO, MINMUSER);

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
        public DataTable GetMovimInv(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT MOVIMINV.*, (TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,'')) NOM_TER FROM MOVIMINV WITH(NOLOCK) ");
                sSql.AppendLine("LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (MICODEMP = TRCODEMP AND MICODTER = TRCODTER)");
                sSql.AppendLine(" WHERE 1=1 ");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
                }
                sSql.AppendLine(" ORDER BY MIIDMOVI ");
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
        //MOVIMBOD
        #region
        public DataTable CargarMovimiento(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM MOVIMBOD WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 AND MBIDITEM=@p2");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI, MBIDITEM);
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
        public DataTable CargarMovimiento(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT MOVIMBOD.*,ARNOMBRE, ARCDALTR, ARUNDINV,TANOMBRE, ");
                sSql.AppendLine(" CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP ");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 ");
                sSql.AppendLine("                              AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine(" CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                sSql.AppendLine("                              AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                sSql.AppendLine("                              AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3 ");
                sSql.AppendLine("FROM MOVIMBOD WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO ON (ARCODEMP = MBCODEMP AND ARTIPPRO = MBTIPPRO AND ARCLAVE1 = MBCLAVE1 AND ARCLAVE2 = MBCLAVE2 ");
                sSql.AppendLine("                    AND ARCLAVE3 = MBCLAVE3 AND ARCLAVE4 = MBCLAVE4) ");
                sSql.AppendLine(" INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO)");

                sSql.AppendLine("WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI);
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
        public int ExisteMovimiento(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM MOVIMBOD WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 AND MBIDITEM=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI,MBIDITEM));
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
        public int InsertMovimBod(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM, DateTime MBFECMOV, string MBBODEGA, string MBTIPPRO, string MBCLAVE1,
                                  string MBCLAVE2, string MBCLAVE3, string MBCLAVE4, string MBCODCAL, string MBCDTRAN, double MBCANMOV, string MBUNDMOV, double MBCANTID, double MBCANORI,
                                  double MBSALDOI, double MBCOSTOA, double MBCOSTOB, int MBOTMOVI, string MBOTBODE, string MBESTADO, string MBCAUSAE, string MBNMUSER, double MBORDPRO,
                                  int MBLINPRO, double MBCSTFOB, double MBCSTNAL, double MBCSTANT, string MBCSTFRZ)
        {
            StringBuilder sSql = new StringBuilder();
            try { 
                sSql.AppendLine("INSERT INTO MOVIMBOD (MBCODEMP,MBIDMOVI,MBIDITEM,MBFECMOV,MBBODEGA,MBTIPPRO,MBCLAVE1,MBCLAVE2,MBCLAVE3,MBCLAVE4,MBCODCAL,MBCDTRAN,MBCANMOV,");
                sSql.AppendLine("MBUNDMOV,MBCANTID,MBCANORI,MBSALDOI,MBCOSTOA,MBCOSTOB,MBOTMOVI,MBOTBODE,MBESTADO,MBCAUSAE,MBNMUSER,MBFECING,MBFECMOD,MBORDPRO,MBLINPRO,");
                sSql.AppendLine("MBCSTFOB,MBCSTNAL,MBCSTANT,MBCSTFRZ) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine("@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,GETDATE(),GETDATE(),@p24,@p25,");
                sSql.AppendLine("@p26,@p27,@p28,@p29)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI, MBIDITEM, MBFECMOV, MBBODEGA, MBTIPPRO, MBCLAVE1, MBCLAVE2, MBCLAVE3, MBCLAVE4, MBCODCAL, MBCDTRAN, MBCANMOV,
                       MBUNDMOV, MBCANTID, MBCANORI, MBSALDOI, MBCOSTOA, MBCOSTOB, MBOTMOVI, MBOTBODE, MBESTADO, MBCAUSAE, MBNMUSER, MBORDPRO, MBLINPRO,
                       MBCSTFOB, MBCSTNAL, MBCSTANT, MBCSTFRZ);
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
        public int UpdateMovimBod(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM, DateTime MBFECMOV, string MBBODEGA, string MBTIPPRO, string MBCLAVE1,
                                  string MBCLAVE2, string MBCLAVE3, string MBCLAVE4, string MBCODCAL, string MBCDTRAN, double MBCANMOV, string MBUNDMOV, double MBCANTID, double MBCANORI,
                                  double MBSALDOI, double MBCOSTOA, double MBCOSTOB, int MBOTMOVI, string MBOTBODE, string MBESTADO, string MBCAUSAE, string MBNMUSER, double MBORDPRO,
                                  int MBLINPRO, double MBCSTFOB, double MBCSTNAL, double MBCSTANT, string MBCSTFRZ)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE MOVIMBOD SET MBFECMOV=@p3,MBBODEGA=@p4,MBTIPPRO=@p5,MBCLAVE1=@p6,MBCLAVE2=@p7,MBCLAVE3=@p8,MBCLAVE4=@p9,MBCODCAL=@p10,MBCDTRAN=@p11,MBCANMOV=@p12,");
                sSql.AppendLine("MBUNDMOV=@p13,MBCANTID=@p14,MBCANORI=@p15,MBSALDOI=@p16,MBCOSTOA=@p17,MBCOSTOB=@p18,MBOTMOVI=@p19,MBOTBODE=@p20,MBESTADO=@p21,MBCAUSAE=@p22,MBNMUSER=@p23,MBFECING=GETDATE(),MBFECMOD=GETDATE(),MBORDPRO=@p24,MBLINPRO=@p25,");
                sSql.AppendLine("MBCSTFOB=@p26,MBCSTNAL=@p27,MBCSTANT=@p28,MBCSTFRZ=@p29 ");
                sSql.AppendLine(" WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 AND MBIDITEM=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI, MBIDITEM, MBFECMOV, MBBODEGA, MBTIPPRO, MBCLAVE1, MBCLAVE2, MBCLAVE3, MBCLAVE4, MBCODCAL, MBCDTRAN, MBCANMOV,
                       MBUNDMOV, MBCANTID, MBCANORI, MBSALDOI, MBCOSTOA, MBCOSTOB, MBOTMOVI, MBOTBODE, MBESTADO, MBCAUSAE, MBNMUSER, MBORDPRO, MBLINPRO,
                       MBCSTFOB, MBCSTNAL, MBCSTANT, MBCSTFRZ);
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
        public int UpdateMovimBod(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM, string MBESTADO, string MBCAUSAE,string MBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMBOD SET MBESTADO=@p3,MBCAUSAE=@p4,MBNMUSER=@p5,MBFECMOD=GETDATE()");                
                sSql.AppendLine(" WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 AND MBIDITEM=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI, MBIDITEM, MBESTADO, MBCAUSAE, MBNMUSER);
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
        public int UpdateMovimBod(SessionManager oSessionManager, string MBCODEMP, int MBIDMOVI, int MBIDITEM, double MBCANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try {

                sSql.AppendLine("UPDATE MOVIMBOD SET MBCANMOV=@p3,MBCANTID=@p3,MBCANORI=@p3");
                sSql.AppendLine(" WHERE MBCODEMP=@p0 AND MBIDMOVI=@p1 AND MBIDITEM=@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MBCODEMP, MBIDMOVI, MBIDITEM, MBCANTID);

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
        //MOVIMLOT
        #region
        public DataTable CargarMovimientoLot(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT *,(MLCODEMP+CAST(MLIDMOVI AS VARCHAR)+CAST(MLIDITEM AS VARCHAR)+CAST(MLIDLOTE AS VARCHAR)) LLAVE FROM MOVIMLOT WITH(NOLOCK) WHERE MLCODEMP =@p0 AND MLIDMOVI =@p1 AND MLIDITEM =@p2");
                return  DBAccess.GetDataTable(oSessionManager,sSql.ToString(),CommandType.Text,MLCODEMP, MLIDMOVI, MLIDITEM);
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
        public int ExisteMovimientoLote(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM, int MLIDLOTE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM MOVIMLOT WITH(NOLOCK) WHERE MLCODEMP=@p0 AND MLIDMOVI=@p1 AND MLIDITEM=@p2 AND MLIDLOTE=@p3");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP, MLIDMOVI, MLIDITEM, MLIDLOTE));
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
        public int GetTotalMovimientoLote(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SUM(MLCANTID) FROM MOVIMLOT WITH(NOLOCK) WHERE MLCODEMP=@p0 AND MLIDMOVI=@p1 AND MLIDITEM=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP, MLIDMOVI, MLIDITEM));
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
        public int InsertMovimLot(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM, int MLIDLOTE, DateTime MLFECMOV, string MLBODEGA, string MLTIPPRO,
                                  string MLCLAVE1, string MLCLAVE2, string MLCLAVE3, string MLCLAVE4, string MLCODCAL, string MLCDLOTE, string MLCDTRAN, double MLCANTID,
                                  double MLCANORI, double MLSALDOI, string MLLOCALI, string MLDTTEC1, string MLDTTEC2, string MLDTTEC3, string MLDTTEC4, double MLDTTEC5, double MLDTTEC6,
                                  string MLESTADO, string MLCAUSAE, string MLNMUSER, string MLCONVEN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO MOVIMLOT (MLCODEMP,MLIDMOVI,MLIDITEM,MLIDLOTE,MLFECMOV,MLBODEGA,MLTIPPRO,");
                sSql.AppendLine("MLCLAVE1,MLCLAVE2,MLCLAVE3,MLCLAVE4,MLCODCAL,MLCDLOTE,MLCDTRAN,MLCANTID,MLCANORI,");
                sSql.AppendLine("MLSALDOI,MLLOCALI,MLDTTEC1,MLDTTEC2,MLDTTEC3,MLDTTEC4,MLDTTEC5,MLDTTEC6,MLESTADO,MLCAUSAE,MLNMUSER,MLFECING,MLFECMOD,MLCONVEN) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,");
                sSql.AppendLine("@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,");
                sSql.AppendLine("@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,GETDATE(),GETDATE(),@p27)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP,MLIDMOVI,MLIDITEM,MLIDLOTE,MLFECMOV,MLBODEGA,MLTIPPRO,
                                                MLCLAVE1,MLCLAVE2,MLCLAVE3,MLCLAVE4,MLCODCAL,MLCDLOTE,MLCDTRAN,MLCANTID,MLCANORI,
                                                MLSALDOI,MLLOCALI,MLDTTEC1,MLDTTEC2,MLDTTEC3,MLDTTEC4,MLDTTEC5,MLDTTEC6,MLESTADO,MLCAUSAE,MLNMUSER,MLCONVEN);
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
        public int UpdateMovimLot(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM, int MLIDLOTE, DateTime MLFECMOV, string MLBODEGA, string MLTIPPRO,
                                  string MLCLAVE1, string MLCLAVE2, string MLCLAVE3, string MLCLAVE4, string MLCODCAL, string MLCDLOTE, string MLCDTRAN, double MLCANTID,
                                  double MLCANORI, double MLSALDOI, string MLLOCALI, string MLDTTEC1, string MLDTTEC2, string MLDTTEC3, string MLDTTEC4, double MLDTTEC5, double MLDTTEC6,
                                  string MLESTADO, string MLCAUSAE, string MLNMUSER, string MLCONVEN)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMLOT SET MLFECMOV=@p4,MLBODEGA=@p5,MLTIPPRO=@p6,");
                sSql.AppendLine("MLCLAVE1=@p7,MLCLAVE2=@p8,MLCLAVE3=@p9,MLCLAVE4=@p10,MLCODCAL=@p11,MLCDLOTE=@p12,MLCDTRAN=@p13,MLCANTID=@p14,MLCANORI=@p15,");
                sSql.AppendLine("MLSALDOI=@p16,MLLOCALI=@p17,MLDTTEC1=@p18,MLDTTEC2=@p19,MLDTTEC3=@p20,MLDTTEC4=@p21,MLDTTEC5=@p22,MLDTTEC6=@p23,MLESTADO=@p24,MLCAUSAE=@p25,MLNMUSER=@p26,MLFECING=GETDATE(),MLFECMOD=GETDATE(),MLCONVEN=@p27");
                sSql.AppendLine("WHERE MLCODEMP=@p0 AND MLIDMOVI=@p1 AND MLIDITEM=@p2 AND MLIDLOTE=@p3");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP, MLIDMOVI, MLIDITEM, MLIDLOTE, MLFECMOV, MLBODEGA, MLTIPPRO,
                                                MLCLAVE1, MLCLAVE2, MLCLAVE3, MLCLAVE4, MLCODCAL, MLCDLOTE, MLCDTRAN, MLCANTID, MLCANORI,
                                                MLSALDOI, MLLOCALI, MLDTTEC1, MLDTTEC2, MLDTTEC3, MLDTTEC4, MLDTTEC5, MLDTTEC6, MLESTADO, MLCAUSAE, MLNMUSER, MLCONVEN);
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
        public int UpdateMovimLot(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM, int MLIDLOTE, string MLESTADO, string MLNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMLOT SET MLESTADO=@p4,MLNMUSER=@p5,MLFECMOD=GETDATE()");
                sSql.AppendLine("WHERE MLCODEMP=@p0 AND MLIDMOVI=@p1 AND MLIDITEM=@p2 AND MLIDLOTE=@p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP, MLIDMOVI, MLIDITEM, MLIDLOTE,MLESTADO, MLNMUSER);
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
        public int UpdateMovimLot(SessionManager oSessionManager, string MLCODEMP, int MLIDMOVI, int MLIDITEM, int MLIDLOTE, double MLCANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMLOT SET MLCANTID=@p4");
                sSql.AppendLine("WHERE MLCODEMP=@p0 AND MLIDMOVI=@p1 AND MLIDITEM=@p2 AND MLIDLOTE=@p3");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MLCODEMP, MLIDMOVI, MLIDITEM, MLIDLOTE, MLCANTID);
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
        //MOVIMELE
        #region
        public DataTable CargarMovimientoEle(SessionManager oSessionManager, string inLlave)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM MOVIMELE WITH(NOLOCK) WHERE (MECODEMP+CAST(MEIDMOVI AS VARCHAR)+CAST(MEIDITEM AS VARCHAR)+CAST(MEIDLOTE AS VARCHAR)) = @p0");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inLlave);
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
        public DataTable CargarMovimientoEle(SessionManager oSessionManager, string MECODEMP,int MEIDMOVI, int MEIDITEM,int MEIDLOTE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM MOVIMELE WITH(NOLOCK) WHERE MECODEMP=@p0 AND MEIDMOVI=@p1 AND MEIDITEM =@p2 AND MEIDLOTE=@p3");
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE);
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
        public int InsertMovimEle(SessionManager oSessionManager, string MECODEMP, int MEIDMOVI, int MEIDITEM, int MEIDLOTE, int MEIDELEM, DateTime MEFECMOV, string MEBODEGA, string METIPPRO,
                                string MECLAVE1, string MECLAVE2, string MECLAVE3, string MECLAVE4, string MECODCAL, string MECDLOTE, string MECDELEM, string MECDTRAN,
                                double MECANTID, double MECANORI, double MEBONIFI, string MEUMELEM, int MENROELE, string MELOCALI, double MEPESOBR, double MEPESONT,
                                double MEANCHEL, double MELARGEL, string MEDTTEC1, string MEDTTEC2, string MEDTTEC3, string MEDTTEC4, double MEDTTEC5, double MEDTTEC6,
                                string MEESTADO, string MECAUSAE, string MENMUSER, string MEPDELEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO MOVIMELE (MECODEMP,MEIDMOVI,MEIDITEM,MEIDLOTE,MEIDELEM,MEFECMOV,MEBODEGA,METIPPRO,");
                sSql.AppendLine("MECLAVE1, MECLAVE2, MECLAVE3, MECLAVE4, MECODCAL, MECDLOTE, MECDELEM, MECDTRAN,");
                sSql.AppendLine("MECANTID, MECANORI, MEBONIFI, MEUMELEM, MENROELE, MELOCALI, MEPESOBR, MEPESONT,");
                sSql.AppendLine("MEANCHEL, MELARGEL, MEDTTEC1, MEDTTEC2, MEDTTEC3, MEDTTEC4, MEDTTEC5, MEDTTEC6,");
                sSql.AppendLine("MEESTADO, MECAUSAE, MENMUSER, MEFECING, MEFECMOD, MEPDELEM) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,");
                sSql.AppendLine("@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,");
                sSql.AppendLine("@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,@p32,@p33,@p34,GETDATE(),GETDATE(),@p35)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE, MEIDELEM, MEFECMOV, MEBODEGA, METIPPRO,
                                                MECLAVE1, MECLAVE2, MECLAVE3, MECLAVE4, MECODCAL, MECDLOTE, MECDELEM, MECDTRAN,
                                                MECANTID, MECANORI, MEBONIFI, MEUMELEM, MENROELE, MELOCALI, MEPESOBR, MEPESONT,
                                                MEANCHEL, MELARGEL, MEDTTEC1, MEDTTEC2, MEDTTEC3, MEDTTEC4, MEDTTEC5, MEDTTEC6,
                                                MEESTADO, MECAUSAE, MENMUSER, MEPDELEM);
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
        public int GetTotalMovimientoEle(SessionManager oSessionManager, string MECODEMP, int MEIDMOVI, int MEIDITEM, int MEIDLOTE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT SUM(MECANTID) FROM MOVIMELE WITH(NOLOCK) WHERE MECODEMP=@p0 AND MEIDMOVI=@p1 AND MEIDITEM=@p2 AND MEIDLOTE=@p3");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE));
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
        public int ExisteMovimientoEle(SessionManager oSessionManager, string MECODEMP, int MEIDMOVI, int MEIDITEM, int MEIDLOTE, int MEIDELEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM MOVIMELE WITH(NOLOCK) WHERE MECODEMP=@p0 AND MEIDMOVI=@p1 AND MEIDITEM=@p2 AND MEIDLOTE=@p3 AND MEIDELEM=@p4");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE, MEIDELEM));
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
        public int UpdateMovimEle(SessionManager oSessionManager, string MECODEMP, int MEIDMOVI, int MEIDITEM, int MEIDLOTE, int MEIDELEM, string MEESTADO, string MENMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE MOVIMELE SET MEESTADO=@p5,MENMUSER=@p6,MEFECMOD=GETDATE()");
                sSql.AppendLine("WHERE MECODEMP=@p0 AND MEIDMOVI=@p1 AND MEIDITEM=@p2 AND MEIDLOTE=@p3 AND MEIDELEM=@p4");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, MECODEMP, MEIDMOVI, MEIDITEM, MEIDLOTE, MEIDELEM, MEESTADO, MENMUSER);
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
        //BALANCES Y BODEGAS
        #region
        public int ExisteArticuloBodega(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                //sSql.AppendLine("SELECT COUNT(*) FROM BALANBOD WITH(NOLOCK) WHERE BBCODEMP=@p0 AND BBBODEGA=@p1 AND BBTIPPRO=@p2 AND BBCLAVE1=@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6 AND BBCODCAL=@p7");
                sSql.AppendLine("SELECT COUNT(*) FROM BALANBOD WHERE BBCODEMP=@p0 AND BBBODEGA=@p1 AND BBTIPPRO=@p2 AND BBCLAVE1=@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6 AND BBCODCAL=@p7");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4, BBCODCAL));
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
        public int InsertBalanBod(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL, double BBCANTID, double BBBODBOD, double BBBODPED, double BBBODPRO, double BBCANCOM, double BBCOMPED, double BBCOMPRO, double BBCANPRO,
                                  double BBPROPED, double BBCANPED, double BBPEDBOD, double BBCANTRN, string BBLOCALI, DateTime? BBFECENT, DateTime? BBFECSAL, string BBNMUSER, double BBCANCTL,
                                  double BBCANREC)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO BALANBOD (BBCODEMP,BBBODEGA,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4,BBCODCAL,BBCANTID,BBBODBOD,BBBODPED,BBBODPRO,BBCANCOM,");
                sSql.AppendLine("BBCOMPED,BBCOMPRO,BBCANPRO,BBPROPED,BBCANPED,BBPEDBOD,BBCANTRN,BBLOCALI,BBFECENT,BBFECSAL,BBNMUSER,BBFECING,BBFECMOD,BBCANCTL,BBCANREC)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine("         @p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,GETDATE(),GETDATE(),@p24,@p25)");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4, BBCODCAL, BBCANTID, BBBODBOD, BBBODPED, BBBODPRO, BBCANCOM,
                                                BBCOMPED, BBCOMPRO, BBCANPRO, BBPROPED, BBCANPED, BBPEDBOD, BBCANTRN, BBLOCALI, BBFECENT, BBFECSAL, BBNMUSER, BBCANCTL, BBCANREC);
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
        public int UpdateBalanBodCantid(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL, double BBCANTID, string BBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE BALANBOD SET BBCANTID = BBCANTID + @p0, BBNMUSER=@p1, BBFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BBCODEMP = @p2");
                sSql.AppendLine("  AND BBBODEGA = @p3");
                sSql.AppendLine("  AND BBTIPPRO = @p4");
                sSql.AppendLine("  AND BBCLAVE1 = @p5");
                sSql.AppendLine("  AND BBCLAVE2 = @p6");
                sSql.AppendLine("  AND BBCLAVE3 = @p7");
                sSql.AppendLine("  AND BBCLAVE4 = @p8");
                sSql.AppendLine("  AND BBCODCAL = @p9");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCANTID, BBNMUSER, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4,
                                  BBCODCAL);                
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
        public int UpdateBalanBodCanTrn(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL, double BBCANTID, string BBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANBOD SET BBCANTRN = BBCANTRN + @p0, BBNMUSER=@p1, BBFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BBCODEMP = @p2");
                sSql.AppendLine("  AND BBBODEGA = @p3");
                sSql.AppendLine("  AND BBTIPPRO = @p4");
                sSql.AppendLine("  AND BBCLAVE1 = @p5");
                sSql.AppendLine("  AND BBCLAVE2 = @p6");
                sSql.AppendLine("  AND BBCLAVE3 = @p7");
                sSql.AppendLine("  AND BBCLAVE4 = @p8");
                sSql.AppendLine("  AND BBCODCAL = @p9");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCANTID, BBNMUSER, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4,
                                  BBCODCAL);
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
        public int UpdateBalanBodCanCtl(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL, double BBCANTID, string BBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANBOD SET BBCANCTL = BBCANCTL + @p0, BBNMUSER=@p1, BBFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BBCODEMP = @p2");
                sSql.AppendLine("  AND BBBODEGA = @p3");
                sSql.AppendLine("  AND BBTIPPRO = @p4");
                sSql.AppendLine("  AND BBCLAVE1 = @p5");
                sSql.AppendLine("  AND BBCLAVE2 = @p6");
                sSql.AppendLine("  AND BBCLAVE3 = @p7");
                sSql.AppendLine("  AND BBCLAVE4 = @p8");
                sSql.AppendLine("  AND BBCODCAL = @p9");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCANTID, BBNMUSER, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4,
                                  BBCODCAL);
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
        public int UpdateBalanBodCanBod(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4,
                                  string BBCODCAL, double BBCANTID, string BBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANBOD SET BBBODBOD = BBBODBOD + @p0, BBNMUSER=@p1, BBFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BBCODEMP = @p2");
                sSql.AppendLine("  AND BBBODEGA = @p3");
                sSql.AppendLine("  AND BBTIPPRO = @p4");
                sSql.AppendLine("  AND BBCLAVE1 = @p5");
                sSql.AppendLine("  AND BBCLAVE2 = @p6");
                sSql.AppendLine("  AND BBCLAVE3 = @p7");
                sSql.AppendLine("  AND BBCLAVE4 = @p8");
                sSql.AppendLine("  AND BBCODCAL = @p9");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCANTID, BBNMUSER, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4,
                                  BBCODCAL);
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
        public int DeteleBalanBod(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM BALANBOD WHERE BBCODEMP =@p0 AND BBBODEGA =@p1 AND BBTIPPRO =@p2 AND BBCLAVE1=@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6");                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4);
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
        public int DeteleBalanBod(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM BALANBOD WHERE BBCODEMP =@p0 AND BBBODEGA =@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BBCODEMP, BBBODEGA);
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

        public int GetInvDisponible(SessionManager oSessionManager, string BBCODEMP, string BBBODEGA, string BBTIPPRO, string BBCLAVE1, string BBCLAVE2, string BBCLAVE3, string BBCLAVE4)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT (BBCANTID-BBCANTRN)  FROM BALANBOD WITH(NOLOCK) WHERE BBCODEMP =@p0 AND BBBODEGA =@p1 AND BBTIPPRO =@p2 AND BBCLAVE1=@p3 AND BBCLAVE2=@p4 AND BBCLAVE3=@p5 AND BBCLAVE4=@p6");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BBCODEMP, BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4));
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
        //BALANCES - LOTES - ELEMENTOS
        #region
        public int GetInvDisponibleElem(SessionManager oSessionManager, string BECODEMP, string BEBODEGA, string BECDELEM)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT (BECANTID-BEBODBOD)  FROM BALANELE WITH(NOLOCK) WHERE BECODEMP =@p0 AND BECDELEM =@p1 AND BEBODEGA=@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BECODEMP, BECDELEM, BEBODEGA));
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
        public int UpdateBalanEleCantid(SessionManager oSessionManager, string BECODEMP, string BEBODEGA, string BECDELEM, string BENMUSER, double BECANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANELE SET BECANTID = BECANTID + @p0, BENMUSER=@p1, BEFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BECODEMP = @p2");
                sSql.AppendLine("  AND BECDELEM = @p3");
                sSql.AppendLine("  AND BEBODEGA = @P4");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BECANTID, BENMUSER, BECODEMP, BECDELEM, BEBODEGA);
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
        public int UpdateBalanEleCanCtl(SessionManager oSessionManager, string BECODEMP, string BEBODEGA, string BECDELEM, string BENMUSER, double BECANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANELE SET BECANCTL = BECANCTL + @p0, BENMUSER=@p1, BEFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BECODEMP = @p2");
                sSql.AppendLine("  AND BECDELEM = @p3");
                sSql.AppendLine("  AND BEBODEGA = @p4");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BECANTID, BENMUSER, BECODEMP, BECDELEM, BEBODEGA);
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
        public int UpdateBalanEleCanBod(SessionManager oSessionManager, string BECODEMP, string BEBODEGA, string BECDELEM, string BENMUSER, double BECANTID)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANELE SET BEBODBOD = BEBODBOD + @p0, BENMUSER=@p1, BEFECMOD=GETDATE()");
                sSql.AppendLine("WHERE BECODEMP = @p2");
                sSql.AppendLine("  AND BECDELEM = @p3");
                sSql.AppendLine("  AND BEBODEGA = @p4");
                

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BECANTID, BENMUSER, BECODEMP, BECDELEM, BEBODEGA);
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
        public int ExisteEle(SessionManager oSessionManager, string BECODEMP, string BEBODEGA, string BECDELEM )
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM BALANELE WITH(NOLOCK) ");
                sSql.AppendLine("WHERE BECODEMP = @p0");
                sSql.AppendLine("  AND BECDELEM = @p1");
                sSql.AppendLine("  AND BEBODEGA = @p2");
                
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BECODEMP, BECDELEM, BEBODEGA));
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
        public int InsertBalanEle(SessionManager oSessionManager, string BECODEMP, string BECDELEM, string BEBODEGA, string BETIPPRO, string BECLAVE1, string BECLAVE2, string BECLAVE3, string BECLAVE4, string BECODCAL, string BECDLOTE, double BECANORI, double BECANTID, double BEBONIFI, string BEUMELEM, int BENROELE, double BEBODBOD,
                                double BEBODPED, double BEBODPRO, string BELOCALI, double BEPESOBR, double BEPESONT, double BEANCHEL, double BELARGEL, string BEDTTEC1, string BEDTTEC2, string BEDTTEC3, string BEDTTEC4, double BEDTTEC5, double BEDTTEC6, DateTime? BEFECENT, DateTime? BEFECSAL, string BENMUSER,
                                string BEPDELEM, double BECANCTL)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO BALANELE (BECODEMP,BECDELEM,BEBODEGA,BETIPPRO,BECLAVE1,BECLAVE2,BECLAVE3,BECLAVE4,BECODCAL,BECDLOTE,BECANORI,BECANTID,BEBONIFI,BEUMELEM,BENROELE,BEBODBOD,");
                sSql.AppendLine("BEBODPED, BEBODPRO, BELOCALI, BEPESOBR, BEPESONT, BEANCHEL, BELARGEL, BEDTTEC1, BEDTTEC2, BEDTTEC3, BEDTTEC4, BEDTTEC5, BEDTTEC6, BEFECENT, BEFECSAL, BENMUSER,");
                sSql.AppendLine("BEFECING, BEFECMOD, BEPDELEM, BECANCTL) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine(" @p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,@p23,@p24,@p25,@p26,@p27,@p28,@p29,@p30,@p31,GETDATE(),GETDATE(),@p32,@p33)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BECODEMP, BECDELEM, BEBODEGA, BETIPPRO, BECLAVE1, BECLAVE2, BECLAVE3, BECLAVE4, BECODCAL, BECDLOTE, BECANORI, BECANTID, BEBONIFI, BEUMELEM, BENROELE, BEBODBOD,
                                                BEBODPED, BEBODPRO, BELOCALI, BEPESOBR, BEPESONT, BEANCHEL, BELARGEL, BEDTTEC1, BEDTTEC2, BEDTTEC3, BEDTTEC4, BEDTTEC5, BEDTTEC6, BEFECENT, BEFECSAL, BENMUSER,
                                                BEPDELEM, BECANCTL);
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
        public int ExisteLote(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL, string BLCDLOTE)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT COUNT(*) FROM BALANLOT WITH(NOLOCK) ");
                sSql.AppendLine("WHERE BLCODEMP = @p0");
                sSql.AppendLine("  AND BLBODEGA = @p1");
                sSql.AppendLine("  AND BLTIPPRO = @p2");
                sSql.AppendLine("  AND BLCLAVE1 = @p3");
                sSql.AppendLine("  AND BLCLAVE2 = @p4");
                sSql.AppendLine("  AND BLCLAVE3 = @p5");
                sSql.AppendLine("  AND BLCLAVE4 = @p6");
                sSql.AppendLine("  AND BLCODCAL = @p7");
                sSql.AppendLine("  AND BLCDLOTE = @p8");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL,
                                                BLCDLOTE));
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
        public int InsertBalanLot(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL, string BLCDLOTE,
                                    double BLCANTID, double BLBODBOD, double BLBODPED, double BLBODPRO, string BLLOCALI, string BLDTTEC1, string BLDTTEC2, string BLDTTEC3, string BLDTTEC4, double BLDTTEC5, double BLDTTEC6,
                                    DateTime? BLFECENT, DateTime? BLFECSAL, string BLNMUSER, string BLCONVEN, double BLCANCTL)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO BALANLOT (BLCODEMP,BLBODEGA,BLTIPPRO,BLCLAVE1,BLCLAVE2,BLCLAVE3,BLCLAVE4,BLCODCAL,BLCDLOTE,BLCANTID,BLBODBOD,BLBODPED,BLBODPRO,");
                sSql.AppendLine("BLLOCALI,BLDTTEC1,BLDTTEC2,BLDTTEC3,BLDTTEC4,BLDTTEC5,BLDTTEC6,BLFECENT,BLFECSAL,BLNMUSER,BLFECING,BLFECMOD,BLCONVEN,BLCANCTL) VALUES");
                sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,");
                sSql.AppendLine(" @p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,GETDATE(),GETDATE(),@p23,@p24)");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text,
                    BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL, BLCDLOTE, BLCANTID, BLBODBOD, BLBODPED, BLBODPRO,
                    BLLOCALI, BLDTTEC1, BLDTTEC2, BLDTTEC3, BLDTTEC4, BLDTTEC5, BLDTTEC6, BLFECENT, BLFECSAL, BLNMUSER, BLCONVEN, BLCANCTL);
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
        public int UpdateBalanLotCantid(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL, string BLCDLOTE,
                                    double BLCANTID, string BLNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("UPDATE BALANLOT SET BLCANTID = BLCANTID + @p0, BLNMUSER =@p1,BLFECMOD =GETDATE() ");
                sSql.AppendLine("WHERE BLCODEMP = @p2");
                sSql.AppendLine("  AND BLBODEGA = @p3");
                sSql.AppendLine("  AND BLTIPPRO = @p4");
                sSql.AppendLine("  AND BLCLAVE1 = @p5");
                sSql.AppendLine("  AND BLCLAVE2 = @p6");
                sSql.AppendLine("  AND BLCLAVE3 = @p7");
                sSql.AppendLine("  AND BLCLAVE4 = @p8");
                sSql.AppendLine("  AND BLCODCAL = @p9");
                sSql.AppendLine("  AND BLCDLOTE = @p10");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLCANTID,BLNMUSER, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL,
                                                BLCDLOTE);
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
        public int UpdateBalanLotCanCtl(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL, string BLCDLOTE,
                                    double BLCANTID, string BLNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANLOT SET BLCANCTL = BLCANCTL + @p0, BLNMUSER =@p1,BLFECMOD =GETDATE() ");
                sSql.AppendLine("WHERE BLCODEMP = @p2");
                sSql.AppendLine("  AND BLBODEGA = @p3");
                sSql.AppendLine("  AND BLTIPPRO = @p4");
                sSql.AppendLine("  AND BLCLAVE1 = @p5");
                sSql.AppendLine("  AND BLCLAVE2 = @p6");
                sSql.AppendLine("  AND BLCLAVE3 = @p7");
                sSql.AppendLine("  AND BLCLAVE4 = @p8");
                sSql.AppendLine("  AND BLCODCAL = @p9");
                sSql.AppendLine("  AND BLCDLOTE = @p10");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLCANTID, BLNMUSER, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL,
                                                BLCDLOTE);
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
        public int UpdateBalanLotCanBod(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL, string BLCDLOTE,
                                    double BLCANTID, string BLNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE BALANLOT SET BLBODBOD = BLBODBOD + @p0, BLNMUSER =@p1,BLFECMOD =GETDATE() ");
                sSql.AppendLine("WHERE BLCODEMP = @p2");
                sSql.AppendLine("  AND BLBODEGA = @p3");
                sSql.AppendLine("  AND BLTIPPRO = @p4");
                sSql.AppendLine("  AND BLCLAVE1 = @p5");
                sSql.AppendLine("  AND BLCLAVE2 = @p6");
                sSql.AppendLine("  AND BLCLAVE3 = @p7");
                sSql.AppendLine("  AND BLCLAVE4 = @p8");
                sSql.AppendLine("  AND BLCODCAL = @p9");
                sSql.AppendLine("  AND BLCDLOTE = @p10");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BLCANTID, BLNMUSER, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL,
                                                BLCDLOTE);
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
        public DataTable GetLotes(SessionManager oSessionManager, string BLCODEMP, string BLBODEGA, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM BALANLOT WITH(NOLOCK) ");
                sSql.AppendLine("WHERE BLCODEMP = @p0");
                sSql.AppendLine("  AND BLBODEGA = @p1");
                sSql.AppendLine("  AND BLTIPPRO = @p2");
                sSql.AppendLine("  AND BLCLAVE1 = @p3");
                sSql.AppendLine("  AND BLCLAVE2 = @p4");
                sSql.AppendLine("  AND BLCLAVE3 = @p5");
                sSql.AppendLine("  AND BLCLAVE4 = @p6");
                sSql.AppendLine("  AND BLCODCAL = @p7");
                sSql.AppendLine("  ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BLCODEMP, BLBODEGA, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL);
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
        public DataTable GetLotesTF(SessionManager oSessionManager, string BLCODEMP, string TFTIPFAC, string BLTIPPRO, string BLCLAVE1, string BLCLAVE2, string BLCLAVE3, string BLCLAVE4, string BLCODCAL)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM BALANLOT WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN TBTIPFAC WITH(NOLOCK) ON (TFCODEMP = BLCODEMP AND TFBODEGA = BLBODEGA) ");
                sSql.AppendLine("WHERE BLCODEMP = @p0");
                sSql.AppendLine("  AND TFTIPFAC = @p1");
                sSql.AppendLine("  AND BLTIPPRO = @p2");
                sSql.AppendLine("  AND BLCLAVE1 = @p3");
                sSql.AppendLine("  AND BLCLAVE2 = @p4");
                sSql.AppendLine("  AND BLCLAVE3 = @p5");
                sSql.AppendLine("  AND BLCLAVE4 = @p6");
                sSql.AppendLine("  AND BLCODCAL = @p7");
                sSql.AppendLine("  ");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BLCODEMP, TFTIPFAC, BLTIPPRO, BLCLAVE1, BLCLAVE2, BLCLAVE3, BLCLAVE4, BLCODCAL);
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
        //FOTOS INVENTARIO
        #region
        public DataTable GetFotoInv(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM FOTOINVF WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
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
        public DataTable GetFotoBod(SessionManager oSessionManager, string FBCODEMP, int FBNROFOT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT FOTOIBOD.*,ARNOMBRE,TANOMBRE, ");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP ");
                sSql.AppendLine("               AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 ");
                sSql.AppendLine("               AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP");
                sSql.AppendLine("               AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3");
                sSql.AppendLine("               AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine(" FROM FOTOIBOD WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON (ARCODEMP = FBCODEMP AND ARCODEMP = FBCODEMP AND ARCLAVE1 = FBCLAVE1 AND ARCLAVE2 = FBCLAVE2 AND ARCLAVE3 = FBCLAVE3 AND ARCLAVE4 = FBCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO WITH(NOLOCK) ON (TACODEMP = ARCODEMP AND TATIPPRO = ARTIPPRO) WHERE FBCODEMP=@p0 AND FBNROFOT=@p1");

                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, FBCODEMP, FBNROFOT);
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
        public int InsertFotoBod(SessionManager oSessionManager, string FICODEMP, int FINROFOT, string FIBODEGA, string FIINVINI, string FINMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO FOTOINVF (FICODEMP,FINROFOT,FIBODEGA,FITIPPRO,FICLAV1I,FICLAV1F,FINMUSER,FIESTADO,FICAUSAE,FIFECING,FIFECMOD,FIINVINI)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,'.','.','.',@p4,'AC','.',GETDATE(),GETDATE(),@p3)");

                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FICODEMP, FINROFOT, FIBODEGA, FIINVINI, FINMUSER);

                sSql.Clear();
                sSql.AppendLine("INSERT INTO FOTOIBOD");
                sSql.AppendLine("SELECT BBCODEMP, @p2,BBBODEGA, BBTIPPRO, BBCLAVE1, BBCLAVE2, BBCLAVE3, BBCLAVE4, BBCODCAL, BBCANTID, BBLOCALI, @p3,BBFECING, BBFECMOD ");
                sSql.AppendLine("FROM BALANBOD WITH(NOLOCK)");
                sSql.AppendLine("WHERE BBCODEMP = @p0");
                sSql.AppendLine("AND BBBODEGA = @p1");

                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FICODEMP, FIBODEGA, FINROFOT, FINMUSER);

                sSql.Clear();
                sSql.AppendLine("INSERT INTO FOTOILOT ");
                sSql.AppendLine(" SELECT BLCODEMP,@p2,BLBODEGA,BLTIPPRO,BLCLAVE1,BLCLAVE2,BLCLAVE3,BLCLAVE4,BLCODCAL,BLCDLOTE,BLCANTID,NULL,@p3,GETDATE(),GETDATE()   ");
                sSql.AppendLine("FROM BALANLOT WITH(NOLOCK)");
                sSql.AppendLine("WHERE BLCODEMP = @p0");
                sSql.AppendLine("AND BLBODEGA = @p1");

                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, FICODEMP, FIBODEGA, FINROFOT, FINMUSER);

                return 0;
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
        //CARGUES INVENTARIO
        #region
        public DataTable GetFisicoInv(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM FISICINV WITH(NOLOCK) WHERE 1=1");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
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
        public DataTable GetFisicoBod(SessionManager oSessionManager, string filter)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT  FISICBOD.*,ARNOMBRE,TANOMBRE,'                         ' ZONA,'                     ' BCODIGO, ");
                sSql.AppendLine("CASE WHEN TACTLSE2 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE2 AND ASNIVELC = 2) ELSE ARCLAVE2 END CLAVE2,");
                sSql.AppendLine("CASE WHEN TACTLSE3 ='S' THEN (SELECT ASNOMBRE FROM ARTICSEC WITH(NOLOCK) WHERE ASCODEMP = ARCODEMP AND ASTIPPRO = ARTIPPRO AND ASCLAVEO = ARCLAVE3 AND ASNIVELC = 3) ELSE ARCLAVE3 END CLAVE3");
                sSql.AppendLine("FROM FISICBOD WITH(NOLOCK) ");
                sSql.AppendLine("INNER JOIN ARTICULO WITH(NOLOCK) ON(ARCODEMP = IBCODEMP AND IBTIPPRO = ARTIPPRO AND ARCLAVE1 = IBCLAVE1 AND ARCLAVE2 = IBCLAVE2 AND ARCLAVE3 = IBCLAVE3 AND ARCLAVE4 = IBCLAVE4)");
                sSql.AppendLine("INNER JOIN TBTIPPRO  WITH(NOLOCK) ON(ARCODEMP = TACODEMP AND ARTIPPRO = TATIPPRO)");
                sSql.AppendLine("WHERE 1=1");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine(" AND " + filter);
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
        public int InsertFisicoInv(SessionManager oSessionManager, string IICODEMP, int IINROFOT, int IINROCON, int IIIDPLAN, DateTime IIFECCAR, string IIBODEGA, string IICOMENT, string IIESTADO, string IICAUSAE, string IINMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO FISICINV (IICODEMP, IINROFOT, IINROCON, IIIDPLAN, IIFECCAR, IIBODEGA, IICOMENT, IIESTADO, IICAUSAE, IINMUSER, IIFECING, IIFECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IICODEMP, IINROFOT, IINROCON, IIIDPLAN, IIFECCAR, IIBODEGA, IICOMENT, IIESTADO, IICAUSAE, IINMUSER);                
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
        public int InsertFisicoBod(SessionManager oSessionManager, string IBCODEMP, int IBNROFOT, int IBNROCON, int IBIDPLAN, int IBIDITEM, DateTime IBFECMOV, string IBBODEGA, string IBTIPPRO, string IBCLAVE1, string IBCLAVE2, string IBCLAVE3, string IBCLAVE4, string IBCODCAL, double IBCANTID, string IBESTADO, string IBCAUSAE, string IBNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO FISICBOD (IBCODEMP,IBNROFOT,IBNROCON,IBIDPLAN,IBIDITEM,IBFECMOV,IBBODEGA,IBTIPPRO,IBCLAVE1,IBCLAVE2,IBCLAVE3,IBCLAVE4,IBCODCAL,IBCANTID,IBESTADO,IBCAUSAE,IBNMUSER,IBFECING,IBFECMOD)");
                sSql.AppendLine(" VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");
                
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, IBCODEMP, IBNROFOT, IBNROCON, IBIDPLAN, IBIDITEM, IBFECMOV, IBBODEGA, IBTIPPRO, IBCLAVE1, IBCLAVE2, IBCLAVE3, IBCLAVE4, IBCODCAL, IBCANTID, IBESTADO, IBCAUSAE, IBNMUSER);
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
