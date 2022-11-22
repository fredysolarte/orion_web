using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Compras
{    
    public partial class SegregacionFacturas : System.Web.UI.Page
    {
        public DataTable tbItems
        {
            set { Cache["tbItems"] = value;  }
            get { return Cache["tbItems"] as DataTable; }
            //set { ViewState["tbItems"] = value; }
            //get { return ViewState["tbItems"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_segregacion, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_segregacions_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    SegregacionBL Obj = new SegregacionBL();                    
                    try
                    {
                        tbItems = Obj.GetSegregacionDT(null, Convert.ToString(Session["CODEMP"]), 0);                        
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

                case "Buscar":
                    obj_segregacion.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_segregacion.DataBind();
                    break;
                case "Edit":

                    break;
                case "Delete":
                    break;
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";                       

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

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
        protected void rlv_segregacion_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    SegregacionBL Obj = new SegregacionBL();
                    
                    try
                    {
                        tbItems = Obj.GetSegregacionDT(null, Convert.ToString(Session["CODEMP"]), -1);                        

                        foreach (DataRow rw in (Obj.GetSegregacionBodegasDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_segregacion.Items[0].FindControl("txt_nroorden") as RadTextBox).Text))).Rows)
                        {
                            tbItems.Columns.Add("BOD_" + Convert.ToString(rw["BDBODEGA"]), typeof(double));
                            GridTemplateColumn templateColumn = new GridTemplateColumn();

                            templateColumn.ItemTemplate = new MyTemplate("BOD_" + Convert.ToString(rw["BDBODEGA"]));
                            templateColumn.UniqueName = "BOD_" + Convert.ToString(rw["BDBODEGA"]);
                            templateColumn.HeaderText = Convert.ToString(rw["BDBODEGA"]);
                            templateColumn.HeaderStyle.Width = 70;
                            (e.Item.FindControl("rg_items") as RadGrid).MasterTableView.Columns.Add(templateColumn);


                            foreach (DataRow rx in (Obj.GetSegregacionDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_segregacion.Items[0].FindControl("txt_nroorden") as RadTextBox).Text))).Rows)
                            {
                                Boolean lb_ind = false;
                                foreach (DataRow rz in tbItems.Rows)
                                {
                                    if (Convert.ToString(rx["FD_NROCMP"]) == Convert.ToString(rz["FD_NROCMP"]) && Convert.ToString(rx["FD_NROFACTURA"]) == Convert.ToString(rz["FD_NROFACTURA"]) && Convert.ToInt32(rx["FD_NROITEM"]) == Convert.ToInt32(rz["FD_NROITEM"]))
                                    {
                                        lb_ind = true;
                                        if (Convert.ToString(rx["BDBODEGA"])== Convert.ToString(rw["BDBODEGA"]))
                                            rz["BOD_" + Convert.ToString(rw["BDBODEGA"])] = rx["SG_CANTIDAD"];
                                    }
                                }

                                if (!lb_ind)
                                {
                                    DataRow row = tbItems.NewRow();
                                    row["SG_ITEM"] = tbItems.Rows.Count + 1;
                                    row["SG_CODIGO"] = 1;
                                    row["FD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                                    row["FD_NROCMP"] = Convert.ToInt32(rx["FD_NROCMP"]);
                                    row["FD_NROITEM"] = Convert.ToInt32(rx["FD_NROITEM"]);
                                    row["FD_NROFACTURA"] = Convert.ToString(rx["FD_NROFACTURA"]);
                                    row["ARTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                    row["ARCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                    row["ARCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                    row["ARCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                    row["ARCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                    row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                    row["TANOMBRE"] = Convert.ToString(rx["TANOMBRE"]);
                                    row["BDBODEGA"] = ".";
                                    row["SG_CANTIDAD"] = 0;
                                    row["BOD_" + Convert.ToString(rw["BDBODEGA"])] = rx["SG_CANTIDAD"];
                                    row["SG_ESTADO"] = "AC";
                                    row["SG_USUARIO"] = ".";
                                    row["NOMTTEC1"] = Convert.ToString(rx["NOMTTEC1"]);
                                    row["NOMTTEC2"] = Convert.ToString(rx["NOMTTEC2"]);
                                    row["NOMTTEC3"] = Convert.ToString(rx["NOMTTEC3"]);
                                    row["NOMTTEC4"] = Convert.ToString(rx["NOMTTEC4"]);
                                    row["NOMTTEC5"] = Convert.ToString(rx["NOMTTEC5"]);
                                    row["NOMTTEC7"] = Convert.ToString(rx["NOMTTEC7"]);
                                    row["SG_FECING"] = DateTime.Today;
                                    row["SG_FECMOD"] = DateTime.Today;

                                    tbItems.Rows.Add(row);
                                    row = null;
                                }

                                /*foreach (DataRow rz in tbItems.Rows)
                                {
                                    if (Convert.ToString(rx["FD_NROCMP"]) == Convert.ToString(rz["FD_NROCMP"]) && Convert.ToString(rx["FD_NROFACTURA"]) == Convert.ToString(rz["FD_NROFACTURA"]) && Convert.ToInt32(rx["FD_NROITEM"]) == Convert.ToInt32(rz["FD_NROITEM"]))
                                        rz["BOD_" + Convert.ToString(rw["BDBODEGA"])] = rx["SG_CANTIDAD"];
                                }*/
                            }
                        }

                        //tbItems = Obj.GetSegregacionDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_segregacion.Items[0].FindControl("txt_nroorden") as RadTextBox).Text));


                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();                        
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
        protected void rlv_segregacion_ItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                e.ExceptionHandled = true;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void btn_loadfac_Click(object sender, EventArgs e)
        {
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            int ln_nrocmp = 0;
            string lc_conact = "";
            try {
                string[] words = (((RadButton)sender).Parent.FindControl("txt_facturas") as RadTextBox).Text.Split(',');
                foreach (string word in words)
                    lc_conact += "'" + word + "',";

                lc_conact = lc_conact.Substring(0, lc_conact.Length - 1);

                foreach (DataRow rw in (Obj.GetComprasHD(null, " CH_NROCMP IN (SELECT FD_NROCMP FROM CMP_FACTURADT WITH(NOLOCK) WHERE FD_NROFACTURA IN ("+lc_conact+"))", 0, 0).Rows))
                {
                    ln_nrocmp = Convert.ToInt32(rw["CH_NROCMP"]);
                }

                foreach (DataRow rw in (Obj.GetFactura(null, Convert.ToString(Session["CODEMP"]), ln_nrocmp)).Rows)
                {
                    DataRow row = tbItems.NewRow();
                    row["SG_ITEM"] = tbItems.Rows.Count + 1;
                    row["SG_CODIGO"] = 1;
                    row["FD_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                    row["FD_NROCMP"] = Convert.ToInt32(rw["FD_NROCMP"]);
                    row["FD_NROITEM"] = Convert.ToInt32(rw["FD_NROITEM"]);
                    row["FD_NROFACTURA"] = Convert.ToString(rw["FD_NROFACTURA"]);
                    row["ARTIPPRO"] = Convert.ToString(rw["FD_TIPPRO"]);
                    row["ARCLAVE1"] = Convert.ToString(rw["FD_CLAVE1"]);
                    row["ARCLAVE2"] = Convert.ToString(rw["FD_CLAVE2"]);
                    row["ARCLAVE3"] = Convert.ToString(rw["FD_CLAVE3"]);
                    row["ARCLAVE4"] = Convert.ToString(rw["FD_CLAVE4"]);
                    row["ARNOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                    row["TANOMBRE"] = Convert.ToString(rw["TANOMBRE"]);
                    row["BDBODEGA"] = ".";
                    row["SG_CANTIDAD"] = Convert.ToDouble(rw["FD_CANTIDAD"]);
                    row["SG_ESTADO"] = "AC";
                    row["SG_USUARIO"] = ".";
                    row["NOMTTEC1"] = Convert.ToString(rw["NOMTTEC1"]);
                    row["NOMTTEC2"] = Convert.ToString(rw["NOMTTEC2"]);
                    row["NOMTTEC3"] = Convert.ToString(rw["NOMTTEC3"]);
                    row["NOMTTEC4"] = Convert.ToString(rw["NOMTTEC4"]);
                    row["NOMTTEC5"] = Convert.ToString(rw["NOMTTEC5"]);
                    row["NOMTTEC7"] = Convert.ToString(rw["NOMTTEC7"]);
                    row["SG_FECING"] = DateTime.Today;
                    row["SG_FECMOD"] = DateTime.Today;

                    tbItems.Rows.Add(row);
                    row = null;                    
                }

                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataBind();

                (((RadButton)sender).Parent.FindControl("txt_facturas") as RadTextBox).Enabled = false;
                ((RadButton)sender).Enabled = false;
                (((RadButton)sender).Parent.FindControl("btn_loadbod") as RadButton).Enabled = true;

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
        protected void btn_loadbod_Click(object sender, EventArgs e)
        {           
            try
            {
                var collection = (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).CheckedItems;
                if (collection.Count != 0)
                {
                    //Valida Existencia en columnas
                    foreach (DataColumn cl in tbItems.Columns)
                    {
                        Boolean lb_ind = true;

                        if (cl.ColumnName.Substring(0, 4) == "BOD_")
                        {
                            lb_ind = false;
                            foreach (var item in collection)
                            {
                                if (cl.ColumnName == "BOD_" + Convert.ToString(item.Value))
                                    lb_ind = true;
                            }
                        }

                        if (!lb_ind)
                            tbItems.Columns.Remove(cl.ColumnName);
                    }

                    // Valida existencia en items
                    foreach (var item in collection)
                    {
                        Boolean lb_ind = true;
                        foreach (DataColumn cl in tbItems.Columns)
                        {
                            if (cl.ColumnName == "BOD_" + Convert.ToString(item.Value))
                                lb_ind = false;
                        }

                        if (lb_ind)
                        {
                            tbItems.Columns.Add("BOD_" + Convert.ToString(item.Value), typeof(double));
                            foreach (DataRow rw in tbItems.Rows)
                                rw["BOD_" + Convert.ToString(item.Value)] = 0;

                            GridTemplateColumn templateColumn = new GridTemplateColumn();
                            templateColumn.ItemTemplate = new MyTemplate("BOD_" + Convert.ToString(item.Value));
                            templateColumn.UniqueName = "BOD_" + Convert.ToString(item.Value);
                            templateColumn.HeaderText = item.Text;
                            templateColumn.HeaderStyle.Width = 70;                                  
                            (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).MasterTableView.Columns.Add(templateColumn);
                        }
                    }
                }
                else {
                    foreach (DataColumn cl in tbItems.Columns)
                    {
                        if (cl.ColumnName.Substring(0,4) =="BOD_")
                            tbItems.Columns.Remove(cl.ColumnName);
                    }
                }

                (sender as RadButton).Enabled = false;

                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (((RadButton)sender).Parent.FindControl("rg_items") as RadGrid).DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        private class MyTemplate : ITemplate
        {
            private string colname;
            public MyTemplate(string cName)
            {
                colname = cName;
            }

            public void InstantiateIn(Control container)
            {
                StringBuilder l_command = new StringBuilder();
                RadNumericTextBox numeric = new RadNumericTextBox();
                numeric.ID = colname;
                numeric.DataBinding += new EventHandler(numeric_DataBinding);                
                numeric.ClientEvents.OnValueChanged += "changevalue";
                numeric.NumberFormat.DecimalDigits = 0;
                numeric.Value = 0;                
                ////numeric.TextChanged += new EventHandler(numeric_TextChanged);
                container.Controls.Add(numeric);
            }
            
            public void numeric_DataBinding(object sender, EventArgs e)
            {
                RadNumericTextBox x = (RadNumericTextBox)sender;
                x.Width = 60;
                //x.AutoPostBack = true;
                GridDataItem container = (GridDataItem)x.NamingContainer;
                x.Text = ((DataRowView)container.DataItem)[colname].ToString();
            }

            public void numeric_TextChanged(object sender, EventArgs e)
            {
                SegregacionFacturas obj = new SegregacionFacturas();
                try
                {
                    int ln_codigo = Convert.ToInt32(((sender as RadNumericTextBox).Parent.FindControl("txt_codpro") as RadTextBox).Text);                    
                    foreach (DataRow rw in obj.tbItems.Rows)
                    {
                        if (Convert.ToInt32(rw["SG_ITEM"]) == ln_codigo)
                        {
                            rw[(sender as RadNumericTextBox).ID] = Convert.ToDouble((sender as RadNumericTextBox).Value);                            
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
        }

        protected void obj_segregacion_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["inDt"] = tbItems;
        }

        protected void obj_segregacion_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {                                
                litTextoMensaje.Text = "Nro Documento Generado :" + Convert.ToString(e.ReturnValue);
                Cache["tbItems"] = null;
            }
            string scriptt = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", scriptt, true);
        }

        protected void rlv_segregacion_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            //foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
            //{
            //    int ln_codigo = Convert.ToInt32((item.FindControl("txt_codpro") as RadTextBox).Text);
            //    foreach (DataRow rw in tbItems.Rows)
            //    {
            //        foreach (DataColumn cl in tbItems.Columns)
            //        {
            //            if (cl.ColumnName.Substring(0, 4) == "BOD_")
            //            {
            //                rw[cl.ColumnName] = (item.FindControl(cl.ColumnName) as RadNumericTextBox).Value;
            //            }
            //        }
            //    }
                
            //}
        }

        [WebMethod]
        public static int EditCantidad(int inCodigo, string inColumna, int inCantidad)
        {
            
            foreach (DataRow rw in (HttpContext.Current.Cache["tbItems"] as DataTable).Rows)
            {
                if (inCodigo == Convert.ToInt32(rw["SG_ITEM"]))
                {
                    rw[inColumna] = inCantidad;
                    break;
                }
            }
            return 0;
        }
    }
    
}