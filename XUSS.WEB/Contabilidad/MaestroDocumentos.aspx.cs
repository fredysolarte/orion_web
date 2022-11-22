using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;

namespace XUSS.WEB.Contabilidad
{
    public partial class MaestroDocumentos : System.Web.UI.Page
    {
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rvl_documentos, "RadDataPager1", "BotonesBarra");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("edt_codigo") as RadTextBox).Text))
            {
                filtro += "AND DOC_IDENTI = '" + (((RadButton)sender).Parent.FindControl("edt_codigo") as RadTextBox).Text + "'";
            }
            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text))
            {
                filtro += "AND UPPER(DOC_NOMBRE) LIKE '%" + (((RadButton)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text.ToUpper() + "%'";
            }


            obj_documentos.SelectParameters["filter"].DefaultValue = filtro;
            rvl_documentos.DataBind();
            if ((rvl_documentos.Controls[0] is RadListViewEmptyDataItem))
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
                (rvl_documentos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_documentos_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
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
                    obj_documentos.SelectParameters["filter"].DefaultValue = "1=0";
                    rvl_documentos.DataBind();
                    e.Canceled = true;
                    break;
                default:
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_documentos_ItemDataBound(object sender, RadListViewItemEventArgs e)
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
        protected void obj_documentos_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            string lc_indicador = "N";            
            if (((CheckBox)rvl_documentos.InsertItem.FindControl("chk_terminos")).Checked)
                lc_indicador = "S";
            e.InputParameters["DOC_AINC"] = lc_indicador;

        }
        protected void obj_documentos_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //string lc_indicador = "N";
            //if (((CheckBox)rvl_documentos.InsertItem.FindControl("chk_terminos")).Checked)
            //    lc_indicador = "S";
            //e.InputParameters["DOC_AINC"] = lc_indicador;

        }
        public Boolean GetValCheck(object a)
        {

            if (a is DBNull || a == null || Convert.ToString(a) =="N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void rvl_documentos_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            string lc_indicador = "N";
            if ((e.ListViewItem.FindControl("chk_auto") as CheckBox).Checked)
                lc_indicador = "S";
            obj_documentos.UpdateParameters["DOC_AINC"].DefaultValue = lc_indicador;
        }
    }
}