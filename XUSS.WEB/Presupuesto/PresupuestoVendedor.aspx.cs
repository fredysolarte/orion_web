using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Costos;

namespace XUSS.WEB.Presupuesto
{
    public partial class PresupuestoVendedor : System.Web.UI.Page
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
                rc_ano.SelectedValue = Convert.ToString(System.DateTime.Now.Year);
                rc_mes.SelectedValue = Convert.ToString(System.DateTime.Now.Month);
            }
        }

        protected void btn_consultar_Click(object sender, EventArgs e)
        {
            PresupuestoBL Obj = new PresupuestoBL();
            try
            {
                tbItems = null;
                tbItems = Obj.GetPresupuestoVendedor(null, Convert.ToInt32(rc_mes.SelectedValue), Convert.ToInt32(rc_ano.SelectedValue));
                rgDetalle.DataSource = tbItems;
                rgDetalle.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rgDetalle.Items.Count; i++)
            {
                PresupuestoBL.InsertPresupuestoVendedor(null,
                                                Convert.ToString(Session["CODEMP"]),
                                                Convert.ToInt32(rgDetalle.Items[i]["ano"].Text),
                                                Convert.ToInt32(rgDetalle.Items[i]["mes"].Text),
                                                Convert.ToInt32(rgDetalle.Items[i]["cod_vendedor"].Text),
                                                Convert.ToDouble(((RadNumericTextBox)rgDetalle.Items[i].FindControl("edt_ventas")).Value),
                                                Convert.ToDouble(((RadNumericTextBox)rgDetalle.Items[i].FindControl("edt_Cartera")).Value),
                                                Convert.ToString(Session["UserLogon"]));
                 
            }
        }

        public double GetValor(object valor)
        {
            if (DBNull.Value == valor || valor == null)
                return 0;
            double val = Convert.ToDouble(valor);
            return val;
        }
    }
}