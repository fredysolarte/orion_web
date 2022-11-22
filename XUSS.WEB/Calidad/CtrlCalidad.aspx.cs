using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using XUSS.BLL.Calidad;

namespace XUSS.WEB.Calidad
{
	public partial class CtrlCalidad : System.Web.UI.Page
	{
		private Telerik.Web.UI.RadListViewDataItem rliItems;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (RadListView1.InsertItem != null)
			{
				rliItems = RadListView1.InsertItem;
			}
			else
			{
				if (RadListView1.EditItems != null && RadListView1.EditItems.Count > 0)
				{
					rliItems = RadListView1.EditItems[0];
				}
			}
			if (!IsPostBack)
			{
				ViewState["toolbars"] = true;
			}
		}
		protected void OnClick_Imprimir(object sender, EventArgs e)
		{
			string filtro="0";
			filtro = (((ImageButton)sender).Parent.FindControl("rdtArreglo") as RadTextBox).Text;
			ObjectDataSource5.SelectParameters["consecutivo"].DefaultValue = filtro;
			//ReportViewer1.ShowPrintButton = true;
			//ReportViewer1.LocalReport.Refresh();
			ModalPopupExtender2.Show();
		}
		protected void BuscarGrilla(object sender, EventArgs e)
		{
			string filtro = "";

			if ((((Button)sender).Parent.FindControl("cb_bodega") as RadComboBox).SelectedValue != "")
				filtro = "AND CA_BODEGA ='" + (((Button)sender).Parent.FindControl("cb_bodega") as RadComboBox).SelectedValue + "'";
			
			if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rdtArreglo") as RadTextBox).Text))
				filtro += "AND CA_NCONSE =" + (((Button)sender).Parent.FindControl("rdtArreglo") as RadTextBox).Text;

			if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rdt_identificacion") as RadTextBox).Text))
				filtro += "AND CA_NRODOC=" + (((Button)sender).Parent.FindControl("rdt_identificacion") as RadTextBox).Text;

			if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rdt_nombre") as RadTextBox).Text))
				filtro += "AND CA_NOMBRE LIKE '%" + (((Button)sender).Parent.FindControl("rdt_nombre") as RadTextBox).Text + "%'";

			if (!string.IsNullOrWhiteSpace(filtro))
			{
				filtro = filtro.Substring(4, filtro.Length - 4);
			}

			ObjectDataSource1.SelectParameters["filter"].DefaultValue = filtro;
			RadListView1.DataBind();
			if ((RadListView1.Controls[0] is RadListViewEmptyDataItem))
			{
				StringBuilder str = new StringBuilder();
				str.AppendLine("<div id=\"box-messages\" class=\"box\">");
				str.AppendLine("<div class=\"messages\">");
				str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
				str.AppendLine("    <div class=\"image\">");
				str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
				str.AppendLine("		</div>");
				str.AppendLine("    <div class=\"text\">");
				str.AppendLine("        <h6>Información</h6>");
				str.AppendLine("        <span>No se encontraron registros</span>");
				str.AppendLine("    </div>");
				str.AppendLine("</div>");
				str.AppendLine("</div>");
				str.AppendLine("</div>");
				(RadListView1.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
			}
		}
		protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
		{
			if (e.Exception != null)
			{
				if (Convert.ToInt32(e.ReturnValue)>0)
				{ 
				
				}
			}
		}
		protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
		{
			ViewState["isClickInsert"] = false;
			switch (e.CommandName)
			{
				case "InitInsert":
					ViewState["isClickInsert"] = true;									
					break;

				case "Buscar":
					ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
					RadListView1.DataBind();
					break;
			}
			this.AnalizarCommand(e.CommandName);


		}
		protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
		{
			if (e.Item.ItemType == RadListViewItemType.DataItem)
			{
				if (Convert.ToBoolean(ViewState["isClickInsert"]))
				{
					e.Item.FindControl("pnItemMaster").Visible = false;
					
					return;
				}
				else
				{
					ViewState["toolbars"] = true;
				}
			}
		}		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.OcultarPaginador(RadListView1, "RadDataPager1", "BotonesBarra");
		}
		protected void OcultarPaginador(RadListView rlv, string idPaginador, string idPanelBotones)
		{
			if (rlv != null)
			{
				Control paginador = rlv.FindControl(idPaginador);
				if (paginador != null)
				{
					paginador.Visible = (bool)ViewState["toolbars"];
				}
				if (rlv.Items.Count > 0 && rlv.Items[0].ItemType == RadListViewItemType.DataItem)
				{
					(rlv.Items[0].FindControl(idPanelBotones) as Control).Visible = (bool)ViewState["toolbars"];
				}
			}
		}
		protected void AnalizarCommand(string comando)
		{
			if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
			{
				ViewState["toolbars"] = true;
			}
			else
			{
				ViewState["toolbars"] = false;
			}
		}
		protected void RadListView1_ItemInserting(object sender, RadListViewCommandEventArgs e)
		{
			ObjectDataSource1.InsertParameters["CA_CLAVE2"].DefaultValue = (e.ListViewItem.FindControl("rcb_clave2") as RadComboBox).SelectedValue;
			ObjectDataSource1.InsertParameters["CA_CLAVE3"].DefaultValue = (e.ListViewItem.FindControl("rcb_clave3") as RadComboBox).SelectedValue;
		}
		protected void RadListView1_ItemEditing(object sender, RadListViewCommandEventArgs e)
		{
			ObjectDataSource1.InsertParameters["CA_CLAVE2"].DefaultValue = (e.ListViewItem.FindControl("rcb_clave2") as RadComboBox).SelectedValue;
			ObjectDataSource1.InsertParameters["CA_CLAVE3"].DefaultValue = (e.ListViewItem.FindControl("rcb_clave3") as RadComboBox).SelectedValue;
		}
		protected void identificacion_TextChanged(object sender, EventArgs e)
		{
			string lc_nombre = "";
			if (!string.IsNullOrWhiteSpace(((RadTextBox)sender).Text.Trim()))
			{
				lc_nombre = CtrlCalidadBL.GetNomTerceros(null, ((RadTextBox)sender).Text);
				if (!string.IsNullOrWhiteSpace(lc_nombre.Trim()))
				{
					((RadTextBox)rliItems.FindControl("rdt_nombre")).Text = lc_nombre;
				}
				else
				{
					LitTitulo.Text = "Error";
					litTextoMensaje.Text = "<li>Persona No Existe.</li>";
					((RadTextBox)rliItems.FindControl("rdt_identificacion")).Text = "";
					ModalPopupExtender1.Show();
				}
			}
		}
		protected void referencia_TextChanged(object sender, EventArgs e)
		{
			string lc_linea = "";
			if (!string.IsNullOrWhiteSpace(((RadTextBox)sender).Text.Trim()))
			{
				lc_linea = CtrlCalidadBL.GetTP(null, ((RadTextBox)sender).Text);
				if (!string.IsNullOrWhiteSpace(lc_linea.Trim()))
				{
					((RadComboBox)rliItems.FindControl("rcb_linea")).SelectedValue = lc_linea;
				}
				else
				{
					LitTitulo.Text = "Error";
					litTextoMensaje.Text = "<li>Referencia No Existe.</li>";
					((RadTextBox)rliItems.FindControl("rdt_referencia")).Text = "";					
					ModalPopupExtender1.Show();
				}
			}
		}
		protected void bodega_SelectedIndexChanged(object sender, EventArgs e)
		{
			((RadDatePicker)rliItems.FindControl("txt_fecha")).SelectedDate = DateTime.Today;
		}

	}
}