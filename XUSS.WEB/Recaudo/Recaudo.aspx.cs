using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data;
using XUSS.BLL.Recaudo;
using AjaxControlToolkit;
using XUSS.BLL.Terceros;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Recaudo
{
    public partial class Recaudo : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable;}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_recaudo.SelectParameters["filter"].DefaultValue = "  RH_NRORECIBO =" + Convert.ToString(Request.QueryString["Documento"]);
                    rlv_recaudo.DataBind();
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
            this.OcultarPaginador(rlv_recaudo, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_recaudo_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    
                    RecaudoBL Obj = new RecaudoBL();
                    try
                    {
                        tbItems = Obj.GetRecaudoDT(null, null,0);
                        //(rlv_recaudo.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        //(rlv_recaudo.FindControl("rg_items") as RadGrid).DataBind();
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
                    obj_recaudo.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_recaudo.DataBind();
                    break;
                case "Delete":
                    RecaudoBL Obj_ = new RecaudoBL();
                    try {
                        Obj_.AnularRecaudo(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((RadNumericTextBox)(rlv_recaudo.Items[0].FindControl("txt_recibo"))).Value));
                        litTextoMensaje.Text = "Recibo " + Convert.ToString(((RadNumericTextBox)(rlv_recaudo.Items[0].FindControl("txt_recibo"))).Value) + " Anulado!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_recaudo_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    RecaudoBL Obj = new RecaudoBL();
                    try {
                        tbItems = Obj.GetRecaudoDT(null, Convert.ToString(Session["CODEMP"]) ,Convert.ToInt32(rlv_recaudo.Items[0].GetDataKeyValue("RH_NRORECIBO").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    catch(Exception ex)
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
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_recibo") as RadNumericTextBox).Text))
                filtro += "AND RH_NRORECIBO =" + (((RadButton)sender).Parent.FindControl("txt_recibo") as RadNumericTextBox).Text ;

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND (TRNOMBRE +' '+ISNULL(TRNOMBR2,' ')+' '+ISNULL(TRAPELLI,' ')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";
            
            //if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text))
            //    filtro += "AND TRNOMBR2 LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadNumericTextBox).Text))
                filtro += "AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadNumericTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codcliente") as RadNumericTextBox).Text))
                filtro += "AND TRCODTER = " + (((RadButton)sender).Parent.FindControl("txt_codcliente") as RadNumericTextBox).Text + "";


            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_recaudo.SelectParameters["filter"].DefaultValue = filtro;
            rlv_recaudo.DataBind();
            if ((rlv_recaudo.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_recaudo.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void txt_nrofactura_TextChanged(object sender, EventArgs e)
        {
            RecaudoBL Obj = new RecaudoBL();
            try {
                if ((!string.IsNullOrWhiteSpace((sender as RadTextBox).Text)) && ((((RadTextBox)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue != "-1"))
                {
                    if (!Obj.ExisteFactura(null,(((RadTextBox)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((sender as RadTextBox).Text)))
                    {
                        (sender as RadTextBox).Text = "";
                        (sender as RadTextBox).Focus();
                        (((RadTextBox)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text = "";
                        litTextoMensaje.Text = "Tipo Factura-Numero Factura Invalido!";
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else{
                    using (IDataReader reader = Obj.GetDatosFactura(null, (((RadTextBox)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue, Convert.ToInt32((sender as RadTextBox).Text)))
                    {
                        while (reader.Read())
                        {                            
                            (((RadTextBox)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(reader["CLIENTE"]);
                            (((RadTextBox)sender).Parent.FindControl("txt_tfactura") as RadNumericTextBox).Value = (Convert.ToDouble(reader["HDTOTFAC"]) + Convert.ToDouble(reader["RECA_SF"])) - (Convert.ToDouble(reader["RC"]) + Convert.ToDouble(reader["TDEV"]));
                            if (((Convert.ToDouble(reader["HDTOTFAC"]) + Convert.ToDouble(reader["RECA_SF"])) - (Convert.ToDouble(reader["RC"]) + Convert.ToDouble(reader["TDEV"]))) == 0)
                            {
                                litTextoMensaje.Text = "La Cartera es 0, Validar!";
                                //mpMensajes.Show();
                                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            double ln_recaudo = 0;
            DataRow row = tbItems.NewRow();
            try {
                row["RC_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["RC_NRORECIBO"] = 0;
                row["RC_CONCEPTO"] = (((ImageButton)sender).Parent.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                row["RC_VALOR"] = (((ImageButton)sender).Parent.FindControl("txt_valor") as RadNumericTextBox).Value;
                row["RC_USUARIO"] = "admin";
                row["RC_ESTADO"] = "AC";
                row["RC_FECREC"] = System.DateTime.Now;
                row["RC_TIPFAC"] = (rlv_recaudo.InsertItem.FindControl("rc_tipfac") as RadComboBox).SelectedValue;                               
                row["RC_NROFAC"] = (rlv_recaudo.InsertItem.FindControl("txt_nrofactura") as RadTextBox).Text;
                row["RC_TIPFACSF"] = (((ImageButton)sender).Parent.FindControl("txt_tfasf") as RadTextBox).Text;
                
                row["RC_NROFACSF"] = 0;
                if ((((ImageButton)sender).Parent.FindControl("txt_nrofacsf") as RadTextBox).Visible)
                    row["RC_NROFACSF"] = (((ImageButton)sender).Parent.FindControl("txt_nrofacsf") as RadTextBox).Text;                
                //row["RC_FECING"] = System.DateTime.Now;
                //row["RC_FECMOD"] = System.DateTime.Now;

                tbItems.Rows.Add(row);
                (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;

                foreach (DataRow rw in tbItems.Rows)
                {
                    ln_recaudo += Convert.ToDouble(rw["RC_VALOR"]);
                }
                (rlv_recaudo.InsertItem.FindControl("txt_trecaudo") as RadNumericTextBox).Value = ln_recaudo;
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
        protected void obj_recaudo_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inTbItems"] = tbItems;
        }
        protected void obj_recaudo_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            else
            {
                litTextoMensaje.Text = "Nro Recibo :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=3003&inban=S&inParametro=inConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=3003&inban=S&inParametro=inConsecutivo&inValor=" + rlv_recaudo.Items[0].GetDataKeyValue("RH_NRORECIBO").ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void iBtnFindSaldoFavor_OnClick(object sender, EventArgs e)
        {
            obj_saldofavor.SelectParameters["inTercero"].DefaultValue = (rlv_recaudo.InsertItem.FindControl("txt_codcliente") as RadTextBox).Text;
            rgSaldosFavor.DataBind();
            string script = "function f(){$find(\"" + modalSaldos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgSaldosFavor_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                //(rlv_liquidacion.Items[0].FindControl("edt_cie") as RadTextBox).Text = Convert.ToString(item["CIE_CODIGO"].Text);
                //(rlv_liquidacion.Items[0].FindControl("edt_diagnostico") as RadTextBox).Text = Convert.ToString(item["CIE_NOMBRE"].Text);
                ((source as RadGrid).Parent.FindControl("txt_valor") as RadNumericTextBox).Value = Math.Abs(Convert.ToDouble(item["SF"].Text));
                ((source as RadGrid).Parent.FindControl("txt_tfasf") as RadTextBox).Text = Convert.ToString(item["HDTIPFAC"].Text);
                ((source as RadGrid).Parent.FindControl("txt_nrofacsf") as RadTextBox).Text = Convert.ToString(item["HDNROFAC"].Text);
                item = null;
            }            
        }
        protected void rc_concepto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ((sender as RadComboBox).Parent.FindControl("iBtnFindSaldoFavor") as ImageButton).Visible = false;
            ((sender as RadComboBox).Parent.FindControl("txt_nrofacsf") as RadTextBox).Visible = false;
            ((sender as RadComboBox).Parent.FindControl("txt_tfasf") as RadTextBox).Visible = false;
            ((sender as RadComboBox).Parent.FindControl("lbl_tfsf") as Control).Visible = false;
            ((sender as RadComboBox).Parent.FindControl("lbl_nrofacsf") as Control).Visible = false;
            ((sender as RadComboBox).Parent.FindControl("txt_nrofacsf") as RadTextBox).Text = "";
            ((sender as RadComboBox).Parent.FindControl("txt_tfasf") as RadTextBox).Text = "";
            if ((sender as RadComboBox).SelectedValue == "4")
            {
                ((sender as RadComboBox).Parent.FindControl("iBtnFindSaldoFavor") as ImageButton).Visible = true;
                ((sender as RadComboBox).Parent.FindControl("txt_nrofacsf") as RadTextBox).Visible = true;
                ((sender as RadComboBox).Parent.FindControl("txt_tfasf") as RadTextBox).Visible = true;
                ((sender as RadComboBox).Parent.FindControl("lbl_tfsf") as Control).Visible = true;
                ((sender as RadComboBox).Parent.FindControl("lbl_nrofacsf") as Control).Visible = true;
            }
            
        }
        protected void rlv_recaudo_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            if (tbItems.Rows.Count == 0)
            {
                litTextoMensaje.Text = "No Se Ingresaron Items!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }
        protected void rg_items_OnItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string url = "";

            try
            {
                switch (e.CommandName)
                {
                    case "factura":
                        GridDataItem item = (GridDataItem)e.Item;
                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturacionPedido.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        item = null;
                        break;
                    case "nc-nd":
                        GridDataItem item_ = (GridDataItem)e.Item;
                        if ((item_.FindControl("lbl_concepto") as Label).Text == "-3")                        
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Notas.aspx?Documento=" + (item_.FindControl("lbl_nota") as Label).Text;
                        else
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/NotasDebito.aspx?Documento=" + (item_.FindControl("lbl_nota") as Label).Text;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        item_ = null;
                        break;
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
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            txt_codter.Text = "";
            //mpTerceros.Show();
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

            edt_nomtercero.Focus();
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); $find(\"" + edt_nomtercero.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaTerceros_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int ln_conse = 1;
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();    
                RecaudoBL ObjR = new RecaudoBL();
                DataTable tb = new DataTable();
                try
                {                                                         
                    (rlv_recaudo.InsertItem.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(item["TRNOMBRE"].Text);
                    (rlv_recaudo.InsertItem.FindControl("txt_codcliente") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                    (rlv_recaudo.InsertItem.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                    //Cargar el Detalle
                    tb = ObjR.GetDetalle(null, Convert.ToString(item["TRCODNIT"].Text));                    

                    foreach (DataRow rx in tb.Rows)
                    {
                        DataRow rw = tbItems.NewRow();
                        rw["DOCUMENTO"] = rx["NRO_FAC"];
                        rw["CLIENTE"] = "";
                        rw["HDTIPFAC"] = rx["HDTIPFAC"];
                        rw["HDNROFAC"] = rx["HDNROFAC"];
                        rw["HDFECFAC"] = rx["HDFECFAC"];
                        rw["RC_CODIGO"] = ln_conse;
                        rw["RC_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rw["RC_NRORECIBO"] = 0;
                        rw["RC_TIPFAC"] = rx["HDTIPFAC"];
                        rw["RC_NROFAC"] = rx["HDNROFAC"];
                        rw["RC_CONCEPTO"] = rx["TTCODCLA"];
                        rw["CONCEPTO"] = rx["TTDESCRI"];
                        rw["RC_VALOR"] = 0;
                        rw["RECAUDO"] = rx["RECAUDO"];
                        rw["HDTOTFAC"] = rx["HDTOTFAC"];
                        rw["NH_NRONOTA"] = "";

                        if (Convert.ToString(rx["NH_NRONOTA"]) != "")
                        {
                            rw["RC_VALOR"] = Convert.ToDouble(rx["VLR_NOTA"]);
                            rw["NH_NRONOTA"] = Convert.ToString(rx["NH_NRONOTA"]);
                        }

                        rw["RC_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                        rw["RC_ESTADO"] = "AC";
                        rw["RC_FECING"] = System.DateTime.Today;
                        rw["RC_FECMOD"] = System.DateTime.Today;
                        rw["RC_FECREC"] = System.DateTime.Today;
                        rw["SALDO"] = rx["SALDO"];
                        rw["RC_NROFACSF"] = 0;
                        tbItems.Rows.Add(rw);
                        rw = null;
                        ln_conse++;
                    }
                    (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {                    
                    item = null;
                    obj = null;
                    tb = null;
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
        protected void txt_trecibo_TextChanged(object sender, EventArgs e)
        {
            double ln_acumulador = 0;
            try {
                if ((sender as RadNumericTextBox).Value != 0)
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToString(rw["RC_CONCEPTO"]) == "1")
                        {
                            if ((ln_acumulador + Convert.ToDouble(rw["SALDO"])) <= (rlv_recaudo.InsertItem.FindControl("txt_trecibo") as RadNumericTextBox).Value)
                            {
                                rw["RC_VALOR"] = rw["SALDO"];
                                ln_acumulador += Convert.ToDouble(rw["SALDO"]);
                            }
                            else
                            {
                                rw["RC_VALOR"] = (rlv_recaudo.InsertItem.FindControl("txt_trecibo") as RadNumericTextBox).Value - ln_acumulador;
                                break;
                            }
                        }
                    }
                }
                
                (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        protected void txt_valor_TextChanged(object sender, EventArgs e)
        {
            try {
                int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text);
                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToInt32(rw["RC_CODIGO"]) == ln_codigo)
                    {
                        rw["RC_VALOR"] = Convert.ToDouble((sender as RadNumericTextBox).Value);        
                    }
                }
                (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        protected void rlv_recaudo_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        double total=0,vlr_factura=0,vlr_recaudo=0;
        string lc_factura = "";
        Boolean lb_ind = true;
        protected void rg_items_ItemDataBound(object sender, GridItemEventArgs e)
        {            
            if (e.Item is GridDataItem)
            {
                lb_ind = true;

                GridDataItem dataItem = (GridDataItem)e.Item;                
                if (lc_factura != (dataItem.FindControl("lbl_doc") as Label).Text)                                    
                {
                    lc_factura = (dataItem.FindControl("lbl_doc") as Label).Text;
                    total = 0;
                    vlr_factura = 0;
                }



                if ((dataItem.FindControl("lbl_concepto") as Label).Text == "1")
                {
                    vlr_factura += Convert.ToDouble((dataItem.FindControl("lbl_factura") as Label).Text);
                    vlr_recaudo = Convert.ToDouble((dataItem.FindControl("lbl_recaudo") as Label).Text);
                }

                //NC
                if ((dataItem.FindControl("lbl_concepto") as Label).Text == "-4")                
                    vlr_factura += Convert.ToDouble((dataItem.FindControl("txt_valor") as RadNumericTextBox).Value);
                
                if ((dataItem.FindControl("lbl_concepto") as Label).Text == "-3")
                    vlr_factura += Convert.ToDouble((dataItem.FindControl("txt_valor") as RadNumericTextBox).Value) * -1;

                if ((dataItem.FindControl("lbl_concepto") as Label).Text != "-4" && (dataItem.FindControl("lbl_concepto") as Label).Text != "-3")
                    total += Convert.ToDouble((dataItem.FindControl("txt_valor") as RadNumericTextBox).Value);

                //vlr_factura += Convert.ToDouble((dataItem.FindControl("txt_valor") as RadNumericTextBox).Value);

                if ((dataItem.FindControl("lbl_concepto") as Label).Text != "4")
                {
                    (dataItem.FindControl("iBtnFindSaldoFavor") as ImageButton).Enabled = false;
                    (dataItem.FindControl("iBtnFindSaldoFavor") as ImageButton).Visible = false;
                    if ((dataItem.FindControl("lbl_concepto") as Label).Text == "-2" || (dataItem.FindControl("lbl_concepto") as Label).Text == "-3" || (dataItem.FindControl("lbl_concepto") as Label).Text == "-4")
                    {
                        (dataItem.FindControl("txt_valor") as RadNumericTextBox).Enabled = false;
                        (dataItem.FindControl("iBtnFindSaldoFavor") as ImageButton).Enabled = false;
                        (dataItem.FindControl("iBtnFindSaldoFavor") as ImageButton).Visible = false;
                    }
                }
                
            }
            if (e.Item is GridGroupFooterItem)
            {
                GridGroupFooterItem ridGroupFooterItem = (GridGroupFooterItem)e.Item;
                GridItem[] groups = (rlv_recaudo.InsertItem.FindControl("rg_items") as RadGrid).MasterTableView.GetItems(GridItemType.GroupHeader);

                foreach (GridGroupHeaderItem group in groups)
                {
                        ridGroupFooterItem["NH_NRONOTA"].Controls.Clear();
                        ridGroupFooterItem["RC_VALOR"].Controls.Clear();
                }

                if (lb_ind)
                {
                    foreach (GridGroupHeaderItem group in groups)
                    {
                        if (ridGroupFooterItem.GroupIndex != group.GroupIndex)
                        {
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Clear();
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Add(new LiteralControl("<b>      Valor T. </b><br/>"));
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Add(new LiteralControl("<b>    Recaudo A. </b><br/>"));
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Add(new LiteralControl("<b>  Saldo Actual </b><br/>"));
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Add(new LiteralControl("<b>Pago x Aplicar </b><br/>"));
                            ridGroupFooterItem["NH_NRONOTA"].Controls.Add(new LiteralControl("<b>         Total </b><br/>"));
                            ridGroupFooterItem["RC_VALOR"].Controls.Clear();
                            ridGroupFooterItem["RC_VALOR"].Controls.Add(new LiteralControl("<b> <font color='blue'>" + vlr_factura.ToString("###,###.##") + "</font></b><br/>"));
                            ridGroupFooterItem["RC_VALOR"].Controls.Add(new LiteralControl("<b> <font color = 'orange' > " + vlr_recaudo.ToString("###,###.##") + "</font></b><br/>"));
                            ridGroupFooterItem["RC_VALOR"].Controls.Add(new LiteralControl("<b> <font color='green'>" + (vlr_factura - vlr_recaudo).ToString("###,###.##") + "</font></b><br/>"));
                            ridGroupFooterItem["RC_VALOR"].Controls.Add(new LiteralControl("<b> <font color = 'red' > " + total.ToString("###,###.##") + "</font></b><br/>"));
                            ridGroupFooterItem["RC_VALOR"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + ((vlr_factura - vlr_recaudo) - total).ToString("###,###.##") + "</font></b><br/>"));
                            break;
                        }

                    }
                }
                lb_ind = false;                
            }
        }
    }
}