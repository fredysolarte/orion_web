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
using XUSS.BLL.Compras;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Compras
{
    public partial class RecepcionCompras : System.Web.UI.Page
    {
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
        private string gc_moneda
        {
            set { ViewState["moneda"] = value; }
            get { return Convert.ToString(ViewState["moneda"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_recibo.SelectParameters["filter"].DefaultValue = " RH_NRORECIBO =" + Convert.ToString(Request.QueryString["Documento"]);
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
                    try
                    {
                        tbItems = Obj.GetReciboDT(null, Convert.ToString(Session["CODEMP"]), 0, 0);
                        gc_moneda = ComunBL.GetMoneda(null, Convert.ToString(Session["CODEMP"]));
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
                    obj_recibo.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_compras.DataBind();
                    break;
                case "Edit":

                    break;
                case "Delete":
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

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text))
                filtro += " AND CH_NROCMP = " + (((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrorecibo") as RadTextBox).Text))
                filtro += " AND RH_NRORECIBO = " + (((RadButton)sender).Parent.FindControl("txt_nrorecibo") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_recibo.SelectParameters["filter"].DefaultValue = filtro;
            rlv_compras.DataBind();
            if ((rlv_compras.Controls[0] is RadListViewEmptyDataItem))
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
                    OrdenesComprasBL Obj = new OrdenesComprasBL();
                    try
                    {
                        //tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_traslados.Items[0].GetDataKeyValue("TSMOVSAL").ToString()));
                        tbItems = Obj.GetComprasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));
                        (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
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
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8004&inban=S&inParametro=inConse&inValor=" + Convert.ToString((rlv_compras.Items[0].FindControl("txt_nroorden") as RadTextBox).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "InitInsert")
            {
                //if (rlv_compras.InsertItem != null)
                {
                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    this.Limpiar();
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
                rqf_catidad1.Validate();
                rqf_catidad2.Validate();
                rqf_referencia.Validate();

                if (rqf_catidad1.IsValid && rqf_catidad2.IsValid && rqf_referencia.IsValid)
                {
                    row["CD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                    row["CD_NROCMP"] = 0;
                    row["CD_NROITEM"] = tbItems.Rows.Count + 1;
                    //row["CD_BODEGA"] = (rlv_compras.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
                    row["CD_BODEGA"] = "";
                    row["CD_TIPPRO"] = txt_tp.Text;
                    row["CD_CLAVE1"] = txt_referencia.Text;
                    row["CD_CLAVE2"] = txt_clave2.Text;
                    row["CD_CLAVE3"] = txt_clave3.Text;
                    row["CD_CLAVE4"] = txt_clave4.Text;
                    //row["CD_PROVEE"] = (rlv_compras.InsertItem.FindControl("rc_proveedor") as RadComboBox).SelectedValue;
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
                    row["C2"] = "";
                    row["C3"] = "";
                    row["C4"] = "";
                    row["ENLACE"] = "";
                    row["VLRTOT"] = (txt_cantidad.Value * txt_precio.Value);
                    row["TANOMBRE"] = "";
                    row["ARNOMBRE"] = txt_descripcion.Text;
                    row["CANRECIBE"] = 0;
                    row["CANRESTANTE"] = 0;

                    //foreach (DataRow rw in tbItems.Rows)
                    //{
                    //    row["CD_NROITEM"] = i;
                    //    i++;
                    //}

                    tbItems.Rows.Add(row);

                    this.Limpiar();
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                            txt_cantidad.Value = 1;
                        }

                        btn_agregar_Aceptar(sender, e);

                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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

        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            if (rlv_compras.InsertItem != null)
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_compras.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_compras.Items[0].FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            //((sender as ImageButton).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();
            //((sender as ImageButton).Parent.FindControl("mpArticulos") as ModalPopupExtender).Show();
            //edt_referencia.Text = "";
            // edt_nombreart.Text = "";
            //rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            edt_referencia.Text = "";
            edt_nombreart.Text = "";

            string script = "function f(){$find(\"" + mpFindArticulo.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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

            //obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_compras.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue;
            obj_articulos.SelectParameters["inBodega"].DefaultValue = null;


            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;


            //rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            rgConsultaArticulos.DataBind();
            string script = "function f(){$find(\"" + mpFindArticulo.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                    txt_clave2.Text = Convert.ToString(item["ARCLAVE2"].Text);
                    txt_clave3.Text = Convert.ToString(item["ARCLAVE3"].Text);
                    txt_clave4.Text = Convert.ToString(item["ARCLAVE4"].Text);
                    txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                    //txt_caninv.Value = Convert.ToDouble(item["BBCANTID"].Text);
                    //txt_preciolta.Value = Convert.ToDouble(item["PRECIO"].Text);
                    //txt_dct.Value = Convert.ToDouble(item["DESCUENTO"].Text);

                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    txt_cantidad.Focus();
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
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            double ln_subtotal = 0, ln_impuesto = 0, ln_total = 0;
            switch (e.CommandName)
            {
                case "Delete":
                    var DTNROITM = item.GetDataKeyValue("CD_NROITEM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == Convert.ToInt32(DTNROITM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();

                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw["CD_NROITEM"] = i;
                        i++;
                    }

                    break;
            }
        }
        protected void rlv_compras_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_compras_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
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
        }
        protected void obj_compras_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbItems;
        }
        protected void rc_moneda_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (Convert.ToString(gc_moneda) != (sender as RadComboBox).SelectedValue)
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
            finally
            {

            }
        }
        protected void txt_nroorden_OnTextChanged(object sender, EventArgs e)
        {
            Boolean lb_ind = false;
            string lc_estado ="";
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                  OrdenesComprasBL Obj = new OrdenesComprasBL();
                  try
                  {
                      using (IDataReader reader = Obj.GetComprasHD(null,Convert.ToString(Session["CODEMP"]),Convert.ToInt32((sender as RadTextBox).Text)))
                      {
                        while(reader.Read())
                        {
                            lb_ind = true;
                            lc_estado =Convert.ToString(reader["CH_ESTADO"]);
                        }
                      }

                      if (lb_ind)
                      {
                          if (lc_estado =="AC")
                          {
                              foreach (DataRow rw in Obj.GetComprasHD(null, " CH_NROCMP = "+(sender as RadTextBox).Text, 0, 0).Rows)
                              {
                                  (((RadTextBox)sender).Parent.FindControl("edt_forden") as RadDatePicker).SelectedDate = Convert.ToDateTime(rw["CH_FECORD"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_bodegas") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_BODEGA"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_proveedor") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_PROVEEDOR"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_torden") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_TIPORD"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_tdespacho") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_TIPDPH"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_tpago") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_TERPAG"]);
                                  (((RadTextBox)sender).Parent.FindControl("rc_moneda") as RadComboBox).SelectedValue = Convert.ToString(rw["CH_MONEDA"]);
                                  (((RadTextBox)sender).Parent.FindControl("txt_observaciones") as RadTextBox).Text = Convert.ToString(rw["CH_OBSERVACIONES"]);
                              }

                              tbItems = Obj.GetReciboDT(null,null,0,0);
                              //tbItems = Obj.GetComprasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadTextBox).Text));
                              foreach (DataRow rw in Obj.GetComprasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadTextBox).Text)).Rows)
                              {
                                  DataRow rx = tbItems.NewRow();
                                  try
                                  {
                                      rx["CD_CODEMP"] = rw["CD_CODEMP"];
                                      rx["CD_NROCMP"] = rw["CD_NROCMP"];
                                      rx["CD_NROITEM"] = rw["CD_NROITEM"];
                                      rx["CD_BODEGA"] = rw["CD_BODEGA"];
                                      rx["CD_TIPPRO"] = rw["CD_TIPPRO"];
                                      rx["CD_CLAVE1"] = rw["CD_CLAVE1"];
                                      rx["CD_CLAVE2"] = rw["CD_CLAVE2"];
                                      rx["CD_CLAVE3"] = rw["CD_CLAVE3"];
                                      rx["CD_CLAVE4"] = rw["CD_CLAVE4"];
                                      rx["CD_PROVEE"] = rw["CD_PROVEE"];
                                      rx["CD_REFPRO"] = rw["CD_REFPRO"];
                                      rx["CD_COLPRO"] = rw["CD_COLPRO"];
                                      rx["CD_CANTIDAD"] = rw["CD_CANTIDAD"];
                                      rx["CD_UNIDAD"] = rw["CD_UNIDAD"];
                                      rx["CD_PRECIO"] = rw["CD_PRECIO"];
                                      rx["CD_OBSERVACIONES"] = rw["CD_OBSERVACIONES"];
                                      rx["CD_USUARIO"] = rw["CD_USUARIO"];
                                      rx["CD_ESTADO"] = "AC";
                                      rx["CD_FECING"] = System.DateTime.Today;
                                      rx["CD_FECMOD"] = System.DateTime.Today;
                                      rx["TANOMBRE"] = rw["TANOMBRE"];
                                      rx["ARNOMBRE"] = rw["ARNOMBRE"];
                                      rx["CLAVE2"] = rw["CLAVE2"];
                                      rx["CLAVE3"] = rw["CLAVE3"];
                                      rx["CLAVE4"] = rw["CLAVE4"];
                                      rx["RD_CANTIDAD"] = 0;
                                      rx["LOT1"] = "";
                                      rx["LOT2"] = "";

                                      tbItems.Rows.Add(rx);
                                  }
                                  catch (Exception ex)
                                  {
                                      throw ex;
                                  }
                                  finally
                                  {
                                      rx = null;                              
                                  }
                              }

                              (((RadTextBox)sender).Parent.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                              (((RadTextBox)sender).Parent.FindControl("rg_items") as RadGrid).DataBind();
                          } else
                          {
                            litTextoMensaje.Text = "Nro Orden se Recepciono de Manera Completa!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                          }
                      } else
                      {
                        litTextoMensaje.Text = "Nro Orden NO EXISTE!";
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
        }
        protected void txt_valor_OnTextChanged(object sender, EventArgs e)
        {
            TipoProductosBL Obj = new TipoProductosBL();
            Boolean lb_lot = false;
            try
            {              
                {
                    int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_item") as RadTextBox).Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CD_NROITEM"]) == ln_codigo)
                        {
                            if ((((sender as RadNumericTextBox).Parent.FindControl("txt_valor") as RadNumericTextBox)).DbValue != null)
                            {
                                using (IDataReader reader = Obj.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), (rlv_compras.InsertItem.FindControl("rc_bodegas") as RadComboBox).SelectedValue, ((sender as RadNumericTextBox).Parent.FindControl("txt_tp") as RadTextBox).Text))
                                {
                                    while (reader.Read())
                                    {
                                        if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                                            lb_lot = true;
                                    }
                                }

                                if ((((sender as RadNumericTextBox).Parent.FindControl("txt_cancom") as RadNumericTextBox).Value - ((sender as RadNumericTextBox).Parent.FindControl("txt_valor") as RadNumericTextBox).Value) < 0)
                                {
                                    litTextoMensaje.Text = "Cantidad Recibida No puede Superar Cantidad Solicitada!";
                                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                }
                                else
                                {
                                    ((sender as RadNumericTextBox).Parent.FindControl("txt_dif") as RadNumericTextBox).Value = ((sender as RadNumericTextBox).Parent.FindControl("txt_cancom") as RadNumericTextBox).Value - ((sender as RadNumericTextBox).Parent.FindControl("txt_valor") as RadNumericTextBox).Value;
                                    if (lb_lot)
                                    {
                                        txt_iditem.Text = Convert.ToString(ln_codigo);
                                        string script = "function f(){$find(\"" + mpDtLotes.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                    }
                                    row["RD_CANTIDAD"] = (sender as RadNumericTextBox).Value;
                                    break;
                                }
                            }

                            
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
        protected void btn_aceptarlot_OnClick(object sender, EventArgs e)
        {
            try
            {
                tbItems.Columns["LOT1"].ReadOnly = false;
                tbItems.Columns["LOT2"].ReadOnly = false;
                foreach (DataRow row in tbItems.Rows)
                {                    
                    if (Convert.ToInt32(row["CD_NROITEM"]) == Convert.ToInt32(txt_iditem.Text))
                    {                        
                        row["LOT1"] = txt_dt1.Text;
                        row["LOT2"] = txt_dt2.Text;
                        break;
                    }
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
        protected void ct_menu_ItemClick(object sender, RadMenuEventArgs e)
        {
            foreach (DataRow row in tbItems.Rows)
            {
                row["RD_CANTIDAD"] = row["CD_CANTIDAD"];
            }
            //(rlv_compras.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
            (rlv_compras.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
        }
    }
}