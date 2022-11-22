using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XUSS.BLL.Consultas;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;

namespace XUSS.WEB.Consultas
{
    public partial class SqlExplorer : System.Web.UI.Page
    {
        bool isConfigured = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ibt_ejecutar_OnClick(object sender, ImageClickEventArgs e)
        {
            rg_consulta.Columns.Clear();

            if ((txt_editor.Text.Trim()).Substring(0, 6).ToUpper() == "SELECT")
            {
                rg_consulta.AutoGenerateColumns = false;
                DataTable tb_resultado = new DataTable();
                SqlExplorerBL obj = new SqlExplorerBL();
                try
                {
                    tb_resultado = obj.ExecuteSQL(null, txt_editor.Text);
                    rg_consulta.DataSource = tb_resultado;
                    for (int i = 0; i < tb_resultado.Columns.Count; i++)
                    {
                        GridBoundColumn Columna;
                        Columna = new GridBoundColumn();
                        Columna.DataField = tb_resultado.Columns[i].ColumnName;
                        Columna.HeaderText = tb_resultado.Columns[i].ColumnName;
                        rg_consulta.Columns.Add(Columna);
                    }
                    rg_consulta.DataBind();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    litTextoMensaje.Text = ex.Message;
                    mpMensajes.Show();
                }
                finally
                {
                    obj = null;
                    tb_resultado = null;
                }
            }
            else
            {
                mp_confirmacion.Show();
            }

        }
        protected void btn_aceptar_OnClick(object sender, EventArgs e)
        {
            if ((txt_editor.Text.Trim()).Substring(0, 6).ToUpper() != "SELECT")
            {
                SqlExplorerBL obj = new SqlExplorerBL();
                try
                {
                    litTextoMensaje.Text = "Ejecutado Correctamente Resultado Operacion :"+ Convert.ToString(obj.ExecuteNonSQL(null,txt_editor.Text.Trim()));
                    mpMensajes.Show();
                }
                catch (Exception ex)
                {
                    //throw ex;
                    litTextoMensaje.Text = ex.Message;
                    mpMensajes.Show();
                }
                finally
                {
                    obj = null;                 
                }
            }
        }
        protected void btn_cerrar_OnClick(object sender, EventArgs e)
        {
            mp_confirmacion.Hide();
        }
        protected void RadGrid1_ExcelMLExportRowCreated(object source, GridExportExcelMLRowCreatedArgs e)
        {

            if (e.RowType == GridExportExcelMLRowType.DataRow)
            {
                //Add custom styles to the desired cells
                CellElement cell = e.Row.Cells.GetCellByName("TANOMBRE");
                //cell.StyleValue = cell.StyleValue == "itemStyle" ? "priceItemStyle" : "alternatingPriceItemStyle";

                cell = e.Row.Cells.GetCellByName("BBCLAVE1");
                //cell.StyleValue = cell.StyleValue == "itemStyle" ? "priceItemStyle" : "alternatingPriceItemStyle";

                cell = e.Row.Cells.GetCellByName("FEC_MIN");
                //cell.StyleValue = cell.StyleValue == "itemStyle" ? "percentItemStyle" : "alternatingPercentItemStyle";


                if (!isConfigured)
                {
                    //Set Worksheet name
                    e.Worksheet.Name = "Order Details Extended";

                    //Set Column widths
                    foreach (ColumnElement column in e.Worksheet.Table.Columns)
                    {
                        if (e.Worksheet.Table.Columns.IndexOf(column) == 2)
                            column.Width = Unit.Point(180); //set width 180 to ProductName column
                        else
                            column.Width = Unit.Point(80); //set width 80 to the rest of the columns
                    }

                    //Set Page options
                    PageSetupElement pageSetup = e.Worksheet.WorksheetOptions.PageSetup;
                    pageSetup.PageLayoutElement.IsCenteredVertical = true;
                    pageSetup.PageLayoutElement.IsCenteredHorizontal = true;
                    pageSetup.PageMarginsElement.Left = 0.5;
                    pageSetup.PageMarginsElement.Top = 0.5;
                    pageSetup.PageMarginsElement.Right = 0.5;
                    pageSetup.PageMarginsElement.Bottom = 0.5;
                    pageSetup.PageLayoutElement.PageOrientation = PageOrientationType.Landscape;

                    //Freeze panes
                    e.Worksheet.WorksheetOptions.AllowFreezePanes = true;
                    e.Worksheet.WorksheetOptions.LeftColumnRightPaneNumber = 1;
                    e.Worksheet.WorksheetOptions.TopRowBottomPaneNumber = 1;
                    e.Worksheet.WorksheetOptions.SplitHorizontalOffset = 1;
                    e.Worksheet.WorksheetOptions.SplitVerticalOffest = 1;

                    e.Worksheet.WorksheetOptions.ActivePane = 2;
                    isConfigured = true;
                }
            }
        }
        protected void RadGrid1_ExcelMLExportStylesCreated(object source, GridExportExcelMLStyleCreatedArgs e)
        {
            //Add currency and percent styles
            StyleElement priceStyle = new StyleElement("priceItemStyle");
            priceStyle.NumberFormat.FormatType = NumberFormatType.Currency;
            priceStyle.FontStyle.Color = System.Drawing.Color.Red;
            e.Styles.Add(priceStyle);

            StyleElement alternatingPriceStyle = new StyleElement("alternatingPriceItemStyle");
            alternatingPriceStyle.NumberFormat.FormatType = NumberFormatType.Currency;
            alternatingPriceStyle.FontStyle.Color = System.Drawing.Color.Red;
            e.Styles.Add(alternatingPriceStyle);

            StyleElement percentStyle = new StyleElement("percentItemStyle");
            percentStyle.NumberFormat.FormatType = NumberFormatType.Percent;
            percentStyle.FontStyle.Italic = true;
            e.Styles.Add(percentStyle);

            StyleElement alternatingPercentStyle = new StyleElement("alternatingPercentItemStyle");
            alternatingPercentStyle.NumberFormat.FormatType = NumberFormatType.Percent;
            alternatingPercentStyle.FontStyle.Italic = true;
            e.Styles.Add(alternatingPercentStyle);

            //Apply background colors
            foreach (StyleElement style in e.Styles)
            {
                if (style.Id == "headerStyle")
                {
                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                    style.InteriorStyle.Color = System.Drawing.Color.Gray;
                }
                if (style.Id == "alternatingItemStyle" || style.Id == "alternatingPriceItemStyle" || style.Id == "alternatingPercentItemStyle" || style.Id == "alternatingDateItemStyle")
                {
                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                }
                if (style.Id.Contains("itemStyle") || style.Id == "priceItemStyle" || style.Id == "percentItemStyle" || style.Id == "dateItemStyle")
                {
                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;
                    style.InteriorStyle.Color = System.Drawing.Color.White;
                }
            }
        }
    }
}