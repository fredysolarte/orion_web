using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using XUSS.BLL.Reportes;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Configuration;
using XUSS.BLL.Mail;
using System.Text;
using XUSS.BLL.Comun;
using VisorBL = XUSS.BLL.Reportes.VisorBL;
using BLL.Administracion;

namespace XUSS.WEB.Reportes
{
    [Serializable]
    public class Iruta
    {
        public string lRuta { get; set; }
    }

    public partial class VisorPantallaCompleta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FastReport.Utils.Config.WebMode = true;
            DataTable dt = new DataTable();            
            List<Iruta> lst = new List<Iruta>();

            try
            {
                //configuracion server mail
                txt_server.Text = ConfigurationManager.AppSettings.Get("MAIL_SERVER");
                txt_user.Text = ConfigurationManager.AppSettings.Get("MAIL_USER");
                txt_password.Text = ConfigurationManager.AppSettings.Get("MAIL_PASSWORD");
                txt_port.Text = ConfigurationManager.AppSettings.Get("MAIL_PORT");

                dt = VisorBL.GetReporte(null, Convert.ToInt32(Request.QueryString["rpt"]));
                if (dt.Rows.Count > 0)
                {
                    byte[] repor = (byte[])dt.Rows[0][0];
                    MemoryStream stream = new MemoryStream(repor);

                    WebReport1.Report.Load(stream);
                    if (Convert.ToString(Request.QueryString["inban"]) == "S")
                    {
                        int i = 0;
                        String[] param = Request.QueryString["inParametro"].Split(',');
                        foreach (var pr in param)
                        {
                            WebReport1.Report.SetParameterValue(Convert.ToString(pr), Request.QueryString["inValor"].Split(',')[i]);
                            i++;
                        }
                    }

                    string lc_close = "";
                    if (Convert.ToString(Request.QueryString["inImp"]) == "S")
                    {
                        Random random = new Random();
                        int random_0 = random.Next(0, 10000);
                        WebReport1.Report.Prepare();
                        if (Convert.ToString(Request.QueryString["inClo"]) == "S")
                            lc_close = " window.close(); ";

                        string lc_webrepot = "/FastReport.Export.axd?object=" + WebReport1.ReportGuid + "&print_pdf=1&s=" + Convert.ToString(random_0);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", " var wait = 9000; var date = new Date(); var startDate = date.getTime(); var a = 1; var b = 0; while (a !== 0) { date = new Date(); if ((date.getTime() - startDate) >= wait) {a = 0;} b++; } " + lc_close + " window.open('" + lc_webrepot + "'); ", true);
                    }
                    stream = null;
                    repor = null;


                    //Para Envio de Email de Aptrobacion
                    if (Convert.ToString(Request.QueryString["inSend"]) == "S")
                    {
                        StringBuilder sTexto = new StringBuilder();

                        sTexto.AppendLine("<html>");
                        sTexto.AppendLine("<head></head><body>");
                        sTexto.AppendLine("<p>Dear All,</p>");
                        sTexto.AppendLine("<p>Our system has detected a request, initiated in your name, for the approval of a purchase order. If you are aware of this fact, click on the following link (or copy and paste it into your browser),</p>");
                        sTexto.AppendLine("<p>​</p>");
                        sTexto.AppendLine("<p><a href= http://" + HttpContext.Current.Request.Url.Authority + "/Compras/AprobacionOC.aspx?NroCmp=" + Request.QueryString["inValor"] + "&CodEmp=" + Convert.ToString(Session["CODEMP"]) + "&IdUser=" + Convert.ToString(Session["UserLogon"]) + ">Approve Purchar Order</a></p>");
                        sTexto.AppendLine("<p>​</p>");
                        sTexto.AppendLine("<p>if you want to edit the information enter the following link,</p>");
                        sTexto.AppendLine("<p>​</p>");
                        sTexto.AppendLine("<p><a href=http://" + HttpContext.Current.Request.Url.Authority + "/Compras/CambioCantidadesOC.aspx?NroCmp=" + Request.QueryString["inValor"] + "&CodEmp=" + Convert.ToString(Session["CODEMP"]) + "&IdUser=" + Convert.ToString(Session["UserLogon"]) + ">Edit Purchar Order</a></p>");
                        sTexto.AppendLine("<p>​</p>");
                        sTexto.AppendLine("<p>If not, please disregard this message</p>");
                        sTexto.AppendLine("</body>");
                        sTexto.AppendLine("</html>");

                        edt_body.Content = sTexto.ToString();

                        mpEmail.Show();
                    }
                }
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
        private string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }
       
        protected void btn_enviar_Click(object sender, EventArgs e)
        {
            UMail Obj = new UMail();
            FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
            System.IO.MemoryStream result = new System.IO.MemoryStream();
            string lc_archivo="",lc_usuario="";
            string[] lc_rutas={""};
            Random random = new Random();
            DataTable dtTO = UMail.NewToDataTable();
            AdmiUsuarioBL ObjU = new AdmiUsuarioBL();
            int i = 0;
            try
            {
                if (Convert.ToString(Request.QueryString["inSend"]) == "S")
                {
                    StringBuilder sTexto = new StringBuilder();

                    using (IDataReader reader = ObjU.GetDataUser(" AND LOWER(usua_email)='" + txt_para.Entries[0].Text.Substring(txt_para.Entries[0].Text.IndexOf(" (") + 2, txt_para.Entries[0].Text.Substring(txt_para.Entries[0].Text.IndexOf(" (") + 2).Length - 1).ToLower() + "'"))
                    {
                        while (reader.Read())
                        {
                            lc_usuario = Convert.ToString(reader["usua_usuario"]);
                        }
                    }

                    sTexto.AppendLine("<html>");
                    sTexto.AppendLine("<head></head><body>");
                    sTexto.AppendLine("<p>Dear All,</p>");
                    sTexto.AppendLine("<p>Our system has detected a request, initiated in your name, for the approval of a purchase order. If you are aware of this fact, click on the following link (or copy and paste it into your browser),</p>");
                    sTexto.AppendLine("<p>​</p>");
                    sTexto.AppendLine("<p><a href= http://" + HttpContext.Current.Request.Url.Authority + "/Compras/AprobacionOC.aspx?NroCmp=" + Request.QueryString["inValor"] + "&CodEmp=" + Convert.ToString(Session["CODEMP"]) + "&IdUser=" + Convert.ToString(lc_usuario) + ">Approve Purchar Order</a></p>");
                    sTexto.AppendLine("<p>​</p>");
                    sTexto.AppendLine("<p>if you want to edit the information enter the following link,</p>");
                    sTexto.AppendLine("<p>​</p>");
                    sTexto.AppendLine("<p><a href=http://" + HttpContext.Current.Request.Url.Authority + "/Compras/CambioCantidadesOC.aspx?NroCmp=" + Request.QueryString["inValor"] + "&CodEmp=" + Convert.ToString(Session["CODEMP"]) + "&IdUser=" + Convert.ToString(lc_usuario) + ">Edit Purchar Order</a></p>");
                    sTexto.AppendLine("<p>​</p>");
                    sTexto.AppendLine("<p>If not, please disregard this message</p>");
                    sTexto.AppendLine("</body>");
                    sTexto.AppendLine("</html>");

                    edt_body.Content = sTexto.ToString();                    

                }

                //Export to PDF
                WebReport1.Report.Prepare();
                export.OpenAfterExport = false;
                
                //Archivo
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                int random_6 = random.Next(0, 100);
                int random_7 = random.Next(0, 100);
                lc_archivo = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(random_6) + Convert.ToString(random_7) + ".pdf";
                lc_rutas[0] = MapPath("~/Uploads/" + lc_archivo);                               
                WebReport1.Report.Export(export, MapPath("~/Uploads/"+lc_archivo));

                //Destinatario
                //dtTO.Rows.Add(txt_para.Text, string.Empty);
                for (i = 0; i < txt_para.Entries.Count; i++)
                {
                    //txt_para.Entries[i].Text
                    if (txt_para.Entries[i].Text.IndexOf(" (") == -1 )
                        dtTO.Rows.Add(txt_para.Entries[i].Text, string.Empty);
                    else
                        dtTO.Rows.Add(txt_para.Entries[i].Text.Substring(txt_para.Entries[i].Text.IndexOf(" (") + 2, txt_para.Entries[i].Text.Substring(txt_para.Entries[i].Text.IndexOf(" (") + 2).Length - 1), string.Empty);
                }

                //Send Email
                string lc_retorno = Obj.SendMail(txt_server.Text, txt_user.Text, txt_password.Text, txt_user.Text, txt_user.Text, dtTO, txt_asunto.Text, edt_body.Content, null, lc_rutas,Convert.ToInt32(txt_port.Text));
                
                ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);

                litTextoMensaje.Text = "¡Se Envio de Manera Correcta el Email!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                //throw ex;
            }
            finally
            {
                export = null;
                result = null;
                Obj = null;
                random = null;
                dtTO = null;
                ObjU = null;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            mpEmail.Show();
        }
    }
}