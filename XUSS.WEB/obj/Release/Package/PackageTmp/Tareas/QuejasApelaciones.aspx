<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="QuejasApelaciones.aspx.cs" Inherits="XUSS.WEB.Tareas.QuejasApelaciones" %>

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
                //if (args.EventTarget.indexOf("rgSoportes") != -1) {
                //    args.set_enableAjax(false);
                //}
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }

            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
            (function (global, undefined) {
                var demo = {};

                function OnClientFilesUploaded(sender, args) {
                    //debugger;
                    //$find(demo.ajaxManagerID).ajaxRequest();

                    var panel = $find("<%= RadAjaxPanel2.ClientID%>");
                    panel.ajaxRequestWithTarget(panel.ClientID, "LoadImage");
                    var eItem = $find('<%= rlv_quejas.ClientID %>');
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
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_quejas" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rlv_quejas_ItemUpdating" OnItemInserted="rlv_quejas_ItemInserted"
            Width="100%" OnItemCommand="rlv_quejas_ItemCommand" OnItemDataBound="rlv_quejas_ItemDataBound"
            DataSourceID="obj_quejas" ItemPlaceholderID="pnlGeneral" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Quejas y Apelaciones</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_quejas" RenderMode="Lightweight"
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
                                <h5>Quejas y Apelaciones</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div>
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                                    <%--<asp:ImageButton ID="iBtnInitInsert" runat="server" CommandName="InitInsert" SkinID="SkinAddUC" />                                                    --%>
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
                                    <label>Nro Radicado</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nrocodigo" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_Click" Text="Buscar" RenderMode="Lightweight" Icon-PrimaryIconCssClass="rbSearch">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table> 
                        <tr>
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("TQ_FECHA") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Autorizacion</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_autoriza" runat="server" Checked='<%# this.GetEstado(Eval("TQ_TRATAMIENTO")) %>' Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro Radicado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TQ_NUMERO") %>' 
                                    Width="250px">
                                </telerik:RadTextBox>                                
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

                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Visible="false" Width="47px">
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
                                    Telefono</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Bind("TRNROTEL") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Correo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_correo" runat="server" Enabled="false" Text='<%# Bind("TRCORREO") %>'
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
                                <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="false" Text='<%# Bind("TRDIRECC") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("TQ_TIPO") %>'
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Queja" Value="Q" />
                                        <telerik:RadComboBoxItem Text="Apelacion" Value="A" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                                <label>Responsable</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_responsable" runat="server" DataSourceID="obj_propietario" Width="300px" SelectedValue='<%# Bind("TR_RESPONSABLE") %>'
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Cargo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_area" runat="server" DataSourceID="obj_area" SelectedValue='<%# Bind("TR_CARGO") %>'
                                    DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="300px"
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fundamentada</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_fundamentada" runat="server" SelectedValue='<%# Bind("TR_VALIDA") %>'
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="SI" Value="S" />
                                        <telerik:RadComboBoxItem Text="NO" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Prioridad</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_prioridad" runat="server" SelectedValue='<%# Bind("TR_PRIORIDAD") %>'
                                    Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Alta" Value="A" />
                                        <telerik:RadComboBoxItem Text="Media" Value="M" />
                                        <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Descripcion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Proceso Inventigacion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Decision/Accion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Evidencias">
                            </telerik:RadTab>                            
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_descripcion" runat="server">
                            <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("TQ_DESCRIPCION") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_proceso" runat="server">
                            <telerik:RadTextBox ID="txt_invetigacion" runat="server" Enabled="false" Text='<%# Bind("TR_PROCESO") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_desicion" runat="server">
                            <telerik:RadTextBox ID="txt_desicion" runat="server" Enabled="false" Text='<%# Bind("TR_DESICION") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                    <MasterTableView ShowGroupFooter="true" AllowPaging="true" PageSize="10">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="40px" DataField="IE_IMAGEN" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IE_IMAGEN") is DBNull ? null : Eval("IE_IMAGEN")%>'
                                                        AutoAdjustImageControlSize="false" Width="500px" Height="500px" ToolTip='<%#Eval("IE_IMAGEN", "Foto {0}") %>'
                                                         />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Tipo" HeaderStyle-Width="40px" DataField="IE_IMAGEN" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("IE_TIPIMA") %>'
                                                                    Culture="es-CO" Width="300px" DataSourceID="obj_tipo" DataTextField="TTDESCRI"
                                                                    Enabled="false" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
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
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table> 
                        <tr>
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("TQ_FECHA") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Autorizacion</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_autoriza" runat="server" Checked='<%# this.GetEstado(Eval("TQ_TRATAMIENTO")) %>' Enabled="true" />
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
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" Text='<%# Bind("TRCODNIT") %>' OnTextChanged="txt_identificacion_TextChanged" AutoPostBack="true"
                                    Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadNumericTextBox ID="txt_digchk" runat="server" Enabled="true" Text='<%# Bind("TRDIGCHK") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="47px">
                                </telerik:RadNumericTextBox>

                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Visible="false" Width="47px">
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
                                    Width="300px">
                                </telerik:RadTextBox>
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
                                    Width="300px">
                                </telerik:RadTextBox>
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
                                    Telefono</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("TRNROTEL") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Correo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_correo" runat="server" Enabled="true" Text='<%# Bind("TRCORREO") %>'
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
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("TQ_TIPO") %>'
                                    Enabled="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Queja" Value="Q" />
                                        <telerik:RadComboBoxItem Text="Apelacion" Value="A" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label>
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("TQ_DESCRIPCION") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table> 
                        <tr>
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("TQ_FECHA") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Autorizacion</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_autoriza" runat="server" Checked='<%# this.GetEstado(Eval("TQ_TRATAMIENTO")) %>' Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Nro Radicado</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TQ_NUMERO") %>' 
                                    Width="250px">
                                </telerik:RadTextBox>                                
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
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false" Text='<%# Bind("TRCODNIT") %>' OnTextChanged="txt_identificacion_TextChanged" AutoPostBack="true"
                                    Width="250px">
                                </telerik:RadTextBox>
                                <telerik:RadNumericTextBox ID="txt_digchk" runat="server" Enabled="false" Text='<%# Bind("TRDIGCHK") %>'
                                    NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="47px">
                                </telerik:RadNumericTextBox>

                                <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Visible="false" Width="47px">
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
                                    Telefono</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Bind("TRNROTEL") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Correo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_correo" runat="server" Enabled="false" Text='<%# Bind("TRCORREO") %>'
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
                                <telerik:RadTextBox ID="txt_direccion" runat="server" Enabled="false" Text='<%# Bind("TRDIRECC") %>'
                                    Width="300px">
                                </telerik:RadTextBox>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Tipo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("TQ_TIPO") %>'
                                    Enabled="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Queja" Value="Q" />
                                        <telerik:RadComboBoxItem Text="Apelacion" Value="A" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>          
                        <tr>
                            <td>
                                <label>Responsable</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_responsable" runat="server" DataSourceID="obj_propietario" Width="300px" SelectedValue='<%# Bind("TR_RESPONSABLE") %>'
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Cargo</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_area" runat="server" DataSourceID="obj_area" SelectedValue='<%# Bind("TR_CARGO") %>'
                                    DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" Width="300px"
                                    AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                    </Items>
                                </telerik:RadComboBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Fundamentada</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_fundamentada" runat="server" SelectedValue='<%# Bind("TR_VALIDA") %>'
                                    Enabled="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="SI" Value="S" />
                                        <telerik:RadComboBoxItem Text="NO" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Prioridad</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_prioridad" runat="server" SelectedValue='<%# Bind("TR_PRIORIDAD") %>'
                                    Enabled="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Alta" Value="A" />
                                        <telerik:RadComboBoxItem Text="Media" Value="M" />
                                        <telerik:RadComboBoxItem Text="Baja" Value="B" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Descripcion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Proceso Inventigacion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Decision/Accion">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Evidencias">
                            </telerik:RadTab>                            
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_descripcion" runat="server">
                            <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Text='<%# Bind("TQ_DESCRIPCION") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_proceso" runat="server">
                            <telerik:RadTextBox ID="txt_invetigacion" runat="server" Enabled="true" Text='<%# Bind("TR_PROCESO") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_desicion" runat="server">
                            <telerik:RadTextBox ID="txt_desicion" runat="server" Enabled="true" Text='<%# Bind("TR_DESICION") %>'
                                    Width="600px" TextMode="MultiLine" Height="250px">
                                </telerik:RadTextBox>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_evidencias" runat="server">
                            <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_imagenes_ItemCommand" OnDeleteCommand="rg_imagenes_DeleteCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_imagenes_NeedDataSource">
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" AllowPaging="true" PageSize="10" DataKeyNames="IE_CONSECUTIVO">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_attach" runat="server" Text="Adjunto" Icon-PrimaryIconCssClass="rbAttach" CommandName="InitInsert" ToolTip="Nueva Adjunto" />
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_foto" runat="server" Text="Nueva Foto" Icon-PrimaryIconCssClass="rbAdd" CommandName="NewPhoto" ToolTip="Nueva Fotografia" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />

                                            <telerik:GridTemplateColumn HeaderText="Imagen/Dibujo" HeaderStyle-Width="40px" DataField="IE_CONSECUTIVO" Visible="true"
                                                Resizable="true">
                                                <ItemTemplate>
                                                    <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" DataValue='<%#Eval("IE_IMAGEN") is DBNull ? null : Eval("IE_IMAGEN")%>'
                                                        AutoAdjustImageControlSize="false" Width="500px" Height="500px" ToolTip='<%#Eval("IE_IMAGEN", "Foto {0}") %>'
                                                         />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div>
                                                    <table>
                                                        <tr>
                                                            <td colspan="2">
                                                                <telerik:RadAsyncUpload RenderMode="Lightweight" ID="AsyncUpload1" runat="server" OnClientAdded="OnClientAdded"
                                                                    OnClientFilesUploaded="OnClientFilesUploaded" OnFileUploaded="AsyncUpload1_FileUploaded"
                                                                    MaxFileSize="2097152" AllowedFileExtensions="jpg,png,gif,bmp"
                                                                    AutoAddFileInputs="false" Localization-Select="Upload Image" />
                                                                <asp:Label ID="Label1" Text="*Size limit: 2MB" runat="server" Style="font-size: 10px;"></asp:Label>
                                                            </td>
                                                            <td colspan="2">
                                                                <telerik:RadButton ID="btn_guardar_img" runat="server" Text="Aceptar" CommandName="PerformInsert" Icon-PrimaryIconCssClass="rbSave"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" RenderMode="Lightweight" />
                                                                <telerik:RadButton ID="btn_cancel" runat="server" Text="Cancelar" CommandName="Cancel" Icon-PrimaryIconCssClass="rbCancel"
                                                                    ValidationGroup="grNuevo" SkinID="SkinUpdateUC" RenderMode="Lightweight" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>Tipo</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadComboBox ID="rc_tipo" runat="server" SelectedValue='<%# Bind("IE_TIPIMA") %>'
                                                                    Culture="es-CO" Width="300px" DataSourceID="obj_tipo" DataTextField="TTDESCRI"
                                                                    Enabled="true" DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td>
                                                                <label>Observaciones</label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Text='<%# Bind("IE_DESCRIPCION") %>'
                                                                    Width="300px" TextMode="MultiLine" Height="40px">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
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
                    </telerik:RadMultiPage>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMensaje" runat="server" Width="750px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_quejas" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertQuejaSolicitudApelacion" OnInserted="obj_quejas_Inserted" OnUpdated="obj_quejas_Updated"
        SelectMethod="GetQuejaSolicitudeApelacion" TypeName="XUSS.BLL.Parametros.QuejasApelacionesBL" UpdateMethod="UpdateQuejaSolicitudApelacion">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="TQ_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TQ_TRATAMIENTO" Type="String" DefaultValue="S" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="TQ_TIPO" Type="String" />
            <asp:Parameter Name="TQ_FECHA" Type="DateTime" />
            <asp:Parameter Name="TQ_DESCRIPCION" Type="String" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="TRDTTEC2" Type="String" DefaultValue="" />
            <asp:Parameter Name="TQ_ESTADO" Type="String" DefaultValue="AC" />            
            <asp:SessionParameter Name="TQ_USUARIO" Type="String" SessionField="UserLogon" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="TQ_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TQ_TRATAMIENTO" Type="String" DefaultValue="S" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="TQ_TIPO" Type="String" />
            <asp:Parameter Name="TQ_FECHA" Type="DateTime" />
            <asp:Parameter Name="TQ_DESCRIPCION" Type="String" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="TQ_ESTADO" Type="String" DefaultValue="AC" />            
            <asp:SessionParameter Name="TQ_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="TR_RESPONSABLE" Type="String" />
            <asp:Parameter Name="TR_CARGO" Type="String" />
            <asp:Parameter Name="TR_VALIDA" Type="String" />
            <asp:Parameter Name="TR_PROCESO" Type="String" />
            <asp:Parameter Name="TR_DESICION" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipdoc" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TIDO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
            <asp:SessionParameter Name="TTCODEMP" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TEVDQA" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_propietario" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_area" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="CARGO" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
