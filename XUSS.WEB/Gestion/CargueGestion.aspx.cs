using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Gestion
{
    public partial class CargueGestion : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Do not display SelectedFilesCount progress indicator.
                RadProgressArea1.ProgressIndicators &= ~Telerik.Web.UI.Upload.ProgressIndicators.SelectedFilesCount;
            }

            RadProgressArea1.Localization.Uploaded = "Total Progress";
            RadProgressArea1.Localization.UploadedFiles = "Progress";
            RadProgressArea1.Localization.CurrentFileName = "Custom progress in action: ";
        }
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            this.ProcesaPlanoGlosa(e.File.InputStream);
        }
        private void ProcesaPlanoGlosa(Stream instream)
        {
            int ln_consecutivo = 35, i = 1;
            
            RadProgressContext progress = RadProgressContext.Current;
            try
            {                
                
                using (Stream stream = instream)
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            if (i == 1)
                            { streamReader.ReadLine(); }
                            else
                            {
                                string lc_cadena = streamReader.ReadLine();
                                string[] words = lc_cadena.Split(',');
                                foreach (string word in words)
                                {

                                }

                                progress.PrimaryTotal = 1;
                                progress.PrimaryValue = 1;
                                progress.PrimaryPercent = 100;
                                progress.SecondaryTotal = stream.Length;
                                progress.SecondaryValue = (stream.Position * 100) / stream.Length;
                                progress.SecondaryPercent = (stream.Position * 100) / stream.Length;
                                progress.CurrentOperationText = "Paso " + Convert.ToInt32((stream.Position * 100) / stream.Length);
                                if (!Response.IsClientConnected) break;
                                progress.TimeEstimated = (stream.Length - stream.Position) * 100;
                                System.Threading.Thread.Sleep(1);
                            }
                            i++;
                        }
                    }
                }
                litTextoMensaje.Text = "Datos Guardados Exitosamente:" + Convert.ToString(ln_consecutivo) + " Por Favor Esperar Notificacion en Bandeja de Entrada";
                mpMensajes.Show();

                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                
            }
        }
    }
}