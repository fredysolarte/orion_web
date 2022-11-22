<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ProntaModa.aspx.cs" Inherits="XUSS.WEB.Compras.ProntaModa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <script type="text/javascript">
            function conditionalPostback(sender, args) {
                console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_traslados$ctrl0$rg_items$ctl00$ctl02$ctl00$ExportToExcelButton")) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadListView ID="rlv_prontamoda" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_prontamoda_OnItemInserted"
            OnItemCommand="rlv_prontamoda_OnItemCommand" OnItemDataBound="rlv_prontamoda_OnItemDataBound"
            DataSourceID="obj_prontamoda" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Pronta Moda</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_prontamoda" PageSize="1">
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
                                <h5>Pronta Moda</h5>
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
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Buscar" ValidationGroup="gvBuscar">
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
                            <telerik:RadTextBox ID="txt_conse" runat="server" Enabled="false" Text='<%# Bind("ICCONSE") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>                                
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("ICFECHA") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                            </td>
                            <td><label>Ref. Proveedor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_refproveedor" runat="server" Enabled="false" Text='<%# Bind("ICREFERENCIA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="true" Visible="false"
                                    Text='<%# Bind("ICPROVEE") %>' 
                                    Width="250px">
                                </telerik:RadTextBox> 
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Eval("TRCODNIT") %>' 
                                    Width="250px">
                                </telerik:RadTextBox>                                 
                            </td>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" 
                                    Text='<%# Eval("NOM_TER") %>' 
                                    Width="300px">
                                </telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td><label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ICMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Precio</label></td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="false" DbValue='<%# Bind("ICCOSTO") %>'  EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>                        
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Articulo">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Precosteo">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Observaciones">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Inventarios">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_darticulo" runat="server">
                            <table>
                                <tr>
                                    <td rowspan="4">
                                        <telerik:RadBinaryImage runat="server" ID="txt_foto" AutoAdjustImageControlSize="false"
                                            Width="100px" Height="170px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Linea</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_categoria" runat="server" SelectedValue='<%# Bind("ICTIPPRO") %>'
                                            Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                            Enabled="false" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td><label>Referencia</label></td>
                                    <td>
                                         <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Text='<%# Bind("ICCLAVE1") %>' Width="300px">
                                            </telerik:RadTextBox>
                                    </td>
                                </tr>
                                
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_dprecosteo" runat="server">
                             <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" 
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" > <%----%>
                                     <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="CC_CONSE">
                                        <CommandItemSettings  ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" /> 
                                            <telerik:GridBoundColumn DataField="CC_CONSE" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CC_CONSE"
                                                UniqueName="CC_CONSE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                                UniqueName="ARCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CC_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="CC_CANTIDAD" UniqueName="CC_CANTIDAD" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataType="System.Decimal"
                                                DataFormatString="{0:C}" HeaderText="Costo" ItemStyle-HorizontalAlign="Right" 
                                                Resizable="true" SortExpression="CC_VALOR" UniqueName="CC_VALOR">
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
                        <telerik:RadPageView ID="pv_dobservaciones" runat="server">
                            <table>
                                <tr>
                                    <td><label>Observaciones</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="false" Width="300px" TextMode="MultiLine" Height="90px" >
                                            </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_invetarios" runat="server">
                            <telerik:RadGrid ID="rg_detalle_inv" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" 
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_detalle_inv_OnNeedDataSource" >
                                     <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="CC_CONSE">
                                        <CommandItemSettings  ShowRefreshButton="false" ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" /> 
                                            <telerik:GridBoundColumn DataField="CC_CONSE" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CC_CONSE"
                                                UniqueName="CC_CONSE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                                UniqueName="ARCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CC_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="CC_CANTIDAD" UniqueName="CC_CANTIDAD">
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
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("ICFECHA") %>'
                                                MinDate="01/01/1900" Enabled="true">
                                            </telerik:RadDatePicker>
                            </td>
                            <td><label>Ref. Proveedor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_refproveedor" runat="server" Enabled="true" Text='<%# Bind("ICREFERENCIA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="true" Visible="false"
                                    Text='<%# Bind("ICPROVEE") %>' 
                                    Width="250px">
                                </telerik:RadTextBox> 
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Eval("TRCODNIT") %>' 
                                    Width="250px">
                                </telerik:RadTextBox> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_nit" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)" >
                                    <asp:Image ID="Image10" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick"  />
                            </td>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" 
                                    Text='<%# Eval("NOM_TER") %>' 
                                    Width="300px">
                                </telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td><label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ICMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Precio</label></td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="true" DbValue='<%# Bind("ICCOSTO") %>'  EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                    ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>                         
                </asp:Panel>
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <telerik:RadTextBox ID="txt_conse" runat="server" Enabled="false" Text='<%# Bind("ICCONSE") %>' Width="300px" Visible="false">
                                </telerik:RadTextBox>                                
                            <td>
                                <label>Fecha</label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecha" runat="server" DbSelectedDate='<%# Bind("ICFECHA") %>'
                                                MinDate="01/01/1900" Enabled="false">
                                            </telerik:RadDatePicker>
                            </td>
                            <td><label>Ref. Proveedor</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_refproveedor" runat="server" Enabled="false" Text='<%# Bind("ICREFERENCIA") %>' Width="300px">
                                </telerik:RadTextBox>                                
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <label>Identificacion</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="true" Visible="false"
                                    Text='<%# Bind("ICPROVEE") %>' 
                                    Width="250px">
                                </telerik:RadTextBox> 
                                <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="false" ValidationGroup="gvInsert"
                                    Text='<%# Eval("TRCODNIT") %>' 
                                    Width="250px">
                                </telerik:RadTextBox>                                 
                            </td>
                            <td>
                                <label>Tercero</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" 
                                    Text='<%# Eval("NOM_TER") %>' 
                                    Width="300px">
                                </telerik:RadTextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td><label>Moneda</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("ICMONEDA") %>'
                                    DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td><label>Precio</label></td>
                            <td>
                                <telerik:RadNumericTextBox runat="server" ID="txt_precio" Width="300px" Enabled="false" DbValue='<%# Bind("ICCOSTO") %>'  EnabledStyle-HorizontalAlign="Right">
                                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>                        
                    </table>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Datos Articulo">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Precosteo">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Observaciones">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Datos Inventarios">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_darticulo" runat="server">
                            <table>
                                <tr>
                                    <td rowspan="4">
                                        <telerik:RadBinaryImage runat="server" ID="txt_foto" AutoAdjustImageControlSize="false"
                                            Width="100px" Height="170px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><label>Linea</label></td>
                                    <td>
                                        <telerik:RadComboBox ID="rc_categoria" runat="server" SelectedValue='<%# Bind("ICTIPPRO") %>'
                                            Culture="es-CO" Width="300px" DataSourceID="obj_tippro" DataTextField="TANOMBRE"
                                            Enabled="true" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Selecionar" Value="" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rqf_categoria" runat="server" ControlToValidate="rc_categoria" InitialValue="Seleccionar"
                                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                    </td>
                                    <td><label>Referencia</label></td>
                                    <td>
                                         <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="true" Text='<%# Bind("ICCLAVE1") %>' Width="300px">
                                            </telerik:RadTextBox>
                                    </td>
                                </tr>
                                
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_dprecosteo" runat="server">
                             <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource" >
                                     <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="CC_CONSE">
                                        <CommandItemSettings  ShowRefreshButton="false" ShowAddNewRecordButton="true" AddNewRecordText="Nuevo Item" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" /> 
                                            <telerik:GridBoundColumn DataField="CC_CONSE" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CC_CONSE"
                                                UniqueName="CC_CONSE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Linea" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                                UniqueName="TANOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                                UniqueName="ARCLAVE1">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                                UniqueName="ARNOMBRE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CC_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="CC_CANTIDAD" UniqueName="CC_CANTIDAD" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataType="System.Decimal"
                                                DataFormatString="{0:C}" HeaderText="Costo" ItemStyle-HorizontalAlign="Right" 
                                                Resizable="true" SortExpression="CC_VALOR" UniqueName="CC_VALOR">
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
                        <telerik:RadPageView ID="pv_dobservaciones" runat="server">
                            <table>
                                <tr>
                                    <td><label>Observaciones</label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_observaciones" runat="server" Enabled="true" Width="100%" TextMode="MultiLine" Height="150px" >
                                            </telerik:RadTextBox>
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="pv_invetarios" runat="server">
                            <telerik:RadGrid ID="rg_detalle_inv" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_detalle_inv_OnItemCommand"
                                    Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_detalle_inv_OnNeedDataSource" >
                                     <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="IC_CONSE" >
                                        <CommandItemSettings  ShowRefreshButton="false" ShowAddNewRecordButton="true" AddNewRecordText="Nuevo Item" />
                                        <Columns>
                                            <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete" 
                                                ButtonType="ImageButton" CommandName="Delete"  HeaderStyle-Width="20px" /> 
                                            <telerik:GridBoundColumn DataField="IC_CONSE" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                                                HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="IC_CONSE"
                                                UniqueName="IC_CONSE">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>                                           
                                            <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                                HeaderText="C2" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE2"
                                                UniqueName="CLAVE2">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="460px"
                                                HeaderText="C3" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CLAVE3"
                                                UniqueName="CLAVE3">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IC_CANTIDAD" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                                DataFormatString="&lt;strong&gt;{0:#.###}&lt;/strong&gt;" HeaderText="Cant" ItemStyle-HorizontalAlign="Right"
                                                Resizable="true" SortExpression="IC_CANTIDAD" UniqueName="IC_CANTIDAD" FooterText="Total: "
                                                Aggregate="Sum">
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
                    </telerik:RadMultiPage>      
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar"
                                    ValidationGroup="gvInsert" />
                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>          
                </asp:Panel>
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;" >
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpTerceros" runat="server" Width="900px" Height="320px" Modal="true" OffsetElementID="main"  Title="Buscar Tercero(Proveedor)"  EnableShadow="true">                    
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Identificacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_identificacion" runat="server" Enabled="true" Width="150px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <label>
                                        Contiene</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="edt_nomtercero" runat="server" Enabled="true" Width="350px">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn_filtroTer" runat="server" Text="Filtrar" OnClick="btn_filtroTer_OnClick"
                                        CommandName="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server">
                            <telerik:RadGrid ID="rgConsultaTerceros" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="10" CellSpacing="0" GridLines="None"
                                DataSourceID="obj_terceros" OnItemCommand="rgConsultaTerceros_OnItemCommand">
                                <MasterTableView DataKeyNames="TRCODTER,TRCODNIT,TRNOMBRE,TRNOMBR2,TRAPELLI">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODTER" HeaderText="Codigo"
                                            UniqueName="TRCODTER" HeaderButtonType="TextButton" DataField="TRCODTER" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="50px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRCODNIT" HeaderText="Ident"
                                            UniqueName="TRCODNIT" HeaderButtonType="TextButton" DataField="TRCODNIT" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="90px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBRE" HeaderText="Nombre"
                                            UniqueName="TRNOMBRE" HeaderButtonType="TextButton" DataField="TRNOMBRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRNOMBR2" HeaderText=""
                                            UniqueName="TRNOMBR2" HeaderButtonType="TextButton" DataField="TRNOMBR2" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Resizable="true" SortExpression="TRLISPRE" HeaderText="Lta Precio"
                                            UniqueName="TRLISPRE" HeaderButtonType="TextButton" DataField="TRLISPRE" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="80px">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div id="message" runat="server">
                                            <div id="box-messages" class="box">
                                                <div class="messages">
                                                    <div id="message-notice" class="message message-notice">
                                                        <div class="image">
                                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                        </div>
                                                        <div class="text">
                                                            <h6>Información</h6>
                                                            <span>No existen Resultados </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpInventario" runat="server" Width="900px" Height="320px" Modal="true" OffsetElementID="main"  Title="Curva"  EnableShadow="true" Style="z-index: 100001;">                    
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td><asp:Label ID="lbl_c2" runat="server" Text="C2" ></asp:Label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_alterna2" runat="server" Enabled="true" Width="600px">
                                    </telerik:RadTextBox>
                                    <telerik:RadComboBox ID="rc_alterna2" runat="server" ZIndex="1000000"
                                        Culture="es-CO" Width="300px" Enabled="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_c3" runat="server" Text="C3"></asp:Label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_alterna3" runat="server" Enabled="true" Width="600px">
                                    </telerik:RadTextBox>
                                    <telerik:RadComboBox ID="rc_alterna3" runat="server" ZIndex="1000000"
                                        Culture="es-CO" Width="300px" Enabled="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td><label>Cantidad</label></td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txt_cantidad" runat="server" Enabled="true" ValidationGroup="grNuevo">
                                                        </telerik:RadNumericTextBox>   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarInv" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbAdd" OnClick="btn_agregarInv_OnClick" ValidationGroup="grNuevoI" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="900px" Height="360px" Modal="true" OffsetElementID="main"  Title="Detalle"  EnableShadow="true">
                    <ContentTemplate>
                    <table>    
                        <%--<tr>
                            <td>Cod Barras</td>
                            <td>
                                <telerik:RadTextBox ID="txt_barras" runat="server" Enabled="true" Visible="true" ValidationGroup="grNuevo" AutoPostBack="true" OnTextChanged="txt_barras_OnTextChanged">
                                </telerik:RadTextBox>    
                            </td>
                        </tr>--%>                    
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
                                <telerik:RadTextBox ID="txt_referencia" runat="server" Enabled="false" Visible="true" >
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
                                <asp:ImageButton ID="iBtnFindArticulo" runat="server" CommandName="Buscar" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindArticulo_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td><label>C2</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nc2" runat="server" Enabled="true" Visible="true">
                                </telerik:RadTextBox>
                            </td>
                            <td><label>C3</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nc3" runat="server" Enabled="true" Visible="true">
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
                                <telerik:RadNumericTextBox ID="txt_preciolta" runat="server" Enabled="false">
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
                                    DropDownWidth="315" AppendDataBoundItems="true" >
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>                                    
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rc_lote" 
                                                        ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="Seleccionar">
                                <asp:Image ID="Image11" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Cantidad</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_cantidadcon" runat="server" Enabled="true" ValidationGroup="grNuevoI" CausesValidation="false">
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rqf_catidad1" runat="server" ControlToValidate="txt_cantidadcon"
                                    ErrorMessage="(*)" ValidationGroup="grNuevoI">
                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>                                
                                <asp:RequiredFieldValidator ID="rqf_catidad2" runat="server" ControlToValidate="txt_cantidadcon"
                                    ErrorMessage="(*)" ValidationGroup="grNuevoI" InitialValue="0">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_agregar" runat="server" Text="Aceptar" OnClick="btn_agregar_Aceptar" ValidationGroup="grNuevoI"/>
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
                                <asp:Button ID="btn_filtroArticulos" runat="server" Text="Filtrar" OnClick="btn_filtroArticulos_OnClick"
                                    CommandName="xxxxxx" />
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
                                        HeaderStyle-Width="20px" >
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
                                    <div id="message" runat="server">
                                        <div id="box-messages" class="box">
                                            <div class="messages">
                                                <div id="message-notice" class="message message-notice">
                                                    <div class="image">
                                                        <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                                    </div>
                                                    <div class="text">
                                                        <h6>Información</h6>
                                                        <span>No existen Resultados </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_prontamoda" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertProntaModa" OnInserted="obj_prontamoda_OnInserted"
        SelectMethod="GetProntaModa" TypeName="XUSS.BLL.Compras.ProntaModaBL"  >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />                        
        </SelectParameters>    
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="ICCODEMP" Type="String" DefaultValue="001" />
            <asp:Parameter Name="ICTIPPRO" Type="String" />
            <asp:Parameter Name="ICCLAVE1" Type="String" />
            <asp:Parameter Name="ICPROVEE" Type="Int32" />
            <asp:Parameter Name="ICREFERENCIA" Type="String" />
            <asp:Parameter Name="ICCANTIDAD" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="ICMONEDA" Type="String" />
            <asp:Parameter Name="ICCOSTO" Type="Double" />
            <asp:Parameter Name="ICFECHA" Type="DateTime" />
            <asp:Parameter Name="ICOBSERVACION" Type="String" />            
            <asp:SessionParameter Name="ICCDUSER" Type="String" SessionField="UserLogon" />            
            <asp:Parameter Name="ICESTADO" Type="String" DefaultValue="AC" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
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
    <asp:ObjectDataSource ID="obj_tippro" runat="server" OldValuesParameterFormatString="original_{0}"
     SelectMethod="GetTiposProducto" TypeName="XUSS.BLL.Comun.ComunBL">
     <SelectParameters>
        <asp:Parameter Name="connection" Type="String" DefaultValue="" />
     </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_articulos" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetArticulos" TypeName="XUSS.BLL.Pedidos.PedidosBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue=" AND 1=0" Name="filter" Type="String" />
            <asp:Parameter Name="inBodega" Type="String" />
            <asp:Parameter Name="LT" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
