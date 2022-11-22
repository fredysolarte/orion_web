<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="OrdenesCompras.aspx.cs" Inherits="XUSS.WEB.Compras.OrdenesCompras" %>

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
            function OnClientEntryAddingHandler(sender, eventArgs) {
                if (sender.get_entries().get_count() > 0) {
                    eventArgs.set_cancel(true);
                }
            }
            function openChild() {
                var oWnd = $find("<%=modalMensaje.ClientID %>");//Parent is radwindow ID name
                oWnd.show();

            }
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_compras$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton") || (args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_compras$ctrl0$rg_factura$ctl00$ctl02$ctl00$btn_excel")) {
                    args.set_enableAjax(false);
                }
                if (args.EventTarget.indexOf("rgSoportes") != -1) {
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
                var menu = $find("<%=ct_bodegas.ClientID %>");
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = "1-" + index;
                if (eventArgs._id.search("rg_proforma") > 0)
                    document.getElementById("radGridClickedRowIndex").value = "2-" + index;
                if (eventArgs._id.search("rg_factura") > 0)
                    document.getElementById("radGridClickedRowIndex").value = "3-" + index;

                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="90000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_compras" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_compras_OnItemInserted"
            OnItemCommand="rlv_compras_OnItemCommand" OnItemDataBound="rlv_compras_OnItemDataBound" OnItemUpdating="rlv_compras_ItemUpdating"
            DataSourceID="obj_compras" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Purchar Order's</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_compras" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Purchar Order's</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
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
                                        Nro Order</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroOrden" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Order Int</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroOrdenInt" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Invoice</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_factura" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Proforma</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_proforma" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        T. Order</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_torden" runat="server"
                                        Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            <telerik:RadComboBoxItem Text="Orden Compra" Value="1" />
                                            <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                            <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                            <telerik:RadComboBoxItem Text="Orden Compra Int" Value="4" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Warehouse</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px"
                                        DataSourceID="obj_bodega" DataTextField="BDNOMBRE"
                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Status</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_estado" runat="server"
                                        Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                            <telerik:RadComboBoxItem Text="Aprobado" Value="AP" />
                                            <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                            <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Search" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Search" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="New" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Edit" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="To Print" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_email" runat="server" Text="Email" OnClick="btn_email_Click" Icon-PrimaryIconCssClass="rbMail" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Cod Int</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Eval("CH_NROCMP") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>D Order</label></td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Nro Order Int</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrocmpalt" runat="server" Enabled="true" Text='<%# Bind("CH_CNROCMPALT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Warehouse</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("CH_BODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Supplier</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" SelectedValue='<%# Bind("CH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>

                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>T. Order</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_torden" runat="server" SelectedValue='<%# Bind("CH_TIPORD") %>' Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Orden de Compra" Value="1" />
                                        <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                        <telerik:RadComboBoxItem Text="Orden Compra Int" Value="4" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T. Shipping</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdespacho" runat="server" SelectedValue='<%# Bind("CH_TIPDPH") %>' Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Completo" Value="01" />
                                        <telerik:RadComboBoxItem Text="Parcial" Value="02" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>T Pago</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_terpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("CH_TERPAG") %>'
                                    DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Currency</label>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CH_MONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Price</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_precio" runat="server" DbValue='<%# Eval("PRECIO") %>' Enabled="false"></telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <label>Status</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("CH_ESTADO") %>' Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Aprobado" Value="AP" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <label>Observations</label></td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("CH_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Purchar Order">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Proforma Invoice/s">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Invoice/s">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Summari">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Aditional Cost">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Bill Of Lading">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Supoort Documents">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Following">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_ordencompra" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" ShowGroupPanel="True"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" AllowDragToGroup="True">                                
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                            UniqueName="CD_NROITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="CD_CLAVE1" HeaderText="Referencia" UniqueName="CD_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_CLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("CD_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="CD_CLAVE1" DataTextField="CD_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="CD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_REFPRO"
                                            UniqueName="CD_REFPRO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                            UniqueName="NOMTTEC2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                            UniqueName="NOMTTEC3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                            UniqueName="NOMTTEC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                            UniqueName="NOMTTEC5">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                            UniqueName="NOMTTEC7">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("CD_PAGO")) %>' Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_PRECIO"
                                            UniqueName="CD_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CANTIDAD"
                                            UniqueName="CD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="140px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                            UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;" >
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_proforma_ItemCommand">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_NROFACPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Nro Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROFACPROFORMA"
                                            UniqueName="PR_NROFACPROFORMA">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                            UniqueName="NOMTTEC2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                            UniqueName="NOMTTEC3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                            UniqueName="NOMTTEC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                            UniqueName="NOMTTEC5">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                            UniqueName="NOMTTEC7">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="rc_porigen" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("PR_ORIGEN") %>'
                                                    Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                                    Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_POSARA" HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                            Resizable="true" SortExpression="PR_POSARA" UniqueName="PR_POSARA">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_arancel" runat="server" Enabled="false" Width="100px" Text='<%# Bind("PR_POSARA") %>'>
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_CANTIDAD"
                                            UniqueName="PR_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="140px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                            UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <telerik:RadGrid ID="rg_factura" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_factura_ItemCommand">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="FD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROITEM"
                                            UniqueName="FD_NROITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FD_NROFACTURA" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Nro Invoice" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROFACTURA"
                                            UniqueName="FD_NROFACTURA">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FD_FECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Invoice" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_FECFAC"
                                            UniqueName="FD_FECFAC">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="FD_CLAVE1" HeaderText="Referencia" UniqueName="FD_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="FD_CLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("FD_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="FD_CLAVE1" DataTextField="FD_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="FD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_REFPRO"
                                            UniqueName="FD_REFPRO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                            UniqueName="NOMTTEC2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                            UniqueName="NOMTTEC3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                            UniqueName="NOMTTEC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                            UniqueName="NOMTTEC5">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                            UniqueName="NOMTTEC7">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="FD_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="FD_ORIGEN" UniqueName="FD_ORIGEN">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="rc_porigenfac" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("FD_ORIGEN") %>'
                                                    Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI" AllowCustomText="true" Filter="Contains"
                                                    Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="FD_POSARA" HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                            Resizable="true" SortExpression="FD_POSARA" UniqueName="FD_POSARA">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_arancelfac" runat="server" Enabled="false" Width="100px" Text='<%# Bind("FD_POSARA") %>'>
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("FD_PAGO")) %>' Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="FD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_PRECIO"
                                            UniqueName="FD_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_CANTIDAD"
                                            UniqueName="FD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="140px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                            UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView5" runat="server" Height="100%">
                                     <telerik:RadGrid ID="rg_resumen" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" 
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_resumen_NeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />                                            
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ITM" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ITM"
                                                    UniqueName="ITM">                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tipo" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="T Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tipo"
                                                    UniqueName="tipo">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CD_NROCMP" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROCMP"
                                                    UniqueName="CD_NROCMP">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="CD_ESTADO" HeaderText="Status" HeaderStyle-Width="150px" Visible="True"
                                                    Resizable="true" SortExpression="CD_ESTADO" UniqueName="CD_ESTADO">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="rc_estadodoc" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("CD_ESTADO") %>'
                                                            Culture="es-CO" Width="130px" Enabled="false" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                                <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView DataKeyNames="CT_NROITEM">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="true" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CT_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_CLAVE1"
                                            UniqueName="CT_CLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                            HeaderText="Servicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO"
                                            UniqueName="CT_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_bl" runat="server">
                            <telerik:RadGrid ID="rg_bl" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnDetailTableDataBind="rg_bl_DetailTableDataBind"
                                Culture="(Default)" ShowFooter="True" OnItemCommand="rg_bl_ItemCommand" OnNeedDataSource="rg_bl_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <DetailTables>
                                        <telerik:GridTableView Name="detalle_item" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="BLD_NROCONTAINER" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Container" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROCONTAINER"
                                                    UniqueName="BLD_NROCONTAINER">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLD_NROPACK" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Pack" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROPACK"
                                                    UniqueName="BLD_NROPACK">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLD_DESCRIPTION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_DESCRIPTION"
                                                    UniqueName="BLD_DESCRIPTION">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLD_GROSSWEIGHT" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="Gross Weigth" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_GROSSWEIGHT"
                                                    UniqueName="BLD_GROSSWEIGHT">
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
                                        <telerik:GridBoundColumn DataField="BLH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Date" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_FECHA"
                                            UniqueName="BLH_FECHA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BLH_NROBILLOFL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro BL" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_NROBILLOFL"
                                            UniqueName="BLH_NROBILLOFL">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BLH_BOOKINGNO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro Booking" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_BOOKINGNO"
                                            UniqueName="BLH_BOOKINGNO">
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
                        <telerik:RadPageView ID="pv_soportes" runat="server">
                            <asp:Panel runat="server" ID="Panel1">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView>
                                        <CommandItemSettings ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No Records to Display!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView4" runat="server">
                            <asp:Panel runat="server" ID="Panel3">
                                <telerik:RadGrid ID="rg_seguimiento" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_seguimiento"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_seguimiento_OnNeedDataSource">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SG_CONSECUTIVO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="SG_CONSECUTIVO" UniqueName="SG_CONSECUTIVO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SG_DESCRIPCION" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SG_DESCRIPCION" UniqueName="SG_DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SG_USUARIO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="User" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SG_USUARIO" UniqueName="SG_USUARIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SG_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Date" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SG_FECING"
                                                UniqueName="SG_FECING">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No Records to Display!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <telerik:RadSplitter RenderMode="Lightweight" ID="Radsplitter3" runat="server" Height="750px" Width="100%" Orientation="Horizontal">
                    <telerik:RadPane ID="Radpane4" runat="server" Height="200px">
                        <asp:Panel ID="pnItemMaster" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>Nro Order</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Eval("CH_NROCMP") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>D. Order</label></td>
                                    <td>
                                        <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="true" OnSelectedDateChanged="edt_forden_SelectedDateChanged" AutoPostBack="true">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rqf_identificacion" runat="server" ControlToValidate="edt_forden"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Nro Order Int</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrocmpalt" runat="server" Enabled="true" Text='<%# Bind("CH_CNROCMPALT") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Warehouse</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("CH_BODEGA") %>'
                                            DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_bodegas" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Supplier</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" SelectedValue='<%# Bind("CH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                            Width="300px" DataSourceID="obj_terceros" AppendDataBoundItems="true" Filter="StartsWith">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            </Items>

                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_proveedor" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>T. Order</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_torden" runat="server" SelectedValue='<%# Bind("CH_TIPORD") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                <telerik:RadComboBoxItem Text="Orden de Compra" Value="1" />
                                                <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                                <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_torden" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>T. Shipping</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tdespacho" runat="server" SelectedValue='<%# Bind("CH_TIPDPH") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                <telerik:RadComboBoxItem Text="Completo" Value="01" />
                                                <telerik:RadComboBoxItem Text="Parcial" Value="02" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_tdespacho" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Payment</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_terpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("CH_TERPAG") %>'
                                            DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_tpago" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Currency</label>
                                        <td>
                                            <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_moneda_OnSelectedIndexChanged" AutoPostBack="true"
                                                Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CH_MONEDA") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_moneda" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Price</label></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_precio" runat="server" DbValue='<%# Eval("PRECIO") %>' Enabled="true"></telerik:RadNumericTextBox>
                                    </td>
                                    <%-- <td>
                                        <label>Estado</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("CH_ESTADO") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                                <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>--%>
                                </tr>
                                <%--<tr>
                            <td>
                                <label>Nro F Proforma</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_fproforma" runat="server" Enabled="true" Text='<%# Eval("CH_NROFACPROFORMA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Factura</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="true" Text='<%# Eval("CH_NROFACTURA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>--%>
                                <tr>
                                    <td>
                                        <label>Observations</label></td>
                                    <td colspan="4">
                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("CH_OBSERVACIONES") %>'
                                            Width="600px" TextMode="MultiLine" Height="60px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="Forward">
                    </telerik:RadSplitBar>
                    <telerik:RadPane ID="Radpane5" runat="server">
                        <asp:Panel ID="pnDetalle" runat="server">
                            <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                                SelectedIndex="0" CssClass="tabStrip">
                                <Tabs>
                                    <telerik:RadTab Text="Purchar Order">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Proforma Invoice/s">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Invoice/s">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Summari">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Aditional Cost">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Supoort Documents">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Following">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="pv_ordencompra" runat="server">
                                    <telerik:RadGrid ID="rg_items" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" Height="350px"
                                        Culture="(Default)" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" OnDeleteCommand="rg_items_OnDeleteCommand" GroupPanelPosition="Top">
                                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                ResizeGridOnColumnResize="False"></Resizing>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CD_NROITEM">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="New Item" RefreshText="Load" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="30px" />
                                                <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                                    UniqueName="CD_NROITEM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                    HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                                    UniqueName="BARRAS">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="CD_CLAVE1" HeaderText="Referencia" UniqueName="CD_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("CD_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="CD_CLAVE1" DataTextField="CD_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="CD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_REFPRO"
                                                    UniqueName="CD_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("CD_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="CD_PRECIO" HeaderText="Pre x UN" UniqueName="CD_PRECIO"
                                                    HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="CD_PRECIO" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_precio_cmp" runat="server" Enabled="true" DbValue='<%# Eval("CD_PRECIO") %>' OnTextChanged="txt_precio_cmp_TextChanged" AutoPostBack="true" Width="90%" NumberFormat-DecimalDigits="6">                                                            
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="CD_CANTIDAD" HeaderText="Cant" UniqueName="CD_CANTIDAD"
                                                    HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="CD_CANTIDAD" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("CD_CANTIDAD") %>' OnTextChanged="txt_cantidad_cmp_TextChanged" AutoPostBack="true" Width="90%" NumberFormat-DecimalDigits="0">
                                                            <%-- ClientEvents-OnValueChanged="cmp_cantidad_changevalue"  --%>
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn DataField="VLRTOT" HeaderText="Vlr Total" UniqueName="VLRTOT"
                                                    HeaderStyle-Width="140px" AllowFiltering="false" SortExpression="VLRTOT" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_total_cmp" runat="server" Enabled="true" DbValue='<%# Eval("VLRTOT") %>' Width="90%" NumberFormat-DecimalDigits="6">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView1" runat="server">
                                    <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_proforma_OnItemCommand"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_proforma_OnNeedDataSource" GroupPanelPosition="Top" OnDeleteCommand="rg_proforma_DeleteCommand">
                                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                ResizeGridOnColumnResize="False"></Resizing>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PR_NROITEM">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                                    UniqueName="PR_NROITEM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PR_FECPROFORMA" HeaderText="D Proforma" HeaderStyle-Width="140px" Visible="True"
                                                    Resizable="true" SortExpression="PR_FECPROFORMA" UniqueName="PR_FECPROFORMA">
                                                    <ItemTemplate>
                                                        <telerik:RadDatePicker ID="edt_fproforma" runat="server" MinDate="01/01/1900" Enabled="true" DbSelectedDate='<%# Bind("PR_FECPROFORMA") %>' Width="120px">
                                                        </telerik:RadDatePicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Nro Proforma" HeaderStyle-Width="150px" Visible="True"
                                                    Resizable="true" SortExpression="PR_NROFACPROFORMA" UniqueName="PR_NROFACPROFORMA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_nroproforma" runat="server" Enabled="true" Width="120px" Text='<%# Bind("PR_NROFACPROFORMA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                                    UniqueName="BARRAS">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                                    UniqueName="PR_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                                    UniqueName="PR_PRECIO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PR_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_CANTIDAD"
                                                    UniqueName="PR_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                                    UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView2" runat="server">
                                    <telerik:RadGrid ID="rg_factura" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_factura_OnItemCommand"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_factura_OnNeedDataSource" GroupPanelPosition="Top" OnDeleteCommand="rg_factura_DeleteCommand">
                                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                ResizeGridOnColumnResize="False"></Resizing>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="FD_NROITEM">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                <telerik:GridBoundColumn DataField="FD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROITEM"
                                                    UniqueName="FD_NROITEM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="FD_FECFAC" HeaderText="D Invoice" HeaderStyle-Width="140px" Visible="True"
                                                    Resizable="true" SortExpression="FD_FECFAC" UniqueName="FD_FECFAC">
                                                    <ItemTemplate>
                                                        <telerik:RadDatePicker ID="edt_ffactura" runat="server" MinDate="01/01/1900" Enabled="true" DbSelectedDate='<%# Bind("FD_FECFAC") %>' Width="120px">
                                                        </telerik:RadDatePicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_NROFACTURA" HeaderText="Nro Invoice" HeaderStyle-Width="150px" Visible="True"
                                                    Resizable="true" SortExpression="FD_NROFACTURA" UniqueName="FD_NROFACTURA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_nrofactura" runat="server" Enabled="true" Width="120px" Text='<%# Bind("FD_NROFACTURA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="BARRAS" HeaderText="Bar Code" HeaderStyle-Width="130px" Visible="True"
                                                    Resizable="true" SortExpression="BARRAS" UniqueName="BARRAS">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_barrasfac" runat="server" Enabled="true" Width="120px" Text='<%# Bind("BARRAS") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="FD_CLAVE1" HeaderText="Referencia" UniqueName="FD_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="FD_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("FD_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="FD_CLAVE1" DataTextField="FD_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="FD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_REFPRO"
                                                    UniqueName="FD_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("FD_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="FD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_PRECIO"
                                                    UniqueName="FD_PRECIO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_CANTIDAD"
                                                    UniqueName="FD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                                    UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView5" runat="server" Height="100%">
                                     <telerik:RadGrid ID="rg_resumen" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" 
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_resumen_NeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />                                            
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ITM" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ITM"
                                                    UniqueName="ITM">                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tipo" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="T Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tipo"
                                                    UniqueName="tipo">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CD_NROCMP" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROCMP"
                                                    UniqueName="CD_NROCMP">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="CD_ESTADO" HeaderText="Status" HeaderStyle-Width="150px" Visible="True"
                                                    Resizable="true" SortExpression="CD_ESTADO" UniqueName="CD_ESTADO">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="rc_estadodoc" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("CD_ESTADO") %>'
                                                            Culture="es-CO" Width="130px" Enabled="true" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                                <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView3" runat="server">
                                    <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_costos_OnItemCommand"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_NROITEM">

                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                            </CommandItemTemplate>

                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CT_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Reference" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_CLAVE1"
                                                    UniqueName="CT_CLAVE1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                    HeaderText="Service Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Price" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO"
                                                    UniqueName="CT_PRECIO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="pv_soportes" runat="server">
                                    <asp:Panel runat="server" ID="Panel1">
                                        <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                            OnItemCommand="rgSoportes_OnItemCommand">
                                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                                <%--<CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />--%>
                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                                <CommandItemTemplate>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                </CommandItemTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                        SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                        HeaderText="">
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings EditFormType="Template">
                                                    <FormTemplate>
                                                        <div>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            Descripcion</label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="edt_nombre" runat="server" Enabled="true" Width="350px">
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            Archivo</label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadAsyncUpload ID="rauCargarSoporte" runat="server" ControlObjectsVisibility="None"
                                                                            Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                                            Width="350px" OnFileUploaded="rauCargarSoporte_FileUploaded"
                                                                            Style="margin-bottom: 0px">
                                                                            <Localization Select="Cargar Archivo" />
                                                                        </telerik:RadAsyncUpload>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                            ValidationGroup="grNuevo" OnClick="btn_aceptar_OnClick" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </FormTemplate>
                                                </EditFormSettings>
                                                <NoRecordsTemplate>
                                                    <div class="alert alert-danger">
                                                        <strong>¡No Records to Display!</strong>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView4" runat="server">
                                    <asp:Panel runat="server" ID="Panel3">
                                        <telerik:RadGrid ID="rg_seguimiento" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_seguimiento" OnItemCommand="rg_seguimiento_ItemCommand"
                                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_seguimiento_OnNeedDataSource">
                                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                                <CommandItemTemplate>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                </CommandItemTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="SG_CONSECUTIVO" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                        SortExpression="SG_CONSECUTIVO" UniqueName="SG_CONSECUTIVO" Visible="true">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SG_DESCRIPCION" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Description" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SG_DESCRIPCION" UniqueName="SG_DESCRIPCION">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="User" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="usua_nombres" UniqueName="usua_nombres">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SG_FECING" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Date" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SG_FECING" UniqueName="SG_FECING">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <div class="alert alert-danger">
                                                        <strong>¡No Records to Display!</strong>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Save" RenderMode="Lightweight" />
                                    <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancel" RenderMode="Lightweight" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </InsertItemTemplate>
            <EditItemTemplate>
                <telerik:RadSplitter RenderMode="Lightweight" ID="Radsplitter3" runat="server" Height="750px" Width="100%" Orientation="Horizontal">
                    <telerik:RadPane ID="Radpane4" runat="server" Height="200px">
                        <asp:Panel ID="pnItemMaster" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>Nro Order</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Bind("CH_NROCMP") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>F Orden</label></td>
                                    <td>
                                        <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="true" OnSelectedDateChanged="edt_forden_SelectedDateChanged" AutoPostBack="true">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rqf_identificacion" runat="server" ControlToValidate="edt_forden"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Nro Orden Int</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrocmpalt" runat="server" Enabled="true" Text='<%# Bind("CH_CNROCMPALT") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Bodega</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("CH_BODEGA") %>'
                                            DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_bodegas" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Proveedor</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" SelectedValue='<%# Bind("CH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                            Width="300px" DataSourceID="obj_terceros" Filter="StartsWith">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            </Items>

                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_proveedor" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>T. Orden</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_torden" runat="server" SelectedValue='<%# Bind("CH_TIPORD") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                <telerik:RadComboBoxItem Text="Orden de Compra" Value="1" />
                                                <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                                <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_torden" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>T. Despacho</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tdespacho" runat="server" SelectedValue='<%# Bind("CH_TIPDPH") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                <telerik:RadComboBoxItem Text="Completo" Value="01" />
                                                <telerik:RadComboBoxItem Text="Parcial" Value="02" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_tdespacho" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>T Pago</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_terpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("CH_TERPAG") %>'
                                            DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_tpago" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Moneda</label>
                                        <td>
                                            <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_moneda_OnSelectedIndexChanged" AutoPostBack="true"
                                                Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CH_MONEDA") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_moneda" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Precio</label></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_precio" runat="server" DbValue='<%# Eval("PRECIO") %>' Enabled="true"></telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <label>Estado</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("CH_ESTADO") %>' Enabled="true"
                                            Width="300px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                                <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                <telerik:RadComboBoxItem Text="Aprobado" Value="AP" />
                                                <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <label>Observaciones</label></td>
                                    <td colspan="4">
                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("CH_OBSERVACIONES") %>'
                                            Width="600px" TextMode="MultiLine" Height="60px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="Forward">
                    </telerik:RadSplitBar>
                    <telerik:RadPane ID="Radpane5" runat="server">
                        <asp:Panel ID="pnDetalle" runat="server" Height="95%">
                            <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                                SelectedIndex="0" CssClass="tabStrip">
                                <Tabs>
                                    <telerik:RadTab Text="Purchar Order">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Proforma Invoice/s">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Invoice/s">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Summary">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Aditional Cost">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Bill Of Lading">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Supoort Documents">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Following">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%">
                                <telerik:RadPageView ID="pv_ordencompra" runat="server">
                                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" 
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" OnDeleteCommand="rg_items_OnDeleteCommand">
                                        
                                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                ResizeGridOnColumnResize="False"></Resizing>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CD_NROITEM" Height="100%">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_excel" runat="server" Text="Xls" Icon-PrimaryIconCssClass="rbDownload" CommandName="ExportExcel" ToolTip="Excel" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="30px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="30px" />
                                                <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                                    UniqueName="CD_NROITEM">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                    HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                                    UniqueName="BARRAS">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="CD_CLAVE1" HeaderText="Referencia" UniqueName="CD_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("CD_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="CD_CLAVE1" DataTextField="CD_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="CD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_REFPRO"
                                                    UniqueName="CD_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("CD_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="CD_PRECIO" HeaderText="Pre x UN" UniqueName="CD_PRECIO"
                                                    HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="CD_PRECIO" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_precio_cmp" runat="server" Enabled="true" DbValue='<%# Eval("CD_PRECIO") %>' OnTextChanged="txt_precio_cmp_TextChanged" AutoPostBack="true" Width="90%" NumberFormat-DecimalDigits="6">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="CD_CANTIDAD" HeaderText="Cant" UniqueName="CD_CANTIDAD"
                                                    HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="CD_CANTIDAD" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("CD_CANTIDAD") %>' OnTextChanged="txt_cantidad_cmp_TextChanged" AutoPostBack="true" Width="90%" NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="140px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                                    HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                                    UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView1" runat="server" Height="100%">
                                    <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_proforma_OnItemCommand" AllowMultiRowSelection="true"  ShowGroupPanel="True"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_proforma_OnNeedDataSource" GroupPanelPosition="Top" OnDeleteCommand="rg_proforma_DeleteCommand">
                                        <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PR_NROITEM" Height="100%">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_excel" runat="server" Text="Xls" Icon-PrimaryIconCssClass="rbDownload" CommandName="ExportExcel" ToolTip="Excel" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="30px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="30px" />

                                                <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                                    UniqueName="PR_NROITEM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PR_NROITEM" HeaderText="" UniqueName="PR_NROITEM"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_NROITEM" Visible="false">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_codpro" runat="server" Enabled="true" Text='<%# Bind("PR_NROITEM") %>' Visible="false">
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_FECPROFORMA" HeaderText="D Proforma" HeaderStyle-Width="140px" Visible="True"
                                                    Resizable="true" SortExpression="PR_FECPROFORMA" UniqueName="PR_FECPROFORMA">
                                                    <ItemTemplate>
                                                        <telerik:RadDatePicker ID="edt_fproforma" runat="server" MinDate="01/01/1900" Enabled="true" DbSelectedDate='<%# Bind("PR_FECPROFORMA") %>' Width="120px">
                                                        </telerik:RadDatePicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Nro Proforma" HeaderStyle-Width="130px" Visible="True"
                                                    Resizable="true" SortExpression="PR_NROFACPROFORMA" UniqueName="PR_NROFACPROFORMA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_nroproforma" runat="server" Enabled="true" Width="120px" Text='<%# Bind("PR_NROFACPROFORMA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_DIAS" HeaderText="Nro Days" HeaderStyle-Width="40px" Visible="True"
                                                    Resizable="true" SortExpression="PR_DIAS" UniqueName="PR_DIAS">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="tx_dias_pro" runat="server" Enabled="true" Width="35px" DbValue='<%# Bind("PR_DIAS") %>' NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>                                                
                                                <telerik:GridTemplateColumn DataField="BARRAS" HeaderText="Bar Code" HeaderStyle-Width="130px" Visible="True"
                                                    Resizable="true" SortExpression="BARRAS" UniqueName="BARRAS">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_barraspro" runat="server" Enabled="true" Width="120px" Text='<%# Bind("BARRAS") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                                    UniqueName="PR_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="450px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                                    Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="rc_porigen" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("PR_ORIGEN") %>'
                                                            Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI" AllowCustomText="true" Filter="Contains"
                                                            Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_POSARA" HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                                    Resizable="true" SortExpression="PR_POSARA" UniqueName="PR_POSARA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_arancel" runat="server" Enabled="true" Width="100px" Text='<%# Bind("PR_POSARA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px" GroupByExpression="PR_PAGO Group By PR_PAGO" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_PRECIO" HeaderText="Pre x UN" UniqueName="PR_PRECIO"
                                                    HeaderStyle-Width="120px" AllowFiltering="false" SortExpression="CD_PRECIO" Visible="true">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_precio_pro" runat="server" Enabled="true" DbValue='<%# Eval("PR_PRECIO") %>' OnTextChanged="txt_precio_pro_TextChanged" AutoPostBack="true" Width="90%" NumberFormat-DecimalDigits="6">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="PR_CANTIDAD" HeaderText="Qty" HeaderStyle-Width="70px" Visible="True"
                                                    Resizable="true" SortExpression="PR_CANTIDAD" UniqueName="PR_CANTIDAD" Aggregate="Sum" FooterText=" ">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_can_pro" runat="server" Enabled="true" DbValue='<%# Bind("PR_CANTIDAD") %>' AutoPostBack="true" OnTextChanged="txt_canproedit_TextChanged"
                                                            Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                                    HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                                    UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView2" runat="server" Height="100%">
                                    <telerik:RadGrid ID="rg_factura" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_factura_OnItemCommand" ShowGroupPanel="True" AllowMultiRowSelection="true"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_factura_OnNeedDataSource" GroupPanelPosition="Top" OnDeleteCommand="rg_factura_DeleteCommand">
                                        <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="FD_NROITEM" Height="100%">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Load File" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_excel" runat="server" Text="Xls" Icon-PrimaryIconCssClass="rbDownload" CommandName="ExportExcel" ToolTip="Excel" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="30px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="30px" />
                                                <telerik:GridBoundColumn DataField="FD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROITEM"
                                                    UniqueName="FD_NROITEM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="FD_NROITEM" HeaderText="" UniqueName="FD_NROITEM"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_NROITEM" Visible="false">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_codpro" runat="server" Enabled="true" Text='<%# Bind("FD_NROITEM") %>' Visible="false">
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_FECFAC" HeaderText="D Invoice" HeaderStyle-Width="140px" Visible="True"
                                                    Resizable="true" SortExpression="FD_FECFAC" UniqueName="FD_FECFAC">
                                                    <ItemTemplate>
                                                        <telerik:RadDatePicker ID="edt_ffactura" runat="server" MinDate="01/01/1900" Enabled="true" DbSelectedDate='<%# Bind("FD_FECFAC") %>' Width="120px">
                                                        </telerik:RadDatePicker>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_NROFACTURA" HeaderText="Nro Invoice" HeaderStyle-Width="130px" Visible="True"
                                                    Resizable="true" SortExpression="FD_NROFACTURA" UniqueName="FD_NROFACTURA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_nrofactura" runat="server" Enabled="true" Width="120px" Text='<%# Bind("FD_NROFACTURA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_DIAS" HeaderText="Nro Days" HeaderStyle-Width="40px" Visible="True"
                                                    Resizable="true" SortExpression="FD_DIAS" UniqueName="FD_DIAS">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="tx_dias_fac" runat="server" Enabled="true" Width="35px" DbValue='<%# Bind("FD_DIAS") %>' NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="BARRAS" HeaderText="Bar Code" HeaderStyle-Width="130px" Visible="True"
                                                    Resizable="true" SortExpression="BARRAS" UniqueName="BARRAS">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_barrasfac" runat="server" Enabled="true" Width="120px" Text='<%# Bind("BARRAS") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                    UniqueName="TANOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="FD_CLAVE1" HeaderText="Referencia" UniqueName="FD_CLAVE1_TK"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="FD_CLAVE1" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("FD_CLAVE1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="link" UniqueName="FD_CLAVE1" DataTextField="FD_CLAVE1"
                                                    HeaderText="Reference" HeaderStyle-Width="100px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="FD_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_REFPRO"
                                                    UniqueName="FD_REFPRO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                    UniqueName="CLAVE2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                    UniqueName="CLAVE3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="450px"
                                                    HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                                    UniqueName="NOMTTEC1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                                    UniqueName="NOMTTEC2">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                                    UniqueName="NOMTTEC3">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                                    UniqueName="NOMTTEC">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                                    UniqueName="NOMTTEC5">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                                    UniqueName="NOMTTEC7">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="FD_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                                    Resizable="true" SortExpression="FD_ORIGEN" UniqueName="FD_ORIGEN">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="rc_porigenfac" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("FD_ORIGEN") %>'
                                                            Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI" AllowCustomText="true" Filter="Contains"
                                                            Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_POSARA" HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                                    Resizable="true" SortExpression="FD_POSARA" UniqueName="FD_POSARA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_arancelfac" runat="server" Enabled="true" Width="100px" Text='<%# Bind("FD_POSARA") %>'>
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("FD_PAGO")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_PRECIO" HeaderText="Pre x Un" HeaderStyle-Width="120px" Visible="True"
                                                    Resizable="true" SortExpression="FD_PRECIO" UniqueName="FD_PRECIO" Aggregate="Sum" FooterText=" ">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_pre_fac" runat="server" Enabled="true" DbValue='<%# Bind("FD_PRECIO") %>' AutoPostBack="true" OnTextChanged="txt_pre_fac_TextChanged"
                                                            Value="0" Width="90%" Visible="true" NumberFormat-DecimalDigits="6">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="FD_CANTIDAD" HeaderText="Qty" HeaderStyle-Width="70px" Visible="True"
                                                    Resizable="true" SortExpression="FD_CANTIDAD" UniqueName="FD_CANTIDAD" Aggregate="Sum" FooterText=" ">
                                                    <ItemTemplate>
                                                        <telerik:RadNumericTextBox ID="txt_can_fac" runat="server" Enabled="true" DbValue='<%# Bind("FD_CANTIDAD") %>' AutoPostBack="true" OnTextChanged="txt_canfacedit_TextChanged"
                                                            Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                        </telerik:RadNumericTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="140px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                                    HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                                    UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="&lt;strong&gt;{0:#.##}&lt;/strong&gt;">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView5" runat="server" Height="100%">
                                     <telerik:RadGrid ID="rg_resumen" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" 
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_resumen_NeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />                                            
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ITM" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ITM"
                                                    UniqueName="ITM">                                                    
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="tipo" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="T Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tipo"
                                                    UniqueName="tipo">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CD_NROCMP" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Document" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROCMP"
                                                    UniqueName="CD_NROCMP">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="CD_ESTADO" HeaderText="Status" HeaderStyle-Width="150px" Visible="True"
                                                    Resizable="true" SortExpression="CD_ESTADO" UniqueName="CD_ESTADO">
                                                    <ItemTemplate>
                                                        <telerik:RadComboBox ID="rc_estadodoc" runat="server" ZIndex="1000000" SelectedValue='<%# Bind("CD_ESTADO") %>'
                                                            Culture="es-CO" Width="130px" Enabled="true" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                                <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView3" runat="server" Height="100%">
                                    <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_costos_OnItemCommand" 
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_NROITEM" Height="100%">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                            </CommandItemTemplate>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CT_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_CLAVE1"
                                                    UniqueName="CT_CLAVE1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                    HeaderText="Servicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                    HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO"
                                                    UniqueName="CT_PRECIO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No Records to Display!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="pv_bl" runat="server">
                                    <telerik:RadGrid ID="rg_bl" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnDetailTableDataBind="rg_bl_DetailTableDataBind"
                                        Culture="(Default)" ShowFooter="True" OnItemCommand="rg_bl_ItemCommand" OnNeedDataSource="rg_bl_NeedDataSource" GroupPanelPosition="Top">
                                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                                ResizeGridOnColumnResize="False"></Resizing>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                            <CommandItemTemplate>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_newitmin" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                            </CommandItemTemplate>
                                            <DetailTables>
                                                <telerik:GridTableView Name="detalle_item" Width="100%">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="BLD_NROCONTAINER" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                            HeaderText="Nro Container" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROCONTAINER"
                                                            UniqueName="BLD_NROCONTAINER">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLD_NROPACK" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                            HeaderText="Nro Pack" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROPACK"
                                                            UniqueName="BLD_NROPACK">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLD_DESCRIPTION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_DESCRIPTION"
                                                            UniqueName="BLD_DESCRIPTION">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BLD_GROSSWEIGHT" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                            HeaderText="Gross Weigth" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_GROSSWEIGHT"
                                                            UniqueName="BLD_GROSSWEIGHT">
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
                                                <telerik:GridBoundColumn DataField="BLC_CONSECUTIVO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLC_CONSECUTIVO"
                                                    UniqueName="BLC_CONSECUTIVO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                    HeaderText="Date" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_FECHA"
                                                    UniqueName="BLH_FECHA">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLH_NROBILLOFL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro BL" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_NROBILLOFL"
                                                    UniqueName="BLH_NROBILLOFL">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BLH_BOOKINGNO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Nro Booking" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLH_BOOKINGNO"
                                                    UniqueName="BLH_BOOKINGNO">
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
                                <telerik:RadPageView ID="pv_soportes" runat="server">
                                    <asp:Panel runat="server" ID="Panel1">
                                        <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                            OnItemCommand="rgSoportes_OnItemCommand" OnDeleteCommand="rgSoportes_DeleteCommand">
                                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="SP_CONSECUTIVO">
                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                                <CommandItemTemplate>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                </CommandItemTemplate>
                                                <Columns>
                                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                    <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                        SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                        HeaderText="">
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings EditFormType="Template">
                                                    <FormTemplate>
                                                        <div>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            Descripcion</label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadTextBox ID="edt_nombre" runat="server" Enabled="true" Width="350px">
                                                                        </telerik:RadTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <label>
                                                                            Archivo</label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadAsyncUpload ID="rauCargarSoporte" runat="server" ControlObjectsVisibility="None"
                                                                            Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                                            Width="350px" OnFileUploaded="rauCargarSoporte_FileUploaded"
                                                                            Style="margin-bottom: 0px">
                                                                            <Localization Select="Cargar Archivo" />
                                                                        </telerik:RadAsyncUpload>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                            ValidationGroup="grNuevo" OnClick="btn_aceptar_OnClick" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </FormTemplate>
                                                </EditFormSettings>
                                                <NoRecordsTemplate>
                                                    <div class="alert alert-danger">
                                                        <strong>¡No Records to Display!</strong>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView4" runat="server">
                                    <asp:Panel runat="server" ID="Panel3">
                                        <telerik:RadGrid ID="rg_seguimiento" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_seguimiento" OnItemCommand="rg_seguimiento_ItemCommand"
                                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_seguimiento_OnNeedDataSource">
                                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                                <CommandItemTemplate>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                </CommandItemTemplate>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="SG_CONSECUTIVO" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                        SortExpression="SG_CONSECUTIVO" UniqueName="SG_CONSECUTIVO" Visible="true">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SG_DESCRIPCION" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Description" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SG_DESCRIPCION" UniqueName="SG_DESCRIPCION">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="User" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="usua_nombres" UniqueName="usua_nombres">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SG_FECING" HeaderButtonType="TextButton"
                                                        HeaderStyle-Width="120px" HeaderText="Date" ItemStyle-HorizontalAlign="Right"
                                                        Resizable="true" SortExpression="SG_FECING" UniqueName="SG_FECING">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <NoRecordsTemplate>
                                                    <div class="alert alert-danger">
                                                        <strong>¡No Records to Display!</strong>
                                                    </div>
                                                </NoRecordsTemplate>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                    <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="1100px" Height="570px" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_agregar" runat="server" DefaultButton="btn_agregar">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_fecha" runat="server">Fecha</asp:Label></td>
                                    <td>
                                        <telerik:RadDatePicker ID="edt_fecha" runat="server" MinDate="01/01/1900" Enabled="true" ZIndex="1000000">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="val_fecha" runat="server" ControlToValidate="edt_fecha"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                            <asp:Image ID="Image37" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_nro" runat="server">Nro Dcmt</asp:Label></td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_dcmt" runat="server" Enabled="true" Visible="true">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="val_nrodoc" runat="server" ControlToValidate="edt_dcmt"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                            <asp:Image ID="Image38" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bar Code</td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_buscarbarras" runat="server" Text="" ToolTip="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="findbar" OnClick="btn_buscarbarras_Click" />
                                    </td>
                                    <td>
                                        <label>Brand</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_categoria" runat="server" ZIndex="1000000" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_categoria_SelectedIndexChanged"
                                            Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE" AutoPostBack="true"
                                            Enabled="true" DataValueField="TATIPPRO">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <telerik:RadTextBox ID="txt_item" runat="server" Enabled="true" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Visible="false">
                                    </telerik:RadTextBox>

                                    <td>
                                        <label>Reference</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Visible="true">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_findref" runat="server" Text="" ToolTip="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="findbar" OnClick="btn_findref_Click" />
                                        <asp:RequiredFieldValidator ID="rqf_referencia" runat="server" ControlToValidate="txt_referencia"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>
                                            Description</label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Visible="true" Width="450px">
                                        </telerik:RadTextBox>
                                        <%----%>
                                        <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Size</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dt1" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_clsec" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton8" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="rc_dt1"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Presentation</label></td>
                                    <td>
                                        <%--<telerik:RadTextBox ID="txt_dtecnico2" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC2") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                        <telerik:RadComboBox ID="rc_dt2" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton2" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton1" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rc_dt2"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image15" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Type</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dt3" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton3" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton9" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rc_dt3"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>T Product</label></td>
                                    <td>
                                        <%--<telerik:RadTextBox ID="txt_dtecnico4" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC4") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                        <telerik:RadComboBox ID="rc_dt4" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton4" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton10" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rc_dt4"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image17" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Gender</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dt5" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton5" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton11" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="rc_dt5"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image18" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtecnico6" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC6") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Fragance</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dt7" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton6" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton12" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rc_dt7"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                            <asp:Image ID="Image19" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 8</label></td>
                                    <td>
                                        <%--<telerik:RadTextBox ID="txt_dtecnico8" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC8") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                        <telerik:RadComboBox ID="rc_dt8" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" ZIndex="1000000"
                                            Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" MinFilterLength="3">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton7" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="insdt" OnClick="btn_clsec_Click" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton13" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" />
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="rc_dt8"
                                                ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                                <asp:Image ID="Image20" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Dato Tecnico 9</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtecnico9" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC9") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>

                                    <td>
                                        <label>Dato Tecnico 10</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtecnico10" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC10") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Origen Country</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_porigen" runat="server" ZIndex="1000000" AllowCustomText="true" Filter="Contains"
                                            Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                            Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>Un Arancelaria</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_posaracelaria" runat="server" Enabled="false" Text='<%# Bind("FD_POSARA") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Quantity</label>
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
                                    <td>
                                        <label>Price</label></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_precio" runat="server" Enabled="true">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_precio"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>R. Supplier</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_rproveedor" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>C Supplier</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_cproveedor" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>FOC</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_foc" runat="server"  Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Observations</label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true"
                                            Width="600px" TextMode="MultiLine" Height="40px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: right;">
                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje" EnableShadow="true">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFindArticulo" runat="server" Width="900px" Height="620px" Modal="true" OffsetElementID="main" Title="Filtro Articulo" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Reference</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_referencia" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Description</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nombreart" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />--%>
                                    <telerik:RadButton ID="btn_filtroArticulos" runat="server" OnClick="btn_filtroArticulos_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight" CommandName="xxxxxx" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel2" runat="server">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="5" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_articulos" OnItemCommand="rgConsultaArticulos_OnItemCommand">
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
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE2" HeaderText="" Visible="true"
                                            UniqueName="ARCLAVE2" HeaderButtonType="None" DataField="ARCLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="" Visible="true"
                                            UniqueName="CLAVE2" HeaderButtonType="None" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE3" HeaderText="" Visible="true"
                                            UniqueName="ARCLAVE3" HeaderButtonType="TextButton" DataField="ARCLAVE3" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="" Visible="true"
                                            UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE4" HeaderText="" Visible="true"
                                            UniqueName="ARCLAVE4" HeaderButtonType="None" DataField="ARCLAVE4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
                                            UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="400px">
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
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpCostos" runat="server" Width="900px" Height="330px" Modal="true" OffsetElementID="main" Title="Costos" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>

                                <telerik:RadTextBox ID="txt_tpct" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave2ct" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave3ct" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave4ct" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>

                                <td>
                                    <label>Reference</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referenciact" runat="server" Enabled="false" Visible="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_referenciact"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoCT">
                                        <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        Description</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_descripcionct" runat="server" Enabled="false" Visible="true" Width="350px">
                                    </telerik:RadTextBox>

                                    <asp:ImageButton ID="iBtnFindArticuloct" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" Enabled="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Currency</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Price</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_precioct" runat="server" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_precioct"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoct">
                                        <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Supplier</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" DataTextField="TRNOMBRE"
                                        Width="300px" DataSourceID="obj_terceros" ZIndex="1000000">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        </Items>

                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>T. Documento</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tdocumento" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        AppendDataBoundItems="true" ZIndex="1000000">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Facturacion" Value="01" />
                                            <telerik:RadComboBoxItem Text="Cuenta Cobro" Value="02" />
                                            <telerik:RadComboBoxItem Text="Remision" Value="03" />

                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>

                                <td>
                                    <label>Nro Documento</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="true" Width="300px" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>F. Documento</label></td>
                                <td>
                                    <telerik:RadDatePicker ID="txt_fdocumento" runat="server" ZIndex="1000000"
                                        MinDate="01/01/1900" Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right;">
                            <%--<asp:Button ID="btn_agregar" runat="server" Text="Agregar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />    --%>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarct" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregarct_Aceptar" ValidationGroup="grNuevoCT" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="190px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargar Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <asp:Label runat="server" ID="lbl_tipocargue" Text="Barras"></asp:Label>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4</asp:ListItem>
                                        <asp:ListItem>Cod. Barras</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Aceptar" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Literal runat="server" ID="ltr_cargue" Text=""></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalSeguimiento" runat="server" Width="700px" Height="540px" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Observations</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_obsseguimiento" runat="server" Enabled="true" Width="520px" TextMode="MultiLine" Height="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarseg" runat="server" Text="Save" Icon-PrimaryIconCssClass="rbAdd" ValidationGroup="grNuevoI" OnClick="btn_agregarseg_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFechaNro" runat="server" Width="780px" Height="190px" OffsetElementID="main" Title="Datos" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Date</label></td>
                                <td>
                                    <telerik:RadDatePicker ID="edt_datenew" runat="server" MinDate="01/01/1900" Enabled="true">
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="validatorxxx" runat="server" ControlToValidate="edt_datenew"
                                        ErrorMessage="(*)" ValidationGroup="gvDatosFecNro">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Nro Document</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrodocnew" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_nrodocnew"
                                        ErrorMessage="(*)" ValidationGroup="gvDatosFecNro">
                                        <asp:Image ID="Image20" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Nro Days</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_dias" runat="server" Enabled="true" Width="50px">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_dias"
                                        ErrorMessage="(*)" ValidationGroup="gvDatosFecNro">
                                        <asp:Image ID="Image36" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarfecnro" runat="server" Text="Update" Icon-PrimaryIconCssClass="rbSave" ValidationGroup="gvDatosFecNro" OnClick="btn_aceptarfecnro_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPaisUNAr" runat="server" Width="850px" Height="190px" OffsetElementID="main" Title="Datos" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Origin Country</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_newpais" runat="server" ZIndex="1000000"
                                        Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                        Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="rc_newpais" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvDatosAranPais">
                                        <asp:Image ID="Image21" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>UN Arancelaria</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_newarancel" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_newarancel"
                                        ErrorMessage="(*)" ValidationGroup="gvDatosAranPais">
                                        <asp:Image ID="Image22" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarPaisAran" runat="server" Text="Update" Icon-PrimaryIconCssClass="rbSave" ValidationGroup="gvDatosAranPais" OnClick="btn_aceptarPaisAran_Click" />
                                </td>
                            </tr>
                        </table>
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
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroTer" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Filtrar" CommandName="Cancel" OnClick="btn_filtroTer_OnClick" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server" Width="100%">
                            <telerik:RadGrid ID="rgConsultaTerceros" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_terceros_modal" OnItemCommand="rgConsultaTerceros_ItemCommand">
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
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRDIRECC" HeaderText="Direccion"
                                            UniqueName="TRDIRECC" HeaderButtonType="TextButton" DataField="TRDIRECC" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="180px">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpBillOfLading" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" ShowContentDuringLoad="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <table>
                                <tr>
                                    <td>
                                        <label>Bill Of Lading</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrobl" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nrobl"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Booking Number</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrobooking" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image23" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Export Reference</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_exportref" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_exportref"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Shipper-Exporter</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_exporter" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_shipper" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_shipper_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txt_exporter"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image24" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <label>Consignee</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_consignatario" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_consignatario" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_consignatario_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_consignatario"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Notify Party</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_notify" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_notify" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_notify_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_notify"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_datexport" runat="server" Enabled="true" Width="270px" TextMode="MultiLine" Height="120px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_datconsignatario" runat="server" Enabled="true" Width="270px" TextMode="MultiLine" Height="120px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_datnotify" runat="server" Enabled="true" Width="270px" TextMode="MultiLine" Height="120px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>M. Initial Carriage</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_minitialcarriage" runat="server" Culture="es-CO" DataSourceID="obj_mcarriage" DataTextField="TTDESCRI" ZIndex="1000000"
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="150px"
                                            Filter="Contains">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="rc_minitialcarriage" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image26" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Place of Receipt</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_lugarrecibe" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_lugarrecibe"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image27" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Precarrie by</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_transportado" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txt_transportado"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image28" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Vessel and Voyage</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nroviaje" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txt_nroviaje"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image29" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Lading</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptocarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txt_ptocarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image30" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Discharge</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptodescarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_ptodescarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image31" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Place of Delivery</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_destino" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txt_destino"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image32" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Domestic Routing</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image33" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Freight Payable AT</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image34" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Type Of Movement</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tipomov" runat="server" Culture="es-CO" DataSourceID="obj_tipmov" DataTextField="TTDESCRI" ZIndex="1000000"
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="150px" Filter="Contains">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="rc_tipomov" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image35" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Date</label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txt_fechaBL" runat="server" MinDate="01/01/1900" ZIndex="1000000">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_fechaBL"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel4" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_container" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="false" GroupPanelPosition="Top"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_container_ItemCommand" OnNeedDataSource="rg_container_NeedDataSource">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="false" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_newitctn" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                        <telerik:GridBoundColumn DataField="BLD_NROCONTAINER" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro Container" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROCONTAINER"
                                            UniqueName="BLD_NROCONTAINER">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BLD_NROPACK" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro Pack" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_NROPACK"
                                            UniqueName="BLD_NROPACK">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BLD_DESCRIPTION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_DESCRIPTION"
                                            UniqueName="BLD_DESCRIPTION">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BLD_GROSSWEIGHT" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="Gross Weigth" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BLD_GROSSWEIGHT"
                                            UniqueName="BLD_GROSSWEIGHT">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>Nro Container</label></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_nrocontainer" runat="server" Width="300px" Enabled="true">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nrocontainer"
                                                            ErrorMessage="(*)" ValidationGroup="gvContainer">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <label>Nro Pack</label></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_nropack" runat="server" Width="300px" Enabled="true">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_nropack"
                                                            ErrorMessage="(*)" ValidationGroup="gvContainer">
                                                            <asp:Image ID="Image22" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Observaciones</label>
                                                    </td>
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txt_obscontainer" runat="server" Width="600px" Enabled="true" TextMode="MultiLine" Height="70px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Gross Weigth</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txt_gross" runat="server" Enabled="true">
                                                        </telerik:RadNumericTextBox>
                                                        <telerik:RadComboBox ID="rc_unidadgross" runat="server" ZIndex="1000000"
                                                            Culture="es-CO" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                            Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <label>Measurements</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadNumericTextBox ID="txt_measurement" runat="server" Enabled="true">
                                                        </telerik:RadNumericTextBox>
                                                        <telerik:RadComboBox ID="rc_unidadmeasurement" runat="server" ZIndex="1000000"
                                                            Culture="es-CO" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                            Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadButton ID="btn_aceptarcontainer" runat="server" Text="" CommandName="PerformInsert" ValidationGroup="gvContainer" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbOk" />
                                                        <telerik:RadButton ID="btn_cancelarcontainer" runat="server" Text="" CommandName="Cancel" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbCancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" Width="100%">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarctn" runat="server" Text="Anexar" Icon-PrimaryIconCssClass="rbSave" ValidationGroup="gvBill" OnClick="btn_agregarctn_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <telerik:RadNotification RenderMode="Lightweight" ID="RadNotification1" runat="server" VisibleOnPageLoad="false" Position="TopRight"
            Width="330" Height="120" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
            Title="Informacion" Text="RadNotification is a lightweight control which can be used to display a notification message"
            Style="z-index: 100000">
        </telerik:RadNotification>
    </telerik:RadAjaxPanel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_bodegas" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_bodegas_ItemClick" Skin="Bootstrap">
        <Items>
            <telerik:RadMenuItem Text="Cargar Orden Compra">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Cargar Factura Proforma">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Cargar Factura">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Cargar Nro Doc-Fecha">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Cargar Pais-UN Aran">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>
    <asp:ObjectDataSource ID="obj_compras" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertCompras" OnInserted="obj_compras_OnInserted" OnInserting="obj_compras_OnInserting"
        SelectMethod="GetComprasHD" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" OnUpdating="obj_compras_OnUpdating" UpdateMethod="UpdateCompras">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="CH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="CH_BODEGA" Type="String" />
            <asp:Parameter Name="CH_PROVEEDOR" Type="Int32" />
            <asp:Parameter Name="CH_TIPORD" Type="Int32" />
            <asp:Parameter Name="CH_FECORD" Type="DateTime" />
            <asp:Parameter Name="CH_TIPCMP" Type="String" />
            <asp:Parameter Name="CH_TIPDPH" Type="String" />
            <asp:Parameter Name="CH_TERPAG" Type="String" />
            <asp:Parameter Name="CH_NROMUESTRA" Type="String" />
            <asp:Parameter Name="CH_SERVICIO" Type="String" />
            <asp:Parameter Name="CH_VLRTOT" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="CH_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="CH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="CH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="CH_ORDENOR" Type="Int32" />
            <asp:Parameter Name="CH_FENTREGA" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="CH_GENINV" Type="String" />
            <asp:Parameter Name="CH_CMPINT" Type="String" />
            <asp:Parameter Name="CH_MONEDA" Type="String" />
            <asp:Parameter Name="CH_CNROCMPALT" Type="String" />
            <asp:Parameter Name="tbDetalle" Type="Object" />
            <asp:Parameter Name="tbProforma" Type="Object" />
            <asp:Parameter Name="tbFactura" Type="Object" />
            <asp:Parameter Name="tbCostos" Type="Object" />
            <asp:Parameter Name="tbImagenes" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="CH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="CH_NROCMP" Type="Int32" />
            <asp:Parameter Name="CH_BODEGA" Type="String" />
            <asp:Parameter Name="CH_PROVEEDOR" Type="Int32" />
            <asp:Parameter Name="CH_TIPORD" Type="Int32" />
            <asp:Parameter Name="CH_FECORD" Type="DateTime" />
            <asp:Parameter Name="CH_TIPCMP" Type="String" />
            <asp:Parameter Name="CH_TIPDPH" Type="String" />
            <asp:Parameter Name="CH_TERPAG" Type="String" />
            <asp:Parameter Name="CH_NROMUESTRA" Type="String" />
            <asp:Parameter Name="CH_SERVICIO" Type="String" />
            <asp:Parameter Name="CH_VLRTOT" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="CH_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="CH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="CH_ESTADO" Type="String" />
            <asp:Parameter Name="CH_ORDENOR" Type="Int32" />
            <asp:Parameter Name="CH_FENTREGA" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="CH_GENINV" Type="String" />
            <asp:Parameter Name="CH_CMPINT" Type="String" />
            <asp:Parameter Name="CH_MONEDA" Type="String" />
            <asp:Parameter Name="CH_CNROCMPALT" Type="String" />
            <asp:Parameter Name="tbDetalle" Type="Object" />
            <asp:Parameter Name="tbProforma" Type="Object" />
            <asp:Parameter Name="tbFactura" Type="Object" />
            <asp:Parameter Name="tbCostos" Type="Object" />
            <asp:Parameter Name="tbImagenes" Type="Object" />
            <asp:Parameter Name="tbBL" Type="Object" />
            <asp:Parameter Name="tbBLDT" Type="Object" />
            <asp:Parameter Name="tbSummari" Type="Object" />            
        </UpdateParameters>
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
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terpago" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerminosPago" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="InCodemp" Type="String" SessionField="CODEMP" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_porigen" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAIS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_aranceles" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAranceles" TypeName="XUSS.BLL.Articulos.ArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_mcarriage" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TITRAN" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipmov" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TOFMV" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terceros_modal" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_unidad" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UNIT" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
