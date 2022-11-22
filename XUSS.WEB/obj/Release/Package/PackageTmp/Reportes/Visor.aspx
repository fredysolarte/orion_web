<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Visor.aspx.cs" Inherits="XUSS.WEB.Reportes.Visor" %>
<%@ Register Assembly="FastReport.Web, Version=2014.1.6.0, Culture=neutral, PublicKeyToken=db7e5ce63278458c"
    Namespace="FastReport.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <cc1:WebReport ID="WebReport1" runat="server" Width="100%" />    
</asp:Content>


