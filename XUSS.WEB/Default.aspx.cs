using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Tareas;
using System.Data;
using System.Configuration;
using System.IO;
using Telerik.Web.UI;
using System.Drawing;
using System.Text;
using XUSS.BLL.Mail;
using XUSS.BLL.Terceros;

namespace XUSS.WEB
{
    public partial class _Default : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public string GetPrioridad(string Cod)
        {
            switch (Cod)
            {
                case "01": return "Bajo";
                case "02": return "Medio";
                case "03": return "Alto";
                case "04": return "Urgente";
                case "05": return "Emergencia";
                case "06": return "Critico";
                default:
                    return null;
            }
        }
        public string GetEstado(string Cod)
        {
            switch (Cod)
            {
                case "AC": return "Activo";
                case "CE": return "Cerrado";
                default: return null;
            }
        }
        public string GetNomUsuario(string Cod)
        {
            return LstTareasBL.GetNomUsuarios("", Cod);
        }
        protected void rgDetalle_PreRender(object sender, EventArgs e)
        {
            grid_rgDetalle(sender, e, rgDetalle);
        }
        protected void grid_rgDetalle(object sender, EventArgs e, RadGrid grilla)
        {
            string aValue = "";

            foreach (GridDataItem item in grilla.Items)
            {
                aValue = ((Label)item.FindControl("tk_prioridadLabel")).Text;
                switch (aValue)
                {
                    case "Bajo":
                        //item.ControlStyle.BackColor = Color.Gainsboro;
                        item.ControlStyle.ForeColor = Color.Black; //aprox rojo
                        item.ControlStyle.Font.Bold = true;
                        break;
                    case "Medio":
                        //item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = Color.Blue;
                        item.ControlStyle.Font.Bold = true;
                        break;
                    case "Alto":
                        //item.ControlStyle.BackColor = Color.Gainsboro; //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = Color.FromArgb(255, 60, 0); //aprox rojo
                        item.ControlStyle.Font.Bold = true;
                        break;
                    case "Urgente":
                        //item.ControlStyle.BackColor = Color.Gainsboro;   //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = Color.Red;
                        item.ControlStyle.Font.Bold = true;
                        break;
                    case "Emergencia":
                        //item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = Color.DeepPink;
                        item.ControlStyle.Font.Bold = true;
                        break;
                    case "Critico":
                        //item.ControlStyle.BackColor = Color.FromArgb(255, 200, 140);  //cxStyleConsMenCeroNoOK //aprox naraja
                        //item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = Color.Red; //aprox rojo
                        item.ControlStyle.Font.Bold = true;
                        break;
                }
            }
        }
        protected void IBtnFind_OnClick(object sender, EventArgs e)
        {
            pnlBuscar.Visible = true;
            pnlGrilla.Visible = false;
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            string lc_sql = "AND 1=1";
            if (!string.IsNullOrEmpty(edt_id.Text))
                lc_sql += " AND TK_NUMERO =" + edt_id.Text;
            if (!string.IsNullOrEmpty(edt_fasunto.Text))
                lc_sql += " AND TK_ASUNTO LIKE'%" + edt_fasunto.Text.ToUpper() + "%'";


            obj_LstTareas.SelectParameters["inTipo"].DefaultValue = rc_tipo.SelectedValue;
            obj_LstTareas.SelectParameters["filter"].DefaultValue = lc_sql;
            rgDetalle.DataBind();
            pnlBuscar.Visible = false;
            pnlGrilla.Visible = true;
            pnlProgramar.Visible = false;
        }
        protected void btnAgregar_click(object sender, EventArgs e)
        {
            string ruta = "", nombre;
            int ln_numero = 0;


            if (this.rauArchivo.UploadedFiles.Count > 0)
            {
                ruta = HttpContext.Current.Server.MapPath("/Uploads") + "\\";

                Random aleatorio = new Random(DateTime.Now.Second);
                do
                {
                    nombre = aleatorio.Next(1, 999999).ToString() + aleatorio.Next(1, 999999).ToString() + "-" + rauArchivo.UploadedFiles[0].GetName();
                } while (File.Exists(ruta + nombre));

                ruta = ruta + nombre;
                rauArchivo.UploadedFiles[0].SaveAs(ruta);
            }

            ln_numero = LstTareasBL.InsertTicket("", rb_responsable.SelectedValue, rb_propietario.SelectedValue, rb_prioridad.SelectedValue, edt_asunto.Text,
                                         edt_Observaciones.Text, "" ,edt_fecVencimiento.SelectedDate, ruta);

            string lbody = string.Format(@"<table>
                                           <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                            <tr><td><b>{1}</b> Le Asigno una Tarea con Prioridad <b>{2}</b>.</br></br> </td> <tr>                                           
                                            <tr><td>Asunto: {3}</br></br></td></tr>
                                            <tr><td>Observaciones:{4}</td></tr>
                                            </table> 
                                            <table>
                                            </tr></td>
                                            No responda a este email ya que es Sólo de envío.
                                            </td></tr>
                                            </table>", rb_responsable.Text, rb_propietario.Text, rb_prioridad.Text,
                                                                                                edt_asunto.Text.Replace("\n", "</br>"), edt_Observaciones.Text);

            UMail lMail = new UMail();
            DataTable dtTO = UMail.NewToDataTable();
            dtTO.Rows.Add(LstTareasBL.GetEmailUsuario("", rb_responsable.SelectedValue), rb_responsable.Text, string.Empty);
            lMail.SendMail(ConfigurationManager.AppSettings["MAIL_SERVER"], ConfigurationManager.AppSettings["MAIL_USER"], ConfigurationManager.AppSettings["MAIL_PASSWORD"],
                                ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM"], dtTO, "[" + Convert.ToString(ln_numero) + "][XUSS] Tarea Asignada", lbody, null, null);
            pnlGrilla.Visible = true;
            pnlBuscar.Visible = false;
            pnlNuevo.Visible = false;
            IBtnFind.Visible = true;
            IBtnInsert.Visible = true;
            pnlProgramar.Visible = false;
            rgDetalle.DataBind();
        }
        protected void btnCancel_click(object sender, EventArgs e)
        {
            pnlGrilla.Visible = true;
            pnlBuscar.Visible = false;
            pnlNuevo.Visible = false;
            IBtnFind.Visible = true;
            IBtnInsert.Visible = true;
            pnlProgramar.Visible = false;
        }
        protected void btn_cancelar_pro_click(object sender, EventArgs e)
        {
            pnlGrilla.Visible = true;
            pnlBuscar.Visible = false;
            pnlNuevo.Visible = false;
            IBtnFind.Visible = true;
            IBtnInsert.Visible = true;
            pnlProgramar.Visible = false;
        }
        protected void IBtnInsert_OnClick(object sender, EventArgs e)
        {
            this.LimpiarCampos();
            rb_propietario.SelectedValue = Convert.ToString(Session["UserLogon"]);
            pnlBuscar.Visible = false;
            pnlGrilla.Visible = false;
            pnlNuevo.Visible = true;
            IBtnFind.Visible = false;
            IBtnInsert.Visible = false;
            pnlProgramar.Visible = false;
        }
        protected void rc_area_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            obj_responsable.SelectParameters["connection"].DefaultValue = "";
            obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(rc_area.SelectedValue);
            rb_responsable.Items.Clear();
            rb_responsable.DataBind();
        }
        private void LimpiarCampos()
        {
            rb_propietario.ClearSelection();
            rb_prioridad.ClearSelection();
            rb_responsable.ClearSelection();
            edt_asunto.Text = "";
            edt_Observaciones.Text = "";
            edt_fecVencimiento.Clear();
        }
        protected void rgDetalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            
            if (e.CommandName == "RowClick")
            {
                GridDataItem item = (GridDataItem)e.Item;
                pnlDetalle.Visible = true;
                pnlGrilla.Visible = false;
                IBtnFind.Visible = false;
                IBtnInsert.Visible = false;
                pnlProgramar.Visible = false;
                obj_detalle.SelectParameters["TK_NUMERO"].DefaultValue = Convert.ToString(item["TK_NUMERO"].Text);
                rgDetalle.DataBind();
                edt_numerod.Text = Convert.ToString(item["TK_NUMERO"].Text);
                edt_asuntod.Text = Convert.ToString(item["TK_ASUNTO"].Text);
                rc_propietariod.SelectedValue = (item.FindControl("tk_propietarioLabelcod") as Label).Text;
            }
        }
        public string GetNombreArchivoRuta(string Nombre)
        {            
            return "../Uploads/" + Path.GetFileName(Nombre);            
        }
        public string GetNombreArchivo(string Nombre)
        {         
            return Path.GetFileName(Nombre);         
        }
        protected void Erc_area_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            obj_responsable.SelectParameters["connection"].DefaultValue = "";
            obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(((RadComboBox)sender).SelectedValue);
            (((RadComboBox)sender).Parent.FindControl("erb_responsable") as RadComboBox).Items.Clear();
            (((RadComboBox)sender).Parent.FindControl("erb_responsable") as RadComboBox).DataBind();
        }
        protected void obj_detalle_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["TK_NUMERO"] = edt_numerod.Text;
        }
        protected void rlv_detalle_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            string ruta = "", nombre = "";
            if ((e.ListViewItem.FindControl("rauArchivo") as RadAsyncUpload).UploadedFiles.Count > 0)
            {
                ruta = HttpContext.Current.Server.MapPath("/Uploads") + "\\";

                Random aleatorio = new Random(DateTime.Now.Second);
                do
                {
                    nombre = aleatorio.Next(1, 999999).ToString() + aleatorio.Next(1, 999999).ToString() + "-" + (e.ListViewItem.FindControl("rauArchivo") as RadAsyncUpload).UploadedFiles[0].GetName();
                } while (File.Exists(ruta + nombre));

                ruta = ruta + nombre;
                (e.ListViewItem.FindControl("rauArchivo") as RadAsyncUpload).UploadedFiles[0].SaveAs(ruta);
            }
            obj_detalle.InsertParameters["RUTA"].DefaultValue = ruta;
            obj_detalle.InsertParameters["TD_RESPONSABLE"].DefaultValue = (e.ListViewItem.FindControl("erb_responsable") as RadComboBox).SelectedValue;
            obj_detalle.InsertParameters["TD_USUARIO"].DefaultValue = HttpContext.Current.Session["UserLogon"].ToString();
            obj_detalle.InsertParameters["MAIL_SERVER"].DefaultValue = ConfigurationManager.AppSettings["MAIL_SERVER"];
            obj_detalle.InsertParameters["MAIL_USER"].DefaultValue = ConfigurationManager.AppSettings["MAIL_USER"];
            obj_detalle.InsertParameters["MAIL_PASSWORD"].DefaultValue = ConfigurationManager.AppSettings["MAIL_PASSWORD"];
            obj_detalle.InsertParameters["MAIL_FROM"].DefaultValue = ConfigurationManager.AppSettings["MAIL_FROM"];
            obj_detalle.InsertParameters["inAsunto"].DefaultValue = edt_asuntod.Text;
            obj_detalle.InsertParameters["inPropietario"].DefaultValue = rc_propietariod.SelectedValue;


        }
        protected void obj_detalle_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
      
        }
        protected void lk_regresar_Click(object sender, EventArgs e)
        {
            string lc_sql = "AND 1=1";
            if (!string.IsNullOrEmpty(edt_id.Text))
                lc_sql += " AND TK_NUMERO =" + edt_id.Text;
            if (!string.IsNullOrEmpty(edt_fasunto.Text))
                lc_sql += " AND TK_ASUNTO LIKE'%" + edt_fasunto.Text.ToUpper() + "%'";


            obj_LstTareas.SelectParameters["inTipo"].DefaultValue = rc_tipo.SelectedValue;
            obj_LstTareas.SelectParameters["filter"].DefaultValue = lc_sql;
            rgDetalle.DataBind();
            pnlBuscar.Visible = false;
            pnlGrilla.Visible = true;
            pnlDetalle.Visible = false;
            IBtnFind.Visible = true;
            IBtnInsert.Visible = true;
            pnlProgramar.Visible = false;

        }
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel"))
                ViewState["toolbars"] = true;
            else
                ViewState["toolbars"] = false;


        }
        protected void OcultarPaginador(RadListView rlv, string idPaginador, string idPanelBotones)
        {
            
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_detalle, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_detalle_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;                   
                    break;                
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void btn_invitacion_Click(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + mpCita.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_cita_Click(object sender, EventArgs e)
        {
            LstTareasBL Obj = new LstTareasBL();
            try {
                var collection = rc_usuarios.CheckedItems;
                Obj.InsertAppoiment(null, txt_asunto.Text, Convert.ToDateTime(txt_finicial.SelectedDate), Convert.ToDateTime(txt_ffinal.SelectedDate), 0, Convert.ToString(Session["UserLogon"]), null, null, 1, null, null, null, null);
                foreach (var item in collection)
                {
                    Obj.InsertAppoiment(null, txt_asunto.Text, Convert.ToDateTime(txt_finicial.SelectedDate), Convert.ToDateTime(txt_ffinal.SelectedDate), 0, Convert.ToString(item.Value), null, null, 1, null, null, null, null);
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

        protected void btn_programacion_Click(object sender, EventArgs e)
        {            
            TitleTextBox.Text = edt_asuntod.Text;
            pnlBuscar.Visible = false;
            pnlGrilla.Visible = false;
            pnlDetalle.Visible = false;
            IBtnFind.Visible = false;
            IBtnInsert.Visible = false;
            pnlProgramar.Visible = true;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            LstTareasBL Obj = new LstTareasBL();
            try
            {
                if (LstTareasBL.ValidaFechas(Convert.ToString(rc_usuario.SelectedValue), Convert.ToDateTime(StartInput.SelectedDate)) == 0)
                {
                    LstTareasBL.InsertDetalleTicket(Convert.ToInt32(edt_numerod.Text), Convert.ToString(rc_usuario.SelectedValue), "Asignacion Insidencia", null, TitleTextBox.Text, Convert.ToDateTime(StartInput.SelectedDate), Convert.ToDateTime(EndInput.SelectedDate), 0, Convert.ToString(rc_usuario.SelectedValue), null, null, 1,
                                        Convert.ToInt32(rc_tercero.SelectedValue), Convert.ToInt32(rc_propiedad.SelectedValue), rc_tservicio.SelectedValue);
                    pnlBuscar.Visible = false;
                    pnlGrilla.Visible = true;
                    pnlNuevo.Visible = false;
                    IBtnFind.Visible = false;
                    IBtnInsert.Visible = false;
                    pnlProgramar.Visible = false;
                }
                else
                {
                    litTextoMensaje.Text = "Rango de Fechas Invalido!!!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
    }
}
