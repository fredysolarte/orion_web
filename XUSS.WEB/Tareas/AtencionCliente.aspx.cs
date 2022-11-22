using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Tareas
{
    public partial class AtencionCliente : System.Web.UI.Page
    {
        private DataTable tbImagenes
        {
            set { ViewState["tbImagenes"] = value; }
            get { return ViewState["tbImagenes"] as DataTable; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel"))
                ViewState["toolbars"] = true;
            else
                ViewState["toolbars"] = false;
        }
        protected void OcultarPaginador(RadListView rlv, string idPaginador, string idPanelBotones)
        {
            if (rlv != null)
            {
                Control paginador = rlv.FindControl(idPaginador);
                if (paginador != null)
                {
                    paginador.Visible = (bool)ViewState["toolbars"];
                }
                if (rlv.Items.Count > 0 && rlv.Items[0].ItemType == RadListViewItemType.DataItem)
                {
                    (rlv.Items[0].FindControl(idPanelBotones) as Control).Visible = (bool)ViewState["toolbars"];
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_atencion, "RadDataPager1", "BotonesBarra");
        }

        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_idapp") as RadTextBox).Text))
                filtro += " AND AT_CODIGO IN ( SELECT AT_CODIGO FROM TB_INSPECCION WHERE IP_CODIGO =" + (((RadButton)sender).Parent.FindControl("txt_idapp") as RadTextBox).Text + ")";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrocodigo") as RadTextBox).Text))
                filtro += " AND AT_CODIGO = " + (((RadButton)sender).Parent.FindControl("txt_nrocodigo") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_ctacontrato") as RadTextBox).Text))
                filtro += " AND AT_CTACONTRATO = '" + (((RadButton)sender).Parent.FindControl("txt_ctacontrato") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_atencion.SelectParameters["filter"].DefaultValue = filtro;
            rlv_atencion.DataBind();
            if ((rlv_atencion.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");

                (rlv_atencion.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string script = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9021&inban=S&inParametro=inConse&inValor=" + Convert.ToString((rlv_atencion.Items[0].FindControl("txt_codigo") as RadTextBox).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + script + "');", true);
        }

        protected void rlv_atencion_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    QuejasApelacionesBL objt = new QuejasApelacionesBL();
                    try
                    {
                        ViewState["isClickInsert"] = true;
                        tbImagenes = objt.GetImagenes(null, Convert.ToString(Session["CODEMP"]), -1);                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objt = null;
                    }
                    break;

                case "Buscar":
                    obj_atencion.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_atencion.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void rlv_atencion_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;
                    return;
                }
                else
                {
                    ViewState["toolbars"] = true;
                }
            }
            QuejasApelacionesBL Obj = new QuejasApelacionesBL();
            try
            {
                //Cargar Imagenes
                tbImagenes = Obj.GetImagenesAntencionCliente(null, Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text));
                (e.Item.FindControl("rg_imagenes") as RadGrid).DataSource = tbImagenes;
                (e.Item.FindControl("rg_imagenes") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

        protected void rg_imagenes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbImagenes;
        }
        
        protected void rg_imagenes_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["EI_CODIGO"].Text);

                QuejasApelacionesBL Obj = new QuejasApelacionesBL();
                foreach (DataRow rw in (Obj.GetImagenAntencionCliente(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["EI_FOTO"]);                    
                }

                ditem = null;
                Random random = new Random();
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + "" + ".jpg";
                string path = MapPath("~/Uploads/" + lc_nombre);

                File.WriteAllBytes(path, archivo);
                byte[] bts = System.IO.File.ReadAllBytes(path);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lc_nombre);
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
            }
            if (e.CommandName == "print_file") {
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["EI_CODIGO"].Text);

                string url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9022&inban=S&inParametro=inConse&inValor=" + Convert.ToString(item);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);

            }
        }

        protected void rlv_atencion_ItemUpdating(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {

        }

        protected void rlv_atencion_ItemInserted(object sender, Telerik.Web.UI.RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }

        protected void txt_identificacion_TextChanged(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            try
            {
                (((RadTextBox)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text = "0";
                foreach (DataRow rw in Obj.GetTerceros(null, " TRCODNIT='" + (sender as RadTextBox).Text.Trim() + "'", 0, 0).Rows)
                {
                    (((RadTextBox)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(rw["TRCODTER"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text = Convert.ToString(rw["TRCODNIT"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text = Convert.ToString(rw["TRNOMBRE"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_snombre") as RadTextBox).Text = Convert.ToString(rw["TRNOMBR2"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text = Convert.ToString(rw["TRAPELLI"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_sapellido") as RadTextBox).Text = Convert.ToString(rw["TRNOMBR3"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_telefono") as RadTextBox).Text = Convert.ToString(rw["TRNROTEL"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_correo") as RadTextBox).Text = Convert.ToString(rw["TRCORREO"]);
                    (((RadTextBox)sender).Parent.FindControl("txt_direccion") as RadTextBox).Text = Convert.ToString(rw["TRDIRECC"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

        protected void obj_atencion_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "";
                litTextoMensaje.Text = "Nro Radicado :" + Convert.ToString(e.ReturnValue);
                try
                {
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9021&inban=S&inParametro=inConse&inValor=" + Convert.ToString(e.ReturnValue);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
        }

        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            //Clear changes and remove uploaded image from Cache
            //RadImageEditor1.ResetChanges();

            Context.Cache.Remove(Session.SessionID + "UploadedFile");
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);
                MemoryStream ms = new MemoryStream();
                ms.Write(imgData, 0, imgData.Length);

                Context.Cache.Insert(Session.SessionID + "UploadedFile", ms, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }
            RadImageEditor x = new RadImageEditor();
        }
        protected void RadImageEditor1_ImageLoading(object sender, ImageEditorLoadingEventArgs args)
        {
            //Handle Uploaded images
            if (!Object.Equals(Context.Cache.Get(Session.SessionID + "UploadedFile"), null))
            {
                using (EditableImage image = new EditableImage((MemoryStream)Context.Cache.Remove(Session.SessionID + "UploadedFile")))
                {
                    args.Image = image.Clone();
                    args.Cancel = true;
                }
            }
        }

        protected void rg_imagenes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            QuejasApelacionesBL Obj = new QuejasApelacionesBL();            
            int pos = 0;
            int xpos = 0;
            try
            {
                var CODIGO = item.GetDataKeyValue("EI_CODIGO").ToString();
                switch (e.CommandName)
                {
                    case "Delete":

                        foreach (DataRow row in tbImagenes.Rows)
                        {
                            if (Convert.ToInt32(row["EI_CODIGO"]) == Convert.ToInt32(CODIGO))
                            {
                                pos = xpos;
                            }
                            xpos++;
                        }

                        tbImagenes.Rows[pos].Delete();
                        tbImagenes.AcceptChanges();


                        Obj.deleteImagenAntencionCliente(null, Convert.ToInt32(CODIGO));
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                Obj = null;
            }
        }

        protected void rauCargarSoporte_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {           
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbImagenes.Columns)
                cl.ReadOnly = false;
            QuejasApelacionesBL Obj = new QuejasApelacionesBL();
            DataRow row = tbImagenes.NewRow();
            try
            {                
                row["EI_CODIGO"] = 0;
                row["IP_CODIGO"] = 0;
                row["Tipo_Nombre"] = (((Button)sender).Parent.FindControl("rc_tipdoc") as RadComboBox).Text;
                row["EI_TIPO"] = (((Button)sender).Parent.FindControl("rc_tipdoc") as RadComboBox).SelectedValue;
                row["ruta"] = prArchivo;
                
                tbImagenes.Rows.Add(row);

                Obj.insertImagenAntencionCliente(null, Convert.ToInt32((rlv_atencion.EditItems[0].FindControl("txt_codigo") as RadTextBox).Text) ,
                    Convert.ToInt32((((Button)sender).Parent.FindControl("rc_tipdoc") as RadComboBox).SelectedValue),
                    prArchivo);

                if (rlv_atencion.InsertItem != null) (rlv_atencion.InsertItem.FindControl("rg_imagenes") as RadGrid).DataSource = tbImagenes;
                else (rlv_atencion.EditItems[0].FindControl("rg_imagenes") as RadGrid).DataSource = tbImagenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
                Obj = null;
            }
        }
    }
}