<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaPagos.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaPagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rg_consulta$ctl00$ctl02$ctl00$ExportToExcelButton")) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback"  ClientEvents-OnResponseEnd="endPostback"
        Width="100%">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Consulta Pagos</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
        <div>
            <table>
                <tr>
                    <td>
                        <label>Bodega</label></td>
                    <td>
                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                            Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" 
                            DataValueField="BDBODEGA" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>                                      
                </tr>                
                <tr>
                    <td>
                        <label>F Incial</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecinicial" runat="server" 
                                                        MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                                                    </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label>F Final</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecfinal" runat="server" 
                                                        MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                                                    </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_consultar" runat="server" Text="Consultar" OnClick="btn_consultar_OnClick" ValidationGroup="gvInsert"  />
                    </td>
                </tr>
            </table>
        </div>
        </fieldset>
        <telerik:RadGrid ID="rg_consulta" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
            Culture="(Default)" CellSpacing="0" ShowFooter="True" DataSourceID="obj_ventas" OnItemCommand="rg_consulta_ItemCommand" >
            <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" ></Scrolling>
                                    </ClientSettings>
            <MasterTableView ShowGroupFooter="true"  CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC"
                        UniqueName="HDFECFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                        UniqueName="BDNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="T Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE"
                        UniqueName="TFNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDNROFAC"
                        UniqueName="HDNROFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn DataField="NRO_FAC" HeaderText="Propietario" UniqueName="NRO_FAC_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="HDNROFAC" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("NRO_FAC") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link" Text="HDNROFAC" UniqueName="HDNROFAC" DataTextField="HDNROFAC"
                        HeaderText="Nro Documento" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Tipo Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                        UniqueName="TTDESCRI">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DETALLE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                        HeaderText="Detalle Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DETALLE"
                        UniqueName="DETALLE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PGVLRPAG" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr Pagos" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HDFECCIE" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Cierre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECCIE"
                        UniqueName="HDFECCIE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                                                                               
                </Columns>
                <NoRecordsTemplate>
                    <div class="alert alert-danger">
                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <strong>Alerta!</strong>No Existen Registros
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
     </telerik:RadAjaxPanel>
            <asp:ObjectDataSource ID="obj_ventas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsultaPagos" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
