using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Text;
using System.Data;
using XUSS.BLL.Articulos;
using XUSS.BLL.Comun;
using XUSS.BLL.ListaPrecios;
using System.IO;
using Telerik.Web.UI.ImageEditor;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Terceros
{
    public partial class MaestroArticulos : System.Web.UI.Page
    {
        private DataTable tbBarras
        {
            set { ViewState["tbBarras"] = value; }
            get { return ViewState["tbBarras"] as DataTable; }
        }
        private DataTable tbClave2
        {
            set { ViewState["tbClave2"] = value; }
            get { return ViewState["tbClave2"] as DataTable; }
        }
        private DataTable tbClave3
        {
            set { ViewState["tbClave3"] = value; }
            get { return ViewState["tbClave3"] as DataTable; }
        }
        private DataTable tbPrecios
        {
            set { ViewState["tbPrecios"] = value; }
            get { return ViewState["tbPrecios"] as DataTable; }
        }
        private DataTable tbImagenes
        {
            set { ViewState["tbImagenes"] = value; }
            get { return ViewState["tbImagenes"] as DataTable; }
        }
        private DataTable tbSoportes
        {
            set { ViewState["tbSoportes"] = value; }
            get { return ViewState["tbSoportes"] as DataTable; }
        }
        private DataTable tbRSanitario
        {
            set { ViewState["tbRSanitario"] = value; }
            get { return ViewState["tbRSanitario"] as DataTable; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        private DataTable tbTester
        {
            set { ViewState["tbTester"] = value; }
            get { return ViewState["tbTester"] as DataTable; }
        }
        private DataTable tbOrigen
        {
            set { ViewState["tbOrigen"] = value; }
            get { return ViewState["tbOrigen"] as DataTable; }
        }
        protected void rlv_articulos_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "InitInsert")
            {
                ViewState["isClickInsert"] = true;                
            }
            else
            {
                ViewState["isClickInsert"] = false;
            }
            switch (e.CommandName)
            {
                case "Buscar":
                    obj_articulos.SelectParameters["filter"].DefaultValue = "1=0";
                    rlv_articulos.DataBind();
                    e.Canceled = true;
                    break;
                case "Edit":                    
                    break;
                case "InitInsert":
                    ArticulosBL Obj = new ArticulosBL();
                    ClavesAlternasBL ObjA = new ClavesAlternasBL();
                    SoportesBL ObjS = new SoportesBL();
                    ListaPreciosBL ObjL = new ListaPreciosBL();

                    try
                    {
                        tbClave2 = Obj.GetClave2(null, null, null, null);
                        tbClave3 = Obj.GetClave3(null, null, null, null);
                        tbSoportes = ObjS.GetSoportes(null, "1", 0);
                        tbTester = Obj.GetTester(null, null, null, null, null, null, null);
                        tbOrigen = Obj.GetOrigen(null, null, null, null, null, null, null);
                        tbBarras = Obj.GetTbBarras(null, "", null,null);
                        tbPrecios = ObjL.GetListaPrecioDT(null, null, null);
                        tbRSanitario = Obj.GetRegistrosSanitarios(null, null, null,null,null,null,null);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Obj = null;
                        ObjL = null;
                        ObjS = null;
                        ObjA = null;
                    }
                    break;
                default:
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
        protected void OcultarPaginador(Telerik.Web.UI.RadListView rlv, string idPaginador, string idPanelBotones)
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
            this.OcultarPaginador(rlv_articulos, "RadDataPager1", "BotonesBarra");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["toolbars"] = true;
                if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["Documento"])))
                {
                    string words = Convert.ToString(Request.QueryString["Documento"]);
                    obj_articulos.SelectParameters["filter"].DefaultValue = "  ARCLAVE1 ='" + words + "'";
                    rlv_articulos.DataBind();
                }
            }
        }
        protected void rlv_articulos_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
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

            ArticulosBL Obj = new ArticulosBL();
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            ListaPreciosBL ObjL = new ListaPreciosBL();
            ComunBL ObjC = new ComunBL();
            SoportesBL ObjS = new SoportesBL();
            DataRow fila = ((DataRowView)(((RadListViewDataItem)(e.Item)).DataItem)).Row;
            try
            {
                DataTable dt1 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 5);
                DataTable dt2 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 6);
                DataTable dt3 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 7);
                DataTable dt4 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 8);
                DataTable dt5 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 9);
                DataTable dt7 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 10);
                DataTable dt8 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, 11);

                (e.Item.FindControl("rc_dt1") as RadComboBox).DataSource = dt1;
                (e.Item.FindControl("rc_dt2") as RadComboBox).DataSource = dt2;
                (e.Item.FindControl("rc_dt3") as RadComboBox).DataSource = dt3;
                (e.Item.FindControl("rc_dt4") as RadComboBox).DataSource = dt4;
                (e.Item.FindControl("rc_dt5") as RadComboBox).DataSource = dt5;
                (e.Item.FindControl("rc_dt7") as RadComboBox).DataSource = dt7;
                (e.Item.FindControl("rc_dt8") as RadComboBox).DataSource = dt8;

                (e.Item.FindControl("rc_dt1") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt2") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt3") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt4") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt5") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt7") as RadComboBox).DataBind();
                (e.Item.FindControl("rc_dt8") as RadComboBox).DataBind();


                (e.Item.FindControl("rc_dt1") as RadComboBox).SelectedValue = fila["ARDTTEC1"].ToString();
                (e.Item.FindControl("rc_dt2") as RadComboBox).SelectedValue = fila["ARDTTEC2"].ToString();
                (e.Item.FindControl("rc_dt3") as RadComboBox).SelectedValue = fila["ARDTTEC3"].ToString();
                (e.Item.FindControl("rc_dt4") as RadComboBox).SelectedValue = fila["ARDTTEC4"].ToString();
                (e.Item.FindControl("rc_dt5") as RadComboBox).SelectedValue = fila["ARDTTEC5"].ToString();
                (e.Item.FindControl("rc_dt7") as RadComboBox).SelectedValue = fila["ARDTTEC7"].ToString();
                (e.Item.FindControl("rc_dt8") as RadComboBox).SelectedValue = fila["ARDTTEC8"].ToString();

                //Cargar TP o Lineas
                using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue))
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["TACTLSE2"]) == "S")
                        {
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[0].Text = Convert.ToString(reader["TADSCLA2"]);
                            tbClave2 = Obj.GetClave2(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_referencia") as RadTextBox).Text);
                            ((e.Item.FindControl("pnl_claves") as RadPanelBar).Items[0].FindControl("rg_clave2") as RadGrid).DataSource = tbClave2;
                            ((e.Item.FindControl("pnl_claves") as RadPanelBar).Items[0].FindControl("rg_clave2") as RadGrid).DataBind();
                        }
                        else
                        {
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[0].Visible = false;
                            tbClave2 = Obj.GetClave2(null, "0", "0", "0");
                        }
                        if (Convert.ToString(reader["TACTLSE3"]) == "S")
                        {
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[1].Text = Convert.ToString(reader["TADSCLA3"]);
                            tbClave3 = Obj.GetClave3(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_referencia") as RadTextBox).Text);
                            ((e.Item.FindControl("pnl_claves") as RadPanelBar).Items[1].FindControl("rg_clave3") as RadGrid).DataSource = tbClave3;
                            ((e.Item.FindControl("pnl_claves") as RadPanelBar).Items[1].FindControl("rg_clave3") as RadGrid).DataBind();
                        }
                        else
                        {
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[1].Visible = false;
                            tbClave3 = Obj.GetClave3(null, "0", "0", "0");
                        }
                        if (Convert.ToString(reader["TACTLSE4"]) == "S")
                        {
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[2].Text = Convert.ToString(reader["TADSCLA4"]);
                        }
                        else
                            (e.Item.FindControl("pnl_claves") as RadPanelBar).Items[2].Visible = false;
                    }
                }

                //Cargar Codigos Barras
                tbBarras = Obj.GetTbBarras(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_referencia") as RadTextBox).Text);
                (e.Item.FindControl("rg_items") as RadGrid).DataSource = tbBarras;
                (e.Item.FindControl("rg_items") as RadGrid).DataBind();

                //Cargar Precios
                tbPrecios = ObjL.GetListaPrecioDT(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_referencia") as RadTextBox).Text);
                (e.Item.FindControl("rg_precios") as RadGrid).DataSource = tbPrecios;
                (e.Item.FindControl("rg_precios") as RadGrid).DataBind();

                //Cargar Imagenes
                tbImagenes = Obj.GetImagenes(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_referencia") as RadTextBox).Text);
                (e.Item.FindControl("rg_imagenes") as RadGrid).DataSource = tbImagenes;
                (e.Item.FindControl("rg_imagenes") as RadGrid).DataBind();

                //Soportes
                tbSoportes = ObjS.GetSoportesArticulos(null, "3", (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text);
                (e.Item.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                (e.Item.FindControl("rgSoportes") as RadGrid).DataBind();

                //Tester
                tbTester = Obj.GetTester(null,Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text,".",".",".");
                (e.Item.FindControl("rgTester") as RadGrid).DataSource = tbTester;
                (e.Item.FindControl("rgTester") as RadGrid).DataBind();

                //Origen
                tbOrigen = Obj.GetOrigen(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text, ".", ".", ".");
                (e.Item.FindControl("rg_origen") as RadGrid).DataSource = tbOrigen;
                (e.Item.FindControl("rg_origen") as RadGrid).DataBind();

                //Registros Sanitarios
                tbRSanitario = Obj.GetRegistrosSanitarios(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text, ".", ".", ".");
                (e.Item.FindControl("rg_rsanitarios") as RadGrid).DataSource = tbRSanitario;
                (e.Item.FindControl("rg_rsanitarios") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjL = null;
                ObjS = null;
                ObjA = null;
            }
        }
        protected void btn_filtro_OnClick(object sender, EventArgs e)
        {
            string filtro = " AND ARCODEMP ='"+ Session["CODEMP"] +"'" ;

            //if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("rc_categoria_find") as RadComboBox).Text))
            //{
                if ((((RadButton)sender).Parent.FindControl("rc_categoria_find") as RadComboBox).SelectedValue != "-1")
                    filtro += "AND ARTIPPRO='" + (((RadButton)sender).Parent.FindControl("rc_categoria_find") as RadComboBox).SelectedValue + "'";
            //}

            if ((((RadButton)sender).Parent.FindControl("rc_dt1") as RadComboBox).SelectedValue != "-1")
                filtro += "AND ARDTTEC1='" + (((RadButton)sender).Parent.FindControl("rc_dt1") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_dt2") as RadComboBox).SelectedValue != "-1")
                filtro += "AND ARDTTEC2='" + (((RadButton)sender).Parent.FindControl("rc_dt2") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_dt3") as RadComboBox).SelectedValue != "-1")
                filtro += "AND ARDTTEC3='" + (((RadButton)sender).Parent.FindControl("rc_dt3") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_dt4") as RadComboBox).SelectedValue != "-1")
                filtro += "AND ARDTTEC4='" + (((RadButton)sender).Parent.FindControl("rc_dt4") as RadComboBox).SelectedValue + "'";

            if ((((RadButton)sender).Parent.FindControl("rc_dt5") as RadComboBox).SelectedValue != "-1")
                filtro += "AND ARDTTEC5='" + (((RadButton)sender).Parent.FindControl("rc_dt5") as RadComboBox).SelectedValue + "'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text))
                filtro += "AND ARCLAVE1 IN (ISNULL((SELECT TOP 1 TB_ORIGEN.ARCLAVE1 FROM TB_ORIGEN WITH(NOLOCK) WHERE TB_ORIGEN.ARCODEMP = ARTICULO.ARCODEMP AND OR_REFERENCIA='" + (((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text + "'),'" + (((RadButton)sender).Parent.FindControl("txt_referencia") as RadTextBox).Text + "'))";                 

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text))
                filtro += "AND ARNOMBRE LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_nombre") as RadTextBox).Text + "%'";

            if (!string.IsNullOrWhiteSpace((((RadButton)sender).Parent.FindControl("txt_fbarras") as RadTextBox).Text))
            {
                if ((((RadButton)sender).Parent.FindControl("txt_fbarras") as RadTextBox).Text.Length == 12)
                    filtro += " AND (ARCLAVE1+ARCLAVE2+ARCLAVE3+ARCLAVE4) IN (SELECT BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 FROM TBBARRA WITH(NOLOCK) WHERE BCODIGO LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_fbarras") as RadTextBox).Text.Substring(0, 11) + "%')";
                else
                    filtro += " AND (ARCLAVE1+ARCLAVE2+ARCLAVE3+ARCLAVE4) IN (SELECT BCLAVE1+BCLAVE2+BCLAVE3+BCLAVE4 FROM TBBARRA WITH(NOLOCK) WHERE BCODIGO LIKE '%" + (((RadButton)sender).Parent.FindControl("txt_fbarras") as RadTextBox).Text + "%')";
            }
            
            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_articulos.SelectParameters["filter"].DefaultValue = filtro;
            rlv_articulos.DataBind();
            if ((rlv_articulos.Controls[0] is RadListViewEmptyDataItem))
            {
                StringBuilder str = new StringBuilder();
                
                str.AppendLine("<div id=\"box-messages\" style=\"color: #a94442;    background-color: #f2dede;    border-color: #ebccd1;     padding: 15px;\" >");
                str.AppendLine("<strong>¡No se Encontaron Registros!</strong>");
                str.AppendLine("</div>");
                (rlv_articulos.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }
        protected void obj_articulos_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
        {            
            e.InputParameters["tbclave2"] = tbClave2;
            e.InputParameters["tbclave3"] = tbClave3;
            e.InputParameters["tbPrecios"] = tbPrecios;
            e.InputParameters["tbSoportes"] = tbSoportes;
            e.InputParameters["tbTester"] = tbTester;
            e.InputParameters["tbOrigen"] = tbOrigen;
            e.InputParameters["tbBarras"] = tbBarras;
            e.InputParameters["tbRSanitario"] = tbRSanitario;
        }
        protected void obj_articulos_OnInserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters["tbclave2"] = tbClave2;
            e.InputParameters["tbclave3"] = tbClave3;            
            e.InputParameters["tbPrecios"] = tbPrecios;
            e.InputParameters["tbBarras"] = tbBarras;
        }
        protected void obj_articulos_OnInserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Articulo Actualizado!";
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rlv_articulos_OnItemUpdated(object sender, RadListViewUpdatedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        protected void obj_articulos_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)                
                litTextoMensaje.Text = e.Exception.InnerException.Message;
            else
            {
                litTextoMensaje.Text = "Articulo Actualizado!";                
            }
            //mpMensajes.Show();
            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }
        protected void rlv_articulos_OnItemInserted(object sender, RadListViewInsertedEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                //e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //mpMensajes.Show();
                string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

        }
        protected void rg_clave2_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Boolean lb_c2 = false;
            if (e.Item.IsInEditMode && e.Item is GridEditFormItem)
            {
                ComunBL ObjC = new ComunBL();
                ArticulosBL ObjA = new ArticulosBL();
                GridEditFormItem editForm = (GridEditFormItem)e.Item;
                RadComboBoxItem item = new RadComboBoxItem();
                try
                {
                    //RadGrid grid = (RadGrid)editForm.FindControl("RadGrid2");
                    //Cargar TP o Lineas
                    ((RadComboBox)editForm.FindControl("rc_alterna2")).Visible = false;
                    ((RadTextBox)editForm.FindControl("txt_alterna2")).Visible = true;
                    //using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue))
                    using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), ((sender as RadGrid).Parent.Parent.Parent.Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue))
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToString(reader["TACTLSE2"]) == "S")
                            {
                                lb_c2 = true;
                                ((RadComboBox)editForm.FindControl("rc_alterna2")).Visible = true;
                                ((RadTextBox)editForm.FindControl("txt_alterna2")).Visible = false;
                            }
                        }
                    }
                    if (lb_c2)
                    {
                        item.Value = "";
                        item.Text = "Seleccionar";
                        ((RadComboBox)editForm.FindControl("rc_alterna2")).Items.Add(item);
                        //using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, 2))
                        using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((sender as RadGrid).Parent.Parent.Parent.Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue,2))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["ASCLAVEO"]);
                                itemi.Text = Convert.ToString(reader["ASNOMBRE"]);
                                ((RadComboBox)editForm.FindControl("rc_alterna2")).Items.Add(itemi);
                                itemi =null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    editForm = null;
                    ObjC = null;
                    ObjA = null;
                }
            }
        }
        protected void rg_clave2_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbClave2;
        }
        protected void rg_clave3_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbClave3;
        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            DataRow rw = tbClave2.NewRow();
            try {
                if (((sender as ImageButton).Parent.FindControl("rc_alterna2") as RadComboBox).Visible)
                {
                    rw["ASNOMBRE"] = ((sender as ImageButton).Parent.FindControl("rc_alterna2") as RadComboBox).Text;
                    rw["ARCLAVE2"] = ((sender as ImageButton).Parent.FindControl("rc_alterna2") as RadComboBox).SelectedValue;
                }
                else {
                    rw["ASNOMBRE"] = ((sender as ImageButton).Parent.FindControl("rc_alterna2") as RadTextBox).Text;
                    rw["ARCLAVE2"] = ((sender as ImageButton).Parent.FindControl("rc_alterna2") as RadTextBox).Text;
                }
                tbClave2.Rows.Add(rw);

                if (rlv_articulos.InsertItem != null)
                    ((rlv_articulos.InsertItem.FindControl("pnl_claves") as RadPanelBar).Items[0].FindControl("rg_clave2") as RadGrid).DataSource = tbClave2;
                else
                    ((rlv_articulos.Items[0].FindControl("pnl_claves") as RadPanelBar).Items[0].FindControl("rg_clave2") as RadGrid).DataSource = tbClave2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rw = null;
            }
        }
        protected void btn_aceptar_c3_OnClick(object sender, EventArgs e)
        {
            Boolean lb_ind = true;
            DataRow rw = tbClave3.NewRow();
            try {
                if (((sender as ImageButton).Parent.FindControl("rc_alterna3") as RadComboBox).Visible)
                {
                    rw["ASNOMBRE"] = ((sender as ImageButton).Parent.FindControl("rc_alterna3") as RadComboBox).Text;
                    rw["ARCLAVE3"] = ((sender as ImageButton).Parent.FindControl("rc_alterna3") as RadComboBox).SelectedValue;
                }
                else {
                    rw["ASNOMBRE"] = ((sender as ImageButton).Parent.FindControl("rc_alterna3") as RadTextBox).Text;
                    rw["ARCLAVE3"] = ((sender as ImageButton).Parent.FindControl("rc_alterna3") as RadTextBox).Text;
                }

                foreach (DataRow rx in tbClave3.Rows)
                {
                    if (Convert.ToString(rx["ARCLAVE3"]) == Convert.ToString(rw["ARCLAVE3"]))
                    {
                        litTextoMensaje.Text = "Ya Existe la Clave en el Articulo";
                        //mpMensajes.Show();
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        lb_ind = false;
                    }
                }
                if (lb_ind)
                {
                    tbClave3.Rows.Add(rw);
                    if (rlv_articulos.InsertItem != null)
                        ((rlv_articulos.InsertItem.FindControl("pnl_claves") as RadPanelBar).Items[1].FindControl("rg_clave3") as RadGrid).DataSource = tbClave3;
                    else
                        ((rlv_articulos.Items[0].FindControl("pnl_claves") as RadPanelBar).Items[1].FindControl("rg_clave3") as RadGrid).DataSource = tbClave3;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rw = null;
            }
        }
        protected void rg_clave3_OnItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Boolean lb_c3 = false;
            if (e.Item.IsInEditMode && e.Item is GridEditFormItem)
            {
                ComunBL ObjC = new ComunBL();
                ArticulosBL ObjA = new ArticulosBL();
                GridEditFormItem editForm = (GridEditFormItem)e.Item;
                RadComboBoxItem item = new RadComboBoxItem();
                try
                {
                    //RadGrid grid = (RadGrid)editForm.FindControl("RadGrid2");
                    //Cargar TP o Lineas
                    ((RadComboBox)editForm.FindControl("rc_alterna3")).Visible = false;
                    ((RadTextBox)editForm.FindControl("txt_alterna3")).Visible = true;
                    using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), ((sender as RadGrid).Parent.Parent.Parent.Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue))
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToString(reader["TACTLSE3"]) == "S")
                            {
                                lb_c3 = true;
                                ((RadComboBox)editForm.FindControl("rc_alterna3")).Visible = true;
                                ((RadTextBox)editForm.FindControl("txt_alterna3")).Visible = false;
                            }
                        }
                    }
                    if (lb_c3)
                    {
                        item.Value = "";
                        item.Text = "Seleccionar";
                        ((RadComboBox)editForm.FindControl("rc_alterna3")).Items.Add(item);
                        using (IDataReader reader = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((sender as RadGrid).Parent.Parent.Parent.Parent.FindControl("rc_categoria") as RadComboBox).SelectedValue,3))
                        {
                            while (reader.Read())
                            {
                                RadComboBoxItem itemi = new RadComboBoxItem();
                                itemi.Value = Convert.ToString(reader["ASCLAVEO"]);
                                itemi.Text = Convert.ToString(reader["ASNOMBRE"]);
                                ((RadComboBox)editForm.FindControl("rc_alterna3")).Items.Add(itemi);
                                itemi = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    editForm = null;
                    ObjC = null;
                    ObjA = null;
                }
            }
        }
        protected void OnSelectedIndexChanged_rc_categoria(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        { 
            ArticulosBL Obj = new ArticulosBL();
            ListaPreciosBL ObjL = new ListaPreciosBL();
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            ComunBL ObjC = new ComunBL();
            try
            {
                //Cargar TP o Lineas
                using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue))
                {
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["TACTLSE2"]) == "S")
                        {
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[0].Text = Convert.ToString(reader["TADSCLA2"]);                            
                        }
                        else
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[0].Visible = false;
                        if (Convert.ToString(reader["TACTLSE3"]) == "S")
                        {
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[1].Text = Convert.ToString(reader["TADSCLA3"]);                            
                        }
                        else
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[1].Visible = false;
                        if (Convert.ToString(reader["TACTLSE4"]) == "S")
                        {
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[2].Text = Convert.ToString(reader["TADSCLA4"]);
                        }
                        else
                            (((RadComboBox)sender).Parent.FindControl("pnl_claves") as RadPanelBar).Items[2].Visible = false;
                        
                        //Clasificaciones
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion1") as RadTextBox).Text = Convert.ToString(reader["TACDCLA1"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion2") as RadTextBox).Text = Convert.ToString(reader["TACDCLA2"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion3") as RadTextBox).Text = Convert.ToString(reader["TACDCLA3"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion4") as RadTextBox).Text = Convert.ToString(reader["TACDCLA4"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion5") as RadTextBox).Text = Convert.ToString(reader["TACDCLA5"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion6") as RadTextBox).Text = Convert.ToString(reader["TACDCLA6"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion7") as RadTextBox).Text = Convert.ToString(reader["TACDCLA7"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion8") as RadTextBox).Text = Convert.ToString(reader["TACDCLA8"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion9") as RadTextBox).Text = Convert.ToString(reader["TACDCLA9"]);
                        (((RadComboBox)sender).Parent.FindControl("txt_clasificacion10") as RadTextBox).Text = Convert.ToString(reader["TACDCLA10"]);

                        DataTable dt1 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 5, "AC");
                        DataTable dt2 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 6, "AC");
                        DataTable dt3 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 7, "AC");
                        DataTable dt4 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 8, "AC");
                        DataTable dt5 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 9, "AC");
                        DataTable dt7 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 10, "AC");
                        DataTable dt8 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 11, "AC");

                        (((RadComboBox)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataSource = dt1;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataSource = dt2;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataSource = dt3;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataSource = dt4;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataSource = dt5;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt7") as RadComboBox).DataSource = dt7;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt8") as RadComboBox).DataSource = dt8;

                        (((RadComboBox)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt7") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt8") as RadComboBox).DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                ObjA = null;
                ObjL = null;
                ObjC = null;
            }

        }
        protected void txt_referencia_OnTextChanged(object sender, EventArgs e)
        {
            ArticulosBL Obj = new ArticulosBL();
            try {
                if (!string.IsNullOrEmpty((sender as RadTextBox).Text))
                {
                    foreach (DataRow rw in Obj.GetArticulos(null, " ARCLAVE1='" + (sender as RadTextBox).Text + "'", 0, 0).Rows)
                    {
                        litTextoMensaje.Text = "Ya existe la Referencia!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
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
        protected void rg_precios_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbPrecios;
        }
        protected void btn_aceptar_pre_OnClick(object sender, EventArgs e)
        {
            string lc_lst = "",lc_c2="",lc_c3="";
            Boolean lb_ind = false;
            DataRow rw = tbPrecios.NewRow();
            try {
                if (((ImageButton)sender).CommandName == "PerformInsert")
                {
                    lc_lst = ((sender as ImageButton).Parent.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;
                    lc_c2 = ((sender as ImageButton).Parent.FindControl("rc_clave2pre") as RadComboBox).SelectedValue;
                    lc_c3 = ((sender as ImageButton).Parent.FindControl("rc_clave3pre") as RadComboBox).SelectedValue;

                    foreach (DataRow rx in tbPrecios.Rows)
                    {
                        if ((Convert.ToString(rx["P_RLISPRE"]) == lc_lst) && (Convert.ToString(rx["P_RCLAVE2"]) == lc_c2) && (Convert.ToString(rx["P_RCLAVE3"]) == lc_c3))
                        {
                            litTextoMensaje.Text = "Ya cuenta Con Precio en esa Lista!";
                            string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                            (rlv_articulos.Items[0].FindControl("rg_precios") as RadGrid).MasterTableView.ClearEditItems();
                            lb_ind = true;
                        }
                    }

                    if (!lb_ind)
                    {
                        DataRow row = tbPrecios.NewRow();
                        try
                        {
                            row["P_RCODEMP"] = "001";
                            row["P_RLISPRE"] = lc_lst;
                            row["P_CNOMBRE"] = ((sender as ImageButton).Parent.FindControl("rc_lstprecio") as RadComboBox).Text;
                            row["P_RITEMLST"] = 0;
                            //row["P_RCODTER"] = null;
                            if (rlv_articulos.InsertItem != null)
                            {
                                row["P_RTIPPRO"] = (rlv_articulos.InsertItem.FindControl("rc_categoria") as RadComboBox).SelectedValue;
                                row["P_RCLAVE1"] = (rlv_articulos.InsertItem.FindControl("txt_referencia") as RadTextBox).Text;
                            }
                            else
                            {
                                row["P_RTIPPRO"] = (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue;
                                row["P_RCLAVE1"] = (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text;
                            }
                            row["CLAVE2"] = ((sender as ImageButton).Parent.FindControl("rc_clave2pre") as RadComboBox).Text;
                            row["CLAVE3"] = ((sender as ImageButton).Parent.FindControl("rc_clave3pre") as RadComboBox).Text;

                            row["P_RCLAVE2"] = lc_c2;
                            row["P_RCLAVE3"] = lc_c3;
                            row["P_RCLAVE4"] = ".";
                            row["P_RCODCLA"] = ".";
                            row["P_RCODCAL"] = ".";
                            row["P_RUNDPRE"] = "UN";
                            row["P_RPRECIO"] = ((sender as ImageButton).Parent.FindControl("txt_precio") as RadNumericTextBox).Value;
                            row["P_RCANMIN"] = 0;
                            row["P_RESTADO"] = "AC";
                            row["P_RCAUSAE"] = ".";
                            row["P_RNMUSER"] = Convert.ToString(Session["UserLogon"]);
                            row["P_RFECING"] = System.DateTime.Today;
                            row["P_RFECMOD"] = System.DateTime.Today;

                            tbPrecios.Rows.Add(row);
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
                }
                else
                {
                    lc_lst = ((sender as ImageButton).Parent.FindControl("rc_lstprecio") as RadComboBox).SelectedValue;
                    lc_c2 = ((sender as ImageButton).Parent.FindControl("rc_clave2pre") as RadComboBox).SelectedValue;
                    lc_c3 = ((sender as ImageButton).Parent.FindControl("rc_clave3pre") as RadComboBox).SelectedValue;

                    foreach (DataRow rx in tbPrecios.Rows)
                    {
                        if ((Convert.ToString(rx["P_RLISPRE"]) == lc_lst) && (Convert.ToString(rx["P_RCLAVE2"]) == lc_c2) && (Convert.ToString(rx["P_RCLAVE3"]) == lc_c3))
                            rx["P_RPRECIO"] = ((sender as ImageButton).Parent.FindControl("txt_precio") as RadNumericTextBox).Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                rw = null;
            }
        }
        protected void rg_imagenes_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbImagenes;
        }
        protected void rg_items_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbBarras;
        }
        protected void rgTester_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbTester;
        }
        protected void rg_origen_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbOrigen;
        }
        protected void AsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            //Clear changes and remove uploaded image from Cache
            //RadImageEditor1.ResetChanges();

            Context.Cache.Remove(Session.SessionID + "UploadedFile");
            using (Stream stream = e.File.InputStream)
            {
                byte[] imgData = new byte[stream.Length];
                stream.Read(imgData, 0, imgData.Length);
                MemoryStream ms = new MemoryStream();
                ms.Write(imgData, 0, imgData.Length);

                Context.Cache.Insert(Session.SessionID + "UploadedFile", ms, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);            
            }
            RadImageEditor x = new RadImageEditor();
            
        }        
        protected void rg_imagenes_OnItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName =="process")
            //    e.Canceled = true;
            ArticulosBL Obj = new ArticulosBL();
            string lc_c2=".",lc_c3=".";
            try
            {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        {
                            string lc_ruta = MapPath("~/Uploads/" + (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text);
                            EditableImage ei = (e.Item.FindControl("RadImageEditor1") as RadImageEditor).GetEditableImage();
                            ei.Image.Save(lc_ruta + "." + ei.Format, ei.RawFormat);
                            //Agregar Extencion
                            lc_ruta += "." + ei.Format;
                            if (tbClave2.Rows.Count > 0)
                            {
                                foreach (DataRow rw in tbClave2.Rows)
                                {
                                    lc_c2 = Convert.ToString(rw["ARCLAVE2"]);
                                    break;
                                }
                                if (tbClave3.Rows.Count > 0)
                                    foreach (DataRow rw in tbClave3.Rows)
                                    {
                                        lc_c3 = Convert.ToString(rw["ARCLAVE3"]);
                                        break;
                                    }
                                else
                                    lc_c3 = ".";
                            }
                            else
                            {
                                lc_c2 = ".";
                            }

                            Obj.InsertImagenArticulo(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text, lc_c2, lc_c3, ".", 1, lc_ruta, Convert.ToString(Session["UserLogon"]));
                            break;
                        }
                    case "NewPhoto":                        
                        myIframe.Attributes["src"] = "//" + HttpContext.Current.Request.Url.Authority + "/webcam.aspx";
                        string script = "function f(){$find(\"" + mp_cam.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                        break;
                }
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
        protected void RadImageEditor1_ImageLoading(object sender, ImageEditorLoadingEventArgs args)
        {
            //Handle Uploaded images
            if (!Object.Equals(Context.Cache.Get(Session.SessionID + "UploadedFile"), null))
            {
                using (EditableImage image = new EditableImage((MemoryStream)Context.Cache.Remove(Session.SessionID + "UploadedFile")))
                {
                    args.Image = image.Clone();
                    args.Cancel = true;
                }
            }
        }
        protected void rgSoportes_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            (source as RadGrid).DataSource = tbSoportes;
        }
        protected void rgSoportes_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string lc_extencion = "";
            if (e.CommandName == "download_file")
            {
                byte[] archivo = null;
                GridDataItem ditem = (GridDataItem)e.Item;
                int item = Convert.ToInt32(ditem["SP_CONSECUTIVO"].Text);

                SoportesBL Obj = new SoportesBL();
                foreach (DataRow rw in (Obj.GetSoporte(null, item) as DataTable).Rows)
                {
                    archivo = ((byte[])rw["SP_IMAGEN"]);
                    lc_extencion = Convert.ToString(rw["SP_EXTENCION"]);
                }
                Obj = null;
                ditem = null;
                Random random = new Random();
                int random_0 = random.Next(0, 100);
                int random_1 = random.Next(0, 100);
                int random_2 = random.Next(0, 100);
                int random_3 = random.Next(0, 100);
                int random_4 = random.Next(0, 100);
                int random_5 = random.Next(0, 100);
                string lc_nombre = Convert.ToString(random_0) + Convert.ToString(random_1) + Convert.ToString(random_2) + Convert.ToString(random_3) + Convert.ToString(random_4) + Convert.ToString(random_5) + Convert.ToString(item) + "" + lc_extencion;
                string path = MapPath("~/Uploads/" + lc_nombre);

                File.WriteAllBytes(path, archivo);
                byte[] bts = System.IO.File.ReadAllBytes(path);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lc_nombre);
                Response.BinaryWrite(bts);
                Response.Flush();
                Response.End();
            }
        }
        protected void rauCargarSoporte_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {            
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
        }
        protected void btn_aceptarSoporte_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbSoportes.NewRow();            
            try
            {
                row["SP_CONSECUTIVO"] = tbSoportes.Rows.Count + 5;
                row["SP_REFERENCIA"] = 0;
                row["SP_DESCRIPCION"] = (((Button)sender).Parent.FindControl("edt_nombre") as RadTextBox).Text;
                row["SP_EXTENCION"] = Path.GetExtension(prArchivo);
                row["SP_TIPO"] = "3";
                row["SP_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["RUTA"] = prArchivo;
                row["SP_FECING"] = System.DateTime.Now;

                tbSoportes.Rows.Add(row);
                if (rlv_articulos.InsertItem != null) (rlv_articulos.InsertItem.FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
                else (rlv_articulos.EditItems[0].FindControl("rgSoportes") as RadGrid).DataSource = tbSoportes;
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
        protected void iBtnFindArticulo_OnClick(object sender, EventArgs e)
        {
            string script = "function f(){$find(\"" + (((ImageButton)sender).Parent.FindControl("modalFiltroArt") as RadWindow).ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_aceptartester_OnClick(object sender, EventArgs e)
        {
            DataRow item = tbTester.NewRow();
            try {

                item["TT_CODIGO"] = tbTester.Rows.Count+4;
                item["ARCODEMP"] = ".";
                item["ARTIPPRO"] = ".";
                item["ARCLAVE1"] = ".";
                item["ARCLAVE2"] = ".";
                item["ARCLAVE3"] = ".";
                item["ARCLAVE4"] = ".";
                item["TT_CODEMP"] = ".";
                item["TT_TIPPRO"] = ((sender as ImageButton).Parent.FindControl("txt_tptester") as RadTextBox).Text;
                item["TT_CLAVE1"] = ((sender as ImageButton).Parent.FindControl("txt_c1tester") as RadTextBox).Text;
                item["TT_CLAVE2"] = ".";
                item["TT_CLAVE3"] = ".";
                item["TT_CLAVE4"] = ".";
                item["TT_FECING"] = System.DateTime.Today;
                item["TT_USUARIO"] = ".";
                item["TT_ESTADO"] = "AC";
                item["ARNOMBRE"] = ((sender as ImageButton).Parent.FindControl("txt_destester") as RadTextBox).Text;
                item["TANOMBRE"] = ((sender as ImageButton).Parent.FindControl("txt_nmtester") as RadTextBox).Text;
                item["BCODIGO"] = ".";
                

                tbTester.Rows.Add(item);
                if (rlv_articulos.InsertItem != null) (rlv_articulos.InsertItem.FindControl("rgTester") as RadGrid).DataSource = tbTester;
                else (rlv_articulos.EditItems[0].FindControl("rgTester") as RadGrid).DataSource = tbTester;
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
        protected void btn_aceptorigen_OnClick(object sender, EventArgs e)
        {
            DataRow row = tbOrigen.NewRow();
            try
            {
                row["OR_CODIGO"] = tbOrigen.Rows.Count + 5;
                row["ARCODEMP"] = Convert.ToString(Session["CODEMP"]);
                row["ARTIPPRO"] = ".";
                row["ARCLAVE1"] = ".";
                row["ARCLAVE2"] = ".";
                row["ARCLAVE3"] = ".";
                row["ARCLAVE4"] = ".";
                row["OR_REFERENCIA"] = (((Button)sender).Parent.FindControl("txt_origenreferencia") as RadTextBox).Text;
                row["OR_FECING"] = System.DateTime.Now; 
                row["OR_USUARIO"] = Convert.ToString(Session["UserLogon"]);
                row["OR_ESTADO"] = "AC";
                

                tbOrigen.Rows.Add(row);
                if (rlv_articulos.InsertItem != null) (rlv_articulos.InsertItem.FindControl("rg_origen") as RadGrid).DataSource = tbOrigen;
                else (rlv_articulos.EditItems[0].FindControl("rg_origen") as RadGrid).DataSource = tbOrigen;
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
        protected void btn_filtroArticulos_OnClick(object sender, EventArgs e)
        {
            string lsql = " 1=1";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text))
                lsql += " AND ARCLAVE1 LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_referencia") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text))
                lsql += " AND ARNOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("edt_nombreart") as RadTextBox).Text + "%'";

            
            lsql += " ";
            obj_tester.SelectParameters["filter"].DefaultValue = lsql;
            ((sender as RadButton).Parent.FindControl("rgConsultaArticulos") as RadGrid).DataBind();

            string script = "function f(){$find(\"" + (((RadButton)sender).Parent.Parent.Parent.FindControl("modalFiltroArt") as RadWindow).ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rgConsultaArticulos_OnItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
             switch (e.CommandName)
            {
                case "Select":
                GridDataItem item = (GridDataItem)e.Item;                
                try
                {
                    ((source as RadGrid).Parent.Parent.Parent.Parent.Parent.FindControl("txt_tptester") as RadTextBox).Text =  Convert.ToString(item["ARTIPPRO"].Text);
                    ((source as RadGrid).Parent.Parent.Parent.Parent.Parent.FindControl("txt_nmtester") as RadTextBox).Text = Convert.ToString(item["ARTIPPRO"].Text);
                    ((source as RadGrid).Parent.Parent.Parent.Parent.Parent.FindControl("txt_c1tester") as RadTextBox).Text = Convert.ToString(item["ARCLAVE1"].Text);
                    ((source as RadGrid).Parent.Parent.Parent.Parent.Parent.FindControl("txt_destester") as RadTextBox).Text = Convert.ToString(item["ARNOMBRE"].Text);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    item = null;
                }
                break;
            }
        }
        protected void btn_aceptarbarras_Click(object sender, ImageClickEventArgs e)
        {
            DataRow row = tbBarras.NewRow();
            try
            {
                if ((sender as ImageButton).CommandName == "PerformInsert")
                {
                    string lc_c2 = ((sender as ImageButton).Parent.FindControl("rc_clave2bar") as RadComboBox).SelectedValue;
                    string lc_c3 = ((sender as ImageButton).Parent.FindControl("rc_clave2bar") as RadComboBox).SelectedValue;

                    row["BCODEMP"] = Convert.ToString(Session["CODEMP"]);

                    if (rlv_articulos.InsertItem != null)
                    {
                        row["BTIPPRO"] = (rlv_articulos.InsertItem.FindControl("rc_categoria") as RadComboBox).SelectedValue;
                        row["BCLAVE1"] = (rlv_articulos.InsertItem.FindControl("txt_referencia") as RadTextBox).Text;
                    }
                    else
                    {
                        row["BTIPPRO"] = (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue;
                        row["BCLAVE1"] = (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text;
                    }
                    
                    row["BCLAVE2"] = lc_c2;
                    row["BCLAVE3"] = lc_c3;
                    row["BCLAVE4"] = ".";
                    row["BCODCAL"] = ".";
                    row["BCODIGO"] = (((ImageButton)sender).Parent.FindControl("txt_barras") as RadTextBox).Text;
                    row["BFECCRE"] = System.DateTime.Now;
                    row["BFECMOD"] = System.DateTime.Now;
                    row["BMNUSER"] = ".";
                    row["BCOPAIS"] = "770";
                    row["BEMPRES"] = "001";
                    row["BTBARRA"] = "1";
                    row["CLAVE2"] = ((sender as ImageButton).Parent.FindControl("rc_clave2bar") as RadComboBox).Text;
                    row["CLAVE3"] = ((sender as ImageButton).Parent.FindControl("rc_clave2bar") as RadComboBox).Text;
                    tbBarras.Rows.Add(row);
                }

                if (rlv_articulos.InsertItem != null) (rlv_articulos.InsertItem.FindControl("rg_items") as RadGrid).DataSource = tbBarras;
                else (rlv_articulos.EditItems[0].FindControl("rg_items") as RadGrid).DataSource = tbBarras;
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
        protected void rgTester_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            ArticulosBL Obj = new ArticulosBL();
            try {
                switch (e.CommandName)
                {
                    case "Delete":
                        var TT_CODIGO = item.GetDataKeyValue("TT_CODIGO").ToString();
                        Obj.DeteleTester(null, Convert.ToInt32(TT_CODIGO));
                        tbTester = Obj.GetTester(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text, ".", ".", ".");
                        (sender as RadGrid).DataSource = tbTester;
                        (sender as RadGrid).DataBind();
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
                Obj = null;
            }
        }
        protected void btn_barancel_Click(object sender, EventArgs e)
        {
            txt_codarancel.Focus();
            string script = "function f(){$find(\"" + mpAranceles.ClientID + "\").show(); $find(\"" + txt_codarancel.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void btn_filtroarancel_Click(object sender, EventArgs e)
        {
            string lsql = " AND 1=1";

            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("txt_codarancel") as RadTextBox).Text))
                lsql += " AND UA_CODIGO LIKE '%" + ((sender as RadButton).Parent.FindControl("txt_codarancel") as RadTextBox).Text + "%'";
            if (!string.IsNullOrEmpty(((sender as RadButton).Parent.FindControl("txt_nomarancel") as RadTextBox).Text))
                lsql += " AND UA_NOMBRE LIKE '%" + ((sender as RadButton).Parent.FindControl("txt_nomarancel") as RadTextBox).Text + "%'";


            lsql += " ";
            obj_aranceles.SelectParameters["filter"].DefaultValue = lsql;
            ((sender as RadButton).Parent.FindControl("rg_Aranceles") as RadGrid).DataBind();

            string script = "function f(){$find(\"" + mpAranceles.ClientID + "\").show(); $find(\"" + txt_codarancel.ClientID + "\").focus(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        protected void rg_Aranceles_ItemCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                if (e.CommandName == "Select")
                {
                    if (rlv_articulos.InsertItem != null)
                        (rlv_articulos.InsertItem.FindControl("txt_posaracelaria") as RadTextBox).Text = Convert.ToString(item["UA_CODIGO"].Text);
                    else
                        (rlv_articulos.Items[0].FindControl("txt_posaracelaria") as RadTextBox).Text = Convert.ToString(item["UA_CODIGO"].Text);
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
        protected void rg_rsanitarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = tbRSanitario;
        }
        protected void btn_aceptar_rs_Click(object sender, ImageClickEventArgs e)
        {
            DataRow rw = tbRSanitario.NewRow();
            try
            {
                if ((sender as ImageButton).CommandName == "PerformInsert")
                {
                    rw["RS_CODIGO"] = tbRSanitario.Rows.Count + 1;
                    rw["ARCODEMP"] = Convert.ToString(Session["CODEMP"]);
                    rw["ARTIPPRO"] = ".";
                    rw["ARCLAVE1"] = ".";
                    rw["ARCLAVE2"] = ".";
                    rw["ARCLAVE3"] = ".";
                    rw["ARCLAVE4"] = ".";
                    rw["RS_REGISTRO"] = ((sender as ImageButton).Parent.FindControl("txt_rsanitario") as RadTextBox).Text;
                    rw["RS_FEINICIO"] = ((sender as ImageButton).Parent.FindControl("edt_finicial") as RadDatePicker).SelectedDate;
                    rw["RS_FECFINAL"] = ((sender as ImageButton).Parent.FindControl("edt_ffinal") as RadDatePicker).SelectedDate;
                    rw["RS_USUARIO"] = ".";
                    rw["RS_ESTADO"] = ".";
                    rw["RS_FECMOD"] = System.DateTime.Today;

                    tbRSanitario.Rows.Add(rw);
                }
                else
                {
                    foreach (DataRow rx in tbRSanitario.Rows)
                    {
                        if (Convert.ToInt32(rx["RS_CODIGO"]) == Convert.ToInt32(((sender as ImageButton).Parent.FindControl("txt_id") as RadTextBox).Text))
                        {
                            rx["RS_REGISTRO"] = ((sender as ImageButton).Parent.FindControl("txt_rsanitario") as RadTextBox).Text;
                            rx["RS_FEINICIO"] = ((sender as ImageButton).Parent.FindControl("edt_finicial") as RadDatePicker).SelectedDate;
                            rx["RS_FECFINAL"] = ((sender as ImageButton).Parent.FindControl("edt_ffinal") as RadDatePicker).SelectedDate;
                        }
                    }
                }

                if (rlv_articulos.InsertItem != null) (rlv_articulos.InsertItem.FindControl("rg_rsanitarios") as RadGrid).DataSource = tbRSanitario;
                else (rlv_articulos.EditItems[0].FindControl("rg_rsanitarios") as RadGrid).DataSource = tbRSanitario;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Obj = null;
                rw = null;
            }
        }
        protected void rg_rsanitarios_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            ArticulosBL Obj = new ArticulosBL();
            int i = 1;
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var RS_CODIGO = Convert.ToInt32((item.FindControl("txt_codigors") as RadTextBox).Text);
                        int pos = 0, xpos = 0;
                        Boolean lb_ibd = false;
                        foreach (DataRow row in tbRSanitario.Rows)
                        {
                            if (Convert.ToInt32(row["RS_CODIGO"]) == Convert.ToInt32(RS_CODIGO))
                            {
                                if (Convert.ToString(row["ARCLAVE1"]) == ".")
                                    lb_ibd = true;                                
                                pos = xpos;
                            }
                            xpos++;
                        }

                        
                        
                        tbRSanitario.Rows[pos].Delete();
                        tbRSanitario.AcceptChanges();

                        foreach (DataRow rw in tbRSanitario.Rows)
                        {
                            if (Convert.ToString(rw["ARCLAVE1"]) == ".")
                            {
                                rw["RS_CODIGO"] = i;
                                i++;
                            }
                        }

                        if (!lb_ibd)                        
                            Obj.DeleteRegistroSanitario(null, Convert.ToInt32(RS_CODIGO));
                        
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
                Obj = null;
            }
        }
        protected void rg_items_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            ArticulosBL Obj = new ArticulosBL();            
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var BCODIGO = item["BCODIGO"].Text;
                        int pos = 0, xpos = 0;                        
                        foreach (DataRow row in tbBarras.Rows)
                        {
                            if (Convert.ToString(row["BCODIGO"]) == Convert.ToString(BCODIGO))                                                            
                                pos = xpos;                            
                            xpos++;
                        }

                        tbBarras.Rows[pos].Delete();
                        tbBarras.AcceptChanges();                        
                        Obj.DeleteTbBarras(null, Convert.ToString(Session["CODEMP"]),Convert.ToString(BCODIGO));

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
                Obj = null;
            }
        }
        protected void rg_origen_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            ArticulosBL Obj = new ArticulosBL();
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var OR_CODIGO = (item.FindControl("txt_codigoor") as RadTextBox).Text;
                        int pos = 0, xpos = 0;
                        foreach (DataRow row in tbOrigen.Rows)
                        {
                            if (Convert.ToInt32(row["OR_CODIGO"]) == Convert.ToInt32(OR_CODIGO))
                                pos = xpos;
                            xpos++;
                        }

                        tbOrigen.Rows[pos].Delete();
                        tbOrigen.AcceptChanges();
                        Obj.DeleteOrigen(null, Convert.ToInt32(OR_CODIGO));

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
                Obj = null;
            }
        }
        protected void rg_imagenes_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            ArticulosBL Obj = new ArticulosBL();
            try
            {
                switch (e.CommandName)
                {
                    case "Delete":
                        var IM_CONSECUTIVO = item.GetDataKeyValue("IM_CONSECUTIVO").ToString();
                        Obj.DeteleImagen(null, Convert.ToInt32(IM_CONSECUTIVO));
                        tbImagenes = Obj.GetImagenes(null, Convert.ToString(Session["CODEMP"]), (rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue, (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text);
                        (sender as RadGrid).DataSource = tbImagenes;
                        (sender as RadGrid).DataBind();
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
                Obj = null;
            }
        }
        protected void btnSaveAnexo_Click(object sender, EventArgs e)
        {
            ArticulosBL Obj = new ArticulosBL();
            try
            {
                if (rlv_articulos.InsertItem != null)
                {
                    //tbAnexos.Columns["ruta"].ReadOnly = false;
                    //DataRow rw = tbAnexos.NewRow();
                    //rw["EV_CODIGO"] = 0;
                    //rw["LH_CODEMP"] = "001";
                    //rw["LH_LSTPAQ"] = 0;
                    //rw["EV_DESCRIPCION"] = txt_observaciones.Text;
                    //rw["EV_FECING"] = System.DateTime.Today;
                    //rw["EV_USUARIO"] = ".";
                    //rw["ruta"] = Convert.ToString(Session["imagePath"]);
                    //tbAnexos.Rows.Add(rw);
                    //rw = null;
                    //(rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataSource = tbAnexos;
                    //(rlv_empaque.InsertItem.FindControl("rg_anexos") as RadGrid).DataBind();
                }
                else
                {
                    if (Convert.ToString(Session["imagePath"]) != "")
                    {
                        Obj.InsertImagenArticulo(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue),
                            (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text, ".",".",".",1,Convert.ToString(Session["imagePath"]), Convert.ToString(Session["UserLogon"]));

                        tbImagenes= Obj.GetImagenes(null, Convert.ToString(Session["CODEMP"]), Convert.ToString((rlv_articulos.Items[0].FindControl("rc_categoria") as RadComboBox).SelectedValue),
                            (rlv_articulos.Items[0].FindControl("txt_referencia") as RadTextBox).Text);
                        (rlv_articulos.Items[0].FindControl("rg_imagenes") as RadGrid).DataSource = tbImagenes;
                        (rlv_articulos.Items[0].FindControl("rg_imagenes") as RadGrid).DataBind();

                    }
                    else
                    {
                        litTextoMensaje.Text = "No Se Capturo Foto!";
                        string script = "function f(){$find(\"" + modalMensaje.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    }
                }
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
        protected void rlv_articulos_ItemInserting(object sender, RadListViewCommandEventArgs e)
        {
            obj_articulos.InsertParameters["ARDTTEC1"].DefaultValue = (e.ListViewItem.FindControl("rc_dt1") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC2"].DefaultValue = (e.ListViewItem.FindControl("rc_dt2") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC3"].DefaultValue = (e.ListViewItem.FindControl("rc_dt3") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC4"].DefaultValue = (e.ListViewItem.FindControl("rc_dt4") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC5"].DefaultValue = (e.ListViewItem.FindControl("rc_dt5") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC7"].DefaultValue = (e.ListViewItem.FindControl("rc_dt7") as RadComboBox).SelectedValue;
            obj_articulos.InsertParameters["ARDTTEC8"].DefaultValue = (e.ListViewItem.FindControl("rc_dt8") as RadComboBox).SelectedValue;            
        }
        protected void rlv_articulos_ItemUpdating(object sender, RadListViewCommandEventArgs e)
        {
            obj_articulos.UpdateParameters["ARDTTEC1"].DefaultValue = (e.ListViewItem.FindControl("rc_dt1") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC2"].DefaultValue = (e.ListViewItem.FindControl("rc_dt2") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC3"].DefaultValue = (e.ListViewItem.FindControl("rc_dt3") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC4"].DefaultValue = (e.ListViewItem.FindControl("rc_dt4") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC5"].DefaultValue = (e.ListViewItem.FindControl("rc_dt5") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC7"].DefaultValue = (e.ListViewItem.FindControl("rc_dt7") as RadComboBox).SelectedValue;
            obj_articulos.UpdateParameters["ARDTTEC8"].DefaultValue = (e.ListViewItem.FindControl("rc_dt8") as RadComboBox).SelectedValue;
        }
        protected void rg_precios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode && e.Item is GridEditFormItem)
            {
                GridEditFormItem editForm = (GridEditFormItem)e.Item;
                RadComboBoxItem item = new RadComboBoxItem();
                
                try
                {
                    RadComboBoxItem itemi_ = new RadComboBoxItem();
                    itemi_.Value = ".";
                    itemi_.Text = "Todos";
                    ((RadComboBox)editForm.FindControl("rc_clave2pre")).Items.Add(itemi_);
                    itemi_ = null;

                    foreach (DataRow rw in tbClave2.Rows)
                    { 
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["ARCLAVE2"]);
                        itemi.Text = Convert.ToString(rw["ASNOMBRE"]);
                        ((RadComboBox)editForm.FindControl("rc_clave2pre")).Items.Add(itemi);
                        itemi = null;
                        
                        
                    }



                    //if (tbClave2.Rows.Count == 0)
                    //{
                    //    RadComboBoxItem itemi = new RadComboBoxItem();
                    //    itemi.Value = ".";
                    //    itemi.Text = "Todos";
                    //    ((RadComboBox)editForm.FindControl("rc_clave2pre")).Items.Add(itemi);
                    //    itemi = null;
                    //}
                    RadComboBoxItem itemi__ = new RadComboBoxItem();
                    itemi__.Value = ".";
                    itemi__.Text = "Todos";
                    ((RadComboBox)editForm.FindControl("rc_clave3pre")).Items.Add(itemi__);
                    itemi__ = null;

                    foreach (DataRow rw in tbClave3.Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["ARCLAVE3"]);
                        itemi.Text = Convert.ToString(rw["ASNOMBRE"]);
                        ((RadComboBox)editForm.FindControl("rc_clave3pre")).Items.Add(itemi);
                        itemi = null;

                        
                    }

                    

                    //if (tbClave3.Rows.Count == 0)
                    //{
                    //    RadComboBoxItem itemi = new RadComboBoxItem();
                    //    itemi.Value = ".";
                    //    itemi.Text = "Todos";
                    //    ((RadComboBox)editForm.FindControl("rc_clave3pre")).Items.Add(itemi);
                    //    itemi = null;
                    //}

                    if (!(e.Item is GridEditFormInsertItem))
                    {
                        DataRow fila = ((DataRowView)(e.Item).DataItem).Row;
                        ((RadComboBox)editForm.FindControl("rc_clave3pre")).SelectedValue = fila["P_RCLAVE3"].ToString();
                        ((RadComboBox)editForm.FindControl("rc_clave2pre")).SelectedValue = fila["P_RCLAVE2"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    editForm = null;                    
                }
            }
        }
        protected void rg_items_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode && e.Item is GridEditFormItem)
            {
                GridEditFormItem editForm = (GridEditFormItem)e.Item;                
                
                try
                {
                    foreach (DataRow rw in tbClave2.Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["ARCLAVE2"]);
                        itemi.Text = Convert.ToString(rw["ASNOMBRE"]);
                        ((RadComboBox)editForm.FindControl("rc_clave2bar")).Items.Add(itemi);
                        itemi = null;
                    }

                    if (tbClave2.Rows.Count == 0)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = ".";
                        itemi.Text = "Todos";
                        ((RadComboBox)editForm.FindControl("rc_clave2bar")).Items.Add(itemi);
                        itemi = null;
                    }

                    foreach (DataRow rw in tbClave3.Rows)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = Convert.ToString(rw["ARCLAVE3"]);
                        itemi.Text = Convert.ToString(rw["ASNOMBRE"]);
                        ((RadComboBox)editForm.FindControl("rc_clave3bar")).Items.Add(itemi);
                        itemi = null;
                    }

                    if (tbClave3.Rows.Count == 0)
                    {
                        RadComboBoxItem itemi = new RadComboBoxItem();
                        itemi.Value = ".";
                        itemi.Text = "Todos";
                        ((RadComboBox)editForm.FindControl("rc_clave3bar")).Items.Add(itemi);
                        itemi = null;
                    }

                    if (!(e.Item is GridEditFormInsertItem))
                    {
                        DataRow fila = ((DataRowView)(e.Item).DataItem).Row;
                        ((RadComboBox)editForm.FindControl("rc_clave2bar")).SelectedValue = fila["BCLAVE2"].ToString();
                        ((RadComboBox)editForm.FindControl("rc_clave3bar")).SelectedValue = fila["BCLAVE3"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    editForm = null;                    
                }
            }
        }
        protected void btn_rfsdt1_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt1 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 5);
                (((RadButton)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataSource = dt1;
                (((RadButton)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }
        protected void btn_rfsdt2_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt2 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 6);
                (((RadButton)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataSource = dt2;
                (((RadButton)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }
        protected void btn_rfsdt3_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt3 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 7);
                (((RadButton)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataSource = dt3;
                (((RadButton)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }
        protected void btn_rfsdt4_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt4 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 8);
                (((RadButton)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataSource = dt4;
                (((RadButton)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }
        protected void btn_rfsdt5_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt5 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 9);
                (((RadButton)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataSource = dt5;
                (((RadButton)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }
        protected void btn_rfsdt7_Click(object sender, EventArgs e)
        {
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            try
            {
                DataTable dt7 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 10);
                (((RadButton)sender).Parent.FindControl("rc_dt7") as RadComboBox).DataSource = dt7;
                (((RadButton)sender).Parent.FindControl("rc_dt7") as RadComboBox).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
            }
        }

        protected void rc_categoria_find_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {             
            ClavesAlternasBL ObjA = new ClavesAlternasBL();
            ComunBL ObjC = new ComunBL();
            try
            {
                //Cargar TP o Lineas
                using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue))
                {
                    while (reader.Read())
                    {                       
                        //Clasificaciones
                        (((RadComboBox)sender).Parent.FindControl("lbl_clasificacion1") as Label).Text = Convert.ToString(reader["TACDCLA1"]);
                        (((RadComboBox)sender).Parent.FindControl("lbl_clasificacion2") as Label).Text = Convert.ToString(reader["TACDCLA2"]);
                        (((RadComboBox)sender).Parent.FindControl("lbl_clasificacion3") as Label).Text = Convert.ToString(reader["TACDCLA3"]);
                        (((RadComboBox)sender).Parent.FindControl("lbl_clasificacion4") as Label).Text = Convert.ToString(reader["TACDCLA4"]);
                        (((RadComboBox)sender).Parent.FindControl("lbl_clasificacion5") as Label).Text = Convert.ToString(reader["TACDCLA5"]);
                        

                        DataTable dt1 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 5);
                        DataTable dt2 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 6);
                        DataTable dt3 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 7);
                        DataTable dt4 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 8);
                        DataTable dt5 = ObjA.GetClavesAlternas(null, Convert.ToString(Session["CODEMP"]), ((RadComboBox)sender).SelectedValue, 9);
                        
                        (((RadComboBox)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataSource = dt1;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataSource = dt2;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataSource = dt3;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataSource = dt4;
                        (((RadComboBox)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataSource = dt5;

                        (((RadComboBox)sender).Parent.FindControl("rc_dt1") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt2") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt3") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt4") as RadComboBox).DataBind();
                        (((RadComboBox)sender).Parent.FindControl("rc_dt5") as RadComboBox).DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ObjA = null;
                ObjC = null;
            }
        }
    }
}