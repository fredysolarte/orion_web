using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class MaestroBodegas : System.Web.UI.Page
    {
        private DataTable tbLnXBD
        {
            set { ViewState["tbLnXBD"] = value; }
            get { return ViewState["tbLnXBD"] as DataTable; }
        }
        private DataTable tbUsrxBD
        {
            set { ViewState["tbUsrxBD"] = value; }
            get { return ViewState["tbUsrxBD"] as DataTable; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;                
            }
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
            this.OcultarPaginador(rlv_bodegas, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_bodegas_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    BodegaBL Obj = new BodegaBL();
                    try
                    {
                        tbLnXBD = Obj.GetLineaXBodega(null, null, Convert.ToString(Session["CODEMP"]), "--");                        
                        tbUsrxBD = Obj.GetUsuariosXBodega(null, null, "--");                        
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
                    obj_bodegas.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_bodegas.DataBind();
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

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_bodega") as RadTextBox).Text))
                filtro += " AND BDBODEGA = '" + (((RadButton)sender).Parent.FindControl("txt_bodega") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombodega") as RadTextBox).Text))
                filtro += " AND BDNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombodega") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_bodegas.SelectParameters["filter"].DefaultValue = filtro;
            rlv_bodegas.DataBind();
            if ((rlv_bodegas.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_bodegas.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_bodegas_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
                    BodegaBL Obj = new BodegaBL();
                    RadComboBoxItem item = new RadComboBoxItem();
                    ComunBL ObjC = new ComunBL();
                    try
                    {
                        tbLnXBD = Obj.GetLineaXBodega(null, null, Convert.ToString(Session["CODEMP"]), rlv_bodegas.Items[0].GetDataKeyValue("BDBODEGA").ToString());
                        (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbLnXBD;
                        (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                        tbUsrxBD = Obj.GetUsuariosXBodega(null, null, rlv_bodegas.Items[0].GetDataKeyValue("BDBODEGA").ToString());
                        (e.Item.FindControl("rg_usuarios") as RadGrid).DataSource = tbUsrxBD;
                        (e.Item.FindControl("rg_usuarios") as RadGrid).DataBind();


                        (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                        item.Value = "";
                        item.Text = "Seleccionar";
                        (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                        using (IDataReader reader = ObjC.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_pais") as RadComboBox).SelectedValue))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                                itemi.Text = Convert.ToString(reader["CIUDAD"]);
                                (e.Item.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
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
                        item = null;

                    }
                }
            }
            (e.Item.FindControl("rc_ciudad") as RadComboBox).SelectedValue = fila["CDCIUDAD"].ToString();
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
        protected void obj_bodegas_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbArtXBod"] = tbLnXBD;
            e.InputParameters["tbUsrXBod"] = tbUsrxBD;
        }
        protected void rlv_bodegas_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            //Bodegas x Linea
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
            {
                string lc_tippro = Convert.ToString(item["TATIPPRO"].Text);
                foreach (DataRow rw in tbLnXBD.Rows)
                {
                    if (lc_tippro == Convert.ToString(rw["TATIPPRO"]))
                    {
                        rw["ABMNLOTE"] = "N";
                        rw["ABMNELEM"] = "N";
                        rw["ABMNNREL"] = "N";
                        rw["ABMNBONI"] = "N";
                        rw["ABMNCONT"] = "N";
                        rw["ABELEMUAT"] = "N";
                        rw["ABTIPPRO"] = "";
                        rw["ABESTADO"] = "AN";

                        if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                        {
                            rw["ABTIPPRO"] = lc_tippro;
                            rw["ABESTADO"] = "AC";
                        }
                        if ((item.FindControl("chk_lote") as CheckBox).Checked)
                            rw["ABMNLOTE"] = "S";
                        if ((item.FindControl("chk_elemento") as CheckBox).Checked)
                            rw["ABMNELEM"] = "S";
                        if ((item.FindControl("chk_noelemento") as CheckBox).Checked)
                            rw["ABMNNREL"] = "S";
                        if ((item.FindControl("chk_bonxelem") as CheckBox).Checked)
                            rw["ABMNBONI"] = "S";
                        if ((item.FindControl("chk_invconte") as CheckBox).Checked)
                            rw["ABMNCONT"] = "S";
                        if ((item.FindControl("chk_elemauto") as CheckBox).Checked)
                            rw["ABELEMUAT"] = "S";
                    }
                }
            }
            //Bodega x Usuario
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_usuarios") as RadGrid).Items)
            {
                string lc_usuario = Convert.ToString(item["usua_usuario"].Text);
                foreach (DataRow rw in tbUsrxBD.Rows)
                {
                    if (lc_usuario.ToLower() == Convert.ToString(rw["usua_usuario"]).ToLower())
                    {
                        rw["BUCDUSER"] = "";
                        if ((item.FindControl("chk_estado") as CheckBox).Checked)
                            rw["BUCDUSER"] = lc_usuario.ToLower();
                    }
                }
            }
            //Almacen
            obj_bodegas.UpdateParameters["BDALMACE"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_almacen") as CheckBox).Checked)
                obj_bodegas.UpdateParameters["BDALMACE"].DefaultValue = "S";
            //Consignacion
            obj_bodegas.UpdateParameters["BDCONSIG"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_consignacion") as CheckBox).Checked)
                obj_bodegas.UpdateParameters["BDCONSIG"].DefaultValue = "S";

            obj_bodegas.UpdateParameters["CDCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;

        }
        protected void obj_bodegas_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbArtXBod"] = tbLnXBD;
            e.InputParameters["tbUsrXBod"] = tbUsrxBD;
        }
        protected void rlv_bodegas_OnItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            //Bodegas x Linea
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_items") as RadGrid).Items)
            {
                string lc_tippro = Convert.ToString(item["TATIPPRO"].Text);
                foreach (DataRow rw in tbLnXBD.Rows)
                {
                    if (lc_tippro == Convert.ToString(rw["TATIPPRO"]))
                    {
                        rw["ABMNLOTE"] = "N";                       
                        rw["ABMNELEM"] = "N";                        
                        rw["ABMNNREL"] = "N";                        
                        rw["ABMNBONI"] = "N";                        
                        rw["ABMNCONT"] = "N";                        
                        rw["ABELEMUAT"] = "N";

                        if ((item.FindControl("chk_habilita") as CheckBox).Checked)
                            rw["ABTIPPRO"] = lc_tippro;
                        if ((item.FindControl("chk_lote") as CheckBox).Checked)
                            rw["ABMNLOTE"] = "S";
                        if ((item.FindControl("chk_elemento") as CheckBox).Checked)
                            rw["ABMNELEM"] = "S";
                        if ((item.FindControl("chk_noelemento") as CheckBox).Checked)
                            rw["ABMNNREL"] = "S";
                        if ((item.FindControl("chk_bonxelem") as CheckBox).Checked)
                            rw["ABMNBONI"] = "S";
                        if ((item.FindControl("chk_invconte") as CheckBox).Checked)
                            rw["ABMNCONT"] = "S";
                        if ((item.FindControl("chk_elemauto") as CheckBox).Checked)
                            rw["ABELEMUAT"] = "S";
                    }
                }
            }
            //Bodega x Usuario
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rg_usuarios") as RadGrid).Items)
            {
                string lc_usuario = Convert.ToString(item["usua_usuario"].Text);
                foreach (DataRow rw in tbUsrxBD.Rows)
                {
                    if (lc_usuario.ToLower() == Convert.ToString(rw["usua_usuario"]).ToLower())
                    {
                        if ((item.FindControl("chk_estado") as CheckBox).Checked)
                            rw["BUCDUSER"] = lc_usuario.ToLower();
                    }
                }
            }
            //Almacen
            obj_bodegas.InsertParameters["BDALMACE"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_almacen") as CheckBox).Checked)
                obj_bodegas.InsertParameters["BDALMACE"].DefaultValue = "S";

            //Consignacion
            obj_bodegas.InsertParameters["BDCONSIG"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_consignacion") as CheckBox).Checked)
                obj_bodegas.InsertParameters["BDCONSIG"].DefaultValue = "S";

            obj_bodegas.InsertParameters["CDCIUDAD"].DefaultValue = (e.ListViewItem.FindControl("rc_ciudad") as RadComboBox).SelectedValue;
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbLnXBD;
        }
        protected void rg_usuarios_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbUsrxBD;
        }

        protected void rc_pais_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBoxItem item = new RadComboBoxItem();
            ComunBL Obj = new ComunBL();
            try
            {
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(item);
                using (IDataReader reader = Obj.GetCiudades(null, Convert.ToString(Session["CODEMP"]), (sender as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(reader["CDCIUDAD"]);
                        itemi.Text = Convert.ToString(reader["CIUDAD"]);
                        ((sender as RadComboBox).Parent.FindControl("rc_ciudad") as RadComboBox).Items.Add(itemi);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                Obj = null;
            }
        }

        protected void ct_marcas_ItemClick(object sender, RadMenuEventArgs e)
        {
            switch (e.Item.Text)
            {
                case "Quitar Seleccion":
                    foreach (DataRow rw in tbLnXBD.Rows)
                        rw["ABTIPPRO"] = "N";
                    if (rlv_bodegas.InsertItem != null)
                    {                        
                        (rlv_bodegas.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbLnXBD;
                        (rlv_bodegas.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_bodegas.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbLnXBD;
                        (rlv_bodegas.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                    }
                    break;
                case "Seleccionar Todos":
                    foreach (DataRow rw in tbLnXBD.Rows)
                        rw["ABTIPPRO"] = "S";
                    if (rlv_bodegas.InsertItem != null)
                    {
                        (rlv_bodegas.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbLnXBD;
                        (rlv_bodegas.InsertItem.FindControl("rg_items") as RadGrid).DataBind();
                    }
                    else
                    {
                        (rlv_bodegas.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbLnXBD;
                        (rlv_bodegas.Items[0].FindControl("rg_items") as RadGrid).DataBind();
                    }
                    break;
            }
        }
    }
}