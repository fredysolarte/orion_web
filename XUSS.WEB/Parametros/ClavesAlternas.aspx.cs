using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class ClavesAlternas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rc_categoria_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            TipoProductosBL obj = new TipoProductosBL();
            
            try
            {
                var collection = (sender as RadComboBox).CheckedItems;

                if (collection.Count != 0)
                {
                    foreach (var item_ in collection)
                    {
                        (((RadComboBox)sender).Parent.FindControl("rc_nivel") as RadComboBox).Items.Clear();
                        foreach (DataRow rw in (obj.GetNivelesTP(null, item_.Value) as DataTable).Rows)
                        {
                            RadComboBoxItem item = new RadComboBoxItem();
                            item.Value = Convert.ToString(rw["NIVEL"]);
                            item.Text = Convert.ToString(rw["TADSCLA2"]);
                            (((RadComboBox)sender).Parent.FindControl("rc_nivel") as RadComboBox).Items.Add(item);
                            item = null;
                        }
                        (((RadComboBox)sender).Parent.FindControl("txt_codigo") as RadTextBox).Text = Convert.ToString(ComunBL.GetValorN(null,Convert.ToString(Session["CODEMP"]),"CONT", "ALTER"));
                        break;
                    }
                }
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
        protected void rg_items_ItemCommand(object sender, GridCommandEventArgs e)
        {
            ClavesAlternasBL Obj = new ClavesAlternasBL();
            try {
                switch (e.CommandName)
                {
                    case "PerformInsert":
                        var collection = (e.Item.FindControl("rc_categoria") as RadComboBox).CheckedItems;
                        if (collection.Count != 0)
                        {
                            int ln_codigo = ComunBL.GeneraConsecutivo(null, "ALTER");
                            foreach (var item in collection)
                            {
                                Obj.InsertClaveAlterna(null, Convert.ToString(Session["CODEMP"]), Convert.ToString(item.Value), (e.Item.FindControl("rc_nivel") as RadComboBox).SelectedValue,
                                               Convert.ToString(ln_codigo), (e.Item.FindControl("txt_nombre") as RadTextBox).Text, (e.Item.FindControl("txt_nombre") as RadTextBox).Text, "AC", ".", Convert.ToString(Session["UserLogon"]));
                            }
                        }
                        break;
                    case "Update":
                        Obj.UpdateClaveAlterna(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_nivelc") as RadTextBox).Text,
                                               (e.Item.FindControl("txt_codigo") as RadTextBox).Text, (e.Item.FindControl("txt_nombre") as RadTextBox).Text, (e.Item.FindControl("txt_nombre") as RadTextBox).Text, "AC", ".", Convert.ToString(Session["UserLogon"]));
                        break;
                    case "Delete":
                        Obj.UpdateClaveAlterna(null, Convert.ToString(Session["CODEMP"]), (e.Item.FindControl("rc_categoria") as RadComboBox).SelectedValue, (e.Item.FindControl("txt_nivelc") as RadTextBox).Text,
                                               (e.Item.FindControl("txt_codigo") as RadTextBox).Text, (e.Item.FindControl("txt_nombre") as RadTextBox).Text, (e.Item.FindControl("txt_nombre") as RadTextBox).Text, "AN", ".", Convert.ToString(Session["UserLogon"]));
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

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            string filtro = "AND 1=1";


            if (!string.IsNullOrWhiteSpace(filtro))
                filtro = filtro.Substring(4, filtro.Length - 4);

            obj_claves.SelectParameters["filter"].DefaultValue = filtro;
            rg_items.DataBind();
        }
    }
}