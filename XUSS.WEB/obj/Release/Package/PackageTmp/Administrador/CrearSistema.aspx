<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CrearSistema.aspx.cs" Inherits="Administrador.CrearSistema" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../ControlesUsuario/LoadFileBinary.ascx" TagName="LoadFileBinary"
    TagPrefix="uc1" %>--%>
<%--<%@ Register src="../UserControls/UserControlFind.ascx" tagname="UserControlFind" tagprefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView11" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetList" TypeName="BLL.Administracion.AdmiSistemaBL" DataObjectTypeName="BE.Administracion.AdmiSistema"
        InsertMethod="Insert" UpdateMethod="Update" DeleteMethod="Delete">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, Select" />
        <telerik:RadListView ID="RadListView11" runat="server" DataSourceID="ObjectDataSource1"
            ItemPlaceholderID="ItemContainer" PageSize="1" AllowPaging="True" DataKeyNames="SistSistema"
            OnItemCommand="RadListView11_ItemCommand" OnItemDataBound="RadListView11_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Sistemas</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView11" RenderMode="Lightweight"
                            PageSize="1">
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
                </fieldset>
            </LayoutTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar" SkinID="SkinEditUC" />
                    <asp:ImageButton ID="iBtnDelete" runat="server" CommandName="Delete" SkinID="SkinDeleteUC" Text="Eliminar" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" />
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />--%>
                     <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" ToolTip="Anular Registro" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    Id Sistema</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="358px" Text='<%# Eval("SistSistema") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Eval("SistNombre") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Descripción</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Eval("SistDescripcion") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                        <%--<tr>
                        <td><label>Logo</label></td>
                        <td><telerik:RadTextBox ID="RadTextBox4" Runat="server" Width="358px" 
                                Text='<%# Eval("BlobNombre") %>' Enabled="False"/>                                        
                        </td>
                    </tr>   --%>
                        <tr>
                            <td>
                                <label>
                                    Identificación</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="358px" Text='<%# Eval("SistIdentifica") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <table cellspacing="8">
                    <tr>
                        <td>
                            <label>
                                Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Bind("SistNombre") %>'
                                MaxLength="50" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="RadTextBox1" ErrorMessage="Campo Obligatorio">
                                    <asp:Image ID="Image1" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Descripción</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Bind("SistDescripcion") %>'
                                MaxLength="255" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Icono Logo</label>
                        </td>
                        <%--<td>
                            <uc1:LoadFileBinary ID="LoadFileBinary1" runat="server" ActiveViewIndex="0" IdBlob='<%# Bind("IconLogo") %>' />
                        </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Identificación</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="358px" Text='<%# Bind("SistIdentifica") %>'
                                MaxLength="10" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="RadTextBox3" ErrorMessage="Campo Obligatorio">
                                    <asp:Image ID="Image2" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" />
                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Bind("SistNombre") %>'
                                    MaxLength="50" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="RadTextBox1" ErrorMessage="Campo Obligatorio">
                                        <asp:Image ID="Image2" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Descripción</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Bind("SistDescripcion") %>'
                                    MaxLength="255" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Icono Logo</label>
                            </td>
                            <%--<td>
                                <uc1:LoadFileBinary ID="LoadFileBinary1" runat="server" ActiveViewIndex="0" IdBlob='<%# Bind("IconLogo") %>'
                                    Text='<%# Eval("BlobNombre") %>' />
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Identificación</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="358px" Text='<%# Bind("SistIdentifica") %>'
                                    MaxLength="10" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="RadTextBox3" ErrorMessage="Campo Obligatorio">
                                        <asp:Image ID="Image3" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
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
            <EmptyDataTemplate>
                <%--<uc3:UserControlFind ID="UserControlFind1" Titulo="Sistema"  runat="server" Entidad="AdmiSistema"  DataSourceID="ObjectDataSource1" FilterControl="RadListView11"/>           --%>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Sistemas</h5>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 7px">
                                                <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
                                                <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
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
                                <telerik:RadTextBox ID="txtCodigo" runat="server" Width="358px" MaxLength="255" />
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
    </telerik:RadAjaxPanel>
</asp:Content>
