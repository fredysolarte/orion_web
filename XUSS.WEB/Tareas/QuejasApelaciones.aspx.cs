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
    public partial class QuejasApelaciones : System.Web.UI.Page
    {
        private DataTable tbImagenes
        {
            set { ViewState["tbImagenes"] = value; }
            get { return ViewState["tbImagenes"] as DataTable; }
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
            this.OcultarPaginador(rlv_quejas, "RadDataPager1", "BotonesBarra");
        }

        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_quejas.SelectParameters["filter"].DefaultValue = filtro;
            rlv_quejas.DataBind();
            if ((rlv_quejas.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");

                (rlv_quejas.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_quejas_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    QuejasApelacionesBL objt = new QuejasApelacionesBL();                    
                    try
                    {
                        ViewState["isClickInsert"] = true;
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
                    obj_quejas.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_quejas.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void rlv_quejas_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
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
                tbImagenes = Obj.GetImagenes(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text));
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

        protected void rlv_quejas_ItemUpdating(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {

        }

        protected void obj_quejas_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)                
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "";                                
                litTextoMensaje.Text = "Nro Radicado :" + Convert.ToString(e.ReturnValue);
                try
                {
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9020&inban=S&inParametro=inNumero&inValor=" + Convert.ToString(e.ReturnValue);
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

        protected void rlv_quejas_ItemInserted(object sender, RadListViewInsertedEventArgs e)
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
                (((RadTextBox)sender).Parent.FindControl("txt_codter") as RadTextBox).Text = "0";
                foreach (DataRow rw in Obj.GetTerceros(null, " TRCODNIT='" + (sender as RadTextBox).Text.Trim() + "'", 0, 0).Rows)
                {
                    (((RadTextBox)sender).Parent.FindControl("txt_codter") as RadTextBox).Text = Convert.ToString(rw["TRCODTER"]);
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
        public Boolean GetEstado(object a)
        {
            if (a is DBNull || a == null || Convert.ToString(a) == "N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9020&inban=S&inParametro=inConse&inValor=" + Convert.ToString((rlv_quejas.Items[0].FindControl("txt_codigo") as RadTextBox).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void obj_quejas_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "";
                litTextoMensaje.Text = "Nro Radicado :" + Convert.ToString(e.ReturnValue);
                try
                {
                    url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9020&inban=S&inParametro=inNumero&inValor=" + Convert.ToString(e.ReturnValue);
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
        protected void rg_imagenes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbImagenes;
        }
        protected void rg_imagenes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            //ArticulosBL Obj = new ArticulosBL();
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var IE_CONSECUTIVO = item.GetDataKeyValue("IE_CONSECUTIVO").ToString();
                        //Obj.DeteleImagen(null, Convert.ToInt32(IM_CONSECUTIVO));
                        //tbImagenes = Obj.GetImagenes(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text);
                        (sender as RadGrid).DataSource = tbImagenes;
                        (sender as RadGrid).DataBind();
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
                //Obj = null;
            }
        }
        protected void rg_imagenes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            QuejasApelacionesBL Obj = new QuejasApelacionesBL();
            Random random = new Random();
            string lc_c2 = ".", lc_c3 = ".";
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        {
                            
                            int random_0 = random.Next(0, 100);
                            int random_1 = random.Next(0, 100);
                            int random_2 = random.Next(0, 100);
                            int random_3 = random.Next(0, 100);
                            int random_4 = random.Next(0, 100);
                            int random_5 = random.Next(0, 100);
                            string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString((rlv_quejas.Items[0].FindControl("txt_codigo") as RadTextBox).Text);

                            string lc_ruta = MapPath("~/Uploads/" + lc_nombre);
                            EditableImage ei = (e.Item.FindControl("RadImageEditor1") as RadImageEditor).GetEditableImage();
                            ei.Image.Save(lc_ruta + "." + ei.Format, ei.RawFormat);
                            //Agregar Extencion
                            lc_ruta += "." + ei.Format;
                                                        
                            Obj.InsertImagen(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_quejas.Items[0].FindControl("txt_codigo") as RadTextBox).Text), (e.Item.FindControl("rc_tipo") as RadComboBox).SelectedValue, lc_ruta, (e.Item.FindControl("txt_observaciones") as RadTextBox).Text, Convert.ToString(Session["UserLogon"]));
                            break;
                        }
                    case "NewPhoto":
                        //myIframe.Attributes["src"] = "//" + HttpContext.Current.Request.Url.Authority + "/webcam.aspx";
                        //string script = "function f(){$find(\"" + mp_cam.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                random = null; 
            }
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
    }
}