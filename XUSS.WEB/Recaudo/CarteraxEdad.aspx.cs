using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Consultas;

namespace XUSS.WEB.Recaudo
{
    public partial class CarteraxEdad : System.Web.UI.Page
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
                lc_filter = " AND SALDO > 0";
            if (!string.IsNullOrEmpty(txt_codigo.Text))
                lc_filter += " AND TRCODTER ='" + txt_codigo.Text + "'";
            if (!string.IsNullOrEmpty(txt_icliente.Text))
                lc_filter += " AND TRCODNIT ='" + txt_icliente.Text + "'";
            if (!string.IsNullOrEmpty(txt_nombre.Text))
                lc_filter += " AND CLIENTE LIKE '%" + txt_nombre.Text + "%'";
            if (!string.IsNullOrEmpty(txt_nrofac.Text))
                lc_filter += " AND HDNROFAC =" + txt_nrofac.Text;

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
                    case "link":
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturaDirecta.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "link_rc":
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Recaudo/Recaudo.aspx?Documento=" + (item.FindControl("lbl_recibo") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "link_tr":
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Documento=" + (item.FindControl("lbl_tercero") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "lkn_nc":
                        string[] lc_concat = (item.FindControl("lbl_doc") as Label).Text.Split('-');
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=102&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + lc_concat[1] + "&inParametro=InTip&inValor=" + lc_concat[0];
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "lkn_nd":
                        string[] lc_concat_ = (item.FindControl("lbl_doc") as Label).Text.Split('-');
                        url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=103&inban=S&inImp=N&inClo=N&inParametro=pTNro&inValor=" + lc_concat_[1] + "&inParametro=InTip&inValor=" + lc_concat_[0];
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
        protected void rg_consulta_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            int ln_nrofac = Convert.ToInt32(dataItem.GetDataKeyValue("HDNROFAC").ToString());
            string lc_tipfac = dataItem.GetDataKeyValue("HDTIPFAC").ToString();
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        ConsultasBL Obj = new ConsultasBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetRecaudoxFactura(null,ln_nrofac,lc_tipfac,Convert.ToString(Session["CODEMP"]) );
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
    }
}