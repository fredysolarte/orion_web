<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="DiarioLegal.aspx.cs" Inherits="XUSS.WEB.Contabilidad.DiarioLegal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">            
            function OnClientEntryAddingHandler(sender, eventArgs) {

                if (sender.get_entries().get_count() > 1) {
                    eventArgs.set_cancel(true);
                }

            }
        </script>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Diario Legal</h5>
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
                    <telerik:RadComboBox ID="rc_mes" runat="server" Culture="es-CO" Width="300px"
                        Enabled="true" AppendDataBoundItems="true" >
                        <Items>
                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                
            </tr>
            <tr>                
                <td>
                    <label>Cta Contable</label></td>
                <td>
                    <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single"
                        DataSourceID="obj_ctacontable" Width="450px" DataTextField="PC_NOMBRE" DropDownWidth="550px" Enabled="true"
                        OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="PC_CODIGO"
                        DropDownHeight="580px" Filter="StartsWith">
                        <DropDownItemTemplate>
                            <table cellspacing="1">
                                <tr>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "PC_CODIGO")%>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "PC_NOMBRE")%>
                                    </td>
                                </tr>
                            </table>
                        </DropDownItemTemplate>
                    </telerik:RadAutoCompleteBox>
                </td>
                
            </tr>
            <tr>
                <td><label>Tipo</label></td>
                <td>
                    <telerik:RadComboBox ID="rc_tipo" runat="server" Enabled="true" Width="300px" AppendDataBoundItems="true">
                        <Items>                            
                            <telerik:RadComboBoxItem Text="Clase" Value="1" />
                            <telerik:RadComboBoxItem Text="Grupo" Value="2" />
                            <telerik:RadComboBoxItem Text="Cuenta" Value="3" />
                            <telerik:RadComboBoxItem Text="SubCuenta" Value="4" />
                            <telerik:RadComboBoxItem Text="Auxiliar" Value="5" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_Click"  Text="Buscar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_ctacontable" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
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
