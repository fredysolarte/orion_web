using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Consultas;

namespace XUSS.WEB.Recaudo
{
    public partial class ConsultaRecaudos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                edt_fcfecini.SelectedDate = System.DateTime.Now;
                edt_fcfecfin.SelectedDate = System.DateTime.Now;
            }
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            int ln_int = 0;
            string lc_filtro = "";
            Boolean lb_uno = false, lb_dos = false;
            object[] param = new object[4];
            ConsultasBL Obj = new ConsultasBL();
            try
            {
                if (!string.IsNullOrWhiteSpace(edt_nrorecibo.Text))
                    lc_filtro += " AND RC_NRORECIBO =" + edt_nrorecibo.Text;

                if (!string.IsNullOrWhiteSpace(txt_nrofac.Text))
                    lc_filtro += " AND HDNROFAC =" + txt_nrofac.Text;

                if (!string.IsNullOrWhiteSpace(txt_nombre.Text))
                    lc_filtro += " AND (TRNOMBRE +' '+ISNULL(TRNOMBR2,'')+' '+ISNULL(TRAPELLI,''))  LIKE '%" + txt_nombre.Text + "%'";

                if (edt_fecini.SelectedDate != null && edt_fecfin.SelectedDate != null)
                {
                    lb_uno = true;
                    ln_int++;
                    lc_filtro += "AND CONVERT(DATE,RC_FECREC,101) BETWEEN CONVERT(DATE, @p"+Convert.ToString(ln_int-1)+",101) AND CONVERT(DATE,@p"+Convert.ToString(ln_int)+",101)";
                }

                if (edt_fcfecini.SelectedDate != null && edt_fcfecfin.SelectedDate != null)
                {
                    lb_dos = true;
                    if (ln_int == 0)
                        ln_int = -1;                    

                    lc_filtro += "AND CONVERT(DATE,RC_FECING,101) BETWEEN CONVERT(DATE,@p" + Convert.ToString(ln_int+1) + ",101) AND CONVERT(DATE,@p" + Convert.ToString(ln_int+2) + ",101)";
                }



                int ln_lon = 0;
                if (lb_uno && lb_dos)
                    ln_lon = 4;
                else
                {
                    if (!lb_uno && lb_dos)
                        ln_lon = 2;
                    else
                    {
                        if (lb_uno && !lb_dos)
                            ln_lon = 2;
                    }
                }
                    

                   
                    if (lb_uno && lb_dos)
                    {
                        param[0] = edt_fecini.SelectedDate;
                        param[1] = edt_fecfin.SelectedDate;
                        param[2] = edt_fcfecini.SelectedDate;
                        param[3] = edt_fcfecfin.SelectedDate;
                    }
                    else
                    {
                        if (!lb_uno && lb_dos)
                        {
                            param[0] = edt_fcfecini.SelectedDate;
                            param[1] = edt_fcfecfin.SelectedDate;
                            param[2] = null;
                            param[3] = null;
                        }
                        else
                        {
                            if (lb_uno && !lb_dos)
                            {
                                param[0] = edt_fecini.SelectedDate;
                                param[1] = edt_fecfin.SelectedDate;
                                param[2] = null;
                                param[3] = null;
                            }
                        }
                    }
                

                if (!string.IsNullOrWhiteSpace(txt_icliente.Text))
                    lc_filtro += " AND TERCEROS.TRCODNIT ='" + txt_icliente.Text+ "'";

                if (!string.IsNullOrWhiteSpace(txt_codcliente.Text))
                    lc_filtro += " AND TERCEROS.TRCODTER =" + txt_codcliente.Text;


                //obj_recaudo.SelectParameters["filter"].DefaultValue = lc_filtro;
                //obj_recaudo.SelectParameters["param"] = param;                
                DataTable dt = new DataTable();
                dt = Obj.GetConsultaRecaudos(null, lc_filtro, param);
                rg_consulta.DataSource = dt;
                rg_consulta.DataBind();
            }
            catch (Exception ex)
            {
                litTextoMensaje.Text = ex.Message;
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                //throw ex;
            }
            finally
            {
                Obj = null;
            }
        }
        protected void rg_consulta_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string url = "";
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                switch (e.CommandName)
                {
                    case "link_rc":
                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//"+ HttpContext.Current.Request.Url.Authority + "/Recaudo/Recaudo.aspx?Documento=" + (item.FindControl("lbl_recibo") as Label).Text;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
                        break;
                    case "link_tr":
                        url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Terceros/Terceros.aspx?Documento=" + (item.FindControl("lbl_tercero") as Label).Text;
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
        protected void rg_consulta_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            int ln_nrorecibo = Convert.ToInt32(dataItem.GetDataKeyValue("RC_NRORECIBO").ToString());
            switch (e.DetailTableView.Name)
            {
                case "detalle_item":
                    {
                        //e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
                        ConsultasBL Obj = new ConsultasBL();
                        try
                        {
                            e.DetailTableView.DataSource = Obj.GetConsultaDetalleRecaudos(null, ln_nrorecibo);
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
    }
}