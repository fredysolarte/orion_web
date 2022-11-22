using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Compras
{
    public partial class CuentasxPagar : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

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
            this.OcultarPaginador(rlv_pagos, "RadDataPager1", "BotonesBarra");
        }
        protected void rc_proveedor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int i = 0;
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            try {
                foreach (DataRow rw in (Obj.GetFacturasxPago(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadComboBox).SelectedValue))).Rows)
                {
                    DataRow rx = tbItems.NewRow();
                    rx["DP_CODIGO"] = i;
                    rx["HP_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                    rx["HP_NROPAGO"] = 0;
                    rx["CH_NROCMP"] = rw["FD_NROCMP"];
                    rx["FD_NROFACTURA"] = rw["FD_NROFACTURA"];
                    rx["DP_CONCEPTO"] = rw["TTCODCLA"];
                    rx["SALDO"] = rw["TOT_FAC"];
                    rx["DP_VALOR"] = 0;
                    rx["DP_USUARIO"] = "amdin";
                    rx["DP_ESTADO"] = "AC";
                    rx["DP_FECING"] = System.DateTime.Today;
                    rx["DP_FECMOD"] = System.DateTime.Today;
                    rx["DP_FECREC"] = System.DateTime.Today;
                    rx["FD_FECFAC"] = rw["FD_FECFAC"];
                    rx["DOCUMENTO"] = rw["FD_NROFACTURA"];

                    tbItems.Rows.Add(rx);
                    rx = null;
                    i++;
                }

                (rlv_pagos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_pagos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
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
        protected void rlv_pagos_ItemInserted(object sender, Telerik.Web.UI.RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rlv_pagos_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    OrdenesComprasBL Obj = new OrdenesComprasBL();

                    try
                    {
                        tbItems = Obj.GetPagosDT(null, Convert.ToString(Session["CODEMP"]), 0);                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;                        
                    }
                    break;

                case "Buscar":
                    obj_pagos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_pagos.DataBind();
                    break;
                case "Edit":

                    break;
                case "Delete":
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_pagos_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
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
                    OrdenesComprasBL Obj = new OrdenesComprasBL();
                    try
                    {
                        tbItems = Obj.GetPagosDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_pagos.Items[0].GetDataKeyValue("HP_NROPAGO").ToString()));
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
        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            obj_pagos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_pagos.DataBind();
            if ((rlv_pagos.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
               
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_pagos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void txt_valor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text);
                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToInt32(rw["DP_CODIGO"]) == ln_codigo)
                    {
                        rw["DP_VALOR"] = Convert.ToDouble((sender as RadNumericTextBox).Value);
                    }
                }
                (rlv_pagos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_pagos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void obj_pagos_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            else
            {
                litTextoMensaje.Text = "Nro Recibo :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=3003&inban=S&inParametro=inConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        protected void obj_pagos_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbItems;
        }
    }
}