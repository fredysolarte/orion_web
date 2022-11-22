<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="SqlExplorer.aspx.cs" Inherits="XUSS.WEB.Consultas.SqlExplorer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="1000000">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <fieldset class="cssFieldSetContainer">
        <div class="box">
            <div class="title">
                <h5>
                    Sql Explorer</h5>
            </div>
        </div>
        <fieldset>
            <asp:ImageButton ID="ibt_ejecutar" runat="server" SkinID="SkinFindUC" OnClick="ibt_ejecutar_OnClick" />
        </fieldset>
        <asp:Panel runat="server" ID="pnlGeneral" Style="padding: 5px 5px 5px 5px" Width="960px">
            <telerik:RadTextBox ID="txt_editor" runat="server" TextMode="MultiLine" Width="940px"
                Height="300px">
            </telerik:RadTextBox>
        </asp:Panel>
        <asp:Panel runat="server" ID="Panel1" Style="padding: 5px 5px 5px 5px" Width="960PX">
            <telerik:RadGrid ID="rg_consulta" runat="server" Width="960PX" AllowPaging="True"
                GridLines="None" AutoGenerateColumns="False" OnExcelMLExportRowCreated="RadGrid1_ExcelMLExportRowCreated"
						OnExcelMLExportStylesCreated="RadGrid1_ExcelMLExportStylesCreated">
						<ExportSettings IgnorePaging="true" OpenInNewWindow="true" ExportOnlyData="True">
							<Excel Format="ExcelML" />
						</ExportSettings>
                <MasterTableView CommandItemDisplay="Top" Width="100%" >
                    <NoRecordsTemplate>
                        <div id="message" runat="server">
                            <div id="box-messages" class="box">
                                <div class="messages">
                                    <div id="message-notice" class="message message-notice">
                                        <div class="image">
                                            <img src="/App_Themes/Tema2/resources/images/icons/notice.png" alt="Notice" height="32" />
                                        </div>
                                        <div class="text">
                                            <h6>
                                                Información</h6>
                                            <span>No existen Resultados </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </NoRecordsTemplate>
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" />
							<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
							</RowIndicatorColumn>
							<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
							</ExpandCollapseColumn>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                </ClientSettings>
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Web20">
				</HeaderContextMenu>
            </telerik:RadGrid>
        </asp:Panel>
    </fieldset>
    <div style="display: none">
        <asp:Button ID="Button5" runat="server" />
    </div>
    <cc1:ModalPopupExtender ID="mpMensajes" runat="server" PopupControlID="pnlAlertMensaje"
        TargetControlID="Button5" BackgroundCssClass="modalBackground" CancelControlID="Button6">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlAlertMensaje" runat="server" CssClass="modalPopup" Style="display: none;">
        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
            <div class="box">
                <div class="title">
                    <h5>
                        <asp:Label runat="server" ID="LitTitulo"></asp:Label>
                    </h5>
                </div>
            </div>
            <div style="padding: 5px 5px 5px 5px">
                <ul>
                    <asp:Literal runat="server" ID="litTextoMensaje"></asp:Literal>
                </ul>
                <div style="text-align: center;">
                    <asp:Button ID="Button6" runat="server" Text="Cerrar" />
                </div>
            </div>
        </fieldset>
    </asp:Panel>


    <div style="display: none">
        <asp:Button ID="Button1" runat="server" />
    </div>
    <cc1:ModalPopupExtender ID="mp_confirmacion" runat="server" PopupControlID="pnlConfirmacion"
        TargetControlID="Button1" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrar">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmacion" runat="server" CssClass="modalPopup" Style="display: none;">
        <fieldset class="cssFieldSetContainer" style="width: auto !important;">
            <div class="box">
                <div class="title">
                    <h5>
                        <asp:Label runat="server" ID="Label1">Confirmacion</asp:Label>
                    </h5>
                </div>
            </div>
            <div style="padding: 5px 5px 5px 5px">
                <ul>
                    <asp:Literal runat="server" ID="Literal1">Esta Seguro de Ejecutar esta Sentencia?</asp:Literal>
                </ul>
                <div style="text-align: center;">
                    <asp:Button ID="btn_aceptar" runat="server" Text="Aceptar" OnClick="btn_aceptar_OnClick"/>
                    <asp:Button ID="btn_cerrar" runat="server" Text="Cerrar" OnClick="btn_cerrar_OnClick"/>
                </div>
            </div>
        </fieldset>
    </asp:Panel>
    </telerik:RadAjaxPanel>
</asp:Content>
