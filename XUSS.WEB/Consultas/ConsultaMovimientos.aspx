<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConsultaMovimientos.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultaMovimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                //debugger;
                //alert(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rg_detalle$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback"  ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Consulta Movimientos</h5>
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
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_barras" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td rowspan="4">
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" Icon-PrimaryIconCssClass="rbSearch"
                                ValidationGroup="UpdateBoton" CausesValidation="true">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td><label>Fec Inicial</label></td>
                        <td>
                            <telerik:RadDatePicker ID="edt_fini" runat="server"  MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker> 
                        </td>
                        <td><label>Fec Final</label></td>
                        <td>
                            <telerik:RadDatePicker ID="edt_ffin" runat="server"  MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Bodega</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega" runat="server" DataSourceID="obj_bodega" Width="300px"
                                DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>
                                Linea</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="Obj_Linea" Width="300px"
                                DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Referencia</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="edt_referencia" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>
                                Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="edt_nombre" runat="server" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>   
                    <tr>
                         <td><label>Transaccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_transaccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_transaccion" DataTextField="TMNOMBRE" 
                                    DataValueField="TMCDTRAN" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                    </tr>                 
                </table>
            </div>   
             <telerik:RadGrid ID="rg_detalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_consulta"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" >
                         <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"  ></Scrolling>
                                    </ClientSettings>
                        <MasterTableView ShowGroupFooter="true"  CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                 ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                                
                                <telerik:GridBoundColumn DataField="BARRAS" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="C. Barras" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BARRAS"
                                                UniqueName="BARRAS">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MBCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCLAVE1"
                                                UniqueName="MBCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">                                                
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="230px"
                                                HeaderText="T. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DESCRIPCION"
                                                UniqueName="DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MBCANMOV" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FBCANTID"
                                                UniqueName="MBCANMOV" FooterText="Total: " Aggregate="Sum">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MBFECMOV" HeaderButtonType="TextButton" HeaderStyle-Width="170px"
                                                HeaderText="Fec Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBFECMOV"
                                                UniqueName="MBFECMOV">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                
                                <telerik:GridBoundColumn DataField="DOCUMENTO" HeaderButtonType="TextButton" HeaderStyle-Width="65px"
                                                HeaderText="Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DOCUMENTO"
                                                UniqueName="DOCUMENTO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>     
                                <telerik:GridBoundColumn DataField="MBNMUSER" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBNMUSER"
                                                UniqueName="MBNMUSER">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                            
                                <telerik:GridBoundColumn DataField="MBESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBESTADO"
                                                UniqueName="MBESTADO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>   
                            </Columns>
                            <NoRecordsTemplate>
                                <div class="alert alert-danger">
                                    <strong>¡No se Encontaron Registros!</strong>
                                </div>
                            </NoRecordsTemplate>
                        </MasterTableView>

                    </telerik:RadGrid>
        </fieldset>   
        </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsultaMovimientos" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0" />
            <asp:Parameter Name="inFecIni" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="inFecFin" Type="DateTime" DefaultValue="01/01/1900"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="Obj_Linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLinea" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_transaccion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTipoMovimiento" TypeName="XUSS.BLL.Parametros.TipoMovimientoBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
