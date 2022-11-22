<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ConsultaVentasDetalle.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaVentasDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback"  ClientEvents-OnResponseEnd="endPostback"
        Width="100%">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Consulta Ventas Detalle</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
        <div>
            <table>
                <tr>
                    <td>
                        <label>C. Barras</label>
                    </td>
                    <td>
                            <telerik:RadTextBox ID="txt_barras" runat="server" Width="300px">
                            </telerik:RadTextBox>
                    </td>
                    <td>
                        <label>Bodega</label></td>
                    <td>
                        <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px" CheckBoxes="true"
                            Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE" 
                            DataValueField="BDBODEGA" >
                            
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Identificacion</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <label>Tercero</label></td>
                    <td>
                        <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <label>Linea</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="Obj_Linea" Width="300px" CheckBoxes="true"
                                DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">                                
                            </telerik:RadComboBox>
                    </td>
                    <td><label>Referencia</label></td>
                    <td>
                        <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true"  Width="300px">
                                                </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>F Incial</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecinicial" runat="server" 
                                                        MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                                                    </telerik:RadDatePicker>
                    </td>
                    <td>
                        <label>F Final</label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="edt_fecfinal" runat="server" 
                                                        MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                                                    </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_consultar" runat="server" Text="Consultar" OnClick="btn_consultar_OnClick" ValidationGroup="gvInsert"  />
                    </td>
                </tr>
            </table>
        </div>
        </fieldset>
        <telerik:RadGrid ID="rg_consulta" runat="server" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="True" ShowGroupPanel="True"
            Culture="(Default)" ShowFooter="True" DataSourceID="obj_ventas" Height="650px" GroupPanelPosition="Top" OnItemCommand="rg_consulta_ItemCommand" >
            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                        <Scrolling AllowScroll="True" SaveScrollPosition="true">
                        </Scrolling>
                    </ClientSettings>
            <MasterTableView ShowGroupFooter="true"  CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC"
                        UniqueName="HDFECFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HDFECING" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:HH:mm:ss}"
                        HeaderText="H. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECING"
                        UniqueName="HORA">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                        UniqueName="BDNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="T Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE"
                        UniqueName="TFNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDNROFAC"
                        UniqueName="HDNROFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn DataField="TFCLAFAC" HeaderText="c_doc" UniqueName="TFCLAFAC_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TFCLAFAC" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_cdoc" runat="server" Text='<%# Eval("TFCLAFAC") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="NRO_FAC" HeaderText="Documento" UniqueName="NRO_FAC_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="HDNROFAC" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("NRO_FAC") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link" Text="HDNROFAC" UniqueName="HDNROFAC" DataTextField="HDNROFAC"
                        HeaderText="Nro Documento" HeaderStyle-Width="100px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="HDAGENTE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Cod Ven" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDAGENTE"
                        UniqueName="HDAGENTE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                        HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMBRE"
                        UniqueName="NOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="500px"
                        HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                        UniqueName="ARNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="TP" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                        UniqueName="TANOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTCLAVE1"
                        UniqueName="DTCLAVE1">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                        UniqueName="CLAVE2">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                        UniqueName="CLAVE3">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                        DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                        Resizable="true" SortExpression="DTCANTID" UniqueName="DTCANTID" FooterText="Total: "
                        Aggregate="Sum">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ARCOSTOA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                         HeaderText="Costo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="ARCOSTOA" UniqueName="ARCOSTOA">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                         HeaderText="P Lista" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="DTPRELIS" UniqueName="DTPRELIS">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTTOTDES" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                         HeaderText="Descuento" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="DTTOTDES" UniqueName="DTTOTDES">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TOTDES" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="T. Descuento" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="TOTDES" UniqueName="TOTDES">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Sub-Total" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="DTSUBTOT" UniqueName="DTSUBTOT">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTTOTIVA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                         HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="DTTOTIVA" UniqueName="DTTOTIVA">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DTTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                         HeaderText="T. Factura" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="DTTOTFAC" UniqueName="DTTOTFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HDESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDESTADO"
                        UniqueName="HDESTADO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NOMDES" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Descuento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMDES"
                        UniqueName="NOMDES">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="HDCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Id Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDCODNIT"
                        UniqueName="HDCODNIT">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                    
                    <telerik:GridBoundColumn DataField="ARDTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="D Tec 1" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARDTTEC1"
                        UniqueName="ARDTTEC1">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                    
                    <telerik:GridBoundColumn DataField="ARDTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="D Tec 2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARDTTEC2"
                        UniqueName="ARDTTEC2">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ARDTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="D Tec 4" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARDTTEC4"
                        UniqueName="ARDTTEC4">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                          
                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="300px"
                        HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                        UniqueName="TRNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TRCORREO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Email" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCORREO"
                        UniqueName="TRCORREO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Ciudad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CDNOMBRE"
                        UniqueName="CDNOMBRE">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CDREGION" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                        HeaderText="Region" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CDREGION"
                        UniqueName="CDREGION">
                        <ItemStyle HorizontalAlign="Right" />
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
    <asp:ObjectDataSource ID="obj_ventas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDetalleFacturacion" TypeName="XUSS.BLL.Consultas.ConsultasFacturasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />           
            <asp:Parameter Name="filter" Type="String" DefaultValue=" 1=0"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--<asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuario" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />                     
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />            
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="Obj_Linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLinea" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
