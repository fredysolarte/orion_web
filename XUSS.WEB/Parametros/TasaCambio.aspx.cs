using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class TasaCambio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    obj_tasacambio.SelectParameters["inFecha"].DefaultValue = Convert.ToString(Request.QueryString["Documento"]);
                    rg_items.DataBind();
                }
            }
        }
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            TasaCambioBL Obj = new TasaCambioBL();
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        if (Obj.ExisteTasa(null, (e.Item.FindControl("rc_moneda") as RadComboBox).SelectedValue, (e.Item.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate))
                        {
                            litTextoMensaje.Text = "Ya Existe Tasa para Esa Fecha";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        }
                        else
                        {
                            Obj.InsertTasaCambio(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_moneda") as RadComboBox).SelectedValue, (e.Item.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate, (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value, Convert.ToString(Session["UserLogon"]), "AC");
                        }
                        break;
                    case "Update":
                        Obj.UpdateTasaCambio(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_moneda") as RadComboBox).SelectedValue, (e.Item.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate, (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value, Convert.ToString(Session["UserLogon"]), "AC");
                        break;
                    case "Delete":
                        Obj.UpdateTasaCambio(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_moneda") as RadComboBox).SelectedValue, (e.Item.FindControl("edt_fSolicitud") as RadDatePicker).SelectedDate, (e.Item.FindControl("txt_valor") as RadNumericTextBox).Value, Convert.ToString(Session["UserLogon"]), "AN");
                        break;
                    case "Edit":
                        //(e.Item.FindControl("txt_codigo") as RadTextBox).Text = 
                        break;
                }
                rg_items.DataBind();
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
    }
}