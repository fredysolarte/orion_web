<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="RecepcionCompras.aspx.cs" Inherits="XUSS.WEB.Compras.RecepcionCompras" %>

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
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_compras$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_compras" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_compras_OnItemInserted"
            OnItemCommand="rlv_compras_OnItemCommand" OnItemDataBound="rlv_compras_OnItemDataBound"
            DataSourceID="obj_recibo" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Recepcion Ordenes de Compra</h5>
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
                                <h5>Recepcion Ordenes de Compras</h5>
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
                                        Nro Orden</label>
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
                                        T. Orden</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_torden" runat="server"
                                        Width="300px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            <telerik:RadComboBoxItem Text="Orden Compra" Value="1" />
                                            <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                            <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        B. Ingreso</label>
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
                                    <label>Nro Recibo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrorecibo" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Orden</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Eval("CH_NROCMP") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Orden</label></td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <%--<td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("CH_BODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>--%>
                            <td>
                                <label>Proveedor</label></td>
                            <td>
                                <%--<telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" SelectedValue='<%# Bind("CH_PROVEEDOR") %>' DataTextField="TRNOMBRE"
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>

                                </telerik:RadComboBox>--%>
                            </td>
                        </tr>
                        <tr>
                           <%-- <td>
                                <label>T. Orden</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_torden" runat="server" SelectedValue='<%# Bind("CH_TIPORD") %>' Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Orden de Compra" Value="1" />
                                        <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>--%>
                            <td>
                                <label>T. Despacho</label></td>
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
                        </tr>
                        <tr>
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
                                <label>Moneda</label>
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
                                <label>Precio</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_precio" runat="server" DbValue='<%# Eval("PRECIO") %>' Enabled="false"></telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("CH_ESTADO") %>' Enabled="false"
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
                                <label>Observaciones</label></td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("CH_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Recibo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrorecibo" runat="server" Enabled="false" Text='<%# Eval("RH_NRORECIBO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Tip Doc</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipdocrec" runat="server" SelectedValue='<%# Eval("RH_TIPDOC") %>' Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Factura" Value="01" />
                                        <telerik:RadComboBoxItem Text="Cuenta Cobro" Value="02" />
                                        <telerik:RadComboBoxItem Text="Remision" Value="03" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Doc Rec</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_docrec" runat="server" Enabled="false" Text='<%# Eval("RH_NRODOC") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                        </ClientSettings>
                        <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                    UniqueName="CD_NROITEM">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                    UniqueName="TANOMBRE">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CD_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CLAVE1"
                                    UniqueName="CD_CLAVE1">
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
                                <telerik:GridBoundColumn DataField="CD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                    HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CANTIDAD"
                                    UniqueName="CD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                    HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_PRECIO"
                                    UniqueName="CD_PRECIO">
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
                                    <strong>¡No se Encontaron Registros!</strong>
                                </div>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Orden</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="true" Text='<%# Bind("CH_NROCMP") %>' Width="300px" AutoPostBack="true" OnTextChanged="txt_nroorden_OnTextChanged">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Orden</label></td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rqf_identificacion" runat="server" ControlToValidate="edt_forden"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
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
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>

                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_proveedor" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
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
                        </tr>
                        <tr>
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
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
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
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            <Selecting AllowRowSelect="True"></Selecting>
                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                ResizeGridOnColumnResize="False"></Resizing>
                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                        </ClientSettings>
                        <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                    UniqueName="CD_NROITEM">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="CD_NROITEM" HeaderText="C Rec" HeaderStyle-Width="40px"
                                    Resizable="true" SortExpression="CD_NROITEM" UniqueName="CD_TIPPRO" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_item" runat="server" Enabled="true" Text='<%# Bind("CD_NROITEM") %>'
                                            MinValue="0" Value="0" Width="60px" Visible="false">
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                    UniqueName="TANOMBRE">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn DataField="CD_TIPPRO" HeaderText="C Rec" HeaderStyle-Width="40px"
                                    Resizable="true" SortExpression="CD_TIPPRO" UniqueName="CD_TIPPRO" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Text='<%# Bind("CD_TIPPRO") %>'
                                            MinValue="0" Value="0" Width="60px" Visible="false">
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="CD_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CLAVE1"
                                    UniqueName="CD_CLAVE1">
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
                                <telerik:GridBoundColumn DataField="CD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                    HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CANTIDAD"
                                    UniqueName="CD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>                                
                                <telerik:GridTemplateColumn DataField="RD_CANTIDAD" HeaderText="C Rec" HeaderStyle-Width="40px"
                                    Resizable="true" SortExpression="RD_CANTIDAD" UniqueName="PDDESCUE">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="true" DbValue='<%# Bind("RD_CANTIDAD") %>' OnTextChanged="txt_valor_OnTextChanged"
                                            MinValue="0" Value="0" Width="60px" AutoPostBack="true">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="CD_CANTIDAD" HeaderText="C Rec" HeaderStyle-Width="40px" Visible="false"
                                    Resizable="true" SortExpression="CD_CANTIDAD" UniqueName="CD_CANTIDAD">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txt_cancom" runat="server" Enabled="true" DbValue='<%# Bind("CD_CANTIDAD") %>'
                                            MinValue="0" Value="0" Width="60px" Visible="false">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Dif" HeaderStyle-Width="40px" Resizable="true">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txt_dif" runat="server" Enabled="false"
                                            MinValue="0" Value="0" Width="60px">
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
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Orden</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Bind("CH_NROCMP") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Orden</label></td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("CH_FECORD") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rqf_identificacion" runat="server" ControlToValidate="edt_forden"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
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
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>

                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_proveedor" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
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
                        </tr>
                        <tr>
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
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
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
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_items_OnItemCommand" OnNeedDataSource="rg_items_OnNeedDataSource" OnDeleteCommand="rg_items_OnDeleteCommand">
                        <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            <Selecting AllowRowSelect="True"></Selecting>
                            <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                ResizeGridOnColumnResize="False"></Resizing>
                        </ClientSettings>
                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CD_NROITEM">
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                            <Columns>
                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                    UniqueName="CD_NROITEM">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                    UniqueName="TANOMBRE">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CD_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CLAVE1"
                                    UniqueName="CD_CLAVE1">
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
                                <telerik:GridBoundColumn DataField="CD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                    HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CANTIDAD"
                                    UniqueName="CD_CANTIDAD" FooterText="Total: " Aggregate="Sum">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                    HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_PRECIO"
                                    UniqueName="CD_PRECIO">
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
                                    <strong>¡No se Encontaron Registros!</strong>
                                </div>
                            </NoRecordsTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
                <table>
                    <tr>
                        <td>
                            <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="330px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Cod Barras</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo" AutoPostBack="true" OnTextChanged="txt_barras_OnTextChanged">
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
                                    <%----%>
                                    <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" Enabled="true" />
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
                                <td>
                                    <label>Precio</label></td>
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
                                    <label>R. Proveedor</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_rproveedor" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>C Proveedor</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_cproveedor" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Observaciones</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true"
                                        Width="600px" TextMode="MultiLine" Height="40px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right;">
                            <%--<asp:Button ID="btn_agregar" runat="server" Text="Agregar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />    --%>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />
                        </div>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFindArticulo" runat="server" Width="900px" Height="620px" Modal="true" OffsetElementID="main" Title="Filtro Articulo" EnableShadow="true">
                    <ContentTemplate>
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
                                    <%--<asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" ToolTip="Filtrar" />
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
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpDtLotes" runat="server" Width="900px" Height="220px" Modal="true" OffsetElementID="main" Title="Datos Lote" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txt_iditem" runat="server" Enabled="true" Width="150px" Visible="false">
                                    </telerik:RadTextBox>
                                    <label>
                                        DT 1</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dt1" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_dt1"
                                        ErrorMessage="(*)" ValidationGroup="gvInsert1">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        DT 2</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dt2" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dt2"
                                        ErrorMessage="(*)" ValidationGroup="gvInsert1">
                                        <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_aceptarlot" runat="server" Text="Aceptar" ValidationGroup="gvInsert1" OnClick="btn_aceptarlot_OnClick"
                                        CommandName="xxxxxx" />
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
    <asp:ObjectDataSource ID="obj_recibo" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertRecibos" OnInserted="obj_compras_OnInserted" OnInserting="obj_compras_OnInserting"
        SelectMethod="GetReciboHD" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" OnUpdating="obj_compras_OnUpdating" UpdateMethod="UpdateCompras">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
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
            <asp:Parameter Name="tbDetalle" Type="Object" />
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
            <asp:Parameter Name="tbDetalle" Type="Object" />
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
</asp:Content>
