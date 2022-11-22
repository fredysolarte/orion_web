using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Contabilidad;
using System.Data;

namespace XUSS.WEB.Contabilidad
{
    public partial class PlanillaCuentas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }        
        protected void tv_puc_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            string lc_nombre = Convert.ToString(((System.Data.DataRowView)e.Node.DataItem).Row["PC_CODIGO"]) + "-" + Convert.ToString(((System.Data.DataRowView)e.Node.DataItem).Row["PC_NOMBRE"]);
            e.Node.Text = lc_nombre;
            //if (lc_nombre == "1010020010001-CARTERA ADMINISTRADA")
            //    e.Node.ImageUrl = "~/App_Themes/Tema2/Images/1-open.png";
            //e.Node.ImageUrl = "~/App_Themes/Tema2/Images/1-open.png";
        }

        protected void tv_puc_DataBound(object sender, EventArgs e)
        {
            try {
                foreach (RadTreeNode rtv in (sender as RadTreeView).Nodes)
                {
                    rtv.ImageUrl = "~/App_Themes/Tema2/Images/1-open.png";
                    this.NodoAsignacionImagen(rtv);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private void NodoAsignacionImagen(RadTreeNode rtv)
        {
            foreach (RadTreeNode rtv_ in rtv.Nodes)
            {
                rtv_.ImageUrl = "~/App_Themes/Tema2/Images/1-open.png";
                if (rtv_.Nodes.Count > 0)
                    this.NodoAsignacionImagen(rtv_);
                else
                    rtv_.ImageUrl= "~/App_Themes/Tema2/Images/2-mail.png";
            }
        }

        protected void tv_puc_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            RadTreeNode clickedNode = e.Node;

            switch (e.MenuItem.Value)
            {
                case "Editar":
                    PlanillaBL Obj = new PlanillaBL();
                    try
                    {
                        btn_aceptar.CommandName = "edit";
                        txt_codigo.Text = e.Node.Value;
                        foreach (DataRow rw in (Obj.GetPuc(null, " AND PC_ID=" + txt_codigo.Text) as DataTable).Rows)
                        {
                            txt_nrocta.Text = Convert.ToString(rw["PC_CODIGO"]);
                            txt_nombre.Text = Convert.ToString(rw["PC_NOMBRE"]);
                            rc_naturaleza.SelectedValue = Convert.ToString(rw["PC_NATURALEZA"]);
                            rc_tipo.SelectedValue = Convert.ToString(rw["PC_TIPO"]);
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
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
                case "Nuevo":
                    btn_aceptar.CommandName = "new";
                    txt_codigo.Text = e.Node.Value;
                    txt_nombre.Text = "";
                    txt_nrocta.Text = "";
                    rc_naturaleza.SelectedValue = "-1";
                    rc_tipo.SelectedValue = "-1";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + mpDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                    break;
                case "Inactivar":

                    break;
                case "MarkAsRead":
                    break;
            }
        }

        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            PlanillaBL Obj = new PlanillaBL();
            try {
                if ((sender as RadButton).CommandName == "edit")
                {
                    Obj.UpdatePUC(null,Convert.ToInt32(txt_codigo.Text), Convert.ToString(Session["CODEMP"]), txt_nrocta.Text, txt_nombre.Text, rc_naturaleza.SelectedValue, rc_tipo.SelectedValue, "AC", Convert.ToString(Session["UserLogon"]));
                    litTextoMensaje.Text = "¡Cuenta Actualizada de Manera Correcta!";
                }

                if ((sender as RadButton).CommandName == "new")
                {
                    Obj.InsertPUC(null, Convert.ToInt32(txt_codigo.Text), Convert.ToString(Session["CODEMP"]), txt_nrocta.Text, txt_nombre.Text, rc_naturaleza.SelectedValue, rc_tipo.SelectedValue, "AC", Convert.ToString(Session["UserLogon"]));
                    litTextoMensaje.Text = "¡Cuenta Agregada de Manera Correcta!";
                }
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", true);
                //throw ex;
            }
            finally
            {
                Obj = null;
            }
        }

        protected void ac_ctacontable_TextChanged(object sender, AutoCompleteTextEventArgs e)
        {

        }
    }
}