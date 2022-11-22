using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Costos
{
    public partial class CargarCostos : System.Web.UI.Page
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
            this.OcultarPaginador(rlv_ccostos, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_ccostos_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_ccostos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_ccostos.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_ccostos_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
            string filtro = " AND 1=1 ";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_tdocorigen") as RadComboBox).SelectedValue))
                filtro += " AND CT_TDOCORIGEN ='" + (((RadButton)sender).Parent.FindControl("rc_tdocorigen") as RadComboBox).SelectedValue+"'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_traslado") as RadTextBox).Text))
                filtro += " AND TSNROTRA =" + (((RadButton)sender).Parent.FindControl("txt_traslado") as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_ccostos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_ccostos.DataBind();
            if ((rlv_ccostos.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_ccostos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void btn_filtroTer_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";
            if (!string.IsNullOrWhiteSpace(edt_nomtercero.Text))
                filter += "AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + edt_nomtercero.Text.ToUpper() + "%'";
            if (!string.IsNullOrWhiteSpace(edt_identificacion.Text))
                filter += "AND UPPER(TRCODNIT) LIKE '%" + edt_identificacion.Text.ToUpper() + "%'";


            obj_terceros.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaTerceros.DataBind();
            mpTerceros.Show();
        }
        protected void rgConsultaTerceros_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                try
                {
                    (rlv_ccostos.InsertItem.FindControl("txt_nomtercero") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                    (rlv_ccostos.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);                                                                 
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item_ = null;
                    item = null;
                    obj = null;
                }
            }
            else
            {
                if (e.CommandName == "Page")
                {
                    mpTerceros.Show();
                }

            }
        }
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            mpTerceros.Show();
        }
        protected void iBtnFindDocumento_OnClick(object sender, EventArgs e)
        {
            if (((sender as ImageButton).Parent.FindControl("rc_tdocorigen") as RadComboBox).SelectedValue=="01" )
                mp_buscartraslado.Show();
            if (((sender as ImageButton).Parent.FindControl("rc_tdocorigen") as RadComboBox).SelectedValue == "02")
                mp_buscarcmp.Show();
        }
        protected void btn_filtroTras_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";

            if (!string.IsNullOrWhiteSpace(txt_nrotra.Text))
                filter += " AND TSNROTRA ="+ txt_nrotra.Text;
            obj_traslados.SelectParameters["filter"].DefaultValue = filter;
            rgTraslados.DataBind();
            mp_buscartraslado.Show();
        }
        protected void rgTraslados_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                try
                {
                    (rlv_ccostos.InsertItem.FindControl("txt_traslado") as RadTextBox).Text = Convert.ToString(item["TSNROTRA"].Text);                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item_ = null;
                    item = null;
                    obj = null;
                }
            }            
        }
        protected void rlv_ccostos_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                mpMensajes.Show();
            }
        }
        protected void obj_ccostos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Costo Cargado Correctamente!";                
            }
            mpMensajes.Show();

        }
        protected void btn_fitrocmp_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";
            if (!string.IsNullOrWhiteSpace(txt_nroorden.Text))
                filter += "AND CH_NROCMP =" + txt_nroorden.Text.ToUpper();            

            obj_compras.SelectParameters["filter"].DefaultValue = filter;
            rgOrdenCmp.DataBind();
            mp_buscarcmp.Show();
        }
        protected void rgOrdenCmp_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                try
                {
                    (rlv_ccostos.InsertItem.FindControl("txt_traslado") as RadTextBox).Text = Convert.ToString(item["CH_NROCMP"].Text);                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item_ = null;
                    item = null;
                    obj = null;
                }
            }            
        }
    }
}