using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public partial class CorrespondenciaIn : System.Web.UI.Page
    {
        private string lc_tipo { 
            set { ViewState["lc_tipo"] = value; }
            get { return ViewState["lc_tipo"] as string; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbDetalle
        {
            set { ViewState["tbDetalle"] = value; }
            get { return ViewState["tbDetalle"] as DataTable; }
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
            this.OcultarPaginador(rlv_corrrespondeciaIn, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_corrrespondeciaIn_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    CorrespondenciaBL Obj = new CorrespondenciaBL();
                    try
                    {
                        tbItems = Obj.GetCorrespondenciDTIN(null, Convert.ToInt32(rlv_corrrespondeciaIn.Items[0].GetDataKeyValue("CIH_CODIGO").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
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
        protected void rlv_corrrespondeciaIn_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {            
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    CorrespondenciaBL Obj = new CorrespondenciaBL();
                    try
                    {
                        tbItems = Obj.GetCorrespondenciDTIN(null, 0);

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
                    rlv_corrrespondeciaIn.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_planilla") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO = " + (((RadButton)sender).Parent.FindControl("txt_planilla") as RadTextBox).Text + "";

            if ((((RadButton)sender).Parent.FindControl("rc_proyectof") as RadComboBox).Text != "Seleccionar" && (((RadButton)sender).Parent.FindControl("rc_proyectof") as RadComboBox).Text != "")
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN (SELECT PH_CODIGO FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE TRCODTER = " + (((RadButton)sender).Parent.FindControl("rc_proyectof") as RadComboBox).SelectedValue + "))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_COMERCIAL WITH(NOLOCK) INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_COMERCIAL.TRCODTER = TERCEROS.TRCODTER AND TB_COMERCIAL.CO_CODEMP = TERCEROS.TRCODEMP) WHERE (TRNOMBRE + ' ' + ISNULL(TRNOMBR2, '') + ' ' + ISNULL(TRAPELLI, '') + ' ' + ISNULL(TRNOMBR3, '')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_COMERCIAL WITH(NOLOCK) INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_COMERCIAL.TRCODTER = TERCEROS.TRCODTER AND TB_COMERCIAL.CO_CODEMP = TERCEROS.TRCODEMP) WHERE TRCODNIT= '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_telefono") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_COMERCIAL WITH(NOLOCK) INNER JOIN TERCEROS WITH(NOLOCK) ON(TB_COMERCIAL.TRCODTER = TERCEROS.TRCODTER AND TB_COMERCIAL.CO_CODEMP = TERCEROS.TRCODEMP) WHERE TRNROTEL= '" + (((RadButton)sender).Parent.FindControl("txt_telefono") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_torre") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE PH_EDIFICIO = '" + (((RadButton)sender).Parent.FindControl("txt_torre") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apto") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE PH_ESCALERA = '" + (((RadButton)sender).Parent.FindControl("txt_apto") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_poliza") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE PH_POLIZA = '" + (((RadButton)sender).Parent.FindControl("txt_poliza") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_ctacontrato") as RadTextBox).Text))
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE PH_CODIGO IN(SELECT PH_CODIGO FROM TB_PROPIEDAHORIZONTAL WITH(NOLOCK) WHERE PH_CTACONTRATO = '" + (((RadButton)sender).Parent.FindControl("txt_ctacontrato") as RadTextBox).Text + "'))";

            if ((((RadButton)sender).Parent.FindControl("rc_comercial") as RadComboBox).Text != "Seleccionar" && (((RadButton)sender).Parent.FindControl("rc_comercial") as RadComboBox).Text != "")
                filtro += " AND CIH_CODIGO IN (SELECT CIH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK)  WHERE CID_ASESOR = '" + (((RadButton)sender).Parent.FindControl("rc_comercial") as RadComboBox).SelectedValue + "')";

            

            if (!string.IsNullOrWhiteSpace(filtro))                
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_correspondenciaIn.SelectParameters["filter"].DefaultValue = filtro;
            rlv_corrrespondeciaIn.DataBind();
            if ((rlv_corrrespondeciaIn.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_corrrespondeciaIn.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CorrespondenciaBL Obj = new CorrespondenciaBL();
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        lc_tipo = "1";
                        rg_items_Asistente.DataSource = Obj.GetCtasRestantes(null, 0,"");
                        rg_items_Asistente.DataBind();
                        rc_comercial.SelectedValue = "-1";
                        rc_proyecto.SelectedValue = "-1";                            
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalAsistente.ClientID + "\").show(); $find(\"" + rc_comercial.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        e.Canceled = true;                    
                        break;
                    case "Insertdemt":
                        lc_tipo = "2";
                        rg_items_Asistente.DataSource = Obj.GetCtasRestantes(null, 0,"");
                        rg_items_Asistente.DataBind();
                        rc_comercial.SelectedValue = "-1";
                        rc_proyecto.SelectedValue = "-1";                        
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalAsistente.ClientID + "\").show(); $find(\"" + rc_comercial.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        e.Canceled = true;
                        break;
                    case "InsertCorr":
                        lc_tipo = "3";
                        rg_items_Asistente.DataSource = Obj.GetCtasRestantes(null, 0,"");
                        rg_items_Asistente.DataBind();
                        rc_comercial.SelectedValue = "-1";
                        rc_proyecto.SelectedValue = "-1";
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
            //this.AnalizarCommand(e.CommandName);
        }
        protected void btn_add_Click(object sender, EventArgs e)
        {            
            try
            {
                foreach (GridDataItem item in rg_items_Asistente.Items)
                {
                    if ((item.FindControl("chk_ind") as CheckBox).Checked)
                    {
                        foreach (DataRow rw in tbDetalle.Rows)
                        {
                            if (Convert.ToString(rw["PH_EDIFICIO"]) == Convert.ToString(item["PH_EDIFICIO"].Text) && Convert.ToString(rw["PH_ESCALERA"]) == Convert.ToString(item["PH_ESCALERA"].Text))
                            {
                                DataRow row = tbItems.NewRow();
                                row["CIH_CODIGO"] = 0;
                                row["CID_ITEM"] = tbItems.Rows.Count + 1;
                                row["PH_CODIGO"] = Convert.ToString((item.FindControl("lbl_propiedad") as Label).Text);                                
                                row["CID_ASESOR"] = rc_comercial.SelectedValue;
                                //row["CID_ASESOR"] = rc_comercial.SelectedValue;
                                row["CID_USUARIO"] = ".";
                                row["CID_CAUSAE"] = ".";
                                
                                row["CID_ESTADO"] = "AC";
                                if (lc_tipo == "3")
                                    row["CID_ESTADO"] = "AN";

                                row["CID_FECING"] = System.DateTime.Now;
                                row["CID_FECMOD"] = System.DateTime.Now;

                                if (item["PH_POLIZA"].Text != "&nbsp;")
                                    row["PH_POLIZA"] = Convert.ToString(item["PH_POLIZA"].Text);
                                if (item["PH_CTACONTRATO"].Text != "&nbsp;")
                                    row["PH_CTACONTRATO"] = Convert.ToString(item["PH_CTACONTRATO"].Text);
                                row["TRNOMBRE"] = rc_proyecto.Text;
                                row["TRDIRECC"] = "";
                                row["PH_EDIFICIO"] = Convert.ToString(item["PH_EDIFICIO"].Text);
                                row["PH_ESCALERA"] = Convert.ToString(item["PH_ESCALERA"].Text);                                
                                row["CO_PRECIO"] = Convert.ToDouble((item.FindControl("txt_precio") as RadNumericTextBox).Value);
                                if (item["CO_CUOTAS"].Text != "&nbsp;")
                                    row["CO_CUOTAS"] = Convert.ToInt32(item["CO_CUOTAS"].Text);
                                row["MECDELEM"] = Convert.ToString(item["MECDELEM"].Text);
                                row["ARNOMBRE"] = Convert.ToString(item["ARNOMBRE"].Text);
                                //if (item["CO_FECHA"].Text != "&nbsp;")
                                row["CO_FECHA"] = System.DateTime.Now;
                                    //Convert.ToDateTime(item["CO_FECHA"].Text);
                                //if (item["CO_FECCOMODATO"].Text != "&nbsp;")
                                    row["CO_FECCOMODATO"] = System.DateTime.Now;
                                //Convert.ToDateTime(item["CO_FECCOMODATO"].Text);
                                row["CLIENTE"] = Convert.ToString(item["CLIENTE"].Text);
                                row["TRCODNIT"] = Convert.ToString(item["TRCODNIT"].Text);                                

                                tbItems.Rows.Add(row);
                                row = null;
                            }
                        }
                    }
                }

                (rlv_corrrespondeciaIn.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_corrrespondeciaIn.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
            
            }
        }
        protected void rc_proyecto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string lc_filtro = "";
            CorrespondenciaBL Obj = new CorrespondenciaBL();
            try
            {
                if ((sender as RadComboBox).SelectedValue != "-1")
                {
                    if (lc_tipo == "1")
                        lc_filtro = " AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_CORRESPONDENCIADTIN.PH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE CID_ESTADO NOT IN ('AN') ) AND CO_CODIGO IS NOT NULL";
                    if (lc_tipo == "2")
                        lc_filtro = " AND TB_PROPIEDAHORIZONTAL.PH_CODIGO NOT IN (SELECT TB_CORRESPONDENCIADTIN.PH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE CID_ESTADO NOT IN ('AN') ) ";
                    if (lc_tipo == "3")
                        lc_filtro = " AND TB_PROPIEDAHORIZONTAL.PH_CODIGO IN (SELECT TB_CORRESPONDENCIADTIN.PH_CODIGO FROM TB_CORRESPONDENCIADTIN WITH(NOLOCK) WHERE CID_ESTADO IN ('PE') ) ";
                    

                    tbDetalle = Obj.GetCtasRestantes(null, Convert.ToInt32((sender as RadComboBox).SelectedValue),lc_filtro);
                    rg_items_Asistente.DataSource = tbDetalle;
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
        protected void rlv_corrrespondeciaIn_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
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
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9017&inban=S&inClo=S&inParametro=inNumero&inValor=" + Convert.ToString(e.ReturnValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void obj_correspondenciaIn_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbitems"] = tbItems;
        }
        protected void rlv_corrrespondeciaIn_PreRender(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ViewState["isClickInsert"]))
            {
                if (((sender as RadListView).InsertItem.FindControl("txt_Fecha") as RadDatePicker).SelectedDate == null)
                    ((sender as RadListView).InsertItem.FindControl("txt_Fecha") as RadDatePicker).SelectedDate = System.DateTime.Now;                
            }
        }
        protected void rg_items_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var codigo = item.GetDataKeyValue("CID_ITEM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["CID_ITEM"]) == Convert.ToInt32(codigo))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    foreach (DataRow row in tbItems.Rows)
                    {
                        row["CID_ITEM"] = i;
                        i++;
                    }                    
                    break;
            }
        }

        protected void rlv_corrrespondeciaIn_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            try {
                foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["CID_ITEM"].Text);
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (ln_codigo == Convert.ToInt32(rw["CID_ITEM"]))
                        {
                            rw["CID_CAUSAE"] = Convert.ToString((item.FindControl("rc_testado") as RadComboBox).SelectedValue);
                            rw["CID_ESTADO"] = "PE";
                            if (Convert.ToString((item.FindControl("rc_testado") as RadComboBox).SelectedValue) == ".")
                                rw["CID_ESTADO"] = "AC";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        protected void obj_correspondenciaIn_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbitems"] = tbItems;
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9017&inban=S&inClo=S&inParametro=inNumero&inValor=" + (rlv_corrrespondeciaIn.Items[0].FindControl("txt_planilla") as RadTextBox).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }

        protected void rg_items_PreRender(object sender, EventArgs e)
        {
            string aValue = "";            

            foreach (GridDataItem item in (sender as RadGrid).Items)
            {
                aValue = (((RadTextBox)item.FindControl("txt_estadod")).Text);
                if (aValue == "PE")
                {
                    item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                    item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                    item.ControlStyle.Font.Bold = true;
                }

                if (aValue == "AN")
                {
                    item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                    item.ControlStyle.ForeColor = System.Drawing.Color.Green;
                    item.ControlStyle.Font.Bold = true;
                }
            }
        }
    }
}