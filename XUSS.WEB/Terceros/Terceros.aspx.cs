using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.Web.UI;
using System.Data;
using XUSS.BLL.Terceros;
using XUSS.BLL.Comun;
using XUSS.BLL.Contabilidad;
using System.IO;
using XUSS.BLL.Nomina;

namespace XUSS.WEB.Terceros
{
    public partial class Terceros : System.Web.UI.Page
    {
        private DataTable tbCuentas
        {
            set { ViewState["tbCuentas"] = value; }
            get { return ViewState["tbCuentas"] as DataTable; }
        }
        private DataTable tbFamilia
        {
            set { ViewState["tbFamilia"] = value; }
            get { return ViewState["tbFamilia"] as DataTable; }
        }
        private DataTable tbTitulos
        {
            set { ViewState["tbTitulos"] = value; }
            get { return ViewState["tbTitulos"] as DataTable; }
        }
        private DataTable tbAnexos
        {
            set { ViewState["tbAnexos"] = value; }
            get { return ViewState["tbAnexos"] as DataTable; }
        }
        private DataTable tbContratos
        {
            set { ViewState["tbContratos"] = value; }
            get { return ViewState["tbContratos"] as DataTable; }
        }
        private DataTable tbPlanillaNM
        {
            set { ViewState["tbPlanillaNM"] = value; }
            get { return ViewState["tbPlanillaNM"] as DataTable; }
        }
        private DataTable tbHorizontal
        {
            set { ViewState["tbHorizontal"] = value; }
            get { return ViewState["tbHorizontal"] as DataTable; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_terceros.SelectParameters["filter"].DefaultValue = " TRCODNIT ='" + Convert.ToString(Request.QueryString["Documento"]) + "'";
                    rlv_terceros.DataBind();
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Codigo"])))
                {
                    obj_terceros.SelectParameters["filter"].DefaultValue = " TRCODTER =" + Convert.ToString(Request.QueryString["Codigo"]);
                    rlv_terceros.DataBind();
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
            this.OcultarPaginador(rlv_terceros, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_terceros_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    TercerosBL objt = new TercerosBL();
                    PlanillaConceptosNMBL ObjNM = new PlanillaConceptosNMBL();
                    try
                    {
                        tbFamilia = objt.GetFamilia(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbTitulos = objt.GetTitulos(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbPlanillaNM = ObjNM.GetPlanillasxTercero(null, 0);
                        tbContratos = objt.GetContratos(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbHorizontal = objt.GetPropiedadHorizontal(null, Convert.ToString(Session["CODEMP"]), 0); 

                        ViewState["isClickInsert"] = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objt = null;
                        ObjNM = null;
                    }
                    break;

                case "Buscar":
                    obj_terceros.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_terceros.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
		{
			string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";
            
            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text))
                filtro += " AND TRCODTER = " + (((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text;

            if ((((RadButton)sender).Parent.FindControl("chk_empleado") as CheckBox).Checked)
                filtro += " AND TRINDEMP = 'S'";
            if ((((RadButton)sender).Parent.FindControl("chk_vendedor") as CheckBox).Checked)
                filtro += " AND TRINDVEN = 'S'";
            if ((((RadButton)sender).Parent.FindControl("chk_cliente") as CheckBox).Checked)
                filtro += " AND TRINDCLI = 'S'";
            if ((((RadButton)sender).Parent.FindControl("chk_proveedor") as CheckBox).Checked)
                filtro += " AND TRINDPRO = 'S'";
            if ((((RadButton)sender).Parent.FindControl("chk_forwarder") as CheckBox).Checked)
                filtro += " AND TRINDFOR = 'S'";

            if (!string.IsNullOrWhiteSpace(filtro))			
				filtro = filtro.Substring(4, filtro.Length - 4);			

			obj_terceros.SelectParameters["filter"].DefaultValue = filtro;
			rlv_terceros.DataBind();
			if ((rlv_terceros.Controls[0] is RadListViewEmptyDataItem))
			{
				StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");

                (rlv_terceros.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
			}
		}
        public bool GetCheck(object a)
        {
            if (a is DBNull || a == null || Convert.ToString(a)=="N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string GetTipoContrato(object a)
        {
            if (a is DBNull || a == null)
                return string.Empty;
            else
                return ComunBL.GetValorc(null,Convert.ToString(Session["CODEMP"]), "TCONTRA" ,Convert.ToString(a));
        }
        protected void rlv_terceros_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
           
            //DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            RadComboBoxItem item = new RadComboBoxItem();
            ComunBL Obj = new ComunBL();
            TercerosBL objt = new TercerosBL();
            PlanillaBL ObjP = new PlanillaBL();
            PlanillaConceptosNMBL ObjNM = new PlanillaConceptosNMBL();
            try
            {
                //tbItems = Obj.GetItemsRecaudo(null, Convert.ToInt32(rlv_recaudo.Items[0].GetDataKeyValue("RC_NRORECIBO").ToString()));
                obj_sucursal.SelectParameters["TRCODTER"].DefaultValue = rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();
                obj_impuestos.SelectParameters["TRCODTER"].DefaultValue = rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();

                tbCuentas = objt.GetCuentasxTercero(null, Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_cuentas") as RadGrid).DataSource = tbCuentas;
                (rlv_terceros.Items[0].FindControl("rg_cuentas") as RadGrid).DataBind();

                tbFamilia = objt.GetFamilia(null,Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_familia") as RadGrid).DataSource = tbFamilia;
                (rlv_terceros.Items[0].FindControl("rg_familia") as RadGrid).DataBind();

                tbTitulos = objt.GetTitulos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_infoacademica") as RadGrid).DataSource = tbTitulos;
                (rlv_terceros.Items[0].FindControl("rg_infoacademica") as RadGrid).DataBind();

                tbAnexos = objt.GetOtrosAnexos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_otrosanexos") as RadGrid).DataSource = tbAnexos;
                (rlv_terceros.Items[0].FindControl("rg_otrosanexos") as RadGrid).DataBind();

                tbContratos = objt.GetContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_contratos") as RadGrid).DataSource = tbContratos;
                (rlv_terceros.Items[0].FindControl("rg_contratos") as RadGrid).DataBind();

                tbPlanillaNM = ObjNM.GetPlanillasxTercero(null, Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_planillanomina") as RadGrid).DataSource = tbPlanillaNM;
                (rlv_terceros.Items[0].FindControl("rg_planillanomina") as RadGrid).DataBind();

                tbHorizontal = objt.GetPropiedadHorizontal(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_horizontal") as RadGrid).DataSource = tbHorizontal;
                (rlv_terceros.Items[0].FindControl("rg_horizontal") as RadGrid).DataBind();

                (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                using (IDataReader reader = Obj.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_pais") as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                        itemi.Text = Convert.ToString(reader["CIUDAD"]);
                        (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
                    }
                }
                //if (!fila.IsNull("TRAUTORE"))
                //{
                //    foreach (DataRow rw in (ObjP.GetPuc(null, " AND PC_ID =" + Convert.ToString(fila["TRAUTORE"])).Rows))
                //    {
                //        (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(0, new AutoCompleteBoxEntry(Convert.ToString(rw["PC_CODIGO"]), Convert.ToString(rw["PC_CODIGO"])));
                //        (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(1, new AutoCompleteBoxEntry(Convert.ToString(rw["PC_NOMBRE"]), Convert.ToString(rw["PC_NOMBRE"])));
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                objt = null;
                Obj = null;
                ObjP = null;
            }

            (e.Item.FindControl("rc_ciudad") as RadComboBox).SelectedValue = fila["TRCIUDAD"].ToString();
        }
        public Boolean GetComun(object a)
        {
            if (a is DBNull || a == null || Convert.ToString(a) == "1")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean GetSimplificado(object a)
        {
            if (a is DBNull || a == null || Convert.ToString(a) == "2")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void rc_pais_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            RadComboBoxItem item = new RadComboBoxItem();
            ComunBL Obj = new ComunBL();
            try
            {
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                using (IDataReader reader = Obj.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (sender as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                        itemi.Text = Convert.ToString(reader["CIUDAD"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
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
                Obj = null;
            }
        }
        protected void rlv_terceros_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            obj_terceros.UpdateParameters["TRCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;
            obj_terceros.UpdateParameters["TRINDEMP"].DefaultValue = "N";
            obj_terceros.UpdateParameters["TRINDVEN"].DefaultValue = "N";
            obj_terceros.UpdateParameters["TRINDCLI"].DefaultValue = "N";
            obj_terceros.UpdateParameters["TRINDPRO"].DefaultValue = "N";
            //obj_terceros.UpdateParameters["TRGRANCT"].DefaultValue = "N";
            //obj_terceros.UpdateParameters["TRAUTORE"].DefaultValue = "N";
            //obj_terceros.UpdateParameters["TRAUTORE"].DefaultValue = ((e.ListViewItem.FindControl("ac_ctacontable") as RadAutoCompleteBox)).Entries[0].Value;
            obj_terceros.UpdateParameters["TRDTTEC1"].DefaultValue = "N";
            
            if ((e.ListViewItem.FindControl("chk_empleado") as CheckBox).Checked) 
                obj_terceros.UpdateParameters["TRINDEMP"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_vendedor") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRINDVEN"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_cliente") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRINDCLI"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_proveedor") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRINDPRO"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_forwarder") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRINDFOR"].DefaultValue = "S";

            /*if ((e.ListViewItem.FindControl("chk_gcontribuyente") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRGRANCT"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_autoretenedor") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRAUTORE"].DefaultValue = "S";*/

            if ((e.ListViewItem.FindControl("chl_alterno") as CheckBox).Checked)
                obj_terceros.UpdateParameters["TRDTTEC1"].DefaultValue = "S";
        }
        protected void rlv_terceros_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_terceros.InsertParameters["TRCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;
            obj_terceros.InsertParameters["TRINDEMP"].DefaultValue = "N";
            obj_terceros.InsertParameters["TRINDVEN"].DefaultValue = "N";
            obj_terceros.InsertParameters["TRINDCLI"].DefaultValue = "N";
            obj_terceros.InsertParameters["TRINDPRO"].DefaultValue = "N";
            //obj_terceros.InsertParameters["TRGRANCT"].DefaultValue = "N";
            //obj_terceros.InsertParameters["TRAUTORE"].DefaultValue = "N";
            obj_terceros.InsertParameters["TRDTTEC1"].DefaultValue = "N";

            if ((e.ListViewItem.FindControl("chk_empleado") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRINDEMP"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_vendedor") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRINDVEN"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_cliente") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRINDCLI"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_proveedor") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRINDPRO"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_forwarder") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRINDFOR"].DefaultValue = "S";

            /*if ((e.ListViewItem.FindControl("chk_gcontribuyente") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRGRANCT"].DefaultValue = "S";
            if ((e.ListViewItem.FindControl("chk_autoretenedor") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRAUTORE"].DefaultValue = "S";*/

            if ((e.ListViewItem.FindControl("chl_alterno") as CheckBox).Checked)
                obj_terceros.InsertParameters["TRDTTEC1"].DefaultValue = "S";            
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            TercerosBL obj = new TercerosBL();
            try
            {
                obj.InsertSucursales(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()),
                                     (((ImageButton)sender).Parent.FindControl("txt_nomsucursal") as RadTextBox).Text, (((ImageButton)sender).Parent.FindControl("txt_telsucursal") as RadTextBox).Text,
                                     (((ImageButton)sender).Parent.FindControl("txt_dirsucursal") as RadTextBox).Text, (((ImageButton)sender).Parent.FindControl("txt_direntsucursal") as RadTextBox).Text,
                                     (((ImageButton)sender).Parent.FindControl("rc_pais") as RadComboBox).SelectedValue,
                                     (((ImageButton)sender).Parent.FindControl("rc_ciudad") as RadComboBox).SelectedValue, "AC");

                //obj_sucursal.SelectParameters["TRCODTER"].DefaultValue = rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                obj = null;
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=1008&inban=S&inParametro=pTNro&inValor=" + rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void txt_identificacion_OnTextChanged(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            try
            {
                if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
                {
                    if (Obj.ExisteTercero(null, (sender as RadTextBox).Text))
                    {
                        litTextoMensaje.Text = "Numero Identificacion Ya Existe!";
                        (sender as RadTextBox).Text = "";
                        string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
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
        protected void rg_impuestos_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        int PH_CODIGO = Convert.ToInt32(dataItem.GetDataKeyValue("PH_CODIGO").ToString());

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        PlanillaImpuestosBL Obj = new PlanillaImpuestosBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetPlanillaImpuestosDT(null, PH_CODIGO);
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
        }
        protected void rg_impuestos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string url = "";
            try
            {
                switch (e.CommandName)
                {
                    case "plantilla":
                        GridDataItem item = (GridDataItem)e.Item;
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Contabilidad/PlanillaImpuestos.aspx?Codigo=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        item = null;
                        break;
                    case "InitInsert":
                        string script = "function f(){$find(\"" + mpFindPlantilla.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                        break;
                    case "Select":
                        GridDataItem item_ = (GridDataItem)e.Item;
                        TercerosBL Obj = new TercerosBL();
                        try
                        {
                            int ln_codigo = Convert.ToInt32(item_["PH_CODIGO"].Text);
                            int ln_codter = Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString());
                            Obj.InsertImpuestosxTercero(null, ln_codigo, ln_codter);
                            obj_impuestos.SelectParameters["TRCODTER"].DefaultValue = ln_codter.ToString();
                            (rlv_terceros.Items[0].FindControl("rg_impuestos") as RadGrid).DataBind();
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
                        break;
                    case "Delete":
                        GridDataItem item__ = (GridDataItem)e.Item;
                        TercerosBL Obj_ = new TercerosBL();
                        try
                        {
                            int ln_codigo = Convert.ToInt32(item__["PH_CODIGO"].Text);
                            int ln_codter = Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString());
                            Obj_.DeleteImpuestosxTercero(null, ln_codigo, ln_codter);
                            obj_impuestos.SelectParameters["TRCODTER"].DefaultValue = ln_codter.ToString();
                            (rlv_terceros.Items[0].FindControl("rg_impuestos") as RadGrid).DataBind();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            item__ = null;
                            Obj_ = null;
                        }
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
        protected void rg_cuentas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbCuentas;
        }
        protected void rg_cuentas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        DataRow rw = tbCuentas.NewRow();
                        rw["CTT_ID"] = 0;
                        rw["PC_ID"] = 0;
                        rw["TRCODTER"] = 0;

                        rw["PC_CODIGO"] = ((e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox)).Entries[0].Value;
                        rw["PC_NOMBRE"] = ((e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox)).Entries[0].Text;

                        rw["CTT_NATURALEZA"] = (e.Item.FindControl("rc_naturaleza") as RadComboBox).SelectedValue;

                        rw["CTT_BASE"] = "N";
                        if ((e.Item.FindControl("chk_indbase") as CheckBox).Checked)
                            rw["CTT_BASE"] = "S";

                        rw["CTT_IMPUESTO"] = (e.Item.FindControl("rc_impuesto") as RadComboBox).SelectedValue;
                        rw["TTDESCRI"] = (e.Item.FindControl("rc_impuesto") as RadComboBox).Text;
                        rw["CTT_TIPPLA"] = 0;
                        
                        tbCuentas.Rows.Add(rw);
                        rw = null;
                        break;
                    case "Update":
                        int ln_item = Convert.ToInt32((e.Item as GridEditableItem).GetDataKeyValue("CTT_ID"));
                        foreach (DataColumn rc in tbCuentas.Columns)
                            rc.ReadOnly = false;

                        foreach (DataRow row in tbCuentas.Rows)
                        {
                            if (ln_item == Convert.ToInt32(row["CTT_ID"]))
                            {                               
                                //row["PC_CODIGO"] = ((e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox)).Entries[0].Value;
                                //row["PC_NOMBRE"] = ((e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox)).Entries[0].Text;
                                row["CTT_NATURALEZA"] = (e.Item.FindControl("rc_naturaleza") as RadComboBox).SelectedValue;
                                row["CTT_BASE"] = "N";
                                if ((e.Item.FindControl("chk_indbase") as CheckBox).Checked)
                                    row["CTT_BASE"] = "S";
                                row["CTT_IMPUESTO"] = (e.Item.FindControl("rc_impuesto") as RadComboBox).SelectedValue;
                                row["TTDESCRI"] = (e.Item.FindControl("rc_impuesto") as RadComboBox).Text;
                                row["CTT_TIPPLA"] = 0;
                            }
                        }
                        break;
                    case "Delete":
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        protected void obj_terceros_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            //e.InputParameters["tbCuentas"] = tbCuentas;
            e.InputParameters["tbFamilia"] = tbFamilia;
            e.InputParameters["tbTitulos"] = tbTitulos;
            e.InputParameters["tbContratos"] = tbContratos;
            e.InputParameters["tbPlanillaNM"] = tbPlanillaNM;
            e.InputParameters["tbHorizontal"] = tbHorizontal;
        }
        protected void obj_terceros_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbCuentas"] = tbCuentas;
            e.InputParameters["tbHorizontal"] = tbHorizontal;
        }
        protected void rg_cuentas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //DataRow fila = ((DataRowView)((e.Item)).DataItem).Row;
                //(e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(0, new AutoCompleteBoxEntry(Convert.ToString(fila["PC_CODIGO"]), Convert.ToString(fila["PC_CODIGO"])));
                //(e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(0, new AutoCompleteBoxEntry(Convert.ToString(fila["PC_NOMBRE"]), Convert.ToString(fila["PC_NOMBRE"])));
            }
        }
        protected void rg_familia_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbFamilia;
        }
        protected void rg_infoacademica_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbTitulos;
        }
        protected void rg_infoacademica_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string lc_alert = "N";
            TercerosBL Obj = new TercerosBL();
            
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        if ((e.Item.FindControl("chk_alertatit") as CheckBox).Checked)
                        lc_alert = "S";

                        Obj.insertTitulo(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()), (e.Item.FindControl("rc_nivel") as RadComboBox).SelectedValue, (e.Item.FindControl("rc_profesion") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_descripciontit") as RadTextBox).Text,
                        (e.Item.FindControl("txt_fdocumentotit") as RadDatePicker).SelectedDate, (e.Item.FindControl("txt_fvencimientotit") as RadDatePicker).SelectedDate, lc_alert, prArchivo, Convert.ToString(Session["UserLogon"]));

                        tbTitulos = Obj.GetTitulos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbTitulos;
                        (sender as RadGrid).DataBind();
                        break;
                    case "Delete":
                        GridDataItem ditem = (GridDataItem)e.Item;
                        int ln_item = Convert.ToInt32(ditem["TT_CODIGO"].Text);
                        Obj.DeleteTitulos(null, ln_item);
                        ditem = null;

                        tbTitulos = Obj.GetTitulos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbTitulos;
                        (sender as RadGrid).DataBind();
                        break;

                    case "download_file":
                        byte[] archivo = null;
                        GridDataItem ditem_ = (GridDataItem)e.Item;
                        int item_ = Convert.ToInt32(ditem_["TT_CODIGO"].Text);
                        archivo = (byte[])Obj.GetImagenAcademico(null, item_);
                        ditem_ = null;
                        Random random = new Random();
                        int random_0 = random.Next(0, 100);
                        int random_1 = random.Next(0, 100);
                        int random_2 = random.Next(0, 100);
                        int random_3 = random.Next(0, 100);
                        int random_4 = random.Next(0, 100);
                        int random_5 = random.Next(0, 100);
                        string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item_) + ".pdf";
                        string path = MapPath("~/Uploads/" + lc_nombre);
                        //File.WriteAllBytes(MapPath("~/Uploads/" + "archivo.pdf"), archivo);
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
        protected void rg_familia_ItemCommand(object sender, GridCommandEventArgs e)
        {            
            TercerosBL Obj = new TercerosBL();            
            try
            {
                switch (e.CommandName)
                {
                    case  "PerformInsert":                    
                        Obj.InsertFamilia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()), (e.Item.FindControl("rc_tdoc") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_nrodocfam") as RadTextBox).Text, (e.Item.FindControl("txt_pnombrefam") as RadTextBox).Text,
                            (e.Item.FindControl("txt_snombrefam") as RadTextBox).Text, (e.Item.FindControl("txt_papellidofam") as RadTextBox).Text, (e.Item.FindControl("txt_sapellidofam") as RadTextBox).Text, (e.Item.FindControl("txt_fnacimientofam") as RadDatePicker).SelectedDate,
                            (e.Item.FindControl("rc_parentesco") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_direccionfam") as RadTextBox).Text, (e.Item.FindControl("txt_emailfam") as RadTextBox).Text, (e.Item.FindControl("txt_telfam") as RadTextBox).Text,
                            (e.Item.FindControl("rc_treferencia") as RadComboBox).SelectedValue, Convert.ToString(Session["UserLogon"]));

                        tbFamilia = Obj.GetFamilia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbFamilia;
                        (sender as RadGrid).DataBind();
                    break;
                    case "Delete":
                        GridDataItem ditem = (GridDataItem)e.Item;
                        int ln_item = Convert.ToInt32(ditem["FM_CODIGO"].Text);
                        Obj.DeleteFamilia(null, ln_item);
                        ditem = null;

                        tbFamilia = Obj.GetFamilia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbFamilia;
                        (sender as RadGrid).DataBind();
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
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void rg_otrosanexos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbAnexos;
        }
        protected void rg_otrosanexos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            string lc_alerta = "N";
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        if ((e.Item.FindControl("chk_alertaotros") as CheckBox).Checked)
                            lc_alerta = "S";

                        Obj.InsertOtrosAnexos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()), (e.Item.FindControl("txt_descripcionotros") as RadTextBox).Text,lc_alerta,
                            (e.Item.FindControl("txt_fvencimientootros") as RadDatePicker).SelectedDate,prArchivo,Convert.ToString(Session["UserLogon"]));
                        
                        tbAnexos = Obj.GetOtrosAnexos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbAnexos;
                        (sender as RadGrid).DataBind();
                        break;
                    case "Delete":
                        GridDataItem ditem = (GridDataItem)e.Item;
                        int ln_item = Convert.ToInt32(ditem["OA_CONSECUTIVO"].Text);
                        Obj.DeleteOtrosAnexos(null, ln_item);
                        ditem = null;

                        tbAnexos = Obj.GetOtrosAnexos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbAnexos;
                        (sender as RadGrid).DataBind();
                        break;
                    case "download_file":
                        byte[] archivo = null;
                        GridDataItem ditem_ = (GridDataItem)e.Item;
                        int item_ = Convert.ToInt32(ditem_["OA_CONSECUTIVO"].Text);
                        archivo = (byte[])Obj.GetImagenOtros(null, item_);
                        ditem_ = null;
                        Random random = new Random();
                        int random_0 = random.Next(0, 100);
                        int random_1 = random.Next(0, 100);
                        int random_2 = random.Next(0, 100);
                        int random_3 = random.Next(0, 100);
                        int random_4 = random.Next(0, 100);
                        int random_5 = random.Next(0, 100);
                        string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item_) + ".pdf";
                        string path = MapPath("~/Uploads/" + lc_nombre);
                        //File.WriteAllBytes(MapPath("~/Uploads/" + "archivo.pdf"), archivo);
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
        //protected void rg_otrosanexos_ItemCommand1(object sender, GridCommandEventArgs e)
        //{
        //    TercerosBL Obj = new TercerosBL();
        //    try
        //    {
        //        switch (e.CommandName)
        //        {
        //            case "Delete":
        //                GridDataItem ditem = (GridDataItem)e.Item;
        //                int ln_item = Convert.ToInt32(ditem["OA_CONSECUTIVO"].Text);
        //                Obj.DeleteOtrosAnexos(null, ln_item);
        //                ditem = null;

        //                tbAnexos = Obj.GetOtrosAnexos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
        //                (sender as RadGrid).DataSource = tbAnexos;
        //                (sender as RadGrid).DataBind();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //}
        protected void rg_contratos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            try {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        if (rlv_terceros.InsertItem != null)
                        {
                            DataRow rw = tbContratos.NewRow();
                            rw["CT_ID"] = tbContratos.Rows.Count + 1;
                            rw["TRCODEMP"] = ".";
                            rw["TRCODTER"] = 0;
                            rw["CT_TNOVEDAD"] = (e.Item.FindControl("rc_tnovedad") as RadComboBox).SelectedValue;
                            rw["CT_TCONTRATO"] = (e.Item.FindControl("rc_tcontrato") as RadComboBox).SelectedValue;
                            rw["CT_CARGO"] = (e.Item.FindControl("rc_cargo") as RadComboBox).SelectedValue;
                            rw["CT_FINGRESO"] = Convert.ToDateTime((e.Item.FindControl("txt_fcontrato") as RadDatePicker).DbSelectedDate);
                            rw["CT_SALARIO"] = Convert.ToDouble((e.Item.FindControl("txt_salario") as RadNumericTextBox).DbValue);
                            rw["CT_USUARIO"] = ".";
                            rw["CT_ESTADO"] = "AC";
                            rw["CT_FECING"] = System.DateTime.Today;
                            //rw["CT_FTERMINACION"] = (DateTime?)null;

                            tbContratos.Rows.Add(rw);
                            rw = null;
                        }
                        else
                        {
                            Obj.InsertContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()), (e.Item.FindControl("rc_tnovedad") as RadComboBox).SelectedValue, (e.Item.FindControl("rc_tcontrato") as RadComboBox).SelectedValue, (e.Item.FindControl("rc_cargo") as RadComboBox).SelectedValue,
                                                Convert.ToDateTime((e.Item.FindControl("txt_fcontrato") as RadDatePicker).DbSelectedDate), Convert.ToDouble((e.Item.FindControl("txt_salario") as RadNumericTextBox).DbValue), Convert.ToString(Session["UserLogon"]), "AC");

                            tbContratos = Obj.GetContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));                            
                        }

                        (sender as RadGrid).DataSource = tbContratos;
                        (sender as RadGrid).DataBind();

                        break;
                    case "Update":
                        Obj.UpdateContratos(null, Convert.ToInt32((e.Item.FindControl("txt_ctid") as RadTextBox).Text), (e.Item.FindControl("rc_tnovedad") as RadComboBox).SelectedValue, (e.Item.FindControl("rc_tcontrato") as RadComboBox).SelectedValue, (e.Item.FindControl("rc_cargo") as RadComboBox).SelectedValue,
                                            Convert.ToDateTime((e.Item.FindControl("txt_fcontrato") as RadDatePicker).DbSelectedDate), string.IsNullOrEmpty(Convert.ToString((e.Item.FindControl("txt_ffinalcontrato") as RadDatePicker).DbSelectedDate)) ? (DateTime?)null : Convert.ToDateTime((e.Item.FindControl("txt_ffinalcontrato") as RadDatePicker).DbSelectedDate), Convert.ToDouble((e.Item.FindControl("txt_salario") as RadNumericTextBox).DbValue), Convert.ToString(Session["UserLogon"]), "AC");

                        tbContratos = Obj.GetContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbContratos;
                        (sender as RadGrid).DataBind();

                        break;
                    case "Delete":
                        GridEditableItem item = e.Item as GridEditableItem;                        
                        Obj.DeleteContrato(null, Convert.ToInt32(item.GetDataKeyValue("CT_ID").ToString()));
                        tbContratos = Obj.GetContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbContratos;
                        (sender as RadGrid).DataBind();
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
        protected void rg_contratos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbContratos;
        }
        protected void rg_planillanomina_ItemCommand(object sender, GridCommandEventArgs e)
        {
            PlanillaConceptosNMBL Obj = new PlanillaConceptosNMBL();
            try
            {
                switch (e.CommandName)
                {                    
                    case "InitInsert":
                        string script = "function f(){$find(\"" + mpFindPlanillaNM.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                        break;

                    case "Delete":
                        GridEditableItem item = e.Item as GridEditableItem;
                        Obj.DeletePlanillaNMTercero(null, Convert.ToInt32(item.GetDataKeyValue("PH_CODPLAN").ToString()));                        
                        tbPlanillaNM = Obj.GetPlanillasxTercero(null, Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                        (sender as RadGrid).DataSource = tbPlanillaNM;
                        (sender as RadGrid).DataBind();
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
        protected void rg_planillanomina_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        int PH_CODIGO = Convert.ToInt32(dataItem.GetDataKeyValue("PH_CODPLAN").ToString());

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        PlanillaConceptosNMBL Obj = new PlanillaConceptosNMBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetPlanillaConceptosDT(null, PH_CODIGO);
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
        }
        protected void rg_nomina_find_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        int PH_CODIGO = Convert.ToInt32(dataItem.GetDataKeyValue("PH_CODPLAN").ToString());

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        PlanillaConceptosNMBL Obj = new PlanillaConceptosNMBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetPlanillaConceptosDT(null, PH_CODIGO);

                            string script = "function f(){$find(\"" + mpFindPlanillaNM.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                        break;
                    }
            }
        }
        protected void rg_nomina_find_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string url = "";
            try
            {
                switch (e.CommandName)
                {                    
                    case "plantilla":
                        GridDataItem item = (GridDataItem)e.Item;
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Contabilidad/PlanillaImpuestos.aspx?Codigo=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        item = null;
                        break;
                    case "InitInsert":
                        string script = "function f(){$find(\"" + mpFindPlanillaNM.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                        break;
                    case "Select":
                        if (rlv_terceros.InsertItem != null)
                        {
                            GridDataItem item_ = (GridDataItem)e.Item;
                            DataRow rw = tbPlanillaNM.NewRow();

                            rw["PH_CODIGO"] = 0;
                            rw["PH_CODEMP"] = ".";
                            rw["PH_CODPLAN"] = Convert.ToInt32((item_.FindControl("lbl_doc") as Label).Text);
                            rw["PH_NOMBRE"] = item_["PH_NOMBRE"].Text;
                            rw["PH_ESTADO"] = "AC";
                            rw["PH_USUARIO"] = ".";
                            rw["PH_FECING"] = System.DateTime.Today;

                            tbPlanillaNM.Rows.Add(rw);
                            rw = null;

                            (rlv_terceros.InsertItem.FindControl("rg_planillanomina") as RadGrid).DataSource = tbPlanillaNM;
                            (rlv_terceros.InsertItem.FindControl("rg_planillanomina") as RadGrid).DataBind();

                        }
                        else
                        {
                            GridDataItem item_ = (GridDataItem)e.Item;
                            PlanillaConceptosNMBL Obj = new PlanillaConceptosNMBL();
                            try
                            {
                                int ln_codigo = Convert.ToInt32((item_.FindControl("lbl_doc") as Label).Text);
                                int ln_codter = Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString());

                                Obj.InsertPlanillaNMTercero(null, ln_codigo, ln_codter);

                                tbPlanillaNM = Obj.GetPlanillasxTercero(null, ln_codter);
                                (rlv_terceros.Items[0].FindControl("rg_planillanomina") as RadGrid).DataSource = tbPlanillaNM;
                                (rlv_terceros.Items[0].FindControl("rg_planillanomina") as RadGrid).DataBind();
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
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
         }
        protected void rg_planillanomina_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbPlanillaNM;
        }
        protected void rg_horizontal_ItemCommand(object sender, GridCommandEventArgs e)
        {
            btnUpdHV.Visible = false;
            btnCanHV.Visible = false;
            txt_fecdesitalacion.Enabled = false;
            txt_nrosoportedes.Enabled = false;
            rc_tdesistala.Enabled = false;
            txt_observaciones.Enabled = false;            
            chk_anucomercial.Enabled = false;
            chk_anucomercial.Visible = false;
            chk_anudesmonte.Enabled = false;
            chk_anudesmonte.Visible = false;

            if (rlv_terceros.EditItems.Count >= 1)
            {
                btnUpdHV.Visible = true;
                btnCanHV.Visible = true;
                txt_fecdesitalacion.Enabled = true;
                txt_nrosoportedes.Enabled = true;
                rc_tdesistala.Enabled = true;
                txt_observaciones.Enabled = true;
                chk_anucomercial.Checked = false;
                chk_anucomercial.Enabled = true;
                chk_anucomercial.Visible = true;
                chk_anudesmonte.Checked = false;
                chk_anudesmonte.Enabled = true;
                chk_anudesmonte.Visible = true;
            }
            switch (e.CommandName)
            {
                case "Select":
                    TercerosBL obj = new TercerosBL();
                    GridDataItem item = (GridDataItem)e.Item;
                    try
                    {                        
                        txt_articuloinstalacion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                        txt_serialinstalacion.Text = Convert.ToString(item["MECDELEM"].Text);
                        rc_tecusuario.SelectedValue = Convert.ToString((item.FindControl("lbl_itusuario") as Label).Text);
                        txt_fecinstalacion.SelectedDate = (item.FindControl("txt_finstalacionrg") as RadDatePicker).SelectedDate;
                        txt_phcodigo.Text = item["PH_CODIGO"].Text;
                        rg_imagenesinstalacion.DataSource = obj.GetImagenesInstalacion(null,Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_imagenesinstalacion.DataBind();                        
                        txt_fcomercial.SelectedDate = (item.FindControl("txt_feccomercialrg") as RadDatePicker).SelectedDate;
                        rc_usucomercial.SelectedValue = Convert.ToString((item.FindControl("lbl_cousuario") as Label).Text);
                        txt_identificacioncom.Text = Convert.ToString(item["TRCODNIT"].Text);
                        txt_nombrecom.Text = Convert.ToString(item["TRNOMBRE"].Text) == "&nbsp;" ? "" : Convert.ToString(item["TRNOMBRE"].Text);
                        txt_snombrecom.Text = Convert.ToString(item["TRNOMBR2"].Text) == "&nbsp;" ? "" : Convert.ToString(item["TRNOMBR2"].Text);
                        txt_apellidoscom.Text = Convert.ToString(item["TRAPELLI"].Text) == "&nbsp;" ? "" : Convert.ToString(item["TRAPELLI"].Text); 
                        txt_sapellidocom.Text = Convert.ToString(item["TRNOMBR3"].Text) == "&nbsp;" ? "" : Convert.ToString(item["TRNOMBR3"].Text); 
                        rg_imgcomercial.DataSource = obj.GetImagenesComercial(null, Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_imgcomercial.DataBind();
                        rg_imgdesinstalacion.DataSource = obj.GetImageneDesmonte(null, Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_imgdesinstalacion.DataBind();

                        rg_gestionp.DataSource = obj.GetgestionP(null, "001",Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_gestionp.DataBind();
                        rg_servicios.DataSource = obj.GetServicios(null, "001",Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_servicios.DataBind();
                        rc_tdesistala.SelectedValue = Convert.ToString((item.FindControl("lbl_desusuario") as Label).Text);
                        //if (item["DI_FECHA"].Text != "&nbsp;")
                        txt_fecdesitalacion.SelectedDate = (item.FindControl("txt_fecdesmorg") as RadDatePicker).SelectedDate;
                        txt_nrosoportedes.Text = Convert.ToString(item["DI_NRODOC"].Text);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpDetalleHV.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
                case "RebindGrid":
                    string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Codigo=" + (item_.FindControl("lbl_codterc") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
            }
        }
        protected void rg_horizontal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbHorizontal;
        }
        protected void btn_procesar_Click(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo));
        }
        protected void procesa_plano(Stream inStream)
        {
            try
            {
                foreach (DataColumn cl in tbHorizontal.Columns) cl.ReadOnly = false;

                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            Boolean lb_existe = false;
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');

                            foreach (DataRow rz in tbHorizontal.Rows)
                            {
                                if (Convert.ToString(rz["PH_EDIFICIO"]) == Convert.ToString(words[1]) && Convert.ToString(rz["PH_ESCALERA"]) == Convert.ToString(words[4]))
                                {
                                    lb_existe = true;
                                    rz["PH_CTACONTRATO"] = words[0];
                                    rz["PH_POLIZA"] = words[1];
                                    rz["PH_OBJCONEXION"] = words[6];
                                    rz["PH_PTOSUMINISTRO"] = words[7];
                                    rz["PH_INSTALACION"] = words[8];
                                    rz["PH_UBCAPARATO"] = words[9];
                                    rz["CP_ID"] = words[10];
                                }
                            }

                            if (!lb_existe)
                            {
                                DataRow rw = tbHorizontal.NewRow();
                                rw["PH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                rw["TRCODTER"] = 0;
                                rw["PH_CODIGO"] = tbHorizontal.Rows.Count + 1;
                                rw["PH_CTACONTRATO"] = words[0];
                                rw["PH_POLIZA"] = words[1];
                                rw["PH_EDIFICIO"] = words[2];
                                rw["PH_PORTAL"] = words[3];
                                rw["PH_PISO"] = words[4];
                                rw["PH_ESCALERA"] = words[5];
                                rw["PH_OBJCONEXION"] = words[6];
                                rw["PH_PTOSUMINISTRO"] = words[7];
                                rw["PH_INSTALACION"] = words[8];
                                rw["PH_UBCAPARATO"] = words[9];
                                rw["CP_ID"] = words[10];
                                rw["PH_USUARIO"] = "...";
                                rw["PH_FECING"] = System.DateTime.Today;
                                rw["TTDESCRI"] = ".";
                                rw["CO_FECHA"] = System.DateTime.Today;
                                rw["DI_FECHA"] = System.DateTime.Today;
                                rw["DI_USUARIO"] = ".";
                                rw["tec_desistala"] = ".";
                                rw["ESTADO_CO"] = ".";
                                rw["comercial"] = ".";
                                rw["CLIENTE"] = ".";
                                rw["TRCODNIT"] = ".";
                                rw["TRNOMBRE"] = ".";
                                rw["TRNOMBR2"] = ".";
                                rw["TRAPELLI"] = ".";
                                rw["TRNOMBR3"] = ".";
                                rw["CO_USUARIO"] = ".";
                                rw["ESTADO_T"] = ".";
                                rw["CODCLI"] = 0;

                                tbHorizontal.Rows.Add(rw);
                                rw = null;
                            }
                            
                        }
                    }
                }
                if (rlv_terceros.InsertItem != null)
                {
                    (rlv_terceros.InsertItem.FindControl("rg_horizontal") as RadGrid).DataSource = tbHorizontal;
                    (rlv_terceros.InsertItem.FindControl("rg_horizontal") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_terceros.Items[0].FindControl("rg_horizontal") as RadGrid).DataSource = tbHorizontal;
                    (rlv_terceros.Items[0].FindControl("rg_horizontal") as RadGrid).DataBind();
                }
                
                litTextoMensaje.Text = "Cargue Efectuado!";
                string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
                //throw ex;
            }
            finally
            {

            }
        }
        protected void obj_terceros_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)            
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
                litTextoMensaje.Text = "Codigo :" + Convert.ToString(e.ReturnValue);

            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rlv_terceros_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {                
                e.ExceptionHandled = true;                
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        protected void rg_imagenesinstalacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            
            TercerosBL obj = new TercerosBL();
            try
            {                
                rg_imagenesinstalacion.DataSource = obj.GetImagenesInstalacion(null, Convert.ToInt32(txt_phcodigo.Text));
                rg_imagenesinstalacion.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpDetalleHV.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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

        protected void rg_imgcomercial_ItemCommand(object sender, GridCommandEventArgs e)
        {
            
            TercerosBL obj = new TercerosBL();
            try
            {
                switch (e.CommandName)
                {
                    case "Page":
                        //rg_imgcomercial.DataSource = obj.GetImagenesComercial(null, Convert.ToInt32(item["PH_CODIGO"].Text));
                        rg_imgcomercial.DataSource = obj.GetImagenesComercial(null, Convert.ToInt32(txt_phcodigo.Text));
                        rg_imgcomercial.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpDetalleHV.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        break;

                    case "download_file":
                        GridDataItem item = (GridDataItem)e.Item;
                        byte[] archivo = null;
                        int id_fallo = Convert.ToInt32((item.FindControl("lbl_idcomercial") as Label).Text);
                        archivo = (byte[])obj.GetImageneComercial(null, id_fallo);
                        Random random = new Random();
                        int random_0 = random.Next(0, 100);
                        int random_1 = random.Next(0, 100);
                        int random_2 = random.Next(0, 100);
                        int random_3 = random.Next(0, 100);
                        int random_4 = random.Next(0, 100);
                        int random_5 = random.Next(0, 100);

                        string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + ".jpg";
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
                        break;
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

        protected void btnUpdHV_Click(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            try {
                if (chk_anucomercial.Checked)
                    Obj.DeleteComercial(null, Convert.ToInt32(txt_phcodigo.Text));
                if (chk_anudesmonte.Checked)
                {
                    Obj.DeleteDesmontaje(null, Convert.ToInt32(txt_phcodigo.Text));
                }
                else
                {
                    if (txt_fecdesitalacion.DbSelectedDate != null && rc_tdesistala.SelectedValue != "-1")
                        Obj.InsertDesmontaje(null, Convert.ToInt32(txt_phcodigo.Text), Convert.ToString(Session["CODEMP"]), Convert.ToDateTime(txt_fecdesitalacion.SelectedDate), Convert.ToString(rc_tdesistala.SelectedValue),Convert.ToString(txt_nrosoportedes.Text));
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