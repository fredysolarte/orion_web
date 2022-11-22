using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using XUSS.BLL.Pedidos;
using System.Data;
using XUSS.BLL.Articulos;
using XUSS.BLL.Parametros;
using XUSS.BLL.Inventarios;
using System.Drawing;
using System.Web.Services;
using System.IO;
using XUSS.BLL.Comun;

namespace XUSS.WEB.LtaEmpaque
{
    public partial class LtaEmpaque : System.Web.UI.Page
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
        private DataTable tbBalance
        {
            set { ViewState["tbBalance"] = value; }
            get { return ViewState["tbBalance"] as DataTable; }
        }
        private DataTable tbCajas
        {
            set { ViewState["tbCajas"] = value; }
            get { return ViewState["tbCajas"] as DataTable; }
        }
        private DataTable tbAnexos
        {
            set { ViewState["tbAnexos"] = value; }
            get { return ViewState["tbAnexos"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                myIframe.Attributes["src"] = "//" + HttpContext.Current.Request.Url.Authority + "/webcam.aspx";
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Empaque"])))
                {
                    obj_empaque.SelectParameters["filter"].DefaultValue = "LH_LSTPAQ =" + Convert.ToString(Request.QueryString["Empaque"]);
                    rlv_empaque.DataBind();
                }
            }
        }
        protected void AnalizarCommand(string comando)
        {
            //if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            //    ViewState["toolbars"] = false;
            //else            
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_empaque, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_empaque_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    LtaEmpaqueBL obj = new LtaEmpaqueBL();
                    try {
                        tbItems = obj.GetLtaEmpaqueDT(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbAnexos = obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), 0);
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

                case "Buscar":
                    obj_empaque.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_empaque.DataBind();
                    break;
                case "Edit":                    
                    break;
                case "Delete":
                    LtaEmpaqueBL objD = new LtaEmpaqueBL();
                    try {
                        if ((objD.GetTieneFactura(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_empaque.Items[0].FindControl("txt_lstpaq")) as RadTextBox).Text)) > 0))
                        {
                            litTextoMensaje.Text = "¡Lista de Empaque Ya Cuenta con una Factura!";                            
                            e.Canceled = true;
                        }
                        else {
                            if (Convert.ToString(((rlv_empaque.Items[0].FindControl("rc_estado")) as RadComboBox).SelectedValue) == "AN")
                            {
                                litTextoMensaje.Text = "¡Lista de Empaque Ya Se Encuentra Anulada!";
                            }
                            else
                            {
                                objD.GetAnulalistaEmpaque(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_empaque.Items[0].FindControl("txt_lstpaq")) as RadTextBox).Text), Convert.ToString(Session["UserLogon"]));
                                litTextoMensaje.Text = "¡Lista de Empaque Anulada Exitosamente!";
                            }
                        }
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objD = null;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND (TRNOMBRE + (TRNOMBR2,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";            

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text))
                filtro += " AND LH_PEDIDO = " + (((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text))
                filtro += " AND LH_LSTPAQ = " + (((RadButton)sender).Parent.FindControl("txt_lista") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_empaque.SelectParameters["filter"].DefaultValue = filtro;
            rlv_empaque.DataBind();
            if ((rlv_empaque.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_empaque.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_empaque_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                    try {
                        tbItems = Obj.GetLtaEmpaqueDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                        tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString()));
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataBind();
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
        protected void rg_items_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        int LH_LSTPAQ = Convert.ToInt32(dataItem.GetDataKeyValue("LH_LSTPAQ").ToString());
                        int LD_ITMPAQ = Convert.ToInt32(dataItem.GetDataKeyValue("LD_ITMPAQ").ToString());

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                        try {
                            e.DetailTableView.DataSource =  Obj.GetDetalleMovimientos(null, Convert.ToString(Session["CODEMP"]), LH_LSTPAQ, LD_ITMPAQ);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally {
                            Obj = null;
                        }
                        break;
                    }
                case "detalle_caja":
                        int LH_LSTPAQ__ = Convert.ToInt32(dataItem.GetDataKeyValue("LH_LSTPAQ").ToString());
                        int LD_ITMPAQ__ = Convert.ToInt32(dataItem.GetDataKeyValue("LD_ITMPAQ").ToString());

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        LtaEmpaqueBL Obj_ = new LtaEmpaqueBL();
                        try {
                            e.DetailTableView.DataSource =  Obj_.GetCajas(null, Convert.ToString(Session["CODEMP"]), LH_LSTPAQ__, LD_ITMPAQ__);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally {
                            Obj_ = null;
                        }
                        break;                    
                case "detalle_insert":
                    {
                        int LH_LSTPAQ = Convert.ToInt32(dataItem.GetDataKeyValue("LH_LSTPAQ").ToString());
                        int LD_ITMPAQ = Convert.ToInt32(dataItem.GetDataKeyValue("LD_ITMPAQ").ToString());

                        DataTable dt = new DataTable();
                        //dt = tbBalance.Copy();
                        dt.Columns.Add("TP", typeof(string));
                        dt.Columns.Add("C1", typeof(string));
                        dt.Columns.Add("C2", typeof(string));
                        dt.Columns.Add("C3", typeof(string));
                        dt.Columns.Add("C4", typeof(string));
                        dt.Columns.Add("MBBODEGA", typeof(string));
                        dt.Columns.Add("MLCDLOTE", typeof(string));
                        dt.Columns.Add("MLCANTID", typeof(Int32));
                        dt.Columns.Add("MBCANTID", typeof(Int32));
                        dt.Columns.Add("IT", typeof(Int32));

                        try
                        {
                            for (int i = 0; i < tbBalance.Rows.Count; i++ )
                            {
                                if (Convert.ToInt32(((DataRow)tbBalance.Rows[i])["IT"]) == LD_ITMPAQ)
                                {
                                    DataRow rw = dt.NewRow();
                                    rw["TP"] = ((DataRow)tbBalance.Rows[i])["TP"];
                                    rw["C1"] = ((DataRow)tbBalance.Rows[i])["C1"];
                                    rw["C2"] = ((DataRow)tbBalance.Rows[i])["C2"];
                                    rw["C3"] = ((DataRow)tbBalance.Rows[i])["C3"];
                                    rw["C4"] = ((DataRow)tbBalance.Rows[i])["C4"];
                                    rw["MBBODEGA"] = ((DataRow)tbBalance.Rows[i])["MBBODEGA"];
                                    rw["MLCDLOTE"] = ((DataRow)tbBalance.Rows[i])["MLCDLOTE"];
                                    rw["MLCANTID"] = ((DataRow)tbBalance.Rows[i])["MLCANTID"];
                                    rw["MBCANTID"] = ((DataRow)tbBalance.Rows[i])["MBCANTID"];
                                    rw["IT"] = ((DataRow)tbBalance.Rows[i])["IT"];

                                    dt.Rows.Add(rw);
                                    rw = null;
                                }
                                    //dt.Rows[i].Delete();
                            }

                            dt.AcceptChanges();
                            e.DetailTableView.DataSource = dt;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            dt = null;
                        }
                        break;                        
                    }
                case "detalle_caja_insert":
                    
                    int LD_ITMPAQ_ = Convert.ToInt32(dataItem.GetDataKeyValue("IT").ToString());

                    DataTable dt_ = new DataTable();
                        //dt = tbBalance.Copy();
                    dt_.Columns.Add("LD_CODEMP", typeof(string));
                    dt_.Columns.Add("LH_LSTPAQ", typeof(Int32));
                    dt_.Columns.Add("LD_ITMPAQ", typeof(Int32));
                    dt_.Columns.Add("CL_CAJA", typeof(Int32));
                    dt_.Columns.Add("CL_CANTIDAD", typeof(Int32));
                    dt_.Columns.Add("TP", typeof(string));
                    dt_.Columns.Add("C1", typeof(string));
                    dt_.Columns.Add("C2", typeof(string));
                    dt_.Columns.Add("C3", typeof(string));
                    dt_.Columns.Add("C4", typeof(string));
                    dt_.Columns.Add("MBBODEGA", typeof(string));
                    dt_.Columns.Add("MLCDLOTE", typeof(string));

                    for (int i = 0; i < tbCajas.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(((DataRow)tbCajas.Rows[i])["LD_ITMPAQ"]) == LD_ITMPAQ_)
                        {
                            DataRow rw_ = dt_.NewRow();
                            rw_["LD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                            rw_["LH_LSTPAQ"] = 0;
                            rw_["LD_ITMPAQ"] = LD_ITMPAQ_;
                            rw_["CL_CAJA"] = ((DataRow)tbCajas.Rows[i])["CL_CAJA"];
                            rw_["CL_CANTIDAD"] = ((DataRow)tbCajas.Rows[i])["CL_CANTIDAD"];
                            rw_["TP"] = ((DataRow)tbCajas.Rows[i])["TP"];
                            rw_["C1"] = ((DataRow)tbCajas.Rows[i])["C1"];
                            rw_["C2"] = ((DataRow)tbCajas.Rows[i])["C2"];
                            rw_["C3"] = ((DataRow)tbCajas.Rows[i])["C3"];
                            rw_["C4"] = ((DataRow)tbCajas.Rows[i])["C4"];
                            rw_["MBBODEGA"] = ((DataRow)tbCajas.Rows[i])["MBBODEGA"];
                            rw_["MLCDLOTE"] = ((DataRow)tbCajas.Rows[i])["MLCDLOTE"];

                            dt_.Rows.Add(rw_);
                            rw_ = null;
                        }
                    }
                    e.DetailTableView.DataSource = dt_;
                    break;
            }
        }
        protected void iBtnBuscarPedido_OnClick(object sender, EventArgs e)
        {
            obj_pedido.SelectParameters["filter"].DefaultValue = "PHESTADO='LQ'";
            rgConsultaPedidos.DataBind();
            string script = "function f(){$find(\"" + mpBuscarPedido.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroPed_OnClick(object sender, EventArgs e)
        {
            string filter = "PHESTADO='LQ' ";
            if (!string.IsNullOrWhiteSpace(edt_nomtercero.Text))
                filter += "AND UPPER(TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + edt_nomtercero.Text.ToUpper() + "%'";

            if (!string.IsNullOrWhiteSpace(edt_pedido.Text))
                filter += "AND PHPEDIDO = " + edt_pedido.Text.ToUpper();


            obj_pedido.SelectParameters["filter"].DefaultValue = filter;
            rgConsultaPedidos.DataBind();
            
            string script = "function f(){$find(\"" + mpBuscarPedido.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaPedidos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string script = "";
            switch (e.CommandName)
            {
                case "Select":
                GridDataItem item = (GridDataItem)e.Item;
                //PedidosBL Obj = new PedidosBL();      
                LtaEmpaqueBL ObjL = new LtaEmpaqueBL();
                DataTable dt = new DataTable();
                DataTable dt_ = new DataTable();
                try
                {
                    (rlv_empaque.InsertItem.FindControl("txt_pedido") as RadTextBox).Text = Convert.ToString(item["PHPEDIDO"].Text);
                    (rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue = Convert.ToString(item["PHBODEGA"].Text);
                    tbItems = ObjL.GetLtaEmpaqueDT(null, null, 0);                        
                    foreach (DataRow row in ObjL.GetPedidoDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(item["PHPEDIDO"].Text)).Rows)
                    {
                        DataRow rw = tbItems.NewRow();
                        rw["LD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rw["LH_LSTPAQ"] = (rlv_empaque.InsertItem.FindControl("txt_pedido") as RadTextBox).Text;
                        rw["LD_ITMPAQ"] = Convert.ToInt32(row["PDLINNUM"]);
                        rw["LD_TIPPRO"] = Convert.ToString(row["PDTIPPRO"]);
                        rw["LD_CLAVE1"] = Convert.ToString(row["PDCLAVE1"]);
                        rw["LD_CLAVE2"] = Convert.ToString(row["PDCLAVE2"]);
                        rw["LD_CLAVE3"] = Convert.ToString(row["PDCLAVE3"]);
                        rw["LD_CLAVE4"] = Convert.ToString(row["PDCLAVE4"]);                        
                        rw["LD_CANTID"] = Convert.ToInt32(row["PDCANTID"]) - Convert.ToInt32(row["CAN_LST"]);
                        rw["LD_PESONT"] = 0;
                            //rw["LD_BODEGA"] = Convert.ToString((rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue);
                        rw["LD_BODEGA"] = Convert.ToString(row["PDBODEGA"]);
                        rw["LD_CDLOTE"] = 0;
                        rw["LD_CDELEM"] = 0;
                        rw["LD_NRMOV"] = 0;
                        rw["LD_ESTADO"] = "AC";
                        rw["LD_CAUSAE"] = ".";
                        rw["LD_NMUSER"] = Convert.ToString(Session["UserLogon"]);
                        rw["LD_FECING"] = System.DateTime.Today;
                        rw["LD_FECMOD"] = System.DateTime.Today;
                        rw["ARNOMBRE"] = Convert.ToString(row["ARNOMBRE"]);
                        rw["PDPRECIO"] = Convert.ToDouble(row["PDPRECIO"]);
                        rw["PDPRELIS"] = 0;
                        rw["PDDESCUE"] = 0;
                        rw["TTVALORF"] = 0;
                        rw["PDCODDES"] = 0;
                        rw["TANOMBRE"] = Convert.ToString(row["TANOMBRE"]);
                        rw["LD_CANCAN"] = Convert.ToInt32(row["PDCANTID"]) - Convert.ToInt32(row["CAN_LST"]);

                        tbItems.Rows.Add(rw);

                    }
                    (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                    //Balances
                    tbBalance = dt;
                    //Crear Campos Persistentes
                    tbBalance.Columns.Add("TP", typeof(string));
                    tbBalance.Columns.Add("C1", typeof(string));
                    tbBalance.Columns.Add("C2", typeof(string));
                    tbBalance.Columns.Add("C3", typeof(string));
                    tbBalance.Columns.Add("C4", typeof(string));
                    tbBalance.Columns.Add("MBBODEGA", typeof(string));
                    tbBalance.Columns.Add("MLCDLOTE", typeof(string));
                    tbBalance.Columns.Add("MLCANTID", typeof(Int32));
                    tbBalance.Columns.Add("MBCANTID", typeof(Int32));
                    tbBalance.Columns.Add("IT", typeof(Int32));

                    //Cajas
                    tbCajas = dt_;
                    tbCajas.Columns.Add("LD_CODEMP", typeof(string));
                    tbCajas.Columns.Add("LH_LSTPAQ", typeof(Int32));
                    tbCajas.Columns.Add("LD_ITMPAQ", typeof(Int32));
                    tbCajas.Columns.Add("CL_CAJA", typeof(Int32));
                    tbCajas.Columns.Add("CL_CANTIDAD", typeof(Int32));
                    tbCajas.Columns.Add("TP", typeof(string));
                    tbCajas.Columns.Add("C1", typeof(string));
                    tbCajas.Columns.Add("C2", typeof(string));
                    tbCajas.Columns.Add("C3", typeof(string));
                    tbCajas.Columns.Add("C4", typeof(string));
                    tbCajas.Columns.Add("MBBODEGA", typeof(string));
                    tbCajas.Columns.Add("MLCDLOTE", typeof(string));
                    
                    rc_cajam.Items.Clear();

                    RadComboBoxItem itemc = new RadComboBoxItem();
                    itemc.Value = Convert.ToString(rc_cajam.Items.Count + 1);
                    itemc.Text = "Caja " + Convert.ToString(rc_cajam.Items.Count + 1);
                    rc_cajam.Items.Add(itemc);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally {
                    item = null;
                    //Obj = null;
                    ObjL = null;
                    dt = null;
                    dt_ = null;
                }
                break;
                default:
                script = "function f(){$find(\"" + mpBuscarPedido.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                break;
            }
        }
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {                            
            double ln_cantidad = 0;                
            LtaEmpaqueBL obj = new LtaEmpaqueBL();
            try
            {
                if (e.CommandName == "Select")
                {
                        GridDataItem item = (GridDataItem)e.Item;

                        string lc_c1 = Convert.ToString((item.FindControl("lbl_clave1") as Label).Text);
                        string lc_c2 = Convert.ToString((item.FindControl("lbl_clave2") as Label).Text);
                        string lc_c3 = Convert.ToString((item.FindControl("lbl_clave3") as Label).Text);
                        string lc_c4 = Convert.ToString((item.FindControl("lbl_clave4") as Label).Text);

                        foreach (DataRow rx in tbBalance.Rows)
                        {
                            if ((Convert.ToString(rx["TP"]) == Convert.ToString(item["LD_TIPPRO"].Text)) && (Convert.ToString(rx["C1"]) == lc_c1)
                                            && (Convert.ToString(rx["C2"]) == lc_c2) && (Convert.ToString(rx["C3"]) == lc_c3)
                                            && (Convert.ToString(rx["C4"]) == lc_c4))
                            {
                                if (Convert.ToString(rx["MLCDLOTE"]) == "")
                                    ln_cantidad += Convert.ToDouble(rx["MBCANTID"]);
                                else
                                    ln_cantidad += Convert.ToDouble(rx["MLCANTID"]);
                            }
                        }

                        if (ln_cantidad < Convert.ToDouble(item["LD_CANTID"].Text))
                        {
                            DataTable tbtmp = new DataTable();
                        //tbtmp = obj.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item["LD_TIPPRO"].Text), lc_c1, lc_c2,lc_c3,lc_c4, Convert.ToString((rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue),

                        tbtmp = obj.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item["LD_TIPPRO"].Text), lc_c1, lc_c2, lc_c3, lc_c4, Convert.ToString((item.FindControl("rc_bodegadt") as RadComboBox).SelectedValue), //Convert.ToString((rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue),
                                                    Convert.ToInt32(item["LD_ITMPAQ"].Text));
                            foreach (DataRow rw in tbtmp.Rows)
                            {
                                foreach (DataRow rx in tbBalance.Rows)
                                {
                                    if (Convert.ToString(rw["BLCDLOTE"]) == "")
                                    {
                                        if ((Convert.ToString(rx["TP"]) == Convert.ToString(rw["BBTIPPRO"])) && (Convert.ToString(rx["C1"]) == Convert.ToString(rw["BBCLAVE1"]))
                                            && (Convert.ToString(rx["C2"]) == Convert.ToString(rw["BBCLAVE2"])) && (Convert.ToString(rx["C3"]) == Convert.ToString(rw["BBCLAVE3"]))
                                            && (Convert.ToString(rx["C4"]) == Convert.ToString(rw["BBCLAVE4"])))
                                            rw["BBCANTID"] = Convert.ToDouble(rw["BBCANTID"]) - Convert.ToDouble(rx["MBCANTID"]);
                                    }
                                    else
                                    {
                                        if ((Convert.ToString(rx["TP"]) == Convert.ToString(rw["BBTIPPRO"])) && (Convert.ToString(rx["C1"]) == Convert.ToString(rw["BBCLAVE1"]))
                                            && (Convert.ToString(rx["C2"]) == Convert.ToString(rw["BBCLAVE2"])) && (Convert.ToString(rx["C3"]) == Convert.ToString(rw["BBCLAVE3"]))
                                            && (Convert.ToString(rx["C4"]) == Convert.ToString(rw["BBCLAVE4"])) && (Convert.ToString(rx["MLCDLOTE"]) == Convert.ToString(rw["BLCDLOTE"])))
                                        {
                                            rw["BBCANTID"] = Convert.ToDouble(rw["BBCANTID"]) - Convert.ToDouble(rx["MLCANTID"]);
                                            rw["BLCANTID"] = Convert.ToDouble(rw["BLCANTID"]) - Convert.ToDouble(rx["MLCANTID"]);
                                        }
                                    }
                                }
                            }
                            rgBalance.DataSource = tbtmp;
                            rgBalance.DataBind();
                            StringBuilder str = new StringBuilder();
                            str.AppendLine("<div class=\"alert alert-success\">");
                            str.AppendLine(" <span class=\"glyphicon glyphicon-exclamation-sign\" aria-hidden=\"true\"></span>");
                            str.AppendLine("<strong>Referencia:</strong>" + Convert.ToString(item["LD_CLAVE1"].Text) + " <strong>Cantidad:</strong>" + Convert.ToString(item["LD_CANTID"].Text));
                            str.AppendLine("</div>");
                            lt_msjdisponibilidad.Text = str.ToString();
                            
                            string script = "function f(){$find(\"" + mpDisposicion.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            lt_msjbalance.Text = "";
                        }
                        else
                        {                            
                                litTextoMensaje.Text = "Ya se Asigno Cantidad al Item";
                                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                e.Canceled = true;                            
                        }

                        item = null;                
                }
                if (e.CommandName == "InitInsert")
                {

                    this.Limpiar();
                    rc_cajam.SelectedValue = Convert.ToString(rc_cajam.Items.Count);
                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    e.Canceled = true;
                }
                else
                {
                    if (e.CommandName == "link")
                    {
                        GridDataItem item_ = (GridDataItem)e.Item;
                        string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_clave1") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        item_ = null;
                    }
                    else
                    {
                        if (e.CommandName == "NewBox")
                        {
                            RadComboBoxItem item = new RadComboBoxItem();
                            item.Value = Convert.ToString(rc_cajam.Items.Count + 1);
                            item.Text = "Caja " + Convert.ToString(rc_cajam.Items.Count + 1);
                            rc_cajam.Items.Add(item);

                            litTextoMensaje.Text ="Se Agrego la Caja Nro :"+ Convert.ToString(item.Value);
                            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                        }
                        else
                        {
                            if (e.CommandName == "ResumeBox")
                            {
                                int ln_cant=0;

                                DataTable dt = new DataTable();
                                dt.Columns.Add("caja", typeof(Int32));
                                dt.Columns.Add("cantidad", typeof(Int32));

                                //while (i <= rc_cajam.Items.Count)
                                foreach(RadComboBoxItem it in rc_cajam.Items)
                                {                                    
                                    ln_cant = 0;
                                    foreach (DataRow itm in tbCajas.Rows)
                                    {
                                        if ( Convert.ToInt32(it.Value) == Convert.ToInt32(itm["CL_CAJA"]))
                                            ln_cant += Convert.ToInt32(itm["CL_CANTIDAD"]);
                                    }

                                    DataRow rx = dt.NewRow();
                                    //rx["caja"] = ln_caja;
                                    rx["caja"] = it.Value;
                                    rx["cantidad"] = ln_cant;
                                    dt.Rows.Add(rx);
                                    rx = null;                                    
                                }
                                rg_resumebox.DataSource = dt;
                                rg_resumebox.DataBind();
                                dt = null;
                                string script = "function f(){$find(\"" + mpResumeBox.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            }
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
                obj = null;
            }
        }
        protected void btn_aceptardis_OnClick(object sender, EventArgs e)
        {
            double? ln_cantidad = 0,ln_canacumulado=0;
            string lc_tp = "", lc_c1 = "", lc_c2 = "", lc_c3 = "", lc_c4 = "";
            int ln_item = 0;
            Boolean lb_ind = true,lb_indexiste =true,lb_indinsert=true;
            try {

                //Valida Cantidades
                for (int i = 0; i < rgBalance.Items.Count; i++)
                {
                    lc_tp = ((GridDataItem)rgBalance.Items[i])["BBTIPPRO"].Text;
                    lc_c1 = ((GridDataItem)rgBalance.Items[i])["BBCLAVE1"].Text;
                    lc_c2 = ((GridDataItem)rgBalance.Items[i])["BBCLAVE2"].Text;
                    lc_c3 = ((GridDataItem)rgBalance.Items[i])["BBCLAVE3"].Text;
                    lc_c4 = ((GridDataItem)rgBalance.Items[i])["BBCLAVE4"].Text;
                    ln_item = Convert.ToInt32(((GridDataItem)rgBalance.Items[i])["IT"].Text);

                    if (((CheckBox)rgBalance.Items[i].FindControl("chk_estado")).Checked)
                    {
                        if (string.IsNullOrEmpty(((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text) || ((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text=="&nbsp;")
                            ln_cantidad += ((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value;                            
                        else
                            ln_cantidad += ((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value;                        
                    }
                }

                lb_indexiste = false;
                //Validar cantidad x Bodega
                foreach (DataRow rz in tbBalance.Rows)
                {
                    if (Convert.ToInt32(rz["IT"]) == ln_item)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(rz["MLCDLOTE"])))
                            ln_canacumulado += Convert.ToDouble(rz["MBCANTID"]);
                        else
                            ln_canacumulado += Convert.ToDouble(rz["MLCANTID"]);
                        lb_indexiste = true;
                    }
                }

                for (int i = 0; i < (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items.Count; i++)
                {
                    if (((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i]).OwnerTableView.Name != "detalle_insert" && ((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i]).OwnerTableView.Name != "detalle_caja_insert" && (Convert.ToInt32(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_ITMPAQ"].Text) == ln_item))
                    {
                        if (Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text) < (ln_cantidad+ln_canacumulado))
                        {
                            lb_ind = false;
                            break;
                        }
                    }
                }

                if (lb_ind)
                {
                    for (int i = 0; i < rgBalance.Items.Count; i++)
                    {
                        if (((CheckBox)rgBalance.Items[i].FindControl("chk_estado")).Checked)
                        {
                            if (!lb_indexiste)
                            {
                                this.InsertBalance(((GridDataItem)rgBalance.Items[i])["BBTIPPRO"].Text, ((GridDataItem)rgBalance.Items[i])["BBCLAVE1"].Text,
                                                   ((GridDataItem)rgBalance.Items[i])["BBCLAVE2"].Text, ((GridDataItem)rgBalance.Items[i])["BBCLAVE3"].Text,
                                                   ((GridDataItem)rgBalance.Items[i])["BBCLAVE4"].Text, ((GridDataItem)rgBalance.Items[i])["BBBODEGA"].Text,
                                                   Convert.ToInt32(((GridDataItem)rgBalance.Items[i])["IT"].Text),
                                                   Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value),
                                                   ((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text,
                                                   Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value));
                            }
                            else
                            {
                                lb_indinsert = true;
                                foreach (DataRow rt in tbBalance.Rows)
                                {
                                    if (Convert.ToInt32(rt["IT"]) == ln_item && Convert.ToString(rt["MBBODEGA"]) == ((GridDataItem)rgBalance.Items[i])["BBBODEGA"].Text && (string.IsNullOrEmpty(((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text) || (((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text == "&nbsp;")))
                                    {
                                        lb_indinsert = false;
                                        rt["MBCANTID"] = Convert.ToDouble(rt["MBCANTID"]) + Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value);
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(rt["IT"]) == ln_item && Convert.ToString(rt["MBBODEGA"]) == ((GridDataItem)rgBalance.Items[i])["BBBODEGA"].Text && Convert.ToString(rt["MLCDLOTE"]) == ((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text)
                                        {
                                            lb_indinsert = false;
                                            rt["MLCANTID"] = Convert.ToDouble(rt["MLCANTID"]) + Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value);
                                        }                                        
                                    }
                                }

                                //
                                if (lb_indinsert)
                                {
                                    this.InsertBalance(((GridDataItem)rgBalance.Items[i])["BBTIPPRO"].Text, ((GridDataItem)rgBalance.Items[i])["BBCLAVE1"].Text,
                                                       ((GridDataItem)rgBalance.Items[i])["BBCLAVE2"].Text, ((GridDataItem)rgBalance.Items[i])["BBCLAVE3"].Text,
                                                       ((GridDataItem)rgBalance.Items[i])["BBCLAVE4"].Text, ((GridDataItem)rgBalance.Items[i])["BBBODEGA"].Text,
                                                       Convert.ToInt32(((GridDataItem)rgBalance.Items[i])["IT"].Text),
                                                       Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value),
                                                       ((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text,
                                                       Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value));
                                }
                            }
                            //Cajas 
                            DataRow rx = tbCajas.NewRow();
                            rx["LD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                            rx["LH_LSTPAQ"] = 0;
                            rx["LD_ITMPAQ"] = ln_item;
                            rx["CL_CAJA"] = ((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_caja")).Value;
                            rx["CL_CANTIDAD"] = ((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value;
                            rx["TP"] = ((GridDataItem)rgBalance.Items[i])["BBTIPPRO"].Text;
                            rx["C1"] = ((GridDataItem)rgBalance.Items[i])["BBCLAVE1"].Text;
                            rx["C2"] = ((GridDataItem)rgBalance.Items[i])["BBCLAVE2"].Text;
                            rx["C3"] = ((GridDataItem)rgBalance.Items[i])["BBCLAVE3"].Text;
                            rx["C4"] = ((GridDataItem)rgBalance.Items[i])["BBCLAVE4"].Text;
                            rx["MBBODEGA"] = ((GridDataItem)rgBalance.Items[i])["BBBODEGA"].Text;
                            rx["MLCDLOTE"] = ((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text;
                            if (string.IsNullOrEmpty(((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text) || (((GridDataItem)rgBalance.Items[i])["BLCDLOTE"].Text == "&nbsp;"))                
                                rx["MLCDLOTE"] = "";
                            
                            tbCajas.Rows.Add(rx);
                            rx = null;

                            // Carga Cantidad Principal
                            foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                            foreach (DataRow rw in tbItems.Rows)
                            {
                                if (Convert.ToInt32(rw["LD_ITMPAQ"]) == ln_item)
                                    rw["LD_CANCAN"] = Convert.ToDouble(rw["LD_CANCAN"]) - Convert.ToDouble(((RadNumericTextBox)rgBalance.Items[i].FindControl("edt_cantidad")).Value);
                            }
                            (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                            (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                        }
                    }
                }
                else
                {
                    StringBuilder str = new StringBuilder();                    
                    str.AppendLine("<div class=\"alert alert-danger\">");
                    str.AppendLine(" <span class=\"glyphicon glyphicon-exclamation-sign\" aria-hidden=\"true\"></span>");
                    str.AppendLine("<strong>Error!</strong>Cantidad Seleccionada Supera Cantidad Solicitada");
                    str.AppendLine("</div>");
                    lt_msjbalance.Text = str.ToString();
                    string script = "function f(){$find(\"" + mpDisposicion.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        private void InsertBalance(string TP, string C1, string C2, string C3, string C4, string MBBODEGA, int IT, double MBCANTID, string MLCDLOTE, double MLCANTID)
        {
            DataRow rw = tbBalance.NewRow();
            try
            {
                rw["TP"] = TP;
                rw["C1"] = C1;
                rw["C2"] = C2;
                rw["C3"] = C3;
                rw["C4"] = C4;
                rw["MBBODEGA"] = MBBODEGA;

                rw["IT"] = IT;
                if (string.IsNullOrEmpty(MLCDLOTE) || (MLCDLOTE == "&nbsp;"))
                {
                    rw["MBCANTID"] = MBCANTID;
                    rw["MLCDLOTE"] = "";
                }
                else
                {
                    rw["MLCDLOTE"] = MLCDLOTE;
                    rw["MLCANTID"] = MLCANTID;
                }

                tbBalance.Rows.Add(rw);            
                
            } catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
            rw = null;
            }
        }
        protected void obj_empaque_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbDetalle"] = tbBalance;
            e.InputParameters["tbCajas"] = tbCajas;
            e.InputParameters["tbAnexos"] = tbAnexos;
        }
        protected void obj_empaque_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Nro Lista Empaque :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri)+ "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=6004&inban=S&inParametro=InConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            
        }
        protected void rlv_empaque_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            switch (e.Item.OwnerTableView.Name)
            {
                case "detalle_insert":
                    int itm = 0;
                    double ln_cancan = 0;
                    string Lote = (e.Item as GridEditableItem).GetDataKeyValue("MLCDLOTE").ToString();
                    string TP = (e.Item as GridEditableItem).GetDataKeyValue("TP").ToString();
                    string C1 = (e.Item as GridEditableItem).GetDataKeyValue("C1").ToString();
                    string C2 = (e.Item as GridEditableItem).GetDataKeyValue("C2").ToString();
                    string C3 = (e.Item as GridEditableItem).GetDataKeyValue("C3").ToString();
                    string C4 = (e.Item as GridEditableItem).GetDataKeyValue("C4").ToString();
                    string BD = (e.Item as GridEditableItem).GetDataKeyValue("MBBODEGA").ToString();                    
                    int pos = 0;

                    foreach (DataRow row in tbBalance.Rows)
                    {
                        if ((Convert.ToString(row["TP"]) == TP) && (Convert.ToString(row["C1"]) == C1) && (Convert.ToString(row["C2"]) == C2) && (Convert.ToString(row["C3"]) == C3)
                            && (Convert.ToString(row["C4"]) == C4) && (Convert.ToString(row["MBBODEGA"]) == BD) && (Convert.ToString(row["MLCDLOTE"]) == Lote))
                        {
                            itm = Convert.ToInt32(row["IT"]);
                            if (Lote=="")
                                ln_cancan = Convert.ToDouble(row["MBCANTID"]);
                            else
                                ln_cancan = Convert.ToDouble(row["MLCANTID"]);
                            tbBalance.Rows[pos].Delete();                            
                            break;
                        }
                        pos++;
                    }
                    tbBalance.AcceptChanges();
                    //Borra Cajas Hijos
                    foreach (DataRow row in tbCajas.Rows)
                    {
                        if ((Convert.ToString(row["TP"]) == TP) && (Convert.ToString(row["C1"]) == C1) && (Convert.ToString(row["C2"]) == C2) && (Convert.ToString(row["C3"]) == C3)
                            && (Convert.ToString(row["C4"]) == C4) && (Convert.ToString(row["MBBODEGA"]) == BD) && (Convert.ToString(row["MLCDLOTE"]) == Lote))
                        {
                            tbCajas.Rows[pos].Delete();
                            break;
                        }
                        pos++;
                    }
                    tbCajas.AcceptChanges();
                    //Carga Principal
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToInt32(rw["LD_ITMPAQ"]) == itm)
                            rw["LD_CANCAN"] = Convert.ToDouble(rw["LD_CANCAN"]) + ln_cancan;
                    }                    
                    break;
                case "detalle_caja_insert":
                    int itm_ = 0;
                    string Lote_ = (e.Item as GridEditableItem).GetDataKeyValue("MLCDLOTE").ToString();
                    string TP_ = (e.Item as GridEditableItem).GetDataKeyValue("TP").ToString();
                    string C1_ = (e.Item as GridEditableItem).GetDataKeyValue("C1").ToString();
                    string C2_ = (e.Item as GridEditableItem).GetDataKeyValue("C2").ToString();
                    string C3_ = (e.Item as GridEditableItem).GetDataKeyValue("C3").ToString();
                    string C4_ = (e.Item as GridEditableItem).GetDataKeyValue("C4").ToString();
                    string BD_ = (e.Item as GridEditableItem).GetDataKeyValue("MBBODEGA").ToString();
                    string CJ_ = (e.Item as GridEditableItem).GetDataKeyValue("CL_CAJA").ToString();
                    int pos_ = 0;
                    int ln_cantidad = 0;                    
                    //Borra Cajas Hijos
                    foreach (DataRow row in tbCajas.Rows)
                    {
                        if ((Convert.ToString(row["TP"]) == TP_) && (Convert.ToString(row["C1"]) == C1_) && (Convert.ToString(row["C2"]) == C2_) && (Convert.ToString(row["C3"]) == C3_)
                            && (Convert.ToString(row["C4"]) == C4_) && (Convert.ToString(row["MBBODEGA"]) == BD_) && (Convert.ToString(row["MLCDLOTE"]) == Lote_) && (Convert.ToString(row["CL_CAJA"]) == CJ_))
                        {
                            ln_cantidad = Convert.ToInt32(row["CL_CANTIDAD"]);
                            tbCajas.Rows[pos_].Delete();
                            break;
                        }
                        pos_++;
                    }
                    tbCajas.AcceptChanges();
                    //
                    foreach (DataRow row in tbBalance.Rows)
                    {
                        if ((Convert.ToString(row["TP"]) == TP_) && (Convert.ToString(row["C1"]) == C1_) && (Convert.ToString(row["C2"]) == C2_) && (Convert.ToString(row["C3"]) == C3_)
                            && (Convert.ToString(row["C4"]) == C4_) && (Convert.ToString(row["MBBODEGA"]) == BD_) && (Convert.ToString(row["MLCDLOTE"]) == Lote_))
                        {
                            itm_ = Convert.ToInt32(row["IT"]);
                            if ( string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                                row["MBCANTID"] = Convert.ToDouble(row["MBCANTID"]) -ln_cantidad;
                            else
                                row["MLCANTID"] = Convert.ToDouble(row["MLCANTID"]) - ln_cantidad;
                            break;
                        }                        
                    }
                    // 0
                    pos_ = 0;
                    foreach (DataRow row in tbBalance.Rows)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                        {
                            if (Convert.ToDouble(row["MBCANTID"]) == 0)
                            {
                                tbBalance.Rows[pos_].Delete();
                                break;
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(row["MBCANTID"]) == 0 && Convert.ToDouble(row["MLCANTID"]) == 0)
                            {
                                tbBalance.Rows[pos_].Delete();
                                break;
                            }
                        }
                        pos_++;
                    }
                    tbBalance.AcceptChanges();
                    //Carga Principal
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Convert.ToInt32(rw["LD_ITMPAQ"]) == itm_)
                            rw["LD_CANCAN"] = Convert.ToDouble(rw["LD_CANCAN"]) + ln_cantidad;
                    }       
                    break;
            }
            //ViewState["del"] = true;
            //(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
            //(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();            
        }
        protected void rlv_empaque_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            //string lc_mensaje = "";
            //Boolean lb_bandera = false;

            //if (tbBalance.Rows.Count <= 0)
            //{
            //    lb_bandera = true;
            //    lc_mensaje += "No Cuenta con Items de Detalle";
            //}

            //if (lb_bandera)
            //{
            //    litTextoMensaje.Text = lc_mensaje;
            //    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            //    e.Canceled = true;
            //}
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=6004&inban=S&inParametro=InConsecutivo&inValor=" + rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void chk_estado_OnCheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                double? ln_total=0;
                int ln_item = Convert.ToInt32((((CheckBox)sender).Parent.FindControl("edt_it") as RadTextBox).Text);
                for (int i = 0; i < (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items.Count; i++)
                {
                    if (((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i]).OwnerTableView.Name != "detalle_insert" && ((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i]).OwnerTableView.Name != "detalle_caja_insert" && (Convert.ToInt32(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_ITMPAQ"].Text) == ln_item))
                    {
                        ln_total = 0;
                        //suma totales
                        for (int j = 0; j < rgBalance.Items.Count; j++)
                        {
                            if ((rgBalance.Items[j].FindControl("chk_estado") as CheckBox).Checked)
                            {
                                ln_total += (rgBalance.Items[j].FindControl("edt_cantidad") as RadNumericTextBox).Value;
                            }
                        }

                        if (ln_total < Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text))
                        {
                            if (!string.IsNullOrEmpty((((CheckBox)sender).Parent.FindControl("edt_lote") as RadTextBox).Text) && (((CheckBox)sender).Parent.FindControl("edt_cantidadl") as RadNumericTextBox).Value != 0)
                            {
                                if ((((CheckBox)sender).Parent.FindControl("edt_cantidadl") as RadNumericTextBox).Value >= Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text))
                                    (((CheckBox)sender).Parent.FindControl("edt_cantidad") as RadNumericTextBox).Value = Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text) - ln_total;
                                else
                                    (((CheckBox)sender).Parent.FindControl("edt_cantidad") as RadNumericTextBox).Value = (((CheckBox)sender).Parent.FindControl("edt_cantidadl") as RadNumericTextBox).Value - ln_total;
                            }
                            else
                            {
                                if ((((CheckBox)sender).Parent.FindControl("edt_cantidadb") as RadNumericTextBox).Value >= Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text))
                                    (((CheckBox)sender).Parent.FindControl("edt_cantidad") as RadNumericTextBox).Value = Convert.ToDouble(((GridDataItem)(rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).Items[i])["LD_CANTID"].Text) - ln_total;
                                else
                                    (((CheckBox)sender).Parent.FindControl("edt_cantidad") as RadNumericTextBox).Value = (((CheckBox)sender).Parent.FindControl("edt_cantidadb") as RadNumericTextBox).Value - ln_total;
                            }
                        }
                        else {
                            ((CheckBox)sender).Checked = false;
                        }
                        (((CheckBox)sender).Parent.FindControl("edt_caja") as RadNumericTextBox).Value = Convert.ToInt32(rc_cajam.SelectedValue);
                        break;                        
                    }
                }                
            }
            else
                (((CheckBox)sender).Parent.FindControl("edt_cantidad") as RadNumericTextBox).Value = 0;

            string script = "function f(){$find(\"" + mpDisposicion.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void ct_menu_ItemClick(object sender, RadMenuEventArgs e)
        {
            double ln_cantidad = 0,ln_cantot = 0;
            int ln_item = 0;
            string radGridClickedRowIndex;
            radGridClickedRowIndex = Convert.ToString(Request.Form["radGridClickedRowIndex"]);
            switch (e.Item.Text)
            {
                case "Seleccionar Todos":
                    LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                    try
                    {
                        tbBalance.Clear();
                        foreach (DataRow rx in tbItems.Rows)
                        {
                            ln_cantidad = 0;
                            ln_item = Convert.ToInt32(rx["LD_ITMPAQ"]);
                            //foreach (DataRow rw in Obj.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["LD_TIPPRO"]), Convert.ToString(rx["LD_CLAVE1"]), Convert.ToString(rx["LD_CLAVE2"]), Convert.ToString(rx["LD_CLAVE3"]), Convert.ToString(rx["LD_CLAVE4"]), Convert.ToString((rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue), Convert.ToInt32(rx["LD_ITMPAQ"])).Rows)
                            foreach (DataRow rw in Obj.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["LD_TIPPRO"]), Convert.ToString(rx["LD_CLAVE1"]), Convert.ToString(rx["LD_CLAVE2"]), Convert.ToString(rx["LD_CLAVE3"]), Convert.ToString(rx["LD_CLAVE4"]), Convert.ToString(rx["LD_BODEGA"]), Convert.ToInt32(rx["LD_ITMPAQ"])).Rows)
                            {
                                //Valida que no se consuma en items anteiores
                                foreach (DataRow rz in tbBalance.Rows)
                                {
                                    if (Convert.ToString(rw["BLCDLOTE"]) == "")
                                    {
                                        if ((Convert.ToString(rz["TP"]) == Convert.ToString(rw["BBTIPPRO"])) && (Convert.ToString(rz["C1"]) == Convert.ToString(rw["BBCLAVE1"]))
                                            && (Convert.ToString(rz["C2"]) == Convert.ToString(rw["BBCLAVE2"])) && (Convert.ToString(rz["C3"]) == Convert.ToString(rw["BBCLAVE3"]))
                                            && (Convert.ToString(rz["C4"]) == Convert.ToString(rw["BBCLAVE4"])))
                                            rw["BBCANTID"] = Convert.ToDouble(rw["BBCANTID"]) - Convert.ToDouble(rz["MBCANTID"]);
                                    }
                                    else
                                    {
                                        if ((Convert.ToString(rz["TP"]) == Convert.ToString(rw["BBTIPPRO"])) && (Convert.ToString(rz["C1"]) == Convert.ToString(rw["BBCLAVE1"]))
                                            && (Convert.ToString(rz["C2"]) == Convert.ToString(rw["BBCLAVE2"])) && (Convert.ToString(rz["C3"]) == Convert.ToString(rw["BBCLAVE3"]))
                                            && (Convert.ToString(rz["C4"]) == Convert.ToString(rw["BBCLAVE4"])) && (Convert.ToString(rz["MLCDLOTE"]) == Convert.ToString(rw["BLCDLOTE"])))
                                        {
                                            rw["BBCANTID"] = Convert.ToDouble(rw["BBCANTID"]) - Convert.ToDouble(rz["MLCANTID"]);
                                            rw["BLCANTID"] = Convert.ToDouble(rw["BLCANTID"]) - Convert.ToDouble(rz["MLCANTID"]);
                                        }
                                    }
                                }

                                if (ln_cantidad < Convert.ToDouble(rx["LD_CANTID"]))
                                {
                                    DataRow ry = tbBalance.NewRow();
                                    ry["TP"] = Convert.ToString(rw["BBTIPPRO"]);
                                    ry["C1"] = Convert.ToString(rw["BBCLAVE1"]);
                                    ry["C2"] = Convert.ToString(rw["BBCLAVE2"]);
                                    ry["C3"] = Convert.ToString(rw["BBCLAVE3"]);
                                    ry["C4"] = Convert.ToString(rw["BBCLAVE4"]);
                                    ry["MBBODEGA"] = Convert.ToString(rw["BBBODEGA"]);
                                    ry["IT"] = Convert.ToString(rw["IT"]);

                                    if (Convert.ToString(rw["BLCDLOTE"]) == "")
                                    {
                                        if (Convert.ToDouble(rw["BBCANTID"]) >= (Convert.ToDouble(rx["LD_CANTID"]) - ln_cantidad))
                                        {
                                            ry["MBCANTID"] = Convert.ToDouble(rx["LD_CANTID"])-ln_cantidad;
                                            ry["MLCDLOTE"] = "";
                                            ln_cantidad += Convert.ToDouble(rx["LD_CANTID"]);
                                        }
                                        else
                                        {
                                            ry["MBCANTID"] = Convert.ToDouble(rw["BBCANTID"])-ln_cantidad;
                                            ry["MLCDLOTE"] = "";
                                            ln_cantidad += Convert.ToDouble(rw["BBCANTID"]) - ln_cantidad;
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(rw["BLCANTID"]) >= (Convert.ToDouble(rx["LD_CANTID"])-ln_cantidad))
                                        {
                                            ry["MLCANTID"] = Convert.ToDouble(rx["LD_CANTID"]) -ln_cantidad;
                                            ry["MLCDLOTE"] = Convert.ToString(rw["BLCDLOTE"]);
                                            ln_cantidad += Convert.ToDouble(rx["LD_CANTID"]) - ln_cantidad;
                                        }
                                        else
                                        {
                                            ry["MLCANTID"] = Convert.ToDouble(rw["BLCANTID"]) -ln_cantidad;
                                            ry["MLCDLOTE"] = Convert.ToString(rw["BLCDLOTE"]);
                                            ln_cantidad += Convert.ToDouble(rw["BLCANTID"]) - ln_cantidad;
                                        }
                                    }
                                    tbBalance.Rows.Add(ry);
                                    ry = null;
                                }
                            }
                            ln_cantot = 0;
                            //Carga Cantidad a Principal
                            foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                            foreach (DataRow rz in tbBalance.Rows)
                            {
                                if (Convert.ToInt32(rz["IT"]) == ln_item)
                                {
                                    if (Convert.ToString(rz["MLCDLOTE"]) == "")
                                        ln_cantot += Convert.ToDouble(rz["MBCANTID"]);
                                    else
                                        ln_cantot += Convert.ToDouble(rz["MLCANTID"]);
                                }
                            }
                            rx["LD_CANCAN"] = Convert.ToDouble(rx["LD_CANCAN"]) - ln_cantot;
                        }
                        
                        (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                        litTextoMensaje.Text = "Seleccion Completa";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        
                    }
                    break;
                case "Anular Seleccion":
                    tbBalance.Clear();
                    break;
            }
        }
        protected void rlv_empaque_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            string lc_mensaje = "";
            Boolean lb_bandera = false;

            if (tbBalance.Rows.Count <= 0)
            {
                lb_bandera = true;
                lc_mensaje += "No Cuenta con Items de Detalle";
            }

            if (lb_bandera)
            {
                litTextoMensaje.Text = lc_mensaje;
                string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                e.Canceled = true;
            }
        }
        protected void txt_barras_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
            {
                ArticulosBL Obj = new ArticulosBL();
                DataTable tbBarras = new DataTable();
                try
                {
                    tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text, null, (rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue);
                    //tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text);
                    //foreach(DataRow rb in tbBarras.Rows)
                    //{
                    //    foreach (DataRow rw in tbItems.Rows)
                    //    {
                    //        if (Convert.ToString(rb["BTIPPRO"]) == Convert.ToString(rw["LD_TIPPRO"]) && Convert.ToString(rb["BCLAVE1"]) == Convert.ToString(rw["LD_CLAVE1"]) && Convert.ToString(rb["BCLAVE2"]) == Convert.ToString(rw["LD_CLAVE2"]) 
                    //            && Convert.ToString(rb["BCLAVE3"]) == Convert.ToString(rw["LD_CLAVE3"]) && Convert.ToDouble(rw["LD_CANCAN"])>0 && Convert.ToString(rb["BBBODEGA"]) == Convert.ToString(rw["LD_BODEGA"]))
                    //        {

                    //        } else
                    //        {

                    //        }
                    //    }
                    //}

                    if (tbBarras.Rows.Count > 0)
                    {
                        foreach (DataRow rw in tbBarras.Rows)
                        {
                            txt_tp.Text = Convert.ToString(rw["BTIPPRO"]);
                            txt_referencia.Text = Convert.ToString(rw["BCLAVE1"]);
                            txt_clave2.Text = Convert.ToString(rw["BCLAVE2"]);
                            txt_clave3.Text = Convert.ToString(rw["BCLAVE3"]);
                            txt_clave4.Text = Convert.ToString(rw["BCLAVE4"]);
                            txt_nc2.Text = Convert.ToString(rw["CLAVE2"]);
                            txt_nc3.Text = Convert.ToString(rw["CLAVE3"]);
                            txt_descripcion.Text = Convert.ToString(rw["ARNOMBRE"]);
                            txt_preciolta.Value = Convert.ToDouble(rw["PRECIO"]);

                            double ln_cantb = 0;
                            foreach (DataRow rx in tbBalance.Rows)
                            {
                                //if ((Convert.ToString(rx["LD_TIPPRO"]) == txt_tp.Text) && (Convert.ToString(rx["LD_CLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rx["LD_CLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rx["LD_CLAVE3"]) == txt_clave3.Text))
                                if ((Convert.ToString(rx["TP"]) == txt_tp.Text) && (Convert.ToString(rx["C1"]) == txt_referencia.Text) && (Convert.ToString(rx["C2"]) == txt_clave2.Text) && (Convert.ToString(rx["C3"]) == txt_clave3.Text))
                                    ln_cantb += Convert.ToDouble(rx["MBCANTID"]);
                            }
                            txt_caninv.Value = Convert.ToDouble(rw["BBCANTID"]) - ln_cantb;
                            txt_cantidad.Value = 1;
                        }
                        this.ConfigLinea();
                        if (!rc_lote.Visible)
                            btn_agregar_Aceptar(sender, e);
                        else
                        {
                            this.CargarLote();
                            btn_agregar_Aceptar(sender, e);
                        }
                        //mpAddArticulos.Show();
                        txt_barras.Focus();
                        this.Limpiar();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        litTextoMensaje.Text = "Codigo Barras Invalido!";
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Obj = null;
                    tbBarras = null;
                }
            }
        }
        private void ConfigLinea()
        {
            TipoProductosBL Obj = new TipoProductosBL();
            try
            {
                using (IDataReader reader = Obj.GetTipoProductoxBodegaR(null, Convert.ToString(Session["CODEMP"]), (rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, txt_tp.Text))
                {
                    while (reader.Read())
                    {
                        rc_lote.Visible = false;
                        lbl_lote.Visible = false;
                        RequiredFieldValidator11.Enabled = false;
                        if (Convert.ToString(reader["ABMNLOTE"]) == "S")
                        {
                            rc_lote.Visible = true;
                            lbl_lote.Visible = true;
                            RequiredFieldValidator11.Enabled = true;
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
                Obj = null;
            }
        }
        private void CargarLote()
        {
            RadComboBoxItem item_ = new RadComboBoxItem();
            MovimientosBL Obj = new MovimientosBL();
            try
            {
                rc_lote.Items.Clear();
                item_.Value = "-1";
                item_.Text = "Seleccionar";
                rc_lote.Items.Add(item_);
                foreach (DataRow rw in Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]), (rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["BLCDLOTE"]);
                    itemi.Text = "Lote: " + Convert.ToString(rw["BLCDLOTE"]) + " Can: " + Convert.ToString(rw["BLCANTID"]);
                    rc_lote.Items.Add(itemi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item_ = null;
                Obj = null;
            }
        }        
        private void Limpiar()
        {
            txt_tp.Text = "";
            txt_referencia.Text = "";
            txt_clave2.Text = "";
            txt_clave3.Text = "";
            txt_clave4.Text = "";
            txt_descripcion.Text = "";
            txt_barras.Text = "";
            txt_cantidad.Value = 0;
            txt_caninv.Value = 0;            
        }
        protected void btn_agregar_Aceptar(object sender, EventArgs e)
        {
            //Boolean lb_bandera = false,lb_bandetalle = false,lb_banderalote = false,lb_indinsert= false;
            //double ln_cantidad = 0, ln_candetalle = 0,ln_can = 0 ;
            //int ln_item = 0;
            Boolean lb_indfin = false, lb_indinsert= true,lb_supcantidad=false,lb_existeart=false,lb_cantdis=false;
            double ln_cantidad = 0;
            DataRow row = tbBalance.NewRow();
            MovimientosBL Obj = new MovimientosBL();
            ArticulosBL ObjA = new ArticulosBL();
            try {
                rqf_catidad1.Validate();
                //rqf_catidad2.Validate();
                rqf_referencia.Validate();
                //cmpNumbers.Validate();

                //if (rqf_catidad1.IsValid && rqf_catidad2.IsValid && cmpNumbers.IsValid && rqf_referencia.IsValid)
                //if (rqf_catidad1.IsValid && rqf_referencia.IsValid)
                if (rqf_referencia.IsValid)
                {
                    foreach (DataColumn cl in tbItems.Columns)
                        cl.ReadOnly = false;

                    foreach (DataRow rw in tbItems.Rows)
                    {
                            if ((Convert.ToString(rw["LD_TIPPRO"]) == txt_tp.Text) && (Convert.ToString(rw["LD_CLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rw["LD_CLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rw["LD_CLAVE3"]) == txt_clave3.Text))
                            {
                                lb_existeart = true;
                                lb_supcantidad = true;
                                if (Convert.ToDouble(rw["LD_CANCAN"]) > 0)
                                {
                                    foreach (DataRow rb in ObjA.GetTbBarrasInv(null, txt_barras.Text, null, Convert.ToString(rw["LD_BODEGA"])).Rows)
                                        ln_cantidad = Convert.ToDouble(rb["BBCANTID"]) - (Convert.ToDouble(rw["LD_CANTID"]) - Convert.ToDouble(rw["LD_CANCAN"]));

                                    if (ln_cantidad > 0)
                                    {
                                        row["TP"] = txt_tp.Text;
                                        row["C1"] = txt_referencia.Text;
                                        row["C2"] = txt_clave2.Text;
                                        row["C3"] = txt_clave3.Text;
                                        row["C4"] = txt_clave4.Text;
                                        row["MBBODEGA"] = rw["LD_BODEGA"];

                                        row["IT"] = Convert.ToInt32(rw["LD_ITMPAQ"]);

                                        lb_indinsert = true;
                                        foreach (DataRow ry in tbBalance.Rows)
                                        {
                                            //if (Convert.ToString(ry["TP"]) == txt_tp.Text && Convert.ToString(ry["C1"]) == txt_referencia.Text && Convert.ToString(ry["C2"]) == txt_clave2.Text && Convert.ToString(ry["C3"]) == txt_clave3.Text && Convert.ToString(ry["C4"]) == txt_clave4.Text)
                                            if (Convert.ToInt32(rw["LD_ITMPAQ"]) == Convert.ToInt32(ry["IT"]))
                                            {
                                                ry["MBCANTID"] = Convert.ToDouble(ry["MBCANTID"]) + 1;
                                                lb_indinsert = false;
                                            }
                                        }
                                        if (lb_indinsert)
                                        {
                                            row["MBCANTID"] = 1;
                                            tbBalance.Rows.Add(row);
                                        }

                                        rw["LD_CANCAN"] = Convert.ToInt32(rw["LD_CANCAN"]) - 1;

                                        //Cajas                             
                                        DataRow rx = tbCajas.NewRow();
                                        rx["LD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                        rx["LH_LSTPAQ"] = 0;
                                        rx["LD_ITMPAQ"] = Convert.ToInt32(rw["LD_ITMPAQ"]);
                                        rx["CL_CAJA"] = rc_cajam.SelectedValue;
                                        rx["CL_CANTIDAD"] = 1;
                                        rx["TP"] = row["TP"];
                                        rx["C1"] = row["C1"];
                                        rx["C2"] = row["C2"];
                                        rx["C3"] = row["C3"];
                                        rx["C4"] = row["C4"];
                                        rx["MBBODEGA"] = row["MBBODEGA"];
                                        rx["MLCDLOTE"] = row["MLCDLOTE"];
                                        if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])) || (Convert.ToString(row["MLCDLOTE"]) == "&nbsp;"))
                                            rx["MLCDLOTE"] = "";
                                        tbCajas.Rows.Add(rx);
                                        rx = null;

                                        lb_indfin = true;
                                        lb_supcantidad = false;

                                    }
                                    else
                                    {
                                        lb_cantdis = true;
                                    }
                                    
                                }
                            }

                        //rompe ciclo al encontrar
                        if (lb_indfin)
                            break;
                    }

                    if (lb_supcantidad)
                    {
                        litTextoMensaje.Text = "Cantidad Supera la Solicitada";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }

                    if (lb_cantdis)
                    {
                        litTextoMensaje.Text = "Cantidad No Disponible Almacen";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }

                    if (!lb_existeart)
                    {
                        litTextoMensaje.Text = "No Existe Referencia en el Packing";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }


                    (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();


                    //if (txt_caninv.Value >= txt_cantidad.Value)
                    //{
                    //    foreach (DataRow rw in tbItems.Rows)
                    //    {
                    //        if ((Convert.ToString(rw["LD_TIPPRO"]) == txt_tp.Text) && (Convert.ToString(rw["LD_CLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rw["LD_CLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rw["LD_CLAVE3"]) == txt_clave3.Text))
                    //        {
                    //            lb_bandera = true;
                    //            ln_cantidad += Convert.ToDouble(rw["LD_CANTID"]);
                    //        }
                    //    }
                    //    if (lb_bandera)
                    //    {
                    //        //Recupera la cantidad del recepcionado.
                    //        foreach (DataRow rx in tbBalance.Rows)
                    //        {
                    //            if ((Convert.ToString(rx["TP"]) == txt_tp.Text) && (Convert.ToString(rx["C1"]) == txt_referencia.Text) && (Convert.ToString(rx["C2"]) == txt_clave2.Text) && (Convert.ToString(rx["C3"]) == txt_clave3.Text))
                    //            {
                    //                if (rc_lote.Visible)
                    //                    ln_candetalle += Convert.ToDouble(rx["MLCANTID"]);
                    //                else
                    //                    ln_candetalle += Convert.ToDouble(rx["MBCANTID"]);
                    //            }
                    //        }

                    //        if (ln_cantidad > ln_candetalle)
                    //            lb_bandetalle = true;

                    //        if (lb_bandetalle)
                    //        {
                    //            row["TP"] = txt_tp.Text;
                    //            row["C1"] = txt_referencia.Text;
                    //            row["C2"] = txt_clave2.Text;
                    //            row["C3"] = txt_clave3.Text;
                    //            row["C4"] = txt_clave4.Text;                                

                    //            //Busca Item para Asociar
                    //            foreach (DataRow rw in tbItems.Rows)
                    //            {
                    //                if ((Convert.ToString(rw["LD_TIPPRO"]) == txt_tp.Text) && (Convert.ToString(rw["LD_CLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rw["LD_CLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rw["LD_CLAVE3"]) == txt_clave3.Text))
                    //                {
                    //                    row["MBBODEGA"] = rw["LD_BODEGA"];
                    //                    ln_can = 0;
                    //                    foreach (DataRow ry in tbBalance.Rows)
                    //                    {
                    //                        if (Convert.ToInt32(ry["IT"]) == Convert.ToInt32(rw["LD_ITMPAQ"]))
                    //                        {
                    //                            if (rc_lote.Visible)
                    //                                ln_can += Convert.ToDouble(ry["MLCANTID"]);
                    //                            else
                    //                                ln_can += Convert.ToDouble(ry["MBCANTID"]);
                    //                        }
                    //                    }
                    //                    if (ln_can < Convert.ToDouble(rw["LD_CANTID"]))
                    //                    {
                    //                        ln_item = Convert.ToInt32(rw["LD_ITMPAQ"]);
                    //                        break;
                    //                    }
                    //                }
                    //            }

                    //            row["IT"] = ln_item;
                    //            if (rc_lote.Visible)
                    //            {
                    //                row["MLCANTID"] = 1;
                    //                //  Asigna Lote
                    //                foreach (DataRow rz in Obj.GetLotes(null, Convert.ToString(Session["CODEMP"]), (rlv_empaque.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, txt_tp.Text, txt_referencia.Text, txt_clave2.Text, txt_clave3.Text, txt_clave4.Text, ".").Rows)
                    //                {
                    //                    //Cantidad del Lote x Referencia
                    //                    foreach (DataRow ry in tbBalance.Rows)
                    //                    {
                    //                        if (Convert.ToString(ry["TP"]) == txt_tp.Text && Convert.ToString(ry["C1"]) == txt_referencia.Text && Convert.ToString(ry["C2"]) == txt_clave2.Text && Convert.ToString(ry["C3"]) == txt_clave3.Text && Convert.ToString(ry["C4"]) == txt_clave4.Text && Convert.ToString(ry["MLCDLOTE"]) == rc_lote.SelectedValue)
                    //                        {
                    //                            ln_can += Convert.ToDouble(ry["MLCANTID"]);
                    //                        }
                    //                    }
                    //                    if ((Convert.ToDouble(rz["BLCANTID"]) - (ln_can + 1)) >= 1)
                    //                    {
                    //                        lb_banderalote = true;
                    //                        row["MLCDLOTE"] = rz["BLCDLOTE"];
                    //                        break;
                    //                    }

                    //                }
                    //                if (!lb_banderalote)
                    //                {
                    //                    litTextoMensaje.Text = "Cantidad Supera la Solicitada Lotes!";
                    //                    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    //                }
                    //                else
                    //                {
                    //                    lb_indinsert = true;
                    //                    foreach (DataRow ry in tbBalance.Rows)
                    //                    {
                    //                        if (Convert.ToString(ry["TP"]) == txt_tp.Text && Convert.ToString(ry["C1"]) == txt_referencia.Text && Convert.ToString(ry["C2"]) == txt_clave2.Text && Convert.ToString(ry["C3"]) == txt_clave3.Text && Convert.ToString(ry["C4"]) == txt_clave4.Text && Convert.ToString(ry["MLCDLOTE"]) == rc_lote.SelectedValue)
                    //                        {
                    //                            ry["MLCANTID"] = Convert.ToDouble(ry["MLCANTID"]) + Convert.ToDouble(1);
                    //                            lb_indinsert = false;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                lb_indinsert = true;
                    //                foreach (DataRow ry in tbBalance.Rows)
                    //                {
                    //                    //if (Convert.ToString(ry["TP"]) == txt_tp.Text && Convert.ToString(ry["C1"]) == txt_referencia.Text && Convert.ToString(ry["C2"]) == txt_clave2.Text && Convert.ToString(ry["C3"]) == txt_clave3.Text && Convert.ToString(ry["C4"]) == txt_clave4.Text)
                    //                    if (Convert.ToInt32(ln_item) == Convert.ToInt32(ry["IT"]))
                    //                    {
                    //                        ry["MBCANTID"] = Convert.ToDouble(ry["MBCANTID"]) + 1;
                    //                        lb_indinsert = false;
                    //                    }
                    //                }
                    //            }

                    //            if (lb_indinsert)
                    //            {
                    //                row["MBCANTID"] = 1;
                    //                tbBalance.Rows.Add(row);
                    //            }

                    //            //Cajas                             
                    //            DataRow rx = tbCajas.NewRow();
                    //            rx["LD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                    //            rx["LH_LSTPAQ"] = 0;
                    //            rx["LD_ITMPAQ"] = ln_item;
                    //            rx["CL_CAJA"] = rc_cajam.SelectedValue;
                    //            rx["CL_CANTIDAD"] = 1;
                    //            rx["TP"] = row["TP"];
                    //            rx["C1"] = row["C1"];
                    //            rx["C2"] = row["C2"];
                    //            rx["C3"] = row["C3"];
                    //            rx["C4"] = row["C4"];
                    //            rx["MBBODEGA"] = row["MBBODEGA"];
                    //            rx["MLCDLOTE"] = row["MLCDLOTE"];
                    //            if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])) || (Convert.ToString(row["MLCDLOTE"]) == "&nbsp;"))
                    //                rx["MLCDLOTE"] = "";
                    //            tbCajas.Rows.Add(rx);
                    //            rx = null;

                    //            //Carga Cantidad a Principal
                    //            foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                    //            foreach (DataRow rw in tbItems.Rows)
                    //            {
                    //                if (Convert.ToInt32(rw["LD_ITMPAQ"]) == ln_item)
                    //                    rw["LD_CANCAN"] = Convert.ToInt32(rw["LD_CANCAN"]) - 1;
                    //            }

                    //            (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //            (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    //        }
                    //        else
                    //        {
                    //            litTextoMensaje.Text = "Cantidad Supera la Solicitada";
                    //            string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        litTextoMensaje.Text = "No Existe Referencia en el Packing";
                    //        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    //    }
                    //}
                    //else
                    //{
                    //    litTextoMensaje.Text = "Cantidad Insuficiente!";
                    //    string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
                Obj = null;
                ObjA = null;
            }
        }
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_PreRender(object sender, EventArgs e)
        {
            double aValue = 0;

            foreach (GridDataItem item in (sender as RadGrid).Items)
            {
                if (item.OwnerTableView.Name != "detalle_item" && item.OwnerTableView.Name != "detalle_caja" && item.OwnerTableView.Name != "detalle_insert" && item.OwnerTableView.Name != "detalle_caja_insert")
                {
                    aValue = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_precio")).Value);
                    if (aValue == 0)
                    {
                        item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                        item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                        item.ControlStyle.Font.Bold = true;
                    }
                }

            }
        }
        protected void rg_resumebox_DeleteCommand(object sender, GridCommandEventArgs e)
        {            
            GridEditableItem item = e.Item as GridEditableItem;
            try
            {
                string CJ_ = (e.Item as GridEditableItem).GetDataKeyValue("caja").ToString();
                Boolean lb_ind = true;
                int i = 0;
                while (lb_ind)
                {
                    if (tbCajas.Rows.Count == 0)
                        lb_ind = false;

                    i = 0;
                    foreach (DataRow rw in tbCajas.Rows)
                    {
                        if (Convert.ToInt32(rw["CL_CAJA"]) == Convert.ToInt32(CJ_))
                        {
                            foreach (DataRow row in tbBalance.Rows)
                            {
                                if ((Convert.ToString(row["TP"]) == Convert.ToString(rw["TP"])) && (Convert.ToString(row["C1"]) == Convert.ToString(rw["C1"])) && (Convert.ToString(row["C2"]) == Convert.ToString(rw["C2"])) && (Convert.ToString(row["C3"]) == Convert.ToString(rw["C3"]))
                                    && (Convert.ToString(row["C4"]) == Convert.ToString(rw["C4"])) && (Convert.ToString(row["MBBODEGA"]) == Convert.ToString(rw["MBBODEGA"])) && (Convert.ToString(row["MLCDLOTE"]) == Convert.ToString(rw["MLCDLOTE"])))
                                {
                                    //itm_ = Convert.ToInt32(row["IT"]);
                                    if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                                        row["MBCANTID"] = Convert.ToDouble(row["MBCANTID"]) - Convert.ToDouble(rw["CL_CANTIDAD"]);
                                    else
                                        row["MLCANTID"] = Convert.ToDouble(row["MLCANTID"]) - Convert.ToDouble(rw["CL_CANTIDAD"]);
                                    break;
                                }
                            }

                            tbCajas.Rows.Remove(rw);
                            break;
                        }

                        i++;
                        if (tbCajas.Rows.Count == i)
                            lb_ind = false;
                    }
                }                

                // 0
                lb_ind = true;
                i = 0;
                while (lb_ind)
                {
                    if (tbBalance.Rows.Count == 0)
                        lb_ind = false;

                    i = 0;
                    foreach (DataRow row in tbBalance.Rows)
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                        {
                            if (Convert.ToDouble(row["MBCANTID"]) == 0)
                            {
                                tbBalance.Rows.Remove(row);                                
                                break;
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(row["MBCANTID"]) == 0 && Convert.ToDouble(row["MLCANTID"]) == 0)
                            {                                
                                tbBalance.Rows.Remove(row);
                                break;
                            }
                        }

                        i++;
                        if (tbBalance.Rows.Count == i)
                            lb_ind = false;
                    }
                }

                double ln_cantidad = 0; 
                foreach (DataRow rw in tbItems.Rows)
                {
                    ln_cantidad = 0;
                    foreach (DataRow row in tbBalance.Rows)
                    {
                        if (Convert.ToInt32(row["IT"]) == Convert.ToInt32(rw["LD_ITMPAQ"]) )
                        {
                            //itm_ = Convert.ToInt32(row["IT"]);
                            if (string.IsNullOrEmpty(Convert.ToString(row["MLCDLOTE"])))
                                ln_cantidad += Convert.ToDouble(row["MBCANTID"]);
                            else
                                ln_cantidad += Convert.ToDouble(row["MLCANTID"]);                            
                        }
                    }
                    rw["LD_CANCAN"] = Convert.ToDouble(rw["LD_CANTID"]) - ln_cantidad;
                }

                int ln_cant = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("caja", typeof(Int32));
                dt.Columns.Add("cantidad", typeof(Int32));

                //while (i <= rc_cajam.Items.Count)
                foreach (RadComboBoxItem it in rc_cajam.Items)
                {
                    ln_cant = 0;
                    foreach (DataRow itm in tbCajas.Rows)
                    {
                        if (Convert.ToInt32(it.Value) == Convert.ToInt32(itm["CL_CAJA"]))
                            ln_cant += Convert.ToInt32(itm["CL_CANTIDAD"]);
                    }

                    DataRow rx = dt.NewRow();
                    //rx["caja"] = ln_caja;
                    rx["caja"] = it.Value;
                    rx["cantidad"] = ln_cant;
                    dt.Rows.Add(rx);
                    rx = null;
                }
                rg_resumebox.DataSource = dt;
                rg_resumebox.DataBind();
                dt = null;

                (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_empaque.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                string script = "function f(){$find(\"" + mpResumeBox.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void rg_resumebox_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detalle_caja":
                    {
                        int ln_caja = Convert.ToInt32(dataItem.GetDataKeyValue("caja").ToString());

                        DataTable dt_ = new DataTable();

                        dt_.Columns.Add("C1", typeof(string));
                        dt_.Columns.Add("NOMBRE", typeof(string));
                        dt_.Columns.Add("CAN", typeof(double));
                        foreach (DataRow rw in tbCajas.Rows)
                        {
                            if (ln_caja == Convert.ToInt32(rw["CL_CAJA"]))
                            {
                                DataRow rt = dt_.NewRow();
                                rt["C1"] = rw["C1"];
                                foreach (DataRow rz in tbItems.Rows)
                                {
                                    if (Convert.ToString(rw["C1"]) == Convert.ToString(rz["LD_CLAVE1"]))
                                        rt["NOMBRE"] = rz["ARNOMBRE"];
                                }
                                rt["CAN"] = rw["CL_CANTIDAD"];                                
                                dt_.Rows.Add(rt);
                                rt = null;
                            }
                        }

                        e.DetailTableView.DataSource = dt_;
                        break;
                    }
            }

            string script = "function f(){$find(\"" + mpResumeBox.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }                
        protected void rg_anexos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "NewPhoto")
            {
                txt_observaciones.Text = "";
                myIframe.Attributes["src"] = "//" + HttpContext.Current.Request.Url.Authority + "/webcam.aspx";
                string script = "function f(){$find(\"" + mp_cam.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            if (e.CommandName == "attach")
            {
                txt_obsfoto.Text = "";
                string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["EV_CODIGO"].Text);

                LtaEmpaqueBL Obj = new LtaEmpaqueBL();
                foreach (DataRow rw in (Obj.GetEvidenciasFoto(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["EV_FOTO"]);                    
                }
                Obj = null;
                ditem = null;
                Random random = new Random();
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + ".png";
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
        protected void rg_anexos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbAnexos;
        }
        protected void btnSaveAnexo_Click(object sender, EventArgs e)
        {
            LtaEmpaqueBL Obj = new LtaEmpaqueBL();
            try {
                if (rlv_empaque.InsertItem != null)
                {
                    tbAnexos.Columns["ruta"].ReadOnly = false;
                    DataRow rw = tbAnexos.NewRow();
                    rw["EV_CODIGO"] = 0;
                    rw["LH_CODEMP"] = "001";
                    rw["LH_LSTPAQ"] = 0;
                    rw["EV_DESCRIPCION"] = txt_observaciones.Text;
                    rw["EV_FECING"] = System.DateTime.Today;
                    rw["EV_USUARIO"] = ".";
                    rw["ruta"] = Convert.ToString(Session["imagePath"]);
                    tbAnexos.Rows.Add(rw);
                    rw = null;
                    (rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    (rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataBind();
                }
                else {
                    if (Convert.ToString(Session["imagePath"]) != "")
                    {
                        Obj.InsertEvidencia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_empaque.Items[0].FindControl("txt_lstpaq") as RadTextBox).Text),
                            txt_observaciones.Text, Convert.ToString(Session["imagePath"]), Convert.ToString(Session["UserLogon"]));

                        tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString()));
                        (rlv_empaque.Items[0].FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                        (rlv_empaque.Items[0].FindControl("rg_anexos") as RadGrid).DataBind();

                    }
                    else
                    {
                        litTextoMensaje.Text = "No Se Capturo Foto!";
                        string script = "function f(){$find(\"" + mpMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
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
        protected void lnk_pedido_Click(object sender, EventArgs e)
        {
            string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Pedidos/Pedidos.aspx?Pedido=" + (sender as LinkButton).Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_procesar_Click(object sender, EventArgs e)
        {
            LtaEmpaqueBL Obj = new LtaEmpaqueBL();
            try
            {
                if (rlv_empaque.InsertItem != null)
                {
                    tbAnexos.Columns["ruta"].ReadOnly = false;
                    DataRow rw = tbAnexos.NewRow();
                    rw["EV_CODIGO"] = 0;
                    rw["LH_CODEMP"] = "001";
                    rw["LH_LSTPAQ"] = 0;
                    rw["EV_DESCRIPCION"] = txt_obsfoto.Text;
                    rw["EV_FECING"] = System.DateTime.Today;
                    rw["EV_USUARIO"] = ".";
                    rw["ruta"] = prArchivo;
                    tbAnexos.Rows.Add(rw);
                    rw = null;
                    (rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    (rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataBind();
                }
                else
                {                    
                   Obj.InsertEvidencia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_empaque.Items[0].FindControl("txt_lstpaq") as RadTextBox).Text),
                            txt_obsfoto.Text, prArchivo, Convert.ToString(Session["UserLogon"]));

                   tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_empaque.Items[0].GetDataKeyValue("LH_LSTPAQ").ToString()));
                   (rlv_empaque.Items[0].FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                   (rlv_empaque.Items[0].FindControl("rg_anexos") as RadGrid).DataBind();                    
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
        protected void rauCargar_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
    }
}