<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MaestroDocumentos.aspx.cs" Inherits="XUSS.WEB.Contabilidad.MaestroDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
             />
    <telerik:RadListView ID="rvl_documentos" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rvl_documentos_OnItemUpdating"
            ItemPlaceholderID="pnlGeneral" DataSourceID="obj_documentos" OnItemCommand="rlv_documentos_OnItemCommand" OnItemDataBound="rlv_documentos_ItemDataBound" >
            <layouttemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Maestro Documentos Contables</h5>
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
            </layouttemplate>
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Maestro Documentos Contables</h5>
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
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" runat="server">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
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
                    <asp:ImageButton ID="iBtnEdit" runat="server" CommandName="Edit" Text="Modificar" SkinID="SkinEditUC" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" Text='<%# Bind("DOC_IDENTI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>    
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" Text='<%# Bind("DOC_NOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>    
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Consecutivo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_conse" Text='<%# Bind("DOC_CONSE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Auto Incremental</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_auto" runat="server" Checked='<%# this.GetValCheck(Eval("DOC_AINC")) %>' Enabled="false"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Clase</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_clase" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_clase" SelectedValue='<%# Bind("DOC_CLASE") %>' DataTextField="TTVALORC"
                                    DataValueField="TTCODCLA" Enabled="false">
                                </telerik:RadComboBox>
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
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" Text='<%# Bind("DOC_IDENTI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="edt_codigo"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" Text='<%# Bind("DOC_NOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edt_nombre"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Consecutivo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_conse" Text='<%# Bind("DOC_CONSE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_conse"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Auto Incremental</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_auto" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Clase</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_clase" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_clase" SelectedValue='<%# Bind("DOC_CLASE") %>' DataTextField="TTVALORC"
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="Seleccionar" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rcb_clase" InitialValue="Seleccionar"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
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
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_codigo" Text='<%# Bind("DOC_IDENTI") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="false">
                                </telerik:RadTextBox>    
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="edt_codigo"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" Text='<%# Bind("DOC_NOMBRE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                </telerik:RadTextBox>   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edt_nombre"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Consecutivo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_conse" Text='<%# Bind("DOC_CONSE") %>' runat="server"
                                    Culture="es-CO" Width="358px" Enabled="true">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_conse"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Clase</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_clase" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_clase" SelectedValue='<%# Bind("DOC_CLASE") %>' DataTextField="TTVALORC"
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="Seleccionar" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rcb_clase" InitialValue="Seleccionar"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Auto Incremental</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_auto" runat="server" Checked='<%# this.GetValCheck(Eval("DOC_AINC")) %>' />
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
    </telerik:RadListView>
</telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_documentos" runat="server" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateDocumento"
        SelectMethod="GetDocumentos" InsertMethod="InsertDocumento" TypeName="XUSS.BLL.Contabilidad.MaestroDocumentosBL"
        OnInserting="obj_documentos_Inserting" OnUpdating="obj_documentos_Updating" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />            
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String"/>
            <asp:SessionParameter Name="DOC_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="DOC_IDENTI" Type="String" />
            <asp:Parameter Name="DOC_NOMBRE" Type="String" />
            <asp:Parameter Name="DOC_CONSE" Type="Int32" />
            <asp:Parameter Name="DOC_CLASE" Type="String" />            
            <asp:SessionParameter Name="DOC_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="DOC_ESTADO" Type="String" DefaultValue="AC"/>       
            <asp:Parameter Name="DOC_AINC" Type="String" DefaultValue="N"/>              
        </InsertParameters>
        <UpdateParameters>
        <asp:Parameter Name="connection" Type="String"/>
            <asp:SessionParameter Name="DOC_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="DOC_IDENTI" Type="String" />
            <asp:Parameter Name="DOC_NOMBRE" Type="String" />
            <asp:Parameter Name="DOC_CONSE" Type="Int32" />
            <asp:Parameter Name="DOC_CLASE" Type="String" />            
            <asp:SessionParameter Name="DOC_USUARIO" Type="String" SessionField="UserLogon" />            
            <asp:Parameter Name="DOC_AINC" Type="String" DefaultValue="N"/>
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_clase" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="CLASDOC" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
