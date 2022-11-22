<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CierreContable.aspx.cs" Inherits="XUSS.WEB.Contabilidad.CierreContable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server"> 
        <script type="text/javascript">
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Cerrar los Meses Seleccionados?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Cierre Contable</h5>
                </div>
            </div>
        </fieldset>
        <table>
            <tr>
                <td>
                    <label>Año</label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rc_ano" runat="server" Culture="es-CO" Width="300px"
                        Enabled="true" DataSourceID="obj_anos" DataTextField="MA_ANO" AutoPostBack="true"
                        DataValueField="MA_ANO" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_ano_SelectedIndexChanged">
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td><label>Mes</label></td>
                <td>
                    <telerik:RadComboBox ID="rc_mes" runat="server" Culture="es-CO" Width="300px" CheckBoxes="true"
                        Enabled="true" AppendDataBoundItems="true" >
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                        </Items>
                    </telerik:RadComboBox>
                </td>                
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="btn_cerrar" runat="server" OnClick="btn_cerrar_Click"  Text="Cerrar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbOk" OnClientClicked="Clicking">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="750px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
    <asp:ObjectDataSource ID="obj_anos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAnos" TypeName="XUSS.BLL.Parametros.MesesBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=1" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />            
            <asp:Parameter Name="maximumRows" Type="Int32" />                        
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
