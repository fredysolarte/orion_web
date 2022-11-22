<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TiposPedido.aspx.cs" Inherits="XUSS.WEB.Parametros.TiposPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_tpedido" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_tpedido_ItemInserted" 
            Width="100%" OnItemCommand="rlv_tpedido_OnItemCommand" OnItemDataBound="rlv_tpedido_OnItemDataBound" OnItemUpdating="rlv_tpedido_ItemUpdating"
            DataKeyNames="PTTIPPED" DataSourceID="obj_tpedido" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Tipos Pedido</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_tpedido" RenderMode="Lightweight"
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
                                <h5>Tipo Pedido</h5>
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
                                <label>Tipo Pedido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Text='<%# Bind("PTTIPPED") %>' Width="100px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("PTNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo Pedido</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Text='<%# Bind("PTDESCRI") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Basicos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Tenicos">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Bodegas">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>Moneda Principal</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                            Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMONEDA") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>Terminos Pago</label>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Modo Despacho</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_mdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                            Enabled="false" DataSourceID="obj_mdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMODDES") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>Terminos Despacho</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                            Enabled="false" DataSourceID="obj_tdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTTERDES") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Idioma Informe</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                            Enabled="false" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTIDIOMA") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>Bod Despacho</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("PTBODEGA") %>'
                                            DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Tran Inventario</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tmovimiento" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_tmovimiento" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("PTCDTRAN") %>'
                                            DataValueField="TMCDTRAN" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Lst Precio</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("PTLISPRE") %>'
                                            DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>T Factura</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tfactura" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFTIPFAC" SelectedValue='<%# Bind("PTTIPFAC") %>'
                                            DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>T Impuesto</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                            Enabled="false" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTCDIMPF") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <label>Parametros Pedido</label>
                                    </td>
                                    <td colspan="2">
                                        <label>Parametros Linea Pedido</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Clasificacion</label></td>
                                    <td>
                                        <label>Dato Tecnico</label></td>
                                    <td>
                                        <label>Clasificacion</label></td>
                                    <td>
                                        <label>Dato Tecnico</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>1.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA1") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect1" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC1") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>1.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD1") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect2" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD1") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>2.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA2") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect3" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC2") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>2.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD2") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect4" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD2") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>3.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA3") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect5" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC3") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>3.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD3") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect6" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD3") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>4.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA4") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect7" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC4") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>4.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD4") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect8" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD4") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>5.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA5") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect9" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC5") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>5.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD5") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect10" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD5") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>6.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion11" runat="server" Enabled="false" Text='<%# Bind("PTCDCLA6") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect11" runat="server" Enabled="false" Text='<%# Bind("PTDTTEC6") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>6.</label>
                                        <telerik:RadTextBox ID="txt_clasificacion12" runat="server" Enabled="false" Text='<%# Bind("PTCDCLD6") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dattect12" runat="server" Enabled="false" Text='<%# Bind("PTDTTCD6") %>' Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView3" runat="server">
                            <telerik:RadGrid ID="rgBodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Orden" UniqueName="corden" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_orden" runat="server" Enabled="false" DbValue='<%# Eval("BPORDEN") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton"
                                                HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                                                UniqueName="BDNOMBRE">
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
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Tipo Pedido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Text='<%# Bind("PTTIPPED") %>' Width="100px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("PTNOMBRE") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Tipo Pedido</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("PTDESCRI") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Datos Basicos">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Datos Tenicos">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Bodegas">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>Moneda Principal</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Terminos Pago</label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Modo Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_mdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_mdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMODDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Terminos Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_tdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTTERDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Idioma Informe</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTIDIOMA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Bod Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                        Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("PTBODEGA") %>'
                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Tran Inventario</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tmovimiento" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_tmovimiento" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("PTCDTRAN") %>'
                                        DataValueField="TMCDTRAN" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Lst Precio</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("PTLISPRE") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>T Factura</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tfactura" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFTIPFAC" SelectedValue='<%# Bind("PTTIPFAC") %>'
                                        DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>T Impuesto</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTCDIMPF") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>Parametros Pedido</label>
                                </td>
                                <td colspan="2">
                                    <label>Parametros Linea Pedido</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Clasificacion</label></td>
                                <td>
                                    <label>Dato Tecnico</label></td>
                                <td>
                                    <label>Clasificacion</label></td>
                                <td>
                                    <label>Dato Tecnico</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>1.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect1" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>1.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect2" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>2.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect3" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>2.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect4" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>3.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect5" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>3.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect6" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>4.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect7" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>4.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect8" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>5.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect9" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>5.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect10" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>6.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion11" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect11" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>6.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion12" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect12" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                            <telerik:RadGrid ID="rgBodegas" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("CHK")) %>' Enabled="true" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Orden" UniqueName="corden" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <telerik:RadNumericTextBox ID="txt_orden" runat="server" Enabled="true" DbValue='<%# Eval("BPORDEN") %>' Width="90%" NumberFormat-DecimalDigits="0">
                                                    </telerik:RadNumericTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="BDBODEGA" HeaderButtonType="TextButton"
                                                HeaderText="Cod Bod" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDBODEGA"
                                                UniqueName="BDBODEGA">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton"
                                                HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                                                UniqueName="BDNOMBRE">
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
                </telerik:RadMultiPage>
                <table>
                    <tr>
                        <td>
                            <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Tipo Pedido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="true" Text='<%# Bind("PTTIPPED") %>' Width="100px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("PTNOMBRE") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Tipo Pedido</label>
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("PTDESCRI") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Datos Basicos">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Datos Tenicos">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>Moneda Principal</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Terminos Pago</label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Modo Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_mdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_mdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTMODDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Terminos Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tdespacho" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_tdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTTERDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Idioma Informe</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTIDIOMA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Bod Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                        Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("PTBODEGA") %>'
                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Tran Inventario</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tmovimiento" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_tmovimiento" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("PTCDTRAN") %>'
                                        DataValueField="TMCDTRAN" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Lst Precio</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("PTLISPRE") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>T Factura</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tfactura" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFTIPFAC" SelectedValue='<%# Bind("PTTIPFAC") %>'
                                        DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>T Impuesto</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                        Enabled="true" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("PTCDIMPF") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>Parametros Pedido</label>
                                </td>
                                <td colspan="2">
                                    <label>Parametros Linea Pedido</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Clasificacion</label></td>
                                <td>
                                    <label>Dato Tecnico</label></td>
                                <td>
                                    <label>Clasificacion</label></td>
                                <td>
                                    <label>Dato Tecnico</label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label>1.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect1" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>1.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect2" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD1") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>2.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect3" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>2.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect4" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD2") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>3.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect5" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>3.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect6" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD3") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>4.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect7" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>4.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect8" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD4") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>5.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect9" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>5.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect10" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD5") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>6.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion11" runat="server" Enabled="true" Text='<%# Bind("PTCDCLA6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect11" runat="server" Enabled="true" Text='<%# Bind("PTDTTEC6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>6.</label>
                                    <telerik:RadTextBox ID="txt_clasificacion12" runat="server" Enabled="true" Text='<%# Bind("PTCDCLD6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_dattect12" runat="server" Enabled="true" Text='<%# Bind("PTDTTCD6") %>' Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <table>
                    <tr>
                        <td>
                            <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
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
    <asp:ObjectDataSource ID="obj_tpedido" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertTipoPedido" UpdateMethod="UpdateTipoPedido" OnInserted="obj_tpedido_Inserted"
        SelectMethod="GetTipPed" TypeName="XUSS.BLL.Parametros.TPedidoBL" OnInserting="obj_tpedido_Inserting" OnUpdating="obj_tpedido_Updating" OnUpdated="obj_tpedido_Updated">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="PTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="PTTIPPED" Type="String" />
            <asp:Parameter Name="PTNOMBRE" Type="String" />
            <asp:Parameter Name="PTDESCRI" Type="String" />
            <asp:Parameter Name="PTBODEGA" Type="String" />
            <asp:Parameter Name="PTTERPAG" Type="String" />
            <asp:Parameter Name="PTMODDES" Type="String" />
            <asp:Parameter Name="PTTERDES" Type="String" />
            <asp:Parameter Name="PTMONEDA" Type="String" />
            <asp:Parameter Name="PTIDIOMA" Type="String" />
            <asp:Parameter Name="PTLISPRE" Type="String" />
            <asp:Parameter Name="PTCDIMPF" Type="String" />
            <asp:Parameter Name="PTCDTRAN" Type="String" />
            <asp:Parameter Name="PTTIPFAC" Type="String" />
            <asp:Parameter Name="PTLSTSEP" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="PTCDCLA1" Type="String" />
            <asp:Parameter Name="PTCDCLA2" Type="String" />
            <asp:Parameter Name="PTCDCLA3" Type="String" />
            <asp:Parameter Name="PTCDCLA4" Type="String" />
            <asp:Parameter Name="PTCDCLA5" Type="String" />
            <asp:Parameter Name="PTCDCLA6" Type="String" />
            <asp:Parameter Name="PTDTTEC1" Type="String" />
            <asp:Parameter Name="PTDTTEC2" Type="String" />
            <asp:Parameter Name="PTDTTEC3" Type="String" />
            <asp:Parameter Name="PTDTTEC4" Type="String" />
            <asp:Parameter Name="PTDTTEC5" Type="String" />
            <asp:Parameter Name="PTDTTEC6" Type="String" />
            <asp:Parameter Name="PTCDCLD1" Type="String" />
            <asp:Parameter Name="PTCDCLD2" Type="String" />
            <asp:Parameter Name="PTCDCLD3" Type="String" />
            <asp:Parameter Name="PTCDCLD4" Type="String" />
            <asp:Parameter Name="PTCDCLD5" Type="String" />
            <asp:Parameter Name="PTCDCLD6" Type="String" />
            <asp:Parameter Name="PTDTTCD1" Type="String" />
            <asp:Parameter Name="PTDTTCD2" Type="String" />
            <asp:Parameter Name="PTDTTCD3" Type="String" />
            <asp:Parameter Name="PTDTTCD4" Type="String" />
            <asp:Parameter Name="PTDTTCD5" Type="String" />
            <asp:Parameter Name="PTDTTCD6" Type="String" />
            <asp:Parameter Name="PTESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="PTCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="PTNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="PTCOTIZA" Type="String" />
            <asp:Parameter Name="tbBodegas" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="PTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="original_PTTIPPED" Type="String" />
            <asp:Parameter Name="PTNOMBRE" Type="String" />
            <asp:Parameter Name="PTDESCRI" Type="String" />
            <asp:Parameter Name="PTBODEGA" Type="String" />
            <asp:Parameter Name="PTTERPAG" Type="String" />
            <asp:Parameter Name="PTMODDES" Type="String" />
            <asp:Parameter Name="PTTERDES" Type="String" />
            <asp:Parameter Name="PTMONEDA" Type="String" />
            <asp:Parameter Name="PTIDIOMA" Type="String" />
            <asp:Parameter Name="PTLISPRE" Type="String" />
            <asp:Parameter Name="PTCDIMPF" Type="String" />
            <asp:Parameter Name="PTCDTRAN" Type="String" />
            <asp:Parameter Name="PTTIPFAC" Type="String" />
            <asp:Parameter Name="PTLSTSEP" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="PTCDCLA1" Type="String" />
            <asp:Parameter Name="PTCDCLA2" Type="String" />
            <asp:Parameter Name="PTCDCLA3" Type="String" />
            <asp:Parameter Name="PTCDCLA4" Type="String" />
            <asp:Parameter Name="PTCDCLA5" Type="String" />
            <asp:Parameter Name="PTCDCLA6" Type="String" />
            <asp:Parameter Name="PTDTTEC1" Type="String" />
            <asp:Parameter Name="PTDTTEC2" Type="String" />
            <asp:Parameter Name="PTDTTEC3" Type="String" />
            <asp:Parameter Name="PTDTTEC4" Type="String" />
            <asp:Parameter Name="PTDTTEC5" Type="String" />
            <asp:Parameter Name="PTDTTEC6" Type="String" />
            <asp:Parameter Name="PTCDCLD1" Type="String" />
            <asp:Parameter Name="PTCDCLD2" Type="String" />
            <asp:Parameter Name="PTCDCLD3" Type="String" />
            <asp:Parameter Name="PTCDCLD4" Type="String" />
            <asp:Parameter Name="PTCDCLD5" Type="String" />
            <asp:Parameter Name="PTCDCLD6" Type="String" />
            <asp:Parameter Name="PTDTTCD1" Type="String" />
            <asp:Parameter Name="PTDTTCD2" Type="String" />
            <asp:Parameter Name="PTDTTCD3" Type="String" />
            <asp:Parameter Name="PTDTTCD4" Type="String" />
            <asp:Parameter Name="PTDTTCD5" Type="String" />
            <asp:Parameter Name="PTDTTCD6" Type="String" />
            <asp:Parameter Name="PTESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="PTCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="PTNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="PTCOTIZA" Type="String" />
            <asp:Parameter Name="tbBodegas" Type="Object" />
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
    <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_idioma" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="LANG" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_mdespacho" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MODE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tdespacho" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEDE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tmovimiento" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
    <asp:ObjectDataSource ID="obj_impuesto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="IMPF" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
