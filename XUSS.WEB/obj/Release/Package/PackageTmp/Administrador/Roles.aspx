<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Administrador.Roles.Roles" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register src="../../UserControls/UserControlFind.ascx" tagname="UserControlFind" tagprefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function TestClick(sender, args) {
            console.log('entra');
            //debugger;
            if (args.get_node().get_checked()) {
                if (args.get_node().get_parent() != null) {
                    setPadre(args.get_node().get_parent());
                }
            } else {
                setHijos(args.get_node());
            }
        }

        function setHijos(nodo) {
            console.log('entra hijos');
            //debugger;
            var i = 0;
            var hijos = nodo.get_allNodes();
            for (i = 0; i < hijos.length; i++) {
                hijos[i].set_checked(false);
            }
        }

        function setPadre(nodo) {
            //debugger;
            console.log('entra padre');
            if (nodo._parent != undefined) {
                console.log('entra padre 1');
                if (!nodo.get_checked()) {
                    console.log('entra padre 2');
                    nodo.set_checked(true);
                    if (nodo.get_parent() != null) {
                        setPadre(nodo.get_parent());
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadListView ID="RadListView1" runat="server" PageSize="1" AllowPaging="True"
            ItemPlaceholderID="ItemContainer" DataSourceID="odsRoles" DataKeyNames="RolmRolm"
            OnItemDataBound="RadListView1_ItemDataBound"
            OnItemCommand="RadListView1_ItemCommand">

            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Roles</h5>
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
                    <table border="0" cellspacing="8">
                        <tr>
                            <td>
                                <label>Id</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="TextBox1" runat="server" Text='<%# Bind("RolmRolm") %>' MaxLength="30" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="TextBox2" runat="server" Text='<%# Bind("RolmNombre") %>' MaxLength="100" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripción</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="TextBox3" runat="server" Text='<%# Bind("RolmDescripcion") %>' MaxLength="255" Width="358px" Enabled="false">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("RolmEstado") %>' Enabled="false" />
                            </td>
                        </tr>
                    </table>   
                    <%--<telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True"
                        RenderMode="Lightweight" Culture="(Default)" CellSpacing="0" AllowFilteringByColumn="true" 
                        DataSourceID="obj_consulta" ShowFooter="true" Height="650px" >
                        <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                            <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                        </ClientSettings>
                        <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" >
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>      --%>           
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <table border="0" cellspacing="8">
                    <tr>
                        <td>
                            <label>Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="TextBox2" runat="server" Text='<%# Bind("RolmNombre") %>' MaxLength="100" Width="358px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="rfvTextBox2" runat="server"
                                ControlToValidate="TextBox2" Display="Dynamic"
                                SetFocusOnError="True">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Descripción</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="TextBox3" runat="server" Text='<%# Bind("RolmDescripcion") %>' Width="358px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Estado</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("RolmEstado") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" CommandName="Update" Text="Aceptar" />
                            <asp:Button ID="Button2" runat="server" CommandName="Cancel" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>

                <table border="0" cellspacing="8">
                    <tr>
                        <td>
                            <label>Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="TextBox2" runat="server" Text='<%# Bind("RolmNombre") %>' MaxLength="100" Width="358px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="rfvTextBox2" runat="server"
                                ControlToValidate="TextBox2" Display="Dynamic"
                                SetFocusOnError="True">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" ToolTip="Campo requerido" />
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Descripción</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="TextBox3" runat="server" Text='<%# Bind("RolmDescripcion") %>' MaxLength="255" Width="358px" TextMode="MultiLine">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Estado</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("RolmEstado") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" CommandName="PerformInsert"
                                Text="Aceptar" />
                            <asp:Button ID="Button2" runat="server" CommandName="Cancel" Text="Cancelar" />
                        </td>
                    </tr>
                </table>

            </InsertItemTemplate>
            <EmptyDataTemplate>
                <%--<uc3:UserControlFind ID="UserControlFind1" runat="server" Titulo="Rol" Entidad="AdmiRol"  DataSourceID="odsRoles" FilterControl="RadListView1"/>   --%>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Roles</h5>
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
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" MaxLength="30" Width="358px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" MaxLength="100" Width="358px">
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
                <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
            </EmptyDataTemplate>
        </telerik:RadListView>
        <asp:Panel ID="pnDetails" runat="server">
            <fieldset class="cssFieldSetContainer">
                <div class="box">
                    <div class="title">
                        <h5>Configuración</h5>
                    </div>
                </div>
                <table style="width: 400px;">
                    <tr>
                        <td>
                            <label>Sistema</label>
                            <asp:DropDownList ID="ddlSistema" runat="server" AutoPostBack="true"
                                DataTextField="SistNombre" DataValueField="SistSistema"
                                OnSelectedIndexChanged="ddlSistema_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <label>Módulo</label>
                            <asp:DropDownList ID="ddlModulo" runat="server" AutoPostBack="true"
                                DataTextField="ModuNombre" DataValueField="ModuModulo"
                                OnSelectedIndexChanged="ddlModulo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <fieldset style="width: 100%">
                                <legend>Permisos</legend>
                                <telerik:RadTreeView ID="RadTreeView1" runat="server" CheckBoxes="true"
                                    DataTextField="AropNombre"
                                    DataFieldID="AropArbolOpcion" DataFieldParentID="AropIdUnicoPadre" Enabled="false"
                                    DataValueField="AropIdOriginal"
                                    OnNodeDataBound="RadTreeView1_NodeDataBound"
                                    OnNodeCheck="RadTreeView1_NodeCheck" EnableViewState="true"
                                    OnClientNodeChecked="TestClick" Height="300px">
                                    <DataBindings>
                                        <telerik:RadTreeNodeBinding Expanded="True" CategoryField="AropEntidad" />
                                    </DataBindings>
                                </telerik:RadTreeView>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="odsRoles" runat="server"
        SelectMethod="GetList"
        InsertMethod="Insert"
        TypeName="BLL.Administracion.AdmiRolBL"
        DataObjectTypeName="BE.Administracion.AdmiRol"
        EnablePaging="True"
        UpdateMethod="Update"
        DeleteMethod="Delete" OnInserted="odsRoles_Inserted"
        OnUpdated="odsRoles_Updated">
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
