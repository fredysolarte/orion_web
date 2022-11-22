using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Text;
using XUSS.BLL.Pedidos;
using AjaxControlToolkit;
using XUSS.BLL.Terceros;
using XUSS.BLL;
using XUSS.BLL.Articulos;
using XUSS.BLL.Parametros;
using System.IO;
using System.Drawing;
using XUSS.BLL.Comun;


namespace XUSS.WEB.Facturacion
{
    public partial class EmpaquePedido : System.Web.UI.Page
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
        private DataTable tbTester
        {
            set { ViewState["tbTester"] = value; }
            get { return ViewState["tbTester"] as DataTable; }
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
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Pedido"])))
                {
                    obj_pedidos.SelectParameters["filter"].DefaultValue = "PHPEDIDO =" + Convert.ToString(Request.QueryString["Pedido"]);
                    rlv_pedidos.DataBind();
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
            this.OcultarPaginador(rlv_pedidos, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_pedidos_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            Boolean lb_bandera = true;
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    PedidosBL obj = new PedidosBL();
                    LtaEmpaqueBL Objl = new LtaEmpaqueBL();
                    try
                    {
                        tbItems = obj.GetPedidoDT(null, Convert.ToString(Session["CODEMP"]), 0);
                        tbAnexos = Objl.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), 0);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                        Objl = null;
                    }
                    break;
                case "Buscar":
                    obj_pedidos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_pedidos.DataBind();
                    break;
                case "Edit":
                    PedidosBL obj_ = new PedidosBL();
                    try
                    {
                        if ((obj_.TieneListaEmpaque(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_pedidos.Items[0].FindControl("txt_pedido")) as RadTextBox).Text)) > 0))
                        {
                            litTextoMensaje.Text = "¡Pedido Ya Cuenta con una Lista de Empaque!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            //e.Canceled = true;
                            //lb_bandera = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj_ = null;
                    }
                    break;
                case "Delete":
                    PedidosBL objD = new PedidosBL();
                    try
                    {
                        if ((objD.TieneListaEmpaque(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_pedidos.Items[0].FindControl("txt_pedido")) as RadTextBox).Text)) > 0))
                        {
                            litTextoMensaje.Text = "¡Pedido Ya Cuenta con una Lista de Empaque!";
                            e.Canceled = true;
                            lb_bandera = false;
                        }
                        else
                        {
                            objD.AnluarPedido(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(((rlv_pedidos.Items[0].FindControl("txt_pedido")) as RadTextBox).Text), Convert.ToString(Session["UserLogon"]));
                            litTextoMensaje.Text = "¡Pedido Anulado Exitosamente!";
                        }
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
                case "Liquidar":
                    double ln_precio = 0;
                    string tp = "", c1 = "", c2 = "", c3, c4;
                    DescuentosBL objp = new DescuentosBL();
                    try
                    {
                        foreach (DataRow row in tbItems.Rows)
                        {
                            tp = Convert.ToString(row["PDTIPPRO"]);
                            c1 = Convert.ToString(row["PDCLAVE1"]);
                            c2 = Convert.ToString(row["PDCLAVE2"]);
                            c3 = Convert.ToString(row["PDCLAVE3"]);
                            c4 = Convert.ToString(row["PDCLAVE4"]);

                            if (Convert.ToString(row["PDLISPRE"]) == "-1" || string.IsNullOrEmpty(Convert.ToString(row["PDLISPRE"])))
                            {
                                ln_precio = objp.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue));
                                row["PDLISPRE"] = Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue);
                            }
                            else
                            {
                                ln_precio = objp.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(row["PDLISPRE"]));
                            }

                            row["PDPRELIS"] = ln_precio;

                            objp.GetVlrDctoArt(Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
                                              Convert.ToString(row["PDCLAVE4"]), ".", Convert.ToDouble(row["PDPRELIS"]));

                            row["PDDESCUE"] = objp.VlrDctoP;
                            row["PDCODDES"] = objp.CodDcto;
                            row["PDPRECIO"] = ln_precio - ((ln_precio * objp.VlrDctoP) / 100);
                            row["PDSUBTOT"] = Convert.ToDouble(row["PDPRECIO"]) * Convert.ToDouble(row["PDCANTID"]);

                            tbItems.AcceptChanges();
                        }
                        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                        (rlv_pedidos.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue = "LQ";
                    }
                    catch (Exception ex)
                    {
                        litTextoMensaje.Text = "Error :" + ex.Message + "TP:" + tp + " C1:" + c1 + " C2:" + c2;
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        //throw ex;
                    }
                    finally
                    {
                        obj = null;
                    }
                    break;
            }
            if (lb_bandera)
                this.AnalizarCommand(e.CommandName);
            else
                this.AnalizarCommand("Cancel");

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += " AND (TRNOMBRE+ISNULL(TRNOMBR2,'')+ISNULL(TRAPELLI,'')) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            //if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text))
            //    filtro += " AND TRNOMBR2 LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_apellidos") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text))
                filtro += " AND TRCODNIT = '" + (((RadButton)sender).Parent.FindControl("txt_identificacion") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text))
                filtro += " AND PHPEDIDO = " + (((RadButton)sender).Parent.FindControl("txt_pedido") as RadTextBox).Text + "";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_pedidos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_pedidos.DataBind();
            if ((rlv_pedidos.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                //str.AppendLine("<div id=\"box-messages\" class=\"box\">");
                //str.AppendLine("<div class=\"messages\">");
                //str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
                //str.AppendLine("    <div class=\"image\">");
                //str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
                //str.AppendLine("		</div>");
                //str.AppendLine("    <div class=\"text\">");
                //str.AppendLine("        <h6>Información</h6>");
                //str.AppendLine("        <span>No se encontraron registros</span>");
                //str.AppendLine("    </div>");
                //str.AppendLine("</div>");
                //str.AppendLine("</div>");
                //str.AppendLine("</div>");
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_pedidos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_pedidos_OnItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
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
                    PedidosBL obj = new PedidosBL();
                    TercerosBL objT = new TercerosBL();
                    RadComboBoxItem item_ = new RadComboBoxItem();
                    RadComboBoxItem item = new RadComboBoxItem();
                    LtaEmpaqueBL Objl = new LtaEmpaqueBL();
                    try
                    {
                        tbItems = obj.GetPedidoDT(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(rlv_pedidos.Items[0].GetDataKeyValue("PHPEDIDO").ToString()));
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        tbAnexos = Objl.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((e.Item.FindControl("txt_lstpaq") as RadTextBox).Text));
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                        (e.Item.FindControl("rg_anexos") as RadGrid).DataBind();

                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                        item_.Value = "";
                        item_.Text = "Seleccionar";
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                        using (IDataReader reader = objT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["PHCODCLI"].ToString())))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                            }
                        }
                        (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = fila["PHCODSUC"].ToString();

                        (e.Item.FindControl("rc_agente") as RadComboBox).Items.Clear();
                        item.Value = "";
                        item.Text = "Seleccionar";
                        (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(item);
                        foreach (DataRow rw in objT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                        {
                            RadComboBoxItem itemi = new RadComboBoxItem();
                            itemi.Value = Convert.ToString(rw["TRCODTER"]);
                            itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                            (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                        }
                        (e.Item.FindControl("rc_agente") as RadComboBox).SelectedValue = fila["PHAGENTE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                        item_ = null;
                        item = null;
                        objT = null;
                        Objl = null;
                    }
                }
            }

            if (e.Item.ItemType == RadListViewItemType.EditItem)
            {
                PedidosBL obj = new PedidosBL();
                TercerosBL objT = new TercerosBL();
                RadComboBoxItem item_ = new RadComboBoxItem();
                RadComboBoxItem item = new RadComboBoxItem();
                try
                {
                    (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                    item_.Value = "";
                    item_.Text = "Seleccionar";
                    (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                    using (IDataReader reader = objT.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(fila["PHCODCLI"].ToString())))
                    {
                        while (reader.Read())
                        {
                            RadComboBoxItem itemi = new RadComboBoxItem();
                            itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                            itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                            (e.Item.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                        }
                    }
                    (e.Item.FindControl("rc_sucursal") as RadComboBox).SelectedValue = fila["PHCODSUC"].ToString();

                    (e.Item.FindControl("rc_agente") as RadComboBox).Items.Clear();
                    item.Value = "";
                    item.Text = "Seleccionar";
                    (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(item);
                    foreach (DataRow rw in objT.GetTerceros(null, " TRINDVEN='S'", 0, 0).Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["TRCODTER"]);
                        itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                        (e.Item.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                    }
                    (e.Item.FindControl("rc_agente") as RadComboBox).SelectedValue = fila["PHAGENTE"].ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    obj = null;
                    item_ = null;
                    item = null;
                    objT = null;
                }
            }



        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void iBtnFindTercero_OnClick(object sender, EventArgs e)
        {
            edt_codter.Text = "";
            edt_identificacion.Text = "";
            edt_nomtercero.Text = "";
            //mpTerceros.Show();
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
        protected void rgConsultaTerceros_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                        if (rlv_pedidos.InsertItem == null)
                        {
                            (rlv_pedidos.Items[0].FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate = System.DateTime.Today;
                            (rlv_pedidos.Items[0].FindControl("txt_nit") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                            (rlv_pedidos.Items[0].FindControl("txt_tercero") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text;
                            (rlv_pedidos.Items[0].FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                            (rlv_pedidos.Items[0].FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(item["TRLISPRE"].Text);

                            (rlv_pedidos.Items[0].FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                            item_.Value = "";
                            item_.Text = "Seleccionar";
                            (rlv_pedidos.Items[0].FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                            using (IDataReader reader = obj.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(item["TRCODTER"].Text)))
                            {
                                while (reader.Read())
                                {
                                    RadComboBoxItem itemi = new RadComboBoxItem();
                                    itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                    itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                    (rlv_pedidos.Items[0].FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                                }
                            }
                        }
                        else
                        {
                            (rlv_pedidos.InsertItem.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate = System.DateTime.Today;
                            (rlv_pedidos.InsertItem.FindControl("txt_nit") as RadTextBox).Text = Convert.ToString(item["TRCODNIT"].Text);
                            (rlv_pedidos.InsertItem.FindControl("txt_tercero") as RadTextBox).Text = (item.FindControl("lbl_nomter") as Label).Text;
                            (rlv_pedidos.InsertItem.FindControl("txt_codcli") as RadTextBox).Text = Convert.ToString(item["TRCODTER"].Text);
                            (rlv_pedidos.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(item["TRLISPRE"].Text);

                            (rlv_pedidos.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Clear();
                            item_.Value = "";
                            item_.Text = "Seleccionar";
                            (rlv_pedidos.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(item_);
                            using (IDataReader reader = obj.GetSucursalesID(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32(item["TRCODTER"].Text)))
                            {
                                while (reader.Read())
                                {
                                    RadComboBoxItem itemi = new RadComboBoxItem();
                                    itemi.Value = Convert.ToString(reader["SC_CONSECUTIVO"]);
                                    itemi.Text = Convert.ToString(reader["SC_NOMBRE"]);
                                    (rlv_pedidos.InsertItem.FindControl("rc_sucursal") as RadComboBox).Items.Add(itemi);
                                }
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
                    //mpTerceros.Show();
                    string script = "function f(){$find(\"" + modalTerceros.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }

            }
        }
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            if (rlv_pedidos.InsertItem != null)
            {
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
            }
            else
            {
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).SelectedValue;
            }
            edt_referencia.Text = "";
            edt_nombreart.Text = "";
            rgConsultaArticulos.DataBind();
            edt_referencia.Focus();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = "";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";
            if (rlv_pedidos.InsertItem != null)
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue;
            else
                obj_articulos.SelectParameters["inBodega"].DefaultValue = (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).SelectedValue;


            obj_articulos.SelectParameters["filter"].DefaultValue = lsql;


            rgConsultaArticulos.DataBind();
            edt_referencia.Focus();
            string script = "function f(){$find(\"" + modalFiltroArt.ClientID + "\").show(); $find(\"" + edt_referencia.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;

                try
                {
                    //(rlv_liquidacion.Items[0].FindControl("edt_cie") as RadTextBox).Text = Convert.ToString(item["CIE_CODIGO"].Text);
                    //(rlv_liquidacion.Items[0].FindControl("edt_diagnostico") as RadTextBox).Text = Convert.ToString(item["CIE_NOMBRE"].Text);                    
                    txt_tp.Text = Convert.ToString(item["ARTIPPRO"].Text);
                    txt_nomtp.Text = (item.FindControl("txt_marcafl") as RadTextBox).Text;
                    txt_referencia.Text = Convert.ToString(item["ARCLAVE1"].Text);
                    txt_clave2.Text = (item.FindControl("txt_fclave2") as RadTextBox).Text;
                    txt_clave3.Text = (item.FindControl("txt_fclave3") as RadTextBox).Text;
                    txt_clave4.Text = (item.FindControl("txt_fclave4") as RadTextBox).Text;
                    txt_nc2.Text = Convert.ToString(item["CLAVE2"].Text);
                    txt_nc3.Text = Convert.ToString(item["CLAVE3"].Text);
                    txt_descripcion.Text = Convert.ToString(item["ARNOMBRE"].Text);
                    double ln_cantb = 0;
                    foreach (DataRow rx in tbItems.Rows)
                    {
                        if ((Convert.ToString(rx["PDTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rx["PDCLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rx["PDCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rx["PDCLAVE3"]) == txt_clave3.Text))
                            ln_cantb += Convert.ToDouble(rx["PDCANTID"]);
                    }
                    txt_caninv.Value = Convert.ToDouble(item["BBCANTID"].Text) - ln_cantb;
                    txt_preciolta.Value = Convert.ToDouble(item["PRECIO"].Text);
                    txt_dct.Value = Convert.ToDouble(item["DESCUENTO"].Text);

                    //mpAddArticulos.Show();
                    this.ConfigLinea();
                    txt_barras.Focus();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_cantidad.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
            }
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            Boolean lb_indicador = true;
            DataRow row = tbItems.NewRow();
            try
            {
                if (txt_caninv.Value >= txt_cantidad.Value)
                {
                    row["PDLINNUM"] = tbItems.Rows.Count + 1;
                    row["PDTIPPRO"] = txt_tp.Text;
                    row["TANOMBRE"] = txt_nomtp.Text;
                    row["PDCLAVE1"] = txt_referencia.Text;
                    row["PDCLAVE2"] = txt_clave2.Text;
                    row["PDCLAVE3"] = txt_clave3.Text;
                    row["PDCLAVE4"] = txt_clave4.Text;
                    row["PDCODCAL"] = ".";
                    row["PDUNDPED"] = "UN";

                    if (rlv_pedidos.InsertItem != null)
                        row["PDLISPRE"] = (rlv_pedidos.InsertItem.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;
                    else
                        row["PDLISPRE"] = (rlv_pedidos.Items[0].FindControl("rc_lstprecio") as RadComboBox).SelectedValue;

                    row["PDPRELIS"] = txt_preciolta.Value;
                    if (Convert.ToDouble(txt_caninv.Value) <= Convert.ToDouble(txt_cantidad.Value))
                    {
                        row["PDCANPED"] = txt_cantidad.Value;
                        row["PDCANTID"] = txt_caninv.Value;
                        row["PDSUBTOT"] = txt_caninv.Value * txt_preciolta.Value;
                    }
                    else
                    {
                        row["PDCANPED"] = txt_cantidad.Value;
                        row["PDCANTID"] = txt_cantidad.Value;
                        row["PDSUBTOT"] = txt_cantidad.Value * txt_preciolta.Value;
                    }

                    row["PDDESCUE"] = txt_dct.Value;
                    row["ARNOMBRE"] = txt_descripcion.Text;
                    row["PDPRECIO"] = 0;
                    row["PDCANDES"] = 0;
                    row["PDCANCAN"] = 0;
                    row["PDASGBOD"] = 0;
                    row["PDASGCOM"] = 0;
                    row["PDASGPRO"] = 0;
                    row["PDESTADO"] = "AC";
                    row["PDCAUSAE"] = ".";
                    row["PDNMUSER"] = "";
                    row["PDFECING"] = System.DateTime.Today;
                    row["PDFECMOD"] = System.DateTime.Today;

                    tbItems.Rows.Add(row);


                    if (rlv_pedidos.InsertItem != null)
                    {
                        (rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                    }
                    this.Limpiar();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    litTextoMensaje.Text = "Cantidad Insuficiente!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
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
        protected void obj_pedidos_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["DetPedidos"] = tbItems;
            e.InputParameters["tbAnexos"] = tbAnexos;
        }
        protected void obj_pedidos_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["DetPedidos"] = tbItems;
        }
        protected void obj_pedidos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                //e.ExceptionHandled = true;
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            }
            else
            {
                litTextoMensaje.Text = "Nro Pedido :" + Convert.ToString(e.ReturnValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8009&inban=S&inParametro=InConsecutivo&inValor=" + Convert.ToString(e.ReturnValue) + "');", true);
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rlv_pedidos_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
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
        protected void rlv_pedidos_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_pedidos.InsertParameters["PHCODSUC"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;
            obj_pedidos.InsertParameters["PHAGENTE"].DefaultValue = (e.ListViewItem.FindControl("rc_agente") as RadComboBox).SelectedValue;
        }
        protected void rlv_pedidos_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            obj_pedidos.UpdateParameters["PHCODSUC"].DefaultValue = (e.ListViewItem.FindControl("rc_sucursal") as RadComboBox).SelectedValue;
            obj_pedidos.UpdateParameters["PHAGENTE"].DefaultValue = (e.ListViewItem.FindControl("rc_agente") as RadComboBox).SelectedValue;
        }
        protected void rg_items_OnDeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var PDLINNUM = item.GetDataKeyValue("PDLINNUM").ToString();
                    int pos = 0;
                    int xpos = 0;
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["PDLINNUM"]) == Convert.ToInt32(PDLINNUM))
                        {
                            pos = xpos;
                        }
                        xpos++;
                    }
                    tbItems.Rows[pos].Delete();
                    tbItems.AcceptChanges();
                    foreach (DataRow row in tbItems.Rows)
                    {
                        row["PDLINNUM"] = i;
                        i++;
                    }
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    //(rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    break;
            }
        }
        protected void iBtnImprimir_OnClick(object sender, EventArgs e)
        {
            string url = "";
            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8009&inban=S&inParametro=InConsecutivo&inValor=" + rlv_pedidos.Items[0].GetDataKeyValue("PHPEDIDO").ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }
        protected void txt_valor_OnTextChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).Items)
            {
                int ln_codigo = Convert.ToInt32(item["PDLINNUM"].Text);
                foreach (DataRow row in tbItems.Rows)
                {
                    if (Convert.ToInt32(row["PDLINNUM"]) == ln_codigo)
                    {
                        if (((item.FindControl("txt_valor") as RadNumericTextBox)).DbValue != null)
                        {
                            row["PDDESCUE"] = ((item.FindControl("txt_valor") as RadNumericTextBox)).DbValue;
                            row["PDPRECIO"] = Convert.ToDouble(row["PDPRELIS"]) - ((((item.FindControl("txt_valor") as RadNumericTextBox)).Value * Convert.ToDouble(row["PDPRELIS"])) / 100);
                            row["PDSUBTOT"] = Convert.ToDouble(row["PDPRECIO"]) * Convert.ToDouble(row["PDCANTID"]);
                        }
                        //if (((item.FindControl("edt_folios") as RadNumericTextBox)).DbValue != null)
                        //    row["SL_FOLIOS"] = ((item.FindControl("edt_folios") as RadNumericTextBox)).DbValue;
                        tbItems.AcceptChanges();
                        break;
                    }
                }
            }
            (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
            (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            (sender as RadNumericTextBox).Focus();
        }
        //protected void ibtnLiquidar_OnClick(object sender, EventArgs e)
        //{
        //    double ln_precio = 0;
        //    string tp="", c1="", c2="", c3, c4;
        //    DescuentosBL obj = new DescuentosBL();
        //    try { 
        //        foreach (DataRow row in tbItems.Rows)
        //        {
        //            tp = Convert.ToString(row["PDTIPPRO"]);
        //            c1 = Convert.ToString(row["PDCLAVE1"]);
        //            c2 = Convert.ToString(row["PDCLAVE2"]);
        //            c3 = Convert.ToString(row["PDCLAVE3"]);
        //            c4 = Convert.ToString(row["PDCLAVE4"]);

        //            if (Convert.ToString(row["PDLISPRE"]) == "-1" || string.IsNullOrEmpty(Convert.ToString(row["PDLISPRE"])))
        //            {
        //                ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue));
        //                row["PDLISPRE"] = Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue);
        //            }
        //            else
        //            {
        //                ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(row["PDLISPRE"]));
        //            }

        //            row["PDPRELIS"] = ln_precio;

        //            obj.GetVlrDctoArt(Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
        //                              Convert.ToString(row["PDCLAVE4"]), ".", Convert.ToDouble(row["PDPRELIS"]));

        //            row["PDDESCUE"] = obj.VlrDctoP;
        //            row["PDCODDES"] = obj.CodDcto;
        //            row["PDPRECIO"] = ln_precio - ((ln_precio * obj.VlrDctoP) / 100);
        //            row["PDSUBTOT"] = Convert.ToDouble(row["PDPRECIO"]) * Convert.ToDouble(row["PDCANTID"]);

        //            tbItems.AcceptChanges();
        //        }
        //        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
        //        (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
        //        (rlv_pedidos.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue = "LQ";
        //    }
        //    catch (Exception ex)
        //    {
        //        litTextoMensaje.Text = "Error :" + ex.Message + "TP:"+tp+" C1:"+c1 +" C2:"+c2;
        //        //mpMensajes.Show();
        //        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //        //throw ex;
        //    }
        //    finally
        //    {
        //        obj = null;
        //    }
        //}
        protected void iBtnDownload_OnClick(object sender, EventArgs e)
        {
            string lc_cadena = "";
            string path = MapPath("~/Uploads/" + (rlv_pedidos.Items[0].FindControl("txt_pedido") as RadTextBox).Text.Trim());
            PedidosBL Obj = new PedidosBL();
            try
            {
                // Archivo Cabecera
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + ".txt"))
                {
                    using (IDataReader reader = Obj.GetPedidoPlano(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_pedidos.Items[0].FindControl("txt_pedido") as RadTextBox).Text.Trim())))
                    {
                        while (reader.Read())
                        {
                            lc_cadena = "";
                            lc_cadena += Convert.ToString(reader["PDPEDIDO"]) + "|";
                            lc_cadena += Convert.ToString(reader["PHFECPED"]) + "|";
                            lc_cadena += Convert.ToString(reader["TERCERO"]) + "|";
                            lc_cadena += Convert.ToString(reader["ARTIPPRO"]) + "|";
                            lc_cadena += Convert.ToString(reader["TANOMBRE"]) + "|";
                            lc_cadena += Convert.ToString(reader["ARCLAVE1"]) + "|";
                            lc_cadena += Convert.ToString(reader["ARNOMBRE"]) + "|";
                            lc_cadena += Convert.ToString(reader["PDCANTID"]);

                            file.WriteLine(lc_cadena);
                        }
                    }
                }

                byte[] bts = System.IO.File.ReadAllBytes(path + ".txt");
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + (rlv_pedidos.Items[0].FindControl("txt_pedido") as RadTextBox).Text.Trim() + ".txt");
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + "enviado_" + this.GeneraCodigo() + ".zip");
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
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
        protected void rg_items_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (rlv_pedidos.InsertItem != null)
            {
                (rlv_pedidos.InsertItem.FindControl("RequiredFieldValidator2") as RequiredFieldValidator).Validate();
                (rlv_pedidos.InsertItem.FindControl("RequiredFieldValidator1") as RequiredFieldValidator).Validate();
                if ((rlv_pedidos.InsertItem.FindControl("RequiredFieldValidator2") as RequiredFieldValidator).IsValid && (rlv_pedidos.InsertItem.FindControl("RequiredFieldValidator1") as RequiredFieldValidator).IsValid)
                {
                    if (e.CommandName == "InitInsert")
                    {

                        (rlv_pedidos.InsertItem.FindControl("rc_tpedido") as RadComboBox).Enabled = false;
                        (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).Enabled = false;
                        this.Limpiar();
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                    }
                    else
                    {
                        if (e.CommandName == "RebindGrid")
                        {
                            (rlv_pedidos.InsertItem.FindControl("rc_tpedido") as RadComboBox).Enabled = false;
                            (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).Enabled = false;

                            string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                    }
                }
                else
                {
                    e.Canceled = true;
                }
            }
            else
            {
                (rlv_pedidos.Items[0].FindControl("RequiredFieldValidator2") as RequiredFieldValidator).Validate();
                (rlv_pedidos.Items[0].FindControl("RequiredFieldValidator1") as RequiredFieldValidator).Validate();
                if ((rlv_pedidos.Items[0].FindControl("RequiredFieldValidator2") as RequiredFieldValidator).IsValid && (rlv_pedidos.Items[0].FindControl("RequiredFieldValidator1") as RequiredFieldValidator).IsValid)
                {
                    if (e.CommandName == "InitInsert")
                    {
                        (rlv_pedidos.Items[0].FindControl("rc_tpedido") as RadComboBox).Enabled = false;
                        (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).Enabled = false;
                        this.Limpiar();
                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        e.Canceled = true;
                    }
                    else
                    {
                        if (e.CommandName == "RebindGrid")
                        {
                            (rlv_pedidos.Items[0].FindControl("rc_tpedido") as RadComboBox).Enabled = false;
                            (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).Enabled = false;

                            string script = "function f(){$find(\"" + modalCargue.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            if (e.CommandName == "Tester")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpTester.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                            }
                            else
                            {
                                double ln_precio = 0;
                                string tp = "", c1 = "", c2 = "", c3, c4;
                                DescuentosBL obj = new DescuentosBL();
                                try
                                {
                                    foreach (DataRow row in tbItems.Rows)
                                    {
                                        tp = Convert.ToString(row["PDTIPPRO"]);
                                        c1 = Convert.ToString(row["PDCLAVE1"]);
                                        c2 = Convert.ToString(row["PDCLAVE2"]);
                                        c3 = Convert.ToString(row["PDCLAVE3"]);
                                        c4 = Convert.ToString(row["PDCLAVE4"]);

                                        if (Convert.ToString(row["PDLISPRE"]) == "-1" || string.IsNullOrEmpty(Convert.ToString(row["PDLISPRE"])))
                                        {
                                            ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue));
                                            row["PDLISPRE"] = Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue);
                                        }
                                        else
                                        {
                                            ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), tp, c1, c2, c3, c4, Convert.ToString(row["PDLISPRE"]));
                                        }

                                        row["PDPRELIS"] = ln_precio;

                                        obj.GetVlrDctoArt(Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]),
                                                          Convert.ToString(row["PDCLAVE4"]), ".", Convert.ToDouble(row["PDPRELIS"]));

                                        row["PDDESCUE"] = obj.VlrDctoP;
                                        row["PDCODDES"] = obj.CodDcto;
                                        row["PDPRECIO"] = ln_precio - ((ln_precio * obj.VlrDctoP) / 100);
                                        row["PDSUBTOT"] = Convert.ToDouble(row["PDPRECIO"]) * Convert.ToDouble(row["PDCANTID"]);

                                        tbItems.AcceptChanges();
                                    }
                                    (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                                    (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                                    (rlv_pedidos.Items[0].FindControl("rc_estado") as RadComboBox).SelectedValue = "LQ";
                                }
                                catch (Exception ex)
                                {
                                    litTextoMensaje.Text = "Error :" + ex.Message + "TP:" + tp + " C1:" + c1 + " C2:" + c2;
                                    //mpMensajes.Show();
                                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                                    //throw ex;
                                }
                                finally
                                {
                                    obj = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    e.Canceled = true;
                }
            }


        }
        protected void PreventErrorOnbinding(object sender, EventArgs e)
        {
            RadComboBox cb = sender as RadComboBox;
            cb.DataBinding -= new EventHandler(PreventErrorOnbinding);
            cb.AppendDataBoundItems = true;

            try
            {
                cb.DataBind();
                cb.Items.Clear();
            }
            catch (ArgumentOutOfRangeException)
            {
                //cb.Items.Clear();
                //cb.ClearSelection();
                //RadComboBoxItem cbI = new RadComboBoxItem("", "0");
                //cbI.Selected = true;
                //cb.Items.Add(cbI);
                litTextoMensaje.Text = "No Tiene Permisos Sobre la Bodega para Confirmar!";
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
                    if (rlv_pedidos.InsertItem == null)
                        tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text, null, (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).SelectedValue);
                    else
                        tbBarras = Obj.GetTbBarrasInv(null, (sender as RadTextBox).Text, null, (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue);

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
                            txt_nomtp.Text = Convert.ToString(rw["TANOMBRE"]);
                            double ln_cantb = 0;
                            foreach (DataRow rx in tbItems.Rows)
                            {
                                if ((Convert.ToString(rx["PDTIPPRO"]) == txt_tp.Text) && (Convert.ToString(rx["PDCLAVE1"]) == txt_referencia.Text) && (Convert.ToString(rx["PDCLAVE2"]) == txt_clave2.Text) && (Convert.ToString(rx["PDCLAVE3"]) == txt_clave3.Text))
                                    ln_cantb += Convert.ToDouble(rx["PDCANTID"]);
                            }
                            txt_preciolta.Value = Convert.ToDouble(rw["PRECIO"]);
                            txt_dct.Value = Convert.ToDouble(rw["DESCUENTO"]);
                            txt_caninv.Value = Convert.ToDouble(rw["BBCANTID"]) - ln_cantb;
                            txt_cantidad.Value = 1;
                        }

                        btn_aceptar_OnClick(sender, e);

                        txt_barras.Focus();
                        string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); $find(\"" + txt_barras.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        private void ConfigLinea()
        {
            rc_lote.Visible = false;
            lbl_lote.Visible = false;
            RequiredFieldValidator11.Enabled = false;
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
            LtaEmpaqueBL ObjL = new LtaEmpaqueBL();
            StringBuilder sCadena = new StringBuilder();
            try
            {
                foreach (System.Data.DataColumn col in tbItems.Columns) col.ReadOnly = false;
                using (Stream stream = inStream)
                {
                    using (StreamReader streamreader = new StreamReader(stream))
                    {
                        while (!streamreader.EndOfStream)
                        {
                            DataRow row = tbItems.NewRow();
                            string cadena = streamreader.ReadLine();
                            string[] words = cadena.Split(';');
                            if (rbl_tiparch.SelectedIndex == 0)
                            {
                                string lc_referencia = words[0];
                                string lc_c2 = words[1];
                                string lc_c3 = words[2];
                                string lc_c4 = words[3];
                                int ln_cantidad = Convert.ToInt32(words[4]);
                                Boolean lb_Existe = false;
                                foreach (DataRow rx in Obj.GetArticulos(null, " AND ARCLAVE1='" + lc_referencia + "' AND ARCLAVE2 ='" + lc_c2 + "' AND ARCLAVE3='" + lc_c3 + "' AND ARCLAVE4='" + lc_c4 + "'").Rows)
                                {
                                    lb_Existe = true;
                                    row["PDLINNUM"] = tbItems.Rows.Count + 1;
                                    row["PDTIPPRO"] = Convert.ToString(rx["ARTIPPRO"]);
                                    row["PDCLAVE1"] = Convert.ToString(rx["ARCLAVE1"]);
                                    row["PDCLAVE2"] = Convert.ToString(rx["ARCLAVE2"]);
                                    row["PDCLAVE3"] = Convert.ToString(rx["ARCLAVE3"]);
                                    row["PDCLAVE4"] = Convert.ToString(rx["ARCLAVE4"]);
                                    row["PDCODCAL"] = ".";
                                    row["PDUNDPED"] = "UN";
                                    row["PDCANPED"] = ln_cantidad;
                                    row["PDCANTID"] = 0;
                                    foreach (DataRow rz in ObjL.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["ARTIPPRO"]), Convert.ToString(rx["ARCLAVE1"]), Convert.ToString(rx["ARCLAVE2"]), Convert.ToString(rx["ARCLAVE3"]), Convert.ToString(rx["ARCLAVE4"]), (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, 0).Rows)
                                    {
                                        if (Convert.ToInt32(rz["BBCANTID"]) > ln_cantidad)
                                            row["PDCANTID"] = ln_cantidad;
                                        else
                                            row["PDCANTID"] = rz["BBCANTID"];
                                    }
                                    row["PDPRELIS"] = 0;
                                    row["PDDESCUE"] = 0;
                                    row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                    row["PDPRECIO"] = 0;
                                    row["PDCANDES"] = 0;
                                    row["PDCANCAN"] = 0;
                                    row["PDASGBOD"] = 0;
                                    row["PDASGCOM"] = 0;
                                    row["PDASGPRO"] = 0;
                                    row["PDESTADO"] = "AC";
                                    row["PDCAUSAE"] = ".";
                                    row["PDNMUSER"] = "";
                                    row["PDFECING"] = System.DateTime.Today;
                                    row["PDFECMOD"] = System.DateTime.Today;
                                    row["TANOMBRE"] = Convert.ToString(rx["TANOMBRE"]);

                                    tbItems.Rows.Add(row);
                                    break;
                                }
                                if (!lb_Existe)
                                    sCadena.AppendLine("Referencia :" + lc_referencia);
                            }
                            else
                            {
                                string lc_barras = words[0];
                                int ln_cantid = Convert.ToInt32(words[1]);
                                foreach (DataRow rx in Obj.GetTbBarrasInv(null, lc_barras, Convert.ToString(((rlv_pedidos.InsertItem.FindControl("rc_lstprecio")) as RadComboBox).SelectedValue), Convert.ToString((rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue)).Rows)
                                {
                                    row["PDLINNUM"] = tbItems.Rows.Count + 1;
                                    row["PDTIPPRO"] = Convert.ToString(rx["BTIPPRO"]);
                                    row["PDCLAVE1"] = Convert.ToString(rx["BCLAVE1"]);
                                    row["PDCLAVE2"] = Convert.ToString(rx["BCLAVE2"]);
                                    row["PDCLAVE3"] = Convert.ToString(rx["BCLAVE3"]);
                                    row["PDCLAVE4"] = Convert.ToString(rx["BCLAVE4"]);
                                    row["PDCODCAL"] = ".";
                                    row["PDUNDPED"] = "UN";
                                    row["PDCANPED"] = ln_cantid;
                                    row["PDCANTID"] = 0;
                                    foreach (DataRow rz in ObjL.GetDisposicion(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rx["BTIPPRO"]), Convert.ToString(rx["BCLAVE1"]), Convert.ToString(rx["BCLAVE2"]), Convert.ToString(rx["BCLAVE3"]), Convert.ToString(rx["BCLAVE4"]), (rlv_pedidos.InsertItem.FindControl("rc_bodega") as RadComboBox).SelectedValue, 0).Rows)
                                    {
                                        if (Convert.ToInt32(rz["BBCANTID"]) > ln_cantid)
                                            row["PDCANTID"] = ln_cantid;
                                        else
                                            row["PDCANTID"] = rz["BBCANTID"];
                                    }
                                    //row["PDCANPED"] = ln_cantid;
                                    //row["PDCANTID"] = ln_cantid;
                                    row["PDPRELIS"] = 0;
                                    row["PDDESCUE"] = 0;
                                    row["ARNOMBRE"] = Convert.ToString(rx["ARNOMBRE"]);
                                    row["PDPRECIO"] = 0;
                                    row["PDCANDES"] = 0;
                                    row["PDCANCAN"] = 0;
                                    row["PDASGBOD"] = 0;
                                    row["PDASGCOM"] = 0;
                                    row["PDASGPRO"] = 0;
                                    row["PDESTADO"] = "AC";
                                    row["PDCAUSAE"] = ".";
                                    row["PDNMUSER"] = "";
                                    row["PDFECING"] = System.DateTime.Today;
                                    row["PDFECMOD"] = System.DateTime.Today;

                                    tbItems.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
                (rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_pedidos.InsertItem.FindControl("rg_items") as RadGrid).DataBind();

                litTextoMensaje.Text = "¡Cargue Terminado! " + sCadena.ToString();
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sCadena = null;
                Obj = null;
                ObjL = null;
            }
        }
        protected void rlv_pedidos_OnPreRender(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ViewState["isClickInsert"]))
            {
                if (((sender as RadListView).InsertItem.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate == null)
                {
                    ((sender as RadListView).InsertItem.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate = System.DateTime.Now;
                }
            }
        }
        protected void rc_lstprecio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if ((sender as RadComboBox).SelectedValue != "")
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        rw["PDLISPRE"] = Convert.ToString((sender as RadComboBox).SelectedValue);
                    }
                    ((sender as RadComboBox).Parent.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                    ((sender as RadComboBox).Parent.FindControl("rg_items") as RadGrid).DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rc_tpedido_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TPedidoBL obj = new TPedidoBL();
            TercerosBL objT = new TercerosBL();
            RadComboBoxItem item = new RadComboBoxItem();
            try
            {
                if ((sender as RadComboBox).SelectedValue != "-1")
                {
                    foreach (DataRow rw in (obj.GetTipPed(null, " PTTIPPED='" + (sender as RadComboBox).SelectedValue + "'", 0, 0)).Rows)
                    {
                        ((sender as RadComboBox).Parent.FindControl("rc_lstprecio") as RadComboBox).SelectedValue = Convert.ToString(rw["PTLISPRE"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_idioma") as RadComboBox).SelectedValue = Convert.ToString(rw["PTIDIOMA"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_moneda") as RadComboBox).SelectedValue = Convert.ToString(rw["PTMONEDA"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue = Convert.ToString(rw["PTBODEGA"]);
                    }
                }

                ((sender as RadComboBox).Parent.FindControl("rc_agente") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";

                ((sender as RadComboBox).Parent.FindControl("rc_agente") as RadComboBox).Items.Clear();
                ((sender as RadComboBox).Parent.FindControl("rc_agente") as RadComboBox).Items.Add(item);
                foreach (DataRow rw in objT.GetTerceros(null, " TRINDVEN='S' AND TRESTADO='AC'", 0, 0).Rows)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(rw["TRCODTER"]);
                    itemi.Text = Convert.ToString(rw["TRNOMBRE"]) + " " + Convert.ToString(rw["TRNOMBR2"]) + " " + Convert.ToString(rw["TRAPELLI"]);
                    ((sender as RadComboBox).Parent.FindControl("rc_agente") as RadComboBox).Items.Add(itemi);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj = null;
                objT = null;
            }
        }
        protected void rc_lstpreciodet_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DescuentosBL obj = new DescuentosBL();
            try
            {
                foreach (GridDataItem item in (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).Items)
                {
                    int ln_codigo = Convert.ToInt32(item["PDLINNUM"].Text);
                    foreach (DataRow row in tbItems.Rows)
                    {
                        if (Convert.ToInt32(row["PDLINNUM"]) == ln_codigo)
                        {
                            if (((item.FindControl("rc_lstpreciodet") as RadComboBox)).SelectedValue != "-1")
                            {
                                row["PDLISPRE"] = Convert.ToString(((item.FindControl("rc_lstpreciodet") as RadComboBox)).SelectedValue);
                                row["PDDESCUE"] = ((item.FindControl("txt_valor") as RadNumericTextBox)).DbValue;
                                double ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]), Convert.ToString(row["PDCLAVE4"]), Convert.ToString(((item.FindControl("rc_lstpreciodet") as RadComboBox)).SelectedValue));
                                row["PDPRELIS"] = ln_precio;
                                row["PDPRECIO"] = ln_precio - ((((item.FindControl("txt_valor") as RadNumericTextBox)).Value * Convert.ToDouble(row["PDPRELIS"])) / 100);
                            }
                            else
                            {
                                row["PDLISPRE"] = Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue);
                                row["PDDESCUE"] = ((item.FindControl("txt_valor") as RadNumericTextBox)).DbValue;
                                double ln_precio = obj.GetPrecio(Convert.ToString(Session["CODEMP"]), Convert.ToString(row["PDTIPPRO"]), Convert.ToString(row["PDCLAVE1"]), Convert.ToString(row["PDCLAVE2"]), Convert.ToString(row["PDCLAVE3"]), Convert.ToString(row["PDCLAVE4"]), Convert.ToString(((rlv_pedidos.Items[0].FindControl("rc_lstprecio")) as RadComboBox).SelectedValue));
                                row["PDPRELIS"] = ln_precio;
                                row["PDPRECIO"] = ln_precio - ((((item.FindControl("txt_valor") as RadNumericTextBox)).Value * Convert.ToDouble(row["PDPRELIS"])) / 100);
                            }

                            tbItems.AcceptChanges();
                            break;
                        }
                    }
                }
                (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                (sender as RadComboBox).Focus();
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
        protected void rg_items_PreRender(object sender, EventArgs e)
        {
            double aValue = 0;
            double aCanPed = 0;
            double aCanDis = 0;

            foreach (GridDataItem item in (sender as RadGrid).Items)
            {
                aValue = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_precio")).Value);
                if (aValue == 0)
                {
                    item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                    item.ControlStyle.ForeColor = System.Drawing.Color.Red;
                    item.ControlStyle.Font.Bold = true;
                }
                aCanPed = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_canped")).Value);
                aCanDis = Convert.ToDouble(((RadNumericTextBox)item.FindControl("txt_candis")).Value);
                if (aCanPed != aCanDis)
                {
                    item.ControlStyle.BackColor = Color.Gainsboro;  //cxStyleConsMenCeroNoOK //aprox naraja
                    item.ControlStyle.ForeColor = System.Drawing.Color.Green;
                    item.ControlStyle.Font.Bold = true;
                }
            }
        }
        protected void btn_calcular_Click(object sender, EventArgs e)
        {
            ArticulosBL Obj = new ArticulosBL();
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("IT", typeof(Int32));
                dt.Columns.Add("TP", typeof(string));
                dt.Columns.Add("C1", typeof(string));
                dt.Columns.Add("C2", typeof(string));
                dt.Columns.Add("C3", typeof(string));
                dt.Columns.Add("C4", typeof(string));
                dt.Columns.Add("PRODUCTO", typeof(string));
                dt.Columns.Add("NOMTP", typeof(string));
                dt.Columns.Add("CANDIS", typeof(double));
                dt.Columns.Add("CANSOL", typeof(double));
                dt.Columns.Add("CHK", typeof(string));

                if (txt_canhomologo.Value > 0)
                {
                    foreach (DataRow rw in tbItems.Rows)
                    {
                        if (Math.Truncate(Convert.ToDouble(rw["PDCANTID"]) / Convert.ToDouble(txt_canhomologo.Value)) > 0)
                        {
                            foreach (DataRow rz in (Obj.GetTesterInv(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(rw["PDTIPPRO"]), Convert.ToString(rw["PDCLAVE1"]), Convert.ToString(rw["PDCLAVE2"]), Convert.ToString(rw["PDCLAVE3"]), Convert.ToString(rw["PDCLAVE4"]), (rlv_pedidos.Items[0].FindControl("rc_bodega") as RadComboBox).SelectedValue).Rows))
                            {
                                DataRow ry = dt.NewRow();
                                ry["IT"] = dt.Rows.Count + 1;
                                ry["TP"] = rz["TT_TIPPRO"];
                                ry["C1"] = rz["TT_CLAVE1"];
                                ry["C2"] = rz["TT_CLAVE2"];
                                ry["C3"] = rz["TT_CLAVE3"];
                                ry["C4"] = rz["TT_CLAVE4"];
                                ry["PRODUCTO"] = rz["ARNOMBRE"];
                                ry["NOMTP"] = rz["TANOMBRE"];
                                ry["CANDIS"] = rz["BBCANTID"];

                                ry["CHK"] = "S";
                                if (Convert.ToDouble(rz["BBCANTID"]) == 0)
                                    ry["CHK"] = "N";

                                if (Math.Truncate(Convert.ToDouble(rw["PDCANTID"]) / Convert.ToDouble(txt_canhomologo.Value)) <= Convert.ToDouble(rz["BBCANTID"]))
                                    ry["CANSOL"] = Math.Truncate(Convert.ToDouble(rw["PDCANTID"]) / Convert.ToDouble(txt_canhomologo.Value));
                                else
                                    ry["CANSOL"] = rz["BBCANTID"];

                                dt.Rows.Add(ry);
                                ry = null;
                            }
                        }
                    }
                }

                tbTester = dt;
                rg_tester.DataSource = tbTester;
                rg_tester.DataBind();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpTester.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                dt = null;
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
        protected void btn_loadtester_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridDataItem item in rg_tester.Items)
                {
                    int ln_item = Convert.ToInt32((item.FindControl("txt_itemtest") as RadTextBox).Text);
                    foreach (DataRow rw in tbTester.Rows)
                    {
                        if (Convert.ToInt32(rw["IT"]) == ln_item)
                        {
                            rw["CHK"] = "N";
                            if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                                rw["CHK"] = "S";

                            rw["CANSOL"] = (item.FindControl("txt_tescan") as RadNumericTextBox).Value;
                        }
                    }
                }
                //-----------------
                foreach (DataRow rx in tbTester.Rows)
                {
                    if (Convert.ToString(rx["CHK"]) == "S")
                    {
                        DataRow row = tbItems.NewRow();

                        row["PDLINNUM"] = tbItems.Rows.Count + 1;
                        row["PDTIPPRO"] = Convert.ToString(rx["TP"]);
                        row["PDCLAVE1"] = Convert.ToString(rx["C1"]);
                        row["PDCLAVE2"] = Convert.ToString(rx["C2"]);
                        row["PDCLAVE3"] = Convert.ToString(rx["C3"]);
                        row["PDCLAVE4"] = Convert.ToString(rx["C4"]);
                        row["PDCODCAL"] = ".";
                        row["PDUNDPED"] = "UN";
                        row["PDCANPED"] = Convert.ToDouble(rx["CANSOL"]);
                        row["PDCANTID"] = Convert.ToDouble(rx["CANSOL"]);
                        row["PDPRELIS"] = 0;
                        row["PDDESCUE"] = 0;
                        row["ARNOMBRE"] = Convert.ToString(rx["PRODUCTO"]);
                        row["PDPRECIO"] = 0;
                        row["PDCANDES"] = 0;
                        row["PDCANCAN"] = 0;
                        row["PDASGBOD"] = 0;
                        row["PDASGCOM"] = 0;
                        row["PDASGPRO"] = 0;
                        row["PDESTADO"] = "AC";
                        row["PDCAUSAE"] = ".";
                        row["PDNMUSER"] = "";
                        row["PDFECING"] = System.DateTime.Today;
                        row["PDFECMOD"] = System.DateTime.Today;
                        row["TANOMBRE"] = Convert.ToString(rx["NOMTP"]);
                        tbItems.Rows.Add(row);
                        row = null;
                    }
                }
                (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (rlv_pedidos.Items[0].FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
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
            try
            {
                if (rlv_pedidos.InsertItem != null)
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
                    (rlv_pedidos.InsertItem.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    (rlv_pedidos.InsertItem.FindControl("rg_anexos") as RadGrid).DataBind();
                }
                else
                {
                    if (Convert.ToString(Session["imagePath"]) != "")
                    {
                        Obj.InsertEvidencia(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_pedidos.Items[0].FindControl("txt_lstpaq") as RadTextBox).Text),
                            txt_observaciones.Text, Convert.ToString(Session["imagePath"]), Convert.ToString(Session["UserLogon"]));

                        tbAnexos = Obj.GetEvidencias(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_pedidos.Items[0].FindControl("txt_lstpaq") as RadTextBox).Text));
                        (rlv_pedidos.Items[0].FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                        (rlv_pedidos.Items[0].FindControl("rg_anexos") as RadGrid).DataBind();

                    }
                    else
                    {
                        litTextoMensaje.Text = "No Se Capturo Foto!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
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
    }
}