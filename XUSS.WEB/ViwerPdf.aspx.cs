using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB
{
    public partial class ViwerPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lc_ruta = Request.QueryString["rta"];
            displaypdf1.Visible = true;
            displaypdf1.FilePath = @"~/Uploads/" + lc_ruta;
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string lc_ruta = Request.QueryString["rta"];
            displaypdf1.Visible = true;
            //displaypdf1.FilePath = MapPath("~/Uploads/" + lc_ruta);
            displaypdf1.FilePath = @"~/Uploads/" + lc_ruta;            
            //HyperLink1.NavigateUrl = @"~/pdf/" + Uploadedfilename;            
        }
    }
}