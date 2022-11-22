using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.Administracion;
using System.Text;
using BE.Administracion;
using Telerik.Web.UI;
using System.IO;

namespace XUSS.WEB.ControlesUsuario
{
    public partial class LoadFileBinary : System.Web.UI.UserControl
    {        
        public int ActiveViewIndex
        {
            get { return MultiView1.ActiveViewIndex; }
            set
            {
                MultiView1.ActiveViewIndex = value;
                MultiView2.ActiveViewIndex = value;
            }
        }

        public string Text
        {
            get { return txtTexto.Text; }
            set { txtTexto.Text = value; }
        }

        public int? IdBlob
        {
            get
            {
                if (ViewState["IdBlob" + this.ID] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(ViewState["IdBlob" + this.ID]);
                }
            }
            set
            {
                ViewState["IdBlob" + this.ID] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            Type cstype = this.Page.GetType();
            AdmiTipoblobBL tipoBL = new AdmiTipoblobBL();
            AdmiTipoblob tipo;
            StringBuilder cstext2 = new StringBuilder();
            tipo = tipoBL.GetById(null, MultiView1.ActiveViewIndex);

            switch (MultiView1.ActiveViewIndex)
            {
                case 0:
                    cstext2.Append("<script type=\"text/javascript\">");
                    cstext2.Append("        function " + this.ID + "fileUploaded(sender, args) {");
                    cstext2.Append("            $find('" + RadAjaxManager.GetCurrent(Page).ClientID + "').ajaxRequestWithTarget('" + rauImagen.UniqueID + "','');");
                    cstext2.Append("        }");
                    cstext2.Append("        function " + this.ID + "fileDeleted (sender, args) {");
                    cstext2.Append("            if(document.getElementById('" + Thumbnail.ClientID + "')!=null){");
                    cstext2.Append("                document.getElementById('" + Thumbnail.ClientID + "').src = '';");
                    cstext2.Append("                document.getElementById('" + Thumbnail.ClientID + "').style.display = 'none'");
                    cstext2.Append("            }");
                    cstext2.Append("        }");
                    cstext2.Append("</script>");
                    RadScriptManager.RegisterStartupScript(Page, cstype, this.ID + "fileDeleted", cstext2.ToString(), false);
                    //rauImagen.OnClientDeleting = this.ID + "fileDeleted";
                    tipo = tipoBL.GetById(null, 0);

                    if (!string.IsNullOrEmpty(tipo.TipbFiltro))
                    {
                        rauImagen.AllowedFileExtensions = tipo.TipbFiltro.Split(',');
                    }

                    rauImagen.OnClientFileUploaded = this.ID + "fileUploaded";
                    break;

                case 1:
                    cstext2.Append("<script type=\"text/javascript\">");
                    cstext2.Append("        function " + this.ID + "fileUploadedText(sender, args) {");
                    cstext2.Append("            $find('" + RadAjaxManager.GetCurrent(Page).ClientID + "').ajaxRequestWithTarget('" + rauTexto.UniqueID + "','');");
                    cstext2.Append("            Array.removeAt(sender._uploadedFiles, 0);");
                    cstext2.Append("            sender.deleteFileInputAt(0);");
                    cstext2.Append("        }");
                    cstext2.Append("</script>");
                    RadScriptManager.RegisterStartupScript(Page, cstype, this.ID + "fileUploadedText", cstext2.ToString(), false);
                    if (!string.IsNullOrEmpty(tipo.TipbFiltro))
                    {
                        rauTexto.AllowedFileExtensions = tipo.TipbFiltro.Split(',');
                    }

                    rauTexto.OnClientFileUploaded = this.ID + "fileUploadedText";
                    break;

                case 2:
                    cstext2.Append("<script type=\"text/javascript\">");
                    cstext2.Append("        function " + this.ID + "fileUploadedText(sender, args) {");
                    cstext2.Append("            $find('" + RadAjaxManager.GetCurrent(Page).ClientID + "').ajaxRequestWithTarget('" + rauHTML.UniqueID + "','');");
                    cstext2.Append("            Array.removeAt(sender._uploadedFiles, 0);");
                    cstext2.Append("            sender.deleteFileInputAt(0);");
                    cstext2.Append("        }");
                    cstext2.Append("</script>");
                    RadScriptManager.RegisterStartupScript(Page, cstype, this.ID + "fileUploadedText", cstext2.ToString(), false);
                    rauHTML.OnClientFileUploaded = this.ID + "fileUploadedText";

                    if (!string.IsNullOrEmpty(tipo.TipbFiltro))
                    {
                        rauHTML.AllowedFileExtensions = tipo.TipbFiltro.Split(',');
                    }
                    break;

                case 3:
                    if (!string.IsNullOrEmpty(tipo.TipbFiltro))
                    {
                        rauBinario.AllowedFileExtensions = tipo.TipbFiltro.Split(',');
                    }

                    break;
            }
        }

        protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            Thumbnail.Visible = true;
            Thumbnail.Width = Unit.Pixel(200);
            Thumbnail.Height = Unit.Pixel(150);
            byte[] imageData = new byte[e.File.InputStream.Length];
            using (Stream stream = e.File.InputStream)
            {
                stream.Read(imageData, 0, (int)e.File.InputStream.Length);
            }
            ViewState["imagenCargada"] = ((FileStream)e.File.InputStream).Name;
            ViewState["nombreImagenCargada"] = e.File.GetName();
            Thumbnail.DataValue = imageData;
        }

        protected void rauTexto_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                StreamReader streamReader = new StreamReader(stream);
                rtbTexto.Text = streamReader.ReadToEnd();
                e.IsValid = false;
            }
        }

        protected void rauHTML_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            using (Stream stream = e.File.InputStream)
            {
                StreamReader streamReader = new StreamReader(stream);
                rdeHTML.Content = streamReader.ReadToEnd();
                e.IsValid = false;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            AdmiBlob blob = new AdmiBlob();
            blob.BlobNombre = this.rtbNombre.Text;
            blob.BlobDescripcion = null;
            blob.BlobVersion = null;
            blob.LogsFecha = DateTime.Now;
            blob.LogsUsuario = (int)Session["UserId"];
            blob.TipbTipoblob = MultiView1.ActiveViewIndex;
            if (rtbNombre.Text.Length <= 0)
            {
                lblMensaje.Text = "El nombre es obligatoria";
                return;
            }
            switch (MultiView1.ActiveViewIndex)
            {
                case 0:
                    if (ViewState["imagenCargada"] != null)
                    {
                        using (Stream file = new FileStream(ViewState["imagenCargada"].ToString(), FileMode.Open))
                        {
                            if (file != null)
                            {
                                byte[] imageData = new byte[file.Length];
                                StreamReader streamReader = new StreamReader(file);
                                blob.BlobOrigen = ViewState["nombreImagenCargada"].ToString();
                                file.Read(imageData, 0, (int)file.Length);
                                blob.BlobBinario = imageData;
                            }
                            else
                            {
                                lblMensaje.Text = "La imagen es obligatoria";
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "La imagen es obligatoria";
                        return;
                    }
                    break;

                case 1:
                    if (string.IsNullOrEmpty(rtbTexto.Text))
                    {
                        lblMensaje.Text = "El texto es obligatorio";
                        return;
                    }
                    blob.BlobOrigen = null;
                    blob.BlobBinario = ASCIIEncoding.ASCII.GetBytes(rtbTexto.Text);
                    break;

                case 2:
                    if (string.IsNullOrEmpty(rdeHTML.Text))
                    {
                        lblMensaje.Text = "El texto es obligatorio";
                        return;
                    }
                    blob.BlobOrigen = null;
                    blob.BlobBinario = ASCIIEncoding.ASCII.GetBytes(rdeHTML.Content);
                    break;

                case 3:
                    if (rauBinario.UploadedFiles.Count <= 0)
                    {
                        lblMensaje.Text = "El archivo es obligatorio";
                        return;
                    }
                    using (Stream file = rauBinario.UploadedFiles[0].InputStream)
                    {
                        byte[] binData = new byte[file.Length];
                        StreamReader streamReader = new StreamReader(rauBinario.UploadedFiles[0].InputStream);
                        blob.BlobOrigen = rauBinario.UploadedFiles[0].GetName();
                        file.Read(binData, 0, (int)file.Length);
                        blob.BlobBinario = binData;
                    }
                    break;

            }
            lblMensaje.Text = "";
            AdmiBlobBL blobBL = new AdmiBlobBL();
            int idBlob = blobBL.Insert(blob);
            txtTexto.Text = blob.BlobNombre;
            ViewState["IdBlob" + this.ID] = idBlob;
            ModalPopupExtender1.Hide();
        }

        protected void RadListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadListView1.SelectedItems[0].FindControl("BlobNombreLabel") != null)
            {
                txtTexto.Text = ((Label)RadListView1.SelectedItems[0].FindControl("BlobNombreLabel")).Text;
                ViewState["IdBlob" + this.ID] = (int)RadListView1.SelectedValue;
                ModalPopupExtender2.Hide();
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (rnbIdBlob.Text.Length > 0)
            {
                ObjectDataSource1.SelectParameters["filter"].DefaultValue = "blob_blob =" + rnbIdBlob.Text;
            }
            else
            {
                if (rtbBuscar.Text.Length > 0)
                {
                    ObjectDataSource1.SelectParameters["filter"].DefaultValue = "blob_nombre LIKE '%" + rtbBuscar.Text + "%'";
                }
                else
                {
                    ObjectDataSource1.SelectParameters["filter"].DefaultValue = "";
                }
            }
            DataBindChildren();
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadGrid tmp = (RadGrid)sender;
            if (tmp.SelectedItems[0].FindControl("BlobNombreLabel") != null)
            {
                txtTexto.Text = ((Label)tmp.SelectedItems[0].FindControl("BlobNombreLabel")).Text;
                ViewState["IdBlob" + this.ID] = (int)tmp.SelectedValue;
                ModalPopupExtender2.Hide();
            }
        }

        protected void VerMas(object sender, EventArgs e)
        {
            LinkButton chkBox = (sender as LinkButton);
            GridDataItem dataItem = chkBox.NamingContainer as GridDataItem;
            AdmiBlobBL blobBL = new AdmiBlobBL();
            AdmiBlob blob = blobBL.GetById(null, null, 0, 0, Convert.ToInt32(((Label)dataItem.FindControl("BlobBlobLabel")).Text));
            rtbVerTexto.Text = ASCIIEncoding.ASCII.GetString(blob.BlobBinario);
            ModalPopupExtender3.Show();
        }

        protected void VerMasHTML(object sender, EventArgs e)
        {
            LinkButton chkBox = (sender as LinkButton);
            GridDataItem dataItem = chkBox.NamingContainer as GridDataItem;
            AdmiBlobBL blobBL = new AdmiBlobBL();
            AdmiBlob blob = blobBL.GetById(null, null, 0, 0, Convert.ToInt32(((Label)dataItem.FindControl("BlobBlobLabel")).Text));
            raeHTMLVer.Enabled = false;
            raeHTMLVer.Style.Add("overflow", "auto");
            raeHTMLVer.Content = ASCIIEncoding.ASCII.GetString(blob.BlobBinario);
            ModalPopupExtender4.Show();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
            switch (MultiView1.ActiveViewIndex)
            {
                case 0:

                    RadListView1.DataSourceID = "ObjectDataSource1";
                    RadListView1.DataBind();
                    break;

                case 1:

                    RadGrid1.DataSourceID = "ObjectDataSource1";
                    RadGrid1.DataBind();
                    break;

                case 2:

                    RadGrid2.DataSourceID = "ObjectDataSource1";
                    RadGrid2.DataBind();
                    break;

                case 3:

                    RadGrid3.DataSourceID = "ObjectDataSource1";
                    RadGrid3.DataBind();
                    break;
            }
        }
    }
}