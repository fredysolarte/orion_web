<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="PeriodosNomina.aspx.cs" Inherits="XUSS.WEB.Nomina.PeriodosNomina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <telerik:RadListView ID="rlv_periodo" runat="server" PageSize="1" AllowPaging="True"
            Width="100%" OnItemCommand="rlv_tpedido_OnItemCommand" OnItemDataBound="rlv_tpedido_OnItemDataBound"
            DataSourceID="obj_periodo" ItemPlaceholderID="pnlGeneral">
            <LayoutTemplate>
                <fieldset class="cssFieldSetContainer">
                    <div class="box">
                        <div class="title">
                            <h5>Periodo Nomina</h5>
                        </div>
                    </div>
                    <div class="paginadorRadListView">
                        <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="rlv_periodo" RenderMode="Lightweight"
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
                                <h5>Periodo Nomina</h5>
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
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_buscar" runat="server" Text="Buscar" Icon-PrimaryIconCssClass="rbSearch" CommandName="Buscar" ToolTip="Buscar" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_agregar" runat="server" Text="Nuevo" Icon-PrimaryIconCssClass="rbAdd" CommandName="InitInsert" ToolTip="Nuevo Registro" />
                    <telerik:RadButton RenderMode="Lightweight" ID="btn_editar" runat="server" Text="Editar" Icon-PrimaryIconCssClass="rbEdit" CommandName="Edit" ToolTip="Editar Registro" />
                </div>
                <asp:Panel ID="pnItemMaster" runat="server">
                    <table>
                        <tr>
                            <td>
                                <label>Fecha Inicial</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecini" runat="server" DbSelectedDate='<%# Bind("NMP_FECINI") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                <label>Fecha Final</label></td>
                            <td>
                                <telerik:RadDatePicker ID="txt_fecfin" runat="server" DbSelectedDate='<%# Bind("NMP_FECFIN") %>'
                                    MinDate="01/01/1900" Enabled="false">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Codigo</label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txt_codigo" runat="server" Enabled="false" Text='<%# Bind("NMP_CODIGO") %>' Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Fecha Inicial</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecini" runat="server" DbSelectedDate='<%# Bind("NMP_FECINI") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>Fecha Final</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecfin" runat="server" DbSelectedDate='<%# Bind("NMP_FECFIN") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="Update" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </EditItemTemplate>
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td>
                            <label>Fecha Inicial</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecini" runat="server" DbSelectedDate='<%# Bind("NMP_FECINI") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <label>Fecha Final</label></td>
                        <td>
                            <telerik:RadDatePicker ID="txt_fecfin" runat="server" DbSelectedDate='<%# Bind("NMP_FECFIN") %>'
                                MinDate="01/01/1900" Enabled="true">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnInsert" runat="server" CommandName="PerformInsert" Text="Aceptar" ValidationGroup="gvInsert" Icon-PrimaryIconCssClass="rbSave" ToolTip="Guardar" RenderMode="Lightweight" />
                            <telerik:RadButton ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancelar" Icon-PrimaryIconCssClass="rbCancel" ToolTip="Cancelar" RenderMode="Lightweight" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </telerik:RadListView>
    </telerik:RadAjaxPanel>
    <asp:ObjectDataSource ID="obj_periodo" runat="server" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertPeriodoNomina" UpdateMethod="UpdatePeriodoNomina"
        SelectMethod="GetPeriodoNomina" TypeName="XUSS.BLL.Nomina.PeriodosNominaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=0" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:SessionParameter Name="NMP_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="NMP_FECINI" Type="DateTime" />
            <asp:Parameter Name="NMP_FECFIN" Type="String" />
            <asp:Parameter Name="NMP_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="NMP_USUARIO" Type="String" SessionField="UserLogon" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter Name="NMP_CODIGO" Type="Int32" />
            <asp:SessionParameter Name="NMP_CODEMP" Type="String" SessionField="CODEMP" />
            <asp:Parameter Name="NMP_FECINI" Type="DateTime" />
            <asp:Parameter Name="NMP_FECFIN" Type="String" />
            <asp:Parameter Name="NMP_ESTADO" Type="String" DefaultValue="AC" />
            <asp:SessionParameter Name="NMP_USUARIO" Type="String" SessionField="UserLogon" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
