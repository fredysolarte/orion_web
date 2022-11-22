using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using XUSS.BLL.Facturacion;
using XUSS.BLL.Pedidos;
using XUSS.BLL.Comun;
using XUSS.BLL.Terceros;
using XUSS.BLL.Parametros;
using System.Configuration;


namespace XUSS.WEB.Facturacion
{
    public partial class FacturacionSegmentos : System.Web.UI.Page
    {
        private string gc_terpago
        {
            set { ViewState["gc_terpago"] = value; }
            get { return Convert.ToString(ViewState["gc_terpago"]); }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbPagos
        {
            set { ViewState["tbPagos"] = value; }
            get { return ViewState["tbPagos"] as DataTable; }
        }
        private DataTable tbBalance
        {
            set { ViewState["tbBalance"] = value; }
            get { return ViewState["tbBalance"] as DataTable; }
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
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
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
            string filtro = "AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(1))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND TRNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text))
                filtro += " AND TRNOMBR2 LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text))
                filtro += " AND HDNROFAC = '" + (((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text))
                filtro += " AND LH_LSTPAQ = '" + (((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue))
                filtro += " AND HDTIPFAC = '" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text))
                filtro += " AND LH_LSTPAQ IN (SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_PEDIDO =  " + (((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text + ")";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_factura.SelectParameters["filter"].DefaultValue = filtro;
            rlv_factura.DataBind();
            if ((rlv_factura.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
               
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
            RadComboBoxItem item_ = new RadComboBoxItem();
            try
            {

                tbItems = Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((e.Item.FindControl("rc_tipfac") as RadComboBox).SelectedValue), Convert.ToInt32((e.Item.FindControl("txt_nrofac") as RadTextBox).Text));
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
                //Cargar Sucursales
                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                item_.Value = "";
                item_.Text = "Seleccionar";
                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                using (IDataReader reader = ObjT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["HDCODCLI"])))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                        itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                    }
                }
                (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = Convert.ToString(fila["HDCODSUC"]);
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
                item_ = null;
            }
        }
        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            obj_ltaEmpaque.SelectParameters["filter"].DefaultValue = "LH_ESTADO in ('CE') ";
            rgLtaEmpaque.DataBind();
            //mpLtaEmpaque.Show();
            string script = "function f(){$find(\"" + mpListaEmpaque.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgLtaEmpaque_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int i = 1;
                string lc_ltaprecio = "", lc_indimpf = "S"; 
                double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
                LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                FacturacionBL ObjF = new FacturacionBL();
                TipoFacturaBL ObjTF = new TipoFacturaBL();
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

                    //Valida si tiene Remision Asociada
                    foreach (DataRow rw in ObjF.GetFacturaHD(null, " HDESTADO NOT IN ('AN') AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(5)) AND LH_LSTPAQ="+ Convert.ToString(item["LH_LSTPAQ"].Text), 0, 0).Rows)
                    {
                        (rlv_factura.InsertItem.FindControl("txt_tiprem") as RadTextBox).Text = Convert.ToString(rw["HDTIPFAC"]);
                        (rlv_factura.InsertItem.FindControl("txt_nrorem") as RadTextBox).Text = Convert.ToString(rw["HDNROFAC"]);
                    }

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
                        //TERMINOS DE PAGO
                        gc_terpago = Convert.ToString(rw["TRTERPAG"]);

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

                        //Cargar Sucursales
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        using (IDataReader reader = ObjT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rw["PHCODCLI"])))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                            }
                        }
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue = Convert.ToString(rw["PHCODSUC"]);
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

                    foreach (DataRow rx in ObjTF.GetTiposFactura(null, " TFTIPFAC ='" + (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'", 0, 0).Rows)
                        lc_indimpf = Convert.ToString(rx["TFINIMPF"]);


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
                            //mpMensajes.Show();
                            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            break;
                        }
                        if (ConfigurationManager.AppSettings["ROUND"] == "S")
                        {
                            row["DTSUBTOT"] = Convert.ToDouble(rw["PDPRECIO"]) * Convert.ToDouble(rw["LD_CANTID"]);
                            
                            row["DTTOTIVA"] = 0;
                            if (lc_indimpf == "S")
                                row["DTTOTIVA"] = (Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100;

                            row["DTTOTFAC"] = Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]);
                            if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                                row["DTTOTIVA"] = 0;
                                //row["DTTOTIVA"] = ((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100);
                        }
                        else
                        {
                            row["DTSUBTOT"] = Math.Round(Convert.ToDouble(rw["PDPRECIO"]) * Convert.ToDouble(rw["LD_CANTID"]));
                            
                            row["DTTOTIVA"] = 0;
                            if (lc_indimpf == "S")
                                row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100);

                            row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                            if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                                row["DTTOTIVA"] = 0;
                                //row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100));
                        }
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
                        row["DEV"] = "N";

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
                    ObjTF = null;
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

                if (gc_terpago != "-1")
                {
                    string[] words = gc_terpago.Split('-');
                    rc_tpago.SelectedValue = words[0];
                    if (words.Length > 1 && !string.IsNullOrEmpty(words[1]))
                    {
                        ComunBL Obj = new ComunBL();
                        RadComboBoxItem item = new RadComboBoxItem();
                        try
                        {
                            string lc_tpago = ComunBL.GetValorc(null, Convert.ToString(Session["CODEMP"]), "PAGO", Convert.ToString(words[0]));
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
                                rc_dpago.SelectedValue = words[1];
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
                        }
                    }
                }

                if (ln_Saldo != 0)
                {
                    string script = "function f(){$find(\"" + modalPagos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
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
                //mpPagos.Show();
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
                row["PGSOPORT"] = ".";
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
                    //mpPagos.Show();
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
        protected double GetSaldo()
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
                obj_factura.InsertParameters["HDCODSUC"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;

                if (lb_bandera)
                {
                    litTextoMensaje.Text = sMensaje.ToString();
                    //mpMensajes.Show();
                    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(retorno[1]);
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
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        //url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inClo=S&inParametro=inNumero&inValor=" + retorno[1] +
                        //      "&inParametro=InTipo&inValor=" + retorno[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);

                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8006&inban=S&inClo=S&inParametro=inNumero&inValor=" + retorno[1] +
                                  "&inParametro=inTipo&inValor=" + retorno[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);

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
            //mpMensajes.Show();
            string script_ = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script_, true);

        }
        protected void rlv_factura_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                    //mpMensajes.Show();
                    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=inNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                          "&inParametro=inTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);
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
                //mpConfirmacion.Show();
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
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                foreach (DataRow row in Obj.GetFacturaHD(null, " HDTIPDEV ='" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "' AND HDNRODEV=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text, 0, 0).Rows)
                {
                    throw new System.ArgumentException("No se puede Anular,Tiene Devoluciones Asociadas!");
                }
                if (chk_inltaemp.Checked)
                {
                    Obj.AnularFacturacionLstEmpaque(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                                    rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]), Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_lta") as RadTextBox).Text));
                    litTextoMensaje.Text = "¡Documento + Lista de Empaque Anulado de Manera Correcta!";
                }
                else
                {
                    Obj.AnularFacturacion(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                          rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]), Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_lta") as RadTextBox).Text));
                    litTextoMensaje.Text = "¡Documento Anulado de Manera Correcta!";
                }

                //mpMensajes.Show();
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        protected void btn_filtroLtsEmpaque_OnClick(object sender, EventArgs e)
        {
            string filter = "LH_ESTADO IN ('CE','FA') ";
            if (!string.IsNullOrWhiteSpace(edt_tercero.Text))
                filter += "AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + edt_tercero.Text.ToUpper() + "%'";

            if (!string.IsNullOrWhiteSpace(edt_numero.Text))
                filter += "AND LH_LSTPAQ = " + edt_numero.Text.ToUpper();


            obj_ltaEmpaque.SelectParameters["filter"].DefaultValue = filter;
            rgLtaEmpaque.DataBind();
            //mpLtaEmpaque.Show();
            string script = "function f(){$find(\"" + mpListaEmpaque.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void lnk_lstempaque_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/LtaEmpaque/LtaEmpaque.aspx?Empaque=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void lnk_pedido_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Pedidos/Pedidos.aspx?Pedido=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void lnk_remision_Click(object sender, EventArgs e)
        {            
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/RemisionSegmento.aspx?Documento=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_filtrorem_Click(object sender, EventArgs e)
        {
            string filtro = "AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(5))";

            if (!string.IsNullOrWhiteSpace(txt_nomrem.Text))
                filtro += " AND (TRNOMBRE+ISNULL(TRNOMBR2,'')) LIKE '%" + txt_nomrem.Text + "%'";            

            if (!string.IsNullOrWhiteSpace(txt_nrorem.Text))
                filtro += " AND HDNROFAC = " + txt_nrorem.Text;            

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_remision.SelectParameters["filter"].DefaultValue = filtro;
            rg_remision.DataBind();

            string script = "function f(){$find(\"" + mpRemisiones.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rg_remision_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int i = 1;
            string lc_ltaprecio = "", lc_indimpf ="S";
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;            
            FacturacionBL ObjF = new FacturacionBL();
            TipoFacturaBL ObjTF = new TipoFacturaBL(); 
            GridDataItem item = (GridDataItem)e.Item;
            ComunBL ObjC = new ComunBL();
            TercerosBL ObjT = new TercerosBL();
            RadComboBoxItem item_ = new RadComboBoxItem();
            try
            {
                item_.Value = "";
                item_.Text = "Seleccionar";

                //caragar Cabecera

                (rlv_factura.InsertItem.FindControl("txt_tiprem") as RadTextBox).Text = Convert.ToString(item["HDTIPFAC"].Text);
                (rlv_factura.InsertItem.FindControl("txt_nrorem") as RadTextBox).Text = Convert.ToString(item["HDNROFAC"].Text);

                foreach (DataRow rw in ObjF.GetFacturaHD(null, "HDNROFAC=" + Convert.ToString(item["HDNROFAC"].Text) + " AND HDTIPFAC='"+ Convert.ToString(item["HDTIPFAC"].Text) + "'", 0, 0).Rows)
                {

                    (rlv_factura.InsertItem.FindControl("txt_lta") as RadTextBox).Text = Convert.ToString(rw["LH_LSTPAQ"]);
                    (rlv_factura.InsertItem.FindControl("txt_pedido") as RadTextBox).Text = Convert.ToString(rw["PEDIDO"]);

                    (rlv_factura.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(rw["HDCODNIT"]);
                    (rlv_factura.InsertItem.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(rw["HDCODCLI"]);
                    (rlv_factura.InsertItem.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(rw["TRNOMBRE"]);
                    (rlv_factura.InsertItem.FindControl("txt_apellido") as RadTextBox).Text = Convert.ToString(rw["TRAPELLI"]);
                    (rlv_factura.InsertItem.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(rw["TRDIRECC"]);
                    (rlv_factura.InsertItem.FindControl("txt_telefono") as RadTextBox).Text = Convert.ToString(rw["TRNROTEL"]);
                    (rlv_factura.InsertItem.FindControl("txt_email") as RadTextBox).Text = Convert.ToString(rw["TRCORREO"]);
                    (rlv_factura.InsertItem.FindControl("rc_pais") as RadComboBox).SelectedValue = Convert.ToString(rw["TRCDPAIS"]);
                    //TERMINOS DE PAGO
                    gc_terpago = Convert.ToString(rw["TRTERPAG"]);

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

                    //Cargar Sucursales
                    (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                    item_.Value = "";
                    item_.Text = "Seleccionar";
                    (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                    using (IDataReader reader = ObjT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rw["HDCODCLI"])))
                    {
                        while (reader.Read())
                        {
                            RadComboBoxItem itemi = new RadComboBoxItem();
                            itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                            itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                            (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                        }
                    }
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue = Convert.ToString(rw["HDCODSUC"]);

                    //lc_ltaprecio = Convert.ToString(rw["PHLISPRE"]);
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

                foreach (DataRow rx in ObjTF.GetTiposFactura(null, " TFTIPFAC ='" + (rlv_factura.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'", 0, 0).Rows)
                    lc_indimpf = Convert.ToString(rx["TFINIMPF"]);

                foreach (DataRow rw in ObjF.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item["HDTIPFAC"].Text) , Convert.ToInt32(item["HDNROFAC"].Text)).Rows)
                {
                    DataRow row = tbItems.NewRow();

                    row["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                    row["DTTIPFAC"] = "";
                    row["DTNROFAC"] = 0;
                    row["DTNROITM"] = Convert.ToInt32(rw["DTNROITM"]);
                    row["DTPEDIDO"] = (rlv_factura.InsertItem.FindControl("txt_pedido") as RadTextBox).Text;
                    row["DTLINNUM"] = Convert.ToInt32(rw["DTLINNUM"]);
                    row["DTTIPPRO"] = Convert.ToString(rw["DTTIPPRO"]);
                    row["DTCLAVE1"] = Convert.ToString(rw["DTCLAVE1"]);
                    row["DTCLAVE2"] = Convert.ToString(rw["DTCLAVE2"]);
                    row["DTCLAVE3"] = Convert.ToString(rw["DTCLAVE3"]);
                    row["DTCLAVE4"] = Convert.ToString(rw["DTCLAVE4"]);
                    row["DTCODCAL"] = ".";
                    row["DTUNDPED"] = "UN";
                    row["DTCANPED"] = Convert.ToDouble(rw["DTCANTID"]);
                    row["DTCANTID"] = Convert.ToDouble(rw["DTCANTID"]);
                    row["DTCANKLG"] = 0;
                    row["DTLISPRE"] = lc_ltaprecio;
                    row["DTPRELIS"] = Convert.ToDouble(rw["DTPRELIS"]);
                    row["DTPRECIO"] = Convert.ToDouble(rw["DTPRECIO"]);
                    row["DTDESCUE"] = Convert.ToDouble(rw["DTDESCUE"]);
                    row["DTTOTDES"] = Convert.ToDouble(rw["DTDESCUE"]);
                    if (string.IsNullOrEmpty(Convert.ToString(rw["TTVALORF"])))
                    {
                        litTextoMensaje.Text = "Ref:" + Convert.ToString(rw["DTCLAVE1"]) + " No Tiene Impuesto Asociado";
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        break;
                    }
                    if (ConfigurationManager.AppSettings["ROUND"] == "S")
                    {
                        row["DTSUBTOT"] = Convert.ToDouble(rw["DTPRECIO"]) * Convert.ToDouble(rw["DTCANTID"]);
                        
                        row["DTTOTIVA"] = 0;
                        if (lc_indimpf == "S")
                            row["DTTOTIVA"] = (Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100;

                        row["DTTOTFAC"] = Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]);
                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                            row["DTTOTIVA"] = 0;
                        //row["DTTOTIVA"] = ((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100);
                    }
                    else
                    {
                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(rw["DTPRECIO"]) * Convert.ToDouble(rw["DTCANTID"]));
                        
                        row["DTTOTIVA"] = 0;
                        if (lc_indimpf == "S")
                            row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100);

                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                            row["DTTOTIVA"] = 0;
                        //row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100));
                    }
                    row["DTCODDES"] = rw["DTCODDES"];
                    if (Convert.ToString(rw["DTCODDES"]) == "")
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
                    row["DTNROMOV"] = Convert.ToInt32(rw["DTNROMOV"]);
                    row["DTITMMOV"] = Convert.ToInt32(rw["DTITMMOV"]);
                    row["LOTE"] = 0;
                    row["DEV"] = "N";

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
                ObjF = null;
                ObjC = null;
                item_ = null;
                ObjT = null;
                ObjTF = null;
            }
        }    
        protected void btn_buscarrem_Click(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + mpRemisiones.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
            }
        }

        protected void rlv_factura_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ViewState["isClickInsert"]))
            {
                if (((sender as RadListView).InsertItem.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate == null)
                {
                    ((sender as RadListView).InsertItem.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate = System.DateTime.Now;
                }
            }
        }
    }
}