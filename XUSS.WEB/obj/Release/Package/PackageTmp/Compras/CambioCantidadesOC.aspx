<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambioCantidadesOC.aspx.cs" Inherits="XUSS.WEB.Compras.CambioCantidadesOC" %>

<%@ Register Assembly="FastReport.Web, Version=2014.1.6.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c" Namespace="FastReport.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <link href="../App_Themes/Tema2/Images/icon.ico" type="image/x-icon" rel="shortcut icon" />
    <title></title>
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function fixEditor() {
                debugger;
                $find("<%=edt_body.ClientID %>").onParentNodeChanged();
            }
            function loadModal(button, args) { button.set_autoPostBack(false); $find(modalPopup.id).show(); }
            function CloseModal() {
                //$find(modalPopup.id).close();
                $find("meEmail").hide();
            }

            function Clicking(sender, args) {
                if (!confirm("¿Are you sure the save changes?"))
                    args.set_cancel(!confirmed);
            }
            function Close() {
                var objwindows = window.open(location.href, "_self");
                objwindows.close();
            }
            function OnClientEntryAddingHandler(sender, eventArgs) {

                if (sender.get_entries().get_count() > 1) {
                    eventArgs.set_cancel(true);
                }
            }
            function onclic_mp() {
                $find("meEmail").show();
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" RenderMode="Lightweight" Skin="Bootstrap">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" RenderMode="Lightweight" Skin="Bootstrap" />        
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
    </telerik:RadAjaxLoadingPanel>
    
        <telerik:RadFormDecorator ID="RadFormDecorator2" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />

        <!--header -->
        <div id="header" style="position: absolute; top: 0%; bottom: 0%;">
            <div id="header-outer">
                <!-- logo -->
                <div id="logo">
                    <h1>
                        <a href="#" title="Xuss">
                            <asp:Image ID="imgLogo" runat="server" Width="186px"
                                ImageUrl="~/App_Themes/Tema2/Images/logo-xuss.png" Height="65px" /></a>
                    </h1>
                </div>
                <!-- end logo -->
                <!-- user -->
                <ul id="user">
                </ul>
                <!--End User-->
                <div id="header-inner">
                    <%--<div id="home">
					<asp:HyperLink ID="hlnk" runat="server" NavigateUrl="~/Default.aspx"></asp:HyperLink>
				</div>--%>

                    <div class="corner tl">
                    </div>
                    <div class="corner tr">
                    </div>
                </div>
            </div>
        </div>
        <div id="content" style="position: absolute; top: 9%; bottom: 0%;">
            <div id="left">
                <telerik:RadPanelBar runat="server" ID="pnl_menu" Height="100%" Width="100%" RenderMode="Lightweight" Skin="Bootstrap">
                    <Items>
                        <telerik:RadPanelItem Text="Herramientas" Expanded="True" ImageUrl="~/App_Themes/Tema2/Images/1-properties.png">
                            <Items>
                                <%--<telerik:RadPanelItem Text="Enviar Email" ImageUrl="~/App_Themes/Tema2/Images/2-mail.png"  />--%>
                            </Items>
                            <ContentTemplate>
                                <telerik:RadButton ID="btn_guardar" runat="server" Text="Save" Icon-PrimaryIconCssClass="rbSave" OnClick="btn_guardar_Click" OnClientClicked="Clicking"
                                    Width="100%" AutoPostBack="true" RenderMode="Lightweight" Skin="Bootstrap">
                                </telerik:RadButton>
                                <telerik:RadButton ID="btn_close" runat="server" Text="Close" Icon-PrimaryIconCssClass="rbCancel"
                                    OnClientClicked="Close" Width="100%" AutoPostBack="false" RenderMode="Lightweight" Skin="Bootstrap">
                                </telerik:RadButton>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>

                </telerik:RadPanelBar>
            </div>
            <div id="right" style="height: 100%;">
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Height="100%">
                    <asp:Panel ID="pnl_cabecera" runat="server" Width="100%">
                        <table>
                        <tr>
                            <td>
                                <label>Cod Int</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nroorden" runat="server" Enabled="false"  Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>D Order</label></td>
                            <td>
                                <telerik:RadDatePicker ID="edt_forden" runat="server"  MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Nro Order Int</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrocmpalt" runat="server" Enabled="false" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Warehouse</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodegas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" 
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Supplier</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false"  DataTextField="TRNOMBRE"
                                    Width="300px" DataSourceID="obj_terceros">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>

                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>T. Order</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_torden" runat="server"  Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Orden de Compra" Value="1" />
                                        <telerik:RadComboBoxItem Text="Orden Servicio" Value="2" />
                                        <telerik:RadComboBoxItem Text="Devolucion" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>T. Shipping</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tdespacho" runat="server"  Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Completo" Value="01" />
                                        <telerik:RadComboBoxItem Text="Parcial" Value="02" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>T Pago</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_terpago" DataTextField="TPNOMBRE" 
                                    DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Currency</label>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" 
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </td>
                        </tr>
                        <tr>                            
                            <td>
                                <label>Status</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server"  Enabled="false"
                                    Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Aprobado" Value="AP" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <label>Observations</label></td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false"
                                    Width="600px" TextMode="MultiLine" Height="40px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="98%" AutoGenerateColumns="False" Height="100%" GroupPanelPosition="Top" ShowGroupPanel="True"
                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnPreRender="rg_items_PreRender">
                    <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                    </ClientSettings>
                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="CD_NROITEM" HeaderButtonType="TextButton" HeaderStyle-Width="30px"
                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_NROITEM"
                                UniqueName="CD_NROITEM">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="CD_NROITEM" HeaderText="Propietario" UniqueName="CD_NROITEM"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_NROITEM" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("CD_NROITEM") %>' Visible="false">
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn DataField="CD_ESTADO" HeaderText="Propietario" UniqueName="CD_ESTADO"
                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CD_ESTADO" Visible="false">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="txt_estado" runat="server" Enabled="true" Text='<%# Bind("CD_ESTADO") %>' Visible="false">
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                HeaderText="Bar Code" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                UniqueName="BARRAS">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                UniqueName="TANOMBRE">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CD_CLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Reference" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CLAVE1"
                                UniqueName="CD_CLAVE1">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                                HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                UniqueName="ARNOMBRE">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                UniqueName="NOMTTEC1">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                UniqueName="NOMTTEC2">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                UniqueName="NOMTTEC3">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                UniqueName="NOMTTEC">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                UniqueName="NOMTTEC5">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CD_PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" ItemStyle-ForeColor="Blue" ItemStyle-Font-Bold="true"
                                HeaderText="Pre x UN" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_PRECIO"
                                UniqueName="CD_PRECIO">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CD_CANSOL" HeaderButtonType="TextButton" HeaderStyle-Width="60px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true"
                                HeaderText="Sug Qty" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CD_CANSOL"
                                UniqueName="CD_CANSOL" FooterText="Total: " Aggregate="Sum">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="CD_CANTIDAD" HeaderText="Qty" HeaderStyle-Width="70px" Visible="True"
                                Resizable="true" SortExpression="CD_CANTIDAD" UniqueName="CD_CANTIDAD" Aggregate="Sum" FooterText=" ">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txt_cancom" runat="server" Enabled="true" DbValue='<%# Bind("CD_CANTIDAD") %>' OnTextChanged="txt_cancom_TextChanged" AutoPostBack="true"
                                        MinValue="0" Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridTemplateColumn DataField="NEW_TOT" HeaderText="New Total" HeaderStyle-Width="95px" Visible="True"
                                Resizable="true" SortExpression="NEW_TOT" UniqueName="NEW_TOT" Aggregate="Sum" FooterText=" ">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="txt_vlrtot" runat="server" Enabled="false" DbValue='<%# Bind("NEW_TOT") %>'
                                        MinValue="0" Value="0" Width="85px" Visible="true" NumberFormat-DecimalDigits="0">
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="VLRTOT" HeaderButtonType="TextButton" HeaderStyle-Width="90px" DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true"
                                HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRTOT"
                                UniqueName="VLRTOT" FooterText="Total: " Aggregate="Sum">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <NoRecordsTemplate>
                            <div class="alert alert-danger">
                                <strong>¡No Records to Display!</strong>
                            </div>
                        </NoRecordsTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                    </telerik:RadAjaxPanel>
            </div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 1900;" RenderMode="Lightweight" Skin="Bootstrap">
                <Shortcuts>
                    <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                    <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                </Shortcuts>
                <Windows>
                    <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_mensaje" runat="server" Width="100%">
                                <div style="padding: 5px 5px 5px 5px">
                                    <ul>
                                        <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                                    </ul>
                                    <div style="text-align: center;">
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>

            <asp:ModalPopupExtender ID="mpEmail" runat="server" BackgroundCssClass="modalBackground"
                BehaviorID="meEmail" PopupControlID="pnEmail" TargetControlID="Button3"
                CancelControlID="bt_cerrar">
            </asp:ModalPopupExtender>
            <div style="display: none;">
                <asp:Button ID="Button3" runat="server" Text="Button" />
            </div>
            <asp:Panel ID="pnEmail" runat="server" CssClass="modalPopup" Style="display: none;">
                <fieldset class="cssFieldSetContainer" style="width: 900px !important">
                    <div class="box">
                        <div class="title">
                            <h5>Enviar Email</h5>
                        </div>
                    </div>
                    <div style="padding: 5px 5px 5px 5px">
                        <table>
                            <tr>
                                <td>
                                    <label>Para</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadAutoCompleteBox runat="server" ID="txt_para" InputType="text" TextSettings-SelectionMode="Multiple"
                                        DataSourceID="obj_usuarios" Width="90%" DataTextField="usuario" DropDownWidth="370px"
                                        OnClientEntryAdding="OnClientEntryAddingHandler" AllowCustomEntry="true" Delimiter=";"
                                        DropDownHeight="370px" Filter="StartsWith" Style="z-index: 9001;">
                                        <DropDownItemTemplate>
                                            <table cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "usuario")%>
                                                    </td>
                                                    <%-- <td><label> - </label></td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "usua_email")%>
                                                    </td>--%>
                                                </tr>
                                            </table>
                                        </DropDownItemTemplate>
                                    </telerik:RadAutoCompleteBox>
                                    <asp:ObjectDataSource ID="obj_usuarios" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetUsuariosEmail" TypeName="XUSS.BLL.Tareas.LstTareasBL">
                                        <SelectParameters>
                                            <asp:Parameter Name="connection" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_para"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Asunto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_asunto" runat="server" Width="150px" ValidationGroup="UpdateBoton">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_asunto"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <telerik:RadEditor runat="server" ID="edt_body" SkinID="DefaultSetOfTools"
                                        Height="475px" EditModes="Design" Width="100%" RenderMode="Lightweight">
                                    </telerik:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Server</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_server" runat="server" Width="150px" ValidationGroup="UpdateBoton">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_server"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Puerto</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_port" runat="server" Width="150px" ValidationGroup="UpdateBoton">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_port"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>User</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_user" runat="server" Width="150px" ValidationGroup="UpdateBoton">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_user"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Password</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_password" runat="server" Width="150px" ValidationGroup="UpdateBoton">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_password"
                                        ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <telerik:RadButton ID="bt_cerrar" runat="server" Text="Cerrar" Icon-PrimaryIconCssClass="rbCancel"
                                        RenderMode="Lightweight">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
            </asp:Panel>
        </div>
        <div id="footer">
            <p>
                <asp:Literal ID="litCopyRight" runat="server"></asp:Literal>
            </p>
        </div>

        <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
                <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="obj_terpago" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetTerminosPago" TypeName="XUSS.BLL.Comun.ComunBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:SessionParameter Name="InCodemp" Type="String" SessionField="CODEMP" />
            </SelectParameters>
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
        <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
