using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Telerik.Web.UI;
using XUSS.BLL.ListaPrecios;
using XUSS.BLL.Articulos;
using System.IO;
using Telerik.Web.UI.ExportInfrastructure;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Parametros
{
    public partial class LstPrecios : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["toolbars"] = true;
        }
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel") || comando.Equals("FindDet"))
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_lstprecios, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_lstprecios_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_lstprecios.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_lstprecios.DataBind();
                    break;
                case "Edit":
                    ListaPreciosBL Obj = new ListaPreciosBL();
                    try
                    {
                        tbItems = Obj.GetListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), "-1");
                        (rlv_lstprecios.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_lstprecios.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                        (rlv_lstprecios.Items[0].FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = false;
                        (rlv_lstprecios.Items[0].FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_lstprecios_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    ListaPreciosBL Obj = new ListaPreciosBL();
                    try
                    {
                        //tbItems = Obj.GetListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), rlv_lstprecios.Items[0].GetDataKeyValue("P_CLISPRE").ToString());
                        tbItems = Obj.GetListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), "-1");
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                        (e.Item.FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = false;
                        (e.Item.FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = true;
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
            }
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codlista") as RadTextBox).Text))
                filtro += " AND P_CLISPRE = '" + (((RadButton)sender).Parent.FindControl("txt_codlista") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND P_CNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_lstprecios.SelectParameters["filter"].DefaultValue = filtro;
            rlv_lstprecios.DataBind();
            if ((rlv_lstprecios.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_lstprecios.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            if (rlv_lstprecios.InsertItem != null)
                obj_articulos.SelectParameters["inBodega"].DefaultValue = "";
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = ""; ;
            

            ((sender as ImageButton).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();
            ((sender as ImageButton).Parent.FindControl("mpArticulos") as ModalPopupExtender).Show();
        }
        protected void btn_buscar_det_OnClick(object sender, EventArgs e)
        {
            string filtro = "", lc_in = "";            
            ListaPreciosBL Obj = new ListaPreciosBL();
            try
            {
                //if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue))
                //    filtro += " AND P_RTIPPRO = '" + (((RadButton)sender).Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue + "'";
                var cll_linea = (((RadButton)sender).Parent.FindControl("rc_categoria") as RadComboBox).CheckedItems;
                if (cll_linea.Count != 0)
                {
                    lc_in = "";
                    filtro += " AND ARTIPPRO IN (";
                    foreach (var item in cll_linea)
                    {
                        lc_in += "'" + Convert.ToString(item.Value) + "',";
                    }

                    filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";
                }

                if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text))
                    filtro += " AND P_RCLAVE1 = '" + (((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text + "'";

                tbItems = Obj.GetListaPrecioDTF(null, Convert.ToString(Session["CODEMP"]), rlv_lstprecios.Items[0].GetDataKeyValue("P_CLISPRE").ToString(),filtro);
                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataBind();
                (((RadButton)sender).Parent.FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = true;
                (((RadButton)sender).Parent.FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = false;
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
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as Button).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as Button).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as Button).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as Button).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";
            if (rlv_lstprecios.InsertItem != null)
                obj_articulos.SelectParameters["inBodega"].DefaultValue = null;
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = null;


            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;

            ((sender as Button).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();
            ((sender as Button).Parent.FindControl("mpArticulos") as ModalPopupExtender).Show();
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                try
                {                    
                    ((source as RadGrid).Parent.FindControl("txt_tp") as RadTextBox).Text = Convert.ToString(item["ARTIPPRO"].Text);
                    ((source as RadGrid).Parent.FindControl("txt_referencia") as RadTextBox).Text = Convert.ToString(item["ARCLAVE1"].Text);
                    ((source as RadGrid).Parent.FindControl("txt_clave2") as RadTextBox).Text = Convert.ToString(item["ARCLAVE2"].Text);
                    ((source as RadGrid).Parent.FindControl("txt_clave3") as RadTextBox).Text = Convert.ToString(item["ARCLAVE3"].Text);
                    ((source as RadGrid).Parent.FindControl("txt_clave4") as RadTextBox).Text = Convert.ToString(item["ARCLAVE4"].Text);
                    ((source as RadGrid).Parent.FindControl("txt_descripcion") as RadTextBox).Text = Convert.ToString(item["ARNOMBRE"].Text);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                }
            }
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            ListaPreciosBL Obj = new ListaPreciosBL();
            try
            {
                if (((ImageButton)sender).CommandName == "PerformInsert")
                {
                    Obj.InsertListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), (rlv_lstprecios.EditItems[0].FindControl("txt_codlista") as RadTextBox).Text, null,
                                                ((sender as ImageButton).Parent.FindControl("txt_tp") as RadTextBox).Text, ((sender as ImageButton).Parent.FindControl("txt_referencia") as RadTextBox).Text,
                                                ((sender as ImageButton).Parent.FindControl("txt_clave2") as RadTextBox).Text, ((sender as ImageButton).Parent.FindControl("txt_clave3") as RadTextBox).Text,
                                                ((sender as ImageButton).Parent.FindControl("txt_clave4") as RadTextBox).Text, ".", ".", "UN", Convert.ToDouble(((sender as ImageButton).Parent.FindControl("txt_precio") as RadNumericTextBox).Value), 0,
                                                "AC", ".", Convert.ToString(Session["UserLogon"]));
                }
                else
                {
                    Obj.UpdateListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), (rlv_lstprecios.EditItems[0].FindControl("txt_codlista") as RadTextBox).Text, null,
                                                ((sender as ImageButton).Parent.FindControl("txt_tp") as RadTextBox).Text, ((sender as ImageButton).Parent.FindControl("txt_referencia") as RadTextBox).Text,
                                                ((sender as ImageButton).Parent.FindControl("txt_clave2") as RadTextBox).Text, ((sender as ImageButton).Parent.FindControl("txt_clave3") as RadTextBox).Text,
                                                ((sender as ImageButton).Parent.FindControl("txt_clave4") as RadTextBox).Text, ".", ".", "UN", Convert.ToDouble((((ImageButton)sender).Parent.FindControl("txt_precio") as RadNumericTextBox).Value), 0,
                                                "AC", ".", Convert.ToString(Session["UserLogon"]));                    
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
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }        
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RebindGrid":
                    (rlv_lstprecios.Items[0].FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = true;
                    (rlv_lstprecios.Items[0].FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = false;
                    e.Canceled = true;
                    break;

                case "ExportToExcel":
                    (rlv_lstprecios.Items[0].FindControl("rg_items") as RadGrid).ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
                    break;

                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
            }
        }
        protected void btn_cargar_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {            
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_procesar_OnClick(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo), Convert.ToString(Session["CODEMP"]), (rlv_lstprecios.Items[0].FindControl("txt_codlista") as RadTextBox).Text, Convert.ToString(Session["UserLogon"]));
        }
        protected void procesa_plano(Stream inStream, string inCodemp, string inLstPre, string inUsuario)
        {
            Boolean lb_indexiste = false;
            ArticulosBL Obj = new ArticulosBL();
            ListaPreciosBL ObjL = new ListaPreciosBL();
            StringBuilder lc_cadena = new StringBuilder();
            double ln_precio = 0;

            try
            {

                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            if (rbl_tiparch.SelectedIndex == 0)
                            {
                                string lc_referencia = words[0];
                                string lc_c2 = words[1];
                                string lc_c3 = words[2];
                                string lc_c4 = words[3];
                                ln_precio = Convert.ToDouble(words[4]);
                                lb_indexiste = false;
                                foreach (DataRow rx in Obj.GetArticulos(null, " AND ARCLAVE1='" + lc_referencia + "' AND ARCLAVE2 ='" + lc_c2 + "' AND ARCLAVE3='" + lc_c3 + "' AND ARCLAVE4='" + lc_c4 + "'").Rows)
                                {
                                    lb_indexiste = true;
                                    if (ObjL.ExisteListaPrecioDT(null, inCodemp, inLstPre, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"])))
                                    {
                                        ObjL.UpdateListaPrecioDT(null, inCodemp, inLstPre, null, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), ".", ".", "UN", ln_precio, 0, "AC", ".", inUsuario);
                                    }
                                    else
                                    {
                                        ObjL.InsertListaPrecioDT(null, inCodemp, inLstPre, null, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), ".", ".", "UN", ln_precio, 0, "AC", ".", inUsuario);
                                    }
                                    //ObjL.InsertListaPrecioDT(null,
                                }
                                if (!lb_indexiste)
                                {
                                    lc_cadena.AppendLine("Referencia Invalida:"+lc_referencia+" "+lc_c2+" "+lc_c3+" "+lc_c4);
                                }
                            }
                            else
                            { 
                                string lc_barras = words[0];
                                ln_precio = Convert.ToDouble(words[1]);
                                foreach (DataRow rx in Obj.GetTbBarras(null, lc_barras, "").Rows)
                                {
                                    lb_indexiste = true;
                                    if (ObjL.ExisteListaPrecioDT(null, inCodemp, inLstPre, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"])))
                                    {
                                        ObjL.UpdateListaPrecioDT(null, inCodemp, inLstPre, null, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), ".", ".", "UN", ln_precio, 0, "AC", ".", inUsuario);
                                    }
                                    else
                                    {
                                        ObjL.InsertListaPrecioDT(null, inCodemp, inLstPre, null, Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), ".", ".", "UN", ln_precio, 0, "AC", ".", inUsuario);
                                    }
                                }
                                if (!lb_indexiste)
                                {
                                    lc_cadena.AppendLine("Referencia Invalida:" + lc_barras);
                                }
                            }
                        }
                    }
                }
                lc_cadena.AppendLine("Proceso Ok!");
                litTextoMensaje.Text = lc_cadena.ToString();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjL = null;
                lc_cadena = null;
            }
        }
        protected void rg_items_InfrastructureExporting(object sender, GridInfrastructureExportingEventArgs e)
        {
            foreach (Cell cell in e.ExportStructure.Tables[0].Columns[2].Cells)
            {
                cell.Format = "@";
            }
        }
    }
}