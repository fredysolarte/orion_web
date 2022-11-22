<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="XUSS.WEB.Consultas.DashBoard" %>

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
                    <h5>DashBoard</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <div>
                <table>
                    <tr>
                        <td>
                            <label>Mes</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_mes" runat="server" Width="300px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                    <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                    <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                    <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                    <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                    <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                    <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                    <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                    <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                    <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                    <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                    <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>Año</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_ano" runat="server" Width="300px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="2016" Value="2016" />
                                    <telerik:RadComboBoxItem Text="2017" Value="2017" />
                                    <telerik:RadComboBoxItem Text="2018" Value="2018" />
                                    <telerik:RadComboBoxItem Text="2019" Value="2019" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton"
                                CausesValidation="true">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </div>
            <telerik:RadPanelBar  runat="server" ID="pnl_items"  Width="100%" Skin="MetroTouch"  RenderMode="Classic">
                <Items>
                    <telerik:RadPanelItem Text="Compañia" Expanded="True">
                        <ItemTemplate>
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="700px">
                                <telerik:RadPane ID="LeftPane" runat="server" Width="70%">
                                    <telerik:RadHtmlChart runat="server" ID="ch_ventas" Width="100%" Height="580" Skin="Silk">
                                        <PlotArea>
                                        </PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <ChartTitle Text="Company performance">
                                            <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance BackgroundColor="Transparent" Position="Bottom">
                                            </Appearance>
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane1" runat="server" Width="30%">
                                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="MES" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                    HeaderText="Mes" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MES"
                                                    UniqueName="MES">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="X" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="X" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="X" UniqueName="X" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Z" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="Z" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="Z" UniqueName="Z" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Porcentaje" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="{0:F1}"
                                                    HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="Porcentaje"
                                                    UniqueName="Porcentaje">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Linea">
                        <ItemTemplate>
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="650px">
                                <telerik:RadPane ID="RadPane2" runat="server" Width="30%">
                                    <telerik:RadHtmlChart runat="server" ID="ch_tipomes" Width="100%" Height="580" Skin="Silk">
                                        <PlotArea>
                                        </PlotArea>
                                        <Appearance>
                                            <FillStyle BackgroundColor="Transparent"></FillStyle>
                                        </Appearance>
                                        <ChartTitle Text="Ventas X Linea">
                                            <Appearance Align="Center" BackgroundColor="Transparent" Position="Top">
                                            </Appearance>
                                        </ChartTitle>
                                        <Legend>
                                            <Appearance BackgroundColor="Transparent" Position="Bottom">
                                            </Appearance>
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane1" runat="server" Width="30%">
                                    <telerik:RadGrid ID="rg_detail_lxm" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                        <MasterTableView ShowGroupFooter="true">
                                            <ColumnGroups>
                                                <telerik:GridColumnGroup Name="actual" HeaderText="X1" HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridColumnGroup Name="mes" HeaderText="Y1" HeaderStyle-HorizontalAlign="Center" />
                                                <telerik:GridColumnGroup Name="ano" HeaderText="Z1" HeaderStyle-HorizontalAlign="Center" />
                                            </ColumnGroups>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="XC" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="actual"
                                                    DataFormatString="{0:F}" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="XC" UniqueName="XC" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="XT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="actual"
                                                    DataFormatString="{0:C}" HeaderText="Ventas" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="XT" UniqueName="XT" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="YC" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="mes"
                                                    DataFormatString="{0:F}" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="YC" UniqueName="YC" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="YT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="mes"
                                                    DataFormatString="{0:C}" HeaderText="Ventas" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="YT" UniqueName="YT" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ZC" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="ano"
                                                    DataFormatString="{0:F}" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="ZC" UniqueName="ZC" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ZT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="ano"
                                                    DataFormatString="{0:C}" HeaderText="Ventas" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="ZT" UniqueName="ZT" Aggregate="Sum" FooterStyle-HorizontalAlign="Right" >
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridBoundColumn DataField="Porcentaje" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="{0:F1}"
                                                HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="Porcentaje"
                                                UniqueName="Porcentaje">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Bodega">
                        <ItemTemplate>
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="700px">
                                <telerik:RadPane ID="left" runat="server" Width="25%">
                                    <telerik:RadGrid ID="rgBodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_bodega"
                                            Culture="(Default)" CellSpacing="0" EnableHeaderContextMenu="true">
                                            <ClientSettings>                                               
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <MasterTableView ShowGroupFooter="true">                                                
                                                <Columns>                                                    
                                                    <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton"
                                                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                                        UniqueName="BDBODEGA">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton"
                                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                                                        UniqueName="BDNOMBRE">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="rigth" runat="server" Width="75%">
                                
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Vendedor">
                        <ItemTemplate>                            
                            <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="10" Height="600px"
                                ID="rg_detail_ven" runat="server" ColumnHeaderZoneText="ColumnHeaderZone">
                                <ClientSettings EnableFieldsDragDrop="true">
                                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                                </ClientSettings>
                                <Fields>
                                    <telerik:PivotGridRowField DataField="NOMBRE" ZoneIndex="0" Caption="Vendedor" CellStyle-Width="350px" >                                            
                                    </telerik:PivotGridRowField>
                                    <telerik:PivotGridColumnField DataField="Tipo">
                                    </telerik:PivotGridColumnField>
                                    <telerik:PivotGridAggregateField DataField="CVTA" Aggregate="Sum" Caption="Cant" >
                                        <HeaderCellTemplate>
                                            <asp:Label ID="AggregateCell1" Text="Cant" runat="server" />
                                        </HeaderCellTemplate>
                                        <ColumnGrandTotalHeaderCellTemplate>
                                            <asp:Label ID="Label1" Text="Total Cant" runat="server" />
                                        </ColumnGrandTotalHeaderCellTemplate>
                                    </telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="TVTA" Aggregate="Sum" DataFormatString="{0:C}" Caption="Valor">
                                        <HeaderCellTemplate>
                                            <asp:Label ID="AggregateCell1" Text="Valor" runat="server" />
                                        </HeaderCellTemplate>
                                        <ColumnGrandTotalHeaderCellTemplate>
                                            <asp:Label ID="Label1" Text="Total Valor" runat="server" />
                                        </ColumnGrandTotalHeaderCellTemplate>
                                    </telerik:PivotGridAggregateField>
                                </Fields>
                            </telerik:RadPivotGrid>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Text="Cliente">
                        <ItemTemplate>
                            <%--<telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="10" Height="600px"
                                ID="rg_detail_cli" runat="server" ColumnHeaderZoneText="ColumnHeaderZone">
                                <ClientSettings EnableFieldsDragDrop="true">
                                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                                </ClientSettings>
                                <Fields>
                                    <telerik:PivotGridRowField DataField="TRNOMBRE" ZoneIndex="0" Caption="Cliente" CellStyle-Width="350px" >                                            
                                    </telerik:PivotGridRowField>
                                    <telerik:PivotGridColumnField DataField="TFNOMBRE">
                                    </telerik:PivotGridColumnField>
                                    <telerik:PivotGridAggregateField DataField="DTCANTID" Aggregate="Sum" Caption="Cant" >
                                        <HeaderCellTemplate>
                                            <asp:Label ID="AggregateCell1" Text="Cant" runat="server" />
                                        </HeaderCellTemplate>
                                        <ColumnGrandTotalHeaderCellTemplate>
                                            <asp:Label ID="Label1" Text="Total Cant" runat="server" />
                                        </ColumnGrandTotalHeaderCellTemplate>
                                    </telerik:PivotGridAggregateField>
                                    <telerik:PivotGridAggregateField DataField="DTSUBTOT" Aggregate="Sum" DataFormatString="{0:C}" Caption="Valor">
                                        <HeaderCellTemplate>
                                            <asp:Label ID="AggregateCell1" Text="Valor" runat="server" />
                                        </HeaderCellTemplate>
                                        <ColumnGrandTotalHeaderCellTemplate>
                                            <asp:Label ID="Label1" Text="Total Valor" runat="server" />
                                        </ColumnGrandTotalHeaderCellTemplate>
                                    </telerik:PivotGridAggregateField>
                                </Fields>
                            </telerik:RadPivotGrid>--%>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelBar>
            <asp:Panel ID="pnItemMaster" runat="server" Width="100%">
                <div style="width: 75%; background-color: White; position: relative; float: left;">
                </div>
                <div style="width: 25%; position: relative; float: right;">
                </div>
            </asp:Panel>
            <asp:Panel ID="pnl_tipo" runat="server" Width="100%">
                <div style="width: 55%; background-color: White; position: relative; float: left;">
                </div>
                <div style="width: 45%; position: relative; float: right;">
                </div>
            </asp:Panel>
        </fieldset>
    </telerik:RadAjaxPanel>
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
