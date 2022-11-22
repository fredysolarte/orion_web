using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Consultas;
using System.Data;
using System.Text;

namespace XUSS.WEB.ControlesUsuario
{
	public partial class Ctl_BodegaFecha : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void btnCancel_click(object sender, EventArgs e)
		{
			ModalPopupFinder.Hide();
		}
		protected void btnBuscar_click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			StringBuilder lc_cadena = new StringBuilder();
			string filtro="";

			ModalPopupFinder.Hide();
			
			((RadTextBox)this.Parent.FindControl("txt_almacen")).Text = rcb_bodega.Text;
			((RadDatePicker)this.Parent.FindControl("rdpFecIni")).SelectedDate = rdpFecIni.SelectedDate;
			((RadDatePicker)this.Parent.FindControl("rdpFecFin")).SelectedDate = rdpFecFin.SelectedDate;
			filtro = " AND BDBODEGA ='" + rcb_bodega.SelectedValue + "' AND FHFECFAC BETWEEN '" + Convert.ToDateTime(rdpFecIni.SelectedDate).ToString("yyyyMMdd") 
				    + "' AND '" + Convert.ToDateTime(rdpFecFin.SelectedDate).ToString("yyyyMMdd") + "'";

			try
			{
				//----Ventas
				//dt = ConsultasBL.GetVentas("", filtro);
				//cellpadding=10
				lc_cadena.AppendLine("<table cellspacing=10>");
				lc_cadena.AppendLine("<tr>");
				lc_cadena.AppendLine("<td><label>Estado</Label></td>");
				lc_cadena.AppendLine("<td><label>Factura</Label></td>");
				lc_cadena.AppendLine("<td><label>SubTotal</Label></td>");
				lc_cadena.AppendLine("<td><label>IVA</Label></td>");
				lc_cadena.AppendLine("<td><label>Total</Label></td>");
				lc_cadena.AppendLine("</tr>");
				foreach (DataRow rw in dt.Rows)
				{
					lc_cadena.AppendLine("<tr>");
					lc_cadena.AppendLine("<td>" + rw[0].ToString() + "</td>");
					lc_cadena.AppendLine("<td>" + rw[1].ToString() + "</td>");
					lc_cadena.AppendLine("<td>" + rw[3].ToString() + "</td>");
					lc_cadena.AppendLine("<td>" + rw[4].ToString() + "</td>");
					lc_cadena.AppendLine("<td>" + rw[5].ToString() + "</td>");
					lc_cadena.AppendLine("</tr>");
				}
				lc_cadena.AppendLine("</table>");
				((Literal)this.Parent.FindControl("Literal1")).Text = lc_cadena.ToString();
				
				//----Pagos


			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				dt = null;
				lc_cadena = null;
			}

		}
	}
}