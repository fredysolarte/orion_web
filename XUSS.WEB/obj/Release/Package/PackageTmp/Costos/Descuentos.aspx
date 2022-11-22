<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Descuentos.aspx.cs" Inherits="XUSS.WEB.Costos.Descuentos" %>

<%@ Register Src="../ControlesUsuario/ctrl_gesArticulosHor.ascx" TagPrefix="uc1"
    TagName="ctrlArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
            Skin="Web20" />
        <telerik:RadListView ID="rvl_descuento" runat="server" PageSize="10" DataSourceID="obj_descuentos"
            AllowPaging="True" ItemPlaceholderID="pnlGeneral" OnItemCommand="rvl_descuento_ItemCommand" OnItemDataBound="rvl_descuento_ItemDataBound">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Descuento Articulos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rvl_descuento"
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
                                Descuento Articulos</h5>
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
                    <table cellspacing="8">
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_almacen" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_almacenes" DataTextField="BDNOMBRE" DataValueField="BDBODEGA"
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="." Text="Todos" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                  </table>
                  <div>
                        <uc1:ctrlArticulos ID="ctrlArticulos1" runat="server" />
                    </div>
                  <table>
                        <tr>
                            <td>
                                <asp:Button ID="btn_buscar" runat="server" OnClick="btn_buscar_OnClick" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>                
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <div>
                        <uc1:ctrlArticulos ID="ctrlArticulos1" runat="server" />
                    </div>
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rcb_almacen" runat="server" Culture="es-CO" Width="358px"
                                    DataSourceID="obj_almacenes" SelectedValue='<%# Bind("BODEGA") %>' DataTextField="BDNOMBRE"
                                    DataValueField="BDBODEGA">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-Todos-" Value="." />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    T Descuento</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rcb_tipdes" runat="server" Culture="es-CO" Width="358px"
                                    AutoPostBack="true" OnSelectedIndexChanged="rcb_tipdes_SelectedIndexChanged"
                                    DataSourceID="obj_tdescuento" DataTextField="NOMBRE" DataValueField="ID" AppendDataBoundItems="true"
                                    SelectedValue='<%# Bind("ID_DESCUENTO") %>'>
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-Seleccione-" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Ins"
                                    ControlToValidate="rcb_tipdes" ErrorMessage="(*)" InitialValue="0">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Vlr Descuento</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadNumericTextBox ID="edt_valor" runat="server" Culture="es-CO" Width="358px"
                                    Text='<%# Bind("VALOR") %>' MaxValue="100" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="edt_valor"
                                    ValidationGroup="Ins" ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Inicial</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>'>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="edt_fecini"
                                    ValidationGroup="Ins" ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>
                                    F. Final</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecfin" runat="server" SelectedDate='<%# Bind("FECHAFIN") %>'>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="edt_fecfin"
                                    ValidationGroup="Ins" ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                    ValidationGroup="Ins" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>
                                Editar</label>
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">                    
                    <asp:ImageButton ID="IBtnFind" runat="server" CommandName="Buscar" SkinID="SkinFindUC" />
                    <asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />                    
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Almacen</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_almacen" runat="server" Culture="es-CO" Width="230px" Enabled="false"
                                    DataSourceID="obj_almacenes" SelectedValue='<%# Bind("BODEGA") %>' DataTextField="BDNOMBRE"
                                    DataValueField="BDBODEGA">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-Todos-" Value="." />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    T Descuento</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcb_tipdes" runat="server" Culture="es-CO" Width="230px" Enabled="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="rcb_tipdes_SelectedIndexChanged"
                                    DataSourceID="obj_tdescuento" DataTextField="NOMBRE" DataValueField="ID" AppendDataBoundItems="true"
                                    SelectedValue='<%# Bind("ID_DESCUENTO") %>'>
                                    <Items>
                                        <telerik:RadComboBoxItem Text="-Seleccione-" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Inicial</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecini" runat="server" SelectedDate='<%# Bind("FECHAINI") %>' Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>
                                    F. Final</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fecfin" runat="server" SelectedDate='<%# Bind("FECHAFIN") %>' Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Vlr Descuento</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="edt_valor" runat="server" Culture="es-CO" Width="150px" Enabled="false"
                                    Text='<%# Bind("VALOR") %>' MaxValue="100" MinValue="0">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <uc1:ctrlArticulos ID="ctrlArticulos1" runat="server" />
                    </div>
                </asp:Panel>
            </ItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_almacenes" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLstBodegas" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="ALMACEN" Type="String" DefaultValue="S" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--<asp:ObjectDataSource ID="obj_linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="obj_descuentos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDescuentosArticulos" TypeName="XUSS.BLL.Costos.DescuentoBL"
        InsertMethod="InsertDecuento" OnInserted="obj_descuentos_Inserted" OnInserting="obj_descuentos_Inserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="BODEGA" Type="String" />
            <asp:Parameter Name="TP" Type="String" DefaultValue="" />
            <asp:Parameter Name="CLAVE1" Type="String" DefaultValue="" />
            <asp:Parameter Name="CLAVE2" Type="String" DefaultValue="" />
            <asp:Parameter Name="CLAVE3" Type="String" DefaultValue="" />
            <asp:Parameter Name="CLAVE4" Type="String" DefaultValue="" />
            <asp:Parameter Name="VALOR" Type="Double" />
            <asp:Parameter Name="FECHAINI" Type="DateTime" />
            <asp:Parameter Name="FECHAFIN" Type="DateTime" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tdescuento" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoDescuento" TypeName="XUSS.BLL.Costos.DescuentoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
