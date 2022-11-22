﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="Administrador.Usuario" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%--<%@ Register src="../../UserControls/UserControlFind.ascx" tagname="UserControlFind" tagprefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function TestClick(sender, args) {
            setHijos(args.get_node(), args.get_node().get_checked());
        }

        function setHijos(nodo,check) {
            var i = 0;
            var hijos = nodo.get_allNodes();
            for (i = 0; i < hijos.length; i++) {
                hijos[i].set_checked(check);
                setHijos(hijos[i], check)
            }
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	</telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                
                <UpdatedControls>
                    
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Block"/>
                </UpdatedControls>
                
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="pnDetails">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnDetails" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="UserControlFilter1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UserControlFilter1" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" 
        DecoratedControls="Default, Textbox, Textarea, Label, Select" />
		<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" >
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" > 
    <telerik:RadListView ID="RadListView1" runat="server" PageSize="1" AllowPaging="True" 
        ItemPlaceholderID="ItemContainer" DataSourceID="odsUsuarios" DataKeyNames="UsuaUsuario" 
        onitemdatabound="RadListView1_ItemDataBound"
        OnItemCommand="RadListView1_ItemCommand" >
            
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                <div class="box">
                    <div class="title">
	                    <h5>Usuarios</h5>
                    </div>
                </div>
                <div class="paginadorRadListView">
					<telerik:RadDataPager ID="RadDataPager1" runat="server" RenderMode="Lightweight"
						PagedControlID="RadListView1" PageSize="1">
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
                <asp:Panel ID="ItemContainer" runat="server" />
                
            </LayoutTemplate>
            <ItemTemplate>
               <div runat="server" id="BotonesBarra"   class="toolBarsMenu"  >
				    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC"/>
				    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar" SkinID="SkinEditUC" />
				    <asp:ImageButton ID="iBtnDelete" runat="server" CommandName="Delete" SkinID="SkinDeleteUC" Text="Eliminar" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" />
				    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />            --%>           
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" ToolTip="Anular Registro" />
			    </div>
                <asp:Panel ID="pnItemMaster" runat="server">                    
                        <table border="0" cellspacing="8" >
                            <tr>
                                <td style="width:25%">
                                    <label>Login</label>
                                    
                                </td>
                                <td style="width:25%">
                                    <telerik:RadTextBox ID="TextBox1" runat="server" Text='<%# Bind("UsuaUsuario") %>' Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:25%">
                                    <label>Identificación</label>
                                    
                                </td>
                                <td style="width:25%">
                                    <telerik:RadNumericTextBox ID="txtIdentification" runat="server" Text='<%# Bind("UsuaIdentifica") %>' Enabled="false">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Nombres</label>
                                </td>
                                <td style="width:25%">
                                    <telerik:RadTextBox ID="txtNames" runat="server" Text='<%# Bind("UsuaNombres") %>' Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:25%">
                                    <label>Teléfonos</label>
                                </td>
                                <td style="width:25%">
                                    <telerik:RadTextBox ID="TextBox6" runat="server" Text='<%# Bind("UsuaTelefonos") %>' Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Dirección</label>
                                </td>
                                <td style="width:25%">
                                    <telerik:RadTextBox ID="TextBox5" runat="server" Text='<%# Bind("UsuaDireccion") %>' Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="width:25%">
                                    <label>e-Mail</label>
                                </td>
                                <td style="width:25%">
                                    <telerik:RadTextBox ID="TextBox7" runat="server" Text='<%# Bind("UsuaEmail") %>' Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Contraseña caduca</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox7" runat="server" 
                                        Checked='<%# GetBoolean(Eval("UsuaClaveCaduca")) %>' Enabled="False" />
                                </td>
                                <td style="width:25%">
                                    <label>Publica Reporte</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox3" runat="server" 
                                        Checked='<%# GetBoolean(Eval("UsuaPublicaReporte")) %>' Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Restringir IP</label>
                                    
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox4" runat="server" 
                                        Checked='<%# GetBoolean(Eval("UsuaRestringeIp")) %>' Enabled="False" />
                                </td>
                                <td style="width:25%">
                                    <label>Restringir Hora</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox5" runat="server" 
                                        Checked='<%# GetBoolean(Eval("UsuaRestringeHora")) %>' Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Habilitado</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox6" runat="server" 
                                        Checked='<%# GetBoolean(Eval("UsuaEstado"))%>' Enabled="False" />
                                </td>
                                <td style="width:25%">
                                    &nbsp;</td>
                                <td style="width:25%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:25%">
                                    <label>Administrador</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# GetBoolean(Eval("usuaadministrador"))%>' Enabled="False" />
                                </td>
                            </tr>
                        </table>                    
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>                    
                    <table border="0" cellspacing="8">
                        <tr>
                            <td style="width:25%">
                                <label>Login</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox1" runat="server" Text='<%# Bind("UsuaUsuario") %>' MaxLength="20" Width="120px" ReadOnly="true">
                                </telerik:RadTextBox>
                                <asp:TextBox ID="TextBox2" runat="server" Visible="false" Text='<%# Bind("UsuaSecuencia") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" 
                                    ControlToValidate="TextBox1" Display="Dynamic"
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />    
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="width:25%">
                                <label>Identificación</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadNumericTextBox ID="txtIdentification" runat="server" Text='<%# Bind("UsuaIdentifica") %>' MaxLength="15" Width="120px">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvIdentification" runat="server" 
                                    ControlToValidate="txtIdentification" Display="Dynamic"
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Nombres</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="txtNames" runat="server" Text='<%# Bind("UsuaNombres") %>' MaxLength="100" Width="120px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvNames" runat="server" 
                                    ControlToValidate="txtNames" Display="Dynamic"  
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="width:25%">
                                <label>Teléfonos</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox6" runat="server" Text='<%# Bind("UsuaTelefonos") %>' MaxLength="100" Width="120px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Dirección</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox5" runat="server" Text='<%# Bind("UsuaDireccion") %>' MaxLength="200" Width="120px">
                                </telerik:RadTextBox>
                            </td>
                            <td style="width:25%">
                                <label>e-Mail</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox7" runat="server" Text='<%# Bind("UsuaEmail") %>' MaxLength="100" Width="120px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                                    ControlToValidate="TextBox7" Display="Dynamic"
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMail" runat="server" 
                                    ControlToValidate="TextBox7" Display="Dynamic" 
                                    ErrorMessage="correo electrónico no válido" SetFocusOnError="True" 
                                    ValidationExpression="^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="txtPassword" runat="server" Text='<%# Bind("UsuaClave") %>' Width="120px" Visible="false">
                                </telerik:RadTextBox>
                                <asp:Panel ID="pnPopUp" runat="server" CssClass="modalPopup" 
                                    style="display:none;" Enabled="false">
                                    <asp:Panel ID="pnTitle" runat="server" 
                                        Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                                        <div>
                                            <p>
                                                Cambio de Contraseña editar</p>
                                        </div>
                                    </asp:Panel>
                                    <div>
                                        <fieldset>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Contraseña:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtSetPassword" runat="server" MaxLength="20" 
                                                            TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSetPassword" runat="server"  
                                                            ControlToValidate="txtSetPassword"
                                                            Enabled="false">
                                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                                        </asp:RequiredFieldValidator>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Confirmar contraseña:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPasswordConfirm" runat="server" 
                                                            ControlToValidate="txtPasswordConfirm"
                                                            Enabled="false">
                                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:center">
                                                        
                                                        <asp:CompareValidator ID="cmpPasswords" runat="server" 
                                                            ControlToCompare="txtSetPassword" ControlToValidate="txtPasswordConfirm" 
                                                            Display="Dynamic" Enabled="False" ErrorMessage="Las contraseñas no coinciden" 
                                                            SetFocusOnError="True"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:center">
                                                        <asp:Button ID="btnAccept" runat="server" CommandName="Accept" Text="Aceptar" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                                <label>Contraseña caduca</label>
                                <cc1:ModalPopupExtender ID="ModalPopup" runat="server" 
                                    BackgroundCssClass="modalBackground" CancelControlID="btnCancel" 
                                    DropShadow="true" PopupControlID="pnPopUp" PopupDragHandleControlID="pnTitle" 
                                    TargetControlID="pnPopUp">
                                </cc1:ModalPopupExtender>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox7" runat="server" 
                                    Checked='<%# Bind("UsuaClaveCaduca") %>' />
                            </td>
                            <td style="width:25%">
                                <label>Debe Cambiar Contraseña</label>
                                
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="chkChangePassword" runat="server" 
                                    Checked='<%# Bind("UsuaCambiaClave") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Restringir IP</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox4" runat="server" 
                                    Checked='<%# Bind("UsuaRestringeIp") %>' />
                            </td>
                            <td style="width:25%">
                                <label>Publica Reporte</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox3" runat="server" 
                                    Checked='<%# Bind("UsuaPublicaReporte") %>' />
                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Habilitado</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox6" runat="server" 
                                    Checked='<%# Bind("UsuaEstado") %>' />
                            </td>
                            <td style="width:25%">
                                <label>Restringir Hora</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox5" runat="server" 
                                    Checked='<%# Bind("UsuaRestringeHora") %>' />
                            </td>
                        </tr>
                        <tr>
                                <td style="width:25%">
                                    <label>Administrador</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# Bind("usuaadministrador")%>' Enabled="true" />
                                </td>
                            </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="Button1" runat="server" CommandName="Update" Text="Aceptar" />
                                <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" Text="Cancelar" />
                                <asp:Button ID="Button4" runat="server" CommandName="ShowModal" 
                                    Text="Cambiar Contraseña" />
                                <asp:Button ID="Button3" runat="server" CommandName="ResetPassword" 
                                    Text="Restaurar Contraseña" />
                            </td>
                        </tr>
                    </table>      
            </EditItemTemplate>
            <InsertItemTemplate>    
                    <table border="0" cellspacing="8">
                        <tr>
                            <td style="width:25%">
                                <label>Login</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox1" runat="server" Text='<%# Bind("UsuaUsuario") %>' Width="120px" MaxLength="20">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" 
                                    ControlToValidate="TextBox1" Display="Dynamic"  
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="width:25%">
                                <label>Identificación</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadNumericTextBox ID="txtIdentification" runat="server" Text='<%# Bind("UsuaIdentifica") %>' Width="120px" MaxLength="15">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvIdentification" runat="server" 
                                    ControlToValidate="txtIdentification" Display="Dynamic" 
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Nombres</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="txtNames" runat="server" Text='<%# Bind("UsuaNombres") %>' Width="120px" MaxLength="100">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvNames" runat="server" 
                                    ControlToValidate="txtNames" Display="Dynamic"  
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td style="width:25%">
                                <label>Teléfonos</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox6" runat="server" Text='<%# Bind("UsuaTelefonos") %>' Width="120px" MaxLength="100">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Dirección</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox5" runat="server" Text='<%# Bind("UsuaDireccion") %>' Width="120px" MaxLength="200">
                                </telerik:RadTextBox>
                            </td>
                            <td style="width:25%">
                                <label>e-Mail</label>
                            </td>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="TextBox7" runat="server" Text='<%# Bind("UsuaEmail") %>' Width="120px" MaxLength="100">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                                    ControlToValidate="TextBox7" Display="Dynamic" 
                                    SetFocusOnError="True">
                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMail" runat="server" 
                                    ControlToValidate="TextBox7" Display="Dynamic" 
                                    ErrorMessage="correo electrónico no válido" SetFocusOnError="True" 
                                    ValidationExpression="^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <telerik:RadTextBox ID="txtPassword" runat="server" Text='<%# Bind("UsuaClave") %>' Width="120px" Visible="false">
                                </telerik:RadTextBox>
                                <asp:Panel ID="pnPopUp" runat="server" CssClass="modalPopup" 
                                    style="display:none;" Enabled="false">
                                    <asp:Panel ID="pnTitle" runat="server" 
                                        Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black">
                                        <div>
                                            <p>
                                                Cambio de Contraseña Insertar</p>
                                        </div>
                                    </asp:Panel>
                                    <div>
                                        <fieldset>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Contraseña:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtSetPassword" runat="server" MaxLength="20" 
                                                            TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSetPassword" runat="server" 
                                                            ControlToValidate="txtSetPassword" Enabled="false" >
                                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                                        </asp:RequiredFieldValidator>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Confirmar contraseña:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPasswordConfirm" runat="server" 
                                                            ControlToValidate="txtPasswordConfirm" Enabled="false">
                                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:center">
                                                        
                                                        <asp:CompareValidator ID="cmpPasswords" runat="server" 
                                                            ControlToCompare="txtSetPassword" ControlToValidate="txtPasswordConfirm" 
                                                            Display="Dynamic" Enabled="False" ErrorMessage="Las contraseñas no coinciden" 
                                                            SetFocusOnError="True"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align:center">
                                                        <asp:Button ID="btnAccept" runat="server" CommandName="Accept" Text="Aceptar" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" />
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </fieldset>
                                    </div>
                                </asp:Panel>
                                <label>Contraseña caduca</label>
                                <cc1:ModalPopupExtender ID="ModalPopup" runat="server" 
                                    BackgroundCssClass="modalBackground" CancelControlID="btnCancel" 
                                    DropShadow="true" PopupControlID="pnPopUp" PopupDragHandleControlID="pnTitle" 
                                    TargetControlID="pnPopUp">
                                </cc1:ModalPopupExtender>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox7" runat="server" 
                                    Checked='<%# GetBoolean(Eval("UsuaClaveCaduca")) %>' />
                            </td>
                            <td style="width:25%">
                                <label>Debe Cambiar Contraseña</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="chkChangePassword" runat="server" 
                                    Checked='<%# GetBoolean(Eval("UsuaCambiaClave")) %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Restringir IP</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox4" runat="server" 
                                    Checked='<%# Bind("UsuaRestringeIp") %>' />
                            </td>
                            <td style="width:25%">
                                <label>Publica Reporte</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox3" runat="server" 
                                    Checked='<%# Bind("UsuaPublicaReporte") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:25%">
                                <label>Habilitado</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox6" runat="server" 
                                    Checked='<%# Bind("UsuaEstado") %>' />
                            </td>
                            <td style="width:25%">
                                <label>Restringir Hora</label>
                            </td>
                            <td style="width:25%">
                                <asp:CheckBox ID="CheckBox5" runat="server" 
                                    Checked='<%# Bind("UsuaRestringeHora") %>' />
                            </td>
                        </tr>
                        <tr>
                                <td style="width:25%">
                                    <label>Administrador</label>
                                </td>
                                <td style="width:25%">
                                    <asp:CheckBox ID="CheckBox1" runat="server" 
                                        Checked='<%# Bind("usuaadministrador")%>' Enabled="true" />
                                </td>
                            </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="Button1" runat="server" CommandName="PerformInsert" 
                                    Text="Aceptar" />
                                <asp:Button ID="Button2" runat="server" CausesValidation="False" 
                                    CommandName="Cancel" CommandArgument="Insertar" Text="Cancelar" />
                                &nbsp;<asp:Button ID="Button4" runat="server" CommandName="ShowModal" 
                                    Text="Establecer Contraseña" />
                                <asp:Button ID="Button3" runat="server" CommandName="ResetPassword" 
                                    Text="Restaurar Contraseña" />
                            </td>
                        </tr>
                    </table>
            </InsertItemTemplate>
            <EmptyDataTemplate>
                <%--<uc3:UserControlFind ID="UserControlFind1" runat="server" Titulo="usuario" Entidad="AdmiUsuario"  DataSourceID="odsUsuarios" FilterControl="RadListView1"/>   --%>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Usuarios</h5>
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
                                                <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="8">                        
                        <tr>
                            <td><label>Usuario</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_usuario" runat="server"  Enabled="true">
                                    </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </EmptyDataTemplate>
    </telerik:RadListView>
    <asp:Panel ID="pnDetails" runat="server">
        <fieldset class="cssFieldSetContainer" >
            <div class="box">
                <div class="title">
                    <h5>Configuración</h5>
                </div>
            </div>
             <asp:Label ID="lblSistema" runat="server" Text="Sistema"></asp:Label>
                                <asp:DropDownList ID="ddlSistema" runat="server" AutoPostBack="true"
                                    DataTextField="SistNombre" DataValueField="SistSistema" 
                                    onselectedindexchanged="ddlSistema_SelectedIndexChanged">
                                </asp:DropDownList>
                <table style="width:100%">
                     <tr>
                        <td style="width:50%; vertical-align:top;">
                            <fieldset>
                                <legend>Reestricciones Asociadas</legend>
                                
                                <telerik:RadTreeView ID="RadTreeView1" Runat="server" 
                                    DataTextField="AropNombre" enabled="false"  
                                    DataFieldID="AropArbolOpcion" DataFieldParentID="AropIdUnicoPadre" 
                                    DataValueField="AropIdOriginal" onclientnodechecked="TestClick"  
                                    EnableViewState="true" Height="300px" width="100%" OnNodeCheck="RadTreeView1_NodeCheck" 
                                    OnNodeDataBound="RadTreeView1_NodeDataBound" 
                                    CheckBoxes="true" >
                                    
                                    <DataBindings>
                                        <telerik:RadTreeNodeBinding Expanded="true" CategoryField="AropEntidad" />
                                    </DataBindings>
                                </telerik:RadTreeView>
                            </fieldset>
                        </td>
                        <td style="width:50%; vertical-align:top;">
                            <fieldset style="height:317px; ">
                                <legend>Roles Asociados</legend>
                                <div style=" overflow: auto; height: 317px; width:100%">
                                
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" 
                                    DataTextField="rolm_nombre" DataValueField="rolm_rolm" Enabled="false"
                                    onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" RepeatColumns="2"  
                                     Width="100%" >
                                </asp:CheckBoxList>
                                </div>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
		</telerik:RadAjaxPanel>

    <asp:ObjectDataSource ID="odsUsuarios" runat="server"
        SelectMethod="GetList" 
        InsertMethod="Insert"
        TypeName="BLL.Administracion.AdmiUsuarioBL"
        DataObjectTypeName="BE.Administracion.AdmiUsuario"
        EnablePaging="True"
        UpdateMethod="Update"
        DeleteMethod="Delete" oninserted="odsUsuarios_Inserted" 
        onupdated="odsUsuarios_Updated">
         <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=0" />
            <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="1" />
            <asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="1" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
