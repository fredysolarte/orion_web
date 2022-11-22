using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaMovimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fini.SelectedDate = System.DateTime.Today;
                edt_ffin.SelectedDate = System.DateTime.Today;
            }
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = " AND 1=1";
            if (!string.IsNullOrWhiteSpace(txt_barras.Text))
                filtro += " AND BCODIGO ='" + txt_barras.Text + "'";
            if (rc_bodega.SelectedValue != "")
                filtro += " AND MBBODEGA ='" + rc_bodega.SelectedValue + "'";
            if (rc_linea.SelectedValue != "")
                filtro += " AND MBTIPPRO ='" + rc_linea.SelectedValue + "'";
            if (!string.IsNullOrWhiteSpace(edt_referencia.Text))
                filtro += " AND MBCLAVE1 ='" + edt_referencia.Text + "'";
            if (!string.IsNullOrWhiteSpace(edt_nombre.Text))
                filtro += " AND ARNOMBRE LIKE '%" + edt_referencia.Text + "%'";
            if (rc_transaccion.SelectedValue != "")
                filtro += " AND MBCDTRAN ='" + rc_transaccion.SelectedValue + "'";

            obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
            obj_consulta.SelectParameters["inFecIni"].DefaultValue = Convert.ToString(edt_fini.SelectedDate);
            obj_consulta.SelectParameters["inFecFin"].DefaultValue = Convert.ToString(edt_ffin.SelectedDate);            
            rg_detalle.DataBind();
        }
    }
}