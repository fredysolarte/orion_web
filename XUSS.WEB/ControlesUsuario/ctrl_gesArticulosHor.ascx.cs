using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using XUSS.BLL.Comun;
using XUSS.BLL.Consultas;

namespace XUSS.WEB.ControlesUsuario
{
    public partial class ctrl_gesArticulosHor : System.Web.UI.UserControl
    {
        public Label objlabellinea
        {
            get { return this.lbl_linea; }            
        }
        public Label objlabelclave1
        {
            get { return this.lbl_clave1; }
        }
        public Label objlabelclave2
        {
            get { return this.lbl_clave2; }
        }
        public Label objlabelclave3
        {
            get { return this.lbl_clave3; }
        }
        public Label objlabelclave4
        {
            get { return this.lbl_clave4; }
        }
        public Label objlabelCodigo
        {
            get { return this.lbl_codigo; }
        }
        public RadComboBox objrcb_linea
        {
            get { return this.rcb_linea; }
        }
        public RadComboBox objrcb_clave1
        {
            get { return this.rcb_clave1; }
        }
        public RadComboBox objrcb_clave2
        {
            get { return this.rcb_clave2; }
        }
        public RadComboBox objrcb_clave3
        {
            get { return this.rcb_clave3; }
        }
        public RadComboBox objrcb_clave4
        {
            get { return this.rcb_clave4; }
        }
        public RadTextBox objedt_codigo
        {
            get { return this.edt_codigo; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void rcb_linea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComunBL ObjC = new ComunBL();
            if (rcb_linea.SelectedValue == ".")
            {
                lbl_clave1.Visible = false;
                rcb_clave1.Visible = false;
                rcb_clave1.Items.Clear();
                rcb_clave1.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                rcb_clave1.SelectedIndex = 0;
                lbl_clave2.Visible = false;
                rcb_clave2.Visible = false;
                rcb_clave2.Items.Clear();
                rcb_clave2.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                rcb_clave2.SelectedIndex = 0;
                lbl_clave3.Visible = false;
                rcb_clave3.Visible = false;
                rcb_clave3.Items.Clear();
                rcb_clave3.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                rcb_clave3.SelectedIndex = 0;
                lbl_clave4.Visible = false;
                rcb_clave4.Visible = false;
                rcb_clave4.SelectedValue = ".";
            }
            else
            {
                using (IDataReader reader = ObjC.GetCaracteristicaTP(null, Convert.ToString(Session["CODEMP"]),rcb_linea.SelectedValue))
                {
                    while (reader.Read())
                    {
                        lbl_clave1.Visible = true;
                        rcb_clave1.Visible = true;
                        rcb_clave1.Items.Clear();
                        rcb_clave1.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                        rcb_clave1.SelectedIndex = 0;
                        lbl_clave2.Visible = false;
                        rcb_clave2.Visible = false;
                        rcb_clave2.Items.Clear();
                        rcb_clave2.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                        rcb_clave2.SelectedIndex = 0;
                        lbl_clave3.Visible = false;
                        rcb_clave3.Visible = false;
                        rcb_clave3.Items.Clear();
                        rcb_clave3.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
                        rcb_clave3.SelectedIndex = 0;
                        lbl_clave4.Visible = false;
                        rcb_clave4.Visible = false;


                        lbl_clave1.Text = Convert.ToString(reader["TADSCLA1"]);
                        obj_clave1.SelectParameters["TP"].DefaultValue = rcb_linea.SelectedValue;
                        rcb_clave1.DataBind();

                        if (Convert.ToString(reader["TACTLSE2"]) == "S")
                        {
                            lbl_clave2.Visible = true;
                            rcb_clave2.Visible = true;
                            lbl_clave2.Text = Convert.ToString(reader["TADSCLA2"]);
                        }
                        if (Convert.ToString(reader["TACTLSE3"]) == "S")
                        {
                            lbl_clave3.Visible = true;
                            rcb_clave3.Visible = true;
                            lbl_clave3.Text = Convert.ToString(reader["TADSCLA3"]);
                        }
                        if (Convert.ToString(reader["TACTLSE4"]) == "S")
                        {
                            lbl_clave4.Visible = true;
                            rcb_clave4.Visible = true;
                            lbl_clave4.Text = Convert.ToString(reader["TADSCLA4"]);
                        }
                    }
                }
            }
        }
        public void rcb_clave1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcb_clave2.SelectedValue = ".";
            rcb_clave2.Items.Clear();
            rcb_clave2.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
            rcb_clave2.SelectedIndex = 0;
            rcb_clave3.Items.Clear();
            rcb_clave3.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
            rcb_clave3.SelectedIndex = 0;
            obj_clave2.SelectParameters["TP"].DefaultValue = rcb_linea.SelectedValue;
            obj_clave2.SelectParameters["C1"].DefaultValue = rcb_clave1.SelectedValue;
            rcb_clave2.DataBind();
        }
        public void rcb_clave2_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcb_clave3.Items.Clear();
            rcb_clave3.Items.Insert(0, new RadComboBoxItem { Text = "-Todos-", Value = "." });
            rcb_clave3.SelectedIndex = 0;
            rcb_clave4.SelectedValue = ".";
            obj_clave3.SelectParameters["TP"].DefaultValue = rcb_linea.SelectedValue;
            obj_clave3.SelectParameters["C1"].DefaultValue = rcb_clave1.SelectedValue;
            obj_clave3.SelectParameters["C2"].DefaultValue = rcb_clave2.SelectedValue;
            rcb_clave3.DataBind();
        }
        protected void edt_codigo_TextChanged(object sender, EventArgs e)
        {
            string cadena, sql;

            if (!string.IsNullOrWhiteSpace(edt_codigo.Text))
            {
                cadena = edt_codigo.Text.Trim();
                cadena = cadena.Substring(0, 12);
                sql = " SUBSTRING(UPPER(BCODIGO),1,12) = '" + cadena.ToUpper() + "'";
                if (cadena.Substring(0, 2) == "70")
                {
                    cadena = cadena.Substring(7, 5);
                    sql = " SUBSTRING(UPPER(BCODIGO),1,12) = '" + cadena.ToUpper() + "'";
                }

                using (IDataReader reader = ConDescuentosArticulosBL.GetArticulo(null, sql))
                {
                    while (reader.Read())
                    {
                        rcb_linea.SelectedValue = Convert.ToString(reader["ARTIPPRO"]);
                        rcb_linea_SelectedIndexChanged(null, null);
                        rcb_clave1.SelectedValue = Convert.ToString(reader["ARCLAVE1"]);
                        rcb_clave1_SelectedIndexChanged(null, null);
                        rcb_clave2.SelectedValue = Convert.ToString(reader["ARCLAVE2"]);
                        rcb_clave2_SelectedIndexChanged(null, null);
                        rcb_clave3.SelectedValue = Convert.ToString(reader["ARCLAVE3"]);
                    }
                }
                //btn_buscar_Click(null, null);
            }
        }
    }
}