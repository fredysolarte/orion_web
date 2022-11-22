<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="SegregacionFacturas.aspx.cs" Inherits="XUSS.WEB.Compras.SegregacionFacturas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadScriptBlock ID="rsSegregacion" runat="server">
        <script type="text/javascript">
            function changevalue(sender, Args) {
                //debugger;
                var listview = $find('<%= rlv_segregacion.ClientID %>');
                var master_view = $telerik.findControl(listview.get_element().parentNode, "rg_items").get_masterTableView()
                var rows = master_view.get_dataItems();
                var mycolums = master_view.get_columns();

                for (var i = 0; i < rows.length; i++)
                {
                    var row = rows[i];  
                    var total = 0;
                    try {
                        //if (row.findControl((sender.get_id()).substring((sender.get_id()).indexOf("txt_"), (sender.get_id()).length)).get_id() != null && row.findControl((sender.get_id()).substring((sender.get_id()).indexOf("txt_"), (sender.get_id()).length)).get_id() == sender.get_id()) {
                        if (row.findControl(sender.get_id()).get_id() == sender.get_id()) {
                            //row.findControl("txt_candif").set_value(row.findControl("txt_canfac").get_value() - sender.get_value());
                            row.findControl("txt_candif").set_value(0);
                            
                            for (var c = 0; c < mycolums.length; c++) {
                                if (mycolums[c].get_uniqueName().indexOf("BOD_") == 0) {
                                    total = total + parseInt(row.findElement(mycolums[c].get_uniqueName()).value);
                                    row.findControl("txt_candif").set_value(row.findControl("txt_canfac").get_value()-total);


                                    function onSucess(result) {
                                        //alert(result);
                                    }
                                    function onError() {
                                            alert('error');
                                    }
                                    //PageMethods.EditCantidad(parseInt(row.findControl("txt_codpro").get_value()), mycolums[c].get_uniqueName(), parseInt(sender.get_value()), onSucess,onError);
                                    

                                    if ((sender.get_id()).substring((sender.get_id()).indexOf("BOD_"), (sender.get_id()).length) == mycolums[c].get_uniqueName()) {
                                        PageMethods.EditCantidad(parseInt(row.findControl("txt_codpro").get_value()), mycolums[c].get_uniqueName(), parseInt(sender.get_value()), onSucess,onError);
                                        for (var z = c; z < mycolums.length; z++) {
                                            if ((sender.get_id()).substring((sender.get_id()).indexOf("BOD_"), (sender.get_id()).length) != mycolums[z].get_uniqueName()) {
                                                row.findElement(mycolums[z].get_uniqueName()).value = parseInt(row.findControl("txt_candif").get_value() / ((mycolums.length - z)));

                                                PageMethods.EditCantidad(parseInt(row.findControl("txt_codpro").get_value()), mycolums[z].get_uniqueName(), parseInt(row.findControl("txt_candif").get_value() / ((mycolums.length - z))), onSucess, onError);

                                                row.findControl("txt_candif").set_value(row.findControl("txt_candif").get_value()-row.findElement(mycolums[z].get_uniqueName()).value);
                                                if (z + 1 >= mycolums.length)
                                                    if ( row.findControl("txt_candif").get_value() % ((mycolums.length - (c + 1))) == 1  )
                                                    row.findElement(mycolums[z].get_uniqueName()).value = parseInt(row.findElement(mycolums[z].get_uniqueName()).value) + 1;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                    catch (e) {
                        //alert(e);
                    }
                }
            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnablePageMethods="true">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" >
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_segregacion" runat="server" PageSize="1" AllowPaging="True" OnItemInserted="rlv_segregacion_ItemInserted"
            OnItemCommand="rlv_segregacions_OnItemCommand" OnItemDataBound="rlv_segregacion_OnItemDataBound" OnItemInserting="rlv_segregacion_ItemInserting"
            DataSourceID="obj_segregacion" ItemPlaceholderID="pnlGeneral"
            DataSourceCount="0" ResolvedRenderMode="Classic">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Segregacion</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_segregacion" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Segregacion</h5>
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
                                    <label>
                                        Nro Order</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroOrden" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Order Int</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroOrdenInt" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        Nro Factura</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_factura" runat="server"
                                        Width="300px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" OnClick="btn_filtro_OnClick" Text="Search" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Search" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="New" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />                    
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                            <tr>
                                <td>
                                    <label>Nro Segregacion</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_nroorden" runat="server" Width="300px" Text='<%# Bind("SG_CODIGO") %>'>
                                    </telerik:RadTextBox>                                    
                                </td>                                
                            </tr>
                        </table>
                    <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>

                                        <%--<telerik:GridBoundColumn DataField="SG_ITEM" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SG_ITEM"
                                            UniqueName="SG_ITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="SG_ITEM" HeaderText="" UniqueName="SG_ITEM"
                                            HeaderStyle-Width="40px" AllowFiltering="false" SortExpression="SG_ITEM" Visible="true">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_codpro" runat="server" Enabled="false" Text='<%# Bind("SG_ITEM") %>' Visible="true" Width="32px" BorderStyle="None">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%--<telerik:GridTemplateColumn  HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                                    Resizable="true" UniqueName="PR_POSARA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_arancel" runat="server" Enabled="true" Width="100px" ClientEvents-OnValueChanged="changevalue">
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>                                        
                                        <telerik:GridBoundColumn DataField="FD_NROFACTURA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROFACTURA"
                                            UniqueName="FD_NROFACTURA">                                            
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Reference" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                            UniqueName="ARCLAVE1">                                            
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                            UniqueName="NOMTTEC2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                            UniqueName="NOMTTEC3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                            UniqueName="NOMTTEC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                            UniqueName="NOMTTEC5">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                            UniqueName="NOMTTEC7">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="SG_CANTIDAD" HeaderText="Qty" HeaderStyle-Width="70px" Visible="True"
                                            Resizable="true" SortExpression="SG_CANTIDAD" UniqueName="SG_CANTIDAD" Aggregate="Sum" FooterText=" ">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_canfac" runat="server" Enabled="false" DbValue='<%# Bind("SG_CANTIDAD") %>' Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>                                        

                                        <telerik:GridTemplateColumn HeaderText="Dif" HeaderStyle-Width="70px" Visible="True"
                                            Resizable="true"  UniqueName="DIF" >
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_candif" runat="server" Enabled="false" Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>                                        
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <telerik:RadSplitter RenderMode="Lightweight" ID="Radsplitter3" runat="server" Height="750px" Width="100%" Orientation="Horizontal">
                    <telerik:RadPane ID="Radpane4" runat="server" Height="100px">
                        <table>
                            <tr>
                                <td>
                                    <label>Nro Facturas</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txt_facturas" runat="server" Width="300px">
                                    </telerik:RadTextBox>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_loadfac" runat="server" Text="Cargar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="xxxxx" ToolTip="Buscar" OnClick="btn_loadfac_Click" />
                                </td>
                                <td>
                                    <label>Bodegas</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="rc_bodega" runat="server" DataSourceID="obj_bodega" Width="300px" CheckBoxes="true"
                                        DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true" >
                                    </telerik:RadComboBox>
                                    <telerik:RadButton RenderMode="Lightweight" ID="btn_loadbod" runat="server" Text="Cargar" Icon-PrimaryIconCssClass="rbRefresh" CommandName="xxxxx" ToolTip="Buscar" OnClick="btn_loadbod_Click" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="Radsplitbar4" runat="server" CollapseMode="Forward">
                    </telerik:RadSplitBar>
                    <telerik:RadPane ID="Radpane5" runat="server">
                        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False"
                                Culture="(Default)" CellSpacing="0" ShowFooter="True">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>

                                        <%--<telerik:GridBoundColumn DataField="SG_ITEM" HeaderButtonType="TextButton" HeaderStyle-Width="40px"
                                            HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="SG_ITEM"
                                            UniqueName="SG_ITEM">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="SG_ITEM" HeaderText="" UniqueName="SG_ITEM"
                                            HeaderStyle-Width="40px" AllowFiltering="false" SortExpression="SG_ITEM" Visible="true">
                                            <ItemTemplate>
                                                <telerik:RadTextBox ID="txt_codpro" runat="server" Enabled="false" Text='<%# Bind("SG_ITEM") %>' Visible="true" Width="32px" BorderStyle="None">
                                                </telerik:RadTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <%--<telerik:GridTemplateColumn  HeaderText="UN Arancel" HeaderStyle-Width="120px" Visible="True"
                                                    Resizable="true" UniqueName="PR_POSARA">
                                                    <ItemTemplate>
                                                        <telerik:RadTextBox ID="txt_arancel" runat="server" Enabled="true" Width="100px" ClientEvents-OnValueChanged="changevalue">
                                                        </telerik:RadTextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>                                        
                                        <telerik:GridBoundColumn DataField="FD_NROFACTURA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Factura" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FD_NROFACTURA"
                                            UniqueName="FD_NROFACTURA">                                            
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Brand" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TANOMBRE"
                                            UniqueName="TANOMBRE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ARCLAVE1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Reference" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARCLAVE1"
                                            UniqueName="ARCLAVE1">                                            
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Description" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE"
                                            UniqueName="ARNOMBRE">                                            
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Size" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC1"
                                            UniqueName="NOMTTEC1">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="NOMTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Presetation" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC2"
                                            UniqueName="NOMTTEC2">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Type" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC3"
                                            UniqueName="NOMTTEC3">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="T Product" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC4"
                                            UniqueName="NOMTTEC">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                            HeaderText="Gender" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC5"
                                            UniqueName="NOMTTEC5">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOMTTEC7" HeaderButtonType="TextButton" HeaderStyle-Width="250px"
                                            HeaderText="Family/Fragance" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOMTTEC7"
                                            UniqueName="NOMTTEC7">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="SG_CANTIDAD" HeaderText="Qty" HeaderStyle-Width="70px" Visible="True"
                                            Resizable="true" SortExpression="SG_CANTIDAD" UniqueName="SG_CANTIDAD" Aggregate="Sum" FooterText=" ">
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_canfac" runat="server" Enabled="false" DbValue='<%# Bind("SG_CANTIDAD") %>' Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>                                        

                                        <telerik:GridTemplateColumn HeaderText="Dif" HeaderStyle-Width="70px" Visible="True"
                                            Resizable="true"  UniqueName="DIF" >
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txt_candif" runat="server" Enabled="false" Value="0" Width="60px" Visible="true" NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>                                        
                                    </Columns>
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger">
                                            <strong>¡No Records to Display!</strong>
                                        </div>
                                    </NoRecordsTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                    </telerik:RadPane>
                </telerik:RadSplitter>
                <table>
                        <tr>
                            <td>                                
                                <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                                <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
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
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_segregacion" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertSegregacion" OnInserting="obj_segregacion_Inserting" OnInserted="obj_segregacion_Inserted"
        SelectMethod="GetSegregacionHD" TypeName="XUSS.BLL.Compras.SegregacionBL" >
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="FD_CODEMP" Type="String" SessionField="CODEMP" />            
            <asp:SessionParameter Name="SG_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="inDt" Type="Object" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuario" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
