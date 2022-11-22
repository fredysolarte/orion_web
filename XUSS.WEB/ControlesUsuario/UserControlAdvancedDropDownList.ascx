<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlAdvancedDropDownList.ascx.cs" Inherits="XUSS.WEB.UserControls.UserControlAdvancedDropDownList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<style type="text/css">
    .style1
    {
        width: 234px;
    }
</style>
<table>
<tr>
<td>
<telerik:RadNumericTextBox ID="rtxtValue" Runat="server" AutoPostBack="true" Visible="false"
    ontextchanged="rtxtValue_TextChanged" 
    Width="30px" Skin="Default" Height="20px" >
    <NumberFormat DecimalDigits="0" />
</telerik:RadNumericTextBox>
</td>
<td class="style1">
<telerik:RadComboBox ID="rcbDropDownList" Runat="server" AutoPostBack="true" 
    onselectedindexchanged="rcbDropDownList_SelectedIndexChanged"  
    onitemdatabound="rcbDropDownList_ItemDataBound"  
    onprerender="rcbDropDownList_PreRender" CausesValidation="False" 
         Height="20px" Width="150px">
</telerik:RadComboBox>
<asp:Label ID="Label1" runat="server" ForeColor="Red" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rtxtValue" ErrorMessage="Campo Obligatorio" Enabled="false" Display="Dynamic">
<asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
</td>
</tr>
</table>