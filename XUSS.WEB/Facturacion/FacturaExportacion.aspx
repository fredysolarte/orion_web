<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="FacturaExportacion.aspx.cs" Inherits="XUSS.WEB.Facturacion.FacturaExportacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_factura" runat="server" PageSize="1" AllowPaging="True" OnItemInserting="rlv_factura_OnItemInserting"
            OnItemCommand="rlv_factura_OnItemCommand" OnItemDataBound="rlv_factura_OnItemDataBound" OnItemInserted="rlv_factura_OnItemInserted"
            DataSourceID="obj_factura" ItemPlaceholderID="pnlGeneral" DataSourceCount="0"
            RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Facturacion Exportacion</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_factura" PageSize="1" RenderMode="Lightweight">
                            <Fields>
                                <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                                <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                <telerik:RadDataPagerTemplatePageField HorizontalPosition="RightFloat">
                                    <PagerTemplate>
                                        <div style="float: right">
                                            <b>Items
                                                <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.Owner.StartRowIndex+1%>" />
                                                de
                                                <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.TotalRowCount%>" />
                                                <br />
                                            </b>
                                        </div>
                                    </PagerTemplate>
                                </telerik:RadDataPagerTemplatePageField>
                            </Fields>
                        </telerik:RadDataPager>
                    </div>
                    <asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px">
                    </asp:Panel>
                </fieldset>
            </LayoutTemplate>
            <EmptyDataTemplate>
                <fieldset class="cssFieldSetContainer">
                    <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_filtro">
                        <div class="box">
                            <div class="title">
                                <h5>Facturacion Exportacion</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>T. Factura</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px"
                                        Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE"
                                        DataValueField="TFTIPFAC">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Factura</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofactura" runat="server" Enabled="true" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Lista</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_lista" runat="server" Enabled="true" Width="250px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Literal ID="litEmptyMessage" runat="server"></asp:Literal>
                </fieldset>
            </EmptyDataTemplate>
            <ItemTemplate>
                <div runat="server" id="BotonesBarra" class="toolBarsMenu">
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" ToolTip="Anular Registro" OnClick="btn_eliminar_OnClick" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                F Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("HDFECFAC") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <label>T Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO"
                                                Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("HDTIPFAC") %>'
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>
                                                Nro Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="false" Text='<%# Eval("HDNROFAC") %>' Width="80px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>
                                                Lta Empaque</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_lta" runat="server" Enabled="false" Text='<%# Bind("LH_LSTPAQ") %>' Width="80px" Visible="false">
                                            </telerik:RadTextBox>
                                            <asp:LinkButton ID="lnk_lstempaque" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Bind("LH_LSTPAQ") %>' OnClick="lnk_lstempaque_Click" />
                                        </td>
                                        <td>
                                            <label>
                                                Pedido</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Width="80px" Visible="false">
                                            </telerik:RadTextBox>
                                            <asp:LinkButton ID="lnk_pedido" CommandName="Cancel" CausesValidation="true" runat="server" Text='<%# Eval("PEDIDO") %>' OnClick="lnk_pedido_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <fieldset>
                                    <%--<legend>-</legend>--%>
                                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                                        SelectedIndex="0" CssClass="tabStrip">
                                        <Tabs>
                                            <telerik:RadTab Text="Datos Tercero">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Datos Comerciales">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                                        <telerik:RadPageView ID="pv_dtercero" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Identificacion</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false" Text='<%# Bind("HDCODNIT") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                        <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Visible="false" Text='<%# Bind("HDCODCLI") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Nombre</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Eval("TRNOMBRE") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Apellido</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_apellido" runat="server" Enabled="false" Text='<%# Eval("TRAPELLI") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Direccion</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="false" Text='<%# Eval("TRDIRECC") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Telefono</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Eval("TRNROTEL") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Email</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_email" runat="server" Enabled="false" Text='<%# Eval("TRCORREO") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="pv_dcomerciales" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Pais</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                                            DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("HDCDPAIS") %>'
                                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <label>
                                                            Ciudad</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px"
                                                            AllowCustomText="true" Filter="Contains" Enabled="false" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Moneda Principal</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                                            Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("HDMONEDA") %>'
                                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Observaciones</label>
                                                    </td>
                                                    <td colspan="4">
                                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("HDOBSERV") %>'
                                                            Width="600px" TextMode="MultiLine" Height="40px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>Vendedor</label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="false" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_agente" InitialValue="Seleccionar"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <label>Estado</label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("HDESTADO") %>'
                                                    Enabled="false" Width="300px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                                        <telerik:RadComboBoxItem Text="Facturado" Value="FA" />
                                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 30%">
                                <fieldset>
                                    <legend>Totales</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>SUBTOTAL</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_subtotal" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDSUBTOT") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>IMPUESTO</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_impuesto" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDTOTIVA") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>TOTAL</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_total" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDTOTFAC") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage2"
                        SelectedIndex="1" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Items Facturacion" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Adicionales">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_items" runat="server">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                                <telerik:RadPane ID="LeftPane" runat="server" Width="70%">
                                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="100%"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="DTNROITM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROITM"
                                                    UniqueName="DTNROITM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTCLAVE1"
                                                    UniqueName="DTCLAVE1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                    HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTCANTID" UniqueName="DTCANTID" FooterText="Total: "
                                                    Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataType="System.Decimal"
                                                    DataFormatString="{0:C}" HeaderText="P Lista" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTPRELIS" UniqueName="DTPRELIS">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTDESCUE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    DataFormatString="{0:0.#}" HeaderText="Dcto" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTDESCUE" UniqueName="DTDESCUE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="P Vta" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTPRECIO" UniqueName="DTPRECIO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTSUBTOT" UniqueName="DTSUBTOT" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTTOTIVA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTTOTIVA" UniqueName="DTTOTIVA" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="T Factura" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTTOTFAC" UniqueName="DTTOTFAC" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                    <strong>Alerta!</strong>No Tiene Registros
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane1" runat="server" Width="30%">
                                    <telerik:RadGrid ID="rg_pagos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                        <MasterTableView ShowGroupFooter="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="PAGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="T Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PAGO"
                                                    UniqueName="PAGO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DETALLE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Detalle" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DETALLE"
                                                    UniqueName="DETALLE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PGVLRPAG" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    DataFormatString="{0:C}" HeaderText="Vlr Pago" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG" >                                                    
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                    <strong>Alerta!</strong>No Tiene Registros
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_adicionales" runat="server">
                            <telerik:RadGrid ID="rg_adicionales" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <MasterTableView ShowGroupFooter="true">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AFCONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Cod" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PAGO"
                                            UniqueName="AFCONCEPTO">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="180px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONCEPTO"
                                            UniqueName="CONCEPTO">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AFVALOR" HeaderButtonType="TextButton" HeaderStyle-Width="180px"
                                            DataFormatString="{0:C}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="AFVALOR" UniqueName="AFVALOR" Aggregate="Sum">                                            
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                            <strong>Alerta!</strong>No Tiene Registros
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                F Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("HDFECFAC") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <label>T Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO"
                                                Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("HDTIPFAC") %>'
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tipfac" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Nro Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="false" Text='<%# Eval("HDNROFAC") %>' Width="80px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>
                                                Lta Empaque</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_lta" runat="server" Enabled="false" Text='<%# Bind("LH_LSTPAQ") %>' Width="80px">
                                            </telerik:RadTextBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" OnClick="btn_buscar_OnClick" runat="server" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Buscar Lta Empaque" />
                                        </td>
                                        <td>
                                            <label>
                                                Pedido</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_pedido" runat="server" Enabled="false" Width="80px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <fieldset>
                                    <%--<legend>-</legend>--%>
                                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                                        SelectedIndex="0" CssClass="tabStrip">
                                        <Tabs>
                                            <telerik:RadTab Text="Datos Tercero">
                                            </telerik:RadTab>
                                            <telerik:RadTab Text="Datos Comerciales">
                                            </telerik:RadTab>
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                                        <telerik:RadPageView ID="pv_dtercero" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Identificacion</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false" Text='<%# Bind("HDCODNIT") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                        <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Visible="false" Text='<%# Bind("HDCODCLI") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Nombre</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Eval("TRNOMBRE") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Apellido</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_apellido" runat="server" Enabled="false" Text='<%# Eval("TRAPELLI") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Direccion</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="false" Text='<%# Eval("TRDIRECC") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Telefono</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Eval("TRNROTEL") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Email</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_email" runat="server" Enabled="false" Text='<%# Eval("TRCORREO") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>F. Nacimiento</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txt_fnacimiento" runat="server" DbSelectedDate='<%# Bind("TRFECNAC") %>'
                                                            MinDate="01/01/1900" Enabled="true">
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="pv_dcomerciales" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Pais</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                            DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("HDCDPAIS") %>'
                                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td colspan="2">
                                                        <label>
                                                            Ciudad</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px"
                                                            AllowCustomText="true" Filter="Contains" Enabled="true" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Moneda Principal</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                                            Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("HDMONEDA") %>'
                                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Observaciones</label>
                                                    </td>
                                                    <td colspan="4">
                                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("HDOBSERV") %>'
                                                            Width="600px" TextMode="MultiLine" Height="40px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>Vendedor</label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="true" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_agente" InitialValue="Seleccionar"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 30%">
                                <fieldset>
                                    <legend>Totales</legend>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>SUBTOTAL</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_subtotal" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDSUBTOT") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>IMPUESTO</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_impuesto" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDTOTIVA") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    <h5>TOTAL</h5>
                                                </label>
                                            </td>
                                            <td>
                                                <telerik:RadNumericTextBox runat="server" ID="txt_total" Width="250px" Font-Size="X-Large" Enabled="false"
                                                    DbValue='<%# Bind("HDTOTFAC") %>' BorderColor="Transparent" EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage2"
                        SelectedIndex="1" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Items Facturacion" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Adicionales">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_items" runat="server">
                            <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                                <telerik:RadPane ID="LeftPane" runat="server" Width="70%">
                                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" Height="100%"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="DTNROITM" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                    HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROITM"
                                                    UniqueName="DTNROITM">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                    HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTCLAVE1"
                                                    UniqueName="DTCLAVE1">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                    HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                    UniqueName="ARNOMBRE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTCANTID" UniqueName="DTCANTID" FooterText="Total: "
                                                    Aggregate="Sum">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTPRELIS" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataType="System.Decimal"
                                                    DataFormatString="{0:C}" HeaderText="P Lista" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTPRELIS" UniqueName="DTPRELIS">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTDESCUE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    DataFormatString="{0:0.#}" HeaderText="Dcto" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTDESCUE" UniqueName="DTDESCUE">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="P Vta" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTPRECIO" UniqueName="DTPRECIO">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTSUBTOT" UniqueName="DTSUBTOT" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTTOTIVA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTTOTIVA" UniqueName="DTTOTIVA" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DTTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    DataFormatString="{0:C}" HeaderText="T Factura" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="DTTOTFAC" UniqueName="DTTOTFAC" Aggregate="Sum">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                    <strong>Alerta!</strong>No Tiene Registros
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                                <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
                                </telerik:RadSplitBar>
                                <telerik:RadPane ID="RadPane1" runat="server" Width="30%">
                                    <telerik:RadGrid ID="rg_pagos" runat="server" GridLines="None" Width="100%" OnDeleteCommand="rg_pagos_OnDeleteCommand"
                                        AutoGenerateColumns="False" OnItemCommand="rg_pagos_OnItemCommand"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True"
                                        OnNeedDataSource="rg_pagos_OnNeedDataSource">
                                        <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PGNROITM">
                                            <CommandItemSettings AddNewRecordText="Nuevo Pago" ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                                <telerik:GridBoundColumn DataField="PAGO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="T Pago" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PAGO"
                                                    UniqueName="PAGO">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DETALLE" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    HeaderText="Detalle" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DETALLE"
                                                    UniqueName="DETALLE">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PGVLRPAG" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                    DataFormatString="{0:C}" HeaderText="Vlr Pago" ItemStyle-HorizontalAlign="Right"
                                                    Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                    <strong>Alerta!</strong>No Tiene Registros
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPane>
                            </telerik:RadSplitter>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_adicionales" runat="server">
                            <telerik:RadGrid ID="rg_adicionales" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_adicionales_NeedDataSource">
                                <MasterTableView ShowGroupFooter="true">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AFCONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                            HeaderText="Cod" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="AFCONCEPTO"
                                            UniqueName="AFCONCEPTO">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridTemplateColumn HeaderText="afconcep" UniqueName="ckey" HeaderStyle-Width="300px" Visible="false">
                                            <ItemTemplate>
                                            <telerik:RadTextBox runat="server" ID="txt_concep" Width="250px" Text='<%# Eval("AFCONCEPTO") %>' EnabledStyle-HorizontalAlign="Right" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn DataField="CONCEPTO" HeaderButtonType="TextButton" HeaderStyle-Width="180px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CONCEPTO"
                                            UniqueName="CONCEPTO">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="AFVALOR" HeaderButtonType="TextButton" HeaderStyle-Width="180px"
                                            DataFormatString="{0:C}" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="AFVALOR" UniqueName="AFVALOR">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="afvalor" UniqueName="ckey" HeaderStyle-Width="300px">
                                            <ItemTemplate>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_values" Width="250px"  EnabledStyle-HorizontalAlign="Right" OnTextChanged="txt_values_TextChanged" AutoPostBack="true">
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
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpListaEmpaque" runat="server" Width="820px" Height="350px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Buscar Lista Empaque">
                    <ContentTemplate>
                        <div style="padding: 5px 5px 5px 5px">
                            <table>
                                <tr>
                                    <td>
                                        <label>Numero</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_numero" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Tercero</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="edt_tercero" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>                                        
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroLtsEmpaque" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroLtsEmpaque_OnClick" CommandName="Cancel" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel1" runat="server">
                                <telerik:RadGrid ID="rgLtaEmpaque" runat="server" AllowSorting="True" Width="100%"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
                                    DataSourceID="obj_ltaEmpaque" OnItemCommand="rgLtaEmpaque_OnItemCommand">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn Resizable="true" SortExpression="LH_LSTPAQ" HeaderText="Lta Empaque"
                                                UniqueName="LH_LSTPAQ" HeaderButtonType="TextButton" DataField="LH_LSTPAQ" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="90px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Resizable="true" SortExpression="LH_PEDIDO" HeaderText="Pedido"
                                                UniqueName="LH_PEDIDO" HeaderButtonType="TextButton" DataField="LH_PEDIDO" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="90px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Resizable="true" SortExpression="NOM_TER" HeaderText="Tercero"
                                                UniqueName="NOM_TER" HeaderButtonType="TextButton" DataField="NOM_TER" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="3500px">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPagos" runat="server" Width="720px" Height="250px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Pagos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>T. Pago</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" DataSourceID="obj_tpago" DataTextField="TTDESCRI" AutoPostBack="true" ZIndex="1000000"
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="300px" AllowCustomText="true" Filter="Contains" OnSelectedIndexChanged="rc_tpago_OnSelectedIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tpago" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td rowspan="3">
                                    <label>Saldo</label>
                                    <telerik:RadNumericTextBox ID="txt_saldo" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>D. Pago</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_dpago" runat="server" Culture="es-CO" Enabled="false" ZIndex="1000000"
                                        AppendDataBoundItems="true" Width="300px" AllowCustomText="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rv_dpago" runat="server" ControlToValidate="rc_dpago" InitialValue="Seleccionar" Enabled="false"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Vlr. Pago</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_valor" runat="server">
                                    </telerik:RadNumericTextBox>
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="(*)" ValidationGroup="gvPagos"
                                        Type="Double" ControlToCompare="txt_valor" CultureInvariantValues="true" Display="Dynamic"
                                        EnableClientScript="true" SetFocusOnError="true" ControlToValidate="txt_saldo"
                                        Operator="GreaterThanEqual">
                                        <asp:Image ID="Image29" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_valor" InitialValue="0" Enabled="true"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>

                        </table>
                        <div style="text-align: center;">
                            <%--<asp:Button ID="btn_aceptarpago" runat="server" Text="Aceptar" OnClick="btn_aceptarpago_OnClick" ValidationGroup="gvPagos" />                       --%>
                            <telerik:RadButton ID="btn_aceptarpago" runat="server" Text="Aceptar" ValidationGroup="gvPagos" Icon-PrimaryIconCssClass="rbOk" ToolTip="Guardar" RenderMode="Lightweight" OnClick="btn_aceptarpago_OnClick" />
                        </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalConfirmacion" runat="server" Width="700px" Height="220px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Confirmar">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Causal</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_causae" runat="server" Culture="es-CO" AppendDataBoundItems="true" Width="300px" AllowCustomText="true" Filter="Contains" ZIndex="1000000">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_causae" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvAnular">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Incluir Lista Empaque?</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chk_inltaemp" Checked="false" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center;">
                            <%--<asp:Button ID="btn_acpetar_anular" runat="server" Text="Aceptar" OnClick="btn_acpetar_anular_OnClick" ValidationGroup="gvAnular" />--%>
                            <%--<asp:Button ID="Button7" runat="server" Text="Cerrar" />--%>
                            <telerik:RadButton ID="btn_acpetar_anular" runat="server" Text="Aceptar" ValidationGroup="gvAnular" Icon-PrimaryIconCssClass="rbOk" ToolTip="Guardar" RenderMode="Lightweight" OnClick="btn_acpetar_anular_OnClick" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_factura" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertFaturacion" OnInserted="obj_factura_OnInserted"
        SelectMethod="GetFacturaHD" TypeName="XUSS.BLL.Facturacion.FacturacionBL" OnInserting="obj_factura_OnInserting">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="HDCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="HDTIPFAC" Type="String" />
            <%--<asp:Parameter Name="HDNROFAC" Type="Int32" DefaultValue="0"/>--%>
            <asp:Parameter Name="HDFECFAC" Type="DateTime" />
            <asp:Parameter Name="HDCODCLI" Type="Int32" />
            <asp:Parameter Name="HDCODSUC" Type="Int32" />
            <asp:Parameter Name="HDFECVEN" Type="DateTime" />
            <asp:Parameter Name="HDSUBTOT" Type="Double" />
            <asp:Parameter Name="HDTOTDES" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTIVA" Type="Double" />
            <asp:Parameter Name="HDTOTFAC" Type="Double" />
            <asp:Parameter Name="HDMONEDA" Type="String" DefaultValue="0" />
            <asp:Parameter Name="HDSUBTTL" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTDSL" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTIVL" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTFCL" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDSUBTTD" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTDSD" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTIVD" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTFCD" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDCODNIT" Type="String" />
            <asp:Parameter Name="HDCDPAIS" Type="String" />
            <asp:Parameter Name="HDCIUDAD" Type="String" />
            <asp:Parameter Name="HDMODDES" Type="String" />
            <asp:Parameter Name="HDTERDES" Type="String" />
            <asp:Parameter Name="HDTERPAG" Type="String" />
            <asp:Parameter Name="HDAGENTE" Type="String" />
            <asp:Parameter Name="HDRSDIAN" Type="String" />
            <asp:Parameter Name="HDCATEGO" Type="String" />
            <asp:Parameter Name="HDCAJCOM" Type="String" />
            <asp:Parameter Name="HDNROALJ" Type="String" />
            <asp:Parameter Name="HDTIPALJ" Type="String" />
            <asp:Parameter Name="HDDIVISI" Type="String" />
            <asp:Parameter Name="HDTOTOTR" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTSEG" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTFLE" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDCNTFIS" Type="String" />
            <asp:Parameter Name="HDOBSERV" Type="String" />
            <asp:Parameter Name="HDTOTICA" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTFTE" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDTOTFIV" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="HDESTADO" Type="String" DefaultValue="CE" />
            <asp:Parameter Name="HDCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="HDNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="HDTRASMI" Type="String" />
            <asp:Parameter Name="HDFECCIE" Type="DateTime" />
            <asp:Parameter Name="HDTIPDEV" Type="String" />
            <asp:Parameter Name="HDNRODEV" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="HDTFCDEV" Type="String" />
            <asp:Parameter Name="HDFACDEV" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="HDNROCAJA" Type="String" />
            <asp:Parameter Name="LH_LSTPAQ" Type="String" />
            <asp:Parameter Name="tbDetalle" Type="Object" />
            <asp:Parameter Name="tbPagos" Type="Object" />
            <asp:Parameter Name="ind_inv" Type="String" DefaultValue="N" />
            <asp:Parameter Name="ind_dev" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="ind_bon" Type="String" DefaultValue="N" />
            <asp:Parameter Name="tbBalance" Type="Object" />
            <asp:Parameter Name="tbAdicionales" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (1)" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_ltaEmpaque" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLtaEmpaque" TypeName="XUSS.BLL.Pedidos.LtaEmpaqueBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tpago" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAGO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_pais" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAIS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_moneda" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MONE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (1)" Name="filter" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
