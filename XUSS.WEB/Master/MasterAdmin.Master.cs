using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TESIS.BLL;
using TESIS.BLL.AdminUser;
using System.Data;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace TESIS.WEB
{
    
	public partial class MasterAdmin : System.Web.UI.MasterPage
	{        
        private DataTable tbPermisos
        {
            set { ViewState["tbPermisos"] = value; }
            get { return ViewState["tbPermisos"] as DataTable; }
        }

        protected void Page_Load(object sender, EventArgs e)
		{
			            
			if (!Convert.ToBoolean(Session["Authenticated"]))
			{
				Response.Redirect("~/Login.aspx");
			}
			else
			{
				if (!IsPostBack)
				{
					LoadModules();
                    int moduleId = Convert.ToInt32(Session["moduloSelect"]);
                    int systemId = Convert.ToInt32(Session["SystemId"]);
				}
			}
			

		}
        //protected void OnCallbackUpdate(object sender, RadNotificationEventArgs e)
        //{
        // //   lbl.Text = "Current time: " + DateTime.Now.ToString();
        //}
        private void LoadModules()
        {
            DataTable dt = new DataTable();
            string str = "";
            int i = 1;
            litCopyRight.Text = "Copyright &copy; " + Convert.ToString(DateTime.Now.Year) + "NEXT SYSTEM SAS.";
            str = Session["NomUsuario"] as string;
            UsuariosBL objBL = new UsuariosBL();
            lblUser.Text = "" + str;
            
            //Home
            Telerik.Web.UI.RadPanelItem itemx = new Telerik.Web.UI.RadPanelItem();
            itemx.Text = Convert.ToString("Home");
            itemx.ImageUrl = "~/App_Themes/Tema2/Images/Home2.png";
            itemx.Expanded = false;
            pnl_menu.Items.Add(itemx);
            Telerik.Web.UI.RadPanelItem subitem = new Telerik.Web.UI.RadPanelItem();
            subitem.Text = "Tickets";
            subitem.NavigateUrl = "~/Default.aspx";
            subitem.ImageUrl = "~/App_Themes/Tema2/Images/Next.png";
            pnl_menu.Items[0].Items.Add(subitem);
            subitem = null;
            //Scheduler o Agenda
            Telerik.Web.UI.RadPanelItem subitem_ = new Telerik.Web.UI.RadPanelItem();
            subitem_.Text = "Agenda";
            subitem_.NavigateUrl = "~/Tareas/Scheduler.aspx";
            subitem_.ImageUrl = "~/App_Themes/Tema2/Images/Next.png";
            pnl_menu.Items[0].Items.Add(subitem_);
            subitem_ = null;
            //Quejas/Apelaciones
            Telerik.Web.UI.RadPanelItem subitemq_ = new Telerik.Web.UI.RadPanelItem();
            subitemq_.Text = "Quejas/Apelaciones";
            subitemq_.NavigateUrl = "~/Tareas/QuejasApelaciones.aspx";
            subitemq_.ImageUrl = "~/App_Themes/Tema2/Images/Next.png";
            pnl_menu.Items[0].Items.Add(subitemq_);
            subitemq_ = null;
            //atencion al cliente
            Telerik.Web.UI.RadPanelItem subitema_ = new Telerik.Web.UI.RadPanelItem();
            subitema_.Text = "Atencion al Cliente";
            subitema_.NavigateUrl = "~/Tareas/AtencionCliente.aspx";
            subitema_.ImageUrl = "~/App_Themes/Tema2/Images/Next.png";
            pnl_menu.Items[0].Items.Add(subitema_);
            subitema_ = null;
            //

            dt = objBL.GetListByUserAndSystem("", "", 1, 1, Convert.ToString(Session["UserLogon"]), Convert.ToInt32(Session["SystemId"]));
            tbPermisos = objBL.GetPerimosFormulario(null, Convert.ToString(Session["UserLogon"]), Convert.ToInt32(Session["SystemId"]));

            foreach (DataRow row in dt.Rows)
            {
                Telerik.Web.UI.RadPanelItem item = new Telerik.Web.UI.RadPanelItem();
                item.Text = Convert.ToString(row["modu_nombre"]);
                item.Expanded = false;
                switch (Convert.ToString(row["modu_nombre"]))
                {
                    case "Maestros":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/1-properties.png";
                        break;
                    case "Comercial":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/Scheduler.png";
                        break;
                    case "Inventarios":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/GridView.png";
                        break;
                    case "Recaudo":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/button.png";
                        break;
                    case "Presupuesto":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/versions.png";
                        break;
                    case "Compras":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/ListView.png";
                        break;
                    case "Costos":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/treeview.png";
                        break;
                    case "Reportes":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/msoffice_chart.png";
                        break;
                    case "Administracion":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/ChangeEditingPermissions.png";
                        break;
                    case "Contabilidad":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/table.png";
                        break;
                    case "Gestion":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/gestion.png";
                        break;
                    case "Nomina":
                        item.ImageUrl = "~/App_Themes/Tema2/Images/user-32.png";
                        break;
                        //if (Convert.ToString(row["modu_nombre"]) == "Maestros")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Comercial")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Inventarios")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Recaudo")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Presupuesto")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Compras")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Costos")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Reportes")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Administracion")
                        //    
                        //if (Convert.ToString(row["modu_nombre"]) == "Contabilidad")
                        //    
                }
                pnl_menu.Items.Add(item);
                // Opciones x Modulo
                crearMenu(0, Convert.ToInt32(row["modu_modulo"]), Convert.ToInt32(Session["SystemId"]),0,i);
                i++;
                item = null;
            }
            
            dt = null;
            
        }
		protected void lbtnLogout_Click(object sender, EventArgs e)
		{
			Session.Clear();
			Session.Abandon();
			Response.Redirect("~/Login.aspx");
		}
		private void crearMenu(int padre, int moduleId, int systemId, int level, int index)
		{
			int i = 0;
            string lc_direccion;
			UsuariosBL objBL = new UsuariosBL();
			DataTable dt = objBL.GetOptionsByUserAndModule("", (Session["UserLogon"] as string), moduleId, systemId, padre);

			while (i < dt.Rows.Count)
			{
				if (Convert.ToInt32(dt.Rows[i]["arop_idUnicoPadre"]) == 0)
				{
					level = 0;
                    lc_direccion = "";
                    //HyperLink hlnkP = new HyperLink();
                    //hlnkP.Text = "<span class='FuenteMenuIz'>" + dt.Rows[i][2].ToString() + "</span>";
					if (dt.Rows[i][3].ToString().Equals("~/#"))
					{
                        lc_direccion = "~/" + Request.Url.PathAndQuery + "#" + dt.Rows[i][2].ToString();
                        //hlnkP.NavigateUrl = "~/" + Request.Url.PathAndQuery + "#" + dt.Rows[i][2].ToString();
					}
					else
					{
						//hlnkP.NavigateUrl = dt.Rows[i][3].ToString();
                        lc_direccion = dt.Rows[i][3].ToString() + "?own=" + dt.Rows[i][6].ToString();
                        if (dt.Rows[i][3].ToString().Substring(dt.Rows[i][3].ToString().Length -10,10) == "Visor.aspx")
                            lc_direccion = dt.Rows[i][3].ToString() + "?rpt=" + dt.Rows[i][5].ToString();
                            //hlnkP.NavigateUrl = dt.Rows[i][3].ToString() + "?rpt=" + dt.Rows[i][5].ToString();
					}

                    //if (Request.Url.PathAndQuery == hlnkP.NavigateUrl.Substring(1))
                    //{
                    //    test.Controls.Add(new LiteralControl("<h6 class='selected' id=\"h-menu-" + dt.Rows[i][2].ToString() + "\">"));
                    //}
                    //else
                    //{
                    //    test.Controls.Add(new LiteralControl("<h6 id=\"h-menu-" + dt.Rows[i][2].ToString() + "\">"));
                    //}
					
                    //test.Controls.Add(hlnkP);
					//test.Controls.Add(new LiteralControl("</h6>"));

                    Telerik.Web.UI.RadPanelItem subitem = new Telerik.Web.UI.RadPanelItem();
                    subitem.Text = dt.Rows[i][2].ToString();
                    subitem.ImageUrl = "~/App_Themes/Tema2/Images/Next.png";
                    subitem.NavigateUrl = lc_direccion;                    
                    subitem.Value = dt.Rows[i][6].ToString();
                    pnl_menu.Items[index].Items.Add(subitem);
                    subitem = null;
				}
				else
				{
                    //HyperLink hlnkC = new HyperLink();
                    //hlnkC.Text = dt.Rows[i][2].ToString();
                    //if (dt.Rows[i][3].ToString().Equals("~/#"))
                    //{
                    //    test.Controls.Add(new LiteralControl("<li class=\"collapsible\">"));
                    //    hlnkC.CssClass = "plus";
                    //    hlnkC.NavigateUrl = "~/" + Request.Url.PathAndQuery + "#";
                    //}
                    //else
                    //{
                    //    test.Controls.Add(new LiteralControl("<li>"));
                    //    hlnkC.NavigateUrl = dt.Rows[i][3].ToString();
                    //}
                    //test.Controls.Add(hlnkC);
				}
                //if (dt.Rows.Count > 0)
                //{
                //    if (level == 0)
                //    {
                //        test.Controls.Add(new LiteralControl("<ul id=\"menu-" + dt.Rows[i][2].ToString() + "\" class=\"closed\">"));
                //    }
                //    else
                //    {
                //        test.Controls.Add(new LiteralControl("<ul id=\"menu-" + dt.Rows[i][2].ToString() + "\" class=\"collapsed\">"));
                //    }
                //    crearMenu(Convert.ToInt32(dt.Rows[i]["arop_arbolOpcion"]), moduleId, systemId, level + 1);
                //    test.Controls.Add(new LiteralControl("</ul>"));
                //}
                //if (Convert.ToInt32(dt.Rows[i]["arop_idUnicoPadre"]) != 0)
                //{
                //    test.Controls.Add(new LiteralControl("</li>"));
                //}
				i = i + 1;
			}
		}
		protected void Button5_Click(object sender, EventArgs e)
		{
			UsuariosBL objBl = new UsuariosBL();
			string userLogon = Session["UserLogon"].ToString();
			string password = txtPasswordConfirm.Text;
			

			objBl.ChangePassword("", userLogon, password);

		}
	}
}