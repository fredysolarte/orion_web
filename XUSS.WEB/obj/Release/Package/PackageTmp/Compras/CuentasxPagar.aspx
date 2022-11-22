<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CuentasxPagar.aspx.cs" Inherits="XUSS.WEB.Compras.CuentasxPagar" %>

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
    </telerik:RadScriptBlock>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_pagos" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_pagos_ItemInserted"
            OnItemCommand="rlv_pagos_ItemCommand" OnItemDataBound="rlv_pagos_ItemDataBound" DataKeyNames="HP_NROPAGO"
            DataSourceID="obj_pagos" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Pagos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_pagos" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Pagos</h5>
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
                                        Nro Pago</label>
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
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_Click" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />                    --%>
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Pago</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Eval("HP_NROPAGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("HP_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" SelectedValue='<%# Bind("TRCODTER") %>' DataTextField="TRNOMBRE"
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("HP_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0">
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
                                    <telerik:GridBoundColumn DataField="DP_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Valor" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                        Resizable="true" SortExpression="DP_VALOR" UniqueName="RC_VALOR">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Pago</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false" Text='<%# Eval("HP_NROPAGO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server" DbSelectedDate='<%# Bind("HP_FECHA") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" SelectedValue='<%# Bind("TRCODTER") %>' DataTextField="TRNOMBRE" OnSelectedIndexChanged="rc_proveedor_SelectedIndexChanged"
                                    Width="300px" DataSourceID="obj_terceros" AutoPostBack="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("HP_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" GroupPanelPosition="Top"
                        Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_NeedDataSource">
                        <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                            <%--<Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>--%>
                        </ClientSettings>
                        <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" ShowGroupFooter="true">
                            <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                            <GroupByExpressions>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Fecha" FieldName="FD_FECFAC" FormatString="{0:D}"
                                            HeaderValueSeparator=":"></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="FD_FECFAC" SortOrder="Descending"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldAlias="Documento" FieldName="DOCUMENTO" 
                                            HeaderValueSeparator=" "></telerik:GridGroupByField>
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="DOCUMENTO" SortOrder="Descending"></telerik:GridGroupByField>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>
                            <Columns>                                
                                <telerik:GridTemplateColumn DataField="DP_CODIGO" HeaderText="Propietario" UniqueName="DP_CODIGO"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="RC_CODIGO" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("DP_CODIGO") %>' Visible="false">
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
                                <telerik:GridTemplateColumn DataField="SALDO" HeaderStyle-Width="120px" HeaderText="Valor"
                                    Resizable="true" SortExpression="SALDO" UniqueName="SALDO" Aggregate="Avg" FooterText="Saldo Actual: ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_factura" runat="server" Text='<%#Eval("SALDO") %>' Visible="false" DataFormatString="{0:C}"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                    HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONCEPTO"
                                    UniqueName="CONCEPTO">
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="DP_VALOR" HeaderStyle-Width="120px" HeaderText="Valor"
                                    Resizable="true" SortExpression="DP_VALOR" UniqueName="DP_VALOR" Aggregate="Sum" FooterText="Aplicado : ">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="true" DbValue='<%# Bind("DP_VALOR") %>' AutoPostBack="true" OnTextChanged="txt_valor_TextChanged">
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
            </Windows>
            </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_pagos" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_pagos_Inserting"
        SelectMethod="GetPagosHD" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" OnInserted="obj_pagos_Inserted" InsertMethod="InsertPago">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="HP_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="HP_FECHA" Type="DateTime" />            
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="HP_NRORECFISICO" Type="Int32" DefaultValue="1" />
            <asp:Parameter Name="HP_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="inUsuario" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbDetalle" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
