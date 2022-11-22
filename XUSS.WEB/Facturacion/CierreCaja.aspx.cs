using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Facturacion;

namespace XUSS.WEB.Facturacion
{
    public partial class CierreCaja : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_fecini.SelectedDate = System.DateTime.Today;
                txt_fecfin.SelectedDate = System.DateTime.Today;
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            
            try
            {
                url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7012&inban=S&inImp=S&inClo=S&inParametro=inCodemp&inValor=" + Convert.ToString(Session["CODEMP"]) + "&inParametro=inTipo&inValor=" + rc_tipfac.SelectedValue + "&inParametro=inFecIni&inValor=" + Convert.ToString(txt_fecini.SelectedDate) + "&inParametro=inFecFin&inValor=" + Convert.ToString(txt_fecfin.SelectedDate);
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
        protected void txt_fecini_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txt_fecfin.SelectedDate = txt_fecini.SelectedDate;
        }
        protected void btn_cerrar_Click(object sender, EventArgs e)
        {
            FacturacionBL Obj = new FacturacionBL();
            try
            {
                tbItems = Obj.GetDifInvFac(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipfac.SelectedValue), txt_fecini.SelectedDate, txt_fecfin.SelectedDate);
                if (tbItems.Rows.Count > 0)
                {
                    rgFacturas.DataSource = tbItems;
                    rgFacturas.DataBind();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    if (!Obj.GetFacturasCierre(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipfac.SelectedValue), txt_fecini.SelectedDate))
                    {
                        litTextoMensaje.Text = "Existen Dias Pasados sin Ejecutar Cierre!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        Obj.CerrarFacturas(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rc_tipfac.SelectedValue), txt_fecini.SelectedDate);
                        litTextoMensaje.Text = "Cierre Ejecutado de Manera Correcta!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
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