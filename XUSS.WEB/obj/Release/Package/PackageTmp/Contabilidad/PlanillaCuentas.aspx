<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PlanillaCuentas.aspx.cs" Inherits="XUSS.WEB.Contabilidad.PlanillaCuentas" ClientIDMode="Static" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
        var timer = null;

        function clientLoad(sender) {
            //debugger;
            $telerik.$(".riTextBox", sender.get_element().parentNode).bind("keydown", valueChanging);
        }

        function valueChanging(sender, args) {
            //debugger;
            if (timer) {
                clearTimeout(timer);
            }

            timer = setTimeout(function () {                
                var tree = $find("<%= tv_puc.ClientID %>");
                var textbox = $find("<%= txt_filtro.ClientID %>");
                var searchString = textbox.get_element().value;

                for (var i = 0; i < tree.get_nodes().get_count(); i++) {
                    findNodes(tree.get_nodes().getNode(i), "AC");
                }
            }, 200);
        }

        function findNodes(node, searchString) {
            //debugger;
            node.set_expanded(true);

            var hasFoundChildren = false;
            for (var i = 0; i < node.get_nodes().get_count(); i++) {
                hasFoundChildren = findNodes(node.get_nodes().getNode(i), searchString) || hasFoundChildren;
            }

            if (hasFoundChildren || node.get_text().toLowerCase().indexOf(searchString.toLowerCase()) != -1) {
                node.set_visible(true);
                return true;
            }
            else {
                node.set_visible(false);
                return false;
            }
        }

        function OnClientEntryAddingHandler(sender, eventArgs) {
            if (sender.get_entries().get_count() > 0) {
                eventArgs.set_cancel(true);
            }
        }
    </script>
    </telerik:RadScriptBlock>
    <style type="text/css">
        .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6 {
            padding: 0px 10px 10px 30px;
        }
        /*.RadWindow_Bootstrap .rwTitleBar {
                border-color: #25A0DA;
                color: #333;
                background-color: #25A0DA;
                margin: 0;
                border-radius: 4px 4px 0 0;
            }*/
    </style>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Plan de Cuentas (PUC)</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <asp:Panel runat="server" ID="Panel1">
                <table>
                    <tr>                        
                        <td><label>Cuenta Contable</label></td>
                        <td>
                           <telerik:RadTextBox ID="txt_filtro" runat="server" Enabled="true" ClientEvents-OnLoad="clientLoad" ClientIDMode="Static" >
                           </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            
                <telerik:RadTreeView RenderMode="Lightweight" runat="server" ID="tv_puc" DataSourceID="obj_consulta" OnContextMenuItemClick="tv_puc_ContextMenuItemClick" ExpandAnimation-Type="None" CollapseAnimation-Type="None"
                    DataFieldID="PC_ID" DataFieldParentID="PC_PARENT" OnNodeDataBound="tv_puc_NodeDataBound" OnDataBound="tv_puc_DataBound" Skin="Bootstrap">
                    <ContextMenus>
                        <telerik:RadTreeViewContextMenu ID="MainContextMenu" runat="server" RenderMode="Lightweight">
                            <Items>
                                <telerik:RadMenuItem Value="Editar" Text="Editar" Enabled="true">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Nuevo" Text="Nuevo">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="Inactivar" Text="Inactivar/Eliminar">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem IsSeparator="true">
                                </telerik:RadMenuItem>
                                <telerik:RadMenuItem Value="MarkAsRead" Text="Expandir Todo">
                                </telerik:RadMenuItem>
                            </Items>
                        </telerik:RadTreeViewContextMenu>
                    </ContextMenus>
                    <DataBindings>
                        <telerik:RadTreeNodeBinding TextField="PC_NOMBRE" ValueField="PC_ID"></telerik:RadTreeNodeBinding>
                        <telerik:RadTreeNodeBinding Depth="0" TextField="PC_NOMBRE" Expanded="true" CssClass="rootNode"></telerik:RadTreeNodeBinding>
                    </DataBindings>

                </telerik:RadTreeView>
            </asp:Panel>
        </div>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpDetalle" runat="server" Width="850px" Height="250px" Modal="true" OffsetElementID="main" Title="Datos Cuentas" EnableShadow="true" Style="z-index: 100001;">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Nro Cuenta</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Width="300px" Visible="false">
                                    </telerik:RadTextBox>
                                    <telerik:RadTextBox ID="txt_nrocta" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nrocta"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Descripcion</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_catidad1" runat="server" ControlToValidate="txt_nombre"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Naturaleza</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_naturaleza" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_naturaleza" DataTextField="TTDESCRI"
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_naturaleza" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Tipo</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_tipo" DataTextField="TTDESCRI"
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tipo" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar" runat="server" Text="Aceptar" Icon-PrimaryIconCssClass="rbSave" OnClick="btn_aceptar_Click" CommandName="Cancel" ValidationGroup="grNuevoI" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                    <ContentTemplate>
                        <div style="padding: 5px 5px 5px 5px">
                            <ul>
                                <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                            </ul>
                            <div style="text-align: center;">
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_naturaleza" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="NATUR" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TIPOCT" />
        </SelectParameters>
    </asp:ObjectDataSource>    
</asp:Content>
