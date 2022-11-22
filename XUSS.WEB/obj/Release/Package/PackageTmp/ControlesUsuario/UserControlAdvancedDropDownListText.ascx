<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlAdvancedDropDownListText.ascx.cs" Inherits="XUSS.UserControls.UserControlAdvancedDropDownListText" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style type="text/css">
    .style1
    {
        width: 274px;
    }
</style>

<table>
<tr>
<td>
<telerik:RadTextBox ID="rtxtValue" Runat="server" AutoPostBack="true" 
    ontextchanged="rtxtValue_TextChanged"  
    Width="30px" Skin="Default" Height="20px" >
</telerik:RadTextBox>
</td>
<td class="style1">
<telerik:RadComboBox ID="rcbDropDownList" Runat="server" AutoPostBack="true" 
    onselectedindexchanged="rcbDropDownList_SelectedIndexChanged"  
    onitemdatabound="rcbDropDownList_ItemDataBound" 
    onprerender="rcbDropDownList_PreRender" CausesValidation="False" >
</telerik:RadComboBox>
<asp:Label ID="Label1" runat="server" ForeColor="Red" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="rtxtValue" ErrorMessage="Campo Obligatorio" Enabled="false"  Display="Dynamic" ><asp:Image ID="Image1" runat="server" 
    ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif"  /></asp:RequiredFieldValidator>
</td>
</tr>

</table>