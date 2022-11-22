using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Comun;
using System.Data;
using System.IO;

namespace XUSS.WEB.Reportes
{
    public partial class Visor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            //}
            DataTable dt = new DataTable();
            try
            {
                dt = VisorBL.GetReporte(null, Convert.ToInt32(Request.QueryString["rpt"]));
                byte[] repor = (byte[])dt.Rows[0][0];
                MemoryStream stream = new MemoryStream(repor);
                WebReport1.Report.Load(stream);
                stream = null;
                repor = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dt = null;                
            }
        }
    }
}