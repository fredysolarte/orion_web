<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="EstadoPedidosGantt.aspx.cs" Inherits="XUSS.WEB.Consultas.EstadoPedidosGantt" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI.Gantt" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
       <script>
           //debugger;
            var $ = $telerik.$;
            function OnClientDataBound(sender, args) {
                var dataItems = sender.get_allTasks();

                for (var j = 0; j < dataItems.length; j++) {
                    var dataItem = dataItems[j];
                    var row = $("[data-uid='" + dataItem._uid + "']");

                    // .eq(1) determines which column should be converted into a hyperlink
                    var span = row.find("td").eq(0).find("span").last();
                   
                    span.replaceWith(function () {
                        // custom logic for creating the link URL
                        var url = $.trim($(this).text());                        
                        return '<a href="' + window.location.origin + '/Pedidos/Pedidos.aspx?Pedido=' + url + '" target="_blank">' + url + '</a>';                        
                    });
                }
            }
        </script>
       <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_buscar">
                <div>
                    <table>
                        <tr>
                            <td><label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" Width="200px" CheckBoxes="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Pedido" Value="1" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="2" />
                                        <telerik:RadComboBoxItem Text="Packing" Value="3" />
                                        <telerik:RadComboBoxItem Text="Facturado/Remision" Value="4" />
                                        <telerik:RadComboBoxItem Text="Entregado" Value="5" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Mes</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_mes" runat="server" Width="200px" CheckBoxes="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                        <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                        <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                        <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                        <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                        <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                        <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                        <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                        <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                        <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                        <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                        <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Año</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_ano" runat="server" Width="200px" CheckBoxes="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="2018" Value="2018" />
                                        <telerik:RadComboBoxItem Text="2019" Value="2019" />
                                        <telerik:RadComboBoxItem Text="2020" Value="2020" />
                                        <telerik:RadComboBoxItem Text="2021" Value="2021" />
                                        <telerik:RadComboBoxItem Text="2022" Value="2022" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_Click" Icon-PrimaryIconCssClass="rbSearch"
                                    ValidationGroup="UpdateBoton" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
        </asp:Panel>
    <telerik:RadGantt ID="gt_pedidos" runat="server" DataSourceID="obj_consulta" RenderMode="Lightweight" Width="100%" SelectedView="MonthView" Skin="Bootstrap" AllowColumnResize="true"
        AutoGenerateColumns="false" AllowTaskDelete="False" AllowTaskInsert="False" AllowTaskUpdate="False" DisplayDeleteConfirmation="False" EnablePdfExport="True" OnClientDataBound="OnClientDataBound" >
        <YearView UserSelectable="true"/>
        <Columns>
            <telerik:GanttBoundColumn DataField="Id" HeaderText="Pedido" DataType="String" Width="30px"></telerik:GanttBoundColumn>
            <telerik:GanttBoundColumn DataField="Title" HeaderText="Cliente" DataType="String" Width="120px"></telerik:GanttBoundColumn>
            <telerik:GanttBoundColumn DataField="Start" DataType="DateTime" DataFormatString="dd/MM/yy HH:MM" Width="40px"></telerik:GanttBoundColumn>
            <telerik:GanttBoundColumn DataField="End" DataType="DateTime" DataFormatString="dd/MM/yy HH:MM" Width="40px"></telerik:GanttBoundColumn>
            <%--<telerik:GanttBoundColumn DataField="N_Estado" HeaderText="Estado" DataType="String" Width="40px"></telerik:GanttBoundColumn>--%>
            <telerik:GanttBoundColumn DataField="PercentComplete" DataType="Number" Width="40px"></telerik:GanttBoundColumn>            
        </Columns>
        <DataBindings>
                <TasksDataBindings
                    IdField="ID" 
                    StartField="Start" EndField="End"
                    OrderIdField="OrderID"                    
                    TitleField="Title" PercentCompleteField="PercentComplete" />
                <%--<DependenciesDataBindings
                    TypeField="Type" IdField="ID"
                    PredecessorIdField="PredecessorID"
                    SuccessorIdField="SuccessorID" />--%>
            </DataBindings>
    </telerik:RadGantt>

    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTrazePedidos" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="AND 1=1" Name="inFilter" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
