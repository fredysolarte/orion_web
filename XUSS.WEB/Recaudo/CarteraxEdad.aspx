<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="CarteraxEdad.aspx.cs" Inherits="XUSS.WEB.Recaudo.CarteraxEdad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>
                        Cartera x Edad</h5>
                </div>
            </div>
        </fieldset>
    <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_buscar">
                <table>
                    <tr>
                        <td><label>Fecha Corte</label></td>
                        <td>
                                <telerik:RadDatePicker ID="edt_fecha" runat="server" 
                                                        MinDate="01/01/1900" Enabled="true" ValidationGroup="gvInsert">
                                                    </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>Saldo 0</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chk_saldo" runat="server"/>
                        </td>                        
                    </tr>
                    <tr>
                        <td><label>Nro Factura</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="true" Width="300px">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <label>Cod Cliente</label></td>
                        <td>
                            <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Width="300px">
                            </telerik:RadTextBox>
                        </td>
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
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" Icon-PrimaryIconCssClass="rbSearch"
                                ValidationGroup="gvInsert" CausesValidation="true">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>            
            </asp:Panel>
        </fieldset>
    <telerik:RadGrid ID="rg_consulta" runat="server" Width="100%" AutoGenerateColumns="False" AllowFilteringByColumn="True" ShowGroupPanel="True" OnDetailTableDataBind="rg_consulta_OnDetailTableDataBind"
            Culture="(Default)" ShowFooter="True" DataSourceID="obj_cartera" Height="650px" GroupPanelPosition="Top" OnItemCommand="rg_consulta_ItemCommand" >
            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                        <Scrolling AllowScroll="True" SaveScrollPosition="true">
                        </Scrolling>
                    </ClientSettings>
            <MasterTableView ShowGroupFooter="true"  CommandItemDisplay="Top" DataKeyNames="HDNROFAC,HDTIPFAC">
                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                <DetailTables>
                    <telerik:GridTableView Name="detalle_item" Width="100%">
                        <Columns>                            
                            <telerik:GridTemplateColumn DataField="RC_NRORECIBO" HeaderText="Propietario" UniqueName="RC_NRORECIBO_TK"
                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="RC_NRORECIBO" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_recibo" runat="server" Text='<%# Eval("RC_NRORECIBO") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn CommandName="link_rc" Text="RC_NRORECIBO" UniqueName="RC_NRORECIBO" DataTextField="RC_NRORECIBO"
                                                        HeaderText="Nro Recibo" HeaderStyle-Width="100px">
                                                    </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn SortExpression="RC_FECREC" HeaderText="F. Recibo" HeaderButtonType="TextButton" AllowFiltering="false" DataFormatString="{0:MM/dd/yyyy}"
                                DataField="RC_FECREC">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn SortExpression="TOT" HeaderText="Valor" HeaderButtonType="TextButton" AllowFiltering="false" DataFormatString="{0:C}"
                                DataField="TOT">
                            </telerik:GridBoundColumn>                            
                        </Columns>
                        <NoRecordsTemplate>
                            <div class="alert alert-danger">
                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                <strong>Alerta!</strong>No Existen Registros
                            </div>
                        </NoRecordsTemplate>
                    </telerik:GridTableView>
                </DetailTables>
                <Columns>                    
                    <telerik:GridTemplateColumn DataField="TRCODNIT" HeaderText="Propietario" UniqueName="TRCODNIT_TK"
                        HeaderStyle-Width="150px" AllowFiltering="false" SortExpression="TRCODNIT" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_tercero" runat="server" Text='<%# Eval("TRCODNIT") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link_tr" Text="TRCODNIT" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                        HeaderText="Identificacion" HeaderStyle-Width="150px">
                    </telerik:GridButtonColumn>
                   <telerik:GridBoundColumn DataField="CLIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="400px"
                        HeaderText="Cliente" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLIENTE"
                        UniqueName="CLIENTE">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VENDEDOR" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                        HeaderText="Vendedor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="VENDEDOR"
                        UniqueName="VENDEDOR">                        
                    </telerik:GridBoundColumn>                  
                    <telerik:GridTemplateColumn DataField="DOCUEMNTO_CON" HeaderText="Propietario" UniqueName="NRO_FAC_TK"
                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DOCUEMNTO_CON" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("NRO_FAC") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridButtonColumn CommandName="link" Text="NRO_FAC" UniqueName="NRO_FAC" DataTextField="NRO_FAC"
                        HeaderText="Nro Factura" HeaderStyle-Width="150px">
                    </telerik:GridButtonColumn>
                    <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Fac" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC"
                        UniqueName="HDFECFAC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="F_VENCIMIENTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="F. Ven" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="F_VENCIMIENTO"
                        UniqueName="F_VENCIMIENTO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="T. Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                        UniqueName="TTDESCRI">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TTDESCRI1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="D. Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI1"
                        UniqueName="TTDESCRI1">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DIAS" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                        HeaderText="D. Ven" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DIAS"
                        UniqueName="DIAS">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VLR_CAR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr Cartera" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="VLR_CAR" UniqueName="VLR_CAR">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="RECAUDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr RC" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="RECAUDO" UniqueName="RECAUDO">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                
                    <%--<telerik:GridBoundColumn DataField="TNC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr NC" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="TNC" UniqueName="TNC">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn> --%>     
                    <telerik:GridButtonColumn CommandName="lkn_nc" Text="TNC" UniqueName="TNC" DataTextField="TNC"
                        HeaderText="Vlr NC" HeaderStyle-Width="100px"  ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>

                    <telerik:GridButtonColumn CommandName="lkn_nd" Text="TND" UniqueName="TND" DataTextField="TND"
                        HeaderText="Vlr ND" HeaderStyle-Width="100px"  ItemStyle-HorizontalAlign="Right" DataTextFormatString="{0:C}">
                    </telerik:GridButtonColumn>

                    <%--<telerik:GridBoundColumn DataField="TND" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr ND" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="TND" UniqueName="TND">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>                --%>
                    <telerik:GridBoundColumn DataField="TDEV" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Vlr Dev" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="TDEV" UniqueName="TDEV">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SAL_FAVOR_APL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Saldo Favor x Aplicar" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="SAL_FAVOR_APL" UniqueName="SAL_FAVOR_APL">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RECA_SF" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Saldo Favor Aplicado" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="RECA_SF" UniqueName="RECA_SF">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SALDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                         HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" Aggregate="Sum"
                        Resizable="true" SortExpression="SALDO" UniqueName="SALDO">
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

    <asp:ObjectDataSource ID="obj_cartera" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetCartera" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />      
            <asp:Parameter Name="inFecha" Type="DateTime" DefaultValue="01/01/2000" />
            <asp:Parameter Name="inFiltro" Type="String" DefaultValue=" AND 1=0"/>           
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
