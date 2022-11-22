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
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;
using XUSS.BLL.Terceros;

namespace XUSS.WEB.Parametros
{
    public partial class PoliticasComerciales : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbBodegas
        {
            set { ViewState["tbBodegas"] = value; }
            get { return ViewState["tbBodegas"] as DataTable; }
        }
        private DataTable tbCategoria
        {
            set { ViewState["tbCategoria"] = value; }
            get { return ViewState["tbCategoria"] as DataTable; }
        }
        private DataTable tbAriculos
        {
            set { ViewState["tbAriculos"] = value; }
            get { return ViewState["tbAriculos"] as DataTable; }
        }
        private string gc_linea
        {
            set { ViewState["gc_linea"] = value; }
            get { return Convert.ToString(ViewState["gc_linea"]); }
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
            this.OcultarPaginador(rlv_politicas, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_politicas_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    //PedidosBL obj = new PedidosBL();
                    //try
                    //{
                    //    tbItems = obj.GetPedidoDT(null, Convert.ToString(Session["CODEMP"]), 0);
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
                    //finally
                    //{
                    //    obj = null;
                    //}
                    break;

                case "Buscar":
                    obj_politicas.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_politicas.DataBind();
                    break;           
                case "Edit":
                    (rlv_politicas.Items[0].FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = false;
                    (rlv_politicas.Items[0].FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = true;
                    break;
            }
            this.AnalizarCommand(e.CommandName);

        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";
            
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_politicas.SelectParameters["filter"].DefaultValue = filtro;
            rlv_politicas.DataBind();
            if ((rlv_politicas.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_politicas.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_politicas_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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

            PoliticasComercialesBL Obj = new PoliticasComercialesBL();
            try { 
                //tbItems = Obj.GetPoliticaDT(null," ESTADO='AC' ",Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text),Convert.ToString(Session["CODEMP"]));
                tbItems = Obj.GetPoliticaDT(null, " CONVERT(DATE,FECHAFIN,101) >= CONVERT(DATE,GETDATE(),101) AND ESTADO='AC' ", Convert.ToInt32((e.Item.FindControl("txt_codigo") as RadTextBox).Text), Convert.ToString(Session["CODEMP"]));
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();
                (e.Item.FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = true;
                (e.Item.FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { 
                Obj =null;
            }
        }        
        protected void rg_items_OnItemCommand(object sender, GridCommandEventArgs e)
        {            
            if (e.CommandName == "RebindGrid")
            {
               //(rlv_lstprecios.Items[0].FindControl("pnlBuscar") as System.Web.UI.WebControls.Panel).Visible = true;
               //(rlv_lstprecios.Items[0].FindControl("pnDetalle") as System.Web.UI.WebControls.Panel).Visible = false;
               //e.Canceled = true;
            }
            if (e.CommandName == "InitInsert")
            {                
                BodegaBL Obj = new BodegaBL();
                ComunBL ObjC = new ComunBL();
                ArticulosBL ObjA = new ArticulosBL();
                try {
                    tbBodegas = new DataTable();
                    tbBodegas.Columns.Add("CHK", typeof(string));
                    tbBodegas.Columns.Add("COD", typeof(string));
                    tbBodegas.Columns.Add("NOMBRE", typeof(string));

                    tbCategoria = new DataTable();
                    tbCategoria.Columns.Add("CHK", typeof(string));
                    tbCategoria.Columns.Add("COD", typeof(string));
                    tbCategoria.Columns.Add("NOMBRE", typeof(string));

                    tbAriculos = new DataTable();
                    tbAriculos.Columns.Add("CHK", typeof(string));
                    tbAriculos.Columns.Add("TP", typeof(string));
                    tbAriculos.Columns.Add("COD", typeof(string));
                    tbAriculos.Columns.Add("NOMBRE", typeof(string));

                    foreach (DataRow rw in Obj.GetBodegas(null, "", 0, 0).Rows)
                    {
                        DataRow rx = tbBodegas.NewRow();
                        rx["CHK"] = "S";
                        rx["COD"] = Convert.ToString(rw["BDBODEGA"]);
                        rx["NOMBRE"] = Convert.ToString(rw["BDNOMBRE"]);
                        tbBodegas.Rows.Add(rx);
                    }
                    rgBodegas.DataSource = tbBodegas;
                    rgBodegas.DataBind();


                    foreach (DataRow rw in ObjC.GetTiposProducto(null).Rows)
                    {
                        DataRow rx = tbCategoria.NewRow();
                        rx["CHK"] = "S";
                        rx["COD"] = Convert.ToString(rw["TATIPPRO"]);
                        rx["NOMBRE"] = Convert.ToString(rw["TANOMBRE"]);
                        tbCategoria.Rows.Add(rx);
                    }

                    rgCategoria.DataSource = tbCategoria;
                    rgCategoria.DataBind();

                    foreach (DataRow rw in ObjA.GetArticulosDINV(null,"",0,0).Rows)
                    {
                        DataRow rx = tbAriculos.NewRow();
                        rx["CHK"] = "S";
                        rx["TP"] = Convert.ToString(rw["ARTIPPRO"]);
                        rx["COD"] = Convert.ToString(rw["ARCLAVE1"]);
                        rx["NOMBRE"] = Convert.ToString(rw["ARNOMBRE"]);
                        tbAriculos.Rows.Add(rx);
                    }

                    edt_valor_ced.MaxValue = 100;
                    edt_valorasi.MaxValue = 100;
                    if ((rlv_politicas.Items[0].FindControl("rc_tipo") as RadComboBox).SelectedValue == "V")
                    {
                        edt_valor_ced.MaxValue = 1000000;
                        edt_valorasi.MaxValue = 1000000;
                    }

                    if ((rlv_politicas.Items[0].FindControl("rc_condicion") as RadComboBox).SelectedValue == "C" || (rlv_politicas.Items[0].FindControl("rc_condicion") as RadComboBox).SelectedValue == "H")
                    {
                        //edt_valor_ced.MaxValue = 100;
                        //edt_valorasi.MaxValue =100;
                        //if ((rlv_politicas.Items[0].FindControl("rc_tipo") as RadComboBox).SelectedValue == "V")
                        //{
                        //    edt_valor_ced.MaxValue = 1000000;
                        //    edt_valorasi.MaxValue = 1000000;
                        //}

                        string script = "function f(){$find(\"" + mpCedula.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    else
                    {
                        pnBodegas.Visible = true;                                                
                        btn_next.Visible = true;
                        edt_finicialasis.SelectedDate = System.DateTime.Now;
                        edt_ffinalasis.SelectedDate = System.DateTime.Now;
                        string script = "function f(){$find(\"" + mpAsistente.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                    e.Canceled = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    Obj = null;
                    ObjC = null;
                }                
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
        protected void btn_buscar_det_OnClick(object sender, EventArgs e)
        {
            string filtro = "";
            PoliticasComercialesBL Obj = new PoliticasComercialesBL();
            try
            {
                if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue))
                    filtro += " AND P_RTIPPRO = '" + (((RadButton)sender).Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue + "'";

                if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text))
                    filtro += " AND P_RCLAVE1 = '" + (((RadButton)sender).Parent.FindControl("txt_referencia") as RadComboBox).SelectedValue + "'";

                tbItems = Obj.GetPoliticaDT(null, " 1=0 ", Convert.ToInt32((rlv_politicas.Items[0].FindControl("txt_codigo")as RadTextBox).Text), Convert.ToString(Session["CODEMP"]));
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
        protected void btn_next_OnClick(object sender, EventArgs e)
        {            
            rgCategoria.DataSource = tbCategoria;
            rgCategoria.DataBind();            
            btn_guardar.Visible = true;

            foreach (GridDataItem item in (rgBodegas.Items))
            {                               
                    foreach (DataRow rw in tbBodegas.Rows)
                    {
                        if (Convert.ToString(item["COD"].Text) == Convert.ToString(rw["COD"]))
                        {
                            if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                                rw["CHK"] = "S";
                            else
                                rw["CHK"] = "N";
                        }
                    }                
            }

            string script = "function f(){$find(\"" + mpLineaXReferencia.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_guardar_OnClick(object sender, EventArgs e)
        {
            PoliticasComercialesBL Obj = new PoliticasComercialesBL();
            try {
                if (rlv_politicas.InsertItem != null)                
                    Obj.InsertPoliticaDT(null, Convert.ToInt32((rlv_politicas.InsertItem.FindControl("txt_codigo") as RadTextBox).Text), tbBodegas, tbCategoria, tbAriculos, edt_finicialasis.SelectedDate, edt_ffinalasis.SelectedDate, edt_valorasi.Value, Convert.ToString(Session["UserLogon"]));
                 else
                    Obj.InsertPoliticaDT(null, Convert.ToInt32((rlv_politicas.Items[0].FindControl("txt_codigo") as RadTextBox).Text), tbBodegas, tbCategoria, tbAriculos, edt_finicialasis.SelectedDate, edt_ffinalasis.SelectedDate, edt_valorasi.Value, Convert.ToString(Session["UserLogon"]));

                litTextoMensaje.Text = "Descuento Almacenado Correctamente!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void rgCategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string lc_chk = "N";
            GridDataItem item = (GridDataItem)rgCategoria.SelectedItems[0];
            if (item != null)
            {
                TableCell cell = (TableCell)item["COD"];
                string s = cell.Text;
                gc_linea = s;
                DataTable dt = new DataTable();

                dt.Columns.Add("CHK", typeof(string));
                dt.Columns.Add("C1", typeof(string));
                dt.Columns.Add("NM", typeof(string));                
              
                foreach (DataRow dr in tbAriculos.Rows)
                {
                    if (Convert.ToString(dr["TP"]) == s)
                    {
                        DataRow row = dt.NewRow();
                        row["CHK"] = dr["CHK"];
                        row["C1"] = dr["COD"];
                        row["NM"] = dr["NOMBRE"];
                        dt.Rows.Add(row);
                        row = null;
                    }                    
                }
                
                foreach (GridDataItem item_ in rgCategoria.Items)
                {
                    lc_chk = "N";
                    if ((item_.FindControl("chk_habilita") as CheckBox).Checked)
                        lc_chk = "S";

                    foreach (DataRow rw in tbCategoria.Rows)
                    {
                        if ((item_.FindControl("lbl_tp") as Label).Text == Convert.ToString(rw["COD"]))
                        {
                            rw["CHK"] = lc_chk;
                            break;
                        }
                    }
                }
                
                rgCategoria.DataSource = tbCategoria;
                rgCategoria.DataBind();

                rgArticulos.DataSource = dt;
                rgArticulos.DataBind();

                string script = "function f(){$find(\"" + mpLineaXReferencia.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                
            }
        }
        protected void btn_guardar_ced_OnClick(object sender, EventArgs e)
        {
            PoliticasComercialesBL Obj = new PoliticasComercialesBL();
            try
            {
                if (rlv_politicas.InsertItem != null)
                {
                    Obj.InsertPoliticaDT(null, Convert.ToInt32((rlv_politicas.InsertItem.FindControl("txt_codigo") as RadTextBox).Text), ".", ".", ".", ".", ".", ".", edt_valor_ced.Value, txt_identificacion.Text, "",
                                        edt_finicialc.SelectedDate, edt_finicialc.SelectedDate, Convert.ToString(Session["UserLogon"]), "AC", 0, 0, 0);
                }
                else
                {
                    if (chk_terminos.Checked)
                        Obj.InsertPoliticaDT(null, Convert.ToInt32((rlv_politicas.Items[0].FindControl("txt_codigo") as RadTextBox).Text), ".", ".", ".", ".", ".", ".", edt_valor_ced.Value, txt_identificacion.Text, "S",
                                        edt_finicialc.SelectedDate, edt_finicialc.SelectedDate, Convert.ToString(Session["UserLogon"]), "AC", 0, 0, 0);
                    else
                        Obj.InsertPoliticaDT(null, Convert.ToInt32((rlv_politicas.Items[0].FindControl("txt_codigo") as RadTextBox).Text), ".", ".", ".", ".", ".", ".", edt_valor_ced.Value, txt_identificacion.Text, "",
                                        edt_finicialc.SelectedDate, edt_finicialc.SelectedDate, Convert.ToString(Session["UserLogon"]), "AC", 0, 0, 0);
                }
                litTextoMensaje.Text = "Descuento x Cedula Almacenado Correctamente!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

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
        protected void txt_identificacion_OnTextChanged(object sender, EventArgs e)
        {
            TercerosBL Obj = new TercerosBL();
            
            RadComboBoxItem item_ = new RadComboBoxItem();

            try
            {
                if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
                {
                    using (IDataReader reader = Obj.GetTercerosR(null, " TRCODNIT='" + (sender as RadTextBox).Text.Trim() + "' ", 0, 0))
                    {
                        while (reader.Read())
                        {
                            txt_nombre.Text = Convert.ToString(reader["TRNOMBRE"]) + " " + Convert.ToString(reader["TRNOMBR2"]) + " " + Convert.ToString(reader["TRAPELLI"]);
                        }
                    }
                }

                edt_valor_ced.MaxValue = 100;
                if ((rlv_politicas.Items[0].FindControl("rc_tipo") as RadComboBox).SelectedValue == "V")
                    edt_valor_ced.MaxValue = 1000000;

                string script = "function f(){$find(\"" + mpCedula.ClientID + "\").show(); $find(\"" + txt_nombre.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void rgCategoria_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbCategoria;
        }
        protected void rgArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Select":
                    GridDataItem item = (GridDataItem)e.Item;                   
                    break;

                default:
                    string script = "function f(){$find(\"" + mpLineaXReferencia.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    break;
            }
        }
        protected void btn_aplicar_OnClick(object sender, EventArgs e)
        {
            string lc_chk = "N";
            try
            {
                //seleccionar Categorias
                foreach (GridDataItem item in rgCategoria.Items)
                {
                    lc_chk = "N";
                    if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                        lc_chk = "S";

                    foreach (DataRow rw in tbCategoria.Rows)
                    {
                        if ((item.FindControl("lbl_tp") as Label).Text == Convert.ToString(rw["COD"]))
                        {
                            rw["CHK"] = lc_chk;
                            break;
                        }
                    }
                }

                //Articulos
                foreach (GridDataItem item in rgArticulos.Items)
                {
                    lc_chk = "N";
                    if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                        lc_chk = "S";

                    foreach (DataRow rw in tbAriculos.Rows)
                    {
                        if ((item.FindControl("lbl_c1") as Label).Text == Convert.ToString(rw["COD"]))
                        {
                            rw["CHK"] = lc_chk;
                            break;
                        }
                    }
                }

                string script = "function f(){$find(\"" + mpLineaXReferencia.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }
        protected void rgArticulos_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //(source as RadGrid).DataSource = tbCategoria;
        }
        protected void ct_bodegas_ItemClick(object sender,RadMenuEventArgs e)
        {
            string radGridClickedRowIndex;
            radGridClickedRowIndex = Convert.ToString(Request.Form["radGridClickedRowIndex"]);
            String[] sub = radGridClickedRowIndex.Split('-');
            string tipo = sub[0];
            string index = sub[1];
            switch (e.Item.Text)
            {
                case "Seleccionar Todos":
                    if (tipo == "1")
                    {
                        
                            DataTable dt = new DataTable();

                            dt.Columns.Add("CHK", typeof(string));
                            dt.Columns.Add("C1", typeof(string));
                            dt.Columns.Add("NM", typeof(string));

                            foreach (DataRow dr in tbAriculos.Rows)
                            {
                                if (Convert.ToString(dr["TP"]) == Convert.ToString(gc_linea))
                                {
                                    dr["CHK"] = "S";
                                    DataRow row = dt.NewRow();
                                    row["CHK"] = dr["CHK"];
                                    row["C1"] = dr["COD"];
                                    row["NM"] = dr["NOMBRE"];
                                    dt.Rows.Add(row);
                                    row = null;
                                }
                            }
                            rgArticulos.DataSource = dt;
                        
                    }
                    if (tipo == "2")
                    {
                        foreach (DataRow rw in tbCategoria.Rows)
                            rw["CHK"] = "S";
                    }
                    break;
                case "Anular Seleccion":
                    if (tipo == "1")
                    {                                                                                   
                                DataTable dt = new DataTable();

                                dt.Columns.Add("CHK", typeof(string));
                                dt.Columns.Add("C1", typeof(string));
                                dt.Columns.Add("NM", typeof(string));

                                foreach (DataRow dr in tbAriculos.Rows)
                                {
                                    if (Convert.ToString(dr["TP"]) == Convert.ToString(gc_linea))
                                    {
                                        dr["CHK"] = "N";
                                        DataRow row = dt.NewRow();
                                        row["CHK"] = dr["CHK"];
                                        row["C1"] = dr["COD"];
                                        row["NM"] = dr["NOMBRE"];
                                        dt.Rows.Add(row);
                                        row = null;
                                    }                                                                
                                }
                        rgArticulos.DataSource = dt;
                        }                                            
                    if (tipo == "2")
                    {
                        foreach (DataRow rw in tbCategoria.Rows)
                            rw["CHK"] = "N";
                    }
                    break;                    
            }
            rgArticulos.DataBind();
            rgCategoria.DataSource = tbCategoria;
            rgCategoria.DataBind();
            string script = "function f(){$find(\"" + mpLineaXReferencia.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void rg_items_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            int i = 1;
            switch (e.CommandName)
            {
                case "Delete":
                    var ID = item.GetDataKeyValue("ID").ToString();
                    PoliticasComercialesBL Obj = new PoliticasComercialesBL();
                    try {
                        Obj.AnularPoliticaDT(null, Convert.ToInt32(ID), Convert.ToString(Session["UserLogon"]));

                        litTextoMensaje.Text = "Descuento Anulado Correctamente!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show();  Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        }
    }
}