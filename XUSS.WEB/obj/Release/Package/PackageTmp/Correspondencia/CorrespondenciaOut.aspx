<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CorrespondenciaOut.aspx.cs" Inherits="XUSS.WEB.Correspondencia.CorrespondenciaOut" %>
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
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_corrrespondeciaIn" runat="server" PageSize="1" AllowPaging="True" DataSourceID="obj_correspondenciaIn" ItemPlaceholderID="pnlGeneral" OnItemInserted="rlv_corrrespondeciaIn_ItemInserted" OnPreRender="rlv_corrrespondeciaIn_PreRender"
            DataKeyNames="COH_CODIGO" DataSourceCount="0" ResolvedRenderMode="Classic" OnItemDataBound="rlv_corrrespondeciaIn_OnItemDataBound" OnItemCommand="rlv_corrrespondeciaIn_ItemCommand">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Correspondencia Salida</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_corrrespondeciaIn" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Correspondecia Salida</h5>
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
                                        Nro Planilla</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_planilla" runat="server" Enabled="true" Width="300px">
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
                                <td>
                                    <label>
                                        Nombres/Apellido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Proyecto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadComboBox ID="rc_proyectof" runat="server" Culture="es-CO" Width="500px" ZIndex="1000000" 
                                        Enabled="true" DataSourceID="obj_clientes" DataTextField="NOM_COMPLETO" AllowCustomText="true" Filter="Contains"
                                        DataValueField="TRCODTER" AppendDataBoundItems="true" ValidationGroup="gvInsertD">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Poliza</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_poliza" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Cta Contrato</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_ctacontrato" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Torre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_torre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Apto</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_apto" runat="server" Enabled="true" Width="300px">
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_descargar" runat="server" Text="Descargar" OnClick="iBtnDownload_OnClick" Icon-PrimaryIconCssClass="rbDownload" CommandName="Cancel" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Planilla</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_planilla" runat="server" Text='<%# Bind("COH_CODIGO") %>' Enabled="false">
                                </telerik:RadTextBox>                                
                            </td>
                            <td>
                                <label>F.Planilla</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_Fecha" runat="server" DbSelectedDate='<%# Bind("COH_FECHA") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("COH_DESCRIPCION") %>' Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_NeedDataSource" >
                            <MasterTableView ShowGroupFooter="true" >                                
                                <Columns>                                    
                                    <telerik:GridBoundColumn DataField="COD_ITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COD_ITEM"
                                        UniqueName="COD_ITEM">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_POLIZA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Poliza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_POLIZA"
                                            UniqueName="PH_POLIZA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                            UniqueName="PH_CTACONTRATO">
                                        </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Proyecto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                        UniqueName="TRNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO"
                                        UniqueName="PH_EDIFICIO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                        UniqueName="PH_ESCALERA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MECDELEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Serial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MECDELEM"
                                        UniqueName="MECDELEM">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_CUOTAS" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Cuotas" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_CUOTAS"
                                        UniqueName="CO_CUOTAS">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:C}"
                                        HeaderText="Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_PRECIO"
                                        UniqueName="CO_PRECIO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_FECCOMODATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comodato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECCOMODATO" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECCOMODATO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comercial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECHA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLIENTE"
                                            UniqueName="CLIENTE">
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
                                <label>F.Planilla</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_Fecha" runat="server" DbSelectedDate='<%# Bind("COH_FECHA") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_Fecha" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" >
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("COH_DESCRIPCION") %>' Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_NeedDataSource" OnItemCommand="rg_items_ItemCommand" OnDeleteCommand="rg_items_DeleteCommand">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="COD_ITEM">
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="COD_ITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COD_ITEM"
                                        UniqueName="COD_ITEM">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_POLIZA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Poliza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_POLIZA"
                                            UniqueName="PH_POLIZA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                            UniqueName="PH_CTACONTRATO">
                                        </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Proyecto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                        UniqueName="TRNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO"
                                        UniqueName="PH_EDIFICIO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                        UniqueName="PH_ESCALERA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MECDELEM" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Serial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MECDELEM"
                                        UniqueName="MECDELEM">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_CUOTAS" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Cuotas" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_CUOTAS"
                                        UniqueName="CO_CUOTAS">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="{0:C}"
                                        HeaderText="Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_PRECIO"
                                        UniqueName="CO_PRECIO">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CO_FECCOMODATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comodato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECCOMODATO" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECCOMODATO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comercial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECHA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLIENTE"
                                            UniqueName="CLIENTE">
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
                </asp:Panel>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalAsistente" runat="server" Width="1200px" Height="570px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Asistente">
                    <ContentTemplate>
                        <table>                            
                            <tr>
                                <td>
                                    <label>Proyecto</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_proyecto" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000" OnSelectedIndexChanged="rc_proyecto_SelectedIndexChanged" AutoPostBack="true"
                                        Enabled="true" DataSourceID="obj_clientes" DataTextField="NOM_COMPLETO" AllowCustomText="true" Filter="Contains"
                                        DataValueField="TRCODTER" AppendDataBoundItems="true" ValidationGroup="gvInsertD">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_proyecto" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvInsertD">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_proyecto" InitialValue=""
                                        ErrorMessage="(*)" ValidationGroup="gvInsertD">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnl_Detalle_asistente" runat="server">
                            <telerik:RadGrid ID="rg_items_Asistente" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="Lote" UniqueName="clote" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_ind" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Lote" UniqueName="clote" HeaderStyle-Width="40px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_propiedad" runat="server"  Text='<%# Eval("PH_CODIGO") %>' Visible="false" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="PH_POLIZA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Poliza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_POLIZA"
                                            UniqueName="PH_POLIZA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                            UniqueName="PH_CTACONTRATO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO"
                                            UniqueName="PH_EDIFICIO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                            UniqueName="PH_ESCALERA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MECDELEM" HeaderButtonType="TextButton" HeaderStyle-Width="140px"
                                            HeaderText="Elemento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MECDELEM"
                                            UniqueName="MECDELEM">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_CUOTAS" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            HeaderText="Cuotas" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_CUOTAS"
                                            UniqueName="CO_CUOTAS">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="CO_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_PRECIO" DataFormatString="{0:C}"
                                            UniqueName="CO_PRECIO">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Valor" UniqueName="CO_PRECIO" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_precio" runat="server" DbValue='<%# Eval("CO_PRECIO") %>' Enabled="false" Width="90px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CO_FECCOMODATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comodato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECCOMODATO" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECCOMODATO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CO_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="F Comercial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                            UniqueName="CO_FECHA">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLIENTE"
                                            UniqueName="CLIENTE">
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
                                <td colspan="2" align="center">
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_add" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" ToolTip="Ok" CommandName="Cancel" OnClick="btn_add_Click" ValidationGroup="gvInsertD"  />
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
    <asp:ObjectDataSource ID="obj_correspondenciaIn" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertCorrespondenciaOUT" SelectMethod="GetCorrespondenciaHDOUT" TypeName="XUSS.BLL.Correspondencia.CorrespondenciaBL" 
        OnInserted="obj_correspondenciaIn_Inserted" OnInserting="obj_correspondenciaIn_Inserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="COH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="COH_DESCRIPCION" Type="String" />
            <asp:Parameter Name="COH_FECHA" Type="DateTime" />
            <asp:SessionParameter Name="COH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="COH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="tbItems" Type="Object"  />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_responsable" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <%--<asp:Parameter Name="area" Type="String" />--%>
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
</asp:Content>
