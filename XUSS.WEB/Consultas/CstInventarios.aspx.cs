using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class CstInventarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void im_buscar(object sender, EventArgs e)
        {            
            ModalPopupConsulta.Show();
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = "";
            if (rc_bodega.SelectedValue != "")
                filtro += " AND BBBODEGA ='" + rc_bodega.SelectedValue + "'";
            if (rc_linea.SelectedValue != "")
                filtro += " AND ARTIPPRO ='" + rc_linea.SelectedValue + "'";
            if (!string.IsNullOrWhiteSpace(edt_referencia.Text))
                filtro += " AND ARCLAVE1 ='"+ edt_referencia.Text + "'" ;            
            if (!string.IsNullOrWhiteSpace(edt_talla.Text))
                filtro += " AND ARCLAVE2 ='" + edt_talla.Text + "'";
            if (!string.IsNullOrWhiteSpace(edt_color.Text))
                filtro += " AND ARCLAVE3 ='" + edt_color.Text + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
            rgDetalle.DataBind();
            ModalPopupConsulta.Hide();
        }
        protected void bt_cerrar_click(object sender, EventArgs e)
        {
            ModalPopupConsulta.Hide();
        }
    }
}