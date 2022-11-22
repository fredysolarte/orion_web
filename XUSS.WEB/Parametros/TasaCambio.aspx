<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="TasaCambio.aspx.cs" Inherits="XUSS.WEB.Parametros.TasaCambio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Tasas de Cambio</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
        <telerik:RadGrid ID="rg_items" runat="server" GridLines="None" Width="100%" AutoGenerateColumns="False" DataSourceID="obj_tasacambio" OnItemCommand="rg_items_ItemCommand"
            Culture="(Default)" CellSpacing="0" ShowFooter="True" AllowPaging="true" PageSize="50" AllowFilteringByColumn="true">
            <MasterTableView ShowGroupFooter="true" InsertItemDisplay="Top" CommandItemDisplay="Top">
                <CommandItemSettings AddNewRecordText="Nuevo Item" ShowRefreshButton="false" />
                <Columns>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Edit" HeaderStyle-Width="20px" />
                    <telerik:GridBoundColumn DataField="TC_FECHA" HeaderButtonType="TextButton" HeaderStyle-Width="100px" DataFormatString="{0:MM/dd/yyyy}"
                        HeaderText="Fecha" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TC_FECHA"
                        UniqueName="TC_FECHA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TC_MONEDA" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Moneda" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="TC_MONEDA"
                        UniqueName="TC_MONEDA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TC_VALOR" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                        HeaderText="Costo" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"
                        Resizable="true" SortExpression="TC_VALOR" UniqueName="TC_VALOR">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                </Columns>
                <NoRecordsTemplate>
                    <div class="alert alert-danger">
                        <strong>¡No se Encontaron Registros!</strong>
                    </div>
                </NoRecordsTemplate>
                <EditFormSettings EditFormType="Template">
                    <FormTemplate>
                        <table>
                            <tr>
                                <td>
                                    <label>Fecha</label></td>
                                <td>
                                    <telerik:RadDatePicker ID="edt_fSolicitud" runat="server" DbSelectedDate='<%# Bind("TC_FECHA") %>'
                                        MinDate="01/01/1900" Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>'>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Moneda</label></td>
                                <td>
                                    <telerik:RadComboBox ID="rc_moneda" runat="server" Culture="es-CO" Width="300px"
                                        Enabled='<%# (Container is GridEditFormInsertItem) ? true : false %>' DataSourceID="obj_moneda" DataTextField="TTDESCRI" SelectedValue='<%# Bind("TC_MONEDA") %>'
                                        DataValueField="TTCODCLA" AppendDataBoundItems="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Seleccionar" Value="-1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>Valor</label></td>
                                <td>
                                    <telerik:RadNumericTextBox runat="server" ID="txt_valor" Width="250px" Enabled="true"
                                        DbValue='<%# Bind("TC_VALOR") %>' EnabledStyle-HorizontalAlign="Right">
                                    </telerik:RadNumericTextBox>
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
    <asp:ObjectDataSource ID="obj_tasacambio" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTasas" TypeName="XUSS.BLL.Parametros.TasaCambioBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter DefaultValue="01/01/1987" Name="inFecha" Type="DateTime" />
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
