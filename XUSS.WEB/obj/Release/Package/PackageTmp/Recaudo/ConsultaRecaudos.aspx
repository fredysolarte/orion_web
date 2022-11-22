<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaRecaudos.aspx.cs" Inherits="XUSS.WEB.Recaudo.ConsultaRecaudos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rgDetalle$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
                    <h5>Consulta Recaudos</h5>
                </div>
            </div>
        </fieldset>
         <asp:Panel ID="pn_filart" runat="server" DefaultButton="btn_buscar" Width="100%">
            <table>
                <tr>
                    <td>
                        <label>Nro Recibo</label></td>
                    <td>
                        <telerik:RadTextBox ID="edt_nrorecibo" runat="server" Enabled="true" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>F. Rec Ini</label></td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecini" runat="server"
                            MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label>F. Rec Fin</label></td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecfin" runat="server"
                            MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>F. Cap Ini</label></td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fcfecini" runat="server"
                            MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                        </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label>F. Cap Fin</label></td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fcfecfin" runat="server"
                            MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Nro Factura</label></td>
                    <td>
                        <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="true" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <label>C. Cliente</label></td>
                    <td>
                        <telerik:RadTextBox ID="txt_codcliente" runat="server" Enabled="true" Width="300px">
                        </telerik:RadTextBox>
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
                        <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_Click" Icon-PrimaryIconCssClass="rbSearch"
                            ValidationGroup="gvInsert" CausesValidation="true">
                        </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <telerik:RadGrid ID="rg_consulta" runat="server" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="false" ShowGroupPanel="True" OnDetailTableDataBind="rg_consulta_DetailTableDataBind"
            Culture="(Default)" ShowFooter="True" Height="650px" GroupPanelPosition="Top" OnItemCommand="rg_consulta_ItemCommand">
            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="RC_NRORECIBO">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <DetailTables>
                    <telerik:GridTableView Name="detalle_item" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn SortExpression="RC_CONCEPTO" HeaderText="" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="RC_CONCEPTO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="TTDESCRI" HeaderText="Concepto" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="TTDESCRI">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                HeaderText="Vlr Recaudo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                                Resizable="true" SortExpression="RC_VALOR" UniqueName="RC_VALOR" Aggregate="Sum">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="NROFAC" HeaderText="Documento" HeaderButtonType="TextButton" AllowFiltering="false"
                                DataField="NROFAC">
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
                    <telerik:GridBoundColumn DataField="BN_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Banco" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BN_NOMBRE"
                        UniqueName="BN_NOMBRE">
                    </telerik:GridBoundColumn>
                     <telerik:GridTemplateColumn DataField="RC_NRORECIBO" HeaderText="Propietario" UniqueName="RC_NRORECIBO"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="RC_NRORECIBO" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_recibo" runat="server" Text='<%# Eval("RC_NRORECIBO") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridButtonColumn CommandName="link_rc" Text="RC_NRORECIBO" UniqueName="RC_NRORECIBO_TK" DataTextField="RC_NRORECIBO"
                        HeaderText="Nro Recibo" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="RC_FECREC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Rec" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RC_FECREC"
                        UniqueName="RC_FECREC">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="TRCODNIT" HeaderText="Propietario" UniqueName="TRCODNIT_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TRCODNIT" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_tercero" runat="server" Text='<%# Eval("TRCODNIT") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link_tr" Text="TRCODNIT" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                        HeaderText="Identificacion" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="TERCERO" HeaderButtonType="TextButton" HeaderStyle-Width="400px"
                        HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TERCERO"
                        UniqueName="TERCERO">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TRDIRECC" HeaderButtonType="TextButton" HeaderStyle-Width="350px"
                        HeaderText="Direccion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRDIRECC"
                        UniqueName="TRDIRECC">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Vlr Recaudo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="VALOR" UniqueName="VALOR">
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

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
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
    <%--
        DataSourceID="obj_recaudo" 
        <asp:ObjectDataSource ID="obj_recaudo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsultaRecaudos" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0" />
            <asp:Parameter Name="paramtervalues" Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
</asp:Content>
