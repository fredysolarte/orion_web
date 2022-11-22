using System;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Administrador
{
    public partial class CrearSistema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }        

        protected void RadListView11_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Buscar")
            {
                ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
                RadListView11.DataBind();
                e.Canceled = true;
                return;
            }
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
            }
            else
            {
                ViewState["isClickInsert"] = false;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void RadListView11_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;
                }
                else
                {
                    e.Item.FindControl("pnItemMaster").Visible = true;
                    ViewState["toolbars"] = true;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(RadListView11, "RadDataPager1", "BotonesBarra");
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
        protected void BuscarGrilla(object sender, EventArgs e)
        {
        string filtro = " AND 1=1";

        if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txtCodigo") as RadTextBox).Text))
        {
            filtro += " AND sist_sistema =" + (((Button)sender).Parent.FindControl("txtCodigo") as RadTextBox).Text + "";
        }
            

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            ObjectDataSource1.SelectParameters["filter"].DefaultValue = filtro;
            RadListView11.DataBind();
            if ((RadListView11.Controls[0] is RadListViewEmptyDataItem))
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
                (RadListView11.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
    }
}