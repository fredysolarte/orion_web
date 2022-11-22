using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Consultas;
using System.Data;
using Telerik.Web.UI.ExportInfrastructure;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Consultas
{
    public partial class InventarioDisponible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            string filtro = " AND 1=1";
            string lc_in = "";
            
            var cll_linea = rc_linea.CheckedItems;
            if (cll_linea.Count != 0)
            {
                lc_in = "";
                filtro += " AND AR.ARTIPPRO IN (";
                foreach (var item in cll_linea)                
                    lc_in += "'" + Convert.ToString(item.Value) + "',";                

                filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";
            }

            if (!string.IsNullOrWhiteSpace(txt_barras.Text))
            {
                if (txt_barras.Text.Length == 12)
                    filtro += " AND (AR.ARCLAVE1+AR.ARCLAVE2+AR.ARCLAVE3+AR.ARCLAVE4) IN (SELECT BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 FROM TBBARRA WITH(NOLOCK) WHERE BCODIGO LIKE '%" + txt_barras.Text.Substring(0, 11) + "%')";
                else
                    filtro += " AND (AR.ARCLAVE1+AR.ARCLAVE2+AR.ARCLAVE3+AR.ARCLAVE4) IN (SELECT BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 FROM TBBARRA WITH(NOLOCK) WHERE BCODIGO LIKE '%" + txt_barras.Text + "%')";
            }
                        
            if (!chk_inv.Checked)
                filtro += " AND BBCANTID <> 0";
            
            if (!string.IsNullOrWhiteSpace(edt_referencia.Text))
                filtro += "AND AR.ARCLAVE1 IN (ISNULL((SELECT TB_ORIGEN.ARCLAVE1 FROM TB_ORIGEN WITH(NOLOCK) WHERE TB_ORIGEN.ARCODEMP = AR.ARCODEMP AND OR_REFERENCIA='" + edt_referencia.Text + "'),'" + edt_referencia.Text + "'))"; 
            
            if (!string.IsNullOrWhiteSpace(edt_nombre.Text))
                filtro += " AND AR.ARNOMBRE LIKE '%" + edt_nombre.Text + "%'";

            var collection = rc_bodega.CheckedItems;
            if (collection.Count != 0)
            {
                lc_in = "";
                filtro += " AND BBBODEGA IN (";
                foreach (var item in collection)
                    lc_in += "'" + Convert.ToString(item.Value) + "',";

                filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";

                obj_consulta.SelectParameters["filter"].DefaultValue = filtro;
                rgDetalle.DataBind();
            }
            else
            {
                litTextoMensaje.Text = "No Se ha Seleccionado Ninguna Bodega!";
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
                       
        }
        protected void rgDetalle_OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            

            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                        string BD = dataItem.GetDataKeyValue("BBBODEGA").ToString();
                        string TP = dataItem.GetDataKeyValue("BBTIPPRO").ToString();
                        string C1 = dataItem.GetDataKeyValue("BBCLAVE1").ToString();
                        string C2 = dataItem.GetDataKeyValue("BBCLAVE2").ToString();
                        string C3 = dataItem.GetDataKeyValue("BBCLAVE3").ToString();
                        string C4 = dataItem.GetDataKeyValue("BBCLAVE4").ToString();

                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        ConsultasBL Obj = new ConsultasBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetConsulatInventarioLote(null, Convert.ToString(Session["CODEMP"]), BD,TP,C1,C2,C3,C4);
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
                case "detalle_item_elemento":
                    ConsultasBL Obj_ = new ConsultasBL();
                    try
                    {
                        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                        string PK = dataItem.GetDataKeyValue("LLAVE").ToString();
                        
                        e.DetailTableView.DataSource = Obj_.GetConsulatInventarioElemento(null, PK);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj_ = null;
                    }
                    break;
                    
            }
        }
        protected void rgDetalle_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                ConsultasBL Obj = new ConsultasBL();
                DataTable dt = new DataTable();
                try {
                    //dt = Obj.GetTrazaInventarios(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item["ARTIPPRO"].Text), Convert.ToString(item["ARCLAVE1"].Text),
                    dt = Obj.GetTrazaInventarios(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item["ARTIPPRO"].Text), Convert.ToString((item.FindControl("lbl_referencia") as Label).Text),
                                            Convert.ToString(item["ARCLAVE2"].Text), Convert.ToString(item["ARCLAVE3"].Text), Convert.ToString(item["ARCLAVE4"].Text),
                                            Convert.ToString(item["BBBODEGA"].Text));
                    rg_detalle.DataSource = dt;
                    rg_detalle.DataBind();
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                    Obj = null;
                    dt = null;
                }
            }

            if (e.CommandName == "link")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_referencia") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }

            if (e.CommandName == "link_test")
            {
                GridDataItem item_ = (GridDataItem)e.Item;
                string url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/MaestroArticulos.aspx?Documento=" + (item_.FindControl("lbl_reftester") as Label).Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                item_ = null;
            }

            if (e.CommandName == "ExportToExcel")
            {
                rgDetalle.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            }
        }
        protected void rg_detalle_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string url = "";
            if (e.CommandName == "link")
            {
                GridDataItem item = (GridDataItem)e.Item;
                try
                {
                    switch(item["MBCDTRAN"].Text)
                    {
                        case "0":
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/FotoInventario.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                            break;
                        case "14":
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/FacturaDirecta.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "15":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Devoluciones.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "16":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/RecepcionCompras.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "25":
                            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Compras/LogisticaIn.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                            break;
                        case "30":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/LtaEmpaque/LtaEmpaque.aspx?Empaque=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "31":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Facturacion/Remisiones.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "98":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/Traslados.aspx?Traslado=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        case "99":
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/Traslados.aspx?Traslado=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                                break;
                        default:
                                url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Inventarios/MovimientosManuales.aspx?Documento=" + (item.FindControl("lbl_doc") as Label).Text;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);                            
                                break;

                    } 
                    
                    string script = "function f(){$find(\"" + modalPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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
        protected void rgDetalle_InfrastructureExporting(object sender, GridInfrastructureExportingEventArgs e)
        {
            foreach (Cell cell in e.ExportStructure.Tables[0].Columns[2].Cells)
            {
                cell.Format = "@";
            }
        }
        protected void rc_bodega_DataBound(object sender, EventArgs e)
        {
            foreach (RadComboBoxItem itm in rc_bodega.Items)
            {
                itm.Checked = true;
            }
        }
    }
}