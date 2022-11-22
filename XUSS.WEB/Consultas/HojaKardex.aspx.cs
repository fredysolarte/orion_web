using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using XUSS.BLL.Comun;
using XUSS.BLL.Consultas;

namespace XUSS.WEB.Consultas
{
    public partial class HojaKardex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rc_dia.SelectedValue = Convert.ToString(System.DateTime.Today.Day);
                rc_mes.SelectedValue = Convert.ToString(System.DateTime.Today.Month);
                rc_ano.SelectedValue = Convert.ToString(System.DateTime.Today.Year);

                rc_diaf.SelectedValue = Convert.ToString(System.DateTime.Today.Day);
                rc_mesf.SelectedValue = Convert.ToString(System.DateTime.Today.Month);
                rc_anof.SelectedValue = Convert.ToString(System.DateTime.Today.Year);
            }
        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ConsultasBL Obj = new ConsultasBL();
            DateTime ld_fechaini = Convert.ToDateTime("01/01/2000");
            DateTime ld_fechafin = Convert.ToDateTime("01/01/2000");
            string filtro="", lc_in="";
            try
            {
                switch (Convert.ToString(Session["fto_date"]))
                {
                    case "mdy":
                        ld_fechaini = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_mes.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(Convert.ToInt32(rc_dia.SelectedValue))) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                        ld_fechafin = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_mesf.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_diaf.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_anof.SelectedValue)));
                        break;
                    case "dmy":
                        ld_fechaini = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(Convert.ToInt32(rc_dia.SelectedValue))) + "/" + Convert.ToString(Convert.ToInt32(Convert.ToInt32(rc_mes.SelectedValue))) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                        ld_fechafin = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_diaf.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_mesf.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_anof.SelectedValue)));
                        break;
                }

                var collection = rc_linea.CheckedItems;
                if (collection.Count != 0)
                {
                    filtro += " AND MBTIPPRO IN (";
                    foreach (var item in collection)
                    {
                        lc_in += "'" + Convert.ToString(item.Value) + "',";
                    }

                    filtro += lc_in.Substring(0, lc_in.Length - 1) + ")";
                }

                dt = Obj.GetHojaKardex(null, filtro,rc_bodega.SelectedValue, ld_fechaini,ld_fechafin);
                rg_hoja.DataSource = dt;
                rg_hoja.DataBind();
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
        protected void rg_hoja_PivotGridCellExporting(object sender, PivotGridCellExportingArgs e)
        {
            PivotGridBaseModelCell modelDataCell = e.PivotGridModelCell as PivotGridBaseModelCell;
            if (modelDataCell != null)
            {
                AddStylesToDataCells(modelDataCell, e);
            }

            if (modelDataCell.TableCellType == PivotGridTableCellType.RowHeaderCell)
            {
                AddStylesToRowHeaderCells(modelDataCell, e);
            }

            if (modelDataCell.TableCellType == PivotGridTableCellType.ColumnHeaderCell)
            {
                AddStylesToColumnHeaderCells(modelDataCell, e);
            }

            if (modelDataCell.IsGrandTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                e.ExportedCell.Style.Font.Bold = true;
            }

            if (IsTotalDataCell(modelDataCell))
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
                AddBorders(e);
            }

            if (IsGrandTotalDataCell(modelDataCell))
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(128, 128, 128);
                e.ExportedCell.Style.Font.Bold = true;
                AddBorders(e);
            }
        }
        private void AddStylesToDataCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (modelDataCell.Data != null && modelDataCell.Data.GetType() == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(modelDataCell.Data);
                if (value > 100000)
                {
                    e.ExportedCell.Style.BackColor = Color.FromArgb(51, 204, 204);
                    AddBorders(e);
                }

                e.ExportedCell.Format = "$0.0";
            }
        }
        private static void AddBorders(PivotGridCellExportingArgs e)
        {
            e.ExportedCell.Style.BorderBottomColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderBottomWidth = new Unit(1);
            e.ExportedCell.Style.BorderBottomStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderRightColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderRightWidth = new Unit(1);
            e.ExportedCell.Style.BorderRightStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderLeftColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderLeftWidth = new Unit(1);
            e.ExportedCell.Style.BorderLeftStyle = BorderStyle.Solid;

            e.ExportedCell.Style.BorderTopColor = Color.FromArgb(128, 128, 128);
            e.ExportedCell.Style.BorderTopWidth = new Unit(1);
            e.ExportedCell.Style.BorderTopStyle = BorderStyle.Solid;
        }
        protected void rg_hoja_PivotGridBiffExporting(object sender, PivotGridBiffExportingEventArgs e)
        {
            
                //eis.Table newWorksheet = new eis.Table("My New Worksheet");

                //eis.Cell headerCell = newWorksheet.Cells[1, 1];
                //headerCell.Value = "Legend";
                //headerCell.Style.BorderBottomColor = System.Drawing.Color.Black;
                //headerCell.Style.BorderBottomStyle = BorderStyle.Double;
                //headerCell.Style.Font.Bold = true;
                //headerCell.Colspan = 2;
                //newWorksheet.Columns[1].Width = 32D;
                //newWorksheet.Cells[1, 2].Value = "Cells";
                //newWorksheet.Cells[1, 2].Style.Font.Bold = true;
                //newWorksheet.Cells[2, 2].Value = "Color";
                //newWorksheet.Cells[2, 2].Style.Font.Bold = true;

                //newWorksheet.Cells[1, 3].Value = "Cells with values bigger than 100 000";
                //newWorksheet.Cells[1, 4].Value = "Totals� cells";
                //newWorksheet.Cells[1, 5].Value = "Grand totals� cells";
                //newWorksheet.Cells[1, 6].Value = "Row and column header tables� cells";

                //newWorksheet.Cells[2, 3].Value = "#33CCCC";
                //newWorksheet.Cells[2, 3].Style.BackColor = Color.FromArgb(51, 204, 204);
                //newWorksheet.Cells[2, 4].Value = "#969696";
                //newWorksheet.Cells[2, 4].Style.BackColor = Color.FromArgb(150, 150, 150);
                //newWorksheet.Cells[2, 5].Value = "#808080";
                //newWorksheet.Cells[2, 5].Style.BackColor = Color.FromArgb(128, 128, 128);
                //newWorksheet.Cells[2, 6].Value = "#C0C0C0";
                //newWorksheet.Cells[2, 6].Style.BackColor = Color.FromArgb(192, 192, 192);

                //e.ExportStructure.Tables.Add(newWorksheet);
            
        }
        protected void ButtonExcel_Click(object sender, EventArgs e)
        {
            //string alternateText = (sender as ImageButton).AlternateText;
            string alternateText = "Xlsx";
            rg_hoja.ExportSettings.Excel.Format = (PivotGridExcelFormat)Enum.Parse(typeof(PivotGridExcelFormat), alternateText);
            rg_hoja.ExportSettings.IgnorePaging = false;
            rg_hoja.ExportToExcel();
        }
        private bool IsTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
               (modelDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowTotalDataCell ||
                 modelDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal);
        }
        private bool IsGrandTotalDataCell(PivotGridBaseModelCell modelDataCell)
        {
            return modelDataCell.TableCellType == PivotGridTableCellType.DataCell &&
                (modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal ||
                    modelDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell ||
                    modelDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal);
        }
        private void AddStylesToColumnHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 200D;
            }

            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }
            AddBorders(e);
        }
        private void AddStylesToRowHeaderCells(PivotGridBaseModelCell modelDataCell, PivotGridCellExportingArgs e)
        {
            if (e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width == 0)
            {
                e.ExportedCell.Table.Columns[e.ExportedCell.ColIndex].Width = 80D;
            }
            if (modelDataCell.IsTotalCell)
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(150, 150, 150);
                e.ExportedCell.Style.Font.Bold = true;
            }
            else
            {
                e.ExportedCell.Style.BackColor = Color.FromArgb(192, 192, 192);
            }

            AddBorders(e);
        }
        protected void rg_hoja_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            ConsultasBL Obj = new ConsultasBL();
            try
            {
                dt = Obj.GetHojaKardex(null, null,rc_bodega.SelectedValue, System.DateTime.Today,System.DateTime.Today);
                rg_hoja.DataSource = dt;
                //rg_hoja.DataBind();
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

        protected void btn_imprimir_Click(object sender, EventArgs e)
        {
            DateTime ld_fechaini = Convert.ToDateTime("01/01/2000");
            DateTime ld_fechafin = Convert.ToDateTime("01/01/2000");

            switch (Convert.ToString(Session["fto_date"]))
            {
                case "mdy":
                    ld_fechaini = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_mes.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(1)) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                    ld_fechafin = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_mes.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_dia.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                    break;
                case "dmy":
                    ld_fechaini = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(1)) + "/" + Convert.ToString(Convert.ToInt32(1)) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                    ld_fechafin = Convert.ToDateTime(Convert.ToString(Convert.ToInt32(rc_dia.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_mes.SelectedValue)) + "/" + Convert.ToString(Convert.ToInt32(rc_ano.SelectedValue)));
                    break;
            }

            string url = "";
            url = ComunBL.GetHttpHttps(HttpContext.Current.Request.Url.AbsoluteUri) + "//" + HttpContext.Current.Request.Url.Authority + "/Reportes/VisorPantallaCompleta.aspx?rpt=8014&inban=S&inParametro=bodega&inValor=" + Convert.ToString(rc_bodega.SelectedValue) + "&inParametro=fini&inValor=" + ld_fechaini.ToShortDateString() + "&inParametro=ffin&inValor=" + ld_fechafin.ToShortDateString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "New", "window.open('" + url + "');", true);
        }

        protected void rc_mes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBoxItem item = new RadComboBoxItem();
            int i=1,ln_dias = System.DateTime.DaysInMonth(Convert.ToInt32(rc_ano.SelectedValue), Convert.ToInt32((sender as RadComboBox).SelectedValue));
            try {
                rc_dia.Items.Clear();
                item.Value = "";
                item.Text = "Seleccionar";
                rc_dia.Items.Add(item);
                for (i = 1; i <= ln_dias; i++)
                {
                    RadComboBoxItem item_ = new RadComboBoxItem();
                    item_.Value = Convert.ToString(i);
                    item_.Text = Convert.ToString(i);
                    rc_dia.Items.Add(item_);
                    item_ = null;
                }

                rc_dia.SelectedValue = Convert.ToString(ln_dias);
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

        protected void rg_hoja_ItemCommand(object sender, PivotGridCommandEventArgs e)
        {
            PivotGridDataItem item = (PivotGridDataItem)e.Item;
            if (e.CommandName == "link")
            {
                string script = "function f(){$find(\"" + mpSeguimeinto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
    }
}