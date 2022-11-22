<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="CrearOpcion.aspx.cs" Inherits="Administrador.CrearOpcion" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../ControlesUsuario/UserControlAdvancedDropDownList.ascx" TagName="SelectAndText"
    TagPrefix="uc2" %>
<%--<%@ Register src="../ControlesUsuario/UserControlFind.ascx" tagname="UserControlFind" tagprefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" UpdatePanelHeight="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, Select" />
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetList" TypeName="BLL.Administracion.AdmiSistemaBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ods_reportes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetlstReportes" TypeName="BLL.Administracion.AdmiOpcionBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetListByIdClass" TypeName="BLL.Administracion.AdmiParametroBL">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="connection" Type="String" />
            <asp:Parameter DefaultValue="" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
            <asp:Parameter DefaultValue="2" Name="idClass" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadListView ID="RadListView1" runat="server" AllowPaging="True" DataSourceID="ObjectDataSource1"
            ItemPlaceholderID="ItemContainer" PageSize="1" DataKeyNames="OpciOpcion" OnItemCommand="RadListView1_ItemCommand"
            OnItemDataBound="RadListView1_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Opciones</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1" RenderMode="Lightweight"
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
                    <table>
                        <tr>
                            <td>
                                <table cellspacing="8">
                                    <tr>
                                        <td>
                                            <label>
                                                Id Opcion</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox8" runat="server" Width="358px" Text='<%# Eval("OpciOpcion") %>'
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
                                                AutoPostBack="True" DataTextField="SistNombre" DataValueField="SistSistema" CausesValidation="False"
                                                Enabled="false" SelectedValue='<%# Eval("SistSistema") %>'>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Módulo</label>
                                        </td>
                                        <td>
                                            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetListBySystem" TypeName="BLL.Administracion.AdmiModuloBL">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="" Name="connection" Type="String" />
                                                    <asp:Parameter DefaultValue="" Name="filter" Type="String" />
                                                    <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                                                    <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
                                                    <asp:ControlParameter ControlID="RadComboBox1" Name="systemId" PropertyName="SelectedValue"
                                                        Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <telerik:RadComboBox ID="SelectAndText2" runat="server" DataSourceID="ObjectDataSource3"
                                                DataTextField="ModuNombre" DataValueField="ModuModulo" Enabled="false" CausesValidation="False"
                                                SelectedValue='<%# Eval("ModuModulo") %>'>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Nombre</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Eval("OpciNombre") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Etiqueta</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Eval("OpciEtiqueta") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Comando</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="358px" Text='<%# Eval("OpciComando") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Orden</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="358px" Text='<%# Eval("OpciOrden") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Hint</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="358px" Text='<%# Eval("OpciHint") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><label>Ruta</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox6" runat="server" Width="358px" Text='<%# Eval("NombreRuta") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Tipo</label>
                                        </td>
                                        <td>
                                            <uc2:SelectAndText ID="SelectAndText5" runat="server" DataTextField="ParaNombre"
                                                DataValueField="ParaParametro" DataSourceID="ObjectDataSource4" DropDownListWidth="330px"
                                                SelectedValue='<%# Eval("ParaClase2") %>' Enabled="false" />
                                        </td>
                                    </tr>                                   
                                    <tr>
                                        <td>
                                            <label>
                                                Padre</label>
                                        </td>
                                        <td>
                                            <uc2:SelectAndText ID="SelectAndText1" runat="server" DataTextField="OpciNombre"
                                                DataValueField="OpciOpcion" DataSourceID="ObjectDataSource5" DropDownListWidth="330px"
                                                SelectedValue='<%# Eval("OpciPadre") %>' Enabled="false" />
                                            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetListByModulo" TypeName="BLL.Administracion.AdmiOpcionBL"
                                                DataObjectTypeName="DAL.BE.Administracion.AdmiOpcion">
                                                <SelectParameters>
                                                    <asp:Parameter Name="connection" DefaultValue="" Type="String" />
                                                    <asp:Parameter Name="filter" DefaultValue="1=0" Type="String" />
                                                    <asp:Parameter Name="startRowIndex" DefaultValue="0" Type="Int32" />
                                                    <asp:Parameter Name="maximumRows" DefaultValue="0" Type="Int32" />
                                                    <asp:ControlParameter Name="modulo" ControlID="SelectAndText2" PropertyName="SelectedValue"
                                                        Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Principal</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox3" runat="server" TextAlign="Left" Checked='<%# Eval("OpciPrincipal") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Estado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CheckBox11" runat="server" TextAlign="Left" Checked='<%# Eval("OpciEstado") %>'
                                                Enabled="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ctrl_nombre" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ctrl_nombre"
                                        UniqueName="ctrl_nombre" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ctrl_descripcion" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Descripion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ctrl_descripcion"
                                        UniqueName="ctrl_descripcion" >
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster1" runat="server">                    
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    Sistema</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox11" runat="server" DataSourceID="ObjectDataSource2"
                                    AutoPostBack="True" DataTextField="SistNombre" DataValueField="SistSistema" CausesValidation="False"
                                    SelectedValue='<%# Bind("SistSistema") %>'>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Módulo</label>
                            </td>
                            <td>
                                <%--<asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetListBySystem" TypeName="Administracion.AdmiModuloBL">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="" Name="connection" Type="String" />
                                        <asp:Parameter DefaultValue="" Name="filter" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
                                        <asp:ControlParameter ControlID="RadComboBox11" Name="systemId" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <uc2:SelectAndText ID="SelectAndText2" runat="server" DataTextField="ModuNombre"
                                    DataValueField="ModuModulo" DataSourceID="ObjectDataSource3" DropDownListWidth="330px"
                                    SelectedValue='<%# Bind("ModuModulo") %>' Validate="true" />--%>
                                <telerik:RadComboBox ID="SelectAndText2" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Bind("OpciNombre") %>'
                                    MaxLength="50" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadTextBox1"
                                    ErrorMessage="Campo Obligatorio">
                                    <asp:Image ID="Image3" runat="server" SkinID="ImagenError" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Etiqueta</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Bind("OpciEtiqueta") %>'
                                    MaxLength="100" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadTextBox2"
                                    ErrorMessage="Campo Obligatorio">
                                    <asp:Image ID="Image1" runat="server" SkinID="ImagenError" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Comando</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="358px" Text='<%# Bind("OpciComando") %>'
                                    MaxLength="255" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Orden</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="rtxtOrden" runat="server" 
                                    MinValue="0" Text='<%# Bind("OpciOrden") %>'>
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rtxtOrden"
                                    ErrorMessage="Campo Obligatorio">
                                    <asp:Image ID="Image2" runat="server" SkinID="ImagenError" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Hint</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="358px" Text='<%# Bind("OpciHint") %>'
                                    MaxLength="100" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tipo</label>
                            </td>
                            <td>
                                <%--<uc2:SelectAndText ID="SelectAndText5" runat="server" DataTextField="ParaNombre"
                                    DataValueField="ParaParametro" DataSourceID="ObjectDataSource4" DropDownListWidth="330px"
                                    SelectedValue='<%# Bind("ParaClase2") %>' Validate="true" />--%>
                                <telerik:RadComboBox ID="SelectAndText5" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="ObjectDataSource4" DataTextField="ParaNombre" SelectedValue='<%# Bind("ParaClase2") %>'
                                    DataValueField="ParaParametro" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>    
                        <tr>
                            <td>
                               <telerik:RadTextBox ID="txt_formulario" runat="server" Width="358px" Text='<%# Eval("formformulario") %>' Visible="false" /> 
                            </td>
                        </tr>                                
                        <%--<tr>
                            <td>
                                <label>
                                    Padre</label>
                            </td>
                            <td>
                                <uc2:SelectAndText ID="SelectAndText41" runat="server" DataTextField="OpciNombre"
                                    DataValueField="OpciOpcion" DataSourceID="ObjectDataSource5" DropDownListWidth="330px"
                                    SelectedValue='<%# Bind("OpciPadre") %>' />
                                <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetListByModulo" TypeName="BLL.Administracion.AdmiOpcionBL"
                                    DataObjectTypeName="DAL.BE.Administracion.AdmiOpcion">
                                    <SelectParameters>
                                        <asp:Parameter Name="connection" DefaultValue="" Type="String" />
                                        <asp:Parameter Name="filter" DefaultValue="1=0" Type="String" />
                                        <asp:Parameter Name="startRowIndex" DefaultValue="0" Type="Int32" />
                                        <asp:Parameter Name="maximumRows" DefaultValue="0" Type="Int32" />
                                        <asp:ControlParameter Name="modulo" ControlID="SelectAndText2" PropertyName="SelectedValue"
                                            Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <td>
                        </tr>--%>
                        <tr>
                            <td>
                                <span>Principal</span>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" TextAlign="Left" Checked='<%# Bind("OpciPrincipal") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckBox11" runat="server" TextAlign="Left" Checked='<%# Bind("OpciEstado") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>                    
                    <div>
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ctrl_nombre" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ctrl_nombre"
                                        UniqueName="ctrl_nombre" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ctrl_descripcion" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Descripion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ctrl_descripcion"
                                        UniqueName="ctrl_descripcion" >
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>
                                                            Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>Nombre</label>
                                                    </td>
                                                    <td>
                                                    <telerik:RadTextBox ID="txt_npermiso" runat="server" Width="300px" MaxLength="50" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Descripcion</label>
                                                    </td>
                                                    <td>
                                                            <telerik:RadTextBox ID="txt_nespcricion" runat="server" Width="300px"  />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                     <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                        ValidationGroup="grNuevo"  SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
                                                    <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                        CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </asp:Panel>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table cellspacing="8">
                                <tr>
                                    <td>
                                        <label>
                                            Sistema</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="RadComboBox11" runat="server" DataSourceID="ObjectDataSource2"
                                            AutoPostBack="True" DataTextField="SistNombre" DataValueField="SistSistema" CausesValidation="False"
                                            SelectedValue='<%# Bind("SistSistema") %>'>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Módulo</label>
                                    </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                                                                    SelectMethod="GetListBySystem" TypeName="BLL.Administracion.AdmiModuloBL">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="" Name="connection" Type="String" />
                                                                        <asp:Parameter DefaultValue="" Name="filter" Type="String" />
                                                                        <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
                                                                        <asp:ControlParameter ControlID="RadComboBox11" Name="systemId" PropertyName="SelectedValue"
                                                                            Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <uc2:SelectAndText ID="SelectAndText2" runat="server" DataTextField="ModuNombre" 
                                                                    DataValueField="ModuModulo" DataSourceID="ObjectDataSource3" DropDownListWidth="330px"
                                                                    SelectedValue='<%# Bind("ModuModulo") %>' Validate="true" ValidationGroup="Opcion" />
                                                            </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Nombre</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox1" runat="server" Width="358px" Text='<%# Bind("OpciNombre") %>'
                                            MaxLength="50" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadTextBox1"
                                            ErrorMessage="Campo Obligatorio" ValidationGroup="Opcion">
                                            <asp:Image ID="Image4" runat="server" SkinID="ImagenError" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Etiqueta</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox2" runat="server" Width="358px" Text='<%# Bind("OpciEtiqueta") %>'
                                            MaxLength="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="RadTextBox2"
                                            ErrorMessage="Campo Obligatorio" ValidationGroup="Opcion">
                                            <asp:Image ID="Image5" runat="server" SkinID="ImagenError" />
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Comando</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="358px" Text='<%# Bind("OpciComando") %>'
                                            MaxLength="255" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Orden</label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="rtxtOrden" runat="server" 
                                            MinValue="0" Text='<%# Bind("OpciOrden") %>'>
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Opcion"
                                            ControlToValidate="rtxtOrden" ErrorMessage="Campo Obligatorio">
                                            <asp:Image ID="Image2" runat="server" SkinID="ImagenError" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Hint</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBox5" runat="server" Width="358px" Text='<%# Bind("OpciHint") %>'
                                            MaxLength="100" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Reporte</label></td>
                                    <td>    
                                        <telerik:RadComboBox ID="RadComboBox3" runat="server" 
                                            DataSourceID="ods_reportes" AutoPostBack="true" 
                                             DataTextField="Nombre" DataValueField="RR_CODIGO" SelectedValue='<%# Bind("opciReporte") %>'
                                            CausesValidation="False" Width="355px" AppendDataBoundItems="true" 
                                            onselectedindexchanged="RadComboBox3_SelectedIndexChanged">
                                             <Items>
                                                <telerik:RadComboBoxItem Value="" Text="Selecionar" />
                                             </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                        <td><label>Ruta</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="RadTextBox6" runat="server" Width="358px" Text='<%# Bind("NombreRuta") %>'
                                                Enabled="true" />
                                        </td>
                                    </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Tipo</label>
                                    </td>
                                    <td>
                                        <uc2:SelectAndText ID="SelectAndText5" runat="server" DataTextField="ParaNombre"
                                            DataValueField="ParaParametro" DataSourceID="ObjectDataSource4" DropDownListWidth="330px"
                                            SelectedValue='<%# Bind("ParaClase2") %>' Validate="true" ValidationGroup="Opcion" />
                                    </td>
                                </tr>                                
                                <tr>
                                    <td>
                                        <label>
                                            Padre</label>
                                    </td>
                                    <td>
                                        <uc2:SelectAndText ID="SelectAndText1" runat="server" DataTextField="OpciNombre"
                                            DataValueField="OpciOpcion" DataSourceID="ObjectDataSource5" DropDownListWidth="330px"
                                            SelectedValue='<%# Bind("OpciPadre") %>' />
                                        <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetListByModulo" TypeName="BLL.Administracion.AdmiOpcionBL"
                                            DataObjectTypeName="DAL.BE.Administracion.AdmiOpcion">
                                            <SelectParameters>
                                                <asp:Parameter Name="connection" DefaultValue="" Type="String" />
                                                <asp:Parameter Name="filter" DefaultValue="1=0" Type="String" />
                                                <asp:Parameter Name="startRowIndex" DefaultValue="0" Type="Int32" />
                                                <asp:Parameter Name="maximumRows" DefaultValue="0" Type="Int32" />
                                                <asp:ControlParameter Name="modulo" ControlID="SelectAndText2" PropertyName="SelectedValue"
                                                    Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Principal</span>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox2" runat="server" TextAlign="Left" Checked='<%# Bind("OpciPrincipal") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Estado</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="CheckBox11" runat="server" TextAlign="Left" Checked='<%# Bind("OpciEstado") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                            ValidationGroup="Opcion" />
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>                        
            </InsertItemTemplate>
            <EmptyDataTemplate>                
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Opciones</h5>
                        </div>
                    </div>
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                    <tr>
                                        <td>
                                            <div style="padding-top: 7px">                                                
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
                                    AutoPostBack="True" DataTextField="SistNombre" DataValueField="SistSistema" CausesValidation="False"
                                    Enabled="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Módulo</label>
                            </td>
                            <td>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetListBySystem" TypeName="BLL.Administracion.AdmiModuloBL">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="" Name="connection" Type="String" />
                                        <asp:Parameter DefaultValue="" Name="filter" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="maximumRows" Type="Int32" />
                                        <asp:ControlParameter ControlID="cb_sistema" Name="systemId" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <telerik:RadComboBox ID="cb_modulo" runat="server" DataSourceID="ObjectDataSource3" AppendDataBoundItems="true"
                                    DataTextField="ModuNombre" DataValueField="ModuModulo" Enabled="true" CausesValidation="False">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button9" runat="server" OnClick="BuscarGrilla" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                </td> </tr> </table>
                <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
            </EmptyDataTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetList" TypeName="BLL.Administracion.AdmiOpcionBL" DataObjectTypeName="BE.Administracion.AdmiOpcion"
        InsertMethod="Insert" DeleteMethod="Delete" UpdateMethod="Update" OnInserted="ObjectDataSource1_Inserted">
        <SelectParameters>
            <asp:Parameter Name="connection" DefaultValue="" Type="String" />
            <asp:Parameter Name="filter" DefaultValue="1=0" Type="String" />
            <asp:Parameter Name="startRowIndex" DefaultValue="0" Type="Int32" />
            <asp:Parameter Name="maximumRows" DefaultValue="0" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_detalle" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetControles" TypeName="BLL.Administracion.AdmiControlBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" DefaultValue="" Type="String" />
            <asp:Parameter Name="opci_opcion" DefaultValue="0" Type="Int32" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
