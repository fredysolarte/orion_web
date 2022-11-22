using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XUSS.DAL.Tareas;
using System.ComponentModel;
using System.Web;
using DataAccess;
using XUSS.BLL.Mail;
using XUSS.DAL.Terceros;

namespace XUSS.BLL.Tareas
{
    [DataObject(true)]
    public class LstTareasBL
    {
        public DataTable GetLstTareas(string connection, string Usuario, string Estado, string inTipo, string filter)
        { 
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetLstTareas(oSessionManager, HttpContext.Current.Session["UserLogon"].ToString(), Estado,inTipo,filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetLstTareasAsig(string connection, string Usuario, string Estado)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetLstTareasAsig(oSessionManager, HttpContext.Current.Session["UserLogon"].ToString(), Estado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetLstTareasEjecutadas(string connection, string Usuario, string Estado)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetLstTareasEjecutadas(oSessionManager, HttpContext.Current.Session["UserLogon"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetUsuarios(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetUsuarios(oSessionManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetUsuariosEmail(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetUsuariosEmail(oSessionManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetUsuarios(string connection, string area)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetUsuarios(oSessionManager,area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static string GetAreaUsuario(string connection, string usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetAreaUsuario(oSessionManager, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public DataTable GetAreas(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetAreas(oSessionManager);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static int InsertTicket(string connection, string TK_RESPONSABLE, string TK_PROPIETARIO, string TK_PRIORIDAD, string TK_ASUNTO ,
                                                          string TK_OBSERVACIONES, string TK_TIPO, DateTime? TK_FECVEN, string RUTA)
        {            
            SessionManager oSessionManager = new SessionManager(connection);

            int TK_NUMERO = 0;
            int TD_NUMERO = 0;
            try
            {
                oSessionManager.BeginTransaction();
                TK_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NROTCK");
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                LstTareasBD.InsertTicket(oSessionManager, TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO, TK_OBSERVACIONES, TK_TIPO, null, "AC", TK_FECVEN);
                //LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO,TK_NUMERO, TK_RESPONSABLE, TK_OBSERVACIONES);  
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, TK_PROPIETARIO, TK_OBSERVACIONES, RUTA);  
                oSessionManager.CommitTranstaction();
                return TK_NUMERO;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static string GetNomUsuarios(string connection, string usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetNomUsuarios(oSessionManager,usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static int UpdateTicket(string connection, int TK_NUMERO, string TK_RESPONSABLE, string TK_PROPIETARIO, string TK_PRIORIDAD, string TK_ASUNTO, string TK_ESTADO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try 
            {
                return LstTareasBD.UpdateTicket(oSessionManager, TK_NUMERO, TK_RESPONSABLE, TK_PROPIETARIO, TK_PRIORIDAD, TK_ASUNTO, TK_ESTADO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static int DeleteTicket(string connection, int TK_NUMERO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.DeleteTicket(oSessionManager, TK_NUMERO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static IDataReader GetlstTarea(string connection, int ticket)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetLstTarea(oSessionManager, ticket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static string GetEmailUsuario(string connection, string usuario)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetEmailUsuario(oSessionManager, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static DataTable GetUsuarioXticket(string connection)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetUsuarioXticket(oSessionManager, HttpContext.Current.Session["UserLogon"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static string GetObservacion(string connection, int TK_NUMERO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetObservacion(oSessionManager, TK_NUMERO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        #region
        public static DataTable GetDetalleTicket(string connection, int TK_NUMERO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetDetalleTicket(oSessionManager, TK_NUMERO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static int InsertDetalleTicket(int TK_NUMERO, string usuario_responsable, string TD_OBSERVACION, string RUTA, 
            string descripcion, DateTime inicio, DateTime final, int RoomID, string usuario, string RecurrenceRule, int? RecurrenceParentID, int? tipo, int? TRCODTER, int? PH_CODIGO, string SERVICIO)
        {
            int TD_NUMERO = 0;
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.BeginTransaction();
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, usuario_responsable, TD_OBSERVACION, RUTA);
                LstTareasBD.UpdateTicket(oSessionManager, TK_NUMERO, usuario_responsable, "AC");
                LstTareasBD.InsertAppoiment(oSessionManager, descripcion, inicio, final, RoomID, usuario_responsable, RecurrenceRule, RecurrenceParentID, tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO, SERVICIO);                
                //LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, usuario_responsable, descripcion, null);

                //1 desmomte D 2 Servicio Tecnico 3 Desmonte Remodelacion 4 Instalacion Remodelacion
                if (SERVICIO == "1")
                    TercerosBD.InsertTranDesmonte(oSessionManager, PH_CODIGO, TK_NUMERO, "AC");
                else
                    TercerosBD.InsertTranServicio(oSessionManager, "001", PH_CODIGO, TK_NUMERO, usuario, "AC", ".");

                oSessionManager.CommitTranstaction();
                //Enviar Email

                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static int ValidaFechas(string inUsuario, DateTime inFechaIni)
        {
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                return LstTareasBD.ValidaFechas(oSessionManager, inUsuario, inFechaIni);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }

        }
        public static int InsertDetalleTicket(int TK_NUMERO, string TD_RESPONSABLE, string TD_OBSERVACION, string RUTA)
        {
            int TD_NUMERO = 0;
            SessionManager oSessionManager = new SessionManager("");
            try
            {
                oSessionManager.BeginTransaction();
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                LstTareasBD.InsertDetalleTicket(oSessionManager,TD_NUMERO,TK_NUMERO,TD_RESPONSABLE,TD_OBSERVACION,RUTA);                
                oSessionManager.CommitTranstaction();
                //Enviar Email

                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public static int InsertDetalleTicketMail(int TK_NUMERO, string TD_RESPONSABLE, string TD_USUARIO,string TD_OBSERVACION, string RUTA, string MAIL_SERVER, string MAIL_USER, string MAIL_PASSWORD,string MAIL_FROM, string inAsunto, string inPropietario, string inEstado)
        {
            int TD_NUMERO = 0;
            SessionManager oSessionManager = new SessionManager(null);
            try
            {
                oSessionManager.BeginTransaction();
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, TD_USUARIO, TD_OBSERVACION, RUTA);

                LstTareasBD.UpdateTicket(oSessionManager, TK_NUMERO, TD_RESPONSABLE, inEstado);
                //Enviar Email

                string lbody = string.Format(@"<table>
                                                           <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                                            <tr><td>Le Acaba de dar Respuesta al Ticket <b></b>.</br></br> </td> <tr>                                           
                                                            <tr><td>Asunto: {1}</br></br></td></tr>
                                                            <tr><td>Observaciones:{2}</td></tr>
                                                            </table> 
                                                            <table>
                                                            </tr></td>
                                                            No responda a este email ya que es Sólo de envío.
                                                            </td></tr>
                                                            </table>", LstTareasBD.GetNomUsuarios(oSessionManager, TD_RESPONSABLE), inAsunto, TD_OBSERVACION);

                UMail lMail = new UMail();
                DataTable dtTO = UMail.NewToDataTable();
                dtTO.Rows.Add(LstTareasBL.GetEmailUsuario("", TD_RESPONSABLE), LstTareasBD.GetNomUsuarios(oSessionManager, TD_RESPONSABLE), string.Empty);
                lMail.SendMail(MAIL_SERVER, MAIL_USER, MAIL_PASSWORD, MAIL_FROM, MAIL_FROM, dtTO, "[" + Convert.ToString(TK_NUMERO) + "][XUSS] Respuesta", lbody, null, null);
                if (inPropietario != TD_RESPONSABLE)
                {
                    string lbody_a = string.Format(@"<table>
                                                           <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                                            <tr><td>Le Acaba de Re-Asignar un Ticket <b></b>.</br></br> </td> <tr>                                           
                                                            <tr><td>Asunto: {1}</br></br></td></tr>
                                                            <tr><td>Observaciones:{2}</td></tr>
                                                            </table> 
                                                            <table>
                                                            </tr></td>
                                                            No responda a este email ya que es Sólo de envío.
                                                            </td></tr>
                                                            </table>", LstTareasBD.GetNomUsuarios(oSessionManager, TD_RESPONSABLE), inAsunto, TD_OBSERVACION);

                    UMail lMail_a = new UMail();
                    DataTable dtTO_a = UMail.NewToDataTable();
                    dtTO_a.Rows.Add(LstTareasBL.GetEmailUsuario("", TD_RESPONSABLE), LstTareasBD.GetNomUsuarios(oSessionManager, TD_RESPONSABLE), string.Empty);
                    lMail_a.SendMail(MAIL_SERVER, MAIL_USER, MAIL_PASSWORD, MAIL_FROM, MAIL_FROM, dtTO, "[" + Convert.ToString(TK_NUMERO) + "][XUSS] Respuesta", lbody, null, null);
                }

                oSessionManager.CommitTranstaction();
                return 1;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        #endregion
        #region
        public DataTable GetAppoiment(string connection, DateTime RangeStart, DateTime RangeEnd, string inUsuario,string inFiltro)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.GetAppoiment(oSessionManager, inUsuario, inFiltro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int InsertAppoiment(string connection, string descripcion, DateTime inicio, DateTime final, int RoomID, string usuario, string RecurrenceRule, int? RecurrenceParentID, int? tipo,string usuario_responsable, int? TRCODTER, int? PH_CODIGO,string SERVICIO)
        {
            int TK_NUMERO = 0, TD_NUMERO = 0,ln_tipo=1;
            string lc_subject = "",lc_descripcion="";
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                oSessionManager.BeginTransaction();
                TK_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NROTCK");
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                //LstTareasBD.InsertAppoiment(oSessionManager, descripcion, inicio, final, RoomID, usuario, RecurrenceRule, RecurrenceParentID, tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO);
                
                if (SERVICIO == "1")
                {
                    ln_tipo = 1;
                    lc_subject = "Desmonte Definitivo ";
                }
                if (SERVICIO == "2")
                {
                    ln_tipo = 2;
                    lc_subject = "Servicio Tecnico ";
                }
                if (SERVICIO == "3")
                {
                    ln_tipo = 3;
                    lc_subject = "Desmonte Remodelacion ";
                }
                if (SERVICIO == "4")
                {
                    ln_tipo = 4;
                    lc_subject = "Instalacion Remodelacion ";
                }
                if (SERVICIO == "5")
                {
                    ln_tipo = 5;
                    lc_subject = "Inspeccion";
                }


                lc_descripcion = lc_subject;
                foreach (DataRow rw in TercerosBD.GetTerceros(oSessionManager, " TRCODTER=" + Convert.ToString(TRCODTER), 0, 0).Rows)
                {
                    lc_subject += Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]);
                    lc_descripcion += Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"])+ " Direccion: " + Convert.ToString(rw["TRDIRECC"]);
                }

                foreach (DataRow rw in TercerosBD.GetPropiedadHorizontal(oSessionManager, "001", Convert.ToInt32(TRCODTER)).Rows)
                {
                    if (Convert.ToInt32(PH_CODIGO) == Convert.ToInt32(rw["PH_CODIGO"]))
                    {
                        lc_subject += Convert.ToString(rw["PH_EDIFICIO"]) + " " + Convert.ToString(rw["PH_ESCALERA"]);
                        //lc_descripcion += " Datos Contacto :" + Convert.ToString(rw["CLIENTE"]) + " Nro Telefono:" + Convert.ToString(rw["TRNROTEL"]);
                    }
                }

                LstTareasBD.InsertAppoiment(oSessionManager, lc_descripcion, inicio, final, RoomID, usuario_responsable, RecurrenceRule, RecurrenceParentID, ln_tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO, SERVICIO);
                LstTareasBD.InsertTicket(oSessionManager, TK_NUMERO, usuario_responsable,usuario,  "03", lc_subject, descripcion + lc_descripcion, Convert.ToString(ln_tipo) ,null,"AC", null);
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, usuario_responsable, descripcion, null);

                //1 desmomte D 2 Servicio Tecnico 3 Desmonte Remodelacion 4 Instalacion Remodelacion
                if (SERVICIO == "1")
                    TercerosBD.InsertTranDesmonte(oSessionManager, PH_CODIGO, TK_NUMERO, "AC");
                else
                    TercerosBD.InsertTranServicio(oSessionManager, "001", PH_CODIGO, TK_NUMERO, usuario, "AC", ".");

                    oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int UpdateAppoiment(string connection, int original_id, string descripcion, DateTime inicio, DateTime final, int RoomID, string usuario, string RecurrenceRule, int? RecurrenceParentID,int? tipo, string usuario_responsable, int TK_NUMERO ,int? TRCODTER, int? PH_CODIGO, string SERVICIO)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            int TD_NUMERO = 0;
            try
            {
                oSessionManager.BeginTransaction();
                TD_NUMERO = LstTareasBD.GeneraConsecutivo(oSessionManager, "NRODTCK");
                LstTareasBD.UpdateTicket(oSessionManager, TK_NUMERO, usuario_responsable, "AC");
                LstTareasBD.InsertDetalleTicket(oSessionManager, TD_NUMERO, TK_NUMERO, usuario_responsable, "Reasigna Usuario", null);
                LstTareasBD.UpdateAppoiment(oSessionManager, original_id, descripcion, inicio, final, RoomID, usuario_responsable, RecurrenceRule, RecurrenceParentID, tipo, usuario_responsable, TK_NUMERO, TRCODTER, PH_CODIGO, SERVICIO);
                oSessionManager.CommitTranstaction();
                return 0;
            }
            catch (Exception ex)
            {
                oSessionManager.RollBackTransaction();
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        public int DeleteAppoiment(string connection, int id, int original_id)
        {
            SessionManager oSessionManager = new SessionManager(connection);
            try
            {
                return LstTareasBD.DeleteAppoiment(oSessionManager, original_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oSessionManager = null;
            }
        }
        #endregion
    }
}
