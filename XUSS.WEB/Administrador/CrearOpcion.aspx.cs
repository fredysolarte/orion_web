using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI;
using System.Text;
using BLL.Administracion;
using BE.Administracion;
using System.Collections.Generic;
using System.Data;

namespace Administrador
{
    public partial class CrearOpcion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //RadListView1.DataBind();
                ViewState["toolbars"] = true;
            }
        }

        protected void RadListView1_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            //DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;
                }
                else
                {
                    e.Item.FindControl("pnItemMaster").Visible = true;
                    ViewState["toolbars"] = true;                    
                }
            }

            AdmiControlBL obj = new AdmiControlBL();
            try
            {
                obj_detalle.SelectParameters["opci_opcion"].DefaultValue = RadListView1.Items[0].GetDataKeyValue("OpciOpcion").ToString();
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = obj_detalle;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                obj = null;
            }

            RadComboBoxItem item = new RadComboBoxItem();
            AdmiModuloBL objm = new AdmiModuloBL();
            List<AdmiModulo> objlm = new List<AdmiModulo>();
            try {

                (e.Item.FindControl("SelectAndText2") as RadComboBox).Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                (e.Item.FindControl("SelectAndText2") as RadComboBox).Items.Add(item);

                objlm = objm.GetListBySystem(null, null, 0, 0, ((BE.Administracion.AdmiOpcion)((((RadListViewDataItem)(e.Item)).DataItem))).SistSistema);
                foreach (AdmiModulo lst in objlm)
                {
                    RadComboBoxItem itemi = new RadComboBoxItem();
                    itemi.Value = Convert.ToString(lst.ModuModulo);
                    itemi.Text = lst.ModuNombre;
                    (e.Item.FindControl("SelectAndText2") as RadComboBox).Items.Add(itemi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
                objm = null;
            }

            (e.Item.FindControl("SelectAndText2") as RadComboBox).SelectedValue = ((BE.Administracion.AdmiOpcion)((((RadListViewDataItem)(e.Item)).DataItem))).ModuModulo.ToString();
        }

        protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            //if (e.CommandName == "Buscar")
            //{
            //    ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
            //    RadListView1.DataBind();
            //    e.Canceled = true;
            //    return;
            //}
            //if (e.CommandName == "InitInsert")
            //{
            //    ViewState["isClickInsert"] = true;
            //}
            //else
            //{
            //    ViewState["isClickInsert"] = false;                
            //}
            //this.AnalizarCommand(e.CommandName);
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;                    
                    break;

                case "Buscar":
                    ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
                    RadListView1.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        } 

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            RadListView1.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(RadListView1, "RadDataPager1", "BotonesBarra");
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

        protected void AnalizarCommand(string comando)
        {
            //if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            if (comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;
            }
            else
            {
                ViewState["toolbars"] = false;
            }
        }

        protected void BuscarGrilla(object sender, EventArgs e)
        {
            string filtro = "";

            if ((((Button)sender).Parent.FindControl("cb_sistema") as RadComboBox).SelectedValue != "")
                filtro = "AND sist_sistema =" + (((Button)sender).Parent.FindControl("cb_sistema") as RadComboBox).SelectedValue + "";

            if ((((Button)sender).Parent.FindControl("cb_modulo") as RadComboBox).SelectedValue != "")
                filtro += " AND modu_modulo =" + (((Button)sender).Parent.FindControl("cb_modulo") as RadComboBox).SelectedValue + "";
            

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            ObjectDataSource1.SelectParameters["filter"].DefaultValue = filtro;
            RadListView1.DataBind();
            if ((RadListView1.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("<div id=\"box-messages\" class=\"box\">");
                str.AppendLine("<div class=\"messages\">");
                str.AppendLine("<div id=\"message-notice\" class=\"message message-notice\">");
                str.AppendLine("    <div class=\"image\">");
                str.AppendLine("         <img src=\"/App_Themes/Tema2/resources/images/icons/notice.png\" alt=\"Notice\" height=\"32\" />");
                str.AppendLine("		</div>");
                str.AppendLine("    <div class=\"text\">");
                str.AppendLine("        <h6>Información</h6>");
                str.AppendLine("        <span>No se encontraron registros</span>");
                str.AppendLine("    </div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                str.AppendLine("</div>");
                (RadListView1.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void RadComboBox3_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!(e.Value == ""))
            {
                (RadListView1.InsertItem.FindControl("RadTextBox6") as RadTextBox).Text = "Reportes/Visor.aspx";
                (RadListView1.InsertItem.FindControl("RadTextBox6") as RadTextBox).Enabled = false;
            }
            else
            {
                (RadListView1.InsertItem.FindControl("RadTextBox6") as RadTextBox).Text = "";
                (RadListView1.InsertItem.FindControl("RadTextBox6") as RadTextBox).Enabled = true;
            }
        }

        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = obj_detalle;
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            AdmiControlBL obj = new AdmiControlBL();
            try { 
                obj.Add(null,((sender as ImageButton).Parent.FindControl("txt_npermiso") as RadTextBox).Text,((sender as ImageButton).Parent.FindControl("txt_nespcricion") as RadTextBox).Text,
                        Convert.ToInt32((RadListView1.Items[0].FindControl("txt_formulario") as RadTextBox).Text),
                        Convert.ToInt32((RadListView1.Items[0].FindControl("RadComboBox11") as RadComboBox).SelectedValue),
                        Convert.ToInt32((RadListView1.Items[0].FindControl("SelectAndText2") as RadComboBox).SelectedValue),
                        Convert.ToInt32(RadListView1.Items[0].GetDataKeyValue("OpciOpcion").ToString())
                        );
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
}