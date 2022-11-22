using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.Web.UI;
using XUSS.WEB.ControlesUsuario;
using System.Data;

namespace XUSS.WEB.Costos
{
    public partial class Descuentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            string filtro = "1=1";

            if ((((Button)sender).Parent.FindControl("rcb_almacen") as RadComboBox).SelectedValue != ".")
                filtro += "AND BODEGA ='" + (((Button)sender).Parent.FindControl("rcb_almacen") as RadComboBox).SelectedValue +"'";

            if ((((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_linea.SelectedValue != ".")
                filtro += "AND TP ='" + (((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_linea.SelectedValue + "'";

            if ((((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave1.SelectedValue != ".")
                filtro += "AND CLAVE1 ='" + (((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave1.SelectedValue + "'";

            if ((((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave2.SelectedValue != ".")
                filtro += "AND CLAVE2 ='" + (((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave2.SelectedValue + "'";

            if ((((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave3.SelectedValue != ".")
                filtro += "AND CLAVE3 ='" + (((Button)sender).Parent.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave3.SelectedValue + "'";

            obj_descuentos.SelectParameters["filter"].DefaultValue = filtro;
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
            }
            else
            {
                ViewState["isClickInsert"] = false;
            }
            switch (e.CommandName)
            {
                case "Buscar":
                    obj_descuentos.SelectParameters["filter"].DefaultValue = "1=0";
                    rvl_descuento.DataBind();
                    e.Canceled = true;
                    break;
                default:
                    break;
            }
            this.AnalizarCommand(e.CommandName);
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rvl_descuento, "RadDataPager1", "BotonesBarra");
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
        protected void rcb_tipdes_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (((RadComboBox)rvl_descuento.InsertItem.FindControl("rcb_tipdes")).SelectedValue == "1")
            {
                ((RadNumericTextBox)rvl_descuento.InsertItem.FindControl("edt_valor")).MaxValue = 100;
            }
            if (((RadComboBox)rvl_descuento.InsertItem.FindControl("rcb_tipdes")).SelectedValue == "2")
            {
                ((RadNumericTextBox)rvl_descuento.InsertItem.FindControl("edt_valor")).MaxValue = 99999;
            }
        }
        protected void obj_descuentos_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }
        protected void obj_descuentos_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters[2] = ((ctrl_gesArticulosHor)rvl_descuento.InsertItem.FindControl("ctrlArticulos1")).objrcb_linea.SelectedValue;
            e.InputParameters[3] = ((ctrl_gesArticulosHor)rvl_descuento.InsertItem.FindControl("ctrlArticulos1")).objrcb_clave1.SelectedValue;
            e.InputParameters[4] = ((ctrl_gesArticulosHor)rvl_descuento.InsertItem.FindControl("ctrlArticulos1")).objrcb_clave2.SelectedValue;
            e.InputParameters[5] = ((ctrl_gesArticulosHor)rvl_descuento.InsertItem.FindControl("ctrlArticulos1")).objrcb_clave3.SelectedValue;
            e.InputParameters[6] = ((ctrl_gesArticulosHor)rvl_descuento.InsertItem.FindControl("ctrlArticulos1")).objrcb_clave4.SelectedValue;
        }    
        protected void rvl_descuento_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            
            DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            //((ctrl_gesArticulosHor)rvl_descuento.Items[0].FindControl("ctrlArticulos1")).objrcb_linea.SelectedValue = fila["TP"].ToString();
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_linea.SelectedValue = fila["TP"].ToString();
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).rcb_linea_SelectedIndexChanged(null, null);
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave1.SelectedValue = fila["CLAVE1"].ToString();
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).rcb_clave1_SelectedIndexChanged(null, null);
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave2.SelectedValue = fila["CLAVE2"].ToString();
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).rcb_clave2_SelectedIndexChanged(null, null);
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave3.SelectedValue = fila["CLAVE3"].ToString();
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave4.SelectedValue = fila["CLAVE4"].ToString();

            // Desabilitando
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_linea.Enabled = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave1.Enabled = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave2.Enabled = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave3.Enabled = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objrcb_clave4.Enabled = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objedt_codigo.Visible = false;
            (e.Item.FindControl("ctrlArticulos1") as ctrl_gesArticulosHor).objlabelCodigo.Visible = false;

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
    }
}