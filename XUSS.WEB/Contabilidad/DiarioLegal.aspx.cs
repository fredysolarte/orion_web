using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Contabilidad
{
    public partial class DiarioLegal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            try
            {
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8012&inban=S&inParametro=InMes&inValor=" + rc_mes.SelectedValue + "&inParametro=InAno&inValor=" + rc_ano.SelectedValue + "&inParametro=InNivel&inValor=" + rc_tipo.SelectedValue;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
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
    }
}