<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="AtencionCliente.aspx.cs" Inherits="XUSS.WEB.Tareas.AtencionCliente" %>

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
                debugger;
                if (args.EventTarget.indexOf("rg_imagenes") != -1) {
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_atencion" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rlv_atencion_ItemUpdating" OnItemInserted="rlv_atencion_ItemInserted"
            Width="100%" OnItemCommand="rlv_atencion_ItemCommand" OnItemDataBound="rlv_atencion_ItemDataBound"
            DataSourceID="obj_atencion" ItemPlaceholderID="pnlGeneral" RenderMode="Lightweight">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Atencion Cliente</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_atencion" RenderMode="Lightweight"
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
                                <h5>Atencion Cliente</h5>
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
                                    <label>Id App</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_idapp" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>                               
                            </tr>
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
                                    <label>Cta Contrato</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_ctacontrato" runat="server" Enabled="true" Width="300px">
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
                    <%--<telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />--%>
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("AT_CODIGO") %>'>
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
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="false" ValidationGroup="gvInsert" OnTextChanged="txt_identificacion_TextChanged" AutoPostBack="true"
                                    Text='<%# Bind("TRCODNIT") %>' Width="230px">
                                </telerik:RadTextBox>
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
                                <label>Tipo Inspeccion</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tinspeccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tinspeccion" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOINSPECCION") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Predio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tpredio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOPREDIO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo Servicio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tservicio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tservicio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Gas</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tgas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tgas" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOGAS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Asociado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox3" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_NEWUSD") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Certificado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox4" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_CLMATRIZ") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado U Inspeccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox5" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_CUINSPECCION") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Fecha U Inspeccion</label></td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" runat="server" DbSelectedDate='<%# Bind("AT_FECUINSPECCION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cta Contrato</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ctacontrato" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CTACONTRATO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Nombre Administrador</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nadministrador" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_ADMINISTRADOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Contacto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrocontacto" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CONTACTO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_IDCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ncomstructor" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_NOMCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Predio</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_idpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_NEWUSD") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Nueva" Value="S" />
                                        <telerik:RadComboBoxItem Text="Usada" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Diseño Isometrico</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_disometrico" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_DISISOMETRICO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tiene Plano</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tplano" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_PLANOAPROBADO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado Laboral</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_certlaboral" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_CERTLABORAL") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Competencias</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_competencias" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" SelectedValue='<%# Bind("AT_COMPETENCIAS") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Programacion</label></td>
                            <td>
                                <telerik:RadDateTimePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("AT_FECPROGRAMACION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDateTimePicker>
                            </td>
                            <td>
                                <label>Inspector</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_responsable" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" SelectedValue='<%# Bind("AT_RESPONSABLE") %>'
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith" Enabled="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Zona</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_ZONA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Evidencias">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_evidencias" runat="server">
                            <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_imagenes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_imagenes_NeedDataSource"
                                    OnItemCommand="rg_imagenes_OnItemCommand">
                                    <MasterTableView>
                                        <CommandItemSettings ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EI_CODIGO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="EI_CODIGO" UniqueName="EI_CODIGO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tipo_Nombre" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="Tipo_Nombre" UniqueName="Tipo_Nombre">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="print_file" Text="Imprimir" UniqueName="Imprimir"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No Records to Display!</strong>
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
                                <label>
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>'>
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
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" ValidationGroup="gvInsert" OnTextChanged="txt_identificacion_TextChanged" AutoPostBack="true"
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
                                <label>Tipo Inspeccion</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tinspeccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tinspeccion" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOINSPECCION") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Predio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tpredio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOPREDIO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo Servicio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tservicio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tservicio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Gas</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tgas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tgas" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOGAS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Asociado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox3" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_ALMATRIZ") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Certificado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox4" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CLMATRIZ") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado U Inspeccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox5" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CUINSPECCION") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Fecha U Inspeccion</label></td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" runat="server" DbSelectedDate='<%# Bind("AT_FECUINSPECCION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Cta Contrato</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ctacontrato" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CTACONTRATO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <label>Nombre Administrador</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nadministrador" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_ADMINISTRADOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Contacto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrocontacto" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CONTACTO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_IDCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ncomstructor" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_NOMCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Predio</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_idpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_NEWUSD") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Nueva" Value="S" />
                                        <telerik:RadComboBoxItem Text="Usada" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Diseño Isometrico</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_disometrico" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_DISISOMETRICO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tiene Plano</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tplano" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_PLANOAPROBADO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado Laboral</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_certlaboral" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CERTLABORAL") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Competencias</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_competencias" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_COMPETENCIAS") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Programacion</label></td>
                            <td>
                                <telerik:RadDateTimePicker ID="RadDateTimePicker1" runat="server" DbSelectedDate='<%# Bind("AT_FECPROGRAMACION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDateTimePicker>
                            </td>
                            <td>
                                <label>Inspector</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" SelectedValue='<%# Bind("AT_RESPONSABLE") %>'
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Zona</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_ZONA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Evidencias">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_evidencias" runat="server">
                            <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_imagenes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_imagenes_NeedDataSource"
                                    OnItemCommand="rg_imagenes_OnItemCommand">
                                    <MasterTableView>
                                        <CommandItemSettings ShowRefreshButton="false" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EI_CODIGO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="EI_CODIGO" UniqueName="EI_CODIGO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tipo_Nombre" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="Tipo_Nombre" UniqueName="Tipo_Nombre">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="download_file" Text="Descargar" UniqueName="Descargar"
                                                HeaderText="">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <NoRecordsTemplate>
                                            <div class="alert alert-danger">
                                                <strong>¡No Records to Display!</strong>
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
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Codigo</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("AT_CODIGO") %>'>
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
                                <telerik:RadTextBox ID="txt_identificacion" runat="server" Enabled="true" ValidationGroup="gvInsert" OnTextChanged="txt_identificacion_TextChanged" AutoPostBack="true"
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
                                <label>Tipo Inspeccion</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tinspeccion" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tinspeccion" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOINSPECCION") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Predio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tpredio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOPREDIO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Tipo Servicio</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tservicio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tservicio" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPO") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tipo Gas</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_tgas" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_tgas" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_TIPOGAS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Asociado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox3" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_NEWUSD") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Certificado Linea Matriz</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox4" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CLMATRIZ") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado U Inspeccion</label></td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox5" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CUINSPECCION") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Fecha U Inspeccion</label></td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker2" runat="server" DbSelectedDate='<%# Bind("AT_FECUINSPECCION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <label>Cta Contrato</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ctacontrato" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CTACONTRATO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <label>Nombre Administrador</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nadministrador" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_ADMINISTRADOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nro Contacto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrocontacto" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_CONTACTO") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox1" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_IDCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Nombre Constructor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ncomstructor" runat="server" Enabled="true" ValidationGroup="gvInsert"
                                    Text='<%# Bind("AT_NOMCONSTRUCTOR") %>' Width="230px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Id Predio</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_idpredio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_NEWUSD") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Nueva" Value="S" />
                                        <telerik:RadComboBoxItem Text="Usada" Value="N" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Diseño Isometrico</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_disometrico" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_DISISOMETRICO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Tiene Plano</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_tplano" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_PLANOAPROBADO") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Certificado Laboral</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_certlaboral" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_CERTLABORAL") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>Competencias</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_competencias" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" SelectedValue='<%# Bind("AT_COMPETENCIAS") %>'
                                    AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Si" Value="S" />
                                        <telerik:RadComboBoxItem Text="No" Value="N" />
                                        <telerik:RadComboBoxItem Text="Terreno" Value="P" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>F Programacion</label></td>
                            <td>
                                <telerik:RadDateTimePicker ID="RadDateTimePicker1" runat="server" DbSelectedDate='<%# Bind("AT_FECPROGRAMACION") %>' ValidationGroup="gvInsert"
                                    MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDateTimePicker>
                            </td>
                            <td>
                                <label>Inspector</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="obj_usuarios" Width="300px" AllowCustomText="true" SelectedValue='<%# Bind("AT_RESPONSABLE") %>'
                                    DataTextField="usua_nombres" DataValueField="usua_usuario" AppendDataBoundItems="true" Filter="StartsWith">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Zona</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_zona" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_zona" DataTextField="TTDESCRI" SelectedValue='<%# Bind("AT_ZONA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Evidencias">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_evidencias" runat="server">
                            <telerik:RadGrid ID="rg_imagenes" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" UniqueID="rg_imagenes"
                                    Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_imagenes_NeedDataSource" OnDeleteCommand="rg_imagenes_DeleteCommand"
                                    OnItemCommand="rg_imagenes_OnItemCommand">
                                    <MasterTableView InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="EI_CODIGO">
                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                            ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" AddNewRecordText="Nuevo Item" RefreshText="Cargar" />
                                        <CommandItemTemplate>
                                            <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar_item" runat="server" Text="New Item" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                                    ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                            <telerik:GridBoundColumn DataField="EI_CODIGO" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="40px" HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true"
                                                SortExpression="EI_CODIGO" UniqueName="EI_CODIGO" Visible="true">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Tipo_Nombre" HeaderButtonType="TextButton"
                                                HeaderStyle-Width="120px" HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="Tipo_Nombre" UniqueName="Tipo_Nombre">
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
                                                                            Tipo</label>
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="rc_tipdoc" runat="server" Culture="es-CO" Width="300px"
                                                                            Enabled="true" AppendDataBoundItems="true">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                                                <telerik:RadComboBoxItem Text="Lecturas" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Trazado" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Linea Matriz" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Artefactos" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Ventilacion" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Panoramica" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Vacio Interno" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Evacuacion" Value="8" />
                                                                                <telerik:RadComboBoxItem Text="co sin Artefactos" Value="9" />
                                                                                <telerik:RadComboBoxItem Text="co con Artefactos" Value="10" />
                                                                                <telerik:RadComboBoxItem Text="Recibo" Value="11" />
                                                                                <telerik:RadComboBoxItem Text="Certificado" Value="12" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
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
                                                                        <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" CommandName="PerformInsert"
                                                                            ValidationGroup="grNuevo" OnClick="btn_aceptar_Click" />
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
                                                <strong>¡No Records to Display!</strong>
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
    <asp:ObjectDataSource ID="obj_atencion" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="insertAtencionCliente" OnInserted="obj_atencion_Inserted"
        SelectMethod="getAtencionCliente" TypeName="XUSS.BLL.Parametros.QuejasApelacionesBL" UpdateMethod="updateAtencionCliente">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="AT_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRNOMCOMERCIAL" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="AT_FECPROGRAMACION" Type="DateTime" />
            <asp:Parameter Name="AT_RESPONSABLE" Type="String" />
            <asp:Parameter Name="AT_CTACONTRATO" Type="String" />
            <asp:Parameter Name="AT_DISISOMETRICO" Type="String" />
            <asp:Parameter Name="AT_PLANOAPROBADO" Type="String" />
            <asp:Parameter Name="AT_CERTLABORAL" Type="String" />
            <asp:Parameter Name="AT_COMPETENCIAS" Type="String" />
            <asp:Parameter Name="AT_ADMINISTRADOR" Type="String" />
            <asp:Parameter Name="AT_CONTACTO" Type="String" />
            <asp:Parameter Name="AT_IDCONSTRUCTOR" Type="String" />
            <asp:Parameter Name="AT_NOMCONSTRUCTOR" Type="String" />
            <asp:Parameter Name="AT_NEWUSD" Type="String" />
            <asp:Parameter Name="AT_ALMATRIZ" Type="String" />
            <asp:Parameter Name="AT_CLMATRIZ" Type="String" />
            <asp:Parameter Name="AT_CUINSPECCION" Type="String" />
            <asp:Parameter Name="AT_FECUINSPECCION" Type="DateTime" />
            <asp:Parameter Name="AT_ZONA" Type="String" />
            <asp:Parameter Name="AT_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="AT_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="AT_TIPOINSPECCION" Type="String"  />
            <asp:Parameter Name="AT_TIPOPREDIO" Type="String"  />
            <asp:Parameter Name="AT_TIPO" Type="String" />
            <asp:Parameter Name="AT_TIPOGAS" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="AT_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="AT_CODIGO" Type="Int32" />
            <asp:Parameter Name="TRCODTER" Type="Int32" DefaultValue="0"/>
            <asp:Parameter Name="TRNOMBRE" Type="String" />
            <asp:Parameter Name="TRNOMBR2" Type="String" />
            <asp:Parameter Name="TRCODNIT" Type="String" />
            <asp:Parameter Name="TRDIRECC" Type="String" />
            <asp:Parameter Name="TRNROTEL" Type="String" />
            <asp:Parameter Name="TRCORREO" Type="String" />
            <asp:Parameter Name="TRAPELLI" Type="String" />
            <asp:Parameter Name="TRNOMBR3" Type="String" />
            <asp:Parameter Name="TRNOMCOMERCIAL" Type="String" />
            <asp:Parameter Name="TRTIPDOC" Type="String" />
            <asp:Parameter Name="TRDIGCHK" Type="String" />
            <asp:Parameter Name="AT_FECPROGRAMACION" Type="DateTime" />
            <asp:Parameter Name="AT_RESPONSABLE" Type="String" />
            <asp:Parameter Name="AT_CTACONTRATO" Type="String" />
            <asp:Parameter Name="AT_DISISOMETRICO" Type="String" />
            <asp:Parameter Name="AT_PLANOAPROBADO" Type="String" />
            <asp:Parameter Name="AT_CERTLABORAL" Type="String" />
            <asp:Parameter Name="AT_COMPETENCIAS" Type="String" />
            <asp:Parameter Name="AT_ADMINISTRADOR" Type="String" />
            <asp:Parameter Name="AT_CONTACTO" Type="String" />
            <asp:Parameter Name="AT_IDCONSTRUCTOR" Type="String" />
            <asp:Parameter Name="AT_NOMCONSTRUCTOR" Type="String" />
            <asp:Parameter Name="AT_NEWUSD" Type="String" />
            <asp:Parameter Name="AT_ALMATRIZ" Type="String" />
            <asp:Parameter Name="AT_CLMATRIZ" Type="String" />
            <asp:Parameter Name="AT_CUINSPECCION" Type="String" />
            <asp:Parameter Name="AT_FECUINSPECCION" Type="DateTime" />
            <asp:Parameter Name="AT_ZONA" Type="String" />
            <asp:Parameter Name="AT_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="AT_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="AT_TIPOINSPECCION" Type="String"  />
            <asp:Parameter Name="AT_TIPOPREDIO" Type="String"  />
            <asp:Parameter Name="AT_TIPO" Type="String" />
            <asp:Parameter Name="AT_TIPOGAS" Type="String" />
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
    <asp:ObjectDataSource ID="obj_tinspeccion" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TINSPEC" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tpredio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TPREDIO" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tservicio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TSERVINS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tgas" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="TGAS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_zona" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="SUBZON" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_usuarios" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsuarios" TypeName="XUSS.BLL.Tareas.LstTareasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
