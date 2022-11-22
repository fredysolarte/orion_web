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
using AjaxControlToolkit;
using XUSS.BLL.Inventarios;
using XUSS.BLL.Articulos;
using XUSS.BLL.ListaPrecios;
using System.Web.Services;
using XUSS.DAL.Comun;
using XUSS.BLL;
using System.IO;
using XUSS.DAL.Pedidos;
using System.Configuration;

namespace XUSS.WEB.Facturacion
{
    public partial class Remisiones : System.Web.UI.Page
    {
        private string gc_terpago
        {
            set { ViewState["gc_terpago"] = value; }
            get { return Convert.ToString(ViewState["gc_terpago"]); }
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
        private string g_bodega
        {
            set { ViewState["g_bodega"] = value; }
            get { return Convert.ToString(ViewState["g_bodega"]); }
        }        
        private string gc_tlista
        {
            set { ViewState["gc_tlista"] = value; }
            get { return ViewState["gc_tlista"] as string; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private string gc_moneda
        {
            set { ViewState["gc_moneda"] = value; }
            get { return Convert.ToString(ViewState["gc_moneda"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gc_moneda = ComunBL.GetMoneda(null, Convert.ToString(Session["CODEMP"]));
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
                    DataTable dt = new DataTable();
                    try
                    {
                        tbItems = Obj.GetFacturaDT(null, null, null, 0);
                        tbPagos = Obj.GetPagos(null, null, null, 0);

                        tbBalance = dt;
                        //Crear Campos Persistentes
                        tbBalance.Columns.Add("TP", typeof(string));
                        tbBalance.Columns.Add("C1", typeof(string));
                        tbBalance.Columns.Add("C2", typeof(string));
                        tbBalance.Columns.Add("C3", typeof(string));
                        tbBalance.Columns.Add("C4", typeof(string));
                        tbBalance.Columns.Add("MBBODEGA", typeof(string));
                        tbBalance.Columns.Add("MLCDLOTE", typeof(string));
                        tbBalance.Columns.Add("MLCANTID", typeof(Int32));
                        tbBalance.Columns.Add("MBCANTID", typeof(Int32));
                        tbBalance.Columns.Add("IT", typeof(Int32));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        dt = null;
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
            string filtro = "AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(5))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND (TRNOMBRE+ISNULL(TRNOMBR2,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";            

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text))
                filtro += " AND HDNROFAC = " + (((RadButton)sender).Parent.FindControl("txt_nrofactura") as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text))
                filtro += " AND LH_LSTPAQ = " + (((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text))
                filtro += " AND LH_LSTPAQ IN (SELECT LH_LSTPAQ FROM TB_EMPAQUEHD WITH(NOLOCK) WHERE LH_PEDIDO =  " + (((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text+ ")";

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
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            break;
                        }
                        if (ConfigurationManager.AppSettings["ROUND"] == "S")
                        {
                            row["DTSUBTOT"] = Convert.ToDouble(rw["PDPRECIO"]) * Convert.ToDouble(rw["LD_CANTID"]);
                            //row["DTTOTIVA"] = (Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100;
                            row["DTTOTIVA"] = 0;
                            row["DTTOTFAC"] = Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]);
                            if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                                row["DTTOTIVA"] = 0;
                            //row["DTTOTIVA"] = ((Convert.ToDouble(rw["PDPRELIS"]) * (Convert.ToDouble(rw["LD_CANTID"])) * Convert.ToDouble(rw["TTVALORF"])) / 100);
                        }
                        else
                        {
                            row["DTSUBTOT"] = Math.Round(Convert.ToDouble(rw["PDPRECIO"]) * Convert.ToDouble(rw["LD_CANTID"]));
                            //row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(rw["TTVALORF"])) / 100);
                            row["DTTOTIVA"] = 0;
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

                obj_factura.InsertParameters["ind_inv"].DefaultValue = "S";
                if (!string.IsNullOrEmpty((e.ListViewItem.FindControl("txt_lta") as RadTextBox).Text))
                    obj_factura.InsertParameters["ind_inv"].DefaultValue = "N";

                obj_factura.InsertParameters["HDCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;
                obj_factura.InsertParameters["HDAGENTE"].DefaultValue = (e.ListViewItem.FindControl("rc_agente") as RadComboBox).SelectedValue;
                obj_factura.InsertParameters["HDCODSUC"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;

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
                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inImp=S&inClo=S&inParametro=InNumero&inValor=" + retorno[1] +
                              "&inParametro=InTipo&inValor=" + retorno[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"])+"&inParametro=inMoneda&inValor=" + Convert.ToString(gc_moneda);
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
                string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalPrinter.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            Boolean lb_ind = false;
            FacturacionBL Obj = new FacturacionBL();
            TipoFacturaBL ObjT = new TipoFacturaBL();
            try
            {
                if ((rlv_factura.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue == "AN")
                {
                    litTextoMensaje.Text = "Factura ya Se Encuentra Anulada!";

                }
                else
                {
                    foreach (DataRow rw in (Obj.GetFacturaHD(null, " HD_TIPREM='" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "' AND HD_NROREM=" + Convert.ToString((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text), 0, 0)).Rows)
                        lb_ind = true;

                    if (!lb_ind)
                    {

                        if (!string.IsNullOrEmpty((rlv_factura.Items[0].FindControl("txt_lta") as RadTextBox).Text) || Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_lta") as RadTextBox).Text) != 0)
                        {
                            Obj.AnularFacturacion(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                              rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]), Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_lta") as RadTextBox).Text));
                            litTextoMensaje.Text = "¡Documento Anulado de Manera Correcta!";
                        }
                        else
                        {
                            Obj.AnularFacturacionInventario(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                                  rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]));
                            litTextoMensaje.Text = "¡Documento Anulado de Manera Correcta!";
                        }

                        foreach (DataRow rw in ObjT.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
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
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inImp=S&inParametro=inNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                                  "&inParametro=inTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=inCodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inCopia&inValor=1";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        }
                    }
                    else
                    {
                        litTextoMensaje.Text = "¡No se Puede Anular, Ya tiene una Factura Asociada!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                ObjT = null; 
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
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaTerceros_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                TercerosBL ObjT = new TercerosBL();
                ListaPreciosBL ObjL = new ListaPreciosBL();
                try
                {

                    (rlv_factura.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                    (rlv_factura.InsertItem.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                    (rlv_factura.InsertItem.FindControl("txt_apellido") as RadTextBox).Text = Convert.ToString(item["TRAPELLI"].Text);
                    (rlv_factura.InsertItem.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(item["TRDIRECC"].Text);
                    (rlv_factura.InsertItem.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                    (rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(item["TRLISPRE"].Text);


                    if (!string.IsNullOrEmpty(item["TRLISPRE"].Text))
                            {
                                (rlv_factura.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(item["TRLISPRE"].Text);
                                foreach (DataRow rx in ObjL.GetListaPrecioHD(null, " P_CLISPRE='" + Convert.ToString(item["TRLISPRE"].Text) + "'", 0, 0).Rows)
                                {
                                    gc_tlista = Convert.ToString(rx["P_CTIPLIS"]);
                                }
                            }
                        
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        using (IDataReader reader = obj.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(item["TRCODTER"].Text)))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                            }
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
                    ObjT = null;
                    ObjL = null;
                }
            }
            else
            {
                if (e.CommandName == "Page")
                {
                    string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
        }
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
                case "InitInsert":
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
                    break;
                case "RebindGrid":                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
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
            //rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
        }
        protected void rc_tipfac_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TipoFacturaBL Obj = new TipoFacturaBL();
            try
            {
                if ((sender as RadComboBox).SelectedValue != "-1")
                {
                    foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + (sender as RadComboBox).SelectedValue + "'", 0, 0).Rows)
                    {
                        if (System.DateTime.Now < Convert.ToDateTime(rw["TFFECFAC"]))
                        {
                            (sender as RadComboBox).SelectedValue = "-1";
                            litTextoMensaje.Text = "Fecha Actual es Inferior a la Ultima Fecha de Factura!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            }
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadComboBoxItem item_ = new RadComboBoxItem();
                MovimientosBL Obj = new MovimientosBL();
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

                    //mpAddArticulos.Show();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    txt_barras.Focus();

                    this.ConfigLinea();

                    rc_lote.Items.Clear();
                    item_.Value = "-1";
                    item_.Text = "Seleccionar";
                    rc_lote.Items.Add(item_);
                    foreach (DataRow rw in  Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]),g_bodega,txt_tp.Text,txt_referencia.Text,txt_clave2.Text,txt_clave3.Text,txt_clave4.Text,".").Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["BLCDLOTE"]);
                        itemi.Text = "Lote: " + Convert.ToString(rw["BLCDLOTE"]) + " Can: " + Convert.ToString(rw["BLCANTID"]) ;
                        rc_lote.Items.Add(itemi);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                    item_ = null;
                    Obj = null;
                }
            }
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

            //mpAddArticulos.Show();            
            //mpArticulos.Show();

            rgConsultaArticulos.DataBind();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0,ln_item = 0;
            int i = 1;
            DataRow row = tbItems.NewRow();
            try {
                ln_item = tbItems.Rows.Count+1;
                row["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["DTTIPFAC"] = "";
                row["DTNROFAC"] = 0;
                row["DTNROITM"] = ln_item;
                row["DTPEDIDO"] = 0;
                row["DTLINNUM"] = ln_item;
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
                //row["DTLISPRE"] = lc_ltaprecio;
                row["DTPRELIS"] = txt_preciolta.Value;
                row["C2"] = txt_nc2r.Text;
                row["C3"] = txt_nc3r.Text;

                if (gc_tipdcto == 1)// 1 Porcentaje 2 Valor
                    row["DTPRECIO"] = txt_preciolta.Value - (txt_preciolta.Value * txt_dct.Value) / 100;
                else
                    row["DTPRECIO"] = txt_preciolta.Value - txt_dct.Value;

                row["DTDESCUE"] = txt_dct.Value;
                row["DTTOTDES"] = txt_dct.Value;

                row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(row["DTCANTID"]));
                row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(19)) / 100);
                row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));                               
                //if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                //    row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(row["PDPRELIS"]) * (Convert.ToDouble(row["DTCANTID"])) * Convert.ToDouble(19)) / 100));

                row["LOTE"] = rc_lote.SelectedValue;
                row["DTCODDES"] = 0;

                if (gc_tlista == "2")
                {
                    row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(txt_cantidad.Value));
                    //row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(txt_impf.Value)) / 100);
                    row["DTTOTIVA"] = 0;
                    row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                    if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                        row["DTTOTIVA"] = 0;
                        //row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(row["DTPRECIO"]) * (Convert.ToDouble(txt_cantidad.Value)) * Convert.ToDouble(txt_impf.Value)) / 100));
                }
                else
                {
                    row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(txt_cantidad.Value));
                    //row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) - (Convert.ToDouble(row["DTSUBTOT"]) / ((Convert.ToDouble(txt_impf.Value) / 100) + 1))));
                    row["DTTOTIVA"] = 0;
                    row["DTSUBTOT"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) / ((Convert.ToDouble(txt_impf.Value) / 100) + 1)));
                    row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                    if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                        row["DTTOTIVA"] = 0;
                        //row["DTTOTIVA"] = Math.Round(((Convert.ToDouble(row["DTPRECIO"]) * (Convert.ToDouble(txt_cantidad.Value)) * Convert.ToDouble(txt_impf.Value)) / 100));
                }

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
                row["DEV"] = "N";


                tbItems.Rows.Add(row);

                DataRow rx = tbBalance.NewRow();
                rx["TP"] = row["DTTIPPRO"];
                rx["C1"] = row["DTCLAVE1"];
                rx["C2"] = row["DTCLAVE2"];
                rx["C3"] = row["DTCLAVE3"];
                rx["C4"] = row["DTCLAVE4"];
                rx["MBBODEGA"] = null;
                rx["MLCDLOTE"] = rc_lote.SelectedValue;
                rx["MLCANTID"] = row["DTCANTID"];
                rx["MBCANTID"] = row["DTCANTID"];
                rx["IT"] = ln_item;

                tbBalance.Rows.Add(rx);
                rx = null;

                foreach (DataRow rw in tbItems.Rows)
                {
                    ln_subtotal += Convert.ToDouble(rw["DTSUBTOT"]);
                    ln_impuesto += Convert.ToDouble(rw["DTTOTIVA"]);
                    ln_total += Convert.ToDouble(rw["DTTOTFAC"]);
                    foreach (DataRow rz in tbBalance.Rows)
                    {
                        if (Convert.ToInt32(rw["DTNROITM"]) == Convert.ToInt32(rz["IT"]))
                        {
                            rz["IT"] = i;
                        }
                    }
                    rw["DTNROITM"] = i;
                    rw["DTLINNUM"] = i;
                    i++;
                }
                
                (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_total;

                (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                this.Limpiar();
                //string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                //txt_barras.Focus();
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
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var PDLINNUM = item.GetDataKeyValue("DTNROITM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["DTNROITM"]) == Convert.ToInt32(PDLINNUM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    foreach (DataRow row in tbItems.Rows)
                    {
                        row["DTNROITM"] = i;
                        i++;
                    }

                    //Balance
                    tbBalance.AcceptChanges();
                    foreach (DataRow rw in tbBalance.Rows)
                    {
                        if (Convert.ToInt32(rw["IT"]) == Convert.ToInt32(PDLINNUM))
                            rw.Delete();
                    }
                    tbBalance.AcceptChanges();
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    break;
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
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                DescuentosBL ObjD = new DescuentosBL();
                ArticulosBL Obj = new ArticulosBL();
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

                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        this.Limpiar();
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
                            ((sender as RadTextBox).Parent.FindControl("txt_fnacimiento") as RadDatePicker).DbSelectedDate = reader.IsDBNull(reader.GetOrdinal("TRFECNAC")) ? null : (DateTime?)Convert.ToDateTime(reader["TRFECNAC"]);

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
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_procesar_Aceptar(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo));
        }
        protected void procesa_plano(Stream inStream)
        {
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
            int i = 0,ln_item=0;
            Boolean lb_ind = false,lb_break =false;
            StringBuilder lc_error = new StringBuilder();
            ArticulosBL Obj = new ArticulosBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            try
            {
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {                            
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            string lc_barras = words[0];
                            int ln_cantidad = Convert.ToInt32(words[1]);
                            Boolean lb_Existe = false;

                            foreach (DataRow rx in Obj.GetTbBarrasInv(null, lc_barras, Convert.ToString(((rlv_factura.InsertItem.FindControl("rc_lstprecio")) as RadComboBox).SelectedValue), g_bodega).Rows)
                            {
                                lb_Existe = true;
                                if (Convert.ToInt32(rx["BBCANTID"]) < ln_cantidad)
                                    lc_error.AppendLine("Cantidad Solicitda Supera Inventario Cod: " + lc_barras + " Ref BBSA:" + Convert.ToString(rx["BCLAVE1"]));

                                foreach (DataRow rz in ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["BTIPPRO"]), Convert.ToString(rx["BCLAVE1"]), Convert.ToString(rx["BCLAVE2"]), Convert.ToString(rx["BCLAVE3"]), Convert.ToString(rx["BCLAVE4"]), g_bodega, 0).Rows)
                                {
                                    lb_ind = false;
                                    lb_break = false;
                                    DataRow row = tbItems.NewRow();

                                    ln_item = tbItems.Rows.Count + 1;
                                    row["DTCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                    row["DTTIPFAC"] = "";
                                    row["DTNROFAC"] = 0;
                                    row["DTNROITM"] = ln_item;
                                    row["DTPEDIDO"] = 0;
                                    row["DTLINNUM"] = ln_item;
                                    row["DTTIPPRO"] = rx["BTIPPRO"];
                                    row["DTCLAVE1"] = rx["BCLAVE1"];
                                    row["DTCLAVE2"] = rx["BCLAVE2"];
                                    row["DTCLAVE3"] = rx["BCLAVE3"];
                                    row["DTCLAVE4"] = rx["BCLAVE4"];
                                    row["DTCODCAL"] = ".";
                                    row["DTUNDPED"] = "UN";

                                    //Nuevo item en Balance
                                    DataRow itmbal = tbBalance.NewRow();
                                    itmbal["TP"] = Convert.ToString(rx["BTIPPRO"]);
                                    itmbal["C1"] = Convert.ToString(rx["BCLAVE1"]);
                                    itmbal["C2"] = Convert.ToString(rx["BCLAVE2"]);
                                    itmbal["C3"] = Convert.ToString(rx["BCLAVE3"]);
                                    itmbal["C4"] = Convert.ToString(rx["BCLAVE4"]);
                                    itmbal["MBBODEGA"] = g_bodega;
                                    itmbal["IT"] = ln_item;

                                    if (Convert.ToInt32(rz["BBCANTID"]) >= ln_cantidad)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(rz["BLCDLOTE"])))
                                        {
                                            if (Convert.ToInt32(rz["BLCANTID"]) >= ln_cantidad)
                                            {
                                                row["DTCANPED"] = ln_cantidad;
                                                row["DTCANTID"] = ln_cantidad;
                                                //Balance
                                                itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                                itmbal["MLCANTID"] = ln_cantidad;
                                                lb_break = true;
                                            }
                                            else
                                            {
                                                row["DTCANPED"] = Convert.ToInt32(rz["BLCANTID"]);
                                                row["DTCANTID"] = Convert.ToInt32(rz["BLCANTID"]);
                                                ln_cantidad = ln_cantidad - Convert.ToInt32(rz["BLCANTID"]);
                                                itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                                itmbal["MLCANTID"] = Convert.ToInt32(rz["BLCANTID"]);
                                                lb_break = false;
                                            }
                                        }
                                        else
                                        {
                                            row["DTCANPED"] = ln_cantidad;
                                            row["DTCANTID"] = ln_cantidad;
                                            //Balance
                                            itmbal["MLCDLOTE"] = "";
                                            itmbal["MBCANTID"] = ln_cantidad;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(rz["BLCDLOTE"])))
                                        {
                                            row["DTCANPED"] = rz["BLCANTID"];
                                            row["DTCANTID"] = rz["BLCANTID"];
                                            //Balance
                                            itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                            itmbal["MLCANTID"] = rz["BLCANTID"];
                                            lb_break = false;
                                        }
                                        else
                                        {
                                            row["DTCANPED"] = Convert.ToInt32(rz["BBCANTID"]);
                                            row["DTCANTID"] = Convert.ToInt32(rz["BBCANTID"]);
                                            //Balance
                                            itmbal["MLCDLOTE"] = "";
                                            itmbal["MBCANTID"] = rz["BBCANTID"];
                                        }

                                    }                                    

                                    row["DTCANKLG"] = 0;
                                    row["DTPRELIS"] = rx["PRECIO"];
                                    row["C2"] = "";
                                    row["C3"] = "";
                                    if (gc_tipdcto == 1)// 1 Porcentaje 2 Valor
                                        row["DTPRECIO"] = Convert.ToDouble(rx["PRECIO"]) - (Convert.ToDouble(rx["PRECIO"]) * Convert.ToDouble(rx["DESCUENTO"])) / 100;
                                    else
                                        row["DTPRECIO"] = Convert.ToDouble(rx["PRECIO"]) - Convert.ToDouble(rx["DESCUENTO"]);

                                    row["DTDESCUE"] = Convert.ToDouble(rx["DESCUENTO"]);
                                    row["DTTOTDES"] = Convert.ToDouble(rx["DESCUENTO"]);
                                    row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(row["DTCANTID"]));
                                    row["DTTOTIVA"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) * Convert.ToDouble(19)) / 100);
                                    row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                                    row["LOTE"] = rc_lote.SelectedValue;
                                    row["DTCODDES"] = 0;
                                    if (gc_tlista == "2")
                                    {
                                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * ln_cantidad);
                                        row["DTTOTIVA"] = 0;
                                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                                            row["DTTOTIVA"] = 0;
                                    }
                                    else
                                    {
                                        row["DTSUBTOT"] = Math.Round(Convert.ToDouble(row["DTPRECIO"]) * Convert.ToDouble(ln_cantidad));
                                        row["DTTOTIVA"] = 0;
                                        row["DTSUBTOT"] = Math.Round((Convert.ToDouble(row["DTSUBTOT"]) / ((Convert.ToDouble(txt_impf.Value) / 100) + 1)));
                                        row["DTTOTFAC"] = Math.Round(Convert.ToDouble(row["DTSUBTOT"]) + Convert.ToDouble(row["DTTOTIVA"]));
                                        if (Convert.ToDouble(row["DTDESCUE"]) == 100)
                                            row["DTTOTIVA"] = 0;
                                    }

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
                                    row["ARNOMBRE"] = rx["ARNOMBRE"];
                                    row["DTESTADO"] = "AC";
                                    row["DTCAUSAE"] = ".";
                                    row["DTNMUSER"] = Convert.ToString(Session["UserLogon"]);
                                    row["DTFECMOD"] = System.DateTime.Today;
                                    row["DTFECING"] = System.DateTime.Today;
                                    row["DTNROMOV"] = 0;
                                    row["DTITMMOV"] = 0;
                                    row["DEV"] = "N";

                                    //if (lb_ind)
                                    //{
                                    foreach (DataRow rt in tbItems.Rows)
                                    {
                                        if (Convert.ToString(rt["DTTIPPRO"]) == Convert.ToString(rx["BTIPPRO"]) && Convert.ToString(rt["DTCLAVE1"]) == Convert.ToString(rx["BCLAVE1"]) && Convert.ToString(rt["DTCLAVE2"]) == Convert.ToString(rx["BCLAVE2"]) && Convert.ToString(rt["DTCLAVE3"]) == Convert.ToString(rx["BCLAVE3"]) && Convert.ToString(rt["DTCLAVE4"]) == Convert.ToString(rx["BCLAVE4"]))
                                        {
                                            rt["DTCANTID"] = Convert.ToInt32(rt["DTCANTID"]) + Convert.ToInt32(row["DTCANTID"]);                                                                                                 
                                            rt["DTCANPED"] = Convert.ToInt32(rt["DTCANPED"]) + Convert.ToInt32(row["DTCANTID"]);
                                            ln_item = Convert.ToInt32(rt["DTNROITM"]);
                                            foreach (DataRow rf in tbBalance.Rows)
                                            {
                                                if (Convert.ToInt32(rt["DTNROITM"]) == Convert.ToInt32(rf["IT"]))
                                                {
                                                    rf["MBCANTID"] = Convert.ToInt32(rf["MBCANTID"]) + Convert.ToInt32(row["DTCANTID"]);
                                                    if (!string.IsNullOrEmpty(Convert.ToString(rf["MLCDLOTE"])))
                                                        rf["MLCANTID"] = Convert.ToInt32(rf["MLCANTID"]) + Convert.ToInt32(row["DTCANTID"]);
                                                }
                                            }
                                            lb_ind = true;
                                        }
                                    }

                                    //if (!lb_ind)                                    
                                    //    tbItems.Rows.Add(row);                                                                        
                                    //itmbal["IT"] = ln_item;
                                    //tbBalance.Rows.Add(itmbal);

                                    if (!lb_ind)
                                    {
                                        tbItems.Rows.Add(row);
                                        itmbal["IT"] = ln_item;
                                        tbBalance.Rows.Add(itmbal);
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(itmbal["MLCDLOTE"])))
                                            tbBalance.Rows.Add(itmbal);
                                    }

                                    if (lb_break)
                                        break;

                                }                            
                            }//FIN FOR ECHA

                            if (!lb_Existe)
                                lc_error.AppendLine("No se Encuentra Cod Barras: " + lc_barras);
                            
                        }//Fin While
                        i = 1;
                        foreach (DataRow rw in tbItems.Rows)
                        {
                            ln_subtotal += Convert.ToDouble(rw["DTSUBTOT"]);
                            ln_impuesto += Convert.ToDouble(rw["DTTOTIVA"]);
                            ln_total += Convert.ToDouble(rw["DTTOTFAC"]);
                            rw["DTNROITM"] = i;
                            rw["DTLINNUM"] = i;
                            i++;
                        }
                        (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                        (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                        (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_total;

                        (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                        litTextoMensaje.Text = "Cargue Efectuado !" + lc_error.ToString();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                ObjE = null;
            }
        }
        protected void rg_items_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            string DTTIPFAC = dataItem.GetDataKeyValue("DTTIPFAC").ToString();
            int DTNROFAC = Convert.ToInt32(dataItem.GetDataKeyValue("DTNROFAC").ToString());
            int DTNROITM = Convert.ToInt32(dataItem.GetDataKeyValue("DTNROITM").ToString());

            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        FacturacionBL Obj = new FacturacionBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetDetalleMovimientos(null, Convert.ToString(Session["CODEMP"]), DTTIPFAC, DTNROFAC, DTNROITM);
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
                    }
                case "detalle_insert":
                    {
                        DataTable dt = new DataTable();
                        //dt = tbBalance.Copy();
                        dt.Columns.Add("TP", typeof(string));
                        dt.Columns.Add("C1", typeof(string));
                        dt.Columns.Add("C2", typeof(string));
                        dt.Columns.Add("C3", typeof(string));
                        dt.Columns.Add("C4", typeof(string));
                        dt.Columns.Add("MBBODEGA", typeof(string));
                        dt.Columns.Add("MLCDLOTE", typeof(string));
                        dt.Columns.Add("MLCANTID", typeof(Int32));
                        dt.Columns.Add("MBCANTID", typeof(Int32));
                        dt.Columns.Add("IT", typeof(Int32));

                        try
                        {
                            for (int i = 0; i < tbBalance.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(((DataRow)tbBalance.Rows[i])["IT"]) == DTNROITM)
                                {
                                    DataRow rw = dt.NewRow();
                                    rw["TP"] = ((DataRow)tbBalance.Rows[i])["TP"];
                                    rw["C1"] = ((DataRow)tbBalance.Rows[i])["C1"];
                                    rw["C2"] = ((DataRow)tbBalance.Rows[i])["C2"];
                                    rw["C3"] = ((DataRow)tbBalance.Rows[i])["C3"];
                                    rw["C4"] = ((DataRow)tbBalance.Rows[i])["C4"];
                                    rw["MBBODEGA"] = ((DataRow)tbBalance.Rows[i])["MBBODEGA"];
                                    rw["MLCDLOTE"] = ((DataRow)tbBalance.Rows[i])["MLCDLOTE"];
                                    rw["MLCANTID"] = ((DataRow)tbBalance.Rows[i])["MLCANTID"];
                                    rw["MBCANTID"] = ((DataRow)tbBalance.Rows[i])["MBCANTID"];
                                    rw["IT"] = ((DataRow)tbBalance.Rows[i])["IT"];

                                    dt.Rows.Add(rw);
                                    rw = null;
                                }
                                //dt.Rows[i].Delete();
                            }

                            dt.AcceptChanges();
                            e.DetailTableView.DataSource = dt;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            dt = null;
                        }
                        break;
                    }

            }
        }
        protected void btn_ok_printer_Click(object sender, EventArgs e)
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
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=InNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                          "&inParametro=InTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"])+"&inParametro=inMoneda&inValor="+ Convert.ToString(rc_moneda_print.SelectedValue);
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
    }
}