using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.ControlesUsuario
{
	public partial class Ctl_Personas : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void btn_find_Click(object sender, EventArgs e)
		{
			string filter = "";
			if (!string.IsNullOrWhiteSpace(txt_identificacion.Text))
				filter += "AND TRCODNIT ='" + txt_identificacion.Text + "'";
			if (!string.IsNullOrWhiteSpace(txt_Nombre.Text))
				filter += "AND TRNOMBRE LIKE '%" + txt_Nombre.Text + "%'";
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
			// ((RadTextBox)this.Parent.FindControl("rdt_identificacion")).Text = "pruebas";
			((RadTextBox)this.Parent.FindControl("rdt_identificacion")).Text = rgConsulta.SelectedValues["TRCODNIT"].ToString();
			((RadTextBox)this.Parent.FindControl("rdt_nombre")).Text = rgConsulta.SelectedValues["TRNOMBRE"].ToString();
		}
		protected void btnCancel_click(object sender, EventArgs e)
		{
			ModalPopupFinder.Hide();
		}
	}
}