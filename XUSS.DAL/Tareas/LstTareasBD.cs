using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;

namespace XUSS.DAL.Tareas
{
    public class LstTareasBD
    {
        public static DataTable GetLstTareas(SessionManager oSessionManager, string Usuario, string Estado,string inTipo, string filter)
        { 
            StringBuilder sSql = new StringBuilder();		
            //sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING ");
            //sSql.AppendLine("  FROM TB_TICKETHD WHERE (TK_RESPONSABLE =@p0 OR TK_PROPIETARIO =@p0) AND TK_ESTADO =@p1 ");

            sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING,TK_FECVEN, BB.TTDESCRI SUB_ZONA,CC.TTDESCRI MIC_ZONA,");
            sSql.AppendLine("CASE");
            sSql.AppendLine("WHEN FLOOR(DATEDIFF(HOUR,TK_FECVEN,GETDATE())/24) > 0 THEN");
            sSql.AppendLine("CONVERT(VARCHAR(10),(DATEDIFF(HOUR,TK_FECVEN,GETDATE())/24)) + ' d '+");
            sSql.AppendLine("CONVERT(VARCHAR(10),DATEDIFF(HOUR,TK_FECVEN,GETDATE())-((DATEDIFF(HOUR,TK_FECVEN,GETDATE())/24)*24)) + ' h'");
		    sSql.AppendLine("ELSE");
		    sSql.AppendLine("'Por Vencer'");
            sSql.AppendLine("END OBS_TK, AA.TTVALORC");
            sSql.AppendLine("  FROM TB_TICKETHD WITH(NOLOCK)");
            sSql.AppendLine(" LEFT OUTER JOIN TB_PROPIEDAHORIZONTAL WITH(NOLOCK) ON (TB_PROPIEDAHORIZONTAL.PH_CODIGO = TB_TICKETHD.PH_CODIGO)");
            sSql.AppendLine(" LEFT OUTER JOIN TERCEROS WITH(NOLOCK) ON (TB_PROPIEDAHORIZONTAL.TRCODTER = TERCEROS.TRCODTER AND TB_PROPIEDAHORIZONTAL.PH_CODEMP = TERCEROS.TRCODEMP)");
            sSql.AppendLine(" LEFT OUTER JOIN TBTABLAS BB WITH(NOLOCK) ON (BB.TTCODTAB ='SUBZON' AND BB.TTCODCLA = TRSUBZONA)");
            sSql.AppendLine(" LEFT OUTER JOIN TBTABLAS CC WITH(NOLOCK) ON (CC.TTCODTAB ='MICZON' AND CC.TTCODCLA = TRMICZONA)");
            if (inTipo=="S")
                sSql.AppendLine("INNER JOIN admi_tusuario ON (usua_usuario = TK_RESPONSABLE)");
            else                                
                sSql.AppendLine("INNER JOIN admi_tusuario ON (usua_usuario = TK_PROPIETARIO)");
            sSql.AppendLine("LEFT OUTER JOIN TBTABLAS AA ON (AA.TTCODEMP = '001' AND AA.TTCODTAB='AREAS' AND AA.TTCODCLA = usua_area)");
            if (inTipo == "S")
                sSql.AppendLine(" WHERE (TK_RESPONSABLE=@p0) AND TK_ESTADO =@p1 "+filter);
            else
                sSql.AppendLine(" WHERE (TK_PROPIETARIO=@p0) AND TK_ESTADO =@p1 "+filter);

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, Usuario, Estado);
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
        public static DataTable GetLstTareasAsig(SessionManager oSessionManager, string Usuario, string Estado)
        {
            StringBuilder sSql = new StringBuilder();
            //sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING ");
            //sSql.AppendLine("  FROM TB_TICKETHD WHERE (TK_RESPONSABLE =@p0 OR TK_PROPIETARIO =@p0) AND TK_ESTADO =@p1 ");

            sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING,TTVALORC ");
            sSql.AppendLine("  FROM TB_TICKETHD WITH(NOLOCK) ");
            sSql.AppendLine("INNER JOIN admi_tusuario ON (usua_usuario = TK_PROPIETARIO)");
            sSql.AppendLine("INNER JOIN TBTABLAS ON (TTCODEMP = '001' AND TTCODTAB='AREAS' AND TTCODCLA = usua_area)");
            sSql.AppendLine("WHERE (TK_RESPONSABLE =@p0) AND TK_ESTADO =@p1 ");

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, Usuario, Estado);
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
        public static DataTable GetUsuarios(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();            

            sSql.AppendLine("SELECT usua_usuario,usua_nombres,usua_email FROM admi_tusuario WHERE usua_estado = 1 ");

            try 
            {
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
        public static DataTable GetUsuariosEmail(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT usua_usuario,(usua_nombres + ' ('+usua_email+')') usuario  FROM admi_tusuario WHERE usua_estado = 1 ");

            try
            {
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
        public static DataTable GetUsuarios(SessionManager oSessionManager,string area)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT usua_usuario,usua_nombres FROM admi_tusuario WHERE usua_estado = 1 AND usua_area =@p0");

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, area);
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
        public static string GetAreaUsuario(SessionManager oSessionManager, string usuario)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT usua_area FROM admi_tusuario WHERE usua_estado = 1 AND usua_usuario =@p0");

            try
            {
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, usuario));
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
        public static DataTable GetAreas(SessionManager oSessionManager)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT TTCODCLA,TTVALORC FROM TBTABLAS WHERE TTCODEMP = '001' AND TTCODTAB='AREAS' ");

            try
            {
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
        public static int InsertTicket(SessionManager oSessionManager,int TK_NUMERO, string TK_RESPONSABLE, string TK_PROPIETARIO, string TK_PRIORIDAD, string TK_ASUNTO,
                                                                      string TK_OBSERVACIONES, string TK_TIPO,int? AT_CODIGO,string TK_ESTADO, DateTime? TK_FECVEN)
        { 
            StringBuilder sSql = new StringBuilder();            
            sSql.AppendLine("INSERT INTO TB_TICKETHD (TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO , TK_OBSERVACIONES, TK_TIPO, TK_ESTADO, AT_CODIGO, TK_FECVEN, TK_FECING) VALUES ");
            sSql.AppendLine("(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,GETDATE())");
            try
            {
                DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO, TK_OBSERVACIONES, TK_TIPO, 
                                        TK_ESTADO, AT_CODIGO, TK_FECVEN);
                return 1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                sSql =null;                
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
                DBAccess.ExecuteNonQuery(oSessionManager, sql.ToString(), CommandType.Text,TTCODCLA);

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
        public static string GetNomUsuarios(SessionManager oSessionManager, string usuario)
        {
            StringBuilder sSql = new StringBuilder();           
            sSql.AppendLine("SELECT usua_nombres FROM admi_tusuario WHERE usua_estado = 1 AND usua_usuario=@p0");

            try
            {
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, usuario));
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
        public static string GetNomArea(SessionManager oSessionManager, string usuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.AppendLine("SELECT TTVALORC FROM admi_tusuario ");
            sSql.AppendLine("INNER JOIN TBTABLAS WHERE TTCODEMP = '001' AND TTCODTAB='AREAS' AND TTCODCLA = usua_area)");
            sSql.AppendLine("WHERE usua_estado = 1 AND usua_usuario=@p0");

            try
            {
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, usuario));
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
        public static int UpdateTicket(SessionManager oSessionManager, int TK_NUMERO, string TK_RESPONSABLE, string TK_PROPIETARIO, string TK_PRIORIDAD, string TK_ASUNTO,
                                                                       string TK_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();                       
            try
            {
                if (TK_ESTADO != "CE")
                {
                    sSql.AppendLine("UPDATE TB_TICKETHD SET TK_RESPONSABLE=@p0, TK_PROPIETARIO=@p1, TK_PRIORIDAD=@p2, TK_ASUNTO=@p3,  ");
                    sSql.AppendLine("                       TK_ESTADO =@p4 WHERE TK_NUMERO=@p5");

                    return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO, TK_ESTADO, TK_NUMERO);
                }
                else
                {
                    sSql.AppendLine("UPDATE TB_TICKETHD SET TK_RESPONSABLE=@p0, TK_PROPIETARIO=@p1, TK_PRIORIDAD=@p2, TK_ASUNTO=@p3, ");
                    sSql.AppendLine("                       TK_ESTADO =@p4, TK_FECFIN=GETDATE() WHERE TK_NUMERO=@p5");

                    return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO, TK_ESTADO, TK_NUMERO);
                }
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
        public static int UpdateTicket(SessionManager oSessionManager, int TK_NUMERO, string TK_RESPONSABLE, string TK_ESTADO)
        {
            StringBuilder sSql = new StringBuilder();
            try
            {                
                    sSql.AppendLine("UPDATE TB_TICKETHD SET TK_RESPONSABLE=@p0, TK_ESTADO =@p1 WHERE TK_NUMERO=@p2");
                    return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TK_RESPONSABLE, TK_ESTADO, TK_NUMERO);                
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
        public static int DeleteTicket(SessionManager oSessionManager, int TK_NUMERO)
        {
            StringBuilder sSql = new StringBuilder();           
            sSql.AppendLine("DELETE FROM TB_TICKETHD WHERE TK_NUMERO=@p0");

            try
            {
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, TK_NUMERO);
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
        public static IDataReader GetLstTarea(SessionManager oSessionManager, int ticket)
        {
            StringBuilder sSql = new StringBuilder();            
            sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING ");
            sSql.AppendLine("  FROM TB_TICKETHD WHERE TK_NUMERO=@p0 ");

            try
            {
                return DBAccess.GetDataReader(oSessionManager, sSql.ToString(), CommandType.Text, ticket);
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
        public static string GetEmailUsuario(SessionManager oSessionManager, string usuario)
        {
            StringBuilder sSql = new StringBuilder();
            
            sSql.AppendLine("SELECT usua_email FROM admi_tusuario WHERE usua_estado = 1 AND usua_usuario=@p0");

            try
            {
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, usuario));
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
        
        public static DataTable GetUsuarioXticket(SessionManager oSessionManager, string usuario)
        {
            StringBuilder sSql = new StringBuilder();            

            sSql.AppendLine("SELECT DISTINCT TK_RESPONSABLE, usua_nombres ");
            sSql.AppendLine("  FROM TB_TICKETHD, admi_tusuario   ");
            sSql.AppendLine(" WHERE TK_RESPONSABLE = usua_usuario");
            sSql.AppendLine("   AND (TK_RESPONSABLE =@p0 OR TK_PROPIETARIO =@p0)");
            sSql.AppendLine("   AND TK_ESTADO = 'AC' ");

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, usuario);
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
        public static string GetObservacion(SessionManager oSessionManager, int TK_NUMERO)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT TOP 1 TD_OBSERVACION FROM TB_TICKETDT WITH(NOLOCK) WHERE TK_NUMERO = @p0 ORDER BY TD_ID DESC");

            try
            {
                return Convert.ToString(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text,TK_NUMERO));
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
        public static DataTable GetLstTareasEjecutadas(SessionManager oSessionManager, string Usuario)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.AppendLine("SELECT TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO ,TK_OBSERVACIONES, TK_ESTADO, TK_FECING,TTVALORC  ");
            sSql.AppendLine("  FROM TB_TICKETHD WITH(NOLOCK) ");
            sSql.AppendLine("INNER JOIN admi_tusuario ON (usua_usuario = TK_PROPIETARIO)");
            sSql.AppendLine("INNER JOIN TBTABLAS ON (TTCODEMP = '001' AND TTCODTAB='AREAS' AND TTCODCLA = usua_area)");
            sSql.AppendLine("WHERE ((TK_RESPONSABLE = @p0) OR (TK_PROPIETARIO = @p0) ) AND TK_ESTADO = 'CE' ");

            try
            {
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, Usuario);
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
        #region
        public static DataTable GetDetalleTicket(SessionManager oSessionManager, int TK_NUMERO)
        {
            StringBuilder sql = new StringBuilder();
            try             
            {
                sql.AppendLine("SELECT TB_TICKETDT.*, usua_nombres FROM TB_TICKETDT, admi_tusuario WHERE TD_RESPONSABLE = usua_usuario AND TK_NUMERO =@p0 ORDER BY TD_FECING DESC");
                return DBAccess.GetDataTable(oSessionManager,sql.ToString(),CommandType.Text,TK_NUMERO);
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
        public static int InsertDetalleTicket(SessionManager oSessionManager,int TD_ID,int TK_NUMERO,string TD_RESPONSABLE, string TD_OBSERVACION, string RUTA)
        {
            StringBuilder sql = new StringBuilder();
            try 
            {
                sql.AppendLine("INSERT INTO TB_TICKETDT (TD_ID,TK_NUMERO,TD_RESPONSABLE,TD_OBSERVACION,TD_FECING,TD_RUTA) VALUES");
                sql.AppendLine("(@p0,@p1,@p2,@p3,GETDATE(),@p4)");

                return DBAccess.ExecuteNonQuery(oSessionManager,sql.ToString(),CommandType.Text,TD_ID,TK_NUMERO,TD_RESPONSABLE,TD_OBSERVACION,RUTA);
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
        #endregion
        #region
        public static DataTable GetAppoiment(SessionManager oSessionManager,string inUsuario, string inFiltro)
        {
            StringBuilder sSql = new StringBuilder();
           
            try
            {
                sSql.AppendLine("SELECT * FROM Appointments WITH(NOLOCK) WHERE usuario=@p0 "+ inFiltro);
                return DBAccess.GetDataTable(oSessionManager, sSql.ToString(), CommandType.Text, inUsuario);
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
        public static int ValidaFechas(SessionManager oSessionManager, string inUsuario, DateTime inFechaIni)
        {
            StringBuilder sSql = new StringBuilder();

            try
            {
                sSql.AppendLine("SELECT count(*) FROM Appointments with(nolock) where usuario_responsable=@p0 and convert(datetime,@p1,101) >= convert(datetime,inicio,101) and convert(datetime,final,101) >= convert(datetime,@p1,101)   ");

                return Convert.ToInt32(DBAccess.GetScalar(oSessionManager, sSql.ToString(), CommandType.Text, inUsuario, inFechaIni));
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
        public static int InsertAppoiment(SessionManager oSessionManager, string descripcion,DateTime inicio,DateTime final,int RoomID,string usuario,string RecurrenceRule,int? RecurrenceParentID,int? tipo,string usuario_responsable,int TK_NUMERO,int? TRCODTER,int? PH_CODIGO,string SERVICIO)
        {
            StringBuilder sSql = new StringBuilder();

            try
            {
                sSql.AppendLine("INSERT INTO Appointments (descripcion,inicio,final,RoomID,usuario,RecurrenceRule,RecurrenceParentID,tipo,usuario_responsable,TK_NUMERO,TRCODTER,PH_CODIGO,SERVICIO) VALUES (@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12) ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, descripcion, inicio, final, RoomID, usuario, RecurrenceRule, RecurrenceParentID,tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO, SERVICIO);
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
        public static int UpdateAppoiment(SessionManager oSessionManager, int id, string descripcion, DateTime inicio, DateTime final, int RoomID, string usuario, string RecurrenceRule, int? RecurrenceParentID, int? tipo, string usuario_responsable, int? TK_NUMERO, int? TRCODTER, int? PH_CODIGO, string SERVICIO)
        {
            StringBuilder sSql = new StringBuilder();

            try
            {
                sSql.AppendLine("UPDATE Appointments SET descripcion=@p0,inicio=@p1,final=@p2,RoomID=@p3,usuario=@p4,RecurrenceRule=@p5,RecurrenceParentID=@p6,tipo=@p7,usuario_responsable=@p8,TK_NUMERO=@p9,TRCODTER=@p10, PH_CODIGO=@p11, SERVICIO=@p12 WHERE id=@p13 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, descripcion, inicio, final, RoomID, usuario, RecurrenceRule, RecurrenceParentID,tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO, SERVICIO, id);
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
        public static int DeleteAppoiment(SessionManager oSessionManager, int id)
        {
            StringBuilder sSql = new StringBuilder();

            try
            {
                sSql.AppendLine("DELETE FROM Appointments WHERE id=@p0 ");
                return DBAccess.ExecuteNonQuery(oSessionManager, sSql.ToString(), CommandType.Text, id);
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
