using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TESIS.BLL.AdminUser;
using System.Data;
using Telerik.Web.UI;


namespace TESIS.WEB
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            
		}

		//protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        protected void LoginButton_Click(object sender, EventArgs e)
		{
			string str = "";
            DataTable dt = new DataTable();
			try
			{                
        
                //if (UsuariosBL.GetUsuarioExiste(null, Login1.UserName, Login1.Password))
                //{
                //    Session["UserLogon"] = Login1.UserName;
                //    Session["Authenticated"] = true;
                //    Session["UserId"] = 1;
                //    Session["SystemId"] = Convert.ToInt32((Login1.FindControl("ddlSistema") as DropDownList).SelectedValue); 
                //    Response.Redirect("Default.aspx");
                //}
                //else
                //{
                //    throw new Exception("Datos Incorrectos");
                //}


                //Session["SystemId"] = Convert.ToInt32((Login1.FindControl("ddlSistema") as RadComboBox).SelectedValue);
                //dt = UsuariosBL.GetUsuarioExiste(null, Login1.UserName, Login1.Password, Convert.ToInt32(Session["SystemId"]));
                
                Session["SystemId"] = Convert.ToInt32(ddlSistema.SelectedValue);
                dt = UsuariosBL.GetUsuarioExiste(null, username.Value, pass.Value, Convert.ToInt32(Session["SystemId"]));
                if (dt.Rows.Count > 0)
                {
                    //Session["UserLogon"] = Login1.UserName;
                    Session["UserLogon"] = username.Value;
                    Session["NomUsuario"] = dt.Rows[0][3].ToString();
                    Session["CODEMP"] = dt.Rows[0]["sist_identifica"].ToString();
                    Session["Authenticated"] = true;
                    Session["UserId"] = 1;
                    Session["UserDBA"] = dt.Rows[0]["usua_administrador"];
                    Session["fto_date"] = dt.Rows[0]["fto_date"];
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    throw new Exception("Datos Incorrectos");
                }
			}
			catch (Exception ex)
			{
				str = str + "<div class=\"messages\">";
				str = str + "   <div id=\"message-error\" class=\"message message-error\">";
				str = str + "	    <div class=\"image\">";
				str = str + "		    <img src=\"App_Themes/Tema2/resources/images/icons/error.png\" alt=\"Error\" height=\"32\" />";
				str = str + "	    </div>";
				str = str + "	    <div class=\"text\">";
				str = str + "		    <h6>Error</h6>";
				str = str + "	        <span>" + ex.Message + "</span>";
				str = str + "	    </div>";
				str = str + "   </div>";
				str = str + "</div>";
				FailureText.Text = str;
			}

		}
	}
}