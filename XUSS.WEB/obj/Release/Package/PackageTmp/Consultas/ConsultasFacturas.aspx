<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="ConsultasFacturas.aspx.cs" Inherits="XUSS.WEB.Consultas.ConsultasFacturas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function rgDetalle_OnRowDblClick(sender, eventArgs) {
            //var modal = $find('MEditTicket');  //haciendo referencia al behavior, y NO al id
            //modal.show();
            document.getElementById("ctl00_ContentPlaceHolder1_toEditRow").value = eventArgs.get_itemIndexHierarchical();
            __doPostBack("ModalEdit", "");

        }
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
    <input type="text" id="toEditRow" name="toEditRow" value="-1" style="display: none" runat="server" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback"  ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Conulta Documentos (Facturas-Remisiones-Devoluciones)</h5>
                </div>
            </div>
        </fieldset>
        <div>
            <table>
                <tr>
                    <td>
                        <label>
                            Identificación</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_identificacion" runat="server" Width="300px">
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
                    <td>
                        <label>T. Documento</label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px" 
                                                Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" 
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>                                                
                                            </telerik:RadComboBox>
                    </td>
                    <td>
                        <label>Nro. Documento</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_NumFac" runat="server" Width="300px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Referencia</label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="edt_referencia" runat="server" Width="300px">
                        </telerik:RadTextBox>
                    </td>

                    <td>
                        <asp:Button ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click"
                            ValidationGroup="UpdateBoton" CausesValidation="true" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                Culture="(Default)" CellSpacing="0" DataSourceID="obj_consulta"
                AllowPaging="false">
                <MasterTableView DataSourceID="obj_consulta" DataKeyNames="HDNROFAC" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <RowIndicatorColumn Visible="true">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridBoundColumn DataField="NOMBRE" HeaderButtonType="TextButton"
                            HeaderStyle-Width="120px" HeaderText="Tercero"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMBRE">
                            <HeaderStyle Width="120px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton"
                            HeaderStyle-Width="100px" HeaderText="Nro. Factura"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDNROFAC">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDTOTFAC" DataFormatString="{0:C}"
                            HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            HeaderText="Vlr. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true"
                            SortExpression="HDTOTFAC">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton"
                            HeaderStyle-Width="80px" HeaderText="Fec. Factura" DataFormatString="{0:dd/MM/yyyy}"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton"
                            HeaderStyle-Width="80px" HeaderText="Almacen" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BDNOMBRE">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HDTIPFAC" HeaderButtonType="TextButton"
                            HeaderStyle-Width="80px" HeaderText="T. Factura"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDTIPFAC"
                            Visible="true">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton"
                            HeaderStyle-Width="200px" HeaderText="T. Factura"
                            ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE">
                            <HeaderStyle Width="200px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn DataField="HDNROFAC" HeaderText="Detalle Factura" UniqueName="HDNROFAC_">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnk_detalle" runat="server" OnClick="OnClick_lnk_detalle">Detalle</asp:LinkButton>                                                                                               
                            </ItemTemplate>
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </telerik:GridTemplateColumn>--%>
                    </Columns>
                    <NoRecordsTemplate>
                        <div id="message" runat="server">
                            <div id="box-messages" class="box">
                                <div class="messages">
                                    <div id="message-notice" class="message message-notice">
                                        <div class="image">
                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                        </div>
                                        <div class="text">
                                            <h6>Información</h6>
                                            <span>No existen Resultados </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </NoRecordsTemplate>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents OnRowDblClick="rgDetalle_OnRowDblClick" />
                </ClientSettings>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
                </HeaderContextMenu>
            </telerik:RadGrid>
            
            <div style="display: none;">
                <asp:Button ID="Button1" runat="server" Text="Button" />
            </div>
            <cc1:ModalPopupExtender ID="mp_detallefac" runat="server" PopupControlID="pnlAlertMensaje2"
                TargetControlID="Button1" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrar">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="pnlAlertMensaje2" runat="server" CssClass="modalPopup" Style="display: none;">
                <fieldset class="cssFieldSetContainer" style="width: 850px !important">
                    <div class="box">
                        <div class="title">
                            <h5>Items Facturados
                            </h5>
                        </div>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <label>T. Factura</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_TFacturaD" runat="server" Width="200px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>Factura</label></td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nFacturaD" runat="server" Width="200px" Enabled="false">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding: 5px 5px 5px 5px">
                        <telerik:RadGrid ID="rgFacturaDetalle" runat="server" GridLines="None"
                            Width="840px" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" DataSourceID="obj_detallefac"
                            AllowPaging="True"
                            OnSelectedIndexChanged="rgFacturaDetalle_SelectedIndexChanged">
                            <MasterTableView DataSourceID="obj_detallefac">
                                <Columns>
                                    <telerik:GridBinaryImageColumn DataField="IM_IMAGEN" HeaderText="Image" UniqueName="Upload"
                                        ImageHeight="80px" ImageWidth="80px" ResizeMode="Fit" DataAlternateTextField="ARNOMBRE"
                                        DataAlternateTextFormatString="Foto: {0}">
                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                    </telerik:GridBinaryImageColumn>
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Articulo" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="ARNOMBRE">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="ARCLAVE1">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText=" " ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="CLAVE2">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText=" " ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DTPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="300px" DataFormatString="{0:C}"
                                        HeaderText="Vlr. Lista" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTPRECIO">
                                        <HeaderStyle Width="300px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PVENTA" HeaderButtonType="TextButton" HeaderStyle-Width="300px" DataFormatString="{0:C}"
                                        HeaderText="Vlr. Venta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PVENTA">
                                        <HeaderStyle Width="300px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DTDESCUE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:0}"
                                        HeaderText="V. Dcto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTDESCUE">
                                        <HeaderStyle Width="80px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOMDES" HeaderButtonType="TextButton" HeaderStyle-Width="300px"
                                        HeaderText="T. Dcto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMDES">
                                        <HeaderStyle Width="300px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div style="text-align: center;">

                            <telerik:RadButton ID="btn_cerrar" runat="server" Text="Cerrar"></telerik:RadButton>
                        </div>
                    </div>
                </fieldset>
            </asp:Panel>
        </div>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFacturasExistentes" TypeName="XUSS.BLL.Consultas.ConsultasFacturasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter_" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter__" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_detallefac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDetalleFactura" TypeName="XUSS.BLL.Consultas.ConsultasFacturasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="TFCLAFAC IN (1,2,3,4,5,6)" Name="filter" Type="String" />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
