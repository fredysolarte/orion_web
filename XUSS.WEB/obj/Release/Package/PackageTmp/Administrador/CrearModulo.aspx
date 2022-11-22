<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CrearModulo.aspx.cs" Inherits="Administrador.CrearModulo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="../ControlesUsuario/LoadFileBinary.ascx" TagName="LoadFileBinary"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, Select" />
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
        SelectMethod="GetList" TypeName="BLL.Administracion.AdmiModuloBL" DataObjectTypeName="BE.Administracion.AdmiModulo"
        DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetList" TypeName="BLL.Administracion.AdmiSistemaBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadListView ID="RadListView11" runat="server" DataSourceID="ObjectDataSource1"
            ItemPlaceholderID="ItemContainer" PageSize="1" AllowPaging="True" DataKeyNames="ModuModulo,SistSistema"
            OnItemCommand="RadListView11_ItemCommand" OnItemDataBound="RadListView11_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Módulos</h5>
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
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar"  SkinID="SkinEditUC" />
                    <asp:ImageButton ID="iBtnDelete" runat="server" CommandName="Delete" SkinID="SkinDeleteUC" Text="Eliminar" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" />
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClick="return confirm('¿confirma que desea eliminar el registro?')" ToolTip="Anular Registro" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <table cellspacing="8">
                                    <tr>
                                        <td>
                                            <label>
                                                Id Modulo</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Eval("ModuModulo") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Sistema</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="ObjectDataSource2"
                                                DataTextField="SistNombre" DataValueField="SistSistema" Enabled="False" SelectedValue='<%# Eval("SistSistema") %>'>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Nombre</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox11" runat="server" Width="358px" Text='<%# Eval("ModuNombre") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Descripción</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox21" runat="server" Width="358px" Text='<%# Eval("ModuDescripcion") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        <label>Formulario</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_formulario" runat="server" Width="358px" Text='<%# Bind("Formulario") %>' Enabled="false"  />
                                    </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <label>
                                                Ayuda</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox41" runat="server" Width="358px" Text='<%# Eval("NombreAyuda") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <label>
                                                Módulos Parámetros</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox31" runat="server" Width="358px" Text='<%# Eval("ModuParametros") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <label>
                                                Icono</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox51" runat="server" Width="358px" Text='<%# Eval("NombreIcono") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <label>
                                                Orden</label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtOrden" runat="server" Width="358px" DbValue='<%# Eval("ModuOrden") %>'
                                                Enabled="False" NumberFormat-DecimalDigits="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Estado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox11" runat="server" TextAlign="Left" Checked='<%# Eval("ModuEstado") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <table cellspacing="8">
                                    <tr>
                                        <td>
                                            <label>
                                                Sistema
                                            </label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="ObjectDataSource2"
                                                DataTextField="SistNombre" DataValueField="SistSistema" SelectedValue='<%# Bind("SistSistema") %>'>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Nombre</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox12" runat="server" Width="358px" Text='<%# Bind("ModuNombre") %>'
                                                MaxLength="50" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="RadTextBox12" ErrorMessage="Campo Obligatorio">
                                                    <asp:Image ID="Image3" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Descripción</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox22" runat="server" Width="358px" Text='<%# Bind("ModuDescripcion") %>'
                                                MaxLength="255" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <label>
                                                Ayuda</label>
                                        </td>
                                        <td>
                                            <uc1:LoadFileBinary ID="LoadFileBinary12" runat="server" ActiveViewIndex="2" IdBlob='<%# Bind("BlobAyuda") %>'
                                                Text='<%# Eval("NombreAyuda") %>' />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <label>
                                                Módulos Parámetros</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox32" runat="server" Width="358px" Text='<%# Bind("ModuParametros") %>'
                                                MaxLength="255" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td>
                                            <label>
                                                Icono</label>
                                        </td>
                                        <td>
                                            <uc1:LoadFileBinary ID="LoadFileBinary22" runat="server" ActiveViewIndex="0" IdBlob='<%# Bind("IconIcono") %>'
                                                Text='<%# Eval("NombreIcono") %>' />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <label>
                                                Orden</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtOrden" runat="server" Width="358px" Text='<%# Bind("ModuOrden") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Estado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox21" runat="server" TextAlign="Left" Checked='<%# Bind("ModuEstado") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" />
                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td>
                            <table cellspacing="8">
                                <tr>
                                    <td>
                                        <label>
                                            Sistema
                                        </label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="ObjectDataSource2"
                                            DataTextField="SistNombre" DataValueField="SistSistema" SelectedValue='<%# Bind("SistSistema") %>'>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Nombre</label>
                                    </td>
                                    <td style="width: 400px">
                                        <telerik:RadTextBox ID="RadTextBox13" runat="server" Width="358px" Text='<%# Bind("ModuNombre") %>'
                                            MaxLength="50" /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="RadTextBox13" ErrorMessage="Campo Obligatorio">
                                                <asp:Image ID="Image3" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Descripción</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox23" runat="server" Width="358px" Text='<%# Bind("ModuDescripcion") %>'
                                            MaxLength="255" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Formulario</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_formulario" runat="server" Width="358px" Text='<%# Bind("Formulario") %>' />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <label>
                                            Ayuda</label>
                                    </td>
                                    <td>
                                        <uc1:LoadFileBinary ID="LoadFileBinary13" runat="server" ActiveViewIndex="2" IdBlob='<%# Bind("BlobAyuda") %>' />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <label>
                                            Módulos Parámetros</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox33" runat="server" Width="358px" Text='<%# Bind("ModuParametros") %>'
                                            MaxLength="255" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>
                                        <label>
                                            Icono</label>
                                    </td>
                                    <td>
                                        <uc1:LoadFileBinary ID="LoadFileBinary23" runat="server" ActiveViewIndex="0" IdBlob='<%# Bind("IconIcono") %>' />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <label>
                                            Orden</label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="txtOrden" runat="server" Width="358px" DataType="System.Int32?"
                                            DbValue='<%# Bind("ModuOrden") %>' NumberFormat-DecimalDigits="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Estado</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox13" runat="server" TextAlign="Left" Checked='<%# Bind("ModuEstado") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" />
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EmptyDataTemplate>
                <%--<uc3:UserControlFind ID="UserControlFind1" runat="server" Titulo="Modulo" Entidad="AdmiModulo"  DataSourceID="ObjectDataSource1" FilterControl="RadListView11"/>   --%>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Modulos</h5>
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
                            <td>
                                <label>
                                    Sistema</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cb_sistema" runat="server" DataSourceID="ObjectDataSource2"
                                    DataTextField="SistNombre" DataValueField="SistSistema" Enabled="true">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Id Modulo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtModulo" runat="server" Width="358px" Enabled="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txtNombre" runat="server" Width="358px" Enabled="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
</asp:Content>
