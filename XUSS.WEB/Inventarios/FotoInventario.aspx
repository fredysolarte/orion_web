<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="FotoInventario.aspx.cs" Inherits="XUSS.WEB.Inventarios.FotoInventario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_foto$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
        <telerik:RadListView ID="rlv_foto" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_foto_OnItemInserted" OnItemInserting="rlv_foto_OnItemInserting"
            OnItemCommand="rlv_foto_OnItemCommand" OnItemDataBound="rlv_foto_OnItemDataBound"
            DataSourceID="obj_foto" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Fotos Inventario</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_foto" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Fotos Inventario</h5>
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
                                    <label>Nro Foto</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofoto" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Nro Foto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrofoto" runat="server" Enabled="false" Text='<%# Bind("FINROFOT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>F. Foto</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecfoto" runat="server" DbSelectedDate='<%# Bind("FIFECING") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Bodega</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("FIBODEGA") %>'
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Inv Inicial</label></td>
                            <td>
                                <asp:CheckBox ID="chk_invInicial" runat="server" Checked='<%# this.GetEstado(Eval("FIINVINI")) %>' Enabled="false" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" AllowPaging="false" ShowGroupPanel="True"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_NeedDataSource">
                            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FBCLAVE1"
                                        UniqueName="FBCLAVE1">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                        UniqueName="CLAVE2">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                        UniqueName="CLAVE3">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="40px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;"
                                        HeaderText="Cant" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FBCANTID"
                                        UniqueName="FBCANTID" FooterText="Total: " Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </asp:Panel>

            </ItemTemplate>
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Nro Foto</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nrofoto" runat="server" Enabled="false" Text='<%# Eval("FINROFOT") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>F. Foto</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecfoto" runat="server" DbSelectedDate='<%# Eval("FIFECING") %>'
                                MinDate="01/01/1900" Enabled="false">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Bodega</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" SelectedValue='<%# Bind("FIBODEGA") %>'
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Inv Inicial</label></td>
                        <td>
                            <asp:CheckBox ID="chk_invInicial" runat="server" Checked='<%# this.GetEstado(Eval("FIINVINI")) %>' Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_foto" runat="server" OldValuesParameterFormatString="original_{0}" OnInserted="obj_foto_OnInserted"
        SelectMethod="GetFotoInv" TypeName="XUSS.BLL.Inventarios.FotoInventarioBL" InsertMethod="InsertFotoBod">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="FBCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="FBNROFOT" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="FIBODEGA" Type="String" />
            <asp:Parameter Name="FIINVINI" Type="String" />
            <asp:SessionParameter Name="FINMUSER" Type="String" SessionField="UserLogon" />
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
</asp:Content>
