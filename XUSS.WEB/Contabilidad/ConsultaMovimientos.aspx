<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaMovimientos.aspx.cs" Inherits="XUSS.WEB.Contabilidad.ConsultaMovimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6 {
            padding: 0px 10px 10px 30px;
        }
        /*.RadWindow_Bootstrap .rwTitleBar {
                border-color: #25A0DA;
                color: #333;
                background-color: #25A0DA;
                margin: 0;
                border-radius: 4px 4px 0 0;
            }*/
    </style>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" >
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Consulta Movimientos</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <div>
                <table>
                    <tr>
                        <td>
                            <label>T. Documento</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE" AppendDataBoundItems="true"
                                DataValueField="TFTIPFAC" RenderMode="Lightweight">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Año</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_ano" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_anos" DataTextField="MA_ANO"
                                DataValueField="MA_ANO" AppendDataBoundItems="true" RenderMode="Lightweight">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btn_consultar" runat="server" Text="Consultar" OnClick="btn_consultar_Click" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1" RenderMode="Lightweight"
            SelectedIndex="0" CssClass="tabStrip">
            <Tabs>
                <telerik:RadTab Text="Enero">
                </telerik:RadTab>
                <telerik:RadTab Text="Febrero">
                </telerik:RadTab>
                <telerik:RadTab Text="Marzo">
                </telerik:RadTab>
                <telerik:RadTab Text="Abril">
                </telerik:RadTab>
                <telerik:RadTab Text="Mayo">
                </telerik:RadTab>
                <telerik:RadTab Text="Junio">
                </telerik:RadTab>
                <telerik:RadTab Text="Julio">
                </telerik:RadTab>
                <telerik:RadTab Text="Agostos">
                </telerik:RadTab>
                <telerik:RadTab Text="Septiembre">
                </telerik:RadTab>
                <telerik:RadTab Text="Octubre">
                </telerik:RadTab>
                <telerik:RadTab Text="Noviembre">
                </telerik:RadTab>
                <telerik:RadTab Text="Diciembre">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="pv_enero" runat="server">                
                <telerik:RadGrid ID="rgEnero" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_enero" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_febrero" runat="server">
                <telerik:RadGrid ID="rg_febrero" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_febrero" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_marzo" runat="server">
                <telerik:RadGrid ID="rg_marzo" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_marzo" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_abril" runat="server">
                <telerik:RadGrid ID="rg_abril" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_abril" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                                                        
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_mayo" runat="server">
                <telerik:RadGrid ID="rg_mayo" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_mayo" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                                                        
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_junio" runat="server">
                <telerik:RadGrid ID="rg_junio" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_junio" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_julio" runat="server">
                <telerik:RadGrid ID="rg_julio" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_julio" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_agosto" runat="server">
                <telerik:RadGrid ID="rg_agosto" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_agosto" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_septiembre" runat="server">
                <telerik:RadGrid ID="rg_septiembre" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_septiembre" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_octubre" runat="server">
                <telerik:RadGrid ID="rg_octubre" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_octubre" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_noviembre" runat="server">
                <telerik:RadGrid ID="rg_noviembre" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_noviembre" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_CODIGO" HeaderText="Nro Int"
                                AllowFiltering="false" UniqueName="MVTH_CODIGO" HeaderButtonType="TextButton" DataField="MVTH_CODIGO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
            <telerik:RadPageView ID="pv_diciembre" runat="server">
                <telerik:RadGrid ID="rg_diciembre" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_diciembre" ShowGroupPanel="True" OnItemCommand="rgDetalle_ItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_PreRender">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>                            
                            <telerik:GridTemplateColumn DataField="CRZ" HeaderText="CRZ" UniqueName="CRZ_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CRZ" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_crz" runat="server" Text='<%# Eval("CRZ") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="MVTH_CODIGO" HeaderText="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LBL"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="MVTH_CODIGO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("MVTH_CODIGO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link" Text="MVTH_CODIGO" UniqueName="MVTH_CODIGO_LNK" DataTextField="MVTH_CODIGO"
                                HeaderText="Nro Int" HeaderStyle-Width="100px">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="TFNOMBRE" HeaderText="T Documento" DataType="System.String"
                                AllowFiltering="false" UniqueName="TFNOMBRE" HeaderButtonType="TextButton" DataField="TFNOMBRE"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_DOCCON" HeaderText="Nro Movimento" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_DOCCON" HeaderButtonType="TextButton" DataField="MVTH_DOCCON"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="120px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_FECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                HeaderText="F. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTH_FECMOV"
                                UniqueName="MVTH_FECMOV">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_DEBITO" UniqueName="MVTH_DEBITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTH_CREDITO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="MVTH_CREDITO" UniqueName="MVTH_CREDITO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Resizable="true" SortExpression="MVTH_ESTADO" HeaderText="Estado" DataType="System.String"
                                AllowFiltering="false" UniqueName="MVTH_ESTADO" HeaderButtonType="TextButton" DataField="MVTH_ESTADO"
                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
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
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_enero" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_febrero" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_marzo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_abril" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_mayo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_junio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_julio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_agosto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_septiembre" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_octubre" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_noviembre" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_diciembre" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosMes" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inFilter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (10)" Name="filter" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_anos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAnos" TypeName="XUSS.BLL.Parametros.MesesBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=1" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
