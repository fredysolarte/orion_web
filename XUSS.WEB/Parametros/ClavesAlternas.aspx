<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ClavesAlternas.aspx.cs" Inherits="XUSS.WEB.Parametros.ClavesAlternas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
         <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Claves Alternas</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_buscar">
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>TP</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="obj_tippro" Width="300px" CheckBoxes="true"
                                    DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Descripcion
                                </label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px" >
                                </telerik:RadTextBox> 
                            </td>
                            <td>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_Click" Icon-PrimaryIconCssClass="rbSearch" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </fieldset>
        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_claves" OnItemCommand="rg_items_ItemCommand"
            Culture="(Default)" CellSpacing="0" ShowFooter="True" AllowPaging="true" PageSize="50" AllowFilteringByColumn="true">
            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" >
            <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                    ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" />     
                    <telerik:GridButtonColumn  ButtonType="ImageButton" CommandName="Edit"  HeaderStyle-Width="20px" />                    
                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                        UniqueName="TANOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                    
                    <telerik:GridBoundColumn DataField="ASNIVELC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Nivel" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNIVELC"
                        UniqueName="ASNIVELC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NOM_NIVEL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="N.Nivel" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOM_NIVEL"
                        UniqueName="NOM_NIVEL">                        
                    </telerik:GridBoundColumn>                    
                    <telerik:GridBoundColumn DataField="ASCLAVEO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASCLAVEO"
                        UniqueName="ASCLAVEO">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                        UniqueName="ASNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                    
                    <telerik:GridBoundColumn DataField="ASESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASESTADO"
                        UniqueName="ASESTADO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                    
                </Columns>
                <EditFormSettings EditFormType="Template">
                    <FormTemplate>
                        <table>
                            <tr>
                                <td><label>Linea</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_categoria" runat="server" OnSelectedIndexChanged="rc_categoria_SelectedIndexChanged" SelectedValue='<%# Bind("ASTIPPRO") %>'
                                        Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE" AutoPostBack="true" CheckBoxes="true"
                                        Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_categoria" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Nivel</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nivelc" runat="server" Enabled="false" Width="300px" Visible="false" Text='<%# Bind("ASNIVELC") %>'>
                                    </telerik:RadTextBox> 
                                    <telerik:RadComboBox ID="rc_nivel" runat="server" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                        Culture="es-CO" Width="300px" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_nivel" InitialValue="Seleccionar" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Codigo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' Width="300px" Text='<%# Bind("ASCLAVEO") %>'>
                                    </telerik:RadTextBox> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_codigo" 
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                                <td><label>Nombre</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px" Text='<%# Bind("ASNOMBRE") %>'>
                                    </telerik:RadTextBox> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nombre" 
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                        ValidationGroup="gvInsert" SkinID="SkinUpdateUC" />
                                    <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </FormTemplate>
                </EditFormSettings>
                <NoRecordsTemplate>
                    <div class="alert alert-danger">
                        <strong>¡No se Encontaron Registros!</strong>
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_claves" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetClavesAlternas"  TypeName="XUSS.BLL.Parametros.ClavesAlternasBL" UpdateMethod="UpdateBodega">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String"  />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
