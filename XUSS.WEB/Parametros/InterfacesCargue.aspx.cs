using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Parametros;

namespace XUSS.WEB.Parametros
{
    public partial class InterfacesCargue : System.Web.UI.Page
    {
        private DataTable tbCampos
        {
            set { ViewState["tbCampos"] = value; }
            get { return ViewState["tbCampos"] as DataTable; }
        }
        private DataTable tbItems_
        {
            set { ViewState["tbItems_"] = value; }
            get { return ViewState["tbItems_"] as DataTable; }
        }
        private DataTable tbItemsTotal
        {
            set { ViewState["tbItemsTotal"] = value; }
            get { return ViewState["tbItemsTotal"] as DataTable; }
        }
        private DataTable tbLstCampos
        {
            set { ViewState["tbLstCampos"] = value; }
            get { return ViewState["tbLstCampos"] as DataTable; }
        }
        private DataTable tbErorres
        {
            set { ViewState["tbErorres"] = value; }
            get { return ViewState["tbErorres"] as DataTable; }
        }
        private string prArchivo
        {
            set { ViewState["prArchivo"] = value; }
            get { return ViewState["prArchivo"] as string; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rauCargar_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            prArchivo = ((System.IO.FileStream)(e.File.InputStream)).Name;
            this.procesa_plano(e.File.InputStream);
        }
        protected void rc_tablas_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CarguePlanosBL Obj = new CarguePlanosBL();
            try {
                tbCampos = Obj.GetCampos(null, Convert.ToInt32((sender as RadComboBox).SelectedValue));                
                rg_campos.DataSource = tbCampos;
                rg_campos.DataBind();
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
        public Boolean GetEstado(object a)
        {
            if (a is DBNull || a == null || Convert.ToInt32(a) == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void procesa_plano(Stream inArchivo)
        {
            int ln_con = 0, ln_col=0;
            Boolean lb_indInicio = true;
            DataTable tbItems = new DataTable();
            DataTable tbItemsTotal = new DataTable();
            DataTable tbLstCampos = new DataTable();
            try
            {
                using (Stream stream = inArchivo)
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            string lc_cadena = streamReader.ReadLine();
                            string[] words = lc_cadena.Split(';');
                            //CRea Columnas
                            if (lb_indInicio)
                            {
                                foreach (string word in words)
                                {
                                    tbItems.Columns.Add("Columna" + Convert.ToString(ln_con), typeof(string));
                                    tbItemsTotal.Columns.Add("Columna" + Convert.ToString(ln_con), typeof(string));
                                    ln_con++;
                                }
                                lb_indInicio = false;
                                ln_con = 0;
                            }

                            DataRow item = tbItems.NewRow();
                            // Fin Crea Columnas
                            ln_col = 0;
                            foreach (string word in words)
                            {                                
                                item["Columna"+Convert.ToString(ln_col)]= word;                                                                
                                ln_col++;
                            }
                            tbItems.Rows.Add(item);
                            //tbItemsTotal.Rows.Add(item);
                            item = null;
                            ln_con++;
                        }
                    }
                }

                rg_cargue.Columns.Clear();
                tbLstCampos.Columns.Clear();
                tbLstCampos.Columns.Add("id", typeof(string));
                tbLstCampos.Columns.Add("name", typeof(string));
                tbLstCampos.Rows.Clear();

                lb_indInicio = true;
                int i = 0;
                for (i = 0; i < ln_col; i++)
                {
                    DataRow itm = tbLstCampos.NewRow();
                    GridBoundColumn boundColumn;
                    boundColumn = new GridBoundColumn();
                    boundColumn.DataField = "Columna" + Convert.ToString(i);
                    boundColumn.HeaderText = "Columna" + Convert.ToString(i);
                    rg_cargue.MasterTableView.Columns.Add(boundColumn);
                    //----
                    if (lb_indInicio)
                    {
                        DataRow itmx = tbLstCampos.NewRow();
                        itm["id"] = "";
                        itm["name"] = "Seleccionar";
                        tbLstCampos.Rows.Add(itmx);
                        itmx = null;
                        lb_indInicio = false;
                    }
                    //-----
                    itm["id"] = "Columna" + Convert.ToString(i);
                    itm["name"] = "Columna" + Convert.ToString(i);

                    tbLstCampos.Rows.Add(itm);
                    itm = null;
                    boundColumn = null;
                }
                

                foreach (GridDataItem item in rg_campos.Items)
                {
                    (item.FindControl("rc_campos") as RadComboBox).Items.Clear();
                    (item.FindControl("rc_campos") as RadComboBox).DataTextField = "name";
                    (item.FindControl("rc_campos") as RadComboBox).DataValueField = "id";
                    (item.FindControl("rc_campos") as RadComboBox).DataSource = tbLstCampos;
                    (item.FindControl("rc_campos") as RadComboBox).DataBind();
                }
                tbItems_ = tbItems;
                rg_cargue.DataSource = tbItems_;
                rg_cargue.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tbItems = null;
                tbItemsTotal = null;
            }
        }
        protected void btn_procesar_Aceptar(object sender, EventArgs e)
        {
            this.procesa_plano(File.OpenRead(prArchivo));
        }
        protected void btn_siguiente_Click(object sender, EventArgs e)
        {
            CarguePlanosBL Obj = new CarguePlanosBL();
            try {
                foreach (GridDataItem item in rg_campos.Items)
                {
                    string lc_campo = Convert.ToString(item["nom_campo"].Text);
                    string lc_columna = Convert.ToString((item.FindControl("rc_campos") as RadComboBox).SelectedValue);
                    // Desbloquear Columnas
                    foreach (DataColumn cl in tbCampos.Columns)                    
                        cl.ReadOnly = false;
                    
                    foreach (DataRow row in tbCampos.Rows)
                    {
                        if (lc_campo == Convert.ToString(row["nom_campo"]))
                            if (!string.IsNullOrEmpty(lc_columna))
                                row["campo"] = lc_columna;
                    }
                }
                Obj.GenerarCargue(null, Convert.ToString(rc_tablas.Text), tbCampos, tbItems_);
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
        protected void btn_validar_Click(object sender, EventArgs e)
        {
            ComunBL Obj = new ComunBL();
            DataTable dt = new DataTable();
            int ln_nrorow = 0;
            try
            {
                dt.Columns.Add("fila", typeof(Int32));
                dt.Columns.Add("Columna", typeof(string));
                dt.Columns.Add("Campo", typeof(string));
                dt.Columns.Add("Error", typeof(string));

                tbErorres = dt;
                foreach (GridDataItem item in rg_campos.Items)
                {
                    string lc_campo = Convert.ToString(item["nom_campo"].Text);
                    string lc_columna = Convert.ToString((item.FindControl("rc_campos") as RadComboBox).SelectedValue);
                    // Desbloquear Columnas
                    foreach (DataColumn cl in tbCampos.Columns)
                        cl.ReadOnly = false;

                    foreach (DataRow row in tbCampos.Rows)
                    {
                        if (lc_campo == Convert.ToString(row["nom_campo"]))
                            if (!string.IsNullOrEmpty(lc_columna))
                                row["campo"] = lc_columna;
                    }
                }
                // Validacion
                foreach (DataRow rw in tbItems_.Rows)
                {
                    foreach (DataColumn cl in tbItems_.Columns)
                    {
                        string lc_tipo = "";
                        int max_length = 0;
                        int lc_null = 0;
                        foreach (DataRow rc in tbCampos.Rows)
                        {
                            if (cl.ColumnName == Convert.ToString(rc["campo"]))
                            {
                                lc_tipo = Convert.ToString(rc["nom_tipo"]);
                                max_length = Convert.ToInt32(rc["max_length"]);
                                lc_null = Convert.ToInt32(rc["is_nullable"]);
                            }
                        }

                        //Valida Tipo de Datos
                        switch (lc_tipo)
                        {
                            case "text": if (Convert.ToString(rw[cl.ColumnName]).Length > max_length) this.InsertError(false, ln_nrorow, cl.ColumnName, "", "Supera Tamaño Maximo ("+Convert.ToString(max_length)+")"); break;
                            case "char": if (Convert.ToString(rw[cl.ColumnName]).Length > max_length) this.InsertError(false, ln_nrorow, cl.ColumnName, "", "Supera Tamaño Maximo (" + Convert.ToString(max_length) + ")"); break;
                            case "nchar": if (Convert.ToString(rw[cl.ColumnName]).Length > max_length) this.InsertError(false, ln_nrorow, cl.ColumnName, "", "Supera Tamaño Maximo (" + Convert.ToString(max_length) + ")"); break;
                            case "varchar": if (Convert.ToString(rw[cl.ColumnName]).Length > max_length) this.InsertError(false, ln_nrorow, cl.ColumnName, "", "Supera Tamaño Maximo (" + Convert.ToString(max_length) + ")"); break;
                            case "date": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])),ln_nrorow,cl.ColumnName,"","Error Tipo de Dato (DateTime)"); break;
                            case "time": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                            case "datetime2": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                            case "datetimeoffset": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                            case "smalldatetime": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                            case "datetime": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                            case "tinyint": this.InsertError(Obj.isInt32(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Int)"); break;
                            case "smallint": this.InsertError(Obj.isInt32(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Int)"); break;
                            case "int": this.InsertError(Obj.isInt32(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Int)"); break;
                            case "real": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "money": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "float": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "bit": this.InsertError(Obj.isInt32(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Int)"); break;
                            case "decimal": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "numeric": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "smallmoney": this.InsertError(Obj.isDouble(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Double)"); break;
                            case "bigint": this.InsertError(Obj.isInt32(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (Int)"); break;
                            case "timestamp": this.InsertError(Obj.isDateTime(Convert.ToString(rw[cl.ColumnName])), ln_nrorow, cl.ColumnName, "", "Error Tipo de Dato (DateTime)"); break;
                        }

                        if (Convert.ToString(rw[cl.ColumnName]).Trim().Length > 0)
                            this.InsertError(false, ln_nrorow, cl.ColumnName, "", "Dato Obligatoirio");
                    }
                    ln_nrorow++;
                }
                rg_Errores.DataSource = tbErorres;
                rg_Errores.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Obj = null;
                dt = null;
            }
        }
        private void InsertError(Boolean inBandera, int inNroFila, string inColumna, string inCampo, string inError)
        {
            DataRow rw = tbErorres.NewRow();
            try
            {
                if (!inBandera)
                {
                    rw["fila"] = inNroFila;
                    rw["Columna"] = inColumna;
                    rw["Campo"] = inCampo;
                    rw["Error"] = inError;

                    tbErorres.Rows.Add(rw);
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
    }

}