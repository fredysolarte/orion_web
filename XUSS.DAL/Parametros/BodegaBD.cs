using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Parametros
{
    public class BodegaBD
    {
        public static DataTable GetBodegas(SessionManager oSessionManager, string filter, int startRowIndex, int maximumRows)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT * FROM TBBODEGA WITH(NOLOCK) WHERE 1=1");
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
        public static DataTable GetLineaXBodega(SessionManager oSessionManager, string filter ,string CODEMP,string ABBODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT TBARTBOD.*,TATIPPRO,TANOMBRE");
                sSql.AppendLine("FROM TBTIPPRO  WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TBARTBOD WITH(NOLOCK) ON (TACODEMP = ABCODEMP AND TATIPPRO = ABTIPPRO AND ABBODEGA=@p1 AND ABESTADO ='AC')");
                sSql.AppendLine(" WHERE TACODEMP=@p0");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, CODEMP,ABBODEGA);
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
        public static DataTable GetUsuariosXBodega(SessionManager oSessionManager, string filter, string BUBODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT usua_usuario,usua_nombres,TBBODUSR.*");
                sSql.AppendLine("FROM admi_tusuario WITH(NOLOCK)");
                sSql.AppendLine("LEFT OUTER JOIN TBBODUSR WITH(NOLOCK) on (admi_tusuario.usua_usuario = TBBODUSR.BUCDUSER AND BUBODEGA =@p0)");

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sSql.AppendLine("AND " + filter);
                }
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, BUBODEGA);
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
        public static DataTable GetBodegasXUsuario(SessionManager oSessionManager, string usua_usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT BDBODEGA,BDNOMBRE ");
                sSql.AppendLine("FROM TBBODUSR WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA WITH(NOLOCK) on (BDBODEGA = TBBODUSR.BUBODEGA)");
                sSql.AppendLine("WHERE BUCDUSER =@p0");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usua_usuario);
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
        public static DataTable GetBodegasXUsuarioDef(SessionManager oSessionManager, string usua_usuario)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("SELECT BDBODEGA,BDNOMBRE ");
                sSql.AppendLine("FROM TBBODUSR WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBBODEGA WITH(NOLOCK) on (BDBODEGA = TBBODUSR.BUBODEGA)");
                sSql.AppendLine("INNER JOIN admi_tusuario WITH(NOLOCK) ON (usua_usuario = BUCDUSER)");
                sSql.AppendLine("WHERE BUCDUSER = @p0");
                sSql.AppendLine("AND BDBODEGA IN (case when usua_administrador = 1 then BDBODEGA ELSE  ");
                sSql.AppendLine("(SELECT TOP 1 TFBODEGA ");
                sSql.AppendLine("FROM TBTIPFAC WITH(NOLOCK)");
                sSql.AppendLine("INNER JOIN TBTFACUSR WITH(NOLOCK) ON (TFCODEMP = FUCODEMP AND TFTIPFAC = FUTIPFAC)");
                sSql.AppendLine("WHERE FUUSUARIO=@p0) END )");
                
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usua_usuario);
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
        public static int InsertBodega(SessionManager oSessionManager,string BDCODEMP,string BDBODEGA, string BDNOMBRE,string BDDIRECC,string BDRESPON,
                                                                      string BDBODCON,DateTime? BDCIERRE,string BDCONSIG,string BDESTADO,string BDCAUSAE,
                                                                      string BDNMUSER,string BDMNCAJA,string BDBODSOC,int? BDCAJADF,string BDCENCOS,
                                                                      string BDPORMAX,string BDCAJAOP,string BDTELEFO,string BDALMACE, string BDPAIS, string CDCIUDAD, string BDCDTEC1, string BDDTTEC1)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("INSERT INTO TBBODEGA (BDCODEMP,BDBODEGA,BDNOMBRE,BDDIRECC,BDRESPON,BDBODCON,BDCIERRE,BDCONSIG,BDESTADO,BDCAUSAE,BDNMUSER,BDMNCAJA,BDBODSOC,BDCAJADF,BDCENCOS,BDPORMAX,BDCAJAOP,BDTELEFO,BDALMACE,BDPAIS, CDCIUDAD,BDCDTEC1, BDDTTEC1,BDFECING,BDFECMOD)");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18,@p19,@p20,@p21,@p22,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BDCODEMP, BDBODEGA, BDNOMBRE, BDDIRECC, BDRESPON, BDBODCON, BDCIERRE, BDCONSIG, BDESTADO, BDCAUSAE, BDNMUSER, BDMNCAJA, BDBODSOC, BDCAJADF, 
                    BDCENCOS, BDPORMAX, BDCAJAOP, BDTELEFO, BDALMACE, BDPAIS, CDCIUDAD, BDCDTEC1, BDDTTEC1);
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
        public static int UpdateBodega(SessionManager oSessionManager, string BDCODEMP, string BDBODEGA, string BDNOMBRE, string BDDIRECC, string BDRESPON,
                                                                      string BDBODCON, DateTime? BDCIERRE, string BDCONSIG, string BDESTADO, string BDCAUSAE,
                                                                      string BDNMUSER, string BDMNCAJA, string BDBODSOC, int? BDCAJADF, string BDCENCOS,
                                                                      string BDPORMAX, string BDCAJAOP, string BDTELEFO, string BDALMACE, string BDPAIS, string CDCIUDAD, string BDCDTEC1, string BDDTTEC1)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBBODEGA SET BDNOMBRE=@p2,BDDIRECC=@p3,BDRESPON=@p4,BDBODCON=@p5,BDCIERRE=@p6,BDCONSIG=@p7,BDESTADO=@p8,BDCAUSAE=@p9,BDNMUSER=@p10,BDMNCAJA=@p11,BDBODSOC=@p12,BDCAJADF=@p13,BDCENCOS=@p14,BDPORMAX=@p15,BDCAJAOP=@p16,BDTELEFO=@p17,BDALMACE=@p18,BDPAIS=@p19,CDCIUDAD=@p20,BDCDTEC1=@p21,BDDTTEC1=@p22,BDFECMOD=GETDATE()");
                sSql.AppendLine(" WHERE BDCODEMP = @p0 AND BDBODEGA = @p1 ");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BDCODEMP, BDBODEGA, BDNOMBRE, BDDIRECC, BDRESPON, BDBODCON, BDCIERRE, BDCONSIG, BDESTADO, BDCAUSAE, BDNMUSER, BDMNCAJA, BDBODSOC, BDCAJADF, BDCENCOS, BDPORMAX, BDCAJAOP, BDTELEFO, BDALMACE, BDPAIS, CDCIUDAD, BDCDTEC1, BDDTTEC1);
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
        public static int ExisteLineaXBodega(SessionManager oSessionManager, string ABCODEMP, string ABBODEGA, string ABTIPPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM TBARTBOD WITH(NOLOCK) WHERE ABCODEMP =@p0 AND ABBODEGA =@p1 AND ABTIPPRO =@p2");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, ABCODEMP, ABBODEGA, ABTIPPRO));
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
        public static int DeleteLineaXBodega(SessionManager oSessionManager, string ABCODEMP, string ABBODEGA, string ABTIPPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBARTBOD WHERE ABCODEMP =@p0 AND ABBODEGA =@p1 AND ABTIPPRO =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ABCODEMP, ABBODEGA, ABTIPPRO);
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
        public static int UpdateLineaXBodega(SessionManager oSessionManager, string ABCODEMP, string ABBODEGA, string ABTIPPRO,string ABNOMBRE,
                                             string ABMNLOTE,string ABMNELEM,string ABMNCONT,string ABMNBONI,string ABMNNREL,string ABFRMUBI,string ABESTADO,
                                             string ABCAUSAE, string ABNMUSER, string ABCTACON, double ABENTLIM, double ABSALLIM, string ABELEMUAT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBARTBOD SET ABNOMBRE=@p3,ABMNLOTE=@p4,ABMNELEM=@p5,ABMNCONT=@p6,ABMNBONI=@p7,ABMNNREL=@p8,ABFRMUBI=@p9,ABESTADO=@p10,");
                sSql.AppendLine("                    ABCAUSAE=@p11,ABNMUSER=@p12,ABCTACON=@p13,ABENTLIM=@p14,ABSALLIM=@p15,ABELEMUAT=@p16");
                sSql.AppendLine("WHERE ABCODEMP =@p0 AND ABBODEGA =@p1 AND ABTIPPRO =@p2");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text,ABCODEMP,  ABBODEGA,  ABTIPPRO, ABNOMBRE,
                                              ABMNLOTE, ABMNELEM, ABMNCONT, ABMNBONI, ABMNNREL, ABFRMUBI, ABESTADO,
                                              ABCAUSAE, ABNMUSER, ABCTACON, ABENTLIM, ABSALLIM, ABELEMUAT);
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
        public static int UpdateLineaXBodega(SessionManager oSessionManager, string ABCODEMP, string ABBODEGA, string ABTIPPRO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("UPDATE TBARTBOD SET ABESTADO='AN' WHERE ABCODEMP =@p0 AND ABBODEGA =@p1");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ABCODEMP, ABBODEGA);
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
        public static int InsertLineaXBodega(SessionManager oSessionManager, string ABCODEMP, string ABBODEGA, string ABTIPPRO, string ABNOMBRE,string ABMNLOTE, string ABMNELEM,
                                            string ABMNCONT,string ABMNBONI,string ABMNNREL,string ABFRMUBI,string ABESTADO,string ABCAUSAE,string ABNMUSER,
                                            string ABCTACON,double? ABENTLIM,double? ABSALLIM,string ABELEMUAT)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBARTBOD (ABCODEMP,ABBODEGA,ABTIPPRO,ABNOMBRE,ABMNLOTE,ABMNELEM,ABMNCONT,ABMNBONI,ABMNNREL,ABFRMUBI,ABESTADO,ABCAUSAE,ABNMUSER,ABCTACON,ABENTLIM,ABSALLIM,ABELEMUAT,ABFECING,ABFECMOD)");
                sSql.AppendLine(" VALUES(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, ABCODEMP, ABBODEGA, ABTIPPRO, ABNOMBRE, ABMNLOTE, ABMNELEM, ABMNCONT, ABMNBONI, ABMNNREL, ABFRMUBI, ABESTADO, ABCAUSAE, 
                                                ABNMUSER, ABCTACON, ABENTLIM, ABSALLIM, ABELEMUAT);
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
        public static int ExisteUsuarioXBodega(SessionManager oSessionManager, string BUCODEMP, string BUBODEGA, string BUCDUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try {
                sSql.AppendLine("SELECT COUNT(*) FROM TBBODUSR WITH(NOLOCK) WHERE BUCODEMP=@p0 AND BUBODEGA =@p1 AND BUCDUSER =@p2");
                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, BUCODEMP, BUBODEGA, BUCDUSER));
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
        public static int DeleteUsuarioXBodega(SessionManager oSessionManager, string BUCODEMP, string BUBODEGA, string BUCDUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBBODUSR WHERE BUCODEMP=@p0 AND BUBODEGA =@p1 AND BUCDUSER =@p2");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BUCODEMP, BUBODEGA, BUCDUSER);
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
        public static int DeleteUsuarioXBodega(SessionManager oSessionManager, string BUCODEMP, string BUBODEGA)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("DELETE FROM TBBODUSR WHERE BUCODEMP=@p0 AND BUBODEGA =@p1 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BUCODEMP, BUBODEGA);
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
        public static int InsertUsuarioXBodega(SessionManager oSessionManager, string BUCODEMP, string BUBODEGA, string BUCDUSER,string BUESTADO,string BUCAUSAE,string BUNMUSER)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {
                sSql.AppendLine("INSERT INTO TBBODUSR (BUCODEMP,BUBODEGA,BUCDUSER,BUESTADO,BUCAUSAE,BUNMUSER,BUFECING,BUFECMOD)");
                sSql.AppendLine("VALUES (@p0,@p1,@p2,@p3,@p4,@p5,GETDATE(),GETDATE())");

                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, BUCODEMP, BUBODEGA, BUCDUSER, BUESTADO, BUCAUSAE, BUNMUSER);
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
