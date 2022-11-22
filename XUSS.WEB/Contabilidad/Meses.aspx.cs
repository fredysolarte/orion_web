using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Contabilidad
{
    public partial class Meses : System.Web.UI.Page
    {
        private DataTable tbItems
        {
            set { ViewState["tbItems"] = value; }
            get { return ViewState["tbItems"] as DataTable; }
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
            this.OcultarPaginador(rlv_meses, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_mes_OnItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    MesesBL Obj = new MesesBL();
                    try
                    {
                        ViewState["isClickInsert"] = true;
                        tbItems = Obj.GetMeses(null, Convert.ToString(Session["CODEMP"]), 0);
                        foreach (DataColumn cl in tbItems.Columns)
                            cl.ReadOnly = false;
                        int i = 1;
                        for (i = 1; i <= 12; i++)
                        {
                            DataRow rw = tbItems.NewRow();
                            rw["MA_ID"] = 0;                            
                            rw["MA_CODEMP"] = Convert.ToString(Session["CODEMP"]);
                            rw["MA_MES"] = i;
                            rw["MA_ANO"] = 0;
                            rw["MA_USUARIO"] = ".";
                            rw["MA_FECING"] = System.DateTime.Today;
                            switch (i)
                            {
                                case 1: rw["NOM_MES"] = "Enero"; break;
                                case 2: rw["NOM_MES"] = "Febrero"; break;
                                case 3: rw["NOM_MES"] = "Marzo"; break;
                                case 4: rw["NOM_MES"] = "Abril"; break;
                                case 5: rw["NOM_MES"] = "Mayo"; break;
                                case 6: rw["NOM_MES"] = "Junio"; break;
                                case 7: rw["NOM_MES"] = "Julio"; break;
                                case 8: rw["NOM_MES"] = "Agosto"; break;
                                case 9: rw["NOM_MES"] = "Septiembre"; break;
                                case 10: rw["NOM_MES"] = "Octubre"; break;
                                case 11: rw["NOM_MES"] = "Noviembre"; break;
                                case 12: rw["NOM_MES"] = "Diciembre"; break;                                
                            }
                            tbItems.Rows.Add(rw);
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
                    break;
                case "Buscar":
                    obj_anos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_meses.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void rlv_mes_OnItemDataBound(object sender, RadListViewItemEventArgs e)
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
            MesesBL Obj = new MesesBL();
            try
            {
                tbItems = Obj.GetMeses(null, Convert.ToString(Session["CODEMP"]), Convert.ToInt32((rlv_meses.Items[0].FindControl("txt_ano") as RadTextBox).Text) );
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
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";           

            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_anos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_meses.DataBind();
            if ((rlv_meses.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_meses.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }        
        protected void rg_items_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbItems;
        }
        protected void obj_anos_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbMes"] = tbItems;
        }
    }
}