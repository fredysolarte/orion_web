using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace AseingesOut.SCW.WEB.SIC.Administrador
{
	public partial class download : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["archivo"] != null)
			{
				DescargarArchivo(Session["archivo"].ToString());
			}
		}

		private void DescargarArchivo(string ruta)
		{
			// Gets the File Name
			string fileName = ruta.Substring(ruta.LastIndexOf('\\') + 1); ;
			byte[] buffer;

			using (FileStream fileStream = new FileStream(ruta, FileMode.Open))
			{
				int fileSize = (int)fileStream.Length;
				buffer = new byte[fileSize];
				// Read file into buffer
				fileStream.Read(buffer, 0, (int)fileSize);
			}

			Response.Clear();
			Response.Buffer = true;
			Response.BufferOutput = true;
			Response.ContentType = "application/x-download";
			Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
			Response.CacheControl = "public";
			// writes buffer to OutputStream
			Response.OutputStream.Write(buffer, 0, buffer.Length);
			Response.End();
		}
	}
}