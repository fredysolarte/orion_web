using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaVentasDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fecfinal.SelectedDate = System.DateTime.Now;
                edt_fecinicial.SelectedDate = System.DateTime.Now;
                //edt_fecinicial.SelectedDate = System.DateTime.Now.AddDays(-30);
            }
        }
        protected void btn_consultar_OnClick(object sender, EventArgs e)
        {
            string sql = " 1=1",lc_in = "";

            if (!string.IsNullOrEmpty(txt_nombre.Text))
                sql += " AND TRNOMBRE LIKE '%" + txt_nombre.Text+"%'";
            if (!string.IsNullOrEmpty(txt_referencia.Text))
                sql += " AND DTCLAVE1 = '" + txt_referencia.Text + "'";
            //if (rc_bodega.SelectedValue!="-1")
            //    sql += " AND BDBODEGA = '" + rc_bodega.SelectedValue + "'";
            if (!string.IsNullOrEmpty(txt_identificacion.Text))
                sql += " AND TRCODNIT = '" + txt_identificacion.Text + "'";
            
            var collection = rc_bodega.CheckedItems;
            if (collection.Count != 0)
            {
                sql += " AND BDBODEGA IN (";
                foreach (var item in collection)
                {
                    lc_in += "'" + Convert.ToString(item.Value) + "',";
                }

                sql += lc_in.Substring(0, lc_in.Length - 1) + ")";
            }

            var cll_linea = rc_linea.CheckedItems;
            if (cll_linea.Count != 0)
            {
                lc_in = "";
                sql += " AND DTTIPPRO IN (";
                foreach (var item in cll_linea)
                {
                    lc_in += "'" + Convert.ToString(item.Value) + "',";
                }

                sql += lc_in.Substring(0, lc_in.Length - 1) + ")";
            }

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
                     switch ((item.FindControl("lbl_cdoc") as Label).Text)
                     {
                         case "1":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturaDirecta.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "2":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Devoluciones.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "3":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Separados.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "4":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Bonos.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "5":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Remisiones.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "6":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Remisiones.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                             ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                             break;
                         case "7":
                             url = "http://" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Gastos.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
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
}