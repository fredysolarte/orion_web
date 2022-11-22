<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="RegistroClientes.aspx.cs" Inherits="XUSS.WEB.Terceros.RegistroClientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            />
    <asp:Panel runat="server" ID="pnl_principal">
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Registro Terceros</h5>
                </div>
            </div>
        
        <div>
            <table>
                <tr>
                    <td>
                        <label>
                            Identificación</label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="edt_cedula" runat="server" ValidationGroup="val" OnTextChanged="edt_cedula_TextChanged"
                            AutoPostBack="true" Width="245px">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ValidationGroup="val" ControlToValidate="edt_cedula" Display="Dynamic">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Nombre</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_Nombre" runat="server" ValidationGroup="val" Width="245px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ValidationGroup="val" ControlToValidate="edt_Nombre" Display="Dynamic">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Apellido</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_Apellido" runat="server" ValidationGroup="val" Width="245px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ValidationGroup="val" ControlToValidate="edt_Apellido" Display="Dynamic">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Email</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_email" runat="server" ValidationGroup="val" Width="238px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ValidationGroup="val" ControlToValidate="edt_email" Display="Dynamic">
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revemail" runat="server" ControlToValidate="edt_email"
                            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="val"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td><label>Ocupación</label></td>
                    <td><telerik:RadTextBox ID="edt_ocupacion" runat="server" ValidationGroup="val" Width="238px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Telefono</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_telefono" runat="server" ValidationGroup="val" Width="245px">
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
                        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" OnClick="btn_cancelar_Click"/>
                        <asp:Button ID="btn_acpetar" runat="server" Text="Aceptar" OnClick="btn_aceptar_Click" ValidationGroup="val"/>
                    </td>
                </tr>
            </table>
        </div>
        </fieldset>
    </asp:Panel>
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" />
    </div>
    <asp:ModalPopupExtender ID="mpMessage" runat="server" PopupControlID="Panel1"
        TargetControlID="Button1" BackgroundCssClass="modalBackground" CancelControlID="Button3">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">
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
                    <asp:Button ID="Button3" runat="server" Text="Cerrar" />
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    </telerik:RadAjaxPanel>

</asp:Content>
