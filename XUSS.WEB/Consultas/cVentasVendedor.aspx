<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="cVentasVendedor.aspx.cs" Inherits="XUSS.WEB.Consultas.cVentasVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
             />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Ventas x Vendedor</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
                <tr>
                    <td><label>F. Inicial</label></td>
                    <td>                     
                        <telerik:RadDatePicker ID="edt_fechaI" runat="server">
                        </telerik:RadDatePicker>                
                    </td>                    
                    <td><label>F. Final</label></td>
                    <td>                     
                        <telerik:RadDatePicker ID="edt_fechaF" runat="server">
                        </telerik:RadDatePicker>                
                    </td>                    
                    <td>
                    <telerik:RadButton ID="btn_cargar" runat="server" Text="Refrescar" 
                            onclick="btn_cargar_Click">
                    </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>            
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="950px" AutoGenerateColumns="False"
                Culture="(Default)" CellSpacing="0" ShowGroupPanel="true" AllowSorting="True"
                DataSourceID="obj_consulta" ShowFooter="true">
                <MasterTableView ShowGroupFooter="true">
                    <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Almacen" FieldName="BDNOMBRE" 
                                HeaderValueSeparator=":"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="BDNOMBRE" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Agente" FieldName="TRNOMBRE"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="TRNOMBRE"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
                    <Columns>                        
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Almacen" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="T. Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE">
                            <HeaderStyle Width="200px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Agente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE">
                            <HeaderStyle Width="200px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="Sub. Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDSUBTOT" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ITEMS" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="Items" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ITEMS" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDTOTIVA" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="IVA" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDTOTIVA" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:N}"
                            HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDTOTFAC" Aggregate="Sum" >
                            <HeaderStyle Width="90px" />
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
                </MasterTableView>                
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                <Selecting AllowRowSelect="True"></Selecting>
                <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                    ResizeGridOnColumnResize="False"></Resizing>
            </ClientSettings>
            <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                </HeaderContextMenu>
            </telerik:RadGrid>

        </div>
        </telerik:RadAjaxPanel>
        <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetVentas" TypeName="XUSS.BLL.Consultas.cVentasVendedorBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />           
            <asp:ControlParameter ControlID="edt_fechaI" DefaultValue="" Name="FecIni" PropertyName="SelectedDate" Type="DateTime" />
            <asp:ControlParameter ControlID="edt_fechaF" DefaultValue="" Name="FecFin" PropertyName="SelectedDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>        
</asp:Content>
