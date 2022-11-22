using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Costos;
using Telerik.Web.UI;
using System.Data;

namespace XUSS.WEB.Costos
{
    public partial class Presupuesto : System.Web.UI.Page
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
            try {
                tbItems = Obj.GetPresupuesto(null, Convert.ToInt32(rc_mes.SelectedValue), Convert.ToInt32(rc_ano.SelectedValue), Convert.ToString(rc_bodega.SelectedValue));
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
                PresupuestoBL.InsertPresupuesto(null,
                                                Convert.ToString(Session["CODEMP"]),
                                                Convert.ToInt32(rgDetalle.Items[i]["ano"].Text),
                                                Convert.ToInt32(rgDetalle.Items[i]["mes"].Text),
                                                Convert.ToInt32(rgDetalle.Items[i]["dia"].Text),
                                                Convert.ToString(((RadComboBox)rgDetalle.Items[i].FindControl("rc_bodega")).SelectedValue),
                                                Convert.ToDouble(((RadNumericTextBox)rgDetalle.Items[i].FindControl("edt_cantidad")).Value),
                                                Convert.ToString(Session["UserLogon"]), "AC");
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