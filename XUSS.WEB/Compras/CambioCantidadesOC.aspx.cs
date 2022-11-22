using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Compras
{
    public partial class CambioCantidadesOC : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();

            try
            {
                if (!IsPostBack)
                {
                    obj_terpago.SelectParameters["InCodemp"].DefaultValue = Convert.ToString(Request.QueryString["CodEmp"]);
                    obj_moneda.SelectParameters["TTCODEMP"].DefaultValue = Convert.ToString(Request.QueryString["CodEmp"]);

                    //Load Head
                    using (IDataReader reader = Obj.GetComprasHD(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"])))
                    {
                        while (reader.Read())
                        {
                            txt_nroorden.Text = Convert.ToString(reader["CH_NROCMP"]);
                            edt_forden.DbSelectedDate = Convert.ToDateTime(reader["CH_FECORD"]);
                            txt_nrocmpalt.Text = Convert.ToString(reader["CH_CNROCMPALT"]);
                            rc_bodegas.SelectedValue = Convert.ToString(reader["CH_BODEGA"]);
                            rc_proveedor.SelectedValue = Convert.ToString(reader["CH_PROVEEDOR"]);
                            rc_torden.SelectedValue = Convert.ToString(reader["CH_TIPORD"]);
                            rc_tdespacho.SelectedValue = Convert.ToString(reader["CH_TIPDPH"]);
                            rc_tpago.SelectedValue = Convert.ToString(reader["CH_TERPAG"]);
                            rc_moneda.SelectedValue = Convert.ToString(reader["CH_MONEDA"]);
                            rc_estado.SelectedValue = Convert.ToString(reader["CH_ESTADO"]);
                            txt_observaciones.Text = Convert.ToString(reader["CH_OBSERVACIONES"]);

                            if (Convert.ToString(reader["CH_ESTADO"]) != "AC")
                                btn_guardar.Enabled = false;
                        }
                    }
                    //Load Detail
                    tbItems = Obj.GetComprasDT(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"]));
                    rg_items.DataSource = tbItems;
                    rg_items.DataBind();
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

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            try
            {
                Obj.UpdateCompras(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"]), rc_bodegas.SelectedValue, Convert.ToInt32(rc_proveedor.SelectedValue), Convert.ToInt32(rc_torden.SelectedValue), Convert.ToDateTime(edt_forden.DbSelectedDate), null, rc_tdespacho.SelectedValue,
                    rc_tpago.SelectedValue, null, null, 0, txt_observaciones.Text, Convert.ToString(Request.QueryString["IdUser"]), "AP", 0, Convert.ToDateTime(edt_forden.DbSelectedDate), null, null, rc_moneda.SelectedValue, txt_nrocmpalt.Text, tbItems);

                Obj.InsertSeguimiento(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"]), "Purchar Order Edit and Approved ", Convert.ToString(Request.QueryString["IdUser"]), "AC");

                btn_guardar.Enabled = false;
                rc_estado.SelectedValue = "AP";

                litTextoMensaje.Text = "Save Changes Sucefull!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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

        protected void txt_cancom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codigo") as RadTextBox).Text);
                foreach (DataColumn dt in tbItems.Columns)
                    dt.ReadOnly = false;

                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToInt32(rw["CD_NROITEM"]) == ln_codigo)
                    {
                        rw["CD_CANTIDAD"] = Convert.ToDouble((sender as RadNumericTextBox).Value);
                        rw["NEW_TOT"] = Convert.ToDouble((sender as RadNumericTextBox).Value) * Convert.ToDouble(rw["CD_PRECIO"]);
                        if (Convert.ToDouble((sender as RadNumericTextBox).Value) != Convert.ToDouble(rw["CD_CANSOL"]))
                            rw["CD_ESTADO"] = "CH";
                        else
                            rw["CD_ESTADO"] = "AC";
                    }
                }
                rg_items.DataSource = tbItems;
                rg_items.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rg_items_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in (sender as RadGrid).Items)
            {
                string aValue = ((RadTextBox)item.FindControl("txt_estado")).Text;
                if (aValue == "CH")
                {
                    item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                    item.ControlStyle.Font.Bold = true;
                }
                    //item.ControlStyle.BackColor = Color.Orange;
            }
        }
    }
}