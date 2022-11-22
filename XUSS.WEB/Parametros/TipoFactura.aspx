<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TipoFactura.aspx.cs" Inherits="XUSS.WEB.Parametros.TipoFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_tfactura" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rlv_tfactura_OnItemUpdating"
            Width="100%" OnItemCommand="rlv_tfactura_OnItemCommand" OnItemDataBound="rlv_tfactura_OnItemDataBound"
            DataKeyNames="TFTIPFAC" DataSourceID="obj_tfactura" ItemPlaceholderID="pnlGeneral" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Tipos de Factura</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_tfactura" RenderMode="Lightweight"
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
                                <h5>Tipos de Facturas</h5>
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
                                    <label>C Documento</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Facturacion Directa" Value="1" />
                                            <telerik:RadComboBoxItem Text="Devolucion" Value="2" />
                                            <telerik:RadComboBoxItem Text="Separados" Value="3" />
                                            <telerik:RadComboBoxItem Text="Venta Bonos" Value="4" />
                                            <telerik:RadComboBoxItem Text="Obsequios/Bonos Redimibles" Value="5" />
                                            <telerik:RadComboBoxItem Text="Garantias" Value="6" />
                                            <telerik:RadComboBoxItem Text="Gastos" Value="7" />
                                            <telerik:RadComboBoxItem Text="Notas Credito" Value="8" />
                                            <telerik:RadComboBoxItem Text="Notas Debito" Value="9" />
                                            <telerik:RadComboBoxItem Text="Comprobante Contabilidad" Value="10" />                                            
                                            <telerik:RadComboBoxItem Text="Facturacion Exportacion" Value="11" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>

                                <td>
                                    <label>Bodega</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE"
                                        DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
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
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Tipo Factura</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tfactura" runat="server" Enabled="false" Text='<%# Bind("TFTIPFAC") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Documento</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                    SelectedValue='<%# Bind("TFCLAFAC") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturacion Directa" Value="1" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="2" />
                                        <telerik:RadComboBoxItem Text="Separados" Value="3" />
                                        <telerik:RadComboBoxItem Text="Venta Bonos" Value="4" />
                                        <telerik:RadComboBoxItem Text="Obsequios/Bonos Redimibles" Value="5" />
                                        <telerik:RadComboBoxItem Text="Garantias" Value="6" />
                                        <telerik:RadComboBoxItem Text="Gastos" Value="7" />
                                        <telerik:RadComboBoxItem Text="Notas Credito" Value="8" />
                                        <telerik:RadComboBoxItem Text="Notas Debito" Value="9" />
                                        <telerik:RadComboBoxItem Text="Comprobante Contabilidad" Value="10" />        
                                        <telerik:RadComboBoxItem Text="Facturacion Exportacion" Value="11" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("TFNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>

                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TFBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Transaccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_transaccion" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("TFCDTRAN") %>'
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
                                    Enabled="false" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TFLSTPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Factura</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrofactura" runat="server" Width="300px" Enabled="false" Text='<%# Bind("TFNROFAC") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Prefijo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_prefijo" runat="server" Width="300px" Enabled="false" Text='<%# Bind("TFPREFIJ") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Reporte</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_reportes" runat="server" DataSourceID="obj_reportes" Width="300px"
                                    DataTextField="Nombre" DataValueField="RR_CODIGO" SelectedValue='<%# Bind("TFFORFAC") %>'
                                    CausesValidation="False" AppendDataBoundItems="true" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Selecionar" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Nro Max Items</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nromaxitems" runat="server" Width="300px" Enabled="false" Text='<%# Bind("TFMAXITM") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Ult Factura</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fufactura" runat="server" DbSelectedDate='<%# Bind("TFFECFAC") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Resolucion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Usuario x TF">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_usuarioxtf" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="RFNRORES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Nro Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFNRORES"
                                                UniqueName="RFNRORES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFECRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFECRES"
                                                UniqueName="RFFECRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFTIPRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="T. Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFTIPRES"
                                                UniqueName="RFTIPRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACINI" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Desde" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACINI"
                                                UniqueName="RFFACINI">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACFIN" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Hasta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACFIN"
                                                UniqueName="RFFACFIN">
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
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadGrid ID="rgUsuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" RenderMode="Lightweight">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("FUESTADO")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="usua_usuario" HeaderButtonType="TextButton"
                                                HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_usuario"
                                                UniqueName="usua_usuario">
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
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Tipo Factura</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tfactura" runat="server" Enabled="true" Text='<%# Bind("TFTIPFAC") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Documento</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                    SelectedValue='<%# Bind("TFCLAFAC") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturacion Directa" Value="1" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="2" />
                                        <telerik:RadComboBoxItem Text="Separados" Value="3" />
                                        <telerik:RadComboBoxItem Text="Venta Bonos" Value="4" />
                                        <telerik:RadComboBoxItem Text="Obsequios/Bonos Redimibles" Value="5" />
                                        <telerik:RadComboBoxItem Text="Garantias" Value="6" />
                                        <telerik:RadComboBoxItem Text="Gastos" Value="7" />
                                        <telerik:RadComboBoxItem Text="Notas Credito" Value="8" />
                                        <telerik:RadComboBoxItem Text="Notas Debito" Value="9" />
                                        <telerik:RadComboBoxItem Text="Comprobante Contabilidad" Value="10" />             
                                        <telerik:RadComboBoxItem Text="Facturacion Exportacion" Value="11" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TFNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>

                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TFBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Transaccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_transaccion" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("TFCDTRAN") %>'
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
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TFLSTPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>

                                <td>
                                    <label>Nro Factura</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofactura" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFNROFAC") %>'>
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>Prefijo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_prefijo" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFPREFIJ") %>'>
                                    </telerik:RadTextBox>
                                </td>
                        </tr>                        
                        <tr>
                            <td>
                                <label>Reporte</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_reportes" runat="server" DataSourceID="obj_reportes" Width="300px"
                                    DataTextField="Nombre" DataValueField="RR_CODIGO" SelectedValue='<%# Bind("TFFORFAC") %>'
                                    CausesValidation="False" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Selecionar" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Nro Max Items</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nromaxitems" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFMAXITM") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Ult Factura</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fufactura" runat="server" DbSelectedDate='<%# Bind("TFFECFAC") %>'
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>                            
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Resolucion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Usuario x TF">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_usuarioxtf" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_OnNeedDataSource">
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="RFNRORES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Nro Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFNRORES"
                                                UniqueName="RFNRORES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFECRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFECRES"
                                                UniqueName="RFFECRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFTIPRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="T. Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFTIPRES"
                                                UniqueName="RFTIPRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACINI" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Desde" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACINI"
                                                UniqueName="RFFACINI">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACFIN" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Hasta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACFIN"
                                                UniqueName="RFFACFIN">
                                            </telerik:GridBoundColumn>

                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Nro Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_resolucion" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>T Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_tipores" runat="server" Culture="es-CO" Width="680px"
                                                                    ValidationGroup="gvInsert" Enabled="true" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                        <telerik:RadComboBoxItem Text="Autoriza" Value="A" />
                                                                        <telerik:RadComboBoxItem Text="Habilita" Value="H" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>Fec Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="edt_fresolucion" runat="server" MinDate="01/01/1900">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>Fac Inicial</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_finicial" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                            <td>
                                                                <label>Fac Final</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_ffinal" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                    CausesValidation="false" />
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
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadGrid ID="rgUsuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgUsuarios_OnNeedDataSource">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("FUESTADO")) %>' Enabled="true" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="usua_usuario" HeaderButtonType="TextButton"
                                                HeaderText="Uuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_usuario"
                                                UniqueName="usua_usuario">
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
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Tipo Factura</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tfactura" runat="server" Enabled="true" Text='<%# Bind("TFTIPFAC") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Documento</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                    SelectedValue='<%# Bind("TFCLAFAC") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturacion Directa" Value="1" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="2" />
                                        <telerik:RadComboBoxItem Text="Separados" Value="3" />
                                        <telerik:RadComboBoxItem Text="Venta Bonos" Value="4" />
                                        <telerik:RadComboBoxItem Text="Obsequios/Bonos Redimibles" Value="5" />
                                        <telerik:RadComboBoxItem Text="Garantias" Value="6" />
                                        <telerik:RadComboBoxItem Text="Gastos" Value="7" />
                                        <telerik:RadComboBoxItem Text="Notas Credito" Value="8" />
                                        <telerik:RadComboBoxItem Text="Notas Debito" Value="9" />
                                        <telerik:RadComboBoxItem Text="Comprobante Contabilidad" Value="10" />       
                                        <telerik:RadComboBoxItem Text="Facturacion Exportacion" Value="11" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TFNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>

                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TFBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Transaccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_transaccion" DataTextField="TMNOMBRE" SelectedValue='<%# Bind("TFCDTRAN") %>'
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
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TFLSTPRE") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <tr>
                                <td>
                                    <label>Nro Factura</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofactura" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFNROFAC") %>'>
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>Prefijo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_prefijo" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFPREFIJ") %>'>
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </tr>                        
                        <tr>
                            <td>
                                <label>Reporte</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_reportes" runat="server" DataSourceID="obj_reportes" Width="300px"
                                    DataTextField="Nombre" DataValueField="RR_CODIGO" SelectedValue='<%# Bind("TFFORFAC") %>'
                                    CausesValidation="False" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Selecionar" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Nro Max Items</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nromaxitems" runat="server" Width="300px" Enabled="true" Text='<%# Bind("TFMAXITM") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Ult Factura</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fufactura" runat="server" DbSelectedDate='<%# Bind("TFFECFAC") %>'
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Resolucion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Usuario x TF">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_usuarioxtf" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_OnNeedDataSource">
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="RFNRORES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Nro Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFNRORES"
                                                UniqueName="RFNRORES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFECRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFECRES"
                                                UniqueName="RFFECRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFTIPRES" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="T. Resolucion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFTIPRES"
                                                UniqueName="RFTIPRES">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACINI" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Desde" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACINI"
                                                UniqueName="RFFACINI">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RFFACFIN" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                HeaderText="Fac Hasta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RFFACFIN"
                                                UniqueName="RFFACFIN">
                                            </telerik:GridBoundColumn>

                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Nro Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_resolucion" runat="server" Enabled="false" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>T Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_sucursal" runat="server" Culture="es-CO" Width="680px"
                                                                    ValidationGroup="gvInsert" Enabled="true" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                        <telerik:RadComboBoxItem Text="Autoriza" Value="A" />
                                                                        <telerik:RadComboBoxItem Text="Habilita" Value="H" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>Fec Resolucion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="edt_fresolucion" runat="server" MinDate="01/01/1900">
                                                                </telerik:RadDatePicker>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>Fac Inicial</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_finicial" runat="server" Enabled="false" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                            <td>
                                                                <label>Fac Final</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_ffinal" runat="server" Enabled="false" Visible="true" ValidationGroup="grNuevo">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                    CausesValidation="false" />
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
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadGrid ID="rgUsuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0">
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("FUESTADO")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="usua_usuario" HeaderButtonType="TextButton"
                                                HeaderText="Uuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_usuario"
                                                UniqueName="usua_usuario">
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
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_tfactura" runat="server" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateTipoFactura" InsertMethod="InsertTipoFactura"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL" OnUpdating="obj_tfactura_OnUpdating">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TFCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="original_TFTIPFAC" Type="String" />
            <asp:Parameter Name="TFCLAFAC" Type="String" />
            <asp:Parameter Name="TFNOMBRE" Type="String" />
            <asp:Parameter Name="TFNROFAC" Type="Int32" />
            <asp:Parameter Name="TFFECFAC" Type="DateTime" />
            <asp:Parameter Name="TFBODEGA" Type="String" />
            <asp:Parameter Name="TFCDTRAN" Type="String" />
            <asp:Parameter Name="TFEXPORT" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TFESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TFCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TFNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TFLSTPRE" Type="String" />
            <asp:Parameter Name="TFPREFIJ" Type="String" />
            <asp:Parameter Name="TFFORFAC" Type="String" />
            <asp:Parameter Name="TFMAXITM" Type="Int32" />            
            <asp:Parameter Name="tbUsuarios" Type="Object" />
            <asp:Parameter Name="tbResolucion" Type="Object" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TFCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TFTIPFAC" Type="String" />
            <asp:Parameter Name="TFCLAFAC" Type="String" />
            <asp:Parameter Name="TFNOMBRE" Type="String" />
            <asp:Parameter Name="TFNROFAC" Type="Int32" />
            <asp:Parameter Name="TFFECFAC" Type="DateTime" />
            <asp:Parameter Name="TFBODEGA" Type="String" />
            <asp:Parameter Name="TFCDTRAN" Type="String" />
            <asp:Parameter Name="TFEXPORT" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TFESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TFCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TFNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TFLSTPRE" Type="String" />
            <asp:Parameter Name="TFPREFIJ" Type="String" />
            <asp:Parameter Name="TFREPORT" Type="String" />
            <asp:Parameter Name="TFFORFAC" Type="String" />
            <asp:Parameter Name="TFMAXITM" Type="Int32" />
        </InsertParameters>
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
    <asp:ObjectDataSource ID="obj_lstprecio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetListaPrecioHD" TypeName="XUSS.BLL.ListaPrecios.ListaPreciosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_transaccion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_reportes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetlstReportes" TypeName="BLL.Administracion.AdmiOpcionBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
