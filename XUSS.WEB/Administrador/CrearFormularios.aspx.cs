using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using BE.Administracion;
using BLL.Administracion;
using BE.General;
using System.Text;
using BE.Web;
using XUSS.BLL.Administracion;

namespace Administrador

{
    public partial class CrearFormularios : System.Web.UI.Page
	{
		private List<Campos> lista;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (RadListView11.SelectedIndexes.Count == 0)
			{
				RadListView11.SelectedIndexes.Add(0);

			}

			if (IsPostBack)
			{
				lista = ViewState["lista"] as List<Campos>;
			}
			else
			{
				fsDetails.Visible = false;
				ViewState["toolbars"] = true;
			}

			if (lista == null)
			{
				lista = new List<Campos>();
			}
		}

        //public override void AddControls()
        //{
            //base.AddControls();
            //FormControls control = new FormControls("RadListView11", "ListView", null);
            //Lista.Add(control);
            //control = new FormControls("RadDataPager1", "Paginador", "RadListView11");
            //Lista.Add(control);
            //control = new FormControls("iBtnInitInsert", "Insert", "RadListView11");
            //Lista.Add(control);
            //control = new FormControls("iBtnEdit", "Edit", "RadListView11");
            //Lista.Add(control);
            //control = new FormControls("iBtnDelete", "Delete", "RadListView11");
            //Lista.Add(control);
        //}

		protected void RadListView1_ItemCommand(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
		{
			string pagina;
			Assembly assembly;
			if (e.CommandName == "Buscar")
			{
				ObjectDataSource1.SelectParameters["filter"].DefaultValue = "1=0";
				RadListView11.DataBind();
				rtvArbol.DataSource = null;
				rtvArbol.Nodes.Clear();
				fsDetails.Visible = false;
				e.Canceled = true;
				return;
			}
			ViewState["isClickInsert"] = false;
            switch (e.CommandName)
            {
                case "Edit":
                    rtvArbol.DataSource = null;
                    rtvArbol.Nodes.Clear();
                    pagina = (e.ListViewItem.FindControl("RadTextBox2") as RadTextBox).Text;
                    assembly = Assembly.GetExecutingAssembly();
                    pagina = pagina.Replace("/", ".");
                    if (pagina.Length > 5)
                    {
                        pagina = pagina.Substring(0, pagina.LastIndexOf("aspx") - 1);
                        PageBase pageBase = assembly.CreateInstance("XUSS.WEB." + pagina) as PageBase;
                        if (pageBase != null)
                        {
                            pageBase.AddControls();
                            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
                            AdmiControlBL admiControlBL = new AdmiControlBL();
                            List<AdmiControl> liAdmiControl = admiControlBL.GetByIdFormaEstadoForma(null, null, 0, 0, (int)item.GetDataKeyValue("FormFormulario"), true);
                            List<AdmiControl> liNoEncontrados = new List<AdmiControl>();
                            foreach (AdmiControl admiControl in liAdmiControl)
                            {
                                FormControls control = pageBase.Lista.Find(t => t.Id == admiControl.CtrlNombre);

                                if (control != null)
                                {
                                    control.ExisteEnBaseDatos = true;
                                    control.Estado = admiControl.CtrlEstado;
                                }
                                else
                                {
                                    liNoEncontrados.Add(admiControl);
                                }
                            }

                            List<AdmiControl> liAdmiControlNoExistentes = admiControlBL.GetByIdFormaEstadoForma(null, null, 0, 0, (int)item.GetDataKeyValue("FormFormulario"), false);
                            List<FormControls> liControlesFromNoEncotrados = pageBase.Lista.FindAll(t => t.ExisteEnBaseDatos == false);
                            liAdmiControlNoExistentes.ForEach(t => liNoEncontrados.Add(t));
                            ViewState["ListaControlesForm"] = pageBase.Lista;
                            ViewState["admiControl"] = liAdmiControl;
                            if ((liControlesFromNoEncotrados != null && liControlesFromNoEncotrados.Count != 0) || liNoEncontrados.Count != 0)
                            {
                                lstbColtrolBD.DataValueField = "CtrlNombre";
                                lstbColtrolBD.DataTextField = "CtrlDescripcion";
                                lstbColtrolBD.DataSource = liNoEncontrados;
                                lstbColtrolBD.DataBind();
                                lstbControlForm.DataTextField = "Descripcion";
                                lstbControlForm.DataValueField = "Id";
                                lstbControlForm.DataSource = liControlesFromNoEncotrados;
                                lstbControlForm.DataBind();
                                lista.Clear();
                                ViewState["lista"] = lista;
                                GridView1.DataSource = lista;
                                GridView1.DataBind();
                                ModalPopupExtender145.Show();
                            }
                            else
                            {
                                rtvArbol.DataFieldID = "Id";
                                rtvArbol.DataFieldParentID = "IdPadre";
                                rtvArbol.DataTextField = "Descripcion";
                                rtvArbol.DataValueField = "Id";
                                rtvArbol.DataSource = pageBase.Lista;
                                rtvArbol.DataBind();
                            }
                        }
                    }
                    else
                    {

                    }
                    break;

                case "BuscarControles":
                    Button botonInsert;
                    if ("Form".Equals(e.CommandArgument))
                    {
                        botonInsert = e.ListViewItem.FindControl("FormView1").FindControl("btnInsert") as Button;
                        pagina = (e.ListViewItem.FindControl("FormView1").FindControl("RadTextBox2") as RadTextBox).Text;
                    }
                    else
                    {
                        botonInsert = e.ListViewItem.FindControl("btnInsert") as Button;
                        pagina = (e.ListViewItem.FindControl("RadTextBox2") as RadTextBox).Text;
                    }

                    assembly = Assembly.GetExecutingAssembly();
                    pagina = pagina.Replace("/", ".");
                    if (pagina.Length > 5)
                    {
                        pagina = pagina.Substring(0, pagina.LastIndexOf("aspx") - 1);
                        PageBase pageBase = assembly.CreateInstance("AseingesOut.SCW.WEB.SIC." + pagina) as PageBase;
                        if (pageBase != null)
                        {
                            pageBase.AddControls();
                            rtvArbol.DataFieldID = "Id";
                            rtvArbol.DataFieldParentID = "IdPadre";
                            rtvArbol.DataTextField = "Descripcion";
                            rtvArbol.DataValueField = "Id";
                            rtvArbol.DataSource = pageBase.Lista;
                            rtvArbol.DataBind();
                            ViewState["ListaControlesForm"] = pageBase.Lista;
                            botonInsert.Enabled = true;
                        }
                        else
                        {
                            botonInsert.Enabled = false;
                        }
                    }
                    else
                    {
                        botonInsert.Enabled = false;
                    }
                    e.Canceled = true;
                    break;

                case "InitInsert":
                    fsDetails.Visible = true;
                    rtvArbol.DataSource = null;
                    rtvArbol.Nodes.Clear();
                    ViewState["isClickInsert"] = true;
                    break;
            }
            this.AnalizarCommand(e.CommandName);
		}

		protected void RadListView1_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
		{
			if (e.Item.ItemType == RadListViewItemType.DataItem)
			{
				if (Convert.ToBoolean(ViewState["isClickInsert"]))
				{
					e.Item.FindControl("pnItemMaster").Visible = false;
				}
				else
				{
					ViewState["toolbars"] = true;
					RadListViewDataItem item = e.Item as RadListViewDataItem;
					AdmiControlBL admiControlBL = new AdmiControlBL();
					List<AdmiControl> admiControl = admiControlBL.GetByIdFormaEstadoForma(null, null, 0, 0, (int)item.GetDataKeyValue("FormFormulario"), true);
					rtvArbol.DataFieldID = "CtrlControl";
					rtvArbol.DataFieldParentID = "CtrlPadre";
					rtvArbol.DataTextField = "CtrlDescripcion";
					rtvArbol.DataValueField = "CtrlControl";
					rtvArbol.DataSource = admiControl;
					rtvArbol.DataBind();
					e.Item.FindControl("pnItemMaster").Visible = true;
					if (rtvArbol.GetAllNodes().Count > 0)
						fsDetails.Visible = true;
					else
						fsDetails.Visible = false;
				}
			}
		}

		protected void btnAceptar_Click(object sender, EventArgs e)
		{
            //ModalPopupExtender145.Hide();
            //AdmiControlBL admiControlBL = new AdmiControlBL();
            ////List<FormControls> li = ViewState["ListaControlesForm"] as List<FormControls>;
            //List<AdmiControl> liAdmiControl = ViewState["admiControl"] as List<AdmiControl>;
            //lista.ForEach(t =>
            //{
            //    AdmiControl tmp = liAdmiControl.Find(k => k.CtrlNombre == t.FileColumnName);
            //    if (tmp != null)
            //    {
            //        FormControls tmp2 = li.Find(j => j.Id == t.LogicalName);
            //        if (tmp2 != null)
            //        {
            //            tmp2.Estado = tmp.CtrlEstado;
            //        }
            //    }
            //});
            //rtvArbol.DataFieldID = "Id";
            //rtvArbol.DataFieldParentID = "IdPadre";
            //rtvArbol.DataTextField = "Descripcion";
            //rtvArbol.DataValueField = "Id";
            //rtvArbol.DataSource = li;
            //rtvArbol.DataBind();
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			if (lstbControlForm.SelectedItem != null && lstbColtrolBD.SelectedItem != null)
			{
				Campos tmp = new Campos();
				tmp.LogicalName = lstbControlForm.SelectedItem.Value;
				tmp.FileColumnName = lstbColtrolBD.SelectedItem.Value;
				lista.Add(tmp);
				lstbControlForm.Items.RemoveAt(lstbControlForm.SelectedIndex);
				lstbColtrolBD.Items.RemoveAt(lstbColtrolBD.SelectedIndex);
				ViewState["lista"] = lista;
				GridView1.DataSource = lista;
				GridView1.DataBind();
			}
		}
		protected void rtvArbol_NodeDataBound(object sender, RadTreeNodeEventArgs e)
		{
            if (e.Node.DataItem is FormControls)
            {
                FormControls fcTmp = e.Node.DataItem as FormControls;
                e.Node.Checked = fcTmp.Estado;
            }
            if (e.Node.DataItem is AdmiControl)
            {
                AdmiControl fcTmp = e.Node.DataItem as AdmiControl;
                e.Node.Checked = fcTmp.CtrlEstado;
            }
		}

		protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
		{
            List<FormControls> lista = ViewState["ListaControlesForm"] as List<FormControls>;
            foreach (RadTreeNode nodo in rtvArbol.GetAllNodes())
            {
                lista.Find(t => t.Id == nodo.Value).Estado = nodo.Checked;
            }
            e.InputParameters["ListaControlesForm"] = lista;
		}

		protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
		{
            List<FormControls> lista2 = ViewState["ListaControlesForm"] as List<FormControls>;
            foreach (RadTreeNode nodo in rtvArbol.GetAllNodes())
            {
                lista2.Find(t => t.Id == nodo.Value).Estado = nodo.Checked;
            }
            e.InputParameters["ListaControlesForm"] = lista2;
            e.InputParameters["lista"] = lista;
		}

		protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
		{
			RadListView11.DataBind();
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            //this.OcultarPaginador(RadListView11, "RadDataPager1", "BotonesBarra");
		}

        protected void BuscarGrilla(object sender, EventArgs e)
        {
            string filtro = " AND 1=1";

            //if ((((Button)sender).Parent.FindControl("cb_sistema") as RadComboBox).SelectedValue != "")
            //    filtro = "AND sist_sistema =" + (((Button)sender).Parent.FindControl("cb_sistema") as RadComboBox).SelectedValue + "";

            //if (!string.IsNullOrWhiteSpace((((Button)sender).Parent.FindControl("txtCodigo") as RadTextBox).Text))            
            //    filtro += " AND sist_sistema =" + (((Button)sender).Parent.FindControl("txtCodigo") as RadTextBox).Text + "";            


            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Substring(4, filtro.Length - 4);
            }

            ObjectDataSource1.SelectParameters["filter"].DefaultValue = filtro;
            RadListView11.DataBind();
            if ((RadListView11.Controls[0] is RadListViewEmptyDataItem))
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
                (RadListView11.Controls[0].FindControl("litEmptyMessage") as Literal).Text = str.ToString();
            }
        }

        protected void AnalizarCommand(string comando)
        {
            if (string.IsNullOrEmpty(comando) || comando.Equals("Cancel"))
            {
                ViewState["toolbars"] = true;
            }
            else
            {
                ViewState["toolbars"] = false;
            }
        }
	}
}