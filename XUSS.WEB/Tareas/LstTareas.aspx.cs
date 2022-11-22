using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Tareas;
using Telerik.Web.UI;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.IO;
using XUSS.BLL.Mail;

namespace XUSS.WEB.Tareas
{
    public partial class LstTareas : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Params["__EVENTTARGET"] == "ModalEdit")
            {
                ViewState["lb_origen"] = false;
                CargarModalPopupEdit();
            }
            if (Page.Request.Params["__EVENTTARGET"] == "ModalEditAsig")
            {
                ViewState["lb_origen"] = true;
                CargarModalPopupEditAsig();
            }
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
            return LstTareasBL.GetNomUsuarios("",Cod);        
        }
        public string GetNombreArchivoRuta(string Nombre)
        {
            //if (!string.IsNullOrEmpty(Nombre))
                return "../Uploads/"+ Path.GetFileName(Nombre);
            //else
                //return "";
        }
        public string GetNombreArchivo(string Nombre)
        {
            //if (!string.IsNullOrEmpty(Nombre))
            return  Path.GetFileName(Nombre);
            //else
            //return "";
        }
        protected void btnCancel_click(object sender, EventArgs e)
        {
            ModalPopupNuevoTicket.Hide();
        }
        protected void btnAgregar_click(object sender, EventArgs e)
        {
            string ruta="",nombre;            

            
            if (this.rauArchivo.UploadedFiles.Count > 0)
            {
                ruta = HttpContext.Current.Server.MapPath("/Uploads") + "\\";
                //nombre = Convert.ToString(ln_numero) +"-"+ rauArchivo.UploadedFiles[0].GetName();
                Random aleatorio = new Random(DateTime.Now.Second); 
                do
                {
                    nombre = aleatorio.Next(1, 999999).ToString() + aleatorio.Next(1, 999999).ToString() + "-" + rauArchivo.UploadedFiles[0].GetName();
                } while (File.Exists(ruta + nombre));

                ruta = ruta + nombre;
                rauArchivo.UploadedFiles[0].SaveAs(ruta);
            }

            LstTareasBL.InsertTicket("", rb_responsable.SelectedValue, rb_propietario.SelectedValue, rb_prioridad.SelectedValue, edt_asunto.Text,
                                         edt_Observaciones.Text, "", edt_fecVencimiento.SelectedDate,ruta);

            string lbody = string.Format(@"<table>
                                           <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                            <tr><td><b>{1}</b> Le Asigno una Tarea con Prioridad <b>{2}</b>.</br></br> </td> <tr>                                           
                                            <tr><td>Asunto: {3}</br></br></td></tr>
                                            <tr><td>Observaciones:{4}</td></tr>
                                            </table> 
                                            No responda a este email ya que es Sólo de envío.", rb_responsable.Text, rb_propietario.Text,rb_prioridad.Text, 
                                                                                                edt_asunto.Text.Replace("\n", "</br>"),edt_Observaciones.Text);

            UMail lMail = new UMail();
            DataTable dtTO = UMail.NewToDataTable();
            dtTO.Rows.Add(LstTareasBL.GetEmailUsuario("", rb_responsable.SelectedValue), rb_responsable.Text, string.Empty);
            if (!lMail.SendMail(ConfigurationManager.AppSettings["MAIL_SERVER"], ConfigurationManager.AppSettings["MAIL_USER"], ConfigurationManager.AppSettings["MAIL_PASSWORD"], 
                                ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM"], dtTO, "[XUSS] Tarea Asignada", lbody, null, null))
            {
                //lblEstado.Text += string.Format(" pero un error se presentó al enviar email a {0}", txtemail.Text);
            }
            else
            { 
            
            }
            rgDetalle.DataBind();
            rg_asignados.DataBind();
            ModalPopupNuevoTicket.Hide();
        }
        protected void im_nuevo(object sender, EventArgs e)
        {
            rb_propietario.SelectedValue = HttpContext.Current.Session["UserLogon"].ToString();
            rb_propietario.Enabled = false;
            edt_asunto.Text = "";
            rc_area.SelectedValue = "";
            rb_responsable.Items.Clear();
            rb_responsable.SelectedValue = "";
            rb_prioridad.SelectedValue = "";
            edt_Observaciones.Text = "";
            edt_fecVencimiento.SelectedDate = null;
            ModalPopupNuevoTicket.Show();
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
        protected void rgDetalle_OnUpdateCommand(object sender, GridCommandEventArgs e)
        {
            //GridEditableItem item = e.Item as GridEditableItem;
            //var TK_NUMERO = item.GetDataKeyValue("TK_NUMERO").ToString();
            //obj_LstTareasAC.UpdateParameters["TK_NUMERO"].DefaultValue = TK_NUMERO;
            //LstTareasBL.UpdateTicket("", Convert.ToInt32(TK_NUMERO),"CE");
            //rgDetalle.DataBind();
            //RadGrid1.DataBind();            
        }
        protected void rgDetalle_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            var TK_NUMERO = item.GetDataKeyValue("TK_NUMERO").ToString();
            //obj_LstTareasAC.UpdateParameters["TK_NUMERO"].DefaultValue = TK_NUMERO;

            LstTareasBL.DeleteTicket("", Convert.ToInt32(TK_NUMERO));
            rgDetalle.DataBind();            


        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            //grid_rgDetalle(sender, e, RadGrid1);
        }
        protected void rg_asignados_PreRender(object sender, EventArgs e)
        {
            grid_rgDetalle(sender, e, rg_asignados);
        }
        protected void CargarModalPopupEdit()
        {
            int id_ticket = Convert.ToInt32(rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
            using (IDataReader reader = LstTareasBL.GetlstTarea("",id_ticket))
            {
                while (reader.Read())
                {
                    erb_propietario.Enabled = false;
                    Erb_estado.Enabled = false;
                    if (HttpContext.Current.Session["UserLogon"].ToString() == reader["TK_PROPIETARIO"].ToString())
                        Erb_estado.Enabled = true;

                    Erc_area.SelectedValue = LstTareasBL.GetAreaUsuario(null,reader["TK_RESPONSABLE"].ToString());
                    obj_responsable.SelectParameters["connection"].DefaultValue = "";
                    obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(Erc_area.SelectedValue);
                    erb_responsable.DataBind();

                    erb_propietario.SelectedValue = reader["TK_PROPIETARIO"].ToString();
                    Eedt_asunto.Text = reader["TK_ASUNTO"].ToString();
                    erb_prioridad.SelectedValue = reader["TK_PRIORIDAD"].ToString();
                    erb_responsable.SelectedValue = reader["TK_RESPONSABLE"].ToString();
                    //Eedt_descripcion.Text = reader["TK_OBSERVACIONES"].ToString();
                    Erb_estado.SelectedValue = reader["TK_ESTADO"].ToString();
                    obj_detalle.SelectParameters["TK_NUMERO"].DefaultValue = Convert.ToString(id_ticket);
                    rg_detalle.DataBind();
                }
            }
            ModalPopupEditTicket.Show();
        }
        protected void btnUpdate_click(object sender, EventArgs e)
        {
            int id_ticket = 0;
            if (Convert.ToBoolean(ViewState["lb_origen"]))
            {
                id_ticket = Convert.ToInt32(rg_asignados.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
            }
            else
            {
                id_ticket = Convert.ToInt32(rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
            }
            
            LstTareasBL.UpdateTicket("", id_ticket, erb_responsable.SelectedValue, erb_propietario.SelectedValue, erb_prioridad.SelectedValue, Eedt_asunto.Text, Erb_estado.SelectedValue);
            rgDetalle.DataBind();
            rg_asignados.DataBind();

            if (HttpContext.Current.Session["UserLogon"].ToString() != erb_responsable.SelectedValue)
            {
                string lbody = string.Format(@"<table>
                                               <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                                <tr><td><b>{1}</b> Le Acaba de re-Asignar la Tarea Asunto: {2}  <b></b>.</br></br> </td> <tr>                                           
                                                <tr><td>Prioridad: {3}</br></br></td></tr>                                          
                                                </table> 
                                                No responda a este email ya que es Sólo de envío.", erb_responsable.Text, LstTareasBL.GetNomUsuarios(null, HttpContext.Current.Session["UserLogon"].ToString()),
                                                                                                    Eedt_asunto.Text.Replace("\n", "</br>"), erb_prioridad.Text);


                UMail lMail = new UMail();
                DataTable dtTO = UMail.NewToDataTable();
                dtTO.Rows.Add(LstTareasBL.GetEmailUsuario("", erb_responsable.SelectedValue), erb_responsable.Text, string.Empty);
                if (!lMail.SendMail(ConfigurationManager.AppSettings["MAIL_SERVER"], ConfigurationManager.AppSettings["MAIL_USER"], ConfigurationManager.AppSettings["MAIL_PASSWORD"],
                                    ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM"], dtTO, "[XUSS] Tarea Asignada", lbody, null, null))
                {
                    //lblEstado.Text += string.Format(" pero un error se presentó al enviar email a {0}", txtemail.Text);
                }
                else
                {

                }
            }
        }
        protected void rg_detalle_ItemCommand(object sender, GridCommandEventArgs e)
        {            
            ModalPopupEditTicket.Show();
        }
        protected void obj_detalle_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //e.InputParameters[2] = 0;
            //e.InputParameters[3] = Convert.ToInt32(rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
            //e.InputParameters[4] = HttpContext.Current.Session["UserLogon"].ToString();
        }
        protected void rg_detalle_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //obj_detalle.InsertParameters[1].DefaultValue = rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text;
            //obj_detalle.InsertParameters[2].DefaultValue = HttpContext.Current.Session["UserLogon"].ToString();
            string ruta = "", nombre;


            if (((RadAsyncUpload)e.Item.FindControl("rauArchivoD")).UploadedFiles.Count > 0)
            {
                ruta = HttpContext.Current.Server.MapPath("/Uploads") + "\\";
                //nombre = Convert.ToString(ln_numero) +"-"+ rauArchivo.UploadedFiles[0].GetName();
                
                Random aleatorio = new Random(DateTime.Now.Second);
                do
                {
                    nombre = aleatorio.Next(1, 999999).ToString() + aleatorio.Next(1, 999999).ToString() + "-" + ((RadAsyncUpload)e.Item.FindControl("rauArchivoD")).UploadedFiles[0].GetName();
                } while (File.Exists(ruta + nombre));

                ruta = ruta + nombre;
                ((RadAsyncUpload)e.Item.FindControl("rauArchivoD")).UploadedFiles[0].SaveAs(ruta);
            }

            if (Convert.ToBoolean(ViewState["lb_origen"]))
            {
                LstTareasBL.InsertDetalleTicket(Convert.ToInt32(rg_asignados.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text), HttpContext.Current.Session["UserLogon"].ToString(),
                                                ((RadTextBox)e.Item.FindControl("edt_observacion")).Text,ruta);
            }
            else
            {
                LstTareasBL.InsertDetalleTicket(Convert.ToInt32(rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text), HttpContext.Current.Session["UserLogon"].ToString(),
                                                ((RadTextBox)e.Item.FindControl("edt_observacion")).Text,ruta);
            }

            string lbody = string.Format(@"<table>
                                           <tr> <td>Señor(a) <b>{0}</b></br></br></td> </tr>
                                            <tr><td><b>{1}</b> Le Acaba de dar Respuesta a la Tarea Asunto: {2}  <b></b>.</br></br> </td> <tr>                                           
                                            <tr><td>Prioridad: {3}</br></br></td></tr>
                                            <tr><td>Observaciones:{4}</td></tr>
                                            </table> 
                                            No responda a este email ya que es Sólo de envío.", erb_propietario.Text, LstTareasBL.GetNomUsuarios(null, HttpContext.Current.Session["UserLogon"].ToString()),
                                                                                                Eedt_asunto.Text.Replace("\n", "</br>"), erb_prioridad.Text, ((RadTextBox)e.Item.FindControl("edt_observacion")).Text );
            

            UMail lMail = new UMail();
            DataTable dtTO = UMail.NewToDataTable();
            dtTO.Rows.Add(LstTareasBL.GetEmailUsuario("", erb_propietario.SelectedValue), erb_propietario.Text, string.Empty);
            if (!lMail.SendMail(ConfigurationManager.AppSettings["MAIL_SERVER"], ConfigurationManager.AppSettings["MAIL_USER"], ConfigurationManager.AppSettings["MAIL_PASSWORD"],
                                ConfigurationManager.AppSettings["MAIL_FROM"], ConfigurationManager.AppSettings["MAIL_FROM"], dtTO, "[XUSS] Tarea Asignada", lbody, null, null))
            {
                //lblEstado.Text += string.Format(" pero un error se presentó al enviar email a {0}", txtemail.Text);
            }
            else
            {

            }
                                            
        }
        protected void rc_area_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            obj_responsable.SelectParameters["connection"].DefaultValue = "";
            obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(rc_area.SelectedValue);
            rb_responsable.Items.Clear();
            rb_responsable.DataBind();
            ModalPopupNuevoTicket.Show();
        }
        protected void Erc_area_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            obj_responsable.SelectParameters["connection"].DefaultValue = "";
            obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(Erc_area.SelectedValue);
            erb_responsable.Items.Clear();
            erb_responsable.DataBind();
            ModalPopupEditTicket.Show();
        }
        protected void CargarModalPopupEditAsig()
        {
            int id_ticket = Convert.ToInt32(rg_asignados.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
            erb_responsable.Items.Clear();

            using (IDataReader reader = LstTareasBL.GetlstTarea("", id_ticket))
            {
                while (reader.Read())
                {
                    erb_propietario.Enabled = false;
                    Erb_estado.Enabled = false;
                    if (HttpContext.Current.Session["UserLogon"].ToString() == reader["TK_PROPIETARIO"].ToString())
                        Erb_estado.Enabled = true;
                                        
                    Erc_area.SelectedValue = LstTareasBL.GetAreaUsuario(null, reader["TK_RESPONSABLE"].ToString());
                    obj_responsable.SelectParameters["connection"].DefaultValue = "";
                    obj_responsable.SelectParameters["area"].DefaultValue = Convert.ToString(Erc_area.SelectedValue);
                    erb_responsable.DataBind();

                    erb_propietario.SelectedValue = reader["TK_PROPIETARIO"].ToString();
                    Eedt_asunto.Text = reader["TK_ASUNTO"].ToString();
                    erb_prioridad.SelectedValue = reader["TK_PRIORIDAD"].ToString();
                    erb_responsable.SelectedValue = reader["TK_RESPONSABLE"].ToString();
                    //Eedt_descripcion.Text = reader["TK_OBSERVACIONES"].ToString();
                    Erb_estado.SelectedValue = reader["TK_ESTADO"].ToString();
                    obj_detalle.SelectParameters["TK_NUMERO"].DefaultValue = Convert.ToString(id_ticket);
                    rg_detalle.DataBind();
                }
            }
            ModalPopupEditTicket.Show();
        }
    }
}