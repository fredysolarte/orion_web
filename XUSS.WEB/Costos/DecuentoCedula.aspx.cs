using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.Web.UI;
using XUSS.BLL.Costos;
using XUSS.BLL.Comun;
using System.Data;

namespace XUSS.WEB.Costos
{
    public partial class DecuentoCedula : System.Web.UI.Page
    {
        private Telerik.Web.UI.RadListViewDataItem rliItems;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (rvl_descuento.InsertItem != null)
            {
                rliItems = rvl_descuento.InsertItem;
            }

            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";
            
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_cedula") as RadNumericTextBox).Text))
            {
                filtro += "AND CONDICION_1 = '" + (((Button)sender).Parent.FindControl("edt_cedula") as RadNumericTextBox).Text + "'";
            }
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text))
            {
                filtro += "AND UPPER(TRNOMBRE) LIKE '%" + (((Button)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text.ToUpper() + "%'";
            }
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_apellido") as RadTextBox).Text))
            {
                filtro += "AND UPPER(TRAPELLI) LIKE '%" + (((Button)sender).Parent.FindControl("edt_apellido") as RadTextBox).Text.ToUpper() + "%'";
            }
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_contacto") as RadTextBox).Text))
            {
                filtro += "AND UPPER(TRCONTAC) LIKE '%" + (((Button)sender).Parent.FindControl("edt_contacto") as RadTextBox).Text.ToUpper() + "%'";
            }
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            obj_descuentocedula.SelectParameters["filter"].DefaultValue = filtro;
            rvl_descuento.DataBind();
            if ((rvl_descuento.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" class=\"box\">");
                str.AppendLine("<div class=\"messages\">");
                str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
                str.AppendLine("    <div class=\"image\">");
                str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
                str.AppendLine("		</div>");
                str.AppendLine("    <div class=\"text\">");
                str.AppendLine("        <h6>Información</h6>");
                str.AppendLine("        <span>No se encontraron registros</span>");
                str.AppendLine("    </div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                (rvl_descuento.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rvl_descuento_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
                //((RadDatePicker)rvl_descuento.Items[0].FindControl("edt_fecfin")).SelectedDate = DateTime.Today;
                //((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecfin")).SelectedDate = System.DateTime.Today;
                //((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecini")).SelectedDate = System.DateTime.Today;
            }
            else
            {
                ViewState["isClickInsert"] = false;
            }
            switch (e.CommandName)
            {
                case "Buscar":
                    obj_descuentocedula.SelectParameters["filter"].DefaultValue = "1=0";
                    rvl_descuento.DataBind();
                    e.Canceled = true;
                    break;
                default:
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rvl_descuento, "RadDataPager1", "BotonesBarra");
        }
        public string GetEstado(object a)
        {
            if (a == DBNull.Value || a == null || a.ToString() == "")
                return null;
            switch (Convert.ToString(a))
            {
                case "AC":
                    return "Activo";
                case "AN":
                    return "Anulado";
                case "CE":
                    return "Cerrado";                
                default:
                    return null;
            }
        }
        protected void OcultarPaginador(RadListView rlv, string idPaginador, string idPanelBotones)
        {
            if (rlv != null)
            {
                Control paginador = rlv.FindControl(idPaginador);
                if (paginador != null)
                {
                    paginador.Visible = (bool)ViewState["toolbars"];
                }
                if (rlv.Items.Count > 0 && rlv.Items[0].ItemType == RadListViewItemType.DataItem)
                {
                    (rlv.Items[0].FindControl(idPanelBotones) as Control).Visible = (bool)ViewState["toolbars"];
                }
            }
        }
        protected void AnalizarCommand(string comando)
        {
            if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;         
            }
            else
            {
                ViewState["toolbars"] = false;

            }
        }
        protected void rtbCodigo_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace((((RadNumericTextBox)(rliItems.FindControl("rtbCodigo"))).Text)))
         {
             DecuentoCedulaBL obj = new DecuentoCedulaBL();
             //((RadTextBox)(rliItems.FindControl("rtbNombre"))).Text = obj.GetNombreTerceros(null, " WHERE TRCODEMP ='001' AND TRCODNIT ='" + ((RadNumericTextBox)(rliItems.FindControl("rtbCodigo"))).Text + "'");
            ((RadTextBox)(rliItems.FindControl("rtbNombre"))).Text = "";
            ((RadTextBox)(rliItems.FindControl("rtbApellido"))).Text = "";
            ((RadTextBox)(rliItems.FindControl("rtbContacto"))).Text = "";

             using (IDataReader reader = obj.GetNombreTerceros(null, " WHERE TRCODEMP ='001' AND TRCODNIT ='" + ((RadNumericTextBox)(rliItems.FindControl("rtbCodigo"))).Text + "'"))
             {
                 while (reader.Read())
                 {
                     ((RadTextBox)(rliItems.FindControl("rtbNombre"))).Text = Convert.ToString(reader["TRNOMBRE"]);
                     ((RadTextBox)(rliItems.FindControl("rtbApellido"))).Text = Convert.ToString(reader["TRAPELLI"]);
                     ((RadTextBox)(rliItems.FindControl("rtbContacto"))).Text = Convert.ToString(reader["TRCONTAC"]);
                     if ((reader.IsDBNull(reader.GetOrdinal("TRFECNAC")) != null) && (Convert.ToString(reader["TRFECNAC"]).Trim() != "") )
                     {
                         ((DropDownList)rliItems.FindControl("rcbDia")).SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Day));
                         if (Convert.ToInt32((Convert.ToDateTime(reader["TRFECNAC"]).Month)) < 10)
                             ((DropDownList)rliItems.FindControl("rcbMes")).SelectedValue = '0' + Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Month));
                         else
                             ((DropDownList)rliItems.FindControl("rcbMes")).SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Month));
                         
                         ((DropDownList)rliItems.FindControl("rcbAno")).SelectedValue = Convert.ToString((Convert.ToDateTime(reader["TRFECNAC"]).Year));
                     }
                 }
             }
             ((RadDatePicker)(rliItems.FindControl("edt_fecini"))).SelectedDate = System.DateTime.Today;
             ((RadDatePicker)(rliItems.FindControl("edt_fecfin"))).SelectedDate = System.DateTime.Today;
         }
        }
        protected void rvl_descuento_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {                    
                    e.Item.FindControl("pnItemMaster").Visible = false;
                    return;
                }
                else
                {
                    ViewState["toolbars"] = true;
                }
            }
        }
        protected void obj_descuentocedula_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }
        protected void obj_descuentocedula_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            string con_2 = "";
            DateTime? l_fecha;
            if (((CheckBox)rvl_descuento.InsertItem.FindControl("chk_terminos")).Checked)
                con_2 = "S";
            e.InputParameters["CONDICION_2"] = con_2;
            try
            {
                l_fecha = Convert.ToDateTime(((DropDownList)rvl_descuento.InsertItem.FindControl("rcbDia")).SelectedValue + "/" +
                                             ((DropDownList)rvl_descuento.InsertItem.FindControl("rcbMes")).SelectedValue + "/" +
                                             ((DropDownList)rvl_descuento.InsertItem.FindControl("rcbAno")).SelectedValue);
            }
            catch(Exception ex)
            { 
                l_fecha= null;
            }

            ComunBL.InsertTercero(null, "001", 0,
                                            ((RadTextBox)rvl_descuento.InsertItem.FindControl("rtbNombre")).Text, null, ((RadTextBox)rvl_descuento.InsertItem.FindControl("rtbContacto")).Text, null, 
                                            ((RadNumericTextBox)rvl_descuento.InsertItem.FindControl("rtbCodigo")).Text, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, "S", "N", "N", "N", "N",
                                                         "N", null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null,
                                                         null, null, null, null, l_fecha, null, null, ((RadTextBox)rvl_descuento.InsertItem.FindControl("rtbApellido")).Text, null,true);                        
        }
        public Boolean GetValCheck(object a)
        {

            if (a is DBNull || a == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void rvl_descuento_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {

        }
        protected void chk_terminos_CheckedChanged(object sender, EventArgs e)
        {
            int dt = System.DateTime.Today.Year;

            if (((CheckBox)(sender)).Checked == true)
            {
                if (rvl_descuento.InsertItem != null)
                {
                    ((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecfin")).SelectedDate = Convert.ToDateTime("31/12/" + Convert.ToString(dt));
                    ((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecini")).SelectedDate = System.DateTime.Today;
                }
                else {

                    ((RadDatePicker)rvl_descuento.Items[0].FindControl("edt_fecfin")).SelectedDate = Convert.ToDateTime("31/12/" + Convert.ToString(dt));
                    ((RadDatePicker)rvl_descuento.Items[0].FindControl("edt_fecini")).SelectedDate = System.DateTime.Today;
                }
            }
            else
            {
                if (rvl_descuento.InsertItem != null)
                {
                    ((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecfin")).SelectedDate = System.DateTime.Today;
                    ((RadDatePicker)rvl_descuento.InsertItem.FindControl("edt_fecini")).SelectedDate = System.DateTime.Today;
                }
                else
                {
                    ((RadDatePicker)rvl_descuento.Items[0].FindControl("edt_fecfin")).SelectedDate = System.DateTime.Today;
                    ((RadDatePicker)rvl_descuento.Items[0].FindControl("edt_fecini")).SelectedDate = System.DateTime.Today;
                }
            }
        }
    }
}