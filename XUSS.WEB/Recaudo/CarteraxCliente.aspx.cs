using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Recaudo
{
    public partial class CarteraxCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                edt_fecha.SelectedDate = System.DateTime.Today;
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string lc_filter = "";
            
            if (!chk_saldo.Checked)
                lc_filter += " AND SALDO > 0";
            if (!string.IsNullOrEmpty(txt_icliente.Text))
                lc_filter += " AND TRCODNIT ='" + txt_icliente.Text + "'";
            if (!string.IsNullOrEmpty(txt_nombre.Text))
                lc_filter += " AND CLIENTE LIKE '%" + txt_nombre.Text + "%'";
            if (rc_agente.SelectedValue != "-1")
                lc_filter += " AND TRAGENTE = " + Convert.ToString(rc_agente.SelectedValue) + "";

            obj_cartera.SelectParameters["inFecha"].DefaultValue = Convert.ToString(edt_fecha.SelectedDate);
            obj_cartera.SelectParameters["inFiltro"].DefaultValue = lc_filter;
            rg_consulta.DataBind();
        }
        protected void rg_consulta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string url = "";

            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                switch (e.CommandName)
                {
                    case "tercero":
                        
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Codigo=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);                        
                        break;
                    case "saldo":                        
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=100&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "Recaudo":                        
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=101&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "lkn_nc":                        
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=104&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "lkn_nd":                        
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=105&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                }
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
}