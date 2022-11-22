<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CargarCostos.aspx.cs" Inherits="XUSS.WEB.Costos.CargarCostos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        <telerik:RadListView ID="rlv_ccostos" runat="server" PageSize="1" AllowPaging="true" OnItemInserted="rlv_ccostos_OnItemInserted"
            Width="100%" OnItemCommand="rlv_ccostos_OnItemCommand" OnItemDataBound="rlv_ccostos_OnItemDataBound" DataSourceID="obj_ccostos" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Cargar Costos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_ccostos"
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
                                <h5>
                                    Cargar Costos</h5>
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
                            <td><label>T. Doc Origen</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdocorigen" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Traslado" Value="01" />
                                        <telerik:RadComboBoxItem Text="Orden Compra" Value="02" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>N. Doc Origen</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="true" Width="300px" Visible="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar"  />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />                                                                                
                                        <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />--%>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked ="Clicking" ToolTip="Anular Registro"/>                                                            
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>T. Doc Origen</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdocorigen" runat="server" Culture="es-CO"  Width="300px" Enabled="false"
                                     SelectedValue='<%# Bind("CT_TDOCORIGEN") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Traslado" Value="01" />
                                        <telerik:RadComboBoxItem Text="Orden Compra" Value="02" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>N. Doc Origen</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("TSNROTRA") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Tercero</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomtercero" runat="server" Enabled="false" Text='<%# Bind("NOMBRE") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                            </td>                            
                        </tr>
                        <tr>
                            <td><label>T. Documento</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdocumento" runat="server" Culture="es-CO"  Width="300px" Enabled="false"
                                     SelectedValue='<%# Bind("CT_TIPDOC") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturacion" Value="01" />
                                        <telerik:RadComboBoxItem Text="Cuenta Cobro" Value="02" />
                                        <telerik:RadComboBoxItem Text="Remision" Value="03" />
                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Nro Documento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="false" Text='<%# Bind("CT_NUMDOC") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>F. Documento</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fdocumento" runat="server" DbSelectedDate='<%# Bind("CT_FECDOC") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Moneda</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CT_MONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Valor</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px"  Enabled="false"
                                                    DbValue='<%# Bind("CT_PRECIO") %>' >
                                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Observaciones</label></td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("CT_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <table>
                        <tr>
                            <td><label>T. Doc Origen</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdocorigen" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("CT_TDOCORIGEN") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Traslado" Value="01" />
                                        <telerik:RadComboBoxItem Text="Orden Compra" Value="02" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rc_tdocorigen" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar" >
                                <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td><label>N. Doc Origen</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("TSNROTRA") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_traslado" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                          
                                <asp:ImageButton ID="iBtnFindDocumento" runat="server" CommandName="BuscarTras" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindDocumento_OnClick"  />

                            </td>
                        </tr>
                        <tr>
                            <td><label>Tercero</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_nomtercero" runat="server" Enabled="false" Text='<%# Eval("NOMBRE") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nomtercero" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                          
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick"  />
                            </td>                            
                        </tr>
                        <tr>
                            <td><label>T. Documento</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdocumento" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("CT_TIPDOC") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Facturacion" Value="01" />
                                        <telerik:RadComboBoxItem Text="Cuenta Cobro" Value="02" />
                                        <telerik:RadComboBoxItem Text="Remision" Value="03" />
                                        
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_tdocumento" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar" >
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td><label>Nro Documento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="true" Text='<%# Bind("CT_NUMDOC") %>' Width="300px" Visible="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_nrodoc" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                          
                            </td>
                            <td><label>F. Documento</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fdocumento" runat="server" DbSelectedDate='<%# Bind("CT_FECDOC") %>'
                                                MinDate="01/01/1900" Enabled="true">
                                            </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_fdocumento" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)"  >
                                <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Moneda</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CT_MONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_moneda" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar" >
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Valor</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px"  Enabled="true"
                                                    DbValue='<%# Bind("CT_PRECIO") %>' >
                                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_precio" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_precio" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="0" >
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Observaciones</label></td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("CT_OBSERVACIONES") %>'
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
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
            </InsertItemTemplate>
        </telerik:RadListView>
        <asp:ModalPopupExtender ID="mpTerceros" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnFindTerceros" TargetControlID="Button5"
            CancelControlID="Button8">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button5" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnFindTerceros" runat="server" CssClass="modalPopup" Style="display: none;" >
            <fieldset class="cssFieldSetContainer" style="width: 820px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Buscar Terceros</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_identificacion" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Contiene</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nomtercero" runat="server" Enabled="true" Width="350px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_filtroTer" runat="server" Text="Filtrar" OnClick="btn_filtroTer_OnClick"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel13" runat="server">
                        <telerik:RadGrid ID="rgConsultaTerceros" runat="server" AllowSorting="True" Width="800px"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                            DataSourceID="obj_terceros" OnItemCommand="rgConsultaTerceros_OnItemCommand">
                            <MasterTableView DataKeyNames="TRCODTER,TRCODNIT,TRNOMBRE,TRNOMBR2,TRAPELLI">
                                <Columns>
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODTER" HeaderText="Codigo"
                                        UniqueName="TRCODTER" HeaderButtonType="TextButton" DataField="TRCODTER" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODNIT" HeaderText="Ident"
                                        UniqueName="TRCODNIT" HeaderButtonType="TextButton" DataField="TRCODNIT" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBRE" HeaderText="Nombre"
                                        UniqueName="TRNOMBRE" HeaderButtonType="TextButton" DataField="TRNOMBRE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="80px">
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBR2" HeaderText=""
                                        UniqueName="TRNOMBR2" HeaderButtonType="TextButton" DataField="TRNOMBR2" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="80px">
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TRLISPRE" HeaderText="Lta Precio"
                                        UniqueName="TRLISPRE" HeaderButtonType="TextButton" DataField="TRLISPRE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="80px">
                                    </telerik:GridBoundColumn>                                    
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
                    </asp:Panel>
                    <div style="text-align: center;">
                        <asp:Button ID="Button8" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>

        <asp:ModalPopupExtender ID="mpMensajes" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnMensaje" TargetControlID="Button3"
            CancelControlID="bt_cerrar">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 700px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Mensaje</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <ul>
                        <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                    </ul>
                    <div style="text-align: center;">
                        <asp:Button ID="bt_cerrar" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>


        <asp:ModalPopupExtender ID="mp_buscartraslado" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnTraslados" TargetControlID="Button1"
            CancelControlID="Button2">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnTraslados" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 830px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Traslados</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <table>
                        <tr>
                            <td><label>Nro Traslado</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrotra" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>B. Origen</label></td>
                            <td>

                            </td>
                            <td>
                                <asp:Button ID="btn_filtroTras" runat="server" Text="Filtrar" OnClick="btn_filtroTras_OnClick"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel1" runat="server">
                        <telerik:RadGrid ID="rgTraslados" runat="server" AllowSorting="True" Width="800px"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                            DataSourceID="obj_traslados" OnItemCommand="rgTraslados_OnItemCommand">
                            <MasterTableView DataKeyNames="TSNROTRA">
                                <Columns>
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TSNROTRA" HeaderText="Nro Traslado"
                                        UniqueName="TSNROTRA" HeaderButtonType="TextButton" DataField="TSNROTRA" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TSFECTRA" HeaderText="F. Traslado"
                                        UniqueName="TSFECTRA" HeaderButtonType="TextButton" DataField="TSFECTRA" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TSBODEGA" HeaderText="B. Salida"
                                        UniqueName="TSBODEGA" HeaderButtonType="TextButton" DataField="TSBODEGA" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TSOTBODE" HeaderText="B. Entrada"
                                        UniqueName="TSOTBODE" HeaderButtonType="TextButton" DataField="TSOTBODE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
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
                                                        <h6>Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <div style="text-align: center;">
                        <asp:Button ID="Button2" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>     
        
        <asp:ModalPopupExtender ID="mp_buscarcmp" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnBuscarcmp" TargetControlID="Button4"
            CancelControlID="Button7">            
        </asp:ModalPopupExtender>  
        <div style="display: none;">
                <asp:Button ID="Button4" runat="server" Text="Button" />
            </div>
        <asp:Panel ID="pnBuscarcmp" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 830px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Ordenes Compra</h5>
                    </div>
                </div>
                <div style="padding: 5px 5px 5px 5px">
                    <table>
                        <tr>
                            <td><label>Nro Orden</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="true" Width="150px">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>B. Origen</label></td>
                            <td>

                            </td>
                            <td>
                                <asp:Button ID="btn_fitrocmp" runat="server" Text="Filtrar" OnClick="btn_fitrocmp_OnClick"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel3" runat="server">
                        <telerik:RadGrid ID="rgOrdenCmp" runat="server" AllowSorting="True" Width="800px"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                            DataSourceID="obj_compras" OnItemCommand="rgOrdenCmp_OnItemCommand">
                            <MasterTableView DataKeyNames="CH_NROCMP">
                                <Columns>
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="CH_NROCMP" HeaderText="Nro Orden"
                                        UniqueName="CH_NROCMP" HeaderButtonType="TextButton" DataField="CH_NROCMP" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="CH_FECORD" HeaderText="F. Orden"
                                        UniqueName="CH_FECORD" HeaderButtonType="TextButton" DataField="CH_FECORD" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="CH_BODEGA" HeaderText="B. Entrada"
                                        UniqueName="CH_BODEGA" HeaderButtonType="TextButton" DataField="CH_BODEGA" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="50px">
                                    </telerik:GridBoundColumn>                                    
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
                                                        <h6>Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                    <div style="text-align: center;">
                        <asp:Button ID="Button7" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_ccostos" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertCosto" OnInserted="obj_ccostos_OnInserted"
        SelectMethod="GetCostos" TypeName="XUSS.BLL.Costos.CargarCostosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="CT_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TSNROTRA" Type="Int32" />
            <asp:Parameter Name="CT_TIPPRO" Type="String" DefaultValue="." />
            <asp:Parameter Name="CT_CLAVE1" Type="String" DefaultValue="."/>
            <asp:Parameter Name="CT_CLAVE2" Type="String" DefaultValue="."/>
            <asp:Parameter Name="CT_CLAVE3" Type="String" DefaultValue="."/>
            <asp:Parameter Name="CT_CLAVE4" Type="String" DefaultValue="."/>
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="CT_TIPDOC" Type="String" />
            <asp:Parameter Name="CT_NUMDOC" Type="String" />
            <asp:Parameter Name="CT_FECDOC" Type="DateTime" />
            <asp:Parameter Name="CT_MONEDA" Type="String" />
            <asp:Parameter Name="CT_PRECIO" Type="Double" />
            <asp:Parameter Name="CT_OBSERVACIONES" Type="String" />
            <asp:SessionParameter Name="CT_USUARIO" Type="String" SessionField="UserLogon" />  
            <asp:Parameter Name="CT_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="CT_TDOCORIGEN" Type="String" />
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
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource> 
    <asp:ObjectDataSource ID="obj_traslados" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetTraslados" TypeName="XUSS.BLL.Inventarios.TrasladosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>    
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_compras" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetComprasHD" TypeName="XUSS.BLL.Compras.OrdenesComprasBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>  
    </asp:ObjectDataSource>
</asp:Content>
