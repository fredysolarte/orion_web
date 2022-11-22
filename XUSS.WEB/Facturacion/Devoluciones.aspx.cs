using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using XUSS.BLL.Facturacion;
using System.Text;
using XUSS.BLL.Comun;
using XUSS.BLL.Terceros;
using XUSS.BLL.Parametros;
using XUSS.BLL.Articulos;
using System.Configuration;

namespace XUSS.WEB.Facturacion
{
    public partial class Devoluciones : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbCopia
        {
            set { ViewState["tbCopia"] = value; }
            get { return ViewState["tbCopia"] as DataTable; }
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
            string filtro = "AND HDTIPFAC IN (SELECT TFTIPFAC FROM TBTIPFAC WITH(NOLOCK) WHERE TFCLAFAC IN(2))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND (TRNOMBRE + ISNULL(TRNOMBR2,'')+ ISNULL(TRAPELLI,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

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
            RadComboBoxItem item_ = new RadComboBoxItem();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                (e.Item.FindControl("lnk_devolucion") as LinkButton).Text = Convert.ToString((e.Item.FindControl("rc_tipfacori") as RadComboBox).SelectedValue) + "-" + Convert.ToString((e.Item.FindControl("txt_facorigen") as RadTextBox).Text);
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

                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                item_.Value = "";
                item_.Text = "Seleccionar";
                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                using (IDataReader reader = ObjT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["HDCODCLI"].ToString())))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                        itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                    }
                }
                (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = fila["HDCODSUC"].ToString();
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
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_pagos_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbPagos;
        }
        protected double GetSaldo()
        {
            double? ln_total = (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).Value;
            double ln_recaudo = 0;
            try
            {
                foreach (DataRow rw in tbPagos.Rows)
                {
                    ln_recaudo += Math.Round(Convert.ToDouble(rw["PGVLRPAG"]), 2); ;
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
            Boolean lb_bandera = false, lb_marcacion = true;

            StringBuilder sMensaje = new StringBuilder();
            try
            {
                if (tbItems.Rows.Count == 0)
                {
                    sMensaje.AppendLine("-No Tiene Items");
                    lb_bandera = true;
                }
                foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
                {
                    //int ln_codigo = Convert.ToInt32(item["DTNROITM"].Text);
                    //if ((item.FindControl("chk_Estado") as CheckBox).Checked)
                    //{
                    if ((item.FindControl("edt_cantidad") as RadNumericTextBox).Value > 0)
                        lb_marcacion = false;
                    //}

                }
                if (lb_marcacion)
                {
                    sMensaje.AppendLine("-No Cuenta Con Devoluciones");
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


                tbCopia = tbItems.Copy();
                tbCopia.Clear();

                for (int i = 0; i < (e.ListViewItem.FindControl("rg_items") as RadGrid).Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Text) || ((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value != 0)
                    {
                       int ln_item = Convert.ToInt32(((GridDataItem)(e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i])["DTNROITM"].Text);
                       foreach (DataRow rw in tbItems.Rows)
                       {
                           if (Convert.ToInt32(rw["DTNROITM"]) == ln_item)                           
                           {
                               tbCopia.ImportRow(rw);                               
                           
                               foreach (DataRow Row in tbCopia.Rows)
                               {
                                   if (Convert.ToInt32(Row["DTNROITM"]) == ln_item)
                                   {
                                       double ln_precio = Convert.ToDouble(((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_precio") as RadNumericTextBox).Value);
                                       double ln_cantidad = Convert.ToDouble(((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value);
                                       double ln_subtot = ln_precio * ln_cantidad;
                                                                      
                                       double ln_imp = 0;
                                       if (Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value) != 0)
                                           ln_imp = (Convert.ToDouble(((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_iva") as RadNumericTextBox).Value) / Convert.ToDouble(((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value)) * 100;

                                       double ln_iva = 0;
                                       if (ln_imp != 0)
                                           ln_iva = (ln_subtot * ln_imp) / 100;

                                       Row["DTSUBTOT"] = ln_subtot;
                                       Row["DTTOTIVA"] = ln_iva;
                                       Row["DTTOTFAC"] = ln_subtot + ln_iva;
                                       Row["DTCANTID"] = ((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value;
                                       Row["DTCANPED"] = ((e.ListViewItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value;

                                       // Balance
                                       DataRow rx = tbBalance.NewRow();
                                       rx["TP"] = Row["DTTIPPRO"];
                                       rx["C1"] = Row["DTCLAVE1"];
                                       rx["C2"] = Row["DTCLAVE2"];
                                       rx["C3"] = Row["DTCLAVE3"];
                                       rx["C4"] = Row["DTCLAVE4"];
                                       rx["MBBODEGA"] = null;
                                       rx["MLCDLOTE"] = "INI";
                                       rx["MLCANTID"] = Row["DTCANTID"];
                                       rx["MBCANTID"] = Row["DTCANTID"];
                                       rx["IT"] = Row["DTNROITM"];

                                       tbBalance.Rows.Add(rx);
                                       rx = null;
                                   }
                               }
                           }
                       }
                    }
                }
                int ln_litem = 1;
                foreach(DataRow rw in tbCopia.Rows)
                {
                    foreach (DataRow rz in tbBalance.Rows)
                    {
                        if (Convert.ToInt32(rw["DTNROITM"]) == Convert.ToInt32(rz["IT"]))
                            rz["IT"] = ln_litem;
                    }
                    rw["DTNROITM"] = ln_litem;
                    ln_litem++;
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
            DataTable dt = new DataTable();
            dt = tbItems.Copy();                       
            e.InputParameters["tbDetalle"] = tbCopia;
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
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inImp=S&inClo=S&inParametro=InNumero&inValor=" + retorno[1] +
                              "&inParametro=InTipo&inValor=" + retorno[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);
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
            
                string script = "function f(){$find(\"" + modalPrinter.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);            
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
                    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=InNumero&inValor=" + (rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text +
                          "&inParametro=InTipo&inValor=" + Convert.ToString((rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inMoneda&inValor=" + Convert.ToString(rc_moneda_print.SelectedValue);
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
                Obj.AnularFacturacionInventario(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.Items[0].FindControl("txt_nrofac") as RadTextBox).Text),
                                      rc_causae.SelectedValue, Convert.ToString(Session["UserLogon"]));
                litTextoMensaje.Text = "Factura Anulada!";
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                            //mpMensajes.Show();
                            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }


                        // Informar lo que falta para terminar Numeros de Facturacion o Vence Fecha
                        //foreach (DataRow rs in Obj.GetResolucion(null, Convert.ToString(Session["CODEMP"]), (sender as RadComboBox).SelectedValue).Rows)
                        //{ 
                        //    if ()
                        //}

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
        protected void txt_facorigen_OnTextChanged(object sender, EventArgs e)
        {
            ComunBL ObjC = new ComunBL();
            TercerosBL ObjT = new TercerosBL();
            FacturacionBL Obj = new FacturacionBL();
            RadComboBoxItem item_ = new RadComboBoxItem();
            try
            {
                foreach (DataRow rw in ObjT.GetTerceros(null, "TRCODTER=(SELECT HDCODCLI FROM FACTURAHD WHERE HDCODEMP='" + Convert.ToString(Session["CODEMP"]) + "' AND HDTIPFAC='" + (rlv_factura.InsertItem.FindControl("rc_tipfacori") as RadComboBox).SelectedValue + "' AND HDNROFAC=" + (rlv_factura.InsertItem.FindControl("txt_facorigen") as RadTextBox).Text + ")", 0, 0).Rows)
                {
                    (rlv_factura.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(rw["TRCODNIT"]);
                    (rlv_factura.InsertItem.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(rw["TRCODTER"]);
                    (rlv_factura.InsertItem.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(rw["TRNOMBRE"]);
                    (rlv_factura.InsertItem.FindControl("txt_apellido") as RadTextBox).Text = Convert.ToString(rw["TRAPELLI"]);
                    (rlv_factura.InsertItem.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(rw["TRDIRECC"]);
                    (rlv_factura.InsertItem.FindControl("txt_telefono") as RadTextBox).Text = Convert.ToString(rw["TRNROTEL"]);
                    (rlv_factura.InsertItem.FindControl("txt_email") as RadTextBox).Text = Convert.ToString(rw["TRCORREO"]);
                    (rlv_factura.InsertItem.FindControl("rc_pais") as RadComboBox).SelectedValue = Convert.ToString(rw["TRCDPAIS"]);

                    item_.Value = "";
                    item_.Text = "Seleccionar";
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
                    
                    //Cargar Agente(Vendedor)
                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Clear();
                    (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(item_);
                    foreach (DataRow row in ObjT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(row["TRCODTER"]);
                        itemi.Text = Convert.ToString(row["TRNOMBRE"]) + " " + Convert.ToString(row["TRNOMBR2"]) + " " + Convert.ToString(row["TRAPELLI"]);
                        (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                    }
                    //Datos de Cabecera de Factura
                    foreach (DataRow row in Obj.GetFacturaHD(null, " HDTIPFAC='" + Convert.ToString((rlv_factura.InsertItem.FindControl("rc_tipfacori") as RadComboBox).SelectedValue) + "' AND HDNROFAC=" + (rlv_factura.InsertItem.FindControl("txt_facorigen") as RadTextBox).Text, 0, 0).Rows)
                    {
                        if (Convert.ToString(row["HDESTADO"]) == "AN")
                        {
                            throw new System.ArgumentException("Factura Origen se Encuentra Anulada!");
                        }
                        (rlv_factura.InsertItem.FindControl("rc_agente") as RadComboBox).SelectedValue = Convert.ToString(row["HDAGENTE"]);

                        //Cargar Sucursales
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        using (IDataReader reader = ObjT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(row["HDCODCLI"])))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                            }
                        }
                    (rlv_factura.InsertItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue = Convert.ToString(row["HDCODSUC"]);
                    }
                    
                }
                tbItems = Obj.GetFacturaDT(null, Convert.ToString(Session["CODEMP"]), (rlv_factura.InsertItem.FindControl("rc_tipfacori") as RadComboBox).SelectedValue, Convert.ToInt32((rlv_factura.InsertItem.FindControl("txt_facorigen") as RadTextBox).Text));
                (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
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
                ObjT = null;
                ObjC = null;
                item_ = null;
            }
        }
        protected void edt_cantidad_OnTextChanged(object sender, EventArgs e)
        {
            double ln_vlrtot = 0, ln_subtotal = 0, ln_impuesto=0;
            Boolean lb_ind = false;
            string lc_impf = "";
            DataRow row = tbPagos.NewRow();
            ArticulosBL Obj = new ArticulosBL ();
            try
                {                    
                    for (int i = 0; i < (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items.Count; i++)
                    {
                        if (string.IsNullOrEmpty(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Text) || ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value != 0)
                        {
                            if (((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value <= ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad_real") as RadNumericTextBox).Value)
                            {
                                lb_ind = true;
                                string lc_cadena = " ARCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND ARTIPPRO ='" + ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_tippro") as RadTextBox).Text + "' AND ARCLAVE1='" + ((GridDataItem)(rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["DTCLAVE1"].Text + "' AND ARCLAVE2 ='" + ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_clave2") as RadTextBox).Text + "' AND ARCLAVE3='.' AND ARCLAVE4='.'";
                                foreach (DataRow rw in Obj.GetArticulos(null, lc_cadena, 0, 0).Rows)
                                    lc_impf = Convert.ToString(rw["ARCDIMPF"]);
                                
                                double ln_precio = Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_precio") as RadNumericTextBox).Value);
                                double ln_cantidad = Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value);
                                double ln_subtot = ln_precio * ln_cantidad;
                                //double ln_imp = ComunBL.GetValorN(null, Convert.ToString(Session["CODEMP"]), "IMPF", lc_impf);
                                double ln_imp = 0;
                                    if (Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value)!=0)
                                        ln_imp = (Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_iva") as RadNumericTextBox).Value) / Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value))*100;
                                double ln_iva = 0;
                                if (ln_imp != 0)
                                 ln_iva = (ln_subtot * ln_imp)/100;
                                if (ConfigurationManager.AppSettings["ROUND"] == "S")
                                {
                                    ln_vlrtot += ln_subtot + ln_iva;
                                    ln_subtotal += ln_subtot;
                                    ln_impuesto += ln_iva;
                                }
                                else
                                {
                                    ln_vlrtot += Math.Round(ln_subtot + ln_iva);
                                    ln_subtotal += Math.Round(ln_subtot);
                                    ln_impuesto += Math.Round(ln_iva);
                                }
                            }
                            else
                            {
                                tbPagos.Rows.Clear();
                                tbPagos.AcceptChanges();
                                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                                (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();

                                ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value = 0;
                                litTextoMensaje.Text = "¡Cantidad a Devolver Supera Cantidad Facturada!";
                            //mpMensajes.Show();
                                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }

                        }
                    }
                if (lb_ind)
                    {
                        tbPagos.Rows.Clear();
                        tbPagos.AcceptChanges();

                        row["PGCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["PGTIPFAC"] = "";
                        row["PGNROFAC"] = 0;
                        row["PGNROITM"] = tbPagos.Rows.Count + 1;
                        row["PGTIPPAG"] = "05";
                        row["PGDETTPG"] = ".";
                        row["PGVLRPAG"] = ln_vlrtot;
                        row["PGSOPORT"] = ".";
                        row["PGSOPFEC"] = System.DateTime.Today;
                        row["PGPAGIMP"] = "";
                        row["PGESTADO"] = "AC";
                        row["PGCAUSAE"] = ".";
                        row["PGNMUSER"] = Convert.ToString(Session["UserLogon"]);
                        row["PGFECING"] = System.DateTime.Today;
                        row["PGFECMOD"] = System.DateTime.Today;
                        row["PGNROCAJA"] = "";
                        row["DETALLE"] = "";
                        row["PAGO"] = "NOTA CREDITO";

                        tbPagos.Rows.Add(row);
                        (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                        (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();

                        (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                        (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                        (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_vlrtot;
                
                        
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
        protected void lnk_devolucion_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturaDirecta.aspx?Documento=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            double ln_vlrtot = 0, ln_subtotal = 0, ln_impuesto = 0;
            Boolean lb_ind = false;
            string lc_impf = "";
            DataRow row = tbPagos.NewRow();
            ArticulosBL Obj = new ArticulosBL();
            try
            {
                for (int i = 0; i < (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Text) || ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value != 0)
                    {
                        if (((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value <= ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad_real") as RadNumericTextBox).Value)
                        {
                            lb_ind = true;
                            string lc_cadena = " ARCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND ARTIPPRO ='" + ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_tippro") as RadTextBox).Text + "' AND ARCLAVE1='" + ((GridDataItem)(rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["DTCLAVE1"].Text + "' AND ARCLAVE2 ='" + ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_clave2") as RadTextBox).Text + "' AND ARCLAVE3='.' AND ARCLAVE4='.'";
                            foreach (DataRow rw in Obj.GetArticulos(null, lc_cadena, 0, 0).Rows)
                                lc_impf = Convert.ToString(rw["ARCDIMPF"]);

                            double ln_precio = Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_precio") as RadNumericTextBox).Value);
                            double ln_cantidad = Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value);
                            double ln_subtot = ln_precio * ln_cantidad;
                            //double ln_imp = ComunBL.GetValorN(null, Convert.ToString(Session["CODEMP"]), "IMPF", lc_impf);
                            double ln_imp = 0;
                            if (Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value) != 0)
                                ln_imp = (Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_iva") as RadNumericTextBox).Value) / Convert.ToDouble(((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_subtotal") as RadNumericTextBox).Value)) * 100;
                            double ln_iva = 0;
                            if (ln_imp != 0)
                                ln_iva = (ln_subtot * ln_imp) / 100;
                            if (ConfigurationManager.AppSettings["ROUND"] == "S")
                            {
                                ln_vlrtot += ln_subtot + ln_iva;
                                ln_subtotal += ln_subtot;
                                ln_impuesto += ln_iva;
                            }
                            else
                            {
                                ln_vlrtot += Math.Round(ln_subtot + ln_iva);
                                ln_subtotal += Math.Round(ln_subtot);
                                ln_impuesto += Math.Round(ln_iva);
                            }
                        }
                        else
                        {
                            //tbPagos.Rows.Clear();
                            //tbPagos.AcceptChanges();
                            //(rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                            //(rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();

                            ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value = ((rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items[i].FindControl("edt_cantidad") as RadNumericTextBox).Value - 1;
                            litTextoMensaje.Text = "¡Cantidad a Devolver Supera Cantidad Facturada!";
                            //mpMensajes.Show();
                            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }

                    }
                }
                if (lb_ind)
                {
                    tbPagos.Rows.Clear();
                    tbPagos.AcceptChanges();

                    row["PGCODEMP"] = Convert.ToString(Session["CODEMP"]);
                    row["PGTIPFAC"] = "";
                    row["PGNROFAC"] = 0;
                    row["PGNROITM"] = tbPagos.Rows.Count + 1;
                    row["PGTIPPAG"] = "05";
                    row["PGDETTPG"] = ".";
                    row["PGVLRPAG"] = ln_vlrtot;
                    row["PGSOPORT"] = ".";
                    row["PGSOPFEC"] = System.DateTime.Today;
                    row["PGPAGIMP"] = "";
                    row["PGESTADO"] = "AC";
                    row["PGCAUSAE"] = ".";
                    row["PGNMUSER"] = Convert.ToString(Session["UserLogon"]);
                    row["PGFECING"] = System.DateTime.Today;
                    row["PGFECMOD"] = System.DateTime.Today;
                    row["PGNROCAJA"] = "";
                    row["DETALLE"] = "";
                    row["PAGO"] = "NOTA CREDITO";

                    tbPagos.Rows.Add(row);
                    (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataSource = tbPagos;
                    (rlv_factura.InsertItem.FindControl("rg_pagos") as RadGrid).DataBind();

                    (rlv_factura.InsertItem.FindControl("txt_subtotal") as RadNumericTextBox).DbValue = ln_subtotal;
                    (rlv_factura.InsertItem.FindControl("txt_impuesto") as RadNumericTextBox).DbValue = ln_impuesto;
                    (rlv_factura.InsertItem.FindControl("txt_total") as RadNumericTextBox).DbValue = ln_vlrtot;


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
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "new")
            {
                txt_barras.Text = "";
                txt_barras.Focus();
                string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }

            if (e.CommandName == "mpResume")
            {
                DataTable tbDev = new DataTable();
                
                tbDev.Columns.Add("C1", typeof(string));
                tbDev.Columns.Add("NOMBRE", typeof(string));
                tbDev.Columns.Add("CANT", typeof(int));

                foreach (GridDataItem item in (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items)
                {
                    if (Convert.ToInt32((item.FindControl("edt_cantidad") as RadNumericTextBox).Value) != 0)
                    {
                        DataRow rw = tbDev.NewRow();
                        rw["C1"] = Convert.ToString(item["DTCLAVE1"].Text);
                        rw["NOMBRE"] = Convert.ToString(item["ARNOMBRE"].Text);
                        rw["CANT"] = Convert.ToInt32((item.FindControl("edt_cantidad") as RadNumericTextBox).Value);
                        tbDev.Rows.Add(rw);
                    }
                }

                rg_resumebox.DataSource = tbDev;
                rg_resumebox.DataBind();

                string script = "function f(){$find(\"" + mpResume.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }
        protected void txt_barras_TextChanged(object sender, EventArgs e)
        {
            Boolean lb_bandera = true, lb_banexis = true;
            ArticulosBL Obj = new ArticulosBL();
            try
            {
                if (!string.IsNullOrEmpty(txt_barras.Text))
                {
                    foreach (DataRow rx in Obj.GetTbBarrasNoInv(null, txt_barras.Text, null).Rows)
                    {
                        lb_bandera = false;
                        foreach (GridDataItem item in (rlv_factura.InsertItem.FindControl("rg_items") as RadGrid).Items)
                        {
                            if ((item.FindControl("edt_tippro") as RadTextBox).Text == Convert.ToString(rx["BTIPPRO"]) && Convert.ToString(item["DTCLAVE1"].Text) == Convert.ToString(rx["BCLAVE1"]) && (item.FindControl("edt_clave2") as RadTextBox).Text == Convert.ToString(rx["BCLAVE2"]))
                            {
                                lb_banexis = false;
                                (item.FindControl("edt_cantidad") as RadNumericTextBox).Value += 1;
                                //this.edt_cantidad_OnTextChanged((item.FindControl("edt_cantidad") as RadNumericTextBox), null);
                            }
                        }
                    }

                    if (lb_bandera)
                    {
                        litTextoMensaje.Text = "¡Codigo Barras Invalido!";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        if (lb_banexis)
                        {
                            litTextoMensaje.Text = "¡Codigo No Existe en el Documento!";
                            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            btn_agregar_Click(sender, e);
                            txt_barras.Text = "";
                            txt_barras.Focus();
                            string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
    }
}