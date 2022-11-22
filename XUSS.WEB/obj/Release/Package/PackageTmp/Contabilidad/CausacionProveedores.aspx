<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CausacionProveedores.aspx.cs" Inherits="XUSS.WEB.Contabilidad.CausacionProveedores" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function OnClientEntryAddingHandler(sender, eventArgs) {

            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
            }
        }
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

        function OnClientAdded(sender, args) {
            var allowedMimeTypes = $telerik.$(sender.get_element()).attr("data-clientFilter");
            $telerik.$(args.get_row()).find(".ruFileInput").attr("accept", allowedMimeTypes);
        }
    </script>
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadListView ID="rlv_causasion" runat="server" ItemPlaceholderID="pnlGeneral" OnPreRender="rlv_causasion_PreRender"
            OnItemCommand="rlv_causasion_ItemCommand" DataSourceID="obj_movhd" AllowPaging="True"
            PageSize="1" OnItemDataBound="rlv_causasion_OnItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Causasion</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_causasion" RenderMode="Lightweight"
                            PageSize="1">
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
                    <div class="box">
                        <div class="title">
                            <h5>Causasion</h5>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 7px">
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
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
                                <label>T. Documento</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE" AppendDataBoundItems="true"
                                    DataValueField="TFTIPFAC">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro Documento</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_numero" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro Interno</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_ninterno" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Año</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_ano" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_anos" DataTextField="MA_ANO" AutoPostBack="true"
                                    DataValueField="MA_ANO" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_ano_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Mes</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_mes" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
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
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Tipo Documento</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Text='<%# Bind("MVTH_CODIGO") %>' Visible="false">
                                </telerik:RadTextBox>

                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px" AppendDataBoundItems="true"
                                    Enabled="false" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("TFTIPFAC") %>'
                                    DataValueField="TFTIPFAC">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("MVTH_FECMOV") %>'
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>
                                    Numero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_numero" runat="server" Text='<%# Bind("MVTH_DOCCON") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("MVTH_MONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Concepto</label></td>
                            <td colspan="6">
                                <telerik:RadTextBox ID="txt_concepto" runat="server" Text='<%# Bind("MVTH_DESCRIPCION") %>' Width="400px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
                        <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemDataBound="rgDetalle_ItemDataBound"
                            Culture="(Default)" CellSpacing="0" OnItemCommand="rgDetalle_ItemCommand" OnNeedDataSource="rgDetalle_OnNeedDataSource" ShowFooter="true">
                            <MasterTableView ShowGroupFooter="true" DataKeyNames="MVTD_IDENT">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Cuenta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                        UniqueName="PC_CODIGO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="D Cuenta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                        UniqueName="PC_NOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MVTD_DESCRIPCION" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="100px" HeaderText="Concepto" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="MVTD_DESCRIPCION" UniqueName="MVTD_DESCRIPCION">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="MVTD_TIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="T Soporte" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTD_TIPDOC"
                                        UniqueName="MVTD_TIPDOC">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MVTD_NRODOC" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Nro Soporte" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTD_NRODOC"
                                        UniqueName="MVTD_NRODOC">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MVTD_FECDOC" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F Soporte" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MVTD_FECDOC"
                                        UniqueName="MVTD_FECDOC">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn DataField="MVTD_DEBITO" HeaderStyle-Width="100px" HeaderText="Debito"
                                        Resizable="true" SortExpression="MVTD_DEBITO" UniqueName="MVTD_DEBITO" FooterText=" " FooterStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txt_mdebito" runat="server" Enabled="false" BorderStyle="None" DbValue='<%# Bind("MVTD_DEBITO") %>' Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="MVTD_CREDITO" HeaderStyle-Width="100px" HeaderText="Credito"
                                        Resizable="true" SortExpression="MVTD_CREDITO" UniqueName="MVTD_CREDITO" FooterText=" " FooterStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txt_mcredito" runat="server" Enabled="false" BorderStyle="None" DbValue='<%# Bind("MVTD_CREDITO") %>' Width="100px">
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
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_attach" runat="server" Text="Adjunto" Icon-PrimaryIconCssClass="rbAttach" CommandName="attach" ToolTip="Nueva Adjunto" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="MVEV_ID" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="MVEV_ID" UniqueName="MVEV_ID" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MVEV_DESCRIPCION" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="MVEV_DESCRIPCION" UniqueName="MVEV_DESCRIPCION">
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
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnCabecera" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Tipo Documento</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px" AppendDataBoundItems="true"
                                    Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("TFTIPFAC") %>'
                                    DataValueField="TFTIPFAC">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("MVTH_FECMOV") %>'
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>
                                    Numero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_numero" runat="server" Text='<%# Bind("MVTH_DOCCON") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnDeleteCommand="rgDetalle_DeleteCommand" OnItemDataBound="rgDetalle_ItemDataBound"
                    Culture="(Default)" CellSpacing="0" OnItemCommand="rgDetalle_ItemCommand" OnNeedDataSource="rgDetalle_OnNeedDataSource" ShowFooter="true">
                    <MasterTableView CommandItemDisplay="Top" ShowGroupFooter="true" DataKeyNames="MVTD_IDENT">
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                        <CommandItemTemplate>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Documento" Icon-PrimaryIconCssClass="rbAdd" CommandName="new" ToolTip="Nuevo Documento" />
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_concepto" runat="server" Text="Nuevo Concepto" Icon-PrimaryIconCssClass="rbAdd" CommandName="new_concept" ToolTip="Nuevo Concepto" />
                        </CommandItemTemplate>
                        <Columns>
                             <telerik:GridTemplateColumn DataField="MVTD_IDENT" HeaderStyle-Width="120px" HeaderText="Debito"
                                Resizable="true" SortExpression="MVTD_IDENT" UniqueName="MVTD_IDENT" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("MVTD_IDENT") %>' Visible="false"  >
                                    </telerik:RadTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn ConfirmText="¿Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                            <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                HeaderText="Cuenta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                UniqueName="PC_CODIGO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                HeaderText="D Cuenta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                UniqueName="PC_NOMBRE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTD_DESCRIPCION" HeaderButtonType="TextButton"
                                HeaderStyle-Width="40px" HeaderText="Concepto" ItemStyle-HorizontalAlign="Right"
                                Resizable="true" SortExpression="MVTD_DESCRIPCION" UniqueName="MVTD_DESCRIPCION">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTD_NOMTIPDOC" HeaderButtonType="TextButton"
                                HeaderStyle-Width="40px" HeaderText="T Doc" ItemStyle-HorizontalAlign="Right"
                                Resizable="true" SortExpression="MVTD_NOMTIPDOC" UniqueName="MVTD_NOMTIPDOC">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MVTD_NRODOC" HeaderButtonType="TextButton"
                                HeaderStyle-Width="40px" HeaderText="Nro Doc" ItemStyle-HorizontalAlign="Right"
                                Resizable="true" SortExpression="MVTD_NRODOC" UniqueName="MVTD_NRODOC">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="MVTD_DEBITO" HeaderStyle-Width="120px" HeaderText="Debito"
                                Resizable="true" SortExpression="MVTD_DEBITO" UniqueName="MVTD_DEBITO" FooterText=" ">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txt_mdebito" runat="server" Enabled="true" DbValue='<%# Bind("MVTD_DEBITO") %>' AutoPostBack="true" OnTextChanged="txt_mdebito_TextChanged" >
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="MVTD_CREDITO" HeaderStyle-Width="120px" HeaderText="Credito"
                                Resizable="true" SortExpression="MVTD_CREDITO" UniqueName="MVTD_CREDITO" FooterText=" ">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txt_mcredito" runat="server" Enabled="true" DbValue='<%# Bind("MVTD_CREDITO") %>' AutoPostBack="true" OnTextChanged="txt_mcredito_TextChanged">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_agregar" runat="server" DefaultButton="btn_agregar">
                            <table>
                                <tr>
                                    <td>
                                        <label>Identificacion</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="false" Visible="false" Width="300px">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert" Width="300px">
                                        </telerik:RadTextBox>
                                        <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick" />
                                    </td>
                                    <td>
                                        <label>Tercero</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Width="300px">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_tercero"
                                            ErrorMessage="(*)" ValidationGroup="grNuevo">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Tip Doc</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tipdoccli" runat="server" Enabled="true" Width="300px" ZIndex="1000000">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                                <telerik:RadComboBoxItem Text="Factura" Value="01" />
                                                <telerik:RadComboBoxItem Text="Cta Cobro" Value="02" />
                                                <telerik:RadComboBoxItem Text="Remision" Value="03" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>Nro Documento</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="true" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Concepto</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_concepto_mov" runat="server" Enabled="true" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>SubTotal/Bruto</label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="true">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rqf_preciolta" runat="server" ControlToValidate="txt_valor"
                                            ErrorMessage="(*)" ValidationGroup="grNuevo">
                                            <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>Total/Neto</label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_neto" runat="server" Enabled="true">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_neto"
                                            ErrorMessage="(*)" ValidationGroup="grNuevo">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" ToolTip="Agregar" ValidationGroup="grNuevo" OnClick="btn_agregar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpConcepto" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main" Title="Detalle" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btn_aceptar_concepto">
                            <table>
                                <tr>
                                    <td>
                                        <label>Cta Contable</label></td>
                                    <td colspan="3">
                                        <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single" ZIndex="1000000"
                                            DataSourceID="obj_ctacontable" Width="350px" DataTextField="UA_NOMBRE" DropDownWidth="550px" Enabled="true"
                                            OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="UA_CODIGO"
                                            DropDownHeight="580px" Filter="Contains">
                                            <DropDownItemTemplate>
                                                <table cellspacing="1">
                                                    <tr>
                                                        <td>
                                                            <%# DataBinder.Eval(Container.DataItem, "UA_CODIGO")%>
                                                        </td>
                                                        <td>
                                                            <%# DataBinder.Eval(Container.DataItem, "UA_NOMBRE")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DropDownItemTemplate>
                                        </telerik:RadAutoCompleteBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Concepto</label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="txt_concepto" runat="server" Enabled="true" Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Debito</label></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_debito" runat="server" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                    <td>
                                        <label>Credito</label></td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txt_credito" runat="server" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar_concepto" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Agregar" OnClick="btn_aceptar_concepto_Click" />
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
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" AllowedFileExtensions="pdf" data-clientFilter="pdf/pdf" OnClientAdded="OnClientAdded">
                                    </telerik:RadAsyncUpload>
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadTextBox ID="txt_obsevidencia" runat="server" Enabled="true" Width="600px" TextMode="MultiLine" Height="40px">
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
    <asp:ObjectDataSource ID="obj_puc" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (10)" Name="filter" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_movhd" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetMovimientosHD" OnInserting="obj_movhd_OnInserting"
        TypeName="XUSS.BLL.Contabilidad.CausacionProveedoresBL" InsertMethod="InsertMovimiento">
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="MVTH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="MVTH_TIPDOC" Type="String" />
            <asp:Parameter Name="MVTH_DOCCON" Type="String" />
            <asp:Parameter Name="MVTH_DIA" Type="Int32" />
            <asp:Parameter Name="MVTH_MES" Type="Int32" />
            <asp:Parameter Name="MVTH_ANO" Type="Int32" />
            <asp:SessionParameter Name="MVTH_CDUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="MVTH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="dtDetalle" Type="Object" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_ctacontable" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_anos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAnos" TypeName="XUSS.BLL.Parametros.MesesBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=1" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />            
            <asp:Parameter Name="maximumRows" Type="Int32" />                        
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
