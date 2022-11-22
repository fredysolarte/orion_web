<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ConDescuentosArticulos.aspx.cs" Inherits="XUSS.WEB.Consultas.ConDescuentosArticulos" %>
<%@ Register Src="../ControlesUsuario/ctrl_gesArticulosHor.ascx" TagPrefix="uc1" TagName="ctrlArticulos"%>

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
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Consulta Descuentos x Articulo</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
            <tr>
                    <td>
                        <label>
                            Bodega</label>
                    </td>
                    <td colspan="4">
                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="358px"
                            DataSourceID="obj_bodega" DataTextField="BDNOMBRE" DataValueField="BDBODEGA"
                            AppendDataBoundItems="true">
                            <Items>
                                <telerik:RadComboBoxItem Value="." Text="Todos" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>                
            </table>
            <uc1:ctrlArticulos id="ctrlArticulos1" runat="server" />
            <table>
                <tr>
                    <td>
                        <telerik:RadButton ID="btn_buscar" runat="server" Text="Buscar" OnClick="btn_buscar_Click">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel runat="server" ID="pnlVisor">
            <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
        </asp:Panel>
        <div>
        </div>
    </telerik:RadAjaxPanel>
<asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLstBodegas" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="ALMACEN" Type="String" DefaultValue="S" />
        </SelectParameters>
    </asp:ObjectDataSource>    
</asp:Content>
