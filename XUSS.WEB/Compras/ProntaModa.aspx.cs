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
using XUSS.BLL.Inventarios;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Compras
{
    public partial class ProntaModa : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbItemsInv
        {
            set { ViewState["tbItemsInv"] = value; }
            get { return ViewState["tbItemsInv"] as DataTable; }
        }
        private void ConfigLinea()
        {
            TipoProductosBL Obj = new TipoProductosBL();
            try
            {
                rc_lote.Visible = false;
                lbl_lote.Visible = false;
                using (IDataReader reader = Obj.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), null, txt_tp.Text))
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
                foreach (DataRow rw in Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]), null, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;                
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
            this.OcultarPaginador(rlv_prontamoda, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_prontamoda_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;                    
                    break;

                case "Buscar":
                    obj_prontamoda.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_prontamoda.DataBind();
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
            
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_prontamoda.SelectParameters["filter"].DefaultValue = filtro;
            rlv_prontamoda.DataBind();
            if ((rlv_prontamoda.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" class=\"box\">");
                str.AppendLine("<div class=\"messages\">");
                str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
                str.AppendLine("    <div class=\"image\">");
                str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
                str.AppendLine("		</div>");
                str.AppendLine("    <div class=\"text\">");
                str.AppendLine("        <h6>Información</h6>");
                str.AppendLine("        <span>No se encontraron registros</span>");
                str.AppendLine("    </div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                (rlv_prontamoda.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_prontamoda_OnItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;
                    return;
                }
                else {
                    ViewState["toolbars"] = true;
                    ProntaModaBL Obj = new ProntaModaBL();
                    try
                    {
                        //tbItems = Obj.CargarMovimiento(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_traslados.Items[0].GetDataKeyValue("TSMOVSAL").ToString()));
                        tbItems = Obj.GetProntaModaDT(null, Convert.ToInt32((rlv_prontamoda.Items[0].FindControl("txt_conse") as RadTextBox).Text));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        tbItemsInv = Obj.GetProntaModaInv(null, Convert.ToInt32((rlv_prontamoda.Items[0].FindControl("txt_conse") as RadTextBox).Text));
                        (e.Item.FindControl("rg_detalle_inv") as RadGrid).DataSource = tbItemsInv;
                        (e.Item.FindControl("rg_detalle_inv") as RadGrid).DataBind();
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
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            string script = "function f(){$find(\"" + mpTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                    if (rlv_prontamoda.InsertItem == null)
                    {
                        (rlv_prontamoda.Items[0].FindControl("txt_nit") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                        (rlv_prontamoda.Items[0].FindControl("txt_tercero") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                        (rlv_prontamoda.Items[0].FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);                        
                    }
                    else
                    {
                        (rlv_prontamoda.InsertItem.FindControl("txt_nit") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                        (rlv_prontamoda.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                        (rlv_prontamoda.InsertItem.FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);                                                
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
                    string script = "function f(){$find(\"" + mpTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
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
            string script = "function f(){$find(\"" + mpTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_detalle_inv_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItemsInv;
        }
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }
        protected void rg_detalle_inv_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                if (rlv_prontamoda.InsertItem == null)
                {
                    (rlv_prontamoda.Items[0].FindControl("rqf_categoria") as RequiredFieldValidator).Validate();

                    if ((rlv_prontamoda.Items[0].FindControl("rqf_categoria") as RequiredFieldValidator).IsValid)
                    {
                        Boolean lb_c2 = false, lb_c3 = false;
                        ComunBL ObjC = new ComunBL();
                        RadComboBoxItem item = new RadComboBoxItem();
                        ArticulosBL ObjA = new ArticulosBL();
                        try
                        {
                            using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), (rlv_prontamoda.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue))
                            {
                                while (reader.Read())
                                {
                                    rc_alterna2.Visible = false;
                                    txt_alterna2.Visible = true;
                                    if (Convert.ToString(reader["TACTLSE2"]) == "S")
                                    {
                                        lb_c2 = true;
                                        lbl_c2.Text = Convert.ToString(reader["TADSCLA2"]);
                                        rc_alterna2.Visible = true;
                                        txt_alterna2.Visible = false;
                                    }
                                    if (Convert.ToString(reader["TACTLSE3"]) == "S")
                                    {
                                        lb_c3 = true;
                                        lbl_c3.Text = Convert.ToString(reader["TADSCLA3"]);
                                        rc_alterna3.Visible = true;
                                        txt_alterna3.Visible = false;
                                    }
                                }
                            }


                            if (lb_c2)
                            {
                                item.Value = "";
                                item.Text = "Seleccionar";
                                rc_alterna2.Items.Add(item);
                                //using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, 2))
                                using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (rlv_prontamoda.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, 2))
                                {
                                    while (reader.Read())
                                    {
                                        RadComboBoxItem itemi = new RadComboBoxItem();
                                        itemi.Value = Convert.ToString(reader["ASCLAVEO"]);
                                        itemi.Text = Convert.ToString(reader["ASNOMBRE"]);
                                        rc_alterna2.Items.Add(itemi);
                                        itemi = null;
                                    }
                                }
                            }

                            if (lb_c3)
                            {
                                item.Value = "";
                                item.Text = "Seleccionar";
                                rc_alterna3.Items.Add(item);
                                //using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, 2))
                                using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (rlv_prontamoda.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, 3))
                                {
                                    while (reader.Read())
                                    {
                                        RadComboBoxItem itemi = new RadComboBoxItem();
                                        itemi.Value = Convert.ToString(reader["ASCLAVEO"]);
                                        itemi.Text = Convert.ToString(reader["ASNOMBRE"]);
                                        rc_alterna3.Items.Add(itemi);
                                        itemi = null;
                                    }
                                }
                            }

                            (rlv_prontamoda.Items[0].FindControl("rc_categoria") as RadComboBox).Enabled = false;

                            string script = "function f(){$find(\"" + mpInventario.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            e.Canceled = true;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            ObjC = null;
                            ObjA = null;
                        }
                    }
                }
            }
        }
        protected void btn_agregarInv_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbItemsInv.NewRow();
            try {
                row["ICCONSE"] = 0;
                row["IC_CONSE"] = 0;
                row["IC_CODEMP"] = ".";
                row["TANOMBRE"] = ".";
                row["ARTIPPRO"] = (rlv_prontamoda.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue;
                row["ARCLAVE1"] = "";
                row["IC_CANTIDAD"] = txt_cantidad.Value;
                row["ARCLAVE2"] = rc_alterna2.SelectedValue;
                row["ARCLAVE3"] = rc_alterna3.SelectedValue;
                row["ARCLAVE4"] = ".";
                row["CLAVE2"] = rc_alterna2.Text;
                row["CLAVE3"] = rc_alterna3.Text;
                row["IC_USUARIO"] = ".";
                row["IC_FECING"] = System.DateTime.Now;
                tbItemsInv.Rows.Add(row);
                (rlv_prontamoda.Items[0].FindControl("rg_detalle_inv") as RadGrid).DataSource = tbItemsInv;
                (rlv_prontamoda.Items[0].FindControl("rg_detalle_inv") as RadGrid).DataBind();
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
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {

            obj_articulos.SelectParameters["inBodega"].DefaultValue = "";            
            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            DataRow rw = tbItems.NewRow();
            try {
                rw["TANOMBRE"] = txt_tp.Text;
                rw["ARNOMBRE"] = txt_descripcion.Text;
                rw["ARPRECIO"] = txt_preciolta.Value;
                rw["ICCONSE"] = 0;
                rw["CC_CONSE"] = 0;
                rw["CC_CODEMP"] = "001";
                rw["ARTIPPRO"] = txt_tp.Text;
                rw["ARCLAVE1"] = txt_referencia.Text;
                rw["ARCLAVE2"] = txt_clave2.Text;
                rw["ARCLAVE3"] = txt_clave3.Text;
                rw["ARCLAVE4"] = ".";
                rw["CC_VALOR"] = txt_preciolta.Value;
                rw["CC_CANTIDAD"] = txt_cantidadcon.Value;
                rw["CC_USUARIO"] = "";
                rw["CC_FECING"] = System.DateTime.Today;

                tbItems.Rows.Add(rw);
                (rlv_prontamoda.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_prontamoda.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as Button).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as Button).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as Button).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as Button).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            obj_articulos.SelectParameters["inBodega"].DefaultValue = null;
            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;
            rgConsultaArticulos.DataBind();            
            string script_ = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script_, true);
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
                    //txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    txt_cantidad.Focus();

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

        protected void rlv_prontamoda_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_prontamoda_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {                
                string[] retorno = Convert.ToString(e.ReturnValue).Split('-');
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);                
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);

        }
    }
}