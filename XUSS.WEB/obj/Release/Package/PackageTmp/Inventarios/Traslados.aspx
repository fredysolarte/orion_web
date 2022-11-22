<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Traslados.aspx.cs" Inherits="XUSS.WEB.Inventarios.Traslados" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<script src="../Scripts/jquery-1.9.1.js" type="text/javascript" ></script>--%>
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
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_traslados$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
            function txt_traslado_ClientEvents_OnValueChanged(sender, eventArgs) {
                //debugger;
                if (eventArgs.get_newValue() != "") {
                    var eItem = $find('<%= rlv_traslados.ClientID %>');
                    ValidatorEnable($telerik.findElement(eItem.get_element().parentNode, "rqf_bingreso"), false);
                }
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

            function onClickingSegregate(sender, args) {
                if (!confirm("¿Desea Agregar todos los Items, de lo contrario solo se agrega la referencia vs segregacion?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="5000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_traslados" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_traslados_ItemInserted"
            OnItemCommand="rlv_traslados_OnItemCommand" OnItemDataBound="rlv_traslados_OnItemDataBound" OnItemUpdating="rlv_traslados_OnItemUpdating"
            DataSourceID="obj_traslados" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" OnPreRender="rlv_traslados_PreRender">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Traslados Inventarios</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_traslados" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Traslados Inventarios</h5>
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
                                        Nro Traslado</label>
                                </td>
                                <td>
                                    <%--OnTextChanged="txt_traslado_OnTextChanged"--%>
                                    <telerik:RadTextBox ID="txt_traslado" runat="server" ClientEvents-OnValueChanged="txt_traslado_ClientEvents_OnValueChanged"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        B. Salida</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                        DataSourceID="obj_bodegaxusuarioDef" DataTextField="BDNOMBRE" AppendDataBoundItems="true"
                                        DataValueField="BDBODEGA">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_bodegas" ValidationGroup="gvBuscar"
                                        ErrorMessage="(*)" InitialValue="Seleccionar">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    <label>
                                        B. Ingreso</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px"
                                        DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains"
                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rqf_bingreso" runat="server" ControlToValidate="rc_otbodega" ValidationGroup="gvBuscar"
                                        ErrorMessage="(*)" InitialValue="Seleccionar">
                                        <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
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
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                            <telerik:RadComboBoxItem Text="Todos" Value="." />
                                            <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                            <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                            <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rqf_estado" runat="server" ControlToValidate="rc_estado" ValidationGroup="gvBuscar"
                                        ErrorMessage="(*)" InitialValue="Seleccionar">
                                        <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" ValidationGroup="gvBuscar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar/Recibir" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Recibir Traslado" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimirtirilla" runat="server" Text="Imprimir Tirilla" OnClick="btn_imprimirtirilla_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir Tirilla" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Traslado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("TSNROTRA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F. Traslado</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_ftraslado" runat="server" DbSelectedDate='<%# Bind("TSFECTRA") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>B. Salida</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>B. Ingreso</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSOTBODE") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("TSESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Usu. Creacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrcreacion" runat="server" Enabled="false" Text='<%# Bind("TSNMUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fec. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fconfirma" runat="server" DbSelectedDate='<%# Bind("TSFECCNF") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Usu. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrconfirma" runat="server" Enabled="false" Text='<%# Bind("TSCFUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>WR IN</label></td>
                            <td>
                                <asp:LinkButton ID="lkn_wrin" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("WIH_CONSECUTIVO") %>' OnClick="lkn_wrin_Click" />
                            </td>
                            <td>
                                <label>WR OUT</label></td>
                            <td>
                                <asp:LinkButton ID="lkn_wrout" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("WOH_CONSECUTIVO") %>' OnClick="lkn_wrout_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cliente</label>
                            </td>
                            <td>
                                <asp:LinkButton ID="lkn_tercero" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("TRCODTER") %>' OnClick="lkn_tercero_Click" />
                            </td>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="lkn_cliente" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("CLIENTE") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("TSCOMENT") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txt_movsal" runat="server" Enabled="false" Text='<%# Bind("TSMOVSAL") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_movent" runat="server" Enabled="false" Text='<%# Bind("TSMOVENT") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Items">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Bill Of Lading">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Segregate">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Costos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_items" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_ItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnDetailTableDataBind="rg_items_OnDetailTableDataBind">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="MBIDMOVI,MBIDITEM">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="LLAVE">
                                                <DetailTables>
                                                    <telerik:GridTableView Name="detalle_item_elemento" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="MECDELEM" HeaderText="Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                                                DataField="MECDELEM">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="MECANTID" HeaderText="Can Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                                                DataField="MECANTID">
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
                                            <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                                UniqueName="MBIDITEM">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                                UniqueName="MBCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridTemplateColumn DataField="MBCLAVE1" HeaderText="Referencia" UniqueName="MBCLAVE1_TK"
                                                HeaderStyle-Width="100px" AllowFiltering="false" SortExpression="MBCLAVE1" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("MBCLAVE1") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="link" UniqueName="MBCLAVE1" DataTextField="MBCLAVE1"
                                                HeaderText="Referencia" HeaderStyle-Width="100px">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
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
                                            <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                                UniqueName="MBCANTID" FooterText="Total: " Aggregate="Sum">
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
                            <telerik:RadPageView ID="pv_costos" runat="server">
                                <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" DataKeyNames="CT_NROITEM">
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
                                            <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="{0:C}"
                                                HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO" FooterText="Total: " Aggregate="Sum"
                                                UniqueName="CT_PRECIO">
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
                            <telerik:RadPageView ID="pv_soportes" runat="server">
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
                                <label>Nro Traslado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Eval("TSNROTRA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F. Traslado</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_ftraslado" runat="server" DbSelectedDate='<%# Bind("TSFECTRA") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_ftraslado" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                          
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>B. Salida</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains" OnSelectedIndexChanged="rc_bodegas_SelectedIndexChanged" AutoPostBack="true"
                                    Enabled="true" DataSourceID="obj_bodegaxusuarioDef" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rqBodegaS" runat="server" ControlToValidate="rc_bodegas" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" ID="compvalbodegas" ControlToValidate="rc_bodegas" Operator="NotEqual" ControlToCompare="rc_otbodega">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:CompareValidator>
                            </td>
                            <td>
                                <label>B. Ingreso</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                    Enabled="true" DataSourceID="obj_bodegaxusuario" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSOTBODE") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rqBodegaE" runat="server" ControlToValidate="rc_otbodega" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_otbodega" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="">
                                    <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tercero</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tercero" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_clientes" DataTextField="NOM_COMPLETO" SelectedValue='<%# Bind("TRCODTER") %>' AllowCustomText="true" Filter="Contains" 
                                    DataValueField="TRCODTER" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <%--<td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="true" Visible="false"
                                    Text='<%# Bind("TRCODTER") %>'
                                    Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Eval("TRCODTER") %>'
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
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("TSESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Usu. Creacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrcreacion" runat="server" Enabled="false" Text='<%# Eval("TSNMUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fec. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fconfirma" runat="server" DbSelectedDate='<%# Eval("TSFECCNF") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Usu. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrconfirma" runat="server" Enabled="false" Text='<%# Eval("TSCFUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>L. Precios</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_lstprecio_OnSelectedIndexChanged" AutoPostBack="true"
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("P_CLISPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("TSCOMENT") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Items">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Bill Of Lading">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Segregate">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Costos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_items" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnDeleteCommand="rg_items_OnDeleteCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" OnDetailTableDataBind="rg_items_OnDetailTableDataBind">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="MBIDMOVI,MBIDITEM">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_archivo" runat="server" Text="Cargar Archivo" Icon-PrimaryIconCssClass="rbRefresh" CommandName="Charge" ToolTip="Cargar Archivo" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_cargarwr" runat="server" Text="Salida Embarque" Icon-PrimaryIconCssClass="rbRefresh" CommandName="wrout" ToolTip="Cargar Salida Embarque" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_cargarwrin" runat="server" Text="Entrada Embarque" Icon-PrimaryIconCssClass="rbRefresh" CommandName="wrin" ToolTip="Cargar Entrada Embarque" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_cargarSeg" runat="server" Text="Segregacion" Icon-PrimaryIconCssClass="rbRefresh" CommandName="segregacion" ToolTip="Cargar Segregacion" />
                                        </CommandItemTemplate>
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_insert" Width="100%">
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
                                                    <telerik:GridBoundColumn SortExpression="MECDELEM" HeaderText="Elemento" HeaderButtonType="TextButton"
                                                        DataField="MECDELEM">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="MECANTID" HeaderText="Can Elem" HeaderButtonType="TextButton"
                                                        DataField="MECANTID">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <Columns>
                                            <%--<telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />--%>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridTemplateColumn DataField="MBIDITEM" HeaderText="" HeaderStyle-Width="70px" Visible="false"
                                                Resizable="true" SortExpression="MBIDITEM" UniqueName="MBCSTNAL">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_item" runat="server" Enabled="true" DbValue='<%# Bind("MBIDITEM") %>'
                                                        MinValue="0" Value="0" Width="100px" Visible="false">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                                UniqueName="MBIDITEM">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="MBTIPPRO" HeaderText="" HeaderStyle-Width="70px" Visible="false"
                                                Resizable="true" SortExpression="MBTIPPRO" UniqueName="MBTIPPRO">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Text='<%# Bind("MBTIPPRO") %>'
                                                        Width="100px" Visible="false">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                                UniqueName="MBCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="MBCLAVE2" HeaderText="" HeaderStyle-Width="70px" Visible="false"
                                                Resizable="true" SortExpression="MBCLAVE2" UniqueName="MBCLAVE2">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txt_c2" runat="server" Enabled="true" Text='<%# Bind("MBTIPPRO") %>'
                                                        Width="100px" Visible="false">
                                                    </telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
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
                                            <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                                UniqueName="MBCANTID" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridBoundColumn DataField="MBCSTNAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                DataFormatString="{0:C}" HeaderText="P Lista" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="MBCSTNAL" UniqueName="MBCSTNAL" Aggregate="Sum">                                                
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridTemplateColumn DataField="MBCSTNAL" HeaderText="P Lista" HeaderStyle-Width="70px"
                                                Resizable="true" SortExpression="MBCSTNAL" UniqueName="MBCSTNAL">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="true" DbValue='<%# Bind("MBCSTNAL") %>'
                                                        MinValue="0" Value="0" Width="100px">
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
                            <telerik:RadPageView ID="pv_costos" runat="server">
                                <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_costos_OnItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_NROITEM">
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
                                            <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="{0:C}"
                                                HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO" FooterText="Total: " Aggregate="Sum"
                                                UniqueName="CT_PRECIO">
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
                            <telerik:RadPageView ID="pv_soportes" runat="server">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
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
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Traslado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("TSNROTRA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F. Traslado</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_ftraslado" runat="server" DbSelectedDate='<%# Bind("TSFECTRA") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_ftraslado" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>B. Salida</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_bodegas" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" ID="compvalbodegas" ControlToValidate="rc_bodegas" Operator="NotEqual" ControlToCompare="rc_otbodega">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:CompareValidator>
                            </td>
                            <td>
                                <label>B. Ingreso</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodegaxusuario" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TSOTBODE") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_otbodega" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("TSESTADO") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Usu. Creacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrcreacion" runat="server" Enabled="false" Text='<%# Eval("TSNMUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fec. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fconfirma" runat="server" DbSelectedDate='<%# Eval("TSFECCNF") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Usu. Confirma</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_usrconfirma" runat="server" Enabled="false" Text='<%# Eval("TSCFUSER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("TSCOMENT") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="txt_movsal" runat="server" Enabled="false" Text='<%# Bind("TSMOVSAL") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_movent" runat="server" Enabled="false" Text='<%# Bind("TSMOVENT") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Items">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Bill Of Lading">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Segregate">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Costos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_items" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" DataKeyNames="MBIDMOVI,MBIDITEM"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" OnDetailTableDataBind="rg_items_OnDetailTableDataBind">
                                    <ClientSettings>
                                        <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="MBIDMOVI,MBIDITEM">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="LLAVE">
                                                <DetailTables>
                                                    <telerik:GridTableView Name="detalle_item_elemento" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn SortExpression="MECDELEM" HeaderText="Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                                                DataField="MECDELEM">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn SortExpression="MECANTID" HeaderText="Can Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                                                DataField="MECANTID">
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
                                            <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                                UniqueName="MBIDITEM">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                                UniqueName="MBCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
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
                                            <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                                UniqueName="MBCANTID" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="40px" DataField="MBCANTID" Visible="false"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_cantidad" runat="server" Enabled="true" Visible="false" DbValue='<%# Bind("MBCANTID") %>'
                                                        MinValue="0" Value="0" Width="60px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Can Rec" HeaderStyle-Width="40px"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_canrec" runat="server" Enabled="true" OnTextChanged="txt_canrec_OnTextChanged"
                                                        MinValue="0" Value="0" Width="60px" AutoPostBack="true">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Can Dif" HeaderStyle-Width="40px"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_candif" runat="server" Enabled="false"
                                                        Value="0" Width="60px">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
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
                            <telerik:RadPageView ID="pv_costos" runat="server">
                                <telerik:RadGrid ID="rg_costos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_costos_OnItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_costos_OnNeedDataSource" GroupPanelPosition="Top">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_NROITEM">
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
                                            <telerik:GridBoundColumn DataField="CT_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" DataFormatString="{0:C}"
                                                HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_PRECIO" FooterText="Total: " Aggregate="Sum"
                                                UniqueName="CT_PRECIO">
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
                            <telerik:RadPageView ID="pv_soportes" runat="server">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
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
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
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
                                        <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txt_cantidad" ControlToCompare="txt_caninv" Operator="LessThanEqual" Type="Integer"
                                            ErrorMessage="(*) Inventario Menor al Valor de Salida" ValidationGroup="grNuevoI">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_cantidad"
                                            ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="750px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFiltroArt" runat="server" Width="900px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" ToolTip="Buscar" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel2" runat="server">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="20" CellSpacing="0" GridLines="None"
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
                                        <%--<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE4" HeaderText="" Visible="true"
                                        UniqueName="ARCLAVE4" HeaderButtonType="None" DataField="ARCLAVE4" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="0px">
                                    </telerik:GridBoundColumn>--%>
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
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Dis" DataFormatString="{0:0.#}"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTRN" HeaderText="Tran" DataFormatString="{0:0.#}"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTRN" ItemStyle-HorizontalAlign="Right"
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalRecibir" runat="server" Width="900px" Height="300px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Cod Barras</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_barrasr" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevoR" AutoPostBack="true" OnTextChanged="txt_barrasr_OnTextChanged">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>

                                <telerik:RadTextBox ID="txt_tpr" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave2r" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave3r" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave4r" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>

                                <td>
                                    <label>Referencia</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referenciar" runat="server" Enabled="false" Visible="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_referenciar" runat="server" ControlToValidate="txt_referenciar"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoR">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        Producto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_descripcionr" runat="server" Enabled="false" Visible="true" Width="450px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C2</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc2r" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>C3</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc3r" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Cantidad</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_cantidadr" runat="server" Enabled="true" ValidationGroup="grNuevoR" CausesValidation="false">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_catidad1r" runat="server" ControlToValidate="txt_cantidadr"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoR">
                                        <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rqf_catidad2r" runat="server" ControlToValidate="txt_cantidadr"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoR" InitialValue="0">
                                        <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<asp:Button ID="btn_agregarr" runat="server" Text="Aceptar" OnClick="btn_agregarr_Aceptar" ValidationGroup="grNuevoR"/>--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarr" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregarr_Aceptar" ValidationGroup="grNuevoR" />
                                </td>
                            </tr>
                        </table>
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
                                    <label>Referencia</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referenciact" runat="server" Enabled="false" Visible="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_referenciact"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoCT">
                                        <asp:Image ID="Image15" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        Producto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_descripcionct" runat="server" Enabled="false" Visible="true" Width="350px">
                                    </telerik:RadTextBox>

                                    <asp:ImageButton ID="iBtnFindArticuloct" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" Enabled="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Moneda</label>
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
                                    <label>Precio</label></td>
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
                                    <label>Proveedor</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" DataTextField="TRNOMBRE"
                                        Width="300px" DataSourceID="obj_proveedor" ZIndex="1000000">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Upload Archivo">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalTerceros" runat="server" Width="830px" Height="350px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Terceros">
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
                                                <asp:Label ID="lbl_nomter" runat="server" Text='<%# String.Format("{0} {1} {2}",Eval("TRNOMBRE")," ",Eval("TRNOMBR2")," ",Eval("TRAPELLI")) %>' Visible="true"></asp:Label>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpWROUT" runat="server" Width="750px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Salida Embarque">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Nro WR OUT</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrowr" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarw" runat="server" Text="Filtro" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarw_Click" />
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarf" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarf_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel4" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_wrout" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="false" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_wrout">
                                <MasterTableView DataKeyNames="WOH_CONSECUTIVO">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_chk" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="WOH_CONSECUTIVO" HeaderText="Nro WR Out"
                                            UniqueName="WOH_CONSECUTIVO" HeaderButtonType="TextButton" DataField="WOH_CONSECUTIVO" ItemStyle-HorizontalAlign="Right"
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpSegregaciones" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Segregacion">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar_seg" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptar_seg_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel1" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_segregaciones" runat="server" AllowSorting="True" Width="100%"
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpBillOfLading" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" ShowContentDuringLoad="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server" Width="100%">
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
                                            <asp:Image ID="Image17" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Export Reference</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_exportref" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_exportref"
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_exporter"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image18" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <label>Consignee</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_consignatario" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_consignatario" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_consignatario_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_consignatario"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image19" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Notify Party</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_notify" runat="server" Enabled="false" Width="110px">
                                        </telerik:RadTextBox>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_notify" runat="server" Text="" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_notify_Click" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txt_notify"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image20" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txt_lugarrecibe"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image22" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Precarrie by</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_transportado" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txt_transportado"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image23" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Vessel and Voyage</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nroviaje" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txt_nroviaje"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image24" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Lading</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptocarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txt_ptocarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Port of Discharge</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_ptodescarga" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txt_ptodescarga"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Place of Delivery</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_destino" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txt_destino"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image26" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Domestic Routing</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image27" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Freight Payable AT</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_nrobooking"
                                            ErrorMessage="(*)" ValidationGroup="gvBill">
                                            <asp:Image ID="Image28" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                        <asp:Panel ID="Panel5" runat="server" Width="100%">
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
                        <asp:Panel ID="Panel6" runat="server" Width="100%">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpWRIN" runat="server" Width="750px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Entrada Embarque">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Nro Entrada Embarque</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrowrin" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscarwrin" runat="server" Text="Filtro" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_buscarwrin_Click" />
                                </td>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarwrin" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Aceptar" CommandName="Cancel" OnClick="btn_aceptarwrin_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel7" runat="server" Width="100%">
                            <telerik:RadGrid ID="rg_wrin" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="false" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_wrin">
                                <MasterTableView DataKeyNames="WIH_CONSECUTIVO">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_chk" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="WIH_CONSECUTIVO" HeaderText="Nro Embarque Entrada"
                                            UniqueName="WIH_CONSECUTIVO" HeaderButtonType="TextButton" DataField="WIH_CONSECUTIVO" ItemStyle-HorizontalAlign="Right"
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
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_menu" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_menu_ItemClick">
        <Items>
            <telerik:RadMenuItem Text="Seleccionar Todos">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Anular Seleccion">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>
    <asp:ObjectDataSource ID="obj_traslados" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_traslados_OnInserting" OnUpdating="obj_traslados_OnUpdating"
        SelectMethod="GetTraslados" TypeName="XUSS.BLL.Inventarios.TrasladosBL" InsertMethod="InsertTraslado" OnInserted="obj_traslados_OnInserted" UpdateMethod="UpdateTraslado" OnUpdated="obj_traslados_OnUpdated">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TSCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TSNROTRA" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TSFECTRA" Type="DateTime" />
            <asp:Parameter Name="TSBODEGA" Type="String" />
            <asp:Parameter Name="TSOTBODE" Type="String" />
            <asp:Parameter Name="TSCDTRAN" Type="String" DefaultValue="99" />
            <asp:Parameter Name="TSOTTRAN" Type="String" DefaultValue="98" />
            <asp:Parameter Name="TSMOVENT" Type="String" DefaultValue="0" />
            <asp:Parameter Name="TSMOVSAL" Type="String" DefaultValue="0" />
            <asp:Parameter Name="TSCOMENT" Type="String" DefaultValue="." />
            <asp:Parameter Name="P_CLISPRE" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="TSESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TSCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TSNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbitems" Type="Object" />
            <asp:Parameter Name="tbCostos" Type="Object" />
            <asp:Parameter Name="tbSoportes" Type="Object" />
            <asp:Parameter Name="tbBLHD" Type="Object" />
            <asp:Parameter Name="tbBLDT" Type="Object" />        
            <asp:Parameter Name="tbSegregacion" Type="Object" />
            <asp:Parameter Name="tbTrasladoWrIn" Type="Object" />            
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TSCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TSNROTRA" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TSFECTRA" Type="DateTime" />
            <asp:Parameter Name="TSBODEGA" Type="String" />
            <asp:Parameter Name="TSOTBODE" Type="String" />
            <asp:Parameter Name="TSCDTRAN" Type="String" DefaultValue="99" />
            <asp:Parameter Name="TSOTTRAN" Type="String" DefaultValue="98" />
            <asp:Parameter Name="TSMOVENT" Type="String" />
            <asp:Parameter Name="TSMOVSAL" Type="String" />
            <asp:Parameter Name="TSCOMENT" Type="String" />
            <asp:Parameter Name="TSESTADO" Type="String" />
            <asp:Parameter Name="TSCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TSNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbitems" Type="Object" />
            <asp:Parameter Name="tbCostos" Type="Object" />
            <asp:Parameter Name="tbSoportes" Type="Object" />
            <asp:Parameter Name="tbBLHD" Type="Object" />
            <asp:Parameter Name="tbBLDT" Type="Object" />           
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
    <asp:ObjectDataSource ID="obj_bodegaxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuario" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_bodegaxusuarioDef" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuarioDef" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
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
    <asp:ObjectDataSource ID="obj_proveedor" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>    
    <asp:ObjectDataSource ID="obj_clientes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TRINDCLI='S'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_wrout" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetWROUT" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />            
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_wrin" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetWRIN" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />            
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_segregacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSegregacionHD" TypeName="XUSS.BLL.Compras.SegregacionBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />            
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
    <asp:ObjectDataSource ID="obj_unidad" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UNIT" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
