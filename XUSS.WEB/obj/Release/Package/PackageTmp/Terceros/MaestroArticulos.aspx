<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MaestroArticulos.aspx.cs" Inherits="XUSS.WEB.Terceros.MaestroArticulos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">     
            function OnClientAdded(sender, args) {
                var allowedMimeTypes = $telerik.$(sender.get_element()).attr("data-clientFilter");
                $telerik.$(args.get_row()).find(".ruFileInput").attr("accept", allowedMimeTypes);
            }
            function conditionalPostback(sender, args) {
                console.log(args.get_eventTarget());
                //debugger;
                if (args.EventTarget.indexOf("rgSoportes") != -1) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }

            (function (global, undefined) {
                var demo = {};

                function OnClientFilesUploaded(sender, args) {
                    //debugger;
                    //$find(demo.ajaxManagerID).ajaxRequest();

                    var panel = $find("<%= RadAjaxPanel2.ClientID%>");
                    panel.ajaxRequestWithTarget(panel.ClientID, "LoadImage");
                    var eItem = $find('<%= rlv_articulos.ClientID %>');
                    panel.ajaxRequestWithTarget(eItem.ClientID, "LoadImage");
                    var grid = $telerik.findControl(eItem.get_element().parentNode, "rg_imagenes");
                    panel.ajaxRequestWithTarget(grid.ClientID, "LoadImage");
                    var editor = $telerik.findControl(grid.get_element().parentNode, "RadImageEditor1");

                    editor.fire("Reset", null);
                    panel.ajaxRequestWithTarget(editor.ClientID, "LoadImage1");
                }

                function serverID(name, id) {
                    demo[name] = id;
                }

                global.serverID = serverID;

                global.OnClientFilesUploaded = OnClientFilesUploaded;
            })(window);

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="100000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadListView ID="rlv_articulos" runat="server" PageSize="1" AllowPaging="True"
            DataSourceID="obj_articulos" ItemPlaceholderID="pnlGeneral" OnItemInserted="rlv_articulos_OnItemInserted" OnItemInserting="rlv_articulos_ItemInserting" OnItemUpdating="rlv_articulos_ItemUpdating"
            OnItemCommand="rlv_articulos_ItemCommand" OnItemDataBound="rlv_articulos_ItemDataBound" OnItemUpdated="rlv_articulos_OnItemUpdated">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Maestro Articulos</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_articulos" RenderMode="Lightweight"
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
                                <h5>Maestro Articulos</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
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
                                    <label>Categoria</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_categoria_find" runat="server" OnSelectedIndexChanged="rc_categoria_find_SelectedIndexChanged"
                                        Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE" AutoPostBack="true"
                                        Enabled="true" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Referencia</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Nombre</label></td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>C. Barras</label></td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_fbarras" runat="server" Enabled="true"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>R. Proveedor</label></td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="txt_refproveedor" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label runat="server" id="lbl_clasificacion1" Text="Clasificacion 1"/>
                                </td>                                                                
                                <td>
                                    <telerik:RadComboBox ID="rc_dt1" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                        Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" id="lbl_clasificacion2" Text="Clasificacion 2"/>
                                </td>                                                                                                
                                <td>
                                    <telerik:RadComboBox ID="rc_dt2" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                        Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" id="lbl_clasificacion3" Text="Clasificacion 3"/>
                                </td>                                                                                                                                
                                <td>
                                    <telerik:RadComboBox ID="rc_dt3" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                        Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" id="lbl_clasificacion4" Text="Clasificacion 4"  />
                                </td>                                
                                <td>
                                    <telerik:RadComboBox ID="rc_dt4" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                        Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" id="lbl_clasificacion5" Text="Clasificacion 5"/>
                                </td>                                
                                <td>
                                    <telerik:RadComboBox ID="rc_dt5" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                        Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aplicar Filtro" RenderMode="Lightweight">
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <label>Categoria</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_categoria" runat="server" SelectedValue='<%# Bind("ARTIPPRO") %>'
                                    Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                    Enabled="false" DataValueField="TATIPPRO">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Referencia</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Text='<%# Bind("ARCLAVE1") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("ARNOMBRE") %>'
                                    Width="600px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("ARESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Descontinuado" Value="DE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="3" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Datos Basicos" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Datos Tecnicos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Cod Barras">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Categorizacion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Imagenes">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Lst Precios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="R Sanitarios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Origen">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Tester">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_datos" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad" runat="server" SelectedValue='<%# Bind("ARUNDINV") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Proveedor</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="false" AppendDataBoundItems="true" 
                                                SelectedValue='<%# Bind("ARCODPRO") %>' DataTextField="TRNOMBRE" Width="300px" DataSourceID="obj_proveedor">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccione" Value="0" />
                                                </Items>

                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 1</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad1" runat="server" SelectedValue='<%# Bind("ARUMALT1") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTDESCRI"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Pais Origen</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_porigen" runat="server" SelectedValue='<%# Bind("ARORIGEN") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 2</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad2" runat="server" SelectedValue='<%# Bind("ARUMALT2") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTDESCRI"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Un Arancelaria</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_posaracelaria" Text='<%# Bind("ARPOSARA") %>' runat="server" Enabled="false" Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Procedencia</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_procedencia" runat="server" SelectedValue='<%# Bind("TR_PROCEDENCIA") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_procedencia" DataTextField="TTDESCRI"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Coleccion</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_coleccion" runat="server" SelectedValue='<%# Bind("ARCOLECCION") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_coleccion" DataTextField="TTDESCRI"
                                                Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Moneda Costos</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARMONEDA") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Standar</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctstandar" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARCOSTOA") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Prima</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mprima" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARCSTMPR") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Obra</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mobra" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARCSTMOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct CIF</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctcif" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARCSTCIF") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Simulador</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctsimulador" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARCOSTOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <label>Precio Vta</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="false"
                                                DbValue='<%# Bind("ARPRECIO") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Convenio</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_convenio" runat="server" Enabled="false"
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Impuesto</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="false" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARCDIMPF") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_dttec" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 1</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA1") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 1</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico1" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC1") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt1" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 2</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA2") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 2</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico2" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC2") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt2" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 3</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA3") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 3</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico3" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC3") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt3" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 4</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA4") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 4</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico4" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC4") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt4" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 5</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA5") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 5</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico5" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC5") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt5" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico6" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 7</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA7") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 7</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico7" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC7") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt7" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 8</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA8") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 8</label></td>
                                        <td>
                                            <%--<telerik:RadTextBox ID="txt_dtecnico8" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC8") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>--%>
                                            <telerik:RadComboBox ID="rc_dt8" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="false" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico9" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="false" Text='<%# Bind("ARCDCLA10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico10" runat="server" Enabled="false" Text='<%# Bind("ARDTTEC10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_barras" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Cod Barras" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BCODIGO"
                                                UniqueName="BCODIGO">
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
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_categorizacion" runat="server">
                                <telerik:RadPanelBar runat="server" ID="pnl_claves" Height="100%" Skin="MetroTouch" Width="100%">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Text="Clave 2">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave2" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE2"
                                                                UniqueName="ARCLAVE2">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
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
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 3">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave3" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE3"
                                                                UniqueName="ARCLAVE3">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
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
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 4">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave4" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE4"
                                                                UniqueName="ARCLAVE4">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
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
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelBar>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_imagenes" runat="server">
                                <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="40px" DataField="IMCLAVE1" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IM_IMAGEN") is DBNull ? null : Eval("IM_IMAGEN")%>'
                                                        AutoAdjustImageControlSize="false" Width="500px" Height="500px" ToolTip='<%#Eval("IM_CLAVE1", "Foto {0}") %>'
                                                        AlternateText='<%#Eval("IM_CLAVE1", "Foto {0}") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Tipo" HeaderStyle-Width="40px" DataField="IMCLAVE1" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox ID="rcbMarca" runat="server" SelectedValue='<%# Bind("IM_TIPIMA") %>'
                                                        Culture="es-CO" Width="200px" Enabled="False" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                                            <telerik:RadComboBoxItem Text="Fotografia" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Dibujo" Value="2" />
                                                        </Items>
                                                    </telerik:RadComboBox>
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
                            <telerik:RadPageView ID="pv_precios" runat="server">
                                <telerik:RadGrid ID="rg_precios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="P_CNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Lista Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_CNOMBRE"
                                                UniqueName="P_CNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn DataField="P_RPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                AllowFiltering="false" DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="P_RPRECIO">
                                            </telerik:GridNumericColumn>
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
                            <telerik:RadPageView ID="pv_rsanitarios" runat="server">
                                <telerik:RadGrid ID="rg_rsanitarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="RS_REGISTRO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="R Sanitario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_REGISTRO"
                                                UniqueName="RS_REGISTRO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RS_FEINICIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_FEINICIO"
                                                UniqueName="RS_FEINICIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RS_FECFINAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_FECFINAL"
                                                UniqueName="RS_FECFINAL">
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
                            <telerik:RadPageView ID="pv_soportes" runat="server">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView>
                                        <CommandItemSettings ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
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
                            <telerik:RadPageView ID="pv_origen" runat="server">
                                <telerik:RadGrid ID="rg_origen" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_origen"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_origen_OnNeedDataSource">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="OR_REFERENCIA" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="150px" HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="OR_REFERENCIA" UniqueName="OR_REFERENCIA" Visible="true">
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
                            <telerik:RadPageView ID="pv_tester" runat="server">
                                <telerik:RadGrid ID="rgTester" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgTester"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgTester_OnNeedDataSource">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="50px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="BCODIGO" UniqueName="BCODIGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="Linea" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TANOMBRE" UniqueName="TANOMBRE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TT_CLAVE1" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="Ref" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TT_CLAVE1" UniqueName="TT_CLAVE1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="100px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARNOMBRE" UniqueName="ARNOMBRE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC1" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 1" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC1" UniqueName="ARDTTEC1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC2" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 2" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC2" UniqueName="ARDTTEC2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC3" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 3" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC3" UniqueName="ARDTTEC3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC4" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 4" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC4" UniqueName="ARDTTEC4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC5" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 5" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC5" UniqueName="ARDTTEC4">
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
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <label>Categoria</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_categoria" runat="server" SelectedValue='<%# Bind("ARTIPPRO") %>'
                                    Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                    Enabled="false" DataValueField="TATIPPRO">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Referencia</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Text='<%# Bind("ARCLAVE1") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("ARNOMBRE") %>'
                                    Width="600px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_nombre" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("ARESTADO") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Descontinuado" Value="DE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_estado" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="3" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Datos Basicos" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Datos Tecnicos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Cod Barras">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Categorizacion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Imagenes">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Lst Precios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="R Sanitarios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Origen">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Tester">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_datos" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad" runat="server" SelectedValue='<%# Bind("ARUNDINV") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTVALORC" AppendDataBoundItems="true"
                                                Enabled="true" DataValueField="TTCODCLA" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Proveedor</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" AppendDataBoundItems="true"
                                                SelectedValue='<%# Bind("ARCODPRO") %>' DataTextField="TRNOMBRE" Width="300px" DataSourceID="obj_proveedor" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccione" Value="0" />
                                                </Items>

                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 1</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad1" runat="server" SelectedValue='<%# Bind("ARUMALT1") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Pais Origen</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_porigen" runat="server" SelectedValue='<%# Bind("ARORIGEN") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 2</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad2" runat="server" SelectedValue='<%# Bind("ARUMALT2") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Un Arancelaria</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_posaracelaria" Text='<%# Bind("ARPOSARA") %>' runat="server" Enabled="true" Width="300px">
                                            </telerik:RadTextBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_barancel" OnClick="btn_barancel_Click" runat="server" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Buscar Unidad Arancelaria" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Procedencia</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_procedencia" runat="server" SelectedValue='<%# Bind("TR_PROCEDENCIA") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_procedencia" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Coleccion</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_coleccion" runat="server" SelectedValue='<%# Bind("ARCOLECCION") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_coleccion" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Moneda Costos</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARMONEDA") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Standar</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctstandar" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCOSTOA") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Prima</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mprima" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTMPR") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Obra</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mobra" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTMOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct CIF</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctcif" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTCIF") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Simulador</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctsimulador" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCOSTOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <label>Precio Vta</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARPRECIO") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Convenio</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_convenio" runat="server" Enabled="true"
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Impuesto</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="true" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARCDIMPF") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_estado" ValidationGroup="gvInsert"
                                                ErrorMessage="(*)" InitialValue="Seleccionar">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_dtecnicos" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 1</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA1") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 1</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt1" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains" >
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt1" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt1_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 2</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA2") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 2</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt2" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt2" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt2_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 3</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA3") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 3</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt3" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt3" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt3_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 4</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA4") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 4</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt4" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt4" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt4_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 5</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA5") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 5</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt5" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt5" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt5_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico6" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 7</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA7") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 7</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt7" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_rfsdt7" runat="server" Text="" ToolTip="Refrescar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="insrf" OnClick="btn_rfsdt7_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 8</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA8") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 8</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt8" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico9" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico10" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_barras" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource" OnDeleteCommand="rg_items_DeleteCommand" OnItemDataBound="rg_items_ItemDataBound"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true" 
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <Columns>
                                            <%--<telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif"  ButtonType="ImageButton" />--%>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">                                                
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Cod Barras" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BCODIGO"
                                                UniqueName="BCODIGO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Cod. Barras</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Text='<%# Bind("BCODIGO") %>'
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_barras" ValidationGroup="gvInsertB"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <label>C2</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_clave2bar" runat="server" 
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>C3</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_clave3bar" runat="server" 
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptarbarras" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="gvInsertB" SkinID="SkinUpdateUC" OnClick="btn_aceptarbarras_Click" />

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
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_claves" runat="server">
                                <telerik:RadPanelBar runat="server" ID="pnl_claves" Height="100%" Skin="MetroTouch" Width="100%" ExpandMode="MultipleExpandedItems" RenderMode="Classic">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Text="Clave 2">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave2" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_clave2_OnNeedDataSource"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemDataBound="rg_clave2_OnItemDataBound">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE2"
                                                                UniqueName="ARCLAVE2">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <FormTemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txt_alterna2" runat="server" Enabled="true" Width="600px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadComboBox ID="rc_alterna2" runat="server"
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
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
                                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                <strong>Alerta!</strong>No Tiene Registros
                                                            </div>
                                                        </NoRecordsTemplate>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 3">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave3" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemDataBound="rg_clave3_OnItemDataBound"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_clave3_OnNeedDataSource">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE3"
                                                                UniqueName="ARCLAVE3">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <FormTemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txt_alterna3" runat="server" Enabled="true" Width="600px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadComboBox ID="rc_alterna3" runat="server"
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_aceptar_c3" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_c3_OnClick" />
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
                                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                <strong>Alerta!</strong>No Tiene Registros
                                                            </div>
                                                        </NoRecordsTemplate>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 4">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave4" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE4"
                                                                UniqueName="ARCLAVE4">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
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
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelBar>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_imagenes" runat="server">
                                <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_imagenes_OnItemCommand" OnDeleteCommand="rg_imagenes_DeleteCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_imagenes_OnNeedDataSource">
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowPaging="true" PageSize="10" DataKeyNames="IM_CONSECUTIVO">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_attach" runat="server" Text="Adjunto" Icon-PrimaryIconCssClass="rbAttach" CommandName="InitInsert" ToolTip="Nueva Adjunto" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />

                                            <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="40px" DataField="IMCLAVE1" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IM_IMAGEN") is DBNull ? null : Eval("IM_IMAGEN")%>'
                                                        AutoAdjustImageControlSize="false" Width="500px" Height="500px" ToolTip='<%#Eval("IM_CLAVE1", "Foto {0}") %>'
                                                        AlternateText='<%#Eval("IM_CLAVE1", "Foto {0}") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server" OnClientAdded="OnClientAdded"
                                                                    OnClientFilesUploaded="OnClientFilesUploaded" OnFileUploaded="AsyncUpload1_FileUploaded"
                                                                    MaxFileSize="2097152" AllowedFileExtensions="jpg,png,gif,bmp"
                                                                    AutoAddFileInputs="false" Localization-Select="Upload Image" />
                                                                <asp:Label ID="Label1" Text="*Size limit: 2MB" runat="server" Style="font-size: 10px;"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadButton ID="btn_guardar_img" runat="server" Text="Aceptar" CommandName="PerformInsert" Icon-PrimaryIconCssClass="rbSave"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" RenderMode="Lightweight" />
                                                                <telerik:RadButton ID="btn_cancel" runat="server" Text="Cancelar" CommandName="Cancel" Icon-PrimaryIconCssClass="rbCancel"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" RenderMode="Lightweight" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <telerik:RadImageEditor RenderMode="Lightweight" ID="RadImageEditor1" runat="server" Width="100%" AllowedSavingLocation="Server"
                                                                    OnImageLoading="RadImageEditor1_ImageLoading" Height="430px">
                                                                </telerik:RadImageEditor>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </FormTemplate>
                                        </EditFormSettings>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_precios" runat="server">
                                <telerik:RadGrid ID="rg_precios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemDataBound ="rg_precios_ItemDataBound"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_precios_OnNeedDataSource">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif" ButtonType="ImageButton" />
                                            <telerik:GridBoundColumn DataField="P_CNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Lista Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_CNOMBRE"
                                                UniqueName="P_CNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn DataField="P_RPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                AllowFiltering="false" DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="P_RPRECIO">
                                            </telerik:GridNumericColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Lst Precio
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("P_RLISPRE") %>'
                                                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE"
                                                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>C2</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_clave2pre" runat="server" 
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>C3</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_clave3pre" runat="server" 
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>Precio</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="250px" Enabled="true"
                                                                    DbValue='<%# Bind("P_RPRECIO") %>' EnabledStyle-HorizontalAlign="Right">
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptar_pre" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_pre_OnClick" />
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
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_rsanitarios" runat="server">
                                <telerik:RadGrid ID="rg_rsanitarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_rsanitarios_NeedDataSource" OnDeleteCommand="rg_rsanitarios_DeleteCommand">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif" ButtonType="ImageButton" />
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridTemplateColumn DataField="RS_CODIGO" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="RS_CODIGO" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txt_codigors" runat="server" Text='<%# Eval("RS_CODIGO") %>' Visible="false"></telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="RS_REGISTRO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="R Sanitario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_REGISTRO"
                                                UniqueName="RS_REGISTRO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RS_FEINICIO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_FEINICIO"
                                                UniqueName="RS_FEINICIO">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RS_FECFINAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                                HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="RS_FECFINAL"
                                                UniqueName="RS_FECFINAL">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadTextBox runat="server" ID="txt_id" Width="250px" Enabled="true" Text='<%# Bind("RS_CODIGO") %>' EnabledStyle-HorizontalAlign="Right" Visible="false">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    F Inicial
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="edt_finicial" runat="server" Enabled="true" ValidationGroup="gvInsertRS" DbSelectedDate='<%# Bind("RS_FEINICIO") %>'>
                                                                </telerik:RadDatePicker>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="edt_finicial" ValidationGroup="gvInsertRS"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <label>
                                                                    F Final
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadDatePicker ID="edt_ffinal" runat="server" Enabled="true" ValidationGroup="gvInsertRS" DbSelectedDate='<%# Bind("RS_FECFINAL") %>'>
                                                                </telerik:RadDatePicker>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="edt_ffinal" ValidationGroup="gvInsertRS"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>R Sanitario</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox runat="server" ID="txt_rsanitario" Width="250px" Enabled="true" Text='<%# Bind("RS_REGISTRO") %>'
                                                                    EnabledStyle-HorizontalAlign="Right" ValidationGroup="gvInsertRS">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_rsanitario" ValidationGroup="gvInsertRS"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptar_rs" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="gvInsertRS" SkinID="SkinUpdateUC" OnClick="btn_aceptar_rs_Click" CausesValidation="true" />
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
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_soportes" runat="server">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Descripcion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="edt_nombre" runat="server" Enabled="true" Width="350px">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Archivo</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadAsyncUpload ID="rauCargarSoporte" runat="server" ControlObjectsVisibility="None"
                                                                    Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                                    Width="350px" OnFileUploaded="rauCargarSoporte_FileUploaded"
                                                                    Style="margin-bottom: 0px">
                                                                    <Localization Select="Cargar Archivo" />
                                                                </telerik:RadAsyncUpload>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btn_aceptarSoporte" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="grNuevo" OnClick="btn_aceptarSoporte_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
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
                            <telerik:RadPageView ID="pv_origen" runat="server">
                                <telerik:RadGrid ID="rg_origen" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_origen"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_origen_OnNeedDataSource" OnDeleteCommand="rg_origen_DeleteCommand">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridTemplateColumn DataField="OR_CODIGO" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                                                HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="OR_CODIGO" Visible="false">
                                                <ItemTemplate>
                                                    <telerik:RadTextBox ID="txt_codigoor" runat="server" Text='<%# Eval("OR_CODIGO") %>' Visible="false"></telerik:RadTextBox>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="OR_REFERENCIA" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="150px" HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="OR_REFERENCIA" UniqueName="OR_REFERENCIA" Visible="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Referencia</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_origenreferencia" runat="server" Enabled="true" Text='<%# Bind("OR_REFERENCIA") %>'
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_origenreferencia" ValidationGroup="gvInsertBR"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btn_aceptorigen" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="gvInsertBR" OnClick="btn_aceptorigen_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
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
                            <telerik:RadPageView ID="pv_tester" runat="server">
                                <telerik:RadGrid ID="rgTester" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgTester"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgTester_OnNeedDataSource" OnDeleteCommand="rgTester_DeleteCommand">
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="TT_CODIGO">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="50px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="BCODIGO" UniqueName="BCODIGO" Visible="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="Linea" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TANOMBRE" UniqueName="TANOMBRE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TT_CLAVE1" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="Ref" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="TT_CLAVE1" UniqueName="TT_CLAVE1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="100px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARNOMBRE" UniqueName="ARNOMBRE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC1" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 1" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC1" UniqueName="ARDTTEC1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC2" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 2" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC2" UniqueName="ARDTTEC2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC3" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 3" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC3" UniqueName="ARDTTEC3">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC4" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 4" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC4" UniqueName="ARDTTEC4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARDTTEC5" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="80px" HeaderText="DT 5" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="ARDTTEC5" UniqueName="ARDTTEC4">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Linea</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_tptester" runat="server" Enabled="true" Text='<%# Bind("TT_TIPPRO") %>' Width="300px" Visible="false">
                                                                </telerik:RadTextBox>
                                                                <telerik:RadTextBox ID="txt_nmtester" runat="server" Enabled="true" Width="300px">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_tptester" ValidationGroup="gvInsertTS"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                <label>Nombre</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_c1tester" runat="server" Enabled="true" Text='<%# Bind("TT_CLAVE1") %>' Width="300px" Visible="false">
                                                                </telerik:RadTextBox>
                                                                <telerik:RadTextBox ID="txt_destester" runat="server" Enabled="true" Width="300px">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_c1tester" ValidationGroup="gvInsertTS"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <%--<td>
                                                                <asp:Button ID="btn_aceptartester" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="grNuevo" OnClick="btn_aceptartester_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptartester" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="gvInsertTS" SkinID="SkinUpdateUC" OnClick="btn_aceptartester_OnClick" />
                                                                <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                                    CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
                                                        <Shortcuts>
                                                            <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                                                            <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                                                            <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
                                                        </Shortcuts>
                                                        <Windows>
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
                                                                                <telerik:RadButton ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Filtrar" RenderMode="Lightweight"
                                                                                    CommandName="xxxxxx" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <telerik:RadGrid ID="rgConsultaArticulos" runat="server" Width="100%"
                                                                            AutoGenerateColumns="False" AllowPaging="True" PageSize="20" CellSpacing="0" GridLines="None"
                                                                            DataSourceID="obj_tester" OnItemCommand="rgConsultaArticulos_OnItemCommand">
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
                                                                                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                                        <strong>Alerta!</strong>No Existen Registros
                                                                                    </div>
                                                                                </NoRecordsTemplate>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </telerik:RadWindow>
                                                        </Windows>
                                                    </telerik:RadWindowManager>
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
                        </telerik:RadMultiPage>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table cellspacing="1">
                        <tr>
                            <td>
                                <label>Categoria</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_categoria" runat="server" SelectedValue='<%# Bind("ARTIPPRO") %>' OnSelectedIndexChanged="OnSelectedIndexChanged_rc_categoria" AutoPostBack="true"
                                    Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE" AppendDataBoundItems="true" Filter="StartsWith"
                                    Enabled="true" DataValueField="TATIPPRO">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_categoria" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <label>Referencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Text='<%# Bind("ARCLAVE1") %>' OnTextChanged="txt_referencia_OnTextChanged" AutoPostBack="true"
                                    Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_referencia" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nombre</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("ARNOMBRE") %>'
                                    Width="600px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_nombre" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td colspan="3">
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("ARESTADO") %>'
                                    Enabled="true" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Descontinuado" Value="DE" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_estado" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                            SelectedIndex="3" CssClass="tabStrip">
                            <Tabs>
                                <telerik:RadTab Text="Datos Basicos" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Datos Tecnicos">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Cod Barras">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Categorizacion">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Imagenes">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Lst Precios">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Soportes">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="pv_datos" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad" runat="server" SelectedValue='<%# Bind("ARUNDINV") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Proveedor</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_proveedor" runat="server" DataValueField="TRCODTER" Enabled="true" AppendDataBoundItems="true" Filter="StartsWith"
                                                SelectedValue='<%# Bind("ARCODPRO") %>' DataTextField="TRNOMBRE" Width="300px" DataSourceID="obj_proveedor" >
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccione" Value="0" />
                                                </Items>

                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 1</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad1" runat="server" SelectedValue='<%# Bind("ARUMALT1") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Pais Origen</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_porigen" runat="server" SelectedValue='<%# Bind("ARORIGEN") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_porigen" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Unidad Inv 2</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_unidad2" runat="server" SelectedValue='<%# Bind("ARUMALT2") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_unidad" DataTextField="TTVALORC"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true" Filter="StartsWith">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Un Arancelaria</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_posaracelaria" Text='<%# Bind("ARPOSARA") %>' runat="server" Enabled="true" Width="300px">
                                            </telerik:RadTextBox>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_barancel" OnClick="btn_barancel_Click" runat="server" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Buscar Unidad Arancelaria" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Procedencia</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_procedencia" runat="server" SelectedValue='<%# Bind("TR_PROCEDENCIA") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_procedencia" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <label>Coleccion</label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_coleccion" runat="server" SelectedValue='<%# Bind("ARCOLECCION") %>'
                                                Culture="es-CO" Width="300px" DataSourceID="obj_coleccion" DataTextField="TTDESCRI"
                                                Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Moneda Costos</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARMONEDA") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Standar</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctstandar" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCOSTOA") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Prima</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mprima" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTMPR") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct M Obra</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="ct_mobra" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTMOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct CIF</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctcif" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCSTCIF") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Ct Simulador</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_ctsimulador" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARCOSTOB") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td>
                                            <label>Precio Vta</label></td>
                                        <td>
                                            <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="true"
                                                DbValue='<%# Bind("ARPRECIO") %>' EnabledStyle-HorizontalAlign="Right">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Convenio</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_convenio" runat="server" Enabled="true"
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Impuesto</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_impuesto" runat="server" Culture="es-CO" Width="300px"
                                                Enabled="true" DataSourceID="obj_impuesto" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ARCDIMPF") %>'
                                                DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_impuesto" ValidationGroup="gvInsert"
                                                ErrorMessage="(*)" InitialValue="Seleccionar">
                                                <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 1</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA1") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 1</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt1" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 2</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA2") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 2</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt2" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 3</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA3") %>' 
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 3</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt3" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 4</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA4") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 4</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt4" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 5</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA5") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 5</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt5" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true" Filter="Contains">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 6</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico6" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC6") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 7</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA7") %>' 
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 7</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt7" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" Filter="Contains"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 8</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA8") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 8</label></td>
                                        <td>
                                            <telerik:RadComboBox ID="rc_dt8" runat="server" Culture="es-CO" Width="300px" DataTextField="ASNOMBRE" Filter="Contains"
                                                Enabled="true" DataValueField="ASCLAVEO" AppendDataBoundItems="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 9</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico9" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC9") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Clasificacion 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("ARCDCLA10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td>
                                            <label>Dato Tecnico 10</label></td>
                                        <td>
                                            <telerik:RadTextBox ID="txt_dtecnico10" runat="server" Enabled="true" Text='<%# Bind("ARDTTEC10") %>'
                                                Width="300px">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server">
                                <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif" ButtonType="ImageButton" />
                                            <telerik:GridBoundColumn DataField="BCODIGO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Cod Barras" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BCODIGO"
                                                UniqueName="BCODIGO">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>Cod. Barras</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Text='<%# Bind("BCODIGO") %>'
                                                                    Width="300px">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_barras" ValidationGroup="gvInsertB"
                                                                    ErrorMessage="(*)">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptarbarras" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="gvInsertB" SkinID="SkinUpdateUC" OnClick="btn_aceptarbarras_Click" />
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
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView3" runat="server">
                                <telerik:RadPanelBar runat="server" ID="pnl_claves" Height="100%" Skin="MetroTouch" Width="100%" ExpandMode="MultipleExpandedItems" RenderMode="Classic">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Text="Clave 2">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave2" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_clave2_OnNeedDataSource"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnItemDataBound="rg_clave2_OnItemDataBound">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE2"
                                                                UniqueName="ARCLAVE2">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <FormTemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txt_alterna2" runat="server" Enabled="true" Width="600px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadComboBox ID="rc_alterna2" runat="server"
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_OnClick" />
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
                                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                <strong>Alerta!</strong>No Tiene Registros
                                                            </div>
                                                        </NoRecordsTemplate>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 3">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave3" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemDataBound="rg_clave3_OnItemDataBound"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_clave3_OnNeedDataSource">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE3"
                                                                UniqueName="ARCLAVE3">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <FormTemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <telerik:RadTextBox ID="txt_alterna3" runat="server" Enabled="true" Width="600px">
                                                                                </telerik:RadTextBox>
                                                                                <telerik:RadComboBox ID="rc_alterna3" runat="server"
                                                                                    Culture="es-CO" Width="300px" Enabled="true">
                                                                                </telerik:RadComboBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btn_aceptar_c3" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_c3_OnClick" />
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
                                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                                <strong>Alerta!</strong>No Tiene Registros
                                                            </div>
                                                        </NoRecordsTemplate>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                        <telerik:RadPanelItem runat="server" Text="Clave 4">
                                            <ItemTemplate>
                                                <telerik:RadGrid ID="rg_clave4" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                    </ClientSettings>
                                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Codigo" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE4"
                                                                UniqueName="ARCLAVE4">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="ASNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ASNOMBRE"
                                                                UniqueName="ASNOMBRE">
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
                                            </ItemTemplate>
                                        </telerik:RadPanelItem>
                                    </Items>
                                </telerik:RadPanelBar>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView5" runat="server">
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView4" runat="server">
                                <telerik:RadGrid ID="rg_precios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_precios_OnNeedDataSource">
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                                        <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn HeaderStyle-Width="40px" EditImageUrl="../App_Themes/Tema2/Images/Edit_.gif" ButtonType="ImageButton" />
                                            <telerik:GridBoundColumn DataField="P_CNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="Lista Precio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="P_CNOMBRE"
                                                UniqueName="P_CNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridNumericColumn DataField="P_RPRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                                                AllowFiltering="false" DataFormatString="&lt;strong&gt;{0:C}&lt;/strong&gt;" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="P_RPRECIO">
                                            </telerik:GridNumericColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Lst Precio
                                                                </label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("P_RLISPRE") %>'
                                                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE"
                                                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <%-- </tr>
                                                        <tr>--%>
                                                            <td>
                                                                <label>Precio</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="250px" Enabled="true"
                                                                    DbValue='<%# Bind("P_RPRECIO") %>' EnabledStyle-HorizontalAlign="Right">
                                                                </telerik:RadNumericTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btn_aceptar_pre" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" OnClick="btn_aceptar_pre_OnClick" />
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
                                                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                                <strong>Alerta!</strong>No Tiene Registros
                                            </div>
                                        </NoRecordsTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="pv_soportes" runat="server">
                                <telerik:RadGrid ID="rgSoportes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rgSoportes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rgSoportes_OnNeedDataSource"
                                    OnItemCommand="rgSoportes_OnItemCommand">
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="SP_CONSECUTIVO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="SP_CONSECUTIVO" UniqueName="SP_CONSECUTIVO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="SP_DESCRIPCION" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="SP_DESCRIPCION" UniqueName="SP_DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Descripcion</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="edt_nombre" runat="server" Enabled="true" Width="350px">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    Archivo</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadAsyncUpload ID="rauCargarSoporte" runat="server" ControlObjectsVisibility="None"
                                                                    Enabled="true" EnableInlineProgress="false" InputSize="32" MaxFileInputsCount="1"
                                                                    Width="350px" OnFileUploaded="rauCargarSoporte_FileUploaded"
                                                                    Style="margin-bottom: 0px">
                                                                    <Localization Select="Cargar Archivo" />
                                                                </telerik:RadAsyncUpload>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btn_aceptarSoporte" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                    ValidationGroup="grNuevo" OnClick="btn_aceptarSoporte_OnClick" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CommandName="Cancel" />
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
                        </telerik:RadMultiPage>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mpAranceles" runat="server" Width="820px" Height="450px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Buscar Aranceles">
                    <ContentTemplate>
                        <div style="padding: 5px 5px 5px 5px">
                            <table>
                                <tr>
                                    <td>
                                        <label>Codigo</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_codarancel" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Nombre</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nomarancel" runat="server" Enabled="true" Width="150px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroarancel" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroarancel_Click" CommandName="Cancel" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="Panel1" runat="server">
                                <telerik:RadGrid ID="rg_Aranceles" runat="server" AllowSorting="True" Width="100%"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="20" CellSpacing="0" GridLines="None"
                                    DataSourceID="obj_aranceles" OnItemCommand="rg_Aranceles_ItemCommand">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                                <HeaderStyle Width="40px" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn Resizable="true" SortExpression="UA_CODIGO" HeaderText="Cod Arancel"
                                                UniqueName="UA_CODIGO" HeaderButtonType="TextButton" DataField="UA_CODIGO" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Width="90px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Resizable="true" SortExpression="UA_NOMBRE" HeaderText="Descripcion"
                                                UniqueName="UA_NOMBRE" HeaderButtonType="TextButton" DataField="UA_NOMBRE" ItemStyle-HorizontalAlign="Right"
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
                <telerik:RadWindow RenderMode="Lightweight" ID="mp_cam" runat="server" Width="737px" Height="600px" Modal="true" OffsetElementID="main" Title="Cam" EnableShadow="true">
                    <ContentTemplate>
                        <iframe name="myIframe" id="myIframe" width="99%" height="400px" runat="server"></iframe>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnSaveAnexo" runat="server" Text="Guardar" ValidationGroup="gvInsertFoto" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" OnClick="btnSaveAnexo_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_articulos" runat="server" OldValuesParameterFormatString="original_{0}" UpdateMethod="UpdateArticulo" OnUpdating="obj_articulos_OnUpdating"
        SelectMethod="GetArticulosD" TypeName="XUSS.BLL.Articulos.ArticulosBL" OnUpdated="obj_articulos_OnUpdated" InsertMethod="InsertArticulo" OnInserting="obj_articulos_OnInserting" OnInserted="obj_articulos_OnInserted">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=0" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="ARCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="ARTIPPRO" Type="String" />
            <asp:Parameter Name="ARCLAVE1" Type="String" />
            <asp:Parameter Name="ARCLAVE2" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARCLAVE3" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARCLAVE4" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARNOMBRE" Type="String" />
            <asp:Parameter Name="ARUNDINV" Type="String" />
            <asp:Parameter Name="ARUMALT1" Type="String" />
            <asp:Parameter Name="ARUMALT2" Type="String" />
            <asp:Parameter Name="ARFCA1IN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARFCA2IN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCDALTR" Type="String" />
            <asp:Parameter Name="ARMONEDA" Type="String" />
            <asp:Parameter Name="ARCOSTOA" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTMPR" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTMOB" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTCIF" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCOSTOB" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARPRECIO" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCDIMPF" Type="String" />
            <asp:Parameter Name="ARORIGEN" Type="String" />
            <asp:Parameter Name="ARPOSARA" Type="String" />
            <asp:Parameter Name="ARPESOUN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARPESOUM" Type="String" />
            <asp:Parameter Name="ARCDCLA1" Type="String" />
            <asp:Parameter Name="ARCDCLA2" Type="String" />
            <asp:Parameter Name="ARCDCLA3" Type="String" />
            <asp:Parameter Name="ARCDCLA4" Type="String" />
            <asp:Parameter Name="ARCDCLA6" Type="String" />
            <asp:Parameter Name="ARCDCLA7" Type="String" />
            <asp:Parameter Name="ARCDCLA8" Type="String" />
            <asp:Parameter Name="ARCDCLA9" Type="String" />
            <asp:Parameter Name="ARCDCLA10" Type="String" />
            <asp:Parameter Name="ARDTTEC1" Type="String" />
            <asp:Parameter Name="ARDTTEC2" Type="String" />
            <asp:Parameter Name="ARDTTEC3" Type="String" />
            <asp:Parameter Name="ARDTTEC4" Type="String" />
            <asp:Parameter Name="ARDTTEC5" Type="String" />
            <asp:Parameter Name="ARDTTEC6" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARDTTEC7" Type="String" />
            <asp:Parameter Name="ARDTTEC8" Type="String" />
            <asp:Parameter Name="ARDTTEC9" Type="String" />
            <asp:Parameter Name="ARDTTEC10" Type="String" />
            <asp:Parameter Name="ARCODPRO" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARFECCOM" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="ARPRECOM" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARMONCOM" Type="String" />
            <asp:Parameter Name="ARPROCOM" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARPROGDT" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARFCINA1" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCOMPOS" Type="String" />
            <asp:Parameter Name="ARCONVEN" Type="String" />
            <asp:Parameter Name="ARMERCON" Type="String" />
            <asp:Parameter Name="ARCODCOM" Type="String" />
            <asp:Parameter Name="ARCDCLA5" Type="String" />
            <asp:Parameter Name="ARCAOPDS" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARUNDODS" Type="String" />
            <asp:Parameter Name="ARTIPTAR" Type="String" />
            <asp:Parameter Name="ARANO" Type="String" />
            <asp:Parameter Name="ARCOLECCION" Type="String" />
            <asp:Parameter Name="ARPRIORIDAD" Type="String" />
            <asp:Parameter Name="TR_PROCEDENCIA" Type="String" />
            <asp:Parameter Name="TR_UEN" Type="String" />
            <asp:Parameter Name="TR_TP" Type="String" />
            <asp:Parameter Name="TR_SCT" Type="String" />
            <asp:Parameter Name="TR_FONDO" Type="String" />
            <asp:Parameter Name="TR_TEJIDO" Type="String" />
            <asp:Parameter Name="ARESTADO" Type="String" />
            <asp:Parameter Name="ARCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="ARNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbclave2" Type="Object" />
            <asp:Parameter Name="tbclave3" Type="Object" />
            <asp:Parameter Name="tbPrecios" Type="Object" />
            <asp:Parameter Name="tbSoportes" Type="Object" />
            <asp:Parameter Name="tbOrigen" Type="Object" />
            <asp:Parameter Name="tbTester" Type="Object" />
            <asp:Parameter Name="tbBarras" Type="Object" />
            <asp:Parameter Name="tbRSanitario" Type="Object" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="ARCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="ARTIPPRO" Type="String" />
            <asp:Parameter Name="ARCLAVE1" Type="String" />
            <asp:Parameter Name="ARCLAVE2" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARCLAVE3" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARCLAVE4" Type="String" DefaultValue="." />
            <asp:Parameter Name="ARNOMBRE" Type="String" />
            <asp:Parameter Name="ARUNDINV" Type="String" />
            <asp:Parameter Name="ARUMALT1" Type="String" />
            <asp:Parameter Name="ARUMALT2" Type="String" />
            <asp:Parameter Name="ARFCA1IN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARFCA2IN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCDALTR" Type="String" />
            <asp:Parameter Name="ARMONEDA" Type="String" />
            <asp:Parameter Name="ARCOSTOA" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTMPR" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTMOB" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCSTCIF" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCOSTOB" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARPRECIO" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCDIMPF" Type="String" />
            <asp:Parameter Name="ARORIGEN" Type="String" />
            <asp:Parameter Name="ARPOSARA" Type="String" />
            <asp:Parameter Name="ARPESOUN" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARPESOUM" Type="String" />
            <asp:Parameter Name="ARCDCLA1" Type="String" />
            <asp:Parameter Name="ARCDCLA2" Type="String" />
            <asp:Parameter Name="ARCDCLA3" Type="String" />
            <asp:Parameter Name="ARCDCLA4" Type="String" />
            <asp:Parameter Name="ARCDCLA6" Type="String" />
            <asp:Parameter Name="ARCDCLA7" Type="String" />
            <asp:Parameter Name="ARCDCLA8" Type="String" />
            <asp:Parameter Name="ARCDCLA9" Type="String" />
            <asp:Parameter Name="ARCDCLA10" Type="String" />
            <asp:Parameter Name="ARDTTEC1" Type="String" />
            <asp:Parameter Name="ARDTTEC2" Type="String" />
            <asp:Parameter Name="ARDTTEC3" Type="String" />
            <asp:Parameter Name="ARDTTEC4" Type="String" />
            <asp:Parameter Name="ARDTTEC5" Type="String" />
            <asp:Parameter Name="ARDTTEC6" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARDTTEC7" Type="String" />
            <asp:Parameter Name="ARDTTEC8" Type="String" />
            <asp:Parameter Name="ARDTTEC9" Type="String" />
            <asp:Parameter Name="ARDTTEC10" Type="String" />
            <asp:Parameter Name="ARCODPRO" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARFECCOM" Type="DateTime" DefaultValue="01/01/1900" />
            <asp:Parameter Name="ARPRECOM" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARMONCOM" Type="String" />
            <asp:Parameter Name="ARPROCOM" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARPROGDT" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARFCINA1" Type="Double" DefaultValue="0" />
            <asp:Parameter Name="ARCOMPOS" Type="String" />
            <asp:Parameter Name="ARCONVEN" Type="String" />
            <asp:Parameter Name="ARMERCON" Type="String" />
            <asp:Parameter Name="ARCODCOM" Type="String" />
            <asp:Parameter Name="ARCDCLA5" Type="String" />
            <asp:Parameter Name="ARCAOPDS" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ARUNDODS" Type="String" />
            <asp:Parameter Name="ARTIPTAR" Type="String" />
            <asp:Parameter Name="ARANO" Type="String" />
            <asp:Parameter Name="ARCOLECCION" Type="String" />
            <asp:Parameter Name="ARPRIORIDAD" Type="String" />
            <asp:Parameter Name="TR_PROCEDENCIA" Type="String" />
            <asp:Parameter Name="TR_UEN" Type="String" />
            <asp:Parameter Name="TR_TP" Type="String" />
            <asp:Parameter Name="TR_SCT" Type="String" />
            <asp:Parameter Name="TR_FONDO" Type="String" />
            <asp:Parameter Name="TR_TEJIDO" Type="String" />
            <asp:Parameter Name="ARESTADO" Type="String" />
            <asp:Parameter Name="ARCAUSAE" Type="String" DefaultValue="." />
            <asp:SessionParameter Name="ARNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="tbclave2" Type="Object" />
            <asp:Parameter Name="tbclave3" Type="Object" />
            <asp:Parameter Name="tbPrecios" Type="Object" />
            <asp:Parameter Name="tbBarras" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_unidad" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="UNIT" />
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
    <asp:ObjectDataSource ID="obj_impuesto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="IMPF" />
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
    <asp:ObjectDataSource ID="obj_proveedor" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProveedores" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tester" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetArticulos" TypeName="XUSS.BLL.Articulos.ArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=0" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_porigen" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAIS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_coleccion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="COLECC" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_procedencia" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PROCED" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_aranceles" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAranceles" TypeName="XUSS.BLL.Articulos.ArticulosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
