using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data;

namespace XUSS.WEB.Costos
{
	public partial class Costos : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void BuscarGrilla(object sender, EventArgs e)
		{
			string filtro = "AND ICCLAVE1 IS NOT NULL ";

			//filtro = "AND 1=1 ";
			//if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rtbCargue") as RadTextBox).Text))
			//{
				//filtro += " AND ICCONSE =" + (((Button)sender).Parent.FindControl("rtbCargue") as RadTextBox).Text;
			//}
			if ((((Button)sender).Parent.FindControl("rcbMarca") as RadComboBox).SelectedValue != "")
			{
				filtro += " AND ICMARCA ='" + (((Button)sender).Parent.FindControl("rcbMarca") as RadComboBox).SelectedValue + "'";
			}

			if ((((Button)sender).Parent.FindControl("rcbTipPro") as RadComboBox).SelectedValue != "")
			{
				filtro += " AND ICTIPPRO ='" + (((Button)sender).Parent.FindControl("rcbTipPro") as RadComboBox).SelectedValue + "'";
			}

			if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rtbrefxuss") as RadTextBox).Text))
			{
				filtro += " AND ICCLAVE1 = '" + (((Button)sender).Parent.FindControl("rtbrefxuss") as RadTextBox).Text + "'";
			}

			if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("rdtrefImportada") as RadTextBox).Text))
			{
				filtro += " AND ICREFERENCIA ='" + (((Button)sender).Parent.FindControl("rdtrefImportada") as RadTextBox).Text + "'";
			}

			if ((((Button)sender).Parent.FindControl("rcbestado") as RadComboBox).SelectedValue != "")
			{
				filtro += " AND ICESTPRE ='" + (((Button)sender).Parent.FindControl("rcbestado") as RadComboBox).SelectedValue + "'";
			}

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

		protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
		{
			//switch (e.CommandName)
			//{
			//    case "Buscar":
			//        ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
			//        RadListView1.DataBind();
			//        break;
			//}			
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
            }
            else
            {
                ViewState["isClickInsert"] = false;                
            }
            switch (e.CommandName)
            {
                case "Buscar":
                   ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
                    RadListView1.DataBind();
                    e.Canceled = true;
                    break;
                default:
                    break;
            }

        
		}

		protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
		{
//            DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
//            if (!fila.IsNull("IM_IMAGEN"))
//            {
//                if ((((sender as RadBinaryImage).Parent.FindControl("RadBinaryImage1") as RadBinaryImage).DataValue) != null)
//                { 
				
//                }
////(e.Item.FindControl("rblTipo") as RadBinaryImage).DataValue = fila["coce_dbcr"].

//            }
		}

		protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
		{
			//if (e.Exception != null)
			//{
			//    LitTitulo.Text = "Error";
			//    litTextoMensaje.Text = "<li>Error Actualizando.</li>" + e.ToString();
			//    e.ExceptionHandled = true;
			//    ModalPopupExtender2.Show();				
			//}
		}

		protected void RadListView1_ItemUpdating(object sender, RadListViewCommandEventArgs e)
		{
			ObjectDataSource1.UpdateParameters["ICCDUSER"].DefaultValue = Session["UserLogon"] as string;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            mpPrecosteo.Show();
        }

	}
}