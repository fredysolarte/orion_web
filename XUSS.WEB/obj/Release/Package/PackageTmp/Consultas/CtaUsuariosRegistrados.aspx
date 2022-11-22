<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CtaUsuariosRegistrados.aspx.cs" Inherits="XUSS.WEB.Consultas.CtaUsuariosRegistrados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Default" />
        <telerik:RadListView ID="RadListView1" runat="server" PageSize="1" AllowPaging="True"
            ItemPlaceholderID="pnlGeneral" DataSourceID="obj_list" OnItemCommand="RadListView1_ItemCommand">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Descuentos X Usuarios</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
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
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"
                        SkinID="SkinEditUC" />
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    No. Cedula</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_cedula" runat="server" Text='<%# Bind("TBCODNIT") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" runat="server" Text='<%# Bind("TRNOMBRE") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Apellidos</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_apellidos" runat="server" Text='<%# Bind("TRNOMBR2") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro. Factura</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_factura" runat="server" Text='<%# Bind("TBFACTURA") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro. Aprobacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_aprovacion" runat="server" Text='<%# Bind("TBCODALT") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbestado" runat="server" AppendDataBoundItems="True" Culture="es-CO"
                                    DataTextField="nombre" SelectedValue='<%# Bind("TBESTADO") %>' DataValueField="codigo"
                                    Width="130px" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Seleccione" />
                                        <telerik:RadComboBoxItem Value="AC" Text="Sin Redimir" />
                                        <telerik:RadComboBoxItem Value="CE" Text="Redimido" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Fec. Redimir</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecmod" runat="server" SelectedDate='<%# Bind("TBFECMOD") %>'
                                    Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Descuentos X Usuario</h5>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 7px">
                                                <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    No. Cedula</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_cedula" runat="server">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
                            </td>
                        </tr>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
            <EditItemTemplate>
            <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    No. Cedula</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_cedula" runat="server" Text='<%# Bind("TBCODNIT") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" runat="server" Text='<%# Bind("TRNOMBRE") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Apellidos</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_apellidos" runat="server" Text='<%# Bind("TRNOMBR2") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro. Factura</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_factura" runat="server" Text='<%# Bind("TBFACTURA") %>'
                                    Enabled="true" ValidationGroup="val">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
								ValidationGroup="val" ControlToValidate="edt_factura" Display="Dynamic">
								<asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro. Aprobacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_aprovacion" runat="server" Text='<%# Bind("TBCODALT") %>'
                                    Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbestado" runat="server" AppendDataBoundItems="True" Culture="es-CO"
                                    DataTextField="nombre" SelectedValue='<%# Bind("TBESTADO") %>' DataValueField="codigo"
                                    Width="130px" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="" Text="Seleccione" />
                                        <telerik:RadComboBoxItem Value="AC" Text="Sin Redimir" />
                                        <telerik:RadComboBoxItem Value="CE" Text="Redimido" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Fec. Redimir</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecmod" runat="server" SelectedDate='<%# Bind("TBFECMOD") %>'
                                    Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
						<td colspan="2">
							<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="val"/>
							<asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
						</td>
					    </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>

        <div style="display: none">
            <asp:Button ID="Button2" runat="server" />
        </div>
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlAlertMensaje"
            TargetControlID="Button2" BackgroundCssClass="modalBackground" CancelControlID="Button1">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlAlertMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
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
                        <asp:Button ID="Button1" runat="server" Text="Cerrar" />
                    </div>
                </div>
            </fieldset>
        </asp:Panel>

    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_list" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTB_UserDescuento" TypeName="XUSS.BLL.CtDescuentos.CtaUsuariosRegistradosBL"
         UpdateMethod="UpdateTB_UserDescuento" onupdating="obj_list_Updating">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter Name="TBCODNIT" Type="String" />
			<asp:Parameter Name="TRNOMBRE" Type="String" />
			<asp:Parameter Name="TRNOMBR2" Type="String" />
			<asp:Parameter Name="TBFACTURA" Type="Int32" />
			<asp:Parameter Name="TBCODALT" Type="String" />
			<asp:Parameter Name="TBESTADO" Type="String" />			
		</UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
