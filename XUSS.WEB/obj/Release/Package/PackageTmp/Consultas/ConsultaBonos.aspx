<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaBonos.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaBonos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Consulta Redencion Bonos</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <div>
                <table>
                    <tr>
                        <td><label>T.  Documento</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px"
                                Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE"
                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td><label>Nro Documento</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td rowspan="3">
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_buscar_click"
                                ValidationGroup="UpdateBoton" CausesValidation="true">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td><label>Identificacion</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_identificacion" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td><label>Nombre/Apellido</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombres" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><label>Nro Bono</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nrobono" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td><label>C Bono</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_barrasbono" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" 
                    RenderMode="Lightweight" Culture="(Default)" CellSpacing="0" AllowFilteringByColumn="true"  DataSourceID="obj_consulta"
                    ShowFooter="true" Height="650px" >
                    <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                        <Scrolling AllowScroll="True" SaveScrollPosition="true">
                        </Scrolling>
                    </ClientSettings>
                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>                            
                            <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                UniqueName="TRCODNIT">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TERCERO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Tercero" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TERCERO"
                                UniqueName="TERCERO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="T. Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE"
                                UniqueName="TFNOMBRE">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>                            
                            <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC"
                                UniqueName="HDFECFAC">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DTNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Nro Doc." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROFAC"
                                UniqueName="DTNROFAC">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="T_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Valor" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="T_VALOR" UniqueName="T_VALOR">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="T_SALDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="T_SALDO" UniqueName="T_SALDO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
            </telerik:RadGrid>
        </fieldset>
    </telerik:RadAjaxPanel>

    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="TFCLAFAC IN (4)" Name="filter" Type="String" />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsultaBonos" TypeName="XUSS.BLL.Consultas.ConsultaBonosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
