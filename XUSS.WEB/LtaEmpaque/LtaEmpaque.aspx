<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LtaEmpaque.aspx.cs" Inherits="XUSS.WEB.LtaEmpaque.LtaEmpaque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
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

        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                console.log(args.get_eventTarget());
                //debugger;
                if (args.EventTarget.indexOf("rg_anexos") != -1) {
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
            function RowContextMenu(sender, eventArgs) {
                //debugger;
                var menu = $find("<%=ct_menu.ClientID %>");
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = index;
                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }
            function OnClientAdded(sender, args) {
                var allowedMimeTypes = $telerik.$(sender.get_element()).attr("data-clientFilter");
                $telerik.$(args.get_row()).find(".ruFileInput").attr("accept", allowedMimeTypes);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Width="100%" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_empaque" runat="server" PageSize="1"
            AllowPaging="True" Width="100%" OnItemCommand="rlv_empaque_OnItemCommand" OnItemDataBound="rlv_empaque_OnItemDataBound"
            DataSourceID="obj_empaque" ItemPlaceholderID="pnlGeneral" DataKeyNames="LH_LSTPAQ" DataSourceCount="0" OnItemInserting="rlv_empaque_ItemInserting"
            OnItemInserted="rlv_empaque_ItemInserted" OnItemUpdating="rlv_empaque_OnItemUpdating">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Lista Empaque</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_empaque" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Lista Empaque</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />                                                    --%>
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
                                    <label>
                                        Nombre/Apellido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Lista</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_lista" runat="server" Enabled="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Pedido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aplicar Filtro" RenderMode="Lightweight">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Lst Empaque</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_lstpaq" runat="server" Enabled="false"
                                    Text='<%# Bind("LH_LSTPAQ") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Pedido</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Visible="false"
                                    Text='<%# Bind("LH_PEDIDO") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                                <asp:LinkButton ID="lnk_pedido" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("LH_PEDIDO") %>' OnClick="lnk_pedido_Click" />
                            </td>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("LH_BODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false"
                                    Text='<%# Eval("TRCODNIT") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false"
                                    Text='<%# Eval("NOM_TER") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("LH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Facturado" Value="FA" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="1" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Detalle" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Anexos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                            <asp:Panel ID="pnDetalle" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnPreRender="rg_items_PreRender"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnDetailTableDataBind="rg_items_OnDetailTableDataBind">
                                    <MasterTableView ShowGroupFooter="true" DataKeyNames="LH_LSTPAQ,LD_ITMPAQ">
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="LH_LSTPAQ,LD_ITMPAQ">
                                                <DetailTables>
                                                    <telerik:GridTableView Name="detalle_caja" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="CL_CAJA" HeaderText="Caja" HeaderButtonType="TextButton"
                                                                DataField="CL_CAJA">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="CL_CANTIDAD" HeaderText="Cantidad" HeaderButtonType="TextButton"
                                                                DataField="CL_CANTIDAD">
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <NoRecordsTemplate>
                                                            <div class="alert alert-danger">
                                                                <strong>¡No se Encontaron Registros!</strong>
                                                            </div>
                                                        </NoRecordsTemplate>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerik:GridBoundColumn SortExpression="MBIDMOVI" HeaderText="ID Movimiento" HeaderButtonType="TextButton"
                                                        DataField="MBIDMOVI">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="MBBODEGA" HeaderText="Bodega" HeaderButtonType="TextButton"
                                                        DataField="MBBODEGA">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="MBCANTID" HeaderText="Can Mov" HeaderButtonType="TextButton"
                                                        DataField="MBCANTID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="MLCDLOTE" HeaderText="Lote" HeaderButtonType="TextButton"
                                                        DataField="MLCDLOTE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="MLCANTID" HeaderText="Can Lote" HeaderButtonType="TextButton"
                                                        DataField="MLCANTID">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <div class="alert alert-danger">
                                                        <strong>¡No se Encontaron Registros!</strong>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="LD_ITMPAQ" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_ITMPAQ"
                                                UniqueName="LD_ITMPAQ">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="LD_BODEGA" HeaderText="Bodega" HeaderStyle-Width="190px"
                                                Resizable="true" SortExpression="PDBODEGA" UniqueName="LD_BODEGA">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox ID="rc_bodegadt" runat="server" Culture="es-CO" Width="190px" 
                                                        Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("LD_BODEGA") %>'
                                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                                        <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                                                    </telerik:RadComboBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn DataField="LD_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_CLAVE1"
                                        UniqueName="LD_CLAVE1">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
                                            <telerik:GridTemplateColumn DataField="LD_CLAVE1" HeaderText="Referencia" UniqueName="LD_CLAVE1_TK"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="LD_CLAVE1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_clave1" runat="server" Text='<%# Eval("LD_CLAVE1") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="link" Text="LD_CLAVE1" UniqueName="LD_CLAVE1" DataTextField="LD_CLAVE1"
                                                HeaderText="Referencia" HeaderStyle-Width="80px">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LD_CANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_CANTID"
                                                UniqueName="LD_CANTID" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PDPRECIO" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                                Resizable="true" SortExpression="PDPRECIO" UniqueName="PDPRECIO">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_precio" runat="server" Enabled="false" DbValue='<%# Bind("PDPRECIO") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_anexos" runat="server">
                            <telerik:RadGrid ID="rg_anexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_anexos_ItemCommand"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_attach" runat="server" Text="Adjunto" Icon-PrimaryIconCssClass="rbAttach" CommandName="attach" ToolTip="Nueva Adjunto" />
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EV_CODIGO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                            SortExpression="EV_CODIGO" UniqueName="EV_CODIGO" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EV_DESCRIPCION" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="EV_DESCRIPCION" UniqueName="EV_DESCRIPCION">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                            HeaderText="">
                                        </telerik:GridButtonColumn>
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
            </ItemTemplate>
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Nro Pedido</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false"
                                Text='<%# Bind("LH_PEDIDO") %>'
                                Width="300px">
                            </telerik:RadTextBox>
                            <telerik:RadButton RenderMode="Lightweight" ID="iBtnBuscarPedido" runat="server" Icon-PrimaryIconCssClass="rbSearch" CommandName="xxxxx" OnClick="iBtnBuscarPedido_OnClick" />
                        </td>
                        <td>
                            <label>Bodega</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("LH_BODEGA") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="1" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Detalle" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Anexos">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_datos" runat="server">
                        <asp:Panel ID="pnDetalle" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnDeleteCommand="rg_items_OnDeleteCommand" ShowHeader="true" OnPreRender="rg_items_PreRender"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnDetailTableDataBind="rg_items_OnDetailTableDataBind" OnNeedDataSource="rg_items_NeedDataSource">
                                <ClientSettings>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true" DataKeyNames="LH_LSTPAQ,LD_ITMPAQ">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                    <CommandItemTemplate>
                                        <%--<asp:Button ID="Button1" Text="Add new item" runat="server" CommandName="Rebind"></asp:Button>--%>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_caja" runat="server" Text="Nueva Caja" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewBox" ToolTip="Nueva Caja" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_lista_cajas" runat="server" Text="Resumen Cajas" Icon-PrimaryIconCssClass="rbConfig" CommandName="ResumeBox" ToolTip="Resumen Cajas" />
                                    </CommandItemTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView Name="detalle_insert" Width="100%" DataKeyNames="TP,C1,C2,C3,C4,MBBODEGA,MLCDLOTE,IT">
                                            <DetailTables>
                                                <telerik:GridTableView Name="detalle_caja_insert" Width="100%" DataKeyNames="LD_CODEMP,LH_LSTPAQ,LD_ITMPAQ,CL_CAJA,TP,C1,C2,C3,C4,MBBODEGA,MLCDLOTE">
                                                    <Columns>
                                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                        <telerik:GridBoundColumn SortExpression="CL_CAJA" HeaderText="Caja" HeaderButtonType="TextButton"
                                                            DataField="CL_CAJA">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn SortExpression="CL_CANTIDAD" HeaderText="Cantidad" HeaderButtonType="TextButton"
                                                            DataField="CL_CANTIDAD">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <NoRecordsTemplate>
                                                        <div class="alert alert-danger">
                                                            <strong>¡No se Encontaron Registros!</strong>
                                                        </div>
                                                    </NoRecordsTemplate>
                                                </telerik:GridTableView>
                                            </DetailTables>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                <telerik:GridBoundColumn SortExpression="IT" HeaderText="ID Movimiento" HeaderButtonType="TextButton"
                                                    DataField="IT">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="MBBODEGA" HeaderText="Bodega" HeaderButtonType="TextButton"
                                                    DataField="MBBODEGA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="MBCANTID" HeaderText="Can Mov" HeaderButtonType="TextButton"
                                                    DataField="MBCANTID">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="MLCDLOTE" HeaderText="Lote" HeaderButtonType="TextButton"
                                                    DataField="MLCDLOTE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="MLCANTID" HeaderText="Can Lote" HeaderButtonType="TextButton"
                                                    DataField="MLCANTID">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No se Encontaron Registros!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridButtonColumn DataTextFormatString="Select {0}" HeaderStyle-Width="20px"
                                            ButtonType="ImageButton" UniqueName="column" HeaderText=""
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CommandName="Select"
                                            ImageUrl="../App_Themes/Tema2/Images/Edit_.gif">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="LD_ITMPAQ" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_ITMPAQ"
                                            UniqueName="LD_ITMPAQ">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LD_TIPPRO" HeaderButtonType="TextButton" HeaderStyle-Width="10px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_TIPPRO"
                                            UniqueName="LD_TIPPRO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="LD_BODEGA" HeaderText="Bodega" HeaderStyle-Width="190px"
                                                Resizable="true" SortExpression="PDBODEGA" UniqueName="LD_BODEGA">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox ID="rc_bodegadt" runat="server" Culture="es-CO" Width="190px" 
                                                        Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("LD_BODEGA") %>'
                                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                                        <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                                                    </telerik:RadComboBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="LD_CLAVE1" HeaderText="Referencia" UniqueName="LD_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="LD_CLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave1" runat="server" Text='<%# Eval("LD_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" Text="LD_CLAVE1" UniqueName="LD_CLAVE1" DataTextField="LD_CLAVE1"
                                            HeaderText="Referencia" HeaderStyle-Width="80px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn DataField="LD_CLAVE2" HeaderText="Referencia" UniqueName="LD_CLAVE2_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="LD_CLAVE2" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave2" runat="server" Text='<%# Eval("LD_CLAVE2") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="LD_CLAVE3" HeaderText="c3" UniqueName="LD_CLAVE3_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="LD_CLAVE3" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave3" runat="server" Text='<%# Eval("LD_CLAVE3") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="LD_CLAVE4" HeaderText="Referencia" UniqueName="LD_CLAVE4_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="LD_CLAVE4" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_clave4" runat="server" Text='<%# Eval("LD_CLAVE4") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LD_CANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="{0:#.###}"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_CANTID"
                                            UniqueName="LD_CANTID">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LD_CANCAN" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="{0:#.###}"
                                            HeaderText="Cant Con" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="LD_CANCAN"
                                            UniqueName="LD_CANCAN">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PDPRECIO" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                            Resizable="true" SortExpression="PDPRECIO" UniqueName="PDPRECIO">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio" runat="server" Enabled="false" DbValue='<%# Bind("PDPRECIO") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_anexos" runat="server">
                        <telerik:RadGrid ID="rg_anexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_anexos_ItemCommand" OnNeedDataSource="rg_anexos_NeedDataSource"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_attach" runat="server" Text="Adjunto" Icon-PrimaryIconCssClass="rbAttach" CommandName="attach" ToolTip="Nueva Adjunto" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EV_CODIGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="EV_CODIGO" UniqueName="EV_CODIGO" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EV_DESCRIPCION" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="EV_DESCRIPCION" UniqueName="EV_DESCRIPCION">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                        HeaderText="">
                                    </telerik:GridButtonColumn>
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
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpBuscarPedido" runat="server" Width="900px" Height="550px" Modal="true" OffsetElementID="main" Title="Buscar Pedido" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Pedido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_pedido" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Contiene</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nomtercero" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroPed" runat="server" Text="Filtrar" OnClick="btn_filtroPed_OnClick" CommandName="Cancel" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroPed" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroPed_OnClick" CommandName="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server" Width="98%" Height="98%">
                            <telerik:RadGrid ID="rgConsultaPedidos" runat="server" AllowSorting="True" Width="100%" Height="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_pedido" OnItemCommand="rgConsultaPedidos_OnItemCommand">
                                <MasterTableView DataKeyNames="PHPEDIDO,PHBODEGA">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PHPEDIDO" HeaderText="Pedido"
                                            UniqueName="PHPEDIDO" HeaderButtonType="TextButton" DataField="PHPEDIDO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PHBODEGA" HeaderText="Bodega"
                                            UniqueName="PHBODEGA" HeaderButtonType="TextButton" DataField="PHBODEGA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="NOM_TER" HeaderText="Tercero"
                                            UniqueName="NOM_TER" HeaderButtonType="TextButton" DataField="NOM_TER" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="450px">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpDisposicion" runat="server" Width="880px" Height="520px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Disponibilidad Inventarios">
                    <ContentTemplate>
                        <asp:Literal runat="server" ID="lt_msjbalance"></asp:Literal>
                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="98%">
                            <asp:Literal runat="server" ID="lt_msjdisponibilidad"></asp:Literal>
                            <telerik:RadGrid ID="rgBalance" runat="server" AllowSorting="True" Width="100%" AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="cestado" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_estado" runat="server" OnCheckedChanged="chk_estado_OnCheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="IT" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IT"
                                            UniqueName="IT">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="item" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="edt_it" runat="server" Text='<%# Bind("IT") %>' Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBBODEGA" HeaderText="Bodega"
                                            UniqueName="BBBODEGA" HeaderButtonType="TextButton" DataField="BBBODEGA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCLAVE1" HeaderText="Referencia"
                                            UniqueName="BBCLAVE1" HeaderButtonType="TextButton" DataField="BBCLAVE1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBTIPPRO" HeaderText=""
                                            UniqueName="BBTIPPRO" HeaderButtonType="TextButton" DataField="BBTIPPRO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCLAVE2" HeaderText=""
                                            UniqueName="BBCLAVE2" HeaderButtonType="TextButton" DataField="BBCLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCLAVE3" HeaderText=""
                                            UniqueName="BBCLAVE3" HeaderButtonType="TextButton" DataField="BBCLAVE3" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCLAVE4" HeaderText=""
                                            UniqueName="BBCLAVE4" HeaderButtonType="TextButton" DataField="BBCLAVE4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Can Bod"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="cantidad" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="edt_cantidadb" runat="server" Text='<%# Bind("BBCANTID") %>' Visible="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="lote" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="edt_lote" runat="server" Text='<%# Bind("BLCDLOTE") %>' Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BLCDLOTE" HeaderText="Lote"
                                            UniqueName="BLCDLOTE" HeaderButtonType="TextButton" DataField="BLCDLOTE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BLDTTEC1" HeaderText="D Tec 1"
                                            UniqueName="BLDTTEC1" HeaderButtonType="TextButton" DataField="BLDTTEC1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BLDTTEC2" HeaderText="D Tec 2"
                                            UniqueName="BLDTTEC2" HeaderButtonType="TextButton" DataField="BLDTTEC2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="cantidad" HeaderStyle-Width="60px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="edt_cantidadl" runat="server" Text='<%# Bind("BLCANTID") %>' Visible="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BLCANTID" HeaderText="Can Lote"
                                            UniqueName="BLCANTID" HeaderButtonType="TextButton" DataField="BLCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn Visible="true" HeaderText="Cant." HeaderStyle-Width="60px" UniqueName="cCant">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="edt_cantidacp" runat="server" Text='<%# Bind("BBCANTID") %>' Visible="true" Width="0px" Enabled="false">
                                                </telerik:RadNumericTextBox>

                                                <telerik:RadNumericTextBox ID="edt_cantidad" runat="server" Value="0" MinValue="0"
                                                    Width="50px" MaxValue="999999">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                                <asp:CompareValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_cantidad" ControlToCompare="edt_cantidacp" Operator="LessThanEqual" Type="Double"
                                                    ErrorMessage="(*)">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:CompareValidator>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Visible="true" HeaderText="Caja" HeaderStyle-Width="40px" UniqueName="cCaja">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="edt_caja" runat="server" Value="0" MinValue="0"
                                                    Width="50px" MaxValue="999999">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <%--<asp:Button ID="btn_aceptardis" runat="server" Text="Aceptar" OnClick="btn_aceptardis_OnClick" />--%>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptardis" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_aceptardis_OnClick" />
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Cod Barras</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Visible="true" AutoPostBack="true" OnTextChanged="txt_barras_OnTextChanged">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <td>
                                    <label>Referencia</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Visible="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_referencia" runat="server" ControlToValidate="txt_referencia"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        Producto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Visible="true" Width="450px">
                                    </telerik:RadTextBox>
                                    <%--<asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C2</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc2" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>C3</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc3" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C Inv</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_caninv" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <label>Precio Lta</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_preciolta" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <label>Dcto</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_dct" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lbl_lote" runat="server">Nro Lote</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="rc_lote" runat="server" ZIndex="1000000"
                                        DropDownWidth="315" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rc_lote"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                        <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Cantidad</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_cantidad" runat="server" Enabled="true" ValidationGroup="grNuevoI" CausesValidation="false">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_catidad1" runat="server" ControlToValidate="txt_cantidad"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txt_cantidad" ControlToCompare="txt_caninv" Operator="LessThanEqual" Type="Integer"
                                        ErrorMessage="(*) Inventario Menor al Valor de Salida" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:CompareValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_cantidad"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    <label>Caja</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="rc_cajam" runat="server" ZIndex="1000000"
                                        DropDownWidth="315" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Caja 1" Value="1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<asp:Button ID="btn_agregar" runat="server" Text="Aceptar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpResumeBox" runat="server" Width="470px" Height="550px" Modal="true" OffsetElementID="main" Title="Resumen Cajas" EnableShadow="true" Style="z-index: 300;">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Width="98%" Height="98%">
                            <telerik:RadGrid ID="rg_resumebox" runat="server" AllowSorting="True" Width="100%" Height="100%" ShowFooter="True" OnDetailTableDataBind="rg_resumebox_DetailTableDataBind"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="25" CellSpacing="0" GridLines="None" OnDeleteCommand="rg_resumebox_DeleteCommand">
                                <MasterTableView DataKeyNames="caja">
                                    <DetailTables>
                                        <telerik:GridTableView Name="detalle_caja" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn SortExpression="C1" HeaderText="Ref" HeaderButtonType="TextButton"
                                                    DataField="C1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="NOMBRE" HeaderText="Nombre" HeaderButtonType="TextButton"
                                                    DataField="NOMBRE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn SortExpression="CAN" HeaderText="Can Mov   " HeaderButtonType="TextButton"
                                                    DataField="CAN">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No se Encontaron Registros!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="caja" HeaderText="Nro Caja"
                                            UniqueName="caja" HeaderButtonType="TextButton" DataField="caja" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="cantidad" HeaderText="Cant"
                                            UniqueName="cantidad" HeaderButtonType="TextButton" DataField="cantidad" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px" FooterText="Total: " Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mp_cam" runat="server" Width="737px" Height="580px" Modal="true" OffsetElementID="main" Title="Cam" EnableShadow="true">
                    <ContentTemplate>
                        <iframe name="myIframe" id="myIframe" width="99%" height="400px" runat="server"></iframe>
                        <table>
                            <tr>
                                <td>
                                    <label>Observaciones</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" ValidationGroup="gvInsertFoto"
                                        Width="600px" TextMode="MultiLine" Height="40px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_observaciones" ValidationGroup="gvInsertFoto"
                                        ErrorMessage="(*)">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnSaveAnexo" runat="server" Text="Guardar" ValidationGroup="gvInsertFoto" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" OnClick="btnSaveAnexo_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargue Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" AllowedFileExtensions="png" data-clientFilter="image/png" OnClientAdded="OnClientAdded">
                                    </telerik:RadAsyncUpload>
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadTextBox ID="txt_obsfoto" runat="server" Enabled="true" Width="600px" TextMode="MultiLine" Height="40px">
                                    </telerik:RadTextBox>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>


    </telerik:RadAjaxPanel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_menu" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_menu_ItemClick">
        <Items>
            <telerik:RadMenuItem Text="Seleccionar Todos">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Anular Seleccion">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>
    <asp:ObjectDataSource ID="obj_empaque" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_empaque_OnInserting" InsertMethod="InsertListaEmpaque"
        SelectMethod="GetLtaEmpaque" TypeName="XUSS.BLL.Pedidos.LtaEmpaqueBL" OnInserted="obj_empaque_OnInserted">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="LH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="LH_PEDIDO" Type="Int32" />
            <asp:Parameter Name="LH_BODEGA" Type="String" />
            <asp:SessionParameter Name="LH_NMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbDetalle" Type="Object" />
            <asp:Parameter Name="tbCajas" Type="Object" />
            <asp:Parameter Name="tbAnexos" Type="Object" />
        </InsertParameters>
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
    <asp:ObjectDataSource ID="obj_pedido" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPedidos" TypeName="XUSS.BLL.Pedidos.PedidosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
