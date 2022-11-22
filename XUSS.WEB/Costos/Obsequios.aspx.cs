using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using XUSS.BLL.Costos;

namespace XUSS.WEB.Costos
{
    public partial class Obsequios : System.Web.UI.Page
    {
        private Telerik.Web.UI.RadListViewDataItem rliItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (rvl_descuento.InsertItem != null)
            {
                rliItems = rvl_descuento.InsertItem;
            }

            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
        }

        protected void btn_buscar_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";



            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("edt_cedula") as RadTextBox).Text))
            {
                filtro += "AND ";
                filtro += "AND CONDICION_1 = '" + (((Button)sender).Parent.FindControl("edt_cedula") as RadTextBox).Text + "'";
            }

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            obj_obsequiocedula.SelectParameters["filter"].DefaultValue = filtro;
            rvl_descuento.DataBind();
            if ((rvl_descuento.Controls[0] is RadListViewEmptyDataItem))
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
                (rvl_descuento.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void rvl_descuento_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
            }
            else
            {
                ViewState["isClickInsert"] = false;
            }
            switch (e.CommandName)
            {
                case "Buscar":
                    obj_obsequiocedula.SelectParameters["filter"].DefaultValue = "1=0";
                    rvl_descuento.DataBind();
                    e.Canceled = true;
                    break;
                default:
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rvl_descuento, "RadDataPager1", "BotonesBarra");
        }

        public string GetEstado(object a)
        {
            if (a == DBNull.Value || a == null || a.ToString() == "")
                return null;
            switch (Convert.ToString(a))
            {
                case "AC":
                    return "Activo";
                case "AN":
                    return "Anulado";
                case "CE":
                    return "Cerrado";
                default:
                    return null;
            }
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
            if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;
            }
            else
            {
                ViewState["toolbars"] = false;

            }
        }

        protected void rtbCodigo_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace((((RadNumericTextBox)(rliItems.FindControl("rtbCodigo"))).Text)))
            {
                ObsequiosBL obj = new ObsequiosBL();
                ((RadTextBox)(rliItems.FindControl("rtbNombre"))).Text = obj.GetNombreTerceros(null, " WHERE TRCODEMP ='001' AND TRCODNIT ='" + ((RadNumericTextBox)(rliItems.FindControl("rtbCodigo"))).Text + "'");
            }
        }

        protected void rvl_descuento_ItemDataBound(object sender, RadListViewItemEventArgs e)
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

        protected void obj_obsequiocedula_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {

        }

        protected void obj_obsequiocedula_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            ObsequiosBL.InsertTercero(null, "001", 0,
                                            ((RadTextBox)rvl_descuento.InsertItem.FindControl("rtbNombre")).Text, null, null, null,
                                            ((RadNumericTextBox)rvl_descuento.InsertItem.FindControl("rtbCodigo")).Text, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null, null, null, null, null, null,
                                                         null, null, null, null,
                                                         null, null, null, null, Convert.ToDateTime("01/01/1990"), null, null, null, null);



        }
    }
}