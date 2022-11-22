using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Articulos;
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Inventarios
{
    public partial class TransformacionTester : System.Web.UI.Page
    {
        private DataTable tbLotes
        {
            set { ViewState["tbLotes"] = value; }
            get { return ViewState["tbLotes"] as DataTable; }
        }
        private DataTable tbLotesT
        {
            set { ViewState["tbLotesT"] = value; }
            get { return ViewState["tbLotesT"] as DataTable; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbTester
        {
            set { ViewState["tbTester"] = value; }
            get { return ViewState["tbTester"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_movimientos.SelectParameters["filter"].DefaultValue = " MIIDMOVI =" + Convert.ToString(Request.QueryString["Documento"]);
                    rlv_movimientos.DataBind();
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
            this.OcultarPaginador(rlv_movimientos, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_movimientos_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    MovimientosBL Obj = new MovimientosBL();
                    try
                    {
                        tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbTester = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), 0);

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
                        tbLotes.Columns.Add("IT", typeof(Int32));

                        tbLotesT = new DataTable();
                        tbLotesT.Columns.Add("TP", typeof(string));
                        tbLotesT.Columns.Add("C1", typeof(string));
                        tbLotesT.Columns.Add("C2", typeof(string));
                        tbLotesT.Columns.Add("C3", typeof(string));
                        tbLotesT.Columns.Add("C4", typeof(string));
                        tbLotesT.Columns.Add("MBBODEGA", typeof(string));
                        tbLotesT.Columns.Add("MLCDLOTE", typeof(string));
                        tbLotesT.Columns.Add("MLCANTID", typeof(Int32));
                        tbLotesT.Columns.Add("MBCANTID", typeof(Int32));
                        tbLotesT.Columns.Add("IT", typeof(Int32));
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
                    obj_movimientos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_movimientos.DataBind();
                    break;
                case "Edit":
                    break;
                case "Delete":
                    MovimientosBL Objm = new MovimientosBL();
                    try
                    {
                        foreach (DataRow rw in tbItems.Rows)
                        {
                            Objm.AnularMovimientoM(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]), Convert.ToString(Session["UserLogon"]));
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

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_movimiento") as RadTextBox).Text))
                filtro += " AND TR_NROTRA = " + (((RadButton)sender).Parent.FindControl("txt_movimiento") as RadTextBox).Text + "";

            if ((((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).Text != "Seleccionar" && (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).Text != "")
                filtro += " AND TR_BODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue + "'";            

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_movimientos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_movimientos.DataBind();
            if ((rlv_movimientos.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_movimientos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_movimientos_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    MovimientosBL Obj = new MovimientosBL();
                    try
                    {
                        tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_movimientos.Items[0].FindControl("txt_msalida") as RadTextBox).Text));
                        (e.Item.FindControl("rg_regular") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_regular") as RadGrid).DataBind();

                        tbLotes = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_movimientos.Items[0].FindControl("txt_mentrada") as RadTextBox).Text));
                        (e.Item.FindControl("rg_tester") as RadGrid).DataSource = tbLotes;
                        (e.Item.FindControl("rg_tester") as RadGrid).DataBind();
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
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (rlv_movimientos.InsertItem == null)
                {

                }
                else
                {
                    (rlv_movimientos.InsertItem.FindControl("RequiredFieldValidator2") as RequiredFieldValidator).Validate();
                    (rlv_movimientos.InsertItem.FindControl("RequiredFieldValidator1") as RequiredFieldValidator).Validate();
                    if ((rlv_movimientos.InsertItem.FindControl("RequiredFieldValidator2") as RequiredFieldValidator).IsValid && (rlv_movimientos.InsertItem.FindControl("RequiredFieldValidator1") as RequiredFieldValidator).IsValid)
                    {
                        (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).Enabled = false;
                        (rlv_movimientos.InsertItem.FindControl("rc_transaccion") as RadComboBox).Enabled = false;
                        //this.Limpiar();
                        //string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    e.Canceled = true;
                }
            }           

        }
        protected void obj_movimientos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Transformacion :" + Convert.ToString(e.ReturnValue) + " Confirmado!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8013&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }
        protected void obj_movimientos_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbitems"] = tbLotes;
            e.InputParameters["tbTester"] = tbLotesT;
        }
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8013&inban=S&inParametro=consecutivo&inValor=" + (rlv_movimientos.Items[0].FindControl("txt_movimiento") as RadTextBox).Text + "');", true);
        }
        protected void rlv_movimientos_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rg_tester_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbTester;
        }
        protected void btn_ndetalle_Click(object sender, EventArgs e)
        {
            (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).Enabled = false;

            this.Limpiar();
            string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {

            obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
            obj_articulos.SelectParameters["LT"].DefaultValue = null;

            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            
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

            obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;                        
            obj_articulos.SelectParameters["LT"].DefaultValue = null;
            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;

            //mpAddArticulos.Show();
            rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                        txt_tp.Text = Convert.ToString(item["ARTIPPRO"].Text);
                        txt_referencia.Text = Convert.ToString(item["ARCLAVE1"].Text);                        
                        txt_clave2.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                        txt_clave3.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                        txt_clave4.Text = ".";
                        txt_nommarca.Text = (item.FindControl("txt_marca") as RadTextBox).Text;
                        txt_nc2.Text = Convert.ToString(item["CLAVE2"].Text);
                        txt_nc3.Text = Convert.ToString(item["CLAVE3"].Text);
                        txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                        txt_caninv.Value = Convert.ToDouble(item["BBCANTID"].Text);
                        txt_preciolta.Value = Convert.ToDouble(item["PRECIO"].Text);
                        txt_dct.Value = Convert.ToDouble(item["DESCUENTO"].Text);

                        script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        txt_cantidad.Focus();

                        this.ConfigLinea();
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
                    break;

                default:
                    script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            txt_nc2.Text = "";
            txt_nc3.Text = "";
            txt_descripcion.Text = "";
            txt_barras.Text = "";
            txt_cantidad.Value = 0;
            txt_caninv.Value = 0;

        }
        private void ConfigLinea()
        {
            TipoProductosBL Obj = new TipoProductosBL();
            try
            {
                using (IDataReader reader = Obj.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, txt_tp.Text))
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

                        cmpNumbers.Enabled = false;
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
                foreach (DataRow rw in Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]), (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
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
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            Boolean lb_bandera = true;
            ArticulosBL Obj = new ArticulosBL();
            DataRow row = tbItems.NewRow();
            DataRow rwl = tbLotes.NewRow();
            DataRow rowt = tbTester.NewRow();
            DataRow rwt = tbLotesT.NewRow();
            string lc_reftester = "",lc_nomtester="",lc_marcatester="";
            try
            {   //Valida Ref Existencia Tester
                lb_bandera = false;
                foreach (DataRow rw in Obj.GetTester(null, Convert.ToString(Session["CODEMP"]), txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text).Rows)
                {
                    lb_bandera = true;
                    lc_reftester = Convert.ToString(rw["TT_CLAVE1"]);
                    lc_nomtester = Convert.ToString(rw["ARNOMBRE"]);
                    lc_marcatester = Convert.ToString(rw["TANOMBRE"]);
                }
                if (lb_bandera)
                {
                    //Valida Existencia Misma Referencia
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
                        row["MBIDITEM"] = tbItems.Rows.Count;
                        row["MBFECMOV"] = System.DateTime.Today;
                        row["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                        row["MBTIPPRO"] = txt_tp.Text;
                        row["MBCLAVE1"] = txt_referencia.Text;
                        row["MBCLAVE2"] = txt_clave2.Text;
                        row["MBCLAVE3"] = txt_clave3.Text;
                        row["MBCLAVE4"] = txt_clave4.Text;
                        row["MBCODCAL"] = ".";
                        row["MBCDTRAN"] = "18";
                        row["MBCANMOV"] = txt_cantidad.Value;
                        row["MBUNDMOV"] = "UN";
                        row["MBCANTID"] = txt_cantidad.Value;
                        row["MBCANORI"] = txt_cantidad.Value;
                        row["MBSALDOI"] = txt_cantidad.Value;
                        row["MBCOSTOA"] = 0;
                        row["MBCOSTOB"] = 0;
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
                        row["TANOMBRE"] = txt_nommarca.Text;
                        row["CLAVE2"] = txt_nc2.Text;
                        row["CLAVE3"] = txt_nc3.Text;

                        tbItems.Rows.Add(row);
                    }

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
                    if (lb_bandera)
                    {
                        rwl["TP"] = txt_tp.Text;
                        rwl["C1"] = txt_referencia.Text;
                        rwl["C2"] = txt_clave2.Text;
                        rwl["C3"] = txt_clave3.Text;
                        rwl["C4"] = txt_clave4.Text;
                        rwl["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                        rwl["MLCDLOTE"] = rc_lote.SelectedValue;
                        rwl["MLCANTID"] = txt_cantidad.Value;
                        rwl["MBCANTID"] = txt_cantidad.Value;
                        rwl["IT"] = tbItems.Rows.Count;
                        tbLotes.Rows.Add(rwl);
                    }
                    
                    (rlv_movimientos.InsertItem.FindControl("rg_regular") as RadGrid).DataSource = tbItems;
                    (rlv_movimientos.InsertItem.FindControl("rg_regular") as RadGrid).DataBind();

                    //Agrega Tester
                    lb_bandera = true;
                    foreach (DataRow rw in tbTester.Rows)
                    {
                        if ((Convert.ToString(rw["MBTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rw["MBCLAVE1"]) == lc_reftester) && (Convert.ToString(rw["MBCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rw["MBCLAVE3"]) == txt_clave3.Text))
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
                        rowt["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rowt["MBIDMOVI"] = 0;
                        rowt["MBIDITEM"] = tbTester.Rows.Count;
                        rowt["MBFECMOV"] = System.DateTime.Today;
                        rowt["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                        rowt["MBTIPPRO"] = txt_tp.Text;
                        rowt["MBCLAVE1"] = lc_reftester;
                        rowt["MBCLAVE2"] = txt_clave2.Text;
                        rowt["MBCLAVE3"] = txt_clave3.Text;
                        rowt["MBCLAVE4"] = txt_clave4.Text;
                        rowt["MBCODCAL"] = ".";
                        rowt["MBCDTRAN"] = "18";
                        rowt["MBCANMOV"] = txt_cantidad.Value;
                        rowt["MBUNDMOV"] = "UN";
                        rowt["MBCANTID"] = txt_cantidad.Value;
                        rowt["MBCANORI"] = txt_cantidad.Value;
                        rowt["MBSALDOI"] = txt_cantidad.Value;
                        rowt["MBCOSTOA"] = 0;
                        rowt["MBCOSTOB"] = 0;
                        rowt["MBOTMOVI"] = 0;
                        rowt["MBOTBODE"] = "";
                        rowt["MBESTADO"] = "AC";
                        rowt["MBCAUSAE"] = ".";
                        rowt["MBNMUSER"] = ".";
                        rowt["MBFECING"] = System.DateTime.Today;
                        rowt["MBFECMOD"] = System.DateTime.Today;
                        rowt["ARNOMBRE"] = lc_nomtester;
                        rowt["ARCDALTR"] = "";
                        rowt["ARUNDINV"] = "UN";
                        rowt["TANOMBRE"] = lc_marcatester;
                        rowt["CLAVE2"] = txt_nc2.Text;
                        rowt["CLAVE3"] = txt_nc3.Text;

                        tbTester.Rows.Add(rowt);
                    }

                    lb_bandera = true;
                    foreach (DataRow rw in tbLotesT.Rows)
                    {
                        if ((Convert.ToString(rw["TP"]) == txt_tp.Text) && (Convert.ToString(rw["C1"]) == lc_reftester) && (Convert.ToString(rw["C2"]) == txt_clave2.Text) && (Convert.ToString(rw["C3"]) == txt_clave3.Text))
                        {
                            lb_bandera = false;
                            rw["MLCANTID"] = Convert.ToDouble(rw["MLCANTID"]) + txt_cantidad.Value;
                            rw["MBCANTID"] = Convert.ToDouble(rw["MBCANTID"]) + txt_cantidad.Value;
                        }
                    }
                    if (lb_bandera)
                    {
                        rwt["TP"] = txt_tp.Text;
                        rwt["C1"] = lc_reftester;
                        rwt["C2"] = txt_clave2.Text;
                        rwt["C3"] = txt_clave3.Text;
                        rwt["C4"] = txt_clave4.Text;
                        rwt["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                        rwt["MLCDLOTE"] = rc_lote.SelectedValue;
                        rwt["MLCANTID"] = txt_cantidad.Value;
                        rwt["MBCANTID"] = txt_cantidad.Value;
                        rwt["IT"] = tbTester.Rows.Count;
                        tbLotesT.Rows.Add(rwt);
                    }

                    (rlv_movimientos.InsertItem.FindControl("rg_tester") as RadGrid).DataSource = tbTester;
                    (rlv_movimientos.InsertItem.FindControl("rg_tester") as RadGrid).DataBind();


                    this.Limpiar();
                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    litTextoMensaje.Text = "¡Referencia No Tiene Asociado Tester!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                rowt = null;
                rwt = null;
                Obj = null;
            }
        }
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
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
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_nc2.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nc3.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_caninv.Value = Convert.ToDouble(rw["BBCANTID"]);
                            txt_nommarca.Text = Convert.ToString(rw["TANOMBRE"]);
                            txt_cantidad.Value = 1;
                        }
                        this.ConfigLinea();
                        if (!rc_lote.Visible)
                            btn_agregar_Aceptar(sender, e);
                        else
                            this.CargarLote();
                        //mpAddArticulos.Show();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "Codigo Barras Invalido!";
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
    }
}