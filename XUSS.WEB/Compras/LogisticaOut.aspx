<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LogisticaOut.aspx.cs" Inherits="XUSS.WEB.Compras.LogisticaOut" %>

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
                //debugger;
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_pedidos$ctrl0$btn_descargar")) {
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
            function ClickingFac(sender, args) {
                if (!confirm("¿Esta Seguro de Generar Factura?"))
                    args.set_cancel(!confirmed);
            }
            function onChangeCheck(sender) {
                debugger;
                var master_view = $telerik.findControl(document, "rg_comprasdt").get_masterTableView();
                var rows = master_view.get_dataItems();
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    try {
                        if (row.findElement(sender.id).id == sender.id) {
                            if (sender.checked) {
                                row.findControl("txt_cancom").set_value(parseFloat(row.findControl("txt_canrestante").get_value()));
                                row.findControl("txt_dif").set_value(0);
                            }
                            else {
                                row.findControl("txt_dif").set_value(parseFloat(row.findControl("txt_canrestante").get_value()));
                                row.findControl("txt_cancom").set_value(0);
                            }
                        }
                    }
                    catch (e) {
                        //alert(e);
                    }
                }
            }

            function changevalue(sender, Args) {
                //debugger;
                var master_view = $telerik.findControl(document, "rg_comprasdt").get_masterTableView();
                var rows = master_view.get_dataItems();
                //var mycolums = master_view.get_columns();

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    try {
                        if (row.findControl(sender.get_id()).get_id() == sender.get_id()) {
                            if (parseFloat(row.findControl("txt_dif").get_value()) < parseFloat(sender.get_value())) {
                                sender.set_value(0);
                                //row.findElement("chk_estado").click();
                                alert("Cantidad Invalida");
                            }
                            else {
                                //row.findElement("chk_estado").click();
                                row.findElement("chk_estado").checked = true;
                                row.findControl("txt_dif").set_value(parseFloat(row.findControl("txt_canrestante").get_value()) - parseFloat(sender.get_value()));
                            }
                        }
                    }
                    catch (e) {
                        //alert(e);
                    }
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_logistica" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_logistica_ItemInserted" OnItemInserting="rlv_logistica_ItemInserting"
            OnItemCommand="rlv_logistica_OnItemCommand" OnItemDataBound="rlv_logistica_OnItemDataBound"
            DataSourceID="obj_logistica" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Salida Embarque</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_logistica" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Salida Embarque</h5>
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
                                    <label>Nro Embarque Out</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_embarque" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>Nro Embarque In</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_embarqueIn" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Container</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_container" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Nro BL</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_bl" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Factura</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofactura" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Nro Segregacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_segregacion" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Estado</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_estado" runat="server"
                                        Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                            <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                            <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_factura" runat="server" Text="Factura" Icon-PrimaryIconCssClass="rbCart" CommandName="Cancel" ToolTip="Generar Factura" OnClick="btn_factura_Click" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Reporte" Icon-PrimaryIconCssClass="rbPrint" CommandName="Print" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro WR</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrowr" runat="server" Width="300px" Enabled="false" Text='<%# Bind("WOH_CONSECUTIVO") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Bodega Out</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bod In 1</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega1" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA1") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains" Enabled="false"
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Bod In 2</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega2" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA2") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains" Enabled="false"
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cod Ter</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false"
                                    Text='<%# Bind("TRCODTER") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Eval("NOM_TER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F. Salida</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechasal" runat="server" DbSelectedDate='<%# Bind("WOH_FECHASAL") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>F. Entrada</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechaent" runat="server" DbSelectedDate='<%# Bind("WOH_FECHAENT") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Factura</label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lkn_factura" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("lkn") %>' OnClick="lkn_factura_Click" />
                            </td>
                        </tr>
                        <%-- <td>
                                <label>Nro Traslado</label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnk_traslado" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Bind("TSNROTRA") %>' OnClick="lnk_traslado_Click" />
                            </td>--%>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("WOH_OBSERVACIONES") %>'
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
                            <telerik:RadTab Text="Detail">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Bill Of Lading">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Segragate">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Traslados">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Files">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="pv_detalle" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                                Culture="(Default)" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="WID_ID">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="WOD_ID" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_ID"
                                            UniqueName="WOD_ID">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="WIH_CONSECUTIVO" HeaderText="Factura" UniqueName="WIH_CONSECUTIVO_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="WIH_CONSECUTIVO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_wrin" runat="server" Text='<%# Eval("WIH_CONSECUTIVO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_wrin" UniqueName="WIH_CONSECUTIVO" DataTextField="WIH_CONSECUTIVO"
                                            HeaderText="WR IN" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn DataField="WID_NROFACTURA" HeaderText="Factura" UniqueName="WID_NROFACTURA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="WIH_CONSECUTIVO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrofactura" runat="server" Text='<%# Eval("WID_NROFACTURA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_invoice" UniqueName="WID_NROFACTURA" DataTextField="WID_NROFACTURA"
                                            HeaderText="Nro Invoice" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE1" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="ARCLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("ARCLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="ARCLAVE1" DataTextField="ARCLAVE1"
                                            HeaderText="Referencia" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
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
                                        <telerik:GridBoundColumn DataField="WOD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Vlr UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_PRECIO"
                                            UniqueName="WOD_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WOD_PRECIOVTA" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Vlr Vta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_PRECIOVTA"
                                            UniqueName="WOD_PRECIOVTA">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WOD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_CANTIDAD"
                                            UniqueName="WOD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
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
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
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
                        <telerik:RadPageView ID="pv_segregacion" runat="server">
                            <telerik:RadGrid ID="rg_segregacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnItemCommand="rg_segregacion_ItemCommand"
                                Culture="(Default)" ShowFooter="True" OnNeedDataSource="rg_segregacion_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="SGH_CODIGO" HeaderText="Factura" UniqueName="SGH_CODIGO_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="SGH_CODIGO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_segregacion" runat="server" Text='<%# Eval("SGH_CODIGO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_segregacion" UniqueName="SGH_CODIGO" DataTextField="SGH_CODIGO"
                                            HeaderText="Nro Segregacion" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Seg" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OTBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Dif" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTBODEGA"
                                            UniqueName="OTBODEGA">
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
                        <telerik:RadPageView ID="pv_traslados" runat="server">
                            <telerik:RadGrid ID="rg_traslados" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnItemCommand="rg_traslados_ItemCommand"
                                Culture="(Default)" ShowFooter="True" OnNeedDataSource="rg_traslados_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="TSNROTRA" HeaderText="Nro Traslado" UniqueName="TSNROTRA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TSNROTRA" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_traslado" runat="server" Text='<%# Eval("TSNROTRA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_traslado" UniqueName="TSNROTRA" DataTextField="TSNROTRA"
                                            HeaderText="Traslado" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Out" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OTBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega In" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTBODEGA"
                                            UniqueName="OTBODEGA">
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
                        <telerik:RadPageView ID="pv_files" runat="server">
                            <asp:Panel runat="server" ID="Panel1">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
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
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro WR</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrowr" runat="server" Width="300px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Bodega Out</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_bodega" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cod Ter</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false"
                                    Text='<%# Bind("TRCODTER") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_codter" ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Eval("NOM_TER") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="xxxx" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bod In 1</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega1" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA1") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains"
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="rc_bodega1" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Bod In 2</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega2" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA2") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains"
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="rc_bodega2" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image23" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F. Salida</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechasal" runat="server" DbSelectedDate='<%# Bind("WOH_FECHASAL") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_fechasal"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>F. Entrada</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechaent" runat="server" DbSelectedDate='<%# Bind("WOH_FECHAENT") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_fechaent"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("WOH_OBSERVACIONES") %>'
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
                            <telerik:RadTab Text="Detail">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Bill Of Lading">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Segregate">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Files">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="pv_detalle" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnDeleteCommand="rg_items_DeleteCommand"
                                Culture="(Default)" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="WID_ID">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_newitmin" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_newfile" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbOpen" CommandName="Load" ToolTip="Cargar Plano" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_newseg" runat="server" Text="Cargar Segregacion" Icon-PrimaryIconCssClass="rbOpen" CommandName="LoadS" ToolTip="Cargar Segregacion" />
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="40px" />
                                        <telerik:GridBoundColumn DataField="WOD_ID" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_ID"
                                            UniqueName="WOD_ID">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="WIH_CONSECUTIVO" HeaderText="Factura" UniqueName="WIH_CONSECUTIVO_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="WIH_CONSECUTIVO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_wrin" runat="server" Text='<%# Eval("WIH_CONSECUTIVO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_wrin" UniqueName="WIH_CONSECUTIVO" DataTextField="WIH_CONSECUTIVO"
                                            HeaderText="WR IN" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridTemplateColumn DataField="WID_NROFACTURA" HeaderText="Factura" UniqueName="WID_NROFACTURA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="WIH_CONSECUTIVO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrofactura" runat="server" Text='<%# Eval("WID_NROFACTURA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_invoice" UniqueName="WID_NROFACTURA" DataTextField="WID_NROFACTURA"
                                            HeaderText="Nro Invoice" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE1" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="ARCLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("ARCLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="ARCLAVE1" DataTextField="ARCLAVE1"
                                            HeaderText="Referencia" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
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
                                        <telerik:GridBoundColumn DataField="WOD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Vlr UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_PRECIO"
                                            UniqueName="WOD_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="WOD_PRECIOVTA" HeaderText="Vlr Vta" UniqueName="WOD_PRECIOVTA"
                                            HeaderStyle-Width="90px" AllowFiltering="false" SortExpression="WOD_PRECIOVTA" Visible="true">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio_vta" runat="server" Enabled="true" DbValue='<%# Eval("WOD_PRECIOVTA") %>' AutoPostBack="false" Width="90%">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="WOD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_CANTIDAD"
                                            UniqueName="WOD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
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
                        <telerik:RadPageView ID="pv_segregacion" runat="server">
                            <telerik:RadGrid ID="rg_segregacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnItemCommand="rg_segregacion_ItemCommand"
                                Culture="(Default)" ShowFooter="True" OnNeedDataSource="rg_segregacion_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <%--<telerik:GridBoundColumn DataField="SGH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro Segregacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SGH_CODIGO"
                                            UniqueName="SGH_CODIGO">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="SGH_CODIGO" HeaderText="Factura" UniqueName="SGH_CODIGO_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="SGH_CODIGO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_segregacion" runat="server" Text='<%# Eval("SGH_CODIGO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_segregacion" UniqueName="SGH_CODIGO" DataTextField="SGH_CODIGO"
                                            HeaderText="Nro Segregacion" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Seg" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OTBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Dif" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTBODEGA"
                                            UniqueName="OTBODEGA">
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
                        <telerik:RadPageView ID="pv_files" runat="server">
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
                                                                <telerik:RadButton ID="btn_aceptar_sop" runat="server" Text="" CommandName="PerformInsert" Icon-PrimaryIconCssClass="rbSave"
                                                                    ValidationGroup="grNuevo" OnClick="btn_aceptar_sop_OnClick" RenderMode="Lightweight" />
                                                            </td>
                                                            <td>
                                                                <telerik:RadButton ID="btn_cancelar" runat="server" Text="" CommandName="Cancel" Icon-PrimaryIconCssClass="rbCancel" RenderMode="Lightweight" />
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
                    </telerik:RadMultiPage>

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
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro WR</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrowr" runat="server" Width="300px" Enabled="false" Text='<%# Bind("WOH_CONSECUTIVO") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Bodega</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("BDBODEGA") %>'
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cod Ter</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Eval("NOM_TER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F. Salida</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechasal" runat="server" DbSelectedDate='<%# Bind("WOH_FECHASAL") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>F. Entrada</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fechaent" runat="server" DbSelectedDate='<%# Bind("WOH_FECHAENT") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("WOH_OBSERVACIONES") %>'
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
                            <telerik:RadTab Text="Detail">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Bill Of Lading">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Segragate">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Traslados">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Traslados Rest">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Files">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%">
                        <telerik:RadPageView ID="pv_detalle" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnDeleteCommand="rg_items_DeleteCommand"
                                Culture="(Default)" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="WID_ID">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                        <telerik:GridBoundColumn DataField="WOD_ID" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_ID"
                                            UniqueName="WOD_ID">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WIH_CONSECUTIVO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="WR IN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WIH_CONSECUTIVO"
                                            UniqueName="WIH_CONSECUTIVO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE1" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="ARCLAVE1" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("ARCLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="ARCLAVE1" DataTextField="ARCLAVE1"
                                            HeaderText="Referencia" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
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
                                        <telerik:GridBoundColumn DataField="WOD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Vlr UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_PRECIO"
                                            UniqueName="WOD_PRECIO">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="WOD_PRECIOVTA" HeaderText="Vlr Vta" UniqueName="WOD_PRECIOVTA"
                                            HeaderStyle-Width="90px" AllowFiltering="false" SortExpression="WOD_PRECIOVTA" Visible="true">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio_vta" runat="server" Enabled="true" DbValue='<%# Eval("WOD_PRECIOVTA") %>' AutoPostBack="false" Width="90%">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="WOD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WOD_CANTIDAD"
                                            UniqueName="WOD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
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
                        <telerik:RadPageView ID="pv_segregacion" runat="server">
                            <telerik:RadGrid ID="rg_segregacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                                Culture="(Default)" ShowFooter="True" OnNeedDataSource="rg_segregacion_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="SGH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Nro Segregacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SGH_CODIGO"
                                            UniqueName="SGH_CODIGO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Seg" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OTBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Bodega Dif" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTBODEGA"
                                            UniqueName="OTBODEGA">
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
                        <telerik:RadPageView ID="pv_traslados" runat="server">
                            <telerik:RadGrid ID="rg_traslados" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnItemCommand="rg_traslados_ItemCommand"
                                Culture="(Default)" ShowFooter="True" OnNeedDataSource="rg_traslados_NeedDataSource" GroupPanelPosition="Top">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                        ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="TSNROTRA" HeaderText="Factura" UniqueName="TSNROTRA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TSNROTRA" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_traslado" runat="server" Text='<%# Eval("TSNROTRA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_traslado" UniqueName="TSNROTRA" DataTextField="TSNROTRA"
                                            HeaderText="Traslados" HeaderStyle-Width="100px">
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
                        <telerik:RadPageView ID="pv_files" runat="server">
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
                                                                <telerik:RadButton ID="btn_aceptar_sop" runat="server" Text="" CommandName="PerformInsert" Icon-PrimaryIconCssClass="rbSave"
                                                                    ValidationGroup="grNuevo" OnClick="btn_aceptar_sop_OnClick" RenderMode="Lightweight" />
                                                            </td>
                                                            <td>
                                                                <telerik:RadButton ID="btn_cancelar" runat="server" Text="" CommandName="Cancel" Icon-PrimaryIconCssClass="rbCancel" RenderMode="Lightweight" />
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
                    </telerik:RadMultiPage>

                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpCompras" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" ShowContentDuringLoad="true">
                    <ContentTemplate>
                        <asp:Panel ID="pnEncabezado" runat="server" Width="100%">
                            <table>
                                <tr>
                                    <td>
                                        <label>WR IN</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_orden" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Nro Container</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_fnrocontainer" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="btn_filtrocmp" runat="server" Text="Filtrar" CommandName="xxxxxx" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight" OnClick="btn_filtrocmp_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnDetalleCmp" runat="server" Width="100%" Height="90%">
                            <telerik:RadGrid ID="rg_comprasdt" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_comprasdt_ItemCommand">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToCsvButton="false" ShowExportToExcelButton="false"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_marcar" runat="server" Text="Seleccionar Todos" Icon-PrimaryIconCssClass="rbOk" CommandName="seleccionar" ToolTip="Seleccionar Todos" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_desmarcar" runat="server" Text="Anular Seleccion" Icon-PrimaryIconCssClass="rbCancel" CommandName="anular" ToolTip="Anular Seleccion" />
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="cestado" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_estado" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' onclick="onChangeCheck(this)" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="citem" HeaderStyle-Width="20px" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_item" runat="server" Enabled="true" Text='<%# Bind("WID_ID") %>' Width="60px" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="WIH_CONSECUTIVO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="WR IN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WIH_CONSECUTIVO"
                                            UniqueName="WIH_CONSECUTIVO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Marca" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARTIPPRO" HeaderText="Marca" HeaderStyle-Width="40px"
                                            Resizable="true" SortExpression="ARTIPPRO" UniqueName="ARTIPPRO" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Text='<%# Bind("ARTIPPRO") %>'
                                                    MinValue="0" Value="0" Width="60px" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                            UniqueName="ARCLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="WID_PRECIOVTA" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="WID_PRECIOVTA"
                                            UniqueName="WID_PRECIOVTA">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="CAN_RESTANTE" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="C Res" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CAN_RESTANTE"
                                            UniqueName="CAN_RESTANTE" Aggregate="Sum">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="CAN_RESTANTE" HeaderText="C Rec" HeaderStyle-Width="60px" Visible="true"
                                            Resizable="true" SortExpression="CAN_RESTANTE" UniqueName="CAN_SOL">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_canrestante" runat="server" Enabled="false" DbValue='<%# Bind("CAN_RESTANTE") %>'
                                                    MinValue="0" Value="0" Width="60px" Visible="true">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="CAN_SOL" HeaderText="C Rec" HeaderStyle-Width="40px" Visible="true"
                                            Resizable="true" SortExpression="CAN_SOL" UniqueName="CAN_SOL">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cancom" runat="server" Enabled="true" DbValue='<%# Bind("CAN_SOL") %>' ClientEvents-OnValueChanged="changevalue"
                                                    MinValue="0" Value="0" Width="60px" Visible="true">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Dif" HeaderStyle-Width="40px" Resizable="true">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_dif" runat="server" Enabled="false" DbValue='<%# Bind("DIF") %>'
                                                    MinValue="0" Value="0" Width="60px">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                            <strong>Alerta!</strong>No Tiene Registros
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarct" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" ValidationGroup="grNuevoCT" OnClick="btn_agregarct_Click" />
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
                                DataSourceID="obj_terceros" OnItemCommand="rgConsultaTerceros_ItemCommand">
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
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Export Reference</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_exportref" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_exportref"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_exporter"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <label>Consignee</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_consignatario" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_consignatario" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_consignatario_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_consignatario"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_notify"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="rc_minitialcarriage" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image21" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Place of Receipt</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_lugarrecibe" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_lugarrecibe"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Precarrie by</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_transportado" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_transportado"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Vessel and Voyage</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nroviaje" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_nroviaje"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image15" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Lading</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptocarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_ptocarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Discharge</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptodescarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_ptodescarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image17" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Place of Delivery</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_destino" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_destino"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image18" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Domestic Routing</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image19" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Freight Payable AT</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image20" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tipomov" InitialValue="Seleccionar"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFactura" runat="server" Width="400px" Height="220px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Generar Factura">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Tipo Factura</td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE"
                                        DataValueField="TFTIPFAC">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_genfac" runat="server" Text="Generar" ToolTip="Generar" Icon-PrimaryIconCssClass="rbSave" CommandName="xxxxx" OnClick="btn_genfac_Click" OnClientClicked="ClickingFac" />
                                </td>
                            </tr>
                        </table>
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
                                        <asp:ListItem Selected="True">WR IN + Referencia + C2 + C3 + C4</asp:ListItem>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpSegregaciones" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Segregacion">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarf" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarf_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel4" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_facturast" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="false" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_segregacion">
                                <MasterTableView DataKeyNames="SGH_CODIGO">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_chk" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="SGH_CODIGO" HeaderText="Nro Segregacion"
                                            UniqueName="SGH_CODIGO" HeaderButtonType="TextButton" DataField="SGH_CODIGO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="150px">
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_logistica" runat="server" OldValuesParameterFormatString="original_{0}" OnInserted="obj_logistica_Inserted" UpdateMethod="UpdateWROUT" OnUpdating="obj_logistica_Updating"
        SelectMethod="GetWROUT" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" InsertMethod="InsertWROUT" OnInserting="obj_logistica_Inserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="WOH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="WOH_FECHASAL" Type="DateTime" />
            <asp:Parameter Name="WOH_FECHAENT" Type="DateTime" />
            <asp:Parameter Name="BDBODEGA" Type="String" />
            <asp:Parameter Name="BDBODEGA1" Type="String" />
            <asp:Parameter Name="BDBODEGA2" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="WOH_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="WOH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="WOH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="tbItems" Type="Object" />
            <asp:Parameter Name="tbBL" Type="Object" />
            <asp:Parameter Name="tbBLDT" Type="Object" />
            <asp:Parameter Name="tbSoportes" Type="Object" />
            <asp:Parameter Name="tbSegregacion" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="WOH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="WOH_CONSECUTIVO" Type="Int32" />
            <asp:Parameter Name="WOH_FECHASAL" Type="DateTime" />
            <asp:Parameter Name="WOH_FECHAENT" Type="DateTime" />
            <asp:Parameter Name="BDBODEGA" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="WOH_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="WOH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="WOH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="tbBL" Type="Object" />
            <asp:Parameter Name="tbBLDT" Type="Object" />
            <asp:Parameter Name="tbSoportes" Type="Object" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_segregacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSegregacionHD" TypeName="XUSS.BLL.Compras.SegregacionBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="SGH_CODIGO NOT IN (SELECT SGH_CODIGO FROM TB_SEGREGACION_WROUT WITH(NOLOCK))" Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_unidad" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UNIT" />
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
    <asp:ObjectDataSource ID="obj_mcarriage" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TITRAN" />
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
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (11)" Name="filter" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
