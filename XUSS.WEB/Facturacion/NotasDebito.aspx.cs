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
using XUSS.BLL.Facturacion;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Facturacion
{
    public partial class NotasDebito : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbMonedas
        {
            set { ViewState["tbMonedas"] = value; }
            get { return ViewState["tbMonedas"] as DataTable; }
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
                    obj_notas.SelectParameters["filter"].DefaultValue = "NH_TIPFAC ='" + words[0] + "' AND NH_NRONOTA =" + words[1];
                    rlv_notas.DataBind();
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
            this.OcultarPaginador(rlv_notas, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_notas_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    NotasBL obj = new NotasBL();
                    try
                    {
                        tbItems = obj.GetNotaDebDT(null, Convert.ToString(Session["CODEMP"]), "",0);
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
                    obj_notas.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_notas.DataBind();
                    break;
                case "Delete":
                    NotasBL objn = new NotasBL();
                    try
                    {
                        if (((rlv_notas.Items[0].FindControl("rc_estado")) as RadComboBox).SelectedValue == "AN")
                        {
                            litTextoMensaje.Text = "¡Nota Credito Ya se Encuentra Anulada!";
                            e.Canceled = true;
                        }
                        else
                        {
                            objn.AnulaNotaDeb(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(((rlv_notas.Items[0].FindControl("txt_nronota")) as RadComboBox).SelectedValue), Convert.ToInt32(((rlv_notas.Items[0].FindControl("txt_nronota")) as RadTextBox).Text), Convert.ToString(Session["UserLogon"]));
                            litTextoMensaje.Text = "¡Nota Credito Anulado Exitosamente!";
                        }
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objn = null;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            //if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text))
            //    filtro += " AND TRNOMBR2 LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text))
                filtro += " AND TERCEROS.TRCODTER = '" + (((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue !="-1")
                filtro += " AND NH_TIPFAC = '" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nronota") as RadTextBox).Text))
                filtro += " AND NH_NRONOTA = " + (((RadButton)sender).Parent.FindControl("txt_nronota") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text))
                filtro += " AND NH_NRONOTA IN (SELECT NH_NRONOTA FROM TB_NOTADEBDT WHERE DTNROFAC =  " + (((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text + ")";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_notas.SelectParameters["filter"].DefaultValue = filtro;
            rlv_notas.DataBind();
            if ((rlv_notas.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_notas.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_notas_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    NotasBL obj = new NotasBL();
                    TercerosBL objT = new TercerosBL();                    
                    RadComboBoxItem item_ = new RadComboBoxItem();
                    try
                    {
                        tbItems = obj.GetNotaDebDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(((rlv_notas.Items[0].FindControl("rc_tipfac")) as RadComboBox).SelectedValue), Convert.ToInt32(rlv_notas.Items[0].GetDataKeyValue("NH_NRONOTA").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        using (IDataReader reader = objT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["TRCODTER"].ToString())))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                            }
                        }
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = fila["SC_CONSECUTIVO"].ToString();

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
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            txt_codter.Text = "";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroTer_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";

            if (!string.IsNullOrWhiteSpace(edt_nomtercero.Text))
                filter += "AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + edt_nomtercero.Text.ToUpper() + "%'";
            if (!string.IsNullOrWhiteSpace(txt_codter.Text))
                filter += "AND UPPER(TRCODTER) =" + txt_codter.Text.ToUpper() + "";
            if (!string.IsNullOrWhiteSpace(edt_identificacion.Text))
                filter += "AND UPPER(TRCODNIT) LIKE '%" + edt_identificacion.Text.ToUpper() + "%'";


            obj_terceros.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaTerceros.DataBind();
            //mpTerceros.Show();
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
                try
                {

                    (rlv_notas.InsertItem.FindControl("txt_nrodoc") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                    (rlv_notas.InsertItem.FindControl("txt_nomter") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                    (rlv_notas.InsertItem.FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);

                    (rlv_notas.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                    item_.Value = "";
                    item_.Text = "Seleccionar";
                    (rlv_notas.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                    using (IDataReader reader = obj.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(Convert.ToString(item["TRCODTER"].Text))))
                    {
                        while (reader.Read())
                        {
                            RadComboBoxItem itemi = new RadComboBoxItem();
                            itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                            itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                            (rlv_notas.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
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
                    obj = null;
                    item_ = null;
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
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                (rlv_notas.InsertItem.FindControl("rqv_tercero") as RequiredFieldValidator).Validate();
                if ((rlv_notas.InsertItem.FindControl("rqv_tercero") as RequiredFieldValidator).IsValid)
                {
                    obj_factura.SelectParameters["filter"].DefaultValue = " TERCEROS.TRCODTER =" + (rlv_notas.InsertItem.FindControl("txt_codcli") as RadTextBox).Text + " AND HDESTADO NOT IN ('AN') AND TFCLAFAC IN ('1','5') ";
                    rg_facturas.DataBind();
                    string script = "function f(){$find(\"" + modalFacturas.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    e.Canceled = true;
                }
                else
                {
                    e.Canceled = true;
                }
            }

            if (e.CommandName == "link")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturacionSegmentos.aspx?Documento=" + (item_.FindControl("lbl_tipfac") as Label).Text + "-" + (item_.FindControl("lbl_nrofac") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }
        }
        protected void rg_facturas_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string script = "";
            GridEditableItem item = e.Item as GridEditableItem;
            try
            {
                switch (e.CommandName)
                {
                    case "Select":
                        obj_facturadt.SelectParameters["DTTIPFAC"].DefaultValue = item.GetDataKeyValue("HDTIPFAC").ToString();
                        obj_facturadt.SelectParameters["DTNROFAC"].DefaultValue = item.GetDataKeyValue("HDNROFAC").ToString();
                        rg_detalle.DataBind();
                        script = "function f(){$find(\"" + modalDetalle.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                        break;
                    default:
                        script = "function f(){$find(\"" + modalFacturas.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btn_agregarItems_OnClick(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem item in rg_detalle.Items)
                {
                    if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                    {
                        DataRow row = tbItems.NewRow();
                        row["ND_NROITEM"] = tbItems.Rows.Count + 1;
                        row["ND_CODEMP"] = "0";
                        row["NH_TIPFAC"] = "XX";
                        row["NH_NRONOTA"] = 0;
                        row["DTTIPFAC"] = Convert.ToString((item.FindControl("rc_tipfac") as RadComboBox).SelectedValue);
                        row["DTNROFAC"] = Convert.ToString((item.FindControl("txt_nrofac") as RadTextBox).Text);
                        row["DTNROITM"] = Convert.ToString((item.FindControl("txt_nroitm") as RadTextBox).Text);
                        //row["ND_DESCRIPCION"] = "Nota Credito";
                        row["ND_DESCRIPCION"] = (item.FindControl("lbl_articulo") as Label).Text;
                        row["ND_TARIFA"] = Convert.ToString((item.FindControl("txt_codtarifa") as RadTextBox).Text);
                        row["ND_SUBTOTAL"] = Convert.ToDouble(((item.FindControl("txt_totalnew") as RadNumericTextBox).Value) / ((((item.FindControl("txt_tarifa") as RadNumericTextBox).Value) / 100) + 1));
                        row["ND_IMPUESTO"] = Convert.ToDouble((item.FindControl("txt_totalnew") as RadNumericTextBox).Value) - Convert.ToDouble(row["ND_SUBTOTAL"]);
                        row["ND_VALOR"] = Convert.ToDouble((item.FindControl("txt_totalnew") as RadNumericTextBox).Value);
                        row["ND_ESTADO"] = "AC";
                        row["ND_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                        row["ND_FECING"] = System.DateTime.Now;
                        row["ND_FECMOD"] = System.DateTime.Now;

                        tbItems.Rows.Add(row);
                    }
                }

                (rlv_notas.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_notas.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        protected void obj_notas_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "";
                //string retorno = Convert.ToString(e.ReturnValue);
                string[] retorno = Convert.ToString(e.ReturnValue).Split('-');
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);
                try
                {                    
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8007&inban=S&inParametro=inNumero&inValor=" + retorno[1]  + "&inParametro=inTipo&inValor=" + retorno[0];
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
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);

        }
        protected void rlv_notas_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_notas_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            foreach (GridDataItem item in (rlv_notas.InsertItem.FindControl("rg_items") as RadGrid).Items)
            {
                foreach (DataRow rw in tbItems.Rows)
                {
                    //int ln_codigo = Convert.ToInt32(item["RS_CODIGO"].Text);
                    string lc_codigo = Convert.ToString((item.FindControl("rc_tipfac") as RadComboBox).SelectedValue) + item["DTNROFAC"].Text + item["DTNROITM"].Text;
                    if (lc_codigo == (Convert.ToString(rw["DTTIPFAC"]) + Convert.ToString(rw["DTNROFAC"]) + Convert.ToString(rw["DTNROITM"])))
                    {
                        rw["ND_DESCRIPCION"] = (item.FindControl("txt_observacion") as RadTextBox).Text;
                    }
                }
            }
            e.InputParameters["intb"] = tbItems;
            e.InputParameters["inMonedas"] = tbMonedas;
        }
        protected void rlv_notas_OnItemInserting(object sender, RadListViewCommandEventArgs e)
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

                if (lb_bandera)
                {
                    litTextoMensaje.Text = sMensaje.ToString();
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    e.Canceled = true;
                }

                obj_notas.InsertParameters["SC_CONSECUTIVO"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;
                obj_notas.InsertParameters["INMONEDA"].DefaultValue = gc_moneda;
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
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var ND_NROITEM = item.GetDataKeyValue("ND_NROITEM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["ND_NROITEM"]) == Convert.ToInt32(ND_NROITEM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();

                    break;
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalPrinter.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            //string url = "";
            //try
            //{
            //    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8007&inban=S&inParametro=inNumero&inValor=" + (rlv_notas.Items[0].FindControl("txt_nronota") as RadTextBox).Text+ "&inParametro=inTipo&inValor="+Convert.ToString((rlv_notas.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);

            //    FastReport.Utils.Config.WebMode = true;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        protected void rg_detalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string script = "function f(){$find(\"" + modalDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void txt_fecnota_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            TasaCambioBL obj = new TasaCambioBL();
            try
            {
                tbMonedas = obj.GetTasas(null, Convert.ToDateTime((sender as RadDatePicker).DbSelectedDate));
                if (tbMonedas.Rows.Count == 0)
                {                    
                    litTextoMensaje.Text = "¡Moneda sin Tasa de Cambio Moneda!";                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                }
                else
                {
                    rg_tasacambio.DataSource = tbMonedas;
                    rg_tasacambio.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMonedas.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
        protected void btn_aceptar_tasas_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rg_tasacambio.Items)//(rlv_notas.InsertItem.FindControl("rg_items") as RadGrid).Items)
            {
                if ((item.FindControl("chk_habilita") as CheckBox).Checked == false)
                {
                    int pos = 0, xpos = 0;
                    foreach (DataRow rw in tbMonedas.Rows)
                    {
                        if (Convert.ToString(rw["TC_MONEDA"]) == Convert.ToString(item["TC_MONEDA"]))
                            pos = xpos;
                        xpos++;
                    }

                    tbMonedas.Rows[pos].Delete();
                    tbMonedas.AcceptChanges();
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
                foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + Convert.ToString((rlv_notas.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
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
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=inNumero&inValor=" + (rlv_notas.Items[0].FindControl("txt_nronota") as RadTextBox).Text +
                          "&inParametro=inTipo&inValor=" + Convert.ToString((rlv_notas.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=inMoneda&inValor=" + Convert.ToString(rc_moneda_print.SelectedValue);
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
        protected void btn_filtro_factura_Click(object sender, EventArgs e)
        {
            (rlv_notas.InsertItem.FindControl("rqv_tercero") as RequiredFieldValidator).Validate();
            if ((rlv_notas.InsertItem.FindControl("rqv_tercero") as RequiredFieldValidator).IsValid)
            {
                obj_factura.SelectParameters["filter"].DefaultValue = " TERCEROS.TRCODTER =" + (rlv_notas.InsertItem.FindControl("txt_codcli") as RadTextBox).Text + " AND HDESTADO NOT IN ('AN') AND TFCLAFAC  IN ('1','5') ";
                if (!string.IsNullOrEmpty(txt_nrodocfilter.Text))
                    obj_factura.SelectParameters["filter"].DefaultValue = " TERCEROS.TRCODTER =" + (rlv_notas.InsertItem.FindControl("txt_codcli") as RadTextBox).Text + " AND HDESTADO NOT IN ('AN') AND TFCLAFAC  IN ('1','5') AND HDNROFAC IN (" + txt_nrodocfilter.Text + ")";
                rg_facturas.DataBind();
                string script = "function f(){$find(\"" + modalFacturas.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
    }
}