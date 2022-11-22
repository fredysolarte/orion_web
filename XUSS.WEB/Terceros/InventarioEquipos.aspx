<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="InventarioEquipos.aspx.cs" Inherits="XUSS.WEB.Terceros.InventarioEquipos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Default" />
        <telerik:RadListView ID="rlv_equipos" runat="server" PageSize="1" AllowPaging="True"
            DataSourceID="obj_equipos" ItemPlaceholderID="pnlGeneral" DataKeyNames="CODIGO"
            OnItemCommand="rlv_equipos_ItemCommand" OnItemDataBound="rlv_equipos_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Equipos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_equipos"
                            PageSize="1">
                            <Fields>
                                <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                <telerik:RadDataPagerButtonField FieldType="NextLast" />
                            </Fields>
                        </telerik:RadDataPager>
                    </div>
                    <asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
                    </asp:Panel>
                </fieldset>
            </LayoutTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"
                        SkinID="SkinEditUC" />
                    <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"
                        ToolTip="Insertar" />
                    <asp:ImageButton ID="iBtnImprimirCO" runat="server" OnClick="iBtnImprimirCO_OnClick"
                        CommandName="Cancel" SkinID="SkinImprimirECO" />
                    <asp:ImageButton ID="iBtnImprimirAC" runat="server" OnClick="iBtnImprimirAC_OnClick"
                        CommandName="Cancel" SkinID="SkinImprimirEAC" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <label>
                                    Cod. Interno</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("CODIGO") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Referencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Text='<%# Bind("REFERENCIA") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    N. Equipo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("NOMBRE") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    T. Equipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tequipo" runat="server" SelectedValue='<%# Bind("T_EQUIPO") %>'
                                    Culture="es-CO" Width="150px" DataSourceID="ObjectDataSource1" DataTextField="TTVALORC"
                                    Enabled="false" DataValueField="TTCODCLA">
                                </telerik:RadComboBox>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEQUIPO" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Ubicacion</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_ubicacion" runat="server" SelectedValue='<%# Bind("UBICACION") %>'
                                    Culture="es-CO" Width="150px" DataSourceID="ObjectDataSource2" DataTextField="TTVALORC"
                                    Enabled="false" DataValueField="TTCODCLA">
                                </telerik:RadComboBox>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UBICA" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Ip 1</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip1" runat="server" Enabled="false" Text='<%# Bind("IP1") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Ip 2</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip2" runat="server" Enabled="false" Text='<%# Bind("IP2") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Ip 3</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip3" runat="server" Enabled="false" Text='<%# Bind("IP3") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Ip 4</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip4" runat="server" Enabled="false" Text='<%# Bind("IP4") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Ip 5</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip5" runat="server" Enabled="false" Text='<%# Bind("IP5") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Ip 6</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip6" runat="server" Enabled="false" Text='<%# Bind("IP6") %>'
                                    Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Observaciones</label></td>
                            <td colspan="3"><telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" TextMode="MultiLine"
                                    Width="400px" Height="60px">
                                </telerik:RadTextBox></td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Hawdware">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Sotfware">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Novedades">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_hadware" runat="server">
                            <telerik:RadGrid ID="rg_hadware" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                 Culture="(Default)" CellSpacing="0" > <%--DataSourceID="obj_hadware"--%>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn DataField="MARCA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Marca" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                            SortExpression="MARCA">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_MARCA" runat="server" Text='<%# this.GetMarca(Eval("MARCA")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="TIPO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Tipo" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                            SortExpression="TIPO">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TIPO" runat="server" Text='<%# this.GetTipo(Eval("TIPO")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="PROVEEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="PROVEEDOR">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PROVEEDOR" runat="server" Text='<%# this.GetProveedor(Eval("PROVEEDOR")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Estado" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="ESTADO">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ESTADO" runat="server" Text='<%# this.GetEstado(Eval("ESTADO")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="FECCOMPRA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="F. Compra" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="FECCOMPRA" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridTemplateColumn DataField="DESCRIPCION" HeaderText="Obs." HeaderStyle-Width="40px"
                                                AllowFiltering="false" UniqueName="DESCRIPCION">
                                                <ItemTemplate>
                                                    <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                                        TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                                        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                                            <div class="box">
                                                                <div class="title">
                                                                    <h5>
                                                                        Observación
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                            <div style="padding: 5px 5px 5px 5px">
                                                                <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                                    Text='<%# Eval("DESCRIPCION") %>' Width="400px" Rows="5"></asp:TextBox>
                                                                <div style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </asp:Panel>
                                                    <asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div id="message" runat="server">
                                            <div id="box-messages" class="box">
                                                <div class="messages">
                                                    <div id="message-notice" class="message message-notice">
                                                        <div class="image">
                                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                        </div>
                                                        <div class="text">
                                                            <h6>
                                                                Información</h6>
                                                            <span>No existen Resultados </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_software" runat="server">
                            <telerik:RadGrid ID="rg_software" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                                 Culture="(Default)" CellSpacing="0" > <%--DataSourceID="obj_sotfware"--%>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Nombre" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="NOMBRE" AllowSorting="true">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LICENCIA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Nro Licencia" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="LICENCIA" AllowSorting="true">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                            AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="FECVEN" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="DESCRIPCION" HeaderText="Obs." HeaderStyle-Width="40px"
                                                AllowFiltering="false" UniqueName="DESCRIPCION">
                                                <ItemTemplate>
                                                    <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                                        TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                                        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                                            <div class="box">
                                                                <div class="title">
                                                                    <h5>
                                                                        Observación
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                            <div style="padding: 5px 5px 5px 5px">
                                                                <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                                    Text='<%# Eval("DESCRIPCION") %>' Width="400px" Rows="5"></asp:TextBox>
                                                                <div style="text-align: center;">
                                                                    <asp:LinkButton ID="LinkButton2232" runat="server">Cerrar</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </asp:Panel>
                                                    <asp:LinkButton ID="LinkButton12" runat="server">Ver</asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div id="message" runat="server">
                                            <div id="box-messages" class="box">
                                                <div class="messages">
                                                    <div id="message-notice" class="message message-notice">
                                                        <div class="image">
                                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                        </div>
                                                        <div class="text">
                                                            <h6>
                                                                Información</h6>
                                                            <span>No existen Resultados </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <%--<asp:Panel ID="pnItemMaster" runat="server">--%>
                <table cellspacing="1">
                    <tr>
                        <td>
                            <label>
                                Referencia</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_codigo" runat="server" Text='<%# Bind("REFERENCIA") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_codigo"
                                ErrorMessage="(*)" ValidationGroup="PostBackBoton">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                N. Equipo</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Text='<%# Bind("NOMBRE") %>' Width="150px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_nombre"
                                ErrorMessage="(*)" ValidationGroup="PostBackBoton">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                T. Equipo</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_tequipo" runat="server" SelectedValue='<%# Bind("T_EQUIPO") %>'
                                Culture="es-CO" Width="150px" DataSourceID="ObjectDataSource1" DataTextField="TTVALORC"
                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="Seleccione" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="PostBackBoton"
                                ControlToValidate="rc_tequipo" ErrorMessage="(*)" InitialValue="Seleccione">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                <SelectParameters>
                                    <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEQUIPO" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ubicacion</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_ubicacion" runat="server" SelectedValue='<%# Bind("UBICACION") %>'
                                Culture="es-CO" Width="150px" DataSourceID="ObjectDataSource2" DataTextField="TTVALORC"
                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Value="" Text="Seleccione" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PostBackBoton"
                                ControlToValidate="rc_ubicacion" ErrorMessage="(*)" InitialValue="Seleccione">
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                <SelectParameters>
                                    <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UBICA" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 1</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip1" runat="server" Text='<%# Bind("IP1") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 2</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip2" runat="server" Text='<%# Bind("IP2") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 3</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip3" runat="server" Text='<%# Bind("IP3") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 4</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip4" runat="server" Text='<%# Bind("IP4") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 5</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip5" runat="server" Text='<%# Bind("IP5") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 6</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip6" runat="server" Text='<%# Bind("IP6") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Hawdware">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Sotfware">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Novedades">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_hadware" runat="server">
                        <asp:ImageButton ID="ibtn_inserthadware" runat="server" SkinID="SkinAddUC" OnClick="OnClick_ibtn_inserthadware"
                            ToolTip="Insertar" />
                        <telerik:RadGrid ID="rg_hadware" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                             Culture="(Default)" CellSpacing="0" OnDeleteCommand="rg_hadware_OnDeleteCommand">
                            <MasterTableView DataKeyNames="CODINT">
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="MARCA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Marca" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="MARCA">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MARCA" runat="server" Text='<%# this.GetMarca(Eval("MARCA")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TIPO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Tipo" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="TIPO">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_TIPO" runat="server" Text='<%# this.GetTipo(Eval("TIPO")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="PROVEEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="PROVEEDOR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PROVEEDOR" runat="server" Text='<%# this.GetProveedor(Eval("PROVEEDOR")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="ESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Estado" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="ESTADO">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ESTADO" runat="server" Text='<%# this.GetEstado(Eval("ESTADO")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="FECCOMPRA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="F. Compra" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="FECCOMPRA" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ConfirmText="Desea Eliminar el Item del Hadware?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>
                                                            Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_software" runat="server">
                        <asp:ImageButton ID="ibtn_insertsoftware" runat="server" SkinID="SkinAddUC" OnClick="OnClick_ibtn_insertsoftware"
                            ToolTip="Insertar" />
                        <telerik:RadGrid ID="rg_software" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                             Culture="(Default)" CellSpacing="0" OnDeleteCommand="rg_software_OnDeleteCommand">
                            <MasterTableView DataKeyNames="CODINT">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Nombre" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="NOMBRE" AllowSorting="true">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LICENCIA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Nro Licencia" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="LICENCIA" AllowSorting="true">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="FECVEN" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ConfirmText="Desea Eliminar el Item del Software?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>
                                                            Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                ValidationGroup="PostBackBoton" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                <%--</asp:Panel>--%>
            </InsertItemTemplate>
            <EditItemTemplate>
                <%--<div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"
                        SkinID="SkinEditUC" />
                    <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"
                        ToolTip="Insertar" />
                </div>--%>
                <%--<asp:Panel ID="pnItemMaster" runat="server">--%>
                <table cellspacing="1">
                    <tr>
                        <td>
                            <label>
                                Cod. Interno</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="false" Text='<%# Bind("CODIGO") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Referencia</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("REFERENCIA") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                N. Equipo</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Text='<%# Bind("NOMBRE") %>' Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                T. Equipo</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_tequipo" runat="server" SelectedValue='<%# Bind("T_EQUIPO") %>'
                                Culture="es-CO" Width="130px" DataSourceID="ObjectDataSource1" DataTextField="TTVALORC"
                                Enabled="true" DataValueField="TTCODCLA">
                            </telerik:RadComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                <SelectParameters>
                                    <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEQUIPO" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ubicacion</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_ubicacion" runat="server" SelectedValue='<%# Bind("UBICACION") %>'
                                Culture="es-CO" Width="130px" DataSourceID="ObjectDataSource2" DataTextField="TTVALORC"
                                Enabled="true" DataValueField="TTCODCLA">
                            </telerik:RadComboBox>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                <SelectParameters>
                                    <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UBICA" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 1</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip1" runat="server" Enabled="true" Text='<%# Bind("IP1") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 2</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip2" runat="server" Enabled="true" Text='<%# Bind("IP2") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 3</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip3" runat="server" Enabled="true" Text='<%# Bind("IP3") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 4</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip4" runat="server" Enabled="true" Text='<%# Bind("IP4") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Ip 5</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip5" runat="server" Enabled="true" Text='<%# Bind("IP5") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Ip 6</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_ip6" runat="server" Enabled="true" Text='<%# Bind("IP6") %>'
                                Width="150px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Hawdware">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Sotfware">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Novedades">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_hadware" runat="server">
                        <asp:ImageButton ID="ibtn_inserthadware" runat="server" SkinID="SkinAddUC" OnClick="OnClick_ibtn_inserthadware"
                            ToolTip="Insertar" />
                        <telerik:RadGrid ID="rg_hadware" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                             Culture="(Default)" CellSpacing="0" OnDeleteCommand="rg_hadware_OnDeleteCommand">
                            <MasterTableView DataKeyNames="CODINT">
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="MARCA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Marca" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="MARCA">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_MARCA" runat="server" Text='<%# this.GetMarca(Eval("MARCA")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="TIPO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Tipo" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="TIPO">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_TIPO" runat="server" Text='<%# this.GetTipo(Eval("TIPO")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="PROVEEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Proveedor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="PROVEEDOR">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_PROVEEDOR" runat="server" Text='<%# this.GetProveedor(Eval("PROVEEDOR")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="ESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Estado" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="ESTADO">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ESTADO" runat="server" Text='<%# this.GetEstado(Eval("ESTADO")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="FECCOMPRA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="F. Compra" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="FECCOMPRA" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ConfirmText="Desea Eliminar el Item del Hadware?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>
                                                            Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_software" runat="server">
                        <asp:ImageButton ID="ibtn_insertsoftware" runat="server" SkinID="SkinAddUC" OnClick="OnClick_ibtn_insertsoftware"
                            ToolTip="Insertar" />
                        <telerik:RadGrid ID="rg_software" runat="server" GridLines="None" Width="700px" AutoGenerateColumns="False"
                             Culture="(Default)" CellSpacing="0" OnDeleteCommand="rg_software_OnDeleteCommand">
                            <MasterTableView DataKeyNames="CODINT">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Nombre" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="NOMBRE" AllowSorting="true">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LICENCIA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Nro Licencia" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="LICENCIA" AllowSorting="true">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="FECVEN" AllowSorting="true" DataFormatString="{0:MM/dd/yyyy}">
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn ConfirmText="Desea Eliminar el Item del Software?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>
                                                            Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="PostBackBoton" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                <%--</asp:Panel>--%>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Equipos</h5>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 7px">
                                                <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"
                                                    ToolTip="Insertar" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <label>
                                    Cod. Interno</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_codigo" runat="server" Enabled="true" Width="150px">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Referencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    N. Equipo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    IP</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_ip" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
        </telerik:RadListView>
        <%--</telerik:RadAjaxPanel>--%>
        <div style="display: none;">
            <asp:Button ID="Button2" runat="server" Text="Button" />
        </div>
        <cc1:ModalPopupExtender ID="mp_hadware" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="pnHadware" TargetControlID="Button2" CancelControlID="btn_cancelH">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnHadware" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                <div class="box">
                    <div class="title">
                        <h5>
                            Hadware</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Marca</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_marca" runat="server" Culture="es-CO" Width="250px" DataSourceID="obj_marca"
                                    DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="Seleccione" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="gv_hadware"
                                    ControlToValidate="rc_marca" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_marca" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MARCAH" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="250px" DataSourceID="obj_tipo"
                                    DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="Seleccione" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="gv_hadware"
                                    ControlToValidate="rc_tipo" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_tipo" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetTbTablas" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TIPOH" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Proveedor</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" Culture="es-CO" Width="250px"
                                    DataSourceID="obj_proveedor" DataTextField="TRNOMBRE" DataValueField="TRCODNIT"
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="Seleccione" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:ObjectDataSource ID="obj_proveedor" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" DefaultValue="" />
                                        <asp:Parameter Name="filter" Type="String" DefaultValue="" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Compra</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fCompra" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Observaciones</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_observhadware" runat="server" Width="300px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_AceptarH" runat="server" Text="Aceptar" OnClick="btn_AceptarH_click"
                                    ValidationGroup="gv_hadware" />
                                <asp:Button ID="btn_cancelH" runat="server" Text="Cancelar" />
                                <%--OnClick="btn_cancelH_click"--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
        <div style="display: none;">
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
        <cc1:ModalPopupExtender ID="mp_software" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="pnSoftware" TargetControlID="Button1" CancelControlID="btn_regReporte">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnSoftware" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                <div class="box">
                    <div class="title">
                        <h5>
                            Software</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Licencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_licencia" runat="server" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Fec. Vencimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecven" runat="server">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Observaciones</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_observacionesSoft" runat="server" Width="300px" TextMode="MultiLine">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_AceptarS" runat="server" Text="Aceptar" OnClick="btn_AceptarS_click" />
                                <asp:Button ID="btn_cancelS" runat="server" Text="Cancelar" />
                                <%--OnClick="btn_cancelS_click"--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
        <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <cc1:ModalPopupExtender ID="mp_reporte" runat="server" BackgroundCssClass="modalBackground"
            PopupControlID="pnReporte" TargetControlID="Button3" CancelControlID="btn_regReporte">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnReporte" runat="server" CssClass="modalPopup" Style="display: none;">
            <%--<fieldset class="cssFieldSetContainer" style="width: auto !important;">--%>
            <fieldset class="cssFieldSetContainer" style="width: 700px !important; height: 630px !important;">
                <div>
                    <%--<telerik:ReportViewer ID="ReportViewer1" runat="server" ReportBookID="ReportBookControl1"
                        Width="100%" Height="600px">
                    </telerik:ReportViewer>
                    <telerik:ReportBookControl ID="ReportBookControl1" runat="server">
                        <Reports>
                            <telerik:ReportInfo Report="XUSS.WEB.Informes.rCodigosEquipos, XUSS.WEB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
                            <telerik:ReportInfo Report="XUSS.WEB.Informes.rActaEntrega, XUSS.WEB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />
                        </Reports>
                    </telerik:ReportBookControl>--%>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_regReporte" runat="server" Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </asp:Panel>
        <div style="display: none">
            <asp:Button ID="Button5" runat="server" />
        </div>
        <cc1:ModalPopupExtender ID="mpMensajes" runat="server" PopupControlID="pnlMensaje"
            TargetControlID="Button5" BackgroundCssClass="modalBackground" CancelControlID="Button6">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                <div class="box">
                    <div class="title">
                        <h5>
                            <asp:Label runat="server" ID="LitTitulo"></asp:Label>
                        </h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <ul>
                        <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                    </ul>
                    <div style="text-align: center;">
                        <asp:Button ID="Button6" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_equipos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetEquipos" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL" InsertMethod="InsertEquipo"
        UpdateMethod="UpdateEquipo" OnInserting="obj_equipos_OnInserting" OnInserted="obj_equipos_OnInserted"
        OnUpdated="obj_equipos_OnUpdated" OnUpdating="obj_equipos_OnUpdating">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="CODIGO" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="T_EQUIPO" Type="String" />
            <asp:Parameter Name="UBICACION" Type="String" />
            <asp:Parameter Name="IP1" Type="String" />
            <asp:Parameter Name="IP2" Type="String" />
            <asp:Parameter Name="IP3" Type="String" />
            <asp:Parameter Name="IP4" Type="String" />
            <asp:Parameter Name="IP5" Type="String" />
            <asp:Parameter Name="IP6" Type="String" />
            <asp:Parameter Name="USUARIO" Type="String" DefaultValue="" />
            <asp:Parameter Name="REFERENCIA" Type="String" />
            <asp:Parameter Name="NOMBRE" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <%--<asp:Parameter Name="CODIGO" Type="Int32" />--%>
            <asp:Parameter Name="T_EQUIPO" Type="String" />
            <asp:Parameter Name="UBICACION" Type="String" />
            <asp:Parameter Name="IP1" Type="String" />
            <asp:Parameter Name="IP2" Type="String" />
            <asp:Parameter Name="IP3" Type="String" />
            <asp:Parameter Name="IP4" Type="String" />
            <asp:Parameter Name="IP5" Type="String" />
            <asp:Parameter Name="IP6" Type="String" />
            <asp:Parameter Name="USUARIO" Type="String" DefaultValue="" />
            <asp:Parameter Name="REFERENCIA" Type="String" />
            <asp:Parameter Name="NOMBRE" Type="String" />
            <asp:Parameter Name="original_CODIGO" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_hadware" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetHadware" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
        <SelectParameters>
            <asp:ControlParameter ControlID="rlv_equipos" Name="Codigo" PropertyName="SelectedValues[CODIGO]"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_sotfware" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSoftware" TypeName="XUSS.BLL.Terceros.InventarioEquiposBL">
        <SelectParameters>
            <asp:ControlParameter ControlID="rlv_equipos" Name="Codigo" PropertyName="SelectedValues[CODIGO]"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
