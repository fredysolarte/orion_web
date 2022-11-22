using System;
using System.Collections.Generic;
using System.Data;
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
using Ionic.Zip;
using System.IO;

namespace XUSS.WEB.Compras
{
    public partial class SegregacionV2 : System.Web.UI.Page
    {
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        public DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        public DataTable tbDefinitivo
        {
            set { ViewState["tbDefinitivo"] = value; }
            get { return ViewState["tbDefinitivo"] as DataTable; }
        }
        public DataTable tbProformas
        {
            set { ViewState["tbProformas"] = value; }
            get { return ViewState["tbProformas"] as DataTable; }
        }
        public DataTable tbBodegas
        {
            set { ViewState["tbBodegas"] = value; }
            get { return ViewState["tbBodegas"] as DataTable; }
        }
        public string gc_tipo // 1 - Editar 2- Aprobar 3- Proforma 4- Facturas
        {
            set { ViewState["gc_tipo"] = value; }
            get { return Convert.ToString(ViewState["gc_tipo"]) ; }
        }
        public string gc_facpro // 1 - Proforma 2- Factura
        {
            set { ViewState["gc_tipo"] = value; }
            get { return Convert.ToString(ViewState["gc_tipo"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Segregacion"])))
                {
                    obj_segregacion.SelectParameters["filter"].DefaultValue = "SGH_CODIGO =" + Convert.ToString(Request.QueryString["Segregacion"]);
                    rlv_segregacion.DataBind();
                }
            }
        }
        protected void AnalizarCommand(string comando)
        {
            if (comando.Equals("Cancel"))
                ViewState["toolbars"] = true;
            else
                ViewState["toolbars"] = false;
        }
        private string gc_indtercero
        {
            set { ViewState["gc_indtercero"] = value; }
            get { return Convert.ToString(ViewState["gc_indtercero"]); }
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
            this.OcultarPaginador(rlv_segregacion, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_segregacion_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.EditItem)
            {
                switch (gc_tipo)
                {
                    case "1":
                        (e.Item.FindControl("txt_fecproforma") as RadDatePicker).Enabled = false;
                        (e.Item.FindControl("txt_proforma") as RadTextBox).Enabled = false;
                        (e.Item.FindControl("ibtn_vendedor") as ImageButton).Enabled = false;
                        (e.Item.FindControl("ibtn_comprador") as ImageButton).Enabled = false;
                        (e.Item.FindControl("txt_observaciones") as RadTextBox).Enabled = false;

                        break;
                    case "2":
                        (e.Item.FindControl("txt_fecproforma") as RadDatePicker).Enabled = false;
                        (e.Item.FindControl("txt_proforma") as RadTextBox).Enabled = false;
                        (e.Item.FindControl("ibtn_vendedor") as ImageButton).Enabled = false;
                        (e.Item.FindControl("ibtn_comprador") as ImageButton).Enabled = false;
                        (e.Item.FindControl("txt_observaciones") as RadTextBox).Enabled = false;
                        
                        break;
                    case "3":
                        (e.Item.FindControl("txt_fecproforma") as RadDatePicker).Enabled = true;
                        (e.Item.FindControl("txt_proforma") as RadTextBox).Enabled = true;
                        (e.Item.FindControl("ibtn_vendedor") as ImageButton).Enabled = true;
                        (e.Item.FindControl("ibtn_comprador") as ImageButton).Enabled = true;
                        (e.Item.FindControl("txt_observaciones") as RadTextBox).Enabled = true;

                        break;
                }
            }
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
                    SegregacionBL Obj = new SegregacionBL();
                    GridBoundColumn boundColumn;
                    try {
                        tbItems = Obj.GetSegregacionProforma(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_segregacion.Items[0].GetDataKeyValue("SGH_CODIGO").ToString()),Convert.ToString((rlv_segregacion.Items[0].FindControl("rc_origen") as RadComboBox).SelectedValue));
                        foreach (DataRow rw in ((DataTable)Obj.GetSegregacionProformasBodegasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_segregacion.Items[0].GetDataKeyValue("SGH_CODIGO").ToString()))).Rows)
                        {
                            tbItems.Columns.Add(Convert.ToString(rw["SGD_BODEGA"]), typeof(double));

                            boundColumn = new GridBoundColumn();
                            boundColumn.UniqueName = Convert.ToString(rw["SGD_BODEGA"]);
                            boundColumn.DataField = Convert.ToString(rw["SGD_BODEGA"]);
                            boundColumn.HeaderText = Convert.ToString(rw["BDNOMBRE"]);
                            boundColumn.HeaderStyle.Width = 120;
                            boundColumn.ColumnGroupName = Convert.ToString(rw["SGD_GRUPO"]);
                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            (e.Item.FindControl("rg_proforma") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                            foreach (DataRow rt in tbItems.Rows)
                            {
                                if (Convert.ToString(rt["SGD_GRUPO"]) == "gp_detalle")
                                {
                                    foreach (DataRow rz in tbItems.Rows)
                                    {
                                        if (Convert.ToString(rz["SGD_BODEGA"]) == Convert.ToString(rw["SGD_BODEGA"]) && Convert.ToString(rz["PR_NROCMP"]) == Convert.ToString(rt["PR_NROCMP"]) && Convert.ToInt32(rz["PR_NROITEM"]) == Convert.ToInt32(rt["PR_NROITEM"]))
                                        {
                                            rt[Convert.ToString(rw["SGD_BODEGA"])] = Convert.ToDouble(rz["SGD_CANTIDAD"]);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        //Borrar Grupos detallados
                        foreach (DataRow rt in tbItems.Rows)
                        {
                            if (Convert.ToString(rt["SGD_GRUPO"]) != "gp_detalle")
                                rt.Delete();
                        }

                        tbItems.AcceptChanges();

                        (e.Item.FindControl("rg_proforma") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_proforma") as RadGrid).DataBind();

                        //Cargar Proformas
                        tbProformas = new DataTable();
                        //tbProformas.Rows.Clear();
                        //tbProformas.Columns.Clear();
                        tbProformas.Columns.Add("NroProforma", typeof(string));
                        tbProformas.Columns.Add("TipDoc", typeof(string));

                        foreach (DataRow rw in tbItems.Rows)
                        {
                            Boolean lb_ind = false;
                            string lc_proforma = Convert.ToString(rw["PR_NROFACPROFORMA"]);
                            foreach (DataRow ry in tbProformas.Rows)
                            {
                                if (Convert.ToString(ry["NroProforma"]) == Convert.ToString(rw["PR_NROFACPROFORMA"]))
                                    lb_ind = true;
                            }

                            if (!lb_ind)
                            {
                                DataRow row = tbProformas.NewRow();
                                row["NroProforma"] = lc_proforma;
                                row["TipDoc"] = "PR";
                                tbProformas.Rows.Add(row);
                                row = null;
                            }
                        }
                        //(e.Item.FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                        //(e.Item.FindControl("rg_ordenes") as RadGrid).DataBind();

                        GridSortExpression expression = new GridSortExpression();
                        expression.FieldName = "PR_NROFACPROFORMA";
                        expression.SortOrder = GridSortOrder.Ascending;
                        (e.Item.FindControl("rg_proforma") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression);
                        (e.Item.FindControl("rg_proforma") as RadGrid).MasterTableView.Rebind();


                        GridSortExpression expression_ = new GridSortExpression();
                        expression_.FieldName = "PR_NROITEM";
                        expression_.SortOrder = GridSortOrder.Ascending;
                        (e.Item.FindControl("rg_proforma") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression_);
                        (e.Item.FindControl("rg_proforma") as RadGrid).MasterTableView.Rebind();


                        (e.Item.FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                        (e.Item.FindControl("rg_ordenes") as RadGrid).DataBind();

                        //***************************************************************************************************************************Facturas******************************************************************
                        tbDefinitivo = Obj.GetSegregacionFactura(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_segregacion.Items[0].GetDataKeyValue("SGH_CODIGO").ToString()), Convert.ToString((rlv_segregacion.Items[0].FindControl("rc_origen") as RadComboBox).SelectedValue));
                        foreach (DataRow rw in ((DataTable)Obj.GetSegregacionProformasBodegasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_segregacion.Items[0].GetDataKeyValue("SGH_CODIGO").ToString()))).Rows)
                        {
                            tbDefinitivo.Columns.Add(Convert.ToString(rw["SGD_BODEGA"]), typeof(double));

                            boundColumn = new GridBoundColumn();
                            boundColumn.UniqueName = Convert.ToString(rw["SGD_BODEGA"]);
                            boundColumn.DataField = Convert.ToString(rw["SGD_BODEGA"]);
                            boundColumn.HeaderText = Convert.ToString(rw["BDNOMBRE"]);
                            boundColumn.HeaderStyle.Width = 120;
                            boundColumn.ColumnGroupName = Convert.ToString(rw["SGD_GRUPO"]);
                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            (e.Item.FindControl("rg_facturas") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                            foreach (DataRow rt in tbDefinitivo.Rows)
                            {
                                if (Convert.ToString(rt["SGD_GRUPO"]) == "gp_detalle")
                                {
                                    foreach (DataRow rz in tbDefinitivo.Rows)
                                    {
                                        if (Convert.ToString(rz["SGD_BODEGA"]) == Convert.ToString(rw["SGD_BODEGA"]) && Convert.ToString(rz["PR_NROCMP"]) == Convert.ToString(rt["PR_NROCMP"]) && Convert.ToInt32(rz["PR_NROITEM"]) == Convert.ToInt32(rt["PR_NROITEM"]))
                                        {
                                            rt[Convert.ToString(rw["SGD_BODEGA"])] = Convert.ToDouble(rz["SGD_CANTIDAD"]);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        //Borrar Grupos detallados
                        foreach (DataRow rt in tbDefinitivo.Rows)
                        {
                            if (Convert.ToString(rt["SGD_GRUPO"]) != "gp_detalle")
                                rt.Delete();
                        }

                        tbDefinitivo.AcceptChanges();

                        (e.Item.FindControl("rg_facturas") as RadGrid).DataSource = tbDefinitivo;
                        (e.Item.FindControl("rg_facturas") as RadGrid).DataBind();

                        foreach (DataRow rw in tbDefinitivo.Rows)
                        {
                            Boolean lb_ind = false;
                            string lc_proforma = Convert.ToString(rw["PR_NROFACPROFORMA"]);
                            foreach (DataRow ry in tbProformas.Rows)
                            {
                                if (Convert.ToString(ry["NroProforma"]) == Convert.ToString(rw["PR_NROFACPROFORMA"]))
                                    lb_ind = true;
                            }

                            if (!lb_ind)
                            {
                                DataRow row = tbProformas.NewRow();
                                row["NroProforma"] = lc_proforma;
                                row["TipDoc"] = "FA";
                                tbProformas.Rows.Add(row);
                                row = null;
                            }
                        }
                        
                        GridSortExpression expressiond = new GridSortExpression();
                        expressiond.FieldName = "PR_NROFACPROFORMA";
                        expressiond.SortOrder = GridSortOrder.Ascending;
                        (e.Item.FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expressiond);
                        (e.Item.FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();


                        GridSortExpression expressiond_ = new GridSortExpression();
                        expressiond_.FieldName = "PR_NROITEM";
                        expressiond_.SortOrder = GridSortOrder.Ascending;
                        (e.Item.FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expressiond_);
                        (e.Item.FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();


                        (e.Item.FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                        (e.Item.FindControl("rg_ordenes") as RadGrid).DataBind();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        boundColumn = null;
                    }
                }
            }
            
        }
        protected void rlv_segregacion_ItemInserting(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_proforma") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                string lc_factura = Convert.ToString((item.FindControl("lbl_proforma") as Label).Text);
                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToString(rw["PR_NROFACPROFORMA"]) == lc_factura && Convert.ToInt32(rw["PR_NROITEM"]) == ln_codigo)
                    {
                        rw["SGD_CANTIDAD"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_cmp")).Value);
                        rw["SGD_PRECIO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_precio_pro")).Value);
                    }
                }                
            }

            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_bodegas") as RadGrid).Items)
            {
                foreach (DataRow rw in tbBodegas.Rows)
                {
                    if (item["BDBODEGA"].Text == Convert.ToString(rw["BDBODEGA"]))                    
                        rw["Tipo"] = ((RadComboBox)item.FindControl("rc_tipo")).SelectedValue;
                }
            }

            obj_segregacion.InsertParameters["SGH_PARCIAL"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_segParcial") as CheckBox).Checked)
                obj_segregacion.InsertParameters["SGH_PARCIAL"].DefaultValue = "S";
        }
        protected void rlv_segregacion_ItemInserted(object sender, Telerik.Web.UI.RadListViewInsertedEventArgs e)
        {

        }
        protected void rlv_segregacion_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    SegregacionBL Obj = new SegregacionBL();
                    BodegaBL ObjB = new BodegaBL();
                    try
                    {
                        tbItems = Obj.GetSegregacionDT(null, Convert.ToString(Session["CODEMP"]), 0);                        
                        tbBodegas = ObjB.GetBodegas(null, "", 0, 0);
                        tbProformas = tbBodegas;
                        tbBodegas.Columns.Add("Tipo", typeof(int));
                        foreach (DataColumn cl in tbBodegas.Columns)
                            cl.ReadOnly = false;
                        foreach (DataRow rw in tbBodegas.Rows)
                        {
                            if (Convert.ToString(rw["BDDTTEC1"]) != "")
                                rw["Tipo"] = Convert.ToString(rw["BDDTTEC1"]);
                            else
                                rw["Tipo"] = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        ObjB = null;
                    }
                    break;

                case "Buscar":
                    obj_segregacion.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_segregacion.DataBind();
                    break;
                case "Edit":
                    BodegaBL ObjB_ = new BodegaBL();
                    try
                    {
                        tbBodegas = ObjB_.GetBodegas(null, "", 0, 0);
                        tbBodegas.Columns.Add("Tipo", typeof(int));
                        foreach (DataColumn cl in tbBodegas.Columns)
                            cl.ReadOnly = false;
                        foreach (DataRow rw in tbBodegas.Rows)
                        {
                            if (Convert.ToString(rw["BDDTTEC1"]) != "")
                                rw["Tipo"] = Convert.ToString(rw["BDDTTEC1"]);
                            else
                                rw["Tipo"] = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ObjB_ = null;
                    }
                    break;
                case "Delete":
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rg_bodegas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbBodegas;
        }
        protected void btn_calcular_Click(object sender, EventArgs e)
        {
            string[] cadena = (((RadButton)sender).Parent.FindControl("txt_proformas") as RadTextBox).Text.Split(',');
            string lc_filtro = "",lc_cadena="";
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            ArticulosBL ObjA = new ArticulosBL();
            GridBoundColumn boundColumn;
            ComunBL objt = new ComunBL();
            DataTable tbgrupos = new DataTable();

            try {
                foreach (string word in cadena)
                    lc_filtro += "'" + word + "',";
                lc_filtro = lc_filtro.Substring(0, lc_filtro.Length - 1);

                switch (Convert.ToString((((RadButton)sender).Parent.FindControl("rc_origen") as RadComboBox).SelectedValue))
                {
                    case "PR":
                        lc_cadena = " AND ARTIPPRO + ARCLAVE1 IN(SELECT PR_TIPPRO + PR_CLAVE1 FROM CMP_PROFACTURADT WITH(NOLOCK) WHERE PR_NROFACPROFORMA  IN(";                        
                        tbItems = Obj.GetProformas(null, Convert.ToString(Session["CODEMP"]), lc_filtro);
                        break;
                    case "FA":
                        lc_cadena = " AND ARTIPPRO + ARCLAVE1 IN(SELECT FD_TIPPRO + FD_CLAVE1 FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA  IN(";                        
                        tbItems = Obj.GetFacturas(null, Convert.ToString(Session["CODEMP"]), lc_filtro);
                        break;
                }
                

                tbgrupos = objt.GetTbTablaLista(null, Convert.ToString(Session["CODEMP"]), "BODDT1");

                foreach (DataRow ry in tbgrupos.Rows)
                {                    
                    foreach (GridDataItem item in (((RadButton)sender).Parent.FindControl("rg_bodegas") as RadGrid).Items)
                    {
                        if ((item.FindControl("rc_tipo") as RadComboBox).SelectedValue == Convert.ToString(Convert.ToString(ry["TTCODCLA"])))
                        {
                            tbItems.Columns.Add(Convert.ToString(item["BDBODEGA"].Text), typeof(double));
                            tbItems.Columns.Add(Convert.ToString(item["BDBODEGA"].Text+"_gr"), typeof(string));

                            boundColumn = new GridBoundColumn();
                            boundColumn.UniqueName = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.DataField = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.HeaderText = Convert.ToString(item["BDNOMBRE"].Text);
                            boundColumn.HeaderStyle.Width = 120;
                            boundColumn.ColumnGroupName = Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue);
                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.Columns.Add(boundColumn);

                            foreach (DataRow rz in tbItems.Rows)
                                rz[Convert.ToString(item["BDBODEGA"].Text)] = 0;

                            foreach (DataRow rw in (ObjA.GetArticulosInv(null, lc_cadena + lc_filtro + "))", Convert.ToString(item["BDBODEGA"].Text)).Rows))
                            {
                                foreach (DataRow rz in tbItems.Rows)
                                {
                                    if (Convert.ToString(rz["PR_TIPPRO"]) == Convert.ToString(rw["ARTIPPRO"]) && Convert.ToString(rz["PR_CLAVE1"]) == Convert.ToString(rw["ARCLAVE1"]))
                                    {
                                        rz[Convert.ToString(item["BDBODEGA"].Text)] = rw["BBCANTID"];
                                        rz[Convert.ToString(item["BDBODEGA"].Text)+"_gr"] = Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue);
                                    }
                                }
                            }
                        }
                    }

                    //--- Agregando Totales         

                    tbItems.Columns.Add("T_"+ Convert.ToString(ry["TTCODCLA"]), typeof(double));
                    boundColumn = new GridBoundColumn();
                    boundColumn.UniqueName = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.DataField = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.HeaderText = Convert.ToString("Total");
                    boundColumn.HeaderStyle.Width = 120;
                    boundColumn.ItemStyle.ForeColor = System.Drawing.Color.Green;
                    boundColumn.ItemStyle.Font.Bold = true;
                    boundColumn.ColumnGroupName = Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                }
                
                tbItems.Columns.Add("SGD_CANTIDAD", typeof(double));
                tbItems.Columns.Add("SGD_PRECIO", typeof(double));
                foreach (DataRow rz in tbItems.Rows)
                {
                    foreach(DataRow rt in tbgrupos.Rows)
                        rz["T_"+Convert.ToString(rt["TTCODCLA"])] = 0;
                    
                    rz["SGD_CANTIDAD"] = 0;
                    rz["SGD_PRECIO"] = rz["PR_PRECIO"];
                }

                foreach (GridDataItem item in (((RadButton)sender).Parent.FindControl("rg_bodegas") as RadGrid).Items)
                {
                    if (((item.FindControl("rc_tipo") as RadComboBox).SelectedValue) != "-1")
                    {
                        foreach (DataRow rw in tbItems.Rows)
                            rw["T_" + Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue)] = Convert.ToDouble(rw["T_" + Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue)]) + Convert.ToDouble(rw[Convert.ToString(item["BDBODEGA"].Text)]);
                    }
                }

                Boolean lb_ind = true;
                int ln_cantpro = 0;
                string lc_proforma = "";
                tbProformas.Rows.Clear();
                tbProformas.Columns.Clear();
                tbProformas.Columns.Add("NroProforma", typeof(string));
                tbProformas.Columns.Add("Cantidad", typeof(int));
                foreach (DataRow rw in tbItems.Rows)
                {
                    lb_ind = false;
                    lc_proforma = Convert.ToString(rw["PR_NROFACPROFORMA"]);
                    ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);

                    foreach (DataRow ry in tbProformas.Rows)
                    {                        
                        if (Convert.ToString(ry["NroProforma"]) == Convert.ToString(rw["PR_NROFACPROFORMA"]))
                        {
                            lb_ind = true;
                            ry["Cantidad"] = Convert.ToInt32(ry["Cantidad"]) + Convert.ToInt32(rw["PR_CANTIDAD"]);
                            ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);
                        }
                    }
                    

                    if (!lb_ind)
                    {
                        if (lc_proforma != "")
                        {
                            DataRow row = tbProformas.NewRow();
                            row["NroProforma"] = lc_proforma;
                            row["Cantidad"] = ln_cantpro;
                            tbProformas.Rows.Add(row);
                            row = null;
                        }
                    }
                }

                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).DataSource = tbItems;
                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).DataBind();

                GridSortExpression expression = new GridSortExpression();
                expression.FieldName = "PR_NROFACPROFORMA";
                expression.SortOrder = GridSortOrder.Ascending;
                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression);
                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.Rebind();


                GridSortExpression expression_ = new GridSortExpression();
                expression_.FieldName= "PR_NROITEM";
                expression_.SortOrder = GridSortOrder.Ascending;
                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression_);

                (((RadButton)sender).Parent.FindControl("rg_proforma") as RadGrid).MasterTableView.Rebind();


                (((RadButton)sender).Parent.FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                (((RadButton)sender).Parent.FindControl("rg_ordenes") as RadGrid).DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjA = null;
                objt = null;
                boundColumn = null;
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
        protected void rg_proforma_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void obj_segregacion_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDt"] = tbItems;
            e.InputParameters["tbBodega"] = tbBodegas;            
        }
        protected void obj_segregacion_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                string url = "";
                string[] retorno = Convert.ToString(e.ReturnValue).Split('-');
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);

                url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9015&inban=S&inParametro=inNumero&inValor=" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
        }
        protected void btn_filtro_Click(object sender, EventArgs e)
        {
            string filtro = " 1=1";
            
            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text))
                filtro += " AND SGH_CODIGO = " + (((RadButton)sender).Parent.FindControl("txt_nroOrden") as RadTextBox).Text;

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nroproforma") as RadTextBox).Text))
                filtro += " AND SGH_CODIGO IN (SELECT SGH_CODIGO FROM TB_SEGREACION_PROFORMADT WITH(NOLOCK) WHERE (CAST(PR_NROCMP AS VARCHAR) + CAST(PR_NROITEM AS VARCHAR)) IN (SELECT CAST(PR_NROCMP AS VARCHAR) + CAST(PR_NROITEM AS VARCHAR) FROM CMP_PROFACTURADT WITH(NOLOCK) WHERE PR_NROFACPROFORMA = '"+ (((RadButton)sender).Parent.FindControl("txt_nroproforma") as RadTextBox).Text + "')) ";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text))
                filtro += " AND SGH_CODIGO IN (SELECT SGH_CODIGO FROM TB_SEGREACION_FACTURADT WITH(NOLOCK) WHERE (CAST(FD_NROCMP AS VARCHAR) + CAST(FD_NROITEM AS VARCHAR)) IN (SELECT CAST(FD_NROCMP AS VARCHAR) + CAST(FD_NROITEM AS VARCHAR) FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA = '" + (((RadButton)sender).Parent.FindControl("txt_factura") as RadTextBox).Text + "')) ";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nrodocumento") as RadTextBox).Text))
                filtro += " AND SG_NROPROFORMA = '" + (((RadButton)sender).Parent.FindControl("txt_nrodocumento") as RadTextBox).Text + "'";


            obj_segregacion.SelectParameters["filter"].DefaultValue = filtro;
            rlv_segregacion.DataBind();
            if ((rlv_segregacion.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_segregacion.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            //string url = "";
            //url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=9015&inban=S&inParametro=inNumero&inValor=" + Convert.ToString((rlv_segregacion.Items[0].FindControl("txt_codigo") as RadTextBox).Text);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
            string script = "function f(){$find(\"" + modalImprimir.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void ct_marcas_ItemClick(object sender, RadMenuEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Asistente":
                    string scriptt = "function f(){$find(\"" + modalAsistente.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
                    break;                
            }
        }
        protected void btnaplicar_Click(object sender, EventArgs e)
        {
            GridBoundColumn boundColumn;
            ComunBL objt = new ComunBL();
            DataTable tbgrupos = new DataTable();

            try
            {
                tbgrupos = objt.GetTbTablaLista(null, Convert.ToString(Session["CODEMP"]), "BODDT1");

                foreach (DataRow rw in tbItems.Rows)                
                    rw["SGD_PRECIO"] = Math.Round(Convert.ToDouble(rw["PR_PRECIO"]) + Convert.ToDouble((Convert.ToDouble(rw["PR_PRECIO"]) * txt_porcentaje.Value) / 100),2);
                
                
                //Recontruiccion Dinamica Grilla
                foreach (DataRow ry in tbgrupos.Rows)
                {                    

                    foreach (GridDataItem item in (rlv_segregacion.InsertItem.FindControl("rg_bodegas") as RadGrid).Items)
                    {
                        if ((item.FindControl("rc_tipo") as RadComboBox).SelectedValue == Convert.ToString(Convert.ToString(ry["TTCODCLA"])))
                        {
                            tbItems.Columns.Add(Convert.ToString(item["BDBODEGA"].Text), typeof(double));
                            tbItems.Columns.Add(Convert.ToString(item["BDBODEGA"].Text + "_gr"), typeof(string));

                            boundColumn = new GridBoundColumn();
                            boundColumn.UniqueName = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.DataField = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.HeaderText = Convert.ToString(item["BDNOMBRE"].Text);
                            boundColumn.HeaderStyle.Width = 120;
                            boundColumn.ColumnGroupName = Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue);
                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            (rlv_segregacion.InsertItem.FindControl("rg_proforma") as RadGrid).MasterTableView.Columns.Add(boundColumn);                            
                        }
                    }

                    //--- Agregando Totales         
                    tbItems.Columns.Add("T_" + Convert.ToString(ry["TTCODCLA"]), typeof(double));
                    boundColumn = new GridBoundColumn();
                    boundColumn.UniqueName = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.DataField = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.HeaderText = Convert.ToString("Total");
                    boundColumn.HeaderStyle.Width = 120;
                    boundColumn.ItemStyle.ForeColor = System.Drawing.Color.Green;
                    boundColumn.ItemStyle.Font.Bold = true;
                    boundColumn.ColumnGroupName = Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    (rlv_segregacion.InsertItem.FindControl("rg_proforma") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                }

                (rlv_segregacion.InsertItem.FindControl("rg_proforma") as RadGrid).DataSource = tbItems;
                (rlv_segregacion.InsertItem.FindControl("rg_proforma") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tbgrupos = null;
                boundColumn = null;
                objt = null;
            }
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

            obj_terceros_modal.SelectParameters["filter"].DefaultValue = filter;
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
                            if (rlv_segregacion.InsertItem != null)
                            {
                                (rlv_segregacion.InsertItem.FindControl("txt_codcomprador") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                                (rlv_segregacion.InsertItem.FindControl("txt_nomcomprador") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                            }
                            else {
                                (rlv_segregacion.Items[0].FindControl("txt_codcomprador") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                                (rlv_segregacion.Items[0].FindControl("txt_nomcomprador") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                            }
                        }
                        if (gc_indtercero == "A")
                        {
                            if (rlv_segregacion.InsertItem != null)
                            {
                                (rlv_segregacion.InsertItem.FindControl("txt_codvendedor") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                                (rlv_segregacion.InsertItem.FindControl("txt_nomvendedor") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                            }
                            else
                            {
                                (rlv_segregacion.Items[0].FindControl("txt_codvendedor") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                                (rlv_segregacion.Items[0].FindControl("txt_nomvendedor") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text + " " + Convert.ToString(item["TRDIRECC"].Text);
                            }
                        }
                    }
                    else
                    {
                        litTextoMensaje.Text = "Tercero se Encuntra Inactivo!";
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
                    string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
            }
        }
        protected void ibtn_vendedor_Click(object sender, ImageClickEventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "A";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void ibtn_comprador_Click(object sender, ImageClickEventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
            gc_indtercero = "B";
            string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_selrepor_Click(object sender, EventArgs e)
        {
            string url = "";
            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt="+Convert.ToString(rc_reporte.SelectedValue)+"&inban=S&inParametro=inNumero&inValor=" + Convert.ToString((rlv_segregacion.Items[0].FindControl("txt_codigo") as RadTextBox).Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void btn_proforma_Click(object sender, EventArgs e)
        {
            gc_tipo = "3";
        }
        protected void btn_aprobar_Click(object sender, EventArgs e)
        {
            gc_tipo = "2";
        }
        protected void btn_editar_Click(object sender, EventArgs e)
        {
            gc_tipo = "1";
        }
        protected void obj_segregacion_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDt"] = tbItems;
            e.InputParameters["inDtf"] = tbDefinitivo;
            e.InputParameters["tbBodega"] = tbBodegas;

            //e.InputParameters["SGH_ESTADO"] = "CE";
        }
        protected void rc_proveedor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string filter = " CHK='N' AND CHK_SEG = 'N' ";

            if (chk_segant.Checked)
                filter = " CHK='S' ";
            else
                filter = " CHK='N' ";

            if (chk_segpar.Checked)
                filter += " AND CHK_SEG = 'S' ";
            else
                filter += " AND CHK_SEG = 'N' ";


            switch (Convert.ToString(((sender as RadComboBox).Parent.FindControl("rc_origen") as RadComboBox).SelectedValue))
            {
                case "PR":
                    obj_pendientes.SelectMethod = "GetProformasProveedor";
                    break;
                case "FA":
                    obj_pendientes.SelectMethod = "GetFacturasProveedor";
                    break;

            }

            obj_pendientes.SelectParameters["CH_PROVEEDOR"].DefaultValue = (sender as RadComboBox).SelectedValue;
            obj_pendientes.SelectParameters["inFilter"].DefaultValue = filter;
            rg_proformaspendientes.DataBind();

            string script = "function f(){$find(\"" + modalProformas.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_proformasfacturas_Click(object sender, EventArgs e)
        {
            string filter = " CHK='S' AND CHK_SEG = 'N' ";
            if (chk_segant.Checked)
                filter = " CHK='S' ";
            else
                filter = " CHK='N' ";

            if (chk_segpar.Checked)
                filter += " AND CHK_SEG = 'S' ";
            else
                filter += " AND CHK_SEG = 'N' ";


            switch (Convert.ToString((rlv_segregacion.InsertItem.FindControl("rc_origen") as RadComboBox).SelectedValue))
            {
                case "PR":
                    obj_pendientes.SelectMethod = "GetProformasProveedor";
                    break;
                case "FA":
                    obj_pendientes.SelectMethod = "GetFacturasProveedor";
                    break;

            }

            obj_pendientes.SelectParameters["CH_PROVEEDOR"].DefaultValue = (rlv_segregacion.InsertItem.FindControl("rc_proveedor") as RadComboBox).SelectedValue;
            obj_pendientes.SelectParameters["inFilter"].DefaultValue = filter;
            rg_proformaspendientes.DataBind();

            string script = "function f(){$find(\"" + modalProformas.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_aceptarproformas_Click(object sender, EventArgs e)
        {
            (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text = "";

            foreach (GridDataItem item in rg_proformaspendientes.Items)
            {
                if ((item.FindControl("chk_chk") as CheckBox).Checked)
                {
                    (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text = (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text + "," + item["PR_NROFACPROFORMA"].Text;
                }
            }

            (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text = (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text.Substring(1, (rlv_segregacion.InsertItem.FindControl("txt_proformas") as RadTextBox).Text.Length - 1);
        }
        protected void rg_proforma_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string lc_ruta = "";
            switch (e.CommandName)
            {
                case "link_documento":
                    SoportesBL Obj = new SoportesBL();
                    GridDataItem item_ = (GridDataItem)e.Item;
                    Random random = new Random();
                    int random_0 = random.Next(0, 100);
                    int random_1 = random.Next(0, 100);
                    int random_2 = random.Next(0, 100);
                    int random_3 = random.Next(0, 100);
                    int random_4 = random.Next(0, 100);
                    int random_5 = random.Next(0, 100);
                    int random_6 = random.Next(0, 100);
                    string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(random_6) + (item_.FindControl("lbl_proforma") as Label).Text;
                    string path = MapPath("~/Uploads/"), lc_tipo="";
                    byte[] archivo = null;
                    List<Iruta> lst = new List<Iruta>();
                    try
                    {
                        lc_tipo = "Proforma";
                        if (rlv_segregacion.InsertItem != null)
                        {
                            if (Convert.ToString((rlv_segregacion.InsertItem.FindControl("rc_origen") as RadComboBox).SelectedValue) == "FA")
                                lc_tipo = "Invoice";
                        }
                        else
                        {
                            if (Convert.ToString((rlv_segregacion.Items[0].FindControl("rc_origen") as RadComboBox).SelectedValue) == "FA")
                                lc_tipo = "Invoice";
                        }
                        foreach (DataRow rw in Obj.GetCodSoporte(null, (item_.FindControl("lbl_proforma") as Label).Text,lc_tipo).Rows)
                        {
                            foreach (DataRow rx in Obj.GetSoporte(null, Convert.ToInt32(rw["SP_CONSECUTIVO"])).Rows)
                            {
                                archivo = (byte[])rx["SP_IMAGEN"];
                                Iruta itm = new Iruta();
                                itm.lRuta = path+Convert.ToString(rx["SP_DESCRIPCION"])+ Convert.ToString(rx["SP_EXTENCION"]);
                                lc_ruta = path + Convert.ToString(rx["SP_DESCRIPCION"]) + Convert.ToString(rx["SP_EXTENCION"]);
                                lst.Add(itm);
                                File.WriteAllBytes(path + Convert.ToString(rx["SP_DESCRIPCION"]) + Convert.ToString(rx["SP_EXTENCION"]), archivo);
                                itm = null;
                            }
                        }

                        using (ZipFile zip = new ZipFile())
                        {
                            foreach (Iruta i in lst)
                                zip.AddFile(i.lRuta, "");
                            zip.Save(path+ lc_nombre + ".zip");
                        }

                        //string url = "http://" + HttpContext.Current.Request.Url.Authority + "/ViwerPdf.aspx?rta=" + lc_ruta;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);

                        byte[] bts = System.IO.File.ReadAllBytes(path + lc_nombre + ".zip");
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.AddHeader("Content-Type", "Application/octet-stream");
                        HttpContext.Current.Response.AddHeader("Content-Length", bts.Length.ToString());
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + lc_nombre + ".zip");
                        HttpContext.Current.Response.BinaryWrite(bts);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.SuppressContent = true;
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        item_ = null;
                        archivo = null;
                        Obj = null;
                        lst = null;
                    }
                    break;
                case "link":
                    GridDataItem item = (GridDataItem)e.Item;                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item.FindControl("lbl_referencia") as Label).Text + "');", true);
                    item = null;
                    break;
                case "rbOpen":
                    gc_facpro = "1";
                    string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "rbTrasladar":
                    string lc_filter = "";
                    foreach (DataRow rw in tbItems.Rows)
                        lc_filter += rw["PR_NROCMP"] + ",";

                    obj_pendientes.SelectMethod = "GetFacturasProveedor";
                    obj_pendientes.SelectParameters["CH_PROVEEDOR"].DefaultValue = (rlv_segregacion.Items[0].FindControl("rc_proveedor") as RadComboBox).SelectedValue;
                    obj_pendientes.SelectParameters["inFilter"].DefaultValue = "PR_NROFACPROFORMA IN (SELECT FD_NROFACTURA FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROCMP IN (" + lc_filter.Substring(0,lc_filter.Length-1)+"))";
                    rg_facturast.DataBind();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalFacturas.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
            }
        }
        protected void rlv_segregacion_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_proforma") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                string lc_nrocmp = Convert.ToString((item.FindControl("lbl_nrocmp") as Label).Text);
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;   
                foreach (DataRow rw in tbItems.Rows)
                {
                    if (Convert.ToString(rw["PR_NROCMP"]) == lc_nrocmp && Convert.ToInt32(rw["PR_NROITEM"]) == ln_codigo && Convert.ToString(rw["SGD_GRUPO"])== "gp_detalle")
                    {
                        rw["SGD_CANTIDAD"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_cmp")).Value);
                        rw["SGD_CANTIDADAPRO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_prcantidad")).Value) - Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_cmp")).Value);
                        //rw["SGD_PRECIO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_precio_pro")).Value);
                    }
                }
            }

            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_facturas") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PR_NROITEM"].Text);
                string lc_nrocmp = Convert.ToString((item.FindControl("lbl_nrocmp") as Label).Text);
                foreach (System.Data.DataColumn col in tbDefinitivo.Columns) col.ReadOnly = false;
                foreach (DataRow rw in tbDefinitivo.Rows)
                {
                    if (Convert.ToString(rw["PR_NROCMP"]) == lc_nrocmp && Convert.ToInt32(rw["PR_NROITEM"]) == ln_codigo )
                    {
                        rw["SGD_CANTIDAD"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_cmp")).Value);
                        rw["SGD_CANTIDADAPRO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_des")).Value);
                        //rw["SGD_CANTIDADAPRO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_prcantidad")).Value) - Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_cantidad_cmp")).Value);
                        //rw["SGD_PRECIO"] = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_precio_pro")).Value);
                    }
                }
            }

            obj_segregacion.UpdateParameters["SGH_PARCIAL"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_segParcial") as CheckBox).Checked)
                obj_segregacion.UpdateParameters["SGH_PARCIAL"].DefaultValue = "S";
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
            string lc_msj = "";
            try
            {
                switch (gc_facpro)
                {
                    case "1":
                        foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                        foreach (DataRow rw in tbItems.Rows)
                            {
                                rw["SGD_CANTIDAD"] = 0;
                                rw["SGD_CANTIDADAPRO"] = Convert.ToInt32(rw["PR_CANTIDAD"]);
                            }
                        using (Stream stream = inStream)
                        {
                            using (StreamReader streamreader = new StreamReader(stream))
                            {
                                while (!streamreader.EndOfStream)
                                {
                                    Boolean lb_ind = false;
                                    string cadena = streamreader.ReadLine();
                                    string[] words = cadena.Split(';');
                                    string lc_referencia = words[0];
                                    string lc_c2 = words[1];
                                    string lc_c3 = words[2];
                                    string lc_c4 = words[3];
                                    int ln_cantidadOri = Convert.ToInt32(words[4]);
                                    int ln_cantidadDes = Convert.ToInt32(words[5]);

                                    foreach (DataRow rw in tbItems.Rows)
                                    {
                                        if (Convert.ToString(rw["PR_CLAVE1"]) == lc_referencia || Convert.ToString(rw["PR_REFPRO"]) == lc_referencia)
                                        {
                                            lb_ind = true;
                                            if (ln_cantidadOri <= Convert.ToInt32(rw["PR_CANTIDAD"]))
                                            {
                                                rw["SGD_CANTIDAD"] = ln_cantidadOri;
                                                rw["SGD_CANTIDADAPRO"] = Convert.ToInt32(rw["PR_CANTIDAD"]) - ln_cantidadOri;
                                                ln_cantidadOri = 0;
                                            }
                                            else
                                            {
                                                rw["SGD_CANTIDAD"] = rw["PR_CANTIDAD"];
                                                ln_cantidadOri = ln_cantidadOri - Convert.ToInt32(rw["PR_CANTIDAD"]);
                                                rw["SGD_CANTIDADAPRO"] = 0;
                                            }
                                        }
                                    }

                                    if (!lb_ind)
                                        lc_msj += " Referencia :" + lc_referencia + " No Existe";
                                }
                            }
                        }

                        (rlv_segregacion.Items[0].FindControl("rg_proforma") as RadGrid).DataSource = tbItems;
                        (rlv_segregacion.Items[0].FindControl("rg_proforma") as RadGrid).DataBind();
                        break;
                    case "2":
                        
                        foreach (System.Data.DataColumn col in tbDefinitivo.Columns) col.ReadOnly = false;
                        foreach (DataRow rw in tbDefinitivo.Rows)
                        {
                            rw["SGD_CANTIDAD"] = 0;
                            rw["SGD_CANTIDADAPRO"] = 0;
                            rw["DIFERENCIA"] = Convert.ToInt32(rw["SGD_CANTIDADCMP"]);
                        }
                        using (Stream stream = inStream)
                        {
                            using (StreamReader streamreader = new StreamReader(stream))
                            {
                                while (!streamreader.EndOfStream)
                                {
                                    Boolean lb_ind = false;
                                    string cadena = streamreader.ReadLine();
                                    string[] words = cadena.Split(';');
                                    string lc_referencia = words[0];
                                    string lc_c2 = words[1];
                                    string lc_c3 = words[2];
                                    string lc_c4 = words[3];
                                    int ln_cantidadOri = Convert.ToInt32(words[4]);
                                    int ln_cantidadDes = Convert.ToInt32(words[5]);

                                    foreach (DataRow rw in tbDefinitivo.Rows)
                                    {
                                        if (Convert.ToString(rw["PR_CLAVE1"]) == lc_referencia || Convert.ToString(rw["PR_REFPRO"]) == lc_referencia)
                                        {
                                            lb_ind = true;
                                            if ((ln_cantidadOri + ln_cantidadDes) <= Convert.ToInt32(rw["DIFERENCIA"]))
                                            {
                                                rw["SGD_CANTIDAD"] = Convert.ToInt32(rw["SGD_CANTIDAD"]) + ln_cantidadOri;
                                                rw["SGD_CANTIDADAPRO"] = Convert.ToInt32(rw["SGD_CANTIDADAPRO"]) + ln_cantidadDes;
                                                rw["DIFERENCIA"] = Convert.ToInt32(rw["DIFERENCIA"]) - (ln_cantidadOri + ln_cantidadDes);
                                                ln_cantidadOri = 0;
                                                ln_cantidadDes = 0;
                                            }
                                            else
                                            {
                                                if (ln_cantidadOri <= Convert.ToInt32(rw["DIFERENCIA"])){
                                                    rw["SGD_CANTIDAD"] = Convert.ToInt32(rw["SGD_CANTIDAD"]) + ln_cantidadOri;
                                                    rw["DIFERENCIA"] = Convert.ToInt32(rw["DIFERENCIA"]) - ln_cantidadOri;
                                                    ln_cantidadOri = 0;
                                                }
                                                else {
                                                    rw["SGD_CANTIDAD"] = Convert.ToInt32(rw["SGD_CANTIDAD"]) + Convert.ToInt32(rw["DIFERENCIA"]);
                                                    ln_cantidadOri = ln_cantidadOri - Convert.ToInt32(rw["DIFERENCIA"]);
                                                    rw["SGD_CANTIDADAPRO"] = rw["SGD_CANTIDADAPRO"];
                                                    rw["DIFERENCIA"] = 0;
                                                }

                                                if (ln_cantidadOri == 0 && (ln_cantidadDes <= Convert.ToInt32(rw["DIFERENCIA"])))
                                                {
                                                    rw["SGD_CANTIDADAPRO"] = Convert.ToInt32(rw["SGD_CANTIDADAPRO"]) + ln_cantidadDes;
                                                    rw["DIFERENCIA"] = Convert.ToInt32(rw["DIFERENCIA"]) - (ln_cantidadDes + ln_cantidadOri);                                                    
                                                    ln_cantidadDes = 0;
                                                }
                                                else
                                                {
                                                    if (ln_cantidadOri == 0)
                                                    {
                                                        rw["SGD_CANTIDADAPRO"] = Convert.ToInt32(rw["SGD_CANTIDADAPRO"]) + Convert.ToInt32(rw["DIFERENCIA"]);
                                                        ln_cantidadDes = ln_cantidadDes - Convert.ToInt32(rw["DIFERENCIA"]);                                                        
                                                        rw["DIFERENCIA"] = 0;
                                                    }
                                                }


                                            }
                                        }
                                    }

                                    if (!lb_ind)
                                        lc_msj += " Referencia :" + lc_referencia + " No Existe";
                                }
                            }
                        }

                        (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).DataSource = tbDefinitivo;
                        (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).DataBind();

                        break;
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
        double ln_cmptotal = 0,ln_tseg=0,ln_tcan2 = 0,ln_dif = 0;
        Boolean lb_ind = true;
        protected void rg_facturas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbDefinitivo;
        }
        protected void rg_facturas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "rbOpen":
                    gc_facpro = "2";
                    string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
                case "link_documento":
                    GridDataItem item_ = (GridDataItem)e.Item;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/OrdenesCompras.aspx?NroCmp=" + (item_.FindControl("lbl_nrocmp") as Label).Text + "');", true);
                    break;
                case "link":
                    GridDataItem item = (GridDataItem)e.Item;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item.FindControl("lbl_referencia") as Label).Text + "');", true);
                    item = null;
                    break;

            }
        }
        protected void rg_facturas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {
                lb_ind = true;
                GridDataItem dataItem = (GridDataItem)e.Item;
                ln_cmptotal += Convert.ToDouble((dataItem.FindControl("txt_prcantidad") as RadNumericTextBox).Value);
                //if (rlv_segregacion.InsertItem == null)                
                ln_tseg += Convert.ToDouble((dataItem.FindControl("txt_cantidad_cmp") as RadNumericTextBox).Value);
                ln_tcan2 += Convert.ToDouble((dataItem.FindControl("txt_cantidad_des") as RadNumericTextBox).Value);
                ln_dif += Convert.ToDouble((dataItem.FindControl("txt_cantidad_dif") as RadNumericTextBox).Value);
            }
            if (e.Item is GridFooterItem)
            {
                if (lb_ind)
                {
                    GridFooterItem ridGroupFooterItem = (GridFooterItem)e.Item;
                    //GridGroupFooterItem ridGroupFooterItem = (GridGroupFooterItem)e.Item;
                    //GridItem[] groups = (rlv_segregacion.InsertItem.FindControl("rg_items") as RadGrid).MasterTableView.GetItems(GridItemType.GroupHeader);
                    ridGroupFooterItem["PR_PRECIO"].Controls.Clear();
                    ridGroupFooterItem["PR_PRECIO"].Controls.Add(new LiteralControl("<b>      Total. </b><br/>"));
                    ridGroupFooterItem["SGD_CANTIDADCMP"].Controls.Clear();
                    ridGroupFooterItem["SGD_CANTIDADCMP"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_cmptotal).ToString("###,###.##") + "</font></b><br/>"));
                    ridGroupFooterItem["SGD_CANTIDAD"].Controls.Clear();
                    ridGroupFooterItem["SGD_CANTIDAD"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_tseg).ToString("###,###.##") + "</font></b><br/>"));

                    ridGroupFooterItem["SGD_CANTIDAD_DES"].Controls.Clear();
                    ridGroupFooterItem["SGD_CANTIDAD_DES"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_tcan2).ToString("###,###.##") + "</font></b><br/>"));
                    ridGroupFooterItem["SGD_CANTIDAD_DIF"].Controls.Clear();
                    ridGroupFooterItem["SGD_CANTIDAD_DIF"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_dif).ToString("###,###.##") + "</font></b><br/>"));
                }
                lb_ind = false;
                ln_cmptotal = 0;
                ln_tseg = 0;
                ln_tcan2 = 0;
                ln_dif = 0;
            }
        }
        protected void btn_aceptarf_Click(object sender, EventArgs e)
        {
            string lc_proformas = "";
            foreach (GridDataItem item in rg_facturast.Items)
            {
                if ((item.FindControl("chk_chk") as CheckBox).Checked)                
                    lc_proformas += item["PR_NROFACPROFORMA"].Text + ",";                
            }


            string[] cadena = lc_proformas.Split(',');
            string lc_filtro = "", lc_cadena = "";
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            ArticulosBL ObjA = new ArticulosBL();
            GridBoundColumn boundColumn;
            ComunBL objt = new ComunBL();
            DataTable tbgrupos = new DataTable();

            try
            {
                foreach (string word in cadena)
                    lc_filtro += "'" + word + "',";
                lc_filtro = lc_filtro.Substring(0, lc_filtro.Length - 1);
                
                lc_cadena = " AND ARTIPPRO + ARCLAVE1 IN(SELECT FD_TIPPRO + FD_CLAVE1 FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA  IN(";
                tbDefinitivo = Obj.GetFacturas(null, Convert.ToString(Session["CODEMP"]), lc_filtro);                

                tbgrupos = objt.GetTbTablaLista(null, Convert.ToString(Session["CODEMP"]), "BODDT1");

                foreach (DataRow ry in tbgrupos.Rows)
                {
                    foreach (GridDataItem item in (rlv_segregacion.Items[0].FindControl("rg_bodegas") as RadGrid).Items)
                    {
                        if ((item.FindControl("rc_tipo") as RadComboBox).SelectedValue == Convert.ToString(Convert.ToString(ry["TTCODCLA"])))
                        {
                            tbDefinitivo.Columns.Add(Convert.ToString(item["BDBODEGA"].Text), typeof(double));
                            tbDefinitivo.Columns.Add(Convert.ToString(item["BDBODEGA"].Text + "_gr"), typeof(string));

                            boundColumn = new GridBoundColumn();
                            boundColumn.UniqueName = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.DataField = Convert.ToString(item["BDBODEGA"].Text);
                            boundColumn.HeaderText = Convert.ToString(item["BDNOMBRE"].Text);
                            boundColumn.HeaderStyle.Width = 120;
                            boundColumn.ColumnGroupName = Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue);
                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                            (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.Columns.Add(boundColumn);

                            foreach (DataRow rz in tbDefinitivo.Rows)
                                rz[Convert.ToString(item["BDBODEGA"].Text)] = 0;

                            foreach (DataRow rw in (ObjA.GetArticulosInv(null, lc_cadena + lc_filtro + "))", Convert.ToString(item["BDBODEGA"].Text)).Rows))
                            {
                                foreach (DataRow rz in tbDefinitivo.Rows)
                                {
                                    if (Convert.ToString(rz["PR_TIPPRO"]) == Convert.ToString(rw["ARTIPPRO"]) && Convert.ToString(rz["PR_CLAVE1"]) == Convert.ToString(rw["ARCLAVE1"]))
                                    {
                                        rz[Convert.ToString(item["BDBODEGA"].Text)] = rw["BBCANTID"];
                                        rz[Convert.ToString(item["BDBODEGA"].Text) + "_gr"] = Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue);
                                    }
                                }
                            }
                        }
                    }

                    //--- Agregando Totales         

                    tbDefinitivo.Columns.Add("T_" + Convert.ToString(ry["TTCODCLA"]), typeof(double));
                    boundColumn = new GridBoundColumn();
                    boundColumn.UniqueName = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.DataField = "T_" + Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.HeaderText = Convert.ToString("Total");
                    boundColumn.HeaderStyle.Width = 120;
                    boundColumn.ItemStyle.ForeColor = System.Drawing.Color.Green;
                    boundColumn.ItemStyle.Font.Bold = true;
                    boundColumn.ColumnGroupName = Convert.ToString(ry["TTCODCLA"]);
                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                    (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                }

                tbDefinitivo.Columns.Add("SGD_CANTIDAD", typeof(double));
                tbDefinitivo.Columns.Add("SGD_PRECIO", typeof(double));
                tbDefinitivo.Columns.Add("SGD_CANTIDADAPRO", typeof(double));

                foreach (DataRow rz in tbDefinitivo.Rows)
                {
                    foreach (DataRow rt in tbgrupos.Rows)
                        rz["T_" + Convert.ToString(rt["TTCODCLA"])] = 0;

                    rz["SGD_CANTIDAD"] = 0;
                    rz["SGD_PRECIO"] = rz["PR_PRECIO"];
                }

                foreach (GridDataItem item in (rlv_segregacion.Items[0].FindControl("rg_bodegas") as RadGrid).Items)
                {
                    if (((item.FindControl("rc_tipo") as RadComboBox).SelectedValue) != "-1")
                    {
                        foreach (DataRow rw in tbDefinitivo.Rows)
                            rw["T_" + Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue)] = Convert.ToDouble(rw["T_" + Convert.ToString((item.FindControl("rc_tipo") as RadComboBox).SelectedValue)]) + Convert.ToDouble(rw[Convert.ToString(item["BDBODEGA"].Text)]);
                    }
                }

                foreach (DataRow rx in tbItems.Rows)
                {
                    int ln_cant = Convert.ToInt32(rx["SGD_CANTIDAD"]);
                    foreach (DataRow ry in tbDefinitivo.Rows)
                    {
                        if (Convert.ToString(rx["PR_CLAVE1"]) == Convert.ToString(ry["PR_CLAVE1"]))
                        {
                            if (ln_cant <= Convert.ToInt32(ry["PR_CANTIDAD"]))
                            {
                                ry["SGD_CANTIDAD"] = ln_cant;
                                ry["SGD_CANTIDADAPRO"] = Convert.ToInt32(ry["PR_CANTIDAD"]) - ln_cant;
                                ln_cant = 0;
                            }
                            else
                            {                                
                                ry["SGD_CANTIDAD"] = Convert.ToInt32(ry["PR_CANTIDAD"]);
                                ln_cant = ln_cant - Convert.ToInt32(ry["PR_CANTIDAD"]);
                                ry["SGD_CANTIDADAPRO"] = 0;
                            }
                        }
                    }
                }

                Boolean lb_ind = true;
                int ln_cantpro = 0;
                string lc_proforma = "";
                tbProformas.Rows.Clear();
                tbProformas.Columns.Clear();
                tbProformas.Columns.Add("NroProforma", typeof(string));
                tbProformas.Columns.Add("Cantidad", typeof(int));
                foreach (DataRow rw in tbDefinitivo.Rows)
                {
                    lb_ind = false;
                    lc_proforma = Convert.ToString(rw["PR_NROFACPROFORMA"]);
                    ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);

                    foreach (DataRow ry in tbProformas.Rows)
                    {
                        if (Convert.ToString(ry["NroProforma"]) == Convert.ToString(rw["PR_NROFACPROFORMA"]))
                        {
                            lb_ind = true;
                            ry["Cantidad"] = Convert.ToInt32(ry["Cantidad"]) + Convert.ToInt32(rw["PR_CANTIDAD"]);
                            ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);
                        }
                    }


                    if (!lb_ind)
                    {
                        if (lc_proforma != "")
                        {
                            DataRow row = tbProformas.NewRow();
                            row["NroProforma"] = lc_proforma;
                            row["Cantidad"] = ln_cantpro;
                            tbProformas.Rows.Add(row);
                            row = null;
                        }
                    }
                }

                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).DataSource = tbDefinitivo;
                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).DataBind();

                GridSortExpression expression = new GridSortExpression();
                expression.FieldName = "PR_NROFACPROFORMA";
                expression.SortOrder = GridSortOrder.Ascending;
                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression);
                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();


                GridSortExpression expression_ = new GridSortExpression();
                expression_.FieldName = "PR_NROITEM";
                expression_.SortOrder = GridSortOrder.Ascending;
                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expression_);
                (rlv_segregacion.Items[0].FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();


                (rlv_segregacion.Items[0].FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                (rlv_segregacion.Items[0].FindControl("rg_ordenes") as RadGrid).DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjA = null;
                objt = null;
                boundColumn = null;
            }
        }
        protected void rg_ordenes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            if ((item.FindControl("lbl_tdocumento") as Label).Text == "FA")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/OrdenesCompras.aspx?Invoice=" + (item.FindControl("lbl_proforma") as Label).Text + "');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/OrdenesCompras.aspx?Proforma=" + (item.FindControl("lbl_proforma") as Label).Text + "');", true);
            item = null;
         
        }
        protected void txt_seganterios_TextChanged(object sender, EventArgs e)
        {
            GridBoundColumn boundColumn;
            SegregacionBL Obj = new SegregacionBL();
            try
            {
                if (!string.IsNullOrEmpty(((RadTextBox)sender).Text))
                {
                    foreach (DataRow rw in Obj.GetSegregacionHD(null, " SGH_CODIGO="+((RadTextBox)sender).Text, 0, 0).Rows)
                    {
                        (((RadTextBox)sender).Parent.FindControl("txt_codvendedor") as RadTextBox).Text = Convert.ToString(rw["SG_VENDEDORPRO"]);
                        (((RadTextBox)sender).Parent.FindControl("txt_nomvendedor") as RadTextBox).Text = Convert.ToString(rw["VENDEDOR"]);
                        (((RadTextBox)sender).Parent.FindControl("txt_codcomprador") as RadTextBox).Text = Convert.ToString(rw["SG_COMPRADORPRO"]);
                        (((RadTextBox)sender).Parent.FindControl("txt_nomcomprador") as RadTextBox).Text = Convert.ToString(rw["COMPRADOR"]);
                        (((RadTextBox)sender).Parent.FindControl("txt_observaciones") as RadTextBox).Text = Convert.ToString(rw["SGH_OBSERVACIONES"]);

                        (((RadTextBox)sender).Parent.FindControl("rc_bodega_can") as RadComboBox).SelectedValue = Convert.ToString(rw["SGH_BODCAN"]);
                        (((RadTextBox)sender).Parent.FindControl("rc_bodega_dif") as RadComboBox).SelectedValue = Convert.ToString(rw["SGH_BODDIF"]);
                        (((RadTextBox)sender).Parent.FindControl("rc_proveedor") as RadComboBox).SelectedValue = Convert.ToString(rw["SGH_PROVEEDOR"]);
                        (((RadTextBox)sender).Parent.FindControl("rc_origen") as RadComboBox).SelectedValue = Convert.ToString(rw["SGH_TIPO"]);
                        (((RadTextBox)sender).Parent.FindControl("rc_estado") as RadComboBox).SelectedValue = Convert.ToString(rw["SGH_ESTADO"]);                                                
                    }

                    tbItems = Obj.GetSegregacionFactura(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((RadTextBox)sender).Text), "FA");
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw["PR_CANTIDAD"] = rw["DIFERENCIA"];
                        rw["SGD_CANTIDAD"] = 0;
                    }


                    foreach (DataRow rw in ((DataTable)Obj.GetSegregacionProformasBodegasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((RadTextBox)sender).Text))).Rows)
                    {
                        tbItems.Columns.Add(Convert.ToString(rw["SGD_BODEGA"]), typeof(double));

                        boundColumn = new GridBoundColumn();
                        boundColumn.UniqueName = Convert.ToString(rw["SGD_BODEGA"]);
                        boundColumn.DataField = Convert.ToString(rw["SGD_BODEGA"]);
                        boundColumn.HeaderText = Convert.ToString(rw["BDNOMBRE"]);
                        boundColumn.HeaderStyle.Width = 120;
                        boundColumn.ColumnGroupName = Convert.ToString(rw["SGD_GRUPO"]);
                        boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        (((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).MasterTableView.Columns.Add(boundColumn);
                        foreach (DataRow rt in tbItems.Rows)
                        {
                            if (Convert.ToString(rt["SGD_GRUPO"]) == "gp_detalle")
                            {
                                foreach (DataRow rz in tbItems.Rows)
                                {
                                    if (Convert.ToString(rz["SGD_BODEGA"]) == Convert.ToString(rw["SGD_BODEGA"]) && Convert.ToString(rz["PR_NROCMP"]) == Convert.ToString(rt["PR_NROCMP"]) && Convert.ToInt32(rz["PR_NROITEM"]) == Convert.ToInt32(rt["PR_NROITEM"]))
                                    {
                                        rt[Convert.ToString(rw["SGD_BODEGA"])] = Convert.ToDouble(rz["SGD_CANTIDAD"]);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //Borrar Grupos detallados
                    foreach (DataRow rt in tbItems.Rows)
                    {
                        if (Convert.ToString(rt["SGD_GRUPO"]) != "gp_detalle")
                            rt.Delete();
                    }

                    tbItems.AcceptChanges();

                    //(((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).DataSource = tbDefinitivo;
                    //(((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).DataBind();

                    Boolean lb_ind = true;
                    int ln_cantpro = 0;
                    string lc_proforma = "";
                    tbProformas.Rows.Clear();
                    tbProformas.Columns.Clear();
                    tbProformas.Columns.Add("NroProforma", typeof(string));
                    tbProformas.Columns.Add("Cantidad", typeof(int));
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        lb_ind = false;
                        lc_proforma = Convert.ToString(rw["PR_NROFACPROFORMA"]);
                        ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);

                        foreach (DataRow ry in tbProformas.Rows)
                        {
                            if (Convert.ToString(ry["NroProforma"]) == Convert.ToString(rw["PR_NROFACPROFORMA"]))
                            {
                                lb_ind = true;
                                ry["Cantidad"] = Convert.ToInt32(ry["Cantidad"]) + Convert.ToInt32(rw["PR_CANTIDAD"]);
                                ln_cantpro = Convert.ToInt32(rw["PR_CANTIDAD"]);
                            }
                        }


                        if (!lb_ind)
                        {
                            if (lc_proforma != "")
                            {
                                DataRow row = tbProformas.NewRow();
                                row["NroProforma"] = lc_proforma;
                                row["Cantidad"] = ln_cantpro;
                                tbProformas.Rows.Add(row);
                                row = null;
                            }
                        }
                    }

                    (((RadTextBox)sender).Parent.FindControl("rg_proforma") as RadGrid).DataSource = tbItems;
                    (((RadTextBox)sender).Parent.FindControl("rg_proforma") as RadGrid).DataBind();

                    /*
                    GridSortExpression expressiond = new GridSortExpression();
                    expressiond.FieldName = "PR_NROFACPROFORMA";
                    expressiond.SortOrder = GridSortOrder.Ascending;
                    (((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expressiond);
                    (((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();


                    GridSortExpression expressiond_ = new GridSortExpression();
                    expressiond_.FieldName = "PR_NROITEM";
                    expressiond_.SortOrder = GridSortOrder.Ascending;
                    (((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).MasterTableView.SortExpressions.AddSortExpression(expressiond_);
                    (((RadTextBox)sender).Parent.FindControl("rg_facturas") as RadGrid).MasterTableView.Rebind();

                    */

                    (((RadTextBox)sender).Parent.FindControl("rg_ordenes") as RadGrid).DataSource = tbProformas;
                    (((RadTextBox)sender).Parent.FindControl("rg_ordenes") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
            }
            finally
            {
                Obj = null;
            }
        }
        protected void rg_proforma_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                lb_ind = true;
                GridDataItem dataItem = (GridDataItem)e.Item;                
                ln_cmptotal += Convert.ToDouble((dataItem.FindControl("txt_prcantidad") as RadNumericTextBox).Value);
                //if (rlv_segregacion.InsertItem == null)                
                ln_tseg += Convert.ToDouble((dataItem.FindControl("txt_cantidad_cmp") as RadNumericTextBox).Value);                
            }
            if (e.Item is GridFooterItem)
            {
                if (lb_ind)
                {
                    GridFooterItem ridGroupFooterItem = (GridFooterItem)e.Item;
                    //GridGroupFooterItem ridGroupFooterItem = (GridGroupFooterItem)e.Item;
                    //GridItem[] groups = (rlv_segregacion.InsertItem.FindControl("rg_items") as RadGrid).MasterTableView.GetItems(GridItemType.GroupHeader);
                    ridGroupFooterItem["PR_PRECIO"].Controls.Clear();
                    ridGroupFooterItem["PR_PRECIO"].Controls.Add(new LiteralControl("<b>      Total. </b><br/>"));
                    ridGroupFooterItem["PR_CANTIDAD"].Controls.Clear();
                    ridGroupFooterItem["PR_CANTIDAD"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_cmptotal).ToString("###,###.##") + "</font></b><br/>"));
                    ridGroupFooterItem["SGD_CANTIDAD"].Controls.Clear();
                    ridGroupFooterItem["SGD_CANTIDAD"].Controls.Add(new LiteralControl("<b> <font color = 'black' > " + (ln_tseg).ToString("###,###.##") + "</font></b><br/>"));
                }
                lb_ind = false;
                ln_cmptotal = 0;
                ln_tseg = 0;
            }
        }
    }
    [Serializable]
    public class Iruta
    {
        public string lRuta { get; set; }
    }
}