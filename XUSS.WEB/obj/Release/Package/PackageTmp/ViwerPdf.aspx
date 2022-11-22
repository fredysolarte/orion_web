<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViwerPdf.aspx.cs" Inherits="XUSS.WEB.ViwerPdf" %>

<%@ Register Assembly="VisorPdf" Namespace="VisorPdf" TagPrefix="PdfViewer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <link href="../App_Themes/Tema2/Images/icon.ico" type="image/x-icon" rel="shortcut icon" /> 
	<title></title>
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">                        
            function Close() {
                //$find(modalPopup.id).close();
                window.close();
            }
            function OnClientEntryAddingHandler(sender, eventArgs) {

            if (sender.get_entries().get_count() > 1) {
                eventArgs.set_cancel(true);
                }                
            }
            
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Guardar los Cambios?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons,Label" />
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
                                <telerik:RadButton ID="btn_close" runat="server" Text="Cerrar" Icon-PrimaryIconCssClass="rbCancel"
                                    OnClientClicked="Close" Width="100%" AutoPostBack="false" RenderMode="Lightweight" Skin="Bootstrap" >
                                </telerik:RadButton>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>

                </telerik:RadPanelBar>
            </div>
            <div id="right">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
                <PdfViewer:VisorPdf ID="displaypdf1" runat="server" BorderStyle="Inset" BorderWidth="2px"
                    Style="height: 500px;" Width="800px" />
            </div>
    </div>
    <div id="footer">
		<p>
			<asp:Literal ID="litCopyRight" runat="server"></asp:Literal></p>
	</div>
    </form>
</body>
</html>

