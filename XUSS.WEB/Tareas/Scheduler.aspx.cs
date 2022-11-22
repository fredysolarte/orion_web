using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Tareas;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Tareas
{
    class AppointmentInfo
    {        
        private readonly string _id;
        private string _subject;
        private DateTime _start;
        private DateTime _end;
        private string _recurrenceRule;
        private string _recurrenceParentId;
        private string _reminder;
        private int? _userID;

        
        public string ID
        {
            get
            {
                return _id;
            }
        }

        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public string RecurrenceRule
        {
            get
            {
                return _recurrenceRule;
            }
            set
            {
                _recurrenceRule = value;
            }
        }

        public string RecurrenceParentID
        {
            get
            {
                return _recurrenceParentId;
            }
            set
            {
                _recurrenceParentId = value;
            }
        }

        public int? UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        public string Reminder
        {
            get
            {
                return _reminder;
            }
            set
            {
                _reminder = value;
            }
        }

        private AppointmentInfo()
        {
            _id = Guid.NewGuid().ToString();
        }

        public AppointmentInfo(string subject, DateTime start, DateTime end,
             string recurrenceRule, string recurrenceParentID, string reminder, int? userID)
            : this()
        {
            _subject = subject;
            _start = start;
            _end = end;
            _recurrenceRule = recurrenceRule;
            _recurrenceParentId = recurrenceParentID;
            _reminder = reminder;
            _userID = userID;
        }

        public AppointmentInfo(Appointment source)
            : this()
        {
            CopyInfo(source);
        }

        public void CopyInfo(Appointment source)
        {
            Subject = source.Subject;
            Start = source.Start;
            End = source.End;
            RecurrenceRule = source.RecurrenceRule;
            if (source.RecurrenceParentID != null)
            {
                RecurrenceParentID = source.RecurrenceParentID.ToString();
            }

            if (!String.IsNullOrEmpty(Reminder))
            {
                Reminder = source.Reminders[0].ToString();
            }

            Resource user = source.Resources.GetResourceByType("User");
            if (user != null)
            {
                UserID = (int?)user.Key;
            }
            else
            {
                UserID = null;
            }
        }
    }
    public partial class Scheduler : System.Web.UI.Page
    {
        private bool lc_indicador
        {
            set { ViewState["tbCuentas"] = value; }
            get { return Convert.ToBoolean(ViewState["tbCuentas"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lc_indicador = false;
                rc_responsable.SelectedValue = Convert.ToString(Session["UserLogon"]);
                RadScheduler1.DataBind();
                //obj_usuarios.DataBind();
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                InitializeResources();
            }
        }
        private void InitializeResources()
        {
            ResourceType resType = new ResourceType("Type");
            resType.ForeignKeyField = "tipo";

            RadScheduler1.ResourceTypes.Add(resType);
            RadScheduler1.Resources.Add(new Resource("Type", 1, "Work"));
            RadScheduler1.Resources.Add(new Resource("Type", 2, "Marketing"));            
        }

        protected void RadScheduler1_AppointmentCommand(object sender, AppointmentCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                obj_consulta.InsertParameters["usuario_responsable"].DefaultValue = (e.Container.FindControl("rb_responsable") as RadComboBox).SelectedValue;
                obj_consulta.InsertParameters["TRCODTER"].DefaultValue = (e.Container.FindControl("rc_tercero") as RadComboBox).SelectedValue;
                obj_consulta.InsertParameters["PH_CODIGO"].DefaultValue = (e.Container.FindControl("rc_propiedad") as RadComboBox).SelectedValue;
                obj_consulta.InsertParameters["SERVICIO"].DefaultValue = (e.Container.FindControl("rc_tservicio") as RadComboBox).SelectedValue;
            }
            if (e.CommandName == "Update")
            {
                obj_consulta.UpdateParameters["usuario_responsable"].DefaultValue = (e.Container.FindControl("rb_responsable") as RadComboBox).SelectedValue;
                obj_consulta.UpdateParameters["TRCODTER"].DefaultValue = (e.Container.FindControl("rc_tercero") as RadComboBox).SelectedValue;
                obj_consulta.UpdateParameters["PH_CODIGO"].DefaultValue = (e.Container.FindControl("rc_propiedad") as RadComboBox).SelectedValue;
                obj_consulta.UpdateParameters["SERVICIO"].DefaultValue = (e.Container.FindControl("rc_tservicio") as RadComboBox).SelectedValue;
                obj_consulta.UpdateParameters["TK_NUMERO"].DefaultValue = (e.Container.FindControl("txt_nroticket") as RadTextBox).Text;
            }
            rc_responsable.Enabled = true;
            //lc_indicador = false;
        }

        protected void rc_tercero_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TercerosBL obj = new TercerosBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                (((RadComboBox)sender).Parent.FindControl("rc_propiedad") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                (((RadComboBox)sender).Parent.FindControl("rc_propiedad") as RadComboBox).Items.Add(item);
                foreach (DataRow rw in obj.GetPropiedadHorizontal(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadComboBox).SelectedValue)).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["PH_CODIGO"]);
                    itemi.Text = Convert.ToString(rw["PH_EDIFICIO"]) + "-" + Convert.ToString(rw["PH_ESCALERA"]) + "-" + Convert.ToString(rw["MECDELEM"]);
                    (((RadComboBox)sender).Parent.FindControl("rc_propiedad") as RadComboBox).Items.Add(itemi);
                    itemi = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj = null;
                item = null;
            }
        }

        protected void rc_responsable_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            obj_consulta.SelectParameters["inUsuario"].DefaultValue = Convert.ToString((sender as RadComboBox).SelectedValue);
            //obj_usuarios.DataBind();
            RadScheduler1.DataBind();
        }       
        protected void RadScheduler1_FormCreated(object sender, SchedulerFormCreatedEventArgs e)
        {
            if (e.Container.Mode == SchedulerFormMode.Insert || e.Container.Mode == SchedulerFormMode.AdvancedInsert)
            {
                lc_indicador = true;
                rc_responsable.Enabled = false;
                (e.Container.FindControl("rb_responsable") as RadComboBox).SelectedValue = rc_responsable.SelectedValue;
                //(e.Container.FindControl("rb_responsable") as RadComboBox).SelectedValue = e.Appointment.
            }
            if (e.Container.Mode == SchedulerFormMode.AdvancedEdit || e.Container.Mode == SchedulerFormMode.Edit)
            {
                LstTareasBL Obj = new LstTareasBL();
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item = new RadComboBoxItem();
                try
                {
                    rc_responsable.Enabled = false;
                    foreach (DataRow rw in Obj.GetAppoiment(null, System.DateTime.Today, System.DateTime.Today, Convert.ToString(rc_responsable.SelectedValue)," AND id="+e.Appointment.ID).Rows)
                    {
                        (e.Container.FindControl("rb_responsable") as RadComboBox).SelectedValue = rc_responsable.SelectedValue;
                        (e.Container.FindControl("rc_tservicio") as RadComboBox).SelectedValue = Convert.ToString(rw["SERVICIO"]);
                        (e.Container.FindControl("rc_tercero") as RadComboBox).SelectedValue = Convert.ToString(rw["TRCODTER"]);

                        item.Value = "";
                        item.Text = "Seleccionar";
                        (e.Container.FindControl("rc_propiedad") as RadComboBox).Items.Add(item);
                        foreach (DataRow rt in obj.GetPropiedadHorizontal(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rw["TRCODTER"])).Rows)
                        {
                            RadComboBoxItem itemi = new RadComboBoxItem();
                            itemi.Value = Convert.ToString(rt["PH_CODIGO"]);
                            itemi.Text = Convert.ToString(rt["PH_EDIFICIO"]) + "-" + Convert.ToString(rt["PH_ESCALERA"]) + "-" + Convert.ToString(rt["MECDELEM"]);
                            (e.Container.FindControl("rc_propiedad") as RadComboBox).Items.Add(itemi);
                            itemi = null;
                        }

                        (e.Container.FindControl("rc_propiedad") as RadComboBox).SelectedValue = Convert.ToString(rw["PH_CODIGO"]);
                        (e.Container.FindControl("txt_nroticket") as RadTextBox).Text = Convert.ToString(rw["TK_NUMERO"]);

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Obj = null;
                    obj = null;
                    item = null;
                }
            }
        }

        protected void RadScheduler1_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            LstTareasBL Obj = new LstTareasBL(); 
            try {
                if (obj_consulta.UpdateParameters["usuario_responsable"].DefaultValue == "" || obj_consulta.UpdateParameters["usuario_responsable"].DefaultValue == null)
                {
                    foreach (DataRow rw in Obj.GetAppoiment(null, System.DateTime.Today, System.DateTime.Today, Convert.ToString(rc_responsable.SelectedValue), " AND id=" + e.Appointment.ID).Rows)
                    {
                        obj_consulta.UpdateParameters["usuario_responsable"].DefaultValue = Convert.ToString(rw["usuario_responsable"]);
                        obj_consulta.UpdateParameters["TRCODTER"].DefaultValue = Convert.ToString(rw["TRCODTER"]);
                        obj_consulta.UpdateParameters["PH_CODIGO"].DefaultValue = Convert.ToString(rw["PH_CODIGO"]);
                        obj_consulta.UpdateParameters["SERVICIO"].DefaultValue = Convert.ToString(rw["SERVICIO"]);
                        obj_consulta.UpdateParameters["TK_NUMERO"].DefaultValue = Convert.ToString(rw["TK_NUMERO"]); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

        protected void RadScheduler1_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            if (lc_indicador)
            {
                lc_indicador = false;
                litTextoMensaje.Text = " Ticket Nro:" + e.Appointment.ID;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
    }
}