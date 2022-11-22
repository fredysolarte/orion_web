<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Obsequios.aspx.cs" Inherits="XUSS.WEB.Costos.Obsequios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Web20" />
        <telerik:RadListView ID="rvl_descuento" runat="server" PageSize="1" AllowPaging="True" OnItemDataBound="rvl_descuento_ItemDataBound"
            ItemPlaceholderID="pnlGeneral" DataSourceID="obj_obsequiocedula" OnItemCommand="rvl_descuento_ItemCommand">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Descuento x Cedula</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rvl_descuento"
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
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Descuento x Cedula</h5>
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
                                                <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
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
                                    Cedula</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_cedula" runat="server" Culture="es-CO" Width="358px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_buscar" runat="server" OnClick="btn_buscar_OnClick" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_almacen" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_almacenes" SelectedValue='<%# Bind("BODEGA") %>' Enabled="false"
                                    DataTextField="BDNOMBRE" DataValueField="BDBODEGA">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cedula</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="rtbCodigo" Text='<%# Bind("CONDICION_1") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="rtbNombre" Text='<%# Bind("TRNOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'
                                    Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Can. Prendas</label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_prendas" Text='<%# Bind("CONDICION_5") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_estado" Text='<%# this.GetEstado(Eval("ESTADO")) %>'
                                    runat="server" Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_almacen" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_almacenes" SelectedValue='<%# Bind("BODEGA") %>' DataTextField="BDNOMBRE"
                                    DataValueField="BDBODEGA">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Cedula</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="rtbCodigo" Text='<%# Bind("CONDICION_1") %>' runat="server"
                                    Culture="es-CO" Width="358px" OnTextChanged="rtbCodigo_OnTextChanged" AutoPostBack="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rtbCodigo"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="rtbNombre" Text='<%# Bind("TRNOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rtbNombre"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_fecini"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <label>Valor %</label>
                            </td>
                            <td>
                            <telerik:RadNumericTextBox ID="edt_valor" Text='<%# Bind("VALOR") %>' runat="server"
                                    Culture="es-CO" Width="358px" MaxValue="100" MinValue ="0" >
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="edt_valor"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <%--<tr>
                            <td><label>Estado</label></td>
                            <td>
                            <telerik:RadTextBox ID="edt_estado" Text='<%# this.GetEstado(Eval("ESTADO")) %>'   Runat="server" Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>                    
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_obsequiocedula" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDescuentosCedula" TypeName="XUSS.BLL.Costos.ObsequiosBL"
        InsertMethod="InsertDecuento" oninserted="obj_obsequiocedula_Inserted" 
        oninserting="obj_obsequiocedula_Inserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="BODEGA" Type="String" />
            <asp:Parameter Name="VALOR" Type="Double" />
            <asp:Parameter Name="CONDICION_1" Type="String" />
            <asp:Parameter Name="FECHAINI" Type="DateTime" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_almacenes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAlmacenes" TypeName="XUSS.BLL.Costos.ObsequiosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
