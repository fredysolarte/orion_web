<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TESIS.WEB.Login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>	
    	<link rel="stylesheet" type="text/css" href="App_Themes/TemaLogin/css/util.css" />
	    <link rel="stylesheet" type="text/css" href="App_Themes/TemaLogin/css/main.css" />
    
</head>
<body>    
	<%--<form id="form1" runat="server" >--%>
	
    
	<%--<telerik:RadFormDecorator ID="RadFormDecorator1" Runat="server" 
		DecoratedControls="Buttons" Skin="Web20" />--%>
	<%--<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="Panel1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>  --%>
    <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<div class="login100-form-title" style="background-image: url(App_Themes/TemaLogin/images/bg-01.jpg);">
					<span class="login100-form-title-1">
						Sign In
					</span>
				</div>

				<form class="login100-form validate-form" runat="server" id="form1">
                    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
	                </telerik:RadScriptManager>

					<div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
						<span class="label-input100">Username</span>
						<input class="input100" type="text" name="username" placeholder="Enter Username" runat="server" id="username" >
						<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input m-b-18" data-validate = "Password is required">
						<span class="label-input100">Password</span>
						<input class="input100" type="password" name="pass" placeholder="Enter Password" runat="server" id="pass">
						<span class="focus-input100"></span>
					</div>

				
					<div class="container-login100-form-btn">
						<asp:button ID="LoginButton" runat="server" CommandName="Login" class="login100-form-btn" Text="Sign In"  OnClick="LoginButton_Click">							
						</asp:button>
					</div>
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    
                        <div style="width: 100%; vertical-align: bottom; padding-top: 30px; height: 100%;">
                            <table style="position: relative; float: right; background-color: white; ">
                                <tr>
                                    <td>
                            <label style="position: relative; float: right; background-color: white; ">Conect To</label>
                                        </td>
                                    <td>
                            <telerik:RadComboBox ID="ddlSistema" runat="server" Width="105px" DataSourceID="ObjectDataSource1" Style="position: relative; float: right; background-color: white; " Enabled="false"
                                DataTextField="SistNombre" DataValueField="SistSistema" BorderStyle="None" Font-Size="XX-Small" BorderColor="White" BackColor="White" BorderWidth="0px" EnableOverlay="False" Skin="Default">
                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    
				</form>
			</div>
		</div>
	</div>  
        <%--<asp:Panel ID="Panel1" runat="server">--%>
		<%--<div id="login">--%>
			<%--<asp:Login ID="Login1" runat="server" Width="100%" OnAuthenticate="Login1_Authenticate">
                <LayoutTemplate>
					<div class="title">						
						<div class="corner tl">
						</div>
						<div class="corner tr">
						</div>
					</div>
					<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                    <table width="350px">
                        <tr>
                            <td><label><h5>Sistema</h5></label> </td>
                            <td><telerik:RadComboBox ID="ddlSistema" runat="server" Width="175px" DataSourceID="ObjectDataSource1"
											DataTextField="SistNombre" DataValueField="SistSistema">
										</telerik:RadComboBox></td>
                        </tr>
                        <tr>
                            <td><label><h5>Usuario</h5></label></td>
                            <td>
                            <telerik:RadTextBox ID="UserName" runat="server" Width="175px" Font-Size="Small"></telerik:RadTextBox>
										<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
											ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio."
											ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><label><h5>Password</h5></label></td>
                            <td>
                            <telerik:RadTextBox  ID="Password" runat="server" TextMode="Password" Width="175px" MaxLength="20"
											Font-Size="Small"></telerik:RadTextBox >
										<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
											ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
											ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><label>
                            </label></td>
                            <td></td>
                        </tr>
                        <tr>                            
                            <td colspan="2" align="center">
                            	<telerik:RadButton ID="LoginButton" runat="server" CommandName="Login" Text="Inicio de sesión"
										ValidationGroup="Login1" />
                            </td>
                        </tr>
                    </table>		                                                          			
				</LayoutTemplate>
			</asp:Login>--%>
		<%--</div>--%>
	<%--</asp:Panel>--%>
	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
		SelectMethod="GetList" TypeName="BLL.Administracion.AdmiSistemaBL">
		<SelectParameters>
			<asp:Parameter Name="connection" Type="String" />
			<asp:Parameter Name="filter" Type="String" />
			<asp:Parameter DefaultValue="-1" Name="startRowIndex" Type="Int32" />
			<asp:Parameter DefaultValue="-1" Name="maximumRows" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
    <%--</form>--%>
        
</body>
    <!--===============================================================================================-->
	<script src="App_Themes/TemaLogin/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="App_Themes/TemaLogin/js/main.js"></script>   
</html>
