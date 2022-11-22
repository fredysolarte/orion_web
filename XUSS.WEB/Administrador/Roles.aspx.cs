using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BE.Administracion;
using BLL.Administracion;
using System.Text;

namespace Administrador.Roles
{
    public partial class Roles : System.Web.UI.Page
    {
        //public override void AddControls()
        //{
        //    base.AddControls();
        //    FormControls control = new FormControls("RadListView1", "ListView", null);
        //    Lista.Add(control);
        //    control = new FormControls("RadDataPager1", "Paginador", "RadListView1");
        //    Lista.Add(control);
        //    control = new FormControls("iBtnInitInsert", "Insert", "RadListView1");
        //    Lista.Add(control);
        //    control = new FormControls("iBtnEdit", "Edit", "RadListView1");
        //    Lista.Add(control);
        //    control = new FormControls("iBtnDelete", "Delete", "RadListView1");
        //    Lista.Add(control);
        //}

        #region Fields
        List<AdmiArbolOpcion> olCheckedNodes = new List<AdmiArbolOpcion>();
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadControls();
                ViewState["toolbars"] = true;
            }
            //else
            //    pnDetails.Visible = false;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (RadListView1.SelectedIndexes.Count == 0)
                RadListView1.SelectedIndexes.Add(0);
        }

        protected void RadListView1_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {

                
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;
                }
                else
                {
                    AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                    ViewState["toolbars"] = true;
                    olCheckedNodes = objBL.GetListCheckedBySystemModuleAndRol("", "", 1, 1, Convert.ToInt32(ddlSistema.SelectedValue), Convert.ToInt32(ddlModulo.SelectedValue), Convert.ToInt32(RadListView1.SelectedValue));
                    
                    RadTreeView1.DataSource = olCheckedNodes.FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue) && item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue));
                    
                    ViewState["nodos"] = olCheckedNodes;
                    //Session["test"] = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.IsChecked);
                    RadTreeView1.DataBind();
                    pnDetails.Visible = true;
                    e.Item.FindControl("pnItemMaster").Visible = true;
                    ddlSistema.SelectedIndex = 0;
                    ddlModulo.SelectedIndex = 0;

                }
            }
        }

        protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
                RadTreeView1.UncheckAllNodes();
                if (ViewState["nodos"] != null)
                    (ViewState["nodos"] as List<AdmiArbolOpcion>).ForEach(item => item.IsChecked = false);
                pnDetails.Visible = false;
            }
            else
            {
                ViewState["isClickInsert"] = false;
                //pnDetails.Visible = true;
            }
            switch (e.CommandName)
            {

                case RadListView.InitInsertCommandName:
                    //pnDetails.Visible = false;
                    RadTreeView1.Enabled = true;
                    break;
                case RadListView.EditCommandName:
                    pnDetails.Visible = true;
                    RadTreeView1.Enabled = true;
                    break;
                case "Buscar":
                    odsRoles.SelectParameters["filter"].DefaultValue = "1=0";
                    RadListView1.DataBind();
                    pnDetails.Visible = false;
                    e.Canceled = true;
                    break;
                default:
                    RadTreeView1.Enabled = false;
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void odsRoles_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            AdmiRolBL objBL = new AdmiRolBL();
            int idRol = Convert.ToInt32(e.ReturnValue);
            if (ViewState["nodos"] != null)
                if ((ViewState["nodos"] as List<AdmiArbolOpcion>).Exists(item => item.IsChecked))
                    objBL.UpdateConfiguration("", (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.IsChecked), idRol);
        }

        protected void odsRoles_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            AdmiRolBL objBL = new AdmiRolBL();
            List<AdmiArbolOpcion> olEntity = new List<AdmiArbolOpcion>();

            objBL.UpdateConfiguration("", (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.IsChecked), Convert.ToInt32(RadListView1.SelectedValue));
            
        }

        protected void RadTreeView1_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            if (!e.Node.Category.Equals("F"))
            {
                if (((AdmiArbolOpcion)e.Node.DataItem).IsChecked && e.Node.ParentNode != null && e.Node.ParentNode.Category.Equals("F"))
                {
                    e.Node.ParentNode.Checkable = true;
                    e.Node.Checked = ((AdmiArbolOpcion)e.Node.DataItem).IsChecked;
                }
                else
                    e.Node.Checked = ((AdmiArbolOpcion)e.Node.DataItem).IsChecked;
            }
            else 
                e.Node.Checkable = false;
        }

        protected void RadTreeView1_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
            {

                (ViewState["nodos"] as List<AdmiArbolOpcion>).Find(item => item.AropIdOriginal == Convert.ToInt32(node.Value) &&
                                            item.AropEntidad.Equals(node.Category) &&
                                            item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue) &&
                                            item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue)).IsChecked = node.Checked;
            }

            RadTreeView1.DataSource = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue) && item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue));
            RadTreeView1.DataBind();
        }
       
        protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ViewState["nodos"] as List<AdmiArbolOpcion>).Exists(item => item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue)))
            {
                RadTreeView1.DataSource = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue) && item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue));                
                RadTreeView1.DataBind();
            }
            else
            {
                AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                olCheckedNodes = objBL.GetListCheckedBySystemModuleAndRol("", "", 1, 1, Convert.ToInt32(ddlSistema.SelectedValue), Convert.ToInt32(ddlModulo.SelectedValue), Convert.ToInt32(RadListView1.SelectedValue));
                RadTreeView1.DataSource = olCheckedNodes.FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue) && item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue));
                (ViewState["nodos"] as List<AdmiArbolOpcion>).AddRange(olCheckedNodes);
                RadTreeView1.DataBind();
            }
        }

        protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModules();
            if (ddlModulo.Items.Count > 0)
            {
                if ((ViewState["nodos"] as List<AdmiArbolOpcion>).Exists(item => item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue)))
                {
                    RadTreeView1.DataSource = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue) && item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue));
                    RadTreeView1.DataBind();
                }
                else
                {
                    AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                    olCheckedNodes = objBL.GetListCheckedBySystemModuleAndRol("", "", 1, 1, Convert.ToInt32(ddlSistema.SelectedValue), Convert.ToInt32(ddlModulo.SelectedValue), Convert.ToInt32(RadListView1.SelectedValue));
                    RadTreeView1.DataSource = olCheckedNodes.FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue) && item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue));
                    (ViewState["nodos"] as List<AdmiArbolOpcion>).AddRange(olCheckedNodes);
                    RadTreeView1.DataBind();
                }
            }
            else
            {
                AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                RadTreeView1.DataSource = objBL.GetListCheckedBySystemModuleAndRol("", "", 1, 1, Convert.ToInt32(ddlSistema.SelectedValue), 0, Convert.ToInt32(RadListView1.SelectedValue)).FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue) && item.ModuModulo == Convert.ToInt32(ddlModulo.SelectedValue));
                RadTreeView1.DataBind();
            }
        }
        #endregion

        #region Methods

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(RadListView1, "RadDataPager1", "BotonesBarra");
        }

        private void LoadControls()
        {
            pnDetails.Visible = false;
            LoadSystem();
            LoadModules();
        }

        public void LoadModules()
        {
            AdmiModuloBL objBL = new AdmiModuloBL();
            try
            {
                ddlModulo.DataSource = objBL.GetListBySystem("", "", 1, 1, Convert.ToInt32(ddlSistema.SelectedValue));
                ddlModulo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objBL = null;
            }
        }

        public void LoadSystem()
        {
            AdmiSistemaBL objBL = new AdmiSistemaBL();
            try
            {
                ddlSistema.DataSource = objBL.GetList("", "", 1, 1);
                ddlSistema.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objBL = null;
            }
        }
        #endregion

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

        public bool GetBoolean(object value)
        {
            bool retValue;
            if (value == null || value == System.DBNull.Value)
                retValue = false;
            else if (Convert.ToInt32(value) == 1)
                retValue = true;
            else
                retValue = false;

            return retValue;
        }

        protected void BuscarGrilla(object sender, EventArgs e)
        {
            string filtro = "";

            if ((((Button)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text != "")
                filtro = "AND rolm_rolm =" + (((Button)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text + "";
            if ((((Button)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text != "")
                filtro = "AND rolm_nombre LIKE '%" + (((Button)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            odsRoles.SelectParameters["filter"].DefaultValue = filtro;
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
        
    }
}
