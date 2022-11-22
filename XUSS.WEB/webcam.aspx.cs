using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XUSS.WEB
{
    public partial class webcam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.InputStream.Length > 0)
                {
                    using (StreamReader reader = new StreamReader(Request.InputStream))
                    {
                        Random random = new Random();
                        int random_0 = random.Next(0, 100);
                        int random_1 = random.Next(0, 100);
                        int random_2 = random.Next(0, 100);
                        int random_3 = random.Next(0, 100);
                        int random_4 = random.Next(0, 100);
                        int random_5 = random.Next(0, 100);

                        string hexString = Server.UrlEncode(reader.ReadToEnd());
                        string imageName = Convert.ToString(Session["UserLogon"])+ "-" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss"+Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) );
                        string imagePath = string.Format("~/Uploads/{0}.png", imageName);
                        File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                        Session["imagePath"] = MapPath("~/Uploads/" + imageName+".png");
                        Session["CapturedImage"] = ResolveUrl(imagePath);
                        random = null;
                    }
                }
            }
        }
        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        [WebMethod(EnableSession = true)]
        public static string GetCapturedImage()
        {
            string url = HttpContext.Current.Session["CapturedImage"].ToString();
            HttpContext.Current.Session["CapturedImage"] = null;
            return url;
        }
    }
}