using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class TiposPedido : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
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
            this.OcultarPaginador(rlv_tpedido, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_tpedido_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_tpedido.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_tpedido.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_tpedido_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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

            TPedidoBL Obj = new TPedidoBL();
            try {
                tbItems = Obj.GetBodegasxPedido(null, Convert.ToString(Session["CODEMP"]), rlv_tpedido.Items[0].GetDataKeyValue("PTTIPPED").ToString());
                (e.Item.FindControl("rgBodegas") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rgBodegas") as RadGrid).DataBind();
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
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_tpedido.SelectParameters["filter"].DefaultValue = filtro;
            rlv_tpedido.DataBind();
            if ((rlv_tpedido.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_tpedido.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
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

        protected void obj_tpedido_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbBodegas"] = tbItems;
        }

        protected void obj_tpedido_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbBodegas"] = tbItems;
        }

        protected void rlv_tpedido_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }            
        }

        protected void obj_tpedido_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        protected void obj_tpedido_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        protected void rlv_tpedido_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            foreach (DataColumn rc in tbItems.Columns)
                rc.ReadOnly = false;

            foreach(GridDataItem itm in (e.ListViewItem.FindControl("rgBodegas") as RadGrid).Items)
            {
                if (((CheckBox)itm.FindControl("chk_habilita")).Checked)
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        string lc_bodega = Convert.ToString(itm["BDBODEGA"].Text);
                        if (lc_bodega == Convert.ToString(rw["BDBODEGA"]))
                        {
                            rw["CHK"] = "S";
                            rw["BPORDEN"] = (itm.FindControl("txt_orden") as RadNumericTextBox).Value;
                        }
                    }
                }
            }
        }
    }
}