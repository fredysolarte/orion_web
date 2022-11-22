using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fecfinal.SelectedDate = System.DateTime.Now;
                edt_fecinicial.SelectedDate = System.DateTime.Now;
            }
        }
        protected void btn_consultar_OnClick(object sender, EventArgs e)
        {
            string sql = " AND 1=1";                        
            if (rc_bodega.SelectedValue != "-1")
                sql += " AND TFBODEGA = '" + rc_bodega.SelectedValue + "'";

            sql += " AND CONVERT(DATE,HDFECFAC,101) BETWEEN CONVERT(DATE,'" + Convert.ToString(edt_fecinicial.SelectedDate.Value.Month) + "/" + Convert.ToString(edt_fecinicial.SelectedDate.Value.Day) + "/" + Convert.ToString(edt_fecinicial.SelectedDate.Value.Year) + "',101) AND CONVERT(DATE,'" + Convert.ToString(edt_fecfinal.SelectedDate.Value.Month) + "/" + Convert.ToString(edt_fecfinal.SelectedDate.Value.Day) + "/" + Convert.ToString(edt_fecfinal.SelectedDate.Value.Year) + "',101) ";
            obj_ventas.SelectParameters["filter"].DefaultValue = sql;
            rg_consulta.DataBind();
        }
        protected void rg_consulta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string url = "";
            if (e.CommandName == "link")
            {
                GridDataItem item = (GridDataItem)e.Item;
                try
                {
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturaDirecta.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
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
}