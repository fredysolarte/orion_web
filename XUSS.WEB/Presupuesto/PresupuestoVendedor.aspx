<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PresupuestoVendedor.aspx.cs" Inherits="XUSS.WEB.Presupuesto.PresupuestoVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="100000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <fieldset class="cssFieldSetContainer">
        <div class="box">
            <div class="title">
                <h5>Presupuesto x Vendedor</h5>
            </div>
        </div>
    </fieldset>
    <div>
        <table>
            <tr>
                <td>
                    <label>
                        Mes</label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rc_mes" runat="server" AppendDataBoundItems="true">
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="Enero" />
                            <telerik:RadComboBoxItem Value="2" Text="Febrero" />
                            <telerik:RadComboBoxItem Value="3" Text="Marzo" />
                            <telerik:RadComboBoxItem Value="4" Text="Abril" />
                            <telerik:RadComboBoxItem Value="5" Text="Mayo" />
                            <telerik:RadComboBoxItem Value="6" Text="Junio" />
                            <telerik:RadComboBoxItem Value="7" Text="Julio" />
                            <telerik:RadComboBoxItem Value="8" Text="Agosto" />
                            <telerik:RadComboBoxItem Value="9" Text="septiembre" />
                            <telerik:RadComboBoxItem Value="10" Text="Octubre" />
                            <telerik:RadComboBoxItem Value="11" Text="Noviembre" />
                            <telerik:RadComboBoxItem Value="12" Text="Diciembre" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <label>
                        Año</label>
                </td>
                <td>
                    <telerik:RadComboBox ID="rc_ano" runat="server" AppendDataBoundItems="true">
                        <Items>
                            <telerik:RadComboBoxItem Value="2016" Text="2016" />
                            <telerik:RadComboBoxItem Value="2017" Text="2017" />
                            <telerik:RadComboBoxItem Value="2018" Text="2018" />
                            <telerik:RadComboBoxItem Value="2019" Text="2019" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadButton ID="btn_consultar" runat="server" Text="Consultar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_consultar_Click">
                    </telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton ID="btn_guardar" runat="server" Text="Guardar" Icon-PrimaryIconCssClass="rbSave" OnClick="btn_guardar_Click">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
            Culture="(Default)" CellSpacing="0"
            ShowFooter="true">
            <MasterTableView>
                <Columns>                   
                    <telerik:GridBoundColumn DataField="ano" HeaderButtonType="TextButton" HeaderStyle-Width="30px"
                        HeaderText="Año" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ano">                        
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="mes" HeaderButtonType="TextButton" HeaderStyle-Width="30px"
                        HeaderText="Mes" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="mes">                        
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="cod_vendedor" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Cod" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="cod_vendedor">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VENDEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Agente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VENDEDOR">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="Ventas" HeaderText="Ventas" UniqueName="Ventas"
                        HeaderStyle-Width="150px" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="{0:C}">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="edt_ventas" runat="server" Value='<%# GetValor(Eval("Ventas")) %>'
                                Width="150px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Cartera" HeaderText="Cartera" UniqueName="Cartera"
                        HeaderStyle-Width="150px" FooterText="Total: " Aggregate="Sum" FooterAggregateFormatString="{0:C}">
                        <ItemTemplate>
                            <telerik:RadNumericTextBox ID="edt_Cartera" runat="server" Value='<%# GetValor(Eval("Cartera")) %>'
                                Width="150px">
                                <NumberFormat DecimalDigits="0" />
                            </telerik:RadNumericTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <NoRecordsTemplate>
                    <div class="alert alert-danger">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <strong>Alerta!</strong>No Tiene Registros
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <asp:ObjectDataSource ID="obj_agente" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" TRINDVEN='S'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
