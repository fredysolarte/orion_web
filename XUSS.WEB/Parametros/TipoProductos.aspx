<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TipoProductos.aspx.cs" Inherits="XUSS.WEB.Parametros.TipoProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_tproducto" runat="server" PageSize="1" AllowPaging="True" 
            Width="100%" OnItemCommand="rlv_tproducto_OnItemCommand" OnItemDataBound="rlv_tproducto_OnItemDataBound"
            DataSourceID="obj_tproducto" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Tipos de Producto(Linea)</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_tproducto" RenderMode="Lightweight"
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
                                    Tipos de Producto(Linea)</h5>
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
                                    <label>Codigo</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td>
                                    <label>Nombre</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
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
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />                                                                                
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked ="Clicking" ToolTip="Anular Registro"/>                                                            
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Tipo Producto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_tippro" runat="server" Enabled="false" Text='<%# Bind("TATIPPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Producto</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cproducto" runat="server" Culture="es-CO"  Width="300px" Enabled="false"
                                     SelectedValue='<%# Bind("TACLAPRO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Articulo" Value="A" />
                                        <telerik:RadComboBoxItem Text="Servicio" Value="S" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>C Claves</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cclaves" runat="server" Culture="es-CO"  Width="300px" Enabled="false"
                                     SelectedValue='<%# Bind("TACLAVES") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                        <telerik:RadComboBoxItem Text="2" Value="2" />    
                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                        <telerik:RadComboBoxItem Text="4" Value="4" />                                      
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Text='<%# Bind("TANOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos TP">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Lotes/Elementos">
                            </telerik:RadTab>          
                            <telerik:RadTab Text="Produccion">
                            </telerik:RadTab>                    
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <table>
                                <tr>
                                    <td><label>Primera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave1" runat="server" Enabled="false" Text='<%# Bind("TADSCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Segunda Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="false" Text='<%# Bind("TADSCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla2" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE2")) %>' Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Tercera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="false" Text='<%# Bind("TADSCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla3" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE3")) %>' Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Cuarta Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="false" Text='<%# Bind("TADSCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla4" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE4")) %>' Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="false" Text='<%# Bind("TACDCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt1" runat="server" Enabled="false" Text='<%# Bind("TADTTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="false" Text='<%# Bind("TACDCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt2" runat="server" Enabled="false" Text='<%# Bind("TADTTEC2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="false" Text='<%# Bind("TACDCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt3" runat="server" Enabled="false" Text='<%# Bind("TADTTEC3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="false" Text='<%# Bind("TACDCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt4" runat="server" Enabled="false" Text='<%# Bind("TADTTEC4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="false" Text='<%# Bind("TACDCLA5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt5" runat="server" Enabled="false" Text='<%# Bind("TADTTEC5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="false" Text='<%# Bind("TACDCLA6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 6</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt6" runat="server" Enabled="false" Text='<%# Bind("TADTTEC6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 7</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="false" Text='<%# Bind("TACDCLA7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 7</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt7" runat="server" Enabled="false" Text='<%# Bind("TADTTEC7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 8</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="false" Text='<%# Bind("TACDCLA8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 8</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt8" runat="server" Enabled="false" Text='<%# Bind("TADTTEC8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 9</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="false" Text='<%# Bind("TACDCLA9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 9</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt9" runat="server" Enabled="false" Text='<%# Bind("TADTTEC9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 10</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="false" Text='<%# Bind("TACDCLA10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 10</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt10" runat="server" Enabled="false" Text='<%# Bind("TADTTEC10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lotes" runat="server">
                            <table>
                                <tr>
                                    <td><label>Dato Tecnico 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl1" runat="server" Enabled="false" Text='<%# Bind("TADTLOT1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte1" runat="server" Enabled="false" Text='<%# Bind("TADTELE1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl2" runat="server" Enabled="false" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte2" runat="server" Enabled="false" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl3" runat="server" Enabled="false" Text='<%# Bind("TADTLOT3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte3" runat="server" Enabled="false" Text='<%# Bind("TADTELE3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl4" runat="server" Enabled="false" Text='<%# Bind("TADTLOT4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte4" runat="server" Enabled="false" Text='<%# Bind("TADTELE4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl5" runat="server" Enabled="false" Text='<%# Bind("TADTLOT5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte5" runat="server" Enabled="false" Text='<%# Bind("TADTELE5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl6" runat="server" Enabled="false" Text='<%# Bind("TADTLOT6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte6" runat="server" Enabled="false" Text='<%# Bind("TADTELE6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td><label>Nro Ref Inicial</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_refini" runat="server" Enabled="false" Text='<%# Bind("TAREFINI") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Ref Automatica</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_refaut" runat="server" Checked='<%# this.GetEstado(Eval("TAAUTINC")) %>' Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate> 
            <InsertItemTemplate>            
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Tipo Producto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_tippro" runat="server" Enabled="true" Text='<%# Bind("TATIPPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Producto</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cproducto" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("TACLAPRO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Articulo" Value="A" />
                                        <telerik:RadComboBoxItem Text="Servicio" Value="S" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>C Claves</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cclaves" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("TACLAVES") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                        <telerik:RadComboBoxItem Text="2" Value="2" />    
                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                        <telerik:RadComboBoxItem Text="4" Value="4" />                                      
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("TANOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos TP">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Lotes/Elementos">
                            </telerik:RadTab>          
                            <telerik:RadTab Text="Produccion">
                            </telerik:RadTab>                    
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <table>
                                <tr>
                                    <td><label>Primera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave1" runat="server" Enabled="true" Text='<%# Bind("TADSCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Segunda Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Text='<%# Bind("TADSCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla2" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE2")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Tercera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Text='<%# Bind("TADSCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla3" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE3")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Cuarta Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Text='<%# Bind("TADSCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla4" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE4")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("TACDCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt1" runat="server" Enabled="true" Text='<%# Bind("TADTTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("TACDCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt2" runat="server" Enabled="true" Text='<%# Bind("TADTTEC2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("TACDCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt3" runat="server" Enabled="true" Text='<%# Bind("TADTTEC3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("TACDCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt4" runat="server" Enabled="true" Text='<%# Bind("TADTTEC4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("TACDCLA5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt5" runat="server" Enabled="true" Text='<%# Bind("TADTTEC5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("TACDCLA6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 6</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt6" runat="server" Enabled="true" Text='<%# Bind("TADTTEC6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 7</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("TACDCLA7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 7</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt7" runat="server" Enabled="true" Text='<%# Bind("TADTTEC7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 8</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("TACDCLA8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 8</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt8" runat="server" Enabled="true" Text='<%# Bind("TADTTEC8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 9</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("TACDCLA9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 9</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt9" runat="server" Enabled="true" Text='<%# Bind("TADTTEC9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 10</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("TACDCLA10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 10</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt10" runat="server" Enabled="true" Text='<%# Bind("TADTTEC10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lotes" runat="server">
                            <table>
                                <tr>
                                    <td><label>Dato Tecnico 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl1" runat="server" Enabled="true" Text='<%# Bind("TADTLOT1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte1" runat="server" Enabled="true" Text='<%# Bind("TADTELE1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl2" runat="server" Enabled="true" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte2" runat="server" Enabled="true" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl3" runat="server" Enabled="true" Text='<%# Bind("TADTLOT3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte3" runat="server" Enabled="true" Text='<%# Bind("TADTELE3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl4" runat="server" Enabled="true" Text='<%# Bind("TADTLOT4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte4" runat="server" Enabled="true" Text='<%# Bind("TADTELE4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl5" runat="server" Enabled="true" Text='<%# Bind("TADTLOT5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte5" runat="server" Enabled="true" Text='<%# Bind("TADTELE5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl6" runat="server" Enabled="true" Text='<%# Bind("TADTLOT6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte6" runat="server" Enabled="true" Text='<%# Bind("TADTELE6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td><label>Nro Ref Inicial</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_refini" runat="server" Enabled="true" Text='<%# Bind("TAREFINI") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Ref Automatica</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_refaut" runat="server" Checked='<%# this.GetEstado(Eval("TAAUTINC")) %>' Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
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
                                <label>Tipo Producto</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_tippro" runat="server" Enabled="false" Text='<%# Bind("TATIPPRO") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>C Producto</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cproducto" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("TACLAPRO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Articulo" Value="A" />
                                        <telerik:RadComboBoxItem Text="Servicio" Value="S" />                                        
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>C Claves</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_cclaves" runat="server" Culture="es-CO"  Width="300px" Enabled="true"
                                     SelectedValue='<%# Bind("TACLAVES") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                        <telerik:RadComboBoxItem Text="2" Value="2" />    
                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                        <telerik:RadComboBoxItem Text="4" Value="4" />                                      
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td colspan="5">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("TANOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos TP">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Lotes/Elementos">
                            </telerik:RadTab>          
                            <telerik:RadTab Text="Produccion">
                            </telerik:RadTab>                    
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <table>
                                <tr>
                                    <td><label>Primera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave1" runat="server" Enabled="true" Text='<%# Bind("TADSCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Segunda Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave2" runat="server" Enabled="true" Text='<%# Bind("TADSCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla2" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE2")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Tercera Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave3" runat="server" Enabled="true" Text='<%# Bind("TADSCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla3" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE3")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Cuarta Clave</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clave4" runat="server" Enabled="true" Text='<%# Bind("TADSCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Validar Clave Adicional</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_valcla4" runat="server" Checked='<%# this.GetEstado(Eval("TACTLSE4")) %>' Enabled="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion1" runat="server" Enabled="true" Text='<%# Bind("TACDCLA1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt1" runat="server" Enabled="true" Text='<%# Bind("TADTTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion2" runat="server" Enabled="true" Text='<%# Bind("TACDCLA2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt2" runat="server" Enabled="true" Text='<%# Bind("TADTTEC2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion3" runat="server" Enabled="true" Text='<%# Bind("TACDCLA3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt3" runat="server" Enabled="true" Text='<%# Bind("TADTTEC3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion4" runat="server" Enabled="true" Text='<%# Bind("TACDCLA4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt4" runat="server" Enabled="true" Text='<%# Bind("TADTTEC4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion5" runat="server" Enabled="true" Text='<%# Bind("TACDCLA5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt5" runat="server" Enabled="true" Text='<%# Bind("TADTTEC5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion6" runat="server" Enabled="true" Text='<%# Bind("TACDCLA6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 6</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt6" runat="server" Enabled="true" Text='<%# Bind("TADTTEC6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 7</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion7" runat="server" Enabled="true" Text='<%# Bind("TACDCLA7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 7</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt7" runat="server" Enabled="true" Text='<%# Bind("TADTTEC7") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 8</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion8" runat="server" Enabled="true" Text='<%# Bind("TACDCLA8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 8</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt8" runat="server" Enabled="true" Text='<%# Bind("TADTTEC8") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 9</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion9" runat="server" Enabled="true" Text='<%# Bind("TACDCLA9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 9</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt9" runat="server" Enabled="true" Text='<%# Bind("TADTTEC9") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Clasificacion 10</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_clasificacion10" runat="server" Enabled="true" Text='<%# Bind("TACDCLA10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 10</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dt10" runat="server" Enabled="true" Text='<%# Bind("TADTTEC10") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lotes" runat="server">
                            <table>
                                <tr>
                                    <td><label>Dato Tecnico 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl1" runat="server" Enabled="true" Text='<%# Bind("TADTLOT1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 1</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte1" runat="server" Enabled="true" Text='<%# Bind("TADTELE1") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 2</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl2" runat="server" Enabled="true" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 2</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte2" runat="server" Enabled="true" Text='<%# Bind("TADTELE2") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 3</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl3" runat="server" Enabled="true" Text='<%# Bind("TADTLOT3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 3</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte3" runat="server" Enabled="true" Text='<%# Bind("TADTELE3") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 4</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl4" runat="server" Enabled="true" Text='<%# Bind("TADTLOT4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 4</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte4" runat="server" Enabled="true" Text='<%# Bind("TADTELE4") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 5</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl5" runat="server" Enabled="true" Text='<%# Bind("TADTLOT5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte5" runat="server" Enabled="true" Text='<%# Bind("TADTELE5") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Dato Tecnico 6</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dtl6" runat="server" Enabled="true" Text='<%# Bind("TADTLOT6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <label>Dato Tecnico 5</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_dte6" runat="server" Enabled="true" Text='<%# Bind("TADTELE6") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <table>
                                <tr>
                                    <td><label>Nro Ref Inicial</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_refini" runat="server" Enabled="true" Text='<%# Bind("TAREFINI") %>' Width="300px">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Ref Automatica</label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chk_refaut" runat="server" Checked='<%# this.GetEstado(Eval("TAAUTINC")) %>' Enabled="true" />
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_tproducto" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertTipoProducto" UpdateMethod="UpdateTipoProducto"
        SelectMethod="GetTipoProducto" TypeName="XUSS.BLL.Parametros.TipoProductosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" /> 
            <asp:SessionParameter Name="TACODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TATIPPRO" Type="String" />
            <asp:Parameter Name="TANOMBRE" Type="String" />
            <asp:Parameter Name="TACLAVES" Type="Int32" />
            <asp:Parameter Name="TACLAPRO" Type="String" />
            <asp:Parameter Name="TADSCLA1" Type="String" />
            <asp:Parameter Name="TADSCLA2" Type="String" />
            <asp:Parameter Name="TACTLSE2" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TADSCLA3" Type="String" />
            <asp:Parameter Name="TACTLSE3" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TADSCLA4" Type="String" />
            <asp:Parameter Name="TACTLSE4" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TACDCLA1" Type="String" />
            <asp:Parameter Name="TACDCLA2" Type="String" />
            <asp:Parameter Name="TACDCLA3" Type="String" />
            <asp:Parameter Name="TACDCLA4" Type="String" />
            <asp:Parameter Name="TACDCLA5" Type="String" />
            <asp:Parameter Name="TACDCLA6" Type="String" />
            <asp:Parameter Name="TACDCLA7" Type="String" />
            <asp:Parameter Name="TACDCLA8" Type="String" />
            <asp:Parameter Name="TACDCLA9" Type="String" />
            <asp:Parameter Name="TACDCLA10" Type="String" />
            <asp:Parameter Name="TADTTEC1" Type="String" />
            <asp:Parameter Name="TADTTEC2" Type="String" />
            <asp:Parameter Name="TADTTEC3" Type="String" />
            <asp:Parameter Name="TADTTEC4" Type="String" />
            <asp:Parameter Name="TADTTEC5" Type="String" />
            <asp:Parameter Name="TADTTEC6" Type="String" />
            <asp:Parameter Name="TADTTEC7" Type="String" />
            <asp:Parameter Name="TADTTEC8" Type="String" />
            <asp:Parameter Name="TADTTEC9" Type="String" />
            <asp:Parameter Name="TADTTEC10" Type="String" />
            <asp:Parameter Name="TADTLOT1" Type="String" />
            <asp:Parameter Name="TADTLOT2" Type="String" />
            <asp:Parameter Name="TADTLOT3" Type="String" />
            <asp:Parameter Name="TADTLOT4" Type="String" />
            <asp:Parameter Name="TADTLOT5" Type="String" />
            <asp:Parameter Name="TADTLOT6" Type="String" />
            <asp:Parameter Name="TAUMPESO" Type="String" />
            <asp:Parameter Name="TAUMANCH" Type="String" />
            <asp:Parameter Name="TAUMLARG" Type="String" />
            <asp:Parameter Name="TADTELE1" Type="String" />
            <asp:Parameter Name="TADTELE2" Type="String" />
            <asp:Parameter Name="TADTELE3" Type="String" />
            <asp:Parameter Name="TADTELE4" Type="String" />
            <asp:Parameter Name="TADTELE5" Type="String" />
            <asp:Parameter Name="TADTELE6" Type="String" />
            <asp:Parameter Name="TAESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TACAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="TANMUSER" Type="String" SessionField="UserLogon" />   
            <asp:Parameter Name="TACLAFLO" Type="Int32" />
            <asp:Parameter Name="TAINDALT" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TACLATEC" Type="Int32" />
            <asp:Parameter Name="TANROBAR" Type="Int32" />
            <asp:Parameter Name="TAFRMBAR" Type="String" />
            <asp:Parameter Name="TACONSEC" Type="String" />
            <asp:Parameter Name="TACONCAT" Type="String" />
            <asp:Parameter Name="TACALIFI" Type="String" />
            <asp:Parameter Name="TASUFCON" Type="String" />
            <asp:Parameter Name="TACNTCLA" Type="Int32" />
            <asp:Parameter Name="TAREFINI" Type="String" />
            <asp:Parameter Name="TAVENTA" Type="String" />
            <asp:Parameter Name="TAAUTINC" Type="String" />
            <asp:Parameter Name="TATOLERA" Type="Double" />
            <asp:Parameter Name="TAREPORTE" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" /> 
            <asp:SessionParameter Name="TACODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TATIPPRO" Type="String" />
            <asp:Parameter Name="TANOMBRE" Type="String" />
            <asp:Parameter Name="TACLAVES" Type="Int32" />
            <asp:Parameter Name="TACLAPRO" Type="String" />
            <asp:Parameter Name="TADSCLA1" Type="String" />
            <asp:Parameter Name="TADSCLA2" Type="String" />
            <asp:Parameter Name="TACTLSE2" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TADSCLA3" Type="String" />
            <asp:Parameter Name="TACTLSE3" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TADSCLA4" Type="String" />
            <asp:Parameter Name="TACTLSE4" Type="String" DefaultValue="N"/>
            <asp:Parameter Name="TACDCLA1" Type="String" />
            <asp:Parameter Name="TACDCLA2" Type="String" />
            <asp:Parameter Name="TACDCLA3" Type="String" />
            <asp:Parameter Name="TACDCLA4" Type="String" />
            <asp:Parameter Name="TACDCLA5" Type="String" />
            <asp:Parameter Name="TACDCLA6" Type="String" />
            <asp:Parameter Name="TACDCLA7" Type="String" />
            <asp:Parameter Name="TACDCLA8" Type="String" />
            <asp:Parameter Name="TACDCLA9" Type="String" />
            <asp:Parameter Name="TACDCLA10" Type="String" />
            <asp:Parameter Name="TADTTEC1" Type="String" />
            <asp:Parameter Name="TADTTEC2" Type="String" />
            <asp:Parameter Name="TADTTEC3" Type="String" />
            <asp:Parameter Name="TADTTEC4" Type="String" />
            <asp:Parameter Name="TADTTEC5" Type="String" />
            <asp:Parameter Name="TADTTEC6" Type="String" />
            <asp:Parameter Name="TADTTEC7" Type="String" />
            <asp:Parameter Name="TADTTEC8" Type="String" />
            <asp:Parameter Name="TADTTEC9" Type="String" />
            <asp:Parameter Name="TADTTEC10" Type="String" />
            <asp:Parameter Name="TADTLOT1" Type="String" />
            <asp:Parameter Name="TADTLOT2" Type="String" />
            <asp:Parameter Name="TADTLOT3" Type="String" />
            <asp:Parameter Name="TADTLOT4" Type="String" />
            <asp:Parameter Name="TADTLOT5" Type="String" />
            <asp:Parameter Name="TADTLOT6" Type="String" />
            <asp:Parameter Name="TAUMPESO" Type="String" />
            <asp:Parameter Name="TAUMANCH" Type="String" />
            <asp:Parameter Name="TAUMLARG" Type="String" />
            <asp:Parameter Name="TADTELE1" Type="String" />
            <asp:Parameter Name="TADTELE2" Type="String" />
            <asp:Parameter Name="TADTELE3" Type="String" />
            <asp:Parameter Name="TADTELE4" Type="String" />
            <asp:Parameter Name="TADTELE5" Type="String" />
            <asp:Parameter Name="TADTELE6" Type="String" />
            <asp:Parameter Name="TAESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="TACAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="TANMUSER" Type="String" SessionField="UserLogon" />   
            <asp:Parameter Name="TACLAFLO" Type="Int32" />
            <asp:Parameter Name="TAINDALT" Type="String" DefaultValue="N" />
            <asp:Parameter Name="TACLATEC" Type="Int32" />
            <asp:Parameter Name="TANROBAR" Type="Int32" />
            <asp:Parameter Name="TAFRMBAR" Type="String" />
            <asp:Parameter Name="TACONSEC" Type="String" />
            <asp:Parameter Name="TACONCAT" Type="String" />
            <asp:Parameter Name="TACALIFI" Type="String" />
            <asp:Parameter Name="TASUFCON" Type="String" />
            <asp:Parameter Name="TACNTCLA" Type="Int32" />
            <asp:Parameter Name="TAREFINI" Type="String" />
            <asp:Parameter Name="TAVENTA" Type="String" />
            <asp:Parameter Name="TAAUTINC" Type="String" />
            <asp:Parameter Name="TATOLERA" Type="Double" />
            <asp:Parameter Name="TAREPORTE" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
