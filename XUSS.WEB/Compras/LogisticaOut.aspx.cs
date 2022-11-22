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
using XUSS.BLL.Compras;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Compras
{
    public partial class LogisticaOut : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbFacturas
        {
            set { ViewState["tbFacturas"] = value; }
            get { return ViewState["tbFacturas"] as DataTable; }
        }
        private DataTable tbBillxWR
        {
            set { ViewState["tbBillxWR"] = value; }
            get { return ViewState["tbBillxWR"] as DataTable; }
        }
        private DataTable tbContainers
        {
            set { ViewState["tbContainers"] = value; }
            get { return ViewState["tbContainers"] as DataTable; }
        }
        private DataTable tbSoportes
        {
            set { ViewState["tbSoportes"] = value; }
            get { return ViewState["tbSoportes"] as DataTable; }
        }
        private DataTable tbSegregacion
        {
            set { ViewState["tbSegregacion"] = value; }
            get { return ViewState["tbSegregacion"] as DataTable; }
        }
        private DataTable tbTraslados
        {
            set { ViewState["tbTraslados"] = value; }
            get { return ViewState["tbTraslados"] as DataTable; }
        }        
        private string gc_indtercero
        {
            set { ViewState["gc_indtercero"] = value; }
            get { return Convert.ToString(ViewState["gc_indtercero"]); }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_logistica.SelectParameters["filter"].DefaultValue = "WOH_CONSECUTIVO =" + Convert.ToString(Request.QueryString["Documento"]);
                    rlv_logistica.DataBind();
                }
            }
        }        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_logistica, "RadDataPager1", "BotonesBarra");
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
        protected void rlv_logistica_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            BillofLadingBL ObjBL = new BillofLadingBL();
            SoportesBL ObjS = new SoportesBL();
            SegregacionBL Objg = new SegregacionBL();
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        ViewState["isClickInsert"] = true;                        
                        tbItems = Obj.GetWROUTDT(null, 0);
                        tbBillxWR = ObjBL.GetBLWROUT(null, 0);
                        tbSoportes = ObjS.GetSoportes(null, "4", 0);
                        tbSegregacion = Objg.GetSegregacionxTraslado(null,0,Convert.ToString(Session["CODEMP"]));
                        tbTraslados = Obj.GetTrasladosWROUT(null, 0);
                        break;                    
                    case "Buscar":
                        obj_logistica.SelectParameters["filter"].DefaultValue = "1=0";
                        rlv_logistica.DataBind();
                        break;
                    case "Edit":
                        break;
                    case "Delete":                        
                        Obj.AnularWRIN(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_logistica.Items[0].FindControl("txt_nrowr") as RadTextBox).Text), Convert.ToString(Session["UserLogon"]),tbItems);
                        litTextoMensaje.Text = "Embarque de Entrada " + (rlv_logistica.Items[0].FindControl("txt_nrowr") as RadTextBox).Text + " Anulado!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                        
                        break;
                }
                this.AnalizarCommand(e.CommandName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjBL = null;
                ObjS = null;
                Objg = null;
            }            
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";
            
            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_embarque") as RadTextBox).Text))
                filtro += " AND WOH_CONSECUTIVO = " + (((RadButton)sender).Parent.FindControl("txt_embarque") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_embarqueIn") as RadTextBox).Text))
                filtro += " AND WOH_CONSECUTIVO IN (SELECT DISTINCT WOH_CONSECUTIVO FROM TB_WROUTDT WITH(NOLOCK) WHERE WIH_CONSECUTIVO =" + (((RadButton)sender).Parent.FindControl("txt_embarqueIn") as RadTextBox).Text + ")";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_container") as RadTextBox).Text))
                filtro += " AND WOH_CONSECUTIVO IN (SELECT WOH_CONSECUTIVO FROM TB_BLH_WROUT WHERE BLH_CODIGO IN (SELECT BLH_CODIGO FROM TB_BLDT WHERE BLD_NROCONTAINER = '" + (((RadButton)sender).Parent.FindControl("txt_container") as RadTextBox).Text + "'))";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_bl") as RadTextBox).Text))
                filtro += " AND WOH_CONSECUTIVO IN (SELECT WOH_CONSECUTIVO FROM TB_BLH_WROUT WHERE BLH_CODIGO IN (SELECT BLH_CODIGO FROM TB_BLHD WHERE BLH_NROBILLOFL='" + (((RadButton)sender).Parent.FindControl("txt_bl") as RadTextBox).Text + "'))";



            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_logistica.SelectParameters["filter"].DefaultValue = filtro;
            rlv_logistica.DataBind();
            if ((rlv_logistica.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_logistica.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_logistica_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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

            OrdenesComprasBL Obj = new OrdenesComprasBL();
            BillofLadingBL ObjBL = new BillofLadingBL();
            SoportesBL ObjS = new SoportesBL();
            SegregacionBL Objg = new SegregacionBL();
            try
            {
                tbItems = Obj.GetWROUTDT(null, Convert.ToInt32((e.Item.FindControl("txt_nrowr") as RadTextBox).Text));
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                tbBillxWR = ObjBL.GetBLWROUT(null, Convert.ToInt32((e.Item.FindControl("txt_nrowr") as RadTextBox).Text));
                (e.Item.FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                (e.Item.FindControl("rg_bl") as RadGrid).DataBind();

                tbSegregacion = Objg.GetSegregacionxTraslado(null, Convert.ToInt32((e.Item.FindControl("txt_nrowr") as RadTextBox).Text), Convert.ToString(Session["CODEMP"]));
                (e.Item.FindControl("rg_segregacion") as RadGrid).DataSource = tbSegregacion;
                (e.Item.FindControl("rg_segregacion") as RadGrid).DataBind();

                tbTraslados = Obj.GetTrasladosWROUT(null, Convert.ToInt32((e.Item.FindControl("txt_nrowr") as RadTextBox).Text));
                (e.Item.FindControl("rg_traslados") as RadGrid).DataSource = tbTraslados;
                (e.Item.FindControl("rg_traslados") as RadGrid).DataBind();

                int ln_codigo = 0;
                foreach (DataRow rw in tbBillxWR.Rows)
                    ln_codigo = Convert.ToInt32(rw["BLH_CODIGO"]);

                tbContainers = ObjBL.GetBLDT(null, ln_codigo);

                tbSoportes = ObjS.GetSoportes(null, "4", Convert.ToInt32((rlv_logistica.Items[0].FindControl("txt_nrowr") as RadTextBox).Text));
                (e.Item.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                (e.Item.FindControl("rgSoportes") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjBL = null;
                ObjS = null;
                Objg = null;
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
                case "InitInsert":
                    OrdenesComprasBL obj = new OrdenesComprasBL();
                    try
                    {
                        tbFacturas = obj.GetCompraDTWROUT(null, Convert.ToString(Session["CODEMP"]), " AND WIH_CONSECUTIVO=0");
                        rg_comprasdt.DataSource = tbFacturas;
                        rg_comprasdt.DataBind();
                        txt_orden.Text = "";
                        string script = "function f(){$find(\"" + mpCompras.ClientID + "\").show(); $find(\"" + mpCompras.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                    }
                    break;
                case "link_wrin":
                    GridDataItem item = (GridDataItem)e.Item;
                    string url_ = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/LogisticaIn.aspx?Documento=" + (item.FindControl("lbl_wrin") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url_ + "');", true);
                    item = null;
                    break;
                case "link_invoice":
                    GridDataItem item_f = (GridDataItem)e.Item;                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/OrdenesCompras.aspx?Invoice=" + (item_f.FindControl("lbl_nrofactura") as Label).Text + "');", true);
                    item_f = null;
                    break;
                case "link":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                    item_ = null;
                    break;
                case "Load":                    
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalCargue.ClientID + "\").show(); $find(\"" + mpCompras.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    e.Canceled = true;
                    break;
                case "LoadS":
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpSegregaciones.ClientID + "\").show(); $find(\"" + mpCompras.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    e.Canceled = true;
                    break;
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
        protected void procesa_plano(Stream inStream)
        {
            ArticulosBL Obj = new ArticulosBL();
            //string lc_tippro = "", lc_clave1 = "", lc_clave2 = "", lc_clave3 = "", lc_clave4 = "", lc_nrowh = "";            
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
                            string lc_nrowh = words[0];
                            string lc_wid = words[1];                            
                            string lc_clave1 = words[2];
                            string lc_clave2 = words[3];
                            string lc_clave3 = words[4];
                            string lc_clave4 = words[5];
                            int ln_cantidad = Convert.ToInt32(words[6]);
                            double ln_precio = Convert.ToInt32(words[7]);

                            foreach (DataRow rw in Obj.GetArticulos(null, " AND ARCLAVE1='" + lc_clave1 + "' AND ARCLAVE2 ='" + lc_clave2 + "' AND ARCLAVE3='" + lc_clave3 + "' AND ARCLAVE4='" + lc_clave4 + "'").Rows)
                            {
                                DataRow rx = tbItems.NewRow();
                                rx["WOD_ID"] = tbItems.Rows.Count + 1;
                                rx["WOH_CONSECUTIVO"] = 0;
                                rx["WID_ID"] = Convert.ToInt32(lc_wid);
                                rx["WIH_CONSECUTIVO"] = Convert.ToInt32(lc_nrowh);
                                rx["ARCODEMP"] = Convert.ToString(Session["CODEMP"]);
                                rx["ARTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]); 
                                rx["ARCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                rx["ARCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                rx["ARCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                rx["ARCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                rx["WOD_CANTIDAD"] = ln_cantidad;
                                rx["WOD_USUARIO"] = ".";
                                rx["TANOMBRE"] = rw["TANOMBRE"];
                                rx["ARNOMBRE"] = rw["ARNOMBRE"];
                                rx["WOD_FECING"] = System.DateTime.Today;
                                rx["WOD_FECMOD"] = System.DateTime.Today;

                                rx["NOMTTEC1"] = rw["NOMTTEC1"];
                                rx["NOMTTEC2"] = rw["NOMTTEC2"];
                                rx["NOMTTEC3"] = rw["NOMTTEC3"];
                                rx["NOMTTEC4"] = rw["NOMTTEC4"];
                                rx["NOMTTEC5"] = rw["NOMTTEC5"];
                                rx["NOMTTEC7"] = rw["NOMTTEC7"];

                                rx["WOD_PRECIO"] = ln_precio;
                                rx["WOD_PRECIOVTA"] = ln_precio;

                                rx["TSNROTRA"] = 0;
                                rx["MBIDITEM"] = tbItems.Rows.Count + 1;
                                rx["MIIDMOVI"] = 0;
                                rx["MIOTMOVI"] = 0;
                                tbItems.Rows.Add(rx);
                            }

                        }
                    }
                }
                (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
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
        protected void btn_filtrocmp_Click(object sender, EventArgs e)
        {
            //obj_compras.SelectParameters["FD_NROCMP"].DefaultValue = txt_orden.Text;            
            OrdenesComprasBL obj = new OrdenesComprasBL();
            try
            {
                //tbFacturas = obj.GetCompraDTWROUT(null, Convert.ToString(Session["CODEMP"]), " AND WIH_CONSECUTIVO=0");
                if (!string.IsNullOrEmpty(txt_orden.Text))
                    tbFacturas = obj.GetCompraDTWROUT(null, Convert.ToString(Session["CODEMP"]), " AND WIH_CONSECUTIVO=" + txt_orden.Text);

                if (!string.IsNullOrEmpty(txt_fnrocontainer.Text))
                    tbFacturas = obj.GetCompraDTWROUT(null, Convert.ToString(Session["CODEMP"]), " AND WIH_CONSECUTIVO IN (SELECT WIH_CONSECUTIVO FROM TB_BLH_WRIN WHERE BLH_CODIGO IN (SELECT BLH_CODIGO FROM TB_BLDT WITH(NOLOCK) WHERE BLD_NROCONTAINER='" + txt_fnrocontainer.Text + "'))");

                rg_comprasdt.DataSource = tbFacturas;
                rg_comprasdt.DataBind();

                string script = "function f(){$find(\"" + mpCompras.ClientID + "\").show(); $find(\"" + mpCompras.ClientID + "\").maximize(); $find(\"" + txt_orden.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj = null;
            }
        }
        protected void rg_comprasdt_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "seleccionar")
            {
                tbFacturas.Columns["CHK"].ReadOnly = false;
                tbFacturas.Columns["CAN_SOL"].ReadOnly = false;
                tbFacturas.Columns["DIF"].ReadOnly = false;

                foreach (DataRow rw in tbFacturas.Rows)
                {
                    rw["CHK"] = "S";
                    rw["CAN_SOL"] = rw["CAN_RESTANTE"];
                    rw["DIF"] = 0;
                }
            }
            if (e.CommandName == "anular")
            {
                tbFacturas.Columns["CHK"].ReadOnly = false;
                tbFacturas.Columns["CAN_SOL"].ReadOnly = false;
                tbFacturas.Columns["DIF"].ReadOnly = false;

                foreach (DataRow rw in tbFacturas.Rows)
                {
                    rw["CHK"] = "N";
                    rw["CAN_SOL"] = 0;
                    rw["DIF"] = rw["CAN_RESTANTE"];
                }
            }

            rg_comprasdt.DataSource = tbFacturas;
            rg_comprasdt.DataBind();
            string script = "function f(){$find(\"" + mpCompras.ClientID + "\").show(); $find(\"" + mpCompras.ClientID + "\").maximize(); $find(\"" + txt_orden.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

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
        protected void btn_agregarct_Click(object sender, EventArgs e)
        {
            foreach (DataColumn cl in tbFacturas.Columns)
                cl.ReadOnly = false;

            foreach (GridDataItem item in rg_comprasdt.Items)
            {
                if ((item.FindControl("chk_estado") as CheckBox).Checked)
                {
                    int ln_item = Convert.ToInt32((item.FindControl("txt_item") as RadTextBox).Text);                    
                    foreach (DataRow rw in tbFacturas.Rows)
                    {
                        if (Convert.ToString(rw["WID_ID"]) == Convert.ToString(ln_item))
                        {
                            //rw["CHK"] = "S";
                            //if ((item.FindControl("txt_cancom") as RadNumericTextBox).Value == 0)
                            //    rw["CAN_SOL"] = rw["CAN_RESTANTE"];
                            //else
                            //    rw["CAN_SOL"] = (item.FindControl("txt_cancom") as RadNumericTextBox).Value;
                            if ((item.FindControl("chk_estado") as CheckBox).Checked)
                                rw["CHK"] = "S";

                            if ((item.FindControl("txt_cancom") as RadNumericTextBox).Value > 0)
                            {
                                rw["CHK"] = "S";
                                rw["CAN_SOL"] = (item.FindControl("txt_cancom") as RadNumericTextBox).Value;
                            }
                        }
                    }
                }

            }
            foreach (DataRow rw in tbFacturas.Rows)
            {
                if (Convert.ToString(rw["CHK"]) == "S")
                {
                    DataRow rx = tbItems.NewRow();
                    try
                    {
                        rx["WOD_ID"] = tbItems.Rows.Count + 1;
                        rx["WOH_CONSECUTIVO"] = 0;
                        rx["WID_ID"] = rw["WID_ID"];
                        rx["WIH_CONSECUTIVO"] = rw["WIH_CONSECUTIVO"];                        
                        rx["ARCODEMP"] = Convert.ToString(Session["CODEMP"]);                                                
                        rx["ARTIPPRO"] = rw["ARTIPPRO"];
                        rx["ARCLAVE1"] = rw["ARCLAVE1"];
                        rx["ARCLAVE2"] = rw["ARCLAVE2"];
                        rx["ARCLAVE3"] = rw["ARCLAVE3"];
                        rx["ARCLAVE4"] = rw["ARCLAVE4"];
                        rx["WOD_CANTIDAD"] = rw["CAN_SOL"];
                        rx["WOD_USUARIO"] = ".";                        
                        rx["TANOMBRE"] = rw["TANOMBRE"];
                        rx["ARNOMBRE"] = rw["ARNOMBRE"];
                        rx["WOD_FECING"] = System.DateTime.Today;
                        rx["WOD_FECMOD"] = System.DateTime.Today;

                        rx["NOMTTEC1"] = rw["NOMTTEC1"];
                        rx["NOMTTEC2"] = rw["NOMTTEC2"];
                        rx["NOMTTEC3"] = rw["NOMTTEC3"];
                        rx["NOMTTEC4"] = rw["NOMTTEC4"];
                        rx["NOMTTEC5"] = rw["NOMTTEC5"];
                        rx["NOMTTEC7"] = rw["NOMTTEC7"];

                        rx["WOD_PRECIO"] = rw["WID_PRECIOVTA"];
                        rx["WOD_PRECIOVTA"] = rw["WID_PRECIOVTA"];

                        rx["TSNROTRA"] = 0;
                        rx["MBIDITEM"] = tbItems.Rows.Count + 1;
                        rx["MIIDMOVI"] = 0;
                        rx["MIOTMOVI"] = 0;


                        tbItems.Rows.Add(rx);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        rx = null;
                    }
                }
            }

            (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
            (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
        }
        protected void obj_logistica_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbItems"] = tbItems;
            e.InputParameters["tbBL"] = tbBillxWR;
            e.InputParameters["tbBLDT"] = tbContainers;
            e.InputParameters["tbSoportes"] = tbSoportes;
            e.InputParameters["tbSegregacion"] = tbSegregacion;
        }
        protected void rlv_logistica_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_logistica_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rg_items_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var WID_ID = item.GetDataKeyValue("WID_ID").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["WID_ID"]) == Convert.ToInt32(WID_ID))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    foreach (DataRow row in tbItems.Rows)
                    {
                        row["WID_ID"] = i;
                        i++;
                    }
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    break;
            }
        }
        protected void rg_container_ItemCommand(object sender, GridCommandEventArgs e)
        {
                switch (e.CommandName)
                {
                    case "InitInsert":
                        string script = "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        break;
                    case "PerformInsert":
                        DataRow itm = tbContainers.NewRow();
                        try
                        {
                            itm["BLD_CODIGO"] = 0;
                            itm["BLH_CODIGO"] = -1;
                            itm["BLD_NROCONTAINER"] = (e.Item.FindControl("txt_nrocontainer") as RadTextBox).Text;
                            itm["BLD_NROPACK"] = (e.Item.FindControl("txt_nropack") as RadTextBox).Text;
                            itm["BLD_DESCRIPTION"] = (e.Item.FindControl("txt_obscontainer") as RadTextBox).Text;
                            itm["BLD_GROSSWEIGHT"] = (e.Item.FindControl("txt_gross") as RadNumericTextBox).Text;
                            itm["BLD_GROSSUN"] = (e.Item.FindControl("rc_unidadgross") as RadComboBox).SelectedValue;
                            itm["BLD_DIMESION"] = (e.Item.FindControl("txt_measurement") as RadNumericTextBox).Text;
                            itm["BLD_DIMESIONUN"] = (e.Item.FindControl("rc_unidadmeasurement") as RadComboBox).SelectedValue;
                        tbContainers.Rows.Add(itm);
                            rg_container.DataSource = tbContainers;
                            rg_container.DataBind();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            itm = null;
                        }
                        break;
                }            
        }
        protected void rg_bl_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbBillxWR;
        }
        protected void rg_bl_ItemCommand(object sender, GridCommandEventArgs e)
        {
            BillofLadingBL ObjBL = new BillofLadingBL();
            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert":                                        
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        tbContainers = ObjBL.GetBLDT(null, 0);
                        rg_container.DataSource = tbContainers;
                        rg_container.DataBind();
                        e.Canceled = true;
                        break;                   
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjBL = null;
            }
        }
        protected void btn_agregarctn_Click(object sender, EventArgs e)
        {
            DataRow itm = tbBillxWR.NewRow();
            try
            {
                itm["BLH_CODIGO"] = -1;
                itm["BLH_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                itm["BLH_CODEXPORTER"] = txt_exporter.Text;
                itm["BLH_CODRECEPTOR"] = txt_consignatario.Text;
                itm["BLH_CODNOTIFY"] = txt_notify.Text;
                itm["BLH_MODTRANS"] = rc_minitialcarriage.SelectedValue;
                itm["BLH_CIUREC"] = txt_lugarrecibe.Text;
                itm["BLH_NROVIAJE"] = txt_nroviaje.Text;
                itm["BLH_PURORIGEN"] = txt_ptocarga.Text;
                itm["BLH_PURDESTINO"] = txt_ptodescarga.Text;
                itm["BLH_CIUDESTI"] = txt_destino.Text;
                itm["BLH_BOOKINGNO"] = txt_nrobooking.Text;
                itm["BLH_NROBILLOFL"] = txt_nrobl.Text;
                itm["BLH_EXPORTREF"] = txt_exportref.Text;
                itm["BLH_PTOPAISORI"] = ".";
                itm["BLH_TIPOENVIO"] = rc_tipomov.SelectedValue;
                itm["BLH_FECHA"] = txt_fechaBL.SelectedDate;
                itm["BLH_USUARIO"] = "..";
                itm["BLH_FECING"] = System.DateTime.Now;

                tbBillxWR.Rows.Add(itm);

                if (rlv_logistica.InsertItem != null)
                {
                    (rlv_logistica.InsertItem.FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                    (rlv_logistica.InsertItem.FindControl("rg_bl") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_logistica.Items[0].FindControl("rg_bl") as RadGrid).DataSource = tbBillxWR;
                    (rlv_logistica.Items[0].FindControl("rg_bl") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                itm = null;
            }
        }
        protected void rg_container_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbContainers;
        }
        protected void btn_aceptar_sop_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbSoportes.NewRow();
            //byte[] result;
            try
            {
                row["SP_CONSECUTIVO"] = tbSoportes.Rows.Count + 5;
                row["SP_REFERENCIA"] = 0;
                row["SP_DESCRIPCION"] = (((RadButton)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text;
                row["SP_EXTENCION"] = Path.GetExtension(prArchivo.Substring(0, prArchivo.Length - 4));
                row["SP_TIPO"] = "4";
                row["SP_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["RUTA"] = prArchivo;
                row["SP_FECING"] = System.DateTime.Now;

                tbSoportes.Rows.Add(row);
                if (rlv_logistica.InsertItem != null) (rlv_logistica.InsertItem.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                else (rlv_logistica.EditItems[0].FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
            }
        }
        protected void rgSoportes_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbSoportes;
        }
        protected void rgSoportes_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string lc_extencion = "";
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["SP_CONSECUTIVO"].Text);

                SoportesBL Obj = new SoportesBL();
                foreach (DataRow rw in (Obj.GetSoporte(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["SP_IMAGEN"]);
                    lc_extencion = Convert.ToString(rw["SP_EXTENCION"]);
                }

                ditem = null;
                Random random = new Random();
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + "" + lc_extencion;
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
        }
        protected void rauCargarSoporte_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            //pArchivo = e.File.InputStream;
            //prArchivo = e.File.FileName;
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_shipper_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "A";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_consignatario_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "B";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_notify_Click(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "C";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroTer_OnClick(object sender, EventArgs e)
        {
            string filter = "1=1 ";
            if (!string.IsNullOrWhiteSpace(edt_codter.Text))
                filter += "AND TRCODTER=" + edt_codter.Text;
            if (!string.IsNullOrWhiteSpace(edt_nomtercero.Text))
                filter += "AND UPPER(TRNOMBRE+' '+ISNULL(TRNOMBR2,'')+ ' ' +ISNULL(TRAPELLI,'')) LIKE '%" + edt_nomtercero.Text.ToUpper() + "%'";
            if (!string.IsNullOrWhiteSpace(edt_identificacion.Text))
                filter += "AND UPPER(TRCODNIT) LIKE '%" + edt_identificacion.Text.ToUpper() + "%'";

            obj_terceros.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaTerceros.DataBind();

            edt_nomtercero.Focus();
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); $find(\"" + edt_nomtercero.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaTerceros_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TercerosBL obj = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                Boolean lb_bandera = false;
                try
                {
                    if ((item.FindControl("lbl_Estadi") as Label).Text == "AC")
                        lb_bandera = true;
                    if (lb_bandera)
                    {
                        if (gc_indtercero == "B")
                        {
                            txt_consignatario.Text = Convert.ToString(item["TRCODTER"].Text); 
                            txt_datconsignatario.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                        if (gc_indtercero == "A")
                        {
                            txt_exporter.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datexport.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }
                        if (gc_indtercero == "C")
                        {
                            txt_notify.Text = Convert.ToString(item["TRCODTER"].Text);
                            txt_datnotify.Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpBillOfLading.ClientID + "\").show(); $find(\"" + mpBillOfLading.ClientID + "\").maximize(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                        }

                        if (gc_indtercero == "D")
                        {
                            (rlv_logistica.InsertItem.FindControl("txt_codter") as RadTextBox).Text =  Convert.ToString(item["TRCODTER"].Text);
                            (rlv_logistica.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text;                            
                        }

                    }
                    else
                    {
                        litTextoMensaje.Text = "Tercero se Encuentra Inactivo!";
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
                    item_ = null;
                    item = null;
                    obj = null;
                }
            }
            else
            {
                if (e.CommandName == "Page")
                {
                    //mpTerceros.Show();
                    string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
        }
        protected void rg_bl_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    e.DetailTableView.DataSource = tbContainers;
                    //e.DetailTableView.DataBind();
                    break;
            }
        }
        protected void rlv_logistica_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
            {
                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToInt32(rw["WOD_ID"]) == Convert.ToInt32(item["WOD_ID"].Text))
                    {
                        rw["WOD_PRECIOVTA"] = Convert.ToDouble((item.FindControl("txt_precio_vta") as RadNumericTextBox).DbValue);
                    }
                }
            }
        }
        protected void btn_genfac_Click(object sender, EventArgs e)
        {
            string[] lc_factura;
            OrdenesComprasBL Obj = new OrdenesComprasBL();   
            try {
                lc_factura = Obj.InsertFactura(null, Convert.ToString(Session["CODEMP"]), rc_tipfac.SelectedValue, Convert.ToInt32((rlv_logistica.Items[0].FindControl("txt_codter") as RadTextBox).Text), Convert.ToString(Session["UserLogon"]), Convert.ToInt32((rlv_logistica.Items[0].FindControl("txt_nrowr") as RadTextBox).Text)).Split('-');

                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9014&inban=S&inParametro=inNumero&inValor=" + lc_factura[1] +
                          "&inParametro=inTipo&inValor=" + lc_factura[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
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
        protected void btn_factura_Click(object sender, EventArgs e)
        {
            string script = "";
            if (!string.IsNullOrEmpty((rlv_logistica.Items[0].FindControl("lkn_factura") as LinkButton).Text))
            {
                litTextoMensaje.Text = "Ya cuenta con Factura!";
                script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            else
            {
                script = "function f(){$find(\"" + mpFactura.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void lkn_factura_Click(object sender, EventArgs e)
        {
            string[] lc_factura;
            lc_factura = (sender as LinkButton).Text.Split('-');
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9014&inban=S&inParametro=inNumero&inValor=" + lc_factura[1] +
                          "&inParametro=inTipo&inValor=" + lc_factura[0] + "&inParametro=CodEmp&inValor=" + Convert.ToString(Session["CODEMP"]);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void iBtnFindTercero_Click(object sender, ImageClickEventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            gc_indtercero = "D";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void obj_logistica_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbBL"] = tbBillxWR;
            e.InputParameters["tbBLDT"] = tbContainers;
            e.InputParameters["tbSoportes"] = tbSoportes;
        }
        protected void btn_aceptarf_Click(object sender, EventArgs e)
        {
            SegregacionBL Obj = new SegregacionBL();
            Boolean lb_ind = false,lb_indseg = false;
            int WIH_CONSECUTIVO = 0;
            string SGH_BODDIF = "",BDBODEGA="", OTBODEGA="",TSBODEGA="",TSOTBODE="";
            try
            {
                foreach (DataColumn cl in tbSegregacion.Columns)
                    cl.ReadOnly = false;

                foreach (GridDataItem item in rg_facturast.Items)
                {
                    if ((item.FindControl("chk_chk") as CheckBox).Checked)
                    {
                        lb_indseg = true;
                        foreach (DataRow rw in Obj.GetDetalleSegregacionFacturaDT(null, Convert.ToInt32(item["SGH_CODIGO"].Text)).Rows)
                        {
                            DataRow rx = tbItems.NewRow();

                            rx["WOD_ID"] = tbItems.Rows.Count + 1;
                            rx["WOH_CONSECUTIVO"] = tbItems.Rows.Count + 1; 
                            rx["WID_ID"] = rw["WID_ID"];
                            rx["WIH_CONSECUTIVO"] = rw["WIH_CONSECUTIVO"];
                            WIH_CONSECUTIVO = Convert.ToInt32(rw["WIH_CONSECUTIVO"]);
                            rx["ARCODEMP"] = Convert.ToString(Session["CODEMP"]);

                            rx["WID_NROFACTURA"] = rw["WID_NROFACTURA"];
                            rx["ARTIPPRO"] = rw["ARTIPPRO"];
                            rx["ARCLAVE1"] = rw["ARCLAVE1"];
                            rx["ARCLAVE2"] = rw["ARCLAVE2"];
                            rx["ARCLAVE3"] = rw["ARCLAVE3"];
                            rx["ARCLAVE4"] = rw["ARCLAVE4"];
                            rx["WOD_CANTIDAD"] = rw["SGD_CANTIDAD"];
                            rx["CAN_DIF"] = rw["SGD_CANTIDADAPRO"];
                            rx["WOD_USUARIO"] = ".";
                            rx["TANOMBRE"] = rw["TANOMBRE"];
                            rx["ARNOMBRE"] = rw["ARNOMBRE"];
                            rx["WOD_FECING"] = System.DateTime.Today;
                            rx["WOD_FECMOD"] = System.DateTime.Today;

                            rx["NOMTTEC1"] = rw["NOMTTEC1"];
                            rx["NOMTTEC2"] = rw["NOMTTEC2"];
                            rx["NOMTTEC3"] = rw["NOMTTEC3"];
                            rx["NOMTTEC4"] = rw["NOMTTEC4"];
                            rx["NOMTTEC5"] = rw["NOMTTEC5"];
                            rx["NOMTTEC7"] = rw["NOMTTEC7"];


                            rx["WOD_PRECIO"] = rw["WID_PRECIOVTA"];
                            rx["WOD_PRECIOVTA"] = rw["WID_PRECIOVTA"];

                            rx["TSNROTRA"] = 0;
                            rx["MBIDITEM"] = tbItems.Rows.Count + 1;
                            rx["MIIDMOVI"] = 0;
                            rx["MIOTMOVI"] = 0;

                            SGH_BODDIF = Convert.ToString(rw["SGH_BODDIF"]);
                            BDBODEGA = Convert.ToString(rw["BDBODEGA"]);
                            OTBODEGA = Convert.ToString(rw["OTBODEGA"]);                            
                            TSOTBODE = Convert.ToString(rw["TSOTBODE"]);
                            TSBODEGA = Convert.ToString(rw["TSBODEGA"]);

                            tbItems.Rows.Add(rx);
                            rx = null;
                            lb_indseg = false;

                        }

                        lb_ind = false;
                        foreach (DataRow rw in tbSoportes.Rows)
                        {
                            if (Convert.ToInt32(rw["SGH_CODIGO"]) == Convert.ToInt32(item["SGH_CODIGO"].Text))
                                lb_ind = true;
                        }

                        if (!lb_ind)
                        {
                            DataRow rx = tbSegregacion.NewRow();
                            rx["TSCODEMP"] = "001";
                            rx["TSNROTRA"] = 0;
                            rx["GW_CODIGO"] = tbSegregacion.Rows.Count + 1;
                            //rx["WOH_CONSECUTIVO"] = 0;
                            rx["SGH_CODIGO"] = Convert.ToInt32(item["SGH_CODIGO"].Text);
                            rx["TSOTBOD"] = SGH_BODDIF;
                            rx["WIH_CONSECUTIVO"] = WIH_CONSECUTIVO;
                            rx["BDBODEGA"] = BDBODEGA;
                            rx["OTBODEGA"] = OTBODEGA;
                            rx["TSBODEGA"] = TSBODEGA;
                            rx["TSOTBODE"] = TSOTBODE;

                            tbSegregacion.Rows.Add(rx);
                            rx = null;
                        }

                        if (lb_indseg) {
                            litTextoMensaje.Text = "Segregacion sin WR IN : " + item["SGH_CODIGO"].Text;
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                    }
                }

                (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_logistica.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                (rlv_logistica.InsertItem.FindControl("rg_segregacion") as RadGrid).DataSource = tbSegregacion;
                (rlv_logistica.InsertItem.FindControl("rg_segregacion") as RadGrid).DataBind();
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

        protected void rg_segregacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbSegregacion;
        }
        
        protected void rg_traslados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbTraslados;
        }

        protected void rg_traslados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {                
                case "link_traslado":
                    GridDataItem item = (GridDataItem)e.Item;
                    string url_ = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/Traslados.aspx?Traslado=" + (item.FindControl("lbl_traslado") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url_ + "');", true);
                    item = null;
                    break;                
            }
        }

        protected void rg_segregacion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "link_segregacion":
                    GridDataItem item = (GridDataItem)e.Item;
                    string url_ = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/SegregacionV2.aspx?Segregacion=" + (item.FindControl("lbl_segregacion") as Label).Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url_ + "');", true);
                    item = null;
                    break;
            }
        }
    }
}