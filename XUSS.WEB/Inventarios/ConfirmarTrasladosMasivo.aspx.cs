using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;

namespace XUSS.WEB.Inventarios
{
    public partial class ConfirmarTrasladosMasivo : System.Web.UI.Page
    {
        private DataTable tbTraslados
        {
            set { ViewState["tbTraslados"] = value; }
            get { return ViewState["tbTraslados"] as DataTable; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            TrasladosBL obj = new TrasladosBL();
            try {
                btn_guardar.Enabled = true;
                tbTraslados = obj.GetTraslados(null, " TSOTBODE = '" + rc_otbodega.SelectedValue + "' AND TSESTADO = 'AC'", 0, 0);
                rgDetalle.DataSource = tbTraslados;
                rgDetalle.DataBind();
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

        protected void rgDetalle_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbTraslados;
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            TrasladosBL Obj = new TrasladosBL();
            try
            {
                btn_guardar.Enabled = false;
                foreach (GridDataItem item in rgDetalle.Items)
                {
                    if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                    {
                        int ln_traslado = Convert.ToInt32((item.FindControl("lbl_traslado") as Label).Text);
                        foreach (DataRow rw in tbTraslados.Rows)
                        {
                            if (Convert.ToInt32(rw["TSNROTRA"]) == ln_traslado)
                            {
                                Obj.confirmTraslado(null, Convert.ToString(Session["CODEMP"]), ln_traslado, Convert.ToDateTime(rw["TSFECTRA"]), Convert.ToString(rw["TSBODEGA"]), Convert.ToString(rw["TSOTBODE"]),
                                    Convert.ToString(rw["TSCDTRAN"]), Convert.ToString(rw["TSOTTRAN"]), Convert.ToInt32(rw["TSMOVENT"]), Convert.ToInt32(rw["TSMOVSAL"]), Convert.ToString(rw["TSCOMENT"]),
                                    "CE", Convert.ToString(rw["TSCAUSAE"]), Convert.ToString(Session["UserLogon"]));
                            }
                        }
                    }
                }
                litTextoMensaje.Text = "Proceso Culminado de Manera Exitosa!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                Obj = null;
            }
        }

        protected void rgDetalle_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link_traslado":
                    GridDataItem item = (GridDataItem)e.Item;
                    string url_ = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/Traslados.aspx?Traslado=" + (item.FindControl("lbl_traslado") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url_ + "');", true);
                    item = null;
                    break;
            }
        }
    }
}