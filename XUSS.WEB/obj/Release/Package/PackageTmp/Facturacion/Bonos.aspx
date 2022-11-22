<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Bonos.aspx.cs" Inherits="XUSS.WEB.Facturacion.Bonos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>    
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
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">

        <script type="text/javascript">
            function txt_valor_ClientEvents_OnValueChanged(sender, eventArgs) {
                //debugger;
                var eValor = eventArgs.get_newValue();
                var eSaldo = $find("<%= txt_saldo.ClientID %>");
                $("#<%= txt_devolucion.ClientID %>").val(0);
                if (eValor > eSaldo.get_value())
                    $("#<%= txt_devolucion.ClientID %>").val(Math.abs(eValor - eSaldo.get_value()));

            }
            function rc_tpago_OnClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                ValidatorEnable(document.getElementById("<%=cv_vlrtotal.ClientID %>"), true);
                if (item.get_value() == "01")
                    ValidatorEnable(document.getElementById("<%=cv_vlrtotal.ClientID %>"), false);
                PageMethods.GetDetallePagos(item.get_value(), onSucess, onError);
                function onSucess(response) {
                    //alert('entro');
                    //comboBox.disable();                    
                    var comboBox = $find("<%=rc_dpago.ClientID %>");
                    comboBox.clearItems();
                    var comboItem_ = new Telerik.Web.UI.RadComboBoxItem();
                    comboItem_.set_text("Seleccionar");
                    comboItem_.set_value("");
                    comboBox.get_items().add(comboItem_);
                    comboBox.findItemByText("Seleccionar").select();
                    comboBox.disable();
                    ValidatorEnable(document.getElementById("<%=rv_dpago.ClientID %>"), false);
                    ValidatorEnable(document.getElementById("<%=rv_soporte.ClientID %>"), false);
                    comboBox.trackChanges();
                    for (var i in response) {
                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text(response[i].TTDESCRI);
                        comboItem.set_value(response[i].TTCODCLA);
                        comboBox.get_items().add(comboItem);
                        comboBox.enable();
                        ValidatorEnable(document.getElementById("<%=rv_dpago.ClientID %>"), true);
                        ValidatorEnable(document.getElementById("<%=rv_soporte.ClientID %>"), true);
                    }
                    comboBox.commitChanges();
                }
                function onError() {
                    alert('error');
                }

            }

            document.onkeypress = function (e) {
                var esIE = (document.all);
                var esNS = (document.layers);
                var boolInsert = $("#<%= isClickInsertxt.ClientID %>").val();
                tecla = (esIE) ? event.keyCode : e.which;
                //console.log(tecla);
                //console.log(boolInsert);
                //alert(boolInsert);
                //if (tecla == 13) {
                //    alert("Ud. ha presionado la tecla Enter"); return false;
                if (tecla == 17) {
                    if (Page_ClientValidate("gvInsert") && boolInsert == "true") {
                        var oWnd = $find("<%=modalPopup.ClientID%>");
                        oWnd.setUrl('FacturaDirecta.aspx');
                        oWnd.show();
                        $("#<%= txt_barras.ClientID %>").val("");
                        var txtControl = document.getElementById('<%= txt_barras.ClientID %>');
                        txtControl.focus();
                    }
                }

                if (tecla == 9) {
                    if (Page_ClientValidate("gvInsert") && boolInsert == "true") {
                        var oWnd = $find("<%=modalPagos.ClientID%>");
                        oWnd.setUrl('FacturaDirecta.aspx');
                        oWnd.show();
                        var comboBox = $find("<%=rc_tpago.ClientID %>");
                        var input = comboBox.get_inputDomElement();
                        input.focus();
                        //calcular saldo
                        var eSaldo = 0;
                        var eItem = $find('<%= rlv_factura.ClientID %>');
                        var grid = $telerik.findControl(eItem.get_element().parentNode, "rg_pagos").get_masterTableView().get_dataItems();
                        //alert(grid.length);                        
                        for (var i = 0; i < grid.length; i++) {
                            //console.log(grid[i].findControl("txt_valor").get_value());
                            eSaldo = parseFloat(eSaldo) + parseFloat(grid[i].findControl("txt_valor").get_value());
                        }
                        eSaldo = parseFloat(eSaldo) - parseFloat($telerik.findControl(eItem.get_element().parentNode, "txt_total").get_value());
                        $("#<%= txt_saldo.ClientID %>").val(Math.abs(eSaldo));
                        $("#<%= txt_valor.ClientID %>").val(Math.abs(eSaldo));
                    }
                }
            }

            function rc_agente_OnClientSelectedIndexChanged(sender, args) {
                if (Page_ClientValidate("gvInsert")) {
                    var oWnd = $find("<%=modalPopup.ClientID%>");
                    oWnd.setUrl('FacturaDirecta.aspx');
                    oWnd.show();
                    var txtControl = document.getElementById('<%= txt_barras.ClientID %>');
                    txtControl.focus();
                }
            }
            function modalPopup_OnClientClose(sender, eventArgs) {
                //console.log(eventArgs.get_eventTarget());
                var window = $find('<%=modalPagos.ClientID %>');
                //window.show();
            }
            function modalPopup_OnClientCommand(sender, eventArgs) {
                //console.log(args.get_eventTarget());
                $find('<%=modalPagos.ClientID %>').show();
            }
            function changeCase(sender) {
                sender.value = sender.value.toUpperCase();
            }
            //function Clicking(sender, args) {
            //    if (!confirm("¿Esta Seguro de Anular el Registro?"))
            //        args.set_cancel(!confirmed);
            //}
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000" EnablePageMethods="true">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_factura" runat="server" PageSize="1" AllowPaging="True" OnItemInserting="rlv_factura_OnItemInserting"
            OnItemCommand="rlv_factura_OnItemCommand" OnItemDataBound="rlv_factura_OnItemDataBound" OnItemInserted="rlv_factura_OnItemInserted"
            DataSourceID="obj_factura" ItemPlaceholderID="pnlGeneral" DataSourceCount="0"
            OnPreRender="rlv_factura_PreRender">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Bonos</h5>
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
                                <h5>Bonos</h5>
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
                                        Apellidos</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_apellidos" runat="server" Enabled="true" Width="250px">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Cancel" ToolTip="Anular Registro" OnClick="btn_eliminar_OnClick" />
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
                                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="false" Text='<%# Eval("HDNROFAC") %>' Width="60px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <%--<label>
                                                Fac Dev</label>--%>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_faOcdev" runat="server" Enabled="false" Text='<%# Bind("HDNRODEV") %>' Width="40px" Visible="false">
                                            </telerik:RadTextBox>
                                            <telerik:RadTextBox ID="txt_facdev" runat="server" Enabled="false" Text='<%# Bind("HDFACDEV") %>' Width="40px" Visible="false">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>
                                                Devolucion</label>
                                        </td>
                                        <td>
                                            <%--<telerik:RadComboBox ID="rc_facdev" runat="server" Culture="es-CO" Width="250px"  Visible="false"
                                                Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("HDTIPDEV") %>'
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true" >
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>--%>
                                            <telerik:RadTextBox ID="txt_tfdev" runat="server" Enabled="false" Text='<%# Bind("HDTFCDEV") %>' Width="40px" Visible="false">
                                            </telerik:RadTextBox>
                                            <asp:LinkButton ID="lnk_devolucion" CommandName="Cancel" CausesValidation="true" runat="server" Text="xxx" OnClick="lnk_devolucion_Click" />
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
                                                <telerik:RadDatePicker ID="edt_feccie" runat="server" DbSelectedDate='<%# Bind("HDFECCIE") %>'
                                                    MinDate="01/01/1900" Enabled="false" Visible="false">
                                                </telerik:RadDatePicker>
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
                                        <telerik:GridBoundColumn DataField="C2" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2"
                                            UniqueName="C2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C3" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3"
                                            UniqueName="C3">
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
                                            Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
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
                                            <label>T Fac</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="250px" OnSelectedIndexChanged="rc_tipfac_OnSelectedIndexChanged" AutoPostBack="true"
                                                Enabled="true" DataSourceID="obj_tfxusuario" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("HDTIPFAC") %>'
                                                DataValueField="TFTIPFAC">
                                                <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="rqf_tipfac" runat="server" ControlToValidate="rc_tipfac" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Nro Factura</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="false" Text='<%# Eval("HDNROFAC") %>' Width="60px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>
                                                Fac Dev</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_facdev" runat="server" Enabled="false" Text='<%# Bind("HDNRODEV") %>' Width="40px">
                                            </telerik:RadTextBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar_fac" OnClick="btn_buscar_fac_OnClick" runat="server" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Buscar Factura" />
                                        </td>
                                        <td>
                                            <label>
                                                T F. Dev</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_facdev" runat="server" Culture="es-CO" Width="250px"
                                                Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("HDTIPDEV") %>'
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>

                                        <td>
                                            <label>
                                                Nro Sep</label>
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_separado" runat="server" Enabled="false" Width="40px">
                                            </telerik:RadTextBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar_sep" OnClick="btn_buscar_sep_OnClick" runat="server" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Buscar Factura" />
                                        </td>
                                        <td>
                                            <label>
                                                T. Sep</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_separado" runat="server" Culture="es-CO" Width="250px"
                                                Enabled="false" DataSourceID="obj_separado" DataTextField="TFNOMBRE"
                                                DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                </Items>
                                            </telerik:RadComboBox>
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
                                                        <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Text='<%# Bind("HDCODNIT") %>' OnTextChanged="txt_identificacion_OnTextChanged" AutoPostBack="true"
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="rqf_identificacion" runat="server" ControlToValidate="txt_identificacion"
                                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
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
                                                        <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TRNOMBRE") %>' onkeyup="changeCase(this)"
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="rqf_nombre" runat="server" ControlToValidate="txt_nombre"
                                                            ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Apellido</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_apellido" runat="server" Enabled="true" Text='<%# Bind("TRAPELLI") %>' onkeyup="changeCase(this)"
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
                                                        <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="true" Text='<%# Bind("TRDIRECC") %>'
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Telefono</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("TRNROTEL") %>'
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
                                                        <telerik:RadTextBox ID="txt_email" runat="server" Enabled="true" Text='<%# Bind("TRCORREO") %>'
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
                                                        <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="true" OnSelectedIndexChanged="rc_pais_OnSelectedIndexChanged" AutoPostBack="true"
                                                            DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("HDCDPAIS") %>'
                                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
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
                                                    <td>
                                                        <label>Lst Precio</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_lstprecio_OnSelectedIndexChanged" AutoPostBack="true"
                                                            Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE"
                                                            DataValueField="P_CLISPRE" AppendDataBoundItems="true">
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
                                                <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px" OnClientSelectedIndexChanged="rc_agente_OnClientSelectedIndexChanged"
                                                    Enabled="true" AppendDataBoundItems="true" AllowCustomText="true" Filter="Contains">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_agente" InitialValue="Seleccionar"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_agente"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                    <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
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
                    <telerik:RadSplitter RenderMode="Lightweight" ID="RadSplitter1" runat="server" Width="100%" Height="380px">
                        <telerik:RadPane ID="LeftPane" runat="server" Width="70%">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnDeleteCommand="rg_items_OnDeleteCommand" Height="100%"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" OnPreRender="rg_items_PreRender">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="DTNROITM">
                                    <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                            ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
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
                                        <telerik:GridBoundColumn DataField="C2" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C2"
                                            UniqueName="C2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="C3" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="C3"
                                            UniqueName="C3">
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
                                        <telerik:GridTemplateColumn DataField="DEV" HeaderStyle-Width="10px" HeaderText="" AllowFiltering="false"
                                            Resizable="true" SortExpression="DEV" UniqueName="DEV" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_devolucion" runat="server" Text='<%# Eval("DEV") %>' Enabled="false" Visible="false" />
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
                                        <%-- <telerik:GridBoundColumn DataField="PGVLRPAG" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                DataFormatString="{0:C}" HeaderText="Vlr Pago" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="PGVLRPAG" HeaderText="Vlr Pago" HeaderStyle-Width="80px"
                                            Resizable="true" SortExpression="PGVLRPAG" UniqueName="PGVLRPAG">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="false" DbValue='<%# Bind("PGVLRPAG") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
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
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
        <telerik:RadTextBox ID="isClickInsertxt" runat="server" Enabled="false" Visible="true" Style="display: none;">
        </telerik:RadTextBox>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="350px" Modal="true" OffsetElementID="main" Title="Detalle" OnClientClose="modalPopup_OnClientClose" OnClientCommand="modalPopup_OnClientCommand" EnableShadow="true">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>Cod Barras</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo" AutoPostBack="true" OnTextChanged="txt_barras_OnTextChanged">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>

                                <telerik:RadTextBox ID="txt_tp" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>
                                <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Visible="false">
                                </telerik:RadTextBox>

                                <td>
                                    <label>Referencia</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Visible="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_referencia" runat="server" ControlToValidate="txt_referencia"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>
                                        Producto</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Visible="true" Width="450px">
                                    </telerik:RadTextBox>
                                    <%----%>
                                    <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" Enabled="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C2</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc2r" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>C3</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nc3r" runat="server" Enabled="true" Visible="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C Inv</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_caninv" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <label>Precio Lta</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_preciolta" runat="server" Enabled="true">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_preciolta" runat="server" ControlToValidate="txt_preciolta"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image13" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rqf_preciolta0" runat="server" ControlToValidate="txt_preciolta"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                        <asp:Image ID="Image15" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadNumericTextBox ID="txt_impf" runat="server" Enabled="false" Visible="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <label>Dcto</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_dct" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lbl_lote" runat="server">Nro Lote</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="rc_lote" runat="server"
                                        DropDownWidth="315" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rc_lote"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                        <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Cantidad</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_cantidad" runat="server" Enabled="true" ValidationGroup="grNuevoI" CausesValidation="false">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="rqf_catidad1" runat="server" ControlToValidate="txt_cantidad"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <%--<asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txt_cantidad" ControlToCompare="txt_caninv" Operator="LessThanEqual" Type="Integer"
                                        ErrorMessage="(*) Inventario Menor al Valor de Salida" ValidationGroup="grNuevoI">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:CompareValidator>--%>
                                    <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_cantidad"
                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right;">
                            <%--<asp:Button ID="btn_agregar" runat="server" Text="Agregar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />    --%>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalConfirmacion" runat="server" Width="500px" Height="200px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Confirmacion">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Causal</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_causae" runat="server" Culture="es-CO" AppendDataBoundItems="true" Width="300px" AllowCustomText="true" Filter="Contains" ZIndex="1000000">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_causae" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvAnular">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center;">
                            <%--<asp:Button ID="btn_acpetar_anular" runat="server" Text="Aceptar" OnClick="btn_acpetar_anular_OnClick" ValidationGroup="gvAnular" />--%>
                            <telerik:RadButton ID="btn_acpetar_anular" runat="server" OnClick="btn_acpetar_anular_OnClick" Text="Aceptar" ValidationGroup="gvAnular" Icon-PrimaryIconCssClass="rbOk" ToolTip="Guardar" RenderMode="Lightweight" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPagos" runat="server" Width="700px" Height="300px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Forma de Pago">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>T. Pago</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" DataSourceID="obj_tpago" DataTextField="TTDESCRI" ZIndex="1000000"
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="300px" Filter="Contains" OnClientSelectedIndexChanged="rc_tpago_OnClientSelectedIndexChanged">
                                        <%--OnSelectedIndexChanged="rc_tpago_OnSelectedIndexChanged" AutoPostBack="true"--%>
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_tpago" InitialValue="Seleccionar"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Saldo</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_saldo" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>D. Pago</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_dpago" runat="server" Culture="es-CO" Enabled="false" ZIndex="1000000"
                                        AppendDataBoundItems="true" Width="300px" Filter="Contains">
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
                                    <label>Soporte</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_soporte" runat="server" Enabled="true" Visible="true" Width="300px">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rv_soporte" runat="server" ControlToValidate="txt_soporte" Enabled="false"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image14" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Vlr. Pago</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_valor" runat="server" ClientEvents-OnValueChanged="txt_valor_ClientEvents_OnValueChanged">
                                    </telerik:RadNumericTextBox>
                                    <asp:CompareValidator ID="cv_vlrtotal" runat="server" ErrorMessage="(*)" ValidationGroup="gvPagos"
                                        Type="Double" ControlToCompare="txt_valor" CultureInvariantValues="true" Display="Dynamic"
                                        EnableClientScript="true" SetFocusOnError="true" ControlToValidate="txt_saldo"
                                        Operator="GreaterThanEqual">
                                        <asp:Image ID="Image29" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_valor"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_valor" InitialValue="0"
                                        ErrorMessage="(*)" ValidationGroup="gvPagos">
                                        <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Cambio</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_devolucion" runat="server" Enabled="false">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center;">
                            <%--<asp:Button ID="btn_aceptarpago" runat="server" Text="Aceptar" OnClick="btn_aceptarpago_OnClick" ValidationGroup="gvPagos" />--%>
                            <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptarpago" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_aceptarpago_OnClick" ValidationGroup="gvPagos" />
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalLstEmpaque" runat="server" Width="900px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Forma de Pago">
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
                                        <asp:Button ID="btn_filtroLtsEmpaque" runat="server" Text="Filtrar" OnClick="btn_filtroLtsEmpaque_OnClick"
                                            CommandName="Cancel" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel13" runat="server">
                                <telerik:RadGrid ID="rgLtaEmpaque" runat="server" AllowSorting="True" Width="800px"
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
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </asp:Panel>
                            <div style="text-align: center;">
                                <asp:Button ID="Button1" runat="server" Text="Cerrar" />
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFactura" runat="server" Width="990px" Height="560px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Detalle Facturas">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>T. Factura</td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipfacdev" runat="server" Culture="es-CO" Width="250px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE"
                                        DataValueField="TFTIPFAC">
                                        <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                                    </telerik:RadComboBox>
                                </td>
                                <td>Nro Factura</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrofacdev" runat="server" Enabled="true" Width="80px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_filtro_fac_dev" runat="server" Text="Filtrar" OnClick="btn_filtro_fac_dev_OnClick"
                                        CommandName="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="rg_items_dev" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="DTNROITM">
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="30px"
                                        Resizable="true" UniqueName="check">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_item" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
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
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:Button ID="btn_aceptar_facdev" runat="server" Text="Agregar" OnClick="btn_aceptar_facdev_OnClick" CommandName="Cancel" Icon-PrimaryIconCssClass="rbAdd" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar_facdev" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="Cancel" OnClick="btn_aceptar_facdev_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalSeparado" runat="server" Width="990px" Height="560px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Detalle Facturas">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>T. Separado</td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipsep" runat="server" Culture="es-CO" Width="250px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_separado" DataTextField="TFNOMBRE"
                                        DataValueField="TFTIPFAC">
                                        <%--<Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>--%>
                                    </telerik:RadComboBox>
                                </td>
                                <td>Nro Separado</td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrosep" runat="server" Enabled="true" Width="80px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtro_sep" runat="server" Text="Filtrar" OnClick="btn_filtro_sep_OnClick" CommandName="Cancel" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtro_sep" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Cancel" OnClick="btn_filtro_sep_OnClick" />
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="rgDetalleSep" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView DataKeyNames="DTNROITM">
                                <Columns>
                                    <%--<telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="30px"
                                                Resizable="true" UniqueName="check">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_item" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn> --%>
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
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:Button ID="btn_aceptar_sep" runat="server" Text="Agregar" OnClick="btn_aceptar_sep_OnClick" CommandName="Cancel" Icon-PrimaryIconCssClass="rbAdd" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar_sep" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="Cancel" OnClick="btn_aceptar_sep_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFiltroArt" runat="server" Width="900px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Referencia</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_referencia" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nombreart" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroArticulos" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroArticulos_OnClick" CommandName="xxxxxx" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel2" runat="server">
                            <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="5" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_articulos" OnItemCommand="rgConsultaArticulos_OnItemCommand">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Sel" CommandName="Select">
                                            <HeaderStyle Width="20px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARTIPPRO" HeaderText="" Visible="true"
                                            UniqueName="ARTIPPRO" HeaderButtonType="None" DataField="ARTIPPRO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="0px" ItemStyle-Width="0px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE1" HeaderText="Referencia"
                                            UniqueName="ARCLAVE1" HeaderButtonType="TextButton" DataField="ARCLAVE1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn Resizable="true" SortExpression="ARCLAVE4" HeaderText="" Visible="true"
                                        UniqueName="ARCLAVE4" HeaderButtonType="None" DataField="ARCLAVE4" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="0px">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE2" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE2" UniqueName="ARCLAVE2">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave2" runat="server" Visible="false" Text='<%# Bind("ARCLAVE2") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn DataField="ARCLAVE3" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE3" UniqueName="ARCLAVE3">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave3" runat="server" Visible="false" Text='<%# Bind("ARCLAVE3") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="ARCLAVE4" HeaderText="" HeaderStyle-Width="80px" Visible="false"
                                            Resizable="true" SortExpression="ARCLAVE4" UniqueName="ARCLAVE4">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_fclave4" runat="server" Visible="false" Text='<%# Bind("ARCLAVE4") %>' BorderStyle="None"
                                                    MinValue="0" Value="0" Width="77px">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARNOMBRE" HeaderText="Nombre"
                                            UniqueName="ARNOMBRE" HeaderButtonType="TextButton" DataField="ARNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="400px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE2" HeaderText="" Visible="true"
                                            UniqueName="CLAVE2" HeaderButtonType="None" DataField="CLAVE2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="20px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="CLAVE3" HeaderText="" Visible="true"
                                            UniqueName="CLAVE3" HeaderButtonType="TextButton" DataField="CLAVE3"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC4" HeaderText=""
                                            UniqueName="ARDTTEC4" HeaderButtonType="TextButton" DataField="ARDTTEC4" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="ARDTTEC1" HeaderText=""
                                            UniqueName="ARDTTEC1" HeaderButtonType="TextButton" DataField="ARDTTEC1" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="100px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="PRECIO" HeaderText="Precio Lts" DataFormatString="{0:0.0}"
                                            UniqueName="PRECIO" HeaderButtonType="TextButton" DataField="PRECIO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="DESCUENTO" HeaderText="Dcto" DataFormatString="{0:0.#}"
                                            UniqueName="DESCUENTO" HeaderButtonType="TextButton" DataField="DESCUENTO" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="BBCANTID" HeaderText="Can" DataFormatString="{0:0.#}"
                                            UniqueName="BBCANTID" HeaderButtonType="TextButton" DataField="BBCANTID" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="25px">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
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
            <asp:Parameter Name="ind_inv" Type="String" DefaultValue="S" />
            <asp:Parameter Name="ind_dev" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRFECNAC" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="ind_bon" Type="String" DefaultValue="S" />
            <asp:Parameter Name="tbBalance" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tfxusuario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTFxUsuario" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (4)" Name="filter" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (4)" Name="filter" Type="String" />
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
    <asp:ObjectDataSource ID="obj_articulos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetArticulos" TypeName="XUSS.BLL.Pedidos.PedidosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" AND 1=0" Name="filter" Type="String" />
            <asp:Parameter Name="inBodega" Type="String" />
            <asp:Parameter Name="LT" Type="String" DefaultValue="NULL" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_lstprecio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetListaPrecioHD" TypeName="XUSS.BLL.ListaPrecios.ListaPreciosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_separado" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (3)" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
