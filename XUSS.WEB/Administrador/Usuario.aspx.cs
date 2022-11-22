using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BE.Administracion;
using BLL.Administracion;

using System.Text;

namespace Administrador
{
    public partial class Usuario : System.Web.UI.Page
    {
        List<AdmiArbolOpcion> olCheckedNodes = new List<AdmiArbolOpcion>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSystem();
                pnDetails.Visible = false;
                ViewState["toolbars"] = true;
            }
            
        }

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
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();

            olCheckedNodes = objBL.GetListCheckedByRolesUserAndSystem("", "", 1, 1, GetCheckedRoles(), Convert.ToString(RadListView1.SelectedValue), Convert.ToInt32(ddlSistema.SelectedValue));
            RadTreeView1.DataSource = olCheckedNodes;
            if ((ViewState["nodos"] as List<AdmiArbolOpcion>).Count != olCheckedNodes.Count)
                ViewState["nodos"] = olCheckedNodes;
            RadTreeView1.DataBind();

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
                    ViewState["toolbars"] = true;
                    pnDetails.Visible = true;
                    AdmiRolBL objRolBL = new AdmiRolBL();
                    DataTable dt = new DataTable();
                    AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                    try
                    {
                        dt = objRolBL.GetDataTableByUserId("", "", 1, 1, Convert.ToString(RadListView1.SelectedValue));
                        string rolesId = "";
                        CheckBoxList1.DataSource = dt;
                        CheckBoxList1.DataBind();
                        foreach (DataRow dr in dt.Rows)
                        {
                            ListItem item = CheckBoxList1.Items.FindByValue(dr["rolm_rolm"].ToString());
                            item.Selected = Convert.ToBoolean(dr["IsChecked"]);
                            if (item.Selected)
                                rolesId = rolesId + item.Value + ",";
                        }
                        if (rolesId.Equals(String.Empty))
                            rolesId = "0,";

                        olCheckedNodes = objBL.GetListCheckedByRolesUserAndSystem("", "", 1, 1, rolesId.Remove(rolesId.Length - 1, 1), Convert.ToString(RadListView1.SelectedValue), Convert.ToInt32(ddlSistema.SelectedValue));
                        RadTreeView1.DataSource = olCheckedNodes;
                        ViewState["nodos"] = olCheckedNodes;
                        RadTreeView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        objBL = null;
                        dt.Dispose();
                    }
                }
            }
        }

        protected void RadListView1_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;
                pnDetails.Visible = false;
            }
            else
            {
                ViewState["isClickInsert"] = false;
                pnDetails.Visible = true;
            }
            switch (e.CommandName)
            {
                case "Buscar":
                    odsUsuarios.SelectParameters["filter"].DefaultValue = "1=0";
                    RadListView1.DataBind();
                    pnDetails.Visible = false;
                    e.Canceled = true;
                    break;
                case RadListView.InitInsertCommandName:
                    RadTreeView1.DataSource = null;
                    RadTreeView1.Nodes.Clear();
                    CheckBoxList1.Enabled = true;
                    pnDetails.Visible = false;
                    CheckBoxList1.ClearSelection();
                    break;
                case RadListView.EditCommandName:
                    RadTreeView1.Enabled = true;
                    CheckBoxList1.Enabled = true;
                    break;
                case "ShowModal":
                    ((Panel)(e.ListViewItem.FindControl("pnPopUp"))).Enabled = true;
                    ((AjaxControlToolkit.ModalPopupExtender)(e.ListViewItem.FindControl("ModalPopup"))).Show();
                    break;
                case "ResetPassword":
                    string deafultPassword = ((RadTextBox)(e.ListViewItem.FindControl("TextBox1"))).Text.Substring(0, 3) + ((RadNumericTextBox)(e.ListViewItem.FindControl("txtIdentification"))).Text.Substring(0, 5);
                    ((RadTextBox)(e.ListViewItem.FindControl("txtPassword"))).Text = deafultPassword;
                    break;
                case "Accept":
                    if (((TextBox)(e.ListViewItem.FindControl("txtSetPassword"))).Text.Equals(""))
                    {
                        ((RequiredFieldValidator)(e.ListViewItem.FindControl("rfvSetPassword"))).Enabled = true;
                        ((RequiredFieldValidator)(e.ListViewItem.FindControl("rfvSetPassword"))).Validate();
                        ((AjaxControlToolkit.ModalPopupExtender)(e.ListViewItem.FindControl("ModalPopup"))).Show();
                    }
                    else if (((TextBox)(e.ListViewItem.FindControl("txtPasswordConfirm"))).Text.Equals(""))
                    {
                        ((RequiredFieldValidator)(e.ListViewItem.FindControl("rfvPasswordConfirm"))).Enabled = true;
                        ((RequiredFieldValidator)(e.ListViewItem.FindControl("rfvPasswordConfirm"))).Validate();
                        ((AjaxControlToolkit.ModalPopupExtender)(e.ListViewItem.FindControl("ModalPopup"))).Show();
                    }
                    else
                    {
                        ((CompareValidator)e.ListViewItem.FindControl("cmpPasswords")).Enabled = true;
                        ((CompareValidator)e.ListViewItem.FindControl("cmpPasswords")).Validate();
                        ((RequiredFieldValidator)(e.ListViewItem.FindControl("rfvSetPassword"))).Enabled = false;
                        if (!((CompareValidator)e.ListViewItem.FindControl("cmpPasswords")).IsValid)
                            ((AjaxControlToolkit.ModalPopupExtender)(e.ListViewItem.FindControl("ModalPopup"))).Show();
                        else
                        {
                            ((RadTextBox)(e.ListViewItem.FindControl("txtPassword"))).Text = ((TextBox)(e.ListViewItem.FindControl("txtSetPassword"))).Text;
                            ((CheckBox)(e.ListViewItem.FindControl("chkChangePassword"))).Checked = false;
                        }
                    }
                    break;
                default:
                    if (e.CommandArgument.Equals("Insertar"))
                        pnDetails.Visible = false;
                    else
                    {
                        RadTreeView1.Enabled = false;
                        CheckBoxList1.Enabled = false;
                    }
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }

        protected void odsUsuarios_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string userId = Convert.ToString(e.ReturnValue);

            List<AdmiRolxUsuario> olAdmiRolxUsuario = new List<AdmiRolxUsuario>();
            AdmiUsuarioBL objAdmiUsuarioBL = new AdmiUsuarioBL();
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    AdmiRolxUsuario oEntity = new AdmiRolxUsuario();
                    oEntity.RolmRolm = Convert.ToInt32(item.Value);
                    oEntity.UsuaUsuario = userId;
                    oEntity.LogsFecha = DateTime.Now;
                    oEntity.LogsUsuario = Convert.ToInt32(Session["UserId"]);
                    olAdmiRolxUsuario.Add(oEntity);
                }
            }
            if (ViewState["nodos"] != null)
            {
                objAdmiUsuarioBL.SetRolesByUser("", olAdmiRolxUsuario, (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(t => t.IsChecked), userId);
            }
        }

        protected void odsUsuarios_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            List<AdmiRolxUsuario> olAdmiRolxUsuario = new List<AdmiRolxUsuario>();
            AdmiUsuarioBL objAdmiUsuarioBL = new AdmiUsuarioBL();

            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                {
                    AdmiRolxUsuario oEntity = new AdmiRolxUsuario();
                    oEntity.RolmRolm = Convert.ToInt32(item.Value);
                    oEntity.UsuaUsuario = Convert.ToString(RadListView1.SelectedValue);
                    oEntity.LogsFecha = DateTime.Now;
                    oEntity.LogsUsuario = Convert.ToInt32(Session["UserId"]);
                    olAdmiRolxUsuario.Add(oEntity);
                }
            }

            objAdmiUsuarioBL.SetRolesByUser("", olAdmiRolxUsuario, (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.IsChecked), Convert.ToString(RadListView1.SelectedValue));

        }


        protected void RadTreeView1_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
            {

                (ViewState["nodos"] as List<AdmiArbolOpcion>).Find(item => item.AropIdOriginal == Convert.ToInt32(node.Value) &&
                                            item.AropEntidad.Equals(node.Category) &&
                                            item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue)).IsChecked = node.Checked;
            }

            RadTreeView1.DataSource = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue));
            RadTreeView1.DataBind();
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

        protected void ddlSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ViewState["nodos"] as List<AdmiArbolOpcion>).Exists(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue)))
            {
                RadTreeView1.DataSource = (ViewState["nodos"] as List<AdmiArbolOpcion>).FindAll(item => item.SistSistema == Convert.ToInt32(ddlSistema.SelectedValue));
                RadTreeView1.DataBind();
            }
            else
            {
                AdmiArbolOpcionBL objBL = new AdmiArbolOpcionBL();
                olCheckedNodes = objBL.GetListCheckedByRolesUserAndSystem("", "", 1, 1, GetCheckedRoles(), Convert.ToString(RadListView1.SelectedValue), Convert.ToInt32(ddlSistema.SelectedValue));
                RadTreeView1.DataSource = olCheckedNodes;
                (ViewState["nodos"] as List<AdmiArbolOpcion>).AddRange(olCheckedNodes);
                RadTreeView1.DataBind();
            }
        }

        private string GetCheckedRoles()
        {
            string rolesId = "";
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected)
                    rolesId = rolesId + item.Value + ",";
            }
            if (rolesId.Equals(String.Empty))
                rolesId = "0,";
            return rolesId.Remove(rolesId.Length - 1, 1);
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(RadListView1, "RadDataPager1", "BotonesBarra");
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
            string filtro = " AND 1=1";

            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txt_usuario") as RadTextBox).Text))
                filtro += "AND usua_usuario = '" + (((Button)sender).Parent.FindControl("txt_usuario") as RadTextBox).Text + "'";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            odsUsuarios.SelectParameters["filter"].DefaultValue = filtro;
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