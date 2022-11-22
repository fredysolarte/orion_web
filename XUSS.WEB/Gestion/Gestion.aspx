<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="XUSS.WEB.Gestion.Gestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
            .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6
            {
	            padding: 0px 10px 10px 30px;                
            }            
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
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_terceros" runat="server" PageSize="1" AllowPaging="True"
            OnItemUpdating="rlv_terceros_OnItemUpdating" OnItemCommand="rlv_terceros_OnItemCommand"
            OnItemDataBound="rlv_terceros_OnItemDataBound" OnItemInserting="rlv_terceros_OnItemInserting"
            DataSourceID="obj_terceros" ItemPlaceholderID="pnlGeneral" DataKeyNames="TRCODTER"
            DataSourceCount="0" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Gestion Prejuridica</h5>
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
                                <h5>
                                    Gestion Prejuridica</h5>
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar"  />
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />                                                                                                                        
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />                    
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir"  OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel"  ToolTip="Imprimir"/>--%>
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
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
                            <telerik:RadTab Text="Contabilidad">
                            </telerik:RadTab> 
                            <telerik:RadTab Text="Obligaciones">
                            </telerik:RadTab> 
                            <telerik:RadTab Text="Pre-Juridico">
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
                                        <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px"  AllowCustomText="true" Filter="Contains"
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
                                            Enabled="false" DataSourceID="obj_agente" DataTextField="TRNOMBRE" SelectedValue='<%# Bind("TRAGENTE") %>'
                                            DataValueField="TRCODTER" AppendDataBoundItems="true">
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
                        <telerik:RadPageView ID="pv_obligaciones" runat="server">
                            <telerik:RadGrid ID="rg_obligaciones" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_obligaciones_ItemCommand"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_obligaciones_NeedDataSource">
                                <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                    <CommandItemTemplate>                                    
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro"  />                                    
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="DD_NROOBLIGACION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Nro Obligacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DD_NROOBLIGACION"
                                            UniqueName="DD_NROOBLIGACION" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DD_DESCRIPCIN" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Producto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DD_DESCRIPCIN"
                                            UniqueName="DD_DESCRIPCIN" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DD_TCARTERA" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Tip Cart" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DD_TCARTERA"
                                            UniqueName="DD_TCARTERA" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DD_FCAPITAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="F Capital" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="DD_FCAPITAL" UniqueName="DD_FCAPITAL" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DD_FCORRIENTE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="F Corriente" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="DD_FCORRIENTE" UniqueName="DD_FCORRIENTE" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DD_FMORA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="F Mora" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="DD_FMORA" UniqueName="DD_FMORA" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SALDO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="SALDO" UniqueName="SALDO" Aggregate="Sum">
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
                        <telerik:RadPageView ID="pv_dJuridico" runat="server">
                            <telerik:RadGrid ID="rg_juridico" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_juridico_ItemCommand"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_juridico_NeedDataSource">
                                <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top">
                                    <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                    <CommandItemTemplate>                                    
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="Nuevo Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro"  />                                    
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Tipificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                            UniqueName="TTDESCRI" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PD_TELEFONO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Telefono" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_TELEFONO"
                                            UniqueName="PD_TELEFONO" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PD_EMAIL" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Email" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_EMAIL"
                                            UniqueName="PD_EMAIL" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PD_OBSERVACION" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Observacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_OBSERVACION"
                                            UniqueName="PD_OBSERVACION" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PD_FECING" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Fecha Hora" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="PD_FECING"
                                            UniqueName="PD_FECING" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                            UniqueName="usua_nombres" Visible="true">
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
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>            
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpFindPlantilla" runat="server" Width="800px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Planilla Impuestos">
                    <ContentTemplate>
                        <telerik:RadGrid ID="rg_impuestos" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnDetailTableDataBind="rg_impuestos_DetailTableDataBind"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" DataSourceID="obj_planillas" OnItemCommand="rg_impuestos_ItemCommand"  >
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpItem" runat="server" Width="850px" Height="370px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Detalle">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td><label>Tipificacion</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipificacion" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                    Enabled="true" DataSourceID="obj_tipificacion" DataTextField="TTDESCRI"  ValidationGroup="gvInsertII"
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_tipificacion" InitialValue="Seleccionar"
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertII">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Nro Telefono</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" 
                                            Width="300px" ValidationGroup="gvInsertII">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_telefono" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertII">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Email</label>
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_email" runat="server" Enabled="true"  Width="300px">
                                        </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Observacion</label></td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" ValidationGroup="gvInsertII"
                                                            Width="550px" TextMode="MultiLine" Height="150px">
                                                        </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_observaciones" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertII">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd"  ToolTip="Agregar" ValidationGroup="gvInsertII" OnClick="btn_agregar_Click"/>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpObligacion" runat="server" Width="850px" Height="370px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Detalle">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td><label>Nro Obligacion</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroobligacion" runat="server" Enabled="true" 
                                            Width="300px" ValidationGroup="gvInsertIII">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_nroobligacion" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <label>Producto</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_producto" runat="server" Enabled="true" 
                                            Width="300px" ValidationGroup="gvInsertIII">
                                    </telerik:RadTextBox>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_producto" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>T Cartera</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_tcartera" runat="server" Enabled="true"  Width="300px" ValidationGroup="gvInsertIII">
                                        </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_tcartera" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            
                                <td><label>Descripcion</label></td>
                                <td >
                                    <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true"  Width="300px" ValidationGroup="gvInsertIII">
                                        </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_descripcion" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Dias Mora</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_dmora" runat="server" ValidationGroup="gvInsertIII"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_dmora" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            
                                <td><label>F Capital</label></td>
                                <td >
                                    <telerik:RadNumericTextBox ID="txt_fcapital" runat="server" ValidationGroup="gvInsertIII"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_fcapital" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>F Corriente</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_fcorriente" runat="server" ValidationGroup="gvInsertIII"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_fcorriente" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            
                                <td><label>F Mora</label></td>
                                <td >
                                    <telerik:RadNumericTextBox ID="txt_fmora" runat="server" ValidationGroup="gvInsertIII"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_fmora" 
                                                    ErrorMessage="(*)" ValidationGroup="gvInsertIII">
                                                    <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_guardarobligacion" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd"  ToolTip="Agregar" ValidationGroup="gvInsertIII" OnClick="btn_guardarobligacion_Click"/>
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
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_terceros_Inserting" OnUpdating="obj_terceros_Updating"
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
            <asp:Parameter Name="TRESTADO" Type="String"/>
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
            <asp:Parameter Name="tbCuentas" Type="Object" />
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
        SelectMethod="GetImpuestosxTercero" TypeName="XUSS.BLL.Terceros.TercerosBL" >
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
    <asp:ObjectDataSource ID="obj_agente" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" TRINDVEN='S'" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
        SelectMethod="GetPlanillaImpuestosHD" TypeName="XUSS.BLL.Contabilidad.PlanillaImpuestosBL" >
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
    <asp:ObjectDataSource ID="obj_tipificacion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TIPIF" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
