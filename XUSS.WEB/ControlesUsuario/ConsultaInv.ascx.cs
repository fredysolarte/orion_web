using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Telerik.Web.UI;

namespace XUSS.WEB.ControlesUsuario
{
	public partial class ConsultaInv : System.Web.UI.UserControl
	{
		private string filterControl;
		[Bindable(true), DefaultValue(@"")]
		public string FilterControl
		{
			get
			{
				return filterControl;
			}
			set
			{
				filterControl = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//if (!IsPostBack)
			//{
			//    for (int i = 0; i < rcb_TipPro.Items.Count; i++)
			//        (rcb_bodega.Items[i].FindControl("chk_bg") as CheckBox).Checked = true;

			//    for (int i = 0; i < rcb_TipPro.Items.Count; i++)
			//        (rcb_TipPro.Items[i].FindControl("chk_tp") as CheckBox).Checked = true;
			//}
		}
		protected void btnBuscar_Click(object sender, EventArgs e)
		{
			string filter = "", l_filter = "";
			
			if ((txt_FechaI.SelectedDate != null) && (txt_FechaF.SelectedDate != null) )
			{
				//filter += " AND FHFECVEN BETWEEN '" + txt_FechaI.SelectedDate.Value.ToString("dd/MM/yyyy") + "' AND '" + txt_FechaF.SelectedDate.Value.ToString("dd/MM/yyyy") +"'";
			}
			
			for ( int i = 0; i < rcb_bodega.Items.Count; i++)
				if ((rcb_bodega.Items[i].FindControl("chk_bg") as CheckBox).Checked)
					l_filter += ",'" + rcb_bodega.Items[i].Value + "'";				
			
			if (l_filter.Length > 0)
			{
				l_filter = " AND BBBODEGA IN("+ l_filter.Substring(1,l_filter.Length -1)+")";
				filter += l_filter;
			}

			l_filter = "";
			for (int i = 0; i < rcb_TipPro.Items.Count; i++)
				if ((rcb_TipPro.Items[i].FindControl("chk_tp") as CheckBox).Checked)
					l_filter += ",'" + rcb_TipPro.Items[i].Value + "'";

			if (l_filter.Length > 0)
			{
				l_filter = " AND BBTIPPRO IN(" + l_filter.Substring(1, l_filter.Length - 1) + ")";
				filter += l_filter;
			}




			if (filter.Length > 0)
			{
				filter = filter.Substring(4, filter.Length - 4);
				Session["filter"] = filter;
			}
			ModalPopupFinder.Hide();
			((RadGrid)this.Parent.FindControl(FilterControl)).DataBind();
		}
		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Session["filter"] = "1=0";
			ModalPopupFinder.Hide();
			((RadGrid)this.Parent.FindControl(FilterControl)).DataBind();
		}
	}
}