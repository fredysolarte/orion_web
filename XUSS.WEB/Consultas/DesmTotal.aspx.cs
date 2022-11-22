using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;

namespace XUSS.WEB.Consultas
{
    public partial class DesmTotal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //edt_fecha.SelectedDate = System.DateTime.Today;
                rc_mes.SelectedValue = Convert.ToString(System.DateTime.Today.Month);
                rc_ano.SelectedValue = Convert.ToString(System.DateTime.Today.Year);
                rc_dia.SelectedValue = Convert.ToString(System.DateTime.Today.Day);
            }
        }
        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            rgDetalle.DataBind();
        }

        protected void rgDetalle_PreRender(object sender, EventArgs e)
        {
            //string avalue = "";
            //foreach (GridDataItem item in rgDetalle.Items)
            //{
            //    avalue = ((Label)item.FindControl("labeldia")).Text;
            //    if (avalue == "DOM")
            //    {
            //        item.ControlStyle.BackColor = Color.FromArgb(255, 200, 140);
            //    }
            //}
        }
    }
}