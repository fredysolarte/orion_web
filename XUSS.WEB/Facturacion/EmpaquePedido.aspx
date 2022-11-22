<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="EmpaquePedido.aspx.cs" Inherits="XUSS.WEB.Facturacion.EmpaquePedido" %>

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
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_pedidos$ctrl0$btn_descargar")) {
                    args.set_enableAjax(false);
                }

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
            function LiquidarPedido(sender, args) {
                if (!confirm("¿Esta Seguro de Liquidar el Pedido?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_pedidos" runat="server" PageSize="1" AllowPaging="True" OnItemCommand="rlv_pedidos_OnItemCommand" OnItemInserting="rlv_pedidos_OnItemInserting" OnItemUpdating="rlv_pedidos_OnItemUpdating"
            OnItemDataBound="rlv_pedidos_OnItemDataBound" OnItemInserted="rlv_pedidos_OnItemInserted" OnPreRender="rlv_pedidos_OnPreRender"
            DataSourceID="obj_pedidos" ItemPlaceholderID="pnlGeneral" DataKeyNames="PHPEDIDO"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Entrega Productos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_pedidos" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Entrega Productos</h5>
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
                                    <label>
                                        Nombre/Apellido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Pedido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Lst Empaque</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="lst_empaque" runat="server" Enabled="true" Width="300px">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_descargar" runat="server" Text="Descargar" OnClick="iBtnDownload_OnClick" Icon-PrimaryIconCssClass="rbDownload" CommandName="Cancel" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_reliquidar" runat="server" Text="Liquidar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="Liquidar" OnClientClicked="LiquidarPedido" ToolTip="Liquidar Pedido" />--%>
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Pedido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Text='<%# Bind("PHPEDIDO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_lstpaq" runat="server" Enabled="false" Text='<%# Eval("LH_LSTPAQ") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Pedido</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("PHFECPED") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
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
                                <label>Sucursal</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_sucursal" runat="server" Culture="es-CO" Width="680px" ValidationGroup="gvInsert"
                                    Enabled="false" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T Pedido</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpedido" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tpedido" DataTextField="PTNOMBRE" SelectedValue='<%# Bind("PHTIPPED") %>'
                                    DataValueField="PTTIPPED" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("PHBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Idioma</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PHIDIOMA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PHMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Lst Precio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("PHLISPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Vendedor</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("PHOBSERV") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("PHESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">                        
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
                                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_items_PreRender">
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="PDLINNUM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                    HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDLINNUM"
                                                    UniqueName="PDLINNUM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCLAVE1"
                                                    UniqueName="PDCLAVE1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                                    HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDCANPED" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;em&gt;{0:#.###}&lt;/em&gt;"
                                                    HeaderText="C Sol" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCANPED"
                                                    UniqueName="PDCANPED" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="C Dis" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCANTID"
                                                    UniqueName="PDCANTID" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PDCANPED" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                                    Resizable="true" SortExpression="PDCANPED" UniqueName="PDCANPED">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_canped" runat="server" Enabled="false" DbValue='<%# Bind("PDCANPED") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PDCANTID" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                                    Resizable="true" SortExpression="PDCANTID" UniqueName="PDCANTID">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_candis" runat="server" Enabled="false" DbValue='<%# Bind("PDCANTID") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="PDDESCUE" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="{0:0.#}"
                                                    HeaderText="Dcto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDDESCUE"
                                                    UniqueName="PDDESCUE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                    HeaderText="P Lista" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDPRELIS"
                                                    UniqueName="PDPRELIS">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                    HeaderText="P Vta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDPRECIO"
                                                    UniqueName="PDPRECIO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PDSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                                    HeaderText="Sub Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDSUBTOT"
                                                    UniqueName="PDSUBTOT" Aggregate="Sum">
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
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="pv_anexos" runat="server">
                                    <telerik:RadGrid ID="rg_anexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_anexos_ItemCommand"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                        <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
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
                    </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Pedido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false"
                                    Text='<%# Eval("PHPEDIDO") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Pedido</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("PHFECPED") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_fSolicitud" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="true" Visible="false"
                                    Text='<%# Bind("PHCODCLI") %>'
                                    Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Eval("TRCODNIT") %>'
                                    Width="250px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_nit" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick" />
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
                                <label>Sucursal</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_sucursal" runat="server" Culture="es-CO" Width="680px" ValidationGroup="gvInsert"
                                    Enabled="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T Pedido</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpedido" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert" OnSelectedIndexChanged="rc_tpedido_SelectedIndexChanged"
                                    Enabled="true" DataSourceID="obj_tpedido" DataTextField="PTNOMBRE" SelectedValue='<%# Bind("PHTIPPED") %>' AutoPostBack="true" CausesValidation="false"
                                    DataValueField="PTTIPPED" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tpedido" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="true" DataSourceID="obj_bodegaxusuario" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("PHBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_bodega" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Idioma</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="true" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PHIDIOMA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_idioma" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PHMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_moneda" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Lst Precio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("PHLISPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Vendedor</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_agente" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("PHOBSERV") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("PHESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
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
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnPreRender="rg_items_PreRender"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" OnDeleteCommand="rg_items_OnDeleteCommand">
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PDLINNUM">
                                        <%--<CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="true" RefreshText="Cargar" />--%>
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="PDLINNUM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDLINNUM"
                                                UniqueName="PDLINNUM">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PDCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCLAVE1"
                                                UniqueName="PDCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PDCANPED" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;em&gt;{0:#.###}&lt;/em&gt;"
                                                HeaderText="C Sol" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCANPED"
                                                UniqueName="PDCANPED" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PDCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                HeaderText="C Dis" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDCANTID"
                                                UniqueName="PDCANTID" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PDCANPED" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                                Resizable="true" SortExpression="PDCANPED" UniqueName="PDCANPED">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_canped" runat="server" Enabled="false" DbValue='<%# Bind("PDCANPED") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn DataField="PDCANTID" HeaderText="PVP" HeaderStyle-Width="90px" Visible="false"
                                                Resizable="true" SortExpression="PDCANTID" UniqueName="PDCANTID">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_candis" runat="server" Enabled="false" DbValue='<%# Bind("PDCANTID") %>' MinValue="0" Value="0" Width="90px" Visible="false">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="PDPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                HeaderText="P Lista" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDPRELIS"
                                                UniqueName="PDPRELIS">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PDPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                HeaderText="P Vta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDPRECIO"
                                                UniqueName="PDPRECIO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PDSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                                HeaderText="Sub Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PDSUBTOT"
                                                UniqueName="PDSUBTOT" Aggregate="Sum">
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
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_anexos" runat="server">
                                <telerik:RadGrid ID="rg_anexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_anexos_ItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
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
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalTerceros" runat="server" Width="830px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Terceros">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Codigo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_codter" runat="server" Enabled="true" Width="80px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_identificacion" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Contiene</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nomtercero" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroTer" runat="server" Text="Filtrar" OnClick="btn_filtroTer_OnClick" CommandName="Cancel" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroTer" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Filtrar" CommandName="Cancel" OnClick="btn_filtroTer_OnClick" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server" Width="100%">
                            <telerik:RadGrid ID="rgConsultaTerceros" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_terceros" OnItemCommand="rgConsultaTerceros_OnItemCommand">
                                <MasterTableView DataKeyNames="TRCODTER,TRCODNIT,TRNOMBRE,TRNOMBR2,TRAPELLI">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODTER" HeaderText="Codigo"
                                            UniqueName="TRCODTER" HeaderButtonType="TextButton" DataField="TRCODTER" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODNIT" HeaderText="Ident"
                                            UniqueName="TRCODNIT" HeaderButtonType="TextButton" DataField="TRCODNIT" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBRE" HeaderText="Nombre"
                                        UniqueName="TRNOMBRE" HeaderButtonType="TextButton" DataField="TRNOMBRE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="80px">
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBR2" HeaderText=""
                                        UniqueName="TRNOMBR2" HeaderButtonType="TextButton" DataField="TRNOMBR2" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="80px">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Tercero" UniqueName="TERCERO_TK"
                                            HeaderStyle-Width="160px" AllowFiltering="false" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nomter" runat="server" Text='<%# String.Format("{0}-{1}",Eval("TRNOMBRE")," ",Eval("TRNOMBR2")," ",Eval("TRAPELLI")) %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRLISPRE" HeaderText="Lta Precio"
                                            UniqueName="TRLISPRE" HeaderButtonType="TextButton" DataField="TRLISPRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Estado" UniqueName="TERCERO_EST"
                                            HeaderStyle-Width="160px" AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Estadi" runat="server" Text='<%# Eval("TRESTADO") %>' Visible="false"></asp:Label>
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
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFiltroArt" runat="server" Width="900px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 1900;" Title="Buscar Productos">
                    <ContentTemplate>
                        <asp:Panel ID="pn_filart" runat="server" DefaultButton="btn_filtroArticulos">
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            Referencia</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_referencia" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>
                                            Nombre</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_nombreart" runat="server" Enabled="true" Width="350px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" ToolTip="Filtro" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="5" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_articulos" OnItemCommand="rgConsultaArticulos_OnItemCommand">
                                <FilterMenu Style="z-index: 2001;"></FilterMenu>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Sel" CommandName="Select">
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARTIPPRO" HeaderText="" Visible="true"
                                            UniqueName="ARTIPPRO" HeaderButtonType="None" DataField="ARTIPPRO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE1" HeaderText="Referencia"
                                            UniqueName="ARCLAVE1" HeaderButtonType="TextButton" DataField="ARCLAVE1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE2" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE2" UniqueName="ARCLAVE2">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave2" runat="server" Visible="false" Text='<%# Bind("ARCLAVE2") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn DataField="ARCLAVE3" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE3" UniqueName="ARCLAVE3">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave3" runat="server" Visible="false" Text='<%# Bind("ARCLAVE3") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE4" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE4" UniqueName="ARCLAVE4">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave4" runat="server" Visible="false" Text='<%# Bind("ARCLAVE4") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
                                            UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="400px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="" Visible="true"
                                            UniqueName="CLAVE2" HeaderButtonType="None" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="20px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="" Visible="true"
                                            UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC4" HeaderText=""
                                            UniqueName="ARDTTEC4" HeaderButtonType="TextButton" DataField="ARDTTEC4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC1" HeaderText=""
                                            UniqueName="ARDTTEC1" HeaderButtonType="TextButton" DataField="ARDTTEC1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PRECIO" HeaderText="Precio Lts" DataFormatString="{0:0.0}"
                                            UniqueName="PRECIO" HeaderButtonType="TextButton" DataField="PRECIO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="DESCUENTO" HeaderText="Dcto" DataFormatString="{0:0.#}"
                                            UniqueName="DESCUENTO" HeaderButtonType="TextButton" DataField="DESCUENTO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Can" DataFormatString="{0:0.#}"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="TANOMBRE" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="TANOMBRE" UniqueName="TANOMBRE">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_marcafl" runat="server" Visible="false" Text='<%# Bind("TANOMBRE") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
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
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_agregar" runat="server" DefaultButton="btn_agregar">
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
                                    <telerik:RadTextBox ID="txt_nomtp" runat="server" Enabled="true" Visible="false">
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
                                        <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />
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
                                        <telerik:RadComboBox RenderMode="Lightweight" ID="rc_lote" runat="server" ZIndex="1000000" Width=""
                                            DropDownWidth="300px" AppendDataBoundItems="true">
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
                                        <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_cantidad"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" ToolTip="Agregar" ValidationGroup="grNuevoI" OnClick="btn_aceptar_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargue Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4</asp:ListItem>
                                        <asp:ListItem>Cod. Barras</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Aceptar" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpTester" runat="server" Width="930px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Relacion Tester">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Cantidad</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_canhomologo" runat="server" Enabled="true" Width="80px">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_calcular" runat="server" Text="Calcular" Icon-PrimaryIconCssClass="rbRefresh" ToolTip="Calcular" CommandName="Cancel" OnClick="btn_calcular_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_tester" runat="server" AllowSorting="True" Width="100%" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">
                                <MasterTableView DataKeyNames="C1">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="30px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_itemtest" runat="server" Enabled="false" Width="150px" Visible="false" Text='<%# Bind("IT") %>'>
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NOMTP" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="80px" HeaderText="Linea" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="NOMTP" UniqueName="NOMTP">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C1" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="80px" HeaderText="Ref" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="C1" UniqueName="C1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PRODUCTO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="200px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="PRODUCTO" UniqueName="PRODUCTO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CANDIS" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="C Dis" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CANDIS"
                                            UniqueName="CANDIS" FooterText="Total: " Aggregate="Sum">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="tes_can" HeaderStyle-Width="30px">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_tescan" runat="server" DbValue='<%# Bind("CANSOL") %>' Enabled="true" MinValue="0" Value="0" Width="90px" Visible="true">
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
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_loadtester" runat="server" Text="Cargar" Icon-PrimaryIconCssClass="rbUpload" ToolTip="Cargar" CommandName="Cancel" OnClick="btn_loadtester_Click" />
                                </td>
                            </tr>
                        </table>
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_pedidos" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_pedidos_OnInserting" OnUpdating="obj_pedidos_OnUpdating"
        SelectMethod="GetPedidosEmpaques" TypeName="XUSS.BLL.Pedidos.PedidosBL" OnInserted="obj_pedidos_OnInserted" InsertMethod="InsertPedidoEmpaqueHD">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="PHCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="PHFECPED" Type="DateTime" />
            <asp:Parameter Name="PHCODCLI" Type="Int32" />
            <asp:Parameter Name="PHCODSUC" Type="Int32" />
            <asp:Parameter Name="PHAGENTE" Type="Int32" />
            <asp:Parameter Name="PHTIPPED" Type="String" />
            <asp:Parameter Name="PHIDIOMA" Type="String" />
            <asp:Parameter Name="PHMONEDA" Type="String" />
            <asp:Parameter Name="PHTRMLOC" Type="Double" />
            <asp:Parameter Name="PHBODEGA" Type="String" />
            <asp:Parameter Name="PHLISPRE" Type="String" />
            <asp:SessionParameter Name="PHNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="PHOBSERV" Type="String" />
            <asp:Parameter Name="PHESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="DetPedidos" Type="Object" />
            <asp:Parameter Name="tbAnexos" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="PHCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="PHFECPED" Type="DateTime" />
            <asp:Parameter Name="PHCODCLI" Type="Int32" />
            <asp:Parameter Name="PHCODSUC" Type="Int32" />
            <asp:Parameter Name="PHAGENTE" Type="Int32" />
            <asp:Parameter Name="PHTIPPED" Type="String" />
            <asp:Parameter Name="PHIDIOMA" Type="String" />
            <asp:Parameter Name="PHMONEDA" Type="String" />
            <asp:Parameter Name="PHBODEGA" Type="String" />
            <asp:Parameter Name="PHLISPRE" Type="String" />
            <asp:SessionParameter Name="PHNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="PHOBSERV" Type="String" />
            <asp:Parameter Name="PHESTADO" Type="String" />
            <asp:Parameter Name="DetPedidos" Type="Object" />
            <asp:Parameter Name="original_PHPEDIDO" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tpedido" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipPed" TypeName="XUSS.BLL.Parametros.TPedidoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
    <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_idioma" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="LANG" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_articulos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetArticulos" TypeName="XUSS.BLL.Pedidos.PedidosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" AND 1=0" Name="filter" Type="String" />
            <asp:Parameter Name="inBodega" Type="String" />
            <asp:Parameter Name="LT" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_lstprecio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetListaPrecioHD" TypeName="XUSS.BLL.ListaPrecios.ListaPreciosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_bodegaxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuario" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
