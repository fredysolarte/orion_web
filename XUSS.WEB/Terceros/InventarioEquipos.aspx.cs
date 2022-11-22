using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data;
using XUSS.BLL.Terceros;
using XUSS.BLL.Comun;

namespace XUSS.WEB.Terceros
{
    public partial class InventarioEquipos : System.Web.UI.Page
    {
        private DataTable DetHadware;
        private DataTable DetSoftware;
        //private RadListViewDataItem rliItems;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
            }
            DetHadware = ViewState["DetHadware"] as DataTable;
            DetSoftware = ViewState["DetSoftware"] as DataTable;
            //if (rlv_equipos.InsertItem != null)
            //    rliItems = rlv_equipos.InsertItem;
            
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " ";

            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txt_codigo") as RadNumericTextBox).Text))
                filtro += "AND CODIGO=" + (((Button)sender).Parent.FindControl("txt_codigo") as RadNumericTextBox).Text;
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text))
                filtro += "AND REFERENCIA = '" + (((Button)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text + "'";
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND NOMBRE LIKE '%" + (((Button)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";
            if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txt_ip") as RadTextBox).Text))
                filtro += "AND IP1 = '" + (((Button)sender).Parent.FindControl("txt_ip") as RadTextBox).Text + "'";
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);
            
            obj_equipos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_equipos.DataBind();
            if ((rlv_equipos.Controls[0] is RadListViewEmptyDataItem))
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
                (rlv_equipos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void rlv_equipos_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "InitInsert":
                    ViewState["isClickInsert"] = true;
                    InventarioEquiposBL obj = new InventarioEquiposBL();
                    try
                    {
                        DetHadware = null;
                        DetHadware = obj.GetHadware(-1);
                        ViewState["DetHadware"] = DetHadware;
                        DetSoftware = null;
                        DetSoftware = obj.GetSoftware(-1);
                        ViewState["DetSoftware"] = DetSoftware;                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        obj = null;
                    }
                    break;

                case "Buscar":
                    obj_equipos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_equipos.DataBind();
                    break;
            }
            this.AnalizarCommand(e.CommandName);
        }
        protected void AnalizarCommand(string comando)
        {
            //if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            //    ViewState["toolbars"] = false;
            //else            
                if (comando.Equals("Cancel"))            
                    ViewState["toolbars"] = true;            
                else            
                    ViewState["toolbars"] = false;

            
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
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.OcultarPaginador(rlv_equipos, "RadDataPager1", "BotonesBarra");
        }
        protected void rlv_equipos_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {            
            //(rlv_pedidohd.Items[0].FindControl("gr_detalle") as RadGrid).DataBind();
            //obj_hadware.SelectParameters["Codigo"].DefaultValue = rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString();
            if ((e.Item).ItemType == Telerik.Web.UI.RadListViewItemType.DataItem)
            {
                InventarioEquiposBL obj = new InventarioEquiposBL();
                try
                {
                    DetHadware = null;
                    DetHadware = obj.GetHadware(Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString()));
                    ViewState["DetHadware"] = DetHadware;
                    (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                    //(rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataBind();
                    DetSoftware = null;
                    DetSoftware = obj.GetSoftware(Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString()));
                    ViewState["DetSoftware"] = DetSoftware;
                    (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                    //(rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataBind();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    obj = null;
                }

            }
            if ((e.Item).ItemType == Telerik.Web.UI.RadListViewItemType.EditItem)
            {
                InventarioEquiposBL obj = new InventarioEquiposBL();
                try
                {
                    DetHadware = null;
                    DetHadware = obj.GetHadware(Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString()));
                    ViewState["DetHadware"] = DetHadware;
                    (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                    //(rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataBind();
                    DetSoftware = null;
                    DetSoftware = obj.GetSoftware(Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString()));
                    ViewState["DetSoftware"] = DetSoftware;
                    (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                    //(rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataBind();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    obj = null;
                }
            }
            
            (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataBind();
            (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataBind();

            if (e.Item.ItemType == RadListViewItemType.DataItem)
            {
                if (Convert.ToBoolean(ViewState["isClickInsert"]))
                {
                    e.Item.FindControl("pnItemMaster").Visible = false;                    
                    return;
                }
                else
                {
                    ViewState["toolbars"] = true;
                }
            }
        }
        protected void OnClick_ibtn_inserthadware(object sender, EventArgs e)
        {
            rc_marca.SelectedIndex = -1;
            rc_tipo.SelectedIndex = -1;
            txt_observhadware.Text = "";
            rc_proveedor.SelectedIndex = -1;
            edt_fCompra.SelectedDate = null;
            mp_hadware.Show();
        }
        protected void btn_AceptarH_click(object sender, EventArgs e)
        {
            DataRow row = DetHadware.NewRow();
            int ln_pos = 0;
            try
            {
                ln_pos = 1;
                if (DetHadware.Rows.Count != 0)
                {
                    //ln_pos = Convert.ToInt32(DetHadware.Rows[DetHadware.Rows.Count]) + 1;
                    ln_pos = DetHadware.Rows.Count;
                    ln_pos = Convert.ToInt32(DetHadware.Rows[ln_pos-1].ItemArray[2]) + 1;
                }
                
                row["CODEMP"] = "001";
                row["CODIGO"] = 0;//rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString();
                row["CODINT"] = ln_pos;
                row["MARCA"] = rc_marca.SelectedValue;
                row["TIPO"] = rc_tipo.SelectedValue;
                row["DESCRIPCION"] = txt_observhadware.Text;
                row["PROVEEDOR"] = rc_proveedor.SelectedValue;
                if (!edt_fCompra.IsEmpty)
                    row["FECCOMPRA"] = edt_fCompra.SelectedDate;
                //Reader.IsDBNull(Reader.GetOrdinal("FECMOD")) ? null : (DateTime?)Convert.ToDateTime(Reader["FECMOD"]);
                //row["FECCOMPRA"] =
                row["ESTADO"] = "AC";
                row["CAUSAE"] = ".";
                row["FECING"] = DateTime.Today;
                row["FECMOD"] = DateTime.Today;
                row["USUARIO"] = HttpContext.Current.Session["UserLogon"].ToString();
                DetHadware.Rows.Add(row);
                if (rlv_equipos.InsertItem != null)
                {
                    (rlv_equipos.InsertItem.FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                    (rlv_equipos.InsertItem.FindControl("rg_hadware") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                    (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataBind();
                }

                mp_hadware.Hide();            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
            }            
        }
        //protected void btn_cancelH_click(object sender, EventArgs e)
        //{
        //    mp_hadware.Hide();
        //}
        protected void OnClick_ibtn_insertsoftware(object sender, EventArgs e)
        {
            txt_nombre.Text = "";
            txt_licencia.Text = "";
            txt_fecven.Clear();
            txt_observacionesSoft.Text = "";
            mp_software.Show();
        }
        protected void btn_AceptarS_click(object sender, EventArgs e)
        {
            DataRow row = DetSoftware.NewRow();
            int ln_pos = 0;
            try
            {
                ln_pos = 1;
                if (DetSoftware.Rows.Count != 0)
                {
                    //ln_pos = Convert.ToInt32(DetHadware.Rows[DetHadware.Rows.Count]) + 1;
                    ln_pos = DetSoftware.Rows.Count;
                    ln_pos = Convert.ToInt32(DetSoftware.Rows[ln_pos - 1].ItemArray[2]) + 1;
                }

                row["CODEMP"] = "001";
                row["CODIGO"] = 0;//rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString();
                row["CODINT"] = ln_pos;
                row["NOMBRE"] = txt_nombre.Text;
                row["LICENCIA"] = txt_licencia.Text;
                row["DESCRIPCION"] = txt_observacionesSoft.Text;
                if (!txt_fecven.IsEmpty)
                   row["FECVEN"] = txt_fecven.SelectedDate;
                row["ESTADO"] = "AC";
                row["CAUSAE"] = ".";
                row["FECING"] = DateTime.Today;
                row["FECMOD"] = DateTime.Today;
                row["USUARIO"] = HttpContext.Current.Session["UserLogon"].ToString();
                //row[""]
                DetSoftware.Rows.Add(row);
                if (rlv_equipos.InsertItem != null)
                {
                    (rlv_equipos.InsertItem.FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                    (rlv_equipos.InsertItem.FindControl("rg_software") as RadGrid).DataBind();
                }
                else
                {
                    (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                    (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataBind();
                }

                mp_software.Hide();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                row = null;
            }
        }
        //protected void btn_cancelS_click(object sender, EventArgs e)
        //{
        //    mp_software.Hide();
        //}        
        protected void rg_hadware_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            var CODINT = item.GetDataKeyValue("CODINT").ToString();
            int pos = 0;
            int xpos = 0;
            foreach (DataRow row in DetHadware.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if (Convert.ToInt32(row["CODINT"]) == Convert.ToInt32(CODINT))
                    {
                        pos = xpos;
                    }
                }
                xpos ++;
            }
            DetHadware.Rows[pos].Delete();
            if (rlv_equipos.InsertItem != null)
            {
                (rlv_equipos.InsertItem.FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                (rlv_equipos.InsertItem.FindControl("rg_hadware") as RadGrid).DataBind();
            }
            else
            {
                (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataSource = DetHadware;
                (rlv_equipos.Items[0].FindControl("rg_hadware") as RadGrid).DataBind();
            }
        }
        protected void rg_software_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            var CODINT = item.GetDataKeyValue("CODINT").ToString();
            int pos = 0;
            int xpos = 0;
            foreach (DataRow row in DetSoftware.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    if (Convert.ToInt32(row["CODINT"]) == Convert.ToInt32(CODINT))
                    {
                        pos = xpos;
                    }
                }
                xpos++;
            }
            DetSoftware.Rows[pos].Delete();
            if (rlv_equipos.InsertItem != null)
            {
                (rlv_equipos.InsertItem.FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                (rlv_equipos.InsertItem.FindControl("rg_software") as RadGrid).DataBind();
            }
            else
            {
                (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataSource = DetSoftware;
                (rlv_equipos.Items[0].FindControl("rg_software") as RadGrid).DataBind();
            }
        }
        protected void obj_equipos_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters[1] = ComunBL.GeneraConsecutivo(null, "CNTEQUI");
            e.InputParameters[10] = HttpContext.Current.Session["UserLogon"].ToString();
        }
        protected void obj_equipos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            int ln_nequipo = Convert.ToInt32(e.ReturnValue);
            int ln_acu = 0;
            try
            {
                foreach (DataRow row in DetHadware.Rows)
                {
                    ln_acu++;
                    InventarioEquiposBL.InsertHadware(null, ln_nequipo, ln_acu, Convert.ToString(row["MARCA"]), Convert.ToString(row["TIPO"]), Convert.ToString(row["DESCRIPCION"]), "AC", ".",
                                                      Convert.ToDateTime(row["FECCOMPRA"]), Convert.ToString(row["PROVEEDOR"]));
                }
                ln_acu = 0;
                foreach (DataRow row in DetSoftware.Rows)
                {
                    ln_acu++;
                    InventarioEquiposBL.InsertSoftware(null, ln_nequipo, ln_acu, Convert.ToString(row["NOMBRE"]), Convert.ToString(row["LICENCIA"]), Convert.ToDateTime(row["FECVEN"]),
                                                       Convert.ToString(row["DESCRIPCION"]));
                }
            }
            catch (Exception ex)
            {
                //throw ex;    
                LitTitulo.Text = "Error";
                litTextoMensaje.Text = ex.Message.ToString();
                mpMensajes.Show();
            }
            finally
            {
                LitTitulo.Text = "Confirmacion";
                litTextoMensaje.Text = "Se Ingreso de forma Correcta el Equipo Nro: " + Convert.ToString(ln_nequipo);
                mpMensajes.Show();
            }
            
        }
        public string GetMarca(object a)
        {
            if (a is DBNull || a == null)
            {
                return string.Empty;
            }
            else
            {
                return InventarioEquiposBL.GetNombreTbTablas("MARCAH", Convert.ToString(a));
            }
        }
        public string GetTipo(object a)
        {
            if (a is DBNull || a == null)
            {
                return string.Empty;
            }
            else
            {
                return InventarioEquiposBL.GetNombreTbTablas("TIPOH", Convert.ToString(a));
            }
        }
        public string GetEstado(object a)
        {
            if (a is DBNull || a == null)
            {
                return string.Empty;
            }
            else
            {
                switch (Convert.ToString(a))
                {
                    case "AC": return "Activo";
                    case "CE": return "Cambio";
                    default: return "Anulado";
                }

            }
        }
        public string GetProveedor(object a)
        {
            if (a is DBNull || a == null)            
                return string.Empty;            
            else           
                return ComunBL.GetNombreProveedor(null, Convert.ToString(a));            
        }        
        protected void obj_equipos_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            int ln_nequipo = Convert.ToInt32(e.ReturnValue);
            int ln_acu = 0;
            try
            {
                InventarioEquiposBL.DeleteHadware(null, ln_nequipo);
                foreach (DataRow row in DetHadware.Rows)
                {
                    ln_acu++;
                    InventarioEquiposBL.InsertHadware(null, ln_nequipo, ln_acu, Convert.ToString(row["MARCA"]), Convert.ToString(row["TIPO"]), Convert.ToString(row["DESCRIPCION"]), "AC", ".",
                                                      Convert.ToDateTime(row["FECCOMPRA"]), Convert.ToString(row["PROVEEDOR"]));
                }
                ln_acu = 0;
                InventarioEquiposBL.DeleteSoftware(null, ln_nequipo);
                foreach (DataRow row in DetSoftware.Rows)
                {
                    ln_acu++;
                    InventarioEquiposBL.InsertSoftware(null, ln_nequipo, ln_acu, Convert.ToString(row["NOMBRE"]), Convert.ToString(row["LICENCIA"]), Convert.ToDateTime(row["FECVEN"]),
                                                       Convert.ToString(row["DESCRIPCION"]));
                }
            }
            catch (Exception ex)
            {
                LitTitulo.Text = "Error";
                litTextoMensaje.Text = ex.Message.ToString();
                mpMensajes.Show();
            }
            finally
            { 
                LitTitulo.Text = "Confirmacion";
                litTextoMensaje.Text = "Se Actualizo de Forma Correcta";
                mpMensajes.Show();
            }
        }
        protected void iBtnImprimirCO_OnClick(object sender, EventArgs e)
        {

            //Informes.rCodigosEquipos requipo = new Informes.rCodigosEquipos();
                        
            //try
            //{
            //    requipo.ReportParameters["inuCodigo"].Value = Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString());
            //    ReportViewer1.Report = requipo;                                                                                
            //    ReportViewer1.RefreshReport();
            //    mp_reporte.Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    requipo = null;
            //}
            
        }
        protected void iBtnImprimirAC_OnClick(object sender, EventArgs e)
        {
            //Informes.rActaEntrega racta = new Informes.rActaEntrega();
            

            //try
            //{
            //    racta.ReportParameters["inuCodigo"].Value = Convert.ToInt32(rlv_equipos.Items[0].GetDataKeyValue("CODIGO").ToString());                                
            //    ReportViewer1.Report = racta;                
            //    ReportViewer1.RefreshReport();
            //    mp_reporte.Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    racta = null;
            //}
            
        }
        protected void obj_equipos_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {            
            e.InputParameters[10] = HttpContext.Current.Session["UserLogon"].ToString();
        }
    }
}