using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Charting;
using Telerik.Web.UI;
using XUSS.BLL.Consultas;

namespace XUSS.WEB.Consultas
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_buscar_click(object sender, EventArgs e)
        {            
            try
            {
                //Ventas Año y Año Pasado
                this.ConfigureChartVentas(Convert.ToInt32(rc_ano.SelectedValue));
                this.ConfigureChartVtaxTipo(Convert.ToInt32(rc_mes.SelectedValue), Convert.ToInt32(rc_ano.SelectedValue));
                this.ConfigureChartVtaxVendedor(Convert.ToInt32(rc_mes.SelectedValue), Convert.ToInt32(rc_ano.SelectedValue));
                //this.ConfigureChartVtaxCliente(Convert.ToInt32(rc_mes.SelectedValue), Convert.ToInt32(rc_ano.SelectedValue));
                //Ventas x Mes Tipo
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }        
        private void ConfigureChartVentas(int inYear)
        {
            DataTable dt = new DataTable();
            ConsultasBL Obj = new ConsultasBL();
            try
            {
                (pnl_items.Items[0].FindControl("ch_ventas") as RadHtmlChart).PlotArea.Series.Clear();
                //ch_ventas.PlotArea.Series.Clear();
                dt = Obj.GetVentasHDAgrupadasxMes(null, inYear);
                for (int i = 0; i < 2; i++)
                {                   
                    AreaSeries currLineSeries = new AreaSeries();
                    if (i == 0)
                    {
                        currLineSeries.Appearance.FillStyle.BackgroundColor = Color.Blue;
                        currLineSeries.MarkersAppearance.BorderColor = Color.Blue;
                        currLineSeries.DataFieldY = "X";
                    }
                    else
                    {
                        currLineSeries.Appearance.FillStyle.BackgroundColor = Color.Red;
                        currLineSeries.MarkersAppearance.BorderColor = Color.Red;
                        currLineSeries.DataFieldY = "Z";
                    }
                    
                    currLineSeries.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.LineAndScatterLabelsPosition.Above;
                    currLineSeries.LabelsAppearance.DataFormatString = "{0:C}";
                    currLineSeries.MarkersAppearance.MarkersType = Telerik.Web.UI.HtmlChart.MarkersType.Circle;
                    currLineSeries.MarkersAppearance.BackgroundColor = Color.White;
                    currLineSeries.MarkersAppearance.Size = 6;
                    currLineSeries.MarkersAppearance.BorderWidth = 2;                    
                    currLineSeries.TooltipsAppearance.Color = Color.White;
                    
                    currLineSeries.Name = Convert.ToString(inYear-i);

                    //ch_ventas.PlotArea.Series.Add(currLineSeries);
                    (pnl_items.Items[0].FindControl("ch_ventas") as RadHtmlChart).PlotArea.Series.Add(currLineSeries);
                }

                (pnl_items.Items[0].FindControl("ch_ventas") as RadHtmlChart).PlotArea.XAxis.DataLabelsField = "MES";
                (pnl_items.Items[0].FindControl("ch_ventas") as RadHtmlChart).DataSource = dt;
                (pnl_items.Items[0].FindControl("ch_ventas") as RadHtmlChart).DataBind();

                //ch_ventas.PlotArea.XAxis.DataLabelsField = "MES";
                //ch_ventas.DataSource = dt;
                //ch_ventas.DataBind();

                //rg_items.Columns[1].HeaderText = Convert.ToString(inYear);
                //rg_items.Columns[2].HeaderText = Convert.ToString(inYear-1);
                //rg_items.DataSource = dt;
                //rg_items.DataBind();

                (pnl_items.Items[0].FindControl("rg_items") as RadGrid).Columns[1].HeaderText = Convert.ToString(inYear);
                (pnl_items.Items[0].FindControl("rg_items") as RadGrid).Columns[2].HeaderText = Convert.ToString(inYear-1);
                (pnl_items.Items[0].FindControl("rg_items") as RadGrid).DataSource = dt;
                (pnl_items.Items[0].FindControl("rg_items") as RadGrid).DataBind();
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
        private void ConfigureChartVtaxTipo(int inMonth, int inYear)
        {
            DataTable dt = new DataTable();
            ConsultasBL Obj = new ConsultasBL();
            try {
                //ch_tipomes.PlotArea.Series.Clear();
                (pnl_items.Items[1].FindControl("ch_tipomes") as RadHtmlChart).PlotArea.Series.Clear();

                dt = Obj.GetVentasTipoxMes(null, inMonth, inYear);
                for (int i = 0; i < 3; i++)
                {
                    BarSeries currLineSeries = new BarSeries();
                   switch (i)
                   {
                       case 0:
                           currLineSeries.Appearance.FillStyle.BackgroundColor = Color.Blue;
                            //currLineSeries.MarkersAppearance.BorderColor = Color.Blue;
                            currLineSeries.DataFieldY = "XC";
                            currLineSeries.Name = Convert.ToString(inYear) + "-" + Convert.ToString(inMonth);
                           break;
                       case 1:
                           currLineSeries.Appearance.FillStyle.BackgroundColor = Color.Red;
                            //currLineSeries.MarkersAppearance.BorderColor = Color.Red;
                            currLineSeries.DataFieldY = "ZC";
                            currLineSeries.Name = Convert.ToString(inYear) + "-" + Convert.ToString(inMonth-1);
                           break;
                       case 2:
                           currLineSeries.Appearance.FillStyle.BackgroundColor = Color.Yellow;
                           //currLineSeries.MarkersAppearance.BorderColor = Color.Red;
                           currLineSeries.DataFieldY = "YC";
                           currLineSeries.Name = Convert.ToString(inYear-1) + "-" + Convert.ToString(inMonth);
                           break;
                   }
                   currLineSeries.TooltipsAppearance.Color = Color.White;
                   (pnl_items.Items[1].FindControl("ch_tipomes") as RadHtmlChart).PlotArea.Series.Add(currLineSeries);    
                }

                (pnl_items.Items[1].FindControl("ch_tipomes") as RadHtmlChart).PlotArea.XAxis.DataLabelsField = "TANOMBRE";
                (pnl_items.Items[1].FindControl("ch_tipomes") as RadHtmlChart).DataSource = dt;
                (pnl_items.Items[1].FindControl("ch_tipomes") as RadHtmlChart).DataBind();

                //rg_detail_lxm.Columns[1].HeaderText = Convert.ToString(inYear);
                //rg_detail_lxm.Columns[2].HeaderText = Convert.ToString(inYear - 1);
                (pnl_items.Items[1].FindControl("rg_detail_lxm") as RadGrid).MasterTableView.ColumnGroups[0].HeaderText = Convert.ToString(inYear) + "-" + Convert.ToString(inMonth);
                (pnl_items.Items[1].FindControl("rg_detail_lxm") as RadGrid).MasterTableView.ColumnGroups[1].HeaderText = Convert.ToString(inYear) + "-" + Convert.ToString(inMonth - 1);
                (pnl_items.Items[1].FindControl("rg_detail_lxm") as RadGrid).MasterTableView.ColumnGroups[2].HeaderText = Convert.ToString(inYear - 1) + "-" + Convert.ToString(inMonth);
                (pnl_items.Items[1].FindControl("rg_detail_lxm") as RadGrid).DataSource = dt;
                (pnl_items.Items[1].FindControl("rg_detail_lxm") as RadGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            { 
            
            }
        }
        private void ConfigureChartVtaxVendedor(int inMonth, int inYear)
        { 
            DataTable dt = new DataTable();
            ConsultasBL Obj = new ConsultasBL();
            try
            {
                dt = Obj.GetVentasVendedorxMes(null, inMonth, inYear);
                (pnl_items.Items[3].FindControl("rg_detail_ven") as RadPivotGrid).DataSource = dt;
                (pnl_items.Items[3].FindControl("rg_detail_ven") as RadPivotGrid).DataBind();
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
        private void ConfigureChartVtaxCliente(int inMonth, int inYear)
        {
            DataTable dt = new DataTable();            
            try
            {
                dt = ConsultasBL.GetVentasDetalle(null, " AND MONTH(HDFECFAC)=" + Convert.ToString(inMonth) + " AND YEAR(HDFECFAC)=" + Convert.ToString(inYear)+" AND HDESTADO NOT IN ('AN')");
                (pnl_items.Items[4].FindControl("rg_detail_cli") as RadPivotGrid).DataSource = dt;
                (pnl_items.Items[4].FindControl("rg_detail_cli") as RadPivotGrid).DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
    }
}