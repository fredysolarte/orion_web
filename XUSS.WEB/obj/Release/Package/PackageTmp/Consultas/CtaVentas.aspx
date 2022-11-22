<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CtaVentas.aspx.cs" Inherits="XUSS.WEB.Consultas.CtaVentas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews"
             />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Indicadores Vendedores Promedio</h5>
                </div>
            </div>
        </fieldset>    
    <div>
            <asp:ImageButton ID="ImageButton1" runat="server" SkinID="SkinBuscarUC" OnClick="im_buscar" 
                ToolTip="Nuevo Ticket" />
        </div>
    <div>
        <telerik:RadGrid ID="rgDetalle" runat="server" GridLines="None" Width="600px" AutoGenerateColumns="False"
             Culture="(Default)" CellSpacing="0" 
            DataSourceID="obj_consulta" AllowPaging="True" >
            <MasterTableView DataSourceID="obj_consulta">
                <Columns>
                    <telerik:GridBoundColumn DataField="bodega" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="BODEGA" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="bodega">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="TOT" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Venta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TOT">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="NROPRENDAS" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Prendas" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NROPRENDAS">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="NROFACTURAS" HeaderButtonType="TextButton" HeaderStyle-Width="300px"
                        HeaderText="Facturas" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NROFACTURAS">
                        <HeaderStyle Width="300px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                
                    <telerik:GridBoundColumn DataField="VLRPRPROM" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                        HeaderText="Vlr Prenda Prom" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRPRPROM">
                        <HeaderStyle Width="200px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="UNDPROMFAC" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Und Prom Fac" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="UNDPROMFAC">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VLRFACPROM" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="Vlr Prom Fac" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VLRFACPROM">
                        <HeaderStyle Width="80px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>            
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
                                        <h6>
                                            Información</h6>
                                        <span>No existen Resultados </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
            </HeaderContextMenu>
        </telerik:RadGrid>

        <asp:ModalPopupExtender ID="ModalPopupConsulta" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MEditTicket" PopupControlID="pnPopUpEdit" TargetControlID="Button3">
        </asp:ModalPopupExtender>
        <div style="display: none;">
            <asp:Button ID="Button3" runat="server" Text="Button" />
        </div>
        <asp:Panel ID="pnPopupEdit" runat="server" CssClass="modalPopup" Style="display: none;">
            <fieldset class="cssFieldSetContainer" style="width: 590px !important">
                <div class="box">
                    <div class="title">
                        <h5>
                            Datos a Buscar</h5>
                    </div>
                </div>
                <div style="width: 100%">
                    <table>                        
                        <tr>
                            <td>
                                <label>Fecha Inicial</label>
                            </td>
                            <td>
                                <telerik:RadCalendar ID="edt_FecIni" runat="server">
                                </telerik:RadCalendar>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fecha Final</label>
                            </td>
                            <td>
                                <telerik:RadCalendar ID="edt_FecFIn" runat="server">
                                </telerik:RadCalendar>
                            </td>
                        </tr>
                        
                    </table>
                </div>
            </fieldset>
            <div style="text-align: right;">
            <asp:Button ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton" CausesValidation="true" />
            <asp:Button ID="bt_cerrar" runat="server" Text="Cerrar" OnClick="bt_cerrar_click" />
        </div>
        </asp:Panel>
        
    </div>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetVentas" TypeName="XUSS.BLL.Consultas.CtaVentasBL">
            <SelectParameters>
                <asp:Parameter Name="connection" Type="String" />
                <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
                <asp:Parameter Name="FecIni" Type="DateTime" />
                <asp:Parameter Name="FecFin" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>
