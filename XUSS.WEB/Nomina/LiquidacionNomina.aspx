<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="LiquidacionNomina.aspx.cs" Inherits="XUSS.WEB.Nomina.LiquidacionNomina" %>

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
                if ((args.get_eventTarget() == "ctl00$ContentPlaceHolder1$rlv_pedidos$ctrl0$btn_descargar")) {
                    args.set_enableAjax(false);
                }
            }
            function endPostback(sender, args) {
                args.set_enableAjax(true);
            }

            function OnClientEntryAddingHandler(sender, eventArgs) {

                if (sender.get_entries().get_count() > 1) {
                    eventArgs.set_cancel(true);
                }

            }
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="conditionalPostback" ClientEvents-OnResponseEnd="endPostback">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_liquidacionnom" runat="server" PageSize="1" AllowPaging="True" OnItemCommand="rlv_liquidacionnom_ItemCommand"
            OnItemDataBound="rlv_liquidacionnom_ItemDataBound" DataSourceID="obj_planillanmHD" ItemPlaceholderID="pnlGeneral" DataKeyNames="NMH_CODIGO"
            DataSourceCount="0">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Planilla Liquidacion Nomina</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_liquidacionnom" PageSize="1" RenderMode="Lightweight">
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
                                <h5>Planilla Liquidacion Nomina</h5>
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
                                    <label>Periodo</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_periodo" runat="server" Culture="es-CO" Width="300px"
                                        DataSourceID="obj_periodo" DataTextField="Periodo"
                                        DataValueField="NMP_CODIGO" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Estado</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_estado" runat="server" Culture="es-CO" Width="300px" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                            <telerik:RadComboBoxItem Text="Pre-Liquidado" Value="AC" />
                                            <telerik:RadComboBoxItem Text="Definitivo" Value="CE" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_filtro" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" ToolTip="Aplicar Filtro" RenderMode="Lightweight" OnClick="btn_filtro_Click">
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_imprimir" runat="server" Text="Imprimir Planilla" OnClick="btn_imprimir_Click" Icon-PrimaryIconCssClass="rbPrint" CommandName="Cancel" ToolTip="Imprimir" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <asp:Panel ID="Panel1" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <label>Periodo</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_periodo" runat="server" Culture="es-CO" Width="300px"
                                        Enabled="false" DataSourceID="obj_periodo" DataTextField="Periodo" SelectedValue='<%# Bind("NMP_CODIGO") %>'
                                        DataValueField="NMP_CODIGO" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Descripcion</label></td>
                                <td>
                                    <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("NMH_DESCRIPCION") %>' Width="300px" TextMode="MultiLine" Height="50px">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Terceros">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Novedades">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Planilla Nomina">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="pv_terceros" runat="server">
                            <telerik:RadGrid ID="rg_terceros" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_terceros_ItemCommand"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_terceros_NeedDataSource">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                            UniqueName="TRTIPDOC" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>--%>
                                        <telerik:GridTemplateColumn DataField="TRCODNIT" HeaderText="Identificacion" UniqueName="TRCODNIT_TK"
                                            HeaderStyle-Width="150px" AllowFiltering="false" SortExpression="TRCODNIT" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codter" runat="server" Text='<%# Eval("TRCODNIT") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                                            HeaderText="Identificacion" HeaderStyle-Width="150px">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                            UniqueName="TRNOMBRE" Visible="true">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBR3" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="TRNOMBR3" UniqueName="TRNOMBR3" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRNOMBR2" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="TRNOMBR2" UniqueName="TRNOMBR2" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRAPELLI" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="TRAPELLI" UniqueName="TRAPELLI" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="Cargo" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="T_CARGO" UniqueName="T_CARGO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="T_CONTRATO" UniqueName="T_CONTRATO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CT_FINGRESO" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FINGRESO"
                                        UniqueName="CT_FINGRESO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CT_FTERMINACION" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FTERMINACION"
                                            UniqueName="CT_FTERMINACION" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CT_SALARIO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" DataFormatString="{0:C}"
                                            HeaderText="Salario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_SALARIO"
                                            UniqueName="CT_SALARIO" Visible="true">
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
                        <telerik:RadPageView ID="pv_novedades" runat="server">
                            <telerik:RadGrid ID="rg_novedades" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_novedades_ItemCommand"
                                AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_novedades_NeedDataSource">
                                <MasterTableView ShowGroupFooter="true">
                                    <CommandItemSettings />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                            UniqueName="TRTIPDOC" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                            UniqueName="TRCODNIT" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EMPLEADO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                            HeaderText="Tercero" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="EMPLEADO"
                                            UniqueName="EMPLEADO" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NV_VALOR" HeaderButtonType="TextButton"
                                            HeaderStyle-Width="150px" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                            Resizable="true" SortExpression="NV_VALOR" UniqueName="NV_VALOR" Visible="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                            HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                            UniqueName="TTDESCRI">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="D_TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                            HeaderText="Tipo S/R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOSR"
                                            UniqueName="D_TIPOSR">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="D_TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                            HeaderText="Tipo P/V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOPV"
                                            UniqueName="D_TIPOPV">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("NV_BASE")) %>' Enabled="false" />
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
                        <telerik:RadPageView ID="pv_nomina" runat="server">
                            <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="200" Height="500px" TotalsSettings-RowsSubTotalsPosition="None"
                                ID="rp_planilla" runat="server" ColumnHeaderZoneText="ColumnHeaderZone">
                                <ClientSettings EnableFieldsDragDrop="true">
                                    <Scrolling AllowVerticalScroll="true"></Scrolling>
                                </ClientSettings>
                                <Fields>
                                    <telerik:PivotGridRowField DataField="TRCODNIT" Caption="Identificacion" CellStyle-Width="140px">
                                    </telerik:PivotGridRowField>
                                    <telerik:PivotGridRowField DataField="EMPLEADO" Caption="Empleado" CellStyle-Width="250px">
                                    </telerik:PivotGridRowField>
                                    <%--<telerik:PivotGridRowField DataField="ProductName" ZoneIndex="1">
                                </telerik:PivotGridRowField>--%>
                                    <telerik:PivotGridColumnField DataField="ORIGEN" Caption="Origen">
                                    </telerik:PivotGridColumnField>
                                    <telerik:PivotGridColumnField DataField="CONCEPTO" Caption="Concepto">
                                    </telerik:PivotGridColumnField>
                                    <%--<telerik:PivotGridColumnField DataField="Quarter" DataFormatString="Quarter {0}">
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField DataField="TotalPrice" Aggregate="Sum" DataFormatString="{0:C}">
                                </telerik:PivotGridAggregateField>--%>
                                    <telerik:PivotGridAggregateField DataField="NMD_VALOR" Aggregate="Sum" DataFormatString="{0:C}" Caption="Valor">
                                        <ColumnGrandTotalHeaderCellTemplate>
                                            <asp:Label Text="Total" runat="server" />
                                        </ColumnGrandTotalHeaderCellTemplate>
                                    </telerik:PivotGridAggregateField>
                                </Fields>
                                <SortExpressions>
                                    <telerik:PivotGridSortExpression FieldName="Total" SortOrder="Descending"></telerik:PivotGridSortExpression>
                                </SortExpressions>
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger">
                                        <strong>¡No se Encontaron Registros!</strong>
                                    </div>
                                </NoRecordsTemplate>
                            </telerik:RadPivotGrid>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </asp:Panel>
            </ItemTemplate>
            <InsertItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Periodo</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_periodo" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_periodo_SelectedIndexChanged" AutoPostBack="true"
                                    Enabled="true" DataSourceID="obj_periodo" DataTextField="Periodo" SelectedValue='<%# Bind("NMP_CODIGO") %>'
                                    DataValueField="NMP_CODIGO" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td rowspan="2">
                                <telerik:RadButton ID="btn_prceosar" runat="server" Text="Procesar" OnClick="btn_prceosar_Click" Icon-PrimaryIconCssClass="rbNext"
                                    ValidationGroup="UpdateBoton" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="true" Text='<%# Bind("NMH_DESCRIPCION") %>' Width="300px" TextMode="MultiLine" Height="50px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Terceros">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Novedades">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Planilla Nomina">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_terceros" runat="server">
                        <telerik:RadGrid ID="rg_terceros" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_terceros_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_terceros_NeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                        UniqueName="TRTIPDOC" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                        UniqueName="TRCODNIT" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn DataField="TRCODNIT" HeaderText="Identificacion" UniqueName="TRCODNIT_TK"
                                            HeaderStyle-Width="150px" AllowFiltering="false" SortExpression="TRCODNIT" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codter" runat="server" Text='<%# Eval("TRCODNIT") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                                            HeaderText="Identificacion" HeaderStyle-Width="150px">
                                        </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                        UniqueName="TRNOMBRE" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR3" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRNOMBR3" UniqueName="TRNOMBR3" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR2" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRNOMBR2" UniqueName="TRNOMBR2" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRAPELLI" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRAPELLI" UniqueName="TRAPELLI" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="Cargo" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="T_CARGO" UniqueName="T_CARGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="T_CONTRATO" UniqueName="T_CONTRATO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CT_FINGRESO" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F. Inicio" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FINGRESO"
                                        UniqueName="CT_FINGRESO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CT_FTERMINACION" HeaderButtonType="TextButton" HeaderStyle-Width="70px" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderText="F. Final" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_FTERMINACION"
                                        UniqueName="CT_FTERMINACION" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CT_SALARIO" HeaderButtonType="TextButton" HeaderStyle-Width="150px" DataFormatString="{0:C}"
                                        HeaderText="Salario" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="CT_SALARIO"
                                        UniqueName="CT_SALARIO" Visible="true">
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
                    <telerik:RadPageView ID="pv_novedades" runat="server">
                        <telerik:RadGrid ID="rg_novedades" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_novedades_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_novedades_NeedDataSource" OnDeleteCommand="rg_novedades_DeleteCommand">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="NV_CODIGO">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                        UniqueName="TRTIPDOC" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                        UniqueName="TRCODNIT" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EMPLEADO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Tercero" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="EMPLEADO"
                                        UniqueName="EMPLEADO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NV_VALOR" HeaderButtonType="TextButton" DataFormatString="{0:C}"
                                        HeaderStyle-Width="150px" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="NV_VALOR" UniqueName="NV_VALOR" Visible="true">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="D_TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Tipo S/R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOSR"
                                        UniqueName="D_TIPOSR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="D_TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Tipo P/V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOPV"
                                        UniqueName="D_TIPOPV">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("NV_BASE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>Tercero</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="true" Text='<%# Eval("TRCODNIT") %>' Width="300px" Visible="true" OnTextChanged="txt_codter_TextChanged" AutoPostBack="true">
                                                    </telerik:RadTextBox>
                                                    <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Eval("TRCODTER") %>' Width="300px" Visible="false">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <label>Tercero</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Eval("EMPLEADO") %>' Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Concepto</label></td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox ID="rc_concepto" runat="server" Culture="es-CO" Width="300px"
                                                        Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' DataSourceID="obj_concepto" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" SelectedValue='<%# Bind("NV_CONCEPTO") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_concepto" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Tipo</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" SelectedValue='<%# Bind("NV_TIPOSR") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Suma" Value="S" />
                                                            <telerik:RadComboBoxItem Text="Resta" Value="R" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_tipo" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>&/$</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tipopv" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" SelectedValue='<%# Bind("NV_TIPOPV") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Porcentaje" Value="P" />
                                                            <telerik:RadComboBoxItem Text="Valor" Value="V" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_tipopv" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Valor</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_valor" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("NV_VALOR") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Ind Base</label></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_indbase" Checked='<%# this.GetEstado(Eval("NV_BASE")) %>'
                                                        runat="server" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        ValidationGroup="gvInsert" SkinID="SkinUpdateUC" />
                                                    <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
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
                    <telerik:RadPageView ID="pv_nomina" runat="server">
                        <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="200" Height="500px" TotalsSettings-RowsSubTotalsPosition="None"
                            ID="rp_planilla" runat="server" ColumnHeaderZoneText="ColumnHeaderZone">
                            <ClientSettings EnableFieldsDragDrop="true">
                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                            </ClientSettings>
                            <Fields>
                                <telerik:PivotGridRowField DataField="TRCODNIT" Caption="Identificacion" CellStyle-Width="140px">
                                </telerik:PivotGridRowField>
                                <telerik:PivotGridRowField DataField="EMPLEADO" Caption="Empleado" CellStyle-Width="250px">
                                </telerik:PivotGridRowField>
                                <%--<telerik:PivotGridRowField DataField="ProductName" ZoneIndex="1">
                                </telerik:PivotGridRowField>--%>
                                <telerik:PivotGridColumnField DataField="ORIGEN" Caption="Origen">
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridColumnField DataField="CONCEPTO" Caption="Concepto">
                                </telerik:PivotGridColumnField>
                                <%--<telerik:PivotGridColumnField DataField="Quarter" DataFormatString="Quarter {0}">
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField DataField="TotalPrice" Aggregate="Sum" DataFormatString="{0:C}">
                                </telerik:PivotGridAggregateField>--%>
                                <telerik:PivotGridAggregateField DataField="NMD_VALOR" Aggregate="Sum" DataFormatString="{0:C}" Caption="Valor">
                                    <ColumnGrandTotalHeaderCellTemplate>
                                        <asp:Label Text="Total" runat="server" />
                                    </ColumnGrandTotalHeaderCellTemplate>
                                </telerik:PivotGridAggregateField>
                            </Fields>
                            <SortExpressions>
                                <telerik:PivotGridSortExpression FieldName="Total" SortOrder="Descending"></telerik:PivotGridSortExpression>
                            </SortExpressions>
                            <NoRecordsTemplate>
                                <div class="alert alert-danger">
                                    <strong>¡No se Encontaron Registros!</strong>
                                </div>
                            </NoRecordsTemplate>
                        </telerik:RadPivotGrid>
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
            </InsertItemTemplate>
            <EditItemTemplate>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <%--<telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Visible="false" Text='<%# Bind("NMH_CODIGO") %>'>
                                </telerik:RadTextBox>--%>
                        <tr>
                            <td>
                                <label>Periodo</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_periodo" runat="server" Culture="es-CO" Width="300px" OnSelectedIndexChanged="rc_periodo_SelectedIndexChanged" AutoPostBack="true"
                                    Enabled="false" DataSourceID="obj_periodo" DataTextField="Periodo" SelectedValue='<%# Bind("NMP_CODIGO") %>'
                                    DataValueField="NMP_CODIGO" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td rowspan="3">
                                <telerik:RadButton ID="btn_prceosar" runat="server" Text="Procesar" OnClick="btn_prceosar_Click" Icon-PrimaryIconCssClass="rbNext"
                                    ValidationGroup="UpdateBoton" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Estado</label></td>
                            <td>
                                <telerik:RadComboBox ID="rc_Estado" runat="server" Culture="es-CO" Width="300px" SelectedValue='<%# Bind("NMH_ESTADO") %>' AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        <telerik:RadComboBoxItem Text="Pre-Liquidacion" Value="AC" />
                                        <telerik:RadComboBoxItem Text="Definitiva" Value="CE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>                           
                        </tr>
                        <tr>
                            <td>
                                <label>Descripcion</label></td>
                            <td>
                                <telerik:RadTextBox ID="txt_descripcion" runat="server" Enabled="true" Text='<%# Bind("NMH_DESCRIPCION") %>' Width="300px" TextMode="MultiLine" Height="50px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <telerik:RadTabStrip ID="tb_pesatanas" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" CssClass="tabStrip">
                    <Tabs>
                        <telerik:RadTab Text="Terceros">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Novedades">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Planilla Nomina">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                    <telerik:RadPageView ID="pv_terceros" runat="server">
                        <telerik:RadGrid ID="rg_terceros" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_terceros_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_terceros_NeedDataSource">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                        UniqueName="TRTIPDOC" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                        UniqueName="TRCODNIT" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn DataField="TRCODNIT" HeaderText="Identificacion" UniqueName="TRCODNIT_TK"
                                            HeaderStyle-Width="150px" AllowFiltering="false" SortExpression="TRCODNIT" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_codter" runat="server" Text='<%# Eval("TRCODNIT") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn CommandName="link" UniqueName="TRCODNIT" DataTextField="TRCODNIT"
                                            HeaderText="Identificacion" HeaderStyle-Width="150px">
                                        </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="P Nombre" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRNOMBRE"
                                        UniqueName="TRNOMBRE" Visible="true">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR3" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="S Nombre" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRNOMBR3" UniqueName="TRNOMBR3" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRNOMBR2" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="P Apellido" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRNOMBR2" UniqueName="TRNOMBR2" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRAPELLI" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="S Apellido" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="TRAPELLI" UniqueName="TRAPELLI" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="T_CARGO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="Cargo" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="T_CARGO" UniqueName="T_CARGO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="T_CONTRATO" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="T Contrato" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="T_CONTRATO" UniqueName="T_CONTRATO" Visible="true">
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
                    <telerik:RadPageView ID="pv_novedades" runat="server">
                        <telerik:RadGrid ID="rg_novedades" runat="server" GridLines="None" Width="100%" RenderMode="Lightweight" OnItemCommand="rg_novedades_ItemCommand"
                            AutoGenerateColumns="False" Culture="(Default)" CellSpacing="0" OnNeedDataSource="rg_novedades_NeedDataSource" OnDeleteCommand="rg_novedades_DeleteCommand">
                            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top" DataKeyNames="NV_CODIGO">
                                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridButtonColumn ConfirmText="Desea Quita Este Item?" ConfirmDialogType="RadWindow" ConfirmTitle="Delete"
                                        ButtonType="ImageButton" CommandName="Delete" HeaderStyle-Width="20px" />
                                    <telerik:GridBoundColumn DataField="TRTIPDOC" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="T Doc" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRTIPDOC"
                                        UniqueName="TRTIPDOC" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TRCODNIT" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Identificacion" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TRCODNIT"
                                        UniqueName="TRCODNIT" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EMPLEADO" HeaderButtonType="TextButton" HeaderStyle-Width="150px"
                                        HeaderText="Tercero" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="EMPLEADO"
                                        UniqueName="EMPLEADO" Visible="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NV_VALOR" HeaderButtonType="TextButton"
                                        HeaderStyle-Width="150px" HeaderText="Valor" ItemStyle-HorizontalAlign="Right"
                                        Resizable="true" SortExpression="NV_VALOR" UniqueName="NV_VALOR" Visible="true">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="TTDESCRI" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Concepto" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TTDESCRI"
                                        UniqueName="TTDESCRI">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="D_TIPOSR" HeaderButtonType="TextButton" HeaderStyle-Width="90px"
                                        HeaderText="Tipo S/R" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOSR"
                                        UniqueName="D_TIPOSR">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="D_TIPOPV" HeaderButtonType="TextButton" HeaderStyle-Width="200px"
                                        HeaderText="Tipo P/V" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="D_TIPOPV"
                                        UniqueName="D_TIPOPV">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="A. Base" UniqueName="cbase" HeaderStyle-Width="40px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_base" runat="server" Checked='<%# this.GetEstado(Eval("NV_BASE")) %>' Enabled="false" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <label>Tercero</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_nit" runat="server" Enabled="true" Text='<%# Eval("TRCODNIT") %>' Width="300px" Visible="true" OnTextChanged="txt_codter_TextChanged" AutoPostBack="true">
                                                    </telerik:RadTextBox>
                                                    <telerik:RadTextBox ID="txt_codter" runat="server" Enabled="false" Text='<%# Eval("TRCODTER") %>' Width="300px" Visible="false">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td>
                                                    <label>Tercero</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txt_tercero" runat="server" Enabled="false" Text='<%# Eval("EMPLEADO") %>' Width="300px">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Concepto</label></td>
                                                <td colspan="3">
                                                    <telerik:RadComboBox ID="rc_concepto" runat="server" Culture="es-CO" Width="300px"
                                                        Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' DataSourceID="obj_concepto" DataTextField="TTDESCRI" DataValueField="TTCODCLA" AppendDataBoundItems="true" SelectedValue='<%# Bind("NV_CONCEPTO") %>'>
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rc_concepto" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Tipo</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tipo" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" SelectedValue='<%# Bind("NV_TIPOSR") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Suma" Value="S" />
                                                            <telerik:RadComboBoxItem Text="Resta" Value="R" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rc_tipo" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <label>&/$</label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rc_tipopv" runat="server" Culture="es-CO" Width="300px" ZIndex="1000000"
                                                        Enabled="true" SelectedValue='<%# Bind("NV_TIPOPV") %>'
                                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                                            <telerik:RadComboBoxItem Text="Porcentaje" Value="P" />
                                                            <telerik:RadComboBoxItem Text="Valor" Value="V" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rc_tipopv" InitialValue="Seleccionar"
                                                        ErrorMessage="(*)" ValidationGroup="gvInsert">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Valor</label></td>
                                                <td>
                                                    <telerik:RadNumericTextBox runat="server" ID="txt_valor" Width="300px" Enabled="true" EnabledStyle-HorizontalAlign="Right" DbValue='<%# Bind("NV_VALOR") %>'>
                                                    </telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>Ind Base</label></td>
                                                <td>
                                                    <asp:CheckBox ID="chk_indbase" Checked='<%# this.GetEstado(Eval("NV_BASE")) %>'
                                                        runat="server" Enabled="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btn_aceptar" runat="server" Text="Aceptar" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                        ValidationGroup="gvInsert" SkinID="SkinUpdateUC" />
                                                    <asp:ImageButton ID="btn_cancelar" runat="server" CommandName="Cancel" SkinID="SkinCancelUC"
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
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
                    <telerik:RadPageView ID="pv_nomina" runat="server">
                        <telerik:RadPivotGrid RenderMode="Lightweight" AllowPaging="true" PageSize="200" Height="500px" TotalsSettings-RowsSubTotalsPosition="None"
                            ID="rp_planilla" runat="server" ColumnHeaderZoneText="ColumnHeaderZone">
                            <ClientSettings EnableFieldsDragDrop="true">
                                <Scrolling AllowVerticalScroll="true"></Scrolling>
                            </ClientSettings>
                            <Fields>
                                <telerik:PivotGridRowField DataField="TRCODNIT" Caption="Identificacion" CellStyle-Width="140px">
                                </telerik:PivotGridRowField>
                                <telerik:PivotGridRowField DataField="EMPLEADO" Caption="Empleado" CellStyle-Width="250px">
                                </telerik:PivotGridRowField>
                                <%--<telerik:PivotGridRowField DataField="ProductName" ZoneIndex="1">
                                </telerik:PivotGridRowField>--%>
                                <telerik:PivotGridColumnField DataField="ORIGEN" Caption="Origen">
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridColumnField DataField="CONCEPTO" Caption="Concepto">
                                </telerik:PivotGridColumnField>
                                <%--<telerik:PivotGridColumnField DataField="Quarter" DataFormatString="Quarter {0}">
                                </telerik:PivotGridColumnField>
                                <telerik:PivotGridAggregateField DataField="TotalPrice" Aggregate="Sum" DataFormatString="{0:C}">
                                </telerik:PivotGridAggregateField>--%>
                                <telerik:PivotGridAggregateField DataField="NMD_VALOR" Aggregate="Sum" DataFormatString="{0:C}" Caption="Valor">
                                    <ColumnGrandTotalHeaderCellTemplate>
                                        <asp:Label Text="Total" runat="server" />
                                    </ColumnGrandTotalHeaderCellTemplate>
                                </telerik:PivotGridAggregateField>
                            </Fields>
                            <SortExpressions>
                                <telerik:PivotGridSortExpression FieldName="Total" SortOrder="Descending"></telerik:PivotGridSortExpression>
                            </SortExpressions>
                            <NoRecordsTemplate>
                                <div class="alert alert-danger">
                                    <strong>¡No se Encontaron Registros!</strong>
                                </div>
                            </NoRecordsTemplate>
                        </telerik:RadPivotGrid>
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
            </EditItemTemplate>
        </telerik:RadListView>
        <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true" Style="z-index: 7001;">
            <Shortcuts>
                <telerik:WindowShortcut CommandName="RestoreAll" Shortcut="Alt+F3"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="Tile" Shortcut="Alt+F6"></telerik:WindowShortcut>
                <telerik:WindowShortcut CommandName="CloseAll" Shortcut="Esc"></telerik:WindowShortcut>
            </Shortcuts>
            <Windows>
                <telerik:RadWindow RenderMode="Lightweight" ID="mpMensaje" runat="server" Width="700px" Height="320px" Modal="true" OffsetElementID="main" Style="z-index: 100001;" Title="Mensaje">
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

    <asp:ObjectDataSource ID="obj_planillanmHD" runat="server" OldValuesParameterFormatString="original_{0}" OnInserting="obj_planillanmHD_Inserting" OnUpdating="obj_planillanmHD_Updating"
        SelectMethod="GetPlanillaNominaHD" TypeName="XUSS.BLL.Nomina.LiquidacionNominaBL" InsertMethod="InsertPlanillaNomina" UpdateMethod="UpdatePlanillaNomina">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=0" />
            <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="0" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="NMP_CODIGO" Type="String" />
            <asp:Parameter Name="NMH_DESCRIPCION" Type="String" />
            <asp:SessionParameter Name="NMH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="NMH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="inDT" Type="Object" />
            <asp:Parameter Name="inNovedades" Type="Object" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />            
            <asp:Parameter Name="NMP_CODIGO" Type="String" />
            <asp:Parameter Name="NMH_DESCRIPCION" Type="String" />
            <asp:SessionParameter Name="NMH_USUARIO" Type="String" SessionField="UserLogon" />
            <asp:Parameter Name="NMH_ESTADO" Type="String" DefaultValue="AC" />
            <asp:Parameter Name="original_NMH_CODIGO" Type="Int32" />
            <asp:Parameter Name="inDT" Type="Object" />
            <asp:Parameter Name="inNovedades" Type="Object" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_periodo" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPeriodoNomina" TypeName="XUSS.BLL.Nomina.PeriodosNominaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="filter" Type="String" DefaultValue="1=1" />
            <asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="0" />
            <asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obj_concepto" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTbTablaLista" TypeName="XUSS.BLL.Comun.ComunBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="TTCODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="TTCODTAB" Type="String" DefaultValue="NMCONC" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
