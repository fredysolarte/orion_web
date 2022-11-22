using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class ReposicionDiaria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fechaIni.SelectedDate = System.DateTime.Today;
                edt_fechaFin.SelectedDate = System.DateTime.Today;
            }
        }
        protected void btn_cargar_Click(object sender, EventArgs e)
        {
            string filtro = " ";

            if ((rc_bodegas.Text)!="Seleccionar")
                filtro += " AND BDBODEGA ='"+rc_bodegas.SelectedValue+"'";
            if ((rc_categoria.Text) != "Seleccionar")
                filtro += " AND DTTIPPRO ='" + rc_categoria.SelectedValue + "'";

            obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
            rgDetalle.DataBind();
        }
    }
}