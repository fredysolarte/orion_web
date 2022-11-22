<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CarteraxCliente.aspx.cs" Inherits="XUSS.WEB.Recaudo.CarteraxCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rg_consulta$ctl00$ctl02$ctl00$ExportToExcelButton")) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Cartera x Cliente</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <div>
                <table>
                    <tr>
                        <td>
                            <label>Fecha</label></td>
                        <td>
                            <telerik:RadDatePicker ID="edt_fecha" runat="server"
                                MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>Saldo 0</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chk_saldo" runat="server" />
                        </td>
                        <td rowspan="3">
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" Icon-PrimaryIconCssClass="rbSearch"
                                ValidationGroup="gvInsert" CausesValidation="true">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>I. Cliente</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_icliente" runat="server" Enabled="true" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>Nombre</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Vendedor</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px" Enabled="true" DataSourceID="obj_agente" DataTextField="TRNOMBRE"
                                DataValueField="TRCODTER" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <telerik:RadGrid ID="rg_consulta" runat="server" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="True" ShowGroupPanel="True"
            Culture="(Default)" ShowFooter="True" DataSourceID="obj_cartera" Height="650px" GroupPanelPosition="Top" OnItemCommand="rg_consulta_ItemCommand">
            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridTemplateColumn DataField="TRCODTER" HeaderText="NIT_CLIENTE" UniqueName="TRCODNIT_LBL"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TRCODTER" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("TRCODTER") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                        UniqueName="TRCODNIT">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="400px"
                        HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLIENTE"
                        UniqueName="CLIENTE">                        
                    </telerik:GridBoundColumn>   --%>
                    <telerik:GridButtonColumn CommandName="tercero" Text="TRCODNIT" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                        HeaderText="Identificacion" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="tercero" Text="CLIENTE" UniqueName="CLIENTE" DataTextField="CLIENTE"
                        HeaderText="Cliente" HeaderStyle-Width="400px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="CAR_30" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Cartera 30" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="CAR_30" UniqueName="CAR_30">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CAR_60" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Cartera 60" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="CAR_60" UniqueName="CAR_60">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CAR_90" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Cartera 90" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="CAR_90" UniqueName="CAR_90">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FACTURACION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="T Facturacion" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="FACTURACION" UniqueName="FACTURACION">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="RECAUDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Recaudo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="RECAUDO" UniqueName="RECAUDO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridButtonColumn CommandName="Recaudo" Text="RECAUDO" UniqueName="RECAUDO" DataTextField="RECAUDO"
                        HeaderText="Recaudo" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="DEVOLUCION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Devolucion" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="DEVOLUCION" UniqueName="DEVOLUCION">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="NC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="N Credito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="NC" UniqueName="NC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ND" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="N Debito" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="ND" UniqueName="ND">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>   --%>                        
                     <telerik:GridButtonColumn CommandName="lkn_nc" Text="NC" UniqueName="NC" DataTextField="NC"
                        HeaderText="N Credito" HeaderStyle-Width="100px"  ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="lkn_nd" Text="ND" UniqueName="ND" DataTextField="ND"
                        HeaderText="N Debito" HeaderStyle-Width="100px"  ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>
                    <telerik:GridButtonColumn CommandName="saldo" Text="SALDO" UniqueName="SALDO" DataTextField="SALDO"
                        HeaderText="Saldo" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>
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
    <asp:ObjectDataSource ID="obj_cartera" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetCarteraxCliente" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />      
            <asp:Parameter Name="inFecha" Type="DateTime" DefaultValue="01/01/2000" />
            <asp:Parameter Name="inFiltro" Type="String" DefaultValue=" AND 1=0"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
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
