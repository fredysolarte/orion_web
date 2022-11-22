using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultasFacturas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Params["__EVENTTARGET"] == "ModalEdit")
            {
                int factura = Convert.ToInt32(rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[3].Text);
                string lc_tf = rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[7].Text;
                string filtro = "HDNROFAC ="+ Convert.ToString(factura) + " AND TFTIPFAC ='"+ lc_tf +"'";
                edt_nFacturaD.Text = lc_tf + "-" + Convert.ToString(factura);
                edt_TFacturaD.Text = rgDetalle.Items[Convert.ToInt32(toEditRow.Value)].Cells[8].Text;

                obj_detallefac.SelectParameters["filter"].DefaultValue = filtro;
                rgFacturaDetalle.DataBind();
                mp_detallefac.Show();
            }
        }
        
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = "";
            string filtro_ = "";
            string filtro__ = "";
            if (!string.IsNullOrWhiteSpace(edt_identificacion.Text))
                filtro_ += " AND HDCODNIT ='" + edt_identificacion.Text + "'";
            if (!string.IsNullOrWhiteSpace(edt_nombre.Text))
                filtro += " AND TRNOMBRE LIKE'%" + edt_nombre.Text + "%'";
            if (!string.IsNullOrWhiteSpace(edt_referencia.Text))
                filtro__ += " AND DTCLAVE1 ='" + edt_referencia.Text + "'";
            if (!string.IsNullOrWhiteSpace(edt_NumFac.Text))
                filtro__ += " AND HDNROFAC ='" + edt_referencia.Text + "'";
            if (rc_tipfac.SelectedValue != "-1")
                filtro__ += " AND HDTIPFAC ='" + Convert.ToString(rc_tipfac.SelectedValue) + "'";


            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            if (!string.IsNullOrWhiteSpace(filtro_))
            {
                filtro_ = filtro_.Substring(4, filtro_.Length - 4);
            }

            if (!string.IsNullOrWhiteSpace(filtro__))
            {
                filtro__ = filtro__.Substring(4, filtro__.Length - 4);
            }

            obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
            obj_consulta.SelectParameters["filter_"].DefaultValue = filtro_;
            obj_consulta.SelectParameters["filter__"].DefaultValue = filtro__;
            rgDetalle.DataBind();            
        }
        protected void OnClick_lnk_detalle(object sender, EventArgs e)
        {
            string filtro = "1=0";
            obj_detallefac.SelectParameters["filter"].DefaultValue = filtro;
            rgFacturaDetalle.DataBind();
            mp_detallefac.Show();
        }
        protected void rgFacturaDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}