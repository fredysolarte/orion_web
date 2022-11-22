<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TransformacionTester.aspx.cs" Inherits="XUSS.WEB.Inventarios.TransformacionTester" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <style type="text/css">
            .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6 {
                padding: 0px 10px 10px 30px;
            }
        </style>
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                debugger;
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="100000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_movimientos" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_movimientos_ItemInserted"
            OnItemCommand="rlv_movimientos_OnItemCommand" OnItemDataBound="rlv_movimientos_OnItemDataBound"
            DataSourceID="obj_movimientos" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Transformacion Tester</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_movimientos" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Transformacion Tester</h5>
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
                                    <label>Nro Movimiento</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_movimiento" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>F Movimiento</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txt_fSolicitud" runat="server" MinDate="01/01/1900" Enabled="true">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Bodega</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Movimiento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_movimiento" runat="server" Enabled="false" Width="300px" Text='<%# Bind("TR_NROTRA") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Movimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TR_FECTRA") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TR_BODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Mov Salida</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_msalida" runat="server" Enabled="false" Width="300px" Text='<%# Bind("TR_MOVSAL") %>'>
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Mov Entrada</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_mentrada" runat="server" Enabled="false" Width="300px" Text='<%# Bind("TR_MOVENT") %>'>
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("TR_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                <asp:Panel runat="server" ID="pnl_detalle">
                    <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                        <telerik:RadPane ID="LeftPane" runat="server" Width="50%">
                            <telerik:RadGrid ID="rg_regular" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true" DataKeyNames="MBIDMOVI,MBIDITEM">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                            UniqueName="MBIDITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                            UniqueName="MBCLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                            UniqueName="MBCANTID">
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
                        </telerik:RadPane>
                        <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                        </telerik:RadSplitBar>
                        <telerik:RadPane ID="RadPane1" runat="server" Width="50%">
                            <telerik:RadGrid ID="rg_tester" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_tester_OnNeedDataSource"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true" DataKeyNames="MBIDMOVI,MBIDITEM">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                            UniqueName="MBIDITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                            UniqueName="MBCLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                            UniqueName="MBCANTID">
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
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </asp:Panel>
                </asp:Panel>

            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Movimiento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_movimiento" runat="server" Enabled="false" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F Movimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TR_FECTRA") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("TR_BODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_bodega" InitialValue="Seleccionar"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Transaccion S</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccions" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_transaccions" DataTextField="TMNOMBRE"
                                    DataValueField="TMCDTRAN" SelectedValue='<%# Bind("TR_CDTRAN") %>'>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Transaccion E</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccione" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_transaccione" DataTextField="TMNOMBRE"
                                    DataValueField="TMCDTRAN" SelectedValue='<%# Bind("TR_OTTRAN") %>'>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("TR_COMENT") %>'
                                    Width="600px" TextMode="MultiLine" Height="60px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton RenderMode="Lightweight" ID="btn_ndetalle" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_ndetalle_Click" ValidationGroup="gvInsert" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnl_detalle">
                    <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                        <telerik:RadPane ID="LeftPane" runat="server" Width="50%">
                            <telerik:RadGrid ID="rg_regular" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true" DataKeyNames="MBIDMOVI,MBIDITEM">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                            UniqueName="MBIDITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                            UniqueName="MBCLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                            UniqueName="MBCANTID">
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
                        </telerik:RadPane>
                        <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                        </telerik:RadSplitBar>
                        <telerik:RadPane ID="RadPane1" runat="server" Width="50%">
                            <telerik:RadGrid ID="rg_tester" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_tester_OnNeedDataSource"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true" DataKeyNames="MBIDMOVI,MBIDITEM">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="MBIDITEM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                            HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBIDITEM"
                                            UniqueName="MBIDITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                            UniqueName="MBCLAVE1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                            UniqueName="CLAVE2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Clave 3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                            UniqueName="CLAVE3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                            HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCANTID"
                                            UniqueName="MBCANTID">
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
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </asp:Panel>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
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
                                    <telerik:RadTextBox ID="txt_nommarca" runat="server" Enabled="true" Visible="false">
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
                                        <%--<asp:Button ID="btn_agregar" runat="server" Text="Aceptar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />--%>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" ToolTip="Nuevo Registro" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFiltroArt" runat="server" Width="950px" Height="560px" Modal="true" OffsetElementID="main" Style="z-index: 1900;" Title="Filtro Articulos">
                    <ContentTemplate>
                        <asp:Panel ID="pn_filart" runat="server" DefaultButton="btn_filtroArticulos" Width="100%">
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
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" CommandName="xxxxxx" ToolTip="Filtrar" OnClick="btn_filtroArticulos_OnClick" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Width="100%">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%" AllowFilteringByColumn="True" AllowSorting="true"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="5" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_articulos" OnItemCommand="rgConsultaArticulos_OnItemCommand">
                                <FilterMenu Style="z-index: 2001;"></FilterMenu>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Sel" CommandName="Select">
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARTIPPRO" HeaderText="" Visible="true" AllowFiltering="false"
                                            UniqueName="ARTIPPRO" HeaderButtonType="None" DataField="ARTIPPRO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="TANOMBRE" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="TANOMBRE" UniqueName="TANOMBRE">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_marca" runat="server" Visible="false" Text='<%# Bind("TANOMBRE") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE1" HeaderText="Referencia" FilterControlWidth="45px"
                                            UniqueName="ARCLAVE1" HeaderButtonType="TextButton" DataField="ARCLAVE1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
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
                                        <telerik:GridTemplateColumn DataField="ARCLAVE4" HeaderText="" HeaderStyle-Width="80px" Visible="false" AllowFiltering="false"
                                            Resizable="true" SortExpression="ARCLAVE4" UniqueName="ARCLAVE4">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave4" runat="server" Visible="false" Text='<%# Bind("ARCLAVE4") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre" FilterControlWidth="200px"
                                            UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="400px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="C2" Visible="true" FilterControlWidth="25px"
                                            UniqueName="CLAVE2" HeaderButtonType="None" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="20px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="C3" Visible="true" FilterControlWidth="85px"
                                            UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC4" HeaderText="" AllowFiltering="false"
                                            UniqueName="ARDTTEC4" HeaderButtonType="TextButton" DataField="ARDTTEC4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC1" HeaderText="" AllowFiltering="false"
                                            UniqueName="ARDTTEC1" HeaderButtonType="TextButton" DataField="ARDTTEC1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PRECIO" HeaderText="Precio Lts" DataFormatString="{0:0.0}" AllowFiltering="false"
                                            UniqueName="PRECIO" HeaderButtonType="TextButton" DataField="PRECIO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="DESCUENTO" HeaderText="Dcto" DataFormatString="{0:0.#}" AllowFiltering="false"
                                            UniqueName="DESCUENTO" HeaderButtonType="TextButton" DataField="DESCUENTO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Can" DataFormatString="{0:0.#}" AllowFiltering="false"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
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
    <asp:ObjectDataSource ID="obj_movimientos" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertTransformacion" OnInserting="obj_movimientos_OnInserting"
        SelectMethod="GetTransformacion" TypeName="XUSS.BLL.Inventarios.TransformacionBL" OnInserted="obj_movimientos_OnInserted">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TR_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TR_NROTRA" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TR_FECTRA" Type="DateTime" DefaultValue="01/01/2000" />
            <asp:Parameter Name="TR_BODEGA" Type="String" />
            <asp:Parameter Name="TR_CDTRAN" Type="String" />
            <asp:Parameter Name="TR_OTTRAN" Type="String" />
            <asp:Parameter Name="TR_MOVENT" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TR_MOVSAL" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TR_COMENT" Type="String" />
            <asp:Parameter Name="TR_ESTADO" Type="String" DefaultValue="CE" />
            <asp:Parameter Name="TR_CAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TR_NMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="P_CLISPRE" Type="String" />
            <asp:Parameter Name="tbitems" Type="Object" />
            <asp:Parameter Name="tbTester" Type="Object" />
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
    <asp:ObjectDataSource ID="obj_transaccions" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" TMCDTRAN='18'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_transaccione" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" TMCDTRAN='19'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
</asp:Content>
