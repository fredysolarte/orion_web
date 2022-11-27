<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="InventarioDisponible.aspx.cs" Inherits="XUSS.WEB.Consultas.InventarioDisponible" %>

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
            function conditionalPostback(sender, args) {
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rgDetalle$ctl00$ctl02$ctl00$ExportToExcelButton")) {
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Disposicion Inventarios</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_buscar">
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>C. Barras</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="txt_barras" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Inv. Vacio
                                </label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_inv" runat="server" />
                            </td>
                            <td rowspan="3">
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" Icon-PrimaryIconCssClass="rbSearch"
                                    ValidationGroup="UpdateBoton" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Bodega</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_bodega" runat="server" DataSourceID="obj_bodega" Width="300px" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                    DataTextField="BDNOMBRE" DataValueField="BDBODEGA" AppendDataBoundItems="true" OnDataBound="rc_bodega_DataBound">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <label>
                                    Linea</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="Obj_Linea" Width="300px" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                    DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    Referencia</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_referencia" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                            <td>
                                <label>
                                    Nombre</label>
                            </td>
                            <td>
                                <telerik:RadTextBox ID="edt_nombre" runat="server" Width="300px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <telerik:RadGrid ID="rgDetalle" runat="server" Width="100%" AutoGenerateColumns="False" ShowGroupPanel="True" OnInfrastructureExporting="rgDetalle_InfrastructureExporting"
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="True" OnItemCommand="rgDetalle_OnItemCommand"
                DataSourceID="obj_consulta" ShowFooter="True" Height="650px" OnDetailTableDataBind="rgDetalle_OnDetailTableDataBind">
                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <ExportSettings HideStructureColumns="true"
                    IgnorePaging="true"
                    OpenInNewWindow="true" />
                <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="BBCODEMP,BBBODEGA,BBTIPPRO,BBCLAVE1,BBCLAVE2,BBCLAVE3,BBCLAVE4">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                        ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <DetailTables>
                        <telerik:GridTableView Name="detalle_item" Width="100%" DataKeyNames="LLAVE">
                            <DetailTables>
                                <telerik:GridTableView Name="detalle_item_elemento" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn SortExpression="BECDELEM" HeaderText="Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                            DataField="BECDELEM">
                                        </telerik:GridBoundColumn>                                        
                                        <telerik:GridBoundColumn SortExpression="BECANTID" HeaderText="Can Elemento" HeaderButtonType="TextButton" AllowFiltering="false"
                                            DataField="BECANTID">
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
                                <telerik:GridBoundColumn SortExpression="BLCDLOTE" HeaderText="Lote" HeaderButtonType="TextButton" AllowFiltering="false"
                                    DataField="BLCDLOTE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="BLDTTEC1" HeaderText="D. Tec1" HeaderButtonType="TextButton" AllowFiltering="false"
                                    DataField="BLDTTEC1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="BLDTTEC2" HeaderText="D. Tec2" HeaderButtonType="TextButton" AllowFiltering="false"
                                    DataField="BLDTTEC2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="BLCANTID" HeaderText="Can Lote" HeaderButtonType="TextButton" AllowFiltering="false"
                                    DataField="BLCANTID">
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
                        <%--<telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="../App_Themes/Tema2/Images/Edit.gif">--%>
                        <telerik:GridButtonColumn Text="Seleccionar" CommandName="Select" ImageUrl="../App_Themes/Tema2/Images/find_.png" ButtonType="ImageButton">
                            <HeaderStyle Width="40px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn Resizable="true" SortExpression="BARRAS" HeaderText="C. Barras" DataType="System.String"
                            AllowFiltering="false" UniqueName="BARRAS" HeaderButtonType="TextButton" DataField="BARRAS"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARTIPPRO" HeaderButtonType="TextButton" HeaderStyle-Width="10px"
                            HeaderText="" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                            Resizable="true" SortExpression="ARTIPPRO">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TANOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            FilterControlWidth="100px" HeaderText="Linea" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TANOMBRE">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="ARCLAVE1" HeaderText="Referencia" UniqueName="ARCLAVE1_TK"
                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="ARCLAVE1" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_referencia" runat="server" Text='<%# Eval("ARCLAVE1") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="link" UniqueName="ARCLAVE1" DataTextField="ARCLAVE1"
                            HeaderText="Referencia" HeaderStyle-Width="100px">
                        </telerik:GridButtonColumn>
                        <%--CommandName="link"--%>
                        <telerik:GridBoundColumn DataField="ORIGEN" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            HeaderText="Origen" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                            Resizable="true" SortExpression="ORIGEN">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARCLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                            HeaderText="" ItemStyle-HorizontalAlign="Right" AllowFiltering="false"
                            Resizable="true" SortExpression="ARCLAVE2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARCLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                            AllowFiltering="false" HeaderText="" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARCLAVE3">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARCLAVE4" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                            AllowFiltering="false" HeaderText="" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARCLAVE4">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CLAVE2" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            FilterControlWidth="100px" HeaderText="Clave 2" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="CLAVE2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CLAVE3" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                            FilterControlWidth="100px" HeaderText="Clave 3" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="CLAVE3">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="580px"
                            HeaderText="Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ARNOMBRE">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true"
                            AllowFiltering="false" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBCANTID" Aggregate="Sum" FooterText="Total:">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBCANTRN" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true"
                            AllowFiltering="false" HeaderText="Can Tran" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBCANTRN">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBBODBOD" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ItemStyle-ForeColor="Purple" ItemStyle-Font-Bold="true"
                            AllowFiltering="false" HeaderText="Can Com" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBBODBOD">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBCANCTL" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ItemStyle-ForeColor="Blue" ItemStyle-Font-Bold="true"
                            AllowFiltering="false" HeaderText="Can Ctl" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBCANCTL">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBBODPED" HeaderButtonType="TextButton" HeaderStyle-Width="80px" ItemStyle-ForeColor="Salmon" ItemStyle-Font-Bold="true"
                            AllowFiltering="false" HeaderText="Can Ped" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBBODPED">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BBBODEGA" HeaderButtonType="TextButton" HeaderStyle-Width="20px"
                            AllowFiltering="false" HeaderText="" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BBBODEGA">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="Bodega" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="BDNOMBRE">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PRECIO" HeaderButtonType="TextButton" HeaderStyle-Width="120px"
                            AllowFiltering="false" DataFormatString="{0:0.0}" HeaderText="Precio" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="PRECIO">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DESCUENTO" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                            AllowFiltering="false" DataFormatString="{0:0.#}" HeaderText="Dcto" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="DESCUENTO">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC1" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 1" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC2" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 2" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC3" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 3" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC3">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC4" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 4" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC4">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC5" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 5" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC5">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ARDTTEC8" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 8" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="ARDTTEC8">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn DataField="REF_TESTER" HeaderText="Ref Tester" UniqueName="REF_TESTER_TK"
                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="REF_TESTER" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_reftester" runat="server" Text='<%# Eval("REF_TESTER") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="link_test" UniqueName="REF_TESTER" DataTextField="REF_TESTER"
                            HeaderText="Ref Tester" HeaderStyle-Width="100px">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="NOM_TESTER" HeaderButtonType="TextButton" HeaderStyle-Width="580px"
                            HeaderText="Nom Tester" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="NOM_TESTER">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="TAM_TESTER" HeaderButtonType="TextButton" HeaderStyle-Width="280px"
                            AllowFiltering="false" HeaderText="DT 1" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="TAM_TESTER">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CAN_INV_TESTER" HeaderButtonType="TextButton" HeaderStyle-Width="80px"
                            AllowFiltering="false" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                            Resizable="true" SortExpression="CAN_INV_TESTER" Aggregate="Sum" FooterText="Total:">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <NoRecordsTemplate>
                        <div class="alert alert-danger">
                            <strong>¡No se Encontaron Registros!</strong>
                        </div>
                    </NoRecordsTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </fieldset>

        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="895px" Height="550px" Modal="true" OffsetElementID="main" Title="Seguimiento Inventario" EnableShadow="true">
                    <ContentTemplate>
                        <telerik:RadGrid ID="rg_detalle" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" OnItemCommand="rg_detalle_OnItemCommand"
                            Culture="(Default)" CellSpacing="0" ShowFooter="True" ShowGroupPanel="True" RenderMode="Lightweight">
                            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView ShowGroupFooter="true" CommandItemDisplay="Top" DataKeyNames="DOCUEMNTO_CON">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true" ShowExportToExcelButton="true"
                                    ShowExportToPdfButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="MBCDTRAN" HeaderButtonType="TextButton" HeaderStyle-Width="35px"
                                        HeaderText="" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="MBCDTRAN"
                                        UniqueName="MBCDTRAN">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIPCION" HeaderButtonType="TextButton" HeaderStyle-Width="230px"
                                        HeaderText="T. Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="DESCRIPCION"
                                        UniqueName="DESCRIPCION">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FBCANTID" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                                        HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FBCANTID"
                                        UniqueName="FBCANTID" FooterText="Total: " Aggregate="Sum">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FBFECING" HeaderButtonType="TextButton" HeaderStyle-Width="170px"
                                        HeaderText="Fec Movimiento" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="FBFECING"
                                        UniqueName="FBFECING">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn DataField="ESTADO" HeaderButtonType="TextButton" HeaderStyle-Width="50px"
                                        HeaderText="Est" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="ESTADO"
                                        UniqueName="ESTADO">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="DOCUEMNTO_CON" HeaderText="Propietario" UniqueName="TK_PROPIETARIO"
                                        HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="DOCUEMNTO_CON" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_doc" runat="server" Text='<%# Eval("DOCUEMNTO_CON") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn CommandName="link" UniqueName="DOCUEMNTO_CON" DataTextField="DOCUEMNTO_CON"
                                        HeaderText="Documento Mov" HeaderStyle-Width="150px">
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
    <asp:ObjectDataSource ID="obj_consulta" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetConsulatInventario" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue=" AND 1=0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--<asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegasXUsuario" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="usua_usuario" Type="String" SessionField="UserLogon" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="Obj_Linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLinea" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
