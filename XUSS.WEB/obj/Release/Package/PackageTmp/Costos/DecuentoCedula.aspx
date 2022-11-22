<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="DecuentoCedula.aspx.cs" Inherits="XUSS.WEB.Costos.DecuentoCedula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rvl_descuento" runat="server" PageSize="1" AllowPaging="True"
            OnItemDataBound="rvl_descuento_ItemDataBound" ItemPlaceholderID="pnlGeneral"
            DataSourceID="obj_descuentocedula" OnItemCommand="rvl_descuento_ItemCommand"
            OnItemInserting="rvl_descuento_ItemInserting">
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
                                                <%--<asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />--%>
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
                                <telerik:RadNumericTextBox ID="edt_cedula" runat="server" Culture="es-CO" Width="358px">
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
                                <telerik:RadTextBox ID="edt_nombre" runat="server" Culture="es-CO" Width="358px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Apellido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_apellido" runat="server" Culture="es-CO" Width="358px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Contacto</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_contacto" runat="server" Culture="es-CO" Width="358px">
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
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                    <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"
                        SkinID="SkinEditUC" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td colspan="3">
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
                            <td colspan="3">
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
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbNombre" Text='<%# Bind("TRNOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Apellido</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbApellido" Text='<%# Bind("TRAPELLI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Contacto</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbContacto" Text='<%# Bind("TRCONTAC") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Inicial</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'
                                    Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>
                                    F. Final</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecfin" runat="server" SelectedDate='<%# Bind("FECHAFIN") %>'
                                    Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Valor</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="edt_valor" Text='<%# Bind("VALOR") %>' runat="server"
                                    Enabled="false" Culture="es-CO" Width="358px" MaxValue="100" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="edt_estado" Text='<%# this.GetEstado(Eval("ESTADO")) %>'
                                    runat="server" Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Indefinido</label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chk_terminos" runat="server" Checked='<%# this.GetValCheck(Eval("CONDICION_2")) %>'
                                    Enabled="false" />
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
                            <td colspan="3">
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
                            <td colspan="3">
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
                            <td colspan="3">
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
                                    Apellido</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbApellido" Text='<%# Bind("TRAPELLI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rtbApellido"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Contacto</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbContacto" Text='<%# Bind("TRCONTAC") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Nacimiento</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="rcbDia" runat="server" ValidationGroup="val">
                                    <asp:ListItem Value="Seleccione" Text="" />
                                    <asp:ListItem Value="1" Text="1" />
                                    <asp:ListItem Value="2" Text="2" />
                                    <asp:ListItem Value="3" Text="3" />
                                    <asp:ListItem Value="4" Text="4" />
                                    <asp:ListItem Value="5" Text="5" />
                                    <asp:ListItem Value="6" Text="6" />
                                    <asp:ListItem Value="7" Text="7" />
                                    <asp:ListItem Value="8" Text="8" />
                                    <asp:ListItem Value="9" Text="9" />
                                    <asp:ListItem Value="10" Text="10" />
                                    <asp:ListItem Value="11" Text="11" />
                                    <asp:ListItem Value="12" Text="12" />
                                    <asp:ListItem Value="13" Text="13" />
                                    <asp:ListItem Value="14" Text="14" />
                                    <asp:ListItem Value="15" Text="15" />
                                    <asp:ListItem Value="16" Text="16" />
                                    <asp:ListItem Value="17" Text="17" />
                                    <asp:ListItem Value="18" Text="18" />
                                    <asp:ListItem Value="19" Text="19" />
                                    <asp:ListItem Value="20" Text="20" />
                                    <asp:ListItem Value="21" Text="21" />
                                    <asp:ListItem Value="22" Text="22" />
                                    <asp:ListItem Value="23" Text="23" />
                                    <asp:ListItem Value="24" Text="24" />
                                    <asp:ListItem Value="25" Text="25" />
                                    <asp:ListItem Value="26" Text="26" />
                                    <asp:ListItem Value="27" Text="27" />
                                    <asp:ListItem Value="28" Text="28" />
                                    <asp:ListItem Value="29" Text="29" />
                                    <asp:ListItem Value="30" Text="30" />
                                    <asp:ListItem Value="31" Text="31" />
                                </asp:DropDownList>
                                <asp:DropDownList ID="rcbMes" runat="server">
                                    <asp:ListItem Value="Seleccione" Text="" />
                                    <asp:ListItem Value="01" Text="Enero" />
                                    <asp:ListItem Value="02" Text="Febrero" />
                                    <asp:ListItem Value="03" Text="Marzo" />
                                    <asp:ListItem Value="04" Text="Abril" />
                                    <asp:ListItem Value="05" Text="Mayo" />
                                    <asp:ListItem Value="06" Text="Junio" />
                                    <asp:ListItem Value="07" Text="Julio" />
                                    <asp:ListItem Value="08" Text="Agosto" />
                                    <asp:ListItem Value="09" Text="Septiembre" />
                                    <asp:ListItem Value="10" Text="Octubre" />
                                    <asp:ListItem Value="11" Text="Noviembre" />
                                    <asp:ListItem Value="12" Text="Diciembre" />
                                </asp:DropDownList>
                                <asp:DropDownList ID="rcbAno" runat="server">
                                    <asp:ListItem Value="Seleccione" Text="" />
                                    <asp:ListItem Value="1940" Text="1940" />
                                    <asp:ListItem Value="1941" Text="1941" />
                                    <asp:ListItem Value="1942" Text="1942" />
                                    <asp:ListItem Value="1943" Text="1943" />
                                    <asp:ListItem Value="1944" Text="1944" />
                                    <asp:ListItem Value="1945" Text="1945" />
                                    <asp:ListItem Value="1946" Text="1946" />
                                    <asp:ListItem Value="1947" Text="1947" />
                                    <asp:ListItem Value="1948" Text="1948" />
                                    <asp:ListItem Value="1949" Text="1949" />
                                    <asp:ListItem Value="1950" Text="1950" />
                                    <asp:ListItem Value="1951" Text="1951" />
                                    <asp:ListItem Value="1952" Text="1952" />
                                    <asp:ListItem Value="1953" Text="1953" />
                                    <asp:ListItem Value="1954" Text="1954" />
                                    <asp:ListItem Value="1955" Text="1955" />
                                    <asp:ListItem Value="1956" Text="1956" />
                                    <asp:ListItem Value="1957" Text="1957" />
                                    <asp:ListItem Value="1958" Text="1958" />
                                    <asp:ListItem Value="1959" Text="1959" />
                                    <asp:ListItem Value="1960" Text="1960" />
                                    <asp:ListItem Value="1961" Text="1961" />
                                    <asp:ListItem Value="1962" Text="1962" />
                                    <asp:ListItem Value="1963" Text="1963" />
                                    <asp:ListItem Value="1964" Text="1964" />
                                    <asp:ListItem Value="1965" Text="1965" />
                                    <asp:ListItem Value="1966" Text="1966" />
                                    <asp:ListItem Value="1967" Text="1967" />
                                    <asp:ListItem Value="1968" Text="1968" />
                                    <asp:ListItem Value="1969" Text="1969" />
                                    <asp:ListItem Value="1970" Text="1970" />
                                    <asp:ListItem Value="1971" Text="1971" />
                                    <asp:ListItem Value="1972" Text="1972" />
                                    <asp:ListItem Value="1973" Text="1973" />
                                    <asp:ListItem Value="1974" Text="1974" />
                                    <asp:ListItem Value="1975" Text="1975" />
                                    <asp:ListItem Value="1976" Text="1976" />
                                    <asp:ListItem Value="1977" Text="1977" />
                                    <asp:ListItem Value="1978" Text="1978" />
                                    <asp:ListItem Value="1979" Text="1979" />
                                    <asp:ListItem Value="1980" Text="1980" />
                                    <asp:ListItem Value="1981" Text="1981" />
                                    <asp:ListItem Value="1982" Text="1982" />
                                    <asp:ListItem Value="1983" Text="1983" />
                                    <asp:ListItem Value="1984" Text="1984" />
                                    <asp:ListItem Value="1985" Text="1985" />
                                    <asp:ListItem Value="1986" Text="1986" />
                                    <asp:ListItem Value="1987" Text="1987" />
                                    <asp:ListItem Value="1988" Text="1988" />
                                    <asp:ListItem Value="1989" Text="1989" />
                                    <asp:ListItem Value="1990" Text="1990" />
                                    <asp:ListItem Value="1991" Text="1991" />
                                    <asp:ListItem Value="1992" Text="1992" />
                                    <asp:ListItem Value="1993" Text="1993" />
                                    <asp:ListItem Value="1994" Text="1994" />
                                    <asp:ListItem Value="1995" Text="1995" />
                                    <asp:ListItem Value="1996" Text="1996" />
                                    <asp:ListItem Value="1997" Text="1997" />
                                    <asp:ListItem Value="1998" Text="1998" />
                                    <asp:ListItem Value="1999" Text="1999" />
                                    <asp:ListItem Value="2000" Text="2000" />
                                    <asp:ListItem Value="2001" Text="2001" />
                                    <asp:ListItem Value="2002" Text="2002" />
                                    <asp:ListItem Value="2003" Text="2003" />
                                    <asp:ListItem Value="2004" Text="2004" />
                                    <asp:ListItem Value="2005" Text="2005" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Inicial</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_fecini"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>
                                    F. Final</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecfin" runat="server" SelectedDate='<%# Bind("FECHAFIN") %>'
                                    Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_fecfin"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Valor %</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="edt_valor" Text='<%# Bind("VALOR") %>' runat="server"
                                    Culture="es-CO" Width="358px" MaxValue="100" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="edt_valor"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Indefinido</label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chk_terminos" runat="server" Checked="false" OnCheckedChanged="chk_terminos_CheckedChanged"
                                    AutoPostBack="true" />
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
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadNumericTextBox ID="RadNumericTextBox1" Text='<%# Bind("ID") %>' runat="server"
                                    Enabled="false" Visible="false" Culture="es-CO" Width="358px" OnTextChanged="rtbCodigo_OnTextChanged"
                                    AutoPostBack="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td colspan="3">
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
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="rtbCodigo" Text='<%# Bind("CONDICION_1") %>' runat="server"
                                    Enabled="false" Culture="es-CO" Width="358px" OnTextChanged="rtbCodigo_OnTextChanged"
                                    AutoPostBack="true">
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
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbNombre" Text='<%# Bind("TRNOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rtbNombre"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Apellido</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbApellido" Text='<%# Bind("TRAPELLI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rtbApellido"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Contacto</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="rtbContacto" Text='<%# Bind("TRCONTAC") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Inicial</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_fecini"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>
                                    F. Final</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecfin" runat="server" SelectedDate='<%# Bind("FECHAFIN") %>'
                                    Enabled="true">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_fecfin"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Valor %</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="edt_valor" Text='<%# Bind("VALOR") %>' runat="server"
                                    Culture="es-CO" Width="358px" MaxValue="100" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="edt_valor"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Indefinido</label>
                            </td>
                            <td colspan="3">
                                <asp:CheckBox ID="chk_terminos" runat="server" Checked="false" OnCheckedChanged="chk_terminos_CheckedChanged"
                                    AutoPostBack="true" />
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
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" AppendDataBoundItems="true" SelectedValue='<%# Bind("ESTADO") %>'>
                                    <Items>
                                        <telerik:RadComboBoxItem Value="AC" Text="Activo" />
                                        <telerik:RadComboBoxItem Value="CE" Text="Cerrado" />
                                        <telerik:RadComboBoxItem Value="AN" Text="Anulado" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_descuentocedula" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDescuentosCedula" TypeName="XUSS.BLL.Costos.DecuentoCedulaBL"
        InsertMethod="InsertDecuento" OnInserted="obj_descuentocedula_Inserted" OnInserting="obj_descuentocedula_Inserting"
        UpdateMethod="UpdateDecuento">
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
            <asp:Parameter Name="FECHAFIN" Type="DateTime" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRCONTAC" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="CONDICION_2" Type="String" DefaultValue="" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="BODEGA" Type="String" />
            <asp:Parameter Name="VALOR" Type="Double" />
            <asp:Parameter Name="CONDICION_1" Type="String" />
            <asp:Parameter Name="FECHAINI" Type="DateTime" />
            <asp:Parameter Name="FECHAFIN" Type="DateTime" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRCONTAC" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="CONDICION_2" Type="String" DefaultValue="" />
            <asp:Parameter Name="ESTADO" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_almacenes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAlmacenes" TypeName="XUSS.BLL.Costos.DecuentoCedulaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
