using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Contabilidad;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Contabilidad
{
    public partial class CierreContable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rc_ano_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            MesesBL Obj = new MesesBL();
            try
            {
                rc_mes.Items.Clear();
                foreach (DataRow rw in (Obj.GetMeses(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((sender as RadComboBox).SelectedValue))).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Text = Convert.ToString(rw["NOM_MES"]);
                    itemi.Value = Convert.ToString(rw["MA_MES"]);
                    rc_mes.Items.Add(itemi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        protected void btn_cerrar_Click(object sender, EventArgs e)
        {
            CierreBL obj = new CierreBL(); 
            try {
                var cll_mes = rc_mes.CheckedItems;
                if (cll_mes.Count != 0)
                {
                    foreach (var item in cll_mes)
                    {
                        obj.GenerarCierre(null, Convert.ToInt32(item.Value), Convert.ToInt32(rc_ano.SelectedValue), Convert.ToString(Session["UserLogon"]));
                    }
                }
                litTextoMensaje.Text = "¡Cierre Terminado de Manera Correcta!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj = null;
            }
        }
    }
}