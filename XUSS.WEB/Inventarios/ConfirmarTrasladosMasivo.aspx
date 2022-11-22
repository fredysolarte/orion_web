<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true" CodeBehind="ConfirmarTrasladosMasivo.aspx.cs" Inherits="XUSS.WEB.Inventarios.ConfirmarTrasladosMasivo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptBlock ID="rsbDownLoad" runat="server">
        <style type="text/css">
            .RadForm.rfdHeading h4, .RadForm.rfdHeading h5, .RadForm.rfdHeading h6 {
                padding: 0px 10px 10px 30px;
            }
            .RadWindow_Bootstrap .rwTitleBar {
                border-color: #25A0DA;
                color: #333;
                background-color: #25A0DA;
                margin: 0;
                border-radius: 4px 4px 0 0;
            }
        </style>       
    </telerik:RadScriptBlock>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Default, Textbox, Textarea, Label, H4H5H6, Select, Zone, GridFormDetailsViews" />
        <fieldset class="cssFieldSetContainer">
            <div class="box">
                <div class="title">
                    <h5>Confirmacion Traslados</h5>
                </div>
            </div>
        </fieldset>
        <fieldset class="cssFieldSetContainer">
            <asp:Panel ID="pnVacio" runat="server" DefaultButton="btn_buscar">
                <div>
                    <table>
                        <tr>
                            <td>
                                <label>Bodega Destino</label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rc_otbodega" runat="server" Culture="es-CO" Width="300px"
                                    DataSourceID="obj_bodega" DataTextField="BDNOMBRE" AllowCustomText="true" Filter="Contains"
                                    DataValueField="BDBODEGA" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Seleccionar" Value="" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rqf_bingreso" runat="server" ControlToValidate="rc_otbodega" ValidationGroup="gvBuscar"
                                    ErrorMessage="(*)" InitialValue="Seleccionar">
                                    <asp:Image ID="Image9" runat="server" ImageUrl="~/App_Themes/Tema2/Images/Cancel.gif" />
                                </asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <telerik:RadButton ID="btn_buscar" runat="server" Text="Aceptar" OnClick="btn_buscar_Click" Icon-PrimaryIconCssClass="rbSearch"
                                    ValidationGroup="gvBuscar" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btn_guardar" runat="server" Text="Guradar" OnClick="btn_guardar_Click" Icon-PrimaryIconCssClass="rbSave"
                                    ValidationGroup="gvBuscar" CausesValidation="true" RenderMode="Lightweight">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <telerik:RadGrid ID="rgDetalle" runat="server" Width="100%" AutoGenerateColumns="false" ShowGroupPanel="false" OnItemCommand="rgDetalle_ItemCommand"
                RenderMode="Lightweight" Culture="(Default)" AllowFilteringByColumn="false" OnNeedDataSource="rgDetalle_NeedDataSource"
                ShowFooter="True" Height="650px">
                <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                    <Selecting AllowRowSelect="True"></Selecting>
                    <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                        ResizeGridOnColumnResize="False"></Resizing>
                </ClientSettings>
                <MasterTableView ShowGroupFooter="false">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="" UniqueName="clote" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_habilita" runat="server"  Enabled="true" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="TSNROTRA" HeaderText="Nro Traslado" UniqueName="TSNROTRA_TK"
                            HeaderStyle-Width="130px" AllowFiltering="false" SortExpression="TSNROTRA" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_traslado" runat="server" Text='<%# Eval("TSNROTRA") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridButtonColumn CommandName="link_traslado" UniqueName="TSNROTRA" DataTextField="TSNROTRA"
                            HeaderText="Traslado" HeaderStyle-Width="100px">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="BDNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Bodega Out" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="BDNOMBRE"
                            UniqueName="BDNOMBRE">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OTNOMBRE" HeaderButtonType="TextButton" HeaderStyle-Width="100px"
                            HeaderText="Bodega In" ItemStyle-HorizontalAlign="Right" Resizable="true" SortExpression="OTNOMBRE"
                            UniqueName="OTNOMBRE">
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
    <asp:ObjectDataSource ID="obj_bodega" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetBodegas" TypeName="XUSS.BLL.Parametros.BodegaBL">
        <SelectParameters>
            <asp:Parameter Name="connection" Type="String" />
            <asp:Parameter DefaultValue="1=1" Name="filter" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
