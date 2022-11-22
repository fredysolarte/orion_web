using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Consultas
{
    public partial class EstadoPedidosGantt : System.Web.UI.Page
    {
        private const string ProviderSessionKey = "RadGanttPdfExport";
        private void Page_Init(object sender, EventArgs e)
        {
            gt_pedidos.ExportSettings.Pdf.ProxyURL = ResolveUrl("~/api/export/file");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 1; i < rc_mes.Items.Count; i++)
            {
                if (Convert.ToInt32(rc_mes.Items[i].Value) == System.DateTime.Today.Month)                
                    rc_mes.Items[i].Checked = true;

                if (Convert.ToInt32(rc_mes.Items[i].Value) == System.DateTime.Today.Month-1)
                    rc_mes.Items[i].Checked = true;
            }

            for (i = 1; i < rc_ano.Items.Count; i++)
            {
                if (Convert.ToInt32(rc_ano.Items[i].Value) == System.DateTime.Today.Year)
                    rc_ano.Items[i].Checked = true;
            }

            btn_buscar_Click(sender, null);
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            string filtro = "",lc_in="";
            var collection = rc_estado.CheckedItems;
            if (collection.Count != 0)
            {
                filtro += " AND ESTADO IN (";
                foreach (var item in collection)
                {
                    lc_in += "'" + Convert.ToString(item.Value) + "',";
                }
                filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";                
            }

            
            lc_in = "";
            var coll_mes = rc_mes.CheckedItems;
            if (coll_mes.Count != 0)
            {
                filtro += " AND MONTH([start]) IN (";
                foreach (var item in coll_mes)                
                    lc_in += "'" + Convert.ToString(item.Value) + "',";                

                filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";
            }

            
            lc_in = "";
            var coll_year = rc_ano.CheckedItems;
            if (coll_year.Count != 0)
            {
                filtro += " AND YEAR([start]) IN (";
                foreach (var item in coll_year)                
                    lc_in += "'" + Convert.ToString(item.Value) + "',";                

                filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";
            }

            obj_consulta.SelectParameters["inFilter"].DefaultValue = filtro;
            gt_pedidos.DataBind();
        }
    }
}