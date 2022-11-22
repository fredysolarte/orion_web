<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="Terceros.aspx.cs" Inherits="XUSS.WEB.Terceros.Terceros" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">            
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
            function OnClientEntryAddingHandler(sender, eventArgs) {

                if (sender.get_entries().get_count() > 1) {
                    eventArgs.set_cancel(true);
                }

            }
            function conditionalPostback(sender, args) {
                if (args.EventTarget.indexOf("rg_otrosanexos") != -1 || args.EventTarget.indexOf("rg_infoacademica") != -1) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }
            function FireClick(sender, args) {
                //debugger;
               <%-- eventArgs.set_cancel(true);
                var lstview = $find("<%=rlv_imginstalacion.ClientID%>");
                lstview.fireCommand("Page","Next");
                var oWnd = $find("<%=mpDetalleHV.ClientID%>");                
                oWnd.show();--%>
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_terceros" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_terceros_ItemInserted"
            OnItemUpdating="rlv_terceros_OnItemUpdating" OnItemCommand="rlv_terceros_OnItemCommand"
            OnItemDataBound="rlv_terceros_OnItemDataBound" OnItemInserting="rlv_terceros_OnItemInserting"
            DataSourceID="obj_terceros" ItemPlaceholderID="pnlGeneral" DataKeyNames="TRCODTER"
            DataSourceCount="0" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Maestro Terceros</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_terceros" RenderMode="Lightweight"
                            PageSize="1">
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
                                <h5>Maestro Terceros</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />--%>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
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
                                        Codigo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nombres/Apellidos</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                Empleado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_empleado" runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Vendedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_vendedor" runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Cliente</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_cliente" runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Proveedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_proveedor" runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Forwarder</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_forwarder" runat="server" Enabled="true" />
                                        </td>
                                    </tr>
                                </table>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="1">
                        <tr>

                            <td rowspan="10">
                                <div style="border: 3px solid; text-align: center;">
                                    <telerik:RadBinaryImage runat="server" ID="txt_foto" AutoAdjustImageControlSize="false"
                                        Width="100px" Height="170px" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    T. Doc</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipdoc" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tipdoc" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPDOC") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false" Text='<%# Bind("TRCODNIT") %>'
                                    Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadNumericTextBox ID="txt_digchk" runat="server" Enabled="false" Text='<%# Bind("TRDIGCHK") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="47px">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>N. Comercial</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nomcomercial" runat="server" Enabled="false" Text='<%# Bind("TRNOMCOMERCIAL") %>' Width="600px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    P Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("TRNOMBRE") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    S Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_snombre" runat="server" Enabled="false" Text='<%# Bind("TRNOMBR2") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    P Apellido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_apellidos" runat="server" Enabled="false" Text='<%# Bind("TRAPELLI") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    S Apellido</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_sapellido" runat="server" Enabled="false" Text='<%# Bind("TRNOMBR3") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    F. Nacimiento</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TRFECNAC") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>
                                    Estado</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("TRESTADO") %>'
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Personales">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Comerciales">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Sucursales">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Familia + Referencia">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Info Academica">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Otros">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Contabilidad">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Nomina">
                            </telerik:RadTab>
                            <telerik:RadTab Text="P. Horizontal">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_dpersonales" runat="server">
                            <table>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            Telefono</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Bind("TRNROTEL") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            Celular</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_celular" runat="server" Enabled="false" Text='<%# Bind("TRNROFAX") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            Direccion</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="false" Text='<%# Bind("TRDIRECC") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            Direccion Entrega</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_direccentrega" runat="server" Enabled="false" Text='<%# Bind("TRDIREC2") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            Pais</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                            DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCDPAIS") %>'
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
                                    <td colspan="2">
                                        <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                            Enabled="false" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <label>
                                            Cos Postal</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_cpostal" runat="server" Enabled="false" Text='<%# Bind("TRPOSTAL") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td colspan="2">
                                        <label>
                                            Email</label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="txt_email" runat="server" Enabled="false" Text='<%# Bind("TRCORREO") %>'
                                            Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>
                                                    Empleado</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_empleado" Checked='<%# this.GetCheck(Eval("TRINDEMP")) %>'
                                                    runat="server" Enabled="false" />
                                            </td>
                                            <td>
                                                <label>
                                                    Vendedor</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_vendedor" Checked='<%# this.GetCheck(Eval("TRINDVEN")) %>'
                                                    runat="server" Enabled="false" />
                                            </td>
                                            <td>
                                                <label>
                                                    Cliente</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_cliente" Checked='<%# this.GetCheck(Eval("TRINDCLI")) %>' runat="server"
                                                    Enabled="false" />
                                            </td>
                                            <td>
                                                <label>
                                                    Proveedor</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_proveedor" Checked='<%# this.GetCheck(Eval("TRINDPRO")) %>'
                                                    runat="server" Enabled="false" />
                                            </td>
                                            <td>
                                                <label>
                                                    Forwarder</label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chk_forwarder" Checked='<%# this.GetCheck(Eval("TRINDFOR")) %>'
                                                    runat="server" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_dcomerciales" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            Moneda Principal</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMONEDA") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            T Pago</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                            DataSourceID="obj_tpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("TRTERPAG") %>'
                                            DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Modo Despacho</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_moddespacho" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_moddespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMODDES") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Terminos Despacho</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_terdespacho" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_terdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTERDES") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Categoria(Canal)</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_canal" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                            DataSourceID="obj_canal" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCATEGO") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Idioma</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRIDIOMA") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Lst Precio</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRE") %>'
                                            DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Lst Precio Alterna</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_lstprealt" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_lstprealt" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRA") %>'
                                            DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Agente</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_agente" DataTextField="usua_nombres" SelectedValue='<%# Bind("TRAGENTE") %>'
                                            DataValueField="usua_secuencia" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Zona</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                            DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCODZONA") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Alterno</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chl_alterno" Checked='<%# this.GetCheck(Eval("TRDTTEC1")) %>' runat="server"
                                            Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_sucursales" runat="server">
                            <telerik:RadGrid ID="rg_sucursales" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_sucursal">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="SC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_NOMBRE"
                                            UniqueName="SC_NOMBRE" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SC_TELEFONO" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Telefono" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_TELEFONO"
                                            UniqueName="SC_TELEFONO" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SC_DIRECCION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Direccion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_DIRECCION"
                                            UniqueName="SC_DIRECCION" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SC_DIRECCION2" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="250px" HeaderText="Dir Entrega" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="SC_DIRECCION2" UniqueName="SC_DIRECCION2" Visible="true">
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
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_familia" runat="server">
                            <telerik:RadGrid ID="rg_familia" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_familia_NeedDataSource">
                                <MasterTableView>
                                    <GroupByExpressions>
                                        <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Tipo" FieldName="NOM_TIPO" FormatString="{0:D}"
                                                    HeaderValueSeparator=":"></telerik:GridGroupByField>
                                            </SelectFields>
                                            <GroupByFields>
                                                <telerik:GridGroupByField FieldName="NOM_TIPO" SortOrder="Descending"></telerik:GridGroupByField>
                                            </GroupByFields>
                                        </telerik:GridGroupByExpression>
                                    </GroupByExpressions>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Parentesco" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                            UniqueName="TTDESCRI" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_PNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_PNOMBRE"
                                            UniqueName="FM_PNOMBRE" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_SNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_SNOMBRE"
                                            UniqueName="FM_SNOMBRE" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_PAPELLIDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_PAPELLIDO"
                                            UniqueName="FM_PAPELLIDO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_SAPELLIDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_SAPELLIDO"
                                            UniqueName="FM_SAPELLIDO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_FNACIMIENTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="F Nacimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_FNACIMIENTO"
                                            UniqueName="FM_FNACIMIENTO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_EMAIL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Email" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_EMAIL"
                                            UniqueName="FM_EMAIL" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_TELEFONO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Telefono" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_TELEFONO"
                                            UniqueName="FM_TELEFONO" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FM_DIRECCION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Direccion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_DIRECCION"
                                            UniqueName="FM_DIRECCION" Visible="true">
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
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_infoacademica" runat="server">
                            <telerik:RadGrid ID="rg_infoacademica" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_infoacademica_ItemCommand"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_infoacademica_NeedDataSource">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TT_CODIGO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                            SortExpression="TT_CODIGO" UniqueName="TT_CODIGO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO_ESTUDIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T. Estudios" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPO_ESTUDIO"
                                            UniqueName="TIPO_ESTUDIO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PROFESION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Profesion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PROFESION"
                                            UniqueName="PROFESION" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TT_DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_DESCRIPCION"
                                            UniqueName="TT_DESCRIPCION" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TT_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_FECHA"
                                            UniqueName="TT_FECHA" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TT_FECHAVEN" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="F Vencimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_FECHAVEN"
                                            UniqueName="TT_FECHAVEN" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar" HeaderStyle-Width="60px"
                                            HeaderText="">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_otros" runat="server">
                            <telerik:RadGrid ID="rg_otrosanexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_otrosanexos"
                                Culture="(Default)" CellSpacing="0" OnItemCommand="rg_otrosanexos_ItemCommand">
                                <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="New Item" RefreshText="Load" />
                                    <Columns>
                                        <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="30px">
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn ConfirmText="Desea Eliminar esta Imagen?" ConfirmDialogType="RadWindow"
                                            HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                            ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                            <HeaderStyle Width="30px" />
                                        </telerik:GridButtonColumn>--%>
                                        <telerik:GridBoundColumn DataField="OA_CONSECUTIVO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                            SortExpression="OA_CONSECUTIVO" UniqueName="OA_CONSECUTIVO" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OA_DESCRIPCION" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="460px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="OA_DESCRIPCION" UniqueName="OA_DESCRIPCION">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OA_FECVENCIMINTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OA_FECVENCIMINTO"
                                            UniqueName="OA_FECVENCIMINTO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar" HeaderStyle-Width="60px"
                                            HeaderText="">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div>
                                                <table>
                                                </table>
                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_contabilidad" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            T Regimen</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_tregimen" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_regimen" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPREG") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <label>
                                            Detalle</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_detalle" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_detalle" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRGRANCT") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage2"
                                SelectedIndex="0" CssClass="tabStrip">
                                <Tabs>
                                    <telerik:RadTab Text="Impuestos">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Causasion">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0">
                                <telerik:RadPageView ID="RadPageView1" runat="server">
                                    <telerik:RadGrid ID="rg_impuestos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_impuestos_DetailTableDataBind"
                                        AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_impuestos" OnItemCommand="rg_impuestos_ItemCommand">
                                        <MasterTableView DataKeyNames="PH_CODIGO">
                                            <DetailTables>
                                                <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                            HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                                            UniqueName="PC_CODIGO">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                            HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                                            UniqueName="PC_NOMBRE">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                            HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                            UniqueName="TTDESCRI">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                            HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                                            UniqueName="PI_PORCENTAJE">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                            HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                                            UniqueName="PI_BASE">
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODIGO"
                                                    UniqueName="PH_CODIGO" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PH_CODIGO" HeaderText="PH_CODIGO" UniqueName="TRCODNIT_LBL"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODIGO" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODIGO") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="plantilla" UniqueName="PH_NOMBRE" DataTextField="PH_NOMBRE"
                                                    HeaderText="Nombre" HeaderStyle-Width="250px">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn DataField="T_PLA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="T. Planilla" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_PLA"
                                                    UniqueName="T_PLA" Visible="true">
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
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView2" runat="server">
                                    <telerik:RadGrid ID="rg_cuentas" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight"
                                        AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0">
                                        <MasterTableView>
                                            <CommandItemSettings ShowRefreshButton="false" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                                    UniqueName="PC_CODIGO" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                                    UniqueName="PC_NOMBRE" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CTT_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTT_NATURALEZA"
                                                    UniqueName="CTT_NATURALEZA" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CTT_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTT_BASE"
                                                    UniqueName="CTT_BASE" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="Impuesto/Res" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                    UniqueName="TTDESCRI" Visible="true">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No se Encontaron Registros!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_nomina" runat="server" SelectedIndex="0">
                            <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="mp_nomina"
                                CssClass="tabStrip">
                                <Tabs>
                                    <telerik:RadTab Text="Contratos">
                                    </telerik:RadTab>
                                    <telerik:RadTab Text="Conceptos Planilla">
                                    </telerik:RadTab>
                                </Tabs>
                            </telerik:RadTabStrip>
                            <telerik:RadMultiPage ID="mp_nomina" runat="server">
                                <telerik:RadPageView ID="RadPageView3" runat="server">
                                    <telerik:RadGrid ID="rg_contratos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_contratos_ItemCommand"
                                        AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_contratos_NeedDataSource">
                                        <MasterTableView DataKeyNames="CT_ID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="CT_ID" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                    HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_ID"
                                                    UniqueName="CT_ID" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CT_FINGRESO" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                    HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FINGRESO"
                                                    UniqueName="CT_FINGRESO" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CT_FTERMINACION" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                    HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FTERMINACION"
                                                    UniqueName="CT_FTERMINACION" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CONTRATO"
                                                    UniqueName="T_CONTRATO" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="T_NOVEDAD" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="T Novedad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_NOVEDAD"
                                                    UniqueName="T_NOVEDAD" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                    HeaderText="Cargo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CARGO"
                                                    UniqueName="T_CARGO" Visible="true">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No se Encontaron Registros!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                                <telerik:RadPageView ID="RadPageView4" runat="server">
                                    <telerik:RadGrid ID="rg_planillanomina" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_planillanomina_DetailTableDataBind"
                                        AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnItemCommand="rg_planillanomina_ItemCommand" OnNeedDataSource="rg_planillanomina_NeedDataSource">
                                        <MasterTableView DataKeyNames="PH_CODPLAN">
                                            <DetailTables>
                                                <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                            HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                            UniqueName="TTDESCRI">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                            HeaderText="Tipo S R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOSR"
                                                            UniqueName="TIPOSR">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                            HeaderText="Tipo % V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOPV"
                                                            UniqueName="TIPOPV">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PD_BASE")) %>' Enabled="false" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                        <telerik:GridBoundColumn DataField="PD_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                            HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_VALOR"
                                                            UniqueName="PD_VALOR">
                                                            <ItemStyle HorizontalAlign="Right" />
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
                                                <telerik:GridBoundColumn DataField="PH_CODPLAN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                    HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODPLAN"
                                                    UniqueName="PH_CODPLAN" Visible="true">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="PH_CODPLAN" HeaderText="PH_CODPLAN" UniqueName="PH_CODPLAN_lbl"
                                                    HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODPLAN" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODPLAN") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="PH_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                    HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_NOMBRE"
                                                    UniqueName="PH_NOMBRE" Visible="true">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <NoRecordsTemplate>
                                                <div class="alert alert-danger">
                                                    <strong>¡No se Encontaron Registros!</strong>
                                                </div>
                                            </NoRecordsTemplate>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadPageView>
                            </telerik:RadMultiPage>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pvHorizontal" runat="server">
                            <telerik:RadGrid ID="rg_horizontal" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_horizontal_ItemCommand" AllowFilteringByColumn="True"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_horizontal_NeedDataSource" Height="650px" AllowPaging="true" PageSize="100">
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView>
                                    <ColumnGroups>
                                        <telerik:GridColumnGroup Name="1" HeaderText="INFORMACION GENERAL" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="2" HeaderText="INSTALACIONES" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="3" HeaderText="COMERCIAL" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="4" HeaderText="DESMONTAJE" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="5" HeaderText="RADICACION OFICINA" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="6" HeaderText="RADICACION FINAL" HeaderStyle-HorizontalAlign="Center" />
                                        <telerik:GridColumnGroup Name="7" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" />
                                    </ColumnGroups>
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select" ImageUrl="../App_Themes/Tema2/Images/find_.png" ButtonType="ImageButton" ColumnGroupName="1" HeaderStyle-Width="40px" />
                                        <%--<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" ColumnGroupName="1"/>--%>

                                        <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" ColumnGroupName="1"
                                            HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" FilterControlWidth="50px"
                                            SortExpression="PH_CODIGO" UniqueName="PH_CODIGO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="1"
                                            HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                            UniqueName="PH_CTACONTRATO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_POLIZA" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="1"
                                            HeaderText="Poliza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_POLIZA"
                                            UniqueName="PH_POLIZA" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                            HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO" FilterControlWidth="50px"
                                            UniqueName="PH_EDIFICIO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_PORTAL" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                            HeaderText="Portal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PORTAL" FilterControlWidth="50px"
                                            UniqueName="PH_PORTAL" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_PISO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                            HeaderText="Piso" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PISO" FilterControlWidth="50px"
                                            UniqueName="PH_PISO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                            HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA" FilterControlWidth="50px"
                                            UniqueName="PH_ESCALERA" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_OBJCONEXION" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                            HeaderText="Obj Conexion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                            UniqueName="PH_OBJCONEXION" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_PTOSUMINISTRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                            HeaderText="Pto Suministro" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PTOSUMINISTRO"
                                            UniqueName="PH_PTOSUMINISTRO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_INSTALACION" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                            HeaderText="Instalacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_INSTALACION"
                                            UniqueName="PH_INSTALACION" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PH_UBCAPARATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                            HeaderText="U. Aparato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_UBCAPARATO"
                                            UniqueName="PH_UBCAPARATO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="2"
                                            HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                            UniqueName="TTDESCRI" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="IT_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="2"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IT_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="IT_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechainst" HeaderStyle-Width="160px" ColumnGroupName="2">
                                            <ItemTemplate>
                                                <telerik:RadDatePicker ID="txt_finstalacionrg" runat="server" DbSelectedDate='<%# Bind("IT_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="tecnico_inst" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="2"
                                            HeaderText="Tecnico" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tecnico_inst"
                                            UniqueName="tecnico_inst" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px" ColumnGroupName="2"
                                            HeaderText="Producto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MECDELEM" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="2"
                                            HeaderText="Serial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MECDELEM"
                                            UniqueName="MECDELEM" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ESTADO_CO" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="3"
                                            HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ESTADO_CO"
                                            UniqueName="ESTADO_CO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="CO_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="3"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="CO_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechacom" HeaderStyle-Width="160px" ColumnGroupName="3">
                                            <ItemTemplate>
                                                <telerik:RadDatePicker ID="txt_feccomercialrg" runat="server" DbSelectedDate='<%# Bind("CO_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="CO_USUARIO" HeaderText="Ref Tester" UniqueName="CO_USUARIO_TK" ColumnGroupName="3"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="OC_USUARIO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_cousuario" runat="server" Text='<%# Eval("CO_USUARIO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="IT_USUARIO" HeaderText="Ref Tester" UniqueName="IT_USUARIO_TK" ColumnGroupName="3"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="IT_USUARIO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_itusuario" runat="server" Text='<%# Eval("IT_USUARIO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="comercial" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                            HeaderText="Asesor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="comercial"
                                            UniqueName="comercial" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="3"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT" Visible="true">
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="CODCLI" HeaderText="Referencia" UniqueName="TRCODTER_TK" ColumnGroupName="3"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CODCLI" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codterc" runat="server" Text='<%# Eval("CODCLI") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="TRCODNIT" DataTextField="TRCODNIT" ColumnGroupName="3"
                                            HeaderText="Identificacion" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                            HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                            UniqueName="TRNOMBRE" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBR2" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                            HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBR2"
                                            UniqueName="TRNOMBR2" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRAPELLI" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                            HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRAPELLI"
                                            UniqueName="TRAPELLI" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBR3" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                            HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBR3"
                                            UniqueName="TRNOMBR3" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="DI_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="4"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DI_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="DI_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechadesm" HeaderStyle-Width="160px" ColumnGroupName="4">
                                            <ItemTemplate>
                                                <telerik:RadDatePicker ID="txt_fecdesmorg" runat="server" DbSelectedDate='<%# Bind("DI_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DI_USUARIO" HeaderText="Ref Tester" UniqueName="DI_USUARIO_TK" ColumnGroupName="4"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DI_USUARIO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_desusuario" runat="server" Text='<%# Eval("DI_USUARIO") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DI_NRODOC" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="4"
                                            HeaderText="Nro Soporte" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DI_NRODOC"
                                            UniqueName="DI_NRODOC" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="tec_desistala" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="4"
                                            HeaderText="Tecnico" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tec_desistala"
                                            UniqueName="tec_desistala" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="CIH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="5"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CIH_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="CIH_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfecradorg" HeaderStyle-Width="160px" ColumnGroupName="5">
                                            <ItemTemplate>
                                                <telerik:RadDatePicker ID="txt_fecradorg" runat="server" DbSelectedDate='<%# Bind("CIH_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfecvantiorg" HeaderStyle-Width="160px" ColumnGroupName="6">
                                            <ItemTemplate>
                                                <telerik:RadDatePicker ID="txt_fecvantiorg" runat="server" DbSelectedDate='<%# Bind("COH_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%--<telerik:GridBoundColumn DataField="COH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="6"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COH_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="COH_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn DataField="ESTADO_T" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="7"
                                            HeaderText="Est Definitivo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ESTADO_T"
                                            UniqueName="ESTADO_T" Visible="true">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No se Encontaron Registros!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <table cellspacing="1">
                    <tr>
                        <td rowspan="10">
                            <telerik:RadBinaryImage runat="server" ID="txt_foto" AutoAdjustImageControlSize="false"
                                Width="100px" Height="170px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Codigo</label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>'
                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                T. Doc</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_tipdoc" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_tipdoc" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPDOC") %>'
                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>
                                Identificacion</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" ValidationGroup="gvInsert" OnTextChanged="txt_identificacion_OnTextChanged" AutoPostBack="true"
                                Text='<%# Bind("TRCODNIT") %>' Width="230px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_identificacion"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image26" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="txt_digchk" runat="server" Enabled="true" Text='<%# Bind("TRDIGCHK") %>'
                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="47px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>N. Comercial</label></td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_nomcomercial" runat="server" Enabled="true" Text='<%# Bind("TRNOMCOMERCIAL") %>' Width="600px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                P Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TRNOMBRE") %>'
                                ValidationGroup="gvInsert" Width="300px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nombre"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>
                                S Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_snombre" runat="server" Enabled="true" Text='<%# Bind("TRNOMBR2") %>'
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                P Apellido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_apellidos" runat="server" Enabled="true" Text='<%# Bind("TRAPELLI") %>'
                                ValidationGroup="gvInsert" Width="300px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_apellidos"
                                ValidationGroup="gvInsert" ErrorMessage="(*)">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>
                                S Apellido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_sapellido" runat="server" Enabled="true" Text='<%# Bind("TRNOMBR3") %>'
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                F. Nacimiento</label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TRFECNAC") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>
                                Estado</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("TRESTADO") %>'
                                Enabled="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                    <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Datos Personales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Datos Comerciales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Sucursales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Familia + Referencia">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Info Academica">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Otros">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Contabilidad">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Nomina">
                        </telerik:RadTab>
                        <telerik:RadTab Text="P. Horizontal">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_dpersonales" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Telefono</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("TRNROTEL") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Celular</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_celular" runat="server" Enabled="true" Text='<%# Bind("TRNROFAX") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Direccion</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="true" Text='<%# Bind("TRDIRECC") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Direccion Entrega</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_direccentrega" runat="server" Enabled="true" Text='<%# Bind("TRDIREC2") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Pais</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" AutoPostBack="true"
                                        Enabled="true" DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCDPAIS") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_pais_OnSelectedIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Ciudad</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                        Enabled="true" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Cos Postal</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_cpostal" runat="server" Enabled="true" Text='<%# Bind("TRPOSTAL") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Email</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_email" runat="server" Enabled="true" Text='<%# Bind("TRCORREO") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                Empleado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_empleado" Checked='<%# this.GetCheck(Eval("TRINDEMP")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Vendedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_vendedor" Checked='<%# this.GetCheck(Eval("TRINDVEN")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Cliente</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_cliente" Checked='<%# this.GetCheck(Eval("TRINDCLI")) %>' runat="server"
                                                Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Proveedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_proveedor" Checked='<%# this.GetCheck(Eval("TRINDPRO")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Forwarder</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_forwarder" Checked='<%# this.GetCheck(Eval("TRINDFOR")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                    </tr>
                                </table>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_dcomerciales" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Moneda Principal</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        T Pago</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_tpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("TRTERPAG") %>'
                                        DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Modo Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moddespacho" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_moddespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMODDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Terminos Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_terdespacho" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_terdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTERDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Categoria(Canal)</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_canal" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_canal" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCATEGO") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Idioma</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRIDIOMA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Lst Precio</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRE") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Lst Precio Alterna</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprealt" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprealt" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRA") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Agente</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_agente" DataTextField="usua_nombres" SelectedValue='<%# Bind("TRAGENTE") %>'
                                        DataValueField="usua_secuencia" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Zona</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCODZONA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Alterno</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chl_alterno" Checked='<%# this.GetCheck(Eval("TRDTTEC1")) %>' runat="server"
                                        Enabled="true" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_sucursales" runat="server">
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_familia" runat="server">
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_infoacademica" runat="server">
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_otros" runat="server">
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_contabilidad" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        T Regimen</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tregimen" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_regimen" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPREG") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Detalle</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_detalle" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_detalle" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRGRANCT") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>Cta Contable</label>
                                </td>
                                <td>
                                    <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single"
                                        DataSourceID="obj_ctacontable" Width="350px" DataTextField="PC_NOMBRE" DropDownWidth="550px" Enabled="true"
                                        OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="PC_CODIGO"
                                        DropDownHeight="580px" Filter="StartsWith">
                                        <DropDownItemTemplate>
                                            <table cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "PC_CODIGO")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "PC_NOMBRE")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownItemTemplate>
                                    </telerik:RadAutoCompleteBox>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="rg_impuestos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_impuestos_DetailTableDataBind"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_impuestos" OnItemCommand="rg_impuestos_ItemCommand">
                            <MasterTableView DataKeyNames="PH_CODIGO">
                                <DetailTables>
                                    <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                UniqueName="TTDESCRI">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                                UniqueName="PI_PORCENTAJE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                                UniqueName="PI_BASE">
                                                <ItemStyle HorizontalAlign="Right" />
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
                                    <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODIGO"
                                        UniqueName="PH_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="PH_CODIGO" HeaderText="PH_CODIGO" UniqueName="TRCODNIT_LBL"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODIGO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODIGO") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="plantilla" UniqueName="PH_NOMBRE" DataTextField="PH_NOMBRE"
                                        HeaderText="Nombre" HeaderStyle-Width="250px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="T_PLA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="T. Planilla" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_PLA"
                                        UniqueName="T_PLA" Visible="true">
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
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_nomina" runat="server">
                        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="mp_nomina"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Contratos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Conceptos Planilla">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="mp_nomina" runat="server">
                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <telerik:RadGrid ID="rg_contratos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_contratos_ItemCommand"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_contratos_NeedDataSource">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_ID">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="CT_ID" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_ID"
                                                UniqueName="CT_ID" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_FINGRESO" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FINGRESO"
                                                UniqueName="CT_FINGRESO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_FTERMINACION" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FTERMINACION"
                                                UniqueName="CT_FTERMINACION" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CONTRATO"
                                                UniqueName="T_CONTRATO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_NOVEDAD" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="T Novedad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_NOVEDAD"
                                                UniqueName="T_NOVEDAD" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Cargo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CARGO"
                                                UniqueName="T_CARGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_SALARIO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" DataFormatString="{0:C}"
                                                HeaderText="Salario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_SALARIO"
                                                UniqueName="CT_SALARIO" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <table>
                                                    <telerik:RadTextBox ID="txt_ctid" runat="server" Enabled="true" Text='<%# Eval("CT_ID") %>' Width="300px" Visible="false">
                                                    </telerik:RadTextBox>

                                                    <tr>
                                                        <td>
                                                            <label>T Contrato</label></td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_tcontrato" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                                Enabled="true" DataSourceID="obj_tcontrato" DataTextField="TTDESCRI" SelectedValue='<%# Eval("CT_TCONTRATO") %>'
                                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rc_tcontrato" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <label>T Novedad</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_tnovedad" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                                Enabled="true" DataSourceID="obj_tnovedad" DataTextField="TTDESCRI" SelectedValue='<%# Eval("CT_TNOVEDAD") %>'
                                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rc_tnovedad" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Cargo</label></td>
                                                        <td colspan="3">
                                                            <telerik:RadComboBox ID="rc_cargo" runat="server" Culture="es-CO" Width="600px" SelectedValue='<%# Eval("CT_CARGO") %>'
                                                                DataSourceID="obj_cargo" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_cargo" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>F Inicio</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txt_fcontrato" runat="server" DbSelectedDate='<%# Bind("CT_FINGRESO") %>' MinDate="01/01/1900" Enabled="true">
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_fcontrato"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <label>F Final</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txt_ffinalcontrato" runat="server" DbSelectedDate='<%# Bind("CT_FTERMINACION") %>' MinDate="01/01/1900">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Salario</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txt_salario" runat="server" DbValue='<%# Eval("CT_SALARIO") %>' Enabled="true"></telerik:RadNumericTextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_salario"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                ValidationGroup="gvInsertNM" SkinID="SkinUpdateUC" />
                                                            <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView4" runat="server">
                                <telerik:RadGrid ID="rg_planillanomina" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_planillanomina_DetailTableDataBind"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnItemCommand="rg_planillanomina_ItemCommand" OnNeedDataSource="rg_planillanomina_NeedDataSource">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PH_CODPLAN">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                        HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                        UniqueName="TTDESCRI">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                        HeaderText="Tipo S R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOSR"
                                                        UniqueName="TIPOSR">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                        HeaderText="Tipo % V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOPV"
                                                        UniqueName="TIPOPV">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PD_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                        HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_VALOR"
                                                        UniqueName="PD_VALOR">
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            <telerik:GridBoundColumn DataField="PH_CODPLAN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODPLAN"
                                                UniqueName="PH_CODPLAN" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PH_CODPLAN" HeaderText="PH_CODPLAN" UniqueName="PH_CODPLAN_lbl"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODPLAN" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODPLAN") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="PH_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_NOMBRE"
                                                UniqueName="PH_NOMBRE" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pvHorizontal" runat="server">
                        <telerik:RadGrid ID="rg_horizontal" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_horizontal_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_horizontal_NeedDataSource">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="PH_CODIGO" UniqueName="PH_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                        UniqueName="PH_CTACONTRATO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO"
                                        UniqueName="PH_EDIFICIO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PORTAL" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Portal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PORTAL"
                                        UniqueName="PH_PORTAL" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PISO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Piso" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PISO"
                                        UniqueName="PH_PISO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                        HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                        UniqueName="PH_ESCALERA" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_OBJCONEXION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Obj Conexion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                        UniqueName="PH_OBJCONEXION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PTOSUMINISTRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Pto Suministro" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PTOSUMINISTRO"
                                        UniqueName="PH_PTOSUMINISTRO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_INSTALACION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Instalacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_INSTALACION"
                                        UniqueName="PH_INSTALACION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_UBCAPARATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="U, Aparato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_UBCAPARATO"
                                        UniqueName="PH_UBCAPARATO" Visible="true">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
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
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr>
                        <td rowspan="7">
                            <telerik:RadBinaryImage runat="server" ID="txt_foto" AutoAdjustImageControlSize="false"
                                Width="100px" Height="170px" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>
                                Codigo</label>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("TRCODTER") %>'
                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                T. Doc</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_tipdoc" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_tipdoc" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPDOC") %>'
                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <label>
                                Identificacion</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                Text='<%# Bind("TRCODNIT") %>' Width="230px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_identificacion"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image26" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="txt_digchk" runat="server" Enabled="true" Text='<%# Bind("TRDIGCHK") %>'
                                NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="47px">
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>N. Comercial</label></td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="txt_nomcomercial" runat="server" Enabled="true" Text='<%# Bind("TRNOMCOMERCIAL") %>' Width="600px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                P Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("TRNOMBRE") %>'
                                ValidationGroup="gvInsert" Width="300px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nombre"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>
                                S Nombre</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_snombre" runat="server" Enabled="true" Text='<%# Bind("TRNOMBR2") %>'
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                P Apellido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_apellidos" runat="server" Enabled="true" Text='<%# Bind("TRAPELLI") %>'
                                ValidationGroup="gvInsert" Width="300px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_apellidos"
                                ValidationGroup="gvInsert" ErrorMessage="(*)">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>
                                S Apellido</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_sapellido" runat="server" Enabled="true" Text='<%# Bind("TRNOMBR3") %>'
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                F. Nacimiento</label>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TRFECNAC") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>
                                Estado</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("TRESTADO") %>'
                                Enabled="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                    <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="1">
                            <telerik:RadButton RenderMode="Lightweight" ID="RadButton1" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" ToolTip="Nuevo Registro" />
                            <telerik:RadButton RenderMode="Lightweight" ID="RadButton2" runat="server" Text="" Icon-PrimaryIconCssClass="rbOpen" ToolTip="Nuevo Registro" />
                        </td>
                    </tr>
                </table>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Datos Personales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Datos Comerciales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Sucursales">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Familia + Referencia">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Info Academica">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Otros">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Contabilidad">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Nomina">
                        </telerik:RadTab>
                        <telerik:RadTab Text="P. Horizontal">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_dpersonales" runat="server">
                        <table>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Telefono</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("TRNROTEL") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Celular</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_celular" runat="server" Enabled="true" Text='<%# Bind("TRNROFAX") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Direccion</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="true" Text='<%# Bind("TRDIRECC") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Direccion Entrega</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_direccentrega" runat="server" Enabled="true" Text='<%# Bind("TRDIREC2") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Pais</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" AutoPostBack="true"
                                        Enabled="true" DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCDPAIS") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_pais_OnSelectedIndexChanged">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Ciudad</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                        Enabled="true" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <label>
                                        Cos Postal</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_cpostal" runat="server" Enabled="true" Text='<%# Bind("TRPOSTAL") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                                <td colspan="2">
                                    <label>
                                        Email</label>
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txt_email" runat="server" Enabled="true" Text='<%# Bind("TRCORREO") %>'
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <table>
                                    <tr>
                                        <td>
                                            <label>
                                                Empleado</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_empleado" Checked='<%# this.GetCheck(Eval("TRINDEMP")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Vendedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_vendedor" Checked='<%# this.GetCheck(Eval("TRINDVEN")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Cliente</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_cliente" Checked='<%# this.GetCheck(Eval("TRINDCLI")) %>' runat="server"
                                                Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Proveedor</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_proveedor" Checked='<%# this.GetCheck(Eval("TRINDPRO")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                        <td>
                                            <label>
                                                Forwarder</label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_forwarder" Checked='<%# this.GetCheck(Eval("TRINDFOR")) %>'
                                                runat="server" Enabled="true" />
                                        </td>
                                    </tr>
                                </table>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_dcomerciales" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Moneda Principal</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        T Pago</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tpago" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_tpago" DataTextField="TPNOMBRE" SelectedValue='<%# Bind("TRTERPAG") %>'
                                        DataValueField="TPTERPAG" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Modo Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moddespacho" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_moddespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRMODDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Terminos Despacho</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_terdespacho" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_terdespacho" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTERDES") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Categoria(Canal)</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_canal" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_canal" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCATEGO") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Idioma</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_idioma" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_idioma" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRIDIOMA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Lst Precio</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRE") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Lst Precio Alterna</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_lstprealt" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_lstprealt" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("TRLISPRA") %>'
                                        DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Agente</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_agente" runat="server" Culture="es-CO" Width="300px" Filter="StartsWith" AllowCustomText="true"
                                        Enabled="true" DataSourceID="obj_agente" DataTextField="usua_nombres" SelectedValue='<%# Bind("TRAGENTE") %>'
                                        DataValueField="usua_secuencia" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Zona</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                        DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRCODZONA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Alterno</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chl_alterno" Checked='<%# this.GetCheck(Eval("TRDTTEC1")) %>' runat="server"
                                        Enabled="true" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_sucursales" runat="server">
                        <telerik:RadGrid ID="rg_sucursales" runat="server" GridLines="None" Width="100%"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_sucursal">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_newsuc" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="SC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                        HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_NOMBRE"
                                        UniqueName="SC_NOMBRE" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SC_TELEFONO" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                        HeaderText="Telefono" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_TELEFONO"
                                        UniqueName="SC_TELEFONO" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SC_DIRECCION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                        HeaderText="Direccion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SC_DIRECCION"
                                        UniqueName="SC_DIRECCION" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SC_DIRECCION2" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="250px" HeaderText="Dir Entrega" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="SC_DIRECCION2" UniqueName="SC_DIRECCION2" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Nombre</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_nomsucursal" runat="server" Enabled="true" ValidationGroup="grNuevo"
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nomsucursal"
                                                            ErrorMessage="(*)" ValidationGroup="grNuevo">
                                                            <asp:Image ID="Image26" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Telefono</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_telsucursal" runat="server" Enabled="true" ValidationGroup="grNuevo"
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
                                                        <telerik:RadTextBox ID="txt_dirsucursal" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Dir Entrega</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_direntsucursal" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                                            Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Pais</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" AutoPostBack="true"
                                                            Enabled="true" DataSourceID="obj_pais" DataTextField="TTDESCRI" DataValueField="TTCODCLA"
                                                            AppendDataBoundItems="true" OnSelectedIndexChanged="rc_pais_OnSelectedIndexChanged">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td>
                                                        <label>
                                                            Ciudad</label>
                                                    </td>
                                                    <td colspan="2">
                                                        <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains"
                                                            Enabled="true" AppendDataBoundItems="true">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                            OnClick="btn_aceptar_OnClick" ValidationGroup="grNuevo" SkinID="SkinUpdateUC" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                            CausesValidation="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_familia" runat="server">
                        <telerik:RadGrid ID="rg_familia" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_familia_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_familia_NeedDataSource">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_newitmin" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                </CommandItemTemplate>
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldAlias="Tipo" FieldName="NOM_TIPO" FormatString="{0:D}"
                                                HeaderValueSeparator=":"></telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="NOM_TIPO" SortOrder="Descending"></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="¿Desea Eliminar este Registro?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="390px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="FM_CODIGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="FM_CODIGO" UniqueName="FM_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Parentesco" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_PNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_PNOMBRE"
                                        UniqueName="FM_PNOMBRE" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_SNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_SNOMBRE"
                                        UniqueName="FM_SNOMBRE" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_PAPELLIDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_PAPELLIDO"
                                        UniqueName="FM_PAPELLIDO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_SAPELLIDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_SAPELLIDO"
                                        UniqueName="FM_SAPELLIDO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_FNACIMIENTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="F Nacimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_FNACIMIENTO"
                                        UniqueName="FM_FNACIMIENTO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_EMAIL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Email" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_EMAIL"
                                        UniqueName="FM_EMAIL" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_TELEFONO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Telefono" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_TELEFONO"
                                        UniqueName="FM_TELEFONO" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FM_DIRECCION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                        HeaderText="Direccion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FM_DIRECCION"
                                        UniqueName="FM_DIRECCION" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>T. Referencia</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_treferencia" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                        AppendDataBoundItems="true" ValidationGroup="grNuevo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Familiar" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Personal" Value="2" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rc_treferencia"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>Parentesco</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_parentesco" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                        DataSourceID="obj_parentesco" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true"
                                                        ValidationGroup="grNuevo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="rc_parentesco"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>T Documento</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tdoc" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                        DataSourceID="obj_tipdoc" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true"
                                                        ValidationGroup="grNuevo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_tdoc"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>Nro Documento</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_nrodocfam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>P Nombre</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_pnombrefam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_pnombrefam"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>S Nombre</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_snombrefam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>P Apellido</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_papellidofam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_papellidofam"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>S Apellido</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_sapellidofam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>F Nacimiento</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txt_fnacimientofam" runat="server" Enabled="true" ValidationGroup="grFamilia" DbSelectedDate='<%# System.DateTime.Now %>'>
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <label>S Telefono</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_telfam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Direccion</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_direccionfam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <label>Email</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_emailfam" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btnInsertFam" runat="server" CommandName="PerformInsert" Text="" ValidationGroup="grFamilia" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                                    <telerik:RadButton ID="btnCancelFam" runat="server" CommandName="Cancel" Text="" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_infoacademica" runat="server">
                        <telerik:RadGrid ID="rg_infoacademica" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_infoacademica_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_infoacademica_NeedDataSource">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_newitmtit" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Item" />
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="¿Desea Eliminar este Registro?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="390px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="TT_CODIGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="TT_CODIGO" UniqueName="TT_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_ESTUDIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="T. Estudios" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPO_ESTUDIO"
                                        UniqueName="TIPO_ESTUDIO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROFESION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Profesion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PROFESION"
                                        UniqueName="PROFESION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TT_DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_DESCRIPCION"
                                        UniqueName="TT_DESCRIPCION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TT_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_FECHA"
                                        UniqueName="TT_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TT_FECHAVEN" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="F Vencimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TT_FECHAVEN" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="TT_FECHAVEN" Visible="true">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>Nivel</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_nivel" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                        DataSourceID="obj_nivelacademico" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true"
                                                        ValidationGroup="grTitulos">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_nivel"
                                                        ErrorMessage="(*)" ValidationGroup="grTitulos" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>Profesion</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_profesion" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                                        DataSourceID="obj_profesion" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true"
                                                        ValidationGroup="grTitulos">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_profesion"
                                                        ErrorMessage="(*)" ValidationGroup="grTitulos" InitialValue="Seleccionar">
                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Descripcion</label></td>
                                                <td colspan="3">
                                                    <telerik:RadTextBox ID="txt_descripciontit" runat="server" Enabled="true" Width="300px">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_descripciontit"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>F. Documento</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txt_fdocumentotit" runat="server" Enabled="true" ValidationGroup="grFamilia" DbSelectedDate='<%# System.DateTime.Now %>'>
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_fdocumentotit"
                                                        ErrorMessage="(*)" ValidationGroup="grFamilia">
                                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>F. Vencimiento</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txt_fvencimientotit" runat="server" Enabled="true" ValidationGroup="grFamilia">
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Gen Alerta</label></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_alertatit" runat="server" Enabled="true" />
                                                </td>
                                                <td>
                                                    <label>Adjunto</label>
                                                </td>
                                                <td>
                                                    <telerik:RadAsyncUpload ID="rauCargar" runat="server" ControlObjectsVisibility="None" Visible='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                                        Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                        Width="350px" OnFileUploaded="rauCargar_FileUploaded" AllowedFileExtensions=".pdf"
                                                        Style="margin-bottom: 0px">
                                                        <Localization Select="Cargar Archivo" />
                                                    </telerik:RadAsyncUpload>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btnInsertTit" runat="server" CommandName="PerformInsert" Text="" ValidationGroup="grTitulos" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                                    <telerik:RadButton ID="btnCancelTit" runat="server" CommandName="Cancel" Text="" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FormTemplate>
                                </EditFormSettings>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_otros" runat="server">
                        <telerik:RadGrid ID="rg_otrosanexos" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_otrosanexos" OnNeedDataSource="rg_otrosanexos_NeedDataSource" OnItemCommand="rg_otrosanexos_ItemCommand"
                            Culture="(Default)" CellSpacing="0">
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="New Item" RefreshText="Load" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nueva Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                </CommandItemTemplate>
                                <Columns>
                                    <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" HeaderStyle-Width="30px">
                                    </telerik:GridEditCommandColumn>--%>
                                    <telerik:GridButtonColumn ConfirmText="Desea Eliminar esta Imagen?" ConfirmDialogType="RadWindow"
                                        HeaderStyle-Width="30px" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete"
                                        ConfirmDialogHeight="100px" ConfirmDialogWidth="220px">
                                        <HeaderStyle Width="30px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="OA_CONSECUTIVO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                        SortExpression="OA_CONSECUTIVO" UniqueName="OA_CONSECUTIVO" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OA_DESCRIPCION" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="460px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="OA_DESCRIPCION" UniqueName="OA_DESCRIPCION">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OA_FECVENCIMINTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OA_FECVENCIMINTO"
                                        UniqueName="OA_FECVENCIMINTO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar" HeaderStyle-Width="60px"
                                        HeaderText="">
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <label>Descripcion</label></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txt_descripcionotros" runat="server" Enabled="true" Width="300px">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        <label>Adjunto</label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadAsyncUpload ID="rauCargar" runat="server" ControlObjectsVisibility="None" Visible='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                                            Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                            Width="350px" OnFileUploaded="rauCargar_FileUploaded" AllowedFileExtensions=".pdf"
                                                            Style="margin-bottom: 0px">
                                                            <Localization Select="Cargar Archivo" />
                                                        </telerik:RadAsyncUpload>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>Alerta</label></td>
                                                    <td>
                                                        <asp:CheckBox ID="chk_alertaotros" runat="server" Enabled="true" />
                                                    </td>
                                                    <td>
                                                        <label>F. Vencimiento</label></td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txt_fvencimientootros" runat="server" Enabled="true" ValidationGroup="grFamilia" DbSelectedDate='<%# System.DateTime.Now %>'>
                                                        </telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_fvencimientootros"
                                                            ErrorMessage="(*)" ValidationGroup="grFamilia">
                                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <telerik:RadButton ID="btnInsertOtros" runat="server" CommandName="PerformInsert" Text="" ValidationGroup="grTitulos" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                                        <telerik:RadButton ID="btnCancelOtros" runat="server" CommandName="Cancel" Text="" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_contabilidad" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        T Regimen</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tregimen" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_regimen" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TRTIPREG") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <label>
                                        Detalle</label>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage2"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Impuestos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Causasion">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage2" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <telerik:RadGrid ID="rg_impuestos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_impuestos_DetailTableDataBind"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_impuestos" OnItemCommand="rg_impuestos_ItemCommand">
                                    <MasterTableView DataKeyNames="PH_CODIGO" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                        HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                        UniqueName="TTDESCRI">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                        HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                                        UniqueName="PI_PORCENTAJE">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                        HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                                        UniqueName="PI_BASE">
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODIGO"
                                                UniqueName="PH_CODIGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PH_CODIGO" HeaderText="PH_CODIGO" UniqueName="TRCODNIT_LBL"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODIGO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODIGO") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="plantilla" UniqueName="PH_NOMBRE" DataTextField="PH_NOMBRE"
                                                HeaderText="Nombre" HeaderStyle-Width="250px">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="T_PLA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="T. Planilla" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_PLA"
                                                UniqueName="T_PLA" Visible="true">
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
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server">
                                <telerik:RadGrid ID="rg_cuentas" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_cuentas_ItemCommand" OnItemDataBound="rg_cuentas_ItemDataBound"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_cuentas_NeedDataSource">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CTT_ID">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                                UniqueName="PC_CODIGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                                UniqueName="PC_NOMBRE" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CTT_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTT_NATURALEZA"
                                                UniqueName="CTT_NATURALEZA" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CTT_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CTT_BASE"
                                                UniqueName="CTT_BASE" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Impuesto/Res" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                UniqueName="TTDESCRI" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <label>Cta</label></td>
                                                        <td>
                                                            <telerik:RadAutoCompleteBox runat="server" ID="ac_ctacontable" InputType="Token" TextSettings-SelectionMode="Single"
                                                                DataSourceID="obj_ctacontable" Width="450px" DataTextField="PC_NOMBRE" DropDownWidth="550px"
                                                                OnClientEntryAdding="OnClientEntryAddingHandler" DataValueField="PC_CODIGO" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>'
                                                                DropDownHeight="580px" Filter="Contains">
                                                                <DropDownItemTemplate>
                                                                    <table cellspacing="1">
                                                                        <tr>
                                                                            <td>
                                                                                <%# DataBinder.Eval(Container.DataItem, "PC_CODIGO")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# DataBinder.Eval(Container.DataItem, "PC_NOMBRE")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DropDownItemTemplate>
                                                            </telerik:RadAutoCompleteBox>
                                                        </td>
                                                        <td>
                                                            <label>Naturaleza</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_naturaleza" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                                Enabled="true" DataSourceID="obj_naturaleza" DataTextField="TTDESCRI" SelectedValue='<%# Bind("CTT_NATURALEZA") %>'
                                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <label>Base</label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chk_indbase" runat="server" Checked='<%# this.GetEstado(Eval("CTT_BASE")) %>' />
                                                        </td>
                                                        <td>
                                                            <label>Impuesto</label></td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("CTT_IMPUESTO") %>'
                                                                DataSourceID="obj_impuesto" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                ValidationGroup="gvInsert" SkinID="SkinUpdateUC" />
                                                            <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pv_nomina" runat="server">
                        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" MultiPageID="mp_nomina"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Contratos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Conceptos Planilla">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="mp_nomina" runat="server">
                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <telerik:RadGrid ID="rg_contratos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_contratos_ItemCommand"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_contratos_NeedDataSource">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="CT_ID">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="CT_ID" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_ID"
                                                UniqueName="CT_ID" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_FINGRESO" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FINGRESO"
                                                UniqueName="CT_FINGRESO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_FTERMINACION" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FTERMINACION"
                                                UniqueName="CT_FTERMINACION" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CONTRATO"
                                                UniqueName="T_CONTRATO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_NOVEDAD" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="T Novedad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_NOVEDAD"
                                                UniqueName="T_NOVEDAD" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Cargo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_CARGO"
                                                UniqueName="T_CARGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CT_SALARIO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" DataFormatString="{0:C}"
                                                HeaderText="Salario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_SALARIO"
                                                UniqueName="CT_SALARIO" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <table>
                                                    <telerik:RadTextBox ID="txt_ctid" runat="server" Enabled="true" Text='<%# Eval("CT_ID") %>' Width="300px" Visible="false">
                                                    </telerik:RadTextBox>

                                                    <tr>
                                                        <td>
                                                            <label>T Contrato</label></td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_tcontrato" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                                Enabled="true" DataSourceID="obj_tcontrato" DataTextField="TTDESCRI" SelectedValue='<%# Eval("CT_TCONTRATO") %>'
                                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="rc_tcontrato" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <label>T Novedad</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="rc_tnovedad" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                                Enabled="true" DataSourceID="obj_tnovedad" DataTextField="TTDESCRI" SelectedValue='<%# Eval("CT_TNOVEDAD") %>'
                                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="rc_tnovedad" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Cargo</label></td>
                                                        <td colspan="3">
                                                            <telerik:RadComboBox ID="rc_cargo" runat="server" Culture="es-CO" Width="600px" SelectedValue='<%# Eval("CT_CARGO") %>'
                                                                DataSourceID="obj_cargo" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_cargo" InitialValue="Seleccionar"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>F Inicio</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txt_fcontrato" runat="server" DbSelectedDate='<%# Bind("CT_FINGRESO") %>' MinDate="01/01/1900" Enabled="true">
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_fcontrato"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <label>F Final</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txt_ffinalcontrato" runat="server" DbSelectedDate='<%# Bind("CT_FTERMINACION") %>' MinDate="01/01/1900">
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Salario</label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadNumericTextBox ID="txt_salario" runat="server" DbValue='<%# Eval("CT_SALARIO") %>' Enabled="true"></telerik:RadNumericTextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_salario"
                                                                ErrorMessage="(*)" ValidationGroup="gvInsertNM">
                                                                <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                ValidationGroup="gvInsertNM" SkinID="SkinUpdateUC" />
                                                            <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView4" runat="server">
                                <telerik:RadGrid ID="rg_planillanomina" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_planillanomina_DetailTableDataBind"
                                    AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnItemCommand="rg_planillanomina_ItemCommand" OnNeedDataSource="rg_planillanomina_NeedDataSource">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="PH_CODPLAN">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <DetailTables>
                                            <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                        HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                        UniqueName="TTDESCRI">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                        HeaderText="Tipo S R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOSR"
                                                        UniqueName="TIPOSR">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                        HeaderText="Tipo % V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TIPOPV"
                                                        UniqueName="TIPOPV">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PD_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                        HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_VALOR"
                                                        UniqueName="PD_VALOR">
                                                        <ItemStyle HorizontalAlign="Right" />
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
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="PH_CODPLAN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODPLAN"
                                                UniqueName="PH_CODPLAN" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn DataField="PH_CODPLAN" HeaderText="PH_CODPLAN" UniqueName="PH_CODPLAN_lbl"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODPLAN" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODPLAN") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="PH_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_NOMBRE"
                                                UniqueName="PH_NOMBRE" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="pvHorizontal" runat="server">
                        <telerik:RadGrid ID="rg_horizontal" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_horizontal_ItemCommand" AllowFilteringByColumn="true"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_horizontal_NeedDataSource" Height="650px" AllowPaging="true" PageSize="100">
                            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                            </ClientSettings>
                            <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <CommandItemTemplate>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_cargar" runat="server" Text="Cargar Plano" Icon-PrimaryIconCssClass="rbUpload" CommandName="RebindGrid" ToolTip="Cargar Plano" />
                                </CommandItemTemplate>
                                <ColumnGroups>
                                    <telerik:GridColumnGroup Name="1" HeaderText="INFORMACION GENERAL" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="2" HeaderText="INSTALACIONES" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="3" HeaderText="COMERCIAL" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="4" HeaderText="DESMONTAJE" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="5" HeaderText="RADICACION OFICINA" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="6" HeaderText="RADICACION FINAL" HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="7" HeaderText="Estado" HeaderStyle-HorizontalAlign="Center" />
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select" ImageUrl="../App_Themes/Tema2/Images/find_.png" ButtonType="ImageButton" ColumnGroupName="1" HeaderStyle-Width="40px" />
                                    <%--<telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" ColumnGroupName="1"/>--%>

                                    <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" ColumnGroupName="1"
                                        HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" FilterControlWidth="50px"
                                        SortExpression="PH_CODIGO" UniqueName="PH_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_CTACONTRATO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="1"
                                        HeaderText="Cta Contrato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CTACONTRATO"
                                        UniqueName="PH_CTACONTRATO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_POLIZA" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="1"
                                        HeaderText="Poliza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_POLIZA"
                                        UniqueName="PH_POLIZA" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_EDIFICIO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                        HeaderText="Edificio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_EDIFICIO" FilterControlWidth="50px"
                                        UniqueName="PH_EDIFICIO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PORTAL" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                        HeaderText="Portal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PORTAL" FilterControlWidth="50px"
                                        UniqueName="PH_PORTAL" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PISO" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                        HeaderText="Piso" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PISO" FilterControlWidth="50px"
                                        UniqueName="PH_PISO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_ESCALERA" HeaderButtonType="TextButton" HeaderStyle-Width="50px" ColumnGroupName="1"
                                        HeaderText="Escalera" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA" FilterControlWidth="50px"
                                        UniqueName="PH_ESCALERA" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_OBJCONEXION" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                        HeaderText="Obj Conexion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_ESCALERA"
                                        UniqueName="PH_OBJCONEXION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_PTOSUMINISTRO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                        HeaderText="Pto Suministro" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_PTOSUMINISTRO"
                                        UniqueName="PH_PTOSUMINISTRO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_INSTALACION" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                        HeaderText="Instalacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_INSTALACION"
                                        UniqueName="PH_INSTALACION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PH_UBCAPARATO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="1"
                                        HeaderText="U. Aparato" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_UBCAPARATO"
                                        UniqueName="PH_UBCAPARATO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="2"
                                        HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="IT_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="2"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IT_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="IT_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechainst" HeaderStyle-Width="160px" ColumnGroupName="2">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txt_finstalacionrg" runat="server" DbSelectedDate='<%# Bind("IT_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="tecnico_inst" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="2"
                                        HeaderText="Tecnico" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tecnico_inst"
                                        UniqueName="tecnico_inst" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px" ColumnGroupName="2"
                                        HeaderText="Producto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                        UniqueName="ARNOMBRE" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MECDELEM" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="2"
                                        HeaderText="Serial" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MECDELEM"
                                        UniqueName="MECDELEM" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ESTADO_CO" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="3"
                                        HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ESTADO_CO"
                                        UniqueName="ESTADO_CO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="CO_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="3"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CO_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="CO_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechacom" HeaderStyle-Width="160px" ColumnGroupName="3">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txt_feccomercialrg" runat="server" DbSelectedDate='<%# Bind("CO_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="CO_USUARIO" HeaderText="Ref Tester" UniqueName="CO_USUARIO_TK" ColumnGroupName="3"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="OC_USUARIO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_cousuario" runat="server" Text='<%# Eval("CO_USUARIO") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="IT_USUARIO" HeaderText="Ref Tester" UniqueName="IT_USUARIO_TK" ColumnGroupName="3"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="IT_USUARIO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_itusuario" runat="server" Text='<%# Eval("IT_USUARIO") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="comercial" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                        HeaderText="Asesor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="comercial"
                                        UniqueName="comercial" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="3"
                                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                        UniqueName="TRCODNIT" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn DataField="CODCLI" HeaderText="Referencia" UniqueName="TRCODTER_TK" ColumnGroupName="3"
                                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="CODCLI" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codterc" runat="server" Text='<%# Eval("CODCLI") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="link" UniqueName="TRCODNIT" DataTextField="TRCODNIT" ColumnGroupName="3"
                                            HeaderText="Identificacion" HeaderStyle-Width="100px">
                                        </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                        HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                        UniqueName="TRNOMBRE" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR2" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                        HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBR2"
                                        UniqueName="TRNOMBR2" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRAPELLI" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                        HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRAPELLI"
                                        UniqueName="TRAPELLI" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR3" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="3"
                                        HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBR3"
                                        UniqueName="TRNOMBR3" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="DI_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="4"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DI_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="DI_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechadesm" HeaderStyle-Width="160px" ColumnGroupName="4">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txt_fecdesmorg" runat="server" DbSelectedDate='<%# Bind("DI_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="DI_USUARIO" HeaderText="Ref Tester" UniqueName="DI_USUARIO_TK" ColumnGroupName="4"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DI_USUARIO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_desusuario" runat="server" Text='<%# Eval("DI_USUARIO") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="tec_desistala" HeaderButtonType="TextButton" HeaderStyle-Width="150px" ColumnGroupName="4"
                                        HeaderText="Tecnico" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="tec_desistala"
                                        UniqueName="tec_desistala" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="CIH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="5"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CIH_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="CIH_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="DI_NRODOC" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="4"
                                            HeaderText="Nro Soporte" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DI_NRODOC"
                                            UniqueName="DI_NRODOC" Visible="true">
                                        </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfecradorg" HeaderStyle-Width="160px" ColumnGroupName="5">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txt_fecradorg" runat="server" DbSelectedDate='<%# Bind("CIH_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfecvantiorg" HeaderStyle-Width="160px" ColumnGroupName="6">
                                        <ItemTemplate>
                                            <telerik:RadDatePicker ID="txt_fecvantiorg" runat="server" DbSelectedDate='<%# Bind("COH_FECHA") %>' MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <%--<telerik:GridBoundColumn DataField="COH_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" ColumnGroupName="6"
                                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="COH_FECHA" DataFormatString="{0:MM/dd/yyyy}"
                                        UniqueName="COH_FECHA" Visible="true">
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="ESTADO_T" HeaderButtonType="TextButton" HeaderStyle-Width="180px" ColumnGroupName="7"
                                        HeaderText="Est Definitivo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ESTADO_T"
                                        UniqueName="ESTADO_T" Visible="true">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="PostBackBoton" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpDetalleHV" runat="server" Width="850px" Height="810px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Informacion">
                    <ContentTemplate>
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="0" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Instalacion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Comercial">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Desinstalacion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Servicios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Gestion">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_instalacionmodal" runat="server">
                                <asp:Panel runat="server" ID="pnl_hinstalacion">
                                    <telerik:RadTextBox ID="txt_phcodigo" runat="server" Enabled="false" Width="300px" Visible="false">
                                    </telerik:RadTextBox>
                                    <table>
                                        <tr>
                                            <td>
                                                <label>Fecha</label></td>
                                            <td>
                                                <telerik:RadDatePicker ID="txt_fecinstalacion" runat="server" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <label>Tecnico</label></td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_tecusuario" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="false" DataSourceID="obj_usuarios" DataTextField="usua_nombres"
                                                    DataValueField="usua_usuario" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Articulo</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_articuloinstalacion" runat="server" Enabled="false" Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Serial</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_serialinstalacion" runat="server" Enabled="false" Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Campaña</label>
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadComboBox ID="rc_campana" runat="server" Culture="es-CO" Width="380px" DataValueField="CP_ID" DataTextField="CP_NOMBRE"
                                                    Enabled="false" DataSourceID="obj_campana" ZIndex="1000000" HighlightTemplatedItems="true"
                                                    AppendDataBoundItems="true">
                                                    <HeaderTemplate>
                                                        <table style="width: 385px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">ID
                                                                </td>
                                                                <td style="width: 210px;">Nombre
                                                                </td>
                                                                <td style="width: 90px;">Precio
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width: 385px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 80px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "CP_NROCAMPANA")%>
                                                                </td>
                                                                <td style="width: 210px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "CP_NOMBRE")%>
                                                                </td>
                                                                <td style="width: 90px;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "CP_VALOR")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnl_dinstalacion">
                                    <telerik:RadGrid ID="rg_imagenesinstalacion" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_imagenesinstalacion_ItemCommand">
                                        <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="1">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="40px" DataField="IT_FOTO" Visible="true"
                                                    Resizable="true">
                                                    <ItemTemplate>
                                                        <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IT_FOTO") is DBNull ? null : Eval("IT_FOTO")%>'
                                                            AutoAdjustImageControlSize="false" Width="500px" Height="390px" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar" HeaderStyle-Width="60px" HeaderText="" />--%>
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
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_comercialmodal" runat="server">
                                <asp:Panel ID="pnl_hcomercial" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <label>F Comercial</label></td>
                                            <td>
                                                <telerik:RadDatePicker ID="txt_fcomercial" runat="server" Enabled="false">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <label>Comercial</label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_usucomercial" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="false" DataSourceID="obj_usuarios" DataTextField="usua_nombres"
                                                    DataValueField="usua_usuario" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>T Documento</label></td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_tdocumentocom" runat="server" Culture="es-CO" Width="300px"
                                                    Enabled="false" DataSourceID="obj_tipdoc" DataTextField="TTDESCRI"
                                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <label>
                                                    Identificacion</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_identificacioncom" runat="server" Enabled="false"
                                                    Width="250px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    P Nombre</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_nombrecom" runat="server" Enabled="false"
                                                    Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>
                                                    S Nombre</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_snombrecom" runat="server" Enabled="false"
                                                    Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>
                                                    P Apellido</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_apellidoscom" runat="server" Enabled="false"
                                                    Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>
                                                    S Apellido</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_sapellidocom" runat="server" Enabled="false"
                                                    Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_anucomercial" runat="server" Text="Anular" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnl_dcomercial">
                                    <telerik:RadGrid ID="rg_imgcomercial" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_imgcomercial_ItemCommand">
                                        <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="1">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="100%" DataField="IT_FOTO" Visible="true"
                                                    Resizable="true">
                                                    <ItemTemplate>
                                                        <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("EC_IMAGEN") is DBNull ? null : Eval("EC_IMAGEN")%>'
                                                            AutoAdjustImageControlSize="false" Width="95%" Height="390px" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar" HeaderStyle-Width="60px" HeaderText="" />

                                                <telerik:GridTemplateColumn HeaderText="Fecha" UniqueName="cfechainst" HeaderStyle-Width="160px" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_idcomercial" runat="server" Text='<%# Bind("EC_CODIGO") %>' Visible="false" />
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
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_desintalacion" runat="server">
                                <asp:Panel ID="pnl_hdesistalacion" runat="server">
                                    <table>
                                        <%--<tr>
                                            <td>
                                                <label>Tipo</label></td>
                                            <td></td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <label>Fecha</label></td>
                                            <td>
                                                <telerik:RadDatePicker ID="txt_fecdesitalacion" runat="server" Enabled="false" ZIndex="1000000">
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td>
                                                <label>Tecnico</label></td>
                                            <td>
                                                <telerik:RadComboBox ID="rc_tdesistala" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                    Enabled="false" DataSourceID="obj_usuarios" DataTextField="usua_nombres" Filter="StartsWith" AllowCustomText="true"
                                                    DataValueField="usua_usuario" AppendDataBoundItems="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Nro Documento</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txt_nrosoportedes" runat="server" Enabled="false"
                                                    Width="300px">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Observaciones</label></td>
                                            <td colspan="3">
                                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Width="300px" Rows="4">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chk_anudesmonte" runat="server" Text="Anular" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnl_ddesintalacion">
                                    <telerik:RadGrid ID="rg_imgdesinstalacion" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                        Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemCommand="rg_imgcomercial_ItemCommand">
                                        <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="1">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="100%" DataField="ED_IMAGEN" Visible="true"
                                                    Resizable="true">
                                                    <ItemTemplate>
                                                        <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("ED_IMAGEN") is DBNull ? null : Eval("ED_IMAGEN")%>'
                                                            AutoAdjustImageControlSize="false" Width="95%" Height="390px" />
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
                                </asp:Panel>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_servicios" runat="server">
                                <telerik:RadGrid ID="rg_servicios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="20">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                                UniqueName="usua_nombres" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TK_NUMERO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                                HeaderText="Ticket" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TK_NUMERO"
                                                UniqueName="TK_NUMERO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SV_OBSERVACIONES" HeaderButtonType="TextButton" HeaderStyle-Width="450px"
                                                HeaderText="Observaciones" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SV_OBSERVACIONES"
                                                UniqueName="SV_OBSERVACIONES" Visible="true">
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
                            <telerik:RadPageView ID="pv_gestion" runat="server">
                                <telerik:RadGrid ID="rg_gestionp" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="20">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                                HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                                UniqueName="usua_nombres" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GP_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="GP_FECHA"
                                                UniqueName="GP_FECHA">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GP_OBSERVACIONES" HeaderButtonType="TextButton" HeaderStyle-Width="450px"
                                                HeaderText="Observaciones" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="GP_OBSERVACIONES"
                                                UniqueName="GP_OBSERVACIONES" Visible="true">
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
                        <table>
                            <tr>
                                <telerik:RadButton ID="btnUpdHV" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="PostBackBoton" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" OnClick="btnUpdHV_Click" />
                                <telerik:RadButton ID="btnCanHV" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </tr>
                        </table>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFindPlantilla" runat="server" Width="800px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Planilla Impuestos">
                    <ContentTemplate>
                        <telerik:RadGrid ID="rg_impuestos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_impuestos_DetailTableDataBind"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_planillas" OnItemCommand="rg_impuestos_ItemCommand">
                            <MasterTableView DataKeyNames="PH_CODIGO">
                                <DetailTables>
                                    <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                UniqueName="TTDESCRI">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PI_PORCENTAJE" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_PORCENTAJE"
                                                UniqueName="PI_PORCENTAJE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PI_INDBASE")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="PI_BASE" HeaderButtonType="TextButton" HeaderStyle-Width="80px" DataFormatString="{0:C}"
                                                HeaderText="Base" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_BASE"
                                                UniqueName="PI_BASE">
                                                <ItemStyle HorizontalAlign="Right" />
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
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="PH_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODIGO"
                                        UniqueName="PH_CODIGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="PH_CODIGO" HeaderText="PH_CODIGO" UniqueName="TRCODNIT_LBL"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODIGO" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODIGO") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="plantilla" UniqueName="PH_NOMBRE" DataTextField="PH_NOMBRE"
                                        HeaderText="Nombre" HeaderStyle-Width="250px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="T_PLA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="T. Planilla" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="T_PLA"
                                        UniqueName="T_PLA" Visible="true">
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
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFindPlanillaNM" runat="server" Width="800px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Planilla Nomina">
                    <ContentTemplate>
                        <telerik:RadGrid ID="rg_nomina_find" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_nomina_find_DetailTableDataBind"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_planimp" OnItemCommand="rg_nomina_find_ItemCommand">
                            <MasterTableView DataKeyNames="PH_CODPLAN">
                                <DetailTables>
                                    <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="PH_CODIGO">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="PC_CODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_CODIGO"
                                                UniqueName="PC_CODIGO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PC_NOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                HeaderText="Nombre Cta" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PC_NOMBRE"
                                                UniqueName="PC_NOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                                HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                                UniqueName="TTDESCRI">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PI_NATURALEZA" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                                                HeaderText="Naturaleza" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PI_NATURALEZA"
                                                UniqueName="PI_NATURALEZA">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PD_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                                HeaderText="%" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_VALOR"
                                                UniqueName="PD_VALOR">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("PD_BASE")) %>' Enabled="false" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No se Encontaron Registros!</strong>
                                            </div>
                                        </NoRecordsTemplate>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <Columns>
                                    <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="PH_CODPLAN" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Cod." ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PH_CODPLAN"
                                        UniqueName="PH_CODPLAN" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="PH_CODPLAN" HeaderText="PH_CODPLAN" UniqueName="PH_CODPLAN_LBL"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="PH_CODPLAN" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("PH_CODPLAN") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="plantilla" UniqueName="PH_NOMBRE" DataTextField="PH_NOMBRE"
                                        HeaderText="Nombre" HeaderStyle-Width="250px">
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalCargue" runat="server" Width="700px" Height="420px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Cargue Archivos">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Cta Contrato+Poliza+Edificio+Portal+Piso+Escalera+Obj Conexion+Pto Suministro+Instlacion+Ubc Aparato+Campana(ID)</label>
                                    <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="rauCargar" ChunkSize="1048576" OnFileUploaded="rauCargar_FileUploaded" />
                                    <telerik:RadProgressArea RenderMode="Lightweight" runat="server" ID="RadProgressArea1" />
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_procesar" runat="server" Text="Procesar" ToolTip="Procesar" Icon-PrimaryIconCssClass="rbNext" OnClick="btn_procesar_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <%--<div style="display: none;">
        <asp:Button ID="Button2" runat="server" Text="Button" />
    </div>--%>
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_terceros_Inserting" OnUpdating="obj_terceros_Updating" OnInserted="obj_terceros_Inserted"
        UpdateMethod="UpdteTercero" SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL"
        InsertMethod="InsertTercero">
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TRCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCONTAC" Type="String" />
            <asp:Parameter Name="TRCODEDI" Type="Int32" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIGVER" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRDIREC2" Type="String" />
            <asp:Parameter Name="TRDELEGA" Type="String" />
            <asp:Parameter Name="TRCOLONI" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRNROFAX" Type="String" />
            <asp:Parameter Name="TRPOSTAL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRCIUDAD" Type="String" />
            <asp:Parameter Name="TRCIUDA2" Type="String" />
            <asp:Parameter Name="TRCDPAIS" Type="String" />
            <asp:Parameter Name="TRMONEDA" Type="String" />
            <asp:Parameter Name="TRIDIOMA" Type="String" />
            <asp:Parameter Name="TRBODEGA" Type="String" />
            <asp:Parameter Name="TRTERPAG" Type="String" />
            <asp:Parameter Name="TRMODDES" Type="String" />
            <asp:Parameter Name="TRTERDES" Type="String" />
            <asp:Parameter Name="TRCATEGO" Type="String" />
            <asp:Parameter Name="TRAGENTE" Type="Int32" />
            <asp:Parameter Name="TRLISPRE" Type="String" />
            <asp:Parameter Name="TRLISPRA" Type="String" />
            <asp:Parameter Name="TRDESCUE" Type="Double" />
            <asp:Parameter Name="TRCUPOCR" Type="Double" />
            <asp:Parameter Name="TRINDCLI" Type="String" />
            <asp:Parameter Name="TRINDPRO" Type="String" />
            <asp:Parameter Name="TRINDSOP" Type="String" />
            <asp:Parameter Name="TRINDEMP" Type="String" />
            <asp:Parameter Name="TRINDSOC" Type="String" />
            <asp:Parameter Name="TRINDVEN" Type="String" />
            <asp:Parameter Name="TRINDFOR" Type="String" />
            <asp:Parameter Name="TRCDCLA1" Type="String" />
            <asp:Parameter Name="TRCDCLA2" Type="String" />
            <asp:Parameter Name="TRCDCLA3" Type="String" />
            <asp:Parameter Name="TRCDCLA4" Type="String" />
            <asp:Parameter Name="TRCDCLA5" Type="String" />
            <asp:Parameter Name="TRCDCLA6" Type="String" />
            <asp:Parameter Name="TRDTTEC1" Type="String" />
            <asp:Parameter Name="TRDTTEC2" Type="String" />
            <asp:Parameter Name="TRDTTEC3" Type="String" />
            <asp:Parameter Name="TRDTTEC4" Type="String" />
            <asp:Parameter Name="TRDTTEC5" Type="Double" />
            <asp:Parameter Name="TRDTTEC6" Type="Double" />
            <asp:Parameter Name="TRPROGDT" Type="Int32" />
            <asp:Parameter Name="TRESTADO" Type="String" />
            <asp:Parameter Name="TRCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TRNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TROBSERV" Type="String" />
            <asp:Parameter Name="TRFECNAC" Type="DateTime" />
            <asp:Parameter Name="TRRESPAL" Type="String" />
            <asp:Parameter Name="TRRESCUP" Type="Double" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="TRCODZONA" Type="String" />
            <asp:Parameter Name="TRTIPREG" Type="String" />
            <asp:Parameter Name="TRGRANCT" Type="String" />
            <asp:Parameter Name="TRAUTORE" Type="String" />
            <asp:Parameter Name="TRNOMCOMERCIAL" Type="String" />
            <asp:Parameter Name="tbCuentas" Type="Object" />
            <asp:Parameter Name="tbHorizontal" Type="Object" />
            <asp:Parameter Name="original_TRCODTER" Type="Int32" DefaultValue="0" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TRCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TRCODTER" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCONTAC" Type="String" />
            <asp:Parameter Name="TRCODEDI" Type="Int32" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIGVER" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRDIREC2" Type="String" />
            <asp:Parameter Name="TRDELEGA" Type="String" />
            <asp:Parameter Name="TRCOLONI" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRNROFAX" Type="String" />
            <asp:Parameter Name="TRPOSTAL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRCIUDAD" Type="String" />
            <asp:Parameter Name="TRCIUDA2" Type="String" />
            <asp:Parameter Name="TRCDPAIS" Type="String" />
            <asp:Parameter Name="TRMONEDA" Type="String" />
            <asp:Parameter Name="TRIDIOMA" Type="String" />
            <asp:Parameter Name="TRBODEGA" Type="String" />
            <asp:Parameter Name="TRTERPAG" Type="String" />
            <asp:Parameter Name="TRMODDES" Type="String" />
            <asp:Parameter Name="TRTERDES" Type="String" />
            <asp:Parameter Name="TRCATEGO" Type="String" />
            <asp:Parameter Name="TRAGENTE" Type="Int32" />
            <asp:Parameter Name="TRLISPRE" Type="String" />
            <asp:Parameter Name="TRLISPRA" Type="String" />
            <asp:Parameter Name="TRDESCUE" Type="Double" />
            <asp:Parameter Name="TRCUPOCR" Type="Double" />
            <asp:Parameter Name="TRINDCLI" Type="String" />
            <asp:Parameter Name="TRINDPRO" Type="String" />
            <asp:Parameter Name="TRINDSOP" Type="String" />
            <asp:Parameter Name="TRINDEMP" Type="String" />
            <asp:Parameter Name="TRINDSOC" Type="String" />
            <asp:Parameter Name="TRINDVEN" Type="String" />
            <asp:Parameter Name="TRINDFOR" Type="String" />
            <asp:Parameter Name="TRCDCLA1" Type="String" />
            <asp:Parameter Name="TRCDCLA2" Type="String" />
            <asp:Parameter Name="TRCDCLA3" Type="String" />
            <asp:Parameter Name="TRCDCLA4" Type="String" />
            <asp:Parameter Name="TRCDCLA5" Type="String" />
            <asp:Parameter Name="TRCDCLA6" Type="String" />
            <asp:Parameter Name="TRDTTEC1" Type="String" />
            <asp:Parameter Name="TRDTTEC2" Type="String" />
            <asp:Parameter Name="TRDTTEC3" Type="String" />
            <asp:Parameter Name="TRDTTEC4" Type="String" />
            <asp:Parameter Name="TRDTTEC5" Type="Double" />
            <asp:Parameter Name="TRDTTEC6" Type="Double" />
            <asp:Parameter Name="TRPROGDT" Type="Int32" />
            <asp:Parameter Name="TRESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TRCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="TRNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TROBSERV" Type="String" />
            <asp:Parameter Name="TRFECNAC" Type="DateTime" />
            <asp:Parameter Name="TRRESPAL" Type="String" />
            <asp:Parameter Name="TRRESCUP" Type="Double" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="TRCODZONA" Type="String" />
            <asp:Parameter Name="TRTIPREG" Type="String" />
            <asp:Parameter Name="TRGRANCT" Type="String" />
            <asp:Parameter Name="TRAUTORE" Type="String" />
            <asp:Parameter Name="TRNOMCOMERCIAL" Type="String" />
            <asp:Parameter Name="tbFamilia" Type="Object" />
            <asp:Parameter Name="tbTitulos" Type="Object" />
            <asp:Parameter Name="tbContratos" Type="Object" />
            <asp:Parameter Name="tbPlanillaNM" Type="Object" />
            <asp:Parameter Name="tbHorizontal" Type="Object" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_sucursal" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSucursales" TypeName="XUSS.BLL.Terceros.TercerosBL" InsertMethod="InsertSucursales">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="SC_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TRCODTER" Type="Int32" DefaultValue="0" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="SC_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TRCODTER" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="SC_NOMBRE" Type="String" />
            <asp:Parameter Name="SC_TELEFONO" Type="String" />
            <asp:Parameter Name="SC_DIRECCION" Type="String" />
            <asp:Parameter Name="SC_DIRECCION2" Type="String" />
            <asp:Parameter Name="SC_PAIS" Type="String" />
            <asp:Parameter Name="SC_CIUDAD" Type="String" />
            <asp:Parameter Name="SC_ESTADO" Type="String" DefaultValue="AC" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_impuestos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetImpuestosxTercero" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="TRCODTER" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipdoc" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TIDO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_parentesco" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PARE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_nivelacademico" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="ESTU" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_profesion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PROF" />
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
    <asp:ObjectDataSource ID="obj_tpago" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerminosPago" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="InCodEmp" Type="String" SessionField="CODEMP" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_moddespacho" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="MODE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terdespacho" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEDE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_canal" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="CATE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_idioma" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="LANG" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_zona" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="ZONA" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_lstprealt" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetListaPrecioHD" TypeName="XUSS.BLL.ListaPrecios.ListaPreciosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
    <%--<asp:ObjectDataSource ID="obj_agente" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" TRINDVEN='S'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="obj_agente" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataUser" TypeName="BLL.Administracion.AdmiUsuarioBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_regimen" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TREGIMEN" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_detalle" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="DREGIMEN" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_planillas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPlanillaImpuestosHD" TypeName="XUSS.BLL.Contabilidad.PlanillaImpuestosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1 AND PH_ESTADO ='AC'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_ctacontable" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPuc" TypeName="XUSS.BLL.Contabilidad.PlanillaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_naturaleza" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="NATUR" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_impuesto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="IMPF" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_cargo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="CARGO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tnovedad" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TNOVE" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tcontrato" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TICO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_planimp" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPlanillaConceptosHD" TypeName="XUSS.BLL.Nomina.PlanillaConceptosNMBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_usuarios" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataUser" TypeName="BLL.Administracion.AdmiUsuarioBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_campana" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetCampanas" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
