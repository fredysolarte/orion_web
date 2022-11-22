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
using XUSS.BLL.Contabilidad;
using XUSS.BLL.Gestion;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Gestion
{
    public partial class Gestion : System.Web.UI.Page
    {
        private DataTable tbCuentas
        {
            set { ViewState["tbCuentas"] = value; }
            get { return ViewState["tbCuentas"] as DataTable; }
        }
        private DataTable tbObligaciones
        {
            set { ViewState["tbObligaciones"] = value; }
            get { return ViewState["tbObligaciones"] as DataTable; }
        }
        private DataTable tbPreJuridico
        {
            set { ViewState["tbPreJuridico"] = value; }
            get { return ViewState["tbPreJuridico"] as DataTable; }
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
                    ViewState["isClickInsert"] = true;
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
            if (a is DBNull || a == null || Convert.ToString(a) == "N")
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
                return ComunBL.GetValorc(null, Convert.ToString(Session["CODEMP"]), "TCONTRA", Convert.ToString(a));
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
            PreJuridicoBL ObjJ = new PreJuridicoBL();
            try
            {
                //tbItems = Obj.GetItemsRecaudo(null, Convert.ToInt32(rlv_recaudo.Items[0].GetDataKeyValue("RC_NRORECIBO").ToString()));
                obj_sucursal.SelectParameters["TRCODTER"].DefaultValue = rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();
                obj_impuestos.SelectParameters["TRCODTER"].DefaultValue = rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString();

                tbCuentas = objt.GetCuentasxTercero(null, Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_cuentas") as RadGrid).DataSource = tbCuentas;
                (rlv_terceros.Items[0].FindControl("rg_cuentas") as RadGrid).DataBind();

                tbObligaciones = ObjJ.CargarObligaciones(null,Convert.ToString(Session["CODEMP"]) ,Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_obligaciones") as RadGrid).DataSource = tbObligaciones;
                (rlv_terceros.Items[0].FindControl("rg_obligaciones") as RadGrid).DataBind();

                tbPreJuridico = ObjJ.GetPrejuridico(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_juridico") as RadGrid).DataSource = tbPreJuridico;
                (rlv_terceros.Items[0].FindControl("rg_juridico") as RadGrid).DataBind();

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
                ObjJ = null;
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
            finally
            {
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
            try
            {
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
            e.InputParameters["tbCuentas"] = tbCuentas;
        }

        protected void obj_terceros_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbCuentas"] = tbCuentas;
        }

        protected void rg_cuentas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                
            }
        }

        protected void rg_juridico_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbPreJuridico;
        }

        protected void rg_juridico_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {                
                string script = "function f(){$find(\"" + mpItem.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }

        protected void btn_agregar_Click(object sender, EventArgs e)
        {
            PreJuridicoBL Obj = new PreJuridicoBL();
            try {
                Obj.InsertPrejuridico(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()),rc_tipificacion.SelectedValue,txt_telefono.Text,txt_email.Text,txt_observaciones.Text,Convert.ToString(Session["UserLogon"]),"AC");
                tbPreJuridico = Obj.GetPrejuridico(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_juridico") as RadGrid).DataSource = tbPreJuridico;
                (rlv_terceros.Items[0].FindControl("rg_juridico") as RadGrid).DataBind();

                rc_tipificacion.SelectedValue = "-1";
                txt_telefono.Text = "";
                txt_email.Text = "";
                txt_observaciones.Text = "";


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

        protected void rg_obligaciones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbObligaciones;
        }

        protected void rg_obligaciones_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                string script = "function f(){$find(\"" + mpObligacion.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }

        protected void btn_guardarobligacion_Click(object sender, EventArgs e)
        {
            PreJuridicoBL Obj = new PreJuridicoBL();
            try
            {
                Obj.InsertObligacion(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()), txt_nroobligacion.Text,txt_descripcion.Text,txt_tcartera.Text,Convert.ToInt32(txt_dmora.Value),txt_fcapital.Value,txt_fcorriente.Value,txt_fmora.Value, Convert.ToString(Session["UserLogon"]), "AC");
                tbObligaciones = Obj.CargarObligaciones(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_terceros.Items[0].GetDataKeyValue("TRCODTER").ToString()));
                (rlv_terceros.Items[0].FindControl("rg_obligaciones") as RadGrid).DataSource = tbObligaciones;
                (rlv_terceros.Items[0].FindControl("rg_obligaciones") as RadGrid).DataBind();

                txt_nroobligacion.Text = "";
                txt_descripcion.Text = "";
                txt_tcartera.Text = "";
                txt_dmora.Value = 0;
                txt_fcapital.Value = 0;
                txt_fcorriente.Value = 0;
                txt_fmora.Value = 0;


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
