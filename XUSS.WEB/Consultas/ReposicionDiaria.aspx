<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ReposicionDiaria.aspx.cs" Inherits="XUSS.WEB.Consultas.ReposicionDiaria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btn_cargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" LoadingPanelID="lpn_circulo" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" LoadingPanelID="lpn_circulo" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadAjaxLoadingPanel ID="lpn_circulo" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <asp:Panel ID="pnl_Prinipal" runat="server">
            <fieldset class="cssFieldSetContainer">
                <div class="box">
                    <div class="title">
                        <h5>
                            Reposicion Diaria</h5>
                    </div>
                </div>
            </fieldset>
            <table>
                <tr>
                    <td>
                        <label>F. Inicial</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fechaIni" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label>F. Final</label>
                    </td>
                    <td>
                    <telerik:RadDatePicker ID="edt_fechaFin" runat="server">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Bodega</label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="rc_bodegas" runat="server" DataTextField="BDNOMBRE" DataValueField="BDBODEGA"
                            DataSourceID="obj_bodegas" Width="480px" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Categoria</label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="rc_categoria" runat="server" DataTextField="TANOMBRE" DataValueField="TATIPPRO"
                            DataSourceID="obj_categoria" Width="480px" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadButton ID="btn_cargar" runat="server" Text="Refrescar" OnClick="btn_cargar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="950px" AutoGenerateColumns="False"
                Height="500px" Culture="(Default)" CellSpacing="0" DataSourceID="obj_consulta"
                AllowPaging="true" PageSize="30">
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                    </Scrolling>
                </ClientSettings>
                <PagerStyle PageSizeControlType="RadDropDownList" Mode="NextPrevAndNumeric"></PagerStyle>
                <ExportSettings IgnorePaging="true">
                    
                </ExportSettings>
                <MasterTableView CommandItemDisplay="Top" >
                
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"  />
                    <ColumnGroups>
                        <telerik:GridColumnGroup Name="gValores" HeaderText="" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Font-Size="9px">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gVentas" HeaderText="Ventas" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Font-Size="9px">
                        </telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gReposicion" HeaderText="Reposicion" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Font-Size="9px">
                        </telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" ColumnGroupName="gValores"
                            HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                            UniqueName="BDNOMBRE">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" ColumnGroupName="gValores"
                            HeaderText="Categoria" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                            UniqueName="TANOMBRE">
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" ColumnGroupName="gValores"
                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTCLAVE1"
                            UniqueName="DTCLAVE1">
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" ColumnGroupName="gValores"
                            HeaderText="Color" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                            UniqueName="ASNOMBRE">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="A" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="Unica" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="A" DataFormatString="{0:0}" UniqueName="A" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="B" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="2XS/4" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="B" DataFormatString="{0:0}" UniqueName="B" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="C" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="XS/6" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="C" DataFormatString="{0:0}" UniqueName="C" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="D" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="S/8" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="D" DataFormatString="{0:0}" UniqueName="D" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="E" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="M/10" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="E" DataFormatString="{0:0}" UniqueName="E" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="F" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="L/12" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="F" DataFormatString="{0:0}" UniqueName="F" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="G" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="XL/14" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="G" DataFormatString="{0:0}" UniqueName="G" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="H" HeaderButtonType="TextButton" ColumnGroupName="gVentas"
                            DefaultInsertValue="0" HeaderText="XXL/16" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="H" DataFormatString="{0:0}" UniqueName="H" Visible="false">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AA" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="Unica" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="AA" DataFormatString="{0:0}" UniqueName="AA">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BB" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="2XS/4" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="BB" DataFormatString="{0:0}" UniqueName="BB">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CC" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="XS/6" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="CC" DataFormatString="{0:0}" UniqueName="CC">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DD" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="S/8 R" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="DD" DataFormatString="{0:0}" UniqueName="DD">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EE" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="M/10" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="EE" DataFormatString="{0:0}" UniqueName="EE">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FF" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="L/12" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="FF" DataFormatString="{0:0}" UniqueName="FF">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GG" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="XL/14" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="GG" DataFormatString="{0:0}" UniqueName="GG">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HH" HeaderButtonType="TextButton" ColumnGroupName="gReposicion"
                            DefaultInsertValue="0" HeaderText="XXL/16" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="HH" DataFormatString="{0:0}" UniqueName="HH">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="MVTD_DEBITO" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                        DefaultInsertValue="0" HeaderText="Debito" ItemStyle-HorizontalAlign="Right"
                        Resizable="true" SortExpression="MVTD_DEBITO" DataFormatString="{0:C}" UniqueName="MVTD_DEBITO">
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>--%>
                    </Columns>
                    <NoRecordsTemplate>
                        <div id="message" runat="server">
                            <div id="box-messages" class="box">
                                <div class="messages">
                                    <div id="message-notice" class="message message-notice">
                                        <div class="image">
                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                        </div>
                                        <div class="text">
                                            <h6>
                                                Información</h6>
                                            <span>No existen Resultados </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
        <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetReposicioDiaria" TypeName="XUSS.BLL.Consultas.ReposicionDiariaBL">
            <SelectParameters>
                <asp:Parameter Name="conecction" Type="String" DefaultValue="" />
                <asp:SessionParameter Name="inCodemp" Type="String" SessionField="CODEMP" />
                <asp:Parameter Name="filter" Type="String" DefaultValue="AND 1=0" />
                <asp:ControlParameter ControlID="edt_fechaIni" DefaultValue="" Name="infechaIni" PropertyName="SelectedDate"
                    Type="DateTime" />
                <asp:ControlParameter ControlID="edt_fechaFin" DefaultValue="" Name="infechaFin" PropertyName="SelectedDate"
                    Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="obj_bodegas" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetLstBodegas" TypeName="XUSS.BLL.Comun.ComunBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                <asp:Parameter Name="ALMACEN" Type="String" DefaultValue="S" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="obj_categoria" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            </SelectParameters>
        </asp:ObjectDataSource>
    <%--</telerik:RadAjaxPanel>--%>
</asp:Content>
