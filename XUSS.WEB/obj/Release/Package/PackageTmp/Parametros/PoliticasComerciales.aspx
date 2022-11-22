<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PoliticasComerciales.aspx.cs" Inherits="XUSS.WEB.Parametros.PoliticasComerciales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
         <style type="text/css">
            .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6
            {
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
                var menu = $find("<%=ct_bodegas.ClientID %>");
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                //document.getElementById("radGridClickedRowIndex").value = index;
                document.getElementById("radGridClickedRowIndex").value = "1-" + index;
                if (eventArgs._id.search("rgCategoria") > 0)
                    document.getElementById("radGridClickedRowIndex").value = "2-" + index;

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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_politicas" runat="server" PageSize="1" AllowPaging="True"
            OnItemCommand="rlv_politicas_OnItemCommand" OnItemDataBound="rlv_politicas_OnItemDataBound"
            DataSourceID="obj_politicas" ItemPlaceholderID="pnlGeneral" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Politicas Comerciales</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_politicas" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Politicas Comerciales</h5>
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />--%>
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false"
                                    Text='<%# Bind("ID") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false"
                                    Text='<%# Bind("NOMBRE") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("TIPO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Porcentaje" Value="P" />
                                        <telerik:RadComboBoxItem Text="Valor" Value="V" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Condicion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_condicion" runat="server" SelectedValue='<%# Bind("CONDICION") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Fecha" Value="F" />
                                        <telerik:RadComboBoxItem Text="Nro Identificacion" Value="C" />
                                        <telerik:RadComboBoxItem Text="Tipo Pago" Value="T" />
                                        <telerik:RadComboBoxItem Text="Cantidad" Value="Q" />
                                        <telerik:RadComboBoxItem Text="Cumpleaños" Value="H" />
                                        <telerik:RadComboBoxItem Text="Especial" Value="E" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnDeleteCommand="rg_items_DeleteCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" AllowPaging="true" PageSize="100" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="ID,ID_DESCUENTO">                                
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" AddNewRecordText="Nuevo Item" ShowRefreshButton="true" RefreshText="Buscar" />
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="¿Desea Inactivar este Descuento?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="ID" HeaderButtonType="TextButton" HeaderStyle-Width="30px"
                                        HeaderText="ID" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ID"
                                        UniqueName="ID">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BODEGA_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BODEGA_"
                                        UniqueName="BODEGA_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TP_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TP_"
                                        UniqueName="TP_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE1_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C1" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE1_"
                                        UniqueName="CLAVE1_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE2_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2_"
                                        UniqueName="CLAVE2_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE3_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3_"
                                        UniqueName="CLAVE3_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE4_" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C4" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE4_"
                                        UniqueName="CLAVE4_">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CONDICION_1" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                        HeaderText="Con 1" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONDICION_1"
                                        UniqueName="CONDICION_1">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CONDICION_2" HeaderButtonType="TextButton" HeaderStyle-Width="30px"
                                        HeaderText="Con 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONDICION_2"
                                        UniqueName="CONDICION_2">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        DataFormatString="{0:#.###}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="VALOR" UniqueName="VALOR">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FECHAINI" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F. Inicial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FECHAINI"
                                        UniqueName="FECHAINI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FECHAFIN" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F. Fin" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FECHAFIN"
                                        UniqueName="FECHAFIN">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="USUARIO" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="USUARIO"
                                        UniqueName="USUARIO">
                                        <ItemStyle HorizontalAlign="Right" />
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
                    <asp:Panel ID="pnlBuscar" runat="server" Visible="false">
                        <div class="box">
                            <div class="title">
                                <h5>Detalle Politica Dcto</h5>
                            </div>
                        </div>
                        <fieldset class="cssFieldSetContainer">
                            <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Categoria</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_categoria" runat="server"
                                                Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                                Enabled="true" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Referencia</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>F. Inicial</label>
                                        </td>
                                        <td></td>
                                        <td>
                                            <label>F. Final</label>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btn_buscar_det" runat="server" Text="Buscar" OnClick="btn_buscar_det_OnClick" CommandName="FindDet" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </fieldset>
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false"
                                    Text='<%# Bind("ID") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false"
                                    Text='<%# Bind("NOMBRE") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("TIPO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Porcentaje" Value="P" />
                                        <telerik:RadComboBoxItem Text="Valor" Value="V" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Condicion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_condicion" runat="server" SelectedValue='<%# Bind("CONDICION") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Fecha" Value="F" />
                                        <telerik:RadComboBoxItem Text="Nro Identificacion" Value="C" />
                                        <telerik:RadComboBoxItem Text="Tipo Pago" Value="T" />
                                        <telerik:RadComboBoxItem Text="Cantidad" Value="Q" />
                                        <telerik:RadComboBoxItem Text="Cumpleaños" Value="H" />
                                        <telerik:RadComboBoxItem Text="Especial" Value="E" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server" Width="100%">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DTNROITM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROITM"
                                        UniqueName="DTNROITM">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TP" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TP"
                                        UniqueName="TP">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C1" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE1"
                                        UniqueName="CLAVE1">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                        UniqueName="CLAVE2">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                        UniqueName="CLAVE3">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE4" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="C4" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE4"
                                        UniqueName="CLAVE4">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        DataFormatString="{0:#.###}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="VALOR" UniqueName="VALOR">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <asp:Panel ID="pnlBuscar" runat="server" Visible="false">
                        <div class="box">
                            <div class="title">
                                <h5>Detalle Politica Dcto</h5>
                            </div>
                        </div>
                        <fieldset class="cssFieldSetContainer">
                            <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Categoria</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_categoria" runat="server"
                                                Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                                Enabled="true" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Referencia</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btn_buscar_det" runat="server" Text="Buscar" OnClick="btn_buscar_det_OnClick" CommandName="FindDet" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </fieldset>
                    </asp:Panel>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpAsistente" runat="server" Width="900px" Height="500px" Modal="true" OffsetElementID="main" Title="Lista Bodegas" EnableShadow="true">
                    <ContentTemplate>                        
                        <telerik:RadSplitter ID="RadSplitter2" runat="server" Width="98%" Height="430px">                              
                                <telerik:RadPane ID="RadPane2" runat="server" Width="100%">                                    
                                    <table>
                                        <tr>
                                            <td>
                                                <label>F. Inicial</label></td>
                                            <td>
                                                <telerik:RadDatePicker ID="edt_finicialasis" runat="server" MinDate="01/01/1900" Enabled="true" ZIndex="1000000" ValidationGroup="gvInsertASI">
                                                </telerik:RadDatePicker>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_finicialasis"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertASI">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>F. Final</label></td>
                                            <td>
                                                <telerik:RadDatePicker ID="edt_ffinalasis" runat="server" MinDate="01/01/1900" Enabled="true" ZIndex="1000000" ValidationGroup="gvInsertASI">
                                                </telerik:RadDatePicker>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="edt_ffinalasis"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertASI">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Porcentaje/Valor</label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox ID="edt_valorasi" MaxValue="100" MinValue="1" runat="server" ValidationGroup="gvInsertASI"></telerik:RadNumericTextBox>
                                                <asp:RequiredFieldValidator ID="rq_valor" runat="server" ControlToValidate="edt_valorasi"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertASI">
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>                                    
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane3" runat="server" Width="50%">
                                    <asp:Panel ID="pnBodegas" runat="server" Visible="true" Width="100%">
                                        <telerik:RadGrid ID="rgBodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="80%"
                                            Culture="(Default)" CellSpacing="0" EnableHeaderContextMenu="true">
                                            <ClientSettings>
                                                <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <MasterTableView ShowGroupFooter="true">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="chk" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="COD" HeaderButtonType="TextButton"
                                                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COD"
                                                        UniqueName="COD">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton"
                                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMBRE"
                                                        UniqueName="NOMBRE">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </telerik:RadPane>                            
                        </telerik:RadSplitter>                        
                        <asp:Panel ID="pnBotonoes" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_next" runat="server" Text="Siguiente" Icon-PrimaryIconCssClass="rbNext" CommandName="Next" ToolTip="Siguiente" OnClick="btn_next_OnClick" ValidationGroup="gvInsertASI" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>                            
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpLineaXReferencia" runat="server" Width="900px" Height="500px" Modal="true" OffsetElementID="main" Title="Lista Bodegas" EnableShadow="true" Style="z-index: 1900;">
                    <ContentTemplate>
                        <asp:Panel ID="pnLineas" runat="server" Visible="true">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="430px">
                                <telerik:RadPane ID="LeftPane" runat="server" Width="30%">
                                    <telerik:RadGrid ID="rgCategoria" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="80%" ClientSettings-EnablePostBackOnRowClick="true"
                                        Culture="(Default)" CellSpacing="0" OnSelectedIndexChanged="rgCategoria_OnSelectedIndexChanged">
                                        <ClientSettings>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="chk" HeaderStyle-Width="30px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_tp" runat="server" Text='<%# Eval("COD") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="COD" HeaderButtonType="TextButton"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COD"
                                                    UniqueName="COD">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton"
                                                    HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMBRE"
                                                    UniqueName="NOMBRE">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane1" runat="server" Width="70%" Height="80%">
                                    <%--AllowPaging="true" PageSize="100"--%>
                                    <telerik:RadGrid ID="rgArticulos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="80%" OnItemCommand="rgArticulos_OnItemCommand"
                                        Culture="(Default)" CellSpacing="0" AllowFilteringByColumn="True" AllowSorting="true" OnNeedDataSource="rgArticulos_OnNeedDataSource">
                                        <FilterMenu Style="z-index: 2001;"></FilterMenu>
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="chk" HeaderStyle-Width="30px" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="c1_lbl" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_c1" runat="server" Text='<%# Eval("C1") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="C1" HeaderButtonType="TextButton" FilterControlWidth="40px"
                                                    HeaderText="Ref." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C1"
                                                    UniqueName="C1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NM" HeaderButtonType="TextButton"
                                                    HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NM"
                                                    UniqueName="NM">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_aplicar" runat="server" Text="Aplicar" Icon-PrimaryIconCssClass="rbSave" CommandName="Save" ToolTip="Aplicar" OnClick="btn_aplicar_OnClick" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_guardar" runat="server" Text="Guardar" Icon-PrimaryIconCssClass="rbOk" CommandName="Save" ToolTip="Guardar" OnClick="btn_guardar_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpCedula" runat="server" Width="800px" Height="285px" Modal="true" OffsetElementID="main" Title="Descuento x Cedula" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>F. Inicial</label></td>
                                <td>
                                    <telerik:RadDatePicker ID="edt_finicialc" runat="server" MinDate="01/01/1900" Enabled="true" ZIndex="1000000" ValidationGroup="gvInsertCV">
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edt_finicialc"
                                        ErrorMessage="(*)" ValidationGroup="gvInsertCV">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>F. Final</label></td>
                                <td>
                                    <telerik:RadDatePicker ID="edt_ffinalc" runat="server" MinDate="01/01/1900" Enabled="true" ZIndex="1000000" ValidationGroup="gvInsertCV">
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_ffinalc"
                                        ErrorMessage="(*)" ValidationGroup="gvInsertCV">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Identificacion</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Width="300px" OnTextChanged="txt_identificacion_OnTextChanged" AutoPostBack="true" ValidationGroup="gvInsertCV">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_identificacion"
                                        ErrorMessage="(*)" ValidationGroup="gvInsertCV">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Nombre</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Procentaje/Valor</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="edt_valor_ced" MaxValue="100" MinValue="1" runat="server" ValidationGroup="gvInsertCV"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="edt_valor_ced"
                                        ErrorMessage="(*)" ValidationGroup="gvInsertCV">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Indefinido</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_terminos" runat="server" Enabled="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_guardar_ced" runat="server" Text="Guardar" Icon-PrimaryIconCssClass="rbSave" CommandName="Save" ToolTip="Guardar" OnClick="btn_guardar_ced_OnClick" ValidationGroup="gvInsertCV" />
                                </td>
                            </tr>
                        </table>
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_bodegas" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_bodegas_ItemClick">
        <Items>
            <telerik:RadMenuItem Text="Seleccionar Todos">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Anular Seleccion">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadContextMenu>
    <asp:ObjectDataSource ID="obj_politicas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPoliticaHD" TypeName="XUSS.BLL.Parametros.PoliticasComercialesBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
