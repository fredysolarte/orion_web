<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Recaudo.aspx.cs" Inherits="XUSS.WEB.Recaudo.Recaudo" uiCulture="en" %>

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
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btn_filtroTer">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel13" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_recaudo" runat="server" PageSize="1" AllowPaging="True" OnItemInserting="rlv_recaudo_OnItemInserting"
            Width="100%" OnItemCommand="rlv_recaudo_OnItemCommand" OnItemDataBound="rlv_recaudo_OnItemDataBound" OnItemInserted="rlv_recaudo_ItemInserted"
            DataKeyNames="RH_NRORECIBO" DataSourceID="obj_recaudo" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Maestro Recaudo</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_recaudo" RenderMode="Lightweight"
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
                    <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_filtro">
                        <div class="box">
                            <div class="title">
                                <h5>Maestro Recaudos</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <%--<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />--%>
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
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
                                    <label>
                                        Nro Recibo</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_recibo" runat="server" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Cod Cliente</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_codcliente" runat="server" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_identificacion" runat="server" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Tercero</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Nro Recibo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_recibo" runat="server" Enabled="false" Text='<%# Eval("RH_NRORECIBO") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="300px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <label>
                                    F. Recibo</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_frecibo" runat="server" DbSelectedDate='<%# Bind("RH_FECHA") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cliente</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("CLIENTE") %>'
                                    Width="717px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Fisico</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_fisico" runat="server" Enabled="false" Text='<%# Bind("RH_NRORECFISICO") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Banco</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_banco" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bancos" DataTextField="BN_NOMBRE" SelectedValue='<%# Bind("RH_BANCO") %>'
                                    DataValueField="BN_ID" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("RH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                        Culture="(Default)" CellSpacing="0" OnItemCommand="rg_items_OnItemCommand">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="DOCUMENTO" HeaderText="Propietario" UniqueName="DOCUMENTO_TK"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DOCUMENTO" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("DOCUMENTO") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="factura" Text="DOCUMENTO" UniqueName="DOCUMENTO" DataTextField="DOCUMENTO"
                                    HeaderText="Documento" HeaderStyle-Width="100px">
                                </telerik:GridButtonColumn>
                                <telerik:GridBoundColumn DataField="CONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                    HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONCEPTO"
                                    UniqueName="CONCEPTO">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                    HeaderText="Valor" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                    Resizable="true" SortExpression="RC_VALOR" UniqueName="RC_VALOR">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Nro Recibo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_recibo" runat="server" Enabled="false" Text='<%# Eval("RH_NRORECIBO") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="300px">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <label>
                                    F. Recibo</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_frecibo" runat="server" DbSelectedDate='<%# Bind("RH_FECHA") %>'
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="edt_frecibo"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="(*)" ValidationGroup="gvInsert"
                                    Type="Date" ControlToCompare="edt_frecibo" CultureInvariantValues="true" Display="Dynamic"
                                    EnableClientScript="true" SetFocusOnError="true" ControlToValidate="edt_factual"
                                    Operator="GreaterThanEqual">
                                    <asp:Image ID="Image29" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:CompareValidator>
                                <telerik:RadDatePicker ID="edt_factual" runat="server" Enabled="false" ValidationGroup="gvInsert" Visible="false"
                                    DbSelectedDate='<%# System.DateTime.Now %>'>
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cod Cliente</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcliente" runat="server" Enabled="false"
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false"
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cliente</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Eval("CLIENTE") %>'
                                    Width="717px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nombre"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Fisico</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_fisico" runat="server" Enabled="true" Text='<%# Bind("RH_NRORECFISICO") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Banco</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_banco" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bancos" DataTextField="BN_NOMBRE" SelectedValue='<%# Bind("RH_BANCO") %>'
                                    DataValueField="BN_ID" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("RH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>                               
                            <td>
                                <label>Vlr Recaudo</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_trecibo" runat="server" Enabled="true" OnTextChanged="txt_trecibo_TextChanged" AutoPostBack="true" Text='<%# Bind("RH_VALOR") %>'>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" GroupPanelPosition="Top" OnItemDataBound="rg_items_ItemDataBound"
                        Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_OnNeedDataSource" ShowFooter="true" OnItemCommand="rg_items_OnItemCommand">
                        <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                            <%--<Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>--%>
                        </ClientSettings>
                        <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" ShowGroupFooter="true">
                            <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression >
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Fecha" FieldName="HDFECFAC" FormatString="{0:D}" 
                                            HeaderValueSeparator=":"></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="HDFECFAC" SortOrder="Descending"   ></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Documento" FieldName="DOCUMENTO" FormatString="{0:D}"
                                            HeaderValueSeparator=" "></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="DOCUMENTO" SortOrder="Descending"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>
                                <telerik:GridTemplateColumn DataField="RC_CODIGO" HeaderText="Propietario" UniqueName="RC_CODIGO"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="RC_CODIGO" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("RC_CODIGO") %>' Visible="false">
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="DOCUMENTO" HeaderText="Propietario" UniqueName="DOCUMENTO_TK"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DOCUMENTO" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("DOCUMENTO") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn CommandName="factura" Text="DOCUMENTO" UniqueName="DOCUMENTO" DataTextField="DOCUMENTO"
                                    HeaderText="Documento" HeaderStyle-Width="100px">
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn DataField="SALDO" HeaderStyle-Width="120px" HeaderText="Valor" Visible="false"
                                    Resizable="true" SortExpression="SALDO" UniqueName="SALDO" >
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lbl_factura" runat="server" Text='<%#Eval("SALDO") %>' Visible="false"></asp:Label>--%>
                                        <asp:Label ID="lbl_recaudo" runat="server" Text='<%#Eval("RECAUDO") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_factura" runat="server" Text='<%#Eval("HDTOTFAC") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_concepto" runat="server" Text='<%#Eval("RC_CONCEPTO") %>' Visible="false" ></asp:Label>
                                        <asp:Label ID="lbl_nota" runat="server" Text='<%# Eval("NH_NRONOTA") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                    HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONCEPTO"
                                    UniqueName="CONCEPTO">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridButtonColumn CommandName="nc-nd" Text="NH_NRONOTA" UniqueName="NH_NRONOTA" DataTextField="NH_NRONOTA"
                                    HeaderText="" HeaderStyle-Width="100px">
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn DataField="RC_VALOR" HeaderStyle-Width="120px" HeaderText="Valor" 
                                    Resizable="true" SortExpression="RC_VALOR" UniqueName="RC_VALOR" FooterText=" ">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="true" DbValue='<%# Bind("RC_VALOR") %>' OnTextChanged="txt_valor_TextChanged" AutoPostBack="true">
                                        </telerik:RadNumericTextBox>
                                        <asp:ImageButton ID="iBtnFindSaldoFavor" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindSaldoFavor_OnClick" />
                                    </ItemTemplate>     
                                    <%--<FooterTemplate>
                                        <table>
                                            <tr>
                                                <td><label>Recaudo</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txt_trecaudo" runat="server" Enabled="true" >
                                                    </telerik:RadNumericTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                    </FooterTemplate>--%>
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
                    <table style="width: 828px">
                        <tr>
                            <td align="right">
                                <label>
                                    T. Factura</label>
                                <telerik:RadNumericTextBox ID="txt_tfactura" runat="server" Enabled="false">
                                </telerik:RadNumericTextBox>
                                <label>
                                    T. Recaudo</label>
                                <telerik:RadNumericTextBox ID="txt_trecaudo" runat="server" Enabled="false">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>                                
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalTerceros" runat="server" Width="890px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Terceros">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Codigo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="true" Width="80px">
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
                                    <telerik:RadTextBox ID="edt_nomtercero" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroTer" runat="server" Text="Filtrar" OnClick="btn_filtroTer_OnClick" CommandName="Cancel" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroTer" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroTer_OnClick" CommandName="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server">
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
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBRE" HeaderText="Nombre"
                                            UniqueName="TRNOMBRE" HeaderButtonType="TextButton" DataField="TRNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBR2" HeaderText=""
                                            UniqueName="TRNOMBR2" HeaderButtonType="TextButton" DataField="TRNOMBR2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRLISPRE" HeaderText="Lta Precio"
                                            UniqueName="TRLISPRE" HeaderButtonType="TextButton" DataField="TRLISPRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
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
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalSaldos" runat="server" Width="860px" Height="520px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Saldo Favor">
                    <ContentTemplate>
                        <asp:Panel ID="Panel16" runat="server" Width="100%">
                            <telerik:RadGrid ID="rgSaldosFavor" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_saldofavor" OnItemCommand="rgSaldosFavor_OnItemCommand">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="HDTIPFAC" HeaderText="T Fac"
                                            UniqueName="HDTIPFAC" HeaderButtonType="TextButton" DataField="HDTIPFAC" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="30px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="HDNROFAC" HeaderText="Nro Factura"
                                            UniqueName="HDNROFAC" HeaderButtonType="TextButton" DataField="HDNROFAC" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="SF" HeaderText="Saldo Favor"
                                            UniqueName="SF" HeaderButtonType="TextButton" DataField="SF" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
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
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_recaudo" runat="server" OldValuesParameterFormatString="original_{0}"
        InsertMethod="InsertRecaudo" SelectMethod="GetRecaudo" TypeName="XUSS.BLL.Recaudo.RecaudoBL"
        OnInserting="obj_recaudo_OnInserting" OnInserted="obj_recaudo_OnInserted">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="RH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="RH_FECHA" Type="DateTime" />
            <asp:Parameter Name="RH_NRORECFISICO" Type="String" />
            <asp:Parameter Name="RH_OBSERVACIONES" Type="String" />
            <asp:Parameter Name="RH_VALOR" Type="Double" />
            <asp:Parameter Name="RH_BANCO" Type="Int32" />
            <asp:SessionParameter Name="RC_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="inTbItems" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_trecuado" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="CONREC" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_items" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetItemsRecaudo" TypeName="XUSS.BLL.Recaudo.RecaudoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="InRecuado" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tfactura" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.ParametrosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_saldofavor" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSaldoFavor" TypeName="XUSS.BLL.Recaudo.RecaudoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="inTercero" Type="String" />
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
    <asp:ObjectDataSource ID="obj_bancos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBancos" TypeName="XUSS.BLL.Parametros.BancoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
