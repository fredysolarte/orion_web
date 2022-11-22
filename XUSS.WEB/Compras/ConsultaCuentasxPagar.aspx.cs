using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Compras;

namespace XUSS.WEB.Compras
{
    public partial class ConsultaCuentasxPagar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rg_consulta_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            string ln_nrofac = dataItem.GetDataKeyValue("FD_NROFACTURA").ToString();
            int ln_nrocmp = Convert.ToInt32(dataItem.GetDataKeyValue("FD_NROCMP").ToString());
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        OrdenesComprasBL Obj = new OrdenesComprasBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetDetalleFacturas(null, "", Convert.ToString(Session["CODEMP"]), ln_nrocmp, ln_nrofac);
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
                    }
            }
        }
        protected void rg_consulta_ItemCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            string url = "";
            try
            {
                switch (e.CommandName)
                {
                    case "link_tr":
                            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Documento=" + (item.FindControl("lbl_tercero") as Label).Text;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);                                               
                        break;
                    case "link_cmp":
                            url = "http://" + HttpContext.Current.Request.Url.Authority + "/Compras/OrdenesCompras.aspx?NroCmp=" + (item.FindControl("lbl_cmp") as Label).Text;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                item = null;
            }

        }
    }
}