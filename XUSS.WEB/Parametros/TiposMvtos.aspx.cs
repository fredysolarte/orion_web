using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace XUSS.WEB.Parametros
{
    public partial class TiposMvtos : System.Web.UI.Page
    {
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
        protected void rlv_tmovitos_ItemDataBound(object sender, RadListViewItemEventArgs e)
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_tmovitos, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_tmovitos_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;

                    break;
                case "Buscar":
                    obj_tmovito.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_tmovitos.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }        
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";

            //if ((((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue != "-1")
            //    filtro += "AND TFCLAFAC = '" + (((RadButton)sender).Parent.FindControl("rc_tipfac") as RadComboBox).SelectedValue + "'";

            //if ((((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue != "-1")
            //    filtro += "AND TFBODEGA = '" + (((RadButton)sender).Parent.FindControl("rc_bodega") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text))
                filtro += "AND TMCDTRAN = '" + (((RadButton)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text.ToUpper() + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND UPPER(TMNOMBRE) LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text.ToUpper() + "%'";

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_tmovito.SelectParameters["filter"].DefaultValue = filtro;
            rlv_tmovitos.DataBind();
            if ((rlv_tmovitos.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_tmovitos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
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

        protected void rlv_tmovitos_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            obj_tmovito.UpdateParameters["TMREQDOC"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_rdocumento") as CheckBox).Checked)
                obj_tmovito.UpdateParameters["TMREQDOC"].DefaultValue = "S";

            obj_tmovito.UpdateParameters["TMMOVMAN"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_mmanual") as CheckBox).Checked)
                obj_tmovito.UpdateParameters["TMMOVMAN"].DefaultValue = "S";
        }

        protected void rlv_tmovitos_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_tmovito.InsertParameters["TMREQDOC"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_rdocumento") as CheckBox).Checked)
                obj_tmovito.InsertParameters["TMREQDOC"].DefaultValue = "S";

            obj_tmovito.InsertParameters["TMMOVMAN"].DefaultValue = "N";
            if ((e.ListViewItem.FindControl("chk_mmanual") as CheckBox).Checked)
                obj_tmovito.InsertParameters["TMMOVMAN"].DefaultValue = "S";
        }
    }
}