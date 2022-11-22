using DataAccess;
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
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;
using XUSS.BLL.Parametros;
using XUSS.BLL.Pedidos;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Inventarios
{
    public partial class MovimientosManuales : System.Web.UI.Page
    {
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbLotes
        {
            set { ViewState["tbLotes"] = value; }
            get { return ViewState["tbLotes"] as DataTable; }
        }        
        private DataTable tbBodega
        {
            set { ViewState["tbBodega"] = value; }
            get { return ViewState["tbBodega"] as DataTable; }
        }
        private string gc_tiptran
        {
            set { ViewState["gc_tiptran"] = value; }
            get { return ViewState["gc_tiptran"] as string; }
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
                            Objm.AnularMovimientoM(null, Convert.ToString(Session["CODEMP"]),Convert.ToInt32(rw["MBIDMOVI"]), Convert.ToInt32(rw["MBIDITEM"]),Convert.ToString(Session["UserLogon"]));
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
                filtro += " AND MIIDMOVI = " + (((RadButton)sender).Parent.FindControl("txt_movimiento") as RadTextBox).Text + "";

            if ((((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).Text != "Seleccionar" && (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).Text != "" )
                filtro += " AND MIBODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_transaccion") as RadComboBox).Text != "Seleccionar" && (((RadButton)sender).Parent.FindControl("rc_transaccion") as RadComboBox).Text != "")
                filtro += " AND MICDTRAN = '" + (((RadButton)sender).Parent.FindControl("rc_transaccion") as RadComboBox).SelectedValue + "'";

            if (!(((RadButton)sender).Parent.FindControl("txt_fSolicitud") as RadDatePicker).IsEmpty)
                filtro += " AND CONVERT(DATE,MIFECMOV,101) = CONVERT(DATE,'" + Convert.ToString((((RadButton)sender).Parent.FindControl("txt_fSolicitud") as RadDatePicker).SelectedDate.Value.Month) + "/" + Convert.ToString((((RadButton)sender).Parent.FindControl("txt_fSolicitud") as RadDatePicker).SelectedDate.Value.Day) +"/"+ Convert.ToString((((RadButton)sender).Parent.FindControl("txt_fSolicitud") as RadDatePicker).SelectedDate.Value.Year) + "',101)";
            
            

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
                    TercerosBL objT = new TercerosBL();
                    RadComboBoxItem item_ = new RadComboBoxItem();
                    
                    try
                    {
                        tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_movimientos.Items[0].FindControl("txt_movimiento") as RadTextBox).Text ));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        if (fila["MICODTER"].ToString() != "")
                        {
                            using (IDataReader reader = objT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["MICODTER"].ToString())))
                            {
                                while (reader.Read())
                                {
                                    RadComboBoxItem itemi = new RadComboBoxItem();
                                    itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                    itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                    (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                                }
                            }
                        }
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = fila["MISUCURSAL"].ToString();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        objT = null;
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
                        this.Limpiar();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    e.Canceled = true;
                }
            }
            if (e.CommandName == "Charge")
            {
                rbl_tiparch.Items[0].Text = "Referencia + C2 + C3 + C4 + Can"; 
                foreach (DataRow rw in tbBodega.Rows)
                {
                    if ((rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue == Convert.ToString(rw["ABBODEGA"]))
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

                        cmpNumbers.Enabled= false;
                        if (gc_tiptran=="S")
                            cmpNumbers.Enabled = true;
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
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {

            obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
            obj_articulos.SelectParameters["LT"].DefaultValue = null;
            //((sender as ImageButton).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();
            //((sender as ImageButton).Parent.FindControl("mpArticulos") as ModalPopupExtender).Show();
            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            //rgConsultaArticulos.DataBind();
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
            
            obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
            if (gc_tiptran=="E")
                obj_articulos.SelectParameters["inBodega"].DefaultValue = null;

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
                    txt_nommarca.Text = (item.FindControl("txt_marca") as RadTextBox).Text;
                    txt_clave2.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                    txt_clave3.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                    txt_clave4.Text = ".";
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
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            Boolean lb_bandera = true;
            DataRow row = tbItems.NewRow();
            DataRow rwl = tbLotes.NewRow();
            try
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
                    row["MBCDTRAN"] = (rlv_movimientos.InsertItem.FindControl("rc_transaccion") as RadComboBox).SelectedValue;
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
                     rwl["IDLOTE"] = 0;
                     rwl["IDELEM"] = 0;
                    tbLotes.Rows.Add(rwl);
                }
                (rlv_movimientos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_movimientos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                this.Limpiar();
                txt_barras.Focus();
                string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarras(null, (sender as RadTextBox).Text,null);
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
        protected void obj_movimientos_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbitems"] = tbLotes;
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
        protected void obj_movimientos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Movimiento :" + Convert.ToString(e.ReturnValue) + " Confirmado!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7009&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7009&inban=S&inParametro=consecutivo&inValor=" + (rlv_movimientos.Items[0].FindControl("txt_movimiento") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void rc_transaccion_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if ((sender as RadComboBox).SelectedValue != "-1")
            {
                try
                {
                    foreach (DataRow rw in TipoMovimientoBL.GetTipoMovimiento(null, " TMCDTRAN ='" + (sender as RadComboBox).SelectedValue + "'", 0, 0).Rows)
                    {
                        gc_tiptran = Convert.ToString(rw["TMENTSAL"]);
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
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;                        
            switch (e.CommandName)
            {
                case "Delete":
                    var MBIDITEM = item.GetDataKeyValue("MBIDITEM").ToString();
                    int i = 0;
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["MBIDITEM"]) == Convert.ToInt32(MBIDITEM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();


                    pos = 0;
                    xpos = 0;
                    foreach (DataRow rw in tbLotes.Rows)
                    {
                        if (Convert.ToInt32(rw["IT"]) == Convert.ToInt32(MBIDITEM))
                            pos = xpos;
                                //rw.Delete();
                        
                    }
                    tbLotes.Rows[pos].Delete();
                    tbLotes.AcceptChanges();

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        foreach (DataRow rx in tbLotes.Rows)
                        {
                            if (Convert.ToInt32(rx["IT"]) == Convert.ToInt32(rw["MBIDITEM"]))
                            {
                                rx["IT"] = i;
                            }                            
                        }
                        rw["MBIDITEM"] = i;
                        i++;
                    }

                    break;
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
            Boolean lb_ind = false, lb_break = false, lb_lote= false,lb_elemento=false;
            string lc_lote = ".", lc_elem = ".";

            DataTable dt = new DataTable();
            ArticulosBL Obj = new ArticulosBL();
            LtaEmpaqueBL ObjE = new LtaEmpaqueBL();
            TipoProductosBL ObjT = new TipoProductosBL();
            string sMessage = "";
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
                            int ln_cantidad = 0;

                            if (rbl_tiparch.SelectedIndex == 0)
                            {
                                string lc_tp = words[0];
                                string lc_c1 = words[1];
                                string lc_c2 = words[2];
                                string lc_c3 = words[3];
                                string lc_c4 = words[4];
                                ln_cantidad = Convert.ToInt32(words[5]);
                                lc_lote = "";
                                lc_elem = "";

                                foreach (DataRow rw in tbBodega.Rows)
                                {
                                    if ((rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue == Convert.ToString(rw["ABBODEGA"]))
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

                                dt = Obj.GetArticuloInv(null,lc_tp,lc_c1,lc_c2,lc_c3,lc_c4,".", (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue);
                                if (dt.Rows.Count == 0)
                                    sMessage += "Articulo No Existe Ref :" + lc_c1;
                            }
                            else
                            {
                                string lc_barras = words[0];
                                ln_cantidad = Convert.ToInt32(words[1]);

                                dt = Obj.GetTbBarrasInv(null, lc_barras, "LBASE07", (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue);
                                if (dt.Rows.Count == 0)
                                    sMessage += "Cod Barrras No Existe Ref :" + lc_barras;
                            }

                            foreach (DataRow rx in dt.Rows)
                            {
                                if (gc_tiptran == "S")
                                {
                                    DataTable detail = new DataTable();
                                    detail = ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, 0);
                                    if (lb_elemento)
                                        detail = ObjE.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue,lc_lote,lc_elem,0);
                                    if (detail.Rows.Count == 0)
                                        sMessage += "Sin Registro Articulo Ref:" + Convert.ToString(rx["ARCLAVE1"]) + " " + Convert.ToString(rx["ARCLAVE2"]) + " " + Convert.ToString(rx["ARCLAVE3"]) + " " + lc_lote + " " + lc_elem;

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
                                        row["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                                        row["MBTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                        row["MBCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                        row["MBCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                        row["MBCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                        row["MBCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                        row["MBCODCAL"] = ".";
                                        row["MBCDTRAN"] = (rlv_movimientos.InsertItem.FindControl("rc_transaccion") as RadComboBox).SelectedValue;
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
                                        itmbal["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                                        itmbal["IDLOTE"] = 0;
                                        itmbal["IDELEM"] = 0;
                                        itmbal["MBCANTID"] = ln_cantidad;
                                        itmbal["IT"] = ln_item;

                                        if (Convert.ToInt32(rz["BBCANTID"]) >= ln_cantidad)
                                        {
                                            //if (!string.IsNullOrEmpty(Convert.ToString(rz["BLCDLOTE"])))
                                            if (lb_elemento)
                                            {
                                                //if (Convert.ToInt32(rz["BLCANTID"]) >= ln_cantidad)
                                                //{
                                                //    row["MBCANMOV"] = ln_cantidad;
                                                //    row["MBCANTID"] = ln_cantidad;
                                                //    row["MBCANORI"] = ln_cantidad;
                                                //    row["MBSALDOI"] = ln_cantidad;
                                                //    //Balance
                                                //    itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                                //    itmbal["MLCANTID"] = ln_cantidad;
                                                //    lb_break = true;
                                                //}
                                                //else
                                                //{
                                                //    row["MBCANMOV"] = Convert.ToInt32(rz["BLCANTID"]);
                                                //    row["MBCANTID"] = Convert.ToInt32(rz["BLCANTID"]);
                                                //    row["MBCANORI"] = Convert.ToInt32(rz["BLCANTID"]);
                                                //    row["MBSALDOI"] = Convert.ToInt32(rz["BLCANTID"]);

                                                //    ln_cantidad = ln_cantidad - Convert.ToInt32(rz["BLCANTID"]);
                                                //    itmbal["MLCDLOTE"] = rz["BLCDLOTE"];
                                                //    itmbal["MLCANTID"] = Convert.ToInt32(rz["BLCANTID"]);
                                                //    lb_break = false;
                                                //}


                                                //if (!string.IsNullOrEmpty(Convert.ToString(rz["BECDELEM"])))
                                                //{
                                                //    //itmbal["MLCDLOTE"] = "INI";
                                                //    itmbal["MECDELEM"] = lc_elem;
                                                //    itmbal["MLCANTID"] = ln_cantidad;
                                                //    itmbal["MECANTID"] = ln_cantidad;
                                                //    itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
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
                                                row["MBCANMOV"] = ln_cantidad;
                                                row["MBCANTID"] = ln_cantidad;
                                                row["MBCANORI"] = ln_cantidad;
                                                row["MBSALDOI"] = ln_cantidad;
                                                //Balance
                                                itmbal["MLCDLOTE"] = "";
                                                itmbal["MBCANTID"] = ln_cantidad;
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
                                                //itmbal["MLCANTID"] = rz["BLCANTID"];
                                                itmbal["MLCANTID"] = ln_cantidad;
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
                                }
                                else {
                                    lb_ind = false;
                                    lb_break = false;
                                    DataRow row = tbItems.NewRow();

                                    ln_item = tbItems.Rows.Count + 1;

                                    row["MBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                    row["MBIDMOVI"] = 0;
                                    row["MBIDITEM"] = ln_item;
                                    row["MBFECMOV"] = System.DateTime.Today;
                                    row["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                                    row["MBTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                    row["MBCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                    row["MBCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                    row["MBCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                    row["MBCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                    row["MBCODCAL"] = ".";
                                    row["MBCDTRAN"] = (rlv_movimientos.InsertItem.FindControl("rc_transaccion") as RadComboBox).SelectedValue;
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
                                    itmbal["MBBODEGA"] = (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
                                    itmbal["IT"] = ln_item;

                                    row["MBCANMOV"] = ln_cantidad;
                                    row["MBCANTID"] = ln_cantidad;
                                    row["MBCANORI"] = ln_cantidad;
                                    row["MBSALDOI"] = ln_cantidad;
                                    //Balance
                                    itmbal["MLCDLOTE"] = "";
                                    itmbal["MLCANTID"] = 0;
                                    itmbal["MBCANTID"] = ln_cantidad;
                                    itmbal["IDLOTE"] = 0;
                                    itmbal["IDELEM"] = 0;

                                    //using (IDataReader reader = ObjT.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), (rlv_movimientos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, Convert.ToString(rx["ARTIPPRO"])))
                                    //{
                                    //    while (reader.Read())
                                    //    {
                                            if (lb_lote)
                                            {
                                                itmbal["MLCDLOTE"] = "INI";
                                                itmbal["MLCANTID"] = ln_cantidad;
                                            }
                                            if (lb_elemento)
                                            {
                                                itmbal["MLCDLOTE"] = "INI";
                                                itmbal["MECDELEM"] = lc_elem;
                                                itmbal["MLCANTID"] = ln_cantidad;
                                                itmbal["MECANTID"] = ln_cantidad;
                                                itmbal["IDELEM"] = tbLotes.Rows.Count + 1;
                                            }
                                    //    }
                                    //}
                                    


                                    foreach (DataRow rt in tbItems.Rows)
                                    {
                                        if (Convert.ToString(rt["MBTIPPRO"]) == Convert.ToString(rx["ARTIPPRO"]) && Convert.ToString(rt["MBCLAVE1"]) == Convert.ToString(rx["ARCLAVE1"]) && Convert.ToString(rt["MBCLAVE2"]) == Convert.ToString(rx["ARCLAVE2"]) && Convert.ToString(rt["MBCLAVE3"]) == Convert.ToString(rx["ARCLAVE3"]) && Convert.ToString(rt["MBCLAVE4"]) == Convert.ToString(rx["ARCLAVE4"]))
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
                                                        itmbal["IDLOTE"] = ln_item;
                                                        rf["MLCANTID"] = Convert.ToInt32(rf["MLCANTID"]) + Convert.ToInt32(row["MBCANTID"]);
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
                        }//Fin While
                        (rlv_movimientos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_movimientos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                        if (!string.IsNullOrEmpty(sMessage))
                            litTextoMensaje.Text = sMessage;                        
                        else
                            litTextoMensaje.Text = "Cargue Completo";

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
                ObjT = null;                
            }
        }
        protected void rg_items_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        int MBIDMOVI = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDMOVI").ToString());
                        int MBIDITEM = Convert.ToInt32(dataItem.GetDataKeyValue("MBIDITEM").ToString());
                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        MovimientosBL Obj = new MovimientosBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.CargarMovimientoLot(null, Convert.ToString(Session["CODEMP"]), MBIDMOVI, MBIDITEM);
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
                    {
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
                        dt.Columns.Add("MECDELEM", typeof(string));
                        dt.Columns.Add("MECANTID", typeof(Int32));
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
                                    rw["MECDELEM"] = ((DataRow)tbLotes.Rows[i])["MECDELEM"];
                                    rw["MECANTID"] = ((DataRow)tbLotes.Rows[i])["MECANTID"];
                                    rw["IT"] = ((DataRow)tbLotes.Rows[i])["IT"];

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
        protected void rc_bodega_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BodegaBL Objb = new BodegaBL();
            try
            {
                if (Convert.ToString((sender as RadComboBox).SelectedValue)!="-1")
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
                        (rlv_movimientos.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text;
                        (rlv_movimientos.InsertItem.FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);                        
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

        protected void rc_tercero_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TercerosBL obj = new TercerosBL();
            RadComboBoxItem item_ = new RadComboBoxItem();
            try {
                item_.Value = "";
                item_.Text = "Seleccionar";
                (((RadComboBox)sender).Parent.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                using (IDataReader reader = obj.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadComboBox).SelectedValue)))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                        itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                        (((RadComboBox)sender).Parent.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                    }
                }
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

        protected void rlv_movimientos_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_movimientos.InsertParameters["MISUCURSAL"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;
        }
    }
}