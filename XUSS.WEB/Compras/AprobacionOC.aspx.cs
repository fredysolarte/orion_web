using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Compras
{
    public partial class AprobacionOC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lc_estado = "";
            OrdenesComprasBL Obj = new OrdenesComprasBL();
            try
            {
                using (IDataReader reader = Obj.GetComprasHD(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"])))
                {
                    while (reader.Read())
                        lc_estado = Convert.ToString(reader["CH_ESTADO"]);
                }

                if (lc_estado == "AC")
                {
                    Obj.UpdateCompras(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"]), Convert.ToString(Request.QueryString["IdUser"]));
                    Obj.InsertSeguimiento(null, Convert.ToString(Request.QueryString["CodEmp"]), Convert.ToInt32(Request.QueryString["NroCmp"]), "Purchar Order Approved ", Convert.ToString(Request.QueryString["IdUser"]), "AC");

                    litTextoMensaje.Text = "¡Purchar Order Approved Correctly!";
                    string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                else
                {
                    if (lc_estado != "AC")
                    {
                        litTextoMensaje.Text = "¡The Purchar Order Previus Approveb for Other User!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }


            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            finally
            {
                Obj = null;
            }
            
        }
    }
}