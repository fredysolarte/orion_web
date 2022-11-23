<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="NotasDebito.aspx.cs" Inherits="XUSS.WEB.Facturacion.NotasDebito" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
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
        <script type="text/javascript">
            function Clicking(sender, args) {
                if (!confirm("¿Esta Seguro de Anular el Registro?"))
                    args.set_cancel(!confirmed);
            }

            function chk_habilita_OnClick(obj) {
                //alert(obj.checked);
                debugger;

                var ln_subtotal = 0, ln_iva = 0, ln_total = 0;

                var masterTable = $find("<%= rg_detalle.ClientID %>").get_masterTableView();
                var row = masterTable.get_dataItems();
                for (var i = 0; i < row.length; i++) {
                    var chk = masterTable.get_dataItems()[i].findElement("chk_habilita");
                    if (chk.checked) {
                        var control = masterTable.get_dataItems()[i].findElement("txt_total");
                        var control1 = masterTable.get_dataItems()[i].findElement("txt_totalnew");
                        if (parseFloat(control1.value) == 0) {
                            control = masterTable.get_dataItems()[i].findControl("txt_total");
                            control1 = masterTable.get_dataItems()[i].findControl("txt_totalnew");
                            $telerik.findControl(masterTable.get_element().parentNode, control1._clientID).set_value($telerik.findControl(masterTable.get_element().parentNode, control._clientID).get_value());
                        }
                        control1 = masterTable.get_dataItems()[i].findElement("txt_totalnew");
                        ln_total += parseFloat(control1.value.replace(",", ""));
                    } else {
                        control = masterTable.get_dataItems()[i].findControl("txt_total");
                        control1 = masterTable.get_dataItems()[i].findControl("txt_totalnew");
                        $telerik.findControl(masterTable.get_element().parentNode, control1._clientID).set_value(0);
                    }
                }

                document.getElementById("<%=lbl_subtotal.ClientID %>").innerHTML = parseFloat(ln_subtotal);
                document.getElementById("<%=lbl_iva.ClientID %>").innerHTML = parseFloat(ln_iva);
                document.getElementById("<%=lbl_total.ClientID %>").innerHTML = parseFloat(ln_total);

                //lb_sub.innerHTML = ln_subtotal;
                //lb_iva.innerHTML = ln_iva;
                //lb_tot.innerHTML = ln_total;
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000" EnablePageMethods="true">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_notas" runat="server" PageSize="1" OnItemInserted="rlv_notas_OnItemInserted" OnItemInserting="rlv_notas_OnItemInserting"
            AllowPaging="True" Width="100%" OnItemCommand="rlv_notas_OnItemCommand" OnItemDataBound="rlv_notas_OnItemDataBound"
            DataSourceID="obj_notas" ItemPlaceholderID="pnlGeneral" DataKeyNames="NH_NRONOTA" DataSourceCount="0">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Notas Debito</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_notas" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Notas Debito</h5>
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
                                        Nombre/Apellido</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nombre" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>T Nota</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="true" DataSourceID="obj_tipnota" DataTextField="TFNOMBRE"
                                        DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Nota</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nronota" runat="server" Enabled="true" Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Factura</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_factura" runat="server" Enabled="true" Width="300px">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_eliminar" runat="server" Text="Anular" Icon-PrimaryIconCssClass="rbRemove" CommandName="Delete" OnClientClicked="Clicking" ToolTip="Anular Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="iBtnImprimir_OnClick" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>T. Nota</label>
                            </td>
                            <td>                                
                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                    Enabled="false" DataSourceID="obj_tipnota" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("NH_TIPFAC") %>'
                                    DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Nota</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nronota" runat="server" Enabled="false" Text='<%# Bind("NH_NRONOTA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Fec. Nota</label></td>
                            <td>

                                <telerik:RadDatePicker ID="txt_fecnota" runat="server" Enabled="false" SelectedDate='<%# Bind("NH_FECNOTA") %>' Width="300px">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Documento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="false" Text='<%# Bind("TRCODNIT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>N. Tercero</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nomter" runat="server" Enabled="false" Text='<%# Bind("NOM_TER") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><label>Sucursal</label></td>
                            <td colspan ="3">
                                <telerik:RadComboBox ID="rc_sucursal" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="false" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="false" Text='<%# Bind("NH_DESCRIPCION") %>' Width="600px" TextMode="MultiLine" Height="60px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Bind("NH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                             <%--<td>
                                <label>Tasa</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_tasa" runat="server" Enabled="false" ValidationGroup="gvInsert" DbValue='<%# Bind("NH_TASA") %>' Width="300px">
                                </telerik:RadNumericTextBox>                                                               
                            </td>--%>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnDeleteCommand="rg_items_OnDeleteCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <MasterTableView ShowGroupFooter="true" DataKeyNames="ND_NROITEM">
                                <CommandItemSettings />
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="DTTIPFAC" HeaderText="T. Doc" HeaderStyle-Width="150px" Visible="false"
                                        Resizable="true" SortExpression="DTTIPFAC" UniqueName="DTTIPFAC">
                                        <ItemTemplate>
                                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="150px" Visible="false"
                                                Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("DTTIPFAC") %>'
                                                DataValueField="TFTIPFAC">
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="DTNROFAC" HeaderText="Referencia" UniqueName="DTNROFAC_TK"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DTNROFAC" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_nrofac" runat="server" Text='<%# Eval("DTNROFAC") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="DTTIPFAC" HeaderText="Referencia" UniqueName="DTTIPFAC_TK"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DTTIPFAC" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_tipfac" runat="server" Text='<%# Eval("DTTIPFAC") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="link" UniqueName="DTNROFAC" DataTextField="DTNROFAC"
                                            HeaderText="Nro Documento" HeaderStyle-Width="100px">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="DTNROITM" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROITM"
                                        UniqueName="DTNROITM">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <%--<telerik:GridTemplateColumn DataField="ND_DESCRIPCION" HeaderText="T. Doc" HeaderStyle-Width="360px" Visible="true"
                                        Resizable="true" SortExpression="ND_DESCRIPCION" UniqueName="ND_DESCRIPCION">
                                        <ItemTemplate>
                                            <telerik:RadTextBox ID="txt_observacion" runat="server" Enabled="true" Text='<%# Bind("ND_DESCRIPCION") %>' Width="300px" Visible="true">
                                            </telerik:RadTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>

                                    <telerik:GridBoundColumn DataField="ND_DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_DESCRIPCION"
                                        UniqueName="ND_DESCRIPCION">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ND_SUBTOTAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr Subtotal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_SUBTOTAL"
                                        UniqueName="ND_SUBTOTAL" Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ND_IMPUESTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr. Imp" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_IMPUESTO"
                                        UniqueName="ND_IMPUESTO" Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ND_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_VALOR"
                                        UniqueName="ND_VALOR" Aggregate="Sum">
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
                    </asp:Panel>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                             <td>
                                 <label>T. Nota</label>
                             </td>
                             <td>
                                 <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="300px"
                                     Enabled="true" DataSourceID="obj_tipnota" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("NH_TIPFAC") %>'
                                     DataValueField="TFTIPFAC" AppendDataBoundItems="true">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                     </Items>
                                 </telerik:RadComboBox>
                                 <asp:RequiredFieldValidator ID="rqf_tipfac" runat="server" ControlToValidate="rc_tipfac" InitialValue="Seleccionar"
                                     ErrorMessage="(*)" ValidationGroup="gvInsert">
                                     <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                 </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Nro Nota</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nronota" runat="server" Enabled="false" Text='<%# Eval("NH_NRONOTA") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>Fec. Nota</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecnota" runat="server" Enabled="true" SelectedDate='<%# Bind("NH_FECNOTA") %>' Width="300px" AutoPostBack="true" OnSelectedDateChanged="txt_fecnota_SelectedDateChanged">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_fecnota" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <telerik:RadTextBox ID="txt_codcli" runat="server" Enabled="false" Text='<%# Bind("TRCODTER") %>' Width="300px" Visible="false">
                            </telerik:RadTextBox>
                            <td>
                                <label>Nro Documento</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nrodoc" runat="server" Enabled="false" Text='<%# Eval("TRCODNIT") %>' Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>N. Tercero</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_nomter" runat="server" Enabled="false" Text='<%# Eval("NOM_TER") %>' Width="300px">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rqv_tercero" runat="server" ControlToValidate="txt_nomter" ValidationGroup="gvInsert"
                                    ErrorMessage="(*)">
                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                                <asp:ImageButton ID="iBtnFindTercero" runat="server" CommandName="xxxx" Text="Buscar" SkinID="SkinBuscarPQ" OnClick="iBtnFindTercero_OnClick" />
                            </td>
                        </tr>
                        <tr>
                            <td><label>Sucursal</label></td>
                            <td colspan ="3">
                                <telerik:RadComboBox ID="rc_sucursal" runat="server" Culture="es-CO" Width="300px" ValidationGroup="gvInsert"
                                    Enabled="true" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Observaciones</label></td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("NH_DESCRIPCION") %>' Width="600px" TextMode="MultiLine" Height="60px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_estado" runat="server" SelectedValue='<%# Eval("NH_ESTADO") %>'
                                    Enabled="false" Width="300px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccione" Value="" />
                                        <telerik:RadComboBoxItem Text="Activo" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Cerrado" Value="CE" />
                                        <telerik:RadComboBoxItem Text="Liquidado" Value="LQ" />
                                        <telerik:RadComboBoxItem Text="Anulado" Value="AN" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                           <%-- <td>
                                <label>Tasa</label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txt_tasa" runat="server" Enabled="true" ValidationGroup="gvInsert" DbValue='<%# Bind("NH_TASA") %>' Width="300px">
                                </telerik:RadNumericTextBox>                               
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_tasa" InitialValue="0"
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_tasa" 
                                    ErrorMessage="(*)" ValidationGroup="gvInsert">
                                    <asp:Image ID="Image12" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" /></asp:RequiredFieldValidator>
                            </td>--%>
                        </tr>
                    </table>
                    <asp:Panel ID="pnDetalle" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_items_OnItemCommand" OnDeleteCommand="rg_items_OnDeleteCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" OnNeedDataSource="rg_items_OnNeedDataSource">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="ND_NROITEM">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="true" RefreshText="Cargar" />
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />                                    
                                    <telerik:GridTemplateColumn DataField="DTTIPFAC" HeaderText="T. Doc" HeaderStyle-Width="150px" Visible="false"
                                        Resizable="true" SortExpression="DTTIPFAC" UniqueName="DTTIPFAC">
                                        <ItemTemplate>
                                            <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="150px" Visible="false"
                                                Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("DTTIPFAC") %>'
                                                DataValueField="TFTIPFAC">
                                            </telerik:RadComboBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="DTNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROFAC"
                                        UniqueName="DTNROFAC">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="DTNROITM" HeaderButtonType="TextButton" HeaderStyle-Width="60px"
                                        HeaderText="Item" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTNROITM"
                                        UniqueName="DTNROITM">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn DataField="ND_DESCRIPCION" HeaderText="T. Doc" HeaderStyle-Width="360px" Visible="true"
                                        Resizable="true" SortExpression="ND_DESCRIPCION" UniqueName="ND_DESCRIPCION">
                                        <ItemTemplate>
                                            <telerik:RadTextBox ID="txt_observacion" runat="server" Enabled="true" Text='<%# Bind("ND_DESCRIPCION") %>' Width="300px" Visible="true">
                                            </telerik:RadTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <%--<telerik:GridBoundColumn DataField="ND_DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="360px"
                                        HeaderText="Descripcion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_DESCRIPCION"
                                        UniqueName="ND_DESCRIPCION">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn DataField="ND_SUBTOTAL" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr Subtotal" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_SUBTOTAL"
                                        UniqueName="ND_SUBTOTAL" Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ND_IMPUESTO" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr. Imp" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_IMPUESTO"
                                        UniqueName="ND_IMPUESTO" Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ND_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:C}"
                                        HeaderText="Vlr Total" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ND_VALOR"
                                        UniqueName="ND_VALOR" Aggregate="Sum">
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
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                    <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" CausesValidation="false" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
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
                <telerik:RadWindow RenderMode="Lightweight" ID="modalTerceros" runat="server" Width="890px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Terceros">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        Codigo</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="true" Width="80px">
                                    </telerik:RadTextBox>
                                </td>
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
                                    <%--<asp:Button ID="btn_filtroTer" runat="server" Text="Filtrar" OnClick="btn_filtroTer_OnClick" CommandName="Cancel" />--%>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_filtroTer" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtroTer_OnClick" CommandName="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panel13" runat="server">
                            <telerik:RadGrid ID="rgConsultaTerceros" runat="server" AllowSorting="True" Width="100%"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None"
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
                                        <div class="alert alert-danger">
                                            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                                            <strong>Alerta!</strong>No Tiene Registros
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalFacturas" runat="server" Width="910px" Height="585px" Modal="true" OffsetElementID="main" Style="z-index: 6999;" Title="Facturas">
                    <ContentTemplate>
                         <asp:Panel ID="Panel4" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <label>Nro Documento</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txt_nrodocfilter" runat="server" Enabled="true" Width="80px">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td>
                                        <telerik:RadButton RenderMode="Lightweight" ID="btn_filtro_factura" runat="server" Text="Filtrar" Icon-PrimaryIconCssClass="rbSearch" OnClick="btn_filtro_factura_Click" CommandName="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                            <telerik:RadGrid ID="rg_facturas" runat="server" AllowSorting="True" Width="98%" ShowFooter="True"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None" AllowFilteringByColumn="false"
                                DataSourceID="obj_factura" OnItemCommand="rg_facturas_OnItemCommand">
                                <FilterMenu CssClass="GridFilterRow_test"></FilterMenu>
                                <MasterTableView DataKeyNames="HDCODEMP,HDTIPFAC,HDNROFAC">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select">
                                            <HeaderStyle Width="40px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="HDFECFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="F. Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDFECFAC" AllowFiltering="false"
                                            UniqueName="HDFECFAC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TFNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="T Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TFNOMBRE"
                                            UniqueName="TFNOMBRE" FilterControlWidth="100px">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="HDNROFAC" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Nro Documento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDNROFAC"
                                            UniqueName="HDNROFAC" FilterControlWidth="100px">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="HDSUBTOT" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                                            Resizable="true" SortExpression="HDSUBTOT" UniqueName="HDSUBTOT" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="HDTOTIVA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="Impuesto" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                                            Resizable="true" SortExpression="HDTOTIVA" UniqueName="HDTOTIVA" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="HDTOTFAC" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            DataFormatString="{0:C}" HeaderText="T Factura" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                                            Resizable="true" SortExpression="HDTOTFAC" UniqueName="HDTOTFAC" Aggregate="Sum">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="HDESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Estado" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="HDESTADO"
                                            UniqueName="HDESTADO" AllowFiltering="false">
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
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalDetalle" runat="server" Width="910px" Height="590px" Modal="true" OffsetElementID="main" Style="z-index: 5999;" Title="Detalle Factura">
                    <ContentTemplate>
                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="80%">
                            <telerik:RadGrid ID="rg_detalle" runat="server" AllowSorting="True" Width="100%" ShowFooter="True" Height="100%" AllowFilteringByColumn="true"
                                AutoGenerateColumns="False"  CellSpacing="0" GridLines="None" OnItemCommand="rg_detalle_ItemCommand"
                                DataSourceID="obj_facturadt">
                                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="40px"
                                            Resizable="true" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_habilita" runat="server" Enabled="true" OnClick="chk_habilita_OnClick(this)" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTTIPFAC" HeaderText="T. Doc" HeaderStyle-Width="150px" Visible="false"
                                            Resizable="true" SortExpression="DTTIPFAC" UniqueName="DTTIPFAC">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="rc_tipfac" runat="server" Culture="es-CO" Width="150px" Visible="false"
                                                    Enabled="false" DataSourceID="obj_tipfac" DataTextField="TFNOMBRE" SelectedValue='<%# Bind("DTTIPFAC") %>'
                                                    DataValueField="TFTIPFAC">
                                                </telerik:RadComboBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTNROFAC" HeaderText="Nro Doc" HeaderStyle-Width="150px" Visible="false"
                                            Resizable="true" SortExpression="DTNROFAC" UniqueName="DTNROFAC">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_nrofac" runat="server" Enabled="false" Text='<%# Bind("DTNROFAC") %>' Width="300px" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTNROITM" HeaderText="Nro Doc" HeaderStyle-Width="150px" Visible="false"
                                            Resizable="true" SortExpression="DTNROITM" UniqueName="DTNROITM">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_nroitm" runat="server" Enabled="false" Text='<%# Bind("DTNROITM") %>' Width="300px" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="DTCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Referencia" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DTCLAVE1"
                                            UniqueName="DTCLAVE1" FilterControlWidth="70px">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn DataField="ARNOMBRE" HeaderText="Articulo" HeaderStyle-Width="390px" Visible="true"
                                            Resizable="true" SortExpression="ARNOMBRE" UniqueName="ARNOMBRE">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_articulo" runat="server" Text='<%# Bind("ARNOMBRE") %>' Width="390px"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="COD_TARIFA" HeaderText="Nro Doc" HeaderStyle-Width="150px" Visible="false"
                                            Resizable="true" SortExpression="COD_TARIFA" UniqueName="COD_TARIFA">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_codtarifa" runat="server" Enabled="false" Text='<%# Bind("COD_TARIFA") %>' Width="300px" Visible="false">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="TARIFA" HeaderText="Impuesto" HeaderStyle-Width="100px" Visible="false"
                                            Resizable="true" SortExpression="TARIFA" UniqueName="TARIFA">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_tarifa" runat="server" Enabled="false" DbValue='<%# Bind("TARIFA") %>'
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false" Visible="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTSUBTOT" HeaderText="Subtotal" HeaderStyle-Width="100px" Visible="false"
                                            Resizable="true" SortExpression="DTSUBTOT" UniqueName="DTSUBTOTV">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_valor" runat="server" Enabled="false" DbValue='<%# Bind("DTSUBTOT") %>'
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTTOTIVA" HeaderText="Impuesto" HeaderStyle-Width="100px" Visible="false"
                                            Resizable="true" SortExpression="DTTOTIVA" UniqueName="DTTOTIVAV">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_iva" runat="server" Enabled="false" DbValue='<%# Bind("DTTOTIVA") %>'
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="DTTOTFAC" HeaderText="Total" HeaderStyle-Width="100px"
                                            Resizable="true" SortExpression="DTTOTFAC" UniqueName="DTTOTFACV" AllowFiltering="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_total" runat="server" Enabled="false" DbValue='<%# Bind("DTTOTFAC") %>'
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false" NumberFormat-DecimalDigits="2">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" Resizable="true" HeaderText="Sub Nota" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_valornew" runat="server" Enabled="false"
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Imp Nota" HeaderStyle-Width="100px" Resizable="true" Visible="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_ivanew" runat="server" Enabled="false"
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="T Nota" HeaderStyle-Width="100px" Resizable="true" AllowFiltering="false">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_totalnew" runat="server" Enabled="true"
                                                    MinValue="0" Value="0" Width="100px" AutoPostBack="false" NumberFormat-DecimalDigits="2">
                                                </telerik:RadNumericTextBox>
                                                <%--<asp:CompareValidator runat="server" id="cmpNumbers" controltovalidate="txt_totalnew" controltocompare="txt_total" operator="LessThanEqual" type="Double"  ErrorMessage="(*)">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                </asp:CompareValidator>   --%>
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
                        <table width="100%">
                            <tr>
                                <td>
                                    <label>SubTotal</label></td>
                                <td>
                                    <asp:Label runat="server" ID="lbl_subtotal" Text="0"></asp:Label></td>
                                <td>
                                    <label>Impuestos</label></td>
                                <td>
                                    <asp:Label runat="server" ID="lbl_iva" Text="0"></asp:Label></td>
                                <td>
                                    <label>Total</label></td>
                                <td>
                                    <asp:Label runat="server" ID="lbl_total" Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregarItems" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Agregar" OnClick="btn_agregarItems_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalMonedas" runat="server" Width="910px" Height="585px" Modal="true" OffsetElementID="main" Style="z-index: 6999;" Title="Detalle Monedas">
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server">
                            <telerik:RadGrid ID="rg_tasacambio" runat="server" AllowSorting="True" Width="98%" ShowFooter="True" AutoGenerateColumns="False" AllowPaging="True" PageSize="7" CellSpacing="0" GridLines="None" AllowFilteringByColumn="true">                                
                                <MasterTableView>
                                    <Columns>                                        
                                         <telerik:GridTemplateColumn HeaderStyle-Width="100px" Resizable="true" HeaderText="" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_habilita" runat="server" Checked="true" Enabled="true" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TC_MONEDA" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="T Moneda" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TC_MONEDA"
                                            UniqueName="TC_MONEDA" FilterControlWidth="100px">                                            
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="TC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Valor" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TC_VALOR"
                                            UniqueName="TC_VALOR" FilterControlWidth="100px">                                            
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
                        <table>
                            <tr>
                                <td align="left">
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_aceptar_tasas" runat="server" Text="Agregar" Icon-PrimaryIconCssClass="rbSave" ToolTip="Agregar" OnClick="btn_aceptar_tasas_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPrinter" runat="server" Width="500px" Height="170px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Asistente Impresion">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Tipo Moneda</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda_print" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                        Enabled="true" DataSourceID="obj_moneda" DataTextField="TTDESCRI"
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_ok_printer" runat="server" Text="" Icon-PrimaryIconCssClass="rbOk" ToolTip="Ok" CommandName="Cancel" OnClick="btn_ok_printer_Click" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_notas" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertNotaDeb" OnInserted="obj_notas_OnInserted" OnInserting="obj_notas_OnInserting"
        SelectMethod="GetNotaDebHD" TypeName="XUSS.BLL.Facturacion.NotasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="NH_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="NH_TIPFAC" Type="String" />
            <asp:Parameter Name="NH_FECNOTA" Type="DateTime" />
            <asp:Parameter Name="TRCODTER" Type="Int32" />
            <asp:Parameter Name="SC_CONSECUTIVO" Type="Int32" />
            <asp:Parameter Name="NH_DESCRIPCION" Type="String" />
            <asp:Parameter Name="NH_TASA" Type="Double" />
            <asp:Parameter Name="NH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="NH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="INMONEDA" Type="String"/>
            <asp:Parameter Name="intb" Type="Object" />
            <asp:Parameter Name="inMonedas" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_terceros" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTerceros" TypeName="XUSS.BLL.Terceros.TercerosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_factura" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFacturaHD" TypeName="XUSS.BLL.Facturacion.FacturacionBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_facturadt" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetFacturaDTIMP" TypeName="XUSS.BLL.Facturacion.FacturacionBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="DTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="DTTIPFAC" Type="String" DefaultValue="" />
            <asp:Parameter Name="DTNROFAC" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipfac" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (1,2,3,4,5)" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_tipnota" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTiposFactura" TypeName="XUSS.BLL.Parametros.TipoFacturaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="TFCLAFAC IN (9)" Name="filter" Type="String" />
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
</asp:Content>
