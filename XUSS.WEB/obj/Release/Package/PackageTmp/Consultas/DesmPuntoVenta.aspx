<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DesmPuntoVenta.aspx.cs" Inherits="XUSS.WEB.Consultas.DesmPuntoVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <script type="text/javascript">
        function onRequestStart(Sender, args) {
            if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgDetalle">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" />
                </UpdatedControls>
            </telerik:AjaxSetting>        
            <telerik:AjaxSetting AjaxControlID="btn_cargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" />
                </UpdatedControls>
            </telerik:AjaxSetting>    
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"/>
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Desempeño Almacen</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
                <tr>
                    <td>
                        <label>
                            Mes</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_mes" runat="server" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="1" Text="Enero" />
                                <telerik:RadComboBoxItem Value="2" Text="Febrero" />
                                <telerik:RadComboBoxItem Value="3" Text="Marzo" />
                                <telerik:RadComboBoxItem Value="4" Text="Abril" />
                                <telerik:RadComboBoxItem Value="5" Text="Mayo" />
                                <telerik:RadComboBoxItem Value="6" Text="Junio" />
                                <telerik:RadComboBoxItem Value="7" Text="Julio" />
                                <telerik:RadComboBoxItem Value="8" Text="Agosto" />
                                <telerik:RadComboBoxItem Value="9" Text="septiembre" />
                                <telerik:RadComboBoxItem Value="10" Text="Octubre" />
                                <telerik:RadComboBoxItem Value="11" Text="Noviembre" />
                                <telerik:RadComboBoxItem Value="12" Text="Diciembre" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <label>
                            Año</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_ano" runat="server" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="2011" Text="2011" />
                                <telerik:RadComboBoxItem Value="2012" Text="2012" />
                                <telerik:RadComboBoxItem Value="2013" Text="2013" />
                                <telerik:RadComboBoxItem Value="2014" Text="2014" />
                                <telerik:RadComboBoxItem Value="2015" Text="2015" />
                                <telerik:RadComboBoxItem Value="2016" Text="2016" />
                                <telerik:RadComboBoxItem Value="2017" Text="2017" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Gerente</label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <label>
                            Almacen</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-ES" 
                            DataSourceID="obj_bodegas" DataTextField="BDNOMBRE" DataValueField="BDBODEGA">
                            <%--<Items>
                                <telerik:RadComboBoxItem Value="-1" Text="-Todos-"/>
                            </Items>--%>
                        </telerik:RadComboBox>
                        <asp:ObjectDataSource ID="obj_bodegas" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetLstBodegas" TypeName="XUSS.BLL.Comun.ComunBL">
                            <SelectParameters>
                                <asp:Parameter Name="connection" Type="String" />
                                <asp:Parameter Name="ALMACEN" Type="String" DefaultValue="S" />                                
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td rowspan="2">
                        <telerik:RadButton ID="btn_cargar" runat="server" Text="Refrescar" OnClick="btn_cargar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="960px" AutoGenerateColumns="False" AllowSorting ="true" 
                 Culture="(Default)" CellSpacing="0" DataSourceID="obj_consulta" onprerender="rgDetalle_PreRender" ShowFooter="true"
                >
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                    <ColumnGroups>
                        <telerik:GridColumnGroup Name="gDias" HeaderText="DIA" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="9px"></telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gUnidades" HeaderText="UNIDADES" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="9px"></telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gFacturas" HeaderText="FACTURAS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="9px"></telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gPromedios" HeaderText="PROM. ACUMULADOS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="9px"></telerik:GridColumnGroup>
                        <telerik:GridColumnGroup Name="gVentas" HeaderText="VENTAS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="9px"></telerik:GridColumnGroup>
                    </ColumnGroups>
                    <Columns>                                                
                        
                        <telerik:GridTemplateColumn DataField="DSEMA" HeaderText=" " UniqueName="DSEMA" ColumnGroupName="gDias"
                            HeaderStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="labeldia" runat="server" Text='<%# Eval("DSEMA") %>' Enabled="false"
                                     />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                        
                        <telerik:GridBoundColumn DataField="DIA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" AllowSorting="true" ColumnGroupName="gDias"
                            HeaderText=" " ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DIA" >
                            <HeaderStyle Width="100px" />                            
                            <ItemStyle HorizontalAlign="Right" />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TOTFAC_DIA" HeaderButtonType="TextButton" HeaderStyle-Width="80px" AllowSorting="true" ColumnGroupName="gVentas"
                            HeaderText="Vta Diaria" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            DataFormatString="{0:N0}" SortExpression="TOTFAC_DIA">
                            <HeaderStyle Width="150px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TOTFAC_ACU" HeaderButtonType="TextButton" HeaderStyle-Width="80px" AllowSorting="true" ColumnGroupName="gVentas"
                            HeaderText="Vta Acumulada" ItemStyle-HorizontalAlign="Right" Resizable="true" 
                            SortExpression="TOTFAC_ACU" DataFormatString="{0:N0}">
                            <HeaderStyle Width="150px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FAC_DIA" HeaderButtonType="TextButton" HeaderStyle-Width="300px" AllowSorting="true" ColumnGroupName="gFacturas"
                            DataFormatString="{0:N0}" HeaderText="F. Dia" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="FAC_DIA">
                            <HeaderStyle Width="70px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FAC_ACU" HeaderButtonType="TextButton" HeaderStyle-Width="80px" AllowSorting="true" ColumnGroupName="gFacturas"
                            HeaderText="F. Acum" ItemStyle-HorizontalAlign="Right" Resizable="true" DataFormatString="{0:N0}"
                            SortExpression="FAC_ACU">
                            <HeaderStyle Width="120px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="170px" AllowSorting="true"
                            HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE" Visible="false" >
                            <HeaderStyle Width="170px" Font-Size="9px"/>                            
                            <ItemStyle HorizontalAlign="Right" Font-Size="9px"/>                            
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="UND_DIA" HeaderButtonType="TextButton" HeaderStyle-Width="300px" AllowSorting="true" ColumnGroupName="gUnidades"
                            DataFormatString="{0:N0}" HeaderText="UN." ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="UND_DIA">
                            <HeaderStyle Width="70px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UND_ACU" HeaderButtonType="TextButton" HeaderStyle-Width="300px" AllowSorting="true" ColumnGroupName="gUnidades"
                            DataFormatString="{0:N0}" HeaderText="UN. ACUM." ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="UND_ACU">
                            <HeaderStyle Width="120px" Font-Size="9px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                                                
                        <%--<telerik:GridBoundColumn DataField="PRO_VTAFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" AllowSorting="true"
                            HeaderText="PROM FACT" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            DataFormatString="{0:N}" SortExpression="PRO_VTAFAC">
                            <HeaderStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="VLR_FAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" AllowSorting="true" ColumnGroupName="gPromedios"
                            HeaderText="Vlr Factura" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            DataFormatString="{0:N0}" SortExpression="VLR_FAC">
                            <HeaderStyle Width="150px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UND_FAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" AllowSorting="true" ColumnGroupName="gPromedios"
                            HeaderText="Un X Factura" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            DataFormatString="{0:N}" SortExpression="UND_FAC">
                            <HeaderStyle Width="150px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="VLR_PREN" HeaderButtonType="TextButton" HeaderStyle-Width="100px" AllowSorting="true" ColumnGroupName="gPromedios"
                            HeaderText="Vlr PRENDA" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            DataFormatString="{0:N0}" SortExpression="VLR_PREN">
                            <HeaderStyle Width="150px" Font-Size="9px"/>
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
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
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"></CommandItemSettings>                    
                </MasterTableView>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <ExportSettings HideStructureColumns="true"></ExportSettings>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </div>
    <%--</telerik:RadAjaxPanel>--%>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDesAlmacen" TypeName="XUSS.BLL.Consultas.DesmPuntoVentaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:ControlParameter ControlID="rc_mes" DefaultValue="" Name="mes" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="rc_ano" DefaultValue="" Name="ano" PropertyName="SelectedValue"
                Type="String" />
            <asp:ControlParameter ControlID="rc_bodega" DefaultValue="" Name="bodega" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
