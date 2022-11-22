<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="XUSS.WEB._Default" %>

<%@ MasterType VirtualPath="~/Master/MasterAdmin.Master" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="10000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" RenderMode="Lightweight" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Tickets-Tareas</h5>
                </div>
            </div>
            <div class="paginadorRadListView">
                <telerik:RadButton RenderMode="Lightweight" ID="IBtnFind" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" OnClick="IBtnFind_OnClick" />
                <telerik:RadButton RenderMode="Lightweight" ID="IBtnInsert" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" OnClick="IBtnInsert_OnClick" />
                <telerik:RadButton RenderMode="Lightweight" ID="btn_invitacion" runat="server" Text="Invitacion" Icon-PrimaryIconCssClass="rbConfig" CommandName="Cita" ToolTip="Nueva Invitacion/Cita" OnClick="btn_invitacion_Click" />
                <telerik:RadButton RenderMode="Lightweight" ID="btn_programacion" runat="server" Text="Programar" Icon-PrimaryIconCssClass="rbOpen" CommandName="Programar" ToolTip="Programacion" OnClick="btn_programacion_Click" />
            </div>
        </fieldset>
        <asp:Panel ID="pnlGrilla" runat="server" Width="100%" Height="950px">
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" Height="950px" ShowGroupPanel="True"
                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_LstTareas"
                OnPreRender="rgDetalle_PreRender" AllowFilteringByColumn="True" AllowSorting="true" OnItemCommand="rgDetalle_ItemCommand">
                <ClientSettings AllowKeyboardNavigation="true" EnablePostBackOnRowClick="true">
                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    <Selecting AllowRowSelect="true"></Selecting>
                </ClientSettings>
                <MasterTableView DataKeyNames="TK_NUMERO" AllowFilteringByColumn="True" AllowSorting="true" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <SelectFields>
                                <telerik:GridGroupByField FieldAlias="TTVALORC" FieldName="TTVALORC" HeaderText="Area " />
                            </SelectFields>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="TTVALORC" />
                            </GroupByFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                    <Columns>
                        <telerik:GridBoundColumn DataField="TK_NUMERO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                            AllowFiltering="false" HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="TK_NUMERO">
                            <HeaderStyle Width="50px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TK_ASUNTO" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                            AllowFiltering="true" HeaderText="Asunto" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TK_ASUNTO">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SUB_ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                            AllowFiltering="true" HeaderText="Sub Zona" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="SUB_ZONA">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MIC_ZONA" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                            AllowFiltering="true" HeaderText="M Zona" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="MIC_ZONA">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="TK_PRIORIDAD" HeaderText="Prioridad" UniqueName="TK_PRIORIDAD"
                            HeaderStyle-Width="80px" AllowFiltering="false" SortExpression="TK_PRIORIDAD">
                            <ItemTemplate>
                                <asp:Label ID="tk_prioridadLabel" runat="server" Text='<%# this.GetPrioridad(Convert.ToString(Eval("TK_PRIORIDAD"))) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="TK_PROPIETARIO" HeaderText="Propietario" UniqueName="TK_PROPIETARIO"
                            HeaderStyle-Width="180px" AllowFiltering="false" SortExpression="TK_PROPIETARIO">
                            <ItemTemplate>
                                <asp:Label ID="tk_propietarioLabelcod" runat="server" Text='<%# Eval("TK_PROPIETARIO") %>' Visible="false"></asp:Label>
                                <asp:Label ID="tk_propietarioLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_PROPIETARIO"))) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="TK_RESPONSABLE" HeaderText="Responsable" UniqueName="TK_RESPONSABLE"
                            HeaderStyle-Width="180px" AllowFiltering="false" SortExpression="TK_RESPONSABLE">
                            <ItemTemplate>
                                <asp:Label ID="tk_responsableLabel" runat="server" Text='<%# this.GetNomUsuario(Convert.ToString(Eval("TK_RESPONSABLE"))) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="TK_OBSERVACIONES" HeaderText="Obs." HeaderStyle-Width="40px"
                            AllowFiltering="false" UniqueName="TK_OBSERVACIONES">
                            <ItemTemplate>
                                <cc1:ModalPopupExtender ID="ModalAlert2" runat="server" PopupControlID="pnlAlertMensaje2"
                                    TargetControlID="LinkButton12" BackgroundCssClass="modalBackground" CancelControlID="LinkButton2232">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                                    <fieldset class="cssFieldSetContainer" style="width: auto !important;">
                                        <div class="box">
                                            <div class="title">
                                                <h5>Observación
                                                </h5>
                                            </div>
                                        </div>
                                        <div style="padding: 5px 5px 5px 5px">
                                            <asp:TextBox ID="TextBoxObservaciones" TextMode="MultiLine" runat="server" ReadOnly="true"
                                                Text='<%# Eval("TK_OBSERVACIONES") %>' Width="500px" Rows="10"></asp:TextBox>
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
                        <telerik:GridTemplateColumn DataField="TK_ESTADO" HeaderText="Estado" UniqueName="TK_ESTADO"
                            HeaderStyle-Width="80px" AllowFiltering="false" SortExpression="TK_ESTADO">
                            <ItemTemplate>
                                <asp:Label ID="tk_estadoLabel" runat="server" Text='<%# this.GetEstado(Convert.ToString(Eval("TK_ESTADO"))) %>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="TK_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            AllowFiltering="false" HeaderText="Fecha Ing." ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TK_FECING" DataFormatString="{0:MM/dd/yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TK_FECVEN" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            AllowFiltering="false" HeaderText="Fecha Ven." ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TK_FECVEN" DataFormatString="{0:MM/dd/yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OBS_TK" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            AllowFiltering="false" HeaderText="Vencimiento" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="OBS_TK" AllowSorting="true">
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
        <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
            <fieldset class="cssFieldSetContainer" style="width: 950px !important">
                <div style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    De</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_propietario" runat="server" DataSourceID="obj_propietario" Width="250px"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_propietario" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Asunto</label>
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="edt_asunto" runat="server" Width="700px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="edt_asunto" ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Area</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_area" runat="server" DataSourceID="obj_area" OnSelectedIndexChanged="rc_area_OnSelectedIndexChanged"
                                    DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="250px"
                                    AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:ObjectDataSource ID="obj_area" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAreas" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                            <td>
                                <label>
                                    Responsable</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_responsable" runat="server" DataSourceID="obj_responsable" Width="250px"
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_responsable" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ObjectDataSource ID="obj_responsable" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Prioridad</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rb_prioridad" runat="server" Width="250px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Bajo" Value="01" />
                                        <telerik:RadComboBoxItem Text="Medio" Value="02" />
                                        <telerik:RadComboBoxItem Text="Alto" Value="03" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="rb_prioridad" ErrorMessage="(*)" InitialValue="Seleccione">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>
                                    Vencimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecVencimiento" runat="server">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="PostBackBoton"
                                    ControlToValidate="edt_fecVencimiento" ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Descripción</label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <telerik:RadTextBox ID="edt_Observaciones" runat="server" TextMode="MultiLine" Width="950px"
                                    Height="250" MaxLength="4000">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Adjunto</label>
                            </td>
                            <td colspan="4">
                                <telerik:RadAsyncUpload ID="rauArchivo" runat="server" MaxFileInputsCount="1">
                                </telerik:RadAsyncUpload>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <div style="text-align: right;">
                <asp:Button ID="btnAgregar" runat="server" Text="Aceptar" OnClick="btnAgregar_click" ValidationGroup="PostBackBoton" CausesValidation="true" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlBuscar" runat="server" Visible="false">
            <fieldset class="cssFieldSetContainer">
                <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                    <table>
                        <tr>
                            <td>ID</td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_id" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Asunto</td>
                            <td>
                                <telerik:RadTextBox ID="edt_fasunto" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" Width="250px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Asignadas" Value="N" />
                                        <telerik:RadComboBoxItem Text="Pendientes" Value="S" />
                                    </Items>
                                </telerik:RadComboBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_OnClick" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
            <fieldset class="cssFieldSetContainer">
                <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                    <asp:Panel ID="pnItemMaster" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Propietario</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_propietariod" runat="server" DataSourceID="obj_propietario" Width="250px" Enabled="false"
                                        DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Numero</label></td>
                                <td>
                                    <telerik:RadTextBox ID="edt_numerod" runat="server" Width="150px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Asunto</label></td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="edt_asuntod" runat="server" Width="700px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>
                </fieldset>
            </fieldset>
            <telerik:RadListView ID="rlv_detalle" runat="server" OnItemCommand="rlv_detalle_ItemCommand" RenderMode="Lightweight"
                ItemPlaceholderID="pnlGeneral" DataSourceID="obj_detalle"
                InsertItemPosition="FirstItem" OnItemInserting="rlv_detalle_ItemInserting">
                <LayoutTemplate>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_responder" runat="server" Text="Responder" Icon-PrimaryIconCssClass="rbNext" CommandName="InitInsert" ToolTip="Responder" />
                    <asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
                    </asp:Panel>
                </LayoutTemplate>
                <ItemTemplate>
                    <fieldset class="cssFieldSetContainer">
                        <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                            <asp:Panel ID="pnItemMaster" runat="server">
                                <table>
                                    <tr>
                                        <td style="width: 150px">
                                            <asp:Literal runat="server" ID="ltNombre" Text='<%# Bind("usua_nombres") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="edt_observacion" runat="server" Enabled="false" Text='<%# Bind("TD_OBSERVACION") %>' TextMode="MultiLine" Height="100px" Width="500px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="edt_Fafiliacion" runat="server" DbSelectedDate='<%# Bind("TD_FECING") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <asp:HyperLink Target="_blank" ID="hpl_ruta" runat="server" NavigateUrl='<%#  this.GetNombreArchivoRuta(Convert.ToString(Eval("TD_RUTA")))%>'
                                                Text='<%#  this.GetNombreArchivo(Convert.ToString(Eval("TD_RUTA")))%>'></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Contacto</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_contacto" Text='<%# Bind("TD_CONTACTO") %>' runat="server" Width="150px" Enabled="false">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Nro Telfono</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_nrotel" Text='<%# Bind("TD_TELEFONO") %>' runat="server" Width="150px" Enabled="false">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                    </fieldset>
                </ItemTemplate>
                <InsertItemTemplate>
                    <fieldset class="cssFieldSetContainer">
                        <fieldset class="cssFieldSet" style="width: 96.5% !important;">
                            <table>
                                <tr>
                                    <td>
                                        <label>Area</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="Erc_area" runat="server" DataSourceID="obj_area" OnSelectedIndexChanged="Erc_area_OnSelectedIndexChanged"
                                            Width="250px" DataTextField="TTVALORC" DataValueField="TTCODCLA" AppendDataBoundItems="true"
                                            AutoPostBack="true" Enabled="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="PostBackBoton"
                                            ControlToValidate="Erc_area" ErrorMessage="(*)" InitialValue="Seleccione">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Responsable</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="erb_responsable" runat="server" DataSourceID="obj_responsable"
                                            Width="250px" DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true"
                                            Enabled="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Estado</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="Erb_estado" runat="server" Width="180px" Enabled="true" SelectedValue='<%# Bind("inEstado") %>'>
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                                <telerik:RadComboBoxItem Text="Pendiente" Value="AC" />
                                                <telerik:RadComboBoxItem Text="Ejecutado" Value="CE" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Observacion</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_observacion" runat="server" Enabled="true" TextMode="MultiLine" Height="150px" Width="700px" Text='<%# Bind("TD_OBSERVACION") %>' MaxLength="4000">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Adjunto</label>
                                    </td>
                                    <td colspan="4">
                                        <telerik:RadAsyncUpload ID="rauArchivo" runat="server" MaxFileInputsCount="1">
                                        </telerik:RadAsyncUpload>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" />
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </fieldset>
                </InsertItemTemplate>
            </telerik:RadListView>
            <table width="750px">
                <tr>
                    <td align="center">
                        <telerik:RadButton RenderMode="Lightweight" ID="btn_regresar" runat="server" Text="Regresar" Icon-PrimaryIconCssClass="rbPrevious" ToolTip="Regresar" OnClick="lk_regresar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlProgramar" runat="server" Visible="false">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbl_descrip" runat="server">Description</asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="TitleTextBox" Rows="5" Columns="20" runat="server"
                            Width="97%" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TitleTextBox"
                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                            <asp:Image ID="Image25" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_fecini" runat="server">Inicio</asp:Label>
                    </td>
                    <td>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="StartInput" runat="server" Width="300px" ZIndex="1000000">
                        </telerik:RadDateTimePicker>
                    </td>
                    <td>

                        <asp:Label ID="lbl_fecfin" runat="server">Fin</asp:Label>
                    </td>
                    <td>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="EndInput" runat="server" Width="300px" ZIndex="1000000">
                        </telerik:RadDateTimePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_usuario" runat="server">Responsable</asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_usuario" runat="server" DataSourceID="obj_propietario" Width="300px" AllowCustomText="true" Enabled="true"
                            DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith" ZIndex="1000000">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_usuario" ErrorMessage="(*)" InitialValue="Seleccionar">
                            <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_usuario" ErrorMessage="(*)" InitialValue="">
                            <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lbl_servicio" runat="server">Serivio</asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_tservicio" runat="server" Width="300px" AllowCustomText="true" Enabled="true"
                            AppendDataBoundItems="true" Filter="StartsWith" ZIndex="1000000">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                <telerik:RadComboBoxItem Text="Desmonte Definitivo" Value="1" />
                                <telerik:RadComboBoxItem Text="Servicio Tecnivo" Value="2" />
                                <telerik:RadComboBoxItem Text="Desmonte Remodelacion" Value="3" />
                                <telerik:RadComboBoxItem Text="Instalacion Remodelacion" Value="4" />
                                <telerik:RadComboBoxItem Text="Inspeccion" Value="5" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="Seleccionar">
                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_tservicio" ErrorMessage="(*)" InitialValue="">
                            <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_proyecto" runat="server">Proyecto</asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_tercero" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true" OnSelectedIndexChanged="rc_tercero_SelectedIndexChanged" AutoPostBack="true"
                            DataTextField="NOM_COMPLETO" DataValueField="TRCODTER" AppendDataBoundItems="true" Filter="StartsWith" ZIndex="1000000">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="Seleccionar">
                            <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_tercero" ErrorMessage="(*)" InitialValue="">
                            <asp:Image ID="Image15" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lbl_phorizontal" runat="server">P. Horizontal</asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_propiedad" runat="server" DataSourceID="obj_clientes" Width="300px" AllowCustomText="true" AppendDataBoundItems="true" Filter="StartsWith" ZIndex="1000000">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="Seleccionar">
                            <asp:Image ID="Image16" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="gvInsert"
                            ControlToValidate="rc_propiedad" ErrorMessage="(*)" InitialValue="">
                            <asp:Image ID="Image17" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <telerik:RadButton ID="btnInsert" runat="server" CommandName="Insert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" Width="110px" OnClick="btnInsert_Click" />
                        <telerik:RadButton ID="btn_cancelar_pro" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" Width="110px" OnClick="btn_cancelar_pro_click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpCita" runat="server" Width="900px" Height="400px" Modal="true" OffsetElementID="main" Title="Nuevo Invitacion/Cita" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Invitados</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_usuarios" runat="server" DataSourceID="obj_propietario" Width="300px" CheckBoxes="true"
                                        DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>F. Inicial</label>
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" runat="server" ID="txt_finicial" Width="300px">
                                    </telerik:RadDateTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Post"
                                        ControlToValidate="txt_finicial" ErrorMessage="(*)">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>F. Final</label>
                                </td>
                                <td>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" runat="server" ID="txt_ffinal" Width="300px">
                                    </telerik:RadDateTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Post"
                                        ControlToValidate="txt_ffinal" ErrorMessage="(*)">
                                        <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="(*)" ValidationGroup="Post"
                                        ControlToValidate="txt_ffinal" ControlToCompare="txt_finicial" Operator="GreaterThan">
                                        <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Asunto</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_asunto" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Post"
                                        ControlToValidate="txt_asunto" ErrorMessage="(*)">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_cita" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" OnClick="btn_cita_Click" RenderMode="Lightweight"
                                        ValidationGroup="Post" CausesValidation="true">
                                    </telerik:RadButton>
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
    <asp:ObjectDataSource ID="obj_LstTareas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLstTareas" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="Usuario" Type="String" />
            <asp:Parameter DefaultValue="AC" Name="Estado" Type="String" />
            <asp:Parameter Name="inTipo" Type="String" DefaultValue="S" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_detalle" runat="server"
        OldValuesParameterFormatString="original_{0}" InsertMethod="InsertDetalleTicketMail"
        SelectMethod="GetDetalleTicket" TypeName="XUSS.BLL.Tareas.LstTareasBL"
        OnInserting="obj_detalle_OnInserting" OnInserted="obj_detalle_Inserted">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="TK_NUMERO" Type="Int32" DefaultValue="0" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="TK_NUMERO" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TD_RESPONSABLE" Type="String" DefaultValue="0" />
            <asp:Parameter Name="TD_USUARIO" Type="String" DefaultValue="0" />
            <asp:Parameter Name="TD_OBSERVACION" Type="String" DefaultValue="0" />
            <asp:Parameter Name="RUTA" Type="String" DefaultValue="" />
            <asp:Parameter Name="MAIL_SERVER" Type="String" DefaultValue="" />
            <asp:Parameter Name="MAIL_USER" Type="String" DefaultValue="" />
            <asp:Parameter Name="MAIL_PASSWORD" Type="String" DefaultValue="" />
            <asp:Parameter Name="MAIL_FROM" Type="String" DefaultValue="" />
            <asp:Parameter Name="inAsunto" Type="String" DefaultValue="" />
            <asp:Parameter Name="inPropietario" Type="String" DefaultValue="" />
            <asp:Parameter Name="inEstado" Type="String" DefaultValue="AC" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_propietario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
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
