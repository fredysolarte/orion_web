<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CierreCaja.aspx.cs" Inherits="XUSS.WEB.Facturacion.CierreCaja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" >
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnMaestro" runat="server" >
                <div class="box">
                    <div class="title">
                        <h5>Cierre de Caja</h5>
                    </div>
                </div>
                <table>
                    <tr>
                            <td><label>Almacen</label></td>
                        <td colspan="3">
                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE"
                                DataValueField="TFTIPFAC">
                                <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td><label>F Inicial</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecini" runat="server" AutoPostBack="true"
                                                MinDate="01/01/1900" Enabled="true" OnSelectedDateChanged="txt_fecini_SelectedDateChanged">
                                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_fecini" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                        <td><label>F Final</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecfin" runat="server" 
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_fecfin" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint"  ToolTip="Imprimir" ValidationGroup="gvInsert" />
                        </td>
                        <td>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_cerrar" runat="server" Text="Cerrar Caja"  Icon-PrimaryIconCssClass="rbUpload"  ToolTip="Imprimir" ValidationGroup="gvInsert" OnClick="btn_cerrar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="320px" Modal="true" OffsetElementID="main"  Title="Detalle"  EnableShadow="true">
                    <ContentTemplate>
                        <telerik:RadGrid ID="rgFacturas" runat="server" AllowSorting="True" Width="800px"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None">
                            <MasterTableView>
                                <Columns>                                                                        
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="TANOMBRE" HeaderText="Linea"
                                        UniqueName="TANOMBRE" HeaderButtonType="TextButton" DataField="TANOMBRE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="BBCLAVE1" HeaderText="Referencia"
                                        UniqueName="BBCLAVE1" HeaderButtonType="TextButton" DataField="BBCLAVE1" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
                                        UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="120px">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="C2"
                                        UniqueName="CLAVE2" HeaderButtonType="TextButton" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="C3"
                                        UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn> 
                                    <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Cantidad"
                                        UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="90px">
                                    </telerik:GridBoundColumn> 
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
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
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="TFCLAFAC IN (1)" Name="filter" Type="String" />       
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />    
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
