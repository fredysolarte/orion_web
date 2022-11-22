<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ConsultaVentasCarteraVendedor.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaVentasCarteraVendedor" %>

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
                        Consulta Vendedor</h5>
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
                    
                </tr>
                <tr>
                    <td>
                        <label>Agente</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains" >                                            
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton" 
                            CausesValidation="true"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>

    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
        SelectedIndex="0" CssClass="tabStrip">
        <Tabs>
            <telerik:RadTab Text="DashBoard">
            </telerik:RadTab>
            <telerik:RadTab Text="Datos Ventas">
            </telerik:RadTab>
            <telerik:RadTab Text="Datos Cartera">
            </telerik:RadTab>
            <telerik:RadTab Text="Datos Recaudo">
            </telerik:RadTab>
            <telerik:RadTab Text="Clientes">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <telerik:RadHtmlChart runat="server" Width="800px" Height="500px" ID="RadHtmlChart1" DataSourceID="obj_grafica" Skin="MetroTouch">
            <PlotArea>
                <Series>
                    <telerik:ColumnSeries DataFieldY="CANTIDAD" Name="Unidades Vendidas">
                        <TooltipsAppearance Color="White" />
                    </telerik:ColumnSeries>
                </Series>
                <XAxis DataLabelsField="ARDTTEC4">
                    <LabelsAppearance RotationAngle="75">
                    </LabelsAppearance>
                    <TitleAppearance Text="Marca">
                    </TitleAppearance>
                </XAxis>
                <YAxis>
                    <TitleAppearance Text="Cantidad">
                    </TitleAppearance>
                </YAxis>
            </PlotArea>
            <Legend>
                <Appearance Visible="false">
                </Appearance>
            </Legend>
            <ChartTitle Text="Unidades Vendidas">
            </ChartTitle>
        </telerik:RadHtmlChart>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pv_dventas" runat="server">
            <telerik:RadGrid ID="rgVentas" runat="server" Width="100%" AutoGenerateColumns="False" 
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="True"
                DataSourceID="obj_ventas" ShowFooter="True" 
                GroupPanelPosition="Top" ResolvedRenderMode="Classic">                
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                </ClientSettings>
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" 
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="NOMBRE" FieldName="NOMBRE" HeaderText="Vendedor"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="NOMBRE" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <ColumnGroups>
                            <telerik:GridColumnGroup Name="Cliente" HeaderText="Informacion Cliente"
                                HeaderStyle-HorizontalAlign="Center" />                            
                    </ColumnGroups>
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />                    
                    <Columns>
                        <telerik:GridBoundColumn DataField="HDCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="HDCODNIT" ColumnGroupName="Cliente">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Nombre" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TRNOMBRE" ColumnGroupName="Cliente">                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Resizable="true" SortExpression="HDFECFAC" HeaderText="Fec. Fac"
                            AllowFiltering="false" UniqueName="HDFECFAC" HeaderButtonType="TextButton" DataField="HDFECFAC"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" DataFormatString="{0:d}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            FilterControlWidth="100px" HeaderText="T. Documento" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TFNOMBRE">
                            <HeaderStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                       
                        <telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                            AllowFiltering="false" HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="HDNROFAC">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                            AllowFiltering="false" HeaderText="Referencia" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTCLAVE1">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                            AllowFiltering="false" HeaderText="Nom Articulo" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARNOMBRE">
                            <HeaderStyle Width="350px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DTCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" DataFormatString="{0:0.0}" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTCANTID" FooterText="" Aggregate="Sum">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="DTPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Pr Lsta" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTPRELIS" FooterText="" Aggregate="Sum">                            
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="DTTOTDES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" DataFormatString="{0:0.#}" HeaderText="Dcto" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTTOTDES">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="DTSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTSUBTOT" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="DTTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Total" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DTTOTFAC" Aggregate="Sum" FooterText="Total:">                            
                        </telerik:GridNumericColumn>                                                
                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="280px" 
                            AllowFiltering="false" HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="NOMBRE">                            
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
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pv_dcartera" runat="server">
            <telerik:RadGrid ID="rg_cartera" runat="server" Width="100%" AutoGenerateColumns="False"
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="True"
                DataSourceID="obj_cartera" ShowFooter="True" 
                GroupPanelPosition="Top" ResolvedRenderMode="Classic">                
                <ClientSettings>
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                </ClientSettings>
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" 
                    GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="VENDEDOR" FieldName="VENDEDOR" HeaderText="Vendedor"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="VENDEDOR" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <ColumnGroups>
                            <telerik:GridColumnGroup Name="Cliente" HeaderText="Informacion Cliente"
                                HeaderStyle-HorizontalAlign="Center" />                            
                    </ColumnGroups>
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />                    
                    <Columns>
                        <telerik:GridBoundColumn DataField="HDCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="HDCODNIT" ColumnGroupName="Cliente">                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Nombre" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="CLIENTE" ColumnGroupName="Cliente">                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Resizable="true" SortExpression="HDFECFAC" HeaderText="Fec. Fac"
                            AllowFiltering="false" UniqueName="HDFECFAC" HeaderButtonType="TextButton" DataField="HDFECFAC"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" DataFormatString="{0:d}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            FilterControlWidth="100px" HeaderText="T. Documento" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TFNOMBRE">
                            <HeaderStyle Width="150px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                        
                        <telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                            AllowFiltering="false" HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="HDNROFAC">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridNumericColumn DataField="VLR_CARTERA" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Cartera" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="VLR_CARTERA" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="RECAUDO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Recaudo" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="RECAUDO" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="TDEV" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Devoluciones" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TDEV" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>                         
                         <telerik:GridNumericColumn DataField="SFXAPL" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="SF x Apli" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="SFXAPL" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="RECA_SF" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="SF Apli" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="RECA_SF" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridNumericColumn DataField="SALDO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="SALDO" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>                        
                        <telerik:GridBoundColumn DataField="VENDEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="VENDEDOR">                            
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
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <telerik:RadGrid ID="rg_recaudo" runat="server" Width="100%" AutoGenerateColumns="False" 
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="True"
                DataSourceID="obj_recaudo" ShowFooter="True" 
                GroupPanelPosition="Top" ResolvedRenderMode="Classic">                
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                </ClientSettings>
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="NOMBRE" FieldName="NOMBRE" HeaderText="Vendedor"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="NOMBRE" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <ColumnGroups>
                            <telerik:GridColumnGroup Name="Cliente" HeaderText="Informacion Cliente"
                                HeaderStyle-HorizontalAlign="Center" />                            
                    </ColumnGroups>
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />                    
                    <Columns>                        
                        <telerik:GridBoundColumn Resizable="true" SortExpression="RC_FECREC" HeaderText="Fec. Fac"
                            AllowFiltering="false" UniqueName="RC_FECREC" HeaderButtonType="TextButton" DataField="HDFECFAC"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" DataFormatString="{0:d}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RC_NRORECIBO" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Nro Recibo" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="RC_NRORECIBO" >                            
                        </telerik:GridBoundColumn>                                                    
                        <telerik:GridNumericColumn DataField="RECAUDO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Vlr Recaudo" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="RECAUDO" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>                      
                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="NOMBRE">                            
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
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <telerik:RadGrid ID="rgClientes" runat="server" Width="100%" AutoGenerateColumns="False" 
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="True"
                DataSourceID="obj_clientes" ShowFooter="True" 
                GroupPanelPosition="Top" ResolvedRenderMode="Classic">                
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2">
                    </Scrolling>
                </ClientSettings>
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" GroupLoadMode="Client">
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="VENDEDOR" FieldName="VENDEDOR" HeaderText="Vendedor"></telerik:GridGroupByField>
                        </SelectFields>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldName="VENDEDOR" SortOrder="Descending"></telerik:GridGroupByField>
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <ColumnGroups>
                            <telerik:GridColumnGroup Name="Cliente" HeaderText="Informacion Cliente"
                                HeaderStyle-HorizontalAlign="Center" />                            
                    </ColumnGroups>
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />                    
                    <Columns>                                                
                        <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Cliente" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="CLIENTE" >                            
                        </telerik:GridBoundColumn>    
                        <telerik:GridBoundColumn Resizable="true" SortExpression="MXFEC" HeaderText="Fec. Ult Fac"
                            AllowFiltering="false" UniqueName="MXFEC" HeaderButtonType="TextButton" DataField="MXFEC"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px" DataFormatString="{0:d}">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                                                
                        <telerik:GridNumericColumn DataField="TT" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Vta Totales" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TT" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>                      
                        <telerik:GridNumericColumn DataField="TTMES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" 
                            DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Vta Ultima Fac" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TTMES" FooterText="" Aggregate="Sum">                            
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn DataField="VENDEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="VENDEDOR">                            
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
                <FilterMenu RenderMode="Lightweight">
                </FilterMenu>
                <HeaderContextMenu RenderMode="Lightweight">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
     <asp:ObjectDataSource ID="obj_ventas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDetalleFacturacion" TypeName="XUSS.BLL.Consultas.ConsultasFacturasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue="AND 1=0"/>      
            <asp:Parameter Name="inMes" Type="String" DefaultValue="1"/>           
            <asp:Parameter Name="inAno" Type="String" DefaultValue="2014"/>          
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_cartera" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetCartera" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0"/>           
            <asp:Parameter Name="inMes" Type="String" DefaultValue="1"/>           
            <asp:Parameter Name="inAno" Type="String" DefaultValue="2014"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_recaudo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetRecaudo" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0"/>           
            <asp:Parameter Name="inMes" Type="String" DefaultValue="1"/>           
            <asp:Parameter Name="inAno" Type="String" DefaultValue="2014"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_grafica" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetVentasGrafica" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0"/>           
            <asp:Parameter Name="inMes" Type="String" DefaultValue="1"/>           
            <asp:Parameter Name="inAno" Type="String" DefaultValue="2014"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="obj_clientes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetClientesxVendedor" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0"/>         
             <asp:Parameter Name="inMes" Type="Int32" DefaultValue="1"/>           
            <asp:Parameter Name="inAno" Type="Int32" DefaultValue="2014"/>                        
        </SelectParameters>
    </asp:ObjectDataSource>
    </fieldset>
    </telerik:RadAjaxPanel>
</asp:Content>
