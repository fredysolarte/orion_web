using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using XUSS.BLL.Comun;
using Telerik.Web.UI;
using XUSS.BLL.Consultas;
using System.Text;

namespace XUSS.WEB.Consultas
{
    public partial class ConDescuentosArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            double ln_valor;
            StringBuilder str = new StringBuilder();

            ConDescuentosArticulosBL obj = new ConDescuentosArticulosBL();
            ln_valor = obj.GetDescuento(null, ctrlArticulos1.objrcb_linea.SelectedValue, ctrlArticulos1.objrcb_clave1.SelectedValue, ctrlArticulos1.objrcb_clave2.SelectedValue, ctrlArticulos1.objrcb_clave3.SelectedValue, ctrlArticulos1.objrcb_clave4.SelectedValue, rc_bodega.SelectedValue);

            
            str.AppendLine("<div id=\"box-messages\" class=\"box\">");
            str.AppendLine("<div class=\"messages\">");
            str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
            str.AppendLine("    <div class=\"image\">");
            str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"42\" />");
            str.AppendLine("		</div>");
            str.AppendLine("    <div class=\"text\">");
            str.AppendLine("        <h5>Información</h5>");
            str.AppendLine("        <span style=\"font-size:30px\">" + Convert.ToString(ln_valor) + " %</span>");            
            str.AppendLine("    </div>");
            str.AppendLine("</div>");
            str.AppendLine("</div>");
            str.AppendLine("</div>");

            litEmptyMessage.Text = str.ToString();
        }
       
        
    }
}