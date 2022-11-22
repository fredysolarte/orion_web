using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Articulos;
using XUSS.BLL.Compras;
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Compras
{
    public partial class OrdenesCompras : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbProforma
        {
            set { ViewState["tbProforma"] = value; }
            get { return ViewState["tbProforma"] as DataTable; }
        }
        private DataTable tbFactura
        {
            set { ViewState["tbFactura"] = value; }
            get { return ViewState["tbFactura"] as DataTable; }
        }
        private DataTable tbLotes
        {
            set { ViewState["tbLotes"] = value; }
            get { return ViewState["tbLotes"] as DataTable; }
        }
        private DataTable tbCostos
        {
            set { ViewState["tbCostos"] = value; }
            get { return ViewState["tbCostos"] as DataTable; }
        }
        private DataTable tbSoportes
        {
            set { ViewState["tbSoportes"] = value; }
            get { return ViewState["tbSoportes"] as DataTable; }
        }
        private DataTable tbSeguimiento
        {
            set { ViewState["tbSeguimiento"] = value; }
            get { return ViewState["tbSeguimiento"] as DataTable; }
        }
        private DataTable tbBillxWR
        {
            set { ViewState["tbBillxWR"] = value; }
            get { return ViewState["tbBillxWR"] as DataTable; }
        }
        private DataTable tbContainers
        {
            set { ViewState["tbContainers"] = value; }
            get { return ViewState["tbContainers"] as DataTable; }
        }
        private DataTable tbSummari
        {
            set { ViewState["tbSummari"] = value; }
            get { return ViewState["tbSummari"] as DataTable; }
        }
        private string gc_moneda
        {
            set { ViewState["moneda"] = value; }
            get { return Convert.ToString(ViewState["moneda"]); }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private string gc_tipo
        {
            set { ViewState["gc_tipo"] = value; }
            get { return ViewState["gc_tipo"] as string; }
        }
        private string gc_origen
        {
            set { ViewState["gc_origen"] = value; }
            get { return ViewState["gc_origen"] as string; }
        }
        private string gc_indtercero
        {
            set { ViewState["gc_indtercero"] = value; }
            get { return Convert.ToString(ViewState["gc_indtercero"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["NroCmp"])))
                {
                    obj_compras.SelectParameters["filter"].DefaultValue = "CH_NROCMP =" + Convert.ToString(Request.QueryString["NroCmp"]);
                    rlv_compras.DataBind();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Invoice"])))
                {
                    obj_compras.SelectParameters["filter"].DefaultValue = "CH_NROCMP IN (SELECT FD_NROCMP FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA ='" + Convert.ToString(Request.QueryString["Invoice"])+"')";
                    rlv_compras.DataBind();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Proforma"])))
                {
                    obj_compras.SelectParameters["filter"].DefaultValue = "CH_NROCMP IN (SELECT PR_NROCMP FROM CMP_PROFACTURADT WITH(NOLOCK) WHERE PR_NROFACPROFORMA ='" + Convert.ToString(Request.QueryString["Proforma"]) + "')";
                    rlv_compras.DataBind();
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
            this.OcultarPaginador(rlv_compras, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_compras_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    OrdenesComprasBL Obj = new OrdenesComprasBL();
                    BillofLadingBL ObjBL = new BillofLadingBL();
                    SoportesBL ObjS = new SoportesBL();
                    try
                    {
                        tbItems = Obj.GetComprasDT(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbProforma = Obj.GetProforma(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbFactura = Obj.GetFactura(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbCostos = Obj.GetCostos(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbSoportes = ObjS.GetSoportes(null, "1", 0);
                        tbSeguimiento = Obj.GetSeguimiento(null, Convert.ToString(Session["CODEMP"]), 0);
                        gc_moneda = ComunBL.GetMoneda(null, Convert.ToString(Session["CODEMP"]));
                        tbBillxWR = ObjBL.GetBLCompra(null, 0);
                        tbContainers = ObjBL.GetBLDT(null,0);
                        tbSummari = Obj.GetSummari(null, Convert.ToString(Session["CODEMP"]), 0);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        ObjS = null;
                        ObjBL = null;
                    }
                    break;

                case "Buscar":
                    obj_compras.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_compras.DataBind();
                    break;
                case "Edit":

                    break;
                case "Delete":
                    OrdenesComprasBL ObjC = new OrdenesComprasBL();
                    try {
                        ObjC.AnularOrdenCompra(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_compras.Items[0].FindControl("txt_nroorden")) as RadTextBox).Text), "AN", Convert.ToString(Session["UserLogon"]));
                        litTextoMensaje.Text = "Se Anulo de Manera Correcta!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ObjC = null;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_torden") as RadComboBox).SelectedValue))
                filtro += " AND CH_TIPORD = '" + (((RadButton)sender).Parent.FindControl("rc_torden") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_otbodega") as RadComboBox).SelectedValue))
                filtro += " AND CH_BODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_otbodega") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue))
                filtro += " AND CH_ESTADO = '" + (((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text))
                filtro += " AND CH_NROCMP = " + (((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nroOrdenInt") as RadTextBox).Text))
                filtro += " AND CH_CNROCMPALT LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nroOrdenInt") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text))
                filtro += " AND CH_NROCMP IN (SELECT FD_NROCMP FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text + "%')";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_proforma") as RadTextBox).Text))
                filtro += " AND CH_NROCMP IN (SELECT PR_NROCMP FROM CMP_PROFACTURADT WITH(NOLOCK) WHERE PR_NROFACPROFORMA LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_proforma") as RadTextBox).Text + "%')";


            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_compras.SelectParameters["filter"].DefaultValue = filtro;
            rlv_compras.DataBind();
            if ((rlv_compras.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_compras.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_compras_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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

            OrdenesComprasBL Obj = new OrdenesComprasBL();
            BillofLadingBL ObjBL = new BillofLadingBL();
            SoportesBL ObjS = new SoportesBL();
            try
            {
                tbItems = Obj.GetComprasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                tbProforma = Obj.GetProforma(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                (e.Item.FindControl("rg_proforma") as RadGrid).DataBind();

                tbFactura = Obj.GetFactura(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                (e.Item.FindControl("rg_factura") as RadGrid).DataBind();

                tbSummari = Obj.GetSummari(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_resumen") as RadGrid).DataSource = tbSummari;
                (e.Item.FindControl("rg_resumen") as RadGrid).DataBind();

                tbCostos = Obj.GetCostos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_costos") as RadGrid).DataSource = tbCostos;
                (e.Item.FindControl("rg_costos") as RadGrid).DataBind();

                tbSoportes = ObjS.GetSoportes(null, "1", Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                (e.Item.FindControl("rgSoportes") as RadGrid).DataBind();

                tbSeguimiento = Obj.GetSeguimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_seguimiento") as RadGrid).DataSource = tbSeguimiento;
                (e.Item.FindControl("rg_seguimiento") as RadGrid).DataBind();

                tbBillxWR = ObjBL.GetBLCompra(null, Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                (e.Item.FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                (e.Item.FindControl("rg_bl") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjS = null;
                ObjBL = null;
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8004&inban=S&inParametro=inConse&inValor=" + Convert.ToString((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            gc_tipo = "OC";
            string script = "";
            switch (e.CommandName)
            {
                case "InitInsert":
                    txt_barras.Focus();
                    edt_fecha.Visible = false;
                    edt_dcmt.Visible = false;
                    lbl_fecha.Visible = false;
                    lbl_nro.Visible = false;
                    val_fecha.Enabled = false;
                    val_nrodoc.Enabled = false;
                    lbl_tipocargue.Text = "Can;Vlr UN";
                    btn_agregar.CommandName = "Insert";
                    btn_agregar.Icon.PrimaryIconCssClass = "rbAdd";
                    btn_agregar.Text = "Insert";
                    script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    this.Limpiar();
                    e.Canceled = true;
                    break;
                case "Edit":
                    txt_barras.Focus();
                    edt_fecha.Visible = false;
                    edt_dcmt.Visible = false;
                    lbl_fecha.Visible = false;
                    lbl_nro.Visible = false;
                    val_fecha.Enabled = false;
                    val_nrodoc.Enabled = false;
                    lbl_tipocargue.Text = "Can;Vlr UN";
                    btn_agregar.CommandName = "Update";
                    btn_agregar.Icon.PrimaryIconCssClass = "rbAdd";
                    btn_agregar.Text = "Update";
                    //e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CD_NROITEM"]
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["CD_NROITEM"]))
                        {
                            txt_item.Text = Convert.ToString(row["CD_NROITEM"]);
                            edt_dcmt.Text = Convert.ToString(row["CD_NROCMP"]);
                            rc_categoria.SelectedValue = Convert.ToString(row["CD_TIPPRO"]);
                            rc_categoria_SelectedIndexChanged(sender, null);
                            txt_referencia.Text = Convert.ToString(row["CD_CLAVE1"]);
                            txt_clave2.Text = Convert.ToString(row["CD_CLAVE2"]);
                            txt_clave3.Text = Convert.ToString(row["CD_CLAVE3"]);
                            txt_clave4.Text = Convert.ToString(row["CD_CLAVE4"]);
                            //row["FD_REFPRO"] = txt_rproveedor.Text;
                            //row["FD_COLPRO"] = txt_cproveedor.Text;

                            txt_cantidad.Value = Convert.ToDouble(row["CD_CANTIDAD"]);
                            txt_precio.Value = Convert.ToDouble(row["CD_PRECIO"]);
                            txt_observaciones.Text = Convert.ToString(row["CD_OBSERVACIONES"]);
                            txt_descripcion.Text = Convert.ToString(row["ARNOMBRE"]);
                                                        
                            txt_barras.Text = Convert.ToString(row["BARRAS"]);
                            rc_dt1.SelectedValue = Convert.ToString(row["ARDTTEC1"]);
                            rc_dt2.SelectedValue = Convert.ToString(row["ARDTTEC2"]);
                            rc_dt3.SelectedValue = Convert.ToString(row["ARDTTEC3"]);
                            rc_dt4.SelectedValue = Convert.ToString(row["ARDTTEC4"]);
                            rc_dt5.SelectedValue = Convert.ToString(row["ARDTTEC5"]);
                            rc_dt7.SelectedValue = Convert.ToString(row["ARDTTEC7"]);
                            rc_dt8.SelectedValue = Convert.ToString(row["ARDTTEC8"]);

                            btn_agregar.CommandName = "Update";
                            btn_agregar.Icon.PrimaryIconCssClass = "rbSave";
                            btn_agregar.Text = "Update";                            
                        }
                    }
                    script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                    
                    e.Canceled = true;
                    break;
                case "RebindGrid":
                    lbl_tipocargue.Text = "Referencia;C2;C3;C4;Can;Vlr UN";
                    script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
            }            
        }
        protected void rg_proforma_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            gc_tipo = "PR";
            switch (e.CommandName)
            {
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
                case "InitInsert":
                    txt_barras.Focus();
                    edt_fecha.Visible = true;
                    edt_dcmt.Visible = true;
                    lbl_fecha.Visible = true;
                    lbl_nro.Visible = true;
                    val_fecha.Enabled = true;
                    val_nrodoc.Enabled = true;
                    btn_agregar.CommandName = "Insert";
                    btn_agregar.Icon.PrimaryIconCssClass = "rbAdd";
                    btn_agregar.Text = "Insert";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    this.Limpiar();
                    e.Canceled = true;
                    break;
                case "RebindGrid":
                    lbl_tipocargue.Text = "Referencia;C2;C3;C4;Can;Vlr UN;Nro ProForma;Fecha;Ref Proveedor;Dias;Cod Pais;Cod Arancel;FOC";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "ExportExcel":
                    (sender as RadGrid).ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
                    (sender as RadGrid).ExportSettings.IgnorePaging = true;
                    (sender as RadGrid).ExportSettings.ExportOnlyData = true;
                    (sender as RadGrid).ExportSettings.OpenInNewWindow = true;
                    (sender as RadGrid).MasterTableView.ExportToExcel();
                    break;
                case "Edit":
                    GridEditableItem item = e.Item as GridEditableItem;
                    var DTNROITM = item.GetDataKeyValue("PR_NROITEM").ToString();
                    edt_fecha.Visible = true;
                    edt_dcmt.Visible = true;
                    lbl_fecha.Visible = true;
                    lbl_nro.Visible = true;
                    val_fecha.Enabled = true;
                    val_nrodoc.Enabled = true;
                    foreach (DataRow row in tbProforma.Rows)
                    {
                        if (Convert.ToInt32(row["PR_NROITEM"]) == Convert.ToInt32(DTNROITM))
                        {
                            txt_item.Text = Convert.ToString(row["PR_NROITEM"]);
                            edt_dcmt.Text = Convert.ToString(row["PR_NROCMP"]);
                            rc_categoria.SelectedValue = Convert.ToString(row["PR_TIPPRO"]);
                            rc_categoria_SelectedIndexChanged(sender, null);
                            txt_referencia.Text = Convert.ToString(row["PR_CLAVE1"]);
                            txt_clave2.Text = Convert.ToString(row["PR_CLAVE2"]);
                            txt_clave3.Text = Convert.ToString(row["PR_CLAVE3"]);
                            txt_clave4.Text = Convert.ToString(row["PR_CLAVE4"]);
                            //row["PR_REFPRO"] = txt_rproveedor.Text;
                            //row["PR_COLPRO"] = txt_cproveedor.Text;

                            txt_cantidad.Value = Convert.ToDouble(row["PR_CANTIDAD"]);
                            txt_precio.Value = Convert.ToDouble(row["PR_PRECIO"]);
                            txt_observaciones.Text = Convert.ToString(row["PR_OBSERVACIONES"]);
                            txt_descripcion.Text = Convert.ToString(row["ARNOMBRE"]);
                            edt_dcmt.Text = Convert.ToString(row["PR_NROFACPROFORMA"]);
                            edt_fecha.SelectedDate = Convert.ToDateTime(row["PR_FECPROFORMA"]);
                            txt_barras.Text = Convert.ToString(row["BARRAS"]);
                            rc_dt1.SelectedValue = Convert.ToString(row["ARDTTEC1"]);
                            rc_dt2.SelectedValue = Convert.ToString(row["ARDTTEC2"]);
                            rc_dt3.SelectedValue = Convert.ToString(row["ARDTTEC3"]);
                            rc_dt4.SelectedValue = Convert.ToString(row["ARDTTEC4"]);
                            rc_dt5.SelectedValue = Convert.ToString(row["ARDTTEC5"]);
                            rc_dt7.SelectedValue = Convert.ToString(row["ARDTTEC7"]);
                            rc_dt8.SelectedValue = Convert.ToString(row["ARDTTEC8"]);

                            btn_agregar.CommandName = "Update";
                            btn_agregar.Icon.PrimaryIconCssClass = "rbSave";
                            btn_agregar.Text = "Update";

                            string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                    }
                    e.Canceled = true;
                    break;
            }            
        }
        protected void rg_factura_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            gc_tipo = "FC";
            switch (e.CommandName)
            {
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
                case "InitInsert":
                    edt_fecha.Visible = true;
                    edt_dcmt.Visible = true;
                    lbl_fecha.Visible = true;
                    lbl_nro.Visible = true;
                    val_fecha.Enabled = true;
                    val_nrodoc.Enabled = true;
                    txt_barras.Focus();                    
                    btn_agregar.CommandName = "Insert";
                    btn_agregar.Icon.PrimaryIconCssClass = "rbAdd";
                    btn_agregar.Text = "Insert";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    this.Limpiar();
                    e.Canceled = true;
                    break;
                case "RebindGrid":
                    lbl_tipocargue.Text = "Referencia;C2;C3;C4;Can;Vlr UN;Nro Factura;Fecha;Ref Proveedor;Dias;Cod Pais;Cod Arancel;FOC";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);                    
                    break;
                case "ExportExcel":
                    (sender as RadGrid).ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
                    (sender as RadGrid).ExportSettings.IgnorePaging = true;
                    (sender as RadGrid).ExportSettings.ExportOnlyData = true;
                    (sender as RadGrid).ExportSettings.OpenInNewWindow = true;
                    (sender as RadGrid).MasterTableView.ExportToExcel();
                    break;
                case "Edit":
                    GridEditableItem item = e.Item as GridEditableItem;
                    var DTNROITM = item.GetDataKeyValue("FD_NROITEM").ToString();

                    edt_fecha.Visible = true;
                    edt_dcmt.Visible = true;
                    lbl_fecha.Visible = true;
                    lbl_nro.Visible = true;
                    val_fecha.Enabled = true;
                    val_nrodoc.Enabled = true;

                    foreach (DataRow row in tbFactura.Rows)
                    {
                        if (Convert.ToInt32(row["FD_NROITEM"]) == Convert.ToInt32(DTNROITM))
                        {
                            txt_item.Text = Convert.ToString(row["FD_NROITEM"]);
                            edt_dcmt.Text = Convert.ToString(row["FD_NROCMP"]);
                            rc_categoria.SelectedValue = Convert.ToString(row["FD_TIPPRO"]);
                            rc_categoria_SelectedIndexChanged(sender, null);
                            txt_referencia.Text = Convert.ToString(row["FD_CLAVE1"]);
                            txt_clave2.Text = Convert.ToString(row["FD_CLAVE2"]);
                            txt_clave3.Text = Convert.ToString(row["FD_CLAVE3"]);
                            txt_clave4.Text = Convert.ToString(row["FD_CLAVE4"]);
                            //row["FD_REFPRO"] = txt_rproveedor.Text;
                            //row["FD_COLPRO"] = txt_cproveedor.Text;

                            txt_cantidad.Value = Convert.ToDouble(row["FD_CANTIDAD"]);
                            txt_precio.Value = Convert.ToDouble(row["FD_PRECIO"]);
                            txt_observaciones.Text = Convert.ToString(row["FD_OBSERVACIONES"]);
                            txt_descripcion.Text = Convert.ToString(row["ARNOMBRE"]);
                            edt_dcmt.Text = Convert.ToString(row["FD_NROFACTURA"]);
                            edt_fecha.SelectedDate = Convert.ToDateTime(row["FD_FECFAC"]);
                            txt_barras.Text = Convert.ToString(row["BARRAS"]);
                            rc_dt1.SelectedValue = Convert.ToString(row["ARDTTEC1"]);
                            rc_dt2.SelectedValue = Convert.ToString(row["ARDTTEC2"]);
                            rc_dt3.SelectedValue = Convert.ToString(row["ARDTTEC3"]);
                            rc_dt4.SelectedValue = Convert.ToString(row["ARDTTEC4"]);
                            rc_dt5.SelectedValue = Convert.ToString(row["ARDTTEC5"]);
                            rc_dt7.SelectedValue = Convert.ToString(row["ARDTTEC7"]);
                            rc_dt8.SelectedValue = Convert.ToString(row["ARDTTEC8"]);

                            btn_agregar.CommandName = "Update";
                            btn_agregar.Icon.PrimaryIconCssClass = "rbSave";
                            btn_agregar.Text = "Update";
                            string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                    }

                    e.Canceled = true;
                    break;
            }            
        }
        protected void btn_agregarct_Aceptar(object sender, EventArgs e)
        {
            DataRow rw = tbCostos.NewRow();
            try
            {
                rw["CT_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                rw["CH_NROCMP"] = 0;
                rw["CT_NROITEM"] = tbCostos.Rows.Count + 1;
                rw["CT_TIPPRO"] = txt_tpct.Text;
                rw["CT_CLAVE1"] = txt_referenciact.Text;
                rw["CT_CLAVE2"] = txt_clave2ct.Text;
                rw["CT_CLAVE3"] = txt_clave3ct.Text;
                rw["CT_CLAVE4"] = txt_clave4ct.Text;
                rw["CT_PRECIO"] = txt_precioct.Value;
                rw["TRCODTER"] = rc_proveedor.SelectedValue;
                rw["CT_TIPDOC"] = rc_tdocumento.SelectedValue;
                rw["CT_NUMDOC"] = txt_nrodoc.Text;
                rw["CT_FECDOC"] = txt_fdocumento.DbSelectedDate;
                rw["CT_MONEDA"] = rc_moneda.SelectedValue;
                rw["CT_OBSERVACIONES"] = "";
                rw["CT_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                rw["CT_ESTADO"] = "AC";
                rw["CT_FECING"] = System.DateTime.Today;
                rw["CT_FECMOD"] = System.DateTime.Today;
                rw["NOMBRE"] = rc_proveedor.Text;
                rw["ARNOMBRE"] = txt_descripcionct.Text;
                tbCostos.Rows.Add(rw);
                (rlv_compras.InsertItem.FindControl("rg_costos") as RadGrid).DataSource = tbCostos;
                (rlv_compras.InsertItem.FindControl("rg_costos") as RadGrid).DataBind();

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
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;

            try
            {
                rqf_catidad1.Validate();
                rqf_catidad2.Validate();
                rqf_referencia.Validate();

                if (rqf_catidad1.IsValid && rqf_catidad2.IsValid && rqf_referencia.IsValid)
                {
                    if ((sender as RadButton).CommandName == "Insert")
                    {
                        switch (gc_tipo)
                        {
                            case "OC":
                                this.InsertOrdenCompra();
                                break;
                            case "PR":
                                this.InsertProforma();
                                break;
                            case "FC":
                                this.InsertFactura();
                                break;
                            default:
                                throw new System.ArgumentException("Error de Seleccion!", "Orion");
                        }
                        this.Limpiar();
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        this.Limpiar();
                    }

                    //Update
                    if ((sender as RadButton).CommandName == "Update")
                    {
                        switch (gc_tipo)
                        {
                            case "OC":
                                this.UpdateOrdenCompra();
                                break;
                            case "PR":
                                this.UpdateProforma();
                                break;
                            case "FC":
                                this.UpdateFactura();
                                break;
                            default:
                                throw new System.ArgumentException("Error de Seleccion!", "Orion");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }        
        private void Limpiar()
        {
            txt_tp.Text = "";
            txt_referencia.Text = "";
            txt_clave2.Text = "";
            txt_clave3.Text = "";
            txt_clave4.Text = "";
            txt_descripcion.Text = "";
            txt_barras.Text = "";
            txt_cantidad.Value = 0;
            txt_precio.Value = 0;

            rc_dt1.SelectedValue = "-1";
            rc_dt2.SelectedValue = "-1";
            rc_dt3.SelectedValue = "-1";
            rc_dt4.SelectedValue = "-1";
            rc_dt5.SelectedValue = "-1";
            rc_dt7.SelectedValue = "-1";
            rc_dt8.SelectedValue = "-1";

            chk_foc.Checked = false;

        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            if (rlv_compras.InsertItem != null)
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_compras.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_compras.Items[0].FindControl("rc_bodegas") as RadComboBox).SelectedValue;

            edt_referencia.Text = "";
            edt_nombreart.Text = "";

            edt_referencia.Focus();
            string script = "function f(){$find(\"" + mpFindArticulo.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            edt_referencia.Focus();
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            obj_articulos.SelectParameters["inBodega"].DefaultValue = null;
            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;

            rgConsultaArticulos.DataBind();

            edt_referencia.Focus();
            string script = "function f(){$find(\"" + mpFindArticulo.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string script = "";

            switch (e.CommandName)
            {
                case "Select":
                    GridDataItem item = (GridDataItem)e.Item;

                    try
                    {
                        if (gc_tipo == "CT")
                        {
                            txt_tpct.Text = Convert.ToString(item["ARTIPPRO"].Text);
                            txt_referenciact.Text = Convert.ToString(item["ARCLAVE1"].Text);
                            txt_clave2ct.Text = Convert.ToString(item["ARCLAVE2"].Text);
                            txt_clave3ct.Text = Convert.ToString(item["ARCLAVE3"].Text);
                            txt_clave4ct.Text = Convert.ToString(item["ARCLAVE4"].Text);
                            txt_descripcionct.Text = Convert.ToString(item["ARNOMBRE"].Text);

                            txt_precioct.Focus();
                            script = "function f(){$find(\"" + mpCostos.ClientID + "\").show(); $find(\"" + txt_precioct.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            txt_precioct.Focus();
                        }
                        else
                        {
                            txt_tp.Text = Convert.ToString(item["ARTIPPRO"].Text);
                            txt_referencia.Text = Convert.ToString(item["ARCLAVE1"].Text);
                            txt_clave2.Text = Convert.ToString(item["ARCLAVE2"].Text);
                            txt_clave3.Text = Convert.ToString(item["ARCLAVE3"].Text);
                            txt_clave4.Text = Convert.ToString(item["ARCLAVE4"].Text);
                            txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);

                            txt_cantidad.Focus();
                            btn_agregar.CommandName = "Update";
                            script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_cantidad.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            txt_cantidad.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        item = null;
                    }
                    break;

                default:
                    script = "function f(){$find(\"" + mpFindArticulo.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
            }
        }
        protected void rg_costos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            gc_tipo = "CT";

            txt_referenciact.Focus();
            string script = "function f(){$find(\"" + mpCostos.ClientID + "\").show(); $find(\"" + txt_referenciact.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            this.Limpiar();
            e.Canceled = true;
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_proforma_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbProforma;
        }
        protected void rg_factura_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbFactura;
        }
        protected void rg_costos_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbCostos;
        }
        protected void rg_seguimiento_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbSeguimiento;
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            var DTNROITM = item.GetDataKeyValue("CD_NROITEM").ToString();
            int i = 1;
            int pos = 0;
            int xpos = 0;
            switch (e.CommandName)
            {
                case "Delete":                                                          
                        OrdenesComprasBL Obj = new OrdenesComprasBL();
                        try
                        {                            
                            foreach (DataRow row in tbItems.Rows)
                            {
                                if (Convert.ToInt32(row["CD_NROITEM"]) == Convert.ToInt32(DTNROITM))
                                {
                                    pos = xpos;
                                }
                                xpos++;
                            }

                        if (rlv_compras.InsertItem == null)
                            Obj.DeleteCompraDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text), Convert.ToInt32(DTNROITM));

                        tbItems.Rows[pos].Delete();
                        tbItems.AcceptChanges();
                        
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
        }
        protected void rgSoportes_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbSoportes;
        }
        protected void rlv_compras_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_compras_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "", lc_reporte = "";
                string[] retorno = Convert.ToString(e.ReturnValue).Split('-');
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);

                url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8004&inban=S&inParametro=inConse&inValor=" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
        }
        protected void obj_compras_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbItems;
            e.InputParameters["tbProforma"] = tbProforma;
            e.InputParameters["tbFactura"] = tbFactura;
            e.InputParameters["tbCostos"] = tbCostos;
            e.InputParameters["tbImagenes"] = tbSoportes;
            e.InputParameters["tbBL"] = tbBillxWR;
            e.InputParameters["tbBLDT"] = tbContainers;
        }
        protected void obj_compras_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {            
            e.InputParameters["tbDetalle"] = tbItems;
            e.InputParameters["tbProforma"] = tbProforma;
            e.InputParameters["tbFactura"] = tbFactura;
            e.InputParameters["tbCostos"] = tbCostos;
            e.InputParameters["tbBL"] = tbBillxWR;
            e.InputParameters["tbBLDT"] = tbContainers;
            e.InputParameters["tbImagenes"] = tbSoportes;
            e.InputParameters["tbSummari"] = tbSummari;
        }
        protected void rc_moneda_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (Convert.ToString(gc_moneda) != (sender as RadComboBox).SelectedValue && Convert.ToDateTime((((RadComboBox)sender).Parent.FindControl("edt_forden") as RadDatePicker).SelectedDate) != null)
                {
                    if (!ComunBL.ExisteTasaCambio(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((sender as RadComboBox).SelectedValue), Convert.ToDateTime((((RadComboBox)sender).Parent.FindControl("edt_forden") as RadDatePicker).SelectedDate)))
                    {
                        litTextoMensaje.Text = "No Existe Tasa de Cambio para la Fecha Seleccionada";
                        (sender as RadComboBox).SelectedValue = "-1";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void procesa_plano_cmp(Stream inStream)
        {
            ArticulosBL Obj = new ArticulosBL();
            string lc_filtro = "",lc_msj="";
            int i = 0;
            try
            {
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            string lc_RefOrigen = "";                            

                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            if (rbl_tiparch.SelectedIndex == 0)
                            {
                                
                                string lc_referencia = words[0];
                                string lc_c2 = words[1];
                                string lc_c3 = words[2];
                                string lc_c4 = words[3];

                                lc_referencia = Obj.GetReffromOrigen(null, Convert.ToString(Session["CODEMP"]), words[0]);

                                if (string.IsNullOrEmpty(lc_referencia))
                                    lc_referencia = words[0];
                                else
                                    lc_RefOrigen = words[0];

                                lc_filtro = " AND ARCLAVE1='" + lc_referencia + "' AND ARCLAVE2 ='" + lc_c2 + "' AND ARCLAVE3='" + lc_c3 + "' AND ARCLAVE4='" + lc_c4 + "'";
                            }
                            else
                            {
                                string lc_barras = words[0];
                                if (lc_barras.Length == 13)
                                    lc_filtro = " AND (ARTIPPRO+ARCLAVE1+ARCLAVE2+ARCLAVE3+ARCLAVE4) IN (SELECT BTIPPRO+BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 WHERE SUBSTRING(UPPER(BCODIGO),1,12)=SUBSTRING(UPPER('" + lc_barras + "'),1,12))";
                                else
                                    lc_filtro = " AND (ARTIPPRO+ARCLAVE1+ARCLAVE2+ARCLAVE3+ARCLAVE4) IN (SELECT BTIPPRO+BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 WHERE UPPER(BCODIGO)=UPPER('" + lc_barras + "') )";
                            }

                            int ln_cantidad = Convert.ToInt32(words[4]);
                            double ln_precio = Convert.ToDouble(words[5]);
                            i = 0;
                            foreach (DataRow rx in Obj.GetArticulos(null, lc_filtro).Rows)
                            {
                                i++;
                                DataRow row = tbItems.NewRow();
                                row["CD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["CD_NROCMP"] = 0;
                                row["CD_NROITEM"] = tbItems.Rows.Count + 1;
                                row["CD_BODEGA"] = "";
                                row["CD_TIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                row["CD_CLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                row["CD_CLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                row["CD_CLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                row["CD_CLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                row["CD_PROVEE"] = 0;
                                row["CD_REFPRO"] = ".";
                                row["CD_COLPRO"] = ".";
                                row["CD_CANTIDAD"] = ln_cantidad;
                                row["CD_UNIDAD"] = "UN";
                                row["CD_PRECIO"] = ln_precio;
                                row["CD_OBSERVACIONES"] = "";
                                row["CD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                                row["CD_ESTADO"] = "AC";
                                row["CD_FECING"] = System.DateTime.Today;
                                row["CD_FECMOD"] = System.DateTime.Today;
                                row["LOT1"] = "";
                                row["LOT2"] = "";
                                row["CLAVE2"] = Convert.ToString(rx["CLAVE2"]);
                                row["CLAVE3"] = Convert.ToString(rx["CLAVE3"]);
                                row["CLAVE4"] = "";
                                row["ENLACE"] = "";
                                row["VLRTOT"] = (ln_cantidad * ln_precio);
                                row["TANOMBRE"] = Convert.ToString(rx["TANOMBRE"]);
                                row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                row["CANRECIBE"] = 0;
                                row["CANRESTANTE"] = 0;

                                row["ARDTTEC1"] = Convert.ToString(rx["ARDTTEC1"]);
                                row["ARDTTEC2"] = Convert.ToString(rx["ARDTTEC2"]);
                                row["ARDTTEC3"] = Convert.ToString(rx["ARDTTEC3"]);
                                row["ARDTTEC4"] = Convert.ToString(rx["ARDTTEC4"]);
                                row["ARDTTEC5"] = Convert.ToString(rx["ARDTTEC5"]);
                                row["ARDTTEC6"] = 0;
                                row["ARDTTEC7"] = Convert.ToString(rx["ARDTTEC7"]);
                                row["ARDTTEC8"] = Convert.ToString(rx["ARDTTEC8"]);
                                row["NOMTTEC1"] = Convert.ToString(rx["NOMTTEC1"]);
                                row["NOMTTEC2"] = Convert.ToString(rx["NOMTTEC2"]);
                                row["NOMTTEC3"] = Convert.ToString(rx["NOMTTEC3"]);
                                row["NOMTTEC4"] = Convert.ToString(rx["NOMTTEC4"]);
                                row["NOMTTEC5"] = Convert.ToString(rx["NOMTTEC5"]);
                                row["NOMTTEC7"] = Convert.ToString(rx["NOMTTEC7"]);
                                row["BARRAS"] = Convert.ToString(rx["BARRAS"]);

                                row["CD_REFPRO"] = lc_RefOrigen;

                                tbItems.Rows.Add(row);
                                row = null;
                                if (i > 1)
                                    lc_msj += Convert.ToString(rx["ARCLAVE1"]) + "-";
                            }
                            
                        }
                    }
                }

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                }

                if (!string.IsNullOrEmpty(lc_msj))
                {
                    litTextoMensaje.Text = "Alerta: Referencias con Duplicidad, Validar! " + lc_msj;
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void procesa_plano_fac(Stream inStream)
        {
            ArticulosBL Obj = new ArticulosBL();
            string lc_msj = "";
            int i = 0;
            try
            {
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {                            
                            string lc_RefOrigen = "";
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            string lc_referencia = words[0];
                            string lc_c2 = words[1];
                            string lc_c3 = words[2];
                            string lc_c4 = words[3];
                            int ln_cantidad = Convert.ToInt32(words[4]);
                            double ln_precio = Convert.ToDouble(words[5]);
                            string lc_factura = words[6];
                            DateTime ld_fecha = Convert.ToDateTime(words[7]);
                            string lc_refproveedor = words[8];
                            int ln_dias = Convert.ToInt32(words[9]);
                            string lc_pais = words[10];
                            string lc_codarancel = words[11];
                            string lc_foc = words[12];

                            lc_referencia = Obj.GetReffromOrigen(null, Convert.ToString(Session["CODEMP"]), words[0]);

                            if (string.IsNullOrEmpty(lc_referencia))
                                lc_referencia = words[0];
                            else
                                lc_RefOrigen = words[0];


                            i = 0;
                            foreach (DataRow rx in Obj.GetArticulos(null, " AND ARCLAVE1='" + lc_referencia + "' AND ARCLAVE2 ='" + lc_c2 + "' AND ARCLAVE3='" + lc_c3 + "' AND ARCLAVE4='" + lc_c4 + "'").Rows)
                            {
                                i++;
                                DataRow row = tbFactura.NewRow();
                                row["FD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["FD_NROCMP"] = 0;
                                row["FD_NROITEM"] = tbFactura.Rows.Count + 1;
                                row["FD_BODEGA"] = "";
                                row["FD_TIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                row["FD_CLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                row["FD_CLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                row["FD_CLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                row["FD_CLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                row["FD_PROVEE"] = 0;
                                row["FD_REFPRO"] = ".";
                                row["FD_COLPRO"] = ".";
                                row["FD_CANTIDAD"] = ln_cantidad;
                                row["FD_UNIDAD"] = "UN";
                                row["FD_PRECIO"] = ln_precio;
                                row["FD_OBSERVACIONES"] = "";
                                row["FD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                                row["FD_ESTADO"] = "AC";
                                row["FD_FECING"] = System.DateTime.Today;
                                row["FD_FECMOD"] = System.DateTime.Today;
                                row["LOT1"] = "";
                                row["LOT2"] = "";
                                row["CLAVE2"] = Convert.ToString(rx["CLAVE2"]);
                                row["CLAVE3"] = Convert.ToString(rx["CLAVE3"]);
                                row["CLAVE4"] = "";
                                row["ENLACE"] = "";
                                row["VLRTOT"] = (ln_cantidad * ln_precio);
                                row["TANOMBRE"] = Convert.ToString(rx["TANOMBRE"]);
                                row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                row["CANRECIBE"] = 0;
                                row["CANRESTANTE"] = 0;
                                row["FD_ORIGEN"] = lc_pais;
                                row["FD_POSARA"] = lc_codarancel;

                                row["FD_NROFACTURA"] = lc_factura;
                                row["FD_FECFAC"] = ld_fecha;
                                row["FD_REFERENCIA"] = lc_refproveedor;
                                row["FD_DIAS"] = ln_dias;

                                row["ARDTTEC1"] = Convert.ToString(rx["ARDTTEC1"]);
                                row["ARDTTEC2"] = Convert.ToString(rx["ARDTTEC2"]);
                                row["ARDTTEC3"] = Convert.ToString(rx["ARDTTEC3"]);
                                row["ARDTTEC4"] = Convert.ToString(rx["ARDTTEC4"]);
                                row["ARDTTEC5"] = Convert.ToString(rx["ARDTTEC5"]);
                                row["ARDTTEC6"] = 0;
                                row["ARDTTEC7"] = Convert.ToString(rx["ARDTTEC7"]);
                                row["ARDTTEC8"] = Convert.ToString(rx["ARDTTEC8"]);
                                row["NOMTTEC1"] = Convert.ToString(rx["NOMTTEC1"]);
                                row["NOMTTEC2"] = Convert.ToString(rx["NOMTTEC2"]);
                                row["NOMTTEC3"] = Convert.ToString(rx["NOMTTEC3"]);
                                row["NOMTTEC4"] = Convert.ToString(rx["NOMTTEC4"]);
                                row["NOMTTEC5"] = Convert.ToString(rx["NOMTTEC5"]);
                                row["NOMTTEC7"] = Convert.ToString(rx["NOMTTEC7"]);
                                row["BARRAS"] = Convert.ToString(rx["BARRAS"]);

                                row["FD_REFPRO"] = lc_RefOrigen;

                                row["FD_PAGO"] = lc_foc;

                                tbFactura.Rows.Add(row);
                                row = null;

                                this.InsertSummari(lc_factura, 3);

                                if (i > 1)
                                    lc_msj += Convert.ToString(rx["ARCLAVE1"]) + "-";
                            }
                            
                        }
                    }
                }

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                }

                //Val
                if (!string.IsNullOrEmpty(lc_msj))
                {
                    litTextoMensaje.Text = "Alerta: Referencias con Duplicidad, Validar! " + lc_msj;
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            finally
            {
                Obj = null;
            }
        }
        protected void procesa_plano_pro(Stream inStream)
        {
            ArticulosBL Obj = new ArticulosBL();
            string lc_msj = "";
            int i = 0;
            try
            {
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            
                            string lc_RefOrigen = "";
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            string lc_referencia = words[0];
                            string lc_c2 = words[1];
                            string lc_c3 = words[2];
                            string lc_c4 = words[3];
                            int ln_cantidad = Convert.ToInt32(words[4]);
                            double ln_precio = Convert.ToDouble(words[5]);
                            string lc_factura = words[6];
                            DateTime ld_fecha = Convert.ToDateTime(words[7]);
                            string lc_refproveedor = words[8];
                            int ln_dias = Convert.ToInt32(words[9]);
                            string lc_pais = words[10];
                            string lc_codarancel = words[11];
                            string lc_foc = words[12];

                            lc_referencia = Obj.GetReffromOrigen(null, Convert.ToString(Session["CODEMP"]), words[0]);

                            if (string.IsNullOrEmpty(lc_referencia))
                                lc_referencia = words[0];
                            else
                                lc_RefOrigen = words[0];

                            i= 0;
                            foreach (DataRow rx in Obj.GetArticulos(null, " AND ARCLAVE1='" + lc_referencia + "' AND ARCLAVE2 ='" + lc_c2 + "' AND ARCLAVE3='" + lc_c3 + "' AND ARCLAVE4='" + lc_c4 + "'").Rows)
                            {
                                i++;
                                DataRow row = tbProforma.NewRow();

                                row["PR_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["PR_NROCMP"] = 0;
                                row["PR_NROITEM"] = tbProforma.Rows.Count + 1;
                                row["PR_BODEGA"] = "";
                                row["PR_TIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                row["PR_CLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                row["PR_CLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                row["PR_CLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                row["PR_CLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                row["PR_PROVEE"] = 0;
                                row["PR_REFPRO"] = ".";
                                row["PR_COLPRO"] = ".";
                                row["PR_CANTIDAD"] = ln_cantidad;
                                row["PR_UNIDAD"] = "UN";
                                row["PR_PRECIO"] = ln_precio;
                                row["PR_OBSERVACIONES"] = "";
                                row["PR_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                                row["PR_ESTADO"] = "AC";
                                row["PR_FECING"] = System.DateTime.Today;
                                row["PR_FECMOD"] = System.DateTime.Today;
                                row["LOT1"] = "";
                                row["LOT2"] = "";
                                row["CLAVE2"] = Convert.ToString(rx["CLAVE2"]);
                                row["CLAVE3"] = Convert.ToString(rx["CLAVE3"]);
                                row["CLAVE4"] = "";
                                row["ENLACE"] = "";
                                row["VLRTOT"] = (ln_cantidad * ln_precio);
                                row["TANOMBRE"] = Convert.ToString(rx["TANOMBRE"]);
                                row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                row["CANRECIBE"] = 0;
                                row["CANRESTANTE"] = 0;

                                row["PR_ORIGEN"] = lc_pais;
                                row["PR_POSARA"] = lc_codarancel;

                                row["PR_NROFACPROFORMA"] = lc_factura;
                                row["PR_FECPROFORMA"] = ld_fecha;
                                row["PR_REFERENCIA"] = lc_refproveedor;
                                row["PR_DIAS"] = ln_dias;

                                row["ARDTTEC1"] = Convert.ToString(rx["ARDTTEC1"]);
                                row["ARDTTEC2"] = Convert.ToString(rx["ARDTTEC2"]);
                                row["ARDTTEC3"] = Convert.ToString(rx["ARDTTEC3"]);
                                row["ARDTTEC4"] = Convert.ToString(rx["ARDTTEC4"]);
                                row["ARDTTEC5"] = Convert.ToString(rx["ARDTTEC5"]);
                                row["ARDTTEC6"] = 0;
                                row["ARDTTEC7"] = Convert.ToString(rx["ARDTTEC7"]);
                                row["ARDTTEC8"] = Convert.ToString(rx["ARDTTEC8"]);
                                row["NOMTTEC1"] = Convert.ToString(rx["NOMTTEC1"]);
                                row["NOMTTEC2"] = Convert.ToString(rx["NOMTTEC2"]);
                                row["NOMTTEC3"] = Convert.ToString(rx["NOMTTEC3"]);
                                row["NOMTTEC4"] = Convert.ToString(rx["NOMTTEC4"]);
                                row["NOMTTEC5"] = Convert.ToString(rx["NOMTTEC5"]);
                                row["NOMTTEC7"] = Convert.ToString(rx["NOMTTEC7"]);
                                row["BARRAS"] = Convert.ToString(rx["BARRAS"]);

                                row["PR_REFPRO"] = lc_RefOrigen;

                                row["PR_PAGO"] = lc_foc;

                                tbProforma.Rows.Add(row);
                                row = null;

                                this.InsertSummari(lc_factura, 2);

                                

                                if (i > 1)
                                    lc_msj += Convert.ToString(rx["ARCLAVE1"]) + "-";
                            }
                            
                        }
                    }
                }

                

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                }

                //Val
                if (!string.IsNullOrEmpty(lc_msj))
                {
                    litTextoMensaje.Text = "Alerta: Referencias con Duplicidad, Validar! " + lc_msj;
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                //throw ex;
            }
            finally
            {

                Obj = null;
            }
        }
        protected void btn_procesar_Aceptar(object sender, EventArgs e)
        {
            switch (gc_tipo)
            {
                case "OC":
                    ltr_cargue.Text = "C1;C2;C3;C4;CAN;PRE / BARRAS;CAN;PRE";
                    this.procesa_plano_cmp(File.OpenRead(prArchivo));
                    break;
                case "PR":
                    ltr_cargue.Text = "C1;C2;C3;C4;CAN;PRE;FAC;FEC;RPRO;DIAS;PAIS;CODARN / BARRAS;CAN;PRE;PRE;FAC;FEC;RPRO;DIAS;PAIS;CODARN";
                    this.procesa_plano_pro(File.OpenRead(prArchivo));
                    break;
                case "FC":
                    ltr_cargue.Text = "C1;C2;C3;C4;CAN;PRE;FAC;FEC;RPRO;DIAS;PAIS;CODARN / BARRAS;CAN;PRE;PRE;FAC;FEC;RPRO;DIAS;PAIS;CODARN";
                    this.procesa_plano_fac(File.OpenRead(prArchivo));
                    break;
            }
        }
        protected void ct_bodegas_ItemClick(object sender, RadMenuEventArgs e)
        {
            string radGridClickedRowIndex;
            radGridClickedRowIndex = Convert.ToString(Request.Form["radGridClickedRowIndex"]);
            String[] sub = radGridClickedRowIndex.Split('-');
            gc_origen = sub[0];
            string index = sub[1];
            switch (e.Item.Text)
            {
                case "Cargar Orden Compra":
                    if (gc_origen == "2")
                    {
                        foreach (DataRow rw in tbProforma.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbItems, "PR", "CD");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                            (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                        }
                    }
                    if (gc_origen == "3")
                    {
                        foreach (DataRow rw in tbFactura.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbItems, "FD", "CD");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                            (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                        }
                    }
                    litTextoMensaje.Text = "Transferencia Realizada!";                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "Cargar Factura Proforma":
                    if (gc_origen == "1")
                    {
                        foreach (DataRow rw in tbItems.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbProforma, "CD", "PR");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                            (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                            (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                        }
                    }
                    if (gc_origen == "3")
                    {
                        foreach (DataRow rw in tbFactura.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbProforma, "FD", "PR");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                            (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                            (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                        }
                    }
                    litTextoMensaje.Text = "Transferencia Realizada!";                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "Cargar Factura":
                    if (gc_origen == "1")
                    {
                        foreach (DataRow rw in tbItems.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbFactura, "CD", "FD");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                            (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                            (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                        }
                    }
                    if (gc_origen == "2")
                    {
                        foreach (DataRow rw in tbProforma.Rows)
                        {
                            this.TrasladarInformacionTablas(rw, tbFactura, "PR", "FD");
                        }
                        if (rlv_compras.InsertItem != null)
                        {
                            (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                            (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                        }
                        else
                        {
                            (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                            (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                        }
                    }
                    litTextoMensaje.Text = "Transferencia Realizada!";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "Cargar Nro Doc-Fecha":
                    if (gc_origen =="2"|| gc_origen == "3")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalFechaNro.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "Cargar Pais-UN Aran":
                    if (gc_origen == "2" || gc_origen == "3")
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalPaisUNAr.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
            }
            

        }
        // Funciones de Insert en cada Pestaña Orden Compra/Proforma/Factura
        #region
        private void InsertOrdenCompra()
        {
            DataRow row = tbItems.NewRow();
            try
            {
                row["CD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["CD_NROCMP"] = 0;
                row["CD_NROITEM"] = this.GetMaxItemOrdenCOmpra() + 1;
                row["CD_BODEGA"] = "";
                row["CD_TIPPRO"] = rc_categoria.SelectedValue;
                row["CD_CLAVE1"] = txt_referencia.Text;
                row["CD_CLAVE2"] = txt_clave2.Text;
                row["CD_CLAVE3"] = txt_clave3.Text;
                row["CD_CLAVE4"] = txt_clave4.Text;
                row["CD_PROVEE"] = 0;
                row["CD_REFPRO"] = txt_rproveedor.Text;
                row["CD_COLPRO"] = txt_cproveedor.Text;
                row["CD_CANTIDAD"] = txt_cantidad.Value;
                row["CD_UNIDAD"] = "UN";
                row["CD_PRECIO"] = txt_precio.Value;
                row["CD_OBSERVACIONES"] = txt_observaciones.Text;
                row["CD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["CD_ESTADO"] = "AC";
                row["CD_FECING"] = System.DateTime.Today;
                row["CD_FECMOD"] = System.DateTime.Today;
                row["LOT1"] = "";
                row["LOT2"] = "";
                row["CLAVE2"] = "";
                row["CLAVE3"] = "";
                row["CLAVE4"] = "";
                row["BARRAS"] = txt_barras.Text;
                row["ARDTTEC1"] = rc_dt1.SelectedValue;
                row["ARDTTEC2"] = rc_dt2.SelectedValue;
                row["ARDTTEC3"] = rc_dt3.SelectedValue;
                row["ARDTTEC4"] = rc_dt4.SelectedValue;
                row["ARDTTEC5"] = rc_dt5.SelectedValue;
                row["ARDTTEC6"] = 0;
                row["ARDTTEC7"] = rc_dt7.SelectedValue;
                row["ARDTTEC8"] = rc_dt8.SelectedValue;
                row["NOMTTEC1"] = rc_dt1.Text;
                row["NOMTTEC2"] = rc_dt2.Text;
                row["NOMTTEC3"] = rc_dt3.Text;
                row["NOMTTEC4"] = rc_dt4.Text;
                row["NOMTTEC5"] = rc_dt5.Text; 
                row["NOMTTEC7"] = rc_dt7.Text;
                row["ENLACE"] = "";
                row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                row["TANOMBRE"] = rc_categoria.Text;
                row["ARNOMBRE"] = txt_descripcion.Text;
                row["CANRECIBE"] = 0;
                row["CANRESTANTE"] = 0;
                row["CD_PAGO"] = chk_foc.Checked ? "S" : "N";

                tbItems.Rows.Add(row);

                //this.InsertSummari();

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
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
        private void UpdateOrdenCompra()
        {            
            try
            {
                foreach(DataColumn x in tbItems.Columns)
                    x.ReadOnly = false;

                foreach (DataRow row in tbItems.Rows)
                {
                    if (txt_item.Text == Convert.ToString(row["CD_NROITEM"]))
                    {
                        row["CD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["CD_NROCMP"] = 0;
                        row["CD_BODEGA"] = "";
                        row["CD_TIPPRO"] = rc_categoria.SelectedValue;
                        row["CD_CLAVE1"] = txt_referencia.Text;
                        row["CD_CLAVE2"] = txt_clave2.Text;
                        row["CD_CLAVE3"] = txt_clave3.Text;
                        row["CD_CLAVE4"] = txt_clave4.Text;
                        row["CD_PROVEE"] = 0;
                        row["CD_REFPRO"] = txt_rproveedor.Text;
                        row["CD_COLPRO"] = txt_cproveedor.Text;
                        row["CD_CANTIDAD"] = txt_cantidad.Value;
                        row["CD_UNIDAD"] = "UN";
                        row["CD_PRECIO"] = txt_precio.Value;
                        row["CD_OBSERVACIONES"] = txt_observaciones.Text;
                        row["CD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                        row["CD_ESTADO"] = "AC";
                        row["CD_FECING"] = System.DateTime.Today;
                        row["CD_FECMOD"] = System.DateTime.Today;
                        row["LOT1"] = "";
                        row["LOT2"] = "";
                        row["CLAVE2"] = "";
                        row["CLAVE3"] = "";
                        row["CLAVE4"] = "";
                        row["BARRAS"] = txt_barras.Text;
                        row["ARDTTEC1"] = rc_dt1.SelectedValue;
                        row["ARDTTEC2"] = rc_dt2.SelectedValue;
                        row["ARDTTEC3"] = rc_dt3.SelectedValue;
                        row["ARDTTEC4"] = rc_dt4.SelectedValue;
                        row["ARDTTEC5"] = rc_dt5.SelectedValue;
                        row["ARDTTEC6"] = 0;
                        row["ARDTTEC7"] = rc_dt7.SelectedValue;
                        row["ARDTTEC8"] = rc_dt8.SelectedValue;
                        row["NOMTTEC1"] = rc_dt1.Text;
                        row["NOMTTEC2"] = rc_dt2.Text;
                        row["NOMTTEC3"] = rc_dt3.Text;
                        row["NOMTTEC4"] = rc_dt4.Text;
                        row["NOMTTEC5"] = rc_dt5.Text;
                        row["NOMTTEC7"] = rc_dt7.Text;
                        row["ENLACE"] = "";
                        row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                        row["TANOMBRE"] = rc_categoria.Text;
                        row["ARNOMBRE"] = txt_descripcion.Text;
                        row["CANRECIBE"] = 0;
                        row["CANRESTANTE"] = 0;
                        row["CD_PAGO"] = chk_foc.Checked ? "S" : "N";
                    }
                }               

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            

        }
        private void InsertProforma()
        {
            DataRow row = tbProforma.NewRow();
            try
            {
                row["PR_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["PR_NROCMP"] = 0;
                row["PR_NROITEM"] = this.GetMaxItemProforma() + 1;
                row["PR_BODEGA"] = "";
                row["PR_TIPPRO"] = rc_categoria.SelectedValue;
                row["PR_CLAVE1"] = txt_referencia.Text;
                row["PR_CLAVE2"] = txt_clave2.Text;
                row["PR_CLAVE3"] = txt_clave3.Text;
                row["PR_CLAVE4"] = txt_clave4.Text;
                row["PR_PROVEE"] = 0;
                row["PR_REFPRO"] = txt_rproveedor.Text;
                row["PR_COLPRO"] = txt_cproveedor.Text;
                row["PR_CANTIDAD"] = txt_cantidad.Value;
                row["PR_UNIDAD"] = "UN";
                row["PR_PRECIO"] = txt_precio.Value;
                row["PR_OBSERVACIONES"] = txt_observaciones.Text;
                row["PR_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["PR_ESTADO"] = "AC";
                row["PR_FECING"] = System.DateTime.Today;
                row["PR_FECMOD"] = System.DateTime.Today;
                row["LOT1"] = "";
                row["LOT2"] = "";
                row["CLAVE2"] = "";
                row["CLAVE3"] = "";
                row["CLAVE4"] = "";
                row["ENLACE"] = "";
                row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                row["TANOMBRE"] = rc_categoria.Text;
                row["ARNOMBRE"] = txt_descripcion.Text;
                row["CANRECIBE"] = 0;
                row["CANRESTANTE"] = 0;

                row["PR_NROFACPROFORMA"] = edt_dcmt.Text;
                row["PR_FECPROFORMA"] = edt_fecha.SelectedDate;

                row["BARRAS"] = txt_barras.Text;

                row["ARDTTEC1"] = rc_dt1.SelectedValue;
                row["ARDTTEC2"] = rc_dt2.SelectedValue;
                row["ARDTTEC3"] = rc_dt3.SelectedValue;
                row["ARDTTEC4"] = rc_dt4.SelectedValue;
                row["ARDTTEC5"] = rc_dt5.SelectedValue;
                row["ARDTTEC6"] = 0;
                row["ARDTTEC7"] = rc_dt7.SelectedValue;
                row["ARDTTEC8"] = rc_dt8.SelectedValue;

                row["NOMTTEC1"] = rc_dt1.Text;
                row["NOMTTEC2"] = rc_dt2.Text;
                row["NOMTTEC3"] = rc_dt3.Text;
                row["NOMTTEC4"] = rc_dt4.Text;
                row["NOMTTEC5"] = rc_dt5.Text; 
                row["NOMTTEC7"] = rc_dt7.Text;

                row["PR_PAGO"] = chk_foc.Checked ? "S" : "N";

                tbProforma.Rows.Add(row);

                this.InsertSummari(edt_dcmt.Text,2);

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
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
        private void UpdateProforma()
        {            
            try
            {
                foreach (DataColumn x in tbProforma.Columns)
                    x.ReadOnly = false;

                foreach (DataRow row in tbProforma.Rows)
                {
                    if (txt_item.Text == Convert.ToString(row["PR_NROITEM"]))
                    {
                        row["PR_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["PR_NROCMP"] = 0;
                        row["PR_BODEGA"] = "";
                        row["PR_TIPPRO"] = rc_categoria.SelectedValue;
                        row["PR_CLAVE1"] = txt_referencia.Text;
                        row["PR_CLAVE2"] = txt_clave2.Text;
                        row["PR_CLAVE3"] = txt_clave3.Text;
                        row["PR_CLAVE4"] = txt_clave4.Text;
                        row["PR_PROVEE"] = 0;
                        row["PR_REFPRO"] = txt_rproveedor.Text;
                        row["PR_COLPRO"] = txt_cproveedor.Text;
                        row["PR_CANTIDAD"] = txt_cantidad.Value;
                        row["PR_UNIDAD"] = "UN";
                        row["PR_PRECIO"] = txt_precio.Value;
                        row["PR_OBSERVACIONES"] = txt_observaciones.Text;
                        row["PR_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                        row["PR_ESTADO"] = "AC";
                        row["PR_FECING"] = System.DateTime.Today;
                        row["PR_FECMOD"] = System.DateTime.Today;
                        row["LOT1"] = "";
                        row["LOT2"] = "";
                        row["CLAVE2"] = "";
                        row["CLAVE3"] = "";
                        row["CLAVE4"] = "";
                        row["ENLACE"] = "";
                        row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                        row["TANOMBRE"] = rc_categoria.Text;
                        row["ARNOMBRE"] = txt_descripcion.Text;
                        row["CANRECIBE"] = 0;
                        row["CANRESTANTE"] = 0;

                        row["PR_NROFACPROFORMA"] = edt_dcmt.Text;
                        row["PR_FECPROFORMA"] = edt_fecha.SelectedDate;

                        row["BARRAS"] = txt_barras.Text;

                        row["ARDTTEC1"] = rc_dt1.SelectedValue;
                        row["ARDTTEC2"] = rc_dt2.SelectedValue;
                        row["ARDTTEC3"] = rc_dt3.SelectedValue;
                        row["ARDTTEC4"] = rc_dt4.SelectedValue;
                        row["ARDTTEC5"] = rc_dt5.SelectedValue;
                        row["ARDTTEC6"] = 0;
                        row["ARDTTEC7"] = rc_dt7.SelectedValue;
                        row["ARDTTEC8"] = rc_dt8.SelectedValue;

                        row["NOMTTEC1"] = rc_dt1.Text;
                        row["NOMTTEC2"] = rc_dt2.Text;
                        row["NOMTTEC3"] = rc_dt3.Text;
                        row["NOMTTEC4"] = rc_dt4.Text;
                        row["NOMTTEC5"] = rc_dt5.Text;
                        row["NOMTTEC7"] = rc_dt7.Text;

                        row["PR_PAGO"] = chk_foc.Checked ? "S" : "N";

                    }                    
                }

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void InsertFactura()
        {
            DataRow row = tbFactura.NewRow();
            try
            {
                row["FD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["FD_NROCMP"] = 0;
                row["FD_NROITEM"] = this.GetMaxItemFactura() + 1;
                row["FD_BODEGA"] = "";
                row["FD_TIPPRO"] = rc_categoria.SelectedValue;
                row["FD_CLAVE1"] = txt_referencia.Text;
                row["FD_CLAVE2"] = txt_clave2.Text;
                row["FD_CLAVE3"] = txt_clave3.Text;
                row["FD_CLAVE4"] = txt_clave4.Text;
                row["FD_PROVEE"] = 0;
                row["FD_REFPRO"] = txt_rproveedor.Text;
                row["FD_COLPRO"] = txt_cproveedor.Text;
                row["FD_CANTIDAD"] = txt_cantidad.Value;
                row["FD_UNIDAD"] = "UN";
                row["FD_PRECIO"] = txt_precio.Value;
                row["FD_OBSERVACIONES"] = txt_observaciones.Text;
                row["FD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["FD_ESTADO"] = "AC";
                row["FD_FECING"] = System.DateTime.Today;
                row["FD_FECMOD"] = System.DateTime.Today;
                row["LOT1"] = "";
                row["LOT2"] = "";
                row["CLAVE2"] = "";
                row["CLAVE3"] = "";
                row["CLAVE4"] = "";
                row["ENLACE"] = "";
                row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                row["TANOMBRE"] = rc_categoria.Text;
                row["ARNOMBRE"] = txt_descripcion.Text;
                row["CANRECIBE"] = 0;
                row["CANRESTANTE"] = 0;

                row["FD_NROFACTURA"] = edt_dcmt.Text;
                row["FD_FECFAC"] = edt_fecha.SelectedDate;

                row["BARRAS"] = txt_barras.Text;

                row["ARDTTEC1"] = rc_dt1.SelectedValue;
                row["ARDTTEC2"] = rc_dt2.SelectedValue;
                row["ARDTTEC3"] = rc_dt3.SelectedValue;
                row["ARDTTEC4"] = rc_dt4.SelectedValue;
                row["ARDTTEC5"] = rc_dt5.SelectedValue;
                row["ARDTTEC6"] = 0;
                row["ARDTTEC7"] = rc_dt7.SelectedValue;
                row["ARDTTEC8"] = rc_dt8.SelectedValue;

                row["NOMTTEC1"] = rc_dt1.Text;
                row["NOMTTEC2"] = rc_dt2.Text;
                row["NOMTTEC3"] = rc_dt3.Text;
                row["NOMTTEC4"] = rc_dt4.Text;
                row["NOMTTEC5"] = rc_dt5.Text; 
                row["NOMTTEC7"] = rc_dt7.Text;

                row["FD_PAGO"] = chk_foc.Checked ? "S" : "N";

                tbFactura.Rows.Add(row);
                this.InsertSummari(edt_dcmt.Text, 3);

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
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
        private void UpdateFactura()
        {            
            try
            {
                foreach (DataColumn x in tbFactura.Columns)
                    x.ReadOnly = false;

                foreach (DataRow row in tbFactura.Rows)
                {
                    if (txt_item.Text == Convert.ToString(row["FD_NROITEM"]))
                    {
                        row["FD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["FD_NROCMP"] = 0;
                        row["FD_BODEGA"] = "";
                        row["FD_TIPPRO"] = rc_categoria.SelectedValue;
                        row["FD_CLAVE1"] = txt_referencia.Text;
                        row["FD_CLAVE2"] = txt_clave2.Text;
                        row["FD_CLAVE3"] = txt_clave3.Text;
                        row["FD_CLAVE4"] = txt_clave4.Text;
                        row["FD_PROVEE"] = 0;
                        row["FD_REFPRO"] = txt_rproveedor.Text;
                        row["FD_COLPRO"] = txt_cproveedor.Text;
                        row["FD_CANTIDAD"] = txt_cantidad.Value;
                        row["FD_UNIDAD"] = "UN";
                        row["FD_PRECIO"] = txt_precio.Value;
                        row["FD_OBSERVACIONES"] = txt_observaciones.Text;
                        row["FD_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                        row["FD_ESTADO"] = "AC";
                        row["FD_FECING"] = System.DateTime.Today;
                        row["FD_FECMOD"] = System.DateTime.Today;
                        row["LOT1"] = "";
                        row["LOT2"] = "";
                        row["CLAVE2"] = "";
                        row["CLAVE3"] = "";
                        row["CLAVE4"] = "";
                        row["ENLACE"] = "";
                        row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                        row["TANOMBRE"] = rc_categoria.Text;
                        row["ARNOMBRE"] = txt_descripcion.Text;
                        row["CANRECIBE"] = 0;
                        row["CANRESTANTE"] = 0;

                        row["FD_NROFACTURA"] = edt_dcmt.Text;
                        row["FD_FECFAC"] = edt_fecha.SelectedDate;

                        row["BARRAS"] = txt_barras.Text;

                        row["ARDTTEC1"] = rc_dt1.SelectedValue;
                        row["ARDTTEC2"] = rc_dt2.SelectedValue;
                        row["ARDTTEC3"] = rc_dt3.SelectedValue;
                        row["ARDTTEC4"] = rc_dt4.SelectedValue;
                        row["ARDTTEC5"] = rc_dt5.SelectedValue;
                        row["ARDTTEC6"] = 0;
                        row["ARDTTEC7"] = rc_dt7.SelectedValue;
                        row["ARDTTEC8"] = rc_dt8.SelectedValue;

                        row["NOMTTEC1"] = rc_dt1.Text;
                        row["NOMTTEC2"] = rc_dt2.Text;
                        row["NOMTTEC3"] = rc_dt3.Text;
                        row["NOMTTEC4"] = rc_dt4.Text;
                        row["NOMTTEC5"] = rc_dt5.Text;
                        row["NOMTTEC7"] = rc_dt7.Text;

                        row["FD_PAGO"] = chk_foc.Checked ? "S" : "N";
                    }
                }
                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                    (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private int GetMaxItemOrdenCOmpra()
        {
            int ln_item = 0;
            foreach (DataRow rw in tbItems.Rows)
            {
                if (ln_item < Convert.ToInt32(rw["CD_NROITEM"]))
                    ln_item = Convert.ToInt32(rw["CD_NROITEM"]);
            }
            return ln_item;
        }
        private int GetMaxItemProforma()
        {
            int ln_item = 0;
            foreach (DataRow rw in tbProforma.Rows)
            {
                if (ln_item < Convert.ToInt32(rw["PR_NROITEM"]))
                    ln_item = Convert.ToInt32(rw["PR_NROITEM"]);
            }
            return ln_item;
        }
        private int GetMaxItemFactura()
        {
            int ln_item = 0;
            foreach (DataRow rw in tbFactura.Rows)
            {
                if (ln_item < Convert.ToInt32(rw["FD_NROITEM"]))
                    ln_item = Convert.ToInt32(rw["FD_NROITEM"]);
            }
            return ln_item;
        }
        #endregion
        //Funciones para Insertar para Trasladar Informaicon entre tablas
        private void TrasladarInformacionTablas(DataRow inRwOrigen, DataTable inDtDestino, string inPreOrigen, string inPreDestino)
        {
            DataRow dw = inDtDestino.NewRow();
            try
            {

                dw[inPreDestino + "_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                dw[inPreDestino + "_NROCMP"] = inRwOrigen[inPreOrigen + "_NROCMP"];
                dw[inPreDestino + "_NROITEM"] = inDtDestino.Rows.Count + 1;
                dw[inPreDestino + "_BODEGA"] = inRwOrigen[inPreOrigen + "_BODEGA"];
                dw[inPreDestino + "_TIPPRO"] = inRwOrigen[inPreOrigen + "_TIPPRO"];
                dw[inPreDestino + "_CLAVE1"] = inRwOrigen[inPreOrigen + "_CLAVE1"];
                dw[inPreDestino + "_CLAVE2"] = inRwOrigen[inPreOrigen + "_CLAVE2"];
                dw[inPreDestino + "_CLAVE3"] = inRwOrigen[inPreOrigen + "_CLAVE3"];
                dw[inPreDestino + "_CLAVE4"] = inRwOrigen[inPreOrigen + "_CLAVE4"];
                dw[inPreDestino + "_PROVEE"] = inRwOrigen[inPreOrigen + "_PROVEE"];
                dw[inPreDestino + "_REFPRO"] = inRwOrigen[inPreOrigen + "_REFPRO"];
                dw[inPreDestino + "_COLPRO"] = inRwOrigen[inPreOrigen + "_COLPRO"];
                dw[inPreDestino + "_CANTIDAD"] = inRwOrigen[inPreOrigen + "_CANTIDAD"];
                dw[inPreDestino + "_UNIDAD"] = inRwOrigen[inPreOrigen + "_UNIDAD"];
                dw[inPreDestino + "_PRECIO"] = inRwOrigen[inPreOrigen + "_PRECIO"];
                dw[inPreDestino + "_OBSERVACIONES"] = inRwOrigen[inPreOrigen + "_OBSERVACIONES"];
                dw[inPreDestino + "_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                dw[inPreDestino + "_ESTADO"] = "AC";
                dw[inPreDestino + "_FECING"] = System.DateTime.Today;
                dw[inPreDestino + "_FECMOD"] = System.DateTime.Today;
                dw["LOT1"] = inRwOrigen["LOT1"];
                dw["LOT2"] = inRwOrigen["LOT2"];
                dw["CLAVE2"] = inRwOrigen["CLAVE2"];
                dw["CLAVE3"] = inRwOrigen["CLAVE3"];
                dw["CLAVE4"] = inRwOrigen["CLAVE4"];
                dw["ENLACE"] = inRwOrigen["ENLACE"];
                dw["CANRECIBE"] = inRwOrigen["CANRECIBE"];
                dw["CANRESTANTE"] = inRwOrigen["CANRESTANTE"];
                dw["VLRTOT"] = inRwOrigen["VLRTOT"];
                dw["TANOMBRE"] = inRwOrigen["TANOMBRE"];
                dw["ARNOMBRE"] = inRwOrigen["ARNOMBRE"];

                dw["ARDTTEC1"] = inRwOrigen["ARDTTEC1"];
                dw["ARDTTEC2"] = inRwOrigen["ARDTTEC2"];
                dw["ARDTTEC3"] = inRwOrigen["ARDTTEC3"];
                dw["ARDTTEC4"] = inRwOrigen["ARDTTEC4"];
                dw["ARDTTEC5"] = inRwOrigen["ARDTTEC5"];
                dw["ARDTTEC6"] = inRwOrigen["ARDTTEC6"];
                dw["ARDTTEC7"] = inRwOrigen["ARDTTEC7"];
                dw["ARDTTEC8"] = inRwOrigen["ARDTTEC8"];

                dw["NOMTTEC1"] = inRwOrigen["NOMTTEC1"];
                dw["NOMTTEC2"] = inRwOrigen["NOMTTEC2"];
                dw["NOMTTEC3"] = inRwOrigen["NOMTTEC3"];
                dw["NOMTTEC4"] = inRwOrigen["NOMTTEC4"];
                dw["NOMTTEC5"] = inRwOrigen["NOMTTEC5"];
                dw["NOMTTEC7"] = inRwOrigen["NOMTTEC7"];

                dw["BARRAS"] = inRwOrigen["BARRAS"];



                inDtDestino.Rows.Add(dw);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dw = null;
            }
        }
        protected void rg_proforma_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var DTNROITM = item.GetDataKeyValue("PR_NROITEM").ToString();
                        int pos = 0;
                        int xpos = 0;
                        foreach (DataRow row in tbProforma.Rows)
                        {
                            if (Convert.ToInt32(row["PR_NROITEM"]) == Convert.ToInt32(DTNROITM))
                            {
                                pos = xpos;
                            }
                            xpos++;
                        }
                        tbProforma.Rows[pos].Delete();
                        tbProforma.AcceptChanges();

                        if (rlv_compras.InsertItem == null)
                            Obj.DeleteCMPFacturaPRO(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text), Convert.ToInt32(DTNROITM));
                        //else
                        //{
                            //foreach (DataRow rw in tbProforma.Rows)
                            //{
                            //    rw["PR_NROITEM"] = i;
                            //    i++;
                            //}
                        //}
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
        protected void rg_factura_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            GridEditableItem item = e.Item as GridEditableItem;
            try
            {
                int i = 1;
                switch (e.CommandName)
                {
                    case "Delete":
                        var DTNROITM = item.GetDataKeyValue("FD_NROITEM").ToString();
                        int pos = 0;
                        int xpos = 0;
                        foreach (DataRow row in tbFactura.Rows)
                        {
                            if (Convert.ToInt32(row["FD_NROITEM"]) == Convert.ToInt32(DTNROITM))
                            {
                                pos = xpos;
                            }
                            xpos++;
                        }
                        tbFactura.Rows[pos].Delete();
                        tbFactura.AcceptChanges();

                        if (rlv_compras.InsertItem == null)
                            Obj.DeleteCMPFacturaDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text), Convert.ToInt32(DTNROITM));
                        //else
                        //{
                            //foreach (DataRow rw in tbFactura.Rows)
                            //{
                            //    rw["FD_NROITEM"] = i;
                            //    i++;
                            //}
                        //}

                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                Obj = null;
            }
        }
        protected void rgSoportes_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string lc_extencion = "";
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["SP_CONSECUTIVO"].Text);

                SoportesBL Obj = new SoportesBL();
                foreach (DataRow rw in (Obj.GetSoporte(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["SP_IMAGEN"]);
                    lc_extencion = Convert.ToString(rw["SP_EXTENCION"]);
                }

                ditem = null;
                Random random = new Random();
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + "" + lc_extencion;
                string path = MapPath("~/Uploads/" + lc_nombre);

                File.WriteAllBytes(path, archivo);
                byte[] bts = System.IO.File.ReadAllBytes(path);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lc_nombre);
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
            }
        }
        protected void rauCargarSoporte_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            //pArchivo = e.File.InputStream;
            //prArchivo = e.File.FileName;
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbSoportes.NewRow();
            //byte[] result;
            try
            {
                row["SP_CONSECUTIVO"] = 0;
                row["SP_REFERENCIA"] = 0;
                row["SP_DESCRIPCION"] = (((Button)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text;
                row["SP_EXTENCION"] = Path.GetExtension(Path.GetExtension(prArchivo.Substring(0, prArchivo.Length - 4)));
                row["SP_TIPO"] = "1";
                row["SP_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["RUTA"] = prArchivo;
                row["SP_FECING"] = System.DateTime.Now;

                tbSoportes.Rows.Add(row);
                if (rlv_compras.InsertItem != null) (rlv_compras.InsertItem.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                else (rlv_compras.EditItems[0].FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
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
        protected void edt_forden_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                if ((((RadDatePicker)sender).Parent.FindControl("rc_moneda") as RadComboBox).SelectedValue != "-1")
                {
                    if (Convert.ToString(gc_moneda) != (((RadDatePicker)sender).Parent.FindControl("rc_moneda") as RadComboBox).SelectedValue && Convert.ToDateTime((sender as RadDatePicker).DbSelectedDate) != null)
                    {
                        if (!ComunBL.ExisteTasaCambio(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((((RadDatePicker)sender).Parent.FindControl("rc_moneda") as RadComboBox).SelectedValue), Convert.ToDateTime((sender as RadDatePicker).DbSelectedDate)))
                        {
                            litTextoMensaje.Text = "No Existe Tasa de Cambio para la Fecha Seleccionada";
                            (sender as RadComboBox).SelectedValue = "-1";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        protected void rc_categoria_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                DataTable dt1 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 5);
                DataTable dt2 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 6);
                DataTable dt3 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 7);
                DataTable dt4 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 8);
                DataTable dt5 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 9);
                DataTable dt7 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 10);
                DataTable dt8 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), rc_categoria.SelectedValue, 11);

                rc_dt1.Items.Clear();
                rc_dt2.Items.Clear();
                rc_dt4.Items.Clear();
                rc_dt5.Items.Clear();
                rc_dt7.Items.Clear();
                rc_dt8.Items.Clear();

                item.Value = "-1";
                item.Text = "Seleccionar";

                rc_dt1.Items.Add(item);
                rc_dt2.Items.Add(item);
                rc_dt3.Items.Add(item);
                rc_dt4.Items.Add(item);
                rc_dt5.Items.Add(item);
                rc_dt7.Items.Add(item);
                rc_dt8.Items.Add(item);


                rc_dt1.DataSource = dt1;
                rc_dt2.DataSource = dt2;
                rc_dt3.DataSource = dt3;
                rc_dt4.DataSource = dt4;
                rc_dt5.DataSource = dt5;
                rc_dt7.DataSource = dt7;
                rc_dt8.DataSource = dt8;

                rc_dt1.DataBind();
                rc_dt2.DataBind();
                rc_dt3.DataBind();
                rc_dt4.DataBind();
                rc_dt5.DataBind();
                rc_dt7.DataBind();
                rc_dt8.DataBind();


                rc_categoria.Focus();

                string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
                item = null;
            }
        }
        protected void btn_email_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "";
                url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8004&inSend=S&inban=S&inParametro=inConse&inValor=" + Convert.ToString((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void rg_seguimiento_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                string script = "function f(){$find(\"" + modalSeguimiento.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void btn_agregarseg_Click(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            try {

                if (rlv_compras.InsertItem != null)
                {
                    Obj.InsertSeguimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.InsertItem.FindControl("txt_nroorden") as RadTextBox).Text), txt_obsseguimiento.Text, Convert.ToString(Session["UserLogon"]), "AC");
                    (rlv_compras.InsertItem.FindControl("rg_seguimiento") as RadGrid).DataBind();
                }
                else
                {
                    Obj.InsertSeguimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text), txt_obsseguimiento.Text, Convert.ToString(Session["UserLogon"]), "AC");
                    (rlv_compras.Items[0].FindControl("rg_seguimiento") as RadGrid).DataBind();
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
        protected void btn_aceptarfecnro_Click(object sender, EventArgs e)
        {
            RadGrid l_grid;
            DataTable dt;
            string lc_prefijo;

            try
            {
                if (rlv_compras.InsertItem != null)
                {
                    if (gc_origen == "2")
                    {
                        l_grid = (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid);
                        dt = tbProforma;
                        lc_prefijo = "PR";
                    }
                    else
                    {
                        l_grid = (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid);
                        dt = tbFactura;
                        lc_prefijo = "FD";
                    }
                }
                else
                {
                    if (gc_origen == "2")
                    {
                        l_grid = (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid);
                        dt = tbProforma;
                        lc_prefijo = "PR";
                    }
                    else
                    {
                        l_grid = (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid);
                        dt = tbFactura;
                        lc_prefijo = "FD";
                    }
                }
                    
                foreach (GridDataItem item in l_grid.SelectedItems)
                {
                    int ln_codigo = Convert.ToInt32(item.GetDataKeyValue(lc_prefijo + "_NROITEM"));
                    foreach (DataRow rw in dt.Rows)
                    {
                        if (ln_codigo == Convert.ToInt32(rw[lc_prefijo + "_NROITEM"]))
                        {
                            if (gc_origen == "2")
                            {
                                rw["PR_FECPROFORMA"] = edt_datenew.DbSelectedDate;
                                rw["PR_NROFACPROFORMA"] = txt_nrodocnew.Text;
                                rw["PR_DIAS"] = txt_dias.DbValue;
                            }
                            else
                            {
                                rw["FD_FECFAC"] = edt_datenew.DbSelectedDate;
                                rw["FD_NROFACTURA"] = txt_nrodocnew.Text;
                                rw["FD_DIAS"] = txt_dias.DbValue;
                            }
                        }
                    }
                }


                if (rlv_compras.InsertItem != null)
                {
                    if (gc_origen == "2")
                    {
                        (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                        (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                        (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();                        
                    }
                }
                else
                {
                    if (gc_origen == "2")
                    {
                        (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                        (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                        (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                l_grid = null;
                dt = null;
            }
        }        
        protected void txt_canproedit_TextChanged(object sender, EventArgs e)
        {            

            foreach (DataColumn cl in tbProforma.Columns)
                cl.ReadOnly = false;


            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                    foreach (DataRow row in tbProforma.Rows)
                    {
                        if (Convert.ToInt32(row["PR_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue == null)
                                row["PR_CANTIDAD"] = 0;
                            else
                            {
                                row["PR_CANTIDAD"] = ((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue));
                            }

                            tbProforma.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                    foreach (DataRow row in tbProforma.Rows)
                    {
                        if (Convert.ToInt32(row["PR_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue == null)
                                row["PR_CANTIDAD"] = 0;
                            else
                            {
                                row["PR_CANTIDAD"] = ((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue));
                            }

                            tbProforma.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
            }


            (sender as RadNumericTextBox).Focus();

        }
        protected void btn_aceptarPaisAran_Click(object sender, EventArgs e)
        {
            RadGrid l_grid;
            DataTable dt;
            string lc_prefijo;

            try
            {
                if (rlv_compras.InsertItem != null)
                {
                    if (gc_origen == "2")
                    {
                        l_grid = (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid);
                        dt = tbProforma;
                        lc_prefijo = "PR";
                    }
                    else
                    {
                        l_grid = (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid);
                        dt = tbFactura;
                        lc_prefijo = "FD";
                    }
                }
                else
                {
                    if (gc_origen == "2")
                    {
                        l_grid = (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid);
                        dt = tbProforma;
                        lc_prefijo = "PR";
                    }
                    else
                    {
                        l_grid = (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid);
                        dt = tbFactura;
                        lc_prefijo = "FD";
                    }
                }

                foreach (GridDataItem item in l_grid.SelectedItems)
                {
                    int ln_codigo = Convert.ToInt32(item.GetDataKeyValue(lc_prefijo + "_NROITEM"));
                    foreach (DataRow rw in dt.Rows)
                    {
                        if (ln_codigo == Convert.ToInt32(rw[lc_prefijo + "_NROITEM"]))
                        {
                            if (gc_origen == "2")
                            {
                                rw["PR_ORIGEN"] = rc_newpais.SelectedValue;
                                rw["PR_POSARA"] = txt_newarancel.Text;
                            }
                            else
                            {
                                rw["FD_ORIGEN"] = rc_newpais.SelectedValue;
                                rw["FD_POSARA"] = txt_newarancel.Text;
                            }
                        }
                    }
                }


                if (rlv_compras.InsertItem != null)
                {
                    if (gc_origen == "2")
                    {
                        (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                        (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                        (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
                    }
                }
                else
                {
                    if (gc_origen == "2")
                    {
                        (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                        (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                        (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                l_grid = null;
                dt = null;
            }
        }        
        protected void txt_canfacedit_TextChanged(object sender, EventArgs e)
        {            
            foreach (DataColumn cl in tbFactura.Columns)
                cl.ReadOnly = false;


            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["FD_NROITEM"].Text);
                    foreach (DataRow row in tbFactura.Rows)
                    {
                        if (Convert.ToInt32(row["FD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue == null)
                                row["FD_CANTIDAD"] = 0;
                            else
                            {
                                row["FD_CANTIDAD"] = ((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue));
                            }

                            tbFactura.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["FD_NROITEM"].Text);
                    foreach (DataRow row in tbFactura.Rows)
                    {
                        if (Convert.ToInt32(row["FD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue == null)
                                row["FD_CANTIDAD"] = 0;
                            else
                            {
                                row["FD_CANTIDAD"] = ((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue));
                            }                           

                            tbFactura.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
            }


            (sender as RadNumericTextBox).Focus();
        }
        protected void btn_clsec_Click(object sender, EventArgs e)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Parametros/ClavesAlternas.aspx";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);

        }        
        protected void rg_factura_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "link")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }
        }
        protected void rg_proforma_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "link")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }
        }
        protected void btn_buscarbarras_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_barras.Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarras(null, txt_barras.Text, null);
                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_cantidad.Value = 0;
                            this.btn_findref_Click(sender, null);
                        }

                        btn_agregar_Aceptar(sender, e);

                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        //litTextoMensaje.Text = "Codigo Barras Invalido!";
                        //string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";                        
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);              
                        StringBuilder str = new StringBuilder();

                        str.AppendLine("<strong>¡Codigo de Barras No Existe!</strong>");

                        RadNotification1.Text = str.ToString();
                        RadNotification1.Show();

                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                }
            }
        }
        protected void btn_findref_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_referencia.Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                OrdenesComprasBL ObjC = new OrdenesComprasBL();
                DataTable tbBarras = new DataTable();
                string lc_Referencia = "",lc_RefOrigen="";
                try
                {
                    lc_Referencia = Obj.GetReffromOrigen(null,Convert.ToString(Session["CODEMP"]), txt_referencia.Text.Trim());

                    if (string.IsNullOrEmpty(lc_Referencia))                    
                        lc_Referencia = txt_referencia.Text.Trim();
                    else
                        lc_RefOrigen = txt_referencia.Text.Trim();


                    tbBarras = Obj.GetArticulos(null, "AND ARCLAVE1 IN ('"+ lc_Referencia +"')");
                    //tbBarras = Obj.GetArticulos(null, "AND ARCLAVE1 IN (ISNULL((SELECT TB_ORIGEN.ARCLAVE1 FROM TB_ORIGEN WITH(NOLOCK) WHERE TB_ORIGEN.ARCODEMP = ARTICULO.ARCODEMP AND OR_REFERENCIA='" + txt_referencia.Text.Trim() + "'),'" + txt_referencia.Text.Trim() + "'))");
                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["ARTIPPRO"]);
                            rc_categoria.SelectedValue = Convert.ToString(rw["ARTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["ARCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["ARCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["ARCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["ARCLAVE4"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_barras.Text = Convert.ToString(rw["BARRAS"]);
                            txt_rproveedor.Text = lc_RefOrigen;

                            rc_categoria_SelectedIndexChanged(sender, null);

                            rc_dt1.SelectedValue = Convert.ToString(rw["ARDTTEC1"]);
                            rc_dt2.SelectedValue = Convert.ToString(rw["ARDTTEC2"]);
                            rc_dt3.SelectedValue = Convert.ToString(rw["ARDTTEC3"]);
                            rc_dt4.SelectedValue = Convert.ToString(rw["ARDTTEC4"]);
                            rc_dt5.SelectedValue = Convert.ToString(rw["ARDTTEC5"]);
                            rc_dt7.SelectedValue = Convert.ToString(rw["ARDTTEC7"]);
                            rc_dt8.SelectedValue = Convert.ToString(rw["ARDTTEC8"]);

                            txt_precio.Value = ObjC.GetUltimoPrecioOC(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]));

                            txt_cantidad.Value = 0;
                        }

                        btn_agregar_Aceptar(sender, e);

                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        //litTextoMensaje.Text = "¡Referencia No Existe!";
                        //string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        StringBuilder str = new StringBuilder();

                        str.AppendLine("<strong>¡Referencia No Existe!</strong>");

                        RadNotification1.Text = str.ToString();
                        RadNotification1.Show();

                        txt_referencia.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                    ObjC = null;
                    Obj = null;
                }
            }
        }
        protected void rg_container_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "InitInsert":
                    string script = "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "PerformInsert":
                    DataRow itm = tbContainers.NewRow();
                    try
                    {
                        itm["BLD_CODIGO"] = 0;
                        itm["BLH_CODIGO"] = 0;
                        itm["BLD_NROCONTAINER"] = (e.Item.FindControl("txt_nrocontainer") as RadTextBox).Text;
                        itm["BLD_NROPACK"] = (e.Item.FindControl("txt_nropack") as RadTextBox).Text;
                        itm["BLD_DESCRIPTION"] = (e.Item.FindControl("txt_obscontainer") as RadTextBox).Text;
                        itm["BLD_GROSSWEIGHT"] = (e.Item.FindControl("txt_gross") as RadNumericTextBox).Text;
                        itm["BLD_GROSSUN"] = (e.Item.FindControl("rc_unidadgross") as RadComboBox).SelectedValue;
                        itm["BLD_DIMESION"] = (e.Item.FindControl("txt_measurement") as RadNumericTextBox).Text;
                        itm["BLD_DIMESIONUN"] = (e.Item.FindControl("rc_unidadmeasurement") as RadComboBox).SelectedValue;
                        tbContainers.Rows.Add(itm);
                        rg_container.DataSource = tbContainers;
                        rg_container.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        itm = null;
                    }
                    break;
            }
        }
        protected void rg_bl_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbBillxWR;
        }
        protected void rg_bl_ItemCommand(object sender, GridCommandEventArgs e)
        {
            BillofLadingBL ObjBL = new BillofLadingBL();
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        tbContainers = ObjBL.GetBLDT(null, 0);
                        rg_container.DataSource = tbContainers;
                        rg_container.DataBind();
                        e.Canceled = true;
                        break;
                    case "Delete":
                        GridEditableItem item = e.Item as GridEditableItem;
                        ObjBL.DeteleBLCompra(null, Convert.ToInt32(item["BLC_CONSECUTIVO"].Text));
                        tbBillxWR = ObjBL.GetBLCompra(null, Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                        tbContainers = ObjBL.GetBLDT(null, 0);
                        item = null;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjBL = null;
            }
        }
        protected void btn_agregarctn_Click(object sender, EventArgs e)
        {
            DataRow itm = tbBillxWR.NewRow();
            try
            {
                itm["BLC_CONSECUTIVO"] = 0;
                itm["BLH_CODIGO"] = 0;
                itm["BLH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                itm["BLH_CODEXPORTER"] = txt_exporter.Text;
                itm["BLH_CODRECEPTOR"] = txt_consignatario.Text;
                itm["BLH_CODNOTIFY"] = txt_notify.Text;
                itm["BLH_MODTRANS"] = rc_minitialcarriage.SelectedValue;
                itm["BLH_CIUREC"] = txt_lugarrecibe.Text;
                itm["BLH_NROVIAJE"] = txt_nroviaje.Text;
                itm["BLH_PURORIGEN"] = txt_ptocarga.Text;
                itm["BLH_PURDESTINO"] = txt_ptodescarga.Text;
                itm["BLH_CIUDESTI"] = txt_destino.Text;
                itm["BLH_BOOKINGNO"] = txt_nrobooking.Text;
                itm["BLH_NROBILLOFL"] = txt_nrobl.Text;
                itm["BLH_EXPORTREF"] = txt_exportref.Text;
                itm["BLH_PTOPAISORI"] = ".";
                itm["BLH_TIPOENVIO"] = rc_tipomov.SelectedValue;
                itm["BLH_FECHA"] = txt_fechaBL.SelectedDate;
                itm["BLH_USUARIO"] = "..";
                itm["BLH_FECING"] = System.DateTime.Now;

                tbBillxWR.Rows.Add(itm);

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                    (rlv_compras.InsertItem.FindControl("rg_bl") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                    (rlv_compras.Items[0].FindControl("rg_bl") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                itm = null;
            }
        }
        protected void rg_container_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbContainers;
        }        
        protected void btn_shipper_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "A";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_consignatario_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "B";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_notify_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "C";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroTer_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";
            if (!string.IsNullOrWhiteSpace(edt_codter.Text))
                filter += "AND TRCODTER=" + edt_codter.Text;
            if (!string.IsNullOrWhiteSpace(edt_nomtercero.Text))
                filter += "AND UPPER(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+ ' ' +ISNULL(TRAPELLI,'')) LIKE '%" + edt_nomtercero.Text.ToUpper() + "%'";
            if (!string.IsNullOrWhiteSpace(edt_identificacion.Text))
                filter += "AND UPPER(TRCODNIT) LIKE '%" + edt_identificacion.Text.ToUpper() + "%'";

            obj_terceros_modal.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaTerceros.DataBind();

            edt_nomtercero.Focus();
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); $find(\"" + edt_nomtercero.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaTerceros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                Boolean lb_bandera = false;
                try
                {
                    if ((item.FindControl("lbl_Estadi") as Label).Text == "AC")
                        lb_bandera = true;
                    if (lb_bandera)
                    {
                        if (gc_indtercero == "B")
                        {
                            txt_consignatario.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datconsignatario.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                        }
                        if (gc_indtercero == "A")
                        {
                            txt_exporter.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datexport.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                        }
                        if (gc_indtercero == "C")
                        {
                            txt_notify.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datnotify.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "Tercero se Encuntra Inactivo!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }

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
                    //mpTerceros.Show();
                    string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
        }
        protected void rg_bl_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    e.DetailTableView.DataSource = tbContainers;
                    //e.DetailTableView.DataBind();
                    break;
            }
        }
        protected void rlv_compras_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            Boolean lb_ind = false;
            foreach (DataColumn cl in tbProforma.Columns)
                cl.ReadOnly = false;

            foreach (DataColumn cl in tbFactura.Columns)
                cl.ReadOnly = false;

            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_proforma") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                string lc_barras = "", lc_nrodoc = "",lc_pago;                
                int ln_dias = 0;
                DateTime? ld_fecha;
                foreach (DataRow row in tbProforma.Rows)
                {
                    lb_ind = false;
                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("edt_fproforma") as RadDatePicker).DbSelectedDate)))
                    {
                        litTextoMensaje.Text = "No Tiene Fecha de Proforma!";
                        lb_ind = true;
                    }

                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("txt_nroproforma") as RadTextBox).Text)))
                    {
                        litTextoMensaje.Text = "No Tiene Nro de Proforma!";
                        lb_ind = true;
                    }

                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("tx_dias_pro") as RadNumericTextBox).Text)))
                    {
                        litTextoMensaje.Text = "No Tiene Nro de Dias!";
                        lb_ind = true;
                    }

                    if (lb_ind)
                    {
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                                                
                        e.Canceled = true;
                        break;
                    }

                    lc_barras = Convert.ToString((item.FindControl("txt_barraspro") as RadTextBox).Text);
                    lc_nrodoc = Convert.ToString((item.FindControl("txt_nroproforma") as RadTextBox).Text);
                    ln_dias = Convert.ToInt32((item.FindControl("tx_dias_pro") as RadNumericTextBox).DbValue);
                    ld_fecha = Convert.ToDateTime((item.FindControl("edt_fproforma") as RadDatePicker).DbSelectedDate);
                    lc_pago = (item.FindControl("chk_pago") as CheckBox).Checked ? "S" : "N";

                    tbFactura.Columns["BARRAS"].ReadOnly = false;
                    if (Convert.ToInt32(row["PR_NROITEM"]) == ln_codigo)
                    {
                        row["BARRAS"] = lc_barras;                       
                        row["PR_FECPROFORMA"] = ld_fecha;
                        row["PR_NROFACPROFORMA"] = lc_nrodoc;
                        row["PR_DIAS"] = ln_dias;
                        row["BARRAS"] = lc_barras;
                        row["PR_PAGO"] = lc_pago;
                        row["PR_ORIGEN"] = Convert.ToString((item.FindControl("rc_porigen") as RadComboBox).SelectedValue);
                        row["PR_POSARA"] = Convert.ToString((item.FindControl("txt_arancel") as RadTextBox).Text);
                    }
                }

                //ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                //lc_nrodoc = Convert.ToString((item.FindControl("txt_nroproforma") as RadTextBox).Text);
                //ld_fecha = Convert.ToDateTime((item.FindControl("edt_fproforma") as RadDatePicker).DbSelectedDate);
            }
            
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_factura") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["FD_NROITEM"].Text);
                string lc_barras = "", lc_nrodoc = "",lc_pago = "";                
                int ln_dias = 0;
                DateTime? ld_fecha;
                foreach (DataRow row in tbFactura.Rows)
                {
                    lb_ind = false;

                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("edt_ffactura") as RadDatePicker).DbSelectedDate)))
                    {
                        litTextoMensaje.Text = "No Tiene Fecha de Factura!";
                        lb_ind = true;
                    }

                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("txt_nrofactura") as RadTextBox).Text)))
                    {
                        litTextoMensaje.Text = "No Tiene Nro de Factura!";
                        lb_ind = true;
                    }

                    if (string.IsNullOrWhiteSpace(Convert.ToString((item.FindControl("tx_dias_fac") as RadNumericTextBox).Text)))
                    {
                        litTextoMensaje.Text = "No Tiene Nro de Dias!";
                        lb_ind = true;
                    }

                    if (lb_ind)
                    {
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                        
                        e.Canceled = true;
                        break;
                    }

                    lc_barras = Convert.ToString((item.FindControl("txt_barrasfac") as RadTextBox).Text);
                    lc_nrodoc = Convert.ToString((item.FindControl("txt_nrofactura") as RadTextBox).Text);
                    ln_dias = Convert.ToInt32((item.FindControl("tx_dias_fac") as RadNumericTextBox).DbValue);
                    ld_fecha = Convert.ToDateTime((item.FindControl("edt_ffactura") as RadDatePicker).DbSelectedDate);
                    lc_pago = (item.FindControl("chk_pago") as CheckBox).Checked ? "S" : "N";

                    tbFactura.Columns["BARRAS"].ReadOnly = false;
                    if (Convert.ToInt32(row["FD_NROITEM"]) == ln_codigo)
                    {
                        row["BARRAS"] = lc_barras;
                        row["FD_NROFACTURA"] = lc_nrodoc;
                        row["FD_FECFAC"] = ld_fecha;
                        row["FD_DIAS"] = ln_dias;                        
                        row["FD_ORIGEN"] = Convert.ToString((item.FindControl("rc_porigenfac") as RadComboBox).SelectedValue);
                        row["FD_POSARA"] = Convert.ToString((item.FindControl("txt_arancelfac") as RadTextBox).Text);
                        row["FD_PAGO"] = lc_pago;
                    }
                }

                //ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                //lc_nrodoc = Convert.ToString((item.FindControl("txt_nroproforma") as RadTextBox).Text);
                //ld_fecha = Convert.ToDateTime((item.FindControl("edt_fproforma") as RadDatePicker).DbSelectedDate);
            }

            foreach (DataColumn cl in tbSummari.Columns)
                cl.ReadOnly = false;

            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_resumen") as RadGrid).Items)
            {
                int ln_tipo = Convert.ToInt32(item["ITM"].Text);
                string lc_documento = Convert.ToString(item["CD_NROCMP"].Text);

                foreach (DataRow rw in tbSummari.Rows)
                {
                    if (Convert.ToInt32(rw["ITM"]) == ln_tipo && Convert.ToString(rw["CD_NROCMP"]) == lc_documento)
                        rw["CD_ESTADO"] = (item.FindControl("rc_estadodoc") as RadComboBox).SelectedValue;
                }
            }
        }
        protected void txt_cantidad_cmp_TextChanged(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbItems.Columns)
                cl.ReadOnly = false;


            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["CD_NROITEM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue == null)
                                row["CD_CANTIDAD"] = 0;
                            else
                            {
                                row["CD_CANTIDAD"] = ((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue));
                            }

                            tbItems.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["CD_NROITEM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue == null)
                                row["CD_CANTIDAD"] = 0;
                            else
                            {
                                row["CD_CANTIDAD"] = ((item.FindControl("txt_cantidad") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue));
                            }

                            tbItems.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }


            (sender as RadNumericTextBox).Focus();
        }
        protected void txt_precio_cmp_TextChanged(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbItems.Columns)
                cl.ReadOnly = false;
            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["CD_NROITEM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == ln_codigo)
                        {
                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue == null)
                                row["CD_PRECIO"] = 0;
                            else
                            {
                                row["CD_PRECIO"] = ((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue));
                            }

                            tbItems.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["CD_NROITEM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == ln_codigo)
                        {
                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue == null)
                                row["CD_PRECIO"] = 0;
                            else
                            {
                                row["CD_PRECIO"] = ((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_cantidad_cmp") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue));
                            }

                            tbItems.AcceptChanges();
                            break;
                        }
                    }
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                }

            (sender as RadNumericTextBox).Focus();
            }
        }
        protected void txt_precio_pro_TextChanged(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbProforma.Columns)
                cl.ReadOnly = false;
            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                    foreach (DataRow row in tbProforma.Rows)
                    {
                        if (Convert.ToInt32(row["PR_NROITEM"]) == ln_codigo)
                        {
                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue == null)
                                row["PR_PRECIO"] = 0;
                            else
                            {
                                row["PR_PRECIO"] = ((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue));
                            }

                            tbProforma.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                (rlv_compras.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                    foreach (DataRow row in tbProforma.Rows)
                    {
                        if (Convert.ToInt32(row["PR_NROITEM"]) == ln_codigo)
                        {
                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_precio_cmp") as RadNumericTextBox)).DbValue == null)
                                row["PR_PRECIO"] = 0;
                            else
                            {
                                row["PR_PRECIO"] = ((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_pro") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_precio_pro") as RadNumericTextBox)).DbValue));
                            }

                            tbProforma.AcceptChanges();
                            break;
                        }
                    }
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbProforma;
                    (rlv_compras.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
                }

            (sender as RadNumericTextBox).Focus();
            }
        }
        protected void txt_pre_fac_TextChanged(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbFactura.Columns)
                cl.ReadOnly = false;


            if (rlv_compras.InsertItem == null)
            {
                foreach (GridDataItem item in (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["FD_NROITEM"].Text);
                    foreach (DataRow row in tbFactura.Rows)
                    {
                        if (Convert.ToInt32(row["FD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue == null)
                                row["FD_PRECIO"] = 0;
                            else
                            {
                                row["FD_PRECIO"] = ((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue));
                            }

                            tbFactura.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                (rlv_compras.Items[0].FindControl("rg_factura") as RadGrid).DataBind();
            }
            else
            {
                foreach (GridDataItem item in (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["FD_NROITEM"].Text);
                    foreach (DataRow row in tbFactura.Rows)
                    {
                        if (Convert.ToInt32(row["FD_NROITEM"]) == ln_codigo)
                        {

                            row["VLRTOT"] = 0;
                            if (((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue == null)
                                row["FD_PRECIO"] = 0;
                            else
                            {
                                row["FD_PRECIO"] = ((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue;
                                row["VLRTOT"] = (Convert.ToDouble(((item.FindControl("txt_can_fac") as RadNumericTextBox)).DbValue) * Convert.ToDouble(((item.FindControl("txt_pre_fac") as RadNumericTextBox)).DbValue));
                            }

                            tbFactura.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataSource = tbFactura;
                (rlv_compras.InsertItem.FindControl("rg_factura") as RadGrid).DataBind();
            }


            (sender as RadNumericTextBox).Focus();
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
        protected void rgSoportes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            SoportesBL Obj = new SoportesBL();
            int i = 1;
            int pos = 0;
            int xpos = 0;
            try
            {
                var CODIGO = item.GetDataKeyValue("SP_CONSECUTIVO").ToString();
                switch (e.CommandName)
                {
                    case "Delete":

                        foreach (DataRow row in tbSoportes.Rows)
                        {
                            if (Convert.ToInt32(row["SP_CONSECUTIVO"]) == Convert.ToInt32(CODIGO))
                            {
                                pos = xpos;
                            }
                            xpos++;
                        }

                        tbSoportes.Rows[pos].Delete();
                        tbSoportes.AcceptChanges();


                        Obj.DeleteSoporte(null, Convert.ToInt32(CODIGO));
                        break;
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
        protected void rg_resumen_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbSummari;
        }
        private void InsertSummari(string inNroDoc,int inTipo)
        {
            Boolean lb_ind = false;
            string lc_tipo = "";
            try {
                switch (inTipo)
                {
                    case 1:
                        foreach (DataRow rw in tbSummari.Rows)
                        {
                            if (inNroDoc == Convert.ToString(rw["CD_NROCMP"]))
                                lb_ind = true;
                        }
                        lc_tipo = "Purchar Order";
                        break;
                    case 2:
                        foreach (DataRow rw in tbSummari.Rows)
                        {
                            if (inNroDoc == Convert.ToString(rw["CD_NROCMP"]))
                                lb_ind = true;
                        }
                        lc_tipo = "Proforma Invoice";
                        break;
                    case 3:
                        foreach (DataRow rw in tbSummari.Rows)
                        {
                            if (inNroDoc == Convert.ToString(rw["CD_NROCMP"]))
                                lb_ind = true;
                        }
                        lc_tipo = "Invoice";
                        break;
                }
                if (!lb_ind)
                {
                    DataRow row = tbSummari.NewRow();
                    row["ITM"] = inTipo;
                    row["tipo"] = lc_tipo;
                    row["CD_NROCMP"] = inNroDoc;
                    row["CD_ESTADO"] = "AC";

                    tbSummari.Rows.Add(row);
                }

                if (rlv_compras.InsertItem != null)
                {
                    (rlv_compras.InsertItem.FindControl("rg_resumen") as RadGrid).DataSource = tbSummari;
                    (rlv_compras.InsertItem.FindControl("rg_resumen") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_compras.Items[0].FindControl("rg_resumen") as RadGrid).DataSource = tbSummari;
                    (rlv_compras.Items[0].FindControl("rg_resumen") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
    }   
}