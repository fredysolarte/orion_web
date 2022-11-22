using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Contabilidad
{
    public partial class ConsultaMovimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rgDetalle_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "link")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Contabilidad/CausacionProveedores.aspx?Documento=" + (item_.FindControl("lbl_doc") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            string filtro = " AND 1=1";
            if (rc_ano.SelectedValue != "")
                filtro += " AND MVTH_ANO =" + rc_ano.SelectedValue;

            if (rc_tipfac.SelectedValue != "")
                filtro += " AND TFTIPFAC ='" + rc_tipfac.SelectedValue + "'";

            obj_enero.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =1";
            rgEnero.DataBind();
            obj_febrero.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =2";
            rg_febrero.DataBind();
            obj_marzo.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =3";
            rg_marzo.DataBind();
            obj_abril.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =4";
            rg_abril.DataBind();
            obj_mayo.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =5";
            rg_mayo.DataBind();
            obj_junio.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =6";
            rg_junio.DataBind();
            obj_julio.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =7";
            rg_julio.DataBind();
            obj_agosto.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =8";
            rg_agosto.DataBind();
            obj_septiembre.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =9";
            rg_septiembre.DataBind();
            obj_octubre.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =10";
            rg_octubre.DataBind();
            obj_noviembre.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =11";
            rg_noviembre.DataBind();
            obj_diciembre.SelectParameters["inFilter"].DefaultValue = filtro + " AND MVTH_MES =12";
            rg_diciembre.DataBind();
        }

        protected void rg_PreRender(object sender, EventArgs e)
        {
            string aValue = "";

            foreach (GridDataItem item in (sender as RadGrid).Items)
            {
                aValue = Convert.ToString(((Label)item.FindControl("lbl_crz")).Text);
                if (aValue == "N")
                {
                    item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                    item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                    item.ControlStyle.Font.Bold = true;
                }
            }
        }
    }
}