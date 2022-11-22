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
using XUSS.BLL.Correspondencia;

namespace XUSS.WEB.Correspondencia
{
    public partial class PlanillaDesmontes : System.Web.UI.Page
    {
        private DataTable tbDetalle
        {
            set { ViewState["tbDetalle"] = value; }
            get { return ViewState["tbDetalle"] as DataTable; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
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
            this.OcultarPaginador(rlv_planillad, "RadDataPager1", "BotonesBarra");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }

        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_planilla") as RadTextBox).Text))
                filtro += " AND PDH_CODIGO = " + (((RadButton)sender).Parent.FindControl("txt_planilla") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_correspondenciaIn.SelectParameters["filter"].DefaultValue = filtro;
            rlv_planillad.DataBind();
            if ((rlv_planillad.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_planillad.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void rlv_planillad_ItemInserted(object sender, Telerik.Web.UI.RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        protected void rlv_planillad_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    PlanillaDesmontesBL Obj = new PlanillaDesmontesBL();
                    try
                    {
                        tbDetalle = Obj.GetPlanillaDesmonteDT(null, 0);

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
                    obj_correspondenciaIn.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_planillad.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void rlv_planillad_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
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
                    PlanillaDesmontesBL Obj = new PlanillaDesmontesBL();
                    try
                    {
                        tbDetalle = Obj.GetPlanillaDesmonteDT(null, Convert.ToInt32(rlv_planillad.Items[0].GetDataKeyValue("PDH_CODIGO").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbDetalle;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
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

        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            PlanillaDesmontesBL Obj = new PlanillaDesmontesBL();
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":                        
                        rg_items_Asistente.DataSource = Obj.GetCuentasRestantes(null, " AND 1=0");
                        rg_items_Asistente.DataBind();
                        rc_comercial.SelectedValue = "-1";
                        rc_proyecto.SelectedValue = "-1";
                        txt_nroaptofiltro.Text = "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalAsistente.ClientID + "\").show(); $find(\"" + rc_comercial.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        e.Canceled = true;
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
        protected void rg_items_DeleteCommand(object sender, GridCommandEventArgs e)
        {

        }
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbDetalle;
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
        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem item in rg_items_Asistente.Items)
                {
                    if ((item.FindControl("chk_ind") as CheckBox).Checked)
                    {
                        foreach (DataRow rw in tbItems.Rows)
                        {
                            if (Convert.ToString(rw["PH_EDIFICIO"]) == Convert.ToString(item["PH_EDIFICIO"].Text) && Convert.ToString(rw["PH_ESCALERA"]) == Convert.ToString(item["PH_ESCALERA"].Text)
                                && (Convert.ToString(item["PH_PORTAL"].Text) == "&nbsp;" || Convert.ToString(rw["PH_PORTAL"]) == Convert.ToString(item["PH_PORTAL"].Text) ))
                            {
                                DataRow row = tbDetalle.NewRow();
                                row["PDD_CODIGO"] = tbDetalle.Rows.Count + 1;
                                row["PDH_CODIGO"] = 1;
                                row["PDD_TECNICO"] = rc_comercial.SelectedValue;
                                row["PH_CODIGO"] = Convert.ToInt32(rw["PH_CODIGO"]);
                                row["PDD_TUERCAPLANA"] = ((RadNumericTextBox)item.FindControl("txt_tplana")).DbValue;
                                row["PDD_COPASCONICAS"] = ((RadNumericTextBox)item.FindControl("txt_cconica")).DbValue;
                                row["PDD_RACORFLARES"] = ((RadNumericTextBox)item.FindControl("txt_rflares")).DbValue;
                                row["PDD_VALVULAALIVIO"] = ((RadNumericTextBox)item.FindControl("txt_vlvalivio")).DbValue;
                                row["PDD_VALVULAAGUA"] = ((RadNumericTextBox)item.FindControl("txt_vlvagua")).DbValue;
                                row["PDD_CHEQUE"] = ((RadNumericTextBox)item.FindControl("txt_cheque")).DbValue;
                                row["PDD_CODOGALVANIZADO"] = ((RadNumericTextBox)item.FindControl("txt_codo90")).DbValue;
                                row["PDD_CODOCALLE"] = ((RadNumericTextBox)item.FindControl("txt_codocalle")).DbValue;
                                row["PDD_MGFLEXOMETALICA"] = ((RadNumericTextBox)item.FindControl("txt_felxometa")).DbValue;
                                row["PDD_COBREMT"] = ((RadNumericTextBox)item.FindControl("txt_cobrecm")).DbValue;
                                row["TRNOMBRE"] = rc_proyecto.Text;
                                row["PH_EDIFICIO"] = item["PH_EDIFICIO"].Text;
                                row["PH_ESCALERA"] = item["PH_ESCALERA"].Text;
                                row["PH_PISO"] = item["PH_PISO"].Text;
                                row["PH_PORTAL"] = item["PH_PORTAL"].Text;
                                row["MECDELEM"] = item["MECDELEM"].Text;
                                row["ARNOMBRE"] = item["ARNOMBRE"].Text;

                                tbDetalle.Rows.Add(row);
                                row = null;
                            }
                        }
                    }
                }

                (rlv_planillad.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbDetalle;
                (rlv_planillad.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }        
        protected void btn_filtro_detalle_Click(object sender, EventArgs e)
        {
            string lc_filtro = "";
            PlanillaDesmontesBL Obj = new PlanillaDesmontesBL();
            try
            {
                if (rc_proyecto.SelectedValue != "-1")
                {
                    lc_filtro = " AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT PH_CODIGO FROM TB_DESISTALACION WITH(NOLOCK) ) AND TB_PROPIEDAHORIZONTAL.TRCODTER=" + Convert.ToString(rc_proyecto.SelectedValue) + " AND TB_PROPIEDAHORIZONTAL.PH_ESCALERA='" + txt_nroaptofiltro.Text+ "'";

                    tbItems = Obj.GetCuentasRestantes(null, lc_filtro);
                    rg_items_Asistente.DataSource = tbItems;
                    rg_items_Asistente.DataBind();
                }
                string script = "function f(){$find(\"" + modalAsistente.ClientID + "\").show(); $find(\"" + rc_comercial.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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

        protected void obj_correspondenciaIn_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbDetalle;
        }

        protected void obj_correspondenciaIn_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Planilla :" + Convert.ToString(e.ReturnValue) + " Confirmado!";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7009&inban=S&inParametro=consecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9019&inban=S&inClo=S&inParametro=inNumero&inValor=" + Convert.ToString(e.ReturnValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }

        protected void rlv_planillad_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PDD_CODIGO"].Text);
                foreach (DataRow rw in tbDetalle.Rows)
                {
                    if (ln_codigo == Convert.ToInt32(rw["PDD_CODIGO"]))
                    {
                        //rw["CID_CAUSAE"] = Convert.ToString((item.FindControl("rc_testado") as RadComboBox).SelectedValue);
                        rw["PDD_TUERCAPLANA"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_tplana")).DbValue);
                        rw["PDD_COPASCONICAS"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_cconica")).DbValue);
                        rw["PDD_RACORFLARES"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_rflares")).DbValue);
                        rw["PDD_VALVULAALIVIO"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_vlvalivio")).DbValue);
                        rw["PDD_VALVULAAGUA"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_vlvagua")).DbValue);
                        rw["PDD_CHEQUE"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_cheque")).DbValue);
                        rw["PDD_CODOGALVANIZADO"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_codo90")).DbValue);
                        rw["PDD_CODOCALLE"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_codocalle")).DbValue);
                        rw["PDD_MGFLEXOMETALICA"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_felxometa")).DbValue);
                        rw["PDD_COBREMT"] = Convert.ToInt32(((RadNumericTextBox)item.FindControl("txt_cobrecm")).DbValue);

                    }
                }
            }
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9019&inban=S&inClo=S&inParametro=inNumero&inValor=" + (rlv_planillad.Items[0].FindControl("txt_planilla") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
    }
}