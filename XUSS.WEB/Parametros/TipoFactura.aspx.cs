using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class TipoFactura : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
        }
        private DataTable tbUsuarios
        {
            set { ViewState["tbUsuarios"] = value; }
            get { return ViewState["tbUsuarios"] as DataTable; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["toolbars"] = true;
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
            this.OcultarPaginador(rlv_tfactura, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_tfactura_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    
                    break;
                case "Buscar":
                    obj_tfactura.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_tfactura.DataBind();
                    break;                
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_tfactura_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
            TipoFacturaBL Obj = new TipoFacturaBL();
            try
            {
                tbItems = Obj.GetResolucion(null, Convert.ToString(Session["CODEMP"]), rlv_tfactura.Items[0].GetDataKeyValue("TFTIPFAC").ToString());
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                tbUsuarios = Obj.GetUsuarioxTF(null, rlv_tfactura.Items[0].GetDataKeyValue("TFTIPFAC").ToString());
                (e.Item.FindControl("rgUsuarios") as RadGrid).DataSource = tbUsuarios;
                (e.Item.FindControl("rgUsuarios") as RadGrid).DataBind();
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
            string filtro = " ";

            if ((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue !="-1")
                filtro += "AND TFCLAFAC = '" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue !="-1")
                filtro += "AND TFBODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND UPPER(TFNOMBRE) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text.ToUpper() + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_tfactura.SelectParameters["filter"].DefaultValue = filtro;
            rlv_tfactura.DataBind();
            if ((rlv_tfactura.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_tfactura.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbItems;
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {            
            DataRow row = tbItems.NewRow();
            try
            {
                foreach (DataRow rw in tbItems.Rows)
                {
                    rw["RFESTADO"] = "AN";
                    rw["RFFECMOD"] = System.DateTime.Today;
                }
              
                row["RFCODEMP"] = Convert.ToString(Session["CODEMP"]);
                if (rlv_tfactura.InsertItem != null)
                    row["RFTIPFAC"] = (rlv_tfactura.InsertItem.FindControl("txt_tfactura") as RadTextBox).Text;
                else
                    row["RFTIPFAC"] = (rlv_tfactura.Items[0].FindControl("txt_tfactura") as RadTextBox).Text;
                
                row["RFNRORES"] = ((sender as ImageButton).Parent.FindControl("txt_resolucion") as RadTextBox).Text;
                row["RFFECRES"] = ((sender as ImageButton).Parent.FindControl("edt_fresolucion") as RadDatePicker).SelectedDate;
                row["RFTIPRES"] = ((sender as ImageButton).Parent.FindControl("rc_tipores") as RadComboBox).SelectedValue;
                row["RFFACINI"] = ((sender as ImageButton).Parent.FindControl("txt_finicial") as RadTextBox).Text;
                row["RFFACFIN"] = ((sender as ImageButton).Parent.FindControl("txt_ffinal") as RadTextBox).Text;
                //row["RFINDCFN"] = 0;
                //row["RFINDFFN"] = 0;
                row["RFESTADO"] = "AC";
                row["RFCAUSAE"] = ".";
                row["RFNMUSER"] = Convert.ToString(Session["UserLogon"]);                
                row["RFFECING"] = System.DateTime.Today;
                row["RFFECMOD"] = System.DateTime.Today;

                tbItems.Rows.Add(row);
                

                if (rlv_tfactura.InsertItem != null)
                    (rlv_tfactura.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbItems;
                else
                    (rlv_tfactura.Items[0].FindControl("rg_items") as RadGrid).DataSource = tbItems;
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
        protected void rgUsuarios_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbUsuarios;
        }
        protected void rlv_tfactura_OnItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            //Bodegas x Linea
            foreach (GridDataItem item in (e.ListViewItem.FindControl("rgUsuarios") as RadGrid).Items)
            {
                string lc_usuario = Convert.ToString(item["usua_usuario"].Text);
                foreach (DataRow rw in tbUsuarios.Rows)
                {                                                                                                                   
                    if (lc_usuario == Convert.ToString(rw["usua_usuario"]))
                    {
                        rw["FUCODEMP"] = Convert.ToString(Session["CODEMP"]);
                        rw["FUUSUARIO"] = lc_usuario;
                        rw["FUTIPFAC"] = (e.ListViewItem.FindControl("txt_tfactura") as RadTextBox).Text;

                        if ((item.FindControl("chk_habilita") as CheckBox).Checked)                        
                            rw["FUESTADO"] = "AC";                           
                        else
                            rw["FUESTADO"] = "AN";                           
                    }
                }
            }
        }
        protected void obj_tfactura_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbUsuarios"] = tbUsuarios;
            e.InputParameters["tbResolucion"] = tbItems;            
        }
    }
}