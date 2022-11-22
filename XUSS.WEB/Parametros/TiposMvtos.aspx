<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TiposMvtos.aspx.cs" Inherits="XUSS.WEB.Parametros.TiposMvtos" %>
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
        <telerik:RadListView ID="rlv_tmovitos" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rlv_tmovitos_ItemUpdating" OnItemInserting="rlv_tmovitos_ItemInserting"
            Width="100%" OnItemCommand="rlv_tmovitos_OnItemCommand" OnItemDataBound="rlv_tmovitos_ItemDataBound"
            DataSourceID="obj_tmovito" ItemPlaceholderID="pnlGeneral" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Tipos de Movimientos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_tmovitos" RenderMode="Lightweight"
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
                                <h5>Tipos de Movimientos</h5>
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
                                    <label>Codigo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
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
                            <td><label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TMCDTRAN") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>T Transaccion</label></td>
                            <td>
                                 <telerik:RadComboBox ID="rc_inout" runat="server" Culture="es-CO" Width="300px" Enabled ="false"
                                    SelectedValue='<%# Bind("TMENTSAL") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Entrada (-)" Value="E" />
                                        <telerik:RadComboBoxItem Text="Salida (+)" Value="S" />                                                                            
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Nombre</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("TMNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Text='<%# Bind("TMDESCRI") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>¿R. Documento?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_rdocumento" runat="server" Checked='<%# this.GetEstado(Eval("TMREQDOC")) %>' Enabled="false" />
                            </td>
                            <td><label>¿M. Manual?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_mmanual" runat="server" Checked='<%# this.GetEstado(Eval("TMMOVMAN")) %>' Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TMCDTRAN") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>T Transaccion</label></td>
                            <td>
                                 <telerik:RadComboBox ID="rc_inout" runat="server" Culture="es-CO" Width="300px"
                                    SelectedValue='<%# Bind("TMENTSAL") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Entrada (-)" Value="E" />
                                        <telerik:RadComboBoxItem Text="Salida (+)" Value="S" />                                                                            
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Nombre</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TMNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("TMDESCRI") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>¿R. Documento?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_rdocumento" runat="server" Checked='<%# this.GetEstado(Eval("TMREQDOC")) %>' Enabled="true" />
                            </td>
                            <td><label>¿M. Manual?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_mmanual" runat="server" Checked='<%# this.GetEstado(Eval("TMMOVMAN")) %>' Enabled="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
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
                            <td><label>Codigo</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("TMCDTRAN") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>T Transaccion</label></td>
                            <td>
                                 <telerik:RadComboBox ID="rc_inout" runat="server" Culture="es-CO" Width="300px"
                                    SelectedValue='<%# Bind("TMENTSAL") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Entrada (-)" Value="E" />
                                        <telerik:RadComboBoxItem Text="Salida (+)" Value="S" />                                                                            
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Nombre</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TMNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("TMDESCRI") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>¿R. Documento?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_rdocumento" runat="server" Checked='<%# this.GetEstado(Eval("TMREQDOC")) %>' Enabled="true" />
                            </td>
                            <td><label>¿M. Manual?</label></td>
                            <td>
                                <asp:CheckBox ID="chk_mmanual" runat="server" Checked='<%# this.GetEstado(Eval("TMMOVMAN")) %>' Enabled="true" />
                            </td>
                        </tr>
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
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_tmovito" runat="server" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateTipoMovimiento" InsertMethod="InsertTipoMovimiento"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TMCODEMP" Type="String" SessionField="CODEMP" />            
            <asp:Parameter Name="TMCDTRAN" Type="String" />
            <asp:Parameter Name="TMNOMBRE" Type="String" />
            <asp:Parameter Name="TMDESCRI" Type="String" />
            <asp:Parameter Name="TMENTSAL" Type="String" />
            <asp:Parameter Name="TMREQDOC" Type="String"  />
            <asp:Parameter Name="TMTIPMOV" Type="String" />
            <asp:Parameter Name="TMMOVMAN" Type="String" />
            <asp:Parameter Name="TMOTTRAN" Type="String" />
            <asp:Parameter Name="TMMOVPAR" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TMCONMAQ" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TMORICST" Type="String" DefaultValue="1" />
            <asp:Parameter Name="TMPRIORI" Type="String" DefaultValue="0" />
            <asp:Parameter Name="TMBODCON" Type="String" DefaultValue="R" />
            <asp:Parameter Name="TMACTFEC" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TMESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TMCAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="TMNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TMCONPRO" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TMCODEMP" Type="String" SessionField="CODEMP" />            
            <asp:Parameter Name="TMCDTRAN" Type="String" />
            <asp:Parameter Name="TMNOMBRE" Type="String" />
            <asp:Parameter Name="TMDESCRI" Type="String" />
            <asp:Parameter Name="TMENTSAL" Type="String" />
            <asp:Parameter Name="TMREQDOC" Type="String" />
            <asp:Parameter Name="TMTIPMOV" Type="String" />
            <asp:Parameter Name="TMMOVMAN" Type="String" />
            <asp:Parameter Name="TMOTTRAN" Type="String" />
            <asp:Parameter Name="TMMOVPAR" Type="String" />
            <asp:Parameter Name="TMCONMAQ" Type="String" />
            <asp:Parameter Name="TMORICST" Type="String" />
            <asp:Parameter Name="TMPRIORI" Type="String" />
            <asp:Parameter Name="TMBODCON" Type="String" />
            <asp:Parameter Name="TMACTFEC" Type="String" />
            <asp:Parameter Name="TMESTADO" Type="String" />
            <asp:Parameter Name="TMCAUSAE" Type="String" />            
            <asp:SessionParameter Name="TMNMUSER" Type="String" SessionField="UserLogon" />            
            <asp:Parameter Name="TMCONPRO" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
