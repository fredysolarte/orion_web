using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Parametros
{
    public partial class ClasesParametros : System.Web.UI.Page
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
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Pedido"])))
                {
                    obj_clasesparametros.SelectParameters["filter"].DefaultValue = "PHPEDIDO =" + Convert.ToString(Request.QueryString["Pedido"]);
                    rlv_clasesparametros.DataBind();
                }
            }
        }
        protected void AnalizarCommand(string comando)
        {
            //if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            //    ViewState["toolbars"] = false;
            //else            
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
            this.OcultarPaginador(rlv_clasesparametros, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_clasesparametros_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    ComunBL obj = new ComunBL();
                    try
                    {
                        tbItems = obj.GetTbTablaLista(null, "0");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                    }
                    break;

                case "Buscar":
                    obj_clasesparametros.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_clasesparametros.DataBind();
                    break;                                
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";
           
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_clasesparametros.SelectParameters["filter"].DefaultValue = filtro;
            rlv_clasesparametros.DataBind();
            if ((rlv_clasesparametros.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_clasesparametros.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_clasesparametros_OnItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
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
                    ComunBL obj = new ComunBL();
                    try
                    {
                        tbItems = obj.GetTbTablaLista(null, Convert.ToString(rlv_clasesparametros.Items[0].GetDataKeyValue("CLAP_CLASE").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                    }
                }
            }
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_OnItemCommand(object source, GridCommandEventArgs e)
        {
            ComunBL Obj = new ComunBL();
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        Obj.InsertTBTABLAS(null, Convert.ToString(Session["CODEMP"]), (rlv_clasesparametros.Items[0].FindControl("txt_codigo") as RadTextBox).Text, (e.Item.FindControl("txt_codcla") as RadTextBox).Text,
                                           (e.Item.FindControl("txt_vlrc") as RadTextBox).Text, Convert.ToInt32((e.Item.FindControl("txt_vlrn") as RadNumericTextBox).Value), 0, (e.Item.FindControl("txt_vlrd") as RadDatePicker).SelectedDate,
                                           (e.Item.FindControl("txt_nomcla") as RadTextBox).Text, Convert.ToString(Session["UserLogon"]), "AC", 0, null, null);
                        break;
                    case "Update":
                        Obj.UpdateTBTABLAS(null, Convert.ToString(Session["CODEMP"]), (rlv_clasesparametros.Items[0].FindControl("txt_codigo") as RadTextBox).Text, (e.Item.FindControl("txt_codcla") as RadTextBox).Text,
                                           (e.Item.FindControl("txt_vlrc") as RadTextBox).Text, Convert.ToInt32((e.Item.FindControl("txt_vlrn") as RadNumericTextBox).Value), 0, (e.Item.FindControl("txt_vlrd") as RadDatePicker).SelectedDate,
                                           (e.Item.FindControl("txt_nomcla") as RadTextBox).Text, Convert.ToString(Session["UserLogon"]), "AC", 0, null, null);
                        break;
                    case "Delete":
                        Obj.UpdateTBTABLAS(null, Convert.ToString(Session["CODEMP"]), (rlv_clasesparametros.Items[0].FindControl("txt_codigo") as RadTextBox).Text, (e.Item.FindControl("txt_codcla") as RadTextBox).Text,
                                           (e.Item.FindControl("txt_vlrc") as RadTextBox).Text, Convert.ToInt32((e.Item.FindControl("txt_vlrn") as RadNumericTextBox).Value), 0, (e.Item.FindControl("txt_vlrd") as RadDatePicker).SelectedDate,
                                           (e.Item.FindControl("txt_nomcla") as RadTextBox).Text, Convert.ToString(Session["UserLogon"]), "AN", 0, null, null);
                        break;
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
    }
}