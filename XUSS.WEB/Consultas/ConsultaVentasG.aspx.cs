using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaVentasG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fecha.SelectedDate = System.DateTime.Today;
            }
        }

        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            rgDetalle.DataBind();
        }

        public bool GetCheck(object valor)
        {
            if (DBNull.Value == valor || valor == null)
            {
                return false;
            }
            string val = valor.ToString();
            if (val.Equals("N") || String.IsNullOrEmpty(val))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}