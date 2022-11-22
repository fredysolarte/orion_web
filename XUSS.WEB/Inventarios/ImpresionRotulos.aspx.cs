using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Articulos;
using XUSS.BLL.Comun;
using XUSS.BLL.Inventarios;

namespace XUSS.WEB.Inventarios
{
    public partial class ImpresionRotulos : System.Web.UI.Page
    {
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbItems = new DataTable();

                tbItems.Columns.Add("ITM", typeof(string));
                tbItems.Columns.Add("TP", typeof(string));
                tbItems.Columns.Add("C1", typeof(string));
                tbItems.Columns.Add("C2", typeof(string));
                tbItems.Columns.Add("C3", typeof(string));
                tbItems.Columns.Add("C4", typeof(string));
                tbItems.Columns.Add("NOMBRE", typeof(string));
                tbItems.Columns.Add("NC2", typeof(string));
                tbItems.Columns.Add("NC3", typeof(string));
                tbItems.Columns.Add("CAN", typeof(Int32));                
            }
        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            //obj_articulos.SelectParameters["inBodega"].DefaultValue = null;            
            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            string script = "function f(){$find(\"" + mpArticulos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            //obj_articulos.SelectParameters["inBodega"].DefaultValue = null; 


            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;
            foreach (GridColumn column in rgConsultaArticulos.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                column.CurrentFilterValue = string.Empty;
            }
            rgConsultaArticulos.MasterTableView.FilterExpression = string.Empty;
            rgConsultaArticulos.MasterTableView.Rebind();

            rgConsultaArticulos.DataBind();
            //mpArticulos.Show();
            string script = "function f(){$find(\"" + mpArticulos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            { 
                case  "Select":            
                GridDataItem item = (GridDataItem)e.Item;
                try
                {                                    
                    txt_tp.Text = Convert.ToString(item["ARTIPPRO"].Text);
                    txt_referencia.Text = Convert.ToString(item["ARCLAVE1"].Text);
                    txt_clave2.Text = Convert.ToString((item.FindControl("lbl_clave2") as Label).Text);
                    txt_clave3.Text = Convert.ToString((item.FindControl("lbl_clave3") as Label).Text);
                    txt_clave4.Text = Convert.ToString(item["ARCLAVE4"].Text);
                    txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                    txt_nomc2.Text = Convert.ToString(item["CLAVE2"].Text);
                    txt_nomc3.Text = Convert.ToString(item["CLAVE3"].Text);
                    txt_cantidad.Focus();                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                }
                break;
                
                default:
                    string script = "function f(){$find(\"" + mpArticulos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;            
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            ImpresionRotulosBL Obj = new ImpresionRotulosBL();
            try
            {
                if (tbItems.Rows.Count > 0)
                {
                    //Obj.GenerarCodigoBarras(null, Convert.ToString(Session["CODEMP"]), txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, Convert.ToString(Session["UserLogon"]));
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        Obj.GenerarCodigoBarras(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["TP"]), Convert.ToString(rw["C1"]), Convert.ToString(rw["C2"]), Convert.ToString(rw["C3"]), ".", Convert.ToString(Session["UserLogon"]));
                    }
                    Obj.InsertCodigoTMP(null, tbItems, Convert.ToString(Session["UserLogon"]));
                    //url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7010&inban=S&inParametro=tp&inValor=" + txt_tp.Text + "&inParametro=c1&inValor=" + txt_referencia.Text + "&inParametro=c2&inValor=" + txt_clave2.Text + "&inParametro=c3&inValor=" + txt_clave3.Text + "&inParametro=c4&inValor=" + txt_clave4.Text + "&inParametro=pg&inValor=" + txt_cantidad.Value;
                    url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=7019&inban=S&inParametro=inUsuario&inValor=" + Convert.ToString(Session["UserLogon"]);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                }
                else
                {
                    litTextoMensaje.Text = "Agregar Items a la Impresion!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            finally
            {
                Obj = null;
            }
        }
        protected void btn_agregar_OnClick(object sender, EventArgs e)
        {
            DataRow rw = tbItems.NewRow();
            try {
                rw["TP"] = txt_tp.Text;
                rw["C1"] = txt_referencia.Text;
                rw["C2"] = txt_clave2.Text;
                rw["C3"] = txt_clave3.Text;
                rw["CAN"] = Convert.ToInt32(txt_cantidad.Value);
                rw["NOMBRE"] = txt_descripcion.Text;
                rw["NC2"] = txt_nomc2.Text;
                rw["NC3"] = txt_nomc3.Text;
                rw["ITM"] = txt_tp.Text + txt_referencia.Text + txt_clave2.Text + txt_clave3.Text + txt_clave4.Text;

                tbItems.Rows.Add(rw);
                rg_items.DataSource = tbItems;
                rg_items.DataBind();

                txt_tp.Text = "";
                txt_referencia.Text = "";
                txt_clave2.Text = "";
                txt_clave3.Text = "";
                txt_nomc2.Text = "";
                txt_nomc3.Text = "";
                txt_cantidad.Value = 0;
                txt_descripcion.Text = "";
                txt_codbarras.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rw = null;
            }
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;            
            switch (e.CommandName)
            {
                case "Delete":
                    var DTNROITM = item.GetDataKeyValue("ITM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToString(row["ITM"]) == Convert.ToString(DTNROITM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    break;
            }

            rg_items.DataSource = tbItems;
            rg_items.DataBind();
        }
        protected void txt_codbarras_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text, null, null);
                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);                            
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_nomc2.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nomc3.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_cantidad.Focus();
                        }


                        txt_codbarras.Text = "";                        
                    }
                    else
                    {
                        litTextoMensaje.Text = "Codigo Barras Invalido!";
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    obj_articulos = null;
                    tbBarras = null;
                }
            }
        }

        protected void btn_masivo_Click(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            //prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_procesar_Aceptar(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo));
        }
        protected void procesa_plano(Stream inStream)
        {
            ArticulosBL Obj = new ArticulosBL();
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
                            
                            string lc_c1 = words[0];
                            string lc_c2 = words[1];
                            string lc_c3 = words[2];
                            string lc_c4 = words[3];
                            int ln_cantidad = Convert.ToInt32(words[4]);
                            foreach (DataRow rx in Obj.GetArticuloInv(null, lc_c1, lc_c2, lc_c3, lc_c4, ".", ".").Rows)
                            {

                                DataRow rw = tbItems.NewRow();

                                rw["TP"] = rx["ARTIPPRO"];
                                rw["C1"] = rx["ARCLAVE1"];
                                rw["C2"] = rx["ARCLAVE2"];
                                rw["C3"] = rx["ARCLAVE3"];
                                rw["CAN"] = Convert.ToInt32(ln_cantidad);
                                rw["NOMBRE"] = rx["ARNOMBRE"];
                                rw["NC2"] = "";
                                rw["NC3"] = "";
                                rw["ITM"] = Convert.ToString(rx["ARTIPPRO"]) + Convert.ToString(rx["ARCLAVE1"]) + Convert.ToString(rx["ARCLAVE2"]) + Convert.ToString(rx["ARCLAVE3"]) + Convert.ToString(rx["ARCLAVE4"]);

                                tbItems.Rows.Add(rw);
                            }

                            rg_items.DataSource = tbItems;
                            rg_items.DataBind();

                        }
                    }
                }
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