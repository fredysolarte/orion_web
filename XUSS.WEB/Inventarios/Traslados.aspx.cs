using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using XUSS.BLL.Inventarios;
using System.Data;
using XUSS.BLL.Articulos;
using XUSS.BLL.Parametros;
using System.IO;
using XUSS.BLL.Costos;
using XUSS.BLL.ListaPrecios;
using XUSS.BLL.Pedidos;
using XUSS.BLL.Comun;
using XUSS.BLL.Terceros;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Inventarios
{
    public partial class Traslados : System.Web.UI.Page
    {
        private string gc_indtercero
        {
            set { ViewState["gc_indtercero"] = value; }
            get { return Convert.ToString(ViewState["gc_indtercero"]); }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private string prEstado
        {
            set { ViewState["prEstado"] = value; }
            get { return ViewState["prEstado"] as string; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbLotes {
            set { ViewState["tbLotes"] = value; }
            get { return ViewState["tbLotes"] as DataTable; }
        }
        private DataTable tbWrIn
        {
            set { ViewState["tbWrIn"] = value; }
            get { return ViewState["tbWrIn"] as DataTable; }
        }
        private DataTable tbSoportes
        {
            set { ViewState["tbSoportes"] = value; }
            get { return ViewState["tbSoportes"] as DataTable; }
        }
        private DataTable tbCostos
        {
            set { ViewState["tbCostos"] = value; }
            get { return ViewState["tbCostos"] as DataTable; }
        }
        private DataTable tbBodega
        {
            set { ViewState["tbBodega"] = value; }
            get { return ViewState["tbBodega"] as DataTable; }
        }
        private DataTable tbWrOut
        {
            set { ViewState["tbWrOut"] = value; }
            get { return ViewState["tbWrOut"] as DataTable; }
        }
        private DataTable tbContainers
        {
            set { ViewState["tbContainers"] = value; }
            get { return ViewState["tbContainers"] as DataTable; }
        }
        private DataTable tbBillxTraslado
        {
            set { ViewState["tbBillxTraslado"] = value; }
            get { return ViewState["tbBillxTraslado"] as DataTable; }
        }
        private DataTable tbSegregacion
        {
            set { ViewState["tbSegregacion"] = value; }
            get { return ViewState["tbSegregacion"] as DataTable; }
        }
        private string gc_tipo
        {
            set { ViewState["gc_tipo"] = value; }
            get { return ViewState["gc_tipo"] as string; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Traslado"])))
                {
                    obj_traslados.SelectParameters["filter"].DefaultValue = "TSNROTRA =" + Convert.ToString(Request.QueryString["Traslado"]);
                    rlv_traslados.DataBind();
                }                
            }
            if ((rlv_traslados.Controls[0] is RadListViewEmptyDataItem))
            {
                (rlv_traslados.Controls[0].FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = true;
                if (Convert.ToInt32(Session["UserDBA"]) == 1)
                    (rlv_traslados.Controls[0].FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = false;
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
            this.OcultarPaginador(rlv_traslados, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_traslados_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    MovimientosBL Obj = new MovimientosBL();
                    BillofLadingBL ObjBL = new BillofLadingBL();
                    SoportesBL ObjS = new SoportesBL();
                    CargarCostosBL ObjC = new CargarCostosBL();
                    SegregacionBL Objg = new SegregacionBL();
                    try {
                        
                        tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbSoportes = ObjS.GetSoportes(null, "1", 0);
                        tbBillxTraslado = ObjBL.GetBLTraslado(null, 0);
                        tbCostos = ObjC.GetCostos(null,"1=0",0,0);
                        tbSegregacion = Objg.GetSegregacionxTraslado(null, 0,Convert.ToString(Session["CODEMP"]));

                        tbLotes = new DataTable();

                        tbLotes.Columns.Add("TP", typeof(string));
                        tbLotes.Columns.Add("C1", typeof(string));
                        tbLotes.Columns.Add("C2", typeof(string));
                        tbLotes.Columns.Add("C3", typeof(string));
                        tbLotes.Columns.Add("C4", typeof(string));
                        tbLotes.Columns.Add("MBBODEGA", typeof(string));
                        tbLotes.Columns.Add("MLCDLOTE", typeof(string));
                        tbLotes.Columns.Add("MLCANTID", typeof(Int32));
                        tbLotes.Columns.Add("MBCANTID", typeof(Int32));
                        tbLotes.Columns.Add("MECDELEM", typeof(string));
                        tbLotes.Columns.Add("MECANTID", typeof(Int32));
                        tbLotes.Columns.Add("IT", typeof(Int32));
                        tbLotes.Columns.Add("IDLOTE", typeof(Int32));
                        tbLotes.Columns.Add("IDELEM", typeof(Int32));

                        tbWrIn = new DataTable();
                        tbWrIn.Columns.Add("WIH_CONSECUTIVO", typeof(int));
                        tbWrIn.Columns.Add("WID_ITEM", typeof(int));
                        tbWrIn.Columns.Add("MBIDITEM", typeof(int));

                        tbWrOut = new DataTable();
                        tbWrOut.Columns.Add("WOH_CONSECUTIVO", typeof(int));
                        tbWrOut.Columns.Add("WOD_ITEM", typeof(int));
                        tbWrOut.Columns.Add("MBIDITEM", typeof(int));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        ObjS = null;
                        ObjC = null;
                        Objg = null;
                        obj_lstprecio = null;
                    }                                        
                    break;

                case "Buscar":
                    obj_traslados.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_traslados.DataBind();
                    break;
                case "Edit":
                    BodegaBL ObjB = new BodegaBL();
                    Boolean lb_bandera = true;
                    try
                    {
                        prEstado = "CE";
                        if (Convert.ToString((rlv_traslados.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue) == "CE")
                        {
                            litTextoMensaje.Text = "¡Traslado Ya Esta Recepcionado, Solo Puede Agregar Costos, Bill Of Loading y Soportes!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            //e.Canceled = true;
                            prEstado = "TT";
                        }
                        if (Convert.ToString((rlv_traslados.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue) == "AN")
                        {
                            litTextoMensaje.Text = "¡Traslado Esta Anulado !";                            
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            e.Canceled = true;
                        }
                        foreach (DataRow rw in ObjB.GetBodegasXUsuario(null, Convert.ToString(Session["UserLogon"])).Rows)
                        {
                            if (Convert.ToString((rlv_traslados.Items[0].FindControl("rc_otbodega") as RadComboBox).SelectedValue) == Convert.ToString(rw["BDBODEGA"]))
                                lb_bandera = false;
                        }
                        if (lb_bandera)
                        {
                            litTextoMensaje.Text = "¡No Cuenta Con Permisos Sobre la Bodega de Recepcion!";
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
                        ObjB = null;                        
                    }
                    break;
                case "Delete":
                    TrasladosBL Objm = new TrasladosBL();
                    try
                    {
                        if (Convert.ToString((rlv_traslados.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue) == "CE")
                        {
                            litTextoMensaje.Text = "¡Traslado Ya Esta Recepcionado!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            e.Canceled = true;
                        }
                        else
                        {
                            if (Convert.ToString((rlv_traslados.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue) == "AN")
                            {
                                litTextoMensaje.Text = "¡Traslado Esta Anulado !";
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                e.Canceled = true;
                            }
                            else
                            {
                                Objm.AnularMovimientos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text),
                                                       Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_movent") as RadTextBox).Text),
                                                       Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_movsal") as RadTextBox).Text), Convert.ToString(Session["UserLogon"]));
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Objm = null;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_bodegas") as RadComboBox).SelectedValue))
                filtro += " AND TSBODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodegas") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_otbodega") as RadComboBox).SelectedValue))
                filtro += " AND TSOTBODE = '" + (((RadButton)sender).Parent.FindControl("rc_otbodega") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue))
            {
                if ((((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue != ".")
                    filtro += " AND TSESTADO = '" + (((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue + "'";
            }

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_traslado") as RadTextBox).Text))
                filtro += " AND TSNROTRA = " + (((RadButton)sender).Parent.FindControl("txt_traslado") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_traslados.SelectParameters["filter"].DefaultValue = filtro;
            rlv_traslados.DataBind();
            if ((rlv_traslados.Controls[0] is RadListViewEmptyDataItem))
            {
                (rlv_traslados.Controls[0].FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = true;
                if (Convert.ToInt32(Session["UserDBA"]) == 1)
                    (rlv_traslados.Controls[0].FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = false;

                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_traslados.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_traslados_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    MovimientosBL Obj = new MovimientosBL();
                    SoportesBL ObjS = new SoportesBL();
                    BillofLadingBL ObjBL = new BillofLadingBL();
                    CargarCostosBL ObjC = new CargarCostosBL();
                    SegregacionBL Objg = new SegregacionBL();
                    try
                    {
                        //tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_traslados.Items[0].GetDataKeyValue("TSMOVSAL").ToString()));
                        tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_movsal") as RadTextBox).Text));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        tbBillxTraslado = ObjBL.GetBLTraslado(null, Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text));
                        (e.Item.FindControl("rg_bl") as RadGrid).DataSource = tbBillxTraslado;
                        (e.Item.FindControl("rg_bl") as RadGrid).DataBind();

                        int ln_codigo = 0;
                        foreach (DataRow rw in tbBillxTraslado.Rows)
                            ln_codigo = Convert.ToInt32(rw["BLH_CODIGO"]);

                        tbContainers = ObjBL.GetBLDT(null, ln_codigo);

                        tbSoportes = ObjS.GetSoportes(null, "1", Convert.ToInt32((rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text));
                        (e.Item.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                        (e.Item.FindControl("rgSoportes") as RadGrid).DataBind();

                        tbCostos = ObjC.GetCostos(null, " TSNROTRA ="+(rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text, 0, 0);
                        (e.Item.FindControl("rg_costos") as RadGrid).DataSource = tbCostos;
                        (e.Item.FindControl("rg_costos") as RadGrid).DataBind();

                        tbSegregacion = Objg.GetSegregacionxTraslado(null, Convert.ToInt32((e.Item.FindControl("txt_traslado") as RadTextBox).Text),Convert.ToString(Session["CODEMP"]));
                        (e.Item.FindControl("rg_segregacion") as RadGrid).DataSource = tbSegregacion;
                        (e.Item.FindControl("rg_segregacion") as RadGrid).DataBind();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        ObjS = null;
                        ObjC = null;
                        obj_lstprecio = null;
                    }
                }
            }
            //if (e.Item.ItemType == RadListViewItemType.InsertItem)
            //{
            //    (e.Item.FindControl("edt_ftraslado") as RadDatePicker).DbSelectedDate = System.DateTime.Now;
            //}
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7008&inban=S&inParametro=consecutivo&inValor=" + (rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_imprimirtirilla_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7020&inban=S&inParametro=consecutivo&inValor=" + (rlv_traslados.Items[0].FindControl("txt_traslado") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                gc_tipo = "IT";
                if (rlv_traslados.InsertItem != null)
                {
                    (rlv_traslados.InsertItem.FindControl("rqBodegaS") as RequiredFieldValidator).Validate();
                    (rlv_traslados.InsertItem.FindControl("rqBodegaE") as RequiredFieldValidator).Validate();
                    (rlv_traslados.InsertItem.FindControl("compvalbodegas") as CompareValidator).Validate();

                    if ((rlv_traslados.InsertItem.FindControl("rqBodegaS") as RequiredFieldValidator).IsValid && (rlv_traslados.InsertItem.FindControl("rqBodegaE") as RequiredFieldValidator).IsValid && (rlv_traslados.InsertItem.FindControl("compvalbodegas") as CompareValidator).IsValid)
                    {
                        this.Limpiar();
                        //mpAddArticulos.Show();
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }                    
                }
                else
                {
                    if ((rlv_traslados.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue == "AC")
                    {
                        this.LimpiarR();
                        txt_barrasr.Focus();
                        string script = "function f(){$find(\"" + modalRecibir.ClientID + "\").show(); $find(\"" + txt_barrasr.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "¡Traslado ya Confirmado, Solo se puede agregar Costos y Soportes !";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                        
                    }
                }
                e.Canceled = true;
            }

            if (e.CommandName == "Charge")
            {
                rbl_tiparch.Items[0].Text = "Referencia + C2 + C3 + C4 + Can";
                foreach (DataRow rw in tbBodega.Rows)
                {
                    if ((rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue == Convert.ToString(rw["ABBODEGA"]))
                    {
                        if (Convert.ToString(rw["ABMNLOTE"]) == "S")
                            rbl_tiparch.Items[0].Text = "Referencia + C2 + C3 + C4 + Cant + Lote";
                        if (Convert.ToString(rw["ABMNELEM"]) == "S")
                            rbl_tiparch.Items[0].Text = "Referencia + C2 + C3 + C4 + Cant + Lote + Elem";
                    }

                }

                string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            if (e.CommandName == "wrout")
            {
                string script = "function f(){$find(\"" + mpWROUT.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            if (e.CommandName == "wrin")
            {
                string script = "function f(){$find(\"" + mpWRIN.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            if (e.CommandName == "segregacion")
            {
                string script = "function f(){$find(\"" + mpSegregaciones.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

        }        
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            if (rlv_traslados.InsertItem != null)                
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_traslados.Items[0].FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            
            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            if (gc_tipo != "CT")
                if (rlv_traslados.InsertItem != null)                
                    obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                else
                    obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_traslados.Items[0].FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            else
            {
                lsql += " AND ARTIPPRO ='X' ";
                obj_articulos.SelectParameters["inBodega"].DefaultValue = "";
            }
            if (rlv_traslados.InsertItem != null)
                obj_articulos.SelectParameters["LT"].DefaultValue = (rlv_traslados.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;
            else
                obj_articulos.SelectParameters["LT"].DefaultValue = (rlv_traslados.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;

            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;            
            rgConsultaArticulos.DataBind();            
            string script_ = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script_, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                GridDataItem item = (GridDataItem)e.Item;
                string script = "";
                try
                {
                    if (gc_tipo == "CT")
                    {
                        txt_tpct.Text = Convert.ToString(item["ARTIPPRO"].Text);
                        txt_referenciact.Text = Convert.ToString(item["ARCLAVE1"].Text);
                        txt_clave2ct.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                        txt_clave3ct.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                        txt_clave4ct.Text = (item.FindControl("txt_fclave4") as RadTextBox).Text; Convert.ToString(item["ARCLAVE4"].Text);
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
                        txt_clave2.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                        txt_clave3.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                        txt_clave4.Text = (item.FindControl("txt_fclave4") as RadTextBox).Text;
                        txt_nc2.Text = Convert.ToString(item["CLAVE2"].Text);
                        txt_nc3.Text = Convert.ToString(item["CLAVE3"].Text);
                        txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                        double ln_cantb = 0;
                        foreach (DataRow rx in tbItems.Rows)
                        {
                            if ((Convert.ToString(rx["MBTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rx["MBCLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rx["MBCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rx["MBCLAVE3"]) == txt_clave3.Text))
                                ln_cantb += Convert.ToDouble(rx["MBCANTID"]);
                        }
                        txt_caninv.Value = Convert.ToDouble(item["BBCANTID"].Text) - ln_cantb;
                        txt_preciolta.Value = Convert.ToDouble(item["PRECIO"].Text);
                        txt_dct.Value = Convert.ToDouble(item["DESCUENTO"].Text);

                        //mpAddArticulos.Show();
                        this.ConfigLinea();
                        txt_barras.Focus();
                        script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        txt_cantidad.Focus();

                        this.CargarLote();
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
                script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                break;
            }
        }
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            Boolean lb_bandera = true;
            DataRow row = tbItems.NewRow();
            DataRow rwl = tbLotes.NewRow();
            try {

                rqf_catidad1.Validate();
                rqf_catidad2.Validate();
                rqf_referencia.Validate();
                cmpNumbers.Validate();

                //Valida Existencia Misma Referencia
                if (rqf_catidad1.IsValid && rqf_catidad2.IsValid && cmpNumbers.IsValid && rqf_referencia.IsValid)
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if ((Convert.ToString(rw["MBTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rw["MBCLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rw["MBCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rw["MBCLAVE3"]) == txt_clave3.Text))
                        {
                            lb_bandera = false;
                            rw["MBCANMOV"] = Convert.ToDouble(rw["MBCANMOV"]) + txt_cantidad.Value;
                            rw["MBCANTID"] = Convert.ToDouble(rw["MBCANTID"]) + txt_cantidad.Value;
                            rw["MBCANORI"] = Convert.ToDouble(rw["MBCANORI"]) + txt_cantidad.Value;
                            rw["MBSALDOI"] = Convert.ToDouble(rw["MBSALDOI"]) + txt_cantidad.Value;
                        }
                    }
                    if (lb_bandera)
                    {
                        row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["MBIDMOVI"] = 0;
                        row["MBIDITEM"] = tbItems.Rows.Count+1;
                        row["MBFECMOV"] = System.DateTime.Today;
                        row["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                        row["MBTIPPRO"] = txt_tp.Text;
                        row["MBCLAVE1"] = txt_referencia.Text;
                        row["MBCLAVE2"] = txt_clave2.Text;
                        row["MBCLAVE3"] = txt_clave3.Text;
                        row["MBCLAVE4"] = txt_clave4.Text;
                        row["MBCODCAL"] = ".";
                        row["MBCDTRAN"] = "99";
                        row["MBCANMOV"] = txt_cantidad.Value;
                        row["MBUNDMOV"] = "UN";
                        row["MBCANTID"] = txt_cantidad.Value;
                        row["MBCANORI"] = txt_cantidad.Value;
                        row["MBSALDOI"] = txt_cantidad.Value;
                        row["MBCOSTOA"] = 0;
                        row["MBCOSTOB"] = 0;
                        row["MBCSTNAL"] = txt_preciolta.Value;
                        row["MBOTMOVI"] = 0;
                        row["MBOTBODE"] = "";
                        row["MBESTADO"] = "AC";
                        row["MBCAUSAE"] = ".";
                        row["MBNMUSER"] = ".";
                        row["MBFECING"] = System.DateTime.Today;
                        row["MBFECMOD"] = System.DateTime.Today;
                        row["ARNOMBRE"] = txt_descripcion.Text;
                        row["ARCDALTR"] = "";
                        row["ARUNDINV"] = "UN";
                        row["TANOMBRE"] = "";
                        row["CLAVE2"] = txt_nc2.Text;
                        row["CLAVE3"] = txt_nc3.Text;

                        tbItems.Rows.Add(row);
                    }
                    
                    //if (rc_lote.Visible)
                    lb_bandera = true;
                    foreach (DataRow rw in tbLotes.Rows)
                    {
                        if ((Convert.ToString(rw["TP"]) == txt_tp.Text) && (Convert.ToString(rw["C1"]) == txt_referencia.Text) && (Convert.ToString(rw["C2"]) == txt_clave2.Text) && (Convert.ToString(rw["C3"]) == txt_clave3.Text))
                        {
                            lb_bandera = false;
                            rw["MLCANTID"] = Convert.ToDouble(rw["MLCANTID"]) + txt_cantidad.Value;
                            rw["MBCANTID"] = Convert.ToDouble(rw["MBCANTID"]) + txt_cantidad.Value;
                        }
                    }

                    if(lb_bandera)
                    {
                        rwl["TP"] = txt_tp.Text;
                        rwl["C1"] = txt_referencia.Text;
                        rwl["C2"] = txt_clave2.Text;
                        rwl["C3"] = txt_clave3.Text;
                        rwl["C4"] = txt_clave4.Text;
                        rwl["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                        rwl["MLCDLOTE"] = rc_lote.SelectedValue;
                        rwl["MLCANTID"] = txt_cantidad.Value;
                        rwl["MBCANTID"] = txt_cantidad.Value;
                        rwl["IT"] = tbItems.Rows.Count;
                        
                        rwl["IDLOTE"] = 0;
                        rwl["IDELEM"] = 0;

                        tbLotes.Rows.Add(rwl);
                    }
                    (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                    this.Limpiar();
                    txt_barras.Focus();
                    //mpAddArticulos.Show();
                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                rwl = null;
            }
        }
        protected void obj_traslados_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {            
            e.InputParameters["tbitems"] = tbLotes;
            e.InputParameters["tbCostos"] = tbCostos;
            e.InputParameters["tbSoportes"] = tbSoportes;
            e.InputParameters["tbBLHD"] = tbBillxTraslado;
            e.InputParameters["tbBLDT"] = tbContainers;
            e.InputParameters["tbSegregacion"] = tbSegregacion;
            e.InputParameters["tbTrasladoWrIn"] = tbWrIn;
            //e.InputParameters["tbTrasladoWrOut"] = tbWrOut;
        }
        protected void rg_items_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    int MBIDMOVI_ = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDMOVI").ToString());
                    int MBIDITEM_ = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDITEM").ToString());
                    //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                    MovimientosBL Obj = new MovimientosBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.CargarMovimientoLot(null, Convert.ToString(Session["CODEMP"]), MBIDMOVI_, MBIDITEM_);
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
                case "detalle_item_elemento":
                    string lc_llave = Convert.ToString(dataItem.GetDataKeyValue("LLAVE").ToString());
                    MovimientosBL Obj_ = new MovimientosBL();
                    try
                    {
                        e.DetailTableView.DataSource = Obj_.CargarMovimientoEle(null, lc_llave);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj_ = null;
                    }
                    break;
                case "detalle_insert":
                    int MBIDMOVI = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDMOVI").ToString());
                    int MBIDITEM = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDITEM").ToString());
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
                            for (int i = 0; i < tbLotes.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(((DataRow)tbLotes.Rows[i])["IT"]) == MBIDITEM)
                                {
                                    DataRow rw = dt.NewRow();
                                    rw["TP"] = ((DataRow)tbLotes.Rows[i])["TP"];
                                    rw["C1"] = ((DataRow)tbLotes.Rows[i])["C1"];
                                    rw["C2"] = ((DataRow)tbLotes.Rows[i])["C2"];
                                    rw["C3"] = ((DataRow)tbLotes.Rows[i])["C3"];
                                    rw["C4"] = ((DataRow)tbLotes.Rows[i])["C4"];
                                    rw["MBBODEGA"] = ((DataRow)tbLotes.Rows[i])["MBBODEGA"];
                                    rw["MLCDLOTE"] = ((DataRow)tbLotes.Rows[i])["MLCDLOTE"];
                                    rw["MLCANTID"] = ((DataRow)tbLotes.Rows[i])["MLCANTID"];
                                    rw["MBCANTID"] = ((DataRow)tbLotes.Rows[i])["MBCANTID"];
                                    rw["IT"] = ((DataRow)tbLotes.Rows[i])["IT"];

                                    dt.Rows.Add(rw);
                                    rw = null;
                                }

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
        protected void obj_traslados_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Traslado :" + Convert.ToString(e.ReturnValue);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7008&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7008&inban=S&inClo=S&inImp=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');" + " window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7020&inban=S&inClo=S&inImp=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);                
            }

            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            //mpMensajes.Show();

        }
        protected void rlv_traslados_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //litTextoMensaje.Text = e.Exception.Message;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_traslados_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Traslado :" + Convert.ToString(e.ReturnValue) + " Confirmado!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7008&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7020&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }        
        protected void rlv_traslados_OnItemUpdated(object sender, RadListViewUpdatedEventArgs e)
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
        protected void txt_canrec_OnTextChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["MBIDITEM"].Text);
                foreach (DataRow row in tbItems.Rows)
                {
                    if (Convert.ToInt32(row["MBIDITEM"]) == ln_codigo)
                    {
                        if (((item.FindControl("txt_canrec") as RadNumericTextBox)).Value != null)
                        {
                            ((item.FindControl("txt_candif") as RadNumericTextBox)).Value = ((item.FindControl("txt_cantidad") as RadNumericTextBox)).Value - ((item.FindControl("txt_canrec") as RadNumericTextBox)).Value;
                            if (((item.FindControl("txt_candif") as RadNumericTextBox)).Value < 0)
                            {
                                litTextoMensaje.Text = "Cantidad Recibida Supera Cantidad Solicitada!";
                                //mpMensajes.Show();
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                break;
                            }
                        }
                    }
                }
            }
            //(sender as RadTextBox).Focus();
        }
        protected void rlv_traslados_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            Boolean lb_bandera = false;
            try
            {
                foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["MBIDITEM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["MBIDITEM"]) == ln_codigo)
                        {
                            if (((item.FindControl("txt_canrec") as RadNumericTextBox)).Value != null)
                            {
                                ((item.FindControl("txt_candif") as RadNumericTextBox)).Value = ((item.FindControl("txt_cantidad") as RadNumericTextBox)).Value - ((item.FindControl("txt_canrec") as RadNumericTextBox)).Value;
                                if (((item.FindControl("txt_candif") as RadNumericTextBox)).Value < 0)
                                {
                                    litTextoMensaje.Text = "Cantidad Recibida Supera Cantidad Solicitada!";
                                    lb_bandera = true;
                                    break;
                                }
                                if (((item.FindControl("txt_candif") as RadNumericTextBox)).Value != 0)
                                {
                                    litTextoMensaje.Text = "Diferencias en Cantidades!";
                                    lb_bandera = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                obj_traslados.UpdateParameters["TSESTADO"].DefaultValue = prEstado;
                if (lb_bandera && prEstado!="TT")
                {                    
                    //mpMensajes.Show();
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

            }
        }
        protected void obj_traslados_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {            
            e.InputParameters["tbitems"] = tbLotes;
            e.InputParameters["tbCostos"] = tbCostos;
            e.InputParameters["tbSoportes"] = tbSoportes;
            e.InputParameters["tbBLHD"] = tbBillxTraslado;
            e.InputParameters["tbBLDT"] = tbContainers;
        }
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text, (rlv_traslados.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue, (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue);
                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);                            
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_nc2.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nc3.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_preciolta.Value = Convert.ToDouble(rw["PRECIO"]);

                            double ln_cantb = 0;
                            foreach(DataRow rx in tbItems.Rows)
                            {
                                if ((Convert.ToString(rx["MBTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rx["MBCLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rx["MBCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rx["MBCLAVE3"]) == txt_clave3.Text))
                                    ln_cantb += Convert.ToDouble(rx["MBCANTID"]);
                            }
                            txt_caninv.Value = Convert.ToDouble(rw["BBCANTID"])-ln_cantb;
                            txt_cantidad.Value = 1;
                        }                        
                        this.ConfigLinea();
                        if (!rc_lote.Visible)
                            btn_agregar_Aceptar(sender, e);
                        else
                            this.CargarLote();
                        //mpAddArticulos.Show();
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "Codigo Barras Invalido!";
                        //mpMensajes.Show();
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
                    obj_articulos = null;
                    tbBarras = null;
                }
            }
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var PDLINNUM = item.GetDataKeyValue("MBIDITEM").ToString();
                    int pos=0,posl = 0;
                    int ypos=0,xpos = 0;                    
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["MBIDITEM"]) == Convert.ToInt32(PDLINNUM))
                        {
                            pos = xpos;
                            foreach (DataRow rx in tbLotes.Rows)
                            {
                                if (Convert.ToString(row["MBTIPPRO"]) == Convert.ToString(rx["TP"]) && Convert.ToString(row["MBCLAVE1"]) == Convert.ToString(rx["C1"]) && Convert.ToString(row["MBCLAVE2"]) == Convert.ToString(rx["C2"]) && Convert.ToString(row["MBCLAVE3"]) == Convert.ToString(rx["C3"]))
                                {
                                    posl = ypos;
                                }
                                ypos++;
                            }
                        }
                        xpos++;
                    }
                    tbLotes.Rows[posl].Delete();
                    tbLotes.AcceptChanges();
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    foreach (DataRow row in tbItems.Rows)
                    {
                        row["MBIDITEM"] = i;
                        i++;
                    }
                    
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
            txt_descripcion.Text = "";
            txt_barras.Text = "";
            txt_cantidad.Value = 0;
            txt_caninv.Value = 0;
            txt_tpct.Text = "";
            txt_referenciact.Text = "";
            txt_clave2ct.Text = "";
            txt_clave3ct.Text = "";
            txt_clave4ct.Text = "";
            txt_descripcionct.Text = "";
            txt_precioct.Value = 0;
            txt_nrodoc.Text = "";
            rc_proveedor.SelectedValue = "";
            rc_moneda.SelectedValue = "-1";
            rc_tdocumento.SelectedValue = "-1";

        }
        private void LimpiarR()
        {
            txt_tpr.Text = "";
            txt_referenciar.Text = "";
            txt_clave2r.Text = "";
            txt_clave3r.Text = "";
            txt_clave4r.Text = "";
            txt_descripcionr.Text = "";
            txt_barrasr.Text = "";
            txt_cantidadr.Value = 0;            
        }
        private void ConfigLinea()
        {
            TipoProductosBL Obj = new TipoProductosBL();
            try
            {
                using (IDataReader reader = Obj.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, txt_tp.Text))
                {
                    while (reader.Read())
                    {
                        rc_lote.Visible = false;
                        lbl_lote.Visible = false;
                        RequiredFieldValidator11.Enabled = false;
                        if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                        {
                            rc_lote.Visible = true;
                            lbl_lote.Visible = true;
                            RequiredFieldValidator11.Enabled = true;
                        }
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
                foreach (DataRow rw in Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
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
        protected void btn_agregarr_Aceptar(object sender, EventArgs e)
        {
            try {
                Boolean lb_bandera = false;
                rqf_catidad1r.Validate();
                rqf_catidad2r.Validate();
                rqf_referenciar.Validate();
                if (rqf_catidad1r.IsValid && rqf_catidad2r.IsValid && rqf_referenciar.IsValid)
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if ((Convert.ToString(rw["MBTIPPRO"]) == txt_tpr.Text) && (Convert.ToString(rw["MBCLAVE1"]) == txt_referenciar.Text) && (Convert.ToString(rw["MBCLAVE2"]) == txt_clave2r.Text) && (Convert.ToString(rw["MBCLAVE3"]) == txt_clave3r.Text))
                        {
                            //rw["MBCANTID"] = Convert.ToDouble(rw["MBCANTID"]) + txt_cantidadr.Value;
                            foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
                            {
                                if (Convert.ToInt32(rw["MBIDITEM"]) == Convert.ToInt32(item["MBIDITEM"].Text))
                                {
                                    lb_bandera = true;
                                    (item.FindControl("txt_canrec") as RadNumericTextBox).Value += txt_cantidadr.Value;
                                    (item.FindControl("txt_candif") as RadNumericTextBox).Value = Convert.ToDouble(rw["MBCANTID"]) - txt_cantidadr.Value;
                                }
                            }
                        }
                    }                    
                }

                if (lb_bandera)
                {
                    //(rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //(rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                    this.LimpiarR();
                    string script = "function f(){$find(\"" + modalRecibir.ClientID + "\").show(); $find(\"" + txt_barrasr.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    litTextoMensaje.Text = "Codigo No se Encuentra en Traslado!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        protected void txt_barrasr_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarras(null, (sender as RadTextBox).Text, null);
                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tpr.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referenciar.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2r.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3r.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4r.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_nc2r.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nc3r.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_descripcionr.Text = Convert.ToString(rw["ARNOMBRE"]);                            
                            txt_cantidadr.Value = 1;
                        }
                        
                        btn_agregarr_Aceptar(sender, e);                        
                        txt_barrasr.Focus();
                        string script = "function f(){$find(\"" + modalRecibir.ClientID + "\").show(); $find(\"" + txt_barrasr.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "Codigo Barras Invalido!";
                        //mpMensajes.Show();
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
                    obj_articulos = null;
                    tbBarras = null;
                }
            }
        }
        protected void PreventErrorOnbinding(object sender, EventArgs e)
        {
            RadComboBox cb = sender as RadComboBox;
            cb.DataBinding -= new EventHandler(PreventErrorOnbinding);
            cb.AppendDataBoundItems = true;

            try
            {
                cb.DataBind();
                cb.Items.Clear();
            }
            catch (ArgumentOutOfRangeException)
            {
                litTextoMensaje.Text = "¡No Cuenta con Permisos para Recibir Bodega Origen!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                //cb.Items.Clear();
                //cb.ClearSelection();
                //RadComboBoxItem cbI = new RadComboBoxItem("", "0");
                //cbI.Selected = true;
                //cb.Items.Add(cbI);
            }
        }
        protected void txt_traslado_OnTextChanged(object sender, EventArgs e)
        {
            //((RadButton)sender).Parent.FindControl("rc_bodegas") as RadComboBox).SelectedValue)
            (((RadTextBox)sender).Parent.FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = true;
            (((RadTextBox)sender).Parent.FindControl("rqf_estado") as RequiredFieldValidator).Enabled = true;
            if (!string.IsNullOrWhiteSpace((sender as RadTextBox).Text))
            {
                (((RadTextBox)sender).Parent.FindControl("rqf_bingreso") as RequiredFieldValidator).Enabled = false;
                (((RadTextBox)sender).Parent.FindControl("rqf_estado") as RequiredFieldValidator).Enabled = false;
            }
        }
        protected void rlv_traslados_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ViewState["isClickInsert"]))
            {
                if (((sender as RadListView).InsertItem.FindControl("edt_ftraslado") as RadDatePicker).SelectedDate == null)
                {
                    ((sender as RadListView).InsertItem.FindControl("edt_ftraslado") as RadDatePicker).SelectedDate = System.DateTime.Now;
                }
            }
        }
        protected void ct_menu_ItemClick(object sender, RadMenuEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Seleccionar Todos":
                    foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
                    {
                        int ln_codigo = Convert.ToInt32(item["MBIDITEM"].Text);
                        foreach (DataRow row in tbItems.Rows)
                        {
                            if (Convert.ToInt32(row["MBIDITEM"]) == ln_codigo)
                            {
                                ((item.FindControl("txt_canrec") as RadNumericTextBox)).Value = ((item.FindControl("txt_cantidad") as RadNumericTextBox)).Value;
                                ((item.FindControl("txt_candif") as RadNumericTextBox)).Value = 0;
                            }
                        }
                    }
                    break;
                case "Anular Seleccion":
                    foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
                    {
                        int ln_codigo = Convert.ToInt32(item["MBIDITEM"].Text);
                        foreach (DataRow row in tbItems.Rows)
                        {
                            if (Convert.ToInt32(row["MBIDITEM"]) == ln_codigo)
                            {
                                ((item.FindControl("txt_canrec") as RadNumericTextBox)).Value = 0;
                                ((item.FindControl("txt_candif") as RadNumericTextBox)).Value = ((item.FindControl("txt_cantidad") as RadNumericTextBox)).Value;
                                
                            }
                        }
                    }
                    break;
            }
        }
        protected void rgSoportes_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbSoportes;
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
                Obj = null;
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
                row["SP_CONSECUTIVO"] = tbSoportes.Rows.Count + 5;
                row["SP_REFERENCIA"] = 0;
                row["SP_DESCRIPCION"] = (((Button)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text;
                row["SP_EXTENCION"] = Path.GetExtension(prArchivo);
                row["SP_TIPO"] = "1";
                row["SP_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["RUTA"] = prArchivo;
                row["SP_FECING"] = System.DateTime.Now;

                tbSoportes.Rows.Add(row);
                if (rlv_traslados.InsertItem != null) (rlv_traslados.InsertItem.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                else (rlv_traslados.EditItems[0].FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
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
        protected void rg_costos_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbCostos;
        }
        protected void rg_costos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            gc_tipo = "CT";
            this.LimpiarR();
            txt_referenciact.Focus();
            string script = "function f(){$find(\"" + mpCostos.ClientID + "\").show(); $find(\"" + txt_referenciact.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            this.Limpiar();
            e.Canceled = true;
        }                       
        protected void btn_agregarct_Aceptar(object sender, EventArgs e)
        {
            DataRow rw = tbCostos.NewRow();
            try
            {
                rw["CT_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                rw["TSNROTRA"] = 0;
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
                if (rlv_traslados.InsertItem != null)
                {
                    (rlv_traslados.InsertItem.FindControl("rg_costos") as RadGrid).DataSource = tbCostos;
                    (rlv_traslados.InsertItem.FindControl("rg_costos") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_traslados.Items[0].FindControl("rg_costos") as RadGrid).DataSource = tbCostos;
                    (rlv_traslados.Items[0].FindControl("rg_costos") as RadGrid).DataBind();
                }

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
        protected void rc_lstprecio_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ListaPreciosBL ObjL = new ListaPreciosBL();
            try
            {
                if (rlv_traslados.InsertItem != null)
                {
                    foreach (GridDataItem item in (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).Items)
                    {
                        int ln_codigo = Convert.ToInt32((item.FindControl("txt_item") as RadNumericTextBox).Value);
                        ObjL.GetPrecio(null, Convert.ToString(Session["CODEMP"]), (item.FindControl("txt_tp") as RadTextBox).Text, Convert.ToString(item["MBCLAVE1"]), (item.FindControl("txt_c2") as RadTextBox).Text, ".", ".", (sender as RadComboBox).SelectedValue);
                    }
                }
                else
                {
                    foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)
                    {
                        int ln_codigo = Convert.ToInt32((item.FindControl("txt_item") as RadNumericTextBox).Value);
                        ObjL.GetPrecio(null, Convert.ToString(Session["CODEMP"]), (item.FindControl("txt_tp") as RadTextBox).Text, Convert.ToString(item["MBCLAVE1"]), (item.FindControl("txt_c2") as RadTextBox).Text, ".", ".", (sender as RadComboBox).SelectedValue);
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
            int i = 0, ln_item = 0;
            StringBuilder lc_error = new StringBuilder();
            Boolean lb_ind = false, lb_break = false, lb_lote = false, lb_elemento = false;
            string lc_lote = ".", lc_elem = ".";

            ArticulosBL Obj = new ArticulosBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            DataTable tb = new DataTable();
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
                            string lc_barras = "";
                            int ln_cantidad = 0;

                            Boolean lb_Existe = false;
                            if (rbl_tiparch.SelectedIndex == 0)
                            {
                                string lc_referencia = words[0];
                                string lc_c2 = words[1];
                                string lc_c3 = words[2];
                                string lc_c4 = words[3];
                                ln_cantidad = Convert.ToInt32(words[4]);
                                lc_lote = "";
                                lc_elem = "";

                                foreach (DataRow rw in tbBodega.Rows)
                                {
                                    if ((rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue == Convert.ToString(rw["ABBODEGA"]))
                                    {
                                        if (Convert.ToString(rw["ABMNLOTE"]) == "S")
                                        {
                                            lc_lote = words[5];
                                            lb_lote = true;
                                        }
                                        if (Convert.ToString(rw["ABMNELEM"]) == "S")
                                        {
                                            lc_elem = words[6];
                                            lb_elemento = true;
                                        }
                                    }

                                }

                                tb = Obj.GetArticuloInv(null, lc_referencia, lc_c2,lc_c3,lc_c4,Convert.ToString((rlv_traslados.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue);
                                if (tb.Rows.Count == 0)
                                    lc_error.AppendLine(" No Existe Ref :"+lc_referencia);
                            }
                            else
                            {
                                lc_barras = words[0];
                                ln_cantidad = Convert.ToInt32(words[1]);
                                tb= Obj.GetTbBarrasInv(null, lc_barras, Convert.ToString((rlv_traslados.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue);
                                if (tb.Rows.Count == 0)
                                    lc_error.AppendLine(" No Existe Cod Barras :" + lc_barras);
                            }

                            foreach (DataRow rx in tb.Rows)
                            {
                                lb_Existe = true;
                                if (Convert.ToInt32(rx["BBCANTID"]) < ln_cantidad)
                                    lc_error.AppendLine("Cantidad Solicitda Supera Inventario Cod: " + lc_barras+" Ref BBSA:"+Convert.ToString(rx["ARCLAVE1"]));

                                DataTable detail = new DataTable();
                                detail = ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, 0);
                                if (lb_elemento)
                                    detail = ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, lc_lote, lc_elem, 0);
                                if (detail.Rows.Count == 0)
                                    lc_error.AppendLine("Sin Regstro Articulo Ref:" + Convert.ToString(rx["ARCLAVE1"]) + " " + Convert.ToString(rx["ARCLAVE2"]) + " " + Convert.ToString(rx["ARCLAVE3"]) + " " + lc_lote + " " + lc_elem);

                                //foreach (DataRow rz in ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, 0).Rows)
                                foreach (DataRow rz in detail.Rows)
                                {
                                    lb_ind = false;
                                    lb_break = false;
                                    DataRow row = tbItems.NewRow();

                                    ln_item = tbItems.Rows.Count + 1;

                                    row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                    row["MBIDMOVI"] = 0;
                                    row["MBIDITEM"] = ln_item;
                                    row["MBFECMOV"] = System.DateTime.Today;
                                    row["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                    row["MBTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                    row["MBCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                    row["MBCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                    row["MBCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                    row["MBCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                    row["MBCODCAL"] = ".";
                                    row["MBCDTRAN"] = "99";                                    
                                    row["MBUNDMOV"] = "UN";                                    
                                    row["MBCOSTOA"] = 0;
                                    row["MBCOSTOB"] = 0;
                                    row["MBCSTNAL"] = Math.Round(Convert.ToDouble(rx["PRECIO"]));
                                    row["MBOTMOVI"] = 0;
                                    row["MBOTBODE"] = "";
                                    row["MBESTADO"] = "AC";
                                    row["MBCAUSAE"] = ".";
                                    row["MBNMUSER"] = ".";
                                    row["MBFECING"] = System.DateTime.Today;
                                    row["MBFECMOD"] = System.DateTime.Today;
                                    row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                    row["ARCDALTR"] = "";
                                    row["ARUNDINV"] = "UN";
                                    row["TANOMBRE"] = rx["TANOMBRE"];
                                    row["CLAVE2"] = ".";
                                    row["CLAVE3"] = ".";

                                    //Nuevo item en Balance
                                    DataRow itmbal = tbLotes.NewRow();
                                    itmbal["TP"] = Convert.ToString(rx["ARTIPPRO"]);
                                    itmbal["C1"] = Convert.ToString(rx["ARCLAVE1"]);
                                    itmbal["C2"] = Convert.ToString(rx["ARCLAVE2"]);
                                    itmbal["C3"] = Convert.ToString(rx["ARCLAVE3"]);
                                    itmbal["C4"] = Convert.ToString(rx["ARCLAVE4"]);
                                    itmbal["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                    itmbal["IDLOTE"] = 0;
                                    itmbal["IDELEM"] = 0;
                                    itmbal["MBCANTID"] = ln_cantidad;
                                    itmbal["IT"] = ln_item;
                                    

                                    if (Convert.ToInt32(rz["BBCANTID"]) >= ln_cantidad)
                                    {                                       
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        //Balance
                                        
                                        //}
                                        if (lb_lote)
                                        {
                                            row["MBCANMOV"] = ln_cantidad;
                                            row["MBCANTID"] = ln_cantidad;
                                            row["MBCANORI"] = ln_cantidad;
                                            row["MBSALDOI"] = ln_cantidad;
                                            itmbal["MLCDLOTE"] = "INI";
                                            itmbal["MLCANTID"] = ln_cantidad;
                                        }
                                        if (lb_elemento)
                                        {
                                            row["MBCANMOV"] = ln_cantidad;
                                            row["MBCANTID"] = ln_cantidad;
                                            row["MBCANORI"] = ln_cantidad;
                                            row["MBSALDOI"] = ln_cantidad;
                                            itmbal["MLCDLOTE"] = "INI";
                                            itmbal["MECDELEM"] = lc_elem;
                                            itmbal["MLCANTID"] = ln_cantidad;
                                            itmbal["MECANTID"] = ln_cantidad;
                                            itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(rz["BLCDLOTE"])))
                                        {
                                            row["MBCANMOV"] = Convert.ToInt32(rz["BLCANTID"]);
                                            row["MBCANTID"] = Convert.ToInt32(rz["BLCANTID"]);
                                            row["MBCANORI"] = Convert.ToInt32(rz["BLCANTID"]);
                                            row["MBSALDOI"] = Convert.ToInt32(rz["BLCANTID"]);
                                            //Balance
                                            itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                            itmbal["MLCANTID"] = rz["BLCANTID"];
                                            itmbal["MECANTID"] = ln_cantidad;
                                            itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                            lb_break = false;
                                        }
                                        else
                                        {
                                            row["MBCANMOV"] = Convert.ToInt32(rz["BBCANTID"]);
                                            row["MBCANTID"] = Convert.ToInt32(rz["BBCANTID"]);
                                            row["MBCANORI"] = Convert.ToInt32(rz["BBCANTID"]);
                                            row["MBSALDOI"] = Convert.ToInt32(rz["BBCANTID"]);
                                            //Balance
                                            itmbal["MLCDLOTE"] = "";
                                            itmbal["MBCANTID"] = rz["BBCANTID"];
                                        }

                                    }
                                                                        
                                    //if (lb_ind)
                                    //{
                                    foreach (DataRow rt in tbItems.Rows)
                                    {
                                        if (Convert.ToString(rt["MBTIPPRO"]) == Convert.ToString(rx["ARTIPPRO"]) && Convert.ToString(rt["MBCLAVE1"]) == Convert.ToString(rx["ARCLAVE1"]) && Convert.ToString(rt["MBCLAVE2"]) == Convert.ToString(rx["ARCLAVE2"]) && Convert.ToString(rt["MBCLAVE3"]) == Convert.ToString(rx["ARCLAVE3"]) && Convert.ToString(rt["MBCLAVE4"]) == Convert.ToString(rx["ARCLAVE4"]))
                                        {
                                            rt["MBCANMOV"] = Convert.ToInt32(rt["MBCANMOV"]) + Convert.ToInt32(row["MBCANTID"]);
                                            rt["MBCANTID"] = Convert.ToInt32(rt["MBCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                            rt["MBCANORI"] = Convert.ToInt32(rt["MBCANORI"]) + Convert.ToInt32(row["MBCANTID"]);
                                            rt["MBSALDOI"] = Convert.ToInt32(rt["MBSALDOI"]) + Convert.ToInt32(row["MBCANTID"]);
                                            ln_item = Convert.ToInt32(rt["MBIDITEM"]);
                                            //foreach (DataRow rf in tbLotes.Rows)
                                            //{
                                            //    if (Convert.ToInt32(rt["MBIDITEM"]) == Convert.ToInt32(rf["IT"]))
                                            //    {
                                            //        rf["MBCANTID"] = Convert.ToInt32(rf["MBCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                            //        if (!string.IsNullOrEmpty(Convert.ToString(rf["MLCDLOTE"])))
                                            //            rf["MLCANTID"] = Convert.ToInt32(rf["MLCANTID"]) + Convert.ToInt32(row["MBCANTID"]);                                                    
                                            //    }
                                            //}
                                            foreach (DataRow rf in tbLotes.Rows)
                                            {
                                                if (Convert.ToInt32(rt["MBIDITEM"]) == Convert.ToInt32(rf["IT"]))
                                                {
                                                    rf["MBCANTID"] = Convert.ToInt32(rf["MBCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                                    if (!string.IsNullOrEmpty(Convert.ToString(rf["MLCDLOTE"])))
                                                    {
                                                        rf["MLCANTID"] = Convert.ToInt32(rf["MLCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                                        itmbal["IDLOTE"] = ln_item;
                                                    }
                                                }
                                            }
                                            lb_ind = true;
                                        }
                                    }

                                    if (!lb_ind)
                                    {
                                        tbItems.Rows.Add(row);
                                        itmbal["IT"] = ln_item;
                                        itmbal["IDLOTE"] = ln_item;
                                        tbLotes.Rows.Add(itmbal);
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(itmbal["MLCDLOTE"])))
                                        {
                                            itmbal["IT"] = ln_item;
                                            tbLotes.Rows.Add(itmbal);
                                        }
                                    }
                                    
                                    if (lb_break)
                                        break;

                                }
                            }//FIN FOR ECHA
                            if (!lb_Existe)
                                lc_error.AppendLine("No se Encuentra Cod Barras: "+lc_barras);
                        }//Fin While
                         
                        (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                        litTextoMensaje.Text = "Cargue Efectuado !"+lc_error.ToString();
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
        protected void lkn_wrout_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as LinkButton).Text))
            {
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/LogisticaOut.aspx?Documento=" + (sender as LinkButton).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
        }
        protected void lkn_wrin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as LinkButton).Text))
            {
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/LogisticaIn.aspx?Documento=" + (sender as LinkButton).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
        }
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "link":
                    GridDataItem item = (GridDataItem)e.Item;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item.FindControl("lbl_referencia") as Label).Text + "');", true);
                    item = null;
                    break;
            }
        }
        protected void rc_bodegas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BodegaBL Objb = new BodegaBL();
            try
            {
                if (Convert.ToString((sender as RadComboBox).SelectedValue) != "-1")
                    tbBodega = Objb.GetLineaXBodega(null, "", Convert.ToString(Session["CODEMP"]), Convert.ToString((sender as RadComboBox).SelectedValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Objb = null;
            }
        }
        protected void lkn_tercero_Click(object sender, EventArgs e)
        {

        }
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
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


            obj_terceros.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaTerceros.DataBind();

            edt_nomtercero.Focus();
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); $find(\"" + edt_nomtercero.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaTerceros_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                        if (gc_indtercero == "A")
                        {
                            txt_exporter.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datexport.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                        if (gc_indtercero == "C")
                        {
                            txt_notify.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datnotify.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }

                        /*
                        if (gc_indtercero == "D")
                        {
                            (rlv_logistica.InsertItem.FindControl("txt_codter") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                            (rlv_logistica.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text;
                        }
                        */

                    }
                    else
                    {
                        litTextoMensaje.Text = "Tercero se Encuentra Inactivo!";
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
        protected void btn_aceptarf_Click(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            Boolean lb_inddis = true,lb_lote = false,lb_elemento=false, lb_break = false, lb_ind=false;
            string lc_lote = ".", lc_elem = ".";

            try {
                //foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)

                foreach (GridDataItem itm in rg_wrout.Items)
                {                    
                    if ((itm.FindControl("chk_chk") as CheckBox).Checked)
                    {                                                
                        foreach (DataRow rw in (Obj.GetWROUTDT(null, Convert.ToInt32(itm["WOH_CONSECUTIVO"].Text))).Rows)
                        {
                            foreach (DataRow rx in ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]), Convert.ToString(rw["ARCLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, 0).Rows)
                            {
                                lb_ind = false;
                                lb_break = false;
                                lb_inddis = false;
                                DataRow row = tbItems.NewRow();

                                int ln_item = tbItems.Rows.Count + 1;
                                double ln_cantidad = Convert.ToDouble(rw["WOD_CANTIDAD"]);

                                row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["MBIDMOVI"] = 0;
                                row["MBIDITEM"] = ln_item;
                                row["MBFECMOV"] = System.DateTime.Today;
                                row["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                row["MBTIPPRO"] = Convert.ToString(rw["ARTIPPRO"]);
                                row["MBCLAVE1"] = Convert.ToString(rw["ARCLAVE1"]);
                                row["MBCLAVE2"] = Convert.ToString(rw["ARCLAVE2"]);
                                row["MBCLAVE3"] = Convert.ToString(rw["ARCLAVE3"]);
                                row["MBCLAVE4"] = Convert.ToString(rw["ARCLAVE4"]);
                                row["MBCODCAL"] = ".";
                                row["MBCDTRAN"] = "99";
                                row["MBUNDMOV"] = "UN";
                                row["MBCOSTOA"] = 0;
                                row["MBCOSTOB"] = 0;
                                row["MBCSTNAL"] = 0;
                                row["MBOTMOVI"] = 0;
                                row["MBOTBODE"] = "";
                                row["MBESTADO"] = "AC";
                                row["MBCAUSAE"] = ".";
                                row["MBNMUSER"] = ".";
                                row["MBFECING"] = System.DateTime.Today;
                                row["MBFECMOD"] = System.DateTime.Today;
                                row["ARNOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                                row["ARCDALTR"] = "";
                                row["ARUNDINV"] = "UN";
                                row["TANOMBRE"] = rw["TANOMBRE"];
                                row["CLAVE2"] = ".";
                                row["CLAVE3"] = ".";

                                //Nuevo item en Balance
                                DataRow itmbal = tbLotes.NewRow();
                                itmbal["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                                itmbal["C1"] = Convert.ToString(rw["ARCLAVE1"]);
                                itmbal["C2"] = Convert.ToString(rw["ARCLAVE2"]);
                                itmbal["C3"] = Convert.ToString(rw["ARCLAVE3"]);
                                itmbal["C4"] = Convert.ToString(rw["ARCLAVE4"]);
                                itmbal["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                itmbal["IDLOTE"] = 0;
                                itmbal["IDELEM"] = 0;
                                itmbal["MBCANTID"] = ln_cantidad;
                                itmbal["IT"] = ln_item;

                                if (Convert.ToInt32(rx["BBCANTID"]) >= ln_cantidad)
                                {
                                    row["MBCANMOV"] = ln_cantidad;
                                    row["MBCANTID"] = ln_cantidad;
                                    row["MBCANORI"] = ln_cantidad;
                                    row["MBSALDOI"] = ln_cantidad;

                                    if (lb_lote)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MLCANTID"] = ln_cantidad;
                                    }
                                    if (lb_elemento)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MECDELEM"] = lc_elem;
                                        itmbal["MLCANTID"] = ln_cantidad;
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                    }

                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(rx["BLCDLOTE"])))
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = rx["BLCDLOTE"];
                                        itmbal["MLCANTID"] = rx["BLCANTID"];
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                        lb_break = false;
                                    }
                                    else
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = "";
                                        itmbal["MBCANTID"] = rx["BBCANTID"];
                                    }

                                }

                                
                                tbItems.Rows.Add(row);
                                itmbal["IT"] = ln_item;
                                itmbal["IDLOTE"] = ln_item;
                                tbLotes.Rows.Add(itmbal);
                                    
                                //tbWrOut.Rows.Add(Convert.ToInt32(rw["WOH_CONSECUTIVO"]), Convert.ToInt32(rw["WOD_ITEM"]), ln_item);

                                if (lb_break)
                                    break;
                            }

                            if (lb_inddis)
                            {
                                litTextoMensaje.Text = "Error !";
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            }
                        }// Fin Foreach wr
                    }
                }

                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

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
        protected void btn_aceptarw_Click(object sender, EventArgs e)
        {
            obj_wrout.SelectParameters["filter"].DefaultValue = " 1=1 ";
            if (!string.IsNullOrEmpty(txt_nrowr.Text))
                obj_wrout.SelectParameters["filter"].DefaultValue = " WOH_CONSECUTIVO=" + txt_nrowr.Text;

            rg_wrout.DataBind();
            
            string script = "function f(){$find(\"" + mpWROUT.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_aceptar_seg_Click(object sender, EventArgs e)
        {
            SegregacionBL Obj = new SegregacionBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            Boolean lb_inddis = true, lb_lote = false, lb_elemento = false, lb_break = false, lb_ind = false;
            string lc_lote = ".", lc_elem = ".";

            try
            {
                //foreach (GridDataItem item in (rlv_traslados.Items[0].FindControl("rg_items") as RadGrid).Items)

                foreach (GridDataItem itm in rg_segregaciones.Items)
                {
                    if ((itm.FindControl("chk_chk") as CheckBox).Checked)
                    {
                        foreach (DataRow rw in (Obj.GetSegregacionFactura(null, Convert.ToString(Session["CODEMP"]),Convert.ToInt32(itm["SGH_CODIGO"].Text),"")).Rows)
                        {
                            foreach (DataRow rx in ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["FD_TIPPRO"]), Convert.ToString(rw["FD_CLAVE1"]), Convert.ToString(rw["FD_CLAVE2"]), Convert.ToString(rw["FD_CLAVE3"]), Convert.ToString(rw["FD_CLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, 0).Rows)
                            {
                                lb_ind = false;
                                lb_break = false;
                                lb_inddis = false;
                                DataRow row = tbItems.NewRow();

                                int ln_item = tbItems.Rows.Count + 1;
                                double ln_cantidad = Convert.ToDouble(rw["SGD_CANTIDAD"]);

                                row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["MBIDMOVI"] = 0;
                                row["MBIDITEM"] = ln_item;
                                row["MBFECMOV"] = System.DateTime.Today;
                                row["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                row["MBTIPPRO"] = Convert.ToString(rw["FD_TIPPRO"]);
                                row["MBCLAVE1"] = Convert.ToString(rw["FD_CLAVE1"]);
                                row["MBCLAVE2"] = Convert.ToString(rw["FD_CLAVE2"]);
                                row["MBCLAVE3"] = Convert.ToString(rw["FD_CLAVE3"]);
                                row["MBCLAVE4"] = Convert.ToString(rw["FD_CLAVE4"]);
                                row["MBCODCAL"] = ".";
                                row["MBCDTRAN"] = "99";
                                row["MBUNDMOV"] = "UN";
                                row["MBCOSTOA"] = 0;
                                row["MBCOSTOB"] = 0;
                                row["MBCSTNAL"] = 0;
                                row["MBOTMOVI"] = 0;
                                row["MBOTBODE"] = "";
                                row["MBESTADO"] = "AC";
                                row["MBCAUSAE"] = ".";
                                row["MBNMUSER"] = ".";
                                row["MBFECING"] = System.DateTime.Today;
                                row["MBFECMOD"] = System.DateTime.Today;
                                row["ARNOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                                row["ARCDALTR"] = "";
                                row["ARUNDINV"] = "UN";
                                row["TANOMBRE"] = rw["TANOMBRE"];
                                row["CLAVE2"] = ".";
                                row["CLAVE3"] = ".";

                                //Nuevo item en Balance
                                DataRow itmbal = tbLotes.NewRow();
                                itmbal["TP"] = Convert.ToString(rw["FD_TIPPRO"]);
                                itmbal["C1"] = Convert.ToString(rw["FD_CLAVE1"]);
                                itmbal["C2"] = Convert.ToString(rw["FD_CLAVE2"]);
                                itmbal["C3"] = Convert.ToString(rw["FD_CLAVE3"]);
                                itmbal["C4"] = Convert.ToString(rw["FD_CLAVE4"]);
                                itmbal["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                itmbal["IDLOTE"] = 0;
                                itmbal["IDELEM"] = 0;
                                itmbal["MBCANTID"] = ln_cantidad;
                                itmbal["IT"] = ln_item;

                                if (Convert.ToInt32(rx["BBCANTID"]) >= ln_cantidad)
                                {
                                    row["MBCANMOV"] = ln_cantidad;
                                    row["MBCANTID"] = ln_cantidad;
                                    row["MBCANORI"] = ln_cantidad;
                                    row["MBSALDOI"] = ln_cantidad;

                                    if (lb_lote)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MLCANTID"] = ln_cantidad;
                                    }
                                    if (lb_elemento)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MECDELEM"] = lc_elem;
                                        itmbal["MLCANTID"] = ln_cantidad;
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                    }

                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(rx["BLCDLOTE"])))
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = rx["BLCDLOTE"];
                                        itmbal["MLCANTID"] = rx["BLCANTID"];
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                        lb_break = false;
                                    }
                                    else
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = "";
                                        itmbal["MBCANTID"] = rx["BBCANTID"];
                                    }

                                }

                                foreach (DataRow rt in tbItems.Rows)
                                {
                                    if (Convert.ToString(rt["MBTIPPRO"]) == Convert.ToString(rw["FD_TIPPRO"]) && Convert.ToString(rt["MBCLAVE1"]) == Convert.ToString(rw["FD_CLAVE1"]) && Convert.ToString(rt["MBCLAVE2"]) == Convert.ToString(rw["FD_CLAVE2"]) && Convert.ToString(rt["MBCLAVE3"]) == Convert.ToString(rw["FD_CLAVE3"]) && Convert.ToString(rt["MBCLAVE4"]) == Convert.ToString(rw["FD_CLAVE4"]))
                                    {
                                        rt["MBCANMOV"] = Convert.ToInt32(rt["MBCANMOV"]) + Convert.ToInt32(row["MBCANTID"]);
                                        rt["MBCANTID"] = Convert.ToInt32(rt["MBCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                        rt["MBCANORI"] = Convert.ToInt32(rt["MBCANORI"]) + Convert.ToInt32(row["MBCANTID"]);
                                        rt["MBSALDOI"] = Convert.ToInt32(rt["MBSALDOI"]) + Convert.ToInt32(row["MBCANTID"]);
                                        ln_item = Convert.ToInt32(rt["MBIDITEM"]);

                                        foreach (DataRow rf in tbLotes.Rows)
                                        {
                                            if (Convert.ToInt32(rt["MBIDITEM"]) == Convert.ToInt32(rf["IT"]))
                                            {
                                                rf["MBCANTID"] = Convert.ToInt32(rf["MBCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                                if (!string.IsNullOrEmpty(Convert.ToString(rf["MLCDLOTE"])))
                                                {
                                                    rf["MLCANTID"] = Convert.ToInt32(rf["MLCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
                                                    itmbal["IDLOTE"] = ln_item;
                                                }
                                            }
                                        }
                                        lb_ind = true;
                                    }
                                }

                                if (!lb_ind)
                                {
                                    tbItems.Rows.Add(row);
                                    itmbal["IT"] = ln_item;
                                    itmbal["IDLOTE"] = ln_item;
                                    tbLotes.Rows.Add(itmbal);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(itmbal["MLCDLOTE"])))
                                    {
                                        itmbal["IT"] = ln_item;
                                        tbLotes.Rows.Add(itmbal);
                                    }
                                }

                                if (lb_break)
                                    break;
                            }

                            if (lb_inddis)
                            {
                                litTextoMensaje.Text = "Error !";
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            }
                        }// Fin Foreach wr
                    }
                }

                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

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
                        itm["BLH_CODIGO"] = -1;
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
        protected void rg_container_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbContainers;
        }
        protected void btn_agregarctn_Click(object sender, EventArgs e)
        {
            DataRow itm = tbBillxTraslado.NewRow();
            try
            {
                itm["BLH_CODIGO"] = -1;
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

                tbBillxTraslado.Rows.Add(itm);

                if (rlv_traslados.InsertItem != null)
                {
                    (rlv_traslados.InsertItem.FindControl("rg_bl") as RadGrid).DataSource = tbBillxTraslado;
                    (rlv_traslados.InsertItem.FindControl("rg_bl") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_traslados.Items[0].FindControl("rg_bl") as RadGrid).DataSource = tbBillxTraslado;
                    (rlv_traslados.Items[0].FindControl("rg_bl") as RadGrid).DataBind();
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
        protected void rg_bl_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbBillxTraslado;
        }
        protected void rg_segregacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link_segregacion":
                    GridDataItem item = (GridDataItem)e.Item;
                    string url_ = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/SegregacionV2.aspx?Segregacion=" + (item.FindControl("lbl_segregacion") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url_ + "');", true);
                    item = null;
                    break;
            }
        }
        protected void rg_segregacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbSegregacion;
        }
        protected void btn_aceptarwrin_Click(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            Boolean lb_inddis = true, lb_lote = false, lb_elemento = false, lb_break = false, lb_ind = false;
            string lc_lote = ".", lc_elem = ".";


            try
            {                
                foreach (GridDataItem itm in rg_wrin.Items)
                {
                    if ((itm.FindControl("chk_chk") as CheckBox).Checked)
                    {
                        foreach (DataRow rw in (Obj.GetWRINDT(null, Convert.ToInt32(itm["WIH_CONSECUTIVO"].Text))).Rows)
                        {
                            foreach (DataRow rx in ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["ARTIPPRO"]), Convert.ToString(rw["ARCLAVE1"]), Convert.ToString(rw["ARCLAVE2"]), Convert.ToString(rw["ARCLAVE3"]), Convert.ToString(rw["ARCLAVE4"]), (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, 0).Rows)
                            {
                                lb_ind = false;
                                lb_break = false;
                                lb_inddis = false;
                                DataRow row = tbItems.NewRow();

                                int ln_item = tbItems.Rows.Count + 1;
                                double ln_cantidad = Convert.ToDouble(rw["WID_CANTIDAD"]);

                                row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                row["MBIDMOVI"] = 0;
                                row["MBIDITEM"] = ln_item;
                                row["MBFECMOV"] = System.DateTime.Today;
                                row["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                row["MBTIPPRO"] = Convert.ToString(rw["ARTIPPRO"]);
                                row["MBCLAVE1"] = Convert.ToString(rw["ARCLAVE1"]);
                                row["MBCLAVE2"] = Convert.ToString(rw["ARCLAVE2"]);
                                row["MBCLAVE3"] = Convert.ToString(rw["ARCLAVE3"]);
                                row["MBCLAVE4"] = Convert.ToString(rw["ARCLAVE4"]);
                                row["MBCODCAL"] = ".";
                                row["MBCDTRAN"] = "99";
                                row["MBUNDMOV"] = "UN";
                                row["MBCOSTOA"] = 0;
                                row["MBCOSTOB"] = 0;
                                row["MBCSTNAL"] = 0;
                                row["MBOTMOVI"] = 0;
                                row["MBOTBODE"] = "";
                                row["MBESTADO"] = "AC";
                                row["MBCAUSAE"] = ".";
                                row["MBNMUSER"] = ".";
                                row["MBFECING"] = System.DateTime.Today;
                                row["MBFECMOD"] = System.DateTime.Today;
                                row["ARNOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                                row["ARCDALTR"] = "";
                                row["ARUNDINV"] = "UN";
                                row["TANOMBRE"] = rw["TANOMBRE"];
                                row["CLAVE2"] = ".";
                                row["CLAVE3"] = ".";

                                //Nuevo item en Balance
                                DataRow itmbal = tbLotes.NewRow();
                                itmbal["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                                itmbal["C1"] = Convert.ToString(rw["ARCLAVE1"]);
                                itmbal["C2"] = Convert.ToString(rw["ARCLAVE2"]);
                                itmbal["C3"] = Convert.ToString(rw["ARCLAVE3"]);
                                itmbal["C4"] = Convert.ToString(rw["ARCLAVE4"]);
                                itmbal["MBBODEGA"] = (rlv_traslados.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                                itmbal["IDLOTE"] = 0;
                                itmbal["IDELEM"] = 0;
                                itmbal["MBCANTID"] = ln_cantidad;
                                itmbal["IT"] = ln_item;

                                if (Convert.ToInt32(rx["BBCANTID"]) >= ln_cantidad)
                                {
                                    row["MBCANMOV"] = ln_cantidad;
                                    row["MBCANTID"] = ln_cantidad;
                                    row["MBCANORI"] = ln_cantidad;
                                    row["MBSALDOI"] = ln_cantidad;

                                    if (lb_lote)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MLCANTID"] = ln_cantidad;
                                    }
                                    if (lb_elemento)
                                    {
                                        row["MBCANMOV"] = ln_cantidad;
                                        row["MBCANTID"] = ln_cantidad;
                                        row["MBCANORI"] = ln_cantidad;
                                        row["MBSALDOI"] = ln_cantidad;
                                        itmbal["MLCDLOTE"] = "INI";
                                        itmbal["MECDELEM"] = lc_elem;
                                        itmbal["MLCANTID"] = ln_cantidad;
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                    }

                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(rx["BLCDLOTE"])))
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BLCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = rx["BLCDLOTE"];
                                        itmbal["MLCANTID"] = rx["BLCANTID"];
                                        itmbal["MECANTID"] = ln_cantidad;
                                        itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                        lb_break = false;
                                    }
                                    else
                                    {
                                        row["MBCANMOV"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANTID"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBCANORI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        row["MBSALDOI"] = Convert.ToInt32(rx["BBCANTID"]);
                                        //Balance
                                        itmbal["MLCDLOTE"] = "";
                                        itmbal["MBCANTID"] = rx["BBCANTID"];
                                    }

                                }

                                                                
                                tbItems.Rows.Add(row);
                                itmbal["IT"] = ln_item;
                                itmbal["IDLOTE"] = ln_item;
                                tbLotes.Rows.Add(itmbal);

                                tbWrIn.Rows.Add(Convert.ToInt32(rw["WIH_CONSECUTIVO"]), Convert.ToInt32(rw["WID_ITEM"]), ln_item);
                                

                                if (lb_break)
                                    break;
                            }

                            if (lb_inddis)
                            {
                                litTextoMensaje.Text = "Error !";
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            }
                        }// Fin Foreach wr
                    }
                }

                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_traslados.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

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
        protected void btn_buscarwrin_Click(object sender, EventArgs e)
        {
            obj_wrin.SelectParameters["filter"].DefaultValue = " 1=1 ";
            if (!string.IsNullOrEmpty(txt_nrowrin.Text))
                obj_wrin.SelectParameters["filter"].DefaultValue = " WIH_CONSECUTIVO=" + txt_nrowrin.Text;

            rg_wrin.DataBind();

            string script = "function f(){$find(\"" + mpWRIN.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
    }
}