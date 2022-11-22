using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Inventarios;

namespace XUSS.WEB.Inventarios
{
    public partial class FotoInventario : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    string[] words = Convert.ToString(Request.QueryString["Documento"]).Split('-');
                    obj_foto.SelectParameters["filter"].DefaultValue = "  FINROFOT =" + words[0];
                    rlv_foto.DataBind();
                }
            }

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
            this.OcultarPaginador(rlv_foto, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_foto_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_foto.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_foto.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_foto_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    FotoInventarioBL Obj = new FotoInventarioBL();
                    try
                    {
                        tbItems = Obj.GetFotoBod(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((e.Item.FindControl("txt_nrofoto") as RadTextBox).Text));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
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
            }            
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " AND 1=1 ";

            if ((((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue !="-1" )
                filtro += " AND FIBODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrofoto") as RadTextBox).Text))
                filtro += " AND FINROFOT = " + (((RadButton)sender).Parent.FindControl("txt_nrofoto") as RadTextBox).Text + "";
           
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_foto.SelectParameters["filter"].DefaultValue = filtro;
            rlv_foto.DataBind();
            if ((rlv_foto.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_foto.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7013&inban=S&inParametro=inConse&inValor=" + (rlv_foto.Items[0].FindControl("txt_nrofoto") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
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
        protected void obj_foto_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Foto :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7008&inban=S&inParametro=InConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }

            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }
        protected void rlv_foto_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        protected void rlv_foto_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_foto.InsertParameters["FIINVINI"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_invInicial") as CheckBox).Checked)
                obj_foto.InsertParameters["FIINVINI"].DefaultValue = "S";
        }

        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
    }
}