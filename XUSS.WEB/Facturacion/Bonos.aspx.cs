using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL;
using XUSS.BLL.Articulos;
using XUSS.BLL.Comun;
using XUSS.BLL.Facturacion;
using XUSS.BLL.Inventarios;
using XUSS.BLL.ListaPrecios;
using XUSS.BLL.Parametros;
using XUSS.BLL.Pedidos;
using XUSS.BLL.Terceros;
using XUSS.DAL.Comun;

namespace XUSS.WEB.Facturacion
{
    public partial class Bonos : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbItemsDev
        {
            set { ViewState["tbItemsDev"] = value; }
            get { return ViewState["tbItemsDev"] as DataTable; }
        }
        private DataTable tbPagos
        {
            set { ViewState["tbPagos"] = value; }
            get { return ViewState["tbPagos"] as DataTable; }
        }
        private DataTable tbPermisos
        {
            set { ViewState["tbPermisos"] = value; }
            get { return ViewState["tbPermisos"] as DataTable; }
        }
        private DataTable tbBalance
        {
            set { ViewState["tbBalance"] = value; }
            get { return ViewState["tbBalance"] as DataTable; }
        }
        private string gc_tlista
        {
            set { ViewState["gc_tlista"] = value; }
            get { return ViewState["gc_tlista"] as string; }
        }
        private int? gc_coddcto
        {
            set { ViewState["gc_coddcto"] = value; }
            get { return ViewState["gc_coddcto"] as int?; }
        }
        private int? gc_tipdcto
        {
            set { ViewState["gc_tipdcto"] = value; }
            get { return ViewState["gc_tipdcto"] as int?; }
        }
        private string g_bodega
        {
            set { ViewState["g_bodega"] = value; }
            get { return Convert.ToString(ViewState["g_bodega"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    string[] words = Convert.ToString(Request.QueryString["Documento"]).Split('-');
                    obj_factura.SelectParameters["filter"].DefaultValue = "  HDTIPFAC ='" + words[0] + "' AND HDNROFAC =" + words[1];
                    rlv_factura.DataBind();
                }
            }
            else
            {
                //tbPermisos
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
            this.OcultarPaginador(rlv_factura, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_factura_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            isClickInsertxt.Text = "false";
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    isClickInsertxt.Text = "true";
                    FacturacionBL Obj = new FacturacionBL();
                    try
                    {
                        tbItems = Obj.GetFacturaDT(null, null, null, 0);
                        tbPagos = Obj.GetPagos(null, null, null, 0);
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
                    obj_factura.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_factura.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(4))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND TRNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text))
                filtro += " AND TRNOMBR2 LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text))
                filtro += " AND HDNROFAC = '" + (((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue))
                filtro += " AND HDTIPFAC = '" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_factura.SelectParameters["filter"].DefaultValue = filtro;
            rlv_factura.DataBind();
            if ((rlv_factura.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_factura.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_factura_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                }
            }

            ComunBL ObjC = new ComunBL();
            FacturacionBL Obj = new FacturacionBL();
            TercerosBL ObjT = new TercerosBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                (e.Item.FindControl("lnk_devolucion") as LinkButton).Text = Convert.ToString((e.Item.FindControl("txt_tfdev") as RadTextBox).Text) + "-" + Convert.ToString((e.Item.FindControl("txt_facdev") as RadTextBox).Text);
                tbItems = Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((e.Item.FindControl("rc_tipfac") as RadComboBox).SelectedValue), Convert.ToInt32((e.Item.FindControl("txt_nrofac") as RadTextBox).Text));
                //Buscar Devoluciones & Agregarlas
                foreach (DataRow rw in Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((e.Item.FindControl("txt_tfdev") as RadTextBox).Text), Convert.ToInt32((e.Item.FindControl("txt_facdev") as RadTextBox).Text)).Rows)
                {
                    tbItems.ImportRow(rw);
                }
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                tbPagos = Obj.GetPagos(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((e.Item.FindControl("rc_tipfac") as RadComboBox).SelectedValue), Convert.ToInt32((e.Item.FindControl("txt_nrofac") as RadTextBox).Text));
                (e.Item.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                (e.Item.FindControl("rg_pagos") as RadGrid).DataBind();
                //Cargar Vendedores(Agentes)                
                (e.Item.FindControl("rc_agente") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(item);
                foreach (DataRow rw in ObjT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["TRCODTER"]);
                    itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                    (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                }
                (e.Item.FindControl("rc_agente") as RadComboBox).SelectedValue = fila["HDAGENTE"].ToString();

                (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                using (IDataReader reader = ObjC.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_pais") as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                        itemi.Text = Convert.ToString(reader["CIUDAD"]);
                        (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                item = null;
                ObjT = null;
                ObjC = null;
            }
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            obj_ltaEmpaque.SelectParameters["filter"].DefaultValue = "LH_ESTADO='CE' ";
            rgLtaEmpaque.DataBind();
            string script = "function f(){$find(\"" + modalLstEmpaque.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgLtaEmpaque_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int i = 1;
                string lc_ltaprecio = "";
                double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
                LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                FacturacionBL ObjF = new FacturacionBL();
                GridDataItem item = (GridDataItem)e.Item;
                ComunBL ObjC = new ComunBL();
                TercerosBL ObjT = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                try
                {
                    item_.Value = "";
                    item_.Text = "Seleccionar";

                    //caragar Cabecera
                    (rlv_factura.InsertItem.FindControl("txt_lta") as RadTextBox).Text = Convert.ToString(item["LH_LSTPAQ"].Text);
                    (rlv_factura.InsertItem.FindControl("txt_pedido") as RadTextBox).Text = Convert.ToString(item["LH_PEDIDO"].Text);

                    foreach (DataRow rw in Obj.GetLtaEmpaque(null, "LH_LSTPAQ=" + Convert.ToString(item["LH_LSTPAQ"].Text), 0, 0).Rows)
                    {
                        (rlv_factura.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(rw["TRCODNIT"]);
                        (rlv_factura.InsertItem.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(rw["PHCODCLI"]);
                        (rlv_factura.InsertItem.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(rw["TRNOMBRE"]);
                        (rlv_factura.InsertItem.FindControl("txt_apellido") as RadTextBox).Text = Convert.ToString(rw["TRAPELLI"]);
                        (rlv_factura.InsertItem.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(rw["TRDIRECC"]);
                        (rlv_factura.InsertItem.FindControl("txt_telefono") as RadTextBox).Text = Convert.ToString(rw["TRNROTEL"]);
                        (rlv_factura.InsertItem.FindControl("txt_email") as RadTextBox).Text = Convert.ToString(rw["TRCORREO"]);
                        (rlv_factura.InsertItem.FindControl("rc_pais") as RadComboBox).SelectedValue = Convert.ToString(rw["TRCDPAIS"]);


                        (rlv_factura.InsertItem.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                        (rlv_factura.InsertItem.FindControl("rc_ciudad") as RadComboBox).Items.Add(item_);

                        using (IDataReader reader = ObjC.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.InsertItem.FindControl("rc_pais") as RadComboBox).SelectedValue))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                                itemi.Text = Convert.ToString(reader["CIUDAD"]);
                                (rlv_factura.InsertItem.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
                            }
                        }

                        (rlv_factura.InsertItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue = Convert.ToString(rw["TRCIUDAD"]);
                        (rlv_factura.InsertItem.FindControl("rc_moneda") as RadComboBox).SelectedValue = Convert.ToString(rw["TRMONEDA"]);

                        lc_ltaprecio = Convert.ToString(rw["PHLISPRE"]);
                    }

                    //Cargar Agente(Vendedor)
                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Clear();
                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(item_);
                    foreach (DataRow rw in ObjT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["TRCODTER"]);
                        itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                        (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                    }

                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).SelectedValue = "";
                    //Cargar Detalle
                    tbItems = ObjF.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), null, 0);

                    foreach (DataRow rw in Obj.GetLtaEmpaqueDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(item["LH_LSTPAQ"].Text)).Rows)
                    {
                        DataRow row = tbItems.NewRow();

                        row["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["DTTIPFAC"] = "";
                        row["DTNROFAC"] = 0;
                        row["DTNROITM"] = i;
                        row["DTPEDIDO"] = Convert.ToInt32(item["LH_PEDIDO"].Text);
                        row["DTLINNUM"] = Convert.ToInt32(rw["LD_ITMPAQ"]);
                        row["DTTIPPRO"] = Convert.ToString(rw["LD_TIPPRO"]);
                        row["DTCLAVE1"] = Convert.ToString(rw["LD_CLAVE1"]);
                        row["DTCLAVE2"] = Convert.ToString(rw["LD_CLAVE2"]);
                        row["DTCLAVE3"] = Convert.ToString(rw["LD_CLAVE3"]);
                        row["DTCLAVE4"] = Convert.ToString(rw["LD_CLAVE4"]);
                        row["DTCODCAL"] = ".";
                        row["DTUNDPED"] = "UN";
                        row["DTCANPED"] = Convert.ToDouble(rw["LD_CANTID"]);
                        row["DTCANTID"] = Convert.ToDouble(rw["LD_CANTID"]);
                        row["DTCANKLG"] = 0;
                        row["DTLISPRE"] = lc_ltaprecio;
                        row["DTPRELIS"] = Convert.ToDouble(rw["PDPRELIS"]);
                        row["DTPRECIO"] = Convert.ToDouble(rw["PDPRECIO"]);
                        row["DTDESCUE"] = Convert.ToDouble(rw["PDDESCUE"]);
                        row["DTTOTDES"] = Convert.ToDouble(rw["PDDESCUE"]);
                        if (string.IsNullOrEmpty(Convert.ToString(rw["TTVALORF"])))
                        {
                            litTextoMensaje.Text = "Ref:" + Convert.ToString(rw["LD_CLAVE1"]) + " No Tiene Impuesto Asociado";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            break;
                        }
                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(rw["PDPRECIO"]) * Convert.ToDouble(rw["LD_CANTID"]));
                        row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100);
                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                            row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100));

                        row["DTCODDES"] = rw["PDCODDES"];
                        if (Convert.ToString(rw["PDCODDES"]) == "")
                            row["DTCODDES"] = 0;

                        row["DTTOTFCL"] = row["DTTOTFAC"];
                        row["DTSUBTTL"] = row["DTSUBTOT"];
                        row["DTTOTIVL"] = row["DTTOTIVA"];
                        row["DTSUBTTD"] = 0;
                        row["DTTOTDSD"] = 0;
                        row["DTTOTIVD"] = 0;
                        row["DTTOTFCD"] = 0;
                        row["DTSUBTTL"] = 0;
                        row["DTTOTDSL"] = 0;
                        row["DTTOTIVL"] = 0;
                        row["DTTOTFCL"] = 0;
                        row["ARNOMBRE"] = rw["ARNOMBRE"];
                        row["DTESTADO"] = "AC";
                        row["DTCAUSAE"] = ".";
                        row["DTNMUSER"] = Convert.ToString(Session["UserLogon"]);
                        row["DTFECMOD"] = System.DateTime.Today;
                        row["DTFECING"] = System.DateTime.Today;
                        row["DTNROMOV"] = Convert.ToInt32(rw["LD_NRMOV"]);
                        row["DTITMMOV"] = Convert.ToInt32(rw["LD_ITMPAQ"]);
                        row["LOTE"] = 0;

                        tbItems.Rows.Add(row);
                        ln_subtotal += Convert.ToDouble(row["DTSUBTOT"]);
                        ln_impuesto += Convert.ToDouble(row["DTTOTIVA"]);
                        ln_total += Convert.ToDouble(row["DTTOTFAC"]);

                        i++;
                    }

                    (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                    (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                    (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_total;

                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                    Obj = null;
                    ObjF = null;
                    ObjC = null;
                    item_ = null;
                    ObjT = null;
                }
            }
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_pagos_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbPagos;
        }
        protected void rg_pagos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                double ln_Saldo = this.GetSaldo();
                txt_valor.Value = ln_Saldo;
                txt_saldo.Value = ln_Saldo;
                rc_tpago.SelectedValue = "";
                rc_dpago.Enabled = false;
                rv_dpago.Enabled = false;
                txt_soporte.Text = "";
                string script = "function f(){$find(\"" + modalPagos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }
        protected void rc_tpago_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string lc_tpago = ComunBL.GetValorc(null, Convert.ToString(Session["CODEMP"]), "PAGO", Convert.ToString((sender as RadComboBox).SelectedValue));
            ComunBL Obj = new ComunBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                rc_dpago.Enabled = false;
                rv_dpago.Enabled = false;
                if (!string.IsNullOrEmpty(lc_tpago))
                {
                    rc_dpago.Enabled = true;
                    rv_dpago.Enabled = true;
                    rc_dpago.Items.Clear();
                    item.Value = "";
                    item.Text = "Seleccionar";
                    rc_dpago.Items.Add(item);
                    foreach (DataRow rw in Obj.GetTbTablaLista(null, Convert.ToString(Session["CODEMP"]), lc_tpago).Rows)
                    {
                        RadComboBoxItem item_ = new RadComboBoxItem();
                        item_.Value = Convert.ToString(rw["TTCODCLA"]);
                        item_.Text = Convert.ToString(rw["TTDESCRI"]);
                        rc_dpago.Items.Add(item_);
                    }
                    rc_dpago.SelectedValue = "";
                }
                string script = "function f(){$find(\"" + modalPagos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                item = null;
            }
        }
        protected void btn_aceptarpago_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbPagos.NewRow();
            try
            {
                row["PGCODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["PGTIPFAC"] = "";
                row["PGNROFAC"] = 0;
                row["PGNROITM"] = tbPagos.Rows.Count + 1;
                row["PGTIPPAG"] = rc_tpago.SelectedValue;
                row["PGDETTPG"] = rc_dpago.SelectedValue;
                row["PGVLRPAG"] = txt_valor.Value;
                if (txt_valor.Value > txt_saldo.Value)
                    row["PGVLRPAG"] = txt_saldo.Value;
                row["PGSOPORT"] = txt_soporte.Text;
                row["PGSOPFEC"] = System.DateTime.Today;
                row["PGPAGIMP"] = "";
                row["PGESTADO"] = "AC";
                row["PGCAUSAE"] = ".";
                row["PGNMUSER"] = Convert.ToString(Session["UserLogon"]);
                row["PGFECING"] = System.DateTime.Today;
                row["PGFECMOD"] = System.DateTime.Today;
                row["PGNROCAJA"] = "";
                row["DETALLE"] = rc_dpago.Text;
                row["PAGO"] = rc_tpago.Text;

                tbPagos.Rows.Add(row);
                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();
                double ln_Saldo = this.GetSaldo();
                if (ln_Saldo > 0)
                {
                    txt_valor.Value = ln_Saldo;
                    txt_saldo.Value = ln_Saldo;
                    rc_tpago.SelectedValue = "";
                    txt_soporte.Text = "";
                    string script = "function f(){$find(\"" + modalPagos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
            }
        }
        protected void rg_pagos_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var PGNROITM = item.GetDataKeyValue("PGNROITM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbPagos.Rows)
                    {
                        if (Convert.ToInt32(row["PGNROITM"]) == Convert.ToInt32(PGNROITM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbPagos.Rows[pos].Delete();
                    tbPagos.AcceptChanges();
                    foreach (DataRow row in tbPagos.Rows)
                    {
                        row["PGNROITM"] = i;
                        i++;
                    }
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    break;
            }
        }
        [WebMethod]
        public static List<TBTABLAS> GetDetallePagos(string TTCODCLA)
        {
            //FacturaDirecta obj = new FacturaDirecta();
            ComunBL Obj = new ComunBL();
            try
            {
                string lc_tpago = ComunBL.GetValorc(null, "001", "PAGO", Convert.ToString(TTCODCLA));
                return Obj.GetTbTablaListaS(null, "001", lc_tpago);
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
        public double GetSaldo()
        {
            double? ln_total = (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).Value;
            double ln_recaudo = 0;
            try
            {
                foreach (DataRow rw in tbPagos.Rows)
                {
                    ln_recaudo += Convert.ToDouble(rw["PGVLRPAG"]);
                }

                return (Convert.ToDouble(ln_total) - ln_recaudo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void rlv_factura_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            Boolean lb_bandera = false;
            StringBuilder sMensaje = new StringBuilder();
            try
            {
                if (tbItems.Rows.Count == 0)
                {
                    sMensaje.AppendLine("-No Tiene Items");
                    lb_bandera = true;
                }

                if (tbPagos.Rows.Count == 0)
                {
                    sMensaje.AppendLine("-No Tiene Forma de Pago");
                    lb_bandera = true;
                }

                if (this.GetSaldo() > 0)
                {
                    sMensaje.AppendLine("-No Concuerda el Medio de Pago con el Saldo Total del Documento");
                    lb_bandera = true;
                }


                obj_factura.InsertParameters["HDCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;
                obj_factura.InsertParameters["HDAGENTE"].DefaultValue = (e.ListViewItem.FindControl("rc_agente") as RadComboBox).SelectedValue;

                if (!string.IsNullOrEmpty((e.ListViewItem.FindControl("txt_facdev") as RadTextBox).Text))
                    obj_factura.InsertParameters["ind_dev"].DefaultValue = "X";

                if (!string.IsNullOrEmpty((e.ListViewItem.FindControl("txt_separado") as RadTextBox).Text))
                {
                    obj_factura.InsertParameters["ind_dev"].DefaultValue = "Z";
                    obj_factura.InsertParameters["HDNRODEV"].DefaultValue = (e.ListViewItem.FindControl("txt_separado") as RadTextBox).Text;
                    obj_factura.InsertParameters["HDTIPDEV"].DefaultValue = (e.ListViewItem.FindControl("rc_separado") as RadComboBox).SelectedValue;
                }

                if (lb_bandera)
                {
                    litTextoMensaje.Text = sMensaje.ToString();
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    e.Canceled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sMensaje = null;
            }
        }
        protected void obj_factura_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbItems;
            e.InputParameters["tbPagos"] = tbPagos;
            e.InputParameters["tbBalance"] = tbBalance;
        }
        protected void obj_factura_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "", lc_reporte = "";
                string[] retorno = Convert.ToString(e.ReturnValue).Split('-');
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);
                TipoFacturaBL Obj = new TipoFacturaBL();
                try
                {
                    foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + retorno[0] + "'", 0, 0).Rows)
                    {
                        lc_reporte = Convert.ToString(rw["TFFORFAC"]);
                    }
                    if (string.IsNullOrEmpty(lc_reporte))
                    {
                        litTextoMensaje.Text = "No se Encuentra Formato Asociado al Tipo de Factura";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inImp=S&inParametro=inNumero&inValor=" + retorno[1] +
                              "&inParametro=inTipo&inValor=" + retorno[0] + "&inParametro=inCodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inCopia&inValor=1";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=6004&inban=S&inParametro=InConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);

        }
        protected void rlv_factura_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "", lc_reporte = "";
            //url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=6004&inban=S&inParametro=InConsecutivo&inValor=" + rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString();
            TipoFacturaBL Obj = new TipoFacturaBL();
            try
            {
                foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
                {
                    lc_reporte = Convert.ToString(rw["TFFORFAC"]);
                }
                if (string.IsNullOrEmpty(lc_reporte))
                {
                    litTextoMensaje.Text = "No se Encuentra Formato Asociado al Tipo de Factura";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=inNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                              "&inParametro=inTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=inCodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inCopia&inValor=0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                }

                FastReport.Utils.Config.WebMode = true;
                //using (Report report = new Report())
                //{
                //    report.Load("your_report.frx");
                //    //report.RegisterData();
                //    report.Prepare();

                //    // Export report to PDF stream
                //    FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
                //    using (MemoryStream strm = new MemoryStream())
                //    {
                //        report.Export(pdfExport, strm);

                //        // Stream the PDF back to the client as an attachment
                //        Response.ClearContent();
                //        Response.ClearHeaders();
                //        Response.Buffer = true;
                //        Response.ContentType = "Application/PDF";
                //        Response.AddHeader("Content-Disposition", "attachment;filename=report.pdf");

                //        strm.Position = 0;
                //        strm.WriteTo(Response.OutputStream);
                //        Response.End();
                //    }
                //}

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
        protected void btn_eliminar_OnClick(object sender, EventArgs e)
        {
            EstadosBL Obj = new EstadosBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {

                item.Value = "";
                item.Text = "Seleccionar";
                rc_causae.Items.Add(item);

                foreach (DataRow rw in Obj.GetEstados(null, " ETCODEMP=" + Convert.ToString(Session["CODEMP"]) + " AND ETGRPTAB = 'FH' AND ETESTADO = 'AN' ", 0, 0).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Text = Convert.ToString(rw["ETNOMBRE"]);
                    itemi.Value = Convert.ToString(rw["ETCAUSAE"]);
                    rc_causae.Items.Add(itemi);
                    itemi = null;
                }
                rc_causae.SelectedValue = "";

                string script = "function f(){$find(\"" + modalConfirmacion.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                item = null;
            }

        }
        protected void btn_acpetar_anular_OnClick(object sender, EventArgs e)
        {
            string url = "", lc_reporte = "";
            litTextoMensaje.Text = "Documento Anulado  :" + (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue + "-" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text;
            TipoFacturaBL ObjT = new TipoFacturaBL();
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                if ((rlv_factura.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue == "AN")
                {
                    litTextoMensaje.Text = "Factura ya Se Encuentra Anulada!";

                }
                else
                {
                    if ((rlv_factura.Items[0].FindControl("edt_feccie") as RadDatePicker).DbSelectedDate != null)
                    {
                        litTextoMensaje.Text = "Factura ya Se Encuentra Cerrada!";

                    }
                    else
                    {
                        Obj.AnularFacturacionInventario(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                              rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]));

                        foreach (DataRow rw in ObjT.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
                        {
                            lc_reporte = Convert.ToString(rw["TFFORFAC"]);
                        }
                        if (string.IsNullOrEmpty(lc_reporte))
                        {
                            litTextoMensaje.Text = "No se Encuentra Formato Asociado al Tipo de Factura";
                            //string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inImp=S&inParametro=inNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                                  "&inParametro=inTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=inCodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inCopia&inValor=1";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        }
                    }
                }
                string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
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
        protected void btn_filtroLtsEmpaque_OnClick(object sender, EventArgs e)
        {
            string filter = "LH_ESTADO='CE' ";
            if (!string.IsNullOrWhiteSpace(edt_tercero.Text))
                filter += "AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + edt_tercero.Text.ToUpper() + "%'";

            if (!string.IsNullOrWhiteSpace(edt_numero.Text))
                filter += "AND LH_LSTPAQ = " + edt_numero.Text.ToUpper();


            obj_ltaEmpaque.SelectParameters["filter"].DefaultValue = filter;
            rgLtaEmpaque.DataBind();
            string script = "function f(){$find(\"" + modalLstEmpaque.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void txt_identificacion_OnTextChanged(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            ListaPreciosBL ObjL = new ListaPreciosBL();
            RadComboBoxItem item_ = new RadComboBoxItem();

            try
            {
                if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
                {
                    using (IDataReader reader = Obj.GetTercerosR(null, " TRCODNIT='" + (sender as RadTextBox).Text.Trim() + "' ", 0, 0))
                    {
                        while (reader.Read())
                        {
                            ((sender as RadTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(reader["TRCODTER"]);
                            ((sender as RadTextBox).Parent.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(reader["TRNOMBRE"]);
                            ((sender as RadTextBox).Parent.FindControl("txt_apellido") as RadTextBox).Text = Convert.ToString(reader["TRAPELLI"]);
                            ((sender as RadTextBox).Parent.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(reader["TRDIRECC"]);
                            ((sender as RadTextBox).Parent.FindControl("txt_telefono") as RadTextBox).Text = Convert.ToString(reader["TRNROTEL"]);
                            ((sender as RadTextBox).Parent.FindControl("txt_email") as RadTextBox).Text = Convert.ToString(reader["TRCORREO"]);
                            if (!reader.IsDBNull(reader.GetOrdinal("TRLISPRE")))
                            {
                                ((sender as RadTextBox).Parent.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(reader["TRLISPRE"]);
                                foreach (DataRow rx in ObjL.GetListaPrecioHD(null, " P_CLISPRE='" + Convert.ToString(reader["TRLISPRE"]) + "'", 0, 0).Rows)
                                {
                                    gc_tlista = Convert.ToString(rx["P_CTIPLIS"]);
                                }
                            }
                        }
                    }
                }

                item_.Value = "";
                item_.Text = "Seleccionar";

                (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Clear();
                (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(item_);
                foreach (DataRow rw in Obj.GetTerceros(null, " TRINDVEN='S' AND TRESTADO='AC'", 0, 0).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["TRCODTER"]);
                    itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                }


                ((sender as RadTextBox).Parent.FindControl("txt_nombre") as RadTextBox).Focus();

            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            finally
            {
                Obj = null;
                item_ = null;
                ObjL = null;
            }
        }
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (rlv_factura.InsertItem != null)
                {
                    (rlv_factura.InsertItem.FindControl("rqf_tipfac") as RequiredFieldValidator).Validate();
                    (rlv_factura.InsertItem.FindControl("rqf_identificacion") as RequiredFieldValidator).Validate();
                    (rlv_factura.InsertItem.FindControl("rqf_nombre") as RequiredFieldValidator).Validate();


                    if ((rlv_factura.InsertItem.FindControl("rqf_tipfac") as RequiredFieldValidator).IsValid && (rlv_factura.InsertItem.FindControl("rqf_identificacion") as RequiredFieldValidator).IsValid && (rlv_factura.InsertItem.FindControl("rqf_nombre") as RequiredFieldValidator).IsValid)
                    {
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        this.Limpiar();
                    }
                    e.Canceled = true;
                }
            }
        }
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            int i = 1;
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
            DataRow row = tbItems.NewRow();
            try
            {
                rqf_preciolta.Validate();
                rqf_catidad1.Validate();
                rqf_catidad2.Validate();
                //cmpNumbers.Validate();
                rqf_referencia.Validate();
                rqf_preciolta0.Validate();


                //if (rqf_preciolta.IsValid && rqf_catidad1.IsValid && rqf_catidad2.IsValid && cmpNumbers.IsValid && rqf_referencia.IsValid)
                if (rqf_preciolta.IsValid && rqf_catidad1.IsValid && rqf_catidad2.IsValid && rqf_referencia.IsValid && rqf_preciolta0.IsValid)
                {
                    if (!string.IsNullOrWhiteSpace(txt_barras.Text))
                        row["ARDTTEC5"] = txt_barras.Text;
                    else
                        row["ARDTTEC5"] = "0";
                    
                    row["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                    row["DTTIPFAC"] = (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue;
                    row["DTNROFAC"] = 0;
                    row["DTNROITM"] = 0;
                    row["DTPEDIDO"] = 0;
                    row["DTLINNUM"] = 0;
                    row["DTTIPPRO"] = txt_tp.Text;
                    row["DTCLAVE1"] = txt_referencia.Text;
                    row["DTCLAVE2"] = txt_clave2.Text;
                    row["DTCLAVE3"] = txt_clave3.Text;
                    row["DTCLAVE4"] = txt_clave4.Text;
                    row["DTCODCAL"] = ".";
                    row["DTUNDPED"] = "UN";
                    row["DTCANPED"] = txt_cantidad.Value;
                    row["DTCANTID"] = txt_cantidad.Value;
                    row["DTCANKLG"] = 0;
                    row["DTLISPRE"] = "";
                    row["DTPRELIS"] = txt_preciolta.Value;
                    row["C2"] = txt_nc2r.Text;
                    row["C3"] = txt_nc3r.Text;

                    if (gc_tipdcto == 1)// 1 Porcentaje 2 Valor
                        row["DTPRECIO"] = txt_preciolta.Value - (txt_preciolta.Value * txt_dct.Value) / 100;
                    else
                        row["DTPRECIO"] = txt_preciolta.Value - txt_dct.Value;

                    row["DTDESCUE"] = txt_dct.Value;
                    row["DTTOTDES"] = txt_dct.Value;
                    //if (string.IsNullOrEmpty(Convert.ToString(rw["TTVALORF"])))
                    //{
                    //    litTextoMensaje.Text = "Ref:" + Convert.ToString(rw["LD_CLAVE1"]) + " No Tiene Impuesto Asociado";
                    //    mpMensajes.Show();
                    //    break;
                    //} ((Convert.ToDouble(txt_preciolta.Value) * Convert.ToDouble(txt_impf.Value))/100)
                    if (gc_tlista == "2")
                    {
                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(txt_cantidad.Value));
                        row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(txt_impf.Value)) / 100);
                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                            row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(row["DTPRECIO"]) * (Convert.ToDouble(txt_cantidad.Value)) * Convert.ToDouble(txt_impf.Value)) / 100));
                    }
                    else
                    {
                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(txt_cantidad.Value));
                        row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) - (Convert.ToDouble(row["DTSUBTOT"]) / ((Convert.ToDouble(txt_impf.Value) / 100) + 1))));
                        row["DTSUBTOT"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) / ((Convert.ToDouble(txt_impf.Value) / 100) + 1)));
                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                            row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(row["DTPRECIO"]) * (Convert.ToDouble(txt_cantidad.Value)) * Convert.ToDouble(txt_impf.Value)) / 100));
                    }
                    //row["DTCODDES"] = rw["PDCODDES"];
                    //if (Convert.ToString(rw["PDCODDES"]) == "")
                    //    row["DTCODDES"] = 0;

                    row["DTCODDES"] = 0;
                    row["DTTOTFCL"] = row["DTTOTFAC"];
                    row["DTSUBTTL"] = row["DTSUBTOT"];
                    row["DTTOTIVL"] = row["DTTOTIVA"];
                    row["DTSUBTTD"] = 0;
                    row["DTTOTDSD"] = 0;
                    row["DTTOTIVD"] = 0;
                    row["DTTOTFCD"] = 0;
                    row["DTSUBTTL"] = 0;
                    row["DTTOTDSL"] = 0;
                    row["DTTOTIVL"] = 0;
                    row["DTTOTFCL"] = 0;
                    row["ARNOMBRE"] = txt_descripcion.Text;
                    row["DTESTADO"] = "AC";
                    row["DTCAUSAE"] = ".";
                    row["DTNMUSER"] = Convert.ToString(Session["UserLogon"]);
                    row["DTFECMOD"] = System.DateTime.Today;
                    row["DTFECING"] = System.DateTime.Today;
                    row["DTNROMOV"] = 0;
                    row["DTITMMOV"] = 0;
                    row["LOTE"] = 0;
                    row["DEV"] = "N";
                    tbItems.Rows.Add(row);

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw["DTNROITM"] = i;
                        if (Convert.ToString(rw["DEV"]) == "N")
                        {
                            ln_subtotal += Convert.ToDouble(rw["DTSUBTOT"]);
                            ln_impuesto += Convert.ToDouble(rw["DTTOTIVA"]);
                            ln_total += Convert.ToDouble(rw["DTTOTFAC"]);
                        }
                        i++;
                    }

                    (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                    (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                    (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_total;

                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                    this.Limpiar();
                }
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DescuentosBL ObjD = new DescuentosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    if ((rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue == "-1")
                        tbBarras = Obj.GetTbBarras(null, (sender as RadTextBox).Text, null);
                    else
                        tbBarras = Obj.GetTbBarras(null, (sender as RadTextBox).Text, (rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue);

                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_nc2r.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nc3r.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_caninv.Value = Convert.ToDouble(rw["BBCANTID"]);
                            txt_impf.Value = Convert.ToDouble(rw["ARCDIMPF"]);
                            txt_preciolta.Value = Convert.ToDouble(rw["PRECIO"]);
                            //txt_dct.Value = Convert.ToDouble(rw["DESCUENTO"]);
                            ObjD.GetVlrDctoTF(Convert.ToString(rw["BTIPPRO"]), Convert.ToString(rw["BCLAVE1"]), Convert.ToString(rw["BCLAVE2"]), Convert.ToString(rw["BCLAVE3"]), Convert.ToString(rw["BCLAVE4"]), (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToDouble(rw["PRECIO"]), (rlv_factura.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text,0);
                            txt_dct.Value = ObjD.VlrDcto;
                            gc_coddcto = ObjD.CodDcto;
                            gc_tipdcto = ObjD.TipDcto;
                            txt_cantidad.Value = 1;
                        }
                        this.ConfigLinea();
                        if (!rc_lote.Visible)
                            btn_agregar_Aceptar(sender, e);
                        else
                            this.CargarLote();

                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        txt_barras.Focus();
                    }
                    else
                    {
                        //string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        //txt_barras.Focus();

                        litTextoMensaje.Text = "Codigo Barras Invalido!";
                        string script_ = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script_, true);

                    }
                }
                catch (Exception ex)
                {
                    litTextoMensaje.Text = ex.Message; //"No Cuenta Con Permisos Para Venta de la Linea";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    //throw ex;
                }
                finally
                {
                    //obj_articulos = null;
                    tbBarras = null;
                    ObjD = null;
                    Obj = null;
                }
            }
        }
        private void Limpiar()
        {
            txt_tp.Text = "";
            txt_referencia.Text = "";
            txt_clave2.Text = "";
            txt_clave3.Text = "";
            txt_clave4.Text = "";
            txt_nc2r.Text = "";
            txt_nc3r.Text = "";
            txt_descripcion.Text = "";
            txt_barras.Text = "";
            txt_cantidad.Value = 0;
            txt_caninv.Value = 0;
            txt_preciolta.Value = 0;

        }
        private void ConfigLinea()
        {
            Boolean lb_ind = false;
            TipoProductosBL Obj = new TipoProductosBL();
            try
            {
                rc_lote.Visible = false;
                lbl_lote.Visible = false;
                RequiredFieldValidator11.Enabled = false;

                using (IDataReader reader = Obj.GetTipoProductoxBodegaTFR(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue, txt_tp.Text))
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                        {
                            rc_lote.Visible = true;
                            lbl_lote.Visible = true;
                            RequiredFieldValidator11.Enabled = true;
                        }
                        lb_ind = true;
                    }
                }
                if (!lb_ind)
                    throw new System.ArgumentException("Linea No Se Encuentra Activa para El Tipo de Factura");

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
        private void CargarLote()
        {
            RadComboBoxItem item_ = new RadComboBoxItem();
            MovimientosBL Obj = new MovimientosBL();
            try
            {
                rc_lote.Items.Clear();
                item_.Value = "-1";
                item_.Text = "Seleccionar";
                rc_lote.Items.Add(item_);
                foreach (DataRow rw in Obj.GetLotesTF(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["BLCDLOTE"]);
                    itemi.Text = "Lote: " + Convert.ToString(rw["BLCDLOTE"]) + " Can: " + Convert.ToString(rw["BLCANTID"]);
                    rc_lote.Items.Add(itemi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item_ = null;
                Obj = null;
            }
        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {

            obj_articulos.SelectParameters["inBodega"].DefaultValue = g_bodega;
            obj_articulos.SelectParameters["LT"].DefaultValue = (rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;
            //((sender as ImageButton).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();
            //((sender as ImageButton).Parent.FindControl("mpArticulos") as ModalPopupExtender).Show();

            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            obj_articulos.SelectParameters["inBodega"].DefaultValue = g_bodega;
            obj_articulos.SelectParameters["LT"].DefaultValue = (rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;           
            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;


            //rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            rgConsultaArticulos.DataBind();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;

                try
                {
                    //(rlv_liquidacion.Items[0].FindControl("edt_cie") as RadTextBox).Text = Convert.ToString(item["CIE_CODIGO"].Text);
                    //(rlv_liquidacion.Items[0].FindControl("edt_diagnostico") as RadTextBox).Text = Convert.ToString(item["CIE_NOMBRE"].Text);                    
                    txt_tp.Text = Convert.ToString(item["ARTIPPRO"].Text);
                    txt_referencia.Text = Convert.ToString(item["ARCLAVE1"].Text);
                    txt_clave2.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                    txt_clave3.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                    txt_clave4.Text = (item.FindControl("txt_fclave4") as RadTextBox).Text;                    
                    txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);

                    txt_caninv.Value = Convert.ToDouble(item["BBCANTID"].Text);
                    txt_preciolta.Value = Convert.ToDouble(item["PRECIO"].Text);
                    txt_dct.Value = Convert.ToDouble(item["DESCUENTO"].Text);


                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    txt_cantidad.Focus();


                    this.ConfigLinea();
                    if (rc_lote.Visible)                        
                        this.CargarLote();

                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                }
            }
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
            switch (e.CommandName)
            {
                case "Delete":
                    var DTNROITM = item.GetDataKeyValue("DTNROITM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["DTNROITM"]) == Convert.ToInt32(DTNROITM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw["DTNROITM"] = i;
                        ln_subtotal += Convert.ToDouble(rw["DTSUBTOT"]);
                        ln_impuesto += Convert.ToDouble(rw["DTTOTIVA"]);
                        ln_total += Convert.ToDouble(rw["DTTOTFAC"]);
                        i++;
                    }

                    (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                    (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                    (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_total;

                    tbPagos.Rows.Clear();
                    ((source as RadGrid).Parent.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                    ((source as RadGrid).Parent.FindControl("rg_pagos") as RadGrid).DataBind();
                    break;
            }
        }
        protected void rlv_factura_PreRender(object sender, EventArgs e)
        {
            if ((sender as RadListView).InsertItem != null && string.IsNullOrEmpty(((sender as RadListView).InsertItem.FindControl("txt_identificacion") as RadTextBox).Text))
                ((sender as RadListView).InsertItem.FindControl("txt_identificacion") as RadTextBox).Focus();
        }
        protected void rc_tipfac_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TipoFacturaBL Obj = new TipoFacturaBL();
            ListaPreciosBL ObjL = new ListaPreciosBL();
            try
            {
                if ((sender as RadComboBox).SelectedValue != "-1")
                {
                    foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + (sender as RadComboBox).SelectedValue + "'", 0, 0).Rows)
                    {
                        ((sender as RadComboBox).Parent.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(rw["TFLSTPRE"]);
                        foreach (DataRow rx in ObjL.GetListaPrecioHD(null, " P_CLISPRE='" + Convert.ToString(rw["TFLSTPRE"]) + "'", 0, 0).Rows)
                        {
                            gc_tlista = Convert.ToString(rx["P_CTIPLIS"]);
                        }
                        g_bodega = Convert.ToString(rw["BDBODEGA"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjL = null;
            }
        }
        protected void btn_filtro_fac_dev_OnClick(object sender, EventArgs e)
        {
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                tbItemsDev = Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipfacdev.SelectedValue), Convert.ToInt32(txt_nrofacdev.Text));
                rg_items_dev.DataSource = tbItemsDev;
                rg_items_dev.DataBind();

                string script = "function f(){$find(\"" + modalFactura.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void btn_buscar_fac_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalFactura.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_aceptar_facdev_OnClick(object sender, EventArgs e)
        {
            double ln_total = 0;
            Boolean lb_bandera = true;
            try
            {
                foreach (GridDataItem item in rg_items_dev.Items)
                {
                    int i = 1, ln_codigo = Convert.ToInt32(item["DTNROITM"].Text);

                    if ((item.FindControl("chk_item") as CheckBox).Checked)
                    {
                        lb_bandera = false;
                        foreach (DataRow row in tbItemsDev.Rows)
                        {
                            if (Convert.ToInt32(row["DTNROITM"]) == ln_codigo)
                            {
                                DataRow rw = tbItems.NewRow();
                                try
                                {
                                    rw["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                    rw["DTTIPFAC"] = (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue;
                                    rw["DTNROFAC"] = 0;
                                    rw["DTNROITM"] = 0;
                                    rw["DTPEDIDO"] = 0;
                                    rw["DTLINNUM"] = 0;
                                    rw["DTTIPPRO"] = row["DTTIPPRO"];
                                    rw["DTCLAVE1"] = row["DTCLAVE1"];
                                    rw["DTCLAVE2"] = row["DTCLAVE2"];
                                    rw["DTCLAVE3"] = row["DTCLAVE3"];
                                    rw["DTCLAVE4"] = row["DTCLAVE4"];
                                    rw["DTCODCAL"] = ".";
                                    rw["DTUNDPED"] = "UN";
                                    rw["DTCANPED"] = row["DTCANPED"];
                                    rw["DTCANTID"] = row["DTCANTID"];
                                    rw["DTCANKLG"] = 0;
                                    rw["DTLISPRE"] = "";
                                    rw["DTPRELIS"] = row["DTPRELIS"];
                                    rw["DTPRECIO"] = row["DTPRELIS"];
                                    rw["DTDESCUE"] = 0;
                                    rw["DTTOTDES"] = 0;
                                    rw["DTSUBTOT"] = Convert.ToDouble(row["DTSUBTOT"]) * -1;
                                    rw["DTTOTIVA"] = Convert.ToDouble(row["DTTOTIVA"]) * -1;
                                    rw["DTTOTFAC"] = Convert.ToDouble(row["DTTOTFAC"]) * -1;
                                    rw["DTTOTIVA"] = Convert.ToDouble(row["DTTOTIVA"]) * -1;
                                    rw["DTCODDES"] = 0;
                                    rw["DTTOTFCL"] = Convert.ToDouble(row["DTTOTFAC"]) * -1;
                                    rw["DTSUBTTL"] = Convert.ToDouble(row["DTSUBTOT"]) * -1;
                                    rw["DTTOTIVL"] = Convert.ToDouble(row["DTTOTIVA"]) * -1;
                                    rw["DTSUBTTD"] = 0;
                                    rw["DTTOTDSD"] = 0;
                                    rw["DTTOTIVD"] = 0;
                                    rw["DTTOTFCD"] = 0;
                                    rw["DTSUBTTL"] = 0;
                                    rw["DTTOTDSL"] = 0;
                                    rw["DTTOTIVL"] = 0;
                                    rw["DTTOTFCL"] = 0;
                                    rw["ARNOMBRE"] = row["ARNOMBRE"];
                                    rw["DTESTADO"] = "AC";
                                    rw["DTCAUSAE"] = ".";
                                    rw["DTNMUSER"] = Convert.ToString(Session["UserLogon"]);
                                    rw["DTFECMOD"] = System.DateTime.Today;
                                    rw["DTFECING"] = System.DateTime.Today;
                                    rw["DTNROMOV"] = 0;
                                    rw["DTITMMOV"] = 0;
                                    rw["LOTE"] = 0;
                                    rw["DEV"] = 'S';

                                    tbItems.Rows.Add(rw);

                                    foreach (DataRow rx in tbItems.Rows)
                                    {
                                        rx["DTNROITM"] = i;
                                        i++;
                                    }

                                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                                    (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    rw = null;
                                }
                            }
                        }
                    }
                }

                if (lb_bandera)
                    throw new System.ArgumentException("¡No ha Seleccionado Items a Devolver!");

                foreach (DataRow rx in tbItems.Rows)
                {
                    ln_total += Math.Abs(Convert.ToDouble(rx["DTTOTFAC"]));
                }

                DataRow rp = tbPagos.NewRow();
                rp["PGCODEMP"] = Convert.ToString(Session["CODEMP"]);
                rp["PGTIPFAC"] = "";
                rp["PGNROFAC"] = 0;
                rp["PGNROITM"] = tbPagos.Rows.Count + 1;
                rp["PGTIPPAG"] = "05";
                rp["PGDETTPG"] = ".";
                rp["PGVLRPAG"] = ln_total;
                rp["PGSOPORT"] = ".";
                rp["PGSOPFEC"] = System.DateTime.Today;
                rp["PGPAGIMP"] = "";
                rp["PGESTADO"] = "AC";
                rp["PGCAUSAE"] = ".";
                rp["PGNMUSER"] = Convert.ToString(Session["UserLogon"]);
                rp["PGFECING"] = System.DateTime.Today;
                rp["PGFECMOD"] = System.DateTime.Today;
                rp["PGNROCAJA"] = "";
                rp["DETALLE"] = "";
                rp["PAGO"] = "NOTAS CREDITO";

                tbPagos.Rows.Add(rp);
                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();
                (rlv_factura.InsertItem.FindControl("txt_facdev") as RadTextBox).Text = txt_nrofacdev.Text;
                (rlv_factura.InsertItem.FindControl("rc_facdev") as RadComboBox).SelectedValue = rc_tipfacdev.SelectedValue;
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void ColorGrilla(object sender, EventArgs e, RadGrid grilla)
        {
            string aValue = "";

            foreach (GridDataItem item in grilla.Items)
            {
                aValue = ((Label)item.FindControl("lbl_devolucion")).Text;
                switch (aValue)
                {
                    case "S":
                        item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                        break;

                }
            }
        }
        protected void rg_items_PreRender(object sender, EventArgs e)
        {
            ColorGrilla(sender, e, (sender as RadGrid));
        }
        protected void lnk_devolucion_Click(object sender, EventArgs e)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Devoluciones.aspx?Documento=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_buscar_sep_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalSeparado.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtro_sep_OnClick(object sender, EventArgs e)
        {
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                string lc_estado = Obj.GetEstadoFatcuraHD(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipsep.SelectedValue), Convert.ToInt32(txt_nrosep.Text));
                if (lc_estado == "FA")
                {
                    litTextoMensaje.Text = " Nro Separado: " + Convert.ToString(rc_tipsep.SelectedValue) + "-" + txt_nrosep.Text + " se Encuentra Facturado!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else
                {
                    if (lc_estado == "AN")
                    {
                        litTextoMensaje.Text = " Nro Separado: " + Convert.ToString(rc_tipsep.SelectedValue) + "-" + txt_nrosep.Text + " se Encuentra Anulado!";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else
                    {
                        tbItemsDev = Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipsep.SelectedValue), Convert.ToInt32(txt_nrosep.Text));
                        rgDetalleSep.DataSource = tbItemsDev;
                        rgDetalleSep.DataBind();
                    }
                }

                string script = "function f(){$find(\"" + modalSeparado.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void btn_aceptar_sep_OnClick(object sender, EventArgs e)
        {
            double ln_total = 0, ln_tofac = 0, ln_subfac = 0, ln_ivafac = 0;
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                foreach (DataRow row in tbItemsDev.Rows)
                {
                    DataRow rw = tbItems.NewRow();
                    try
                    {
                        rw["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rw["DTTIPFAC"] = (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue;
                        rw["DTNROFAC"] = 0;
                        rw["DTNROITM"] = row["DTNROITM"];
                        rw["DTPEDIDO"] = 0;
                        rw["DTLINNUM"] = 0;
                        rw["DTTIPPRO"] = row["DTTIPPRO"];
                        rw["DTCLAVE1"] = row["DTCLAVE1"];
                        rw["DTCLAVE2"] = row["DTCLAVE2"];
                        rw["DTCLAVE3"] = row["DTCLAVE3"];
                        rw["DTCLAVE4"] = row["DTCLAVE4"];
                        rw["DTCODCAL"] = ".";
                        rw["DTUNDPED"] = "UN";
                        rw["DTCANPED"] = row["DTCANPED"];
                        rw["DTCANTID"] = row["DTCANTID"];
                        rw["DTCANKLG"] = 0;
                        rw["DTLISPRE"] = "";
                        rw["DTPRELIS"] = row["DTPRELIS"];
                        rw["DTPRECIO"] = row["DTPRELIS"];
                        rw["DTDESCUE"] = 0;
                        rw["DTTOTDES"] = 0;
                        rw["DTSUBTOT"] = Convert.ToDouble(row["DTSUBTOT"]);
                        rw["DTTOTIVA"] = Convert.ToDouble(row["DTTOTIVA"]);
                        rw["DTTOTFAC"] = Convert.ToDouble(row["DTTOTFAC"]);
                        rw["DTTOTIVA"] = Convert.ToDouble(row["DTTOTIVA"]);
                        rw["DTCODDES"] = 0;
                        rw["DTTOTFCL"] = Convert.ToDouble(row["DTTOTFAC"]);
                        rw["DTSUBTTL"] = Convert.ToDouble(row["DTSUBTOT"]);
                        rw["DTTOTIVL"] = Convert.ToDouble(row["DTTOTIVA"]);
                        rw["DTSUBTTD"] = 0;
                        rw["DTTOTDSD"] = 0;
                        rw["DTTOTIVD"] = 0;
                        rw["DTTOTFCD"] = 0;
                        rw["DTSUBTTL"] = 0;
                        rw["DTTOTDSL"] = 0;
                        rw["DTTOTIVL"] = 0;
                        rw["DTTOTFCL"] = 0;
                        rw["ARNOMBRE"] = row["ARNOMBRE"];
                        rw["DTESTADO"] = "AC";
                        rw["DTCAUSAE"] = ".";
                        rw["DTNMUSER"] = Convert.ToString(Session["UserLogon"]);
                        rw["DTFECMOD"] = System.DateTime.Today;
                        rw["DTFECING"] = System.DateTime.Today;
                        rw["DTNROMOV"] = 0;
                        rw["DTITMMOV"] = 0;
                        rw["LOTE"] = 0;
                        rw["DEV"] = "N";

                        ln_subfac = Convert.ToDouble(row["DTSUBTOT"]);
                        ln_ivafac = Convert.ToDouble(row["DTTOTIVA"]);
                        ln_tofac += Convert.ToDouble(row["DTTOTFAC"]);


                        tbItems.Rows.Add(rw);

                        (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        rw = null;
                    }
                }

                foreach (DataRow rt in Obj.GetPagos(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipsep.SelectedValue), Convert.ToInt32(txt_nrosep.Text)).Rows)
                {
                    ln_total += Convert.ToDouble(rt["PGVLRPAG"]);
                }

                DataRow rp = tbPagos.NewRow();
                rp["PGCODEMP"] = Convert.ToString(Session["CODEMP"]);
                rp["PGTIPFAC"] = "";
                rp["PGNROFAC"] = 0;
                rp["PGNROITM"] = tbPagos.Rows.Count + 1;
                rp["PGTIPPAG"] = "14";
                rp["PGDETTPG"] = ".";
                rp["PGVLRPAG"] = ln_total;
                rp["PGSOPORT"] = ".";
                rp["PGSOPFEC"] = System.DateTime.Today;
                rp["PGPAGIMP"] = "";
                rp["PGESTADO"] = "AC";
                rp["PGCAUSAE"] = ".";
                rp["PGNMUSER"] = Convert.ToString(Session["UserLogon"]);
                rp["PGFECING"] = System.DateTime.Today;
                rp["PGFECMOD"] = System.DateTime.Today;
                rp["PGNROCAJA"] = "";
                rp["DETALLE"] = "";
                rp["PAGO"] = "ANTICIPO-SEPARADO";

                tbPagos.Rows.Add(rp);

                (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).Value = ln_tofac;
                (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).Value = ln_ivafac;
                (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).Value = ln_subfac;

                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();
                (rlv_factura.InsertItem.FindControl("txt_separado") as RadTextBox).Text = txt_nrosep.Text;
                (rlv_factura.InsertItem.FindControl("rc_separado") as RadComboBox).SelectedValue = rc_tipsep.SelectedValue;
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rc_pais_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            RadComboBoxItem item = new RadComboBoxItem();
            ComunBL Obj = new ComunBL();
            try
            {
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                using (IDataReader reader = Obj.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (sender as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                        itemi.Text = Convert.ToString(reader["CIUDAD"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                Obj = null;
            }
        }
        protected void rc_lstprecio_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ListaPreciosBL ObjL = new ListaPreciosBL();
            try
            {
                gc_tlista = null;
                if (Convert.ToString((sender as RadComboBox).SelectedValue) != "-1")
                {
                    foreach (DataRow rx in ObjL.GetListaPrecioHD(null, " P_CLISPRE='" + Convert.ToString((sender as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
                    {
                        gc_tlista = Convert.ToString(rx["P_CTIPLIS"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjL = null;
            }
        }
    }
}