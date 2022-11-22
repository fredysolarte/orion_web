using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;

namespace XUSS.WEB.Consultas
{
    public partial class CtaUsuariosRegistrados : System.Web.UI.Page
    {
        protected void BuscarGrilla(object sender, EventArgs e)
        {
            string filtro = " ";



            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_cedula") as RadNumericTextBox).Text))
            {
                filtro += "  TBCODNIT = '" + (((Button)sender).Parent.FindControl("edt_cedula") as RadNumericTextBox).Text + "'";
            }

            

            obj_list.SelectParameters["filter"].DefaultValue = filtro;
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
		
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
                    obj_list.SelectParameters["filter"].DefaultValue = "1=0";
                    RadListView1.DataBind();
                    e.Canceled = true;
                    break;
                case "Edit":
                    if (!string.IsNullOrEmpty(((sender as RadListView).Items[0].FindControl("edt_factura") as RadTextBox).Text))
                    {
                        litTextoMensaje.Text = "Ya se Encuentra Redimido";
                        LitTitulo.Text = "Error";
                        ModalPopupExtender2.Show();
                        e.Canceled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void obj_list_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
//            e.InputParameters[3] = RadListView1.i
        }

    }
}