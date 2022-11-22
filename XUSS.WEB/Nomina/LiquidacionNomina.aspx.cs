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
using XUSS.BLL.Nomina;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Nomina
{
    public partial class LiquidacionNomina : System.Web.UI.Page
    {
        private DataTable tbTerceros
        {
            set { ViewState["tbTerceros"] = value; }
            get { return ViewState["tbTerceros"] as DataTable; }
        }
        private DataTable tbNovedades
        {
            set { ViewState["tbNovedades"] = value; }
            get { return ViewState["tbNovedades"] as DataTable; }
        }
        private DataTable tbLiquidacion
        {
            set { ViewState["tbLiquidacion"] = value; }
            get { return ViewState["tbLiquidacion"] as DataTable; }
        }
        private DataTable tbPeriodo
        {
            set { ViewState["tbPeriodo"] = value; }
            get { return ViewState["tbPeriodo"] as DataTable; }
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
            this.OcultarPaginador(rlv_liquidacionnom, "RadDataPager1", "BotonesBarra");
        }

        protected void rg_terceros_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbTerceros;
        }

        protected void rc_periodo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TercerosBL ObjT = new TercerosBL();
            LiquidacionNominaBL ObjL = new LiquidacionNominaBL();
            PeriodosNominaBL ObjP = new PeriodosNominaBL();
            try {
                if (ObjL.GetPlanillaNominaHD(null, " NMP_CODIGO="+(sender as RadComboBox).SelectedValue, 0, 0).Rows.Count > 0)
                {
                    (sender as RadComboBox).SelectedValue ="-1";
                    litTextoMensaje.Text = "Perido Seleccionado Ya Existe!";                    
                    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    tbTerceros = ObjT.GetTercerosPlanillaNomina(null, " AND 1=1");
                    tbPeriodo = ObjP.GetPeriodoNomina(null, " NMP_CODIGO=" + Convert.ToString((sender as RadComboBox).SelectedValue), 0, 0);
                    tbLiquidacion = ObjL.GetPlanillaNominaDT(null, -1);
                    (((RadComboBox)sender).Parent.FindControl("rg_terceros") as RadGrid).DataSource = tbTerceros;
                    (((RadComboBox)sender).Parent.FindControl("rg_terceros") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjT = null;
                ObjL = null;
                ObjP = null;
            }
        }
        protected void rlv_liquidacionnom_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    TercerosBL objt = new TercerosBL();
                    LiquidacionNominaBL objL = new LiquidacionNominaBL();
                    try
                    {
                        //tbFamilia = objt.GetFamilia(null, Convert.ToString(Session["CODEMP"]), 0);         
                        tbTerceros = objt.GetTercerosPlanillaNomina(null, " AND 1=0");
                        tbNovedades = objL.GetNovedades(null, -1);

                        ViewState["isClickInsert"] = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objt = null;
                        objL = null;
                    }
                    break;

                case "Buscar":
                    obj_planillanmHD.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_liquidacionnom.DataBind();
                    break;                
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_liquidacionnom_ItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    TercerosBL objt = new TercerosBL();
                    LiquidacionNominaBL objL = new LiquidacionNominaBL();
                    PeriodosNominaBL ObjP = new PeriodosNominaBL();
                    try
                    {
                        tbTerceros = objt.GetTercerosPlanillaNomina(null, "AND TERCEROS.TRCODTER IN (SELECT TRCODTER FROM NM_LIQUIDACIONDT WITH(NOLOCK) WHERE NMH_CODIGO = "+ rlv_liquidacionnom.Items[0].GetDataKeyValue("NMH_CODIGO").ToString() + ")");
                        (e.Item.FindControl("rg_terceros") as RadGrid).DataSource = tbTerceros;
                        (e.Item.FindControl("rg_terceros") as RadGrid).DataBind();

                        tbNovedades = objL.GetNovedades(null, Convert.ToInt32(fila["NMP_CODIGO"]));
                        (e.Item.FindControl("rg_novedades") as RadGrid).DataSource = tbNovedades;
                        (e.Item.FindControl("rg_novedades") as RadGrid).DataBind();

                        tbLiquidacion = objL.GetPlanillaNominaDT(null, Convert.ToInt32(rlv_liquidacionnom.Items[0].GetDataKeyValue("NMH_CODIGO").ToString()));
                        (e.Item.FindControl("rp_planilla") as RadPivotGrid).DataSource = tbLiquidacion;
                        (e.Item.FindControl("rp_planilla") as RadPivotGrid).DataBind();

                        tbPeriodo = ObjP.GetPeriodoNomina(null, " NMP_CODIGO=" + Convert.ToString(fila["NMP_CODIGO"]), 0, 0);


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objt = null;
                        objL = null;
                        ObjP = null;
                    }
                }
            }
        }
        protected void rg_novedades_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbNovedades;
        }
        protected void btn_prceosar_Click(object sender, EventArgs e)
        {
            TercerosBL ObjT = new TercerosBL();
            PlanillaConceptosNMBL ObjP = new PlanillaConceptosNMBL();
            DateTime ld_feccontrato=System.DateTime.Today;
            DateTime? ld_fefincontrato = null;
            try
            {
                tbTerceros = ObjT.GetTercerosPlanillaNomina(null, " AND 1=1 ");                 //AND TERCEROS.TRCODTER= 25621
                (((RadButton)sender).Parent.FindControl("rg_terceros") as RadGrid).DataSource = tbTerceros;
                (((RadButton)sender).Parent.FindControl("rg_terceros") as RadGrid).DataBind();

                tbLiquidacion.Rows.Clear();

                foreach (DataRow rt in tbTerceros.Rows)
                {
                    double ln_salario = 0;                    
                    //Carga Contratos
                    foreach (DataRow rc in ObjT.GetContratos(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rt["TRCODTER"])).Rows)
                    {
                        ld_feccontrato = Convert.ToDateTime(rc["CT_FINGRESO"]);
                        ld_fefincontrato = (rc["CT_FTERMINACION"] != DBNull.Value) ? Convert.ToDateTime(rc["CT_FTERMINACION"]) : (DateTime?)null;
                        DataRow rl = tbLiquidacion.NewRow();

                        rl["ORIGEN"] = "Salario";
                        rl["NMD_ORIGEN"] = "1";

                        rl["NMD_CODIGO"] = tbLiquidacion.Rows.Count + 1;
                        rl["NMH_CODIGO"] = 0;
                        rl["TRCODTER"] = rt["TRCODTER"];
                        rl["TRCODNIT"] = rt["TRCODNIT"];                        
                        rl["EMPLEADO"] = rt["EMPLEADO"];
                        rl["PD_CODIGO"] = 10;
                        rl["CONCEPTO"] = "Salario";
                        
                        foreach (DataRow rp in tbPeriodo.Rows)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(rc["CT_FTERMINACION"])))
                            {
                                if (Convert.ToDateTime(rp["NMP_FECINI"]) < Convert.ToDateTime(rc["CT_FTERMINACION"]))
                                {
                                    if (Convert.ToDateTime(rc["CT_FINGRESO"]) <= Convert.ToDateTime(rp["NMP_FECINI"]))
                                        if (Convert.ToDateTime(rc["CT_FTERMINACION"]) >= Convert.ToDateTime(rp["NMP_FECFIN"]))
                                            ln_salario = (Convert.ToDateTime(rp["NMP_FECFIN"]) - Convert.ToDateTime(rp["NMP_FECINI"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rc["CT_SALARIO"]) / 30, 2);
                                        else
                                            ln_salario = (Convert.ToDateTime(rc["CT_FTERMINACION"]) - Convert.ToDateTime(rp["NMP_FECINI"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rc["CT_SALARIO"]) / 30, 2);
                                    else
                                    {
                                        if (Convert.ToDateTime(rc["CT_FINGRESO"]) >= Convert.ToDateTime(rp["NMP_FECFIN"]))
                                            ln_salario = 0;
                                        else
                                            ln_salario = (Convert.ToDateTime(rc["CT_FTERMINACION"]) - Convert.ToDateTime(rc["CT_FINGRESO"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rc["CT_SALARIO"]) / 30, 2);
                                    }
                                }
                            }
                            else
                            {                                
                                    if (Convert.ToDateTime(rc["CT_FINGRESO"]) <= Convert.ToDateTime(rp["NMP_FECINI"]))
                                        ln_salario = (Convert.ToDateTime(rp["NMP_FECFIN"]) - Convert.ToDateTime(rp["NMP_FECINI"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rc["CT_SALARIO"]) / 30, 2);
                                    else
                                    {
                                        if (Convert.ToDateTime(rc["CT_FINGRESO"]) >= Convert.ToDateTime(rp["NMP_FECFIN"]))
                                            ln_salario = 0;
                                        else
                                            ln_salario = (Convert.ToDateTime(rp["NMP_FECFIN"]) - Convert.ToDateTime(rc["CT_FINGRESO"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rc["CT_SALARIO"]) / 30, 2);
                                    }                            
                            }
                            
                        }
                        rl["NMD_VALOR"] = ln_salario;                        
                        rl["NMD_ESTADO"] = "AC";
                        rl["NMD_FECING"] = System.DateTime.Today;
                        tbLiquidacion.Rows.Add(rl);
                        rl = null;
                    }
                    //Planilla de Liquidacion
                    foreach (DataRow rp in ObjP.GetPlanillasxTercero(null, Convert.ToInt32(rt["TRCODTER"])).Rows)
                    {
                        foreach (DataRow rdp in ObjP.GetPlanillaConceptosDT(null, Convert.ToInt32(rp["PH_CODPLAN"])).Rows)
                        {
                            DataRow rl = tbLiquidacion.NewRow();
                            rl["ORIGEN"] = "Planilla";
                            rl["NMD_ORIGEN"] = "2";

                            rl["NMD_CODIGO"] = tbLiquidacion.Rows.Count + 1;
                            rl["NMH_CODIGO"] = 0;
                            rl["TRCODTER"] = rt["TRCODTER"];
                            rl["TRCODNIT"] = rt["TRCODNIT"];
                            rl["EMPLEADO"] = rt["EMPLEADO"];
                            rl["PD_CODIGO"] = rdp["PD_CONCEPTO"];
                            rl["CONCEPTO"] = rdp["TTDESCRI"]; 
                            double ln_valor = 0;
                            if (Convert.ToString(rdp["PD_BASE"]) == "S")
                            {
                                if (Convert.ToString(rdp["PD_TIPOPV"]) == "P")
                                    ln_valor = (Convert.ToDouble(rdp["PD_VALOR"]) * ln_salario) / 100;
                                else
                                {
                                    foreach (DataRow rpp in tbPeriodo.Rows)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(ld_fefincontrato)))
                                        {
                                            if (Convert.ToDateTime(rpp["NMP_FECINI"]) < Convert.ToDateTime(ld_fefincontrato))
                                            {
                                                if (ld_feccontrato <= Convert.ToDateTime(rpp["NMP_FECINI"]))
                                                    ln_valor = (Convert.ToDateTime(ld_fefincontrato) - Convert.ToDateTime(rpp["NMP_FECINI"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rdp["PD_VALOR"]) / 30, 2);
                                                else
                                                {
                                                    if (ld_feccontrato >= Convert.ToDateTime(rpp["NMP_FECFIN"]))
                                                        ln_valor = 0;
                                                    else
                                                        ln_valor = (Convert.ToDateTime(ld_fefincontrato) - ld_feccontrato).Days * Math.Round(Convert.ToDouble(rdp["PD_VALOR"]) / 30, 2);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (ld_feccontrato <= Convert.ToDateTime(rpp["NMP_FECINI"]))
                                                ln_valor = (Convert.ToDateTime(rpp["NMP_FECFIN"]) - Convert.ToDateTime(rpp["NMP_FECINI"]).AddDays(-1)).Days * Math.Round(Convert.ToDouble(rdp["PD_VALOR"]) / 30, 2);
                                            else
                                            {
                                                if (ld_feccontrato >= Convert.ToDateTime(rpp["NMP_FECFIN"]))
                                                    ln_valor = 0;
                                                else
                                                    ln_valor = (Convert.ToDateTime(rpp["NMP_FECFIN"]) - ld_feccontrato).Days * Math.Round(Convert.ToDouble(rdp["PD_VALOR"]) / 30, 2);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (Convert.ToString(rdp["PD_TIPOPV"]) == "V")
                                    ln_valor = Convert.ToDouble(rdp["PD_VALOR"]);
                            }

                            if (Convert.ToString(rdp["PD_TIPO"]) == "R")
                                ln_valor = ln_valor * -1;

                            rl["NMD_VALOR"] = ln_valor;
                            rl["NMD_ESTADO"] = "AC";
                            rl["NMD_FECING"] = System.DateTime.Today;
                            tbLiquidacion.Rows.Add(rl);
                            rl = null;
                        }
                    }
                    
                }
                //Novedades
                foreach (DataRow rn in tbNovedades.Rows)
                {
                    DataRow rl = tbLiquidacion.NewRow();
                    rl["ORIGEN"] = "Novedad";
                    rl["NMD_ORIGEN"] = "3";

                    rl["NMD_CODIGO"] = tbLiquidacion.Rows.Count + 1;
                    rl["NMH_CODIGO"] = 0;
                    rl["TRCODTER"] = rn["TRCODTER"];
                    rl["TRCODNIT"] = rn["TRCODNIT"];
                    rl["EMPLEADO"] = rn["EMPLEADO"];
                    rl["PD_CODIGO"] = rn["NV_CONCEPTO"];
                    rl["CONCEPTO"] = rn["TTDESCRI"];
                    double ln_valor = 0;
                    if (Convert.ToString(rn["NV_TIPOPV"]) == "V")
                        ln_valor = Convert.ToDouble(rn["NV_VALOR"]);

                    if (Convert.ToString(rn["NV_TIPOSR"]) == "R")
                        ln_valor = ln_valor * -1;

                    rl["NMD_VALOR"] = ln_valor;
                    rl["NMD_ESTADO"] = "AC";
                    rl["NMD_FECING"] = System.DateTime.Today;
                    tbLiquidacion.Rows.Add(rl);
                    rl = null;

                }

                for (int i = tbLiquidacion.Rows.Count - 1; i >= 0;i--)
                {
                    DataRow dr = tbLiquidacion.Rows[i];
                    if (Convert.ToDouble(dr["NMD_VALOR"]) == 0)
                        dr.Delete();
                }
                tbLiquidacion.AcceptChanges();

                (((RadButton)sender).Parent.FindControl("rp_planilla") as RadPivotGrid).DataSource = tbLiquidacion;
                (((RadButton)sender).Parent.FindControl("rp_planilla") as RadPivotGrid).DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjT = null;
                ObjP = null;
            }
        }
        protected void obj_planillanmHD_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDT"] = tbLiquidacion;
            e.InputParameters["inNovedades"] = tbNovedades;
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
        protected void rg_novedades_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PerformInsert":
                    DataRow row = tbNovedades.NewRow();
                    row["NMP_CODIGO"] = 0;
                    row["NV_CODIGO"] = tbNovedades.Rows.Count + 1;                    

                    row["TRCODNIT"] = (e.Item.FindControl("txt_nit") as RadTextBox).Text;
                    row["TRCODTER"] = (e.Item.FindControl("txt_codter") as RadTextBox).Text;
                    row["EMPLEADO"] = (e.Item.FindControl("txt_tercero") as RadTextBox).Text;

                    row["NV_CONCEPTO"] = (e.Item.FindControl("rc_concepto") as RadComboBox).SelectedValue;
                    row["TTDESCRI"] = (e.Item.FindControl("rc_concepto") as RadComboBox).Text;

                    row["NV_TIPOSR"] = (e.Item.FindControl("rc_tipo") as RadComboBox).SelectedValue;
                    row["D_TIPOSR"] = (e.Item.FindControl("rc_tipo") as RadComboBox).Text;
                    row["NV_TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).SelectedValue;
                    row["D_TIPOPV"] = (e.Item.FindControl("rc_tipopv") as RadComboBox).Text;

                    row["NV_VALOR"] = (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value;                    
                    row["NV_BASE"] = "N";

                    if ((e.Item.FindControl("chk_indbase") as CheckBox).Checked)
                        row["NV_BASE"] = "S";

                    //row["PI_BASE"] = (e.Item.FindControl("txt_base") as RadNumericTextBox).Value;
                    
                    row["NV_ESTADO"] = "AC";
                    row["NV_USUARIO"] = ".";
                    row["NV_FECING"] = System.DateTime.Today;                    

                    tbNovedades.Rows.Add(row);
                    row = null;

                    break;
            }
            }
        protected void txt_codter_TextChanged(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            try
            {
                if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
                {
                    using (IDataReader reader = Obj.GetTercerosR(null, " TRCODNIT='" + (sender as RadTextBox).Text + "'", 0, 0))
                    {
                        while (reader.Read())
                        {
                            (((RadTextBox)sender).Parent.FindControl("txt_tercero") as RadTextBox).Text = Convert.ToString(reader["TRNOMBRE"]) + " " + Convert.ToString(reader["TRNOMBR2"]) + " " + Convert.ToString(reader["TRAPELLI"]);
                            (((RadTextBox)sender).Parent.FindControl("txt_codter") as RadTextBox).Text = Convert.ToString(reader["TRCODTER"]);
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
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9014&inban=S&inParametro=inConsecutivo&inValor="+ rlv_liquidacionnom.Items[0].GetDataKeyValue("NMH_CODIGO").ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = " AND 1=1";

            if ((((RadButton)sender).Parent.FindControl("rc_periodo") as RadComboBox).SelectedValue !="-1")
                filtro += " AND NMP_CODIGO = " + (((RadButton)sender).Parent.FindControl("rc_periodo") as RadComboBox).SelectedValue;

            if ((((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue != "-1")
                filtro += " AND NMH_ESTADO = '" + (((RadButton)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_planillanmHD.SelectParameters["filter"].DefaultValue = filtro;
            rlv_liquidacionnom.DataBind();
            if ((rlv_liquidacionnom.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_liquidacionnom.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void rg_novedades_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    foreach (DataColumn rc in tbNovedades.Columns)
                        rc.ReadOnly = false;

                    var NV_CODIGO = item.GetDataKeyValue("NV_CODIGO").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbNovedades.Rows)
                    {
                        if (Convert.ToInt32(row["NV_CODIGO"]) == Convert.ToInt32(NV_CODIGO))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbNovedades.Rows[pos].Delete();
                    tbNovedades.AcceptChanges();

                    foreach (DataRow rw in tbNovedades.Rows)
                    {
                        rw["NV_CODIGO"] = i;
                        i++;
                    }

                    break;
            }
        }

        protected void obj_planillanmHD_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDT"] = tbLiquidacion;
            e.InputParameters["inNovedades"] = tbNovedades;
        }

        protected void rg_terceros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Documento=" + (item_.FindControl("lbl_codter") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
            }
        }
    }
}