<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CentroCostos.aspx.cs" Inherits="XUSS.WEB.Contabilidad.CentroCostos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rvl_documentos" runat="server" PageSize="1" AllowPaging="True"
            ItemPlaceholderID="pnlGeneral" DataSourceID="obj_centrocostos" OnItemCommand="rlv_documentos_OnItemCommand"
            OnItemDataBound="rlv_documentos_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Centro de Costos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rvl_documentos"
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
                                Centro de Costos</h5>
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
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_OnClick">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
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
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" Text='<%# Bind("CTC_IDENTI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" Text='<%# Bind("CTC_NOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
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
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" Text='<%# Bind("CTC_IDENTI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="edt_codigo"
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
                                <telerik:RadTextBox ID="edt_nombre" Text='<%# Bind("CTC_NOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edt_nombre"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
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
        <asp:ObjectDataSource ID="obj_centrocostos" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetCentroCostos" InsertMethod="InsertCentroCostos" TypeName="XUSS.BLL.Contabilidad.CentroCostosBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:SessionParameter Name="CTC_CODEMP" Type="String" SessionField="CODEMP" />
                <asp:Parameter Name="CTC_IDENTI" Type="String" />
                <asp:Parameter Name="CTC_NOMBRE" Type="String" />
                <asp:SessionParameter Name="CTC_USUARIO" Type="String" SessionField="UserLogon" />
                <asp:Parameter Name="CTC_ESTADO" Type="String" DefaultValue="AC" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </telerik:RadAjaxPanel>
</asp:Content>
