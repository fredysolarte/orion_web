using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Parametros
{
    public partial class TipoProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["toolbars"] = true;
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_tproducto, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_tproducto_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_tproducto.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_tproducto.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_tproducto_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text))
                filtro += "AND TATIPPRO='" + (((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND UPPER(TANOMBRE) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text.ToUpper() + "%'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_tproducto.SelectParameters["filter"].DefaultValue = filtro;
            rlv_tproducto.DataBind();
            if ((rlv_tproducto.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                //str.AppendLine("<div id=\"box-messages\" class=\"box\">");
                //str.AppendLine("<div class=\"messages\">");
                //str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
                //str.AppendLine("    <div class=\"image\">");
                //str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
                //str.AppendLine("		</div>");
                //str.AppendLine("    <div class=\"text\">");
                //str.AppendLine("        <h6>Información</h6>");
                //str.AppendLine("        <span>No se encontraron registros</span>");
                //str.AppendLine("    </div>");
                //str.AppendLine("</div>");
                //str.AppendLine("</div>");
                //str.AppendLine("</div>");
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_tproducto.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        public Boolean GetEstado(object a)
        {
            if (a is DBNull || a == null || Convert.ToString(a) == "N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}