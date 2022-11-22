<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LstPrecios.aspx.cs" Inherits="XUSS.WEB.Parametros.LstPrecios" %>

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
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_lstprecios$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback"  ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_lstprecios" runat="server" PageSize="1" AllowPaging="True" DataKeyNames="P_CLISPRE"
            Width="100%" OnItemCommand="rlv_lstprecios_OnItemCommand" OnItemDataBound="rlv_lstprecios_OnItemDataBound"
            DataSourceID="obj_lstprecios" ItemPlaceholderID="pnlGeneral" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Lista de Precio</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_lstprecios" RenderMode="Lightweight"
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
                                <h5>Lista de Precio</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />                                                    --%>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td><label>Codigo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codlista" runat="server" Enabled="true"  Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true"  Width="300px">
                                    </telerik:RadTextBox>
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
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Cod. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codlista" runat="server" Enabled="false" Text='<%# Bind("P_CLISPRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nom. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("P_CNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Moneda</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("P_CMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("P_CDESCRI") %>'
                                                            Width="600px" TextMode="MultiLine" Height="40px">
                                                        </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>T Lista</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tlista" runat="server" Culture="es-CO"  Width="300px" Enabled="false"
                                     SelectedValue='<%# Bind("P_CTIPLIS") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="." />
                                        <telerik:RadComboBoxItem Text="Lista con Impuestos" Value="1" />
                                        <telerik:RadComboBoxItem Text="Lista sin Impuestos" Value="2" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                    SelectedValue='<%# Bind("P_CESTADO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                            Culture="(Default)" CellSpacing="0" OnInfrastructureExporting="rg_items_InfrastructureExporting">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" RefreshText="Buscar" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="P_RITEMLST" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_RITEMLST"
                                        UniqueName="P_RITEMLST">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="P_RCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="120px" DataType="System.String"
                                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_RCLAVE1"
                                        UniqueName="P_RCLAVE1">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn DataField="P_RCLAVE1" HeaderText="Referencia" UniqueName="P_RCLAVE1_TK"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="P_RCLAVE1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("P_RCLAVE1") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="link" UniqueName="P_RCLAVE1" DataTextField="P_RCLAVE1"
                                        HeaderText="Referencia" HeaderStyle-Width="120px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2"
                                        UniqueName="C2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3"
                                        UniqueName="C3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="P_RPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        DataFormatString="{0:C}" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="P_RPRECIO" UniqueName="P_RPRECIO">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <asp:Panel ID="pnlBuscar" runat="server" Visible="false">
                        <div class="box">
                            <div class="title">
                                <h5>Detalle Lista Precios</h5>
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
                                        <%--<telerik:RadComboBox ID="rc_categoria" runat="server"
                                            Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                            Enabled="true" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>--%>

                                        <telerik:RadComboBox ID="rc_categoria" runat="server" DataSourceID="obj_tippro" Width="300px" CheckBoxes="true"
                                            DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
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
                                        <telerik:RadButton ID="btn_cargar" runat="server" Text="Cargar" OnClick="btn_cargar_OnClick" CommandName="UploadPr" Icon-PrimaryIconCssClass="rbUpload" RenderMode="Lightweight">
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="btn_buscar_det" runat="server" Text="Filtro" OnClick="btn_buscar_det_OnClick" CommandName="FindDet" Icon-PrimaryIconCssClass="rbRefresh" RenderMode="Lightweight">
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
                                <label>Cod. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codlista" runat="server" Enabled="false" Text='<%# Bind("P_CLISPRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nom. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("P_CNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Moneda</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("P_CMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("P_CDESCRI") %>'
                                                            Width="600px" TextMode="MultiLine" Height="40px">
                                                        </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>T Lista</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tlista" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("P_CTIPLIS") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="." />
                                        <telerik:RadComboBoxItem Text="Lista con Impuestos" Value="1" />
                                        <telerik:RadComboBoxItem Text="Lista sin Impuestos" Value="2" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                    SelectedValue='<%# Bind("P_CESTADO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" AddNewRecordText="Nuevo Item" ShowRefreshButton="true" RefreshText="Buscar" />
                                <Columns>
                                    <telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif"  ButtonType="ImageButton" />                                    
                                    <telerik:GridBoundColumn DataField="P_RITEMLST" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_RITEMLST"
                                        UniqueName="P_RITEMLST">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="P_RCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_RCLAVE1"
                                        UniqueName="P_RCLAVE1">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn DataField="P_RCLAVE1" HeaderText="Referencia" UniqueName="P_RCLAVE1_TK"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="P_RCLAVE1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("P_RCLAVE1") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="link" UniqueName="P_RCLAVE1" DataTextField="P_RCLAVE1"
                                        HeaderText="Referencia" HeaderStyle-Width="120px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2"
                                        UniqueName="C2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="C3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3"
                                        UniqueName="C3">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="P_RPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        DataFormatString="{0:C}" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="P_RPRECIO" UniqueName="P_RPRECIO">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Visible="false" Text='<%# Bind("P_RTIPPRO") %>'>
                                                        </telerik:RadTextBox>
                                                        <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Visible="false" Text='<%# Bind("P_RCLAVE2") %>'>
                                                        </telerik:RadTextBox>
                                                        <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Visible="false" Text='<%# Bind("P_RCLAVE3") %>'>
                                                        </telerik:RadTextBox>
                                                        <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Visible="false" Text='<%# Bind("P_RCLAVE4") %>'>
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Referencia</label></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Visible="true" ValidationGroup="grNuevo" Text='<%# Bind("P_RCLAVE1") %>'>
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txt_referencia"
                                                            ErrorMessage="(*)" ValidationGroup="grNuevo">
                                                            <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Producto</label>
                                                    </td>
                                                    <td colspan="3">
                                                        <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Visible="true" Width="450px" Text='<%# Bind("ARNOMBRE") %>'>
                                                        </telerik:RadTextBox>
                                                        <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />
                                                    </td>
                                                    <td>
                                                        <label>Precio</label>
                                                    </td>
                                                    <td><telerik:RadNumericTextBox ID="txt_precio" runat="server" Enabled="true" 
                                                            DbValue='<%# Bind("P_RPRECIO") %>' NumberFormat-DecimalDigits="0" 
                                                            Width="300px">
                                                        </telerik:RadNumericTextBox>                                                         
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                            ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                            CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:ModalPopupExtender ID="mpArticulos" runat="server" BackgroundCssClass="modalBackground"
                                            BehaviorID="MEditTicket" PopupControlID="pnFindArticulos" TargetControlID="Button5"
                                            CancelControlID="Button8">
                                        </asp:ModalPopupExtender>
                                        <div style="display: none;">
                                            <asp:Button ID="Button5" runat="server" Text="Button" />
                                        </div>
                                        <asp:Panel ID="pnFindArticulos" runat="server" CssClass="modalPopup" Style="display: none;">
                                            <fieldset class="cssFieldSetContainer" style="width: 1000px !important">
                                                <div class="box">
                                                    <div class="title">
                                                        <h5>Buscar Articulos</h5>
                                                    </div>
                                                </div>
                                                <div style="padding: 5px 5px 5px 5px">
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
                                                                <asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick"
                                                                    CommandName="xxxxxx" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="Panel13" runat="server">
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
                                                                    <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE3" HeaderText="" Visible="true"
                                                                        UniqueName="ARCLAVE3" HeaderButtonType="TextButton" DataField="ARCLAVE3" ItemStyle-HorizontalAlign="Right"
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
                                                    <div style="text-align: center;">
                                                        <asp:Button ID="Button8" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </asp:Panel>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlBuscar" runat="server" Visible="false">
                        <div class="box">
                            <div class="title">
                                <h5>Detalle Lista Precios</h5>
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
                                        <telerik:RadButton ID="btn_buscar_det" runat="server" Text="Buscar" OnClick="btn_buscar_det_OnClick" CommandName="FindDet">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </fieldset>
                </asp:Panel>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Cod. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codlista" runat="server" Enabled="true" Text='<%# Bind("P_CLISPRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nom. Lista</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("P_CNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Moneda</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("P_CMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("P_CDESCRI") %>'
                                                            Width="600px" TextMode="MultiLine" Height="40px">
                                                        </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>T Lista</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tlista" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("P_CTIPLIS") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="." />
                                        <telerik:RadComboBoxItem Text="Lista con Impuestos" Value="1" />
                                        <telerik:RadComboBoxItem Text="Lista sin Impuestos" Value="2" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                    SelectedValue='<%# Bind("P_CESTADO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                    ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargar Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rbl_tiparch" runat="server" RepeatDirection="Horizontal" >
                                        <asp:ListItem Selected="True">Referencia + C2 + C3 + C4</asp:ListItem>
                                        <asp:ListItem>Cod. Barras</asp:ListItem>                                
                                    </asp:RadioButtonList>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_OnClick" />
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

    <asp:ObjectDataSource ID="obj_lstprecios" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertListaPrecioHD"
        SelectMethod="GetListaPrecioHD" TypeName="XUSS.BLL.ListaPrecios.ListaPreciosBL" UpdateMethod="UpdateListaPrecioHD">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="P_CCODEMP" Type="String" SessionField="CODEMP" />            
            <asp:Parameter Name="P_CFECPRE" Type="DateTime" DefaultValue="01/01/2010" />
            <asp:Parameter Name="P_CNOMBRE" Type="String" />
            <asp:Parameter Name="P_CDESCRI" Type="String" />
            <asp:Parameter Name="P_CMONEDA" Type="String" />
            <asp:Parameter Name="P_CCLIPRO" Type="String" DefaultValue="." />
            <asp:Parameter Name="P_CTIPLIS" Type="String" DefaultValue="." />
            <asp:Parameter Name="P_CREDOND" Type="Double" DefaultValue="1000"/>
            <asp:Parameter Name="P_CESTADO" Type="String" />
            <asp:Parameter Name="P_CCAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="P_CNMUSER" Type="String" SessionField="UserLogon" />         
            <asp:Parameter Name="original_P_CLISPRE" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="P_CCODEMP" Type="String" SessionField="CODEMP" />            
            <asp:Parameter Name="P_CLISPRE" Type="String" />
            <asp:Parameter Name="P_CFECPRE" Type="DateTime" DefaultValue="01/01/2010" />
            <asp:Parameter Name="P_CNOMBRE" Type="String" />
            <asp:Parameter Name="P_CDESCRI" Type="String" />
            <asp:Parameter Name="P_CMONEDA" Type="String" />
            <asp:Parameter Name="P_CCLIPRO" Type="String" DefaultValue="." />
            <asp:Parameter Name="P_CTIPLIS" Type="String" DefaultValue="." />
            <asp:Parameter Name="P_CREDOND" Type="Double" DefaultValue="1000"/>
            <asp:Parameter Name="P_CESTADO" Type="String" />
            <asp:Parameter Name="P_CCAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="P_CNMUSER" Type="String" SessionField="UserLogon" />                     
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
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
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
     SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
     <SelectParameters>
        <asp:Parameter Name="connection" Type="String" DefaultValue="" />
     </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
