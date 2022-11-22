<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="MaestroBodegas.aspx.cs" Inherits="XUSS.WEB.Parametros.MaestroBodegas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsDownload" runat="server">
        <script type="text/javascript">
            function RowContextMenu(sender, eventArgs) {
                //debugger;
                var menu = $find("<%=ct_marcas.ClientID %>");
                var evt = eventArgs.get_domEvent();

                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") {
                    return;
                }

                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = "1-" + index;
                
                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);

                menu.show(evt);

                evt.cancelBubble = true;
                evt.returnValue = false;

                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" >
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_bodegas" runat="server" PageSize="1" AllowPaging="True" OnItemUpdating="rlv_bodegas_OnItemUpdating" OnItemInserting="rlv_bodegas_OnItemInserting"
            OnItemCommand="rlv_bodegas_OnItemCommand" OnItemDataBound="rlv_bodegas_OnItemDataBound" 
            DataSourceID="obj_bodegas" ItemPlaceholderID="pnlGeneral" DataKeyNames="BDBODEGA">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>
                                Maestro Bodegas</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_bodegas" PageSize="1" RenderMode="Lightweight">
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
                                    Maestro Bodegas</h5>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" style="margin: 0 0 0 0">
                                        <tr>
                                            <td>
                                                <div style="padding-top: 7px">                                                    
                                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="" ToolTip="Agregar" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert"  />
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
                                    <telerik:RadTextBox ID="txt_bodega" runat="server"  Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    <label>
                                        Nombre</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombodega" runat="server"  Width="300px">
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
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>C Bodega</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("BDBODEGA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="false" Text='<%# Bind("BDNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Pais</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="false"
                                    DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDPAIS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Ciudad</label></td>
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
                            <td><label>Ubicacion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ubicacion" runat="server" Enabled="false" Text='<%# Bind("BDDIRECC") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Telefono</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="false" Text='<%# Bind("BDTELEFO") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Responsable</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_responsable" runat="server" Enabled="false" Text='<%# Bind("BDRESPON") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>F Cierre</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_feccierre" runat="server" DbSelectedDate='<%# Bind("BDCIERRE") %>' MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>                                                           
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Lst Precios</label>
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("BDCENCOS") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Almacen</label></td>
                            <td>
                                <asp:CheckBox ID="chk_almacen" runat="server" Checked='<%# this.GetEstado(Eval("BDALMACE")) %>' Enabled="false" />
                            </td>     
                            <td><label>Consignacion</label></td>
                            <td>
                                <asp:CheckBox ID="chk_consignacion" runat="server" Checked='<%# this.GetEstado(Eval("BDCONSIG")) %>' Enabled="false" />
                            </td>  
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Tecnicos">
                            </telerik:RadTab>                        
                            <telerik:RadTab Text="Linea">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Usuarios x Bodega">
                            </telerik:RadTab>                          
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                            <table>
                                <tr>
                                    <td><label>DT 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nmdt1" runat="server" Enabled="false" Text='<%# Bind("BDCDTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>                                
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dttec1" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="false" DataSourceID="obj_dt1" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDDTTEC1") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">                            
                            <MasterTableView >                                
                                <Columns>   
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("ABTIPPRO")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>  
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridTemplateColumn HeaderText="Lote" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_lote" runat="server" Checked='<%# this.GetEstado(Eval("ABMNLOTE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Elemento" UniqueName="celemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNELEM")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="No Elemento" UniqueName="cnroelemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_noelemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNNREL")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Bon X Elem" UniqueName="cbonelem" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_bonxelem" runat="server" Checked='<%# this.GetEstado(Eval("ABMNBONI")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Inv Contenedor" UniqueName="cinvcont" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_invconte" runat="server" Checked='<%# this.GetEstado(Eval("ABMNCONT")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Nro Elem Auto" UniqueName="cnroelemauto" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemauto" runat="server" Checked='<%# this.GetEstado(Eval("ABELEMUAT")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_usuarioxbodega" runat="server">                            
                            <telerik:RadGrid ID="rg_usuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">                            
                            <MasterTableView>                                
                                <Columns>   
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="ceestus" HeaderStyle-Width="30" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_estado" runat="server" Checked='<%# this.GetEstado(Eval("BUCDUSER")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>  
                                    <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="500px"
                                        HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                        UniqueName="usua_nombres">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>   
                                </Columns>
                            </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>                
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>                
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>C Bodega</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="false" Text='<%# Bind("BDBODEGA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("BDNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Pais</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                    DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDPAIS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rc_pais_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Ciudad</label></td>
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
                            <td><label>Ubicacion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ubicacion" runat="server" Enabled="true" Text='<%# Bind("BDDIRECC") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Telefono</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("BDTELEFO") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Responsable</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_responsable" runat="server" Enabled="true" Text='<%# Bind("BDRESPON") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>F Cierre</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_feccierre" runat="server" DbSelectedDate='<%# Bind("BDCIERRE") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>                                                           
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Lst Precios</label>
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("BDCENCOS") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Almacen</label></td>
                            <td>
                                <asp:CheckBox ID="chk_almacen" runat="server" Checked='<%# this.GetEstado(Eval("BDALMACE")) %>' Enabled="true" />
                            </td>
                            <td><label>Consignacion</label></td>
                            <td>
                                <asp:CheckBox ID="chk_consignacion" runat="server" Checked='<%# this.GetEstado(Eval("BDCONSIG")) %>' Enabled="true" />
                            </td>  
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Tecnicos">
                            </telerik:RadTab>                        
                            <telerik:RadTab Text="Linea">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Usuarios x Bodega">
                            </telerik:RadTab>                          
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                            <table>
                                <tr>
                                    <td><label>DT 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nmdt1" runat="server" Enabled="true" Text='<%# Bind("BDCDTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>                                
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dttec1" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_dt1" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDDTTEC1") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">                            
                                <ClientSettings>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                </ClientSettings>
                            <MasterTableView >                                
                                <Columns>     
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("ABTIPPRO")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="TATIPPRO" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="Ln" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TATIPPRO"
                                        UniqueName="TATIPPRO">                                        
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridTemplateColumn HeaderText="Lote" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_lote" runat="server" Checked='<%# this.GetEstado(Eval("ABMNLOTE")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Elemento" UniqueName="celemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNELEM")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="No Elemento" UniqueName="cnroelemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_noelemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNNREL")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Bon X Elem" UniqueName="cbonelem" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_bonxelem" runat="server" Checked='<%# this.GetEstado(Eval("ABMNBONI")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Inv Contenedor" UniqueName="cinvcont" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_invconte" runat="server" Checked='<%# this.GetEstado(Eval("ABMNCONT")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Nro Elem Auto" UniqueName="cnroelemauto" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemauto" runat="server" Checked='<%# this.GetEstado(Eval("ABELEMUAT")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_usuarioxbodega" runat="server">                            
                            <telerik:RadGrid ID="rg_usuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_usuarios_OnNeedDataSource"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">                            
                            <MasterTableView>                                
                                <Columns>   
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="ceestus" HeaderStyle-Width="30" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_estado" runat="server" Checked='<%# this.GetEstado(Eval("BUCDUSER")) %>'  />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>  
                                    <telerik:GridBoundColumn DataField="usua_usuario" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_usuario"
                                        UniqueName="usua_usuario">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="500px"
                                        HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                        UniqueName="usua_nombres">
                                        <ItemStyle HorizontalAlign="Left" />
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
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>          
                </asp:Panel>
            </EditItemTemplate>
            <InsertItemTemplate>                
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td><label>C Bodega</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_traslado" runat="server" Enabled="true" Text='<%# Bind("BDBODEGA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Text='<%# Bind("BDNOMBRE") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Pais</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_pais" runat="server" Culture="es-CO" Width="300px" Enabled="true"
                                    DataSourceID="obj_pais" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDPAIS") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rc_pais_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Ciudad</label></td>
                            <td colspan="2">
                                <telerik:RadComboBox ID="rc_ciudad" runat="server" Culture="es-CO" Width="300px" AllowCustomText="true" Filter="Contains" 
                                    Enabled="true" AppendDataBoundItems="true" >
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Ubicacion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_ubicacion" runat="server" Enabled="true" Text='<%# Bind("BDDIRECC") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>Telefono</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_telefono" runat="server" Enabled="true" Text='<%# Bind("BDTELEFO") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td><label>Responsable</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_responsable" runat="server" Enabled="true" Text='<%# Bind("BDRESPON") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>
                            <td><label>F Cierre</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_feccierre" runat="server" DbSelectedDate='<%# Bind("BDCIERRE") %>' MinDate="01/01/1900" Enabled="true">
                                </telerik:RadDatePicker>                                                           
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Lst Precios</label>
                            </td>
                            <td>
                                 <telerik:RadComboBox ID="rc_lstprecio" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_lstprecio" DataTextField="P_CNOMBRE" SelectedValue='<%# Bind("BDCENCOS") %>'
                                    DataValueField="P_CLISPRE" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Almacen</label></td>
                            <td>
                                <asp:CheckBox ID="chk_almacen" runat="server" Checked='<%# this.GetEstado(Eval("BDALMACE")) %>' Enabled="true" />
                            </td>
                            <td><label>Consignacion</label></td>
                            <td>
                                <asp:CheckBox ID="chk_consignacion" runat="server" Checked='<%# this.GetEstado(Eval("BDCONSIG")) %>' Enabled="true" />
                            </td>  
                        </tr>
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Tecnicos">
                            </telerik:RadTab>                        
                            <telerik:RadTab Text="Linea">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Usuarios x Bodega">
                            </telerik:RadTab>                          
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_datos" runat="server">
                            <table>
                                <tr>
                                    <td><label>DT 1</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nmdt1" runat="server" Enabled="true" Text='<%# Bind("BDCDTEC1") %>' Width="300px">
                                        </telerik:RadTextBox>                                
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_dttec1" runat="server" Culture="es-CO" Width="300px"
                                            Enabled="true" DataSourceID="obj_dt1" DataTextField="TTDESCRI" SelectedValue='<%# Bind("BDDTTEC1") %>'
                                            DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_lineaxbodega" runat="server">
                            <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_items_OnNeedDataSource"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True">                    
                                <ClientSettings>
                                    <ClientEvents OnRowContextMenu="RowContextMenu"></ClientEvents>
                                </ClientSettings>
                            <MasterTableView >                                
                                <Columns>     
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_habilita" runat="server" Checked='<%# this.GetEstado(Eval("ABTIPPRO")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="TATIPPRO" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                        HeaderText="Ln" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TATIPPRO"
                                        UniqueName="TATIPPRO">                                        
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                        UniqueName="TANOMBRE">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridTemplateColumn HeaderText="Lote" UniqueName="clote" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_lote" runat="server" Checked='<%# this.GetEstado(Eval("ABMNLOTE")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Elemento" UniqueName="celemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNELEM")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="No Elemento" UniqueName="cnroelemento" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_noelemento" runat="server" Checked='<%# this.GetEstado(Eval("ABMNNREL")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Bon X Elem" UniqueName="cbonelem" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_bonxelem" runat="server" Checked='<%# this.GetEstado(Eval("ABMNBONI")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Inv Contenedor" UniqueName="cinvcont" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_invconte" runat="server" Checked='<%# this.GetEstado(Eval("ABMNCONT")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                    <telerik:GridTemplateColumn HeaderText="Nro Elem Auto" UniqueName="cnroelemauto" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_elemauto" runat="server" Checked='<%# this.GetEstado(Eval("ABELEMUAT")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                            
                                </Columns>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                            </telerik:RadGrid>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_usuarioxbodega" runat="server">                            
                            <telerik:RadGrid ID="rg_usuarios" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnNeedDataSource="rg_usuarios_OnNeedDataSource"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True">                            
                            <MasterTableView>                                
                                <Columns>   
                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="ceestus" HeaderStyle-Width="30" >
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_estado" runat="server" Checked='<%# this.GetEstado(Eval("BUCDUSER")) %>'  />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>  
                                    <telerik:GridBoundColumn DataField="usua_usuario" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Id" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_usuario"
                                        UniqueName="usua_usuario">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>   
                                    <telerik:GridBoundColumn DataField="usua_nombres" HeaderButtonType="TextButton" HeaderStyle-Width="500px"
                                        HeaderText="Usuario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="usua_nombres"
                                        UniqueName="usua_nombres">
                                        <ItemStyle HorizontalAlign="Left" />
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
                                <%--<asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />--%>
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar"  RenderMode="Lightweight"/>
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar"  RenderMode="Lightweight" CausesValidation="false"/>
                            </td>
                        </tr>
                    </table>          
                </asp:Panel>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadContextMenu ID="ct_marcas" runat="server" EnableRoundedCorners="true" EnableShadows="true" OnItemClick="ct_marcas_ItemClick" Skin="Bootstrap">
        <Items>
            <telerik:RadMenuItem Text="Quitar Seleccion">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Seleccionar Todos">
            </telerik:RadMenuItem>            
        </Items>
    </telerik:RadContextMenu>
    <asp:ObjectDataSource ID="obj_bodegas" runat="server" OldValuesParameterFormatString="original_{0}" OnUpdating="obj_bodegas_OnUpdating" OnInserting="obj_bodegas_OnInserting"
        SelectMethod="GetBodegas" InsertMethod="InsertBodega" TypeName="XUSS.BLL.Parametros.BodegaBL" UpdateMethod="UpdateBodega">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />     
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />       
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="BDCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="BDBODEGA" Type="String" />
            <asp:Parameter Name="BDNOMBRE" Type="String" />
            <asp:Parameter Name="BDDIRECC" Type="String" />
            <asp:Parameter Name="BDRESPON" Type="String" />
            <asp:Parameter Name="BDBODCON" Type="String" DefaultValue="01" />
            <asp:Parameter Name="BDCIERRE" Type="DateTime" />
            <asp:Parameter Name="BDCONSIG" Type="String" />
            <asp:Parameter Name="BDESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="BDCAUSAE" Type="String" DefaultValue="." />            
            <asp:SessionParameter Name="BDNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="BDMNCAJA" Type="String" />
            <asp:Parameter Name="BDBODSOC" Type="String" />
            <asp:Parameter Name="BDCAJADF" Type="Int32" />
            <asp:Parameter Name="BDCENCOS" Type="String" />
            <asp:Parameter Name="BDPORMAX" Type="String" />
            <asp:Parameter Name="BDCAJAOP" Type="String" />
            <asp:Parameter Name="BDTELEFO" Type="String" />
            <asp:Parameter Name="BDALMACE" Type="String" />
            <asp:Parameter Name="BDPAIS" Type="String" />
            <asp:Parameter Name="CDCIUDAD" Type="String" />
            <asp:Parameter Name="BDCDTEC1" Type="String" />
            <asp:Parameter Name="BDDTTEC1" Type="String" />
            <asp:Parameter Name="tbArtXBod" Type="Object" />
            <asp:Parameter Name="tbUsrXBod" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:SessionParameter Name="BDCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="original_BDBODEGA" Type="String" />
            <asp:Parameter Name="BDNOMBRE" Type="String" />
            <asp:Parameter Name="BDDIRECC" Type="String" />
            <asp:Parameter Name="BDRESPON" Type="String" />
            <asp:Parameter Name="BDBODCON" Type="String" DefaultValue="01" />
            <asp:Parameter Name="BDCIERRE" Type="DateTime" />
            <asp:Parameter Name="BDCONSIG" Type="String" />
            <asp:Parameter Name="BDESTADO" Type="String" DefaultValue="." />
            <asp:Parameter Name="BDCAUSAE" Type="String" DefaultValue="AC" />            
            <asp:SessionParameter Name="BDNMUSER" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="BDMNCAJA" Type="String" />
            <asp:Parameter Name="BDBODSOC" Type="String" />
            <asp:Parameter Name="BDCAJADF" Type="Int32" />
            <asp:Parameter Name="BDCENCOS" Type="String" />
            <asp:Parameter Name="BDPORMAX" Type="String" />
            <asp:Parameter Name="BDCAJAOP" Type="String" />
            <asp:Parameter Name="BDTELEFO" Type="String" />
            <asp:Parameter Name="BDALMACE" Type="String" />
            <asp:Parameter Name="BDPAIS" Type="String" />
            <asp:Parameter Name="CDCIUDAD" Type="String" />
            <asp:Parameter Name="BDCDTEC1" Type="String" />
            <asp:Parameter Name="BDDTTEC1" Type="String" />
            <asp:Parameter Name="tbArtXBod" Type="Object" />
            <asp:Parameter Name="tbUsrXBod" Type="Object" />
        </UpdateParameters>
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
    <asp:ObjectDataSource ID="obj_pais" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="PAIS" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_dt1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="BODDT1" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
