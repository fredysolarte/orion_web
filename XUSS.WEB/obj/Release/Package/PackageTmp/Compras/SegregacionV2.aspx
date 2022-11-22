<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SegregacionV2.aspx.cs" Inherits="XUSS.WEB.Compras.SegregacionV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsDownload" runat="server">
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
            function RowContextMenu(sender, eventArgs) {
                //debugger;
                var menu = $find("<%=ct_marcas.ClientID %>");
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = "1-" + index;

                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }

            function conditionalPostback(sender, args) {
                if (args.EventTarget.indexOf("rg_proforma") != -1) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }

            function changevalue(sender, Args) {
                debugger;
                var master_view = $telerik.findControl(document, "rg_proforma").get_masterTableView();
                var rows = master_view.get_dataItems();
                //var mycolums = master_view.get_columns();

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    try {
                        if (row.findControl(sender.get_id()).get_id() == sender.get_id()) {
                            if (parseFloat(row.findControl("txt_prcantidad").get_value()) < parseFloat(sender.get_value()) ) {
                                sender.set_value(0);
                                alert("Cantidad Invalida");
                            } else {
                                row.findControl("txt_cantidad_dif").set_value(parseFloat(row.findControl("txt_prcantidad").get_value()) - parseFloat(sender.get_value()) );
                            }
                        }
                    }
                    catch (e) {
                        //alert(e);
                    }
                }
            }

            function changevalueFacOri(sender, Args) {
                //debugger;
                var master_view = $telerik.findControl(document, "rg_facturas").get_masterTableView();
                var rows = master_view.get_dataItems();
                //var mycolums = master_view.get_columns();

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    try {
                        if (row.findControl(sender.get_id()).get_id() == sender.get_id()) {
                            if (parseFloat(row.findControl("txt_prcantidad").get_value()) < parseFloat(sender.get_value()) + parseFloat(row.findControl("txt_cantidad_des").get_value())) {
                                sender.set_value(0);
                                alert("Cantidad Invalida");
                            } else {
                                row.findControl("txt_cantidad_dif").set_value(parseFloat(row.findControl("txt_prcantidad").get_value()) - (parseFloat(sender.get_value()) + parseFloat(row.findControl("txt_cantidad_des").get_value())));
                            }
                        }
                    }
                    catch (e) {
                        //alert(e);
                    }
                }
            }

            function changevalueFacDes(sender, Args) {
                //debugger;
                var master_view = $telerik.findControl(document, "rg_facturas").get_masterTableView();
                var rows = master_view.get_dataItems();
                //var mycolums = master_view.get_columns();

                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    try {
                        if (row.findControl(sender.get_id()).get_id() == sender.get_id()) {
                            if (parseFloat(row.findControl("txt_prcantidad").get_value()) < parseFloat(sender.get_value()) + parseFloat(row.findControl("txt_cantidad_cmp").get_value())) {
                                sender.set_value(0);
                                alert("Cantidad Invalida");
                            } else {
                                row.findControl("txt_cantidad_dif").set_value(parseFloat(row.findControl("txt_prcantidad").get_value()) - (parseFloat(sender.get_value()) + parseFloat(row.findControl("txt_cantidad_cmp").get_value())));
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="100000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_segregacion" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_segregacion_ItemInserted" OnItemUpdating="rlv_segregacion_ItemUpdating"
            OnItemCommand="rlv_segregacion_ItemCommand" OnItemDataBound="rlv_segregacion_ItemDataBound" OnItemInserting="rlv_segregacion_ItemInserting" 
            DataSourceID="obj_segregacion" ItemPlaceholderID="pnlGeneral" DataKeyNames="SGH_CODIGO"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Segregacion</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_segregacion" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Segregacion</h5>
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
                                        Nro Segregacion</label>
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
                                        Nro Proforma</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroproforma" runat="server"
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
                                    <telerik:RadTextBox ID="txt_factura" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Documento</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrodocumento" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" Text="Search" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight" OnClick="btn_filtro_Click">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" OnClick="btn_editar_Click" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aprobar" runat="server" Text="Aprobar" Icon-PrimaryIconCssClass="rbConfig" CommandName="Edit" ToolTip="Aprobar Segregacion" OnClick="btn_aprobar_Click" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_proforma" runat="server" Text="G. Proforma" Icon-PrimaryIconCssClass="rbUpload" CommandName="Edit" ToolTip="Generar Proforma" OnClick="btn_proforma_Click" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Segregacion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("SGH_CODIGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Documento</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecproforma" runat="server" DbSelectedDate='<%# Bind("SG_FECPROFORMA") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Nro Documento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_proforma" runat="server" Enabled="false" Text='<%# Bind("SG_NROPROFORMA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Vendedor</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_codvendedor" runat="server" Enabled="false" Text='<%# Bind("SG_VENDEDORPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomvendedor" runat="server" Enabled="false" Text='<%# Eval("VENDEDOR") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Comprador</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_codcomprador" runat="server" Enabled="false" Text='<%# Bind("SG_COMPRADORPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomcomprador" runat="server" Enabled="false" Text='<%# Eval("COMPRADOR") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bod Can 1</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega_can" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODCAN") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Bob Can 2</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega_dif" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODDIF") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("SGH_OBSERVACIONES") %>' Width="300px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Origen</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_origen" runat="server" Enabled="false" SelectedValue='<%# Bind("SGH_TIPO") %>' Width="300px" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Proforma" Value="PR" />
                                        <telerik:RadComboBoxItem Text="Factura" Value="FA" />
                                    </Items>

                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F. Proformas</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_proformas" runat="server" Enabled="false" Width="300px">
                                </telerik:RadTextBox>

                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" SelectedValue='<%# Bind("SGH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                    Width="300px" DataSourceID="obj_terceros" AppendDataBoundItems="true" Filter="StartsWith">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Segregacion Parcial</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_segParcial" runat="server" Enabled="false" Checked='<%# this.GetEstado(Eval("SGH_PARCIAL")) %>' />
                            </td>
                            <td>
                                <label>Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("SGH_ESTADO") %>'
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Aprobado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Detalle/Segregacion/Proforma">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Detalle/Segregacion/Facturas">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_proformas" runat="server">
                            <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_proforma_ItemCommand"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_proforma_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>                       
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_CANTIDAD"
                                            UniqueName="PR_CANTIDAD" FooterText="Total: " Aggregate="Sum" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_PRECIO" HeaderText="Price" UniqueName="SGD_PRECIO"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_PRECIO" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio_pro" runat="server" Enabled="false" DbValue='<%# Eval("SGD_PRECIO") %>' Width="90%" NumberFormat-DecimalDigits="2">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Seg" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="false" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDADAPRO" HeaderText="Dif" UniqueName="SGD_CANTIDADAPRO"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDADAPRO" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmpapro" runat="server" Enabled="false" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>' Width="90%" NumberFormat-DecimalDigits="0">
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
                        <telerik:RadPageView ID="pv_facturas" runat="server">
                            <telerik:RadGrid ID="rg_facturas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_facturas_ItemCommand" OnItemDataBound="rg_facturas_ItemDataBound"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_facturas_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        
                                    </CommandItemTemplate>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROCMP" HeaderText="Referencia" UniqueName="PR_NROCMP_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROCMP" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrocmp" runat="server" Text='<%# Eval("PR_NROCMP") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="40px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" Width="30px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDADCMP" HeaderText="Cant Factura" UniqueName="SGD_CANTIDADCMP"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDADCMP" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_prcantidad" runat="server" Enabled="false" DbValue='<%# Eval("SGD_CANTIDADCMP") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Can 1" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="false" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalue">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Can 2" UniqueName="SGD_CANTIDAD_DES"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_des" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>'>
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Dif" UniqueName="SGD_CANTIDAD_DIF"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_dif" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" DbValue='<%# Eval("DIFERENCIA") %>'>
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
                            <telerik:RadGrid ID="rg_ordenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_ordenes_ItemCommand"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <%--<telerik:GridBoundColumn DataField="TipDoc" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="T Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TipDoc"
                                            UniqueName="TipDoc">                                            
                                        </telerik:GridBoundColumn>--%>                                        
                                        <telerik:GridTemplateColumn DataField="TipDoc" HeaderText="T Documento" UniqueName="TipDoc"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TipDoc" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_tdocumento" runat="server" Text='<%# Eval("TipDoc") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="NroProforma" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("NroProforma") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="NroProforma" DataTextField="NroProforma"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>

                                        <%--<telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA">
                                        </telerik:GridBoundColumn>--%>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
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
                            <label>Segregacion Ant</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_seganterios" runat="server" Enabled="true" AutoPostBack="true" OnTextChanged="txt_seganterios_TextChanged" Width="300px">
                                </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <tr>
                            <td>
                                <label>Vendedor</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_codvendedor" runat="server" Enabled="false" Text='<%# Bind("SG_VENDEDORPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomvendedor" runat="server" Enabled="false" Text='<%# Eval("VENDEDOR") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:ImageButton ID="ibtn_vendedor" runat="server" CommandName="findVendedor" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="ibtn_vendedor_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Comprador</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_codcomprador" runat="server" Enabled="false" Text='<%# Bind("SG_COMPRADORPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomcomprador" runat="server" Enabled="false" Text='<%# Eval("COMPRADOR") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:ImageButton ID="ibtn_comprador" runat="server" CommandName="findComprador" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="ibtn_comprador_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("SGH_OBSERVACIONES") %>' Width="450px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </tr>
                    <tr>
                        <td>
                            <label>Bod Can 1</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega_can" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODCAN") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>Bob Can 2</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega_dif" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODDIF") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Origen</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_origen" runat="server" Enabled="true" SelectedValue='<%# Bind("SGH_TIPO") %>' Width="300px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    <telerik:RadComboBoxItem Text="Proforma" Value="PR" />
                                    <telerik:RadComboBoxItem Text="Factura" Value="FA" />
                                </Items>

                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Factura/Proformas</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_proformas" runat="server" Enabled="false" Width="300px">
                            </telerik:RadTextBox>

                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" SelectedValue='<%# Bind("SGH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                Width="300px" DataSourceID="obj_terceros" AppendDataBoundItems="true" Filter="StartsWith" AutoPostBack="true" OnSelectedIndexChanged="rc_proveedor_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Segregacion Parcial</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chk_segParcial" runat="server" Enabled="true" Checked='<%# this.GetEstado(Eval("SGH_PARCIAL")) %>' />
                        </td>
                        <td>
                            <label>Estado</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("SGH_ESTADO") %>'
                                Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                    <telerik:RadComboBoxItem Text="Aprobado" Value="CE" />
                                    <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <telerik:RadButton ID="btn_calcular" runat="server" OnClick="btn_calcular_Click" Text="Procesar" Icon-PrimaryIconCssClass="rbRefresh" RenderMode="Lightweight">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Parametros">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Detalle/Segregacion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_parametros" runat="server">
                            <telerik:RadGrid ID="rg_bodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_bodegas_NeedDataSource"
                                Culture="(Default)" CellSpacing="0" Height="800px">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Cod Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                                            UniqueName="BDNOMBRE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ctipo" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="true" DataSourceID="obj_tipo" DataTextField="TTDESCRI" SelectedValue='<%# Bind("Tipo") %>'
                                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="N/A" Value="-1" />
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
                        <telerik:RadPageView ID="pv_proformas" runat="server">
                            <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_proforma_ItemCommand" OnItemDataBound="rg_proforma_ItemDataBound"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_proforma_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="120px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CANTIDAD" HeaderText="Cant Factura" UniqueName="PR_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="PR_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_prcantidad" runat="server" Enabled="false" DbValue='<%# Eval("PR_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_PRECIO" HeaderText="Price" UniqueName="SGD_PRECIO"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_PRECIO" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio_pro" runat="server" Enabled="true" DbValue='<%# Eval("SGD_PRECIO") %>' Width="90%" NumberFormat-DecimalDigits="2">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Cant" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalue">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="Dif" UniqueName="SGD_CANTIDAD_DIF"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_dif" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0">
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
                        <telerik:RadPageView ID="pv_facturas" runat="server">
                            <telerik:RadGrid ID="rg_facturas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_facturas_ItemCommand" OnItemDataBound="rg_facturas_ItemDataBound"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_facturas_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_Cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbOpen" CommandName="rbOpen" ToolTip="Cargar Plano" ValidationGroup="gvInsert" />
                                    </CommandItemTemplate>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROCMP" HeaderText="Referencia" UniqueName="PR_NROCMP_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROCMP" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrocmp" runat="server" Text='<%# Eval("PR_NROCMP") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="40px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" Width="30px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CANTIDAD" HeaderText="Cant Factura" UniqueName="SGD_CANTIDADCMP"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="PR_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_prcantidad" runat="server" Enabled="false" DbValue='<%# Eval("PR_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Seg" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalue">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Dif" UniqueName="SGD_CANTIDAD_DIF"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_dif" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>'>
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Can 2" UniqueName="SGD_CANTIDAD_DES" 
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_des" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" >
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
                            <telerik:RadGrid ID="rg_ordenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NroProforma" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Nro Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NroProforma"
                                            UniqueName="NroProforma">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Cantidad" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="Cantidad"
                                            UniqueName="Cantidad" FooterText="Total: " Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA">
                                        </telerik:GridBoundColumn>--%>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
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
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Save" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancel" RenderMode="Lightweight" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>F Documento</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecproforma" runat="server" DbSelectedDate='<%# Bind("SG_FECPROFORMA") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>Nro Documento</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_proforma" runat="server" Enabled="true" Text='<%# Bind("SG_NROPROFORMA") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Vendedor</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_codvendedor" runat="server" Enabled="false" Text='<%# Bind("SG_VENDEDORPRO") %>' Width="300px">
                            </telerik:RadTextBox>
                            <telerik:RadTextBox ID="txt_nomvendedor" runat="server" Enabled="false" Text='<%# Eval("VENDEDOR") %>' Width="300px">
                            </telerik:RadTextBox>
                            <asp:ImageButton ID="ibtn_vendedor" runat="server" CommandName="findVendedor" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="ibtn_vendedor_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Comprador</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_codcomprador" runat="server" Enabled="false" Text='<%# Bind("SG_COMPRADORPRO") %>' Width="300px">
                            </telerik:RadTextBox>
                            <telerik:RadTextBox ID="txt_nomcomprador" runat="server" Enabled="false" Text='<%# Eval("COMPRADOR") %>' Width="300px">
                            </telerik:RadTextBox>
                            <asp:ImageButton ID="ibtn_comprador" runat="server" CommandName="findComprador" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="ibtn_comprador_Click" />
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <label>Observaciones</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("SGH_OBSERVACIONES") %>' Width="300px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Bod Can1</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega_can" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODCAN") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>Bod Can 2</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega_dif" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("SGH_BODDIF") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Proveedor</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" SelectedValue='<%# Eval("SGH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                Width="300px" DataSourceID="obj_terceros" AppendDataBoundItems="true" Filter="StartsWith" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label>Segregacion Parcial</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_segParcial" runat="server" Enabled="false" Checked='<%# this.GetEstado(Eval("SGH_PARCIAL")) %>' />
                            </td>
                        <td>
                            <label>Estado</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("SGH_ESTADO") %>'
                                Enabled="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                    <telerik:RadComboBoxItem Text="Aprobado" Value="CE" />
                                    <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>F. Proformas</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_proformas" runat="server" Enabled="true" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <telerik:RadButton ID="btn_calcular" runat="server" OnClick="btn_calcular_Click" Text="Procesar" Icon-PrimaryIconCssClass="rbRefresh" RenderMode="Lightweight">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Parametros">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Detalle/Segregacion/Proforma">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Detalle/Segregacion/Facturas">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Documentos">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_parametros" runat="server">
                            <telerik:RadGrid ID="rg_bodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_bodegas_NeedDataSource"
                                Culture="(Default)" CellSpacing="0" Height="800px">
                                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"></Resizing>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Cod Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                            UniqueName="BDBODEGA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                                            UniqueName="BDNOMBRE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ctipo" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="true" DataSourceID="obj_tipo" DataTextField="TTDESCRI" SelectedValue='<%# Bind("Tipo") %>'
                                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="N/A" Value="-1" />
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
                        <telerik:RadPageView ID="pv_proformas" runat="server">
                            <telerik:RadGrid ID="rg_proforma" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_proforma_ItemCommand" OnItemDataBound="rg_proforma_ItemDataBound"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_proforma_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_Cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbOpen" CommandName="rbOpen" ToolTip="Cargar Plano" ValidationGroup="gvInsert" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_facturar" runat="server" Text="Trasladar Factura" Icon-PrimaryIconCssClass="rbUpload" CommandName="rbTrasladar" ToolTip="Cargar Plano" ValidationGroup="gvInsert" />
                                    </CommandItemTemplate>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROCMP" HeaderText="Referencia" UniqueName="PR_NROCMP_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROCMP" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrocmp" runat="server" Text='<%# Eval("PR_NROCMP") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="40px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" Width="30px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CANTIDAD" HeaderText="Cant Factura" UniqueName="PR_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="PR_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_prcantidad" runat="server" Enabled="false" DbValue='<%# Eval("PR_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Seg" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalue">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Dif" UniqueName="SGD_CANTIDAD_DIF"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_dif" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>'>
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%-- <telerik:GridTemplateColumn DataField="SGD_CANTIDADAPRO" HeaderText="Aprobada" UniqueName="SGD_CANTIDADAPRO"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDADAPRO" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_facturas" runat="server">
                            <telerik:RadGrid ID="rg_facturas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnItemCommand="rg_facturas_ItemCommand" OnItemDataBound="rg_facturas_ItemDataBound"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" Height="800px" OnNeedDataSource="rg_facturas_NeedDataSource">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowMultiColumnSorting="true">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                                    <CommandItemTemplate>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_Cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbOpen" CommandName="rbOpen" ToolTip="Cargar Plano" ValidationGroup="gvInsert" />
                                    </CommandItemTemplate>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="gp_detalle" HeaderText="Detalle" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="1" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="Transito" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="Stock Margarita" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="Segregaciones" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PR_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_NROITEM"
                                            UniqueName="PR_NROITEM" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROFACPROFORMA" HeaderText="Referencia" UniqueName="PR_NROFACPROFORMA_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROFACPROFORMA" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_proforma" runat="server" Text='<%# Eval("PR_NROFACPROFORMA") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PR_NROCMP" HeaderText="Referencia" UniqueName="PR_NROCMP_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_NROCMP" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_nrocmp" runat="server" Text='<%# Eval("PR_NROCMP") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link_documento" UniqueName="PR_NROFACPROFORMA" DataTextField="PR_NROFACPROFORMA"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                            HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                            UniqueName="BARRAS" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_CLAVE1" HeaderText="Referencia" UniqueName="PR_CLAVE1_TK"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PR_CLAVE1" Visible="false" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("PR_CLAVE1") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="PR_CLAVE1" DataTextField="PR_CLAVE1"
                                            HeaderText="Reference" HeaderStyle-Width="100px" ColumnGroupName="gp_detalle">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="PR_REFPRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Origin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_REFPRO"
                                            UniqueName="PR_REFPRO" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1" ColumnGroupName="gp_detalle">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="PR_ORIGEN" HeaderText="Origin Country" HeaderStyle-Width="320px" Visible="True"
                                            Resizable="true" SortExpression="PR_ORIGEN" UniqueName="PR_ORIGEN" ColumnGroupName="gp_detalle">
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
                                        <telerik:GridTemplateColumn HeaderText="FOC" UniqueName="cpago" HeaderStyle-Width="40px" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_pago" runat="server" Checked='<%# this.GetEstado(Eval("PR_PAGO")) %>' Enabled="false" Width="30px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PR_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataFormatString="&lt;strong&gt;{0:#.######}&lt;/strong&gt;"
                                            HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_PRECIO"
                                            UniqueName="PR_PRECIO" ColumnGroupName="gp_detalle">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDADCMP" HeaderText="Cant Factura" UniqueName="SGD_CANTIDADCMP"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDADCMP" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_prcantidad" runat="server" Enabled="false" DbValue='<%# Eval("SGD_CANTIDADCMP") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="SGD_CANTIDAD" HeaderText="Can 1" UniqueName="SGD_CANTIDAD"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDAD" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDAD") %>' Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalueFacOri">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Can 2" UniqueName="SGD_CANTIDAD_DES"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_des" runat="server" Enabled="true" Width="90%" NumberFormat-DecimalDigits="0" ClientEvents-OnValueChanged="changevalueFacDes" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>'>
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Dif" UniqueName="SGD_CANTIDAD_DIF"
                                            HeaderStyle-Width="70px" AllowFiltering="false" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_dif" runat="server" Enabled="false" Width="90%" NumberFormat-DecimalDigits="0" DbValue='<%# Eval("DIFERENCIA") %>'>
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%-- <telerik:GridTemplateColumn DataField="SGD_CANTIDADAPRO" HeaderText="Aprobada" UniqueName="SGD_CANTIDADAPRO"
                                            HeaderStyle-Width="70px" AllowFiltering="false" SortExpression="SGD_CANTIDADAPRO" Visible="true" ColumnGroupName="gp_detalle">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_cantidad_cmp" runat="server" Enabled="true" DbValue='<%# Eval("SGD_CANTIDADAPRO") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
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
                            <telerik:RadGrid ID="rg_ordenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NroProforma" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Nro Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NroProforma"
                                            UniqueName="NroProforma">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="PR_FECPROFORMA" HeaderButtonType="TextButton" HeaderStyle-Width="130px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="D. Proforma" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PR_FECPROFORMA"
                                            UniqueName="PR_FECPROFORMA">
                                        </telerik:GridBoundColumn>--%>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
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
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Save" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancel" RenderMode="Lightweight" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalAsistente" runat="server" Width="600px" Height="150px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Asistente" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Porcentaje</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_porcentaje" runat="server" Enabled="true" NumberFormat-DecimalDigits="2">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnaplicar" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Save" RenderMode="Lightweight" OnClick="btnaplicar_Click" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalImprimir" runat="server" Width="400px" Height="180px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Reportes" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Reporte</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_reporte" runat="server" ZIndex="1000000"
                                        Enabled="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Reporte Agrupado" Value="9015" />
                                            <telerik:RadComboBoxItem Text="Reporte Lineal" Value="9016" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_selrepor" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbPrint" ToolTip="Save" RenderMode="Lightweight" OnClick="btn_selrepor_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalProformas" runat="server" Width="830px" Height="520px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Facturas/Proformas">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Se Encuentra Segregacion Anteriores</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_segant" runat="server" Enabled="true" />
                                </td>
                                <td>
                                    <label>Segregacion Parcial</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_segpar" runat="server" Enabled="true" />
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_proformasfacturas" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Filtrar" CommandName="Cancel" OnClick="btn_proformasfacturas_Click" />
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarproformas" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarproformas_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_proformaspendientes" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="false" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_pendientes">
                                <MasterTableView DataKeyNames="PR_NROFACPROFORMA">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_chk" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PR_NROFACPROFORMA" HeaderText="Nro Proforma"
                                            UniqueName="PR_NROFACPROFORMA" HeaderButtonType="TextButton" DataField="PR_NROFACPROFORMA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="150px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PR_FECPROFORMA" HeaderText="F. Proforma" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="PR_FECPROFORMA" HeaderButtonType="TextButton" DataField="PR_FECPROFORMA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Segregacion?" UniqueName="SEGRE_EST"
                                            HeaderStyle-Width="80px" AllowFiltering="false" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_segregacion" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="false" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFacturas" runat="server" Width="830px" Height="520px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Facturas">
                    <ContentTemplate>
                        <table>       
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarf" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarf_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_facturast" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="false" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_pendientes">
                                <MasterTableView DataKeyNames="PR_NROFACPROFORMA">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_chk" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PR_NROFACPROFORMA" HeaderText="Nro Proforma"
                                            UniqueName="PR_NROFACPROFORMA" HeaderButtonType="TextButton" DataField="PR_NROFACPROFORMA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="150px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PR_FECPROFORMA" HeaderText="F. Proforma" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="PR_FECPROFORMA" HeaderButtonType="TextButton" DataField="PR_FECPROFORMA" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Segregacion?" UniqueName="SEGRE_EST"
                                            HeaderStyle-Width="80px" AllowFiltering="false" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_segregacion" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="false" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargue Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal" >
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4 + QTY Ori + QTY Des</asp:ListItem>
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
            </Windows>
        </telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_marcas" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_marcas_ItemClick" Skin="Bootstrap">
        <Items>
            <telerik:RadMenuItem Text="Asistente">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>

    <asp:ObjectDataSource ID="obj_segregacion" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_segregacion_Inserting" OnInserted="obj_segregacion_Inserted"
        SelectMethod="GetSegregacionHD" TypeName="XUSS.BLL.Compras.SegregacionBL" InsertMethod="InsertSegregacion" UpdateMethod="UpdateSegregacion" OnUpdating="obj_segregacion_Updating">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="HDCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="HDTIPFAC" Type="String" />
            <asp:Parameter Name="HDNROFAC" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="SGH_NROFAC" Type="String" />
            <asp:Parameter Name="SG_FECPROFORMA" Type="DateTime" />
            <asp:Parameter Name="SG_NROPROFORMA" Type="String" />
            <asp:Parameter Name="SG_VENDEDORPRO" Type="Int32" />
            <asp:Parameter Name="SG_COMPRADORPRO" Type="Int32" />
            <asp:Parameter Name="SGH_TIPO" Type="String" />
            <asp:Parameter Name="SGH_PROVEEDOR" Type="Int32" />
            <asp:Parameter Name="SGH_OBSERVACIONES" Type="String" />
            <asp:Parameter Name="SGH_BODCAN" Type="String" />
            <asp:Parameter Name="SGH_BODDIF" Type="String" />
            <asp:Parameter Name="SGH_PARCIAL" Type="String" />
            <asp:Parameter Name="SGH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="SGH_USUARIO" SessionField="UserLogon" />
            <asp:Parameter Name="inDt" Type="Object" />
            <asp:Parameter Name="tbBodega" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="HDCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="HDTIPFAC" Type="String" />
            <asp:Parameter Name="HDNROFAC" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="SGH_PARCIAL" Type="String" />
            <asp:Parameter Name="SGH_NROFAC" Type="String" />
            <asp:Parameter Name="SGH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="SG_FECPROFORMA" Type="DateTime" />
            <asp:Parameter Name="SG_NROPROFORMA" Type="String" />
            <asp:Parameter Name="SG_VENDEDORPRO" Type="Int32" />
            <asp:Parameter Name="SG_COMPRADORPRO" Type="Int32" />
            <asp:Parameter Name="SGH_OBSERVACIONES" Type="String" />            
            
            <asp:Parameter Name="SGH_BODCAN" Type="String" />
            <asp:Parameter Name="SGH_BODDIF" Type="String" />

            <asp:SessionParameter Name="SGH_USUARIO" SessionField="UserLogon" />
            <asp:Parameter Name="inDt" Type="Object" />
            <asp:Parameter Name="inDtf" Type="Object" />
            <asp:Parameter Name="tbBodega" Type="Object" />
            <asp:Parameter Name="original_SGH_CODIGO" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_porigen" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAIS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="BODDT1" />
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
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_pendientes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProformasProveedor" TypeName="XUSS.BLL.Compras.SegregacionBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="0" Name="CH_PROVEEDOR" Type="Int32" />
            <asp:Parameter DefaultValue="1=1" Name="inFilter" Type="String" />
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
</asp:Content>
