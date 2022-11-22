<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaCuentasxPagar.aspx.cs" Inherits="XUSS.WEB.Compras.ConsultaCuentasxPagar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
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
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Consulta Recaudos</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <table>
                <tr>
                    <td></td>
                </tr>
            </table>
        </fieldset>
        <telerik:RadGrid ID="rg_consulta" runat="server" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="false" ShowGroupPanel="True" OnDetailTableDataBind="rg_consulta_DetailTableDataBind"
            Culture="(Default)" ShowFooter="True" DataSourceID="obj_consulta" Height="650px" GroupPanelPosition="Top" OnItemCommand="rg_consulta_ItemCommand">
            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="FD_NROFACTURA,FD_NROCMP">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <DetailTables>
                    <telerik:GridTableView Name="detalle_item" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="TANOMBRE" HeaderText="Marca" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="TANOMBRE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="FD_CLAVE1" HeaderText="Referencia" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="FD_CLAVE1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="ARNOMBRE" HeaderText="Producto" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="ARNOMBRE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FD_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Cant" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="FD_CANTIDAD" UniqueName="TOT" Aggregate="Sum">
                            </telerik:GridBoundColumn>  
                            <telerik:GridBoundColumn DataField="TOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Vlr Recaudo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="TOT" UniqueName="TOT" Aggregate="Sum">
                            </telerik:GridBoundColumn>                            
                        </Columns>
                        <NoRecordsTemplate>
                            <div class="alert alert-danger">
                                <strong>¡No se Encontaron Registros!</strong>
                            </div>
                        </NoRecordsTemplate>
                    </telerik:GridTableView>
                </DetailTables>
                <Columns>                     
                    <telerik:GridTemplateColumn DataField="NIT_TER" HeaderText="Propietario" UniqueName="NIT_TER_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="NIT_TER" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_tercero" runat="server" Text='<%# Eval("NIT_TER") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridButtonColumn CommandName="link_tr" Text="NIT_TER" UniqueName="NIT_TER" DataTextField="NIT_TER"
                        HeaderText="Identificacion" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="TERCERO" HeaderButtonType="TextButton" HeaderStyle-Width="400px"
                        HeaderText="Proveedor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TERCERO"
                        UniqueName="TERCERO">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="FD_NROCMP" HeaderText="Propietario" UniqueName="FD_NROCMP_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="FD_NROCMP" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_cmp" runat="server" Text='<%# Eval("FD_NROCMP") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link_cmp" Text="FD_NROCMP" UniqueName="FD_NROCMP" DataTextField="FD_NROCMP"
                        HeaderText="Nro CMP" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="FD_NROFACTURA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Nro Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROFACTURA"
                        UniqueName="FD_NROFACTURA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FD_FECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Fac" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_FECFAC"
                        UniqueName="FD_FECFAC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="F_VENCIMIENTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Ven" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="F_VENCIMIENTO"
                        UniqueName="F_VENCIMIENTO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TOT_FAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="TOT_FAC" UniqueName="TOT_FAC">
                    </telerik:GridBoundColumn>
                </Columns>
                <NoRecordsTemplate>
                    <div class="alert alert-danger">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <strong>Alerta!</strong>No Existen Registros
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsultaPagos" TypeName="XUSS.BLL.Compras.OrdenesComprasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />                 
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=1"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
