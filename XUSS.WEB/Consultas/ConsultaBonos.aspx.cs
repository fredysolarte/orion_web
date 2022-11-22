using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaBonos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string lc_filtro = " AND 1=1";

            if (!string.IsNullOrEmpty(txt_identificacion.Text))
                lc_filtro += " AND TRCODNIT = '" + txt_identificacion.Text + "'";

            if (rc_tipfac.SelectedValue != "-1")
                lc_filtro += " AND HDTIPFAC = '" + rc_tipfac.SelectedValue + "'";

            if (!string.IsNullOrEmpty(txt_nrofac.Text))
                lc_filtro += " AND HDNROFAC = " + txt_nrofac.Text;

            if (!string.IsNullOrEmpty(txt_nombres.Text))
                lc_filtro += " AND (TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI ,'')) LIKE '%" + txt_nombres.Text + "%'";

            if (!string.IsNullOrEmpty(txt_nrobono.Text))
                lc_filtro += " AND T_NROBONO = " + txt_nrobono.Text;
            

            obj_consulta.SelectParameters["filter"].DefaultValue = lc_filtro;
            rgDetalle.DataBind();
        }
    }
}