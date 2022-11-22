using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class cVentasVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fechaI.SelectedDate = System.DateTime.Today;
                edt_fechaF.SelectedDate = System.DateTime.Today;
            }
        }

        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            rgDetalle.DataBind();
        }
    }
}