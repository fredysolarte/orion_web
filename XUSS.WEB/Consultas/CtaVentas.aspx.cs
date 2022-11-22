using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class CtaVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void im_buscar(object sender, EventArgs e)
        {
            ModalPopupConsulta.Show();
        }
        protected void bt_cerrar_click(object sender, EventArgs e)
        {
            ModalPopupConsulta.Hide();
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = "";            
            
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
            obj_consulta.SelectParameters["FecIni"].DefaultValue = Convert.ToString(edt_FecIni.SelectedDate);
            obj_consulta.SelectParameters["FecFin"].DefaultValue = Convert.ToString(edt_FecFIn.SelectedDate);

            rgDetalle.DataBind();
            ModalPopupConsulta.Hide();
        }
    }
}