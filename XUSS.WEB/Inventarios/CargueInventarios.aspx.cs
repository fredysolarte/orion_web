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
using XUSS.BLL.Articulos;
using XUSS.BLL.Consultas;
using XUSS.BLL.Inventarios;

namespace XUSS.WEB.Inventarios
{
    public partial class CargueInventarios : System.Web.UI.Page
    {
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private DataTable tbDetalle
        {
            set { ViewState["tbDetalle"] = value; }
            get { return ViewState["tbDetalle"] as DataTable; }
        }
        private DataTable tbInconsistencias
        {
            set { ViewState["tbInconsistencias"] = value; }
            get { return ViewState["tbInconsistencias"] as DataTable; }
        }
        private DataTable tbDiferencia
        {
            set { ViewState["tbDiferencia"] = value; }
            get { return ViewState["tbDiferencia"] as DataTable; }
        }
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel"))
                ViewState["toolbars"] = true;
            else
                ViewState["toolbars"] = false;
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rvl_toma, "RadDataPager1", "BotonesBarra");
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
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_procesar_Aceptar(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo));
        }
        private void procesa_plano(Stream inStream)
        {
            int i = 0;
            string cadena = "";
            Boolean lb_indexiste;
            //tbDetalle = new DataTable();
            tbInconsistencias = new DataTable();
            ArticulosBL Obj = new ArticulosBL();

            try
            {
                RadProgressContext progress = RadProgressContext.Current;
                //tbDetalle.Columns.Add("LINEA", typeof(string));
                //tbDetalle.Columns.Add("TP", typeof(string));
                //tbDetalle.Columns.Add("C1", typeof(string));
                //tbDetalle.Columns.Add("C1DES", typeof(string));
                //tbDetalle.Columns.Add("C2", typeof(string));
                //tbDetalle.Columns.Add("C2DES", typeof(string));
                //tbDetalle.Columns.Add("C3", typeof(string));
                //tbDetalle.Columns.Add("C3DES", typeof(string));
                //tbDetalle.Columns.Add("C4", typeof(string));
                //tbDetalle.Columns.Add("CAN", typeof(Int32));
                //tbDetalle.Columns.Add("ZONA", typeof(string));

                tbInconsistencias.Columns.Add("BARRAS", typeof(string));
                tbInconsistencias.Columns.Add("ZONA", typeof(string));

                foreach (DataColumn rc in tbDetalle.Columns)
                    rc.ReadOnly = true;

                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            string lc_barras = words[0];
                            int ln_cantidad = Convert.ToInt32(words[1]);
                            string lc_zona = words[2]; // 1 EAN13 - 2 Cualquiera
                            DataTable dt = new DataTable();
                            //if (lc_tipo == "1")
                            //    dt = ((DataTable)Obj.GetTbBarras(null, lc_barras.Substring(0,12), null));
                            //else
                            dt = ((DataTable)Obj.GetTbBarras(null, lc_barras, null));
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow rw in dt.Rows)
                                {
                                    lb_indexiste = false;
                                    //Valida que no se encuentre dentro del DataTable
                                    foreach (DataRow rx in tbDetalle.Rows)
                                    {
                                        if (Convert.ToString(rw["BTIPPRO"]) == Convert.ToString(rx["IBTIPPRO"]) && Convert.ToString(rw["BCLAVE1"]) == Convert.ToString(rx["IBCLAVE1"]) &&
                                            Convert.ToString(rw["BCLAVE2"]) == Convert.ToString(rx["IBCLAVE2"]) && Convert.ToString(rw["BCLAVE3"]) == Convert.ToString(rx["IBCLAVE3"]) &&
                                            Convert.ToString(rw["BCLAVE4"]) == Convert.ToString(rx["IBCLAVE4"]) && Convert.ToString(rx["ZONA"]) == lc_zona)
                                        {
                                            rx["IBCANTID"] = Convert.ToInt32(rx["IBCANTID"]) + ln_cantidad;
                                            lb_indexiste = true;
                                        }
                                    }
                                    if (!lb_indexiste)
                                    {
                                        DataRow row = tbDetalle.NewRow();
                                        row["TANOMBRE"] = Convert.ToString(rw["TANOMBRE"]);
                                        row["IBTIPPRO"] = Convert.ToString(rw["BTIPPRO"]);
                                        row["IBCLAVE1"] = Convert.ToString(rw["BCLAVE1"]);
                                        row["ARNOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                                        row["IBCLAVE2"] = Convert.ToString(rw["BCLAVE2"]);
                                        row["CLAVE2"] = Convert.ToString(rw["CLAVE2"]);
                                        row["IBCLAVE3"] = Convert.ToString(rw["BCLAVE3"]);
                                        row["CLAVE3"] = Convert.ToString(rw["CLAVE3"]);
                                        row["IBCLAVE4"] = Convert.ToString(rw["BCLAVE4"]);
                                        row["IBCANTID"] = ln_cantidad;
                                        row["ZONA"] = lc_zona;
                                        row["BCODIGO"] = lc_barras;
                                        row["IBBODEGA"] = ".";

                                        row["IBCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                        row["IBNROFOT"] = 0;
                                        row["IBNROCON"] = 0;
                                        row["IBIDPLAN"] = 0;
                                        row["IBIDITEM"] = 0;
                                        row["IBFECMOV"] = System.DateTime.Today;
                                        row["IBCODCAL"] = ".";
                                        row["IBESTADO"] = "AC";
                                        row["IBCAUSAE"] = ".";
                                        row["IBNMUSER"] = ".";
                                        row["IBFECMOD"] = System.DateTime.Today;
                                        row["IBFECING"] = System.DateTime.Today;

                                        tbDetalle.Rows.Add(row);
                                        row = null;
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                DataRow row = tbInconsistencias.NewRow();
                                row["BARRAS"] = Convert.ToString(lc_barras);
                                row["ZONA"] = lc_zona;
                                tbInconsistencias.Rows.Add(row);
                                row = null;
                            }
                            dt = null;                           
                            i++;
                        }//end while streamreader
                    }
                }
                (rvl_toma.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbDetalle;
                (rvl_toma.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                //rgDetalle.DataSource = tbDetalle;
                //rgDetalle.DataBind();

                //rgInconsistencias.DataSource = tbInconsistencias;
                //rgInconsistencias.DataBind();

                (rvl_toma.InsertItem.FindControl("rgInconsistencias") as RadGrid).DataSource = tbInconsistencias;
                (rvl_toma.InsertItem.FindControl("rgInconsistencias") as RadGrid).DataBind();
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
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_toma.SelectParameters["filter"].DefaultValue = filtro;
            rvl_toma.DataBind();
            if ((rvl_toma.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rvl_toma.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rvl_toma_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
        }
        protected void rvl_toma_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    CargueInventariosBL Obj = new CargueInventariosBL();
                    try
                    {
                        tbDetalle = Obj.GetTomaFisicaDT(null, " 1=0");                        
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                    }
                    ViewState["isClickInsert"] = true;
                    break;

                case "Buscar":
                    obj_toma.SelectParameters["filter"].DefaultValue = "1=0";
                    rvl_toma.DataBind();
                    break;
                case "Edit":
                    break;
                case "Delete":
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "RebindGrid")
            {
                string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            if (e.CommandName == "comparativo")
            {
                ConsultasBL Obj = new ConsultasBL();
                DataTable tb = new DataTable();

                tb.Columns.Add("TP", typeof(string));
                tb.Columns.Add("TP_NOM", typeof(string));
                tb.Columns.Add("C1", typeof(string));
                tb.Columns.Add("C1_NOM", typeof(string));
                tb.Columns.Add("C2", typeof(string));
                tb.Columns.Add("C2_NOM", typeof(string));
                tb.Columns.Add("C3", typeof(string));
                tb.Columns.Add("C3_NOM", typeof(string));
                tb.Columns.Add("C4", typeof(string));
                tb.Columns.Add("CT", typeof(Int32));
                tb.Columns.Add("IN", typeof(string));
                tbDiferencia = new DataTable();
                try
                {
                    //Unifica Valores x Referencia
                    foreach (DataRow rx in tbDetalle.Rows)
                    {
                        Boolean lb_ind = true;
                        foreach (DataRow rz in tb.Rows)
                        {
                            if (Convert.ToString(rx["IBTIPPRO"]) == Convert.ToString(rz["TP"])
                             && Convert.ToString(rx["IBCLAVE1"]) == Convert.ToString(rz["C1"])
                             && Convert.ToString(rx["IBCLAVE2"]) == Convert.ToString(rz["C2"])
                             && Convert.ToString(rx["IBCLAVE3"]) == Convert.ToString(rz["C3"])
                             && Convert.ToString(rx["IBCLAVE4"]) == Convert.ToString(rz["C4"]))
                            {
                                rz["CT"] = Convert.ToInt32(rz["CT"]) + Convert.ToInt32(rx["IBCANTID"]);
                                lb_ind = false;
                            }
                        }

                        if (lb_ind)
                        {
                            DataRow row = tb.NewRow();
                            row["TP"] = Convert.ToString(rx["IBTIPPRO"]);
                            row["TP_NOM"] = Convert.ToString(rx["TANOMBRE"]);
                            row["C1"] = Convert.ToString(rx["IBCLAVE1"]);
                            row["C1_NOM"] = Convert.ToString(rx["ARNOMBRE"]);
                            row["C2"] = Convert.ToString(rx["IBCLAVE2"]);
                            row["C2_NOM"] = Convert.ToString(rx["CLAVE2"]);
                            row["C3"] = Convert.ToString(rx["IBCLAVE3"]);
                            row["C3_NOM"] = Convert.ToString(rx["CLAVE3"]);
                            row["C4"] = Convert.ToString(rx["IBCLAVE4"]);
                            row["CT"] = Convert.ToString(rx["IBCANTID"]);
                            row["IN"] = "X";

                            tb.Rows.Add(row);
                            row = null;
                        }
                    }
                    //Fin Unificacion


                    //Comparativo
                    tbDiferencia.Columns.Add("TP", typeof(string));
                    tbDiferencia.Columns.Add("TP_NOM", typeof(string));
                    tbDiferencia.Columns.Add("C1", typeof(string));
                    tbDiferencia.Columns.Add("C1_NOM", typeof(string));
                    tbDiferencia.Columns.Add("C2", typeof(string));
                    tbDiferencia.Columns.Add("C2_NOM", typeof(string));
                    tbDiferencia.Columns.Add("C3", typeof(string));
                    tbDiferencia.Columns.Add("C3_NOM", typeof(string));
                    tbDiferencia.Columns.Add("C4", typeof(string));
                    tbDiferencia.Columns.Add("CTI", typeof(Int32));
                    tbDiferencia.Columns.Add("CTO", typeof(Int32));

                    foreach (DataRow rw in Obj.GetConsulatInventario(null, "AND BBBODEGA IN ('" + (rvl_toma.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue + "')  AND BBCANTID > 0").Rows)
                    {
                        Boolean lb_ind = true;
                        foreach (DataRow rx in tb.Rows)
                        {
                            if (Convert.ToString(rw["ARTIPPRO"]) == Convert.ToString(rx["TP"]) 
                                && Convert.ToString(rw["ARCLAVE1"]) == Convert.ToString(rx["C1"]) 
                                && Convert.ToString(rw["ARCLAVE2"]) == Convert.ToString(rx["C2"])
                                && Convert.ToString(rw["ARCLAVE3"]) == Convert.ToString(rx["C3"])
                                && Convert.ToString(rw["ARCLAVE4"]) == Convert.ToString(rx["C4"]))
                            {
                                rx["IN"] = "Y";
                                if (Convert.ToInt32(rw["BBCANTID"]) < Convert.ToInt32(rx["CT"]))
                                {
                                    DataRow row = tbDiferencia.NewRow();
                                    row["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                                    row["TP_NOM"] = Convert.ToString(rw["TANOMBRE"]);
                                    row["C1"] = Convert.ToString(rw["ARCLAVE1"]);
                                    row["C1_NOM"] = Convert.ToString(rw["ARNOMBRE"]);
                                    row["C2"] = Convert.ToString(rw["ARCLAVE2"]);
                                    row["C2_NOM"] = Convert.ToString(rw["CLAVE2"]);
                                    row["C3"] = Convert.ToString(rw["ARCLAVE3"]);
                                    row["C3_NOM"] = Convert.ToString(rw["CLAVE3"]);
                                    row["C4"] = Convert.ToString(rw["ARCLAVE4"]);
                                    row["CTI"] = Convert.ToInt32(rx["CT"]) - Convert.ToInt32(rw["BBCANTID"]);
                                    row["CTO"] = 0;
                                    tbDiferencia.Rows.Add(row);
                                    row = null;
                                    lb_ind = false;
                                }
                                else
                                {
                                    if (Convert.ToInt32(rw["BBCANTID"]) > Convert.ToInt32(rx["CT"]))
                                    {
                                        DataRow row = tbDiferencia.NewRow();
                                        row["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                                        row["TP_NOM"] = Convert.ToString(rw["TANOMBRE"]);
                                        row["C1"] = Convert.ToString(rw["ARCLAVE1"]);
                                        row["C1_NOM"] = Convert.ToString(rw["ARNOMBRE"]);
                                        row["C2"] = Convert.ToString(rw["ARCLAVE2"]);
                                        row["C2_NOM"] = Convert.ToString(rw["CLAVE2"]);
                                        row["C3"] = Convert.ToString(rw["ARCLAVE3"]);
                                        row["C3_NOM"] = Convert.ToString(rw["CLAVE3"]);
                                        row["C4"] = Convert.ToString(rw["ARCLAVE4"]);
                                        row["CTO"] = Convert.ToInt32(rw["BBCANTID"]) - Convert.ToInt32(rx["CT"]);
                                        row["CTI"] = 0;
                                        tbDiferencia.Rows.Add(row);
                                        row = null;
                                        lb_ind = false;
                                    }
                                }
                            }
                        }
                        if (lb_ind)
                        {
                            DataRow row = tbDiferencia.NewRow();
                            row["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                            row["TP_NOM"] = Convert.ToString(rw["TANOMBRE"]);
                            row["C1"] = Convert.ToString(rw["ARCLAVE1"]);
                            row["C1_NOM"] = Convert.ToString(rw["ARNOMBRE"]);
                            row["C2"] = Convert.ToString(rw["ARCLAVE2"]);
                            row["C2_NOM"] = Convert.ToString(rw["CLAVE2"]);
                            row["C3"] = Convert.ToString(rw["ARCLAVE3"]);
                            row["C3_NOM"] = Convert.ToString(rw["CLAVE3"]);
                            row["C4"] = Convert.ToString(rw["ARCLAVE4"]);
                            row["CTO"] = Convert.ToInt32(rw["BBCANTID"]);
                            row["CTI"] = 0;
                            tbDiferencia.Rows.Add(row);
                            row = null;
                        }
                    }

                    foreach (DataRow rw in tb.Rows)
                    {
                        if (Convert.ToString(rw["IN"]) == "X")
                        {
                            DataRow row = tbDiferencia.NewRow();
                            row["TP"] = Convert.ToString(rw["TP"]);
                            row["TP_NOM"] = Convert.ToString(rw["TP_NOM"]);
                            row["C1"] = Convert.ToString(rw["C1"]);
                            row["C1_NOM"] = Convert.ToString(rw["C1_NOM"]);
                            row["C2"] = Convert.ToString(rw["C2"]);
                            row["C2_NOM"] = Convert.ToString(rw["C2_NOM"]);
                            row["C3"] = Convert.ToString(rw["C3"]);
                            row["C3_NOM"] = Convert.ToString(rw["C3_NOM"]);
                            row["C4"] = Convert.ToString(rw["C4"]);
                            row["CTO"] = 0;
                            row["CTI"] = Convert.ToInt32(rw["CT"]);

                            tbDiferencia.Rows.Add(row);
                            row = null;
                        }
                    }

                    (rvl_toma.InsertItem.FindControl("rg_diferencias") as RadGrid).DataSource = tbDiferencia;
                    (rvl_toma.InsertItem.FindControl("rg_diferencias") as RadGrid).DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally {
                    Obj = null;
                    tb = null;
                }
            }
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbDetalle;
        }
        protected void obj_toma_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["DetPedidos"] = tbDetalle;
        }
        protected void obj_toma_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            else
            {
                litTextoMensaje.Text = "Nro Planilla :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=6003&inban=S&inParametro=inConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rvl_toma_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
    }
}