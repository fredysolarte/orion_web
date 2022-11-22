<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisorPantallaCompleta.aspx.cs" Inherits="XUSS.WEB.Reportes.VisorPantallaCompleta" %>
<%@ Register Assembly="FastReport.Web, Version=2014.1.6.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c" Namespace="FastReport.Web" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <link href="../App_Themes/Tema2/Images/icon.ico" type="image/x-icon" rel="shortcut icon" />
    <title></title>
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function fixEditor()
            {
                debugger;
                $find("<%=edt_body.ClientID %>").onParentNodeChanged();
            }
            function loadModal(button, args) { button.set_autoPostBack(false); $find(modalPopup.id).show(); }
            function CloseModal() {
                //$find(modalPopup.id).close();
                $find("meEmail").hide();
            }
            function Close() {
                //$find(modalPopup.id).close();
                window.close();
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
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" RenderMode="Lightweight" Skin="Bootstrap" >
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="All" RenderMode="Lightweight" Skin="Bootstrap" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"  >
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btn_enviar">
                    <UpdatedControls>                        
                        <telerik:AjaxUpdatedControl ControlID="pnEmail" LoadingPanelID="RadAjaxLoadingPanel1"/>
                         <telerik:AjaxUpdatedControl ControlID="pnl_mensaje" LoadingPanelID="RadAjaxLoadingPanel1"/>                           
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <!--header -->
        <div id="header">
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
        <div id="content">
            <div id="left">
                <telerik:RadPanelBar runat="server" ID="pnl_menu" Height="100%" Width="100%" RenderMode="Lightweight" Skin="Bootstrap" >
                    <Items>
                        <telerik:RadPanelItem Text="Herramientas" Expanded="True" ImageUrl="~/App_Themes/Tema2/Images/1-properties.png"  >
                            <Items>
                                <%--<telerik:RadPanelItem Text="Enviar Email" ImageUrl="~/App_Themes/Tema2/Images/2-mail.png"  />--%>
                            </Items>
                            <ContentTemplate>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Enviar Email" Icon-PrimaryIconCssClass="rbMail"
                                    OnClientClicked="onclic_mp" Width="100%" AutoPostBack="false" RenderMode="Lightweight" Skin="Bootstrap" >
                                </telerik:RadButton>
                                <telerik:RadButton ID="btn_close" runat="server" Text="Cerrar" Icon-PrimaryIconCssClass="rbCancel"
                                    OnClientClicked="Close" Width="100%" AutoPostBack="false" RenderMode="Lightweight" Skin="Bootstrap" >
                                </telerik:RadButton>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>

                </telerik:RadPanelBar>
            </div>
            <div id="right">
                <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>--%>
                <cc1:WebReport ID="WebReport1" runat="server" Width="100%" ToolbarStyle="Large" PdfDisplayDocTitle="false" ToolbarIconsStyle="Blue" />                                 
            </div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 1900;" RenderMode="Lightweight" Skin="Bootstrap" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_mensaje" runat="server"  Width="100%">
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
                        <h5>
                            Enviar Email</h5>
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
                                    <telerik:RadTextBox ID="txt_asunto" runat="server" Width="150px" ValidationGroup="UpdateBoton" >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_asunto" 
                                                ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
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
                                    <telerik:RadTextBox ID="txt_server" runat="server" Width="150px" ValidationGroup="UpdateBoton" >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_server" 
                                                ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Puerto</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_port" runat="server" Width="150px" ValidationGroup="UpdateBoton" >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_port" 
                                                ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                                 <td>
                                    <label>User</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_user" runat="server" Width="150px" ValidationGroup="UpdateBoton" >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_user" 
                                                ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Password</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_password" runat="server" Width="150px" ValidationGroup="UpdateBoton" >
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_password" 
                                                ErrorMessage="(*)" ValidationGroup="UpdateBoton">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <telerik:RadButton ID="btn_enviar" runat="server" Text="Enviar" OnClick="btn_enviar_Click" Icon-PrimaryIconCssClass="rbMail"
                                        ValidationGroup="UpdateBoton" CausesValidation="true" RenderMode="Lightweight">
                                    </telerik:RadButton>
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
        
    </form>
</body>
</html>
