<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CargueInventarios.aspx.cs" Inherits="XUSS.WEB.Inventarios.CargueInventarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
            .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6
            {
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
    <script type="text/javascript">        
        function conditionalPostback(sender, args) {
            console.log(args.get_eventTarget());
            if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_foto$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
                args.set_enableAjax(false);
            }
        }
        function endPostback(sender, args) {
            args.set_enableAjax(true);
        }
        function Clicking(sender, args) {
            if (!confirm("¿Esta Seguro de Anular el Registro?"))
                args.set_cancel(!confirmed);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rauCargar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rauCargar" />
                    <telerik:AjaxUpdatedControl ControlID="rgDetalle" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="rgInconsistencias" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="RadProgressManager1" />
                    <telerik:AjaxUpdatedControl ControlID="RadProgressArea1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator2" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rvl_toma" runat="server" PageSize="1" AllowPaging="True" OnItemCommand="rvl_toma_OnItemCommand" OnItemDataBound="rvl_toma_OnItemDataBound"
            DataSourceID="obj_toma" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" OnItemInserted="rvl_toma_OnItemInserted">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Toma Fisica Inventarios</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rvl_toma" PageSize="1" RenderMode="Lightweight">
                            <Fields>
                                <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                <telerik:RadDataPagerTemplatePageField HorizontalPosition="RightFloat">
                                    <PagerTemplate>
                                        <div style="float: right">
                                            <b>Items
                                                <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.Owner.StartRowIndex+1%>" />
                                                de
                                                <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.TotalRowCount%>" />
                                                <br />
                                            </b>
                                        </div>
                                    </PagerTemplate>
                                </telerik:RadDataPagerTemplatePageField>
                            </Fields>
                        </telerik:RadDataPager>
                    </div>
                    <asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
                    </asp:Panel>
                </fieldset>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_filtro">
                        <div class="box">
                            <div class="title">
                                <h5>Toma Fisica Inventarios</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <label>Nro Foto</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofoto" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
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
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked ="Clicking" ToolTip="Anular Registro"/>                                        
                   <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir"  OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel"  ToolTip="Imprimir"/>--%>
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Planilla</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroplanilla" runat="server" Enabled="false" Text='<%# Bind("IINROFOT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Movimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("IIFECCAR") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Conteo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_conteo" runat="server" Enabled="false" Text='<%# Bind("IINROCON") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Planilla</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_plantilla" runat="server" Enabled="false" Text='<%# Bind("IIIDPLAN") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bodega</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("MIBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("IICOMENT") %>' Width="300px" Height="40px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="3" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Basicos" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Tecnicos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Cod Barras">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Planilla</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroplanilla" runat="server" Enabled="false" Text='<%# Bind("IINROFOT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Movimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("IIFECCAR") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Conteo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_conteo" runat="server" Enabled="false" Text='<%# Bind("IINROCON") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Planilla</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_plantilla" runat="server" Enabled="false" Text='<%# Bind("IIIDPLAN") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bodega</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("MIBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td >
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("IICOMENT") %>' Width="600px" Height="80px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="3" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Toma Fisica" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Diferencias">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Cod Erroneos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" ShowGroupPanel="True">
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_comparativo" runat="server" Text="Comparativo" Icon-PrimaryIconCssClass="rbRefresh" CommandName="comparativo" ToolTip="Comparativo" />
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Zona" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ZONA"
                                            UniqueName="ZONA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Cod Barras" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BCODIGO"
                                            UniqueName="BCODIGO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="TP" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IBCLAVE1"
                                            UniqueName="IBCLAVE1">                                           
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="70px"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IBCANTID"
                                            UniqueName="IBCANTID" Aggregate="Sum" FooterText="Total:">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                            <strong>Alerta!</strong>No Tiene Registros
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_diferencias" runat="server">
                            <telerik:RadGrid ID="rg_diferencias" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" ShowGroupPanel="True">
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TP_NOM" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Marca" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TP_NOM"
                                            UniqueName="TP_NOM">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C1"
                                            UniqueName="C1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C1_NOM" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                            HeaderText="Producto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C1_NOM"
                                            UniqueName="C1_NOM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C2_NOM" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2_NOM"
                                            UniqueName="C2_NOM">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C3_NOM" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3_NOM"
                                            UniqueName="C3_NOM">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Can Fal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTO"
                                            UniqueName="CTO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CTI" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Can Res" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTI"
                                            UniqueName="CTI">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>                        
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_erroneos" runat="server">
                            <telerik:RadGrid ID="rgInconsistencias" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Zona" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ZONA"
                                            UniqueName="ZONA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
                <table>
                        <tr>
                            <td>                                
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                            </td>
                        </tr>
                    </table>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="220px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargue Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>(Referencia + C2 + C3 + C4) o (Cod Barras) - Cantidad - Zona </label>
                                </td>
                            </tr>
                            <tr>
                                <td>                                    
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal" >
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4</asp:ListItem>
                                        <asp:ListItem>Cod. Barras</asp:ListItem>                                
                                    </asp:RadioButtonList>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />        
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Aceptar"   />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>   
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                    <ContentTemplate>
                        <div style="padding: 5px 5px 5px 5px">
                            <ul>
                                <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                            </ul>
                            <div style="text-align: center;">
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
    </telerik:RadWindowManager>

    <%--<fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Cargue Inventarios</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
        <div>
            <table>
                <tr>
                    <td>
                        <label>Tip. Cargue</label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbl_tipinv" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Inv. Inicial</asp:ListItem>
                            <asp:ListItem>T. Fis. Inv.</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Tip. Archivo</label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbl_tipArc" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Cod. Barras</asp:ListItem>
                            <asp:ListItem>Ref + C2 + C3 +C4</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Bodega</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_bodega" runat="server" DataSourceID="obj_bodega" Width="300px"
                            DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadAsyncUpload ID="rauCargar" runat="server" ControlObjectsVisibility="None"
                            Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                            OnFileUploaded="rauCargar_FileUploaded" OnClientFileUploaded="fileUploaded" Width="350px"
                            Style="margin-bottom: 0px">
                            <Localization Select="Cargar Archivo" />
                        </telerik:RadAsyncUpload>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnl_detalle" runat="server" Width="100%">
                <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                <telerik:RadProgressArea ID="RadProgressArea1" runat="server" />
                <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                    <telerik:RadPane ID="LeftPane" runat="server" Width="70%">
                        <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="true">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Zona" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ZONA"
                                        UniqueName="ZONA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LINEA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LINEA"
                                        UniqueName="LINEA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C1DES" HeaderButtonType="TextButton" HeaderText="Referencia"
                                        ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C1DES" UniqueName="C1DES">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C2DES" HeaderButtonType="TextButton" HeaderText="C2"
                                        ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2DES" UniqueName="C2DES">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C3DES" HeaderButtonType="TextButton" HeaderText="C3"
                                        ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3DES" UniqueName="C3DES">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CAN" HeaderButtonType="TextButton" HeaderText="Can"
                                        ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CAN" UniqueName="CAN" Aggregate="Sum" FooterText="Total:">
                                        <HeaderStyle Width="200px" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                    </telerik:RadSplitBar>
                    <telerik:RadPane ID="RadPane1" runat="server" Width="20%">
                        <telerik:RadGrid ID="rgInconsistencias" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Zona" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ZONA"
                                        UniqueName="ZONA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                        UniqueName="BARRAS">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </asp:Panel>       
            <asp:Panel ID="pnl_pie" runat="server" Width="100%">
                <table>
                    <tr>
                        <td>

                        </td>
                    </tr>
                </table>
            </asp:Panel>     
        </div>
    </fieldset>
    
    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                    <ContentTemplate>
                        <div style="padding: 5px 5px 5px 5px">
                            <ul>
                                <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                            </ul>
                            <div style="text-align: center;">
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>            
            </Windows>
    </telerik:RadWindowManager>--%>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_toma" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_toma_OnInserting" OnInserted="obj_toma_OnInserted"
        SelectMethod="GetTomaFisicaHD" InsertMethod="InsertTomaFisica" TypeName="XUSS.BLL.Inventarios.CargueInventariosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue=" 1=0" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="IICODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="IIBODEGA" Type="String" />
            <asp:Parameter Name="IICOMENT" Type="String" />
            <asp:Parameter Name="IIESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="IICAUSAE" Type="String" />            
            <asp:SessionParameter Name="IINMUSER" Type="String" SessionField="UserLogon" />   
            <asp:Parameter Name="inDT" Type="Object" />            
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
