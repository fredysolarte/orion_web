using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Consultas
{
    public partial class ConsultaVentasCarteraVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rc_mes.SelectedValue = Convert.ToString(System.DateTime.Today.Month);
                rc_ano.SelectedValue = Convert.ToString(System.DateTime.Today.Year);

                TercerosBL ObjT = new TercerosBL();
                RadComboBoxItem item = new RadComboBoxItem();
                rc_agente.Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                rc_agente.Items.Add(item);
                foreach (DataRow rw in ObjT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["TRCODTER"]);
                    itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                    rc_agente.Items.Add(itemi);
                }

                if (Convert.ToString(Session["UserDBA"]) != "1")
                {
                    rc_agente.Enabled = false;
                }
            }
        }

        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = " AND 1=1", filtro_ = " AND 1=1";

            //if (rc_mes.SelectedValue != "")
            //    filtro += " AND MONTH(HDFECFAC) ='" + rc_mes.SelectedValue + "'";
            //if (rc_ano.SelectedValue != "")
            //    filtro += " AND YEAR(HDFECFAC) ='" + rc_ano.SelectedValue + "'";
            

            if (Convert.ToString(Session["UserDBA"]) != "1")
            {               
                filtro += " AND HDAGENTE =(SELECT TRCODTER FROM TERCEROS WHERE TRCODNIT=(SELECT usua_identifica FROM admi_tusuario WHERE usua_usuario ='" + Convert.ToString(Session["UserLogon"]).ToLower()+"'))";
                filtro_ += " AND A.TRAGENTE =(SELECT TRCODTER FROM TERCEROS WHERE TRCODNIT=(SELECT usua_identifica FROM admi_tusuario WHERE usua_usuario ='" + Convert.ToString(Session["UserLogon"]).ToLower() + "'))";
            }

            if (rc_agente.SelectedValue != "")
            {
                filtro += " AND HDAGENTE =" + rc_agente.SelectedValue;
                filtro_ += " AND A.TRAGENTE =" + rc_agente.SelectedValue;
            }

            try
            {
                obj_ventas.SelectParameters["filter"].DefaultValue = filtro;
                obj_ventas.SelectParameters["inMes"].DefaultValue = rc_mes.SelectedValue;
                obj_ventas.SelectParameters["inAno"].DefaultValue = rc_ano.SelectedValue;
                rgVentas.DataBind();

                obj_cartera.SelectParameters["filter"].DefaultValue = " AND 1=1 " + filtro;
                obj_cartera.SelectParameters["inMes"].DefaultValue = rc_mes.SelectedValue;
                obj_cartera.SelectParameters["inAno"].DefaultValue = rc_ano.SelectedValue;
                rg_cartera.DataBind();

                obj_recaudo.SelectParameters["filter"].DefaultValue = " AND 1=1 " + filtro;
                obj_recaudo.SelectParameters["inMes"].DefaultValue = rc_mes.SelectedValue;
                obj_recaudo.SelectParameters["inAno"].DefaultValue = rc_ano.SelectedValue;
                rg_recaudo.DataBind();

                //grafica
                obj_grafica.SelectParameters["filter"].DefaultValue = " AND 1=1 " + filtro;
                obj_grafica.SelectParameters["inMes"].DefaultValue = rc_mes.SelectedValue;
                obj_grafica.SelectParameters["inAno"].DefaultValue = rc_ano.SelectedValue;
                RadHtmlChart1.DataBind();

                obj_clientes.SelectParameters["filter"].DefaultValue = " AND 1=1 " + filtro_;
                obj_clientes.SelectParameters["inMes"].DefaultValue = rc_mes.SelectedValue;
                obj_clientes.SelectParameters["inAno"].DefaultValue = rc_ano.SelectedValue;
                rgClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}