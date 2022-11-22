using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Nomina;

namespace XUSS.WEB.Nomina
{
    public partial class PlanillaConceptos : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Codigo"])))
                {
                    obj_planimp.SelectParameters["filter"].DefaultValue = " PH_CODPLAN ='" + Convert.ToString(Request.QueryString["Codigo"]) + "'";
                    rlv_planconcep.DataBind();
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
            this.OcultarPaginador(rlv_planconcep, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_planimp_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            Boolean lb_bandera = true;
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    PlanillaConceptosNMBL obj = new PlanillaConceptosNMBL();
                    try
                    {
                        tbItems = obj.GetPlanillaConceptosDT(null, 0);
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
                    obj_planimp.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_planconcep.DataBind();
                    break;
                case "Delete":
                    break;
            }
            if (lb_bandera)
                this.AnalizarCommand(e.CommandName);
            else
                this.AnalizarCommand("Cancel");

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND TRNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";


            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_planimp.SelectParameters["filter"].DefaultValue = filtro;
            rlv_planconcep.DataBind();
            if ((rlv_planconcep.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_planconcep.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_planimp_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    PlanillaConceptosNMBL obj = new PlanillaConceptosNMBL();
                    try
                    {
                        tbItems = obj.GetPlanillaConceptosDT(null, Convert.ToInt32(rlv_planconcep.Items[0].GetDataKeyValue("PH_CODPLAN").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
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
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {

            PlanillaConceptosNMBL Obj = new PlanillaConceptosNMBL();
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        DataRow row = tbItems.NewRow();
                        row["PH_CODIGO"] = 0;
                        row["PD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        row["PD_CODIGO"] = tbItems.Rows.Count + 1;

                        row["PD_CONCEPTO"] = (e.Item.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                        row["TTDESCRI"] = (e.Item.FindControl("rc_concepto") as RadComboBox).Text;

                        row["PD_TIPO"] = (e.Item.FindControl("rc_tipo") as RadComboBox).SelectedValue;
                        row["TIPOSR"] = (e.Item.FindControl("rc_tipo") as RadComboBox).Text;
                        row["PD_TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).SelectedValue;
                        row["TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).Text;

                        row["PD_VALOR"] = (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value;
                        row["PC_NATURALEZA"] = ".";
                        row["PC_ID"] = 0;
                        row["PC_CODIGO"] = (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries[0].Value;
                        row["PC_NOMBRE"] = (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries[0].Text;                        

                        row["PD_BASE"] = "N";

                        if ((e.Item.FindControl("chk_indbase") as CheckBox).Checked)
                            row["PD_BASE"] = "S";

                        //row["PI_BASE"] = (e.Item.FindControl("txt_base") as RadNumericTextBox).Value;

                        row["PD_CONCEPTO"] = (e.Item.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                        row["PD_ESTADO"] = "AC";
                        row["PD_USUARIO"] = ".";
                        row["PD_FECING"] = System.DateTime.Today;
                        row["PD_FECMOD"] = System.DateTime.Today;


                        tbItems.Rows.Add(row);
                        row = null;

                        break;
                    case "Update":
                        int ln_item = Convert.ToInt32((e.Item as GridEditableItem).GetDataKeyValue("PD_CODIGO"));
                        foreach (DataColumn rc in tbItems.Columns)
                            rc.ReadOnly = false;

                        foreach (DataRow rw in tbItems.Rows)
                        {
                            if (ln_item == Convert.ToInt32(rw["PD_CODIGO"]))
                            {
                                rw["PD_CONCEPTO"] = (e.Item.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                                rw["TTDESCRI"] = (e.Item.FindControl("rc_concepto") as RadComboBox).Text;

                                rw["PD_TIPO"] = (e.Item.FindControl("rc_tipo") as RadComboBox).SelectedValue;
                                rw["TIPOSR"] = (e.Item.FindControl("rc_tipo") as RadComboBox).Text;
                                rw["PD_TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).SelectedValue;
                                rw["TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).Text;

                                rw["PD_VALOR"] = (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value;
                                rw["PC_NATURALEZA"] = ".";
                                rw["PC_ID"] = 0;
                                //rw["PC_CODIGO"] = (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries[0].Value;
                                //rw["PC_NOMBRE"] = (e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries[0].Text;

                                rw["PD_BASE"] = "N";

                                if ((e.Item.FindControl("chk_indbase") as CheckBox).Checked)
                                    rw["PD_BASE"] = "S";

                                //row["PI_BASE"] = (e.Item.FindControl("txt_base") as RadNumericTextBox).Value;

                                rw["PD_CONCEPTO"] = (e.Item.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                            }
                        }
                        break;
                    case "Delete":
                        int ln_item_ = Convert.ToInt32((e.Item as GridEditableItem).GetDataKeyValue("PI_ITEM"));
                        Obj.DeletePlanillaImpuestosDT(null, ln_item_);
                        break;
                    case "Edit":
                        break;
                }
                (sender as RadGrid).DataBind();
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
        protected void obj_planimp_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDT"] = tbItems;
        }
        protected void obj_planimp_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDT"] = tbItems;
        }
        protected void obj_planims_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            else
            {
                litTextoMensaje.Text = "Nro Planilla :" + Convert.ToString(e.ReturnValue);
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rlv_planimp_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rg_items_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                //DataRow fila = ((DataRowView)((e.Item)).DataItem).Row;
                //(e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(0, new AutoCompleteBoxEntry(Convert.ToString(fila["PC_CODIGO"]), Convert.ToString(fila["PC_CODIGO"])));
                //(e.Item.FindControl("ac_ctacontable") as RadAutoCompleteBox).Entries.Insert(0, new AutoCompleteBoxEntry(Convert.ToString(fila["PC_NOMBRE"]), Convert.ToString(fila["PC_NOMBRE"])));
            }
        }
    }
}