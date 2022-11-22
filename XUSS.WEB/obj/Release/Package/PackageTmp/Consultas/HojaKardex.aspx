<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="HojaKardex.aspx.cs" Inherits="XUSS.WEB.Consultas.HojaKardex" %>

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
                //debugger;
                //console.log(args.get_eventTarget());
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$btn_export")) {
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
                    <h5>Hoja Kardex</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <div>
                <table>
                    <tr>    
                        <td>
                            <label>F Inicial </label>
                        </td>
                        <td>
                            <label>Año</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_ano" runat="server" Width="130px" AppendDataBoundItems="true" DataSourceID="obj_anos" DataTextField="ANO"
                                DataValueField="ANO">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_ano" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Mes</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_mes" runat="server" Width="130px" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_mes_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                    <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                    <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                    <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                    <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                    <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                    <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                    <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                    <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                    <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                    <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                    <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_mes" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Dia</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_dia" runat="server" Width="130px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    <telerik:RadComboBoxItem Text="1" Value="1" />
                                    <telerik:RadComboBoxItem Text="2" Value="2" />
                                    <telerik:RadComboBoxItem Text="3" Value="3" />
                                    <telerik:RadComboBoxItem Text="4" Value="4" />
                                    <telerik:RadComboBoxItem Text="5" Value="5" />
                                    <telerik:RadComboBoxItem Text="6" Value="6" />
                                    <telerik:RadComboBoxItem Text="7" Value="7" />
                                    <telerik:RadComboBoxItem Text="8" Value="8" />
                                    <telerik:RadComboBoxItem Text="9" Value="9" />
                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                    <telerik:RadComboBoxItem Text="11" Value="11" />
                                    <telerik:RadComboBoxItem Text="12" Value="12" />
                                    <telerik:RadComboBoxItem Text="13" Value="13" />
                                    <telerik:RadComboBoxItem Text="14" Value="14" />
                                    <telerik:RadComboBoxItem Text="15" Value="15" />
                                    <telerik:RadComboBoxItem Text="16" Value="16" />
                                    <telerik:RadComboBoxItem Text="17" Value="17" />
                                    <telerik:RadComboBoxItem Text="18" Value="18" />
                                    <telerik:RadComboBoxItem Text="19" Value="19" />
                                    <telerik:RadComboBoxItem Text="20" Value="20" />
                                    <telerik:RadComboBoxItem Text="21" Value="21" />
                                    <telerik:RadComboBoxItem Text="22" Value="22" />
                                    <telerik:RadComboBoxItem Text="23" Value="23" />
                                    <telerik:RadComboBoxItem Text="24" Value="24" />
                                    <telerik:RadComboBoxItem Text="25" Value="25" />
                                    <telerik:RadComboBoxItem Text="26" Value="26" />
                                    <telerik:RadComboBoxItem Text="27" Value="27" />
                                    <telerik:RadComboBoxItem Text="28" Value="28" />
                                    <telerik:RadComboBoxItem Text="29" Value="29" />
                                    <telerik:RadComboBoxItem Text="30" Value="30" />
                                    <telerik:RadComboBoxItem Text="31" Value="31" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_dia" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>                        
                        <td>
                            <label>Bodega</label></td>
                        <td>
                            <telerik:RadComboBox ID="rc_bodega" runat="server" Culture="es-CO" Width="300px"
                                Enabled="true" DataSourceID="obj_bodega" DataTextField="BDNOMBRE"
                                DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rc_bodega" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>
                                Linea</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_linea" runat="server" DataSourceID="Obj_Linea" Width="300px" CheckBoxes="true"
                                DataTextField="TANOMBRE" DataValueField="TATIPPRO" AppendDataBoundItems="true">
                            </telerik:RadComboBox>
                        </td>
                        <%--<td>
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton" CausesValidation="true">
                            </telerik:RadButton>
                        </td>--%>
                    </tr>
                    <tr>    
                        <td>
                            <label>F Final   </label>
                        </td>
                        <td>
                            <label>Año</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_anof" runat="server" Width="130px" AppendDataBoundItems="true" DataSourceID="obj_anos" DataTextField="ANO"
                                DataValueField="ANO">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rc_ano" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Mes</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_mesf" runat="server" Width="130px" AppendDataBoundItems="true" OnSelectedIndexChanged="rc_mes_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    <telerik:RadComboBoxItem Text="Enero" Value="1" />
                                    <telerik:RadComboBoxItem Text="Febrero" Value="2" />
                                    <telerik:RadComboBoxItem Text="Marzo" Value="3" />
                                    <telerik:RadComboBoxItem Text="Abril" Value="4" />
                                    <telerik:RadComboBoxItem Text="Mayo" Value="5" />
                                    <telerik:RadComboBoxItem Text="Junio" Value="6" />
                                    <telerik:RadComboBoxItem Text="Julio" Value="7" />
                                    <telerik:RadComboBoxItem Text="Agosto" Value="8" />
                                    <telerik:RadComboBoxItem Text="Septiembre" Value="9" />
                                    <telerik:RadComboBoxItem Text="Octubre" Value="10" />
                                    <telerik:RadComboBoxItem Text="Noviembre" Value="11" />
                                    <telerik:RadComboBoxItem Text="Diciembre" Value="12" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rc_mes" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image6" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <label>Dia</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rc_diaf" runat="server" Width="130px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    <telerik:RadComboBoxItem Text="1" Value="1" />
                                    <telerik:RadComboBoxItem Text="2" Value="2" />
                                    <telerik:RadComboBoxItem Text="3" Value="3" />
                                    <telerik:RadComboBoxItem Text="4" Value="4" />
                                    <telerik:RadComboBoxItem Text="5" Value="5" />
                                    <telerik:RadComboBoxItem Text="6" Value="6" />
                                    <telerik:RadComboBoxItem Text="7" Value="7" />
                                    <telerik:RadComboBoxItem Text="8" Value="8" />
                                    <telerik:RadComboBoxItem Text="9" Value="9" />
                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                    <telerik:RadComboBoxItem Text="11" Value="11" />
                                    <telerik:RadComboBoxItem Text="12" Value="12" />
                                    <telerik:RadComboBoxItem Text="13" Value="13" />
                                    <telerik:RadComboBoxItem Text="14" Value="14" />
                                    <telerik:RadComboBoxItem Text="15" Value="15" />
                                    <telerik:RadComboBoxItem Text="16" Value="16" />
                                    <telerik:RadComboBoxItem Text="17" Value="17" />
                                    <telerik:RadComboBoxItem Text="18" Value="18" />
                                    <telerik:RadComboBoxItem Text="19" Value="19" />
                                    <telerik:RadComboBoxItem Text="20" Value="20" />
                                    <telerik:RadComboBoxItem Text="21" Value="21" />
                                    <telerik:RadComboBoxItem Text="22" Value="22" />
                                    <telerik:RadComboBoxItem Text="23" Value="23" />
                                    <telerik:RadComboBoxItem Text="24" Value="24" />
                                    <telerik:RadComboBoxItem Text="25" Value="25" />
                                    <telerik:RadComboBoxItem Text="26" Value="26" />
                                    <telerik:RadComboBoxItem Text="27" Value="27" />
                                    <telerik:RadComboBoxItem Text="28" Value="28" />
                                    <telerik:RadComboBoxItem Text="29" Value="29" />
                                    <telerik:RadComboBoxItem Text="30" Value="30" />
                                    <telerik:RadComboBoxItem Text="31" Value="31" />
                                </Items>
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rc_dia" InitialValue="Seleccionar"
                                ErrorMessage="(*)" ValidationGroup="gvInsert">
                                <asp:Image ID="Image7" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                            </asp:RequiredFieldValidator>
                        </td>                                                                    
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_click" ValidationGroup="UpdateBoton" CausesValidation="true" Icon-PrimaryIconCssClass="rbSearch" RenderMode="Lightweight">
                            </telerik:RadButton>
                            <telerik:RadButton ID="btn_imprimir" runat="server" Text="Imprimir" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" RenderMode="Lightweight">
                            </telerik:RadButton>
                            <telerik:RadButton ID="btn_export" runat="server" Text="Descargar xls" OnClick="ButtonExcel_Click" Icon-PrimaryIconCssClass="rbDownload" RenderMode="Lightweight">
                            </telerik:RadButton>
                        </td>                        
                    </tr>
                </table>
            </div>
            <div>
                <%--<asp:ImageButton ID="Button2" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Excel_XLSX.png" OnClick="ButtonExcel_Click" AlternateText="Xlsx" />--%>

                <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="4000" Height="1000px" OnPivotGridCellExporting="rg_hoja_PivotGridCellExporting" OnItemCommand="rg_hoja_ItemCommand"
                    ID="rg_hoja" runat="server" OnPivotGridBiffExporting="rg_hoja_PivotGridBiffExporting" OnNeedDataSource="rg_hoja_NeedDataSource" ShowRowHeaderZone="false"
                    ShowColumnHeaderZone="false" AllowSorting="true" ShowFilterHeaderZone="false" TotalsSettings-RowsSubTotalsPosition="None" AllowFiltering="true" EnableConfigurationPanel="true">
                    <ClientSettings EnableFieldsDragDrop="true">
                        <Scrolling AllowVerticalScroll="true" SaveScrollPosition="true"></Scrolling>
                        <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                    </ClientSettings>
                    <Fields>
                        <telerik:PivotGridRowField DataField="TANOMBRE" ZoneIndex="0" Caption="Linea" CellStyle-Width="100px" UniqueName="TANOMBRE">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField DataField="ARNOMBRE" ZoneIndex="0" Caption="Articulo" CellStyle-Width="350px" UniqueName="ARNOMBRE">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField DataField="CLAVE2" Caption="C2" CellStyle-Width="50px" UniqueName="CLAVE2">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridRowField DataField="CLAVE3" Caption="C3" CellStyle-Width="50px" UniqueName="CLAVE3">
                        </telerik:PivotGridRowField>
                        <telerik:PivotGridColumnField DataField="TMNOMBRE" Caption="Transaccion">
                        </telerik:PivotGridColumnField>                        
                        <telerik:PivotGridAggregateField DataField="FBCANTID" Aggregate="Sum" Caption="Cant" >
                            <CellTemplate>
                                <asp:LinkButton ID="lbl_cant" runat="server" CommandName="link">
                                    <%# Container.DataItem %>
                                </asp:LinkButton>
                            </CellTemplate>
                            <HeaderCellTemplate>
                                <asp:Label ID="AggregateCell1" Text="Cant" runat="server" />
                            </HeaderCellTemplate>
                            <ColumnGrandTotalHeaderCellTemplate>
                                <asp:Label ID="Label1" Text="Total Cant" runat="server" />
                            </ColumnGrandTotalHeaderCellTemplate>
                        </telerik:PivotGridAggregateField>
                    </Fields>
                    <%--<ConfigurationPanelSettings Position="Bottom" DefaultDeferedLayoutUpdate="true" />--%>
                    <NoRecordsTemplate>
                        <div class="alert alert-danger">
                            <strong>¡No se Encontaron Registros!</strong>
                        </div>
                    </NoRecordsTemplate>
                </telerik:RadPivotGrid>
            </div>
        </fieldset>
    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpSeguimeinto" runat="server" Width="815px" Height="550px" Modal="true" OffsetElementID="main" Title="Seguimiento Inventario" EnableShadow="true">
                    <ContentTemplate>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
    </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_anos" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAnosMov" TypeName="XUSS.BLL.Consultas.ConsultasBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="Obj_Linea" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLinea" TypeName="XUSS.BLL.Consultas.CstInventariosBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
