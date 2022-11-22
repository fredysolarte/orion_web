using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using XUSS.UserControls;
using System.Data;
using XUSS.BLL.Contabilidad;
using XUSS.BLL.Terceros;
using XUSS.BLL.Parametros;
using XUSS.BLL.Comun;
using System.IO;

namespace XUSS.WEB.Contabilidad
{
    public partial class CausacionProveedores : System.Web.UI.Page
    {
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private DataTable tbDetalle
        {
            set { ViewState["tbDetalle"] = value; }
            get { return ViewState["tbDetalle"] as DataTable; }
        }
        private DataTable tbAnexos
        {
            set { ViewState["tbAnexos"] = value; }
            get { return ViewState["tbAnexos"] as DataTable; }
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
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;
            }
            else
            {
                ViewState["toolbars"] = false;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_movhd.SelectParameters["filter"].DefaultValue = "MVTH_CODIGO =" + Convert.ToString(Request.QueryString["Documento"]);
                    rlv_causasion.DataBind();
                }
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_causasion, "RadDataPager1", "BotonesBarra");
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("edt_ninterno") as RadTextBox).Text))
                filtro += " AND MVTH_NUMERO =" + (((RadButton)sender).Parent.FindControl("edt_ninterno") as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("edt_numero") as RadTextBox).Text))
                filtro += " AND MVTH_DOCCON LIKE '%" + (((RadButton)sender).Parent.FindControl("edt_numero") as RadTextBox).Text +"%'";

            if ((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue != "")
                filtro += " AND TFTIPFAC ='" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_ano") as RadComboBox).SelectedValue != "")
                filtro += " AND MVTH_ANO ='" + (((RadButton)sender).Parent.FindControl("rc_ano") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_mes") as RadComboBox).SelectedValue != "")
                filtro += " AND MVTH_MES ='" + (((RadButton)sender).Parent.FindControl("rc_mes") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            obj_movhd.SelectParameters["filter"].DefaultValue = filtro;
            rlv_causasion.DataBind();
            if ((rlv_causasion.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_causasion.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_causasion_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    CausacionProveedoresBL obj = new CausacionProveedoresBL();
                    try
                    {
                        tbDetalle = obj.GetMovimientosDT(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbAnexos = obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), 0);
                        //(e.ListViewItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                        //(e.ListViewItem.FindControl("rgDetalle") as RadGrid).DataBind();
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
                    obj_movhd.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_causasion.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }        
        protected void rlv_causasion_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    CausacionProveedoresBL Obj = new CausacionProveedoresBL();
                    try
                    {
                        tbDetalle = Obj.GetMovimientosDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text));                        
                        (e.Item.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                        (e.Item.FindControl("rgDetalle") as RadGrid).DataBind();

                        tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text));
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataBind();
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
        protected void rgDetalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string script = "";
            switch (e.CommandName)
            {
                case "Filter":
                    if ((tbDetalle != null) && (tbDetalle.Rows.Count > 0))
                    {
                        if (rlv_causasion.InsertItem != null)
                        {
                            (rlv_causasion.InsertItem.FindControl("gr_detalle") as RadGrid).DataSource = tbDetalle;
                            (rlv_causasion.InsertItem.FindControl("gr_detalle") as RadGrid).DataBind();
                        }
                    }
                    break;
                case "new_concept":
                    script = "function f(){$find(\"" + mpConcepto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "new":
                    script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
            }

        }
        protected void rc_tDocumento_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int dias = DateTime.DaysInMonth(Convert.ToInt32(System.DateTime.Now.Year.ToString()), Convert.ToInt32(System.DateTime.Now.Month.ToString()));

            (((RadComboBox)sender).Parent.FindControl("rc_ano") as RadComboBox).SelectedValue = System.DateTime.Now.Year.ToString();
            (((RadComboBox)sender).Parent.FindControl("rc_mes") as RadComboBox).SelectedValue = System.DateTime.Now.Month.ToString();
            while (dias > 0)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = Convert.ToString(dias);
                item.Value = Convert.ToString(dias);
                (((RadComboBox)sender).Parent.FindControl("rc_dia") as RadComboBox).Items.Add(item);
                dias--;
            }

            (((RadComboBox)sender).Parent.FindControl("rc_dia") as RadComboBox).SelectedValue = System.DateTime.Now.Day.ToString();
            (((RadComboBox)sender).Parent.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
            (((RadComboBox)sender).Parent.FindControl("rgDetalle") as RadGrid).DataBind();
        }
        protected void rgDetalle_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (rlv_causasion.InsertItem != null)
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
            else
                (source as RadGrid).DataSource = tbDetalle;
        }
        protected void OnClick_btn_aceptar(object sender, EventArgs e)
        {
            //DataRow row = tbDetalle.NewRow();
            //row["MVTD_CODEMP"] = Session["CODEMP"];
            //row["MVTD_CONSE"] = 1;
            //row["MVTD_NITEM"] = 1;
            //row["MVTD_CUENTA"] = (((Button)sender).FindControl("ac_cuenta") as RadAutoCompleteBox).Text.Substring(0, (((Button)sender).FindControl("ac_cuenta") as RadAutoCompleteBox).Text.Length - 2);
            //row["MVTD_CODTER"] = 1;
            //if ((((Button)sender).Parent.FindControl("edt_credito") as RadNumericTextBox).Value == null)
            //{
            //    row["MVTD_CREDITO"] = 0;
            //    if (((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value != null)
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value = ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value + 0;
            //    else
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value = 0;
            //}
            //else
            //{
            //    row["MVTD_CREDITO"] = (((Button)sender).Parent.FindControl("edt_credito") as RadNumericTextBox).Value;
            //    if (((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value != null)
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value = ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value + (((Button)sender).Parent.FindControl("edt_credito") as RadNumericTextBox).Value;
            //    else
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piecredito")).Value = (((Button)sender).Parent.FindControl("edt_credito") as RadNumericTextBox).Value;
            //}

            //if ((((Button)sender).Parent.FindControl("edt_debito") as RadNumericTextBox).Value == null)
            //{
            //    row["MVTD_DEBITO"] = 0;
            //    if (((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value != null)
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value = ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value + 0;
            //    else
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value = 0;
            //}
            //else
            //{
            //    row["MVTD_DEBITO"] = (((Button)sender).Parent.FindControl("edt_debito") as RadNumericTextBox).Value;
            //    if (((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value != null)
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value = ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value + (((Button)sender).Parent.FindControl("edt_debito") as RadNumericTextBox).Value;
            //    else
            //        ((RadNumericTextBox)rlv_causasion.InsertItem.FindControl("edt_piedebito")).Value = (((Button)sender).Parent.FindControl("edt_debito") as RadNumericTextBox).Value;
            //}

            //row["MVTD_DESCRIPCION"] = (((Button)sender).Parent.FindControl("edt_descripcion") as RadTextBox).Text;
            //row["MVTD_FECING"] = System.DateTime.Now;
            //row["MVTD_FECMOD"] = System.DateTime.Now;
            //row["MVTD_CDUSER"] = "ADMIN";
            //row["MVTD_ESTADO"] = "AC";

            //tbDetalle.Rows.Add(row);
            //(rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;

        }        
        protected void im_buscar(object sender, EventArgs e)
        {
            //obj_terceros.SelectParameters["filter"].DefaultValue = " AND 1=0";
            //rgTerceros.DataBind();
            //mpFindTerceros.Show();
        }
        protected void obj_movhd_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["dtDetalle"] = tbDetalle;
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
                try
                {
                    txt_codcli.Text = Convert.ToString(item["TRCODTER"].Text);
                    txt_nit.Text = Convert.ToString(item["TRCODNIT"].Text);
                    txt_tercero.Text = (item.FindControl("lbl_nomter") as Label).Text;

                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_aceptar_concepto_Click(object sender, EventArgs e)
        {
            DataRow rw = tbDetalle.NewRow();
            try {
                rw["MVTH_CODIGO"] = 0;
                rw["MVTD_IDENT"] = tbDetalle.Rows.Count + 1;
                rw["MVTH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                rw["PC_ID"] = 0;
                rw["PC_CODIGO"] = ac_ctacontable.Entries[0].Value;
                rw["PC_NOMBRE"] = ac_ctacontable.Entries[0].Text;
                rw["MVTD_DESCRIPCION"] = txt_concepto.Text;
                rw["MVTD_CREDITO"] = txt_credito.Value;
                rw["MVTD_DEBITO"] = txt_debito.Value;
                rw["TRCODTER"] = 0;
                rw["MVTD_TIPDOC"] = ".";
                rw["MVTD_NRODOC"] = ".";
                rw["MVTD_FECDOC"] = System.DateTime.Today;

                rw["MVTD_FECING"] = System.DateTime.Today;
                rw["MVTD_FECMOD"] = System.DateTime.Today;
                rw["MVTD_CDUSER"] = ".";
                rw["MVTD_ESTADO"] = "AC";

                tbDetalle.Rows.Add(rw);
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataBind();

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
        protected void rgDetalle_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var codigo = item.GetDataKeyValue("MVTD_IDENT").ToString();
                    int pos = 0;
                    int xpos = 0;

                    foreach (DataColumn cl in tbDetalle.Columns)                    
                        cl.ReadOnly = false;
                    

                    foreach (DataRow row in tbDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["MVTD_IDENT"]) == Convert.ToInt32(codigo))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbDetalle.Rows[pos].Delete();
                    tbDetalle.AcceptChanges();
                    foreach (DataRow row in tbDetalle.Rows)
                    {
                        row["MVTD_IDENT"] = i;
                        i++;
                    }

                    break;
            }
        }
        double ln_creditos = 0, ln_debitos = 0;        
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string url = "", lc_reporte = "";

            TipoFacturaBL Obj = new TipoFacturaBL();
            try
            {
                foreach (DataRow rw in Obj.GetTiposFactura(null, "TFCODEMP ='" + Convert.ToString(Session["CODEMP"]) + "' AND TFTIPFAC='" + Convert.ToString((rlv_causasion.Items[0].FindControl("rc_tipfac") as RadComboBox).SelectedValue) + "'", 0, 0).Rows)
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
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=" + lc_reporte + "&inban=S&inParametro=InCodigo&inValor=" + (rlv_causasion.Items[0].FindControl("txt_codigo") as RadTextBox).Text +
                              "&inParametro=InCodEmp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inCopia&inValor=0";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                }

                FastReport.Utils.Config.WebMode = true;                
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
        protected void rgDetalle_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                ln_creditos += Convert.ToDouble((dataItem.FindControl("txt_mcredito") as RadNumericTextBox).Value);
                ln_debitos += Convert.ToDouble((dataItem.FindControl("txt_mdebito") as RadNumericTextBox).Value);
            }

            if (e.Item is GridFooterItem)
            {
                GridFooterItem ridGroupFooterItem = (GridFooterItem)e.Item;                
                ridGroupFooterItem["MVTD_CREDITO"].Controls.Add(new LiteralControl("<b> <font color='red'>" + ln_creditos.ToString("###,###.##") + "</font></b><br/>"));
                ridGroupFooterItem["MVTD_DEBITO"].Controls.Add(new LiteralControl("<b> <font color='green'>" + ln_debitos.ToString("###,###.##") + "</font></b><br/>"));
            }
        }
        protected void rauCargar_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_procesar_Click(object sender, EventArgs e)
        {
            CausacionProveedoresBL Obj = new CausacionProveedoresBL();
            try
            {
                if (rlv_causasion.InsertItem != null)
                {
                    tbAnexos.Columns["ruta"].ReadOnly = false;
                    DataRow rw = tbAnexos.NewRow();
                    rw["MVTH_CODIGO"] = 0;
                    rw["MVEV_CODEMP"] = "001";
                    rw["MVEV_ID"] = 0;
                    rw["MVEV_DESCRIPCION"] = txt_obsevidencia.Text;
                    rw["MVEV_FECING"] = System.DateTime.Today;
                    rw["MVEV_USUARIO"] = ".";                    
                    rw["ruta"] = prArchivo;
                    tbAnexos.Rows.Add(rw);
                    rw = null;
                    (rlv_causasion.InsertItem.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    (rlv_causasion.InsertItem.FindControl("rg_anexos") as RadGrid).DataBind();
                }
                else
                {
                    Obj.InsertEvidencia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_causasion.Items[0].FindControl("txt_codigo") as RadTextBox).Text),
                             txt_obsevidencia.Text, Convert.ToString(Session["UserLogon"]), prArchivo);

                    tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_causasion.Items[0].FindControl("txt_codigo") as RadTextBox).Text));
                    (rlv_causasion.Items[0].FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    (rlv_causasion.Items[0].FindControl("rg_anexos") as RadGrid).DataBind();
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
        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            TercerosBL ObjT = new TercerosBL();
            CausacionProveedoresBL Obj = new CausacionProveedoresBL();
            PlanillaImpuestosBL ObjI = new PlanillaImpuestosBL();
            double ln_valor = 0;
            try
            {
                //Valida Existencia de Documento Cargado
                if (Obj.ExisteDocumento(null, Convert.ToInt32(txt_codcli.Text), Convert.ToString(rc_tipdoccli.SelectedValue), txt_nrodoc.Text) == 0)
                {
                    //Aplica Impuestos
                    foreach (DataRow rit in ObjT.GetImpuestosxTercero(null, Convert.ToInt32(txt_codcli.Text)).Rows)
                    {
                        foreach (DataRow rdi in ObjI.GetPlanillaImpuestosDT(null, Convert.ToInt32(rit["PH_CODIGO"])).Rows)
                        {
                            DataRow rw = tbDetalle.NewRow();
                            rw["MVTD_IDENT"] = tbDetalle.Rows.Count + 1;
                            rw["PC_ID"] = 0;
                            rw["MVTH_CODIGO"] = 0;
                            rw["TRCODTER"] = txt_codcli.Text;
                            rw["MVTH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                            rw["PC_CODIGO"] = rdi["PC_CODIGO"];
                            rw["PC_NOMBRE"] = rdi["PC_NOMBRE"];
                            rw["MVTD_DEBITO"] = 0;
                            rw["MVTD_CREDITO"] = 0;
                            rw["IM_IMPUESTO"] = rdi["IM_IMPUESTO"];
                            rw["MVTD_DESCRIPCION"] = txt_concepto_mov.Text + " " +Convert.ToString(rdi["PC_NOMBRE"]) + " " + Convert.ToString(rdi["PI_PORCENTAJE"]) ;
                            

                            ln_valor = Math.Round(Convert.ToDouble(txt_valor.Value) * Convert.ToDouble(rdi["PI_PORCENTAJE"])/100,2);
                            if (Convert.ToString(rdi["PI_INDBASE"]) == "S")
                            {
                                foreach (DataRow x in tbDetalle.Rows)
                                {
                                    if (Convert.ToString(rdi["TTVALORC"]) == Convert.ToString(x["IM_IMPUESTO"]))
                                    {
                                        ln_valor = Math.Round((Convert.ToDouble(x["MVTD_CREDITO"]) * Convert.ToDouble(rdi["PI_BASE"])) / 100, 2);
                                        if (Convert.ToDouble(x["MVTD_DEBITO"]) != 0)
                                            ln_valor = Math.Round((Convert.ToDouble(x["MVTD_DEBITO"]) * Convert.ToDouble(rdi["PI_BASE"]))/100,2);
                                        break;
                                    }
                                }
                            }

                            if (Convert.ToString(rdi["PI_NATURALEZA"]) == "D")
                                rw["MVTD_DEBITO"] = ln_valor;
                            else
                                rw["MVTD_CREDITO"] = ln_valor;

                            rw["MVTD_TIPDOC"] = rc_tipdoccli.SelectedValue;
                            rw["MVTD_NOMTIPDOC"] = rc_tipdoccli.Text;
                            rw["MVTD_NRODOC"] = txt_nrodoc.Text;
                            rw["MVTD_FECDOC"] = System.DateTime.Today;

                            rw["MVTD_FECING"] = System.DateTime.Today;
                            rw["MVTD_FECMOD"] = System.DateTime.Today;
                            rw["MVTD_CDUSER"] = ".";
                            rw["MVTD_ESTADO"] = "AC";

                            tbDetalle.Rows.Add(rw);
                            rw = null;
                        }
                    }

                    //Aplica Cuentas/Contrapartes
                    foreach (DataRow rit in ObjT.GetCuentasxTercero(null, Convert.ToInt32(txt_codcli.Text)).Rows)
                    {
                        DataRow rw = tbDetalle.NewRow();
                        rw["MVTD_IDENT"] = tbDetalle.Rows.Count + 1;
                        rw["PC_ID"] = 0;
                        rw["MVTH_CODIGO"] = 0;
                        rw["TRCODTER"] = txt_codcli.Text;
                        rw["MVTH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rw["PC_CODIGO"] = rit["PC_CODIGO"];
                        rw["PC_NOMBRE"] = rit["PC_NOMBRE"];
                        rw["MVTD_DEBITO"] = 0;
                        rw["MVTD_CREDITO"] = 0;
                        rw["IM_IMPUESTO"] = "";
                        rw["MVTD_DESCRIPCION"] = txt_concepto_mov.Text + " " + Convert.ToString(rit["PC_NOMBRE"]);

                        if (Convert.ToString(rit["CTT_BASE"])=="S")
                            ln_valor = Math.Round(Convert.ToDouble(txt_valor.Value), 2);
                        else
                            ln_valor = Math.Round(Convert.ToDouble(txt_neto.Value), 2);

                        if (Convert.ToString(rit["CTT_IMPUESTO"]) != "-1")
                        {
                            foreach (DataRow x in tbDetalle.Rows)
                            {
                                if (Convert.ToString(x["IM_IMPUESTO"]) == Convert.ToString(rit["CTT_IMPUESTO"]))
                                {
                                    ln_valor = Math.Round(Convert.ToDouble(x["MVTD_CREDITO"]), 2);
                                    if (Convert.ToDouble(x["MVTD_DEBITO"]) != 0)
                                        ln_valor = Math.Round(Convert.ToDouble(x["MVTD_DEBITO"]), 2);
                                }
                            }
                        }

                        if (Convert.ToString(rit["CTT_NATURALEZA"]) == "D")
                            rw["MVTD_DEBITO"] = ln_valor;
                        else
                            rw["MVTD_CREDITO"] = ln_valor;

                        rw["MVTD_TIPDOC"] = rc_tipdoccli.SelectedValue;
                        rw["MVTD_NOMTIPDOC"] = rc_tipdoccli.Text;
                        rw["MVTD_NRODOC"] = txt_nrodoc.Text;
                        rw["MVTD_FECDOC"] = System.DateTime.Today;

                        rw["MVTD_FECING"] = System.DateTime.Today;
                        rw["MVTD_FECMOD"] = System.DateTime.Today;
                        rw["MVTD_CDUSER"] = ".";
                        rw["MVTD_ESTADO"] = "AC";

                        tbDetalle.Rows.Add(rw);
                        rw = null;
                    }

                    (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                    (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataBind();

                    
                }
                else
                {
                    litTextoMensaje.Text = "¡Nro Documento YA se Encuentra Registrado!";                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
                ObjI = null;
            }
        }
        protected void rc_ano_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            MesesBL Obj = new MesesBL();
            try
            {
                ((sender as RadComboBox).Parent.FindControl("rc_mes") as RadComboBox).Items.Clear();
                foreach (DataRow rw in (Obj.GetMeses(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadComboBox).SelectedValue))).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Text = Convert.ToString(rw["NOM_MES"]);
                    itemi.Value = Convert.ToString(rw["MA_MES"]);
                    ((sender as RadComboBox).Parent.FindControl("rc_mes") as RadComboBox).Items.Add(itemi);
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
        protected void rlv_causasion_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ViewState["isClickInsert"]))
            {
                if (((sender as RadListView).InsertItem.FindControl("txt_fecha") as RadDatePicker).SelectedDate == null)
                {
                    ((sender as RadListView).InsertItem.FindControl("txt_fecha") as RadDatePicker).SelectedDate = System.DateTime.Now;
                }
            }
        }

        protected void txt_mdebito_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text);
                foreach (DataRow rw in tbDetalle.Rows)
                {
                    if (Convert.ToInt32(rw["MVTD_IDENT"]) == ln_codigo)
                    {
                        rw["MVTD_DEBITO"] = Convert.ToDouble((sender as RadNumericTextBox).Value);
                    }
                }
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void txt_mcredito_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text);
                foreach (DataRow rw in tbDetalle.Rows)
                {
                    if (Convert.ToInt32(rw["MVTD_CREDITO"]) == ln_codigo)
                    {
                        rw["MVTD_CREDITO"] = Convert.ToDouble((sender as RadNumericTextBox).Value);
                    }
                }
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataSource = tbDetalle;
                (rlv_causasion.InsertItem.FindControl("rgDetalle") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        protected void rg_anexos_ItemCommand(object sender, GridCommandEventArgs e)
        {            
            if (e.CommandName == "attach")
            {
                txt_obsevidencia.Text = "";
                string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["MVEV_ID"].Text);

                CausacionProveedoresBL Obj = new CausacionProveedoresBL();
                foreach (DataRow rw in (Obj.GetEvidenciasAnexo(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["MVEV_ANEXO"]);
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
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + ".pdf";
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
    }
}