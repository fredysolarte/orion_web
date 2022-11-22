using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.ControlesUsuario
{
	public partial class Ctl_Articulos : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void btn_find_Click(object sender, EventArgs e)
		{
			string filter = "";
			if (!string.IsNullOrWhiteSpace(rcb_TipPro.Text))
				filter += "AND ARTIPPRO ='" + rcb_TipPro.SelectedValue + "'";
			if (!string.IsNullOrWhiteSpace(txt_clave1.Text))
				filter += "AND ARCLAVE1 ='" + txt_clave1.Text + "'";
			if (!string.IsNullOrWhiteSpace(txt_clave2.Text))
				filter += "AND ARCLAVE2 = '" + txt_clave2.Text + "'";
			if (!string.IsNullOrWhiteSpace(txt_clave3.Text))
				filter += "AND ARCLAVE3 ='" + txt_clave3.Text + "'";
			if (!string.IsNullOrWhiteSpace(txt_clave4.Text))
				filter += "AND ARCLAVE4 = '" + txt_clave4.Text + "'";

			if (!string.IsNullOrWhiteSpace(filter))
			{
				filter = filter.Substring(4, filter.Length - 4);
			}

			ObjectDataSource1.SelectParameters["filter"].DefaultValue = filter;
			rgConsulta.DataBind();

		}
		protected void btnBuscar_click(object sender, EventArgs e)
		{
			ModalPopupFinder.Hide();
			((RadComboBox)this.Parent.FindControl("rcb_linea")).SelectedValue = rgConsulta.SelectedValues["ARTIPPRO"].ToString();
			((RadTextBox)this.Parent.FindControl("rdt_referencia")).Text = rgConsulta.SelectedValues["ARCLAVE1"].ToString();
			((RadTextBox)this.Parent.FindControl("rdt_talla")).Text = rgConsulta.SelectedValues["ARCLAVE2"].ToString();
			((RadTextBox)this.Parent.FindControl("rdt_color")).Text = rgConsulta.SelectedValues["ARCLAVE3"].ToString();
			
		}
		protected void btnCancel_click(object sender, EventArgs e)
		{
			ModalPopupFinder.Hide();
		}
	}
}